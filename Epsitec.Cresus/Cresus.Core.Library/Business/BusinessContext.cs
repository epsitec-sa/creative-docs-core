﻿//	Copyright © 2010-2013, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Pierre ARNAUD, Maintainer: Pierre ARNAUD

using Epsitec.Common.Support;
using Epsitec.Common.Support.EntityEngine;
using Epsitec.Common.Support.Extensions;
using Epsitec.Common.Types;
using Epsitec.Common.Types.Collections;

using Epsitec.Cresus.Core.Controllers;
using Epsitec.Cresus.Core.Data;
using Epsitec.Cresus.Core.Entities;
using Epsitec.Cresus.Core.Library;
using Epsitec.Cresus.Core.Orchestrators.Navigation;

using Epsitec.Cresus.DataLayer.Context;

using System.Collections.Generic;
using System.Linq;

namespace Epsitec.Cresus.Core.Business
{
	public sealed class BusinessContext : IIsDisposed, ICoreManualComponent
	{
		public BusinessContext(CoreData data, bool enableReload)
		{
			this.pool     = data.GetComponent<BusinessContextPool> ();
			this.UniqueId = System.Threading.Interlocked.Increment (ref BusinessContext.nextUniqueId);
			
			this.pool.Add (this);
			
			this.entityRecords      = new List<EntityRecord> ();
			this.masterEntities     = new List<AbstractEntity> ();
			this.delayedUpdates     = new Stack<DelayedUpdate> ();
			this.dataChangedCounter = new SafeCounter ();
			this.lockMonitors       = new List<LockMonitor> ();

			this.enableReload = enableReload;

			this.data        = this.pool.Host;
			this.dataContext = this.pool.CreateDataContext (this, enableReload);
			
			this.dataContext.EntityChanged += this.HandleDataContextEntityChanged;
			
			this.locker = this.Data.DataLocker;
		}


		public int								UniqueId
		{
			get;
			private set;
		}

		public DataContext						DataContext
		{
			get
			{
				return this.dataContext;
			}
		}

		public CoreData							Data
		{
			get
			{
				return this.data;
			}
		}

		public bool								IsLocked
		{
			get
			{
				return this.lockTransaction != null;
			}
		}

		public bool								IsDiscarded
		{
			get
			{
				return this.dataContextDiscarded;
			}
		}

		public bool								EnableReload
		{
			get
			{
				return this.enableReload;
			}
		}

		public bool								IsEmpty
		{
			get
			{
				return (this.activeEntity == null)
					&& (this.entityRecords.Count == 0)
					&& (this.activeNavigationPathElement == null);
			}
		}

		public AbstractEntity					ActiveEntity
		{
			get
			{
				return this.activeEntity;
			}
		}

		public NavigationPathElement			ActiveNavigationPathElement
		{
			get
			{
				return this.activeNavigationPathElement;
			}
		}

		public GlobalLock						GlobalLock
		{
			get
			{
				return this.globalLock;
			}
			set
			{
				if (this.IsLocked)
				{
					throw new System.InvalidOperationException ();
				}

				this.globalLock = value;
			}
		}

		#region IIsDisposed Members
		
		public bool IsDisposed
		{
			get
			{
				return this.isDisposed;
			}
		}

		#endregion

		
		public bool ContainsChanges()
		{
			if ((this.isDisposed) ||
				(this.IsDiscarded))
			{
				return false;
			}
			else
			{
				return this.hasExternalChanges || this.dataContext.ContainsChanges ();
			}
		}

		public bool AreAllLocksAvailable()
		{
			string[] lockNames = this.GetLockNames ().ToArray ();

			if (lockNames.Length == 0)
			{
				return true;
			}
			else
			{
				return this.locker.AreAllLocksAvailable (lockNames);
			}
		}

		public void NotifyExternalChanges()
		{
			if (this.hasExternalChanges == false)
			{
				this.hasExternalChanges = true;
				this.OnContainsChangesChanged ();
			}
		}

		public void NotifyUIChanges()
		{
			this.OnRefreshUIRequested ();
		}


		/// <summary>
		/// Gets the cached entity.
		/// Warning: the entity might not belong to this context and will be read-only.
		/// </summary>
		/// <returns>The cached entity.</returns>
		public T GetCached<T>()
			where T : AbstractEntity, new ()
		{
			return this.GetAllEntities<T> ().FirstOrDefault ()
				?? this.dataContext.CreateNullEntity<T> (freeze: true);
		
		}

		public void AssignIds<T>(T entity, RefIdGeneratorPool generatorPool, IEnumerable<GeneratorDefinitionEntity> generators)
			where T : AbstractEntity, IReferenceNumber, new ()
		{
			var generator = new FormattedIdGenerator (generatorPool, generators);

			if (generator.AssignIds (this, entity) == false)
			{
				entity.IdA = string.Format ("{0:000000}", generatorPool.GetGenerator<T> ().GetNextId ());
			}
		}

		public IEnumerable<T> GetAllEntities<T>()
			where T : AbstractEntity, new ()
		{
			return this.data.GetAllEntities<T> (dataContext: this.dataContext);
		}


		/// <summary>
		/// Acquires the lock (as defined by the <see cref="BusinessContext.GlobalLock"/> and
		/// <see cref="BusinessContext.ActiveEntity"/> properties).
		/// </summary>
		/// <returns><c>true</c> if the lock could be acquired; otherwise, <c>false</c>.</returns>
		public bool AcquireLock()
		{
			IList<LockOwner> foreignLockOwners;
			return this.AcquireLock (out foreignLockOwners);
		}

		/// <summary>
		/// Acquires the lock (as defined by the <see cref="BusinessContext.GlobalLock"/> and
		/// <see cref="BusinessContext.ActiveEntity"/> properties).
		/// </summary>
		/// <param name="foreignLockOwners">The foreign lock owners, if the lock acquisition failed.</param>
		/// <returns><c>true</c> if the lock could be acquired; otherwise, <c>false</c>.</returns>
		public bool AcquireLock(out IList<LockOwner> foreignLockOwners)
		{
			if (this.IsLocked)
			{
				foreignLockOwners = EmptyList<LockOwner>.Instance;
				return true;
			}

			string[] lockNames = this.GetLockNames ().Where (name => !string.IsNullOrEmpty (name)).ToArray ();

			if (lockNames.Length == 0)
			{
				foreignLockOwners = EmptyList<LockOwner>.Instance;
				return false;
			}

			var lockTransaction = this.locker.RequestLock (lockNames, out foreignLockOwners);

			if (lockTransaction == null)
			{
				return false;
			}

			if (lockTransaction.LockSate == DataLayer.Infrastructure.LockState.Locked)
			{
				this.lockTransaction = lockTransaction;
				this.OnLockAcquired ();

				return true;
			}
			else
			{
				lockTransaction.Dispose ();
				return false;
			}
		}

		/// <summary>
		/// Releases the lock acquired by <see cref="BusinessContext.AcquireLock"/>.
		/// </summary>
		/// <returns></returns>
		internal bool ReleaseLock()
		{
			if (this.IsLocked == false)
			{
				return false;
			}

			System.Diagnostics.Debug.WriteLine ("*** LOCK RELEASED ***");
			this.lockTransaction.Dispose ();
			this.lockTransaction = null;

			return true;
		}

		public LockMonitor CreateLockMonitor()
		{
			var lockMonitor = this.Data.DataLocker.CreateLockMonitor (this.GetLockNames ());
			
			lockMonitor.Disposed += m => this.lockMonitors.Remove (m as LockMonitor);

			this.lockMonitors.Add (lockMonitor);

			return lockMonitor;
		}
		
		public System.IDisposable AutoLock<T>(T entity)
			where T : AbstractEntity
		{
			if (this.IsLocked)
			{
				return new NoOpUnlocker (this);
			}
			if (entity.IsNull ())
			{
				throw new System.ArgumentNullException ("Cannot acquire lock on null entity");
			}

			this.lockEntity = entity;

			if (this.AcquireLock ())
			{
				return new Unlocker (this);
			}

			throw new System.InvalidOperationException ("Could not acquire lock");
		}


		public Logic CreateLogic(AbstractEntity entity)
		{
			return new Logic (entity, this, this.data.GetComponent<RefIdGeneratorPool> ());
		}

		public void SetActiveEntity(EntityKey? entityKey, NavigationPathElement navigationPathElement = null)
		{
			System.Diagnostics.Debug.Assert (this.activeEntity == null);
			System.Diagnostics.Debug.Assert (this.activeNavigationPathElement == null);

			this.SetActiveEntity (this.dataContext.ResolveEntity (entityKey));
			this.SetNavigationPathElement (navigationPathElement);
		}

		public T SetActiveEntity<T>(EntityKey? entityKey, NavigationPathElement navigationPathElement = null)
			where T : AbstractEntity
		{
			this.SetActiveEntity (entityKey, navigationPathElement);
			return this.ActiveEntity as T;
		}

		public T ResolveEntity<T>(EntityKey? entityKey)
			where T : AbstractEntity
		{
			return (T) this.DataContext.ResolveEntity (entityKey);
		}

		/// <summary>
		/// Adds a master entity of a specific type. See <seealso cref="GerMasterEntity{T}"/>.
		/// </summary>
		/// <param name="masterEntity">The master entity.</param>
		public void AddMasterEntity(AbstractEntity masterEntity)
		{
			if (this.masterEntities.Contains (masterEntity))
			{
				//	The master entity is already known. Add it, but don't count this as a change
				//	in the list of master entities.
				
				this.masterEntities.Add (masterEntity);
			}
			else
			{
				this.masterEntities.Add (masterEntity);
				this.OnMasterEntitiesChanged ();
			}
		}

		/// <summary>
		/// Removes a master entity of a specific type. See <seealso cref="GerMasterEntity{T}"/>.
		/// </summary>
		/// <param name="masterEntity">The master entity.</param>
		public void RemoveMasterEntity(AbstractEntity masterEntity)
		{
			int pos = this.masterEntities.FindLastIndex (x => x == masterEntity);

			if (pos < 0)
			{
				throw new System.ArgumentException ("The specified master entity is not known in this context");
			}

			this.masterEntities.RemoveAt (pos);

			if (this.masterEntities.Contains (masterEntity))
			{
				//	The master entity was removed, but it is still known as such; this means that
				//	it was added several times into the list. Don't notify anybody about this non-
				//	change.
			}
			else
			{
				this.OnMasterEntitiesChanged ();
			}
		}

		/// <summary>
		/// Gets the master entity of type <typeparamref name="T"/>. A master entity is an entity
		/// for which there is currently an open piece of UI, or simply an entity which is a key
		/// element in a graph (for instance the 'active' affair for a customer).
		/// </summary>
		/// <typeparam name="T">Type of the entity.</typeparam>
		/// <returns>The master entity of type <typeparamref name="T"/> if it exists; otherwise, returns a wrapped null entity.</returns>
		public T GetMasterEntity<T>()
			where T : AbstractEntity, new ()
		{
			//	Either use the most recent entity on which the business logic is currently
			//	operating, or the freshest entity defined as a master entity.
			
			T master;

			if (Logic.Current == null)
			{
				master = null;
			}
			else
			{
				master = Logic.Current.Find<T> ().FirstOrDefault ();
			}

			if (master == null)
			{
				master = this.masterEntities.OfType<T> ().LastOrDefault ();
			}

			if (master == null)
			{
				//	There is no defined master entity in the current context; try to derive it
				//	from the active entities.

				var entities = this.DataContext.GetEntities ().OfType<T> ();
				
				foreach (var entity in entities)
				{
					if (master != null)
					{
						throw new System.InvalidOperationException ("More than one entity of type " + typeof (T).Name);
					}

					master = entity;
				}
			}

			return this.dataContext.WrapNullEntity (master);
		}

		/// <summary>
		/// Gets the master entities, i.e. those which are currently displayed in the UI.
		/// </summary>
		/// <returns>A collection of master entities.</returns>
		public IEnumerable<AbstractEntity> GetMasterEntities()
		{
			if (Logic.Current == null)
			{
				return this.masterEntities.Distinct ();
			}
			else
			{
				return Logic.Current.Find ().Concat (this.masterEntities).Distinct ();
			}
		}

		public Date GetReferenceDate()
		{
			//	TODO: use real reference date, based on the active master entities...
			return Date.Today;
		}
		
		public void Discard()
		{
			if (this.dataContextDiscarded == false)
			{
				this.dataContextDiscarded = true;
			}
		}

		public void SaveChanges(LockingPolicy lockingPolicy, EntitySaveMode entitySaveMode = EntitySaveMode.None)
		{
			this.ClearDummyEntities ();
			this.ApplyRulesBeforeSaveChanges (entitySaveMode);

			if (this.ContainsChanges ())
			{
				var notYetPersistedEntities = new HashSet<AbstractEntity> (this.dataContext.GetEntities ().Where (x => !this.dataContext.IsPersistent (x)));
				System.Predicate<AbstractEntity> isEmptyEntity = x => x.IsEntityEmpty;

				if (entitySaveMode.HasFlag (EntitySaveMode.IncludeEmpty))
				{
					isEmptyEntity = x => false;
				}
				
				foreach (var entity in notYetPersistedEntities)
				{
					this.dataContext.UpdateEmptyEntityStatus (entity, isEmptyEntity (entity));
				}

				var e = new CancelEventArgs ();

				this.hasExternalChanges = false;
				this.OnSavingChanges (e);

				if (e.Cancel == false)
				{
					this.dataContext.SaveChanges ();
				}

				this.OnContainsChangesChanged ();
			}

			switch (lockingPolicy)
			{
				case LockingPolicy.KeepLock:
					break;
				case LockingPolicy.ReleaseLock:
					this.ReleaseLock ();
					break;
				default:
					throw new System.NotSupportedException (string.Format ("{0} not supported", lockingPolicy.GetQualifiedName ()));
			}
		}

		private void ApplyRulesBeforeSaveChanges(EntitySaveMode entitySaveMode)
		{
			if (this.asyncUpdatePending)
			{
				this.ApplyRulesToRegisteredEntities (RuleType.Update);
				this.asyncUpdatePending = false;
			}

			this.ApplyRulesToRegisteredEntities (RuleType.Validate);

			System.Diagnostics.Debug.Assert (this.asyncUpdatePending == false);
		}

		/// <summary>
		/// Gets the local equivalent of the specified entity: if the entity belongs to another
		/// data context, then it will have to be mapped to the local data context through its
		/// database key.
		/// </summary>
		/// <typeparam name="T">The entity type.</typeparam>
		/// <param name="entity">The entity (or <c>null</c>).</param>
		/// <returns>The local entity (or a wrapped null entity if <c>entity</c> was <c>null</c>).</returns>
		public T GetLocalEntity<T>(T entity)
			where T : AbstractEntity, new ()
		{
			if (entity == null)
			{
				return this.dataContext.CreateNullEntity<T> ();
			}
			else
			{
				return this.dataContext.GetLocalEntity (entity) ?? entity;
			}
		}

		/// <summary>
		/// Gets the local equivalent of the specified entity: if the entity belongs to another
		/// data context, then it will have to be mapped to the local data context through its
		/// database key.
		/// </summary>
		/// <param name="entity">The entity (which may not be <c>null</c>).</param>
		/// <returns>The local entity.</returns>
		public AbstractEntity GetLocalEntity(AbstractEntity entity)
		{
			if (entity == null)
			{
				throw new System.ArgumentNullException ("entity");
			}
			else
			{
				return this.dataContext.GetLocalEntity (entity) ?? entity;
			}
		}

		/// <summary>
		/// Gets the specified repository. The entities returned by the repository will
		/// automatically be mapped to the current context.
		/// </summary>
		/// <typeparam name="T">The repository type.</typeparam>
		/// <returns>The repository.</returns>
		public T GetSpecificRepository<T>()
			where T : Repositories.Repository
		{
			var repository = this.Data.GetSpecificRepository<T> ();
			return repository.DefineMapper (x => this.GetLocalEntity (x)) as T;
		}

		/// <summary>
		/// Gets the repository for the specified entity type. The entities returned by the
		/// repository will automatically be mapped to the current context.
		/// </summary>
		/// <typeparam name="T">The entity type.</typeparam>
		/// <returns>The repository.</returns>
		public Repositories.Repository<T> GetRepository<T>()
			where T : AbstractEntity, new ()
		{
			var repository = this.Data.GetRepository<T> (this.dataContext);
			return repository.DefineMapper (x => this.GetLocalEntity (x)) as Repositories.Repository<T>;
		}
		


		public void SetActiveEntity(AbstractEntity entity)
		{
			System.Diagnostics.Debug.Assert (entity != null);
			System.Diagnostics.Debug.Assert (this.activeEntity == null);
			
			this.activeEntity = entity;

			this.Register (entity);
		}

		/// <summary>
		/// Registers the specified entity with the business context. The business rules will
		/// be applied to the entity, until it gets unregistered.
		/// </summary>
		/// <param name="entity">The entity.</param>
		public void Register(AbstractEntity entity)
		{
			if (entity.IsNotNull ())
			{
				var record = this.entityRecords.FirstOrDefault (x => x.Entity == entity);

				if (record == null)
				{
					this.entityRecords.Add (new EntityRecord (entity, this));
					this.ApplyRules (RuleType.Bind, entity);
				}
				else
				{
					record.IncrementRegistration ();
				}
			}
		}

		/// <summary>
		/// Unregisters the specified entity from the business context.
		/// </summary>
		/// <param name="entity">The entity.</param>
		public void Unregister(AbstractEntity entity)
		{
			if (entity.IsNotNull ())
			{
				var record = this.entityRecords.FirstOrDefault (x => x.Entity == entity);

				System.Diagnostics.Debug.Assert (record != null);

				if (record.DecrementRegistration ())
				{
					//	The entity is no longer referenced; remove it from our records, so that
					//	we no longer apply the business rules to it.

					this.entityRecords.Remove (record);
					this.ApplyRules (RuleType.Unbind, record.Entity);
				}
			}
		}


		public void ApplyRulesToRegisteredEntities(RuleType ruleType, EntitySaveMode entitySaveMode = EntitySaveMode.None)
		{
			bool ignore = entitySaveMode.HasFlag (EntitySaveMode.IgnoreValidationErrors);

			this.entityRecords.ForEach (record => record.Logic.ApplyRule (ruleType, record.Entity, disableBusinessRuleExceptions: ignore));
		}

		public T ApplyRules<T>(RuleType ruleType, T entity, EntitySaveMode entitySaveMode = EntitySaveMode.None)
			where T : AbstractEntity
		{
			bool ignore = entitySaveMode.HasFlag (EntitySaveMode.IgnoreValidationErrors);
			var logic = this.CreateLogic (entity);
			logic.ApplyRule (ruleType, entity, disableBusinessRuleExceptions: ignore);
			return entity;
		}

		public AbstractEntity CreateEntity(Druid entityType)
		{
			var newEntity = this.DataContext.CreateEntity (entityType);
			return this.ApplyRules (RuleType.Setup, newEntity);
		}

		public T CreateEntity<T>()
			where T : AbstractEntity, new ()
		{
			T newEntity = this.DataContext.CreateEntity<T> ();
			return this.ApplyRules (RuleType.Setup, newEntity);
		}

		public T CreateMasterEntity<T>()
			where T : AbstractEntity, new ()
		{
			var entity = this.CreateEntity<T> ();
			this.AddMasterEntity (entity);
			return entity;
		}

		public T CreateEntityAndRegisterAsEmpty<T>()
			where T : AbstractEntity, new ()
		{
			T newEntity = this.DataContext.CreateEntityAndRegisterAsEmpty<T> ();
			return this.ApplyRules (RuleType.Setup, newEntity);
		}

		public T CreateAndRegisterEntity<T>() where T : AbstractEntity, new ()
		{
			var entity = this.CreateEntity<T> ();
			this.Register (entity);
			return entity;
		}

		public bool ArchiveEntity<T>(T entity)
			where T : AbstractEntity, new ()
		{
			ILifetime lifetime = entity as ILifetime;

			if (lifetime == null)
			{
				return false;
			}

			if (this.DataContext.IsPersistent (entity))
			{
				lifetime.IsArchive = true;
			}
			else
			{
				this.DataContext.DeleteEntity (entity);
			}

			return true;
		}

		public bool ArchiveOrDeleteEntity<T>(T entity)
			where T : AbstractEntity, new ()
		{
			if (this.ArchiveEntity (entity))
			{
				return true;
			}
			else
			{
				return this.DeleteEntity (entity);
			}
		}

		public bool DeleteEntity(AbstractEntity entity)
		{
			return this.DataContext.DeleteEntity (entity);
		}

		public void ClearAndDeleteEntities<T>(ICollection<T> collection)
			where T : AbstractEntity
		{
			if ((collection != null) &&
				(collection.Count > 0))
			{
				var copy = collection.ToArray ();

				collection.Clear ();

				copy.ForEach (x => this.DeleteEntity (x));
			}
		}


		/// <summary>
		/// Creates a dummy entity, which can be identified as such (see <see cref="IsDummyEntity"/>).
		/// When the user later calls <see cref="ReplaceDummyEntity"/>, the replacement callback
		/// will be invoked.
		/// </summary>
		/// <typeparam name="T">type of the dummy entity</typeparam>
		/// <param name="replacementCallback">The callback called to replace the dummy entity with a real entity.</param>
		/// <returns>The dummy entity.</returns>
		public T CreateDummyEntity<T>(System.Action<AbstractEntity, AbstractEntity> replacementCallback)
			where T : AbstractEntity, new ()
		{
			var item = this.data.CreateDummyEntity<T> ();

			//	The dummy entity has not yet been virtualized; do this here, so that the user
			//	won't be surprised to get back null references for undefined fields :

			EntityNullReferenceVirtualizer.PatchNullReferences (item);

			this.RememberDummyEntity (item, replacementCallback);

			return item;
		}

		/// <summary>
		/// Determines whether the specified entity is a dummy entity.
		/// </summary>
		/// <param name="entity">The entity.</param>
		/// <returns>
		///   <c>true</c> if the specified entity is a dummy entity; otherwise, <c>false</c>.
		/// </returns>
		public bool IsDummyEntity(AbstractEntity entity)
		{
			return this.data.IsDummyEntity (entity);
		}

		/// <summary>
		/// Replaces the dummy entity with the real entity (or remove it if the real entity is
		/// <c>null</c>).
		/// </summary>
		/// <param name="dummyEntity">The dummy entity.</param>
		/// <param name="realEntity">The real entity.</param>
		/// <returns><c>true</c> if the dummy entity was replaced; otherwise, <c>false</c>.</returns>
		public bool ReplaceDummyEntity(AbstractEntity dummyEntity, AbstractEntity realEntity)
		{
			if (this.dummyEntityReplacementCallbacks == null)
			{
				return false;
			}

			DummyReplacement dummyReplacement;

			long key = dummyEntity.GetEntitySerialId ();

			if (this.dummyEntityReplacementCallbacks.TryGetValue (key, out dummyReplacement))
			{
				this.dummyEntityReplacementCallbacks.Remove (key);
				dummyReplacement.Callback (dummyEntity, realEntity);
				return true;
			}

			return false;
		}

		/// <summary>
		/// Clears any pending dummy entities.
		/// </summary>
		public void ClearDummyEntities()
		{
			if (this.dummyEntityReplacementCallbacks != null)
			{
				var dummyReplacements = this.dummyEntityReplacementCallbacks.Values.ToArray ();
				
				this.dummyEntityReplacementCallbacks.Clear ();
				
				foreach (var dummyReplacement in dummyReplacements)
				{
					dummyReplacement.Callback (dummyReplacement.DummyEntity, null);
				}
			}
		}

		/// <summary>
		/// Reloads the entities if they have changed in the database.
		/// </summary>
		/// <returns><c>true</c> if some entities changed; otherwise, <c>false</c>.</returns>
		public bool ReloadEntities()
		{
			this.dataContext.EntityChanged -= this.HandleDataContextEntityChanged;

			try
			{
				return this.dataContext.Reload ();
			}

			finally
			{
				this.dataContext.EntityChanged += this.HandleDataContextEntityChanged;
			}
		}

		public System.IDisposable SuspendUpdates()
		{
			var helper = new DelayedUpdate (this);
			this.delayedUpdates.Push (helper);
			return helper;
		}

		
		internal void DisposeFromPool()
		{
			if (this.isDisposed == false)
			{
				this.dataContext.EntityChanged -= this.HandleDataContextEntityChanged;

				this.pool.DisposeDataContext (this, this.dataContext);
				this.pool.Remove (this);

				if (this.lockTransaction != null)
				{
					this.ReleaseLock ();
				}
				
				this.isDisposed = true;
			}
		}


		#region IDisposable Members

		public void Dispose()
		{
			if (this.asyncUpdateQueued)
			{
				Dispatcher.RemoveFromQueue (this.SyncUpdateMainWindowController);
			}

			this.lockMonitors.ToArray ().ForEach (m => m.Dispose ());

			System.Diagnostics.Debug.Assert (this.lockMonitors.Count == 0);

			this.pool.DisposeBusinessContext (this);

			System.Diagnostics.Debug.Assert (this.isDisposed);

			System.GC.SuppressFinalize (this);
		}

		#endregion

		#region DelayedUpdate Class

		private sealed class DelayedUpdate : System.IDisposable
		{
			public DelayedUpdate(BusinessContext context)
			{
				this.context = context;
				this.records = new HashSet<EntityRecord> ();
			}

			public void Enqueue(IEnumerable<EntityRecord> records)
			{
				this.records.UnionWith (records);
			}

			#region IDisposable Members

			void System.IDisposable.Dispose()
			{
				var helper = this.context.delayedUpdates.Pop ();

				System.Diagnostics.Debug.Assert (helper == this);

				foreach (var record in this.records)
				{
					record.Logic.ApplyRule (RuleType.Update, record.Entity);
				}
			}

			#endregion

			private readonly BusinessContext context;
			private readonly HashSet<EntityRecord> records;
		}

		#endregion

		#region Unlocker Class

		private sealed class Unlocker : System.IDisposable
		{
			public Unlocker(BusinessContext context)
			{
				this.context = context;
			}

			#region IDisposable Members

			public void Dispose()
			{
				if (this.context != null)
				{
					this.context.ReleaseLock ();
					this.context.lockEntity = null;
				}
			}

			#endregion

			private readonly BusinessContext	context;
		}

		#endregion

		#region NoOpUnlocker Class

		private sealed class NoOpUnlocker : System.IDisposable
		{
			public NoOpUnlocker(BusinessContext context)
			{
				this.context = context;
			}

			#region IDisposable Members

			public void Dispose()
			{
				if (this.context != null)
				{
					System.Diagnostics.Debug.Assert (this.context.IsLocked);
				}
			}

			#endregion

			private readonly BusinessContext	context;
		}

		#endregion

		#region EntityRecord Class

		private sealed class EntityRecord
		{
			public EntityRecord(AbstractEntity entity, BusinessContext businessContext)
			{
				this.entity = entity;
				this.businessContext = businessContext;
				this.dataContext = this.businessContext.Data.DataContextPool.FindDataContext (entity);
				this.counter = 1;
			}

			public DataContext DataContext
			{
				get
				{
					return this.dataContext;
				}
			}

			public AbstractEntity Entity
			{
				get
				{
					return this.entity;
				}
			}

			public Logic Logic
			{
				get
				{
					if (this.logic == null)
					{
						this.logic = this.businessContext.CreateLogic (this.entity);
					}

					return this.logic;
				}
			}

			public bool HasMultipleRegistration
			{
				get
				{
					return this.counter > 1;
				}
			}

			public void IncrementRegistration()
			{
				System.Threading.Interlocked.Increment (ref this.counter);
			}

			/// <summary>
			/// Decrements the registration counter.
			/// </summary>
			/// <returns><c>true</c> if this was the last registration; otherwise, <c>false</c>.</returns>
			public bool DecrementRegistration()
			{
				return System.Threading.Interlocked.Decrement (ref this.counter) == 0;
			}


			private readonly AbstractEntity entity;
			private readonly BusinessContext businessContext;
			private readonly DataContext dataContext;
			private Logic logic;
			private int counter;
		}

		#endregion

		#region DummyReplacement Structure

		private struct DummyReplacement
		{
			public DummyReplacement(AbstractEntity dummyEntity, System.Action<AbstractEntity, AbstractEntity> callback)
			{
				this.dummyEntity = dummyEntity;
				this.callback    = callback;
			}

			public System.Action<AbstractEntity, AbstractEntity> Callback
			{
				get
				{
					return this.callback;
				}
			}

			public AbstractEntity DummyEntity
			{
				get
				{
					return this.dummyEntity;
				}
			}

			private readonly System.Action<AbstractEntity, AbstractEntity> callback;
			private readonly AbstractEntity		dummyEntity;
		}

		#endregion

		
		private void SetNavigationPathElement(NavigationPathElement navigationPathElement)
		{
			this.activeNavigationPathElement = navigationPathElement;
		}
		
		private void AsyncUpdateMainWindowController()
		{
			this.asyncUpdateQueued = true;
			Dispatcher.Queue (this.SyncUpdateMainWindowController);
		}

		private void SyncUpdateMainWindowController()
		{
			if (this.asyncUpdatePending)
			{
				this.dataChangedCounter.IfZero
					(
						delegate
						{
							this.asyncUpdatePending = false;
							this.ApplyRulesToRegisteredEntities (RuleType.Update);
						}
					);
			}

			this.asyncUpdateQueued = false;

			if (this.isDisposed)
			{
				throw new System.ObjectDisposedException ("Calling BusinessContext.SyncUpdateMainWindowController on disposed context #" + this.UniqueId.ToString ());
			}

			this.OnRefreshUIRequested ();
		}


		private void RememberDummyEntity(AbstractEntity entity, System.Action<AbstractEntity, AbstractEntity> replacementCallback)
		{
			if (this.dummyEntityReplacementCallbacks == null)
			{
				this.dummyEntityReplacementCallbacks = new Dictionary<long, DummyReplacement> ();
			}

			long key = entity.GetEntitySerialId ();

			this.dummyEntityReplacementCallbacks[key] = new DummyReplacement (entity, replacementCallback);
		}
		
		private void HandleDataContextEntityChanged(object sender, EntityChangedEventArgs e)
		{
			this.dataChangedCounter.IfZero (this.HandleDataContextEntityChanged, e.EventType);
		}

		private void HandleDataContextEntityChanged(EntityChangedEventType change)
		{
			bool notify = false;

			if (this.dataContextDirty == false)
			{
				this.dataContextDirty = true;
				this.HandleFirstEntityChange ();
			}

			if ((Logic.Current == null) &&
				(change == EntityChangedEventType.Updated))
			{
				if (this.delayedUpdates.Count > 0)
				{
					this.delayedUpdates.Peek ().Enqueue (this.entityRecords);
				}
				else
				{
					notify = this.asyncUpdatePending == false;
				}
			}

			this.OnContainsChangesChanged ();

			if (notify)
			{
				//	If we need to execute business rules as a consequence of the change, we do
				//	not do it immediately, but we postpone their execution to a later point in
				//	time (we would otherwise end up executing the same rule again and again).

				this.asyncUpdatePending = true;
				this.AsyncUpdateMainWindowController ();
			}
		}

		private void HandleFirstEntityChange()
		{
			this.AcquireLock ();
		}

		private IEnumerable<string> GetLockNames()
		{
			if (this.globalLock !=  null)
			{
				yield return this.globalLock.LockId;
			}

			if (this.activeEntity != null)
			{
				yield return Locker.GetEntityLockName (this.dataContext, this.activeEntity);
			}
			if (this.lockEntity != null)
			{
				yield return Locker.GetEntityLockName (this.dataContext, this.lockEntity);
			}
		}


		private void OnLockAcquired()
		{
			System.Diagnostics.Debug.WriteLine ("*** LOCK ACQUIRED ***");

			if (this.EnableReload)
			{
				this.ReloadEntities ();
			}

			this.NotifyExternalChanges ();
		}

		private void OnSavingChanges(CancelEventArgs e)
		{
			this.SavingChanges.Raise (this, e);
		}

		private void OnContainsChangesChanged()
		{
			this.ContainsChangesChanged.Raise (this);
		}

		private void OnMasterEntitiesChanged()
		{
			this.MasterEntitiesChanged.Raise (this);
		}

		private void OnRefreshUIRequested()
		{
			this.RefreshUIRequested.Raise (this);
		}

		public event EventHandler<CancelEventArgs>	SavingChanges;
		public event EventHandler					ContainsChangesChanged;
		public event EventHandler					MasterEntitiesChanged;
		public event EventHandler					RefreshUIRequested;

		private static int						nextUniqueId;

		private readonly BusinessContextPool	pool;
		private readonly DataContext			dataContext;
		private readonly List<EntityRecord>		entityRecords;
		private readonly List<AbstractEntity>	masterEntities;
		private readonly Locker					locker;
		private readonly CoreData				data;
		private readonly Stack<DelayedUpdate>	delayedUpdates;
		private readonly SafeCounter			dataChangedCounter;
		private readonly List<LockMonitor>		lockMonitors;
		private readonly bool					enableReload;
		private bool							dataContextDirty;
		private bool							dataContextDiscarded;
		private bool							isDisposed;
		private bool							hasExternalChanges;
		private bool							asyncUpdatePending;
		private bool							asyncUpdateQueued;

		private Data.LockTransaction			lockTransaction;

		private GlobalLock						globalLock;
		private AbstractEntity					activeEntity;
		private AbstractEntity					lockEntity;
		private NavigationPathElement			activeNavigationPathElement;
		
		private Dictionary<long, DummyReplacement> dummyEntityReplacementCallbacks;
	}
}

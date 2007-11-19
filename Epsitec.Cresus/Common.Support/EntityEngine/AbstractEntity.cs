//	Copyright � 2007, EPSITEC SA, CH-1092 BELMONT, Switzerland
//	Author: Pierre ARNAUD, Maintainer: Pierre ARNAUD

using Epsitec.Common.Support;
using Epsitec.Common.Support.EntityEngine;
using Epsitec.Common.Types;
using Epsitec.Common.Types.Collections;

using System.Collections.Generic;

namespace Epsitec.Common.Support.EntityEngine
{
	/// <summary>
	/// The <c>AbstractEntity</c> class is the base class used to store the
	/// data represented by entity instances.
	/// </summary>
	public abstract class AbstractEntity : IStructuredTypeProvider, IStructuredData
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="AbstractEntity"/> class.
		/// </summary>
		protected AbstractEntity()
		{
			this.entitySerialId = System.Threading.Interlocked.Increment (ref AbstractEntity.nextSerialId);
			this.context = EntityContext.Current;
		}

		/// <summary>
		/// Gets a value indicating whether this entity is currently defining
		/// its original values (see <see cref="DefineOriginalValues"/>).
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this entity is currently defining its original
		///		values; otherwise, <c>false</c>.
		/// </value>
		public bool IsDefiningOriginalValues
		{
			get
			{
				if (this.defineOriginalValuesCount > 0)
				{
					return true;
				}
				else
				{
					return false;
				}
			}
		}

		/// <summary>
		/// Gets the id of the <see cref="StructuredType"/> which describes
		/// this entity.
		/// </summary>
		/// <returns>The id of the <see cref="StructuredType"/>.</returns>
		public abstract Druid GetEntityStructuredTypeId();

		/// <summary>
		/// Gets the context associated with this entity.
		/// </summary>
		/// <returns>The <see cref="EntityContext"/> instance.</returns>
		public EntityContext GetEntityContext()
		{
			return this.context;
		}

		/// <summary>
		/// Gets the state of the entity data.
		/// </summary>
		/// <value>The state of the entity data.</value>
		public EntityDataState GetEntityDataState()
		{
			if (this.modifiedValues != null)
			{
				return EntityDataState.Modified;
			}
			else
			{
				return EntityDataState.Unchanged;
			}
		}

		/// <summary>
		/// Gets the (unique) serial id for this entity.
		/// </summary>
		/// <returns>The serial id for this entity.</returns>
		public long GetEntitySerialId()
		{
			return this.entitySerialId;
		}

		/// <summary>
		/// Determines whether the entity contains the specified data version.
		/// </summary>
		/// <param name="version">The version.</param>
		/// <returns>
		/// 	<c>true</c> if the entity contains the specified data version;
		///		otherwise, <c>false</c>.
		/// </returns>
		public bool ContainsDataVersion(EntityDataVersion version)
		{
			switch (version)
			{
				case EntityDataVersion.Modified:
					return this.modifiedValues != null;
				case EntityDataVersion.Original:
					return this.originalValues != null;
				default:
					throw new System.NotImplementedException ();
			}
		}


		/// <summary>
		/// Switches the entity into a mode which allows the caller to define
		/// the original values. Call this method in a <c>using</c> block.
		/// </summary>
		/// <returns>The object which must be disposed of to end the special
		/// data definition mode.</returns>
		public System.IDisposable DefineOriginalValues()
		{
			return new DefineOriginalValuesHelper (this);
		}
		

		public T GetField<T>(string id)
		{
			StructuredTypeField field = this.context.GetStructuredType (this).GetField (id);

			System.Diagnostics.Debug.Assert (field != null);
			System.Diagnostics.Debug.Assert (field.Relation != FieldRelation.Collection);

			object value = this.InternalGetValue (id);

			if (UnknownValue.IsUnknownValue (value))
			{
				throw new System.NotSupportedException (string.Format ("Field {0} not supported by value store", id));
			}

			if (UndefinedValue.IsUndefinedValue (value))
			{
				return default (T);
			}
			
			return (T) value;
		}

		public IList<T> GetFieldCollection<T>(string id) where T : AbstractEntity
		{
			object  value = this.InternalGetValue (id);
			IList<T> list = value as IList<T>;

			if (list == null)
			{
				if (UndefinedValue.IsUndefinedValue (value))
				{
					//	The value store does not (yet) contain a collection for the
					//	specified items. We have to allocate one :

					using (this.DefineOriginalValues ())
					{
						list = new EntityCollection<T> (id, this, true);
						this.InternalSetValue (id, list);
					}
				}
				else
				{
					throw new System.NotSupportedException (string.Format ("Field {0} uses incompatible collection type", id));
				}
			}

			return list;
		}

		public void SetField<T>(string id, T newValue)
		{
			T oldValue = this.GetField<T> (id);
			this.SetField<T> (id, oldValue, newValue);
		}
		
		public void SetField<T>(string id, T oldValue, T newValue)
		{
			this.GenericSetValue (id, oldValue, newValue);
		}

		public static TResult GetCalculation<T, TResult>(T entity, string id, System.Func<T, TResult> func)
		{
			return func (entity);
		}
		
		public object InternalGetValue(string id)
		{
			object value;

			if ((this.modifiedValues != null) &&
				(this.IsDefiningOriginalValues == false))
			{
				value = this.modifiedValues.GetValue (id);

				if ((this.originalValues != null) &&
					(UndefinedValue.IsUndefinedValue (value)))
				{
					value = this.originalValues.GetValue (id);
				}
			}
			else if (this.originalValues != null)
			{
				value = this.originalValues.GetValue (id);
			}
			else
			{
				value = UndefinedValue.Value;
			}
			
			return value;
		}
		
		public void InternalSetValue(string id, object value)
		{
			if (this.IsDefiningOriginalValues)
			{
				if (this.originalValues == null)
				{
					this.originalValues = this.context.CreateValueStore (this);
				}

				this.originalValues.SetValue (id, value);
			}
			else
			{
				if (this.modifiedValues == null)
				{
					this.modifiedValues = this.context.CreateValueStore (this);
				}

				this.modifiedValues.SetValue (id, value);
			}
		}

		public System.Collections.IList InternalGetFieldCollection(string id)
		{
			object value = this.InternalGetValue (id);
			System.Collections.IList list = value as System.Collections.IList;

			if (list == null)
			{
				if (UndefinedValue.IsUndefinedValue (value))
				{
					//	The value store does not (yet) contain a collection for the
					//	specified items. We have to allocate one :

					using (this.DefineOriginalValues ())
					{
						IStructuredType     type  = this.context.GetStructuredType (this);
						StructuredTypeField field = type.GetField (id);
						AbstractEntity      model = this.context.CreateEmptyEntity (field.TypeId);
						
						System.Type itemType = model.GetType ();
						System.Type genericType = typeof (EntityCollection<>);
						System.Type collectionType = genericType.MakeGenericType (itemType);

						list = System.Activator.CreateInstance (collectionType, id, this, true) as System.Collections.IList;
						this.InternalSetValue (id, list);
					}
				}
				else
				{
					throw new System.NotSupportedException (string.Format ("Field {0} uses incompatible collection type", id));
				}
			}

			return list;
		}

		internal EntityCollection<T> CopyFieldCollection<T>(string id, EntityCollection<T> collection) where T : AbstractEntity
		{
			System.Diagnostics.Debug.Assert (this.IsDefiningOriginalValues == false);

			EntityCollection<T> copy = new EntityCollection<T> (id, this, false);

			copy.AddRange (collection);

			this.InternalSetValue (id, copy);

			return copy;
		}

		internal IStructuredTypeProvider GetStructuredTypeProvider()
		{
			return (this.originalValues ?? this.modifiedValues) as IStructuredTypeProvider;
		}

		protected virtual object DynamicGetField(string id)
		{
			PropertyGetter getter = this.context.FindPropertyGetter (this, id);

			if (getter == null)
			{
				return this.GenericGetValue (id);
			}
			else
			{
				return getter (this);
			}
		}

		protected virtual void DynamicSetField(string id, object newValue)
		{
			PropertySetter setter = this.context.FindPropertySetter (this, id);

			if (setter == null)
			{
				this.GenericSetValue (id, this.InternalGetValue (id), newValue);
			}
			else
			{
				setter (this, newValue);
			}
		}

		protected object GenericGetValue(string id)
		{
			return this.InternalGetValue (id);
		}

		protected void GenericSetValue(string id, object oldValue, object newValue)
		{
			StructuredTypeField field = this.context.GetStructuredType (this).GetField (id);

			System.Diagnostics.Debug.Assert (field != null);
			System.Diagnostics.Debug.Assert (field.Relation != FieldRelation.Collection);

			if ((field.IsNullable) &&
				(newValue == null))
			{
				//	The value is null and the field is nullable; this operation
				//	is valid and it will clear the field.

				this.InternalSetValue (id, UndefinedValue.Value);
			}
			else
			{
				IDataConstraint constraint = field.Type as IDataConstraint;

				System.Diagnostics.Debug.Assert (constraint != null);

				if (constraint.IsValidValue (newValue))
				{
					object value;

					if (newValue == null)
					{
						value = UndefinedValue.Value;
					}
					else
					{
						value = (object) newValue;
					}

					this.InternalSetValue (id, value);
				}
				else
				{
					throw new System.ArgumentException (string.Format ("Invalid value '{0}' specified for field {1}", newValue, id));
				}
			}

			if (this.eventHandlers != null)
			{
				System.Delegate handler;
				
				lock (this.eventHandlers)
				{
					this.eventHandlers.TryGetValue (id, out handler);
				}

				if (handler != null)
				{
					((EventHandler<DependencyPropertyChangedEventArgs>) handler) (this, new DependencyPropertyChangedEventArgs (id, oldValue, newValue));
				}
			}
		}

		#region IStructuredTypeProvider Members

		IStructuredType IStructuredTypeProvider.GetStructuredType()
		{
			return this.context.GetStructuredType (this);
		}

		#endregion

		#region IStructuredData Members

		/// <summary>
		/// Attaches a listener to the specified structured value.
		/// </summary>
		/// <param name="id">The identifier of the value.</param>
		/// <param name="handler">The handler which implements the listener.</param>
		void IStructuredData.AttachListener(string id, EventHandler<DependencyPropertyChangedEventArgs> handler)
		{
			this.EnsureEventHandlers ();

			lock (this.eventHandlers)
			{
				System.Delegate handlers;

				if (this.eventHandlers.TryGetValue (id, out handlers))
				{
					this.eventHandlers[id] = System.Delegate.Combine (handlers, handler);
				}
				else
				{
					this.eventHandlers[id] = handler;
				}
			}
		}

		/// <summary>
		/// Detaches a listener from the specified structured value.
		/// </summary>
		/// <param name="id">The identifier of the value.</param>
		/// <param name="handler">The handler which implements the listener.</param>
		void IStructuredData.DetachListener(string id, EventHandler<DependencyPropertyChangedEventArgs> handler)
		{
			this.EnsureEventHandlers ();

			lock (this.eventHandlers)
			{
				System.Delegate handlers;

				if (this.eventHandlers.TryGetValue (id, out handlers))
				{
					handlers = System.Delegate.Remove (handlers, handler);

					if (handlers == null)
					{
						this.eventHandlers.Remove (id);
					}
					else
					{
						this.eventHandlers[id] = handlers;
					}
				}
			}
		}

		/// <summary>
		/// Gets the collection of identifiers used to define the structured values.
		/// </summary>
		/// <returns>The collection of identifiers.</returns>
		IEnumerable<string> IStructuredData.GetValueIds()
		{
			return this.context.GetEntityFieldIds (this);
		}

		#endregion

		#region IValueStore Members

		/// <summary>
		/// Gets the value for the specified identifier.
		/// </summary>
		/// <param name="id">The identifier of the value.</param>
		/// <returns>
		/// The value, or either <see cref="UndefinedValue.Value"/> if the
		/// value is currently undefined or <see cref="UnknownValue.Value"/> if the
		/// identifier does not map to a known value.
		/// </returns>
		object IValueStore.GetValue(string id)
		{
			return this.DynamicGetField (id);
		}

		/// <summary>
		/// Sets the value for the specified identifier.
		/// </summary>
		/// <param name="id">The identifier of the value.</param>
		/// <param name="value">The value to store into the structure record;
		/// specifying <see cref="UndefinedValue.Value"/> clears the value.
		/// <see cref="UnknownValue.Value"/> may not be specified as a value.</param>
		void IValueStore.SetValue(string id, object value)
		{
			this.DynamicSetField (id, value);
		}

		#endregion

		private void EnsureEventHandlers()
		{
			if (this.eventHandlers == null)
			{
				lock (AbstractEntity.globalExclusion)
				{
					if (this.eventHandlers == null)
					{
						this.eventHandlers = new Dictionary<string, System.Delegate> ();
					}
				}
			}
		}

		#region DefineOriginalValuesHelper Class

		/// <summary>
		/// The <c>DefineOriginalValuesHelper</c> is used by the <see cref="DefineOriginalValues"/>
		/// method to manage the end of the definition phase; instances of this class
		/// are meant to be used in a <c>using</c> block.
		/// </summary>
		private sealed class DefineOriginalValuesHelper : System.IDisposable
		{
			public DefineOriginalValuesHelper(AbstractEntity entity)
			{
				this.entity = entity;
				System.Threading.Interlocked.Increment (ref this.entity.defineOriginalValuesCount);
			}

			~DefineOriginalValuesHelper()
			{
				throw new System.InvalidOperationException ("Caller of DefineOriginalValues forgot to call Dispose");
			}

			#region IDisposable Members

			public void Dispose()
			{
				if (this.entity != null)
				{
					System.GC.SuppressFinalize (this);
					System.Threading.Interlocked.Decrement (ref this.entity.defineOriginalValuesCount);
					this.entity = null;
				}
			}

			#endregion

			AbstractEntity entity;
		}

		#endregion

		private static long nextSerialId = 1;
		private static readonly object globalExclusion = new object ();

		private readonly EntityContext context;
		private readonly long entitySerialId;
		private IValueStore originalValues;
		private IValueStore modifiedValues;
		private int defineOriginalValuesCount;
		private Dictionary<string, System.Delegate> eventHandlers;
	}
}

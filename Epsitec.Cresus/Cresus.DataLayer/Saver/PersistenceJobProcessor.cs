﻿using Epsitec.Common.Support;
using Epsitec.Common.Support.EntityEngine;
using Epsitec.Common.Support.Extensions;

using Epsitec.Common.Types;

using Epsitec.Cresus.Database;
using Epsitec.Cresus.Database.Collections;

using Epsitec.Cresus.DataLayer.Infrastructure;

using Epsitec.Cresus.DataLayer.Context;
using Epsitec.Cresus.DataLayer.Saver.PersistenceJobs;
using Epsitec.Cresus.DataLayer.Schema;

using System.Collections.Generic;

using System.Linq;


namespace Epsitec.Cresus.DataLayer.Saver
{


	/// <summary>
	/// The <c>PersistenceJobProcessor</c> class is used to execute the <see cref="AbstractPersistenceJob"/>
	/// in order to persist them to the database.
	/// </summary>
	internal sealed class PersistenceJobProcessor
	{


		/// <summary>
		/// Creates a new <c>PersistenceJobProcessor</c>.
		/// </summary>
		/// <param name="dataContext">The <see cref="DataContext"/> used by this instance.</param>
		/// <exception cref="System.ArgumentNullException">If <paramref name="dataContext"/> is <c>null</c>.</exception>
		public PersistenceJobProcessor(DataContext dataContext)
		{
			dataContext.ThrowIfNull ("dataContext");

			this.DataContext = dataContext;
		}


		/// <summary>
		/// The <see cref="DataContext"/> used by this instance.
		/// </summary>
		private DataContext DataContext
		{
			get;
			set;
		}


		private DataInfrastructure DataInfrastructure
		{
			get
			{
				return this.DataContext.DataInfrastructure;
			}
		}


		/// <summary>
		/// The <see cref="SchemaEngine"/> used by this instance.
		/// </summary>
		private EntitySchemaEngine SchemaEngine
		{
			get
			{
				return this.DataInfrastructure.EntityEngine.EntitySchemaEngine;
			}
		}


		private EntityTypeEngine TypeEngine
		{
			get
			{
				return this.DataInfrastructure.EntityEngine.EntityTypeEngine;
			}
		}


		/// <summary>
		/// The <see cref="DbInfrastructure"/> used by this instance.
		/// </summary>
		private DbInfrastructure DbInfrastructure
		{
			get
			{
				return this.DataInfrastructure.DbInfrastructure;
			}
		}


		/// <summary>
		/// The <see cref="DataConverter"/> used by this instance.
		/// </summary>
		private DataConverter DataConverter
		{
			get
			{
				return this.DataContext.DataConverter;
			}
		}


		/// <summary>
		/// Executes the given sequence of <see cref="AbstractPersistenceJob"/> in order to persist
		/// them to the database. In addition, this method returns a mapping from <see cref="AbstractEntity"/>
		/// to <see cref="DbKey"/> that contain the new <see cref="DbKey"/> that has been assigned to
		/// each <see cref="AbstractEntity"/> which has been inserted to the database.
		/// </summary>
		/// <param name="transaction">The <see cref="DbTransaction"/> that must be used for the operation.</param>
		/// <param name="entityModificationEntry">The <see cref="EntityModificationEntry"/> that must be used to log the operation.</param>
		/// <param name="jobs">The sequence of <see cref="AbstractPersistenceJob"/> to execute.</param>
		/// <returns>The mapping from the <see cref="AbstractEntity"/> that have been inserted in the database to their newly assigned <see cref="DbKey"/>.</returns>
		/// <exception cref="System.ArgumentNullException">If <paramref name="transaction"/> is <c>null</c>.</exception>
		/// <exception cref="System.ArgumentNullException">If <paramref name="jobs"/> is <c>null</c>.</exception>
		public IEnumerable<KeyValuePair<AbstractEntity, DbKey>> ProcessJobs(DbTransaction transaction, EntityModificationEntry entityModificationEntry, IEnumerable<AbstractPersistenceJob> jobs)
		{
			jobs.ThrowIfNull ("jobs");
			transaction.ThrowIfNull ("transaction");
			entityModificationEntry.ThrowIfNull ("entityModificationEntry");

			List<AbstractPersistenceJob> jobsCopy = jobs.ToList ();
			Dictionary<AbstractEntity, DbKey> newEntityKeys = new Dictionary<AbstractEntity, DbKey> ();

			foreach (var deleteJob in jobsCopy.OfType<DeletePersistenceJob> ())
			{
				this.ProcessJob (transaction, entityModificationEntry, deleteJob);
			}

			// First we execute the root value jobs, as they are the one that will create the
			// entity keys for the new entities with the autoincrement on the CR_ID column.

			foreach (var rootValueJob in jobsCopy.OfType<ValuePersistenceJob> ().Where (j => j.IsRootTypeJob))
			{
				this.ProcessJob (transaction, newEntityKeys, entityModificationEntry, rootValueJob);
			}

			// Now we can use the id of the new entities in the other jobs, such as the jobs that
			// insert row in the subtype tables or in the reference and collection jobs that might
			// also target these entities.

			foreach (var subRootValueJob in jobsCopy.OfType<ValuePersistenceJob> ().Where (j => !j.IsRootTypeJob))
			{
				this.ProcessJob (transaction, newEntityKeys, entityModificationEntry, subRootValueJob);
			}

			foreach (var referenceJob in jobsCopy.OfType<ReferencePersistenceJob> ())
			{
				this.ProcessJob (transaction, newEntityKeys, referenceJob);
			}

			foreach (var collectionJob in jobsCopy.OfType<CollectionPersistenceJob> ())
			{
				this.ProcessJob (transaction, newEntityKeys, collectionJob);
			}

			return newEntityKeys;
		}


		/// <summary>
		/// Executes the given <see cref="DeletePersistenceJob"/> to the database.
		/// </summary>
		/// <param name="transaction">The <see cref="DbTransaction"/> object to use.</param>
		/// <param name="entityModificationEntry">The <see cref="EntityModificationEntry"/> that must be used to log the operation.</param>
		/// <param name="job">The <see cref="DeletePersistenceJob"/> to execute.</param>
		private void ProcessJob(DbTransaction transaction, EntityModificationEntry entityModificationEntry, DeletePersistenceJob job)
		{
			AbstractEntity entity = job.Entity;
			DbKey dbKey = this.GetPersistentEntityDbKey (entity);

			this.DeleteEntityValues (transaction, entity, dbKey);
			this.DeleteEntitySourceCollections (transaction, entity, dbKey);
			this.DeleteEntityTargetRelations (transaction, entity, dbKey);
			this.AddEntityDeletionLogEntry (entityModificationEntry, entity, dbKey);
		}


		/// <summary>
		/// Deletes all the value rows in the database for the given <see cref="AbstractEntity"/>.
		/// </summary>
		/// <param name="transaction">The <see cref="DbTransaction"/> object to use.</param>
		/// <param name="entity">The <see cref="AbstractEntity"/> whose values to delete.</param>
		/// <param name="dbKey">The <see cref="DbKey"/> of the given <see cref="AbstractEntity"/>.</param>
		private void DeleteEntityValues(DbTransaction transaction, AbstractEntity entity, DbKey dbKey)
		{
			Druid leafEntityId = entity.GetEntityStructuredTypeId ();

			foreach (var localEntityType in this.TypeEngine.GetBaseTypes (leafEntityId))
			{
				this.DeleteEntityValues (transaction, localEntityType.CaptionId, dbKey);
			}
		}


		/// <summary>
		/// Deletes the value row in the database for the given <see cref="AbstractEntity"/> and the
		/// given type.
		/// </summary>
		/// <param name="transaction">The <see cref="DbTransaction"/> object to use.</param>
		/// <param name="localEntityId">The <see cref="Druid"/> defining the type of the value row to remove.</param>
		/// <param name="dbKey">The <see cref="DbKey"/> of the <see cref="AbstractEntity"/>.</param>
		private void DeleteEntityValues(DbTransaction transaction, Druid localEntityId, DbKey dbKey)
		{
			DbTable table = this.SchemaEngine.GetEntityTable (localEntityId);

			SqlFieldList conditions = new SqlFieldList ();
			conditions.Add (this.CreateConditionForRowId (table, dbKey));

			string tableName = table.GetSqlName ();

			transaction.SqlBuilder.RemoveData (tableName, conditions);

			this.DbInfrastructure.ExecuteNonQuery (transaction);
		}


		/// <summary>
		/// Removes all the relations from a given <see cref="AbstractEntity"/> to other
		/// <see cref="AbstractEntity"/> in the database.
		/// </summary>
		/// <param name="transaction">The <see cref="DbTransaction"/> object to use.</param>
		/// <param name="entity">The <see cref="AbstractEntity"/> whose outward relations to remove.</param>
		/// <param name="dbKey">The <see cref="DbKey"/> of the <see cref="AbstractEntity"/>.</param>
		private void DeleteEntitySourceCollections(DbTransaction transaction, AbstractEntity entity, DbKey dbKey)
		{
			Druid leafEntityId = entity.GetEntityStructuredTypeId ();

			foreach (var entityType in this.TypeEngine.GetBaseTypes (leafEntityId))
			{
				Druid localEntityId = entityType.CaptionId;

				foreach (var field in this.TypeEngine.GetLocalCollectionFields (localEntityId))
				{
					Druid fieldId = field.CaptionId;

					this.DeleteEntitySourceCollection (transaction, localEntityId, fieldId, dbKey);
				}
			}
		}


		/// <summary>
		/// Removes all the relations from any <see cref="AbstractEntity"/> to the given
		/// <see cref="AbstractEntity"/> in the database.
		/// </summary>
		/// <param name="transaction">The <see cref="DbTransaction"/> object to use.</param>
		/// <param name="entity">The <see cref="AbstractEntity"/> whose inward relations to remove.</param>
		/// <param name="dbKey">The <see cref="DbKey"/> of the <see cref="AbstractEntity"/>.</param>
		private void DeleteEntityTargetRelations(DbTransaction transaction, AbstractEntity entity, DbKey dbKey)
		{
			Druid leafEntityId = entity.GetEntityStructuredTypeId ();

			foreach (var item in this.TypeEngine.GetReferencingFields (leafEntityId))
			{
				Druid localSourceEntityId = item.Key.CaptionId;

				foreach (var field in item.Value)
				{
					Druid fieldId = field.CaptionId;

					switch (field.Relation)
					{
						case FieldRelation.Reference:
							this.DeleteEntityTargetReference (transaction, localSourceEntityId, fieldId, dbKey);
							break;

						case FieldRelation.Collection:
							this.DeleteEntityTargetCollection (transaction, localSourceEntityId, fieldId, dbKey);
							break;

						default:
							throw new System.NotImplementedException ();
					}
				}
			}
		}


		/// <summary>
		/// Adds an entry in the CR_EDL table that store which entities have been deleted.
		/// </summary>
		/// <param name="entityModificationEntry">The <see cref="EntityModificationEntry"/> that must be used to log the operation.</param>
		/// <param name="entity">The entity which has been deleted.</param>
		/// <param name="dbKey">The <see cref="DbKey"/> of the deleted entity.</param>
		private void AddEntityDeletionLogEntry(EntityModificationEntry entityModificationEntry, AbstractEntity entity, DbKey dbKey)
		{
			DbId entityModificationEntryId = entityModificationEntry.EntryId;
			Druid entityTypeId = entity.GetEntityStructuredTypeId ();
			DbId entityId = dbKey.Id;

			this.DataInfrastructure.CreateEntityDeletionEntry (entityModificationEntryId, entityTypeId, entityId);
		}


		/// <summary>
		/// Executes the given <see cref="ValuePersistenceJob"/> in order to persist it to the
		/// database.
		/// </summary>
		/// <param name="transaction">The <see cref="DbTransaction"/> object to use.</param>
		/// <param name="newEntityKeys">The <see cref="Dictionary{AbstractEntity, DbKey}"/> containing the mapping from the newly inserted <see cref="AbstractEntity"/> to their newly assigned <see cref="DbKey"/>.</param>
		/// <param name="entityModificationEntry">The <see cref="EntityModificationEntry"/> that must be used to log the operation.</param>
		/// <param name="job">The <see cref="ValuePersistenceJob"/> to execute.</param>
		private void ProcessJob(DbTransaction transaction, Dictionary<AbstractEntity, DbKey> newEntityKeys, EntityModificationEntry entityModificationEntry, ValuePersistenceJob job)
		{
			switch (job.JobType)
			{
				case PersistenceJobType.Insert:
					this.InsertValueData (transaction, newEntityKeys, entityModificationEntry, job);
					break;

				case PersistenceJobType.Update:
					this.UpdateValueData (transaction, entityModificationEntry, job);
					break;

				default:
					throw new System.NotSupportedException ();
			}
		}


		/// <summary>
		/// Executes the given <see cref="ReferencePersistenceJob"/> in order to persist it to the
		/// database.
		/// </summary>
		/// <param name="transaction">The <see cref="DbTransaction"/> object to use.</param>
		/// <param name="newEntityKeys">The <see cref="Dictionary{AbstractEntity, DbKey}"/> containing the mapping from the newly inserted <see cref="AbstractEntity"/> to their newly assigned <see cref="DbKey"/>.</param>
		/// <param name="job">The <see cref="ReferencePersistenceJob"/> to execute.</param>
		private void ProcessJob(DbTransaction transaction, Dictionary<AbstractEntity, DbKey> newEntityKeys, ReferencePersistenceJob job)
		{
			this.UpdateReferenceData (transaction, newEntityKeys, job);
		}


		/// <summary>
		/// Executes the given <see cref="CollectionPersistenceJob"/> in order to persist it to the
		/// database.
		/// </summary>
		/// <param name="transaction">The <see cref="DbTransaction"/> object to use.</param>
		/// <param name="newEntityKeys">The <see cref="Dictionary{AbstractEntity, DbKey}"/> containing the mapping from the newly inserted <see cref="AbstractEntity"/> to their newly assigned <see cref="DbKey"/>.</param>
		/// <param name="job">The <see cref="CollectionPersistenceJob"/> to execute.</param>
		private void ProcessJob(DbTransaction transaction, Dictionary<AbstractEntity, DbKey> newEntityKeys, CollectionPersistenceJob job)
		{
			switch (job.JobType)
			{
				case PersistenceJobType.Insert:
					this.InsertCollectionData (transaction, newEntityKeys, job);
					break;
				case PersistenceJobType.Update:
					this.UpdateCollectionData (transaction, newEntityKeys, job);
					break;
				default:
					throw new System.NotSupportedException ();
			}
		}


		/// <summary>
		/// Inserts the value data the given <see cref="ValuePersistenceJob"/> in the database.
		/// </summary>
		/// <param name="transaction">The <see cref="DbTransaction"/> object to use.</param>
		/// <param name="newEntityKeys">The <see cref="Dictionary{AbstractEntity, DbKey}"/> containing the mapping from the newly inserted <see cref="AbstractEntity"/> to their newly assigned <see cref="DbKey"/>.</param>
		/// <param name="entityModificationEntry">The <see cref="EntityModificationEntry"/> that must be used to log the operation.</param>
		/// <param name="job">The <see cref="ValuePersistenceJob"/> to execute.</param>
		private void InsertValueData(DbTransaction transaction, Dictionary<AbstractEntity, DbKey> newEntityKeys, EntityModificationEntry entityModificationEntry, ValuePersistenceJob job)
		{
			Druid leafEntityId = job.Entity.GetEntityStructuredTypeId ();
			Druid localEntityId = job.LocalEntityId;

			DbTable table = this.SchemaEngine.GetEntityTable (localEntityId);
			string tableName = table.GetSqlName ();

			SqlFieldList fields = new SqlFieldList ();

			fields.AddRange (this.CreateSqlFields (localEntityId, job.GetFieldIdsWithValues ()));

			if (job.IsRootTypeJob)
			{
				fields.Add (this.CreateSqlFieldForLog (table, entityModificationEntry));
				fields.Add (this.CreateSqlFieldForType (table, leafEntityId));

				SqlFieldList fieldsToReturn = new SqlFieldList ()
				{
					new SqlField () { Alias = table.Columns[EntitySchemaBuilder.EntityTableColumnIdName].GetSqlName() },
				};

				transaction.SqlBuilder.InsertData (tableName, fields, fieldsToReturn);
				object data = this.DbInfrastructure.ExecuteScalar (transaction);

				newEntityKeys[job.Entity] = new DbKey (new DbId ((long) data));
			}
			else
			{
				DbKey dbKey = this.GetNonPersistentEntityDbKey (job.Entity, newEntityKeys);

				fields.Add (this.CreateSqlFieldForKey (table, dbKey));

				transaction.SqlBuilder.InsertData (tableName, fields);
				this.DbInfrastructure.ExecuteNonQuery (transaction);
			}
		}


		/// <summary>
		/// Updates the value data for the given <see cref="ValuePersistenceJob"/> in the database.
		/// </summary>
		/// <param name="transaction">The <see cref="DbTransaction"/> object to use.</param>
		/// <param name="entityModificationEntry">The <see cref="EntityModificationEntry"/> that must be used to log the operation.</param>
		/// <param name="job">The <see cref="ValuePersistenceJob"/> to execute.</param>
		private void UpdateValueData(DbTransaction transaction, EntityModificationEntry entityModificationEntry, ValuePersistenceJob job)
		{
			var fieldIdsWithValues = job.GetFieldIdsWithValues ().ToList ();

			Druid localEntityId = job.LocalEntityId;
			DbKey dbKey = this.GetPersistentEntityDbKey (job.Entity);

			DbTable table = this.SchemaEngine.GetEntityTable (localEntityId);
			string tableName = table.GetSqlName ();

			SqlFieldList fields = new SqlFieldList ();
			fields.AddRange (this.CreateSqlFields (localEntityId, fieldIdsWithValues));

			if (job.IsRootTypeJob)
			{
				fields.Add (this.CreateSqlFieldForLog (table, entityModificationEntry));
			}

			SqlFieldList conditions = new SqlFieldList ();
			conditions.Add (this.CreateConditionForRowId (table, dbKey));

			transaction.SqlBuilder.UpdateData (tableName, fields, conditions);
			this.DbInfrastructure.ExecuteNonQuery (transaction);
		}


		/// <summary>
		/// Inserts the reference data for the given <see cref="ReferencePersistenceJob"/> in the database .
		/// </summary>
		/// <param name="transaction">The <see cref="DbTransaction"/> object to use.</param>
		/// <param name="newEntityKeys">The <see cref="Dictionary{AbstractEntity, DbKey}"/> containing the mapping from the newly inserted <see cref="AbstractEntity"/> to their newly assigned <see cref="DbKey"/>.</param>
		/// <param name="job">The <see cref="ReferencePersistenceJob"/> to execute.</param>
		private void UpdateReferenceData(DbTransaction transaction, Dictionary<AbstractEntity, DbKey> newEntityKeys, ReferencePersistenceJob job)
		{
			Druid localEntityId = job.LocalEntityId;

			DbTable table = this.SchemaEngine.GetEntityTable (localEntityId);
			string tableName = table.GetSqlName ();

			SqlFieldList fields = new SqlFieldList ();

			fields.AddRange (this.CreateSqlFields (newEntityKeys, localEntityId, job.GetFieldIdsWithTargets ()));

			SqlFieldList conditions = new SqlFieldList ();

			DbKey dbKey = this.GetEntityDbKey (job.Entity, newEntityKeys);

			conditions.Add (this.CreateConditionForRowId (table, dbKey));

			transaction.SqlBuilder.UpdateData (tableName, fields, conditions);
			this.DbInfrastructure.ExecuteNonQuery (transaction);
		}


		/// <summary>
		/// Deletes all reference relations that reference the given entity.
		/// </summary>
		/// <param name="transaction">The <see cref="DbTransaction"/> object to use.</param>
		/// <param name="localEntityId">The <see cref="Druid"/> defining the type of the entities to update.</param>
		/// <param name="fieldId">The <see cref="Druid"/> defining the field to update.</param>
		/// <param name="targetKey">The key that defines the references that must be removed.</param>
		private void DeleteEntityTargetReference(DbTransaction transaction, Druid localEntityId, Druid fieldId, DbKey targetKey)
		{
			DbTable table = this.SchemaEngine.GetEntityTable (localEntityId);
			DbColumn column = this.SchemaEngine.GetEntityFieldColumn (localEntityId, fieldId);
			string tableName = table.GetSqlName ();

			SqlFieldList fields = new SqlFieldList ();
			fields.Add (this.CreateSqlFieldForEntityField (localEntityId, fieldId, System.DBNull.Value));

			SqlFieldList conditions = new SqlFieldList ();
			conditions.Add (this.CreateConditionForField (column, targetKey.Id.Value));

			transaction.SqlBuilder.UpdateData (tableName, fields, conditions);

			this.DbInfrastructure.ExecuteNonQuery (transaction);
		}


		/// <summary>
		/// Inserts the collection data for the given <see cref="CollectionPersistenceJob"/> in the database .
		/// </summary>
		/// <param name="transaction">The <see cref="DbTransaction"/> object to use.</param>
		/// <param name="newEntityKeys">The <see cref="Dictionary{AbstractEntity, DbKey}"/> containing the mapping from the newly inserted <see cref="AbstractEntity"/> to their newly assigned <see cref="DbKey"/>.</param>
		/// <param name="job">The <see cref="CollectionPersistenceJob"/> to execute.</param>
		private void InsertCollectionData(DbTransaction transaction, Dictionary<AbstractEntity, DbKey> newEntityKeys, CollectionPersistenceJob job)
		{
			DbKey sourceKey = this.GetEntityDbKey (job.Entity, newEntityKeys);
			var targetKeys = job.Targets.Select (t => this.GetEntityDbKey (t, newEntityKeys));

			this.InsertEntityCollection (transaction, job.LocalEntityId, job.FieldId, sourceKey, targetKeys);
		}


		/// <summary>
		/// Updates the collection data for the given <see cref="CollectionPersistenceJob"/> in the database .
		/// </summary>
		/// <param name="transaction">The <see cref="DbTransaction"/> object to use.</param>
		/// <param name="newEntityKeys">The <see cref="Dictionary{AbstractEntity, DbKey}"/> containing the mapping from the newly inserted <see cref="AbstractEntity"/> to their newly assigned <see cref="DbKey"/>.</param>
		/// <param name="job">The <see cref="CollectionPersistenceJob"/> to execute.</param>
		private void UpdateCollectionData(DbTransaction transaction, Dictionary<AbstractEntity, DbKey> newEntityKeys, CollectionPersistenceJob job)
		{
			// TODO This function might be optimized by having a better policy to delete/update/Insert
			// the relation rows. It could take advantage to what already exists in the database, which
			// would have the following two advantages:
			// - It might require less queries
			// - If another user has modified the data and our version of the relations is not up to
			//   date, it might do less overwrite.

			DbKey sourceKey = this.GetEntityDbKey (job.Entity, newEntityKeys);
			var targetKeys = job.Targets.Select (t => this.GetEntityDbKey (t, newEntityKeys));

			Druid localEntityId = job.LocalEntityId;
			Druid fieldId = job.FieldId;

			this.DeleteEntitySourceCollection (transaction, localEntityId, fieldId, sourceKey);
			this.InsertEntityCollection (transaction, localEntityId, fieldId, sourceKey, targetKeys);
		}


		/// <summary>
		/// Inserts the relation from an <see cref="AbstractEntity"/> to a sequence of
		/// <see cref="AbstractEntity"/> in the database.
		/// </summary>
		/// <param name="transaction">The <see cref="DbTransaction"/> object to use.</param>
		/// <param name="localEntityId">The <see cref="Druid"/> defining the local type of the <see cref="AbstractEntity"/> that contains the field.</param>
		/// <param name="fieldId">The <see cref="Druid"/> defining the field of the relation</param>
		/// <param name="sourceKey">The <see cref="DbKey"/> of the <see cref="AbstractEntity"/> that is the source of the relation.</param>
		/// <param name="targetKeys">The sequence of <see cref="DbKey"/> of the <see cref="AbstractEntity"/> that are the target of the relation.</param>
		private void InsertEntityCollection(DbTransaction transaction, Druid localEntityId, Druid fieldId, DbKey sourceKey, IEnumerable<DbKey> targetKeys)
		{
			DbTable table = this.SchemaEngine.GetEntityFieldTable (localEntityId, fieldId);

			string tableName = table.GetSqlName ();

			List<DbKey> targetKeysList = targetKeys.ToList ();

			for (int rank = 0; rank < targetKeysList.Count; rank++)
			{
				SqlFieldList fields = new SqlFieldList ();

				fields.Add (this.CreateSqlFieldForSourceId (table, sourceKey));
				fields.Add (this.CreateSqlFieldForTargetId (table, targetKeysList[rank]));
				fields.Add (this.CreateSqlFieldForRank (table, rank));

				transaction.SqlBuilder.InsertData (tableName, fields);

				this.DbInfrastructure.ExecuteNonQuery (transaction);
			}
		}


		/// <summary>
		/// Deletes all items of the relation defined by the given field that have a given source in
		/// the database.
		/// </summary>
		/// <param name="transaction">The <see cref="DbTransaction"/> object to use.</param>
		/// <param name="localEntityId">The <see cref="Druid"/> defining the type of the <see cref="AbstractEntity"/> owning the field.</param>
		/// <param name="fieldId">The <see cref="Druid"/> of the field.</param>
		/// <param name="sourceKey">The <see cref="DbKey"/> of the source of the relation.</param>
		private void DeleteEntitySourceCollection(DbTransaction transaction, Druid localEntityId, Druid fieldId, DbKey sourceKey)
		{
			DbTable table = this.SchemaEngine.GetEntityFieldTable (localEntityId, fieldId);
			string tableName = table.GetSqlName ();

			SqlFieldList conditions = new SqlFieldList ();
			conditions.Add (this.CreateConditionForSourceId (table, sourceKey));

			transaction.SqlBuilder.RemoveData (tableName, conditions);
			this.DbInfrastructure.ExecuteNonQuery (transaction);
		}


		/// <summary>
		/// Deletes all items of the relation defined by the given field that have a given target in
		/// the database.
		/// </summary>
		/// <param name="transaction">The <see cref="DbTransaction"/> object to use.</param>
		/// <param name="localEntityId">The <see cref="Druid"/> defining the type of the <see cref="AbstractEntity"/> owning the field.</param>
		/// <param name="fieldId">The <see cref="Druid"/> of the field.</param>
		/// <param name="targetKey">The <see cref="DbKey"/> of the target of the relation.</param>
		private void DeleteEntityTargetCollection(DbTransaction transaction, Druid localEntityId, Druid fieldId, DbKey targetKey)
		{
			DbTable table = this.SchemaEngine.GetEntityFieldTable (localEntityId, fieldId);
			string tableName = table.GetSqlName ();

			SqlFieldList conditions = new SqlFieldList ();
			conditions.Add (this.CreateConditionForTargetId (table, targetKey));

			transaction.SqlBuilder.RemoveData (tableName, conditions);

			this.DbInfrastructure.ExecuteNonQuery (transaction);
		}


		/// <summary>
		/// Builds a <see cref="SqlField"/> that contains the condition that holds true when the
		/// id of a row is equal to the given <see cref="DbKey"/> for the given <see cref="DbTable"/>.
		/// </summary>
		/// <param name="table">The <see cref="DbTable"/> targeted by the condition.</param>
		/// <param name="dbKey">The value of the <see cref="DbKey"/> of the condition.</param>
		/// <returns>The <see cref="SqlField"/> that holds the condition.</returns>
		private SqlField CreateConditionForRowId(DbTable table, DbKey dbKey)
		{
			DbColumn column = table.Columns[EntitySchemaBuilder.EntityTableColumnIdName];
			long value = dbKey.Id.Value;

			return this.CreateConditionForField (column, value);
		}


		/// <summary>
		/// Builds a <see cref="SqlField"/> that contains the condition that holds true when the
		/// source id  field of a row is equal to the given <see cref="DbKey"/> for the given
		/// relation <see cref="DbTable"/>.
		/// </summary>
		/// <param name="table">The <see cref="DbTable"/> targeted by the condition.</param>
		/// <param name="dbKey">The value of the <see cref="DbKey"/> of the condition.</param>
		/// <returns>The <see cref="SqlField"/> that holds the condition.</returns>
		private SqlField CreateConditionForSourceId(DbTable table, DbKey dbKey)
		{
			DbColumn column = table.Columns[EntitySchemaBuilder.EntityFieldTableColumnSourceIdName];
			long value = dbKey.Id.Value;

			return this.CreateConditionForField (column, value);
		}


		/// <summary>
		/// Builds a <see cref="SqlField"/> that contains the condition that holds true when the
		/// target id  field of a row is equal to the given <see cref="DbKey"/> for the given
		/// relation <see cref="DbTable"/>.
		/// </summary>
		/// <param name="table">The <see cref="DbTable"/> targeted by the condition.</param>
		/// <param name="dbKey">The value of the <see cref="DbKey"/> of the condition.</param>
		/// <returns>The <see cref="SqlField"/> that holds the condition.</returns>
		private SqlField CreateConditionForTargetId(DbTable table, DbKey dbKey)
		{
			DbColumn column = table.Columns[EntitySchemaBuilder.EntityFieldTableColumnTargetIdName];
			long value = dbKey.Id.Value;

			return this.CreateConditionForField (column, value);
		}


		/// <summary>
		/// Builds a <see cref="SqlField"/> that contains the condition that holds true when the
		/// given <see cref="DbColumn"/> has the given value.
		/// </summary>
		/// <param name="column">The <see cref="DbColumn"/> that is targeted by the condition.</param>
		/// <param name="value">The value that the <see cref="DbColumn"/> must have in order to satisfy the condition.</param>
		/// <returns>The <see cref="SqlField"/> that holds the condition.</returns>
		private SqlField CreateConditionForField(DbColumn column, object value)
		{
			string columnName = column.GetSqlName ();
			SqlField columnField = SqlField.CreateName (columnName);

			DbTypeDef columnType = column.Type;
			DbRawType rawType = columnType.RawType;
			DbSimpleType simpleType = columnType.SimpleType;
			DbNumDef numDef = columnType.NumDef;

			object convertedValue = this.DataConverter.FromCresusToDatabaseValue (rawType, simpleType, numDef, value);
			DbRawType convertedRawType = this.DataConverter.FromDotNetToDatabaseType (columnType.RawType);

			SqlField constantField = SqlField.CreateConstant (convertedValue, convertedRawType);

			SqlFunction condition = new SqlFunction (
				SqlFunctionCode.CompareEqual,
				columnField,
				constantField
			);

			return SqlField.CreateFunction (condition);
		}


		/// <summary>
		/// Builds the sequence of <see cref="SqlField"></see> that are used to set the values of the
		/// <see cref="AbstractEntity"></see> in an INSERT or an UPDATE SQL request.
		/// </summary>
		/// <param name="localEntityId">The <see cref="Druid"></see> defining the local type of the <see cref="AbstractEntity"></see>.</param>
		/// <param name="fieldIdsWithValues">The mapping from the field ids to their values.</param>
		/// <returns>The sequence of the <see cref="SqlField"></see> that are used within the SQl Request.</returns>
		private IEnumerable<SqlField> CreateSqlFields(Druid localEntityId, IEnumerable<KeyValuePair<Druid, object>> fieldIdsWithValues)
		{
			foreach (var fieldIdWithValue in fieldIdsWithValues)
			{
				Druid fieldId = fieldIdWithValue.Key;
				object value = fieldIdWithValue.Value;

				var field = this.TypeEngine.GetField (localEntityId, fieldId);

				AbstractType fieldType = field.Type as AbstractType;
				INullableType nullableType = field.GetNullableType ();

				if (UndefinedValue.IsUndefinedValue (value) || nullableType.IsNullValue (value))
				{
					value = (nullableType.IsNullable) ? System.DBNull.Value : fieldType.DefaultValue;
				}

				yield return this.CreateSqlFieldForEntityField (localEntityId, fieldId, value);
			}
		}


		/// <summary>
		/// Builds the sequence of <see cref="SqlField"/> that are used to set the references of the
		/// <see cref="AbstractEntity"/> in an UPDATE SQL request.
		/// </summary>
		/// <param name="newEntityKeys">The key of the entities that have been recently inserted.</param>
		/// <param name="localEntityId">The <see cref="Druid"/> defining the type of the entity whose reference fields to update.</param>
		/// <param name="fieldIdsWithTargets">The mapping of the field that must be updated and their value.</param>
		/// <returns>The sequence of the <see cref="SqlField"></see> that are used within the SQl Request.</returns>
		private IEnumerable<SqlField> CreateSqlFields(Dictionary<AbstractEntity, DbKey> newEntityKeys, Druid localEntityId, IEnumerable<KeyValuePair<Druid, AbstractEntity>> fieldIdsWithTargets)
		{
			foreach (var fieldIdsWithTarget in fieldIdsWithTargets)
			{
				Druid fieldId = fieldIdsWithTarget.Key;
				AbstractEntity target = fieldIdsWithTarget.Value;

				object value = System.DBNull.Value;

				if (target != null)
				{
					value = this.GetEntityDbKey (target, newEntityKeys).Id.Value;
				}

				yield return this.CreateSqlFieldForEntityField (localEntityId, fieldId, value);
			}
		}


		/// <summary>
		/// Builds the <see cref="SqlField"/> used to set the value of the row of a row in a SQL
		/// request.
		/// </summary>
		/// <param name="table">The <see cref="DbTable"/> targeted by the SQL request.</param>
		/// <param name="key">The value of the <see cref="DbKey"/>.</param>
		/// <returns>The <see cref="SqlField"/> that contain the setter clause.</returns>
		private SqlField CreateSqlFieldForKey(DbTable table, DbKey key)
		{
			DbColumn column = table.Columns[EntitySchemaBuilder.EntityTableColumnIdName];
			object value = key.Id.Value;

			return this.CreateSqlFieldForColumn (column, value);
		}


		/// <summary>
		/// Builds the <see cref="SqlField"/> used to set the value of the instance type of a row in
		/// a SQL request.
		/// </summary>
		/// <param name="table">The <see cref="DbTable"/> targeted by the SQL request.</param>
		/// <param name="leafEntityId">The value of the instance type.</param>
		/// <returns>The <see cref="SqlField"/> that contain the setter clause.</returns>
		private SqlField CreateSqlFieldForType(DbTable table, Druid leafEntityId)
		{
			DbColumn column = table.Columns[EntitySchemaBuilder.EntityTableColumnEntityTypeIdName];
			object value = leafEntityId.ToLong ();

			return this.CreateSqlFieldForColumn (column, value);
		}


		/// <summary>
		/// Builds the <see cref="SqlField"/> used to set the value of log of a row in a SQL request.
		/// </summary>
		/// <param name="table">The <see cref="DbTable"/> targeted by the SQL request.</param>
		/// <param name="entityModificationEntry">The <see cref="EntityModificationEntry"/> that must be used to log the operation.</param>
		/// <returns>The <see cref="SqlField"/> that contain the setter clause.</returns>
		private SqlField CreateSqlFieldForLog(DbTable table, EntityModificationEntry entityModificationEntry)
		{
			DbColumn column = table.Columns[EntitySchemaBuilder.EntityTableColumnEntityModificationEntryIdName];
			long value = entityModificationEntry.EntryId.Value;

			return this.CreateSqlFieldForColumn (column, value);
		}


		/// <summary>
		/// Builds the <see cref="SqlField"/> used to set the value of source key of a relation row
		/// in a SQL request.
		/// </summary>
		/// <param name="table">The <see cref="DbTable"/> targeted by the SQL request.</param>
		/// <param name="key">The value of the source key.</param>
		/// <returns>The <see cref="SqlField"/> that contain the setter clause.</returns>
		private SqlField CreateSqlFieldForSourceId(DbTable table, DbKey key)
		{
			DbColumn column = table.Columns[EntitySchemaBuilder.EntityFieldTableColumnSourceIdName];
			object value = key.Id.Value;

			return this.CreateSqlFieldForColumn (column, value);
		}


		/// <summary>
		/// Builds the <see cref="SqlField"/> used to set the value of target key of a relation row
		/// in a SQL request.
		/// </summary>
		/// <param name="table">The <see cref="DbTable"/> targeted by the SQL request.</param>
		/// <param name="key">The value of the target key.</param>
		/// <returns>The <see cref="SqlField"/> that contain the setter clause.</returns>
		private SqlField CreateSqlFieldForTargetId(DbTable table, DbKey key)
		{
			DbColumn column = table.Columns[EntitySchemaBuilder.EntityFieldTableColumnTargetIdName];
			object value = key.Id.Value;

			return this.CreateSqlFieldForColumn (column, value);
		}


		/// <summary>
		/// Builds the <see cref="SqlField"/> used to set the value of rank of a relation row
		/// in a SQL request.
		/// </summary>
		/// <param name="table">The <see cref="DbTable"/> targeted by the SQL request.</param>
		/// <param name="rank">The value of the rank.</param>
		/// <returns>The <see cref="SqlField"/> that contain the setter clause.</returns>
		private SqlField CreateSqlFieldForRank(DbTable table, int rank)
		{
			DbColumn column = table.Columns[EntitySchemaBuilder.EntityFieldTableColumnRankName];
			object value = rank;

			return this.CreateSqlFieldForColumn (column, value);
		}


		/// <summary>
		/// Builds the <see cref="SqlField"/> used to set the value of an <see cref="AbstractEntity"/>
		/// field of a row in a SQL request.
		/// </summary>
		/// <param name="localEntityId">The <see cref="Druid"/> defining the id of the entity.</param>
		/// <param name="fieldId">The <see cref="Druid"/> defining the id of the field.</param>
		/// <param name="value">The value of the field.</param>
		/// <returns>The <see cref="SqlField"/> that contain the setter clause.</returns>
		private SqlField CreateSqlFieldForEntityField(Druid localEntityId, Druid fieldId, object value)
		{
			DbColumn column = this.SchemaEngine.GetEntityFieldColumn (localEntityId, fieldId);

			return this.CreateSqlFieldForColumn (column, value);
		}


		/// <summary>
		/// Builds the <see cref="SqlField"/> used to set the value of a <see cref="DbColumn"/> in a
		/// row in a SQL request.
		/// </summary>
		/// <param name="column">The <see cref="DbColumn"/> whose value to set.</param>
		/// <param name="value">The value that must be set.</param>
		/// <returns>The <see cref="SqlField"/> that contain the setter clause.</returns>
		private SqlField CreateSqlFieldForColumn(DbColumn column, object value)
		{
			DbTypeDef columnType = column.Type;
			DbRawType rawType = columnType.RawType;
			DbSimpleType simpleType = columnType.SimpleType;
			DbNumDef numDef = columnType.NumDef;

			object convertedValue = this.DataConverter.FromCresusToDatabaseValue (rawType, simpleType, numDef, value);
			DbRawType convertedRawType = this.DataConverter.FromDotNetToDatabaseType (columnType.RawType);

			SqlField SqlField = SqlField.CreateConstant (convertedValue, convertedRawType);
			SqlField.Alias = column.GetSqlName ();

			return SqlField;
		}


		/// <summary>
		/// Retrieves the <see cref="DbKey"/> of an <see cref="AbstractEntity"/> that is persistent
		/// within the <see cref="DataContext"/> used by this instance.
		/// </summary>
		/// <param name="entity">The <see cref="AbstractEntity"/> whose <see cref="DbKey"/> to obtain.</param>
		/// <returns>The <see cref="DbKey"/> of the given <see cref="AbstractEntity"/>.</returns>
		private DbKey GetPersistentEntityDbKey(AbstractEntity entity)
		{
			return this.DataContext.GetNormalizedEntityKey (entity).Value.RowKey;
		}


		/// <summary>
		/// Retrieves the <see cref="DbKey"/> of an <see cref="AbstractEntity"/> that is not persistent
		/// within the <see cref="DataContext"/> used by this instance but is defined in the given
		/// <see cref="Dictionary{AbstractEntity,DbKey}"/>.
		/// </summary>
		/// <param name="entity">The <see cref="AbstractEntity"/> whose <see cref="DbKey"/> to obtain.</param>
		/// <param name="newEntityKeys">The <see cref="Dictionary{AbstractEntity, DbKey}"/> containing the mapping between the newly inserted <see cref="AbstractEntity"/> and their <see cref="DbKey"/>.</param>
		/// <returns>The <see cref="DbKey"/> of the given <see cref="AbstractEntity"/>.</returns>
		private DbKey GetNonPersistentEntityDbKey(AbstractEntity entity, Dictionary<AbstractEntity, DbKey> newEntityKeys)
		{
			return newEntityKeys[entity];
		}


		/// <summary>
		/// Retrieves the <see cref="DbKey"/> of an <see cref="AbstractEntity"/>.
		/// </summary>
		/// <param name="entity">The <see cref="AbstractEntity"/> whose <see cref="DbKey"/> to obtain.</param>
		/// <param name="newEntityKeys">The <see cref="Dictionary{AbstractEntity, DbKey}"/> containing the mapping between the newly inserted <see cref="AbstractEntity"/> and their <see cref="DbKey"/>.</param>
		/// <returns>The <see cref="DbKey"/> of the given <see cref="AbstractEntity"/>.</returns>
		private DbKey GetEntityDbKey(AbstractEntity entity, Dictionary<AbstractEntity, DbKey> newEntityKeys)
		{
			if (this.DataContext.IsPersistent (entity))
			{
				return this.GetPersistentEntityDbKey (entity);
			}
			else
			{
				return this.GetNonPersistentEntityDbKey (entity, newEntityKeys);
			}
		}


	}


}

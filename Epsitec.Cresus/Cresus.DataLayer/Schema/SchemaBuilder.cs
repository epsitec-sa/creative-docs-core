//	Copyright � 2007-2008, EPSITEC SA, 1400 Yverdon-les-Bains, Switzerland
//	Author: Pierre ARNAUD, Maintainer: Pierre ARNAUD

using Epsitec.Common.Support;
using Epsitec.Common.Types;

using Epsitec.Cresus.Database;
using Epsitec.Cresus.DataLayer;

using System.Collections.Generic;


namespace Epsitec.Cresus.DataLayer.Schema
{
	
	
	/// <summary>
	/// The <c>SchemaBuilder</c> class is used internally to build
	/// one or more <see cref="DbTable"/> instances based on entity ids.
	/// </summary>
	internal class SchemaBuilder
	{
		
		
		/// <summary>
		/// Initializes a new instance of the <see cref="SchemaBuilder"/> class.
		/// </summary>
		/// <param name="schemaEngine">The schema engine.</param>
		public SchemaBuilder(SchemaEngine schemaEngine)
		{
			this.schemaEngine = schemaEngine;
			
			this.tables = new List<DbTable> ();
			this.newTables = new List<DbTable> ();

			this.tablesDictionary = new Dictionary<Druid, DbTable> ();
			this.typesDictionary = new Dictionary<Druid, DbTypeDef> ();
		}


		public void CreateSchema(Druid entityId)
		{
			using (this.BeginTransaction ())
			{
				this.Add (entityId);
				this.CommitTransaction ();
			}
		}


		/// <summary>
		/// Starts a transaction. This will inherit the currently active
		/// transaction, if one is already active. Use this method inside
		/// a <c>using</c> block.
		/// </summary>
		/// <returns>A <see cref="System.IDisposable"/> object which must be
		/// disposed of when the transaction block ends.</returns>
		private System.IDisposable BeginTransaction()
		{
			if (this.transaction != null && this.transaction.IsActive)
			{
				throw new System.InvalidOperationException ("SchemaEngineTableBuilder already has an active transaction");
			}

			this.transaction = this.schemaEngine.DbInfrastructure.InheritOrBeginTransaction (DbTransactionMode.ReadWrite);

			return this.transaction;
		}

		/// <summary>
		/// Commits the active transaction, which must have been started
		/// with the <see cref="BeginTransaction"/> method.
		/// </summary>
		private void CommitTransaction()
		{
			this.transaction.Commit ();
			this.transaction = null;
		}


		/// <summary>
		/// Adds the table graph required to store data for the specified entity.
		/// </summary>
		/// <param name="entityId">The entity id.</param>
		public void Add(Druid entityId)
		{
			System.Diagnostics.Debug.Assert (this.newTables.Count == 0);

			this.CreateTable (entityId);

			foreach (DbTable table in this.newTables)
			{
				this.schemaEngine.DbInfrastructure.RegisterNewDbTable (this.transaction, table);
			}

			foreach (DbTable table in this.newTables)
			{
				this.schemaEngine.DbInfrastructure.RegisterColumnRelations (this.transaction, table);
			}

			this.newTables.Clear ();
		}


		public Dictionary<Druid, DbTable> GetNewTableDefinitions()
		{
			return this.tablesDictionary;
		}


		public Dictionary<Druid, DbTypeDef> GetNewTypeDefinitions()
		{
			return this.typesDictionary;
		}


		/// <summary>
		/// Creates the table definition, without registering it with the database.
		/// This method will be called recursively.
		/// </summary>
		/// <param name="entityId">The entity id.</param>
		/// <returns>The root table for the entity.</returns>
		private DbTable CreateTable(Druid entityId)
		{
			DbTable table;

			//	If we have already generated a table definition for this entity,
			//	just re-use it. This check makes circular references possible.
			
			if (this.tablesDictionary.TryGetValue (entityId, out table))
			{
				return table;
			}

			ResourceManager manager = this.schemaEngine.DbInfrastructure.DefaultContext.ResourceManager;
			StructuredType entityType = TypeRosetta.CreateTypeObject (manager, entityId) as StructuredType;
			
			if (entityType == null)
			{
				throw new System.ArgumentException ("Invalid entity ID", "entityId");
			}
			
			this.AssertTransaction ();

			table = this.schemaEngine.GetEntityTableDefinition (entityId);

			if (table != null)
			{
				//	Add the table to the list of tables, but not to the tables
				//	dictionary - that one will only contain "new" tables.

				this.tables.Add (table);
				return table;
			}

			table = this.schemaEngine.DbInfrastructure.CreateDbTable (entityId, DbElementCat.ManagedUserData, DbRevisionMode.TrackChanges);

			table.Comment = table.DisplayName;
			this.newTables.Add (table);
			this.tables.Add (table);
			this.tablesDictionary[entityId] = table;

			if (entityType.BaseTypeId.IsEmpty)
			{
				//	If this entity has no parent in the class hierarchy, then we
				//	need to add a special identification column, which can be used
				//	to map a row to its proper derived entity class.

				DbTypeDef typeDef = this.schemaEngine.DbInfrastructure.ResolveDbType (this.transaction, Tags.TypeKeyId);
				DbColumn column = new DbColumn (Tags.ColumnInstanceType, typeDef, DbColumnClass.Data, DbElementCat.Internal, DbRevisionMode.Immutable);
				
				table.Columns.Add (column);
			}
			else
			{
				this.CreateTable (entityType.BaseTypeId);
			}



			//	For every locally defined field (this includes field inserted
			//	through an interface, possibly locally overridden), create a
			//	column in the table.

			foreach (StructuredTypeField field in entityType.Fields.Values)
			{
				if ((field.Membership == FieldMembership.Local) ||
					(field.Membership == FieldMembership.LocalOverride))
				{
					if (field.Source == FieldSource.Value)
					{		
						this.CreateColumn (table, field);
					}
				}
			}

			return table;
		}

		/// <summary>
		/// Creates a column to represent the specified field.
		/// </summary>
		/// <param name="table">The table.</param>
		/// <param name="field">The field.</param>
		private void CreateColumn(DbTable table, StructuredTypeField field)
		{
			switch (field.Relation)
			{
				case FieldRelation.None:
					this.CreateDataColumn (table, field);
					break;
				
				case FieldRelation.Reference:
					this.CreateRelationColumn (table, field, DbCardinality.Reference);
					break;
				
				case FieldRelation.Collection:
					this.CreateRelationColumn (table, field, DbCardinality.Collection);
					break;

				default:
					throw new System.NotImplementedException (string.Format ("Missing support for Relation.{0}", field.Relation));
			}
		}

		/// <summary>
		/// Creates a data column for the specified field.
		/// </summary>
		/// <param name="table">The table.</param>
		/// <param name="field">The field.</param>
		private void CreateDataColumn(DbTable table, StructuredTypeField field)
		{
			DbTypeDef typeDef = this.CreateTypeDef (field.Type, field.Options);
			DbColumn  column  = new DbColumn (field.CaptionId, typeDef, DbColumnClass.Data, DbElementCat.ManagedUserData, DbRevisionMode.TrackChanges);

			column.Comment = column.DisplayName;

			table.Columns.Add (column);
		}

		/// <summary>
		/// Creates a relation column for the specified field.
		/// </summary>
		/// <param name="table">The table.</param>
		/// <param name="field">The field.</param>
		/// <param name="cardinality">The cardinality of the relation.</param>
		private void CreateRelationColumn(DbTable table, StructuredTypeField field, DbCardinality cardinality)
		{
			System.Diagnostics.Debug.Assert (cardinality != DbCardinality.None);
			System.Diagnostics.Debug.Assert (field.CaptionId.IsValid);
			System.Diagnostics.Debug.Assert (field.Type is StructuredType);

			DbTable  target = this.CreateTable (field.TypeId);
			DbColumn column = DbTable.CreateRelationColumn (this.transaction, this.schemaEngine.DbInfrastructure, field.CaptionId, target, DbRevisionMode.TrackChanges, cardinality);

			table.Columns.Add (column);
		}

		/// <summary>
		/// Gets the type definition object for the specified type. If the
		/// type is not yet known, register it with the database.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <param name="options">The field options.</param>
		/// <returns>
		/// The <see cref="DbTypeDef"/> instance or <c>null</c>.
		/// </returns>
		private DbTypeDef CreateTypeDef(INamedType type, FieldOptions options)
		{
			this.AssertTransaction ();

			if (type == null)
			{
				throw new System.ArgumentNullException ("type");
			}
			if (type is IStructuredType)
			{
				throw new System.InvalidOperationException ("Cannot create type definition for structure");
			}

			DbTypeDef typeDef;
			Druid typeId = type.CaptionId;

			System.Diagnostics.Debug.Assert (typeId.IsValid);

			if (this.typesDictionary.TryGetValue (typeId, out typeDef))
			{
				return typeDef;
			}

			typeDef = this.schemaEngine.GetTypeDefinition (typeId);

			if (typeDef != null)
			{
				return typeDef;
			}

			DbInfrastructure infrastructure = this.schemaEngine.DbInfrastructure;
			typeDef = infrastructure.ResolveDbType (this.transaction, type);

			if (typeDef == null)
			{
				typeDef = new DbTypeDef (type, options == FieldOptions.Nullable);

				infrastructure.RegisterNewDbType (this.transaction, typeDef);
			}

			System.Diagnostics.Debug.Assert (typeDef != null);
			System.Diagnostics.Debug.Assert (!typeDef.Key.IsEmpty);

			this.typesDictionary[typeId] = typeDef;

			return typeDef;
			
		}

		private void AssertTransaction()
		{
			if ((this.transaction != null) &&
				(this.transaction.IsActive))
			{
				return;
			}
			else
			{
				throw new System.InvalidOperationException ("SchemaEngineTableBuilder has no active transaction");
			}
		}


		private SchemaEngine schemaEngine;
		private DbTransaction transaction;

		private List<DbTable> tables;
		private List<DbTable> newTables;

		private Dictionary<Druid, DbTable> tablesDictionary;
		private Dictionary<Druid, DbTypeDef> typesDictionary;


	}


}

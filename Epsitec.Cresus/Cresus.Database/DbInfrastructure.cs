//	Copyright � 2003-2006, EPSITEC SA, CH-1092 BELMONT, Switzerland
//	Author: Pierre ARNAUD, Maintainer: Pierre ARNAUD

using System.Collections.Generic;

using Epsitec.Common.Types;

namespace Epsitec.Cresus.Database
{
	/// <summary>
	/// The <c>DbInfrastructure</c> class provides support for the database
	/// infrastructure needed by CRESUS (internal tables, metadata, etc.)
	/// </summary>
	public sealed class DbInfrastructure : System.IDisposable
	{
		public DbInfrastructure()
		{
			this.localizations    = "fr";
			this.liveTransactions = new List<DbTransaction> ();
			this.releaseRequested = new List<IDbAbstraction> ();
		}
		
		
		public string[]							DefaultLocalizations
		{
			get
			{
				if (string.IsNullOrEmpty (this.localizations))
				{
					return new string[0];
				}
				else
				{
					return this.localizations.Split ('/');
				}
			}
			set
			{
				if ((value == null) ||
					(value.Length == 0))
				{
					this.localizations = null;
				}
				else
				{
					this.localizations = string.Join ("/", value);
				}
			}
		}
		
		public ISqlBuilder						DefaultSqlBuilder
		{
			get
			{
				return this.abstraction.SqlBuilder;
			}
		}
		
		public ISqlEngine						DefaultSqlEngine
		{
			get
			{
				return this.sqlEngine;
			}
		}
		
		public IDbAbstraction					DefaultDbAbstraction
		{
			get
			{
				return this.abstraction;
			}
		}
		
		public ITypeConverter					Converter
		{
			get
			{
				return this.converter;
			}
		}
		
		public DbTransaction[]					LiveTransactions
		{
			get
			{
				lock (this.liveTransactions)
				{
					return this.liveTransactions.ToArray ();
				}
			}
		}
		
		public DbLogger							Logger
		{
			get
			{
				return this.logger;
			}
		}
		
		public DbClientManager					ClientManager
		{
			get
			{
				if (this.LocalSettings.IsServer)
				{
					return this.clientManager;
				}
				else
				{
					return null;
				}
			}
		}
		
		public bool								IsInGlobalLock
		{
			get
			{
				return this.globalLock.IsWriterLockHeld;
			}
		}
		
		public Settings.Globals					GlobalSettings
		{
			get
			{
				return this.globals;
			}
		}
		
		public Settings.Locals					LocalSettings
		{
			get
			{
				return this.locals;
			}
		}


		/// <summary>
		/// Creates a new database using the specified database access. This
		/// is only possible if the <c>DbInfrastructure</c> is not yet connected
		/// to any database.
		/// </summary>
		/// <param name="access">The database access.</param>
		public void CreateDatabase(DbAccess access)
		{
			this.CreateDatabase (access, false);
		}

		/// <summary>
		/// Creates a new database using the specified database access. This
		/// is only possible if the <c>DbInfrastructure</c> is not yet connected
		/// to any database.
		/// </summary>
		/// <param name="access">The database access.</param>
		/// <param name="isServer">If set to <c>true</c> creates the database for a server.</param>
		public void CreateDatabase(DbAccess access, bool isServer)
		{
			if (this.access.IsValid)
			{
				throw new Exceptions.GenericException (this.access, "A database already exists for this DbInfrastructure");
			}
			
			this.access = access;
			this.access.CreateDatabase = true;

			this.InitializeDatabaseAbstraction ();
			
			this.types.RegisterTypes ();
			
			//	The database is now ready to be filled. At this point, it is totally
			//	empty of any tables.
			
			System.Diagnostics.Debug.Assert (this.abstraction.QueryUserTableNames ().Length == 0);
			
			using (DbTransaction transaction = this.BeginTransaction (DbTransactionMode.ReadWrite))
			{
				BootHelper helper = new BootHelper (this, transaction);

				//	Create the tables required for our own metadata management. The
				//	created tables must be commited before they can be populated.

				helper.CreateTableTableDef ();
				helper.CreateTableColumnDef ();
				helper.CreateTableTypeDef ();
				helper.CreateTableEnumValDef ();
				helper.CreateTableLog ();
				helper.CreateTableRequestQueue ();
				helper.CreateTableClientDef ();
				
				transaction.Commit ();
			}
			
			//	TODO: handle clientId
			this.clientId = 1;
			
			//	Fill the tables with the initial metadata.
			
			using (DbTransaction transaction = this.BeginTransaction (DbTransactionMode.ReadWrite))
			{
				this.SetupTables (transaction);
				
				transaction.Commit ();
			}

			using (DbTransaction transaction = this.BeginTransaction (DbTransactionMode.ReadWrite))
			{
				Settings.Globals.CreateTable (this, transaction, Settings.Globals.Name, DbElementCat.Internal, DbRevisionMode.Disabled, DbReplicationMode.Automatic);
				Settings.Locals.CreateTable (this, transaction, Settings.Locals.Name, DbElementCat.Internal, DbRevisionMode.Disabled, DbReplicationMode.None);
				
				transaction.Commit ();
			}
			
			//	Create the default values for the global and local settings :
			
			using (DbTransaction transaction = this.BeginTransaction (DbTransactionMode.ReadWrite))
			{
				Settings.Globals globals = new Settings.Globals (this, transaction);
				Settings.Locals  locals  = new Settings.Locals (this, transaction);
				
				locals.ClientId = this.clientId;
				locals.IsServer = isServer;

				globals.PersistToBase (transaction);
				locals.PersistToBase (transaction);

				transaction.Commit ();
			}
			
			//	The database is ready. Start using it...
			
			this.StartUsingDatabase ();
		}

		/// <summary>
		/// Attaches to an existing database. This is only possible if the
		/// <c>DbInfrastructure</c> is not yet connected to any database.
		/// </summary>
		/// <param name="access">The database access.</param>
		public void AttachToDatabase(DbAccess access)
		{
			if (this.access.IsValid)
			{
				throw new Exceptions.GenericException (this.access, "Database already attached");
			}
			
			this.access = access;
			this.access.CreateDatabase = false;

			this.InitializeDatabaseAbstraction ();
			
			//	The database must have user tables (at least, it has our metadata
			//	tables)...
			
			System.Diagnostics.Debug.Assert (this.abstraction.QueryUserTableNames ().Length > 0);
			
			using (DbTransaction transaction = this.BeginTransaction (DbTransactionMode.ReadOnly))
			{
				this.internalTables.Add (this.ResolveDbTable (transaction, Tags.TableLog));
				this.internalTables.Add (this.ResolveDbTable (transaction, Tags.TableTableDef));
				this.internalTables.Add (this.ResolveDbTable (transaction, Tags.TableColumnDef));
				this.internalTables.Add (this.ResolveDbTable (transaction, Tags.TableTypeDef));
				this.internalTables.Add (this.ResolveDbTable (transaction, Tags.TableEnumValDef));
				this.internalTables.Add (this.ResolveDbTable (transaction, Tags.TableClientDef));
				this.internalTables.Add (this.ResolveDbTable (transaction, Tags.TableRequestQueue));
				
				this.types.ResolveTypes (transaction);
				
				transaction.Commit ();
			}
			
			this.StartUsingDatabase ();
		}

		/// <summary>
		/// Sets up a roaming database. Call <c>AttachToDatabase</c> first.
		/// </summary>
		/// <param name="clientId">The current client id.</param>
		public void SetupRoamingDatabase(int clientId)
		{
			if (this.clientId != clientId)
			{
				using (DbTransaction transaction = this.BeginTransaction ())
				{
					//	The last ID stored in the log is considered to be the active id at the
					//	synchronization point (setting up a roaming database requires an image
					//	of the server database to start its operations).
					
					DbId lastServerId = this.logger.CurrentId;
					
					//	Since this is a fresh roaming database, all table ids will be reset to
					//	one (1) :
					
					this.ResetColumnData (transaction, this.internalTables[Tags.TableTableDef], Tags.ColumnNextId, DbId.CreateId (1, clientId));
					
					//	Clear tables which should not be replicated between the server and the
					//	client databases :
					
					this.ClearTable (transaction, this.internalTables[Tags.TableRequestQueue]);
					this.ClearTable (transaction, this.internalTables[Tags.TableClientDef]);
					
					//	Update the logger in order to use the new client id instead of the
					//	id found in the copied database :
					
					this.logger.Detach ();
					
					this.logger = new DbLogger ();
					this.logger.DefineClientId (clientId);
					this.logger.DefineInitialLogId (1);
					this.logger.Attach (this, this.internalTables[Tags.TableLog]);
					this.logger.CreateInitialEntry (transaction);
					
					//	Define local settings based on the client :
					
					this.LocalSettings.ClientId  = clientId;
					this.LocalSettings.IsServer  = false;
					this.LocalSettings.SyncLogId = lastServerId.Value;
					
					this.LocalSettings.PersistToBase (transaction);
					
					transaction.Commit ();
					
					this.clientId = clientId;
				}
			}
		}

		/// <summary>
		/// Clears the table by removing all rows.
		/// </summary>
		/// <param name="transaction">The transaction.</param>
		/// <param name="table">The table.</param>
		private void ClearTable(DbTransaction transaction, DbTable table)
		{
			transaction.SqlBuilder.RemoveData (table.CreateSqlName (), null);
			this.ExecuteNonQuery (transaction);
		}

		/// <summary>
		/// Resets the data in the specified column to the specified value.
		/// </summary>
		/// <param name="transaction">The transaction.</param>
		/// <param name="table">The table.</param>
		/// <param name="columnName">Name of the column.</param>
		/// <param name="data">The data.</param>
		private void ResetColumnData(DbTransaction transaction, DbTable table, string columnName, DbId data)
		{
			DbColumn              column = table.Columns[columnName];
			Collections.SqlFields fields = new Collections.SqlFields ();
			
			fields.Add (column.CreateSqlName (), SqlField.CreateConstant (data));
			
			transaction.SqlBuilder.UpdateData (table.CreateSqlName (), fields, null);
			
			this.ExecuteNonQuery (transaction);
		}

		/// <summary>
		/// Releases the database connection. If transactions are still active
		/// for this database, the connection will be released autmatically
		/// when the transactions finish.
		/// </summary>
		public void ReleaseConnection()
		{
			this.ReleaseConnection (this.abstraction);
		}

		/// <summary>
		/// Releases the database connection. If transactions are still active
		/// for this database, the connection will be released autmatically
		/// when the transactions finish.
		/// </summary>
		/// <param name="abstraction">The database abstraction.</param>
		private void ReleaseConnection(IDbAbstraction abstraction)
		{
			lock (this.liveTransactions)
			{
				foreach (DbTransaction item in this.liveTransactions)
				{
					if (item.Database == abstraction)
					{
						this.releaseRequested.Add (abstraction);
						return;
					}
				}
			}				
			
			abstraction.ReleaseConnection ();
		}


		/// <summary>
		/// Creates the database access.
		/// </summary>
		/// <param name="name">The database file name.</param>
		/// <returns>The database access.</returns>
		public static DbAccess CreateDatabaseAccess(string name)
		{
			return new DbAccess ("Firebird", name, "localhost", "sysdba", "masterkey", false);
		}

		/// <summary>
		/// Creates the database access.
		/// </summary>
		/// <param name="provider">The database provider.</param>
		/// <param name="name">The database file name.</param>
		/// <returns>The database access.</returns>
		public static DbAccess CreateDatabaseAccess(string provider, string name)
		{
			return new DbAccess (provider, name, "localhost", "sysdba", "masterkey", false);
		}


		/// <summary>
		/// Creates a new database abstraction. This will create a new connection
		/// with the database.
		/// </summary>
		/// <returns>The database abstraction.</returns>
		public IDbAbstraction CreateDatabaseAbstraction()
		{
			IDbAbstraction abstraction = DbFactory.CreateDatabaseAbstraction (this.access);
			
			abstraction.SqlBuilder.AutoClear = true;
			
			return abstraction;
		}


		/// <summary>
		/// Begins a read and write transaction for the default database abstraction.
		/// </summary>
		/// <returns>The transaction.</returns>
		public DbTransaction BeginTransaction()
		{
			return this.BeginTransaction (DbTransactionMode.ReadWrite);
		}

		/// <summary>
		/// Begins the specified transaction for the default database abstraction.
		/// </summary>
		/// <param name="mode">The transaction mode.</param>
		/// <returns>The transaction.</returns>
		public DbTransaction BeginTransaction(DbTransactionMode mode)
		{
			return this.BeginTransaction (mode, this.abstraction);
		}

		/// <summary>
		/// Begins a transaction for the specified database abstraction.
		/// </summary>
		/// <param name="mode">The transaction mode.</param>
		/// <param name="abstraction">The database abstraction.</param>
		/// <returns>The transaction.</returns>
		public DbTransaction BeginTransaction(DbTransactionMode mode, IDbAbstraction abstraction)
		{
			System.Diagnostics.Debug.Assert (abstraction != null);
			
			//	We currently allow a single transaction per database abstraction,
			//	because some ADO.NET providers do not support cascaded transactions.
			
			DbTransaction transaction = null;
			
			//	Make sure we can get a lock on the database. If not, this means
			//	that someone holds a global lock and we may not access the database
			//	at all, even within a transaction. This is the case when restoring
			//	a database, for instance.
			
			this.DatabaseLock (abstraction);
			
			try
			{
				switch (mode)
				{
					case DbTransactionMode.ReadOnly:
						transaction = new DbTransaction (abstraction.BeginReadOnlyTransaction (), abstraction, this, mode);
						break;
					
					case DbTransactionMode.ReadWrite:
						transaction = new DbTransaction (abstraction.BeginReadWriteTransaction (), abstraction, this, mode);
						break;
					
					default:
						throw new System.ArgumentOutOfRangeException ("mode", mode, string.Format ("Transaction mode {0} not supported", mode.ToString ()));
				}
			}
			catch
			{
				this.DatabaseUnlock (abstraction);
				throw;
			}
			
			return transaction;
		}


		/// <summary>
		/// Creates a minimal database table definition. This will only contain
		/// the basic id and status columns required by <c>DbInfrastructure</c>.
		/// </summary>
		/// <param name="name">The table name.</param>
		/// <param name="category">The category.</param>
		/// <param name="revisionMode">The revision mode.</param>
		/// <returns>The database table definition.</returns>
		public DbTable CreateDbTable(string name, DbElementCat category, DbRevisionMode revisionMode)
		{
			switch (category)
			{
				case DbElementCat.Internal:
					throw new Exceptions.GenericException (this.access, string.Format ("Users may not create internal tables (table '{0}')", name));
				
				case DbElementCat.ManagedUserData:
					return this.CreateTable(name, category, revisionMode, DbReplicationMode.Automatic);
				
				default:
					throw new Exceptions.GenericException (this.access, string.Format ("Unsupported category {0} specified for table '{1}'", category, name));
			}
		}

		/// <summary>
		/// Registers a new table for this database. This creates both the
		/// metadata and the database table itself.
		/// </summary>
		/// <param name="transaction">The transaction.</param>
		/// <param name="table">The table.</param>
		public void RegisterNewDbTable(DbTransaction transaction, DbTable table)
		{
			this.RegisterDbTable (transaction, table, false);
		}

		/// <summary>
		/// Registers a known table for this database. This call is reserved for
		/// the replication service which must be able to create tables in the
		/// database for which there already exists metadata.
		/// </summary>
		/// <param name="transaction">The transaction.</param>
		/// <param name="table">The table.</param>
		public void RegisterKnownDbTable(DbTransaction transaction, DbTable table)
		{
			this.RegisterDbTable (transaction, table, true);
		}

		/// <summary>
		/// Unregisters a table from the database. The metadata is updated but
		/// the real database table is not dropped for security reasons.
		/// </summary>
		/// <param name="table">The table.</param>
		public void UnregisterDbTable(DbTable table)
		{
			using (DbTransaction transaction = this.BeginTransaction ())
			{
				this.UnregisterDbTable (transaction, table);
				transaction.Commit ();
			}
		}

		/// <summary>
		/// Unregisters a table from the database. The metadata is updated but
		/// the real database table is not dropped for security reasons.
		/// </summary>
		/// <param name="transaction">The transaction.</param>
		/// <param name="table">The table.</param>
		public void UnregisterDbTable(DbTransaction transaction, DbTable table)
		{
			System.Diagnostics.Debug.Assert (transaction != null);
			
			this.CheckForKnownTable (transaction, table);
			
			DbKey oldKey = table.Key;
			DbKey newKey = new DbKey (oldKey.Id, DbRowStatus.Deleted);
			
			this.UpdateKeyInRow (transaction, Tags.TableTableDef, oldKey, newKey);
		}

		/// <summary>
		/// Resolves the database table definition with the specified name. This
		/// will return the same object when called multiple times with the same
		/// name, unless the cache is cleared with <c>ClearCaches</c>.
		/// </summary>
		/// <param name="transaction">The transaction.</param>
		/// <param name="tableName">Name of the table.</param>
		/// <returns>The table definition.</returns>
		public DbTable ResolveDbTable(DbTransaction transaction, string tableName)
		{
			DbKey key = this.FindDbTableKey (transaction, tableName);
			return this.ResolveDbTable (transaction, key);
		}

		/// <summary>
		/// Resolves the database table definition with the specified key. This
		/// will return the same object when called multiple times with the same
		/// key, unless the cache is cleared with <c>ClearCaches</c>.
		/// </summary>
		/// <param name="transaction">The transaction.</param>
		/// <param name="key">The key to the table metadata.</param>
		/// <returns>The table definition.</returns>
		public DbTable ResolveDbTable(DbTransaction transaction, DbKey key)
		{
			if (key.IsEmpty)
			{
				return null;
			}

			lock (this.tableCache)
			{
				DbTable table = this.tableCache[key];
				
				if (table == null)
				{
					List<DbTable> tables = this.LoadDbTable (transaction, key, DbRowSearchMode.LiveActive);
					
					if (tables.Count > 0)
					{
						System.Diagnostics.Debug.Assert (tables.Count == 1);
						System.Diagnostics.Debug.Assert (tables[0].Key == key);
						System.Diagnostics.Debug.Assert (this.tableCache[key] == tables[0]);
						
						table = tables[0];
					}
				}
				
				return table;
			}
		}

		/// <summary>
		/// Finds all live database table definitions belonging to the specified
		/// category (either internal or user data).
		/// </summary>
		/// <param name="transaction">The transaction.</param>
		/// <param name="category">The table category.</param>
		/// <returns>The table definitions or an empty array.</returns>
		public DbTable[] FindDbTables(DbTransaction transaction, DbElementCat category)
		{
			return this.FindDbTables (transaction, category, DbRowSearchMode.LiveActive);
		}

		/// <summary>
		/// Finds the database table definitions belonging to the specified
		/// category (either internal or user data).
		/// </summary>
		/// <param name="transaction">The transaction.</param>
		/// <param name="category">The table category.</param>
		/// <param name="rowSearchMode">The row search mode.</param>
		/// <returns>The table definitions or an empty array.</returns>
		public DbTable[] FindDbTables(DbTransaction transaction, DbElementCat category, DbRowSearchMode rowSearchMode)
		{
			List<DbTable> list = this.LoadDbTable (transaction, DbKey.Empty, rowSearchMode);
			
			if (category != DbElementCat.Any)
			{
				list.RemoveAll
					(
						delegate (DbTable table)
						{
							return table.Category != category;
						}
					);
			}
			
			return list.ToArray ();
		}


		/// <summary>
		/// Clears the table and type caches. This will force a reload of the
		/// table definitions and type definitions.
		/// </summary>
		public void ClearCaches()
		{
			lock (this.tableCache)
			{
				this.tableCache.ClearCache ();
			}
			lock (this.typeCache)
			{
				this.typeCache.ClearCache ();
			}
		}
		

		public void RegisterColumnRelations(DbTransaction transaction, DbTable table)
		{
			if (transaction == null)
			{
				using (transaction = this.BeginTransaction ())
				{
					this.RegisterColumnRelations (transaction, table);
					transaction.Commit ();
					return;
				}
			}
			
			//	Passe en revue toutes les colonnes de type ID qui font r�f�rence � une table
			//	et enregistre l'information dans la table de d�finition des r�f�rences.
			//
			//	Note: il faut que les tables aient �t� enregistr�es aupr�s de Cr�sus pour
			//	que cette m�thode fonctionne (on a besoin des IDs des tables et des colonnes
			//	concern�es).
			
			System.Collections.Hashtable ref_columns = new System.Collections.Hashtable ();
			
			for (int i = 0; i < table.Columns.Count; i++)
			{
				DbColumn column = table.Columns[i];
				
				switch (column.ColumnClass)
				{
					case DbColumnClass.RefId:
						
						string parent_name = column.TargetTableName;
						
						if (parent_name != null)
						{
							DbTable parent_table = this.ResolveDbTable (transaction, parent_name);
							
							if (parent_table == null)
							{
								string message = string.Format ("Table '{0}' referenced from '{1}.{2}' not found in database.", parent_name, table.Name, column.Name);
								throw new Exceptions.GenericException (this.access, message);
							}
							
							DbKey source_table_key  = table.Key;
							DbKey source_column_key = column.Key;
							DbKey parent_table_key  = parent_table.Key;
							
							if (source_table_key.IsEmpty)
							{
								string message = string.Format ("Reference of '{0}' from '{1}.{2}' specifies unregistered table '{1}'.", parent_name, table.Name, column.Name);
								throw new Exceptions.GenericException (this.access, message);
							}

							if (source_column_key.IsEmpty)
							{
								string message = string.Format ("Reference of '{0}' from '{1}.{2}' specifies unregistered column '{2}'.", parent_name, table.Name, column.Name);
								throw new Exceptions.GenericException (this.access, message);
							}

							if (parent_table_key.IsEmpty)
							{
								string message = string.Format ("Reference of '{0}' from '{1}.{2}' specifies unregistered table '{0}'.", parent_name, table.Name, column.Name);
								throw new Exceptions.GenericException (this.access, message);
							}
							
							this.UpdateColumnRelation (transaction, source_table_key, source_column_key, parent_table_key);
						}
						break;
					
					default:
						break;
				}
			}
		}


		public void      RegisterNewDbType(DbTransaction transaction, DbTypeDef typeDef)
		{
			//	Enregistre un nouveau type dans la base de donn�es. Ceci va attribuer au
			//	type une clef DbKey et v�rifier qu'il n'y a pas de collision avec un
			//	�ventuel type d�j� existant.
			
			if (transaction == null)
			{
				using (transaction = this.BeginTransaction ())
				{
					this.RegisterNewDbType (transaction, typeDef);
					transaction.Commit ();
					return;
				}
			}
			
			this.CheckForUnknownType (transaction, typeDef);

			long table_id = this.NewRowIdInTable (transaction, this.internalTables[Tags.TableTypeDef].Key, 1);

#if false
			//	TODO: handle enumeration types here:
			
			DbTypeEnum type_enum = typeDef as DbTypeEnum;
			
			long enum_id  = (type_enum == null) ? 0 : this.NewRowIdInTable (transaction, this.internal_tables[Tags.TableEnumValDef].Key, type_enum.Count);
#endif

			//	Cr�e la ligne de description du type :
			
			typeDef.DefineKey (new DbKey (table_id));
			this.InsertTypeDefRow (transaction, typeDef);
#if false
			if (type_enum != null)
			{
				//	Cr�e les lignes de description des valeurs de l'�num�ration :
				
				int i = 0;
				
				foreach (DbEnumValue value in type_enum.Values)
				{
					value.DefineInternalKey (new DbKey (enum_id + i));
					this.InsertEnumValueDefRow (transaction, type, value);
					i++;
				}
			}
#endif
		}
		
		public void      UnregisterDbType(DbTransaction transaction, DbTypeDef typeDef)
		{
			//	Supprime la description du type de la base. Pour des raisons de s�curit�,
			//	le type SQL n'est pas r�ellement supprim�.
			
			if (transaction == null)
			{
				using (transaction = this.BeginTransaction ())
				{
					this.UnregisterDbType (transaction, typeDef);
					transaction.Commit ();
					return;
				}
			}
			
			this.CheckForKnownType (transaction, typeDef);
			
			DbKey old_key = typeDef.Key;
			DbKey new_key = new DbKey (old_key.Id, DbRowStatus.Deleted);
			
			this.UpdateKeyInRow (transaction, Tags.TableTypeDef, old_key, new_key);
		}
		
		public DbTypeDef ResolveDbType(DbTransaction transaction, string type_name)
		{
			DbKey key = this.FindDbTypeKey (transaction, type_name);
			return this.ResolveDbType (transaction, key);
		}
		
		public DbTypeDef  ResolveDbType(DbTransaction transaction, DbKey key)
		{
			if (key.IsEmpty)
			{
				return null;
			}

			//	Trouve le type corredpondant � une clef sp�cifique.
			
			lock (this.typeCache)
			{
				DbTypeDef typeDef = this.typeCache[key];
				
				if (typeDef == null)
				{
					List<DbTypeDef> types = this.LoadDbType (transaction, key, DbRowSearchMode.LiveActive);
					
					if (types.Count > 0)
					{
						typeDef = types[0] as DbTypeDef;
						
//-						System.Diagnostics.Debug.WriteLine (string.Format ("Loaded {0} {1} from database.", type.GetType ().Name, type.Name));
						System.Diagnostics.Debug.Assert (types.Count == 1);
						System.Diagnostics.Debug.Assert (typeDef.Key == key);
						
						this.typeCache[key] = typeDef;
					}
				}
				
				return typeDef;
			}
		}
		
		public DbTypeDef[]  FindDbTypes(DbTransaction transaction)
		{
			return this.FindDbTypes (transaction, DbRowSearchMode.LiveActive);
		}
		
		public DbTypeDef[]  FindDbTypes(DbTransaction transaction, DbRowSearchMode row_search_mode)
		{
			//	Liste tous les types.
			
			return this.LoadDbType (transaction, DbKey.Empty, row_search_mode).ToArray ();
		}

		
		public SqlField CreateSqlField(DbColumn column, int value)
		{
			//	TODO: pas une bonne id�e d'avoir ces m�thodes de cr�ation qui ne tiennent
			//	pas compte des types internes support�s par la base... c'est valable pour
			//	toutes les variantes de CreateSqlField.

			SqlField field = SqlField.CreateConstant (value, DbRawType.Int32);
			field.Alias = column.Name;
			return field;
		}

		public SqlField CreateSqlField(DbColumn column, long value)
		{
			SqlField field = SqlField.CreateConstant (value, DbRawType.Int64);
			field.Alias = column.Name;
			return field;
		}

		public SqlField CreateSqlField(DbColumn column, string value)
		{
			SqlField field = SqlField.CreateConstant (value, DbRawType.String);
			field.Alias = column.Name;
			return field;
		}

		public SqlField CreateSqlField(DbColumn column, System.DateTime value)
		{
			SqlField field = SqlField.CreateConstant (value, DbRawType.DateTime);
			field.Alias = column.Name;
			return field;
		}

		public SqlField CreateEmptySqlField(DbColumn column)
		{
			DbRawType raw_type = column.Type.RawType;
			SqlField field    = SqlField.CreateConstant (null, raw_type);
			field.Alias = column.CreateSqlName ();
			return field;
		}




		
		internal DbTable CreateTable(string name, DbElementCat category, DbRevisionMode revision_mode, DbReplicationMode replication_mode)
		{
			System.Diagnostics.Debug.Assert (revision_mode != DbRevisionMode.Unknown);
			
			DbTable table = new DbTable (name);
			
			DbTypeDef typeDef = this.internalTypes[Tags.TypeKeyId];
			
			DbColumn col_id   = new DbColumn (Tags.ColumnId,     this.internalTypes[Tags.TypeKeyId],     DbColumnClass.KeyId, DbElementCat.Internal);
			DbColumn col_stat = new DbColumn (Tags.ColumnStatus, this.internalTypes[Tags.TypeKeyStatus], DbColumnClass.KeyStatus, DbElementCat.Internal);
			DbColumn col_log  = new DbColumn (Tags.ColumnRefLog, this.internalTypes[Tags.TypeKeyId],     DbColumnClass.RefInternal, DbElementCat.Internal);
			
			table.DefineCategory (category);
			table.DefineRevisionMode (revision_mode);
			table.DefineReplicationMode (replication_mode);
			
			table.Columns.Add (col_id);
			table.Columns.Add (col_stat);
			table.Columns.Add (col_log);
			
			table.PrimaryKeys.Add (col_id);
			
			return table;
		}


		private void RegisterDbTable(DbTransaction transaction, DbTable table, bool check_for_known)
		{
			//	Enregistre une nouvelle table dans la base de donn�es. Ceci va attribuer �
			//	la table une clef DbKey et v�rifier qu'il n'y a pas de collision avec une
			//	�ventuelle table d�j� existante. Cela va aussi attribuer des colonnes pour
			//	la nouvelle table.
			
			if (transaction == null)
			{
				using (transaction = this.BeginTransaction ())
				{
					this.RegisterDbTable (transaction, table, check_for_known);
					transaction.Commit ();
					return;
				}
			}
			
			this.CheckForRegisteredTypes (transaction, table);
			
			if (check_for_known)
			{
				this.CheckForKnownTable (transaction, table);
			}
			else
			{
				this.CheckForUnknownTable (transaction, table);
				
				long table_id  = this.NewRowIdInTable (transaction, this.internalTables[Tags.TableTableDef] .Key, 1);
				long column_id = this.NewRowIdInTable (transaction, this.internalTables[Tags.TableColumnDef].Key, table.Columns.Count);
				
				//	Cr�e la ligne de description de la table :
				
				table.DefineKey (new DbKey (table_id));
				table.UpdatePrimaryKeyInfo ();
				
				this.InsertTableDefRow (transaction, table);
				
				//	Cr�e les lignes de description des colonnes :
				
				for (int i = 0; i < table.Columns.Count; i++)
				{
					table.Columns[i].DefineKey (new DbKey (column_id + i));
					this.InsertColumnDefRow (transaction, table, table.Columns[i]);
				}
			}
			
			//	Finalement, il faut cr�er la table elle-m�me :
			
			SqlTable sql_table = table.CreateSqlTable (this.converter);
			
			transaction.SqlBuilder.InsertTable (sql_table);
			this.ExecuteSilent (transaction);
		}


		private void CheckForRegisteredTypes(DbTransaction transaction, DbTable table)
		{
			//	V�rifie que tous les types utilis�s dans la d�finition des colonnes sont bien
			//	connus (on v�rifie qu'ils ont une clef valide).
			
			Collections.DbColumns columns = table.Columns;
			
			for (int i = 0; i < columns.Count; i++)
			{
				DbTypeDef typeDef = columns[i].Type;
				
				System.Diagnostics.Debug.Assert (typeDef != null);
				
				if (typeDef.Key.IsEmpty)
				{
					string message = string.Format ("Unregistered type '{0}' used in table '{1}', column '{2}'.",
						/**/						typeDef.Name, table.Name, columns[i].Name);
					
					throw new Exceptions.GenericException (this.access, message);
				}
			}
		}

		private void CheckForUnknownType(DbTransaction transaction, DbTypeDef typeDef)
		{
			System.Diagnostics.Debug.Assert (typeDef != null);
			
			if (this.CountMatchingRows (transaction, Tags.TableTypeDef, Tags.ColumnName, typeDef.Name) > 0)
			{
				string message = string.Format ("Type {0} already exists in database.", typeDef.Name);
				throw new Exceptions.GenericException (this.access, message);
			}
		}

		private void CheckForKnownType(DbTransaction transaction, DbTypeDef typeDef)
		{
			System.Diagnostics.Debug.Assert (typeDef != null);
			
			if (this.CountMatchingRows (transaction, Tags.TableTypeDef, Tags.ColumnName, typeDef.Name) == 0)
			{
				string message = string.Format ("Type {0} does not exist in database.", typeDef.Name);
				throw new Exceptions.GenericException (this.access, message);
			}
		}

		private void CheckForUnknownTable(DbTransaction transaction, DbTable table)
		{
			//	Cherche si une table avec ce nom existe dans la base. Si c'est le cas,
			//	g�n�re une exception.
			//
			//	NOTE:
			//
			//	On cherche les lignes dans CR_TABLE_DEF dont la colonne CR_NAME contient le nom
			//	sp�cifi� et dont CR_REV = 0. Cette seconde condition est n�cessaire, car une table
			//	d�truite figure encore dans CR_TABLE_DEF avec CR_REV > 0, et elle ne doit pas �tre
			//	compt�e.
			
			if (this.CountMatchingRows (transaction, Tags.TableTableDef, Tags.ColumnName, table.Name) > 0)
			{
				string message = string.Format ("Table {0} already exists in database.", table.Name);
				throw new Exceptions.GenericException (this.access, message);
			}
		}

		private void CheckForKnownTable(DbTransaction transaction, DbTable table)
		{
			//	Cherche si une table avec ce nom existe dans la base. Si ce n'est pas le cas,
			//	g�n�re une exception.
			//
			//	NOTE:
			//
			//	On cherche les lignes dans CR_TABLE_DEF dont la colonne CR_NAME contient le nom
			//	sp�cifi� et dont CR_REV = 0. Cette seconde condition est n�cessaire, car une table
			//	d�truite figure encore dans CR_TABLE_DEF avec CR_REV > 0, et elle ne doit pas �tre
			//	compt�e.
			
			if (this.CountMatchingRows (transaction, Tags.TableTableDef, Tags.ColumnName, table.Name) == 0)
			{
				string message = string.Format ("Table {0} does not exist in database.", table.Name);
				throw new Exceptions.GenericException (this.access, message);
			}
		}
		
		
		public void GlobalLock()
		{
			this.globalLock.AcquireWriterLock (this.lockTimeout);
		}
		
		public void GlobalUnlock()
		{
			this.globalLock.ReleaseWriterLock ();
		}
		
		
		internal void DatabaseLock(IDbAbstraction database)
		{
			this.globalLock.AcquireReaderLock (this.lockTimeout);
			
			if (System.Threading.Monitor.TryEnter (database, this.lockTimeout) == false)
			{
				this.globalLock.ReleaseReaderLock ();
				throw new Exceptions.DeadLockException (this.access, "Cannot lock database.");
			}
		}
		
		internal void DatabaseUnlock(IDbAbstraction database)
		{
			this.globalLock.ReleaseReaderLock ();
			System.Threading.Monitor.Exit (database);
		}
		
		
		internal void NotifyBeginTransaction(DbTransaction transaction)
		{
			IDbAbstraction abstraction = transaction.Database;
			
			lock (this.liveTransactions)
			{
				foreach (DbTransaction item in this.liveTransactions)
				{
					if (item.Database == abstraction)
					{
						throw new Exceptions.GenericException (this.access, string.Format ("Nested transactions not supported."));
					}
				}
				
				this.liveTransactions.Add (transaction);
			}
		}
		
		internal void NotifyEndTransaction(DbTransaction transaction)
		{
			IDbAbstraction abstraction = transaction.Database;
			
			this.DatabaseUnlock (abstraction);

			bool release = false;
			
			lock (this.liveTransactions)
			{
				if (this.liveTransactions.Remove (transaction) == false)
				{
					throw new Exceptions.GenericException (this.access, string.Format ("Ending wrong transaction."));
				}
				
				if (this.releaseRequested.Contains (abstraction))
				{
					this.releaseRequested.Remove (abstraction);
					release = true;
				}
			}

			if (release)
			{
				this.ReleaseConnection (abstraction);
			}
		}
		
		
		public void Execute(DbTransaction transaction, DbRichCommand command)
		{
			if (transaction == null)
			{
				using (transaction = this.BeginTransaction ())
				{
					this.Execute (transaction, command);
					transaction.Commit ();
					return;
				}
			}
			
			this.sqlEngine.Execute (command, this, transaction);
		}
		
		
		public int ExecuteSilent(DbTransaction transaction)
		{
			if (transaction == null)
			{
				using (transaction = this.BeginTransaction ())
				{
					int result = this.ExecuteSilent (transaction, transaction.SqlBuilder);
					transaction.Commit ();
					return result;
				}
			}
			else
			{
				return this.ExecuteSilent (transaction, transaction.SqlBuilder);
			}
		}
		
		public int ExecuteSilent(DbTransaction transaction, ISqlBuilder builder)
		{
			System.Diagnostics.Debug.Assert (transaction != null);
			System.Diagnostics.Debug.Assert (builder != null);
			
			int count = builder.CommandCount;
			
			if (count < 1)
			{
				return 0;
			}
			
			using (System.Data.IDbCommand command = builder.CreateCommand (transaction.Transaction))
			{
				int result;
				this.sqlEngine.Execute (command, DbCommandType.Silent, count, out result);
				return result;
			}
		}
		
		public object ExecuteScalar(DbTransaction transaction)
		{
			if (transaction == null)
			{
				using (transaction = this.BeginTransaction ())
				{
					object value = this.ExecuteScalar (transaction, transaction.SqlBuilder);
					transaction.Commit ();
					return value;
				}
			}
			else
			{
				return this.ExecuteScalar (transaction, transaction.SqlBuilder);
			}
		}
		
		public object ExecuteScalar(DbTransaction transaction, ISqlBuilder builder)
		{
			System.Diagnostics.Debug.Assert (transaction != null);
			System.Diagnostics.Debug.Assert (builder != null);
			
			int count = builder.CommandCount;
			
			if (count < 1)
			{
				return null;
			}
			
			using (System.Data.IDbCommand command = builder.CreateCommand (transaction.Transaction))
			{
				object data;
				
				this.sqlEngine.Execute (command, DbCommandType.ReturningData, count, out data);
				
				return data;
			}
		}
		
		public object ExecuteNonQuery(DbTransaction transaction)
		{
			if (transaction == null)
			{
				using (transaction = this.BeginTransaction ())
				{
					object value = this.ExecuteNonQuery (transaction, transaction.SqlBuilder);
					transaction.Commit ();
					return value;
				}
			}
			else
			{
				return this.ExecuteNonQuery (transaction, transaction.SqlBuilder);
			}
		}
		
		public object ExecuteNonQuery(DbTransaction transaction, ISqlBuilder builder)
		{
			System.Diagnostics.Debug.Assert (transaction != null);
			System.Diagnostics.Debug.Assert (builder != null);
			
			int count = builder.CommandCount;
			
			if (count < 1)
			{
				return null;
			}
			
			using (System.Data.IDbCommand command = builder.CreateCommand (transaction.Transaction))
			{
				object data;
				
				this.sqlEngine.Execute (command, DbCommandType.NonQuery, count, out data);
				
				return data;
			}
		}
		
		public System.Data.DataSet ExecuteRetData(DbTransaction transaction)
		{
			if (transaction == null)
			{
				using (transaction = this.BeginTransaction ())
				{
					System.Data.DataSet value = this.ExecuteRetData (transaction, transaction.SqlBuilder);
					transaction.Commit ();
					return value;
				}
			}
			else
			{
				return this.ExecuteRetData (transaction, transaction.SqlBuilder);
			}
		}
		
		public System.Data.DataSet ExecuteRetData(DbTransaction transaction, ISqlBuilder builder)
		{
			System.Diagnostics.Debug.Assert (transaction != null);
			System.Diagnostics.Debug.Assert (builder != null);
			
			int count = builder.CommandCount;
			
			if (count < 1)
			{
				return null;
			}
			
			using (System.Data.IDbCommand command = builder.CreateCommand (transaction.Transaction))
			{
				System.Data.DataSet data;
				
				this.sqlEngine.Execute (command, DbCommandType.ReturningData, count, out data);
				
				return data;
			}
		}
		
		public System.Data.DataTable ExecuteSqlSelect(DbTransaction transaction, SqlSelect query, int min_rows)
		{
			if (transaction == null)
			{
				using (transaction = this.BeginTransaction (DbTransactionMode.ReadOnly))
				{
					System.Data.DataTable value = this.ExecuteSqlSelect (transaction, transaction.SqlBuilder, query, min_rows);
					transaction.Commit ();
					return value;
				}
			}
			else
			{
				return this.ExecuteSqlSelect (transaction, transaction.SqlBuilder, query, min_rows);
			}
		}
		
		public System.Data.DataTable ExecuteSqlSelect(DbTransaction transaction, ISqlBuilder builder, SqlSelect query, int min_rows)
		{
			System.Diagnostics.Debug.Assert (transaction != null);
			System.Diagnostics.Debug.Assert (builder != null);
			
			builder.SelectData (query);
			
			System.Data.DataSet data_set;
			System.Data.DataTable data_table;
			
			data_set = this.ExecuteRetData (transaction);
			
			if ((data_set == null) ||
				(data_set.Tables.Count != 1))
			{
				throw new Exceptions.GenericException (this.access, string.Format ("Query failed."));
			}
			
			data_table = data_set.Tables[0];
			
			if (data_table.Rows.Count < min_rows)
			{
				throw new Exceptions.GenericException (this.access, string.Format ("Query returned to few rows; expected {0}, found {1}.", min_rows, data_table.Rows.Count));
			}
			
			return data_table;
		}
		
		
		public DbKey FindDbTableKey(DbTransaction transaction, string name)
		{
			return this.FindLiveKey (this.FindDbKeys (transaction, Tags.TableTableDef, name));
		}
		
		public DbKey FindDbTypeKey(DbTransaction transaction, string name)
		{
			return this.FindLiveKey (this.FindDbKeys (transaction, Tags.TableTypeDef, name));
		}
		
		
		internal DbKey FindLiveKey(DbKey[] keys)
		{
			for (int i = 0; i < keys.Length; i++)
			{
				switch (keys[i].Status)
				{
					case DbRowStatus.Live:
					case DbRowStatus.Copied:
						return keys[i];
				}
			}
			
			return DbKey.Empty;
		}
		
		internal DbKey[] FindDbKeys(DbTransaction transaction, string table_name, string row_name)
		{
			//	Trouve la (ou les) clefs des lignes de la table 'table_name', pour lesquelles le
			//	contenu de la colonne CR_NAME correspond au nom d�fini par 'row_name'.
			
			SqlSelect query = new SqlSelect ();
			
			query.Fields.Add ("T_ID",   SqlField.CreateName ("T", Tags.ColumnId));
			query.Fields.Add ("T_STAT",	SqlField.CreateName ("T", Tags.ColumnStatus));
			
			query.Tables.Add ("T", SqlField.CreateName (table_name));
			
			query.Conditions.Add (new SqlFunction (SqlFunctionCode.CompareEqual, SqlField.CreateName ("T", Tags.ColumnName), SqlField.CreateConstant (row_name, DbRawType.String)));
			
			System.Data.DataTable data_table = this.ExecuteSqlSelect (transaction, query, 0);
			
			DbKey[] keys = new DbKey[data_table.Rows.Count];
			
			for (int i = 0; i < data_table.Rows.Count; i++)
			{
				System.Data.DataRow row = data_table.Rows[i];
				
				long id;
				int  status;
				
				InvariantConverter.Convert (row["T_ID"],   out id);
				InvariantConverter.Convert (row["T_STAT"], out status);
				
				keys[i] = new DbKey (id, DbKey.ConvertFromIntStatus (status));
			}
			
			return keys;
		}
		
		
		public int CountMatchingRows(DbTransaction transaction, string table_name, string name_column, string value)
		{
			int count = 0;
			
			//	Compte combien de lignes dans la table ont le texte sp�cifi� dans la colonne sp�cifi�e.
			//	Ne consid�re que les lignes actives.
			
			if (transaction == null)
			{
				using (transaction = this.BeginTransaction (DbTransactionMode.ReadOnly))
				{
					count = this.CountMatchingRows (transaction, table_name, name_column, value);
					transaction.Commit ();
					return count;
				}
			}
			
			SqlSelect query = new SqlSelect ();
			
			query.Fields.Add ("N", new SqlAggregate (SqlAggregateFunction.Count, SqlField.CreateAll ()));
			query.Tables.Add ("T", SqlField.CreateName (table_name));
			
			query.Conditions.Add (new SqlFunction (SqlFunctionCode.CompareEqual, SqlField.CreateName ("T", name_column), SqlField.CreateConstant (value, DbRawType.String)));

			DbInfrastructure.AddKeyExtraction (query.Conditions, "T", DbRowSearchMode.LiveActive);
			
			transaction.SqlBuilder.SelectData (query);
			
			InvariantConverter.Convert (this.ExecuteScalar (transaction), out count);
			
			return count;
		}
		
		
		public void UpdateKeyInRow(DbTransaction transaction, string table_name, DbKey old_key, DbKey new_key)
		{
			//	Met � jour la clef de la ligne sp�cifi�e. Ceci est utile pour mettre � jour
			//	le champ DbRowStatus.
			
			if (transaction == null)
			{
				using (transaction = this.BeginTransaction (DbTransactionMode.ReadOnly))
				{
					this.UpdateKeyInRow (transaction, table_name, old_key, new_key);
					transaction.Commit ();
					return;
				}
			}
			
			Collections.SqlFields fields = new Collections.SqlFields ();
			Collections.SqlFields conds  = new Collections.SqlFields ();
			
			fields.Add (Tags.ColumnId,     SqlField.CreateConstant (new_key.Id,            DbKey.RawTypeForId));
			fields.Add (Tags.ColumnStatus, SqlField.CreateConstant (new_key.IntStatus,     DbKey.RawTypeForStatus));
			fields.Add (Tags.ColumnRefLog, SqlField.CreateConstant (this.logger.CurrentId, DbKey.RawTypeForId));
			
			DbInfrastructure.AddKeyExtraction (conds, table_name, old_key);
			
			transaction.SqlBuilder.UpdateData (table_name, fields, conds);
			
			int num_rows_affected;
			
			InvariantConverter.Convert (this.ExecuteNonQuery (transaction), out num_rows_affected);
			
			if (num_rows_affected != 1)
			{
				throw new Exceptions.GenericException (this.access, string.Format ("Update of row {0} in table {1} produced {2} updates.", old_key, table_name, num_rows_affected));
			}
		}
		
		public void UpdateTableNextId(DbTransaction transaction, DbKey key, DbId next_id)
		{
			if (transaction == null)
			{
				using (transaction = this.BeginTransaction (DbTransactionMode.ReadOnly))
				{
					this.UpdateTableNextId (transaction, key, next_id);
					transaction.Commit ();
					return;
				}
			}
			
			Collections.SqlFields fields = new Collections.SqlFields ();
			Collections.SqlFields conds  = new Collections.SqlFields ();
			
			fields.Add (Tags.ColumnNextId, SqlField.CreateConstant (next_id, DbKey.RawTypeForId));
			
			DbInfrastructure.AddKeyExtraction (conds, Tags.TableTableDef, key);
			
			transaction.SqlBuilder.UpdateData (Tags.TableTableDef, fields, conds);
			this.ExecuteSilent (transaction);
		}
		
		
		public List<DbTable> LoadDbTable(DbTransaction transaction, DbKey key, DbRowSearchMode rowSearchMode)
		{
			//	Charge les d�finitions pour la table au moyen d'une requ�te unique qui va
			//	aussi retourner les diverses d�finitions de colonnes.
			
			SqlSelect query = new SqlSelect ();
			
			//	Ce qui est propre � la table :
			
			query.Fields.Add ("T_ID",   SqlField.CreateName ("T_TABLE", Tags.ColumnId));
			query.Fields.Add ("T_NAME", SqlField.CreateName ("T_TABLE", Tags.ColumnName));
			query.Fields.Add ("T_INFO", SqlField.CreateName ("T_TABLE", Tags.ColumnInfoXml));
			
			//	Ce qui est propre aux colonnes :
			
			query.Fields.Add ("C_ID",     SqlField.CreateName ("T_COLUMN", Tags.ColumnId));
			query.Fields.Add ("C_NAME",   SqlField.CreateName ("T_COLUMN", Tags.ColumnName));
			query.Fields.Add ("C_INFO",   SqlField.CreateName ("T_COLUMN", Tags.ColumnInfoXml));
			query.Fields.Add ("C_TYPE",   SqlField.CreateName ("T_COLUMN", Tags.ColumnRefType));
			query.Fields.Add ("C_PARENT", SqlField.CreateName ("T_COLUMN", Tags.ColumnRefParent));
			
			//	Les deux tables utilis�es pour l'extraction :
			
			query.Tables.Add ("T_TABLE",  SqlField.CreateName (Tags.TableTableDef));
			query.Tables.Add ("T_COLUMN", SqlField.CreateName (Tags.TableColumnDef));
			
			if (key.IsEmpty)
			{
				//	On extrait toutes les d�finitions de tables qui correspondent � une version
				//	'active' (ignore les versions archiv�es et d�truites). Extrait aussi les colonnes
				//	correspondantes.
				
				DbInfrastructure.AddKeyExtraction (query.Conditions, "T_TABLE", rowSearchMode);
				DbInfrastructure.AddKeyExtraction (query.Conditions, "T_COLUMN", Tags.ColumnRefTable, "T_TABLE");
			}
			else
			{
				//	On extrait toutes les lignes de T_TABLE qui ont un CR_ID = key, ainsi que
				//	les lignes correspondantes de T_COLUMN qui ont un CREF_TABLE = key.
				
				DbInfrastructure.AddKeyExtraction (query.Conditions, "T_TABLE", key);
				DbInfrastructure.AddKeyExtraction (query.Conditions, "T_COLUMN", Tags.ColumnRefTable, key);
			}
			
			System.Data.DataTable dataTable = this.ExecuteSqlSelect (transaction, query, 1);
			
			long          rowId   = -1;
			List<DbTable> tables  = new List<DbTable> ();
			DbTable		  dbTable = null;
			bool          recycle = false;
			
			
			//	Analyse toutes les lignes retourn�es. On suppose que les lignes sont group�es
			//	logiquement par tables.
			
			foreach (System.Data.DataRow row in dataTable.Rows)
			{
				long currentRowId = InvariantConverter.ToLong (row["T_ID"]);
				
				if (rowId != currentRowId)
				{
					rowId   = currentRowId;
					dbTable = null;
					
					string tableInfo = InvariantConverter.ToString (row["T_INFO"]);
					string tableName = InvariantConverter.ToString (row["T_NAME"]);
					DbKey  tableKey  = key.IsEmpty ? new DbKey (rowId) : key;
					
					dbTable = this.tableCache[tableKey];
					
					if (dbTable == null)
					{
						dbTable = DbTools.DeserializeFromXml<DbTable> (tableInfo);
						recycle = false;

						dbTable.DefineName (tableName);
						dbTable.DefineKey (tableKey);
						
						//	Afin d'�viter de recharger cette table plus tard, on va en prendre note tout de suite; �a permet
						//	aussi d'�viter des boucles sans fin dans le cas de tables qui ont des r�f�rences circulaires, car
						//	la prochaine recherche avec ResolveDbTable s'appliquant � cette table se terminera avec succ�s.
						
						if ((tableKey.Status == DbRowStatus.Live) ||
							(tableKey.Status == DbRowStatus.Copied))
						{
							this.tableCache[tableKey] = dbTable;
						}
					}
					else
					{
						System.Diagnostics.Debug.WriteLine (string.Format ("Recycling known table {0}", dbTable.Name));
						recycle = true;
					}
					
					tables.Add (dbTable);
				}
				
				if (recycle)
				{
					continue;
				}
				
				//	Chaque ligne contient une d�finition de colonne.

				long   typeDefId  = InvariantConverter.ToLong (row["C_TYPE"]);
				long   columnId   = InvariantConverter.ToLong (row["C_ID"]);
				string columnName = InvariantConverter.ToString (row["C_NAME"]);
				string columnInfo = InvariantConverter.ToString (row["C_INFO"]);
				string targetName = null;

				if (InvariantConverter.IsNotNull (row["C_PARENT"]))
				{
					DbKey   parentKey   = new DbKey (InvariantConverter.ToLong (row["C_PARENT"]));
					DbTable parentTable = this.ResolveDbTable (transaction, parentKey);

					targetName = parentTable.Name;
				}

				DbKey     typeDefKey = new DbKey (typeDefId);
				DbTypeDef typeDef    = this.ResolveDbType (transaction, typeDefKey);
				
				if (typeDef == null)
				{
					throw new Exceptions.GenericException (this.access, string.Format ("Missing type for column '{0}' in table '{1}'", columnName, dbTable.Name));
				}

				System.Diagnostics.Debug.Assert (typeDef.Key == typeDefKey);

				DbColumn dbColumn = DbTools.DeserializeFromXml<DbColumn> (columnInfo);

				dbColumn.DefineName (columnName);
				dbColumn.DefineKey (new DbKey (columnId));
				dbColumn.DefineType (typeDef);
				dbColumn.DefineTargetTableName (targetName);
				
				dbTable.Columns.Add (dbColumn);
				
				if (dbColumn.IsPrimaryKey)
				{
					dbTable.PrimaryKeys.Add (dbColumn);
				}
			}
			
			//	TODO: il faut encore initialiser les champs TargetTableName des diverses colonnes
			//	qui �tablissent une relation avec une autre table. Pour cela, il faudra faire un
			//	SELECT dans Tags.TableRelationDef pour les colonnes dont DbColumnClass est parmi
			//	RefSimpleId/RefLiveId/RefTupleId/RefTupleRevision et d�terminer le nom des tables
			//	cibles, puis appeler DbColumn.DefineTargetTableName...
			
			return tables;
		}
		
		public List<DbTypeDef> LoadDbType(DbTransaction transaction, DbKey key, DbRowSearchMode rowSearchMode)
		{
			SqlSelect query = new SqlSelect ();
			
			query.Fields.Add ("T_ID",   SqlField.CreateName ("T_TYPE", Tags.ColumnId));
			query.Fields.Add ("T_NAME", SqlField.CreateName ("T_TYPE", Tags.ColumnName));
			query.Fields.Add ("T_INFO", SqlField.CreateName ("T_TYPE", Tags.ColumnInfoXml));
			
			query.Tables.Add ("T_TYPE", SqlField.CreateName (Tags.TableTypeDef));
			
			if (key.IsEmpty)
			{
				//	On extrait toutes les d�finitions de types qui correspondent � la version
				//	'active'.
				
				DbInfrastructure.AddKeyExtraction (query.Conditions, "T_TYPE", rowSearchMode);
			}
			else
			{
				//	Cherche la ligne de la table dont 'CR_ID = key'.
				
				DbInfrastructure.AddKeyExtraction (query.Conditions, "T_TYPE", key);
			}
			
			System.Data.DataTable dataTable = this.ExecuteSqlSelect (transaction, query, 1);
			List<DbTypeDef> types = new List<DbTypeDef> ();
			
			foreach (System.Data.DataRow row in dataTable.Rows)
			{
				long   typeId   = InvariantConverter.ToLong (row["T_ID"]);
				string typeName = InvariantConverter.ToString (row["T_NAME"]);
				string typeInfo = InvariantConverter.ToString (row["T_INFO"]);
				
				//	A partir de l'information trouv�e dans la base, g�n�re l'objet DbTypeDef
				//	qui correspond.

				DbTypeDef typeDef = DbTools.DeserializeFromXml<DbTypeDef> (typeInfo);

				typeDef.DefineName (typeName);
				typeDef.DefineKey (new DbKey (typeId));
				
				//	TODO: handle enumeration types
#if false
				if (type is DbTypeEnum)
				{
					DbTypeEnum type_enum = type as DbTypeEnum;
					DbEnumValue[] values = this.LoadEnumValues (transaction, type_enum);
					
					type_enum.DefineValues (values);
				}
#endif
				
				types.Add (typeDef);
			}
			
			return types;
		}
		
		
		public long NextRowIdInTable(DbTransaction transaction, DbKey key)
		{
			return this.NewRowIdInTable (transaction, key, 0);
		}
		
		public long NewRowIdInTable(DbTransaction transaction, DbKey key, int numKeys)
		{
			//	Attribue 'numKeys' clef cons�cutives dans la table sp�cifi�e.
			
			System.Diagnostics.Debug.Assert (numKeys >= 0);
			
			if (transaction == null)
			{
				using (transaction = this.BeginTransaction ())
				{
					long id = this.NewRowIdInTable (transaction, key, numKeys);
					transaction.Commit ();
					return id;
				}
			}
			
			Collections.SqlFields fields = new Collections.SqlFields ();
			Collections.SqlFields conds  = new Collections.SqlFields ();
			
			SqlField fieldNextId = SqlField.CreateName (Tags.ColumnNextId);
			SqlField fieldConstN = SqlField.CreateConstant (numKeys, DbRawType.Int32);
			
			fields.Add (Tags.ColumnNextId, new SqlFunction (SqlFunctionCode.MathAdd, fieldNextId, fieldConstN));
			
			DbInfrastructure.AddKeyExtraction (conds, Tags.TableTableDef, key);
			
			if (numKeys > 0)
			{
				transaction.SqlBuilder.UpdateData (Tags.TableTableDef, fields, conds);
				this.ExecuteSilent (transaction);
			}
			
			SqlSelect query = new SqlSelect ();

			System.Diagnostics.Debug.Assert (conds.Count == 1);

			query.Fields.Add (fieldNextId);
			query.Tables.Add (SqlField.CreateName (Tags.TableTableDef));
			query.Conditions.Add (conds[0]);
			
			transaction.SqlBuilder.SelectData (query);
			
			return InvariantConverter.ToLong (this.ExecuteScalar (transaction)) - numKeys;
		}
		
#if false
		private DbEnumValue[] LoadEnumValues(DbTransaction transaction, DbTypeEnum type_enum)
		{
			System.Diagnostics.Debug.Assert (type_enum != null);
			System.Diagnostics.Debug.Assert (type_enum.Count == 0);
			
			SqlSelect query = new SqlSelect ();
			
			query.Fields.Add ("E_ID",   SqlField.CreateName ("T_ENUM", Tags.ColumnId));
			query.Fields.Add ("E_NAME", SqlField.CreateName ("T_ENUM", Tags.ColumnName));
			query.Fields.Add ("E_INFO", SqlField.CreateName ("T_ENUM", Tags.ColumnInfoXml));
			
			this.AddLocalisedColumns (query, "ENUM_CAPTION", "T_ENUM", Tags.ColumnCaption);
			this.AddLocalisedColumns (query, "ENUM_DESCRIPTION", "T_ENUM", Tags.ColumnDescription);
			
			query.Tables.Add ("T_ENUM", SqlField.CreateName (Tags.TableEnumValDef));
			
			//	Cherche les lignes de la table dont la colonne CREF_TYPE correspond � l'ID du type.
			
			DbInfrastructure.AddKeyExtraction (query.Conditions, "T_ENUM", Tags.ColumnRefType, type_enum.InternalKey);
			
			System.Data.DataTable data_table = this.ExecuteSqlSelect (transaction, query, 1);
			
			DbEnumValue[] values = new DbEnumValue[data_table.Rows.Count];
			
			for (int i = 0; i < data_table.Rows.Count; i++)
			{
				System.Data.DataRow data_row = data_table.Rows[i];
				
				//	Pour chaque valeur retourn�e dans la table, il y a une ligne. Cette ligne
				//	contient toute l'information n�cessaire � la cr�ation d'une instance de la
				//	class DbEnumValue :
				
				string val_name = InvariantConverter.ToString (data_row["E_NAME"]);
				string val_id   = InvariantConverter.ToString (data_row["E_ID"]);
				string val_info = InvariantConverter.ToString (data_row["E_INFO"]);
				
				System.Xml.XmlDocument xml = new System.Xml.XmlDocument ();
				xml.LoadXml (val_info);
				
				values[i] = new DbEnumValue (xml.DocumentElement);
				
				values[i].Attributes.SetAttribute (Tags.Name, val_name);
				values[i].Attributes.SetAttribute (Tags.Id, val_id);
				
				this.DefineLocalisedAttributes (data_row, "ENUM_CAPTION", Tags.ColumnCaption, values[i].Attributes, Tags.Caption);
				this.DefineLocalisedAttributes (data_row, "ENUM_DESCRIPTION", Tags.ColumnDescription, values[i].Attributes, Tags.Description);
			}
			
			return values;
		}
#endif


		private static void AddKeyExtraction(Collections.SqlFields conditions, string table_name, DbKey key)
		{
			SqlField name_col_id = SqlField.CreateName (table_name, Tags.ColumnId);
			SqlField constant_id = SqlField.CreateConstant (key.Id, DbKey.RawTypeForId);
			
			conditions.Add (new SqlFunction (SqlFunctionCode.CompareEqual, name_col_id, constant_id));
		}

		private static void AddKeyExtraction(Collections.SqlFields conditions, string sourceTableName, string sourceColumnName, string parentTableName)
		{
			SqlField parentColumnId = SqlField.CreateName (parentTableName, Tags.ColumnId);
			SqlField sourceColumnId = SqlField.CreateName (sourceTableName, sourceColumnName);
			
			conditions.Add (new SqlFunction (SqlFunctionCode.CompareEqual, sourceColumnId, parentColumnId));
		}

		private static void AddKeyExtraction(Collections.SqlFields conditions, string source_table_name, string source_col_name, DbKey key)
		{
			SqlField source_col_id = SqlField.CreateName (source_table_name, source_col_name);
			SqlField constant_id   = SqlField.CreateConstant (key.Id, DbKey.RawTypeForId);
			
			conditions.Add (new SqlFunction (SqlFunctionCode.CompareEqual, source_col_id, constant_id));
		}

		private static void AddKeyExtraction(Collections.SqlFields conditions, string table_name, DbRowSearchMode search_mode)
		{
			//	G�n�re la condition d'extraction pour une recherche selon le statut des lignes
			//	(voir aussi la d�finition de DbRowStatus).
			
			SqlFunctionCode function;
			DbRowStatus     status;
			
			switch (search_mode)
			{
				case DbRowSearchMode.Copied:		status = DbRowStatus.Copied;		function = SqlFunctionCode.CompareEqual;	break;
				case DbRowSearchMode.Live:			status = DbRowStatus.Live;			function = SqlFunctionCode.CompareEqual;	break;
				case DbRowSearchMode.LiveActive:	status = DbRowStatus.ArchiveCopy;	function = SqlFunctionCode.CompareLessThan;	break;
				case DbRowSearchMode.ArchiveCopy:	status = DbRowStatus.ArchiveCopy;	function = SqlFunctionCode.CompareEqual;	break;
				case DbRowSearchMode.LiveAll:		status = DbRowStatus.Deleted;		function = SqlFunctionCode.CompareLessThan;	break;
				case DbRowSearchMode.Deleted:		status = DbRowStatus.Deleted;		function = SqlFunctionCode.CompareEqual;	break;
				
				case DbRowSearchMode.All:
					return;
				
				default:
					throw new System.ArgumentException (string.Format ("Search mode {0} not supported.", search_mode), "search_mode");
			}
			
			SqlField name_status  = SqlField.CreateName (table_name, Tags.ColumnStatus);
			SqlField const_status = SqlField.CreateConstant (DbKey.ConvertToIntStatus (status), DbKey.RawTypeForStatus);
			
			conditions.Add (new SqlFunction (function, name_status, const_status));
		}


		private void StartUsingDatabase()
		{
			using (DbTransaction transaction = this.BeginTransaction (DbTransactionMode.ReadOnly))
			{
				this.globals = new Settings.Globals (this, transaction);
				this.locals  = new Settings.Locals (this, transaction);
				
				this.clientId = this.locals.ClientId;
				this.SetupLogger (transaction);

				this.clientManager = new DbClientManager ();

				this.clientManager.Attach (this, this.ResolveDbTable (transaction, Tags.TableClientDef));
				this.clientManager.LoadFromBase (transaction);
				
				transaction.Commit ();
			}
		}
		
		
		private void SetupLogger(DbTransaction transaction)
		{
			this.logger = new DbLogger ();
			this.logger.DefineClientId (this.clientId);
			this.logger.Attach (this, this.internalTables[Tags.TableLog]);
			this.logger.ResetCurrentLogId (transaction);
		}
		
		private void SetupTables(DbTransaction transaction)
		{
			//	Remplit les tables de gestion (CR_*) avec les valeurs par d�faut et
			//	les d�finitions initiales de la structure interne de la base vide.
			
			long type_key_id     = DbId.CreateId (1, this.clientId);
			long table_key_id    = DbId.CreateId (1, this.clientId);
			long column_key_id   = DbId.CreateId (1, this.clientId);
			long enum_val_key_id = DbId.CreateId (1, this.clientId);
			long client_key_id   = DbId.CreateId (1, this.clientId);
			
			//	Initialisation partielle de DbLogger (juste ce qu'il faut pour pouvoir
			//	acc�der � this.logger.CurrentId) :
			
			this.logger = new DbLogger ();
			this.logger.DefineClientId (this.clientId);
			this.logger.DefineInitialLogId (1);
			
			System.Diagnostics.Debug.Assert (this.logger.CurrentId.LocalId == 1);
			
			//	Il faut commencer par finir d'initialiser les descriptions des types, parce
			//	que les description des colonnes doivent y faire r�f�rence.
			
			foreach (DbTypeDef typeDef in this.internalTypes)
			{
				//	Attribue � chaque type interne une clef unique et �tablit les informations de base
				//	dans la table de d�finition des types.
				
				typeDef.DefineKey (new DbKey (type_key_id++));
				this.InsertTypeDefRow (transaction, typeDef);
			}
			
			foreach (DbTable table in this.internalTables)
			{
				//	Attribue � chaque table interne une clef unique et �tablit les informations de base
				//	dans la table de d�finition des tables.
				
				table.DefineKey (new DbKey (table_key_id++));
				table.UpdatePrimaryKeyInfo ();
				
				this.InsertTableDefRow (transaction, table);
				
				foreach (DbColumn column in table.Columns)
				{
					//	Pour chaque colonne de la table, �tablit les informations de base dans la table de
					//	d�finition des colonnes.
					
					column.DefineKey (new DbKey (column_key_id++));
					this.InsertColumnDefRow (transaction, table, column);
				}
			}
			
			//	Compl�te encore les informations au sujet des relations :
			//
			//	- La description d'une colonne fait r�f�rence � la table et � un type.
			//	- La description d'une valeur d'enum fait r�f�rence � un type.
			//	- La description d'une r�f�rence fait elle-m�me r�f�rence � la table
			//	  source et destination, ainsi qu'� la colonne.
			
			this.UpdateColumnRelation (transaction, Tags.TableColumnDef,   Tags.ColumnRefTable,  Tags.TableTableDef);
			this.UpdateColumnRelation (transaction, Tags.TableColumnDef,   Tags.ColumnRefType,   Tags.TableTypeDef);
			this.UpdateColumnRelation (transaction, Tags.TableColumnDef,   Tags.ColumnRefParent, Tags.TableTypeDef);
			this.UpdateColumnRelation (transaction, Tags.TableEnumValDef,  Tags.ColumnRefType,   Tags.TableTypeDef);
			
			this.UpdateTableNextId (transaction, this.internalTables[Tags.TableTableDef].Key, table_key_id);
			this.UpdateTableNextId (transaction, this.internalTables[Tags.TableColumnDef].Key, column_key_id);
			this.UpdateTableNextId (transaction, this.internalTables[Tags.TableTypeDef].Key, type_key_id);
			this.UpdateTableNextId (transaction, this.internalTables[Tags.TableEnumValDef].Key, enum_val_key_id);
			this.UpdateTableNextId (transaction, this.internalTables[Tags.TableClientDef].Key, client_key_id);
			
			//	On ne peut attacher le DbLogger qu'ici, car avant, les tables et les clefs
			//	indispensables ne sont pas encore utilisables :
			
			this.logger.Attach (this, this.internalTables[Tags.TableLog]);
			this.logger.CreateInitialEntry (transaction);
			
			System.Diagnostics.Debug.Assert (this.logger.CurrentId.LocalId == 1);
			System.Diagnostics.Debug.Assert (this.NextRowIdInTable (transaction, this.internalTables[Tags.TableLog].Key) == DbId.CreateId (2, this.clientId));
		}


		private static void SetCategory(DbColumn[] columns, DbElementCat cat)
		{
			for (int i = 0; i < columns.Length; i++)
			{
				columns[i].DefineCategory (cat);
			}
		}


		private void UpdateColumnRelation(DbTransaction transaction, DbKey source_table_key, DbKey source_column_key, DbKey parent_table_key)
		{
			System.Diagnostics.Debug.Assert (transaction != null);
			
			System.Diagnostics.Debug.Assert (source_table_key  != null);
			System.Diagnostics.Debug.Assert (source_column_key != null);
			System.Diagnostics.Debug.Assert (parent_table_key  != null);
			
			Collections.SqlFields fields = new Collections.SqlFields ();
			Collections.SqlFields conds  = new Collections.SqlFields ();
			
			fields.Add (Tags.ColumnRefParent, SqlField.CreateConstant (parent_table_key.Id, DbKey.RawTypeForId));

			DbInfrastructure.AddKeyExtraction (conds, Tags.TableColumnDef, source_column_key);
			
			transaction.SqlBuilder.UpdateData (Tags.TableColumnDef, fields, conds);
			this.ExecuteSilent (transaction);
		}

		private void UpdateColumnRelation(DbTransaction transaction, string src_table_name, string src_columnName, string targetTableName)
		{
			DbTable  source = this.internalTables[src_table_name];
			DbTable parent = this.internalTables[targetTableName];
			DbColumn column = source.Columns[src_columnName];
			
			DbKey src_table_key    = source.Key;
			DbKey src_column_key   = column.Key;
			DbKey parent_table_key = parent.Key;
			
			this.UpdateColumnRelation (transaction, src_table_key, src_column_key, parent_table_key);
		}


		private void InsertTypeDefRow(DbTransaction transaction, DbTypeDef typeDef)
		{
			System.Diagnostics.Debug.Assert (this.logger.CurrentId.LocalId > 0);
			
			DbTable type_def_table = this.internalTables[Tags.TableTypeDef];
			
			//	Ins�re une ligne dans la table de d�finition des types.
			
			Collections.SqlFields fields = new Collections.SqlFields ();

			fields.Add (this.CreateSqlField (type_def_table.Columns[Tags.ColumnId], typeDef.Key.Id));
			fields.Add (this.CreateSqlField (type_def_table.Columns[Tags.ColumnStatus], typeDef.Key.IntStatus));
			fields.Add (this.CreateSqlField (type_def_table.Columns[Tags.ColumnRefLog], this.logger.CurrentId));
			fields.Add (this.CreateSqlField (type_def_table.Columns[Tags.ColumnName], typeDef.Name));
			fields.Add (this.CreateSqlField (type_def_table.Columns[Tags.ColumnInfoXml], DbTools.GetCompactXml (typeDef)));
			
			//	TODO: Initializer les colonnes descriptives
			
			transaction.SqlBuilder.InsertData (type_def_table.CreateSqlName (), fields);
			this.ExecuteSilent (transaction);
		}

#if false
		private void InsertEnumValueDefRow(DbTransaction transaction, DbType type, DbEnumValue value)
		{
			System.Diagnostics.Debug.Assert (this.logger.CurrentId.LocalId > 0);
			System.Diagnostics.Debug.Assert (transaction != null);
			
			DbTable enum_def = this.internal_tables[Tags.TableEnumValDef];
			
			//	Ins�re une ligne dans la table de d�finition des �num�rations.
			
			Collections.SqlFields fields = new Collections.SqlFields ();
			
			fields.Add (enum_def.Columns[Tags.ColumnId]	     .CreateSqlField (this.type_converter, value.InternalKey.Id));
			fields.Add (enum_def.Columns[Tags.ColumnStatus]  .CreateSqlField (this.type_converter, value.InternalKey.IntStatus));
			fields.Add (enum_def.Columns[Tags.ColumnRefLog]  .CreateSqlField (this.type_converter, this.logger.CurrentId));
			fields.Add (enum_def.Columns[Tags.ColumnName]    .CreateSqlField (this.type_converter, value.Name));
			fields.Add (enum_def.Columns[Tags.ColumnInfoXml] .CreateSqlField (this.type_converter, DbEnumValue.SerializeToXml (value, false)));
			fields.Add (enum_def.Columns[Tags.ColumnRefType] .CreateSqlField (this.type_converter, type.InternalKey.Id));
			
			//	TODO: Initialiser les colonnes descriptives
			
			transaction.SqlBuilder.InsertData (enum_def.CreateSqlName (), fields);
			this.ExecuteSilent (transaction);
		}
#endif

		private void InsertTableDefRow(DbTransaction transaction, DbTable table)
		{
			System.Diagnostics.Debug.Assert (this.logger.CurrentId.LocalId > 0);
			System.Diagnostics.Debug.Assert (transaction != null);
			
			DbTable table_def = this.internalTables[Tags.TableTableDef];
			
			//	Ins�re une ligne dans la table de d�finition des tables.
			
			Collections.SqlFields fields = new Collections.SqlFields ();

			fields.Add (this.CreateSqlField (table_def.Columns[Tags.ColumnId], table.Key.Id));
			fields.Add (this.CreateSqlField (table_def.Columns[Tags.ColumnStatus], table.Key.IntStatus));
			fields.Add (this.CreateSqlField (table_def.Columns[Tags.ColumnRefLog], this.logger.CurrentId));
			fields.Add (this.CreateSqlField (table_def.Columns[Tags.ColumnName], table.Name));
			fields.Add (this.CreateSqlField (table_def.Columns[Tags.ColumnInfoXml], DbTools.GetCompactXml (table)));
			fields.Add (this.CreateSqlField (table_def.Columns[Tags.ColumnNextId], DbId.CreateId (1, this.clientId)));
			
			//	TODO: Initialiser les colonnes descriptives
			
			transaction.SqlBuilder.InsertData (table_def.CreateSqlName (), fields);
			this.ExecuteSilent (transaction);
		}

		private void InsertColumnDefRow(DbTransaction transaction, DbTable table, DbColumn column)
		{
			System.Diagnostics.Debug.Assert (this.logger.CurrentId.LocalId > 0);
			System.Diagnostics.Debug.Assert (transaction != null);
			
			DbTable column_def = this.internalTables[Tags.TableColumnDef];
			
			//	Ins�re une ligne dans la table de d�finition des colonnes.
			
			Collections.SqlFields fields = new Collections.SqlFields ();

			fields.Add (this.CreateSqlField (column_def.Columns[Tags.ColumnId], column.Key.Id));
			fields.Add (this.CreateSqlField (column_def.Columns[Tags.ColumnStatus], column.Key.IntStatus));
			fields.Add (this.CreateSqlField (column_def.Columns[Tags.ColumnRefLog], this.logger.CurrentId));
			fields.Add (this.CreateSqlField (column_def.Columns[Tags.ColumnName], column.Name));
			fields.Add (this.CreateSqlField (column_def.Columns[Tags.ColumnInfoXml], DbTools.GetCompactXml (column)));
			fields.Add (this.CreateSqlField (column_def.Columns[Tags.ColumnRefTable], table.Key.Id));
			fields.Add (this.CreateSqlField (column_def.Columns[Tags.ColumnRefType], column.Type.Key.Id));
			
			//	TODO: Initialiser les colonnes descriptives
			
			transaction.SqlBuilder.InsertData (column_def.CreateSqlName (), fields);
			this.ExecuteSilent (transaction);
		}


		private void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this.globalLock != null)
				{
					this.globalLock.ReleaseLock ();
					this.globalLock = null;
				}
				
				if (this.logger != null)
				{
					this.logger.Detach ();
					this.logger = null;
				}
				
				if (this.abstraction != null)
				{
					this.abstraction.Dispose ();
					
					System.Diagnostics.Debug.Assert (this.abstraction.IsConnectionOpen == false);
					
					this.abstraction = null;
					this.sqlEngine     = null;
					this.converter = null;
				}
				
				System.Diagnostics.Debug.Assert (this.sqlEngine == null);
				System.Diagnostics.Debug.Assert (this.converter == null);
			}
		}
		
		
		#region Initialisation
		private void InitializeDatabaseAbstraction()
		{
			this.types = new TypeHelper (this);
			
			this.abstraction = this.CreateDatabaseAbstraction ();
			
			this.sqlEngine  = this.abstraction.SqlEngine;
			
			System.Diagnostics.Debug.Assert (this.sqlEngine != null);
			
			this.converter = this.abstraction.Factory.TypeConverter;
			
			System.Diagnostics.Debug.Assert (this.converter != null);
			
			this.abstraction.SqlBuilder.AutoClear = true;
		}
		#endregion
		
		#region IDisposable Members
		public void Dispose()
		{
			this.Dispose (true);
			System.GC.SuppressFinalize (this);
		}
		#endregion
		
		#region BootHelper Class
		public class BootHelper
		{
			public BootHelper(DbInfrastructure infrastructure, DbTransaction transaction)
			{
				this.infrastructure = infrastructure;
				this.transaction    = transaction;

				System.Diagnostics.Debug.Assert (this.infrastructure.types.KeyId.IsNullable == false);
				System.Diagnostics.Debug.Assert (this.infrastructure.types.KeyStatus.IsNullable == false);
				System.Diagnostics.Debug.Assert (this.infrastructure.types.NullableKeyId.IsNullable == true);
				System.Diagnostics.Debug.Assert (this.infrastructure.types.Name.IsNullable == false);
				System.Diagnostics.Debug.Assert (this.infrastructure.types.InfoXml.IsNullable == false);
			}
			
			
			public void CreateTableTableDef()
			{
				TypeHelper types   = this.infrastructure.types;
				DbTable    table   = new DbTable (Tags.TableTableDef);
				DbColumn[] columns = new DbColumn[]
					{
						new DbColumn (Tags.ColumnId,		  types.KeyId,		 DbColumnClass.KeyId),
						new DbColumn (Tags.ColumnStatus,	  types.KeyStatus,	 DbColumnClass.KeyStatus),
						new DbColumn (Tags.ColumnRefLog,	  types.KeyId,		 DbColumnClass.RefInternal),
						new DbColumn (Tags.ColumnName,		  types.Name,		 DbColumnClass.Data),
						new DbColumn (Tags.ColumnInfoXml,	  types.InfoXml,	 DbColumnClass.Data),
						new DbColumn (Tags.ColumnNextId,	  types.KeyId,		 DbColumnClass.RefInternal)
					};

				this.CreateTable (table, columns, DbReplicationMode.Manual);
			}
			
			public void CreateTableColumnDef()
			{
				TypeHelper types   = this.infrastructure.types;
				DbTable    table   = new DbTable (Tags.TableColumnDef);
				DbColumn[] columns = new DbColumn[]
					{
						new DbColumn (Tags.ColumnId,		  types.KeyId,		   DbColumnClass.KeyId),
						new DbColumn (Tags.ColumnStatus,	  types.KeyStatus,	   DbColumnClass.KeyStatus),
						new DbColumn (Tags.ColumnRefLog,	  types.KeyId,		   DbColumnClass.RefInternal),
						new DbColumn (Tags.ColumnName,		  types.Name,		   DbColumnClass.Data),
						new DbColumn (Tags.ColumnInfoXml,	  types.InfoXml,	   DbColumnClass.Data),
						new DbColumn (Tags.ColumnRefTable,	  types.KeyId,         DbColumnClass.RefId),
						new DbColumn (Tags.ColumnRefType,	  types.KeyId,         DbColumnClass.RefId),
						new DbColumn (Tags.ColumnRefParent,	  types.NullableKeyId, DbColumnClass.RefId)
					};
				
				this.CreateTable (table, columns, DbReplicationMode.Manual);
			}
			
			public void CreateTableTypeDef()
			{
				TypeHelper types   = this.infrastructure.types;
				DbTable    table   = new DbTable (Tags.TableTypeDef);
				DbColumn[] columns = new DbColumn[]
					{
						new DbColumn (Tags.ColumnId,		  types.KeyId,		 DbColumnClass.KeyId),
						new DbColumn (Tags.ColumnStatus,	  types.KeyStatus,	 DbColumnClass.KeyStatus),
						new DbColumn (Tags.ColumnRefLog,	  types.KeyId,		 DbColumnClass.RefInternal),
						new DbColumn (Tags.ColumnName,		  types.Name,		 DbColumnClass.Data),
						new DbColumn (Tags.ColumnInfoXml,	  types.InfoXml,	 DbColumnClass.Data)
					};
				
				this.CreateTable (table, columns, DbReplicationMode.Manual);
			}
			
			public void CreateTableEnumValDef()
			{
				TypeHelper types   = this.infrastructure.types;
				DbTable    table   = new DbTable (Tags.TableEnumValDef);
				DbColumn[] columns = new DbColumn[]
					{
						new DbColumn (Tags.ColumnId,		  types.KeyId,		DbColumnClass.KeyId),
						new DbColumn (Tags.ColumnStatus,	  types.KeyStatus,	DbColumnClass.KeyStatus),
						new DbColumn (Tags.ColumnRefLog,	  types.KeyId,		DbColumnClass.RefInternal),
						new DbColumn (Tags.ColumnName,		  types.Name,		DbColumnClass.Data),
						new DbColumn (Tags.ColumnInfoXml,	  types.InfoXml,	DbColumnClass.Data),
						new DbColumn (Tags.ColumnRefType,	  types.KeyId,      DbColumnClass.RefId)
					};
				
				this.CreateTable (table, columns, DbReplicationMode.Manual);
			}
			
			public void CreateTableLog()
			{
				TypeHelper types   = this.infrastructure.types;
				DbTable    table   = new DbTable (Tags.TableLog);
				DbColumn[] columns = new DbColumn[]
					{
						new DbColumn (Tags.ColumnId,		  types.KeyId,		DbColumnClass.KeyId),
						new DbColumn (Tags.ColumnDateTime,	  types.DateTime,	DbColumnClass.Data)
					};
				
				//	TODO: ajouter ici une colonne d�finissant la nature du changement (et l'utilisateur
				//	qui en est la cause).
				
				this.CreateTable (table, columns, DbReplicationMode.Manual);
			}
			
			public void CreateTableRequestQueue()
			{
				TypeHelper types   = this.infrastructure.types;
				DbTable    table   = new DbTable (Tags.TableRequestQueue);
				DbColumn[] columns = new DbColumn[]
					{
						new DbColumn (Tags.ColumnId,		  types.KeyId,		DbColumnClass.KeyId),
						new DbColumn (Tags.ColumnStatus,	  types.KeyStatus,	DbColumnClass.KeyStatus),
						new DbColumn (Tags.ColumnRefLog,	  types.KeyId,		DbColumnClass.RefInternal),
						new DbColumn (Tags.ColumnReqExState,  types.ReqExecState, DbColumnClass.Data),
						new DbColumn (Tags.ColumnReqData,	  types.ReqData,	DbColumnClass.Data),
						new DbColumn (Tags.ColumnDateTime,    types.DateTime,   DbColumnClass.Data)
					};
				
				this.CreateTable (table, columns, DbReplicationMode.None);
			}
			
			public void CreateTableClientDef()
			{
				TypeHelper types   = this.infrastructure.types;
				DbTable    table   = new DbTable (Tags.TableClientDef);
				DbColumn[] columns = new DbColumn[]
					{
						new DbColumn (Tags.ColumnId,			types.KeyId,	 DbColumnClass.KeyId),
						new DbColumn (Tags.ColumnStatus,		types.KeyStatus, DbColumnClass.KeyStatus),
						new DbColumn (Tags.ColumnRefLog,		types.KeyId,	 DbColumnClass.RefInternal),
						new DbColumn (Tags.ColumnClientId,		types.KeyId,	 DbColumnClass.Data),
						new DbColumn (Tags.ColumnClientName,	types.Name,      DbColumnClass.Data),
						new DbColumn (Tags.ColumnClientSync,	types.KeyId,	 DbColumnClass.Data),
						new DbColumn (Tags.ColumnClientCreDate,	types.DateTime,  DbColumnClass.Data),
						new DbColumn (Tags.ColumnClientConDate,	types.DateTime,  DbColumnClass.Data)
					};
				
				this.CreateTable (table, columns, DbReplicationMode.None);
			}
			
			
			private void CreateTable(DbTable table, DbColumn[] columns, DbReplicationMode replication_mode)
			{
				DbInfrastructure.SetCategory (columns, DbElementCat.Internal);
				
				table.Columns.AddRange (columns);
				
				table.DefineCategory (DbElementCat.Internal);
				table.DefinePrimaryKey (columns[0]);
				table.DefineReplicationMode (replication_mode);
				
				this.infrastructure.internalTables.Add (table);
				
				SqlTable sql_table = table.CreateSqlTable (this.infrastructure.converter);
				this.infrastructure.DefaultSqlBuilder.InsertTable (sql_table);
				this.infrastructure.ExecuteSilent (this.transaction);
			}
		
			
			private DbInfrastructure			infrastructure;
			private DbTransaction				transaction;
		}
		#endregion
		
		#region TypeHelper Class
		private class TypeHelper
		{
			public TypeHelper(DbInfrastructure infrastructure)
			{
				this.infrastructure = infrastructure;
			}
			
			
			public void RegisterTypes()
			{
				this.InitializeNumTypes ();
				this.InitializeStrTypes ();
				this.InitializeOtherTypes ();
				
				this.AssertAllTypesReady ();
			}
			
			public void ResolveTypes(DbTransaction transaction)
			{
				this.num_type_key_id          = this.infrastructure.ResolveDbType (transaction, Tags.TypeKeyId);
				this.num_type_nullable_key_id = this.infrastructure.ResolveDbType (transaction, Tags.TypeNullableKeyId);
				this.num_type_key_status      = this.infrastructure.ResolveDbType (transaction, Tags.TypeKeyStatus);
				this.num_type_req_ex_state    = this.infrastructure.ResolveDbType (transaction, Tags.TypeReqExState);
				
				this.str_type_name        = this.infrastructure.ResolveDbType (transaction, Tags.TypeName);
				this.str_type_info_xml    = this.infrastructure.ResolveDbType (transaction, Tags.TypeInfoXml);
				this.str_type_dict_key    = this.infrastructure.ResolveDbType (transaction, Tags.TypeDictKey);
				this.str_type_dict_value  = this.infrastructure.ResolveDbType (transaction, Tags.TypeDictValue);
				
				this.d_t_type_datetime    = this.infrastructure.ResolveDbType (transaction, Tags.TypeDateTime);
				this.bin_type_req_data    = this.infrastructure.ResolveDbType (transaction, Tags.TypeReqData);
				
				this.infrastructure.internalTypes.Add (this.num_type_key_id);
				this.infrastructure.internalTypes.Add (this.num_type_nullable_key_id);
				this.infrastructure.internalTypes.Add (this.num_type_key_status);
				this.infrastructure.internalTypes.Add (this.num_type_req_ex_state);
				
				this.infrastructure.internalTypes.Add (this.str_type_name);
				this.infrastructure.internalTypes.Add (this.str_type_info_xml);
				this.infrastructure.internalTypes.Add (this.str_type_dict_key);
				this.infrastructure.internalTypes.Add (this.str_type_dict_value);
				
				this.infrastructure.internalTypes.Add (this.d_t_type_datetime);
				this.infrastructure.internalTypes.Add (this.bin_type_req_data);
				
				this.AssertAllTypesReady ();
			}


			void InitializeNumTypes()
			{
				this.num_type_key_id          = new DbTypeDef (Res.Types.Num.KeyId);
				this.num_type_nullable_key_id = new DbTypeDef (Res.Types.Num.NullableKeyId);
				this.num_type_key_status      = new DbTypeDef (Res.Types.Num.KeyStatus);
				this.num_type_req_ex_state    = new DbTypeDef (Res.Types.Num.ReqExecState);
			
				this.infrastructure.internalTypes.Add (this.num_type_key_id);
				this.infrastructure.internalTypes.Add (this.num_type_nullable_key_id);
				this.infrastructure.internalTypes.Add (this.num_type_key_status);
				this.infrastructure.internalTypes.Add (this.num_type_req_ex_state);
			}

			void InitializeOtherTypes()
			{
				this.d_t_type_datetime   = new DbTypeDef (Res.Types.Other.DateTime);
				this.bin_type_req_data   = new DbTypeDef (Res.Types.Other.ReqData);
		
				this.infrastructure.internalTypes.Add (this.d_t_type_datetime);
				this.infrastructure.internalTypes.Add (this.bin_type_req_data);
			}

			void InitializeStrTypes()
			{
				this.str_type_name        = new DbTypeDef (Res.Types.Str.Name);
				this.str_type_info_xml    = new DbTypeDef (Res.Types.Str.InfoXml);
				this.str_type_dict_key    = new DbTypeDef (Res.Types.Str.Dict.Key);
				this.str_type_dict_value  = new DbTypeDef (Res.Types.Str.Dict.Value);
			
				this.infrastructure.internalTypes.Add (this.str_type_name);
				this.infrastructure.internalTypes.Add (this.str_type_info_xml);
				this.infrastructure.internalTypes.Add (this.str_type_dict_key);
				this.infrastructure.internalTypes.Add (this.str_type_dict_value);
			}
			
			
			void AssertAllTypesReady ()
			{
				System.Diagnostics.Debug.Assert (this.num_type_key_id != null);
				System.Diagnostics.Debug.Assert (this.num_type_nullable_key_id != null);
				System.Diagnostics.Debug.Assert (this.num_type_key_status != null);
				System.Diagnostics.Debug.Assert (this.num_type_req_ex_state != null);
				
				System.Diagnostics.Debug.Assert (this.str_type_name != null);
				System.Diagnostics.Debug.Assert (this.str_type_info_xml != null);
				System.Diagnostics.Debug.Assert (this.str_type_dict_key != null);
				System.Diagnostics.Debug.Assert (this.str_type_dict_value != null);
				
				System.Diagnostics.Debug.Assert (this.d_t_type_datetime != null);
				System.Diagnostics.Debug.Assert (this.bin_type_req_data != null);
			}
			
			
			public DbTypeDef					KeyId
			{
				get
				{
					return this.num_type_key_id;
				}
			}

			public DbTypeDef					NullableKeyId
			{
				get
				{
					return this.num_type_nullable_key_id;
				}
			}

			public DbTypeDef					KeyStatus
			{
				get
				{
					return this.num_type_key_status;
				}
			}

			public DbTypeDef ReqExecState
			{
				get
				{
					return this.num_type_req_ex_state;
				}
			}

			public DbTypeDef					DateTime
			{
				get
				{
					return this.d_t_type_datetime;
				}
			}

			public DbTypeDef ReqData
			{
				get
				{
					return this.bin_type_req_data;
				}
			}

			public DbTypeDef Name
			{
				get
				{
					return this.str_type_name;
				}
			}

			public DbTypeDef InfoXml
			{
				get
				{
					return this.str_type_info_xml;
				}
			}

			public DbTypeDef DictKey
			{
				get
				{
					return this.str_type_dict_key;
				}
			}

			public DbTypeDef DictValue
			{
				get
				{
					return this.str_type_dict_value;
				}
			}


			private DbInfrastructure infrastructure;

			private DbTypeDef num_type_key_id;
			private DbTypeDef num_type_nullable_key_id;
			private DbTypeDef num_type_key_status;
			private DbTypeDef num_type_req_ex_state;

			private DbTypeDef d_t_type_datetime;
			private DbTypeDef bin_type_req_data;

			private DbTypeDef str_type_name;
			private DbTypeDef str_type_info_xml;
			private DbTypeDef str_type_dict_key;
			private DbTypeDef str_type_dict_value;
		}
		#endregion


		private DbAccess access;
		private IDbAbstraction abstraction;

		private ISqlEngine sqlEngine;
		private ITypeConverter converter;
		
		private TypeHelper						types;
		private DbLogger						logger;
		private DbClientManager					clientManager;
		
		private Settings.Globals				globals;
		private Settings.Locals					locals;

		private Collections.DbTables internalTables = new Collections.DbTables ();
		private Collections.DbTypeDefs internalTypes = new Collections.DbTypeDefs ();

		private int clientId;
		
		string									localizations;
		
		Cache.DbTypeDefs						typeCache = new Cache.DbTypeDefs ();
		Cache.DbTables							tableCache = new Cache.DbTables ();

		private List<DbTransaction> liveTransactions;
		private List<IDbAbstraction> releaseRequested;
		private int lockTimeout = 15000;
		System.Threading.ReaderWriterLock		globalLock = new System.Threading.ReaderWriterLock ();
	}
}

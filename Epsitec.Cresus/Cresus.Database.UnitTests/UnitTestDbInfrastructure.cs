using Epsitec.Common.Support;

using Epsitec.Common.UnitTesting;

using Epsitec.Cresus.Database.Exceptions;
using Epsitec.Cresus.Database.UnitTests.Helpers;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.Collections.Generic;

using System.Linq;


namespace Epsitec.Cresus.Database.UnitTests
{


	[TestClass]
	public sealed class UnitTestDbInfrastructure
	{


		// TODO Do not use DbInfrastructureHelper in this test? Because DbInfrastructure is precisely
		// what we want to test here.
		// Marc


		[ClassInitialize]
		public static void ClassInitialize(TestContext testContext)
		{
			TestHelper.Initialize ();
		}


		[TestInitialize]
		public void TestInitialize()
		{
			IDbAbstractionHelper.ResetTestDatabase ();
		}


		[TestMethod]
		public void CreateDatabaseTest()
		{
			DbInfrastructureHelper.DeleteTestDatabase ();
			
			using (DbInfrastructure infrastructure = new DbInfrastructure ())
			{
				DbAccess dbAccess = TestHelper.GetDbAccessForTestDatabase ();

				infrastructure.CreateDatabase (dbAccess);

				DbTable table;

				table = infrastructure.ResolveDbTable ("CR_TABLE_DEF");

				Assert.IsNotNull (table);
				Assert.AreEqual (1L, table.Key.Id.Value);
				Assert.AreEqual (5, table.Columns.Count);

				table = infrastructure.ResolveDbTable ("CR_COLUMN_DEF");

				Assert.IsNotNull (table);
				Assert.AreEqual (2L, table.Key.Id.Value);
				Assert.AreEqual (8, table.Columns.Count);

				table = infrastructure.ResolveDbTable ("CR_TYPE_DEF");

				Assert.IsNotNull (table);
				Assert.AreEqual (3L, table.Key.Id.Value);
				Assert.AreEqual (5, table.Columns.Count);

				using (DbTransaction transaction = infrastructure.BeginTransaction (DbTransactionMode.ReadOnly))
				{
					Assert.AreEqual (0, infrastructure.CountMatchingRows (transaction, "CR_COLUMN_DEF", "CR_NAME", DbSqlStandard.MakeSimpleSqlName ("MyColumn")));
					Assert.AreEqual (3, infrastructure.CountMatchingRows (transaction, "CR_COLUMN_DEF", "CR_NAME", "CR_INFO"));
				}
			}
		}

		
		[TestMethod]
		public void AddTypeTest()
		{
			DbInfrastructureHelper.ResetTestDatabase ();

			using (DbInfrastructure infrastructure = DbInfrastructureHelper.ConnectToTestDatabase ())
			{
				DbTypeDef dbTypeSample1 = new DbTypeDef ("Nom", DbSimpleType.String, null, 40, false, DbNullability.Yes);
				DbTypeDef dbTypeSample2 = new DbTypeDef ("NUPO", DbSimpleType.Decimal, new DbNumDef (4, 0, 1000, 9999), 0, false, DbNullability.Yes);
				DbTypeDef dbTypeSample3 = new DbTypeDef ("IsMale", DbSimpleType.Decimal, DbNumDef.FromRawType (DbRawType.Boolean), 0, false, DbNullability.Yes);

				DbTypeDef dbType1 = infrastructure.ResolveDbType ("Nom");
				DbTypeDef dbType2 = infrastructure.ResolveDbType ("NUPO");
				DbTypeDef dbType3 = infrastructure.ResolveDbType ("IsMale");

				Assert.IsNull (dbType1);
				Assert.IsNull (dbType2);
				Assert.IsNull (dbType3);

				infrastructure.AddType (dbTypeSample1);
				infrastructure.AddType (dbTypeSample2);
				infrastructure.AddType (dbTypeSample3);

				dbType1 = infrastructure.ResolveDbType ("Nom");
				dbType2 = infrastructure.ResolveDbType ("NUPO");
				dbType3 = infrastructure.ResolveDbType ("IsMale");

				Assert.IsTrue (DbSchemaChecker.AreDbTypeDefEqual (dbTypeSample1, dbType1));
				Assert.IsTrue (DbSchemaChecker.AreDbTypeDefEqual (dbTypeSample2, dbType2));
				Assert.IsTrue (DbSchemaChecker.AreDbTypeDefEqual (dbTypeSample3, dbType3));
			}
		}


		[TestMethod]
		public void AddExistingTypeExceptionTest()
		{
			DbInfrastructureHelper.ResetTestDatabase ();

			using (DbInfrastructure infrastructure = DbInfrastructureHelper.ConnectToTestDatabase ())
			{
				DbTypeDef dbTypeSample1 = new DbTypeDef ("Nom", DbSimpleType.String, null, 40, false, DbNullability.Yes);
				DbTypeDef dbTypeSample2 = new DbTypeDef ("Nom", DbSimpleType.String, null, 40, false, DbNullability.Yes);
				
				infrastructure.AddType (dbTypeSample1);

				ExceptionAssert.Throw<GenericException>
				(
					() => infrastructure.AddType (dbTypeSample2)
				);
			}
		}


		[TestMethod]
		public void RemoveTypeTest()
		{
			DbInfrastructureHelper.ResetTestDatabase ();

			using (DbInfrastructure infrastructure = DbInfrastructureHelper.ConnectToTestDatabase ())
			{
				DbTypeDef dbTypeSample1 = new DbTypeDef ("Nom", DbSimpleType.String, null, 40, false, DbNullability.Yes);
				DbTypeDef dbTypeSample2 = new DbTypeDef ("NUPO", DbSimpleType.Decimal, new DbNumDef (4, 0, 1000, 9999), 0, false, DbNullability.Yes);
				DbTypeDef dbTypeSample3 = new DbTypeDef ("IsMale", DbSimpleType.Decimal, DbNumDef.FromRawType (DbRawType.Boolean), 0, false, DbNullability.Yes);

				infrastructure.AddType (dbTypeSample1);
				infrastructure.AddType (dbTypeSample2);
				infrastructure.AddType (dbTypeSample3);

				DbTypeDef dbType1 = infrastructure.ResolveDbType ("Nom");
				DbTypeDef dbType2 = infrastructure.ResolveDbType ("NUPO");
				DbTypeDef dbType3 = infrastructure.ResolveDbType ("IsMale");

				Assert.IsTrue (DbSchemaChecker.AreDbTypeDefEqual (dbTypeSample1, dbType1));
				Assert.IsTrue (DbSchemaChecker.AreDbTypeDefEqual (dbTypeSample2, dbType2));
				Assert.IsTrue (DbSchemaChecker.AreDbTypeDefEqual (dbTypeSample3, dbType3));

				infrastructure.RemoveType (dbType1);
				infrastructure.RemoveType (dbType2);
				infrastructure.RemoveType (dbType3);
				
				dbType1 = infrastructure.ResolveDbType ("Nom");
				dbType2 = infrastructure.ResolveDbType ("NUPO");
				dbType3 = infrastructure.ResolveDbType ("IsMale");
				
				Assert.IsNull (dbType1);
				Assert.IsNull (dbType2);
				Assert.IsNull (dbType3);
			}
		}


		[TestMethod]
		public void RemoveUnexistingTypeExceptionTest()
		{
			DbInfrastructureHelper.ResetTestDatabase ();

			using (DbInfrastructure infrastructure = DbInfrastructureHelper.ConnectToTestDatabase ())
			{
				DbTypeDef dbTypeSample = new DbTypeDef ("Nom", DbSimpleType.String, null, 40, false, DbNullability.Yes);
				
				ExceptionAssert.Throw<GenericException>
				(
					() => infrastructure.RemoveType (dbTypeSample)
				);
			}
		}


		[TestMethod]
		public void RemoveBuiltInTypeExceptionTest()
		{
			DbInfrastructureHelper.ResetTestDatabase ();

			using (DbInfrastructure infrastructure = DbInfrastructureHelper.ConnectToTestDatabase ())
			{
				DbTypeDef dbType = infrastructure.FindBuiltInDbTypes ().First ();

				ExceptionAssert.Throw<GenericException>
				(
					() => infrastructure.RemoveType (dbType)
				);
			}
		}


		[TestMethod]
		public void RemoveUsedTypeExceptionTest()
		{
			DbInfrastructureHelper.ResetTestDatabase ();

			using (DbInfrastructure infrastructure = DbInfrastructureHelper.ConnectToTestDatabase ())
			{
				DbTypeDef dbType  = new DbTypeDef ("Name", DbSimpleType.String, null, 80, false, DbNullability.No);
				DbTable dbTable = infrastructure.CreateDbTable ("SimpleTest", DbElementCat.ManagedUserData, false);
				DbColumn dbColumn = DbTable.CreateUserDataColumn ("Name", dbType);

				dbTable.Columns.Add (dbColumn);

				infrastructure.AddType (dbType);
				infrastructure.AddTable (dbTable);

				ExceptionAssert.Throw<GenericException>
				(
					() => infrastructure.RemoveType (dbType)
				);
			}
		}


		[TestMethod]
		public void ResolveDbTypeTest()
		{
			DbInfrastructureHelper.ResetTestDatabase ();

			using (DbInfrastructure infrastructure = DbInfrastructureHelper.ConnectToTestDatabase ())
			{
				DbTypeDef dbType1 = new DbTypeDef ("Nom", DbSimpleType.String, null, 40, false, DbNullability.Yes);
				DbTypeDef dbType2 = new DbTypeDef ("NUPO", DbSimpleType.Decimal, new DbNumDef (4, 0, 1000, 9999), 0, false, DbNullability.Yes);
				DbTypeDef dbType3 = new DbTypeDef ("IsMale", DbSimpleType.Decimal, DbNumDef.FromRawType (DbRawType.Boolean), 0, false, DbNullability.Yes);

				infrastructure.AddType (dbType1);
				infrastructure.AddType (dbType2);
				infrastructure.AddType (dbType3);
			}

			using (DbInfrastructure infrastructure = DbInfrastructureHelper.ConnectToTestDatabase ())
			{
				DbTypeDef dbType1 = infrastructure.ResolveDbType ("Nom");
				DbTypeDef dbType2 = infrastructure.ResolveDbType ("NUPO");
				DbTypeDef dbType3 = infrastructure.ResolveDbType ("IsMale");
				
				Assert.IsNotNull (dbType1);
				Assert.IsNotNull (dbType2);
				Assert.IsNotNull (dbType3);

				Assert.AreEqual ("Nom", dbType1.Name);
				Assert.AreEqual ("NUPO", dbType2.Name);
				Assert.AreEqual ("IsMale", dbType3.Name);
			}
		}


		[TestMethod]
		public void AddTableTest()
		{
			DbInfrastructureHelper.ResetTestDatabase ();

			using (DbInfrastructure infrastructure = DbInfrastructureHelper.ConnectToTestDatabase ())
			{
				DbTable dbTable1 = infrastructure.CreateDbTable ("SimpleTest", DbElementCat.ManagedUserData, false);

				DbTypeDef dbTypeName  = new DbTypeDef ("Name", DbSimpleType.String, null, 80, false, DbNullability.No);
				DbTypeDef dbTypeLevel = new DbTypeDef ("Level", DbSimpleType.String, null, 4, false, DbNullability.No);
				DbTypeDef dbTypeType  = new DbTypeDef ("Type", DbSimpleType.String, null, 25, false, DbNullability.Yes);
				DbTypeDef dbTypeData  = new DbTypeDef ("Data", DbSimpleType.ByteArray, null, 0, false, DbNullability.Yes);
				DbTypeDef dbTypeGuid  = new DbTypeDef ("Guid", DbSimpleType.Guid, null, 0, false, DbNullability.Yes);

				infrastructure.AddType (dbTypeName);
				infrastructure.AddType (dbTypeLevel);
				infrastructure.AddType (dbTypeType);
				infrastructure.AddType (dbTypeData);
				infrastructure.AddType (dbTypeGuid);

				DbColumn col1 = DbTable.CreateUserDataColumn ("Name", dbTypeName);
				DbColumn col2 = DbTable.CreateUserDataColumn ("Level", dbTypeLevel);
				DbColumn col3 = DbTable.CreateUserDataColumn ("Type", dbTypeType);
				DbColumn col4 = DbTable.CreateUserDataColumn ("Data", dbTypeData);
				DbColumn col5 = DbTable.CreateUserDataColumn ("Guid", dbTypeGuid);

				dbTable1.Columns.AddRange (new DbColumn[] { col1, col2, col3, col4, col5 });
				dbTable1.AddIndex (col1);
				dbTable1.AddIndex (col2);

				infrastructure.AddTable (dbTable1);
				infrastructure.ClearCaches ();

				DbTable dbTable2 = infrastructure.ResolveDbTable ("SimpleTest");

				Assert.IsNotNull (dbTable2);
				Assert.AreEqual (dbTable1.Name, dbTable2.Name);
				Assert.AreEqual (dbTable1.Category, dbTable2.Category);
				Assert.AreEqual (dbTable1.PrimaryKeys.Count, dbTable2.PrimaryKeys.Count);
				Assert.AreEqual (dbTable1.PrimaryKeys[0].Name, dbTable2.PrimaryKeys[0].Name);
				Assert.AreEqual (dbTable1.Columns.Count, dbTable2.Columns.Count);
			}
		}


		[TestMethod]
		public void AddTableWithRelationTest()
		{
			DbInfrastructureHelper.ResetTestDatabase ();

			using (DbInfrastructure infrastructure = DbInfrastructureHelper.ConnectToTestDatabase ())
			{
				DbTable dbTableRef = infrastructure.FindDbTables (DbElementCat.Any).First ();

				DbTable dbTable1 = infrastructure.CreateDbTable ("table", DbElementCat.ManagedUserData, false);

				DbColumn relationColumn = DbTable.CreateRelationColumn (Druid.FromLong (0), dbTableRef, DbCardinality.Collection);
				relationColumn.DefineDisplayName ("column");

				dbTable1.Columns.Add (relationColumn);
								
				infrastructure.AddTable (dbTable1);
				infrastructure.ClearCaches ();

				DbTable dbTable2 = infrastructure.ResolveDbTable ("table");
				DbTable dbTableRelation2 = infrastructure.ResolveDbTable (dbTable1.GetRelationTableName (relationColumn));

				Assert.IsTrue (DbSchemaChecker.CheckTables (dbTable1, dbTable2));
				Assert.IsNotNull (dbTableRelation2);
			}
		}


		[TestMethod]
		public void AddTableWithInvalidRelationExceptionTest()
		{
			DbInfrastructureHelper.ResetTestDatabase ();

			using (DbInfrastructure infrastructure = DbInfrastructureHelper.ConnectToTestDatabase ())
			{
				DbTable dbTable1 = infrastructure.CreateDbTable ("table1", DbElementCat.ManagedUserData, false);
				DbTable dbTable2 = infrastructure.CreateDbTable ("table2", DbElementCat.ManagedUserData, false);

				DbColumn relationColumn = DbTable.CreateRelationColumn (Druid.FromLong (0), dbTable2, DbCardinality.Collection);
				dbTable1.Columns.Add (relationColumn);

				ExceptionAssert.Throw<GenericException>
				(
					() => infrastructure.AddTable (dbTable1)
				);
			}
		}


		[TestMethod]
		public void AddTableWithInvalidTypeExceptionTest()
		{
			DbInfrastructureHelper.ResetTestDatabase ();

			using (DbInfrastructure infrastructure = DbInfrastructureHelper.ConnectToTestDatabase ())
			{
				DbTypeDef dbType = new DbTypeDef ("type", DbSimpleType.String, null, 80, false, DbNullability.No);
				DbTable dbTable = infrastructure.CreateDbTable ("table", DbElementCat.ManagedUserData, false);

				DbColumn dbColumn = DbTable.CreateUserDataColumn ("column", dbType);
				dbTable.Columns.Add (dbColumn);

				ExceptionAssert.Throw<GenericException>
				(
					() => infrastructure.AddTable (dbTable)
				);
			}
		}


		[TestMethod]
		public void AddTablesWithRelationTest()
		{
			DbInfrastructureHelper.ResetTestDatabase ();

			using (DbInfrastructure infrastructure = DbInfrastructureHelper.ConnectToTestDatabase ())
			{
				DbTable dbTable1 = infrastructure.CreateDbTable ("table1", DbElementCat.ManagedUserData, false);
				DbTable dbTable2 = infrastructure.CreateDbTable ("table2", DbElementCat.ManagedUserData, false);

				DbColumn relationColumn1 = DbTable.CreateRelationColumn (Druid.FromLong (0), dbTable1, DbCardinality.Collection);
				relationColumn1.DefineDisplayName ("column1");
				dbTable2.Columns.Add (relationColumn1);

				DbColumn relationColumn2 = DbTable.CreateRelationColumn (Druid.FromLong (0), dbTable2, DbCardinality.Collection);
				relationColumn2.DefineDisplayName ("column2");
				dbTable1.Columns.Add (relationColumn2);

				infrastructure.AddTables (new List<DbTable> () { dbTable1, dbTable2 });
				infrastructure.ClearCaches ();

				DbTable dbTable3 = infrastructure.ResolveDbTable ("table1");
				DbTable dbTable4 = infrastructure.ResolveDbTable ("table2");
				DbTable dbTableRelation3 = infrastructure.ResolveDbTable (dbTable1.GetRelationTableName (relationColumn1));
				DbTable dbTableRelation4 = infrastructure.ResolveDbTable (dbTable1.GetRelationTableName (relationColumn1));

				Assert.IsTrue (DbSchemaChecker.CheckTables (dbTable1, dbTable3));
				Assert.IsTrue (DbSchemaChecker.CheckTables (dbTable2, dbTable4));
				Assert.IsNotNull (dbTableRelation3);
				Assert.IsNotNull (dbTableRelation4);
			}
		}


		[TestMethod]
		public void AddTablesWithRelationExceptionTest()
		{
			DbInfrastructureHelper.ResetTestDatabase ();

			using (DbInfrastructure infrastructure = DbInfrastructureHelper.ConnectToTestDatabase ())
			{
				DbTable dbTable1 = infrastructure.CreateDbTable ("table1", DbElementCat.ManagedUserData, false);
				DbTable dbTable2 = infrastructure.CreateDbTable ("table2", DbElementCat.ManagedUserData, false);
				DbTable dbTable3 = infrastructure.CreateDbTable ("table3", DbElementCat.ManagedUserData, false);

				DbColumn relationColumn1 = DbTable.CreateRelationColumn (Druid.FromLong (0), dbTable2, DbCardinality.Collection);
				relationColumn1.DefineDisplayName ("column1");
				dbTable1.Columns.Add (relationColumn1);

				DbColumn relationColumn2 = DbTable.CreateRelationColumn (Druid.FromLong (0), dbTable3, DbCardinality.Collection);
				relationColumn2.DefineDisplayName ("column2");
				dbTable1.Columns.Add (relationColumn2);

				ExceptionAssert.Throw<GenericException>
				(
					() => infrastructure.AddTables (new List<DbTable> () { dbTable1, dbTable2 })
				);			
			}
		}


		[TestMethod]
		public void AddExistingTableExceptionTest()
		{
			DbInfrastructureHelper.ResetTestDatabase ();

			using (DbInfrastructure infrastructure = DbInfrastructureHelper.ConnectToTestDatabase ())
			{
				DbTypeDef dbTypeName  = new DbTypeDef ("Name", DbSimpleType.String, null, 80, false, DbNullability.No);
				infrastructure.AddType (dbTypeName);
				
				DbTable dbTable1 = infrastructure.CreateDbTable ("table", DbElementCat.ManagedUserData, false);
				infrastructure.AddTable (dbTable1);

				DbTable dbTable2 = infrastructure.CreateDbTable ("table", DbElementCat.ManagedUserData, false);
				
				ExceptionAssert.Throw<Exceptions.GenericException>
				(
					() => infrastructure.AddTable (dbTable2)
				);
			}
		}


		[TestMethod]
		public void RemoveTableTest()
		{
			DbInfrastructureHelper.ResetTestDatabase ();

			using (DbInfrastructure infrastructure = DbInfrastructureHelper.ConnectToTestDatabase ())
			{
				DbTable dbTable = infrastructure.CreateDbTable ("table", DbElementCat.ManagedUserData, false);
				infrastructure.AddTable (dbTable);
			}

			using (DbInfrastructure infrastructure = DbInfrastructureHelper.ConnectToTestDatabase ())
			{
				DbTable dbTable = infrastructure.ResolveDbTable ("table");
				Assert.IsNotNull (dbTable);

				infrastructure.RemoveTable (dbTable);
				Assert.IsNull (infrastructure.ResolveDbTable ("table"));
			}
		}


		[TestMethod]
		public void RemoveTablesWithRelationTest()
		{
			DbInfrastructureHelper.ResetTestDatabase ();

			using (DbInfrastructure infrastructure = DbInfrastructureHelper.ConnectToTestDatabase ())
			{
				DbTable dbTable1 = infrastructure.CreateDbTable ("table1", DbElementCat.ManagedUserData, false);
				DbTable dbTable2 = infrastructure.CreateDbTable ("table2", DbElementCat.ManagedUserData, false);

				DbColumn relationColumn1 = DbTable.CreateRelationColumn (Druid.FromLong (0), dbTable1, DbCardinality.Collection);
				relationColumn1.DefineDisplayName ("column1");
				dbTable2.Columns.Add (relationColumn1);

				DbColumn relationColumn2 = DbTable.CreateRelationColumn (Druid.FromLong (0), dbTable2, DbCardinality.Collection);
				relationColumn2.DefineDisplayName ("column2");
				dbTable1.Columns.Add (relationColumn2);

				infrastructure.AddTables (new List<DbTable> () { dbTable1, dbTable2 });
				infrastructure.RemoveTable (infrastructure.ResolveDbTable ("table1"));

				Assert.IsNull (infrastructure.ResolveDbTable ("table1"));
				Assert.IsNull (infrastructure.ResolveDbTable (dbTable1.GetRelationTableName (relationColumn2)));
				Assert.IsNull (infrastructure.ResolveDbTable (dbTable2.GetRelationTableName (relationColumn1)));
				Assert.IsNotNull (infrastructure.ResolveDbTable ("table2"));
			}
		}


		[TestMethod]
		public void RemoveUnexistingTableExeptionTest()
		{
			DbInfrastructureHelper.ResetTestDatabase ();

			using (DbInfrastructure infrastructure = DbInfrastructureHelper.ConnectToTestDatabase ())
			{
				DbTable dbTable = infrastructure.CreateDbTable ("table", DbElementCat.ManagedUserData, false);

				ExceptionAssert.Throw<Exceptions.GenericException>
				(
					() => infrastructure.RemoveTable (dbTable)
				);
			}
		}
		

		[TestMethod]
		public void AddColumnToTable()
		{
			DbInfrastructureHelper.ResetTestDatabase ();

			using (DbInfrastructure infrastructure = DbInfrastructureHelper.ConnectToTestDatabase ())
			{
				DbTable table1 = infrastructure.CreateDbTable ("table", DbElementCat.ManagedUserData, false);
				DbTable table2 = infrastructure.CreateDbTable ("table", DbElementCat.ManagedUserData, false);

				infrastructure.AddTable (table1);

				DbColumn column1 = new DbColumn ("column", infrastructure.TypeManager.DefaultInteger, DbColumnClass.Data, DbElementCat.ManagedUserData);
				DbColumn column2 = new DbColumn ("column", infrastructure.TypeManager.DefaultInteger, DbColumnClass.Data, DbElementCat.ManagedUserData);

				infrastructure.AddColumnToTable (table1, column1);
				table2.Columns.Add (column2);

				DbTable result = infrastructure.ResolveDbTable ("table");

				Assert.IsTrue (DbSchemaChecker.CheckTables (result, table2));
			}
		}


		[TestMethod]
		public void AddColumnWithRelationToTable()
		{
			DbInfrastructureHelper.ResetTestDatabase ();

			using (DbInfrastructure infrastructure = DbInfrastructureHelper.ConnectToTestDatabase ())
			{
				DbTable dbTable1 = infrastructure.CreateDbTable ("table", DbElementCat.ManagedUserData, false);
				DbTable dbTable2 = infrastructure.CreateDbTable ("table", DbElementCat.ManagedUserData, false);

				infrastructure.AddTable (dbTable1);

				DbTable dbTableRef = infrastructure.CreateDbTable ("tableRef", DbElementCat.ManagedUserData, false);

				DbColumn relationColumn1 = DbTable.CreateRelationColumn (Druid.FromLong (0), dbTableRef, DbCardinality.Collection);
				relationColumn1.DefineDisplayName ("column");
				dbTable1.Columns.Add (relationColumn1);

				DbColumn relationColumn2 = DbTable.CreateRelationColumn (Druid.FromLong (0), dbTableRef, DbCardinality.Collection);
				relationColumn2.DefineDisplayName ("column");
				dbTable2.Columns.Add (relationColumn2);

				infrastructure.AddTable (dbTableRef);
				infrastructure.AddColumnToTable (dbTable1, relationColumn1);

				DbTable resulta = infrastructure.ResolveDbTable ("table");
				DbTable resultb = infrastructure.ResolveDbTable (dbTable1.GetRelationTableName (relationColumn1));

				Assert.IsTrue (DbSchemaChecker.CheckTables (resulta, dbTable2));
				Assert.IsNotNull (resultb);
			}
		}


		[TestMethod]
		public void AddColumnToTableExistingColumnException()
		{
			DbInfrastructureHelper.ResetTestDatabase ();

			using (DbInfrastructure infrastructure = DbInfrastructureHelper.ConnectToTestDatabase ())
			{
				DbTable table = infrastructure.CreateDbTable ("table", DbElementCat.ManagedUserData, false);
				
				infrastructure.AddTable (table);

				DbColumn column1 = new DbColumn ("column", infrastructure.TypeManager.DefaultInteger, DbColumnClass.Data, DbElementCat.ManagedUserData);
				DbColumn column2 = new DbColumn ("column", infrastructure.TypeManager.DefaultInteger, DbColumnClass.Data, DbElementCat.ManagedUserData);
				
				infrastructure.AddColumnToTable (table, column1);

				ExceptionAssert.Throw<GenericException>
				(
					() => infrastructure.AddColumnToTable (table, column2)
				);
			}
		}


		[TestMethod]
		public void AddColumnToTablePrimaryKeyException()
		{
			DbInfrastructureHelper.ResetTestDatabase ();

			using (DbInfrastructure infrastructure = DbInfrastructureHelper.ConnectToTestDatabase ())
			{
				DbTable table = infrastructure.CreateDbTable ("table", DbElementCat.ManagedUserData, false);

				infrastructure.AddTable (table);

				DbColumn column = new DbColumn ("column", infrastructure.TypeManager.DefaultInteger, DbColumnClass.Data, DbElementCat.ManagedUserData);
				column.DefinePrimaryKey (true);

				ExceptionAssert.Throw<GenericException>
				(
					() => infrastructure.AddColumnToTable (table, column)
				);
			}
		}


		[TestMethod]
		public void AddColumnToTableStatusException()
		{
			DbInfrastructureHelper.ResetTestDatabase ();

			using (DbInfrastructure infrastructure = DbInfrastructureHelper.ConnectToTestDatabase ())
			{
				DbTable table = infrastructure.CreateDbTable ("table", DbElementCat.ManagedUserData, false);

				infrastructure.AddTable (table);

				DbColumn column1 = new DbColumn ("column", infrastructure.TypeManager.DefaultInteger, DbColumnClass.Data, DbElementCat.ManagedUserData);
				column1.DefineColumnClass (DbColumnClass.KeyId);

				DbColumn column2 = new DbColumn ("column", infrastructure.TypeManager.DefaultInteger, DbColumnClass.Data, DbElementCat.ManagedUserData);
				column2.DefineColumnClass (DbColumnClass.KeyId);

				ExceptionAssert.Throw<GenericException>
				(
					() => infrastructure.AddColumnToTable (table, column1)
				);

				ExceptionAssert.Throw<GenericException>
				(
					() => infrastructure.AddColumnToTable (table, column2)
				);
			}
		}


		[TestMethod]
		public void RemoveColumnFromTable()
		{
			DbInfrastructureHelper.ResetTestDatabase ();

			using (DbInfrastructure infrastructure = DbInfrastructureHelper.ConnectToTestDatabase ())
			{
				DbTable table1 = infrastructure.CreateDbTable ("table", DbElementCat.ManagedUserData, false);
				DbTable table2 = infrastructure.CreateDbTable ("table", DbElementCat.ManagedUserData, false);

				DbColumn column1 = new DbColumn ("column", infrastructure.TypeManager.DefaultInteger, DbColumnClass.Data, DbElementCat.ManagedUserData);
				table1.Columns.Add (column1);

				infrastructure.AddTable (table1);

				infrastructure.RemoveColumnFromTable (table1, column1);

				DbTable result = infrastructure.ResolveDbTable ("table");

				Assert.IsTrue (DbSchemaChecker.CheckTables (result, table2));
			}
		}


		[TestMethod]
		public void RemoveColumnWithRelationFromTable()
		{
			DbInfrastructureHelper.ResetTestDatabase ();

			using (DbInfrastructure infrastructure = DbInfrastructureHelper.ConnectToTestDatabase ())
			{
				DbTable dbTable1 = infrastructure.CreateDbTable ("table", DbElementCat.ManagedUserData, false);
				DbTable dbTable2 = infrastructure.CreateDbTable ("table", DbElementCat.ManagedUserData, false);

				DbTable dbTableRef = infrastructure.CreateDbTable ("tableRef", DbElementCat.ManagedUserData, false);

				DbColumn relationColumn1 = DbTable.CreateRelationColumn (Druid.FromLong (0), dbTableRef, DbCardinality.Collection);
				relationColumn1.DefineDisplayName ("column");
				dbTable1.Columns.Add (relationColumn1);

				infrastructure.AddTables (new List<DbTable> () { dbTable1, dbTableRef });

				infrastructure.RemoveColumnFromTable (dbTable1, relationColumn1);

				DbTable resulta = infrastructure.ResolveDbTable ("table");
				DbTable resultb = infrastructure.ResolveDbTable (dbTable1.GetRelationTableName (relationColumn1));

				Assert.IsTrue (DbSchemaChecker.CheckTables (resulta, dbTable2));
				Assert.IsNull (resultb);
			}
		}


		[TestMethod]
		public void RemoveColumnFromTableUnexistingColumnException()
		{
			DbInfrastructureHelper.ResetTestDatabase ();

			using (DbInfrastructure infrastructure = DbInfrastructureHelper.ConnectToTestDatabase ())
			{
				DbTable table = infrastructure.CreateDbTable ("table", DbElementCat.ManagedUserData, false);

				infrastructure.AddTable (table);

				DbColumn column = new DbColumn ("column", infrastructure.TypeManager.DefaultInteger, DbColumnClass.Data, DbElementCat.ManagedUserData);
				
				ExceptionAssert.Throw<GenericException>
				(
					() => infrastructure.RemoveColumnFromTable (table, column)
				);
			}
		}


		[TestMethod]
		public void RemoveColumnFromTablePrimaryKeyException()
		{
			DbInfrastructureHelper.ResetTestDatabase ();

			using (DbInfrastructure infrastructure = DbInfrastructureHelper.ConnectToTestDatabase ())
			{
				DbTable table = infrastructure.CreateDbTable ("table", DbElementCat.ManagedUserData, false);

				infrastructure.AddTable (table);

				ExceptionAssert.Throw<GenericException>
				(
					() => infrastructure.RemoveColumnFromTable (table, table.Columns[Tags.ColumnId])
				);
			}
		}


		[TestMethod]
		public void RemoveColumnFromTableStatusException()
		{
			DbInfrastructureHelper.ResetTestDatabase ();

			using (DbInfrastructure infrastructure = DbInfrastructureHelper.ConnectToTestDatabase ())
			{
				DbTable table = infrastructure.CreateDbTable ("table", DbElementCat.ManagedUserData, false);

				infrastructure.AddTable (table);

				ExceptionAssert.Throw<GenericException>
				(
					() => infrastructure.RemoveColumnFromTable (table, table.Columns[Tags.ColumnId])
				);

				ExceptionAssert.Throw<GenericException>
				(
					() => infrastructure.RemoveColumnFromTable (table, table.Columns[Tags.ColumnStatus])
				);
			}
		}


		[TestMethod]
		public void RemoveColumnFromTableIndexException()
		{
			DbInfrastructureHelper.ResetTestDatabase ();

			using (DbInfrastructure infrastructure = DbInfrastructureHelper.ConnectToTestDatabase ())
			{
				DbTable table = infrastructure.CreateDbTable ("table", DbElementCat.ManagedUserData, false);
				DbColumn column = new DbColumn ("column", infrastructure.TypeManager.DefaultInteger, DbColumnClass.Data, DbElementCat.ManagedUserData);

				table.Columns.Add (column);
				table.Indexes.Add (new DbIndex (SqlSortOrder.Ascending, column));

				infrastructure.AddTable (table);

				ExceptionAssert.Throw<GenericException>
				(
					() => infrastructure.RemoveColumnFromTable (table, column)
				);
			}
		}


		[TestMethod]
		public void MultipleTransactionsTest()
		{
			DbInfrastructureHelper.ResetTestDatabase ();

			using (DbInfrastructure infrastructure = DbInfrastructureHelper.ConnectToTestDatabase ())
			{
				Assert.IsNotNull (infrastructure);

				using (IDbAbstraction dbAbstraction1 = infrastructure.CreateDatabaseAbstraction ())
				using (IDbAbstraction dbAbstraction2 = infrastructure.CreateDatabaseAbstraction ())
				{
					Assert.AreNotSame (dbAbstraction1, dbAbstraction2);
					Assert.AreNotSame (dbAbstraction1.SqlBuilder, dbAbstraction2.SqlBuilder);
					Assert.AreSame (dbAbstraction1.Factory, dbAbstraction2.Factory);

					using (DbTransaction transaction1 = infrastructure.BeginTransaction (DbTransactionMode.ReadOnly))
					using (DbTransaction transaction2 = infrastructure.BeginTransaction (DbTransactionMode.ReadWrite, dbAbstraction1))
					using (DbTransaction transaction3 = infrastructure.BeginTransaction (DbTransactionMode.ReadOnly, dbAbstraction2))
					{
						Assert.AreEqual (3, infrastructure.LiveTransactions.Length);
						
						using (DbTransaction transaction4 = infrastructure.InheritOrBeginTransaction (DbTransactionMode.ReadOnly))
						{
							Assert.AreEqual (3, infrastructure.LiveTransactions.Length);
							transaction4.Commit ();
						}

						transaction3.Commit ();
						Assert.AreEqual (2, infrastructure.LiveTransactions.Length);
						
						transaction2.Rollback ();
						Assert.AreEqual (1, infrastructure.LiveTransactions.Length);
						
						transaction1.Commit ();
						Assert.AreEqual (0, infrastructure.LiveTransactions.Length);
					}
				}
			}
		}


		[TestMethod]
		public void MultipleTransactionsExeptionTest1()
		{
			DbInfrastructureHelper.ResetTestDatabase ();

			using (DbInfrastructure infrastructure = DbInfrastructureHelper.ConnectToTestDatabase ())
			{
				DbTransaction transaction1 = infrastructure.BeginTransaction (DbTransactionMode.ReadOnly);

				ExceptionAssert.Throw<System.InvalidOperationException>
				(
					() => infrastructure.InheritOrBeginTransaction (DbTransactionMode.ReadWrite)
				);

				transaction1.Commit ();
				transaction1.Dispose ();
			}
		}


		[TestMethod]
		public void MultipleTransactionsExeptionTest2()
		{
			DbInfrastructureHelper.ResetTestDatabase ();

			using (DbInfrastructure infrastructure = DbInfrastructureHelper.ConnectToTestDatabase ())
			{
				DbTransaction transaction1 = infrastructure.BeginTransaction (DbTransactionMode.ReadOnly);
				DbTransaction transaction2 = infrastructure.InheritOrBeginTransaction (DbTransactionMode.ReadOnly);

				ExceptionAssert.Throw<System.InvalidOperationException>
				(
					() => transaction2.Dispose ()
				);

				ExceptionAssert.Throw<System.InvalidOperationException>
				(
					() => transaction1.Dispose ()
				);
			}
		}


		[TestMethod]
		public void MultipleTransactionsExeptionTest3()
		{
			DbInfrastructureHelper.ResetTestDatabase ();

			using (DbInfrastructure infrastructure = DbInfrastructureHelper.ConnectToTestDatabase ())
			{		
				DbTransaction transaction1 = infrastructure.BeginTransaction (DbTransactionMode.ReadOnly);
				DbTransaction transaction2 = infrastructure.InheritOrBeginTransaction (DbTransactionMode.ReadOnly);

				ExceptionAssert.Throw<System.InvalidOperationException>
				(
					() => transaction1.Commit ()
				);
			}
		}


		[TestMethod]
		public void MultipleTransactionsExceptionTest4()
		{
			DbInfrastructureHelper.ResetTestDatabase ();

			using (DbInfrastructure infrastructure = DbInfrastructureHelper.ConnectToTestDatabase ())
			{
				DbTransaction transaction1 = infrastructure.BeginTransaction (DbTransactionMode.ReadOnly);
				DbTransaction transaction2 = infrastructure.InheritOrBeginTransaction (DbTransactionMode.ReadOnly);

				ExceptionAssert.Throw<System.InvalidOperationException>
				(
					() => transaction1.Rollback ()
				);
			}
		}


		[TestMethod]
		public void MultipleTransactionsExceptionTest5()
		{
			DbInfrastructureHelper.ResetTestDatabase ();

			using (DbInfrastructure infrastructure = DbInfrastructureHelper.ConnectToTestDatabase ())
			{
				DbTransaction transaction1 = infrastructure.BeginTransaction (DbTransactionMode.ReadOnly);
				DbTransaction transaction2 = infrastructure.InheritOrBeginTransaction (DbTransactionMode.ReadOnly);

				ExceptionAssert.Throw<System.InvalidOperationException>
				(
					() => transaction2.Rollback ()
				);
			}
		}


	}


}

﻿using Epsitec.Common.Support;

using Epsitec.Common.Types;

using Epsitec.Common.UnitTesting;

using Epsitec.Cresus.Database;

using Epsitec.Cresus.DataLayer.Schema;
using Epsitec.Cresus.DataLayer.UnitTests.Helpers;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.Collections.Generic;

using System.Linq;


namespace Epsitec.Cresus.DataLayer.UnitTests.Schema
{


    [TestClass]
	public sealed class UnitTestSchemaBuilder
	{


		[ClassInitialize]
		public static void ClassInitialize(TestContext testContext)
		{
			TestHelper.Initialize ();

			DatabaseHelper.CreateAndConnectToDatabase ();
		}


		[ClassCleanup]
		public static void ClassCleanup()
		{
			DatabaseHelper.DisconnectFromDatabase ();
		}


		[TestInitialize]
		public void TestInitialize()
		{
			DatabaseHelper.CreateAndConnectToDatabase ();
		}

		
		[TestMethod]
		[DeploymentItem ("Cresus.DataLayer.dll")]
		public void SchemaBuilderConstructorTest()
		{
			DbInfrastructure dbInfrastructure = DatabaseHelper.DbInfrastructure;

			new SchemaBuilder_Accessor (dbInfrastructure);
		}


		[TestMethod]
		[DeploymentItem ("Cresus.DataLayer.dll")]
		public void SchemaBuilderConstructorArgumentCheck()
		{
			ExceptionAssert.Throw<System.ArgumentNullException>
			(
				() => new SchemaBuilder_Accessor ((DbInfrastructure) null)
			);
		}


		[TestMethod]
		[DeploymentItem ("Cresus.DataLayer.dll")]
		public void BuildTableTest()
		{
			foreach (var tuple in this.GetSampleEntityIds ())
			{
				DbInfrastructure dbInfrastructure = DatabaseHelper.DbInfrastructure;

				var builder = new SchemaBuilder_Accessor (dbInfrastructure);

				List<DbTable> newTables = new List<DbTable> ();

				using (DbTransaction transaction = dbInfrastructure.BeginTransaction ())
				{
					builder.BuildTable (transaction, newTables, tuple.Item1);

					transaction.Commit ();
				}

				Assert.IsTrue (tuple.Item2.Except (newTables.Select (t => t.CaptionId)).Count () == 0);
				Assert.IsTrue (newTables.Select (t => t.CaptionId).Except (tuple.Item2).Count () == 0);

				DatabaseHelper.CreateAndConnectToDatabase ();
			}
		}


		[TestMethod]
		[DeploymentItem ("Cresus.DataLayer.dll")]
		public void BuildTypeDefTest()
		{
			DbInfrastructure dbInfrastructure = DatabaseHelper.DbInfrastructure;

			var builder = new SchemaBuilder_Accessor (dbInfrastructure);

			List<DbTypeDef> types1 = new List<DbTypeDef> ();
			List<DbTypeDef> types2 = new List<DbTypeDef> ();

			foreach (INamedType type in this.GetSampleTypes ())
			{
				using (DbTransaction transaction = dbInfrastructure.BeginTransaction ())
				{
					DbTypeDef typeDef = builder.BuildTypeDef (transaction, type);

					Assert.IsNotNull (typeDef);

					transaction.Commit ();

					types1.Add (typeDef);
				}
			}

			DatabaseHelper.DisconnectFromDatabase ();
			DatabaseHelper.ConnectToDatabase ();

			dbInfrastructure = DatabaseHelper.DbInfrastructure;
			builder = new SchemaBuilder_Accessor (dbInfrastructure);

			foreach (INamedType type in this.GetSampleTypes ())
			{
				using (DbTransaction transaction = dbInfrastructure.BeginTransaction ())
				{
					DbTypeDef typeDef = typeDef = dbInfrastructure.ResolveDbType (transaction, type);

					Assert.IsNotNull (typeDef);

					transaction.Commit ();

					types2.Add (typeDef);
				}
			}

			CollectionAssert.AreEquivalent (types1, types2);
		}


		[TestMethod]
		[DeploymentItem ("Cresus.DataLayer.dll")]
		public void CreateSchemaTest()
		{
			foreach (var tuple in this.GetSampleEntityIds ())
			{
				DbInfrastructure dbInfrastructure = DatabaseHelper.DbInfrastructure;

				List<Druid> tables1 = new List<Druid> ();
				List<Druid> tables2 = new List<Druid> ();

				var builder = new SchemaBuilder_Accessor (dbInfrastructure);

				using (DbTransaction transaction = dbInfrastructure.BeginTransaction ())
				{
					List<DbTable> newTables = builder.CreateSchema (transaction, tuple.Item1).ToList ();

					transaction.Commit ();

					tables1.AddRange (newTables.Select (t => t.CaptionId));
				}

				DatabaseHelper.DisconnectFromDatabase ();
				DatabaseHelper.ConnectToDatabase ();

				dbInfrastructure = DatabaseHelper.DbInfrastructure;
				builder = new SchemaBuilder_Accessor (dbInfrastructure);

				using (DbTransaction transaction = dbInfrastructure.BeginTransaction ())
				{
					List<DbTable> newTables = builder.CreateSchema (transaction, tuple.Item1).ToList ();

					transaction.Commit ();

					tables2.AddRange (newTables.Select (t => t.CaptionId));
				}

				Assert.IsTrue (tables1.Except (tuple.Item2).Count () == 0);
				Assert.IsTrue (tuple.Item2.Except (tables1).Count () == 0);
				Assert.IsTrue (tables2.Count == 0);

				DatabaseHelper.CreateAndConnectToDatabase ();
			}
		}


		[TestMethod]
		[DeploymentItem ("Cresus.DataLayer.dll")]
		public void CreateSchemaArgumentCheck()
		{
			DbInfrastructure dbInfrastructure = DatabaseHelper.DbInfrastructure;

			var builder = new SchemaBuilder_Accessor (dbInfrastructure);

			ExceptionAssert.Throw<System.ArgumentNullException>
			(
				() => builder.CreateSchema (null, Druid.Parse ("[L0AM]"))
			);
		}


		[TestMethod]
		[DeploymentItem ("Cresus.DataLayer.dll")]
		public void GetOrCreateTypeDefTest()
		{
			DbInfrastructure dbInfrastructure = DatabaseHelper.DbInfrastructure;

			var builder = new SchemaBuilder_Accessor (dbInfrastructure);

			foreach (INamedType type in this.GetSampleTypes ())
			{
				DbTypeDef type1;
				DbTypeDef type2;

				using (DbTransaction transaction = dbInfrastructure.BeginTransaction ())
				{
					type1 = builder.GetOrCreateTypeDef (transaction, type);

					Assert.IsNotNull (type1);

					transaction.Commit ();
				}

				using (DbTransaction transaction = dbInfrastructure.BeginTransaction ())
				{
					type2 =  builder.GetOrCreateTypeDef (transaction, type);

					Assert.IsNotNull (type2);

					transaction.Commit ();
				}

				Assert.AreEqual (type1, type2);
			}
		}


		[TestMethod]
		[DeploymentItem ("Cresus.DataLayer.dll")]
		public void LookForTableInCacheTest()
		{
			foreach (var tuple in this.GetSampleEntityIds ())
			{
				DbInfrastructure dbInfrastructure = DatabaseHelper.DbInfrastructure;

				List<DbTable> tables1 = new List<DbTable> ();
				List<DbTable> tables2 = new List<DbTable> ();

				var builder = new SchemaBuilder_Accessor (dbInfrastructure);

				using (DbTransaction transaction = dbInfrastructure.BeginTransaction ())
				{
					tables1 = builder.CreateSchema (transaction, tuple.Item1).ToList ();

					transaction.Commit ();
				}

				foreach (DbTable table1 in tables1)
				{
					DbTable table2 = builder.LookForTableInCache (table1.CaptionId);

					Assert.IsNotNull (table2);

					tables2.Add (table2);
				}

				CollectionAssert.AreEqual (tables1, tables2);
				Assert.IsTrue (tables1.Select (t => t.CaptionId).Except (tuple.Item2).Count () == 0);
				Assert.IsTrue (tuple.Item2.Except (tables1.Select (t => t.CaptionId)).Count () == 0);

				DatabaseHelper.CreateAndConnectToDatabase ();
			}
		}


		[TestMethod]
		[DeploymentItem ("Cresus.DataLayer.dll")]
		public void LookForTableInDatabaseTest()
		{
			foreach (var tuple in this.GetSampleEntityIds ())
			{
				DbInfrastructure dbInfrastructure = DatabaseHelper.DbInfrastructure;

				List<Druid> tables1 = new List<Druid> ();
				List<Druid> tables2 = new List<Druid> ();

				var builder = new SchemaBuilder_Accessor (dbInfrastructure);

				using (DbTransaction transaction = dbInfrastructure.BeginTransaction ())
				{
					List<DbTable> newTables = builder.CreateSchema (transaction, tuple.Item1).ToList ();

					transaction.Commit ();

					tables1.AddRange (newTables.Select (t => t.CaptionId));
				}

				DatabaseHelper.DisconnectFromDatabase ();
				DatabaseHelper.ConnectToDatabase ();

				dbInfrastructure = DatabaseHelper.DbInfrastructure;
				builder = new SchemaBuilder_Accessor (dbInfrastructure);

				using (DbTransaction transaction = dbInfrastructure.BeginTransaction ())
				{
					foreach (Druid druid in tuple.Item2)
					{
						DbTable table = builder.LookForTableInDatabase (transaction, druid);

						Assert.IsNotNull (table);

						tables2.Add (table.CaptionId);
					}

					transaction.Commit ();
				}

				Assert.IsTrue (tables1 .Except (tables2).Count () == 0);
				Assert.IsTrue (tables2.Except (tables1).Count () == 0);

				DatabaseHelper.CreateAndConnectToDatabase ();
			}
		}


		[TestMethod]
		[DeploymentItem ("Cresus.DataLayer.dll")]
		public void LookForTypeDefInCacheTest()
		{
			DbInfrastructure dbInfrastructure = DatabaseHelper.DbInfrastructure;

			var builder = new SchemaBuilder_Accessor (dbInfrastructure);

			foreach (INamedType type in this.GetSampleTypes ())
			{
				DbTypeDef type1;
				DbTypeDef type2;

				using (DbTransaction transaction = dbInfrastructure.BeginTransaction ())
				{
					type1 = builder.BuildTypeDef (transaction, type);

					Assert.IsNotNull (type1);

					transaction.Commit ();
				}

				using (DbTransaction transaction = dbInfrastructure.BeginTransaction ())
				{
					type2 = builder.LookForTypeDefInCache (type);

					Assert.IsNotNull (type2);

					transaction.Commit ();
				}

				Assert.AreEqual (type1, type2);
			}

			DatabaseHelper.DisconnectFromDatabase ();
			DatabaseHelper.ConnectToDatabase ();

			dbInfrastructure = DatabaseHelper.DbInfrastructure;
			builder = new SchemaBuilder_Accessor (dbInfrastructure);

			foreach (INamedType type in this.GetSampleTypes ())
			{
				DbTypeDef type1;
				DbTypeDef type2;

				using (DbTransaction transaction = dbInfrastructure.BeginTransaction ())
				{
					type1 = builder.LookForTypeDefInDatabase (transaction, type);

					Assert.IsNotNull (type1);

					transaction.Commit ();
				}

				using (DbTransaction transaction = dbInfrastructure.BeginTransaction ())
				{
					type2 = builder.LookForTypeDefInCache (type);

					Assert.IsNotNull (type2);

					transaction.Commit ();
				}

				Assert.AreEqual (type1, type2);
			}
		}


		[TestMethod]
		[DeploymentItem ("Cresus.DataLayer.dll")]
		public void LookForTypeDefInDatabaseTest()
		{
			DbInfrastructure dbInfrastructure = DatabaseHelper.DbInfrastructure;

			var builder = new SchemaBuilder_Accessor (dbInfrastructure);

			List<DbTypeDef> types1 = new List<DbTypeDef> ();
			List<DbTypeDef> types2 = new List<DbTypeDef> ();

			foreach (INamedType type in this.GetSampleTypes ())
			{
				using (DbTransaction transaction = dbInfrastructure.BeginTransaction ())
				{
					DbTypeDef typeDef = new DbTypeDef (type);

					dbInfrastructure.RegisterNewDbType (transaction, typeDef);

					transaction.Commit ();

					types1.Add (typeDef);
				}
			}

			DatabaseHelper.DisconnectFromDatabase ();
			DatabaseHelper.ConnectToDatabase ();

			dbInfrastructure = DatabaseHelper.DbInfrastructure;
			builder = new SchemaBuilder_Accessor (dbInfrastructure);

			foreach (INamedType type in this.GetSampleTypes ())
			{
				using (DbTransaction transaction = dbInfrastructure.BeginTransaction ())
				{
					DbTypeDef typeDef = builder.LookForTypeDefInDatabase (transaction, type);

					Assert.IsNotNull (typeDef);

					transaction.Commit ();

					types2.Add (typeDef);
				}
			}

			CollectionAssert.AreEquivalent (types1, types2);
		}


		[TestMethod]
		[DeploymentItem ("Cresus.DataLayer.dll")]
		public void DbInfrastructureTest()
		{
			DbInfrastructure dbInfrastructure = DatabaseHelper.DbInfrastructure;

			var builder = new SchemaBuilder_Accessor (dbInfrastructure);

			Assert.AreSame (dbInfrastructure, builder.DbInfrastructure);
		}


		private IEnumerable<System.Tuple<Druid, List<Druid>>> GetSampleEntityIds()
		{
			yield return System.Tuple.Create (Druid.Parse ("[L0AT]"), new List<Druid> ()
			{
				Druid.Parse ("[L0AT]"),
				Druid.Parse ("[L0AA1]"),
			});

			yield return System.Tuple.Create (Druid.Parse ("[L0A5]"), new List<Druid> ()
			{
				Druid.Parse ("[L0A5]"),
				Druid.Parse ("[L0A4]"),
				Druid.Parse ("[L0A1]"),
			});

			yield return System.Tuple.Create (Druid.Parse ("[L0AM]"), new List<Druid> ()
			{
				Druid.Parse ("[L0AM]"),
				Druid.Parse ("[L0AN]"),
				Druid.Parse ("[L0A21]"),
				Druid.Parse ("[L0AT]"),
				Druid.Parse ("[L0AA1]"),
				Druid.Parse ("[L0AP]"),
				Druid.Parse ("[L0AE1]"),
				Druid.Parse ("[L0AQ1]"),
				Druid.Parse ("[L0AO]"),
				Druid.Parse ("[L0AL1]"),
			});
		}


		private IEnumerable<INamedType> GetSampleTypes()
		{
			yield return BooleanType.Default;
			yield return DateTimeType.Default;
			yield return DateType.Default;
			yield return DecimalType.Default;
			yield return DoubleType.Default;

			// Don't try with the following ones, because they are already added by DbInfrastructure
			// for the uid counters.
			// Marc

			//yield return IntegerType.Default;
			//yield return LongIntegerType.Default;
		}


	}


}

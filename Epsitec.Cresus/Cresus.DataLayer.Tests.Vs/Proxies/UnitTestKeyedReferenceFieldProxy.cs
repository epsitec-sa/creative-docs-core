﻿using Epsitec.Common.Support;
using Epsitec.Common.Types;

using Epsitec.Common.UnitTesting;

using Epsitec.Cresus.Database;

using Epsitec.Cresus.DataLayer.Context;
using Epsitec.Cresus.DataLayer.Infrastructure;
using Epsitec.Cresus.DataLayer.Proxies;
using Epsitec.Cresus.DataLayer.Tests.Vs.Entities;
using Epsitec.Cresus.DataLayer.Tests.Vs.Helpers;

using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace Epsitec.Cresus.DataLayer.Tests.Vs.Proxies
{


	[TestClass]
	public sealed class UnitTestKeyedReferenceFieldProxy
	{


		[ClassInitialize]
		public static void ClassInitialize(TestContext testContext)
		{
			TestHelper.Initialize ();
		}


		[TestInitialize]
		public void TestInitialize()
		{
			DatabaseCreator2.ResetPopulatedTestDatabase ();
		}


		[TestMethod]
		public void KeyedReferenceFieldProxyConstructorTest()
		{
			using (DB db = DB.ConnectToTestDatabase ())
			using (DataContext dataContext = DataContextHelper.ConnectToTestDatabase (db.DataInfrastructure))
			{
				NaturalPersonEntity person = dataContext.ResolveEntity<NaturalPersonEntity> (new DbKey (new DbId (1000000001)));
				Druid fieldId = Druid.Parse ("[J1AN1]");
				EntityKey targetKey = dataContext.GetNormalizedEntityKey (person.Gender).Value;

				var proxy = new PrivateObject (typeof (KeyedReferenceFieldProxy), dataContext, person, fieldId, targetKey);

				Assert.AreSame (dataContext, proxy.GetProperty ("DataContext"));
				Assert.AreSame (person, proxy.GetProperty ("Entity"));
				Assert.AreEqual (fieldId, proxy.GetProperty ("FieldId"));
			}
		}


		[TestMethod]
		public void KeyedReferenceFieldProxyConstructorArgumentCheck()
		{
			using (DB db = DB.ConnectToTestDatabase ())
			{
				using (DataContext dataContext1 = DataContextHelper.ConnectToTestDatabase (db.DataInfrastructure))
				using (DataContext dataContext2 = DataContextHelper.ConnectToTestDatabase (db.DataInfrastructure))
				{
					NaturalPersonEntity person = dataContext1.ResolveEntity<NaturalPersonEntity> (new DbKey (new DbId (1000000001)));
					Druid fieldId = Druid.Parse ("[J1AN1]");
					EntityKey targetKey = dataContext1.GetNormalizedEntityKey (person.Gender).Value;

					ExceptionAssert.Throw<System.ArgumentNullException>
					(
						() => new KeyedReferenceFieldProxy (null, person, fieldId, targetKey)
					);

					ExceptionAssert.Throw<System.ArgumentNullException>
					(
						() => new KeyedReferenceFieldProxy (dataContext1, null, fieldId, targetKey)
					);

					ExceptionAssert.Throw<System.ArgumentException>
					(
						() => new KeyedReferenceFieldProxy (dataContext1, person, Druid.Empty, targetKey)
					);

					ExceptionAssert.Throw<System.ArgumentException>
					(
						() => new KeyedReferenceFieldProxy (dataContext1, person, Druid.Parse ("[J1AJ1]"), targetKey)
					);

					ExceptionAssert.Throw<System.ArgumentException>
					(
						() => new KeyedReferenceFieldProxy (dataContext1, person, Druid.Parse ("[J1AC1]"), targetKey)
					);

					ExceptionAssert.Throw<System.ArgumentException>
					(
						() => new KeyedReferenceFieldProxy (dataContext2, person, fieldId, targetKey)
					);

					ExceptionAssert.Throw<System.ArgumentException>
					(
						() => new KeyedReferenceFieldProxy (dataContext2, person, fieldId, EntityKey.Empty)
					);
				}
			}
		}


		[TestMethod]
		public void DiscardWriteEntityValueTest()
		{
			using (DB db = DB.ConnectToTestDatabase ())
			using (DataContext dataContext = DataContextHelper.ConnectToTestDatabase (db.DataInfrastructure))
			{
				NaturalPersonEntity person = dataContext.ResolveEntity<NaturalPersonEntity> (new DbKey (new DbId (1000000001)));
				Druid fieldId = Druid.Parse ("[J1AN1]");
				EntityKey targetKey = dataContext.GetNormalizedEntityKey (person.Gender).Value;

				var proxy = new KeyedReferenceFieldProxy (dataContext, person, fieldId, targetKey);

				object obj = new object ();

				Assert.IsFalse (proxy.DiscardWriteEntityValue (new TestStore (), "J1AN1", ref obj));
			}
		}


		[TestMethod]
		public void GetReadEntityValueTest()
		{
			using (DB db = DB.ConnectToTestDatabase ())
			using (DataContext dataContext = DataContextHelper.ConnectToTestDatabase (db.DataInfrastructure))
			{
				NaturalPersonEntity person = dataContext.ResolveEntity<NaturalPersonEntity> (new DbKey (new DbId (1000000001)));
				PersonGenderEntity gender1 = dataContext.ResolveEntity<PersonGenderEntity> (new DbKey (new DbId (1000000001)));

				Druid fieldId = Druid.Parse ("[J1AN1]");
				EntityKey targetKey = dataContext.GetNormalizedEntityKey (person.Gender).Value;

				var proxy = new KeyedReferenceFieldProxy (dataContext, person, fieldId, targetKey);

				TestStore testStore = new TestStore ();

				object gender2 = proxy.GetReadEntityValue (testStore, "J1AN1");
				object gender3 = testStore.GetValue ("J1AN1");

				Assert.AreSame (gender1, gender2);
				Assert.AreSame (gender1, gender3);
			}
		}


		[TestMethod]
		public void GetWriteEntityValueTest()
		{
			using (DB db = DB.ConnectToTestDatabase ())
			using (DataContext dataContext = DataContextHelper.ConnectToTestDatabase (db.DataInfrastructure))
			{
				NaturalPersonEntity person = dataContext.ResolveEntity<NaturalPersonEntity> (new DbKey (new DbId (1000000001)));
				Druid fieldId = Druid.Parse ("[J1AN1]");
				EntityKey targetKey = dataContext.GetNormalizedEntityKey (person.Gender).Value;

				var proxy = new KeyedReferenceFieldProxy (dataContext, person, fieldId, targetKey);
				object gender = proxy.GetWriteEntityValue (new TestStore (), "J1AN1");

				Assert.AreSame (proxy, gender);
			}
		}


		[TestMethod]
		public void PromoteToRealInstanceTest1()
		{
			using (DB db = DB.ConnectToTestDatabase ())
			using (DataContext dataContext = DataContextHelper.ConnectToTestDatabase (db.DataInfrastructure))
			{
				NaturalPersonEntity person = dataContext.ResolveEntity<NaturalPersonEntity> (new DbKey (new DbId (1000000001)));
				PersonGenderEntity gender1 = dataContext.ResolveEntity<PersonGenderEntity> (new DbKey (new DbId (1000000001)));

				Druid fieldId = Druid.Parse ("[J1AN1]");
				EntityKey targetKey = dataContext.GetNormalizedEntityKey (person.Gender).Value;

				var proxy = new KeyedReferenceFieldProxy (dataContext, person, fieldId, targetKey);

				object gender2 = proxy.PromoteToRealInstance ();

				Assert.AreSame (gender1, gender2);
			}
		}


		[TestMethod]
		public void PromoteToRealInstanceTest2()
		{
			using (DB db = DB.ConnectToTestDatabase ())
			using (DataContext dataContext = DataContextHelper.ConnectToTestDatabase (db.DataInfrastructure))
			{
				NaturalPersonEntity person = dataContext.ResolveEntity<NaturalPersonEntity> (new DbKey (new DbId (1000000001)));

				Druid fieldId = Druid.Parse ("[J1AN1]");
				EntityKey targetKey = new EntityKey (Druid.Parse ("[J1AQ]"), new DbKey (new DbId (1000000001)));

				var proxy = new KeyedReferenceFieldProxy (dataContext, person, fieldId, targetKey);

				object gender2 = proxy.PromoteToRealInstance ();
				PersonGenderEntity gender1 = dataContext.ResolveEntity<PersonGenderEntity> (new DbKey (new DbId (1000000001)));

				Assert.AreSame (gender1, gender2);
			}
		}


		[TestMethod]
		public void PromoteToRealInstanceTest3()
		{
			using (DB db = DB.ConnectToTestDatabase ())
			using (DataContext dataContext = DataContextHelper.ConnectToTestDatabase (db.DataInfrastructure))
			{
				NaturalPersonEntity person = dataContext.ResolveEntity<NaturalPersonEntity> (new DbKey (new DbId (1000000001)));

				Druid fieldId = Druid.Parse ("[J1AN1]");
				EntityKey targetKey = new EntityKey (Druid.Parse ("[J1AQ]"), new DbKey (new DbId (1000000003)));

				var proxy = new KeyedReferenceFieldProxy (dataContext, person, fieldId, targetKey);

				object gender2 = proxy.PromoteToRealInstance ();
				PersonGenderEntity gender1 = dataContext.ResolveEntity<PersonGenderEntity> (new DbKey (new DbId (1000000001)));

				Assert.AreSame (gender1, gender2);
			}
		}


		[TestMethod]
		public void PromoteToRealInstanceTest4()
		{
			using (DB db = DB.ConnectToTestDatabase ())
			using (DataContext dataContext = DataContextHelper.ConnectToTestDatabase (db.DataInfrastructure))
			{
				NaturalPersonEntity person = dataContext.ResolveEntity<NaturalPersonEntity> (new DbKey (new DbId (1000000003)));

				Druid fieldId = Druid.Parse ("[J1AN1]");
				EntityKey targetKey = new EntityKey (Druid.Parse ("[J1AQ]"), new DbKey (new DbId (1000000003)));

				var proxy = new KeyedReferenceFieldProxy (dataContext, person, fieldId, targetKey);

				object gender = proxy.PromoteToRealInstance ();

				Assert.AreSame (UndefinedValue.Value, gender);
			}
		}


		[TestMethod]
		public void PromoteToRealInstanceTest5()
		{
			using (DB db1 = DB.ConnectToTestDatabase ())
			using (DB db2 = DB.ConnectToTestDatabase ())
			using (DataContext dataContext1 = DataContextHelper.ConnectToTestDatabase (db1.DataInfrastructure))
			using (DataContext dataContext2 = DataContextHelper.ConnectToTestDatabase (db2.DataInfrastructure))
			{
				NaturalPersonEntity person1 = dataContext1.ResolveEntity<NaturalPersonEntity> (new DbKey (new DbId (1000000001)));
				NaturalPersonEntity person2 = dataContext2.ResolveEntity<NaturalPersonEntity> (new DbKey (new DbId (1000000001)));

				dataContext1.DeleteEntity (person1);
				dataContext1.SaveChanges ();

				Druid fieldId = Druid.Parse ("[J1AN1]");
				EntityKey targetKey = new EntityKey (Druid.Parse ("[J1AQ]"), new DbKey (new DbId (1000000001)));

				var proxy = new KeyedReferenceFieldProxy (dataContext2, person2, fieldId, targetKey);

				object gender = proxy.PromoteToRealInstance ();

				Assert.AreSame (UndefinedValue.Value, gender);
			}
		}


	}


}

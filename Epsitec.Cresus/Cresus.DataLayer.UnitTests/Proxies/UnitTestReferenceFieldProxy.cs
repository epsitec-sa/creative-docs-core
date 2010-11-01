﻿using Epsitec.Common.Support;
using Epsitec.Common.Types;

using Epsitec.Common.UnitTesting;

using Epsitec.Cresus.Database;

using Epsitec.Cresus.DataLayer.Context;
using Epsitec.Cresus.DataLayer.Infrastructure;
using Epsitec.Cresus.DataLayer.Proxies;
using Epsitec.Cresus.DataLayer.UnitTests.Entities;
using Epsitec.Cresus.DataLayer.UnitTests.Helpers;

using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace Epsitec.Cresus.DataLayer.UnitTests.Proxies
{


	[TestClass]
	public sealed class UnitTestReferenceFieldProxy
	{


		[ClassInitialize]
		public static void ClassInitialize(TestContext testContext)
		{
			TestHelper.Initialize ();

			DatabaseHelper.CreateAndConnectToDatabase ();

			using (DataInfrastructure dataInfrastructure = new DataInfrastructure (DatabaseHelper.DbInfrastructure))
			{
				using (DataContext dataContext = dataInfrastructure.CreateDataContext ())
				{
					DatabaseCreator2.PupulateDatabase (dataContext);
				}
			}
		}


		[ClassCleanup]
		public static void ClassCleanup()
		{
			DatabaseHelper.DisconnectFromDatabase ();
		}


		[TestMethod]
		public void ReferenceFieldProxyConstructorTest()
		{
			using (DataInfrastructure dataInfrastructure = new DataInfrastructure (DatabaseHelper.DbInfrastructure))
			{
				using (DataContext dataContext = dataInfrastructure.CreateDataContext ())
				{
					NaturalPersonEntity person = dataContext.ResolveEntity<NaturalPersonEntity> (new DbKey (new DbId (1000000001)));
					Druid fieldId = Druid.Parse ("[L0A11]");

					var proxy = new ReferenceFieldProxy_Accessor (dataContext, person, fieldId);

					Assert.AreSame (dataContext, proxy.DataContext);
					Assert.AreSame (person, proxy.Entity);
					Assert.AreEqual (fieldId, proxy.FieldId);
				}
			}
		}


		[TestMethod]
		public void ReferenceFieldProxyConstructorArgumentCheck()
		{
			using (DataInfrastructure dataInfrastructure = new DataInfrastructure (DatabaseHelper.DbInfrastructure))
			{
				using (DataContext dataContext1 = dataInfrastructure.CreateDataContext ())
				using (DataContext dataContext2 = dataInfrastructure.CreateDataContext ())
				{
					NaturalPersonEntity person = dataContext1.ResolveEntity<NaturalPersonEntity> (new DbKey (new DbId (1000000001)));
					Druid fieldId = Druid.Parse ("[L0A11]");

					ExceptionAssert.Throw<System.ArgumentNullException>
					(
						() => new ReferenceFieldProxy (null, person, fieldId)
					);

					ExceptionAssert.Throw<System.ArgumentNullException>
					(
						() => new ReferenceFieldProxy (dataContext1, null, fieldId)
					);

					ExceptionAssert.Throw<System.ArgumentException>
					(
						() => new ReferenceFieldProxy (dataContext1, person, Druid.Empty)
					);

					ExceptionAssert.Throw<System.ArgumentException>
					(
						() => new ReferenceFieldProxy (dataContext1, person, Druid.Parse ("[L0AN]"))
					);

					ExceptionAssert.Throw<System.ArgumentException>
					(
						() => new ReferenceFieldProxy (dataContext1, person, Druid.Parse ("[L0AS]"))
					);

					ExceptionAssert.Throw<System.ArgumentException>
					(
						() => new ReferenceFieldProxy (dataContext2, person, fieldId)
					);
				}
			}
		}


		[TestMethod]
		public void DiscardWriteEntityValueTest()
		{
			using (DataInfrastructure dataInfrastructure = new DataInfrastructure (DatabaseHelper.DbInfrastructure))
			{
				using (DataContext dataContext = dataInfrastructure.CreateDataContext ())
				{
					NaturalPersonEntity person = dataContext.ResolveEntity<NaturalPersonEntity> (new DbKey (new DbId (1000000001)));
					Druid fieldId = Druid.Parse ("[L0A11]");

					var proxy = new ReferenceFieldProxy (dataContext, person, fieldId);

					object obj = new object ();

					Assert.IsFalse (proxy.DiscardWriteEntityValue (new TestStore (), "L0A11", ref obj));
				}
			}
		}

		[TestMethod ]
		public void GetReadEntityValueTest()
		{
			using (DataInfrastructure dataInfrastructure = new DataInfrastructure (DatabaseHelper.DbInfrastructure))
			{
				using (DataContext dataContext = dataInfrastructure.CreateDataContext ())
				{
					NaturalPersonEntity person = dataContext.ResolveEntity<NaturalPersonEntity> (new DbKey (new DbId (1000000001)));
					PersonGenderEntity gender1 = dataContext.ResolveEntity<PersonGenderEntity> (new DbKey (new DbId (1000000001)));

					Druid fieldId = Druid.Parse ("[L0A11]");

					var proxy = new ReferenceFieldProxy (dataContext, person, fieldId);

					TestStore testStore = new TestStore ();

					object gender2 = proxy.GetReadEntityValue (testStore, "L0A11");
					object gender3 = testStore.GetValue ("L0A11");

					Assert.AreSame (gender1, gender2);
					Assert.AreSame (gender1, gender3);
				}
			}
		}


		[TestMethod ]
		public void GetWriteEntityValueTest()
		{
			using (DataInfrastructure dataInfrastructure = new DataInfrastructure (DatabaseHelper.DbInfrastructure))
			{
				using (DataContext dataContext = dataInfrastructure.CreateDataContext ())
				{
					NaturalPersonEntity person = dataContext.ResolveEntity<NaturalPersonEntity> (new DbKey (new DbId (1000000001)));
					Druid fieldId = Druid.Parse ("[L0A11]");

					var proxy = new ReferenceFieldProxy (dataContext, person, fieldId);
					object gender = proxy.GetWriteEntityValue (new TestStore (), "L0A11");


					Assert.AreSame (proxy, gender);
				}
			}
		}


		[TestMethod]
		public void PromoteToRealInstanceTest1()
		{
			using (DataInfrastructure dataInfrastructure = new DataInfrastructure (DatabaseHelper.DbInfrastructure))
			{
				using (DataContext dataContext = dataInfrastructure.CreateDataContext ())
				{
					NaturalPersonEntity person = dataContext.ResolveEntity<NaturalPersonEntity> (new DbKey (new DbId (1000000001)));
					PersonGenderEntity gender1 = dataContext.ResolveEntity<PersonGenderEntity> (new DbKey (new DbId (1000000001)));

					Druid fieldId = Druid.Parse ("[L0A11]");

					var proxy = new ReferenceFieldProxy (dataContext, person, fieldId);

					object gender2 = proxy.PromoteToRealInstance ();

					Assert.AreSame (gender1, gender2);
				}
			}
		}


		[TestMethod]
		public void PromoteToRealInstanceTest2()
		{
			using (DataInfrastructure dataInfrastructure = new DataInfrastructure (DatabaseHelper.DbInfrastructure))
			{
				using (DataContext dataContext = dataInfrastructure.CreateDataContext ())
				{
					NaturalPersonEntity person = dataContext.ResolveEntity<NaturalPersonEntity> (new DbKey (new DbId (1000000003)));

					Druid fieldId = Druid.Parse ("[L0A11]");

					var proxy = new ReferenceFieldProxy (dataContext, person, fieldId);

					object gender = proxy.PromoteToRealInstance ();

					Assert.AreSame (UndefinedValue.Value, gender);
				}
			}
		}


	}


}

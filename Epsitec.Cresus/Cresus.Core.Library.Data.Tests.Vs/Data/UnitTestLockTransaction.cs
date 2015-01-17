﻿using Epsitec.Common.Support.Extensions;

using Epsitec.Cresus.Core.Data;
using Epsitec.Cresus.Core.Library.Data.Tests.Vs.Helpers;

using Epsitec.Cresus.Database;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.Collections.Generic;

using System.Linq;
using Epsitec.Common.Support;


namespace Epsitec.Cresus.Core.Library.Data.Tests.Vs.Data
{


	[TestClass]
	public class UnitTestLockTransaction
	{


		[ClassInitialize]
		public static void Initialize(TestContext testContext)
		{
			TestHelper.Initialize ();
		}


		[TestInitialize]
		public void TestInitialize()
		{
			DatabaseHelper.ResetTestDatabase ();
		}


		[TestMethod]
		public void SimpleTest1()
		{

			using (var dbInfrastructure = DatabaseHelper.CreateDbInfrastructure ())
			using (var dataInfrastructure = DatabaseHelper.CreateDataInfrastructure (dbInfrastructure))
			{
				dataInfrastructure.OpenConnection ("id");

				using (LockTransaction lt = new LockTransaction (dataInfrastructure, new List<string> { "lock", }))
				{
					Assert.IsTrue (lt.Poll ());
					Assert.IsTrue (lt.Acquire ());
				}
			}
		}


		[TestMethod]
		public void SimpleTest2()
		{
			using (var dbInfrastructure1 = DatabaseHelper.CreateDbInfrastructure ())
			using (var dbInfrastructure2 = DatabaseHelper.CreateDbInfrastructure ())
			using (var dataInfrastructure1 = DatabaseHelper.CreateDataInfrastructure (dbInfrastructure1))
			using (var dataInfrastructure2 = DatabaseHelper.CreateDataInfrastructure (dbInfrastructure2))
			{
				string cId1 = new ConnectionUserIdentity (new ItemCode ("id1")).ToString ();
				string cId2 = new ConnectionUserIdentity (new ItemCode ("id2")).ToString ();

				dataInfrastructure1.OpenConnection (cId1);
				dataInfrastructure2.OpenConnection (cId2);

				using (LockTransaction lt1 = new LockTransaction (dataInfrastructure1, new List<string> { "lock1", "lock2" }))
				using (LockTransaction lt2 = new LockTransaction (dataInfrastructure1, new List<string> { "lock2" }))
				using (LockTransaction lt3 = new LockTransaction (dataInfrastructure2, new List<string> { "lock2", "lock3" }))
				{
					Assert.IsTrue (lt1.Poll ());
					Assert.IsTrue (lt2.Poll ());
					Assert.IsTrue (lt3.Poll ());

					Assert.IsTrue (lt1.Acquire ());

					Assert.IsTrue (lt1.Poll ());
					Assert.IsTrue (lt2.Poll ());
					Assert.IsFalse (lt3.Poll ());

					Assert.IsTrue (lt2.Acquire ());
					Assert.IsFalse (lt3.Acquire ());

					Assert.IsTrue (lt1.Poll ());
					Assert.IsTrue (lt2.Poll ());
					Assert.IsFalse (lt3.Poll ());

					lt1.Dispose ();

					Assert.IsFalse (lt3.Acquire ());

					Assert.IsTrue (lt2.Poll ());
					Assert.IsFalse (lt3.Poll ());

					lt2.Dispose ();

					Assert.IsTrue (lt3.Poll ());
					Assert.IsTrue (lt3.Acquire ());

					Assert.IsTrue (lt3.Poll ());

					lt3.Dispose ();
				}
			}
		}


		[TestMethod]
		public void GetLockOwnersAndCreationTimeTest()
		{
			using (var dbInfrastructure1 = DatabaseHelper.CreateDbInfrastructure())
			using (var dbInfrastructure2 = DatabaseHelper.CreateDbInfrastructure ())
			using (var dbInfrastructure3 = DatabaseHelper.CreateDbInfrastructure ())
			using (var dbInfrastructure4 = DatabaseHelper.CreateDbInfrastructure ())
			using (var dataInfrastructure1 = DatabaseHelper.CreateDataInfrastructure (dbInfrastructure1))
			using (var dataInfrastructure2 = DatabaseHelper.CreateDataInfrastructure (dbInfrastructure2))
			using (var dataInfrastructure3 = DatabaseHelper.CreateDataInfrastructure (dbInfrastructure3))
			using (var dataInfrastructure4 = DatabaseHelper.CreateDataInfrastructure (dbInfrastructure4))
			{
				string cId1 = new ConnectionUserIdentity (new ItemCode ("id1")).ToString ();
				string cId2 = new ConnectionUserIdentity (new ItemCode ("id2")).ToString ();
				string cId3 = new ConnectionUserIdentity (new ItemCode ("id3")).ToString ();
				string cId4 = new ConnectionUserIdentity (new ItemCode ("id4")).ToString ();

				dataInfrastructure1.OpenConnection (cId1);
				dataInfrastructure2.OpenConnection (cId2);
				dataInfrastructure3.OpenConnection (cId3);
				dataInfrastructure4.OpenConnection (cId4);

				using (LockTransaction lt1 = new LockTransaction (dataInfrastructure1, new List<string> { "lock1", "lock2" }))
				using (LockTransaction lt2 = new LockTransaction (dataInfrastructure2, new List<string> { "lock3" }))
				using (LockTransaction lt3 = new LockTransaction (dataInfrastructure3, new List<string> { "lock4" }))
				using (LockTransaction lt4 = new LockTransaction (dataInfrastructure4, new List<string> { "lock1", "lock2", "lock3" }))
				{
					Assert.IsTrue (lt1.Acquire ());
					Assert.IsTrue (lt2.Acquire ());
					Assert.IsTrue (lt3.Acquire ());
					Assert.IsFalse (lt4.Acquire ());

					Assert.IsFalse (lt1.ForeignLockOwners.Any ());
					Assert.IsFalse (lt2.ForeignLockOwners.Any ());
					Assert.IsFalse (lt3.ForeignLockOwners.Any ());

					var expectedLockOwners4 = new Dictionary<string, string>
		            {
		                { "lock1", cId1 },
		                { "lock2", cId1 },
		                { "lock3", cId2 },
		            };

					this.CheckLockOwner (expectedLockOwners4, lt4.ForeignLockOwners);
				}
			}
		}


		private void CheckLockOwner(Dictionary<string, string> expected, IEnumerable<LockOwner> actual)
		{
			var set1 = expected.Select (i => System.Tuple.Create (i.Key, i.Value)).ToList ();
			var set2 = actual.Select (i => System.Tuple.Create (i.LockName, i.User.ToString ())).ToList ();

			Assert.IsTrue (set1.SetEquals(set2));
		}


	}


}

using NUnit.Framework;

using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters.Soap;

namespace Epsitec.Cresus.Database
{
	[TestFixture]
	public class RequestsTest
	{
		[TestFixtureSetUp] public void Setup()
		{
		}
		
		[TestFixtureTearDown] public void TearDown()
		{
		}
		
		
		[Test] public void Check01Group()
		{
			Requests.Group group = new Requests.Group ();
			
			Assert.AreEqual (0, group.Count);
			Assert.AreEqual (Requests.RequestType.Group, group.RequestType);
			
			group.AddRange (null);
			group.AddRange (new object[] { });
			
			Assert.AreEqual (0, group.Count);
			
			Requests.AbstractRequest req = new Requests.Group ();
			
			group.Add (req);
			
			Assert.AreEqual (1, group.Count);
			Assert.AreEqual (req, group[0]);
		}
		
		[Test] [ExpectedException (typeof (System.ArgumentNullException))] public void Check02GroupEx()
		{
			Requests.Group group = new Requests.Group ();
			
			Assert.AreEqual (0, group.Count);
			
			group.Add (null);
		}
		
		[Test] [ExpectedException (typeof (System.IndexOutOfRangeException))] public void Check03GroupEx()
		{
			Requests.Group group = new Requests.Group ();
			
			Assert.AreEqual (0, group.Count);
			
			Requests.AbstractRequest req = group[0];
		}
		
		[Test] public void Check04Types()
		{
			Requests.AbstractRequest req1 = new Requests.Group ();
			Requests.AbstractRequest req2 = new Requests.InsertStaticData ();
			Requests.AbstractRequest req3 = new Requests.UpdateStaticData ();
			Requests.AbstractRequest req4 = new Requests.UpdateDynamicData ();
			
			Assert.AreEqual (Requests.RequestType.Group, req1.RequestType);
			Assert.AreEqual (Requests.RequestType.InsertStaticData, req2.RequestType);
			Assert.AreEqual (Requests.RequestType.UpdateStaticData, req3.RequestType);
			Assert.AreEqual (Requests.RequestType.UpdateDynamicData, req4.RequestType);
		}
		
		[Test] public void Check05Serialization()
		{
			try
			{
				System.IO.File.Delete ("test-requests.bin");
			}
			catch {}
			
			using (System.IO.Stream stream = System.IO.File.Open ("test-requests.bin", System.IO.FileMode.Create))
			{
				BinaryFormatter formatter = new BinaryFormatter ();
				
				System.Data.DataTable table = RequestsTest.CreateSampleTable ();
				
				Requests.Group group = new Requests.Group ();
				
				Requests.InsertStaticData req_1 = new Requests.InsertStaticData (table.Rows[0]);
				Requests.InsertStaticData req_2 = new Requests.InsertStaticData (table.Rows[1]);
				
				table.Rows[0].BeginEdit ();
				table.Rows[0][1] = "Pierre Arnaud-B�hlmann";
				table.Rows[0].EndEdit ();
				
				Requests.UpdateStaticData req_3 = new Requests.UpdateStaticData (table.Rows[0], Requests.UpdateMode.Changed);
				
				group.Add (req_1);
				group.Add (req_2);
				group.Add (req_3);
				
				Assert.AreEqual (2, req_3.ColumnNames.Length);
				Assert.AreEqual ("ID", req_3.ColumnNames[0]);
				Assert.AreEqual ("Name", req_3.ColumnNames[1]);
				
				formatter.Serialize (stream, group);
			}
			
			using (System.IO.Stream stream = System.IO.File.Open ("test-requests.bin", System.IO.FileMode.Open))
			{
				BinaryFormatter formatter = new BinaryFormatter ();
				Requests.Group group = formatter.Deserialize (stream) as Requests.Group;
				
				Assert.AreEqual (3, group.Count);
				
				Assert.AreEqual (Requests.RequestType.InsertStaticData, group[0].RequestType);
				Assert.AreEqual (Requests.RequestType.InsertStaticData, group[1].RequestType);
				Assert.AreEqual (Requests.RequestType.UpdateStaticData, group[2].RequestType);
				
				Requests.InsertStaticData req_1 = group[0] as Requests.InsertStaticData;
				Requests.InsertStaticData req_2 = group[1] as Requests.InsertStaticData;
				Requests.UpdateStaticData req_3 = group[2] as Requests.UpdateStaticData;
				
				Assert.AreEqual ("DemoTable", req_1.TableName);
				Assert.AreEqual ("DemoTable", req_2.TableName);
				
				Assert.AreEqual (1L, req_1.ColumnValues[0]);
				Assert.AreEqual (2L, req_2.ColumnValues[0]);
				
				Assert.AreEqual ("Pierre Arnaud", req_1.ColumnValues[1]);
				Assert.AreEqual ("J�r�me Andr�",  req_2.ColumnValues[1]);
				
				Assert.AreEqual (1972, req_1.ColumnValues[2]);
				Assert.AreEqual (1994, req_2.ColumnValues[2]);
				
				Assert.AreEqual (2, req_3.ColumnNames.Length);
				Assert.AreEqual ("ID", req_3.ColumnNames[0]);
				Assert.AreEqual ("Name", req_3.ColumnNames[1]);
				Assert.AreEqual (1L, req_3.ColumnValues[0]);
				Assert.AreEqual ("Pierre Arnaud-B�hlmann", req_3.ColumnValues[1]);
			}
		}
		
		[Test] public void Check06UpdateStaticData()
		{
			System.Data.DataTable table = RequestsTest.CreateSampleTable ();
			
			Requests.UpdateStaticData req_1 = new Requests.UpdateStaticData (table.Rows[0], Requests.UpdateMode.Changed);
			
			table.Rows[0].BeginEdit ();
			table.Rows[0][1] = "Pierre Arnaud-B�hlmann";
			table.Rows[0].EndEdit ();
			
			Requests.UpdateStaticData req_2 = new Requests.UpdateStaticData (table.Rows[0], Requests.UpdateMode.Changed);
			Requests.UpdateStaticData req_3 = new Requests.UpdateStaticData (table.Rows[1], Requests.UpdateMode.Full);
			
			Assert.IsTrue (req_1.ContainsData == false);
			Assert.IsTrue (req_2.ContainsData == true);
			Assert.IsTrue (req_3.ContainsData == true);
			
			Assert.AreEqual (2, req_2.ColumnNames.Length);
			Assert.AreEqual ("ID", req_2.ColumnNames[0]);
			Assert.AreEqual ("Name", req_2.ColumnNames[1]);
			Assert.AreEqual (1L, req_2.ColumnValues[0]);
			Assert.AreEqual ("Pierre Arnaud-B�hlmann", req_2.ColumnValues[1]);
			Assert.AreEqual ("Pierre Arnaud", req_2.ColumnOriginalValues[1]);
			
			Assert.AreEqual (3, req_3.ColumnNames.Length);
			Assert.AreEqual ("ID", req_3.ColumnNames[0]);
			Assert.AreEqual ("Name", req_3.ColumnNames[1]);
			Assert.AreEqual ("Birth Year", req_3.ColumnNames[2]);
			Assert.AreEqual (2L, req_3.ColumnValues[0]);
			Assert.AreEqual ("J�r�me Andr�", req_3.ColumnValues[1]);
			Assert.AreEqual (1994, req_3.ColumnValues[2]);
		}
		
		[Test] public void Check07ExecutionQueue()
		{
			using (DbInfrastructure infrastructure = DbInfrastructureTest.GetInfrastructureFromBase ("fiche", true))
			{
				Assert.IsNotNull (infrastructure);
				
				Requests.ExecutionQueue queue = new Requests.ExecutionQueue (infrastructure, null);
				
				System.Data.DataRow[] rows;
				
				using (DbTransaction transaction = infrastructure.BeginTransaction (DbTransactionMode.ReadWrite))
				{
					System.Data.DataTable table = RequestsTest.CreateSampleTable ();
					
					Requests.Group group = new Requests.Group ();
					
					Requests.InsertStaticData req_1 = new Requests.InsertStaticData (table.Rows[0]);
					Requests.InsertStaticData req_2 = new Requests.InsertStaticData (table.Rows[1]);
					
					table.Rows[0].BeginEdit ();
					table.Rows[0][1] = "Pierre Arnaud-B�hlmann";
					table.Rows[0].EndEdit ();
					
					Requests.UpdateStaticData req_3 = new Requests.UpdateStaticData (table.Rows[0], Requests.UpdateMode.Changed);
					
					group.Add (req_1);
					group.Add (req_2);
					group.Add (req_3);
					
					rows = queue.Rows;
					
					int n = rows.Length;
					
					queue.Enqueue (group);
					
					Assert.AreEqual (n, rows.Length);
					
					rows = queue.Rows;
					
					System.Data.DataRow row = rows[n];
					
					Assert.AreEqual (n+1, rows.Length);
					Assert.AreEqual (DbIdClass.Temporary, DbId.AnalyzeClass ((long) row[Tags.ColumnId]));
					Assert.AreEqual (Requests.ExecutionState.Pending, queue.GetRequestExecutionState (row));
					
					queue.SerializeToBase (transaction);
					
					Assert.AreEqual (DbIdClass.Standard, DbId.AnalyzeClass ((long) row[Tags.ColumnId]));
					
					transaction.Commit ();
				}
				
				queue.Detach ();
				
				queue = new Requests.ExecutionQueue (infrastructure, null);
				rows  = queue.Rows;
				
				foreach (System.Data.DataRow row in rows)
				{
					System.Diagnostics.Debug.WriteLine ("Row " + row[0] + " contains " + ((byte[])row[Tags.ColumnReqData]).Length + " bytes, state = " + queue.GetRequestExecutionState (row));
				}
				
				using (DbTransaction transaction = infrastructure.BeginTransaction (DbTransactionMode.ReadWrite))
				{
					queue.ClearQueue ();
					queue.SerializeToBase (transaction);
					transaction.Commit ();
				}
			}
		}
		
		[Test] public void Check08ExecutionEngine()
		{
			DbInfrastructure infrastructure = DbInfrastructureTest.GetInfrastructureFromBase ("fiche", false);
			Requests.ExecutionEngine engine = new Epsitec.Cresus.Requests.ExecutionEngine (infrastructure);
			
			Assert.AreEqual (0L, engine.CurrentLogId.Value);
			Assert.AreEqual (null, engine.CurrentTransaction);
			
			infrastructure.Logger.CreateTemporaryEntry (null);
			
			DbType db_type_name = infrastructure.ResolveDbType (null, "Customer Name");
			DbType db_type_date = infrastructure.ResolveDbType (null, "Birth Date");
			
			if (db_type_name == null)
			{
				db_type_name = infrastructure.CreateDbType ("Customer Name", 80, false);
				infrastructure.RegisterNewDbType (null, db_type_name);
			}
			
			if (db_type_date == null)
			{
				db_type_date = infrastructure.CreateDbTypeDateTime ("Birth Date");
				infrastructure.RegisterNewDbType (null, db_type_date);
			}
			
			DbTable db_table = infrastructure.CreateDbTable ("Simple Exec Table Test", DbElementCat.UserDataManaged, DbRevisionMode.Disabled);
			
			DbColumn db_col_1 = DbColumn.CreateUserDataColumn ("Name",       db_type_name, Nullable.No);
			DbColumn db_col_2 = DbColumn.CreateUserDataColumn ("Birth Date", db_type_date, Nullable.Yes);
			
			db_table.Columns.AddRange (new DbColumn[] { db_col_1, db_col_2 });
			
			infrastructure.RegisterNewDbTable (null, db_table);
			
			DbColumn db_col_id   = db_table.Columns[0];
			DbColumn db_col_stat = db_table.Columns[1];
			
			System.Data.DataSet   set   = new System.Data.DataSet ();
			System.Data.DataTable table = new System.Data.DataTable (db_table.Name);
			
			set.Tables.Add (table);
			
			System.Data.DataColumn col_1 = new System.Data.DataColumn (db_col_id.Name, typeof (long));
			System.Data.DataColumn col_2 = new System.Data.DataColumn (db_col_stat.Name, typeof (int));
			System.Data.DataColumn col_3 = new System.Data.DataColumn (db_col_1.Name, typeof (string));
			System.Data.DataColumn col_4 = new System.Data.DataColumn (db_col_2.Name, typeof (System.DateTime));
			
			col_1.Unique = true;
			
			table.Columns.Add (col_1);
			table.Columns.Add (col_2);
			table.Columns.Add (col_3);
			table.Columns.Add (col_4);
			
			DataLayer.RequestFactory factory = new DataLayer.RequestFactory ();
			
			table.Rows.Add (new object[] { DbId.CreateId (1, 1).Value, 0, "Pierre Arnaud", new System.DateTime (1972, 2, 11) });
			
			factory.GenerateRequests (table);
			
			DbTransaction transaction = infrastructure.BeginTransaction (DbTransactionMode.ReadWrite);
			
			engine.Execute (transaction, factory.CreateGroup ());
			
			table.AcceptChanges ();
			table.Rows[0][col_3.ColumnName] = "Pierre Arnaud-Roost";
			table.Rows[0][col_4.ColumnName] = new System.DateTime (1940, 5, 20);
			
			factory.Clear ();
			factory.GenerateRequests (table);
			
			engine.Execute (transaction, factory.CreateGroup ());
			
			transaction.Commit ();
			
			infrastructure.UnregisterDbTable (null, db_table);
		}
		
		[Test] public void Check09Orchestrator()
		{
			DbInfrastructure infrastructure = DbInfrastructureTest.GetInfrastructureFromBase ("fiche", false);
			Requests.Orchestrator orchestrator = new Requests.Orchestrator (infrastructure);
			
			infrastructure.Logger.CreateTemporaryEntry (null);
			
			DbType db_type_name = infrastructure.ResolveDbType (null, "Customer Name");
			DbType db_type_date = infrastructure.ResolveDbType (null, "Birth Date");
			
			Assert.IsNotNull (db_type_name);
			Assert.IsNotNull (db_type_date);
			
			DbTable db_table = infrastructure.CreateDbTable ("Simple Exec Table Test", DbElementCat.UserDataManaged, DbRevisionMode.Disabled);
			
			DbColumn db_col_1 = DbColumn.CreateUserDataColumn ("Name",       db_type_name, Nullable.No);
			DbColumn db_col_2 = DbColumn.CreateUserDataColumn ("Birth Date", db_type_date, Nullable.Yes);
			
			db_table.Columns.AddRange (new DbColumn[] { db_col_1, db_col_2 });
			
			infrastructure.RegisterNewDbTable (null, db_table);
			
			DbColumn db_col_id   = db_table.Columns[0];
			DbColumn db_col_stat = db_table.Columns[1];
			
			System.Data.DataSet   set   = new System.Data.DataSet ();
			System.Data.DataTable table = new System.Data.DataTable (db_table.Name);
			
			set.Tables.Add (table);
			
			System.Data.DataColumn col_1 = new System.Data.DataColumn (db_col_id.Name, typeof (long));
			System.Data.DataColumn col_2 = new System.Data.DataColumn (db_col_stat.Name, typeof (int));
			System.Data.DataColumn col_3 = new System.Data.DataColumn (db_col_1.Name, typeof (string));
			System.Data.DataColumn col_4 = new System.Data.DataColumn (db_col_2.Name, typeof (System.DateTime));
			
			col_1.Unique = true;
			
			table.Columns.Add (col_1);
			table.Columns.Add (col_2);
			table.Columns.Add (col_3);
			table.Columns.Add (col_4);
			
			DataLayer.RequestFactory factory = new DataLayer.RequestFactory ();
			
			table.Rows.Add (new object[] { DbId.CreateId (1, 1).Value, 0, "Pierre Arnaud", new System.DateTime (1972, 2, 11) });
			
			factory.GenerateRequests (table);
			
			orchestrator.ExecutionQueue.Enqueue (factory.CreateGroup ());
			
			table.AcceptChanges ();
			table.Rows[0][col_3.ColumnName] = "Pierre Arnaud-Roost";
			table.Rows[0][col_4.ColumnName] = new System.DateTime (1940, 5, 20);
			
			factory.Clear ();
			factory.GenerateRequests (table);
			
			orchestrator.ExecutionQueue.Enqueue (factory.CreateGroup ());
			orchestrator.Dispose ();
			
			infrastructure.UnregisterDbTable (null, db_table);
		}

		[Test] public void Check10ExecutionQueueDump()
		{
			using (DbInfrastructure infrastructure = DbInfrastructureTest.GetInfrastructureFromBase ("fiche", true))
			{
				Assert.IsNotNull (infrastructure);
				
				Requests.ExecutionQueue queue = new Requests.ExecutionQueue (infrastructure, null);
				System.Data.DataRow[] rows;
				
				queue = new Requests.ExecutionQueue (infrastructure, null);
				rows  = queue.Rows;
				
				foreach (System.Data.DataRow row in rows)
				{
					System.Diagnostics.Debug.WriteLine ("Row " + row[0] + " contains " + ((byte[])row[Tags.ColumnReqData]).Length + " bytes, state = " + queue.GetRequestExecutionState (row));
				}
			}
		}

		[Test] public void Check11ExecutionQueueClearQueue()
		{
			using (DbInfrastructure infrastructure = DbInfrastructureTest.GetInfrastructureFromBase ("fiche", true))
			{
				Assert.IsNotNull (infrastructure);
				
				Requests.ExecutionQueue queue = new Requests.ExecutionQueue (infrastructure, null);
				System.Data.DataRow[] rows;
				
				queue = new Requests.ExecutionQueue (infrastructure, null);
				rows  = queue.Rows;
				
				foreach (System.Data.DataRow row in rows)
				{
					System.Diagnostics.Debug.WriteLine ("Row " + row[0] + " contains " + ((byte[])row[Tags.ColumnReqData]).Length + " bytes, state = " + queue.GetRequestExecutionState (row));
				}
				
				using (DbTransaction transaction = infrastructure.BeginTransaction (DbTransactionMode.ReadWrite))
				{
					queue.ClearQueue ();
					queue.SerializeToBase (transaction);
					transaction.Commit ();
				}
			}
		}

		
		[Test] /*[Ignore ("Temporary")]*/ public void Check12ServiceServer()
		{
			DbInfrastructure      infrastructure = DbInfrastructureTest.GetInfrastructureFromBase ("fiche", false);
			
			infrastructure.LocalSettings.IsServer = true;
			
			Requests.Orchestrator orchestrator   = new Requests.Orchestrator (infrastructure);
			Services.Engine       engine         = new Services.Engine (orchestrator, 1234);
			
			infrastructure.LocalSettings.IsServer = false;
			
			RequestsTest.CreateTestTable (infrastructure, "ServiceTest");
			
			System.Diagnostics.Debug.WriteLine ("Server ready. Running for 60 seconds.");
			
			System.Threading.Thread.Sleep (1000*60);
			
			RequestsTest.DeleteTestTable (infrastructure, "ServiceTest");
			
			System.Diagnostics.Debug.WriteLine ("Aborting server...");
			Common.Support.Globals.SignalAbort ();
		}
		
		
		[Test] /*[Ignore ("Temporary")]*/ public void Check13ConnectionClient()
		{
			Remoting.IConnectionService service = Services.Engine.GetRemoteConnectionService ("localhost", 1234);
			
			Assert.IsNotNull (service);
			
			string[] service_names;
			
			Remoting.ClientIdentity.DefineDefaultClientId (1);
			
			service.CheckConnectivity (new Remoting.ClientIdentity ("NUnit Test Client"));
			service.QueryAvailableServices (new Remoting.ClientIdentity ("NUnit Test Client"), out service_names);
			
			System.Diagnostics.Debug.WriteLine ("Found " + service_names.Length + " services:");
			foreach (string name in service_names)
			{
				System.Diagnostics.Debug.WriteLine ("-- " + name);
			}
		}
		
		[Test] /*[Ignore ("Temporary")]*/ public void Check14RequestExecutionClient()
		{
			DbInfrastructure infrastructure = DbInfrastructureTest.GetInfrastructureFromBase ("fiche", false);
			System.Data.DataTable table = RequestsTest.GetDataTableFromTable (infrastructure, "ServiceTest");
			
			Remoting.ClientIdentity.DefineDefaultClientId (1);
			
			Remoting.IRequestExecutionService service = Services.Engine.GetRemoteRequestExecutionService ("localhost", 1234);
			
			Assert.IsNotNull (service);
			
			DataLayer.RequestFactory factory = new DataLayer.RequestFactory ();
			
			System.Data.DataRow data_row;
			
			DbRichCommand.CreateRow (table, out data_row);
			
			data_row.BeginEdit ();
			data_row[0] = DbId.CreateId (1, 1).Value;
			data_row[3] = "Pierre Arnaud";
			data_row[4] = new System.DateTime (1972, 2, 11);
			data_row.EndEdit ();
			
			table.Rows.Add (data_row);
			
			factory.GenerateRequests (table);
			
			byte[] serialized_1 = Requests.AbstractRequest.SerializeToMemory (factory.CreateGroup ());
			
			table.AcceptChanges ();
			table.Rows[0][3] = "Pierre Arnaud-Roost";
			table.Rows[0][4] = new System.DateTime (1940, 5, 20);
			
			factory.Clear ();
			factory.GenerateRequests (table);
			
			byte[] serialized_2 = Requests.AbstractRequest.SerializeToMemory (factory.CreateGroup ());
			
			service.EnqueueRequest (new Remoting.SerializedRequest[] { new Remoting.SerializedRequest (DbId.CreateId (100, 1).Value, serialized_1),
				/**/												   new Remoting.SerializedRequest (DbId.CreateId (101, 1).Value, serialized_2) });
			
			Remoting.RequestState[] states;
			
			service.QueryRequestStates (new Remoting.ClientIdentity ("NUnit Test Client"), out states);
			
			System.Diagnostics.Debug.WriteLine ("1/ Got " + states.Length + " states back from server :");
			
			for (int i = 0; i < states.Length; i++)
			{
				System.Diagnostics.Debug.WriteLine ("-- " + states[i].Identifier + ", state = " + (Requests.ExecutionState) states[i].State);
			}
			
			System.Threading.Thread.Sleep (500);
			
			service.QueryRequestStates (new Remoting.ClientIdentity ("NUnit Test Client"), out states);
			
			System.Diagnostics.Debug.WriteLine ("2/ Got " + states.Length + " states back from server :");
			
			for (int i = 0; i < states.Length; i++)
			{
				System.Diagnostics.Debug.WriteLine ("-- " + states[i].Identifier + ", state = " + (Requests.ExecutionState) states[i].State);
			}
			
			service.ClearRequestStates (new Remoting.RequestState[] { states[0] });
			service.QueryRequestStates (new Remoting.ClientIdentity ("NUnit Test Client"), out states);
			
			System.Diagnostics.Debug.WriteLine ("3/ After clearing first state, got " + states.Length + " states back from server :");
			
			for (int i = 0; i < states.Length; i++)
			{
				System.Diagnostics.Debug.WriteLine ("-- " + states[i].Identifier + ", state = " + (Requests.ExecutionState) states[i].State);
			}
			
		}
		
		[Test] /*[Ignore ("Temporary")]*/ public void Check15OperatorClient()
		{
			Remoting.IOperatorService service = Services.Engine.GetRemoteOperatorService ("localhost", 1234);
			Remoting.IOperation operation;
			Remoting.IProgressInformation progress;
			
			Assert.IsNotNull (service);
			
			service.CreateRoamingClient (out operation);
			
			for (int i = 0; i < 10; i++)
			{
				System.Threading.Thread.Sleep (100);
				bool finished = operation.HasFinished;
				System.Diagnostics.Debug.WriteLine ("Operation: " + (finished ? "finished" : "running") + " after " + (int) operation.RunningDuration.TotalMilliseconds + " ms.");
				if (finished) break;
			}
			
			System.Diagnostics.Debug.WriteLine ("Cancelling...");
			operation.CancelOperation (out progress);
			
			for (int i = 0; i < 100; i++)
			{
				System.Threading.Thread.Sleep (10);
				bool finished = progress.HasFinished;
				System.Diagnostics.Debug.WriteLine ("Cancellation: " + (finished ? "finished" : "running") + " after " + (int) progress.RunningDuration.TotalMilliseconds + " ms.");
				if (finished) break;
			}
		}
		
		
		private static void CreateTestTable(DbInfrastructure infrastructure, string name)
		{
			infrastructure.Logger.CreateTemporaryEntry (null);
			
			DbType db_type_name = infrastructure.ResolveDbType (null, "Customer Name");
			DbType db_type_date = infrastructure.ResolveDbType (null, "Birth Date");
			
			Assert.IsNotNull (db_type_name);
			Assert.IsNotNull (db_type_date);
			
			DbTable db_table = infrastructure.CreateDbTable (name, DbElementCat.UserDataManaged, DbRevisionMode.Disabled);
			
			DbColumn db_col_1 = DbColumn.CreateUserDataColumn ("Name",       db_type_name, Nullable.No);
			DbColumn db_col_2 = DbColumn.CreateUserDataColumn ("Birth Date", db_type_date, Nullable.Yes);
			
			db_table.Columns.AddRange (new DbColumn[] { db_col_1, db_col_2 });
			
			infrastructure.RegisterNewDbTable (null, db_table);
		}
		
		private static System.Data.DataTable GetDataTableFromTable(DbInfrastructure infrastructure, string name)
		{
			DbTable db_table = infrastructure.ResolveDbTable (null, name);
			
			DbColumn db_col_id   = db_table.Columns[0];
			DbColumn db_col_stat = db_table.Columns[1];
			DbColumn db_col_log  = db_table.Columns[2];
			DbColumn db_col_1    = db_table.Columns[3];
			DbColumn db_col_2    = db_table.Columns[4];
			
			System.Data.DataSet   set   = new System.Data.DataSet ();
			System.Data.DataTable table = new System.Data.DataTable (db_table.Name);
			
			set.Tables.Add (table);
			
			System.Data.DataColumn col_1 = new System.Data.DataColumn (db_col_id.Name, typeof (long));
			System.Data.DataColumn col_2 = new System.Data.DataColumn (db_col_stat.Name, typeof (int));
			System.Data.DataColumn col_3 = new System.Data.DataColumn (db_col_log.Name, typeof (long));
			System.Data.DataColumn col_4 = new System.Data.DataColumn (db_col_1.Name, typeof (string));
			System.Data.DataColumn col_5 = new System.Data.DataColumn (db_col_2.Name, typeof (System.DateTime));
			
			col_1.Unique = true;
			
			table.Columns.Add (col_1);
			table.Columns.Add (col_2);
			table.Columns.Add (col_3);
			table.Columns.Add (col_4);
			table.Columns.Add (col_5);
			
			return table;
		}
			
		private static void DeleteTestTable(DbInfrastructure infrastructure, string name)
		{
			DbTable db_table = infrastructure.ResolveDbTable (null, name);
			infrastructure.UnregisterDbTable (null, db_table);
		}
		
		
		public static System.Data.DataTable CreateSampleTable()
		{
			System.Data.DataSet   set   = new System.Data.DataSet ();
			System.Data.DataTable table = new System.Data.DataTable ("DemoTable");
			
			set.Tables.Add (table);
			
			System.Data.DataColumn col_1 = new System.Data.DataColumn ("ID", typeof (long));
			System.Data.DataColumn col_2 = new System.Data.DataColumn ("Name", typeof (string));
			System.Data.DataColumn col_3 = new System.Data.DataColumn ("Birth Year", typeof (int));
			
			col_1.Unique = true;
			
			table.Columns.Add (col_1);
			table.Columns.Add (col_2);
			table.Columns.Add (col_3);
			
			table.Rows.Add (new object[] { 1L, "Pierre Arnaud", 1972 });
			table.Rows.Add (new object[] { 2L, "J�r�me Andr�",  1994 });
			
			table.AcceptChanges ();
			
			return table;
		}
	}
}

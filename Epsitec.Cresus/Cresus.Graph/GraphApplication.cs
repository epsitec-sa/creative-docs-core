﻿//	Copyright © 2009, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Pierre ARNAUD, Maintainer: Pierre ARNAUD

using Epsitec.Common.Drawing;
using Epsitec.Common.Graph.Widgets;
using Epsitec.Common.Graph.Renderers;
using Epsitec.Common.UI;

using Epsitec.Common.Support;
using Epsitec.Common.Support.EntityEngine;
using Epsitec.Common.Types;
using Epsitec.Common.Widgets;

using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Epsitec.Common.Graph.Data;

namespace Epsitec.Cresus.Graph
{
	public partial class GraphApplication : Application
	{
		public GraphApplication()
		{
			this.graphCommands = new GraphCommands (this);
			this.persistenceManager = new Core.UI.PersistenceManager ();
			this.documents = new List<GraphDocument> ();
			this.mainWindowController = new Controllers.MainWindowController ();

			this.loadDataSetAction = Actions.Factory.New (this.LoadDataSet);
			this.removeSeriesFromGraphAction = Actions.Factory.New (this.RemoveFromChart);
		}

		
		public bool IsReady
		{
			get;
			private set;
		}

		
		public override string ShortWindowTitle
		{
			get
			{
				return "Crésus Graphe";
			}
		}


		public GraphDocument Document
		{
			get
			{
				this.SetupDefaultDocument ();
				return this.activeDocument;
			}
		}

		internal Controllers.MainWindowController MainWindowController
		{
			get
			{
				return this.mainWindowController;
			}
		}

		internal void SetupDefaultDocument()
		{
			if (this.activeDocument == null)
			{
				this.CreateDocument ();
				this.SetupDataSet ();
			}
		}

		internal void SetupDataSet()
		{
			this.loadDataSetAction ();
		}

		internal void SetupInterface()
		{
			this.Window = this.mainWindowController.Window;

			this.SetEnable (ApplicationCommands.Save, false);
			
			this.seriesPickerController = new Controllers.SeriesPickerController (this.Window);
			this.seriesPickerController.SumSeriesAction = Actions.Factory.New (this.SumRows);
			this.seriesPickerController.NegateSeriesAction = Actions.Factory.New (this.NegateRows);
			this.seriesPickerController.AddSeriesToGraphAction = Actions.Factory.New (this.AddToChart);

			this.RestoreApplicationState ();
			this.Document.MakeVisible ();
			this.IsReady = true;
		}

		internal void AsyncSaveApplicationState()
		{
			Application.QueueAsyncCallback (this.SaveApplicationState);
		}

		internal void RegisterDocument(GraphDocument graphDocument)
		{
			System.Diagnostics.Debug.Assert (this.documents.Contains (graphDocument) == false);

			this.documents.Add (graphDocument);

			if (this.activeDocument == null)
			{
				this.SetActiveDocument (graphDocument);
			}
		}

		internal void SetActiveDocument(GraphDocument document)
		{
			if (this.activeDocument != document)
			{
				this.activeDocument = document;
				this.OnActiveDocumentChanged ();
			}
		}


		protected override void ExecuteQuit(CommandDispatcher dispatcher, CommandEventArgs e)
		{
			this.SaveApplicationState ();
			base.ExecuteQuit (dispatcher, e);
		}
		
		private void RestoreApplicationState()
		{
			if (System.IO.File.Exists (GraphApplication.Paths.SettingsPath))
			{
				XDocument doc = XDocument.Load (GraphApplication.Paths.SettingsPath);
				XElement store = doc.Element ("store");

//-				this.stateManager.RestoreStates (store.Element ("stateManager"));
				this.RestoreDocumentSettings (store.Element ("documents"));
				Core.UI.RestoreWindowPositions (store.Element ("windowPositions"));
				this.persistenceManager.Restore (store.Element ("uiSettings"));
			}

			this.persistenceManager.DiscardChanges ();
			this.persistenceManager.SettingsChanged += (sender) => this.AsyncSaveApplicationState ();
		}
		
		private void SaveApplicationState()
		{
			if (this.IsReady)
			{
				System.Diagnostics.Debug.WriteLine ("Saving application state.");
				System.DateTime now = System.DateTime.Now.ToUniversalTime ();
				string timeStamp = string.Concat (now.ToShortDateString (), " ", now.ToShortTimeString (), " UTC");

				XDocument doc = new XDocument (
					new XDeclaration ("1.0", "utf-8", "yes"),
					new XComment ("Saved on " + timeStamp),
					new XElement ("store",
//						this.StateManager.SaveStates ("stateManager"),
						this.SaveDocumentSettings ("documents"),
						Core.UI.SaveWindowPositions ("windowPositions"),
						this.persistenceManager.Save ("uiSettings")));

				doc.Save (GraphApplication.Paths.SettingsPath);
				System.Diagnostics.Debug.WriteLine ("Save done.");
			}
		}

		private XElement SaveDocumentSettings(string xmlNodeName)
		{
			return new XElement (xmlNodeName,
					from doc in this.documents
					select doc.SaveSettings (new XElement ("doc")));
		}

		private void RestoreDocumentSettings(XElement xml)
		{
			if (xml == null)
			{
				return;
			}

			foreach (XElement node in xml.Elements ())
			{
				System.Diagnostics.Debug.Assert (node.Name == "doc");

				var doc = this.CreateDocument ();

				doc.RestoreSettings (node);
			}
		}

		private GraphDocument CreateDocument()
		{
			var doc = new GraphDocument ()
			{
				RemoveSeriesFromGraphAction = this.removeSeriesFromGraphAction
			};



			return doc;
		}

		
		private void SumRows(IEnumerable<int> rows)
		{
			var sum = this.activeDocument.DataSet.DataTable.SumRows (rows, this.seriesPickerController.GetRowSeries);

			if (sum == null)
			{
				return;
			}

			this.activeDocument.DataSet.DataTable.RemoveRows (rows);
			this.activeDocument.DataSet.DataTable.Insert (rows.First (), sum.Label.Replace ("+-", "-"), sum.Values);

			this.seriesPickerController.UpdateScrollListItems ();
			this.seriesPickerController.UpdateChartView ();
		}

		private void NegateRows(IEnumerable<int> rows)
		{
			foreach (int row in rows)
			{
				var series = this.activeDocument.DataSet.DataTable.GetRowSeries (row);
				this.seriesPickerController.NegateSeries (series.Label);
			}

			this.seriesPickerController.UpdateScrollListItems ();
			this.seriesPickerController.UpdateChartView ();
		}

		private void AddToChart(IEnumerable<int> rows)
		{
			var table = this.activeDocument.DataSet.DataTable;

			foreach (int row in rows)
			{
				this.activeDocument.Add (table.GetRowSeries (row));
			}

			table.RemoveRows (rows);
			
			this.seriesPickerController.UpdateScrollListItems ();
			this.seriesPickerController.UpdateChartView ();
		}

		private void RemoveFromChart(IEnumerable<int> rows)
		{
			var table = this.activeDocument.DataSet.DataTable;
			var list  = new List<ChartSeries> (rows.Select (x => this.activeDocument.Find (x)));

			list.ForEach (series => this.activeDocument.Remove (series));
			list.ForEach (series => table.Add (series.Label, series.Values));
			
			this.seriesPickerController.UpdateScrollListItems ();
			this.seriesPickerController.UpdateChartView ();

			this.seriesPickerController.SetSelectedItem (table.RowCount-1);
		}

		private void LoadDataSet()
		{
			System.Diagnostics.Debug.Assert (this.activeDocument != null);
			
			this.activeDocument.DataSet.LoadDataTable ();
			this.OnActiveDocumentChanged ();
		}


		private Controllers.SeriesPickerController seriesPickerController;

		private void OnActiveDocumentChanged()
		{
			var handler = this.ActiveDocumentChanged;

			if (handler != null)
			{
				handler (this);
			}
		}
		
		
		public event EventHandler ActiveDocumentChanged;

		private readonly System.Action loadDataSetAction;

		private GraphCommands graphCommands;
		private GraphDocument activeDocument;
		private readonly Controllers.MainWindowController mainWindowController;
		private readonly List<GraphDocument> documents;
		private readonly Core.UI.PersistenceManager persistenceManager;
		private readonly System.Action<IEnumerable<int>> removeSeriesFromGraphAction;
	}
}

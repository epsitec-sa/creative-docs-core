﻿//	Copyright © 2008-2010, EPSITEC SA, 1400 Yverdon-les-Bains, Switzerland
//	Author: Pierre ARNAUD, Maintainer: Pierre ARNAUD

using Epsitec.Common.Dialogs;
using Epsitec.Common.Support;
using Epsitec.Common.Support.Extensions;
using Epsitec.Common.Support.EntityEngine;
using Epsitec.Common.Types;
using Epsitec.Common.Widgets;

using Epsitec.Cresus.Core;
using Epsitec.Cresus.Core.Library;
using Epsitec.Cresus.Core.Business.UserManagement;
using Epsitec.Cresus.Core.Controllers;
using Epsitec.Cresus.Core.Entities;
using Epsitec.Cresus.Core.Orchestrators;

using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Epsitec.Cresus.Core
{
	/// <summary>
	/// The <c>CoreApplication</c> class implements the central application
	/// logic.
	/// </summary>
	public partial class CoreApplication : Application
	{
		public CoreApplication()
		{
			CoreProgram.Application = this;
			UI.SetApplication (this);
			
			this.plugIns = new List<PlugIns.ICorePlugIn> ();
			this.attachedDialogs = new List<Dialogs.IAttachedDialog> ();
			
			this.persistenceManager = new PersistenceManager ();

			this.data = new CoreData (forceDatabaseCreation: false);

			this.exceptionManager = new ExceptionManager ();
			this.commands = new CoreCommandDispatcher (this);
			this.userManager = new Business.UserManagement.UserManager (this.data);

			this.mainWindowOrchestrator = new DataViewOrchestrator (this.data, this.CommandContext);
			this.mainWindowController = new MainWindowController (this.data, this.CommandContext, this.mainWindowOrchestrator);

			this.userManager.AuthenticatedUserChanged += this.HandleAuthenticatedUserChanged;
		}


		public bool								IsReady
		{
			get;
			private set;
		}

		public CoreData							Data
		{
			get
			{
				return this.data;
			}
		}

		public BusinessSettingsEntity BusinessSettings
		{
			get
			{
				if (this.businessSettings == null)
				{
					this.businessSettings = this.data.GetAllEntities<BusinessSettingsEntity> ().FirstOrDefault ();
				}

				return this.businessSettings;
			}
		}

		public FinanceSettingsEntity FinanceSettings
		{
			get
			{
				if (this.financeSettings == null)
				{
					this.financeSettings = this.data.GetAllEntities<FinanceSettingsEntity> ().FirstOrDefault ();
				}

				return this.financeSettings;
			}
		}

		public IExceptionManager ExceptionManager
		{
			get
			{
				return this.exceptionManager;
			}
		}

		public PersistenceManager				PersistanceManager
		{
			get
			{
				return this.persistenceManager;
			}
		}
		
		public override string					ShortWindowTitle
		{
			get
			{
				return Res.Strings.ProductName.ToSimpleText ();
			}
		}

		public override string					ApplicationIdentifier
		{
			get
			{
				return "EpCresusCore";		//	TODO: Res.Strings.ProductAppId.ToSimpleText ();
			}
		}

		public CoreCommandDispatcher			Commands
		{
			get
			{
				return this.commands;
			}
		}

		public MainWindowController				MainWindowController
		{
			get
			{
				return this.mainWindowController;
			}
		}

		public DataViewOrchestrator				MainWindowOrchestrator
		{
			get
			{
				return this.mainWindowOrchestrator;
			}
		}

		public IList<Dialogs.IAttachedDialog>	AttachedDialogs
		{
			get
			{
				return this.attachedDialogs.AsReadOnly ();
			}
		}

		public UserManager						UserManager
		{
			get
			{
				return this.userManager;
			}
		}

		
		public void AttachDialog(Dialogs.IAttachedDialog dialog)
		{
			if (!this.attachedDialogs.Contains (dialog))
			{
				this.attachedDialogs.Add (dialog);
			}
		}

		public void DetachDialog(Dialogs.IAttachedDialog dialog)
		{
			if (this.attachedDialogs.Contains (dialog))
			{
				this.attachedDialogs.Remove (dialog);
			}
		}


		public static T GetController<T>(CommandContext context)
			where T : CoreController
		{
			var root = CoreProgram.Application.MainWindowController;

			if (root is T)
			{
				return root as T;
			}

			return root.GetAllSubControllers ().Where (item => item is T).FirstOrDefault () as T;
		}


		#region Settings
		public static string GetSettings(string key)
		{
			//	Donne le contenu d'un réglage global.
			if (CoreApplication.settings.ContainsKey (key))
			{
				return CoreApplication.settings[key];
			}

			return null;
		}

		public static void SetSettings(string key, string value)
		{
			//	Modifie un réglage global.
			CoreApplication.settings[key] = value;
		}

		public static Dictionary<string, string> ExtractSettings(string startKey)
		{
			//	Extrait tous les réaglages d'une catégorie donnée.
			Dictionary<string, string> dict = new Dictionary<string, string> ();

			foreach (var pair in CoreApplication.settings)
			{
				if (pair.Key.StartsWith (startKey))
				{
					dict.Add (pair.Key, pair.Value);
				}
			}

			return dict;
		}

		public static void MergeSettings(string startKey, Dictionary<string, string> dict)
		{
			//	Met à jour tous les réaglages d'une catégorie donnée.
			var keys = new List<string> ();
			foreach (var key in CoreApplication.settings.Keys)
			{
				keys.Add (key);
			}

			foreach (var key in keys)
			{
				if (key.StartsWith (startKey))
				{
					CoreApplication.settings.Remove (key);
				}
			}

			foreach (var pair in dict)
			{
				CoreApplication.settings.Add (pair.Key, pair.Value);
			}
		}

		public static void SaveSettings()
		{
			try
			{
				string[] lines = CoreApplication.GetDataToSerialize ();
				System.IO.File.WriteAllLines (CoreApplication.GetSettingsPath (), lines);
			}
			catch
			{
			}

		}

		public static void LoadSettings()
		{
			try
			{
				string[] lines = System.IO.File.ReadAllLines (CoreApplication.GetSettingsPath ());
				CoreApplication.SetDeserializedData (lines);
			}
			catch
			{
			}
		}

		private void HandleAuthenticatedUserChanged(object sender)
		{
			this.data.ConnectionManager.ReopenConnection ();
		}

        private static string GetSettingsPath()
		{
			return System.IO.Path.Combine (Globals.Directories.UserAppData, "Cresus.Core.settings.data");
		}

		private static string[] GetDataToSerialize()
		{
			var lines = new List<string> ();

			foreach (var pair in CoreApplication.settings)
			{
				lines.Add (string.Concat (pair.Key, "=", pair.Value));
			}

			return lines.ToArray ();
		}

		private static void SetDeserializedData(string[] lines)
		{
			foreach (var line in lines)
			{
				int i = line.IndexOf ("=");
				if (i != -1)
				{
					string key   = line.Substring (0, i);
					string value = line.Substring (i+1);

					CoreApplication.settings.Add (key, value);
				}
			}
		}
		#endregion


		internal void CreateUI()
		{
			this.OnCreatingUI ();
			this.CreateUIMainWindow ();
			this.CreateUIControllers ();
			this.RestoreApplicationState ();
			this.OnCreatedUI ();

			this.IsReady = true;
		}

		internal void AsyncSaveApplicationState()
		{
			Application.QueueAsyncCallback (this.SaveApplicationState);
		}

		internal void SetupData()
		{
			this.data.SetupDatabase ();
			this.data.SetupBusiness ();
			this.OnSetupDataDone ();

#if false
			var blob  = this.data.ImageDataStore.PersistImageBlob (new System.IO.FileInfo (@"C:\Users\arnaud\Pictures\Lionel Tardy et Marc Bettex.jpg"));
			var image = this.data.ImageDataStore.GetImageData (blob.Code);
			bool same1 = this.data.ImageDataStore.CheckEqual (blob.Code, image);
			bool same2 = this.data.ImageDataStore.CheckEqual (blob.Code, new Epsitec.Cresus.Core.Data.ImageData (System.IO.File.ReadAllBytes (image.Uri.Path)));

			var imageEntity = this.Data.DataContext.CreateEntity<ImageEntity> ();

			this.data.ImageDataStore.UpdateImage (this.Data.DataContext, imageEntity, new System.IO.FileInfo (@"C:\Users\arnaud\Pictures\Lionel Tardy et Marc Bettex.jpg"));
			this.data.ImageDataStore.GetImageData (blob.Code, 100);
			this.data.ImageDataStore.GetImageData (blob.Code, 200);
			this.data.ImageDataStore.GetImageData (blob.Code, 100);
#endif
		}

		internal void DiscoverPlugIns()
		{
			this.plugInFactory = new PlugIns.PlugInFactory (this);
		}

		internal void CreatePlugIns()
		{
			foreach (var attribute in this.plugInFactory.GetPlugInAttributeList ())
			{
				this.plugIns.Add (this.plugInFactory.CreatePlugIn (attribute.Name));
			}
		}

		internal void Shutdown()
		{
			this.OnShutdownStarted ();

			this.plugIns.ForEach (x => x.Dispose ());
			this.plugIns.Clear ();
		}

		
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.mainWindowOrchestrator.Dispose ();
				this.mainWindowController.Dispose ();

				if (this.data != null)
				{
					this.data.Dispose ();
					this.data = null;
				}
				if (this.exceptionManager != null)
				{
					this.exceptionManager.Dispose ();
					this.exceptionManager = null;
				}
			}

			base.Dispose (disposing);
		}


		private void CreateUIMainWindow()
		{
			string path = System.IO.Path.Combine (Globals.Directories.ExecutableRoot, "app.ico");
			
			Window window = new Window
			{
				Text = this.ShortWindowTitle,
				ClientSize = new Epsitec.Common.Drawing.Size (600, 400),
				Icon = Epsitec.Common.Drawing.Bitmap.FromNativeIcon (path, 48, 48)
			};

			this.Window = window;
			this.Window.Root.SizeChanged +=
				delegate
				{
					this.Window.Text = string.Format ("{0} Alpha {1}x{2}", this.ShortWindowTitle, this.Window.ClientSize.Width, this.Window.ClientSize.Height);
				};
		}

		private void CreateUIControllers()
		{
			this.mainWindowController.CreateUI (this.Window.Root);
		}

		private void RestoreApplicationState()
		{
			if (System.IO.File.Exists (CoreApplication.Paths.SettingsPath))
			{
				XDocument doc = XDocument.Load (CoreApplication.Paths.SettingsPath);
				XElement store = doc.Element ("store");

//-				this.stateManager.RestoreStates (store.Element ("stateManager"));
				UI.RestoreWindowPositions (store.Element ("windowPositions"));
				this.persistenceManager.Restore (store.Element ("uiSettings"));
			}
			
			this.persistenceManager.DiscardChanges ();
			this.persistenceManager.SettingsChanged += (sender) => this.AsyncSaveApplicationState ();

//-			this.UpdateCommandsAfterStateChange ();
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
//-						this.StateManager.SaveStates ("stateManager"),
						UI.SaveWindowPositions ("windowPositions"),
						this.persistenceManager.Save ("uiSettings")));

				doc.Save (CoreApplication.Paths.SettingsPath);
				System.Diagnostics.Debug.WriteLine ("Save done.");
			}
		}



		private void OnSetupDataDone()
		{
			var handler = this.SetupDataDone;

			if (handler != null)
			{
				handler (this);
			}
		}

		private void OnCreatingUI()
		{
			var handler = this.CreatingUI;

			if (handler != null)
			{
				handler (this);
			}
		}

		private void OnCreatedUI()
		{
			var handler = this.CreatedUI;

			if (handler != null)
			{
				handler (this);
			}
		}

		
		private void OnShutdownStarted()
		{
			var handler = this.ShutdownStarted;
			
			if (handler != null)
			{
				handler (this);
			}
		}

		public event EventHandler						SetupDataDone;
		public event EventHandler						CreatingUI;
		public event EventHandler						CreatedUI;
		public event EventHandler						ShutdownStarted;


		private static Dictionary<string, string>		settings = new Dictionary<string, string> ();

		private readonly List<PlugIns.ICorePlugIn>		plugIns;
		private readonly List<Dialogs.IAttachedDialog>	attachedDialogs;

		private PersistenceManager						persistenceManager;
		private CoreData								data;
		private Business.UserManagement.UserManager		userManager;
		private ExceptionManager						exceptionManager;
		private CoreCommandDispatcher					commands;
		private DataViewOrchestrator					mainWindowOrchestrator;
		private MainWindowController					mainWindowController;
		private BusinessSettingsEntity					businessSettings;
		private FinanceSettingsEntity					financeSettings;
		private PlugIns.PlugInFactory					plugInFactory;
	}
}

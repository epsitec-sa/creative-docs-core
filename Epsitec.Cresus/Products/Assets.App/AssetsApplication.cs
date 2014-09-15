﻿//	Copyright © 2013, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Pierre ARNAUD, Maintainer: Pierre ARNAUD

using System.Collections.Generic;
using System.Linq;
using Epsitec.Common.Drawing;
using Epsitec.Common.Widgets;
using Epsitec.Cresus.Core.Business;
using Epsitec.Cresus.Core.Library;
using Epsitec.Cresus.Assets.App.Widgets;
using Epsitec.Cresus.Assets.Server.SimpleEngine;
using Epsitec.Cresus.Assets.Server.Engine;

namespace Epsitec.Cresus.Assets.App
{
	public class AssetsApplication : CoreInteractiveApp
	{
		public AssetsApplication()
		{
			this.commandDispatcher = new CommandDispatcher ("Assets", CommandDispatcherLevel.Primary);
			this.commandContext = new CommandContext ();

			this.commandDispatcher.RegisterController (this);
		}

		public override string					ShortWindowTitle
		{
			get
			{
				return Res.Strings.AssetsApplication.WindowTitle.ToString ();
			}
		}
		
		public override string					ApplicationIdentifier
		{
			get
			{
				return "Cr.Assets";
			}
		}

		
		public override bool					StartupLogin()
		{
			return true;
		}


		protected override Window CreateWindow(Size size)
		{
			var window = base.CreateWindow (new Size (1000, 700));

//-			window.MakeTitlelessResizableWindow ();

			return window;
		}

		protected override void ExecuteQuit(CommandDispatcher dispatcher, CommandEventArgs e)
		{
			base.ExecuteQuit (dispatcher, e);
		}

		protected override CoreAppPolicy CreateDefaultPolicy()
		{
			var policy = base.CreateDefaultPolicy ();
			
			policy.RequiresCoreCommandHandlers = false;
			policy.UseEmbeddedServer = true;

			return policy;
		}

		protected override void CreateManualComponents(IList<System.Action> initializers)
		{
			initializers.Add (this.InitializeApplication);
		}

		protected override System.Xml.Linq.XDocument LoadApplicationState()
		{
			return null;
		}

		protected override void SaveApplicationState(System.Xml.Linq.XDocument doc)
		{
		}

		private void InitializeApplication()
		{
			this.businessContext = new BusinessContext (this.Data, enableReload: true);

			var window = this.Window;
			
			window.Root.BackColor = Color.FromName ("White");
			this.CreateUI (window);	

			window.Show ();
			window.MakeActive ();
		}


		private void CreateUI(Window window)
		{
			//?Epsitec.Common.Widgets.Adorners.Factory.SetActive ("LookMetal");
			//?Epsitec.Common.Widgets.Adorners.Factory.SetActive ("LookSimply");
			Epsitec.Common.Widgets.Adorners.Factory.SetActive ("LookFlat");

			CommandDispatcher.SetDispatcher (this.Window, this.commandDispatcher);
			CommandContext.SetContext (this.Window, this.commandContext);

			var frame = new ShortcutCatcher
			{
				Parent    = window.Root,
				Dock      = DockStyle.Fill,
				BackColor = ColorManager.WindowBackgroundColor,
				Name      = "PopupParentFrame",
			};

			//	Crée le clipboard unique à l'application.
			var cb = new DataClipboard ();

			//	Crée et ouvre le mandat par défaut.
			var accessor = new DataAccessor(cb);
			var factory = MandatFactory.Factories.Where (x => x.IsDefault).FirstOrDefault ();
			System.Diagnostics.Debug.Assert (factory != null);
			factory.Create (accessor, Res.Strings.AssetsApplication.DefaultMandat.ToString (), new System.DateTime (2011, 1, 1), true);

			var ui = new AssetsUI (accessor, this.commandDispatcher, this.commandContext);
			ui.CreateUI (frame);

			frame.Focus ();
		}


		private readonly CommandDispatcher		commandDispatcher;
		private readonly CommandContext			commandContext;
		private BusinessContext					businessContext;
	}
}

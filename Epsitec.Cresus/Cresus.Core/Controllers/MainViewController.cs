﻿//	Copyright © 2010, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Pierre ARNAUD, Maintainer: Pierre ARNAUD

using Epsitec.Common.Drawing;
using Epsitec.Common.Support;
using Epsitec.Common.Support.EntityEngine;
using Epsitec.Common.Types;
using Epsitec.Common.Widgets;

using Epsitec.Cresus.Core.Entities;

using System.Collections.Generic;
using System.Linq;

namespace Epsitec.Cresus.Core.Controllers
{
	public class MainViewController : CoreViewController
	{
		public MainViewController(CoreData data)
			: base ("MainView")
		{
			this.data     = data;

			this.browserController = new BrowserViewController ("Browser", data);
			this.browserSettingsController = new BrowserSettingsController ("BrowserSettings", this.browserController);
			this.dataViewController = new DataViewController ("Data", data);

			this.browserController.SetContents (() => this.data.GetCustomers ());
			
			this.browserController.CurrentChanging +=
				delegate
				{
					this.dataViewController.ClearActiveEntity ();
				};

			this.browserController.CurrentChanged +=
				delegate
				{
					this.dataViewController.SetActiveEntity (this.browserController.GetActiveEntity ());
				};
		}

		public BrowserSettingsMode BrowserSettingsMode
		{
			get
			{
				return this.browserSettingsMode;
			}
			set
			{
				if (this.browserSettingsMode != value)
                {
					this.browserSettingsMode = value;
					this.UpdateBrowserSettingsPanel ();
                }
			}
		}


		public override IEnumerable<CoreController> GetSubControllers()
		{
			yield return this.browserController;
			yield return this.browserSettingsController;
			yield return this.dataViewController;
		}

		public override void CreateUI(Widget container)
		{
			this.CreateUIFrame (container);

			this.browserController.CreateUI (this.leftPanel);
			this.browserSettingsController.CreateUI (this.browserSettingsPanel);
			this.dataViewController.CreateUI (this.rightPanel);

			this.BrowserSettingsMode = Controllers.BrowserSettingsMode.Compact;
		}


		private void UpdateBrowserSettingsPanel()
		{
			var expandedPanel = this.topPanel;
			var compactPanel  = this.browserController.SettingsPanel;
			
			switch (this.browserSettingsMode)
			{
				case Controllers.BrowserSettingsMode.Hidden:
					expandedPanel.Visibility = false;
					compactPanel.Visibility  = false;
					this.browserSettingsPanel.Parent = null;
					break;

				case Controllers.BrowserSettingsMode.Compact:
					expandedPanel.Visibility = false;
					compactPanel.Visibility  = true;
					this.browserSettingsPanel.Parent = compactPanel;
					break;

				case Controllers.BrowserSettingsMode.Expanded:
					expandedPanel.Visibility = true;
					compactPanel.Visibility  = false;
					this.browserSettingsPanel.Parent = expandedPanel;
					break;

				default:
					throw new System.NotSupportedException (string.Format ("BrowserSettingsMode.{0} not supported", this.browserSettingsMode));
			}
		}

		private void CreateUIFrame(Widget container)
		{
			this.frame = new FrameBox
			{
				Parent = container,
				Dock = DockStyle.Fill,
				DrawFullFrame = false,
				DrawFrameState = FrameState.None
			};
			
			this.CreateUITopPanel ();
			this.CreateUISettingsPanel ();
			this.CreateUILeftPanel ();
			this.CreateUIRightPanel ();
			this.CreateUISplitter ();
		}

		private void CreateUITopPanel()
		{
			this.topPanel = new FrameBox
			{
				Parent = this.frame,
				Name = "TopPanel",
				Dock = DockStyle.Top,
				PreferredHeight = 40,
			};
		}

		private void CreateUISettingsPanel()
		{
			this.browserSettingsPanel = new FrameBox
			{
				Parent = this.topPanel,
				Name = "SettingsPanel",
				Dock = DockStyle.Fill,
			};
		}

		private void CreateUILeftPanel()
		{
			this.leftPanel = new FrameBox
			{
				Parent = this.frame,
				Name = "LeftPanel",
				Dock = DockStyle.Left,
				Padding = new Margins (0, 0, 0, 0),
				PreferredWidth = 150,
			};
		}

		private void CreateUIRightPanel()
		{
			this.rightPanel = new FrameBox
			{
				Parent = this.frame,
				Name = "RightPanel",
				Dock = DockStyle.Fill,
				Padding = new Margins (0, 0, 0, 0),
			};
		}

		private void CreateUISplitter()
		{
			this.splitter = new VSplitter
			{
				Parent = this.frame,
				Dock = DockStyle.Left,
				PreferredWidth = 8,
			};
		}
		
		private readonly CoreData data;
		private readonly BrowserViewController browserController;
		private readonly BrowserSettingsController browserSettingsController;
		private readonly DataViewController dataViewController;

		private FrameBox frame;

		private FrameBox topPanel;
		private FrameBox browserSettingsPanel;
		private FrameBox leftPanel;
		private VSplitter splitter;
		private FrameBox rightPanel;

		private BrowserSettingsMode browserSettingsMode;
	}
}

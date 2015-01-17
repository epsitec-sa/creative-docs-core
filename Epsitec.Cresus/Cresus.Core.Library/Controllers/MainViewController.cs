﻿//	Copyright © 2010-2011, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Pierre ARNAUD, Maintainer: Pierre ARNAUD

using Epsitec.Common.Drawing;
using Epsitec.Common.Support.EntityEngine;
using Epsitec.Common.Support.Extensions;
using Epsitec.Common.Widgets;

using Epsitec.Cresus.Core.Controllers.BrowserControllers;
using Epsitec.Cresus.Core.Entities;
using Epsitec.Cresus.Core.Orchestrators;
using Epsitec.Cresus.Core.Print;

using System.Collections.Generic;
using System.Linq;
using Epsitec.Cresus.Core.Library;

namespace Epsitec.Cresus.Core.Controllers
{
	public sealed class MainViewController : ViewControllerComponent<MainViewController>, ICommandHandler
	{
		public MainViewController(DataViewOrchestrator orchestrator)
			: base (orchestrator)
		{
			this.commandContext = orchestrator.Host.CommandContext;
			
			this.Orchestrator.RegisterApplicationComponent (this);

			this.previewViewController     = new PreviewViewController (this.Orchestrator);
			this.browserViewController     = new BrowserViewController (this.Orchestrator);
			this.browserSettingsController = new BrowserSettingsController (this.browserViewController);

			this.browserViewController.DataSetSelected += this.HandleBrowserViewControllerDataSetSelected;
			
			
			//	TODO: check if following code is still meaningful

			this.commandContext.AttachCommandHandler (this);
		}


		public CommandContext					CommandContext
		{
			get
			{
				return this.commandContext;
			}
		}

		public new NavigationOrchestrator		Navigator
		{
			get
			{
				return this.Orchestrator.Navigator;
			}
		}

		public BrowserSettingsMode				BrowserSettingsMode
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

		public BrowserViewController			BrowserViewController
		{
			get
			{
				return this.browserViewController;
			}
		}

		public DataViewController				DataViewController
		{
			get
			{
				return this.Orchestrator.DataViewController;
			}
		}

		public PreviewViewController			PreviewViewController
		{
			get
			{
				return this.previewViewController;
			}
		}

		
		public override IEnumerable<CoreController> GetSubControllers()
		{
			yield return this.browserViewController;
			yield return this.browserSettingsController;
			yield return this.DataViewController;
			yield return this.previewViewController;
		}

		public override void CreateUI(Widget container)
		{
			base.CreateUI (container);
			this.CreateUIFrame (container);

			this.browserViewController.CreateUI (this.leftPanel);
			this.browserSettingsController.CreateUI (this.browserSettingsPanel);
			this.DataViewController.CreateUI (this.mainPanel);
			this.previewViewController.CreateUI (this.rightPreviewPanel);

			this.BrowserSettingsMode = BrowserSettingsMode.Compact;
		}

		public static MainViewController Find(CommandContextChain contextChain)
		{
			return contextChain.Contexts.Select (x => x.GetCommandHandler<MainViewController> ()).Where (x => x != null).FirstOrDefault ();
		}

		public void SetPreviewPanelVisibility(bool visibility)
		{
			if (this.rightPreviewPanel != null)
			{
				this.rightPreviewPanel.Visibility = visibility;
				this.rightSplitter.Visibility = this.rightPreviewPanel.Visibility;
			}
		}

		public void PrintPrintableEntity()
		{
//?			var context   = this.Data.CreateDataContext ("PrintEngine:Print");
			var entity = this.GetPrintableEntity ();

			//	TODO: vérifier que cette logique est bien correcte; pour le moment, on doit partager les BusinessContext avec l'interface graphique, mais à terme il faudra probablement découpler cela...

			PrintEngine.PrintCommand (this.BusinessContext, entity);

//?			this.Data.DisposeDataContext (context);
		}

		public void PreviewPrintableEntity()
		{
//?			var context   = this.Data.CreateDataContext ("PrintEngine:Print");
			var entity = this.GetPrintableEntity ();

			//	TODO: vérifier que cette logique est bien correcte; pour le moment, on doit partager les BusinessContext avec l'interface graphique, mais à terme il faudra probablement découpler cela...

			PrintEngine.PreviewCommand (this.BusinessContext, entity);

//?			this.Data.DisposeDataContext (context);
		}


		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.commandContext.DetachCommandHandler (this);

				this.browserViewController.DataSetSelected -= this.HandleBrowserViewControllerDataSetSelected;

				this.browserSettingsController.Dispose ();
				this.browserViewController.Dispose ();
				this.previewViewController.Dispose ();
			}

			base.Dispose (disposing);
		}

		
		private void CreateUIFrame(Widget container)
		{
			this.frame = new FrameBox
			{
				Parent = container,
				Dock = DockStyle.Fill,
				DrawFullFrame = false,
				DrawFrameEdges = FrameEdges.None
			};
			
			this.CreateUITopPanel ();
			this.CreateUISettingsPanel ();
			this.CreateUILeftPanel ();
			this.CreateUISplitter ();
			this.CreateUIMainPanel ();
			//?this.CreateUIRightPanels ();
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

		private void CreateUIMainPanel()
		{
			this.mainPanel = new FrameBox
			{
				Parent = this.frame,
				Name = "MainPanel",
				Dock = DockStyle.Fill,
				Padding = new Margins (0, 0, 0, 0),
			};
		}

		private void CreateUIRightPanels()
		{
			this.rightPreviewPanel = new FrameBox
			{
				Parent = this.frame,
				Name = "RightPreviewPanel",
				Dock = DockStyle.Right,
				Padding = new Margins (0, 0, 0, 0),
				Visibility = false,
				PreferredWidth = 400,
			};
			
			this.rightSplitter = new VSplitter
			{
				Parent = this.frame,
				Dock = DockStyle.Right,
				PreferredWidth = 8,
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

		private void UpdateBrowserSettingsPanel()
		{
			var expandedPanel = this.topPanel;
			var compactPanel  = this.browserViewController.TopPanel;

			var mode = this.browserSettingsMode;

			if (this.browserViewController.DataSetMetadata == null)
			{
				mode = BrowserSettingsMode.Disabled;
			}

			switch (mode)
			{
				case BrowserSettingsMode.Disabled:
				case BrowserSettingsMode.Hidden:
					expandedPanel.Visibility = false;
					compactPanel.Visibility  = false;
					this.browserSettingsPanel.Parent = null;
					break;

				case BrowserSettingsMode.Compact:
					expandedPanel.Visibility = false;
					compactPanel.Visibility  = true;
					this.browserSettingsPanel.Parent = compactPanel;
					break;

				case BrowserSettingsMode.Expanded:
					expandedPanel.Visibility = true;
					compactPanel.Visibility  = false;
					this.browserSettingsPanel.Parent = expandedPanel;
					break;

				default:
					throw new System.NotSupportedException (string.Format ("BrowserSettingsMode.{0} not supported", mode));
			}

			this.browserSettingsController.NotifyBrowserSettingsModeChanged (mode);
		}


		private IEnumerable<CoreViewController> GetCoreViewControllers()
		{
			return this.GetSubControllers ().Where (x => x is CoreViewController).Cast<CoreViewController> ();
		}


		private AbstractEntity GetPrintableEntity()
		{
			var entityKey = this.GetVisiblePersistedEntities ().Select (x => this.Data.FindEntityKey (x)).FirstOrDefault ();
			var context   = this.BusinessContext.DataContext;
			var entity    = context.ResolveEntity (entityKey);
			
			return entity;
		}
		
		private IEnumerable<AbstractEntity> GetVisiblePersistedEntities()
		{
			var leaf = this.DataViewController.GetLeafViewController ();

			if (leaf != null)
			{
				foreach (var node in leaf.GetControllerChain ().Select (x => x as EntityViewController))
				{
					if (node != null)
					{
						var entity = node.GetEntity ();
						var context = this.Data.DataContextPool.FindDataContext (entity);

						if ((context != null) &&
							(context.IsPersistent (entity)))
						{
							//	HACK
							//	TODO: remove this

							// HACK II
							// There was already a hack here but I needed to add one more. When the
							// Epsitec.Cresus.Core.Library and the Epsitec.Cresus.Core.Library.Business
							// projects where not clearly separated, the type BusinessDocumentEntity
							// was in the same project at this one. Now it is in another project and
							// is not accessible anymore. Therefore, we rely on the name of the type
							// for the check, instead of relying on the type itself. This works
							// because BusinessDocumentEntity has no descendant classes, so we can
							// get away by only checking the name of the type.That's ugly but since
							// this code is already a hack that shouldn't be here in the first place
							// (and that it will hopefully be removed once) there's no point in
							// making something clean right here. A real fix would be to remove all
							// this code and replacing it with something dynamic, like plugins or a
							// subclass or whathever. But I don't know what Pierre intends to do
							// with that...

							if (entity.GetType ().Name == "BusinessDocumentEntity")
							{
								DocumentMetadataEntity example = new DocumentMetadataEntity ();
								example.BusinessDocument = entity as AbstractDocumentEntity;
								example.IsArchive = false;
								yield return context.GetByExample<DocumentMetadataEntity> (example).FirstOrDefault ();
							}
							yield return entity;
						}
					}
				}
			}
		}


		public IEnumerable<AbstractEntity> GetVisibleEntities()
		{
			var leaf = this.DataViewController.GetLeafViewController ();

			if (leaf != null)
			{
				foreach (var node in leaf.GetControllerChain ().Select (x => x as EntityViewController))
				{
					if (node != null)
					{
						yield return node.GetEntity ();
					}
				}
			}
		}


		private void HandleBrowserViewControllerDataSetSelected(object sender)
		{
			this.UpdateBrowserSettingsPanel ();
		}

		public bool GetPrintCommandEnable()
		{
			return PrintEngine.CheckPrintCommandEnable (this.BusinessContext, this.GetPrintableEntity ());
		}

		#region ICommandHandler Members

		void ICommandHandler.UpdateCommandStates(object sender)
		{
		}

		#endregion

		#region Factory Class

		private sealed class Factory : Epsitec.Cresus.Core.Factories.DefaultViewControllerComponentFactory<MainViewController>
		{
		}

		#endregion


		private readonly CommandContext				commandContext;
		private readonly BrowserViewController		browserViewController;
		private readonly BrowserSettingsController	browserSettingsController;
		private readonly PreviewViewController		previewViewController;

		private FrameBox							frame;
		private FrameBox							topPanel;
		private FrameBox							browserSettingsPanel;
		private FrameBox							leftPanel;
		private VSplitter							splitter;
		private VSplitter							rightSplitter;
		private FrameBox							mainPanel;
		private FrameBox							rightPreviewPanel;
		private BrowserSettingsMode					browserSettingsMode;
	}
}

﻿//	Copyright © 2010, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Pierre ARNAUD, Maintainer: Pierre ARNAUD

using Epsitec.Common.Drawing;
using Epsitec.Common.Widgets;

using Epsitec.Cresus.Core.Widgets;

using System.Collections.Generic;
using System.Linq;
using Epsitec.Cresus.Core.Orchestrators;

namespace Epsitec.Cresus.Core.Controllers
{
	class RibbonViewController : CoreViewController
	{
		public RibbonViewController(DataViewOrchestrator orchestrator)
			: base ("Ribbon", orchestrator)
		{
			CoreProgram.Application.UserManager.AuthenticatedUserChanged += this.HandleAuthenticatedUserChanged;
		}

		public override IEnumerable<CoreController> GetSubControllers()
		{
			yield break;
		}

		public override void CreateUI(Widget container)
		{
			base.CreateUI (container);
			this.CreateRibbonBook (container);
			this.CreateRibbonHomePage ();
		}


		private void UpdateAuthenticatedUser()
		{
			//	Met à jour le nom de l'utilisateur dans le ruban.
			Entities.SoftwareUserEntity user = CoreProgram.Application.UserManager.AuthenticatedUser;

			if (user == null)
			{
				this.authenticateUserWidget.Text = null;

				ToolTip.Default.HideToolTipForWidget (this.authenticateUserWidget);
			}
			else
			{
				this.authenticateUserWidget.Text = string.Concat ("<font size=\"9\">", user.LoginName, "</font>");

				ToolTip.Default.SetToolTip (this.authenticateUserWidget, user.ShortDescription);
			}
		}

		
		private void CreateRibbonBook(Widget container)
		{
			this.ribbonBook = new RibbonBook ()
			{
				Parent = container,
				Dock = DockStyle.Fill,
				Name = "Ribbon"
			};

			CoreProgram.Application.PersistanceManager.Register (this.ribbonBook);
		}

		private static RibbonPage CreateRibbonPage(RibbonBook book, string name, string title)
		{
			return new RibbonPage (book)
			{
				Name = name,
				RibbonTitle = title,
				PreferredHeight = 78,
			};
		}

		private void CreateRibbonHomePage()
		{
			this.ribbonPageHome = RibbonViewController.CreateRibbonPage (this.ribbonBook, "Home", "Principal");

			this.CreateRibbonEditSection ();
			this.CreateRibbonClipboardSection ();
			this.CreateRibbonFontSection ();
			this.CreateRibbonDatabaseSection ();
			this.CreateRibbonStateSection ();
			this.CreateRibbonSettingsSection ();
			this.CreateRibbonNavigationSection ();
		}

		private void CreateRibbonEditSection()
		{
			var section = new RibbonSection (this.ribbonPageHome)
			{
				Name = "Edit",
				Title = "Édition",
				ContainerLayoutMode = ContainerLayoutMode.HorizontalFlow,
				PreferredWidth = RibbonViewController.GetButtonWidth (RibbonViewController.buttonLargeWidth) * 4 +
								 RibbonViewController.GetButtonWidth (RibbonViewController.buttonSmallWidth) * 1,
			};

			section.Children.Add (RibbonViewController.CreateButton (Res.Commands.Edition.SaveRecord));
			section.Children.Add (RibbonViewController.CreateButton (Res.Commands.Edition.DiscardRecord));
			section.Children.Add (RibbonViewController.CreateButton (Res.Commands.Edition.Print));
			section.Children.Add (RibbonViewController.CreateButton (Res.Commands.Edition.Preview));

			var frame = new FrameBox
			{
				Parent = section,
				Dock = DockStyle.StackBegin,
				ContainerLayoutMode = ContainerLayoutMode.VerticalFlow,
				PreferredWidth = RibbonViewController.GetButtonWidth (RibbonViewController.buttonSmallWidth),
			};

			frame.Children.Add (RibbonViewController.CreateButton (Res.Commands.File.ImportV11, dx: RibbonViewController.buttonSmallWidth));
		}

		private void CreateRibbonClipboardSection()
		{
			var section = new RibbonSection (this.ribbonPageHome)
			{
				Name = "Clipboard",
				Title = "Presse-papier",
				ContainerLayoutMode = ContainerLayoutMode.HorizontalFlow,
				PreferredWidth = RibbonViewController.GetButtonWidth (RibbonViewController.buttonSmallWidth) + RibbonViewController.GetButtonWidth (RibbonViewController.buttonLargeWidth),
			};

			var frame = new FrameBox
			{
				Parent = section,
				Dock = DockStyle.StackBegin,
				ContainerLayoutMode = ContainerLayoutMode.VerticalFlow,
				PreferredWidth = RibbonViewController.GetButtonWidth (RibbonViewController.buttonSmallWidth),
			};

			frame.Children.Add (RibbonViewController.CreateButton (ApplicationCommands.Cut,  dx: RibbonViewController.buttonSmallWidth));
			frame.Children.Add (RibbonViewController.CreateButton (ApplicationCommands.Copy, dx: RibbonViewController.buttonSmallWidth));

			section.Children.Add (RibbonViewController.CreateButton (ApplicationCommands.Paste));
		}

		private void CreateRibbonFontSection()
		{
			var section = new RibbonSection (this.ribbonPageHome)
			{
				Name = "Font",
				Title = "Police",
				ContainerLayoutMode = ContainerLayoutMode.HorizontalFlow,
				PreferredWidth = RibbonViewController.GetButtonWidth (RibbonViewController.buttonSmallWidth) * 3,
			};

			var frame = new FrameBox
			{
				Parent = section,
				Dock = DockStyle.StackBegin,
				ContainerLayoutMode = ContainerLayoutMode.VerticalFlow,
				PreferredWidth = section.PreferredWidth,
			};

			var topFrame = new FrameBox
			{
				Parent = frame,
				Dock = DockStyle.StackBegin,
				ContainerLayoutMode = ContainerLayoutMode.HorizontalFlow,
				PreferredWidth = section.PreferredWidth,
			};

			var bottomFrame = new FrameBox
			{
				Parent = frame,
				Dock = DockStyle.StackBegin,
				ContainerLayoutMode = ContainerLayoutMode.HorizontalFlow,
				PreferredWidth = section.PreferredWidth,
			};

			topFrame.Children.Add (RibbonViewController.CreateButton (ApplicationCommands.Bold,       dx: RibbonViewController.buttonSmallWidth, isActivable: true));
			topFrame.Children.Add (RibbonViewController.CreateButton (ApplicationCommands.Italic,     dx: RibbonViewController.buttonSmallWidth, isActivable: true));
			topFrame.Children.Add (RibbonViewController.CreateButton (ApplicationCommands.Underlined, dx: RibbonViewController.buttonSmallWidth, isActivable: true));

			//?bottomFrame.Children.Add (RibbonViewController.CreateButton (ApplicationCommands.Subscript,   dx: RibbonViewController.buttonSmallWidth, isActivable: true));
			//?bottomFrame.Children.Add (RibbonViewController.CreateButton (ApplicationCommands.Superscript, dx: RibbonViewController.buttonSmallWidth, isActivable: true));
		}

		private void CreateRibbonDatabaseSection()
		{
			var section = new RibbonSection (this.ribbonPageHome)
			{
				Name = "Database",
				Title = "Bases de données",
				ContainerLayoutMode = ContainerLayoutMode.HorizontalFlow,
				PreferredWidth = RibbonViewController.GetButtonWidth (RibbonViewController.buttonLargeWidth) * 3,
				Dock = DockStyle.Fill,
			};

			section.Children.Add (RibbonViewController.CreateButton (Res.Commands.Base.ShowCustomers));
			section.Children.Add (RibbonViewController.CreateButton (Res.Commands.Base.ShowArticleDefinitions));
			section.Children.Add (RibbonViewController.CreateButton (Res.Commands.Base.ShowDocuments));
			section.Children.Add (RibbonViewController.CreateButton (Res.Commands.Base.ShowBusinessSettings));
			section.Children.Add (RibbonViewController.CreateButton (Res.Commands.Base.ShowWorkflowDefinitions));
			//?section.Children.Add (RibbonViewController.CreateButton (Res.Commands.Base.ShowInvoiceDocuments));
		}

		private void CreateRibbonStateSection()
		{
#if false
			var section = new RibbonSection (this.ribbonPageHome)
			{
				Name = "State",
				Title = "États",
				Dock = DockStyle.Fill,
				ContainerLayoutMode = ContainerLayoutMode.HorizontalFlow
			};
#endif
		}

		private void CreateRibbonNavigationSection()
		{
			var section = new RibbonSection (this.ribbonPageHome)
			{
				Name = "Navigation",
				Title = "Navigation",
				PreferredWidth = RibbonViewController.GetButtonWidth (RibbonViewController.buttonLargeWidth) * 2,
				Dock = DockStyle.Right,
				ContainerLayoutMode = ContainerLayoutMode.HorizontalFlow
			};

			section.Children.Add (RibbonViewController.CreateButton (Res.Commands.History.NavigateBackward));
			section.Children.Add (RibbonViewController.CreateButton (Res.Commands.History.NavigateForward));
		}

		private void CreateRibbonSettingsSection()
		{
			var section = new RibbonSection (this.ribbonPageHome)
			{
				Name = "Settings",
				Title = "Réglages",
				ContainerLayoutMode = ContainerLayoutMode.HorizontalFlow,
				Dock = DockStyle.Right,
				PreferredWidth = RibbonViewController.GetButtonWidth (RibbonViewController.buttonLargeWidth) * 3,
			};

			{
				var frame1 = new FrameBox
				{
					Parent = section,
					Dock = DockStyle.StackBegin,
					ContainerLayoutMode = ContainerLayoutMode.VerticalFlow,
					PreferredWidth = 21,
				};

				var frame2 = new FrameBox
				{
					Parent = section,
					Dock = DockStyle.StackBegin,
					ContainerLayoutMode = ContainerLayoutMode.VerticalFlow,
					PreferredWidth = 21,
				};

				//	HACK: faire cela proprement avec des commandes multi-états

				var selectLanaugage1 = new IconButton ()
				{
					Parent = frame1,
					Name = "language=fr",
					Dock = DockStyle.Stacked,
					Text = @"<img src=""manifest:Epsitec.Common.Widgets.Images.Flags.FlagFR.icon""/>",
					ActiveState = ActiveState.Yes,
				};

				var selectLanaugage2 = new IconButton ()
				{
					Parent = frame1,
					Name = "language=de",
					Dock = DockStyle.Stacked,
					Text = @"<img src=""manifest:Epsitec.Common.Widgets.Images.Flags.FlagDE.icon""/>",
				};

				var selectLanaugage3 = new IconButton ()
				{
					Parent = frame2,
					Name = "language=en",
					Dock = DockStyle.Stacked,
					Text = @"<img src=""manifest:Epsitec.Common.Widgets.Images.Flags.FlagGB.icon""/>",
				};

				var selectLanaugage4 = new IconButton ()
				{
					Parent = frame2,
					Name = "language=it",
					Dock = DockStyle.Stacked,
					Text = @"<img src=""manifest:Epsitec.Common.Widgets.Images.Flags.FlagIT.icon""/>",
				};

				selectLanaugage1.Clicked += delegate
				{
					UI.Settings.CultureForData.SelectLanguage (null);
					selectLanaugage1.ActiveState = ActiveState.Yes;
					selectLanaugage2.ActiveState = ActiveState.No;
					selectLanaugage3.ActiveState = ActiveState.No;
					selectLanaugage4.ActiveState = ActiveState.No;
				};

				selectLanaugage2.Clicked += delegate
				{
					UI.Settings.CultureForData.SelectLanguage ("de");
					selectLanaugage1.ActiveState = ActiveState.No;
					selectLanaugage2.ActiveState = ActiveState.Yes;
					selectLanaugage3.ActiveState = ActiveState.No;
					selectLanaugage4.ActiveState = ActiveState.No;
				};

				selectLanaugage3.Clicked += delegate
				{
					UI.Settings.CultureForData.SelectLanguage ("en");
					selectLanaugage1.ActiveState = ActiveState.No;
					selectLanaugage2.ActiveState = ActiveState.No;
					selectLanaugage3.ActiveState = ActiveState.Yes;
					selectLanaugage4.ActiveState = ActiveState.No;
				};

				selectLanaugage4.Clicked += delegate
				{
					UI.Settings.CultureForData.SelectLanguage ("it");
					selectLanaugage1.ActiveState = ActiveState.No;
					selectLanaugage2.ActiveState = ActiveState.No;
					selectLanaugage3.ActiveState = ActiveState.No;
					selectLanaugage4.ActiveState = ActiveState.Yes;
				};
			}

			section.Children.Add (RibbonViewController.CreateButton (ApplicationCommands.MultilingualEdition));
			section.Children.Add (RibbonViewController.CreateButton (Res.Commands.Global.ShowSettings));

			{
				var frame = new FrameBox
				{
					Parent = section,
					Dock = DockStyle.StackBegin,
					ContainerLayoutMode = ContainerLayoutMode.VerticalFlow,
					PreferredWidth = RibbonViewController.GetButtonWidth (RibbonViewController.buttonLargeWidth),
				};

				frame.Children.Add (RibbonViewController.CreateButton (Res.Commands.Global.ShowUserManager));

				//	Le widget 'authenticateUserWidget' déborde volontairement sur le bas du bouton 'ShowUserManager',
				//	pour permettre d'afficher un nom d'utilisateur lisible.
				this.authenticateUserWidget = new StaticText
				{
					Parent = frame,
					ContentAlignment = Common.Drawing.ContentAlignment.MiddleCenter,
					PreferredHeight = 6+6,
					PreferredWidth = RibbonViewController.GetButtonWidth (RibbonViewController.buttonLargeWidth),
					Dock = DockStyle.Stacked,
					Margins = new Margins (0, 0, -6, 0),
				};
			}
		}


		private void HandleAuthenticatedUserChanged(object sender)
		{
			this.UpdateAuthenticatedUser ();
		}

		
		private static IconButton CreateButton(Command command, DockStyle dockStyle = DockStyle.StackBegin, CommandEventHandler handler = null, int? dx = null, bool isActivable = false)
		{
			if (handler != null)
			{
				CoreProgram.Application.CommandDispatcher.Register (command, handler);
			}

			if (!dx.HasValue)
			{
				dx = RibbonViewController.buttonLargeWidth;
			}

			double buttonWidth = RibbonViewController.GetButtonWidth (dx.Value);

			if (isActivable)
			{
				return new IconButton
				{
					CommandObject = command,
					PreferredIconSize = new Size (dx.Value, dx.Value),
					PreferredSize = new Size (buttonWidth, buttonWidth),
					Dock = dockStyle,
					Name = command.Name,
					VerticalAlignment = VerticalAlignment.Top,
					HorizontalAlignment = HorizontalAlignment.Center,
					AutoFocus = false,
				};
			}
			else
			{
				return new RibbonIconButton
				{
					CommandObject = command,
					PreferredIconSize = new Size (dx.Value, dx.Value),
					PreferredSize = new Size (buttonWidth, buttonWidth),
					Dock = dockStyle,
					Name = command.Name,
					VerticalAlignment = VerticalAlignment.Top,
					HorizontalAlignment = HorizontalAlignment.Center,
					AutoFocus = false,
				};
			}
		}
		
		private static double GetButtonWidth(int dx)
		{
			return 2 * ((dx + 1) / 2 + 5);
		}


		private static readonly int buttonSmallWidth = 14;
		private static readonly int buttonLargeWidth = 31;


		private RibbonBook						ribbonBook;
		private RibbonPage						ribbonPageHome;
		private StaticText						authenticateUserWidget;
	}
}

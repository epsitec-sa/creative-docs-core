﻿//	Copyright © 2010, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Pierre ARNAUD, Maintainer: Pierre ARNAUD

using Epsitec.Common.Drawing;
using Epsitec.Common.Widgets;

using Epsitec.Cresus.Core.Library;
using Epsitec.Cresus.Core.Orchestrators;
using Epsitec.Cresus.Core.Widgets;
using Epsitec.Cresus.Core.Entities;
using Epsitec.Cresus.Core.Business;
using Epsitec.Cresus.Core.Business.UserManagement;

using System.Collections.Generic;
using System.Linq;

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
			var user = CoreProgram.Application.UserManager.AuthenticatedUser;

			if (user.IsNull ())
			{
				this.authenticateUserButton.ImageEntity = null;
				this.authenticateUserWidget.Text = null;

				ToolTip.Default.HideToolTipForWidget (this.authenticateUserButton);
				ToolTip.Default.HideToolTipForWidget (this.authenticateUserWidget);
			}
			else
			{
				this.authenticateUserButton.ImageEntity = user.Person.Photo;
				this.authenticateUserWidget.Text = string.Concat ("<font size=\"9\">", user.LoginName, "</font>");

				ToolTip.Default.SetToolTip (this.authenticateUserButton, user.ShortDescription);
				ToolTip.Default.SetToolTip (this.authenticateUserWidget, user.ShortDescription);
			}

			this.UdpateDatabaseButtonsForUser ();
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

			this.CreateRibbonUserSection ();
			this.CreateRibbonEditSection ();
			this.CreateRibbonClipboardSection ();
			this.CreateRibbonFontSection ();
			this.CreateRibbonDatabaseSection ();
			this.CreateRibbonStateSection ();
			this.CreateRibbonSettingsSection ();
			this.CreateRibbonNavigationSection ();
		}

		private void CreateRibbonUserSection()
		{
			var section = new RibbonSection (this.ribbonPageHome)
			{
				Name = "User",
				Title = "Identité",
				ContainerLayoutMode = ContainerLayoutMode.HorizontalFlow,
				PreferredWidth = Misc.GetButtonWidth (large: true) * 1,
			};

			{
				var frame = new FrameBox
				{
					Parent = section,
					Dock = DockStyle.StackBegin,
					ContainerLayoutMode = ContainerLayoutMode.VerticalFlow,
					PreferredWidth = Misc.GetButtonWidth (large: true),
				};

				this.authenticateUserButton = RibbonViewController.CreateIconOrImageButton (Res.Commands.Global.ShowUserManager);
				this.authenticateUserButton.CoreData = this.Orchestrator.Data;
				this.authenticateUserButton.IconUri = Misc.GetResourceIconUri ("UserManager");
				this.authenticateUserButton.IconPreferredSize = new Size (31, 31);

				frame.Children.Add (this.authenticateUserButton);

				//	Le widget 'authenticateUserWidget' déborde volontairement sur le bas du bouton 'ShowUserManager',
				//	pour permettre d'afficher un nom d'utilisateur lisible.
				this.authenticateUserWidget = new StaticText
				{
					Parent = frame,
					ContentAlignment = Common.Drawing.ContentAlignment.MiddleCenter,
					PreferredHeight = 12,
					PreferredWidth = Misc.GetButtonWidth (large: true),
					Dock = DockStyle.Stacked,
					Margins = new Margins (0, 0, -2, 0),
				};
			}
		}

		private void CreateRibbonEditSection()
		{
			var section = new RibbonSection (this.ribbonPageHome)
			{
				Name = "Edit",
				Title = "Édition",
				ContainerLayoutMode = ContainerLayoutMode.HorizontalFlow,
				PreferredWidth = Misc.GetButtonWidth (large: true) * 4 +
								 Misc.GetButtonWidth (large: false) * 1,
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
				PreferredWidth = Misc.GetButtonWidth (large: false),
			};

			frame.Children.Add (RibbonViewController.CreateButton (Res.Commands.File.ImportV11, large: false));
		}

		private void CreateRibbonClipboardSection()
		{
			var section = new RibbonSection (this.ribbonPageHome)
			{
				Name = "Clipboard",
				Title = "Presse-papier",
				ContainerLayoutMode = ContainerLayoutMode.HorizontalFlow,
				PreferredWidth = Misc.GetButtonWidth (large: false) + Misc.GetButtonWidth (large: true),
			};

			var frame = new FrameBox
			{
				Parent = section,
				Dock = DockStyle.StackBegin,
				ContainerLayoutMode = ContainerLayoutMode.VerticalFlow,
				PreferredWidth = Misc.GetButtonWidth (large: false),
			};

			frame.Children.Add (RibbonViewController.CreateButton (ApplicationCommands.Cut,  large: false));
			frame.Children.Add (RibbonViewController.CreateButton (ApplicationCommands.Copy, large: false));

			section.Children.Add (RibbonViewController.CreateButton (ApplicationCommands.Paste));
		}

		private void CreateRibbonFontSection()
		{
			var section = new RibbonSection (this.ribbonPageHome)
			{
				Name = "Font",
				Title = "Police",
				ContainerLayoutMode = ContainerLayoutMode.HorizontalFlow,
				PreferredWidth = Misc.GetButtonWidth (large: false) * 3,
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

			topFrame.Children.Add (RibbonViewController.CreateButton (ApplicationCommands.Bold,       large: false, isActivable: true));
			topFrame.Children.Add (RibbonViewController.CreateButton (ApplicationCommands.Italic,     large: false, isActivable: true));
			topFrame.Children.Add (RibbonViewController.CreateButton (ApplicationCommands.Underlined, large: false, isActivable: true));

			//?bottomFrame.Children.Add (RibbonViewController.CreateButton (ApplicationCommands.Subscript,   large: false, isActivable: true));
			//?bottomFrame.Children.Add (RibbonViewController.CreateButton (ApplicationCommands.Superscript, large: false, isActivable: true));
		}

		private void CreateRibbonDatabaseSection()
		{
			var section = new RibbonSection (this.ribbonPageHome)
			{
				Name = "Database",
				Title = "Bases de données",
				ContainerLayoutMode = ContainerLayoutMode.HorizontalFlow,
				PreferredWidth = Misc.GetButtonWidth (large: true) * 3,
				Dock = DockStyle.Fill,
			};

			//	Place les boutons pour les bases de données les plus courantes.
			section.Children.Add (RibbonViewController.CreateButton (Res.Commands.Base.ShowCustomers));
			section.Children.Add (RibbonViewController.CreateButton (Res.Commands.Base.ShowArticleDefinitions));
			section.Children.Add (RibbonViewController.CreateButton (Res.Commands.Base.ShowDocuments));

			//	Place le bouton 'magique' qui donne accès aux bases de données d'usage moins fréquent.
			double buttonWidth = Misc.GetButtonWidth (large: true);

			var group = new FrameBox ()
			{
				PreferredSize = new Size (buttonWidth, buttonWidth+11-1),
				Dock = DockStyle.StackBegin,
				VerticalAlignment = VerticalAlignment.Top,
				HorizontalAlignment = HorizontalAlignment.Center,
			};

			this.databaseMenuButton = new GlyphButton()
			{
				Parent = group,
				ButtonStyle = ButtonStyle.ComboItem,
				GlyphShape = GlyphShape.Menu,
				AutoFocus = false,
				PreferredSize = new Size (buttonWidth, 11),
				Dock = DockStyle.Bottom,
				Margins = new Margins (0, 0, -1, 0),
			};

			ToolTip.Default.SetToolTip (this.databaseMenuButton, "Montre une autre base de données...");

			this.databaseButton = RibbonViewController.CreateButton (null);
			this.databaseButton.Parent = group;
			this.databaseButton.PreferredSize = new Size (buttonWidth, buttonWidth);
			this.databaseButton.Dock = DockStyle.Fill;

			section.Children.Add (group);

			this.databaseMenuButton.Clicked += delegate
			{
				this.ShowDatabasesMenu (this.databaseMenuButton);
			};

			this.databaseMenuButton.Entered += delegate
			{
				this.databaseButton.ButtonStyle = ButtonStyle.Combo;
			};

			this.databaseMenuButton.Exited += delegate
			{
				this.databaseButton.ButtonStyle = ButtonStyle.ToolItem;
			};

			var databaseCommandHandler = RibbonViewController.DatabaseCommandHandler;
			databaseCommandHandler.Changed += delegate
			{
				this.UpdateDatabaseButton ();
			};

			this.UdpateDatabaseButtonsForUser ();
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
				PreferredWidth = Misc.GetButtonWidth (large: true) * 2,
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
				PreferredWidth = Misc.GetButtonWidth (large: true) * 3,
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
		}


		#region Databases manager
		private void UdpateDatabaseButtonsForUser()
		{
			//	Met à jour les boutons pour les bases de données d'usage peu fréquent, après un changement d'utilisateur.
			var list = RibbonViewController.MenuDatabaseCommands;
			int count = list.Count ();

			this.databaseButton.Visibility = (count > 0);
			this.databaseMenuButton.Visibility = (count > 1);

			if (count > 0)
			{
				this.databaseButton.CommandObject = list.First ();
				this.UpdateDatabaseButton ();
			}
		}

		private void ShowDatabasesMenu(Widget parentButton)
		{
			//	Construit puis affiche le menu des bases de données d'usage peu fréquent.
			var menu = new VMenu ();

			foreach (var command in RibbonViewController.MenuDatabaseCommands)
			{
				RibbonViewController.AddDatabaseToMenu (menu, command);
			}

			TextFieldCombo.AdjustComboSize (parentButton, menu, false);
			menu.Host = this.ribbonBook;
			menu.ShowAsComboList (parentButton, Point.Zero, parentButton);
		}

		private static void AddDatabaseToMenu(VMenu menu, Command command)
		{
			var item = new MenuItem ()
			{
				CommandObject = command,
				Name = command.Name,
			};

			menu.Items.Add (item);
		}

		private void UpdateDatabaseButton()
		{
			//	Met à jour le bouton qui surplombe le bouton du menu, en fonction de la base sélectionnée.
			var selectedCommand = RibbonViewController.SelectedDatabaseCommand;

			if (selectedCommand != null)
			{
				this.databaseButton.CommandObject = selectedCommand;
			}
		}

		private static Command SelectedDatabaseCommand
		{
			//	Retourne la commande correspondant à la base sélectionnée.
			get
			{
				var name = RibbonViewController.SelectedDatabaseCommandName;
				return RibbonViewController.MenuDatabaseCommands.Where (x => x.Name == name).FirstOrDefault ();
			}
		}

		private static string SelectedDatabaseCommandName
		{
			//	Retourne le nom de la commande correspondant à la base sélectionnée.
			get
			{
				var databaseCommandHandler = RibbonViewController.DatabaseCommandHandler;
				return databaseCommandHandler.SelectedDatabaseCommandName;
			}
		}

		private static CommandHandlers.DatabaseCommandHandler DatabaseCommandHandler
		{
			get
			{
				return CoreProgram.Application.Commands.CommandHandlers
					.Where (x => x is CommandHandlers.DatabaseCommandHandler)
					.FirstOrDefault () as CommandHandlers.DatabaseCommandHandler;
			}
		}

		private static IEnumerable<Command> MenuDatabaseCommands
		{
			//	Liste des commandes des bases de données d'usage peu fréquent accessibles via le menu.
			//	Cette liste dépend de l'utilisateur identifié. Elle peut très bien être vide.
			get
			{
				bool admin = CoreProgram.Application.UserManager.IsAuthenticatedUserAtPowerLevel (UserPowerLevel.Administrator);
				bool devel = CoreProgram.Application.UserManager.IsAuthenticatedUserAtPowerLevel (UserPowerLevel.Developer);
				bool power = CoreProgram.Application.UserManager.IsAuthenticatedUserAtPowerLevel (UserPowerLevel.PowerUser);

				if (admin || devel || power)
				{
					yield return Res.Commands.Base.ShowImages;
				}

				if (admin || devel)
				{
					yield return Res.Commands.Base.ShowImageBlobs;
				}

				if (admin || devel)
				{
					yield return Res.Commands.Base.ShowBusinessSettings;
				}

				if (devel)
				{
					yield return Res.Commands.Base.ShowWorkflowDefinitions;
				}
			}
		}
		#endregion


		private void HandleAuthenticatedUserChanged(object sender)
		{
			this.UpdateAuthenticatedUser ();
		}

		
		private static IconButton CreateButton(Command command, DockStyle dockStyle = DockStyle.StackBegin, CommandEventHandler handler = null, bool large = true, bool isActivable = false)
		{
			if (handler != null)
			{
				CoreProgram.Application.CommandDispatcher.Register (command, handler);
			}

			double buttonWidth = Misc.GetButtonWidth (large);
			int width = large ? Misc.buttonLargeWidth : Misc.buttonSmallWidth;

			if (isActivable)
			{
				return new IconButton
				{
					CommandObject       = command,
					PreferredIconSize   = new Size (width, width),
					PreferredSize       = new Size (buttonWidth, buttonWidth),
					Dock                = dockStyle,
					Name                = (command == null) ? null : command.Name,
					VerticalAlignment   = VerticalAlignment.Top,
					HorizontalAlignment = HorizontalAlignment.Center,
					AutoFocus           = false,
				};
			}
			else
			{
				return new RibbonIconButton
				{
					CommandObject       = command,
					PreferredIconSize   = new Size (width, width),
					PreferredSize       = new Size (buttonWidth, buttonWidth),
					Dock                = dockStyle,
					Name                = (command == null) ? null : command.Name,
					VerticalAlignment   = VerticalAlignment.Top,
					HorizontalAlignment = HorizontalAlignment.Center,
					AutoFocus           = false,
				};
			}
		}

		private static IconOrImageButton CreateIconOrImageButton(Command command)
		{
			double buttonWidth = Misc.GetButtonWidth (large: true);

			var button = new IconOrImageButton
			{
				CommandObject       = command,
				PreferredSize       = new Size (buttonWidth, buttonWidth),
				Dock                = DockStyle.StackBegin,
				Name                = (command == null) ? null : command.Name,
				VerticalAlignment   = VerticalAlignment.Top,
				HorizontalAlignment = HorizontalAlignment.Center,
				AutoFocus           = false,
			};

			return button;
		}
		

		private RibbonBook						ribbonBook;
		private RibbonPage						ribbonPageHome;

		private IconButton						databaseButton;
		private GlyphButton						databaseMenuButton;

		private IconOrImageButton				authenticateUserButton;
		private StaticText						authenticateUserWidget;
	}
}

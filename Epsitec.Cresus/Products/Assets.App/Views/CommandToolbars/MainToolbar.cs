﻿//	Copyright © 2013, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Daniel ROUX, Maintainer: Daniel ROUX

using System.Collections.Generic;
using System.Linq;

using Epsitec.Common.Widgets;
using Epsitec.Cresus.Assets.App.Helpers;
using Epsitec.Cresus.Assets.App.Popups;
using Epsitec.Cresus.Assets.App.Widgets;
using Epsitec.Cresus.Assets.Server.SimpleEngine;
using Epsitec.Common.Support;
using Epsitec.Common.Drawing;

namespace Epsitec.Cresus.Assets.App.Views.CommandToolbars
{
	public class MainToolbar : AbstractCommandToolbar
	{
		public MainToolbar(DataAccessor accessor, CommandContext commandContext)
			: base (accessor, commandContext)
		{
		}


		public ViewType							ViewType
		{
			get
			{
				return this.viewType;
			}
			set
			{
				if (this.viewType != value)
				{
					this.viewType = value;
					this.UpdateViewTypeCommands ();
					this.UpdateViewModeCommands ();
				}
			}
		}

		public ViewMode							ViewMode
		{
			get
			{
				return this.viewMode;
			}
			set
			{
				if (this.viewMode != value)
				{
					this.viewMode = value;
					this.UpdateViewModeCommands ();
				}
			}
		}

		public int								Simulation
		{
			get
			{
				return this.simulation;
			}
			set
			{
				if (this.simulation != value)
				{
					this.simulation = value;
					this.UpdateSimulation ();
				}
			}
		}

		public int								WarningsRedDotCount
		{
			set
			{
				if (this.buttonWarnings != null)
				{
					this.buttonWarnings.RedDotCount = value;
				}
			}
		}


		public override void CreateUI(Widget parent)
		{
			base.CreateUI (parent);

			this.toolbar.PreferredHeight = AbstractCommandToolbar.primaryToolbarHeight;

			this.viewType = ViewType.Assets;
			this.viewMode = ViewMode.Single;
			this.simulation = 0;

			this.CreateButton (DockStyle.Left, Res.Commands.Main.New);
			this.CreateButton (DockStyle.Left, Res.Commands.Main.Navigate.Back);
			this.CreateButton (DockStyle.Left, Res.Commands.Main.Navigate.Forward);
			this.CreateButton (DockStyle.Left, Res.Commands.Main.Navigate.Menu);

			this.CreateButton (DockStyle.Left, Res.Commands.View.Assets);
			this.CreateButton (DockStyle.Left, Res.Commands.View.Amortizations);
			this.CreateButton (DockStyle.Left, Res.Commands.View.Entries);
			this.CreateButton (DockStyle.Left, Res.Commands.View.Categories);
			this.CreateButton (DockStyle.Left, Res.Commands.View.Groups);
			this.CreateButton (DockStyle.Left, Res.Commands.View.Persons);
			this.CreateButton (DockStyle.Left, Res.Commands.View.Reports);
			this.buttonWarnings =
			this.CreateButton (DockStyle.Left, Res.Commands.View.Warnings);
			this.CreateButton (DockStyle.Left, Res.Commands.View.Settings);

			this.CreateSajex (10);

			this.CreateButton (DockStyle.Left, Res.Commands.ViewMode.Single);
			this.CreateButton (DockStyle.Left, Res.Commands.ViewMode.Event);
			this.CreateButton (DockStyle.Left, Res.Commands.ViewMode.Multiple);

			this.CreateSajex (40);

			this.CreateButton (DockStyle.Left, Res.Commands.Main.Edit);
			this.CreateButton (DockStyle.Left, Res.Commands.Main.Locked);
			this.buttonSimulation =
			this.CreateButton (DockStyle.Left, Res.Commands.Main.Simulation);

			this.CreateButton (DockStyle.Right, Res.Commands.Edit.Cancel);
			this.CreateButton (DockStyle.Right, Res.Commands.Edit.Accept);

			this.UpdateViewTypeCommands ();
			this.UpdateViewModeCommands ();
			this.UpdateSimulation ();
		}


		private void CreateSajex(int width)
		{
			new FrameBox
			{
				Parent        = this.toolbar,
				Dock          = DockStyle.Left,
				PreferredSize = new Size (width, this.toolbar.PreferredHeight),
			};
		}

		//?[Command (Res.CommandIds.View.Settings)]
		//?void CommandToto(CommandDispatcher dispatcher, CommandEventArgs e)
		//?{
		//?	var target = e.Source as Widget;
		//?	var pos = e.CommandMessage.Cursor;  // position absolue dans la fenêtre
		//?
		//?	CreateAssetPopup.Show (target, this.accessor, null);
		//?
		//?	var cs = this.commandContext.GetCommandState (Res.Commands.View.Settings);
		//?	//?cs.Enable = false;
		//?
		//?	if (cs.ActiveState == ActiveState.Yes)
		//?	{
		//?		cs.ActiveState = ActiveState.No;
		//?	}
		//?	else
		//?	{
		//?		cs.ActiveState = ActiveState.Yes;
		//?	}
		//?}

		[Command (Res.CommandIds.View.Assets)]
		[Command (Res.CommandIds.View.Amortizations)]
		[Command (Res.CommandIds.View.Entries)]
		[Command (Res.CommandIds.View.Categories)]
		[Command (Res.CommandIds.View.Groups)]
		[Command (Res.CommandIds.View.Persons)]
		[Command (Res.CommandIds.View.Reports)]
		[Command (Res.CommandIds.View.Warnings)]
		[Command (Res.CommandIds.View.AssetsSettings)]
		[Command (Res.CommandIds.View.PersonsSettings)]
		[Command (Res.CommandIds.View.Accounts)]
		private void OnView(CommandDispatcher dispatcher, CommandEventArgs e)
		{
			this.ViewType = ViewType.FromDefaultKind (this.accessor, MainToolbar.GetViewKind (e.Command));
			this.OnChangeView ();
		}

		[Command (Res.CommandIds.View.Settings)]
		private void OnViewSettings(CommandDispatcher dispatcher, CommandEventArgs e)
		{
			var target = this.GetTarget (e);
			this.ShowViewPopup (target);
		}

		[Command (Res.CommandIds.ViewMode.Single)]
		[Command (Res.CommandIds.ViewMode.Event)]
		[Command (Res.CommandIds.ViewMode.Multiple)]
		private void OnViewMode(CommandDispatcher dispatcher, CommandEventArgs e)
		{
			this.ViewMode = MainToolbar.GetViewMode (e.Command);
			this.OnChangeView ();
		}


		private void UpdateViewTypeCommands()
		{
			//	"Allume" le bouton correspondant à la vue sélectionnée.
			foreach (var kind in MainToolbar.ViewTypeKinds)
			{
				var command = MainToolbar.GetViewCommand (kind);
				this.SetActiveState (command, this.viewType.Kind == kind);
			}

			//	"Allume" l'engrenage si la vue sélectionnée a été choisie dans le Popup.
			{
				bool active = MainToolbar.PopupViewTypeKinds.Contains (this.viewType.Kind);
				this.SetActiveState (Res.Commands.View.Settings, active);
			}
		}

		private void UpdateViewModeCommands()
		{
			bool visibility = (this.viewType.Kind == ViewTypeKind.Assets);

			foreach (var mode in MainToolbar.ViewTypeModes)
			{
				var command = MainToolbar.GetViewModeCommand (mode);

				if (visibility)
				{
					this.SetVisibility  (command, true);
					this.SetEnable      (command, true);
					this.SetActiveState (command, this.viewMode == mode);
				}
				else
				{
					this.SetVisibility  (command, false);
					this.SetActiveState (command, false);
				}
			}
		}

		private void UpdateSimulation()
		{
			string icon = "Main.Simulation";
			
			if (this.simulation >= 0)
			{
				icon += this.simulation.ToString (System.Globalization.CultureInfo.InvariantCulture);
			}
			
			this.buttonSimulation.IconUri = Misc.GetResourceIconUri (icon);
		}

		private void ShowViewPopup(Widget target)
		{
			var commands = MainToolbar.PopupViewTypeKinds
				.Select (x => MainToolbar.GetViewCommand (x))
				.ToArray ();

			var popup = new ViewPopup ()
			{
				ViewCommands = commands,
			};

			popup.Create (target, leftOrRight: false);
		}


		#region View
		public static ViewTypeKind GetViewKind(Command command)
		{
			foreach (var kind in MainToolbar.ViewTypeKinds)
			{
				if (command == MainToolbar.GetViewCommand (kind))
				{
					return kind;
				}
			}

			return ViewTypeKind.Unknown;
		}

		private static Command GetViewCommand(ViewTypeKind kind)
		{
			switch (kind)
			{
				case ViewTypeKind.Assets:
					return Res.Commands.View.Assets;

				case ViewTypeKind.Amortizations:
					return Res.Commands.View.Amortizations;

				case ViewTypeKind.Entries:
					return Res.Commands.View.Entries;

				case ViewTypeKind.Categories:
					return Res.Commands.View.Categories;

				case ViewTypeKind.Groups:
					return Res.Commands.View.Groups;

				case ViewTypeKind.Persons:
					return Res.Commands.View.Persons;

				case ViewTypeKind.Reports:
					return Res.Commands.View.Reports;

				case ViewTypeKind.Warnings:
					return Res.Commands.View.Warnings;

				case ViewTypeKind.AssetsSettings:
					return Res.Commands.View.AssetsSettings;

				case ViewTypeKind.PersonsSettings:
					return Res.Commands.View.PersonsSettings;

				case ViewTypeKind.Accounts:
					return Res.Commands.View.Accounts;

				default:
					throw new System.InvalidOperationException (string.Format ("Unsupported ViewType {0}", kind.ToString ()));
			}
		}

		private static IEnumerable<ViewTypeKind> PopupViewTypeKinds
		{
			//	Enumère uniquement les vues choisies via le ViewPopup.
			get
			{
				yield return ViewTypeKind.AssetsSettings;
				yield return ViewTypeKind.PersonsSettings;
				yield return ViewTypeKind.Accounts;
			}
		}

		private static IEnumerable<ViewTypeKind> ViewTypeKinds
		{
			//	Enumère toutes les vues possibles.
			get
			{
				yield return ViewTypeKind.Assets;
				yield return ViewTypeKind.Amortizations;
				yield return ViewTypeKind.Entries;
				yield return ViewTypeKind.Categories;
				yield return ViewTypeKind.Groups;
				yield return ViewTypeKind.Persons;
				yield return ViewTypeKind.Reports;
				yield return ViewTypeKind.Warnings;
				yield return ViewTypeKind.AssetsSettings;
				yield return ViewTypeKind.PersonsSettings;
				yield return ViewTypeKind.Accounts;
			}
		}
		#endregion


		#region ViewMode
		public static ViewMode GetViewMode(Command command)
		{
			foreach (var mode in MainToolbar.ViewTypeModes)
			{
				if (command == MainToolbar.GetViewModeCommand (mode))
				{
					return mode;
				}
			}

			return ViewMode.Unknown;
		}

		private static Command GetViewModeCommand(ViewMode mode)
		{
			switch (mode)
			{
				case ViewMode.Single:
					return Res.Commands.ViewMode.Single;

				case ViewMode.Event:
					return Res.Commands.ViewMode.Event;

				case ViewMode.Multiple:
					return Res.Commands.ViewMode.Multiple;

				default:
					throw new System.InvalidOperationException (string.Format ("Unsupported ViewMode {0}", mode.ToString ()));
			}
		}

		private static IEnumerable<ViewMode> ViewTypeModes
		{
			get
			{
				yield return ViewMode.Single;
				yield return ViewMode.Event;
				yield return ViewMode.Multiple;
			}
		}
		#endregion


		#region Events handler
		private void OnChangeView()
		{
			this.ChangeView.Raise (this);
		}

		public event EventHandler ChangeView;
		#endregion


		private ButtonWithRedDot				buttonWarnings;
		private ButtonWithRedDot				buttonSimulation;

		private ViewType						viewType;
		private ViewMode						viewMode;
		private int								simulation;
	}
}

﻿//	Copyright © 2013, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Daniel ROUX, Maintainer: Daniel ROUX

using System.Collections.Generic;
using System.Linq;

using Epsitec.Common.Widgets;
using Epsitec.Common.Drawing;

namespace Epsitec.Cresus.Assets.App.Views
{
	public class TimelineToolbar : AbstractCommandToolbar
	{
		public override void CreateUI(Widget parent)
		{
			this.CreateToolbar (parent, 24+6);  // les icônes actuelles font 24
			this.UpdateCommandButtons ();
		}


		public TimelineMode TimelineMode
		{
			get
			{
				return this.timelineMode;
			}
			set
			{
				if (this.timelineMode != value)
				{
					this.timelineMode = value;
					this.UpdateModeButtons ();
				}
			}
		}


		protected override void UpdateCommandButtons()
		{
			this.UpdateCommandButton (this.buttonFirst,    ToolbarCommand.First);
			this.UpdateCommandButton (this.buttonNow,      ToolbarCommand.Now);
			this.UpdateCommandButton (this.buttonLast,     ToolbarCommand.Last);
			this.UpdateCommandButton (this.buttonNew,      ToolbarCommand.New);
			this.UpdateCommandButton (this.buttonDelete,   ToolbarCommand.Delete);
			this.UpdateCommandButton (this.buttonDeselect, ToolbarCommand.Deselect);
		}


		protected override void CreateToolbar(Widget parent, int size)
		{
			var toolbar = new HToolBar
			{
				Parent          = parent,
				Dock            = DockStyle.Top,
				PreferredHeight = size,
				Padding         = new Margins (0),
			};

			this.buttonLabels      = this.CreateModeButton (toolbar, TimelineMode.Labels,      "Timeline.Labels",      "Affiche les noms des lignes");
								   															   
			this.buttonCompacted   = this.CreateModeButton (toolbar, TimelineMode.Compacted,   "Timeline.Compacted",   "Affichage compact");
			this.buttonExpended    = this.CreateModeButton (toolbar, TimelineMode.Extended,    "Timeline.Extended",    "Affichage étendu");

			this.buttonWeeksOfYear = this.CreateModeButton (toolbar, TimelineMode.WeeksOfYear, "Timeline.WeeksOfYear", "Affiche les numéros des semaines");
			this.buttonDaysOfWeek  = this.CreateModeButton (toolbar, TimelineMode.DaysOfWeek,  "Timeline.DaysOfWeek",  "Affiche les jours de la semaine");
			this.buttonGraph       = this.CreateModeButton (toolbar, TimelineMode.Graph,       "Timeline.Graph",       "Affiche les graphique des valeurs");

			this.buttonFirst       = this.CreateCommandButton (toolbar, DockStyle.Left, ToolbarCommand.First,    "Timeline.First",    "Retour sur le premier événement");
			this.buttonNow         = this.CreateCommandButton (toolbar, DockStyle.Left, ToolbarCommand.Now,      "Timeline.Now",      "Va à la date du jour");
			this.buttonLast        = this.CreateCommandButton (toolbar, DockStyle.Left, ToolbarCommand.Last,     "Timeline.Last",     "Avance sur le dernier événement");
			this.buttonNew         = this.CreateCommandButton (toolbar, DockStyle.Left, ToolbarCommand.New,      "Timeline.New",      "Nouvel événement");
			this.buttonDelete      = this.CreateCommandButton (toolbar, DockStyle.Left, ToolbarCommand.Delete ,  "Timeline.Delete",   "Supprimer l'événement");
			this.buttonDeselect    = this.CreateCommandButton (toolbar, DockStyle.Left, ToolbarCommand.Deselect, "Timeline.Deselect", "Désélectionne l'événement");

			this.buttonCompacted.Margins   = new Margins (20, 0, 0, 0);
			this.buttonWeeksOfYear.Margins = new Margins (20, 0, 0, 0);
			this.buttonFirst.Margins       = new Margins (20, 0, 0, 0);
			this.buttonNew.Margins         = new Margins (20, 0, 0, 0);

			this.UpdateModeButtons ();
		}

		private IconButton CreateModeButton(HToolBar toolbar, TimelineMode mode, string icon, string tooltip)
		{
			var size = toolbar.PreferredHeight;

			var button = new IconButton
			{
				Parent        = toolbar,
				ButtonStyle   = ButtonStyle.ActivableIcon,
				AutoFocus     = false,
				Dock          = DockStyle.Left,
				IconUri       = MainToolbar.GetResourceIconUri (icon),
				PreferredSize = new Size (size, size),
			};

			ToolTip.Default.SetToolTip (button, tooltip);

			button.Clicked += delegate
			{
				this.ChangeMode (mode);
				this.UpdateModeButtons ();
				this.OnModeChanged (this.timelineMode);
			};

			return button;
		}


		private void ChangeMode(TimelineMode mode)
		{
			if (mode == Views.TimelineMode.Compacted)
			{
				this.timelineMode |=  Views.TimelineMode.Compacted;
				this.timelineMode &= ~Views.TimelineMode.Extended;
			}
			else if (mode == Views.TimelineMode.Extended)
			{
				this.timelineMode |=  Views.TimelineMode.Extended;
				this.timelineMode &= ~Views.TimelineMode.Compacted;
			}
			else
			{
				this.timelineMode ^= mode;
			}
		}

		private void UpdateModeButtons()
		{
			this.SetActiveState (this.buttonLabels,      (this.timelineMode & TimelineMode.Labels     ) != 0);
			this.SetActiveState (this.buttonCompacted,   (this.timelineMode & TimelineMode.Compacted  ) != 0);
			this.SetActiveState (this.buttonExpended,    (this.timelineMode & TimelineMode.Extended   ) != 0);
			this.SetActiveState (this.buttonWeeksOfYear, (this.timelineMode & TimelineMode.WeeksOfYear) != 0);
			this.SetActiveState (this.buttonDaysOfWeek,  (this.timelineMode & TimelineMode.DaysOfWeek ) != 0);
			this.SetActiveState (this.buttonGraph,       (this.timelineMode & TimelineMode.Graph      ) != 0);
		}


		#region Events handler
		private void OnModeChanged(TimelineMode timelineMode)
		{
			if (this.ModeChanged != null)
			{
				this.ModeChanged (this, timelineMode);
			}
		}

		public delegate void ModeChangedEventHandler(object sender, TimelineMode timelineMode);
		public event ModeChangedEventHandler ModeChanged;
		#endregion


		private IconButton buttonLabels;
		private IconButton buttonCompacted;
		private IconButton buttonExpended;
		private IconButton buttonWeeksOfYear;
		private IconButton buttonDaysOfWeek;
		private IconButton buttonGraph;

		private IconButton buttonFirst;
		private IconButton buttonNow;
		private IconButton buttonLast;
		private IconButton buttonNew;
		private IconButton buttonDelete;
		private IconButton buttonDeselect;

		private TimelineMode timelineMode;
	}
}

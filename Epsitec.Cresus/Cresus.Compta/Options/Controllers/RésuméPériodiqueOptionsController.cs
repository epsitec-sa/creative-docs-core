﻿//	Copyright © 2010, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Daniel ROUX, Maintainer: Daniel ROUX

using Epsitec.Common.Drawing;
using Epsitec.Common.Widgets;
using Epsitec.Common.Types;

using Epsitec.Cresus.Compta.Accessors;
using Epsitec.Cresus.Compta.Entities;
using Epsitec.Cresus.Compta.Controllers;
using Epsitec.Cresus.Compta.Options.Data;
using Epsitec.Cresus.Compta.Helpers;

using System.Collections.Generic;
using System.Linq;

namespace Epsitec.Cresus.Compta.Options.Controllers
{
	/// <summary>
	/// Ce contrôleur gère les options d'affichage du résumé périodique de la comptabilité.
	/// </summary>
	public class RésuméPériodiqueOptionsController : AbstractOptionsController
	{
		public RésuméPériodiqueOptionsController(AbstractController controller)
			: base (controller)
		{
		}


		public override void UpdateContent()
		{
			base.UpdateContent ();

			if (this.showPanel)
			{
				this.UpdateWidgets ();
			}
		}


		public override void CreateUI(FrameBox parent, System.Action optionsChanged)
		{
			base.CreateUI (parent, optionsChanged);

			this.CreateMainUI (this.mainFrame);

			var line = this.CreateSpecialistFrameUI (this.mainFrame);
			this.CreateCatégoriesUI (line);
			this.CreateSeparator (line);
			this.CreateDeepUI (line);

			line = this.CreateSpecialistFrameUI (this.mainFrame);
			this.CreateZeroFilteredUI (line);
			this.CreateZeroDisplayedInWhiteUI (line);

			this.UpdateWidgets ();
		}

		protected void CreateMainUI(FrameBox parent)
		{
			var frame = new FrameBox
			{
				Parent          = parent,
				PreferredHeight = 20,
				Dock            = DockStyle.Top,
				TabIndex        = ++this.tabIndex,
			};

			this.CreateGraphUI (frame);

			var lp = new StaticText
			{
				Parent         = frame,
				Text           = "Périodicité",
				Dock           = DockStyle.Left,
				Margins        = new Margins (0, 10, 0, 0),
			};
			UIBuilder.AdjustWidth (lp);

			this.monthsField = new TextFieldCombo
			{
				Parent          = frame,
				PreferredWidth  = 100,
				PreferredHeight = 20,
				MenuButtonWidth = UIBuilder.ComboButtonWidth,
				IsReadOnly      = true,
				Dock            = DockStyle.Left,
				TabIndex        = ++this.tabIndex,
				Margins         = new Margins (0, 20, 0, 0),
			};

			for (int i = 0; i < 5; i++)
			{
				int months = RésuméPériodiqueOptionsController.IndexToMonths (i);
				this.monthsField.Items.Add (RésuméPériodiqueOptions.MonthsToDescription (months));
			}

			this.cumulButton = new CheckButton
			{
				Parent         = frame,
				Text           = "Chiffres cumulés",
				Dock           = DockStyle.Left,
				Margins        = new Margins (0, 10, 0, 0),
				TabIndex       = ++this.tabIndex,
			};
			UIBuilder.AdjustWidth (this.cumulButton);

			this.CreateSeparator (frame);

			this.graphLabel = new StaticText
			{
				Parent         = frame,
				FormattedText  = "Graphiques",
				Dock           = DockStyle.Left,
				Margins        = new Margins (0, 10, 0, 0),
				TabIndex       = ++this.tabIndex,
			};
			UIBuilder.AdjustWidth (this.graphLabel);

			this.stackedGraphButton = new CheckButton
			{
				Parent         = frame,
				FormattedText  = "Cumulés",
				Dock           = DockStyle.Left,
				Margins        = new Margins (0, 10, 0, 0),
				TabIndex       = ++this.tabIndex,
			};
			UIBuilder.AdjustWidth (this.stackedGraphButton);

			this.sideBySideGraphButton = new CheckButton
			{
				Parent         = frame,
				FormattedText  = "Côte à côte",
				Dock           = DockStyle.Left,
				Margins        = new Margins (0, 10, 0, 0),
				TabIndex       = ++this.tabIndex,
			};
			UIBuilder.AdjustWidth (this.sideBySideGraphButton);

			this.monthsField.SelectedItemChanged += delegate
			{
				if (this.ignoreChanges.IsZero)
				{
					this.Options.NumberOfMonths = RésuméPériodiqueOptionsController.IndexToMonths (this.monthsField.SelectedItemIndex);
					this.OptionsChanged ();
				}
			};

			this.cumulButton.ActiveStateChanged += delegate
			{
				if (this.ignoreChanges.IsZero)
				{
					this.Options.Cumul = (this.cumulButton.ActiveState == ActiveState.Yes);
					this.OptionsChanged ();
				}
			};

			this.stackedGraphButton.ActiveStateChanged += delegate
			{
				if (this.ignoreChanges.IsZero)
				{
					this.Options.HasStackedGraph = (this.stackedGraphButton.ActiveState == ActiveState.Yes);

					if (this.Options.HasStackedGraph)
					{
						this.Options.HasSideBySideGraph = false;
					}

					this.OptionsChanged ();
				}
			};

			this.sideBySideGraphButton.ActiveStateChanged += delegate
			{
				if (this.ignoreChanges.IsZero)
				{
					this.Options.HasSideBySideGraph = (this.sideBySideGraphButton.ActiveState == ActiveState.Yes);

					if (this.Options.HasSideBySideGraph)
					{
						this.Options.HasStackedGraph = false;
					}

					this.OptionsChanged ();
				}
			};
		}


		protected override bool HasBeginnerSpecialist
		{
			get
			{
				return true;
			}
		}

		protected override void OptionsChanged()
		{
			this.UpdateWidgets ();
			base.OptionsChanged ();
		}

		protected override void LevelChangedAction()
		{
			base.LevelChangedAction ();
		}

		protected override void UpdateWidgets()
		{
			this.UpdateGraphWidgets ();
			this.UpdateCatégories ();
			this.UpdateDeep ();
			this.UpdateZeroFiltered ();
			this.UpdateZeroDisplayedInWhite ();

			using (this.ignoreChanges.Enter ())
			{
				this.cumulButton.Visibility                = !this.options.ViewGraph;
				this.zeroFilteredButton.Visibility         = !this.options.ViewGraph;
				this.zeroDisplayedInWhiteButton.Visibility = !this.options.ViewGraph;
				this.graphLabel.Visibility                 = !this.options.ViewGraph;
				this.stackedGraphButton.Visibility         = !this.options.ViewGraph;
				this.sideBySideGraphButton.Visibility      = !this.options.ViewGraph;

				this.monthsField.Text = RésuméPériodiqueOptions.MonthsToDescription (this.Options.NumberOfMonths);
				this.cumulButton.ActiveState           = this.Options.Cumul              ? ActiveState.Yes : ActiveState.No;
				this.stackedGraphButton.ActiveState    = this.Options.HasStackedGraph    ? ActiveState.Yes : ActiveState.No;
				this.sideBySideGraphButton.ActiveState = this.Options.HasSideBySideGraph ? ActiveState.Yes : ActiveState.No;
			}

			base.UpdateWidgets ();
		}


		private static int IndexToMonths(int index)
		{
			switch (index)
			{
				case 0:
					return 1;  // mensuel

				case 1:
					return 2;  // bimestriel

				case 2:
					return 3;  // trimestriel

				case 3:
					return 6;  // semestriel

				case 4:
					return 12;  // annuel

				default:
					return 0;  // inconnu
			}
		}


		private RésuméPériodiqueOptions Options
		{
			get
			{
				return this.options as RésuméPériodiqueOptions;
			}
		}


		private TextFieldCombo		monthsField;
		private CheckButton			cumulButton;
		private StaticText			graphLabel;
		private CheckButton			stackedGraphButton;
		private CheckButton			sideBySideGraphButton;
	}
}

﻿//	Copyright © 2010, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Daniel ROUX, Maintainer: Daniel ROUX

using Epsitec.Common.Drawing;
using Epsitec.Common.Widgets;
using Epsitec.Common.Types;

using Epsitec.Cresus.Core;
using Epsitec.Cresus.Core.Entities;
using Epsitec.Cresus.Core.Controllers;
using Epsitec.Cresus.Core.Widgets;
using Epsitec.Cresus.Core.Widgets.Tiles;

using Epsitec.Cresus.DataLayer.Context;

using System.Collections.Generic;
using System.Linq;

namespace Epsitec.Cresus.Core.Controllers.ComptabilitéControllers
{
	/// <summary>
	/// Ce contrôleur gère la barre d'outil inférieure pour la comptabilité.
	/// </summary>
	public class BottomToolbarController
	{
		public BottomToolbarController(TileContainer tileContainer)
		{
			this.tileContainer = tileContainer;
			this.toolbarShowed = true;
		}


		public void CreateUI(FrameBox parent, System.Action acceptAction, System.Action cancelAction, System.Action duplicateAction, System.Action deleteAction)
		{
			this.toolbar = new FrameBox
			{
				Parent          = parent,
				PreferredHeight = BottomToolbarController.toolbarHeight,
				Dock            = DockStyle.Bottom,
				Margins         = new Margins (0, 0, 1, 1),
				Padding         = new Margins (0, 20, 0, 0),
			};

			//	|-->
			this.operationLabel = new StaticText
			{
				Parent           = this.toolbar,
				ContentAlignment = ContentAlignment.MiddleLeft,
				PreferredWidth   = 200,
				PreferredHeight  = BottomToolbarController.toolbarHeight,
				Dock             = DockStyle.Left,
			};

			//	<--|
			this.deleteButton = new IconButton
			{
				Parent          = this.toolbar,
				IconUri         = Misc.GetResourceIconUri ("Lines.Delete"),
				AutoFocus       = false,
				Enable          = false,
				PreferredHeight = BottomToolbarController.toolbarHeight,
				Dock            = DockStyle.Right,
				Margins         = new Margins (0, 0, 0, 0),
			};

			this.duplicateButton = new IconButton
			{
				Parent          = this.toolbar,
				IconUri         = Misc.GetResourceIconUri ("Lines.Duplicate"),
				AutoFocus       = false,
				Enable          = false,
				PreferredHeight = BottomToolbarController.toolbarHeight,
				Dock            = DockStyle.Right,
				Margins         = new Margins (10, 0, 0, 0),
			};

			this.cancelButton = new IconButton
			{
				Parent          = this.toolbar,
				IconUri         = Misc.GetResourceIconUri ("Lines.Cancel"),
				AutoFocus       = false,
				Enable          = false,
				PreferredHeight = BottomToolbarController.toolbarHeight,
				Dock            = DockStyle.Right,
				Margins         = new Margins (0, 0, 0, 0),
			};

			this.acceptButton = new IconButton
			{
				Parent          = this.toolbar,
				IconUri         = Misc.GetResourceIconUri ("Lines.Ok"),
				AutoFocus       = false,
				Enable          = false,
				PreferredHeight = BottomToolbarController.toolbarHeight,
				Dock            = DockStyle.Right,
				Margins         = new Margins (0, 0, 0, 0),
			};

			this.acceptButton.Clicked += delegate
			{
				acceptAction ();
			};

			this.cancelButton.Clicked += delegate
			{
				cancelAction ();
			};

			this.duplicateButton.Clicked += delegate
			{
				duplicateAction ();
			};

			this.deleteButton.Clicked += delegate
			{
				deleteAction ();
			};

			ToolTip.Default.SetToolTip (this.acceptButton,    "Valide l'édition (Enter)");
			ToolTip.Default.SetToolTip (this.cancelButton,    "Annule l'édition (Esc)");
			ToolTip.Default.SetToolTip (this.duplicateButton, "Duplique la ligne sélectionnée");
			ToolTip.Default.SetToolTip (this.deleteButton,    "Supprime la ligne sélectionnée");
		}


		public double BottomOffset
		{
			get
			{
				return this.bottomOffset;
			}
			set
			{
				if (this.bottomOffset != value)
				{
					this.bottomOffset = value;
					this.UpdateShowHideButton ();
				}
			}
		}


		public void SetOperationDescription(FormattedText text, bool hilited)
		{
#if false
			text = text.ApplyFontSize (12.5);

			if (hilited)
			{
				text = text.ApplyBold ().ApplyFontColor (Color.FromHexa ("b00000"));  // gras + rouge
			}
#else
			if (hilited)
			{
				text = text.ApplyBold ();
			}
#endif

			this.operationLabel.FormattedText = text;
		}

		public bool AcceptEnable
		{
			get
			{
				return this.acceptButton.Enable;
			}
			set
			{
				this.acceptButton.Enable = value;
			}
		}

		public bool CancelEnable
		{
			get
			{
				return this.cancelButton.Enable;
			}
			set
			{
				this.cancelButton.Enable = value;
			}
		}

		public bool DuplicateEnable
		{
			get
			{
				return this.duplicateButton.Enable;
			}
			set
			{
				this.duplicateButton.Enable = value;
			}
		}

		public bool DeleteEnable
		{
			get
			{
				return this.deleteButton.Enable;
			}
			set
			{
				this.deleteButton.Enable = value;
			}
		}


		public void FinalizeUI(FrameBox parent)
		{
			//	Widgets créés en dernier, pour être par-dessus tout le reste.
			this.showHideButton = new GlyphButton
			{
				Parent        = parent,
				Anchor        = AnchorStyles.BottomRight,
				PreferredSize = new Size (16, 16),
				ButtonStyle   = ButtonStyle.Slider,
			};

			this.showHideButton.Clicked += delegate
			{
				this.toolbarShowed = !this.toolbarShowed;
				this.UpdateShowHideButton ();
			};

			this.UpdateShowHideButton ();
		}


		private void UpdateShowHideButton()
		{
			//	Met à jour le bouton pour montrer/cacher la barre d'icône.
			this.showHideButton.GlyphShape = this.toolbarShowed ? GlyphShape.ArrowDown : GlyphShape.ArrowUp;
			this.showHideButton.Margins = new Margins (0, 0, 0, this.toolbarShowed ? 25+this.bottomOffset : 2);

			ToolTip.Default.SetToolTip (this.showHideButton, this.toolbarShowed ? "Cache la barre d'outils" : "Montre la barre d'outils");

			this.toolbar.Visibility   = this.toolbarShowed;
		}


		private static readonly double			toolbarHeight = 20;

		private readonly TileContainer			tileContainer;

		private FrameBox						toolbar;
		private StaticText						operationLabel;
		private IconButton						acceptButton;
		private IconButton						cancelButton;
		private IconButton						duplicateButton;
		private IconButton						deleteButton;
		private GlyphButton						showHideButton;
		private double							bottomOffset;
		private bool							toolbarShowed;
	}
}

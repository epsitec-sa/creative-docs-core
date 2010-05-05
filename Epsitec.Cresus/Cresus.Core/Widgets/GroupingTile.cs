﻿//	Copyright © 2010, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Daniel ROUX, Maintainer: Daniel ROUX

using Epsitec.Common.Support;
using Epsitec.Common.Support.EntityEngine;
using Epsitec.Common.Types;
using Epsitec.Common.Drawing;
using Epsitec.Common.Widgets;

using System.Collections.Generic;
using System.Linq;

namespace Epsitec.Cresus.Core.Widgets
{
	/// <summary>
	/// Cette tuile regroupe plusieurs tuiles simples (AbstractTile) dans son conteneur (Container).
	/// Elle affiche une icône en haut à gauche (TopLeftIconUri) et un titre (Title).
	/// </summary>
	public class GroupingTile : ArrowedTile
	{
		public GroupingTile()
		{
			this.PreferredWidth = GroupingTile.iconSize+GroupingTile.iconMargins*2;
			this.childrenTiles = new List<ContainerTile> ();
			this.CreateUI ();
		}

		public GroupingTile(Widget embedder)
			: this ()
		{
			this.SetEmbedder (embedder);
		}


		static GroupingTile()
		{
			DependencyPropertyMetadata metadataDy = Visual.PreferredHeightProperty.DefaultMetadata.Clone ();

			metadataDy.DefineDefaultValue (GroupingTile.iconSize+GroupingTile.iconMargins*2);

			Common.Widgets.Visual.PreferredHeightProperty.OverrideMetadata (typeof (GroupingTile), metadataDy);
		}


		public List<ContainerTile> ChildrenTiles
		{
			get
			{
				return this.childrenTiles;
			}
		}

		/// <summary>
		/// Détermine si le widget est sensible au survol de la souris.
		/// </summary>
		/// <value><c>true</c> if [entered sensitivity]; otherwise, <c>false</c>.</value>
		public bool EnteredSensitivity
		{
			get
			{
				return this.enteredSensitivity;
			}
			set
			{
				this.enteredSensitivity = value;
			}
		}

	
		public virtual FrameBox Container
		{
			get
			{
				return this.mainPanel;
			}
		}


		public static double WidthWithOnlyIcon
		{
			get
			{
				return GroupingTile.iconSize+GroupingTile.iconMargins*2;
			}
		}
	

		/// <summary>
		/// Icône visible en haut à gauche de la tuile.
		/// Si on donne un seul caractère, il est affiché tel quel.
		/// </summary>
		/// <value>Nom brut de l'icône, sans prefix ni extension.</value>
		public string TopLeftIconUri
		{
			get
			{
				return this.topLeftIconUri;
			}
			set
			{
				this.topLeftIconUri = value;

				if (string.IsNullOrEmpty (this.topLeftIconUri) || this.topLeftIconUri.Length == 1)  // un seul caractère ?
				{
					this.staticTextTopLeftIcon.Text = string.Concat ("<font size=\"200%\">", this.topLeftIconUri, "</font>");
				}
				else
				{
					this.staticTextTopLeftIcon.Text = Misc.GetResourceIconImageTag (value);
				}
			}
		}

		/// <summary>
		/// Titre affiché en haut de la tuile.
		/// </summary>
		/// <value>The title.</value>
		public string Title
		{
			get
			{
				return this.title;
			}
			set
			{
				this.title = value;
				this.staticTextTitle.Text = string.Concat ("<b><font size=\"120%\">", this.title, "</font></b>");
			}
		}


		private void CreateUI()
		{
			//	Crée deux panneaux gauche/droite.
			this.leftPanel = new FrameBox
			{
				Parent = this,
				PreferredWidth = GroupingTile.iconSize+GroupingTile.iconMargins*2,
				Dock = DockStyle.Left,
			};

			this.rightPanel = new FrameBox
			{
				Parent = this,
				PreferredWidth = 0,
				Dock = DockStyle.Fill,
			};

			//	Crée le contenu du panneau de gauche.
			this.staticTextTopLeftIcon = new StaticText
			{
				Parent = this.leftPanel,
				Margins = new Margins (GroupingTile.iconMargins),
				PreferredSize = new Size (GroupingTile.iconSize, GroupingTile.iconSize),
				Dock = DockStyle.Top,
				ContentAlignment = Common.Drawing.ContentAlignment.MiddleCenter,
			};

			//	Crée le contenu du panneau de droite.
			this.staticTextTitle = new StaticText
			{
				Parent = this.rightPanel,
				PreferredHeight = GroupingTile.titleHeight,
				PreferredWidth = 0,
				Dock = DockStyle.Top,
				Margins = new Margins (2, GroupingTile.ArrowBreadth, 0, 0),
				ContentAlignment = ContentAlignment.TopLeft,
				TextBreakMode = Common.Drawing.TextBreakMode.Ellipsis | Common.Drawing.TextBreakMode.Split | Common.Drawing.TextBreakMode.SingleLine,
			};

			this.mainPanel = new FrameBox
			{
				Parent = this.rightPanel,
				PreferredWidth = 0,
				Dock = DockStyle.Fill,
			};
		}


		protected override void SetBoundsOverride(Rectangle oldRect, Rectangle newRect)
		{
			if (newRect.Width <= GroupingTile.WidthWithOnlyIcon)  // icône seule ?
			{
				this.rightPanel.Visibility = false;
			}
			else
			{
				this.rightPanel.Visibility = true;
			}
		}


		protected override void PaintBackgroundImplementation(Graphics graphics, Rectangle clipRect)
		{
			PaintingArrowMode mode = this.GetPaintingArrowMode ();
			Color thicknessColor = this.GetThicknessColor ();
			Color outlineColor = this.GetOutlineColor ();
			Color surfaceColor = this.GetSurfaceColor ();

			this.PaintArrow (graphics, clipRect, mode, thicknessColor, outlineColor, surfaceColor);
		}

		protected override void PaintForegroundImplementation(Graphics graphics, Rectangle clipRect)
		{
#if false
			PaintingArrowMode mode = this.GetRevertPaintingArrowMode ();
			Color thicknessColor = this.GetRevertThicknessColor ();
			Color outlineColor = this.GetRevertOutlineColor ();
			Color surfaceColor = this.GetRevertSurfaceColor ();

			this.PaintRevertArrow (graphics, clipRect, mode, thicknessColor, outlineColor, surfaceColor);
#endif
		}


		private PaintingArrowMode GetPaintingArrowMode()
		{
			if (this.IsEditing)
			{
				return Widgets.PaintingArrowMode.None;
			}
			else
			{
				if (this.enteredSensitivity && ((this.IsEntered && !this.HasManyChildren) || this.HasEnteredChildren))
				{
					return Widgets.PaintingArrowMode.Normal;
				}

				if (this.HasSelectedChildren || this.HasEnteredChildren)
				{
					return Widgets.PaintingArrowMode.Normal;
				}
			}

			return Widgets.PaintingArrowMode.None;
		}

		private Color GetSurfaceColor()
		{
			if (this.IsEditing)
			{
				return ArrowedTile.BackgroundEditingColor;
			}
			else
			{
				if (this.enteredSensitivity && (this.IsEntered || this.HasEnteredChildren))
				{
					return ArrowedTile.BackgroundSurfaceHilitedColor;
				}

				if (this.HasSelectedChildren)
				{
					return ArrowedTile.BackgroundSelectedGroupingColor;
				}
			}

			return ArrowedTile.BackgroundSummaryColor;
		}

		private Color GetOutlineColor()
		{
			return ArrowedTile.BorderColor;
		}

		private Color GetThicknessColor()
		{
			if (this.IsEditing)
			{
				return Color.Empty;
			}
			else
			{
				if (this.enteredSensitivity && ((this.IsEntered && !this.HasManyChildren) || this.HasEnteredChildren))
				{
					return ArrowedTile.BackgroundOutlineHilitedColor;
				}
			}

			return Color.Empty;
		}


		private PaintingArrowMode GetRevertPaintingArrowMode()
		{
			if (this.IsEditing)
			{
				return Widgets.PaintingArrowMode.None;
			}
			else
			{
			}

			return Widgets.PaintingArrowMode.None;
		}

		private Color GetRevertSurfaceColor()
		{
			if (this.IsEditing)
			{
				return ArrowedTile.BackgroundEditingColor;
			}
			else
			{
			}

			return Color.Empty;
		}

		private Color GetRevertOutlineColor()
		{
			return Color.Empty;
		}

		private Color GetRevertThicknessColor()
		{
			if (this.IsEditing)
			{
				return Color.Empty;
			}
			else
			{
			}

			return Color.Empty;
		}


#if false
		private bool HasArrow
		{
			get
			{
				return !this.IsEditing && (this.IsEntered || this.HasSelectedChildren || this.HasEnteredChildren);
			}
		}

		private bool HasRevertedArrow
		{
			get
			{
				return this.HasSelectedChildren && this.HasMouseHilite;
			}
		}

		private bool HasMouseHilite
		{
			get
			{
				return !this.IsEditing && (this.IsEntered || this.HasEnteredChildren) && this.enteredSensitivity;
			}
		}
#endif

		private bool HasExpandableChildren
		{
			get
			{
				foreach (var containerTile in this.childrenTiles)
				{
					if (containerTile.IsEntered && !containerTile.IsSelected)
					{
						return true;
					}
				}

				return false;
			}
		}

		private bool HasManyChildren
		{
			get
			{
				return this.childrenTiles.Count > 1;
			}
		}

		private bool HasEnteredChildren
		{
			get
			{
				foreach (var containerTile in this.childrenTiles)
				{
					if (containerTile.IsEntered)
					{
						return true;
					}
				}

				return false;
			}
		}

		private bool HasSelectedChildren
		{
			get
			{
				foreach (var containerTile in this.childrenTiles)
				{
					if (containerTile.IsSelected)
					{
						return true;
					}
				}

				return false;
			}
		}


		private static readonly double iconSize = 32;
		private static readonly double iconMargins = 2;
		private static readonly double titleHeight = 20;


		private readonly List<ContainerTile> childrenTiles;

		private bool enteredSensitivity;

		private FrameBox leftPanel;
		private FrameBox rightPanel;
		protected FrameBox mainPanel;

		private string topLeftIconUri;
		private StaticText staticTextTopLeftIcon;

		private string title;
		private StaticText staticTextTitle;
	}
}

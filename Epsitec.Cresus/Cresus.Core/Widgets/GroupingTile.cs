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
	public class GroupingTile : BackgroundTile
	{
		public GroupingTile()
		{
			this.PreferredWidth = GroupingTile.iconSize+GroupingTile.iconMargins*2;

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
				Margins = new Margins (2, ContainerTile.ArrowBreadth, 0, 0),
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


		public virtual FrameBox Container
		{
			get
			{
				return this.mainPanel;
			}
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


		public bool IsSoftHilite
		{
			get
			{
				return this.isSoftHilite;
			}
			set
			{
				if (this.isSoftHilite != value)
				{
					this.isSoftHilite = value;
					this.Invalidate ();
				}
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


		protected override void PaintBackgroundImplementation(Graphics graphics, Rectangle clipRect)
		{
			IAdorner adorner = Common.Widgets.Adorners.Factory.Active;

			Rectangle rect = this.Client.Bounds;
			rect.Deflate (0.5);
			rect = new Rectangle (rect.Left, rect.Bottom, rect.Width-ContainerTile.ArrowBreadth, rect.Height);

			//	Dessine toujours le fond.
			Color backColor = this.BackgroundColor;

			if (this.isSoftHilite)
			{
				backColor = this.BackgroundSurfaceHilitedColor;
			}

			graphics.AddFilledRectangle (rect);
			graphics.RenderSolid (backColor);

			//	Dessine le cadre.
			graphics.AddRectangle (rect);
			graphics.RenderSolid (adorner.ColorBorder);
		}

	
		private static readonly double iconSize = 32;
		private static readonly double iconMargins = 2;
		private static readonly double titleHeight = 20;

		private FrameBox leftPanel;
		private FrameBox rightPanel;
		protected FrameBox mainPanel;

		private string topLeftIconUri;
		private StaticText staticTextTopLeftIcon;

		private string title;
		private StaticText staticTextTitle;

		private bool isSoftHilite;
	}
}

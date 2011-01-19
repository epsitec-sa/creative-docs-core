﻿//	Copyright © 2010, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Daniel ROUX, Maintainer: Daniel ROUX

using Epsitec.Common.Support;
using Epsitec.Common.Support.EntityEngine;
using Epsitec.Common.Types;
using Epsitec.Common.Drawing;
using Epsitec.Common.Widgets;

using System.Collections.Generic;
using System.Linq;

namespace Epsitec.Cresus.Core.Widgets.Tiles
{
	/// <summary>
	/// Cette tuile regroupe plusieurs tuiles simples (AbstractTile) dans son conteneur (Container).
	/// Elle affiche une icône en haut à gauche (TitleIconUri) et un titre (Title).
	/// </summary>
	public class TitleTile : StaticTitleTile, Epsitec.Common.Widgets.Collections.IWidgetCollectionHost<GenericTile>
	{
		public TitleTile()
		{
			this.items = new TileCollection (this);
			
			this.CreateUI ();
		}


		public bool CanExpandSubTile
		{
			get;
			set;
		}

		public bool ContainsCollectionItemTiles
		{
			get;
			set;
		}

		public bool ContainsFrozenTiles
		{
			get
			{
				return this.Items.Any (item => item.IsFrozen);
			}
		}

		public TileCollection Items
		{
			get
			{
				return this.items;
			}
		}

		public override Controllers.ITileController Controller
		{
			get
			{
				if ((this.ContainsCollectionItemTiles) &&
					(this.ContainsFrozenTiles))
				{
					return this.Items.Select (item => item.Controller).FirstOrDefault ();
				}
				else
				{
					return null;
				}
			}
			set
			{
				throw new System.InvalidOperationException ();
			}
		}
		
		public override TileArrowMode ArrowMode
		{
			get
			{
				return this.GetArrowMode ();
			}
			set
			{
				throw new System.NotImplementedException ();
			}
		}

		public override TileArrow TileArrow
		{
			get
			{
				this.tileArrow.SetOutlineColors (this.OutlineColors);
				this.tileArrow.SetSurfaceColors (this.SurfaceColors);
				this.tileArrow.MouseHilite = this.MouseHilite;

				return this.tileArrow;
			}
		}

		public override bool IsDragAndDropEnabled
		{
			get
			{
				return this.ContainsFrozenTiles;
			}
		}

		private bool HasSingleChild
		{
			get
			{
				return this.items.Count < 2;
			}
		}

		private bool HasManyChildren
		{
			get
			{
				return this.items.Count > 1;
			}
		}

		private bool HasEnteredChild
		{
			get
			{
				return this.items.Any (x => x.IsEntered);
			}
		}

		private bool HasSelectedChild
		{
			get
			{
				return this.items.Any (x => x.IsSelected);
			}
		}

	
		public static double MinimumTileWidth
		{
			get
			{
				return TitleTile.iconSize+TitleTile.iconMargins*2;
			}
		}
	

		public double GetFullHeight()
		{
			double height = TitleTile.titleHeight;

			foreach (var item in this.Items)
			{
				height += item.PreferredHeight;
			}

			return System.Math.Max (height, TitleTile.iconMargins + TitleTile.iconSize);
		}
		
		protected override void SetBoundsOverride(Rectangle oldRect, Rectangle newRect)
		{
			if (newRect.Width <= TitleTile.MinimumTileWidth)  // icône seule ?
			{
				this.rightPanel.Visibility = false;
			}
			else
			{
				this.rightPanel.Visibility = true;
			}
		}

		protected override void OnEntered(MessageEventArgs e)
		{
			base.OnEntered (e);

			this.UpdateDefaultChildHilite ();
			this.SetButtonVisibility (true);
		}

		protected override void OnExited(MessageEventArgs e)
		{
			base.OnExited (e);

// Suppression de la sélection de la première tuile contenue lorsque le parent TitleTile est cliqué.
// Cela n'était pas logique et causait des interférences avec le drag & drop.
#if false
			if (this.HasChildren)
			{
				this.Items[0].Hilited = false;
				this.Items[0].Invalidate ();
			}
#endif

			this.SetButtonVisibility (false);
		}

		private void UpdateDefaultChildHilite()
		{
#if false
			if (this.HasManyChildren)
			{
				bool hilite = this.IsEntered;

				if (hilite && this.items.Any (x => x.IsEntered))
				{
					hilite = false;
				}

				this.Items[0].Hilited = hilite;
				this.Items[0].Invalidate ();
			}
#endif
		}

		private void CreateUI()
		{
			this.CreateButtons ();
		}

		private void CreateButtons()
		{
			this.CreateAddButton ();
			this.CreateRemoveButton ();
		}

		private void CreateAddButton()
		{
			this.buttonAdd = new GlyphButton
			{
				Parent			= this,
				ButtonStyle		= Common.Widgets.ButtonStyle.Normal,
				GlyphShape		= Common.Widgets.GlyphShape.Plus,
				Anchor			= AnchorStyles.TopLeft,
				PreferredSize	= new Size (TitleTile.iconSize, TitleTile.iconSize),
				Margins			= new Margins (TitleTile.iconMargins, 0, TitleTile.iconMargins, 0),
				Visibility		= false,
			};

			this.buttonAdd.Clicked += (sender, e) => this.OnAddClicked (e);
		}

		private void CreateRemoveButton()
		{
			this.buttonRemove = new GlyphButton
			{
				Parent			= this,
				ButtonStyle		= Common.Widgets.ButtonStyle.Normal,
				GlyphShape		= Common.Widgets.GlyphShape.Minus,
				Anchor			= AnchorStyles.BottomRight,
				PreferredSize	= new Size (TitleTile.buttonSize, TitleTile.buttonSize),
				Margins			= new Margins (0, TileArrow.Breadth, 0, 0),
				Visibility		= false,
			};

			this.buttonRemove.Clicked += (sender, e) => this.OnRemoveClicked (e);
		}


		private void SetButtonVisibility(bool visibility)
		{
			this.buttonAdd.Visibility = visibility && this.ContainsCollectionItemTiles;
			this.buttonRemove.Visibility = this.buttonAdd.Visibility && this.ContainsFrozenTiles;
		}




		private TileArrowMode GetArrowMode()
		{
			if (this.IsReadOnly)
			{
				if (this.HasSelectedChild)
				{
					return Tiles.TileArrowMode.Selected;
				}
			}
			else if (this.CanExpandSubTile)
			{
				if (this.HasSelectedChild)
				{
					return Tiles.TileArrowMode.Selected;
				}
			}

			return Tiles.TileArrowMode.Normal;
		}

		private bool MouseHilite
		{
			get
			{
				return Misc.ColorsCompare (this.SurfaceColors, Tile.SurfaceHilitedColors) ||
					   Misc.ColorsCompare (this.SurfaceColors, Tile.SurfaceHilitedSelectedColors);
			}
		}

		private IEnumerable<Color> SurfaceColors
		{
			get
			{
				if (this.IsReadOnly)
				{
					if (this.IsEntered)
					{
						if (this.HasSelectedChild)
						{
							return Tile.SurfaceHilitedSelectedColors;
						}
						else
						{
							return Tile.SurfaceHilitedColors;
						}
					}

					if (this.HasSelectedChild)
					{
						return Tile.SurfaceSelectedGroupingColors;
					}

					return Tile.SurfaceSummaryColors;
				}
				else if (this.CanExpandSubTile)
				{
					if (this.IsEntered)
					{
						if (this.HasSelectedChild)
						{
							return Tile.SurfaceHilitedSelectedColors;
						}
						else
						{
							return Tile.SurfaceHilitedColors;
						}
					}

					if (this.HasSelectedChild)
					{
						return Tile.SurfaceSelectedGroupingColors;
					}
				}

				return Tile.SurfaceEditingColors;
			}
		}

		private IEnumerable<Color> OutlineColors
		{
			get
			{
				return Tile.BorderColors;
			}
		}


		#region IWidgetCollectionHost<GroupingTile> Members

		void Common.Widgets.Collections.IWidgetCollectionHost<GenericTile>.NotifyInsertion(GenericTile widget)
		{
			widget.Dock   = DockStyle.Top;
			widget.Parent = this.mainPanel;

			this.AttachEventHandlers (widget);
		}

		void Common.Widgets.Collections.IWidgetCollectionHost<GenericTile>.NotifyRemoval(GenericTile widget)
		{
			widget.Parent  = null;
			widget.Hilited = false;

			this.DetachEventHandlers (widget);
		}

		void Common.Widgets.Collections.IWidgetCollectionHost<GenericTile>.NotifyPostRemoval(GenericTile widget)
		{
		}

		Common.Widgets.Collections.WidgetCollection<GenericTile> Common.Widgets.Collections.IWidgetCollectionHost<GenericTile>.GetWidgetCollection()
		{
			return this.Items;
		}

		#endregion

		#region TileCollection Class

		
		public class TileCollection : Epsitec.Common.Widgets.Collections.WidgetCollection<GenericTile>
		{
			public TileCollection(TitleTile host)
				: base (host)
			{
			}
		}

		#endregion


		private void AttachEventHandlers(GenericTile widget)
		{
			widget.Entered    += this.HandleChildWidgetEnteredOrExited;
			widget.Exited     += this.HandleChildWidgetEnteredOrExited;
			widget.Selected   += this.HandleChildWidgetSelectedOrDeselected;
			widget.Deselected += this.HandleChildWidgetSelectedOrDeselected;
		}

		private void DetachEventHandlers(GenericTile widget)
		{
			widget.Entered    -= this.HandleChildWidgetEnteredOrExited;
			widget.Exited     -= this.HandleChildWidgetEnteredOrExited;
			widget.Selected   -= this.HandleChildWidgetSelectedOrDeselected;
			widget.Deselected -= this.HandleChildWidgetSelectedOrDeselected;
		}

		private void HandleChildWidgetEnteredOrExited(object sender, MessageEventArgs e)
		{
			this.UpdateDefaultChildHilite ();
			this.Invalidate ();
		}

		private void HandleChildWidgetSelectedOrDeselected(object sender)
		{
			this.Invalidate ();
		}

		protected virtual void OnAddClicked(MessageEventArgs e)
		{
			var handler = this.AddClicked;
			
			if (handler != null)
			{
				handler (this, e);
			}
		}

		protected virtual void OnRemoveClicked(MessageEventArgs e)
		{
			var handler = this.RemoveClicked;

			if (handler != null)
			{
				handler (this, e);
			}
		}
		
		static TitleTile()
		{
			DependencyPropertyMetadata metadataDy = Visual.PreferredHeightProperty.DefaultMetadata.Clone ();

			metadataDy.DefineDefaultValue (TitleTile.iconSize+TitleTile.iconMargins*2);

			Common.Widgets.Visual.PreferredHeightProperty.OverrideMetadata (typeof (TitleTile), metadataDy);
		}


		public event EventHandler<MessageEventArgs> AddClicked;
		public event EventHandler<MessageEventArgs> RemoveClicked;


		private static readonly double buttonSize	= 16;

		private readonly TileCollection items;

		private GlyphButton buttonAdd;
		private GlyphButton buttonRemove;
	}
}

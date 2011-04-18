﻿//	Copyright © 2010, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Daniel ROUX, Maintainer: Daniel ROUX

using Epsitec.Common.Drawing;
using Epsitec.Common.Widgets;
using Epsitec.Common.Types;

using System.Collections.Generic;
using System.Linq;

namespace Epsitec.Cresus.Core.Widgets.Tiles
{
	/// <summary>
	/// The <c>Tile</c> class paints a widget with an arrow; the logic which defines
	/// how and where the arrow should be painted can be overridden by the derived
	/// classes.
	/// 
	/// Résumé de l'héritage des différents widgets 'Tile':
	/// 
	/// o--Common.Widgets.FrameBox
	///    |
	///    o--Tiles.Tile (abstract)
  	///       | 
	///       o--ArrowedTile
	///       |
	///       o--ControllerTile
	///       |  |
	///       |  o--Tiles.GenericTile (abstract)
	///       |  |  |
	///       |  |  o--Tiles.EditionTile
	///       |  |  |
	///       |  |  o--Tiles.SummaryTile
	///       |  |  |  |
	///       |  |  |  o--Tiles.CollectionItemTile
	///       |  | 
	///       |  o--Tiles.StaticTitleTile (abstract)
	///       |  |  |
	///       |  |  o--Tiles.PanelTitleTile
	///       |  |  |
	///       |  |  o--Tiles.TitleTile
	///       |  | 
	///       |  o--TilePageButton
	/// 	  |
	/// 	  o--ControllerTile.DragHelper.ErsatzTile
	/// 	     |
	/// 	     o--ControllerTile.DragHelper.DragTile
	/// </summary>
	public abstract class Tile : FrameBox, IReadOnly
	{
		protected Tile(Direction arrowDirection)
		{
			this.tileArrow = new TileArrow (arrowDirection);
		}

		
		public bool Frameless
		{
			get;
			set;
		}

		/// <summary>
		/// Gets the container padding, which excludes the space used by the arrow.
		/// </summary>
		public Margins ContainerPadding
		{
			get
			{
				return TileArrow.GetContainerPadding (this.tileArrow.ArrowDirection);
			}
		}

		/// <summary>
		/// Gets the container bounds, which excludes the space used by the arrow.
		/// </summary>
		protected Rectangle ContainerBounds
		{
			get
			{
				var bounds = this.Client.Bounds;
				
				bounds.Deflate (this.ContainerPadding);

				return bounds;
			}
		}

		public virtual TileArrowMode ArrowMode
		{
			get
			{
				return this.arrowMode;
			}
			set
			{
				if (this.arrowMode != value)
				{
					this.arrowMode = value;
					this.Invalidate ();
				}
			}
		}

		public virtual TileArrow Arrow
		{
			get
			{
				return this.tileArrow;
			}
		}

		
		protected override void PaintBackgroundImplementation(Graphics graphics, Rectangle clipRect)
		{
			this.Arrow.Paint (graphics, this.Client.Bounds, this.ArrowMode, this.Frameless);
		}

		#region IReadOnly Members

		/// <summary>
		/// Gets or sets a value indicating whether this tile is read only.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this tile is read only; otherwise, <c>false</c>.
		/// </value>
		public bool IsReadOnly
		{
			get;
			set;
		}

		#endregion

		protected readonly TileArrow							tileArrow;
		
		protected TileArrowMode									arrowMode;
	}
}

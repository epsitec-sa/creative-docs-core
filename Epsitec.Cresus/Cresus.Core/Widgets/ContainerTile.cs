﻿//	Copyright © 2010, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Daniel ROUX, Maintainer: Daniel ROUX

using Epsitec.Common.Support;
using Epsitec.Common.Types;
using Epsitec.Common.Drawing;
using Epsitec.Common.Widgets;
using Epsitec.Common.Widgets.Helpers;

using System.Collections.Generic;
using System.Linq;

namespace Epsitec.Cresus.Core.Widgets
{
	/// <summary>
	/// Ce widget est un conteneur générique, qui peut être sélectionné. L'un de ses côté est
	/// alors une flèche (qui déborde de son Client.Bounds) qui pointe vers son enfant.
	/// </summary>
	public class ContainerTile : Tile
	{
		public ContainerTile()
		{
		}

		public ContainerTile(Widget embedder)
			: this ()
		{
			this.SetEmbedder (embedder);
		}


		/// <summary>
		/// Gets or sets the parent widget GroupingTile.
		/// </summary>
		/// <value>The GroupingTile widget.</value>
		public GroupingTile ParentGroupingTile
		{
			get;
			set;
		}


		public bool ArrowEnabled
		{
			get
			{
				return this.arrowEnabled;
			}
			set
			{
				this.arrowEnabled = value;
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
		
		private bool IsSoloContainer
		{
			get
			{
				return this.ParentGroupingTile != null && this.ParentGroupingTile.ChildrenTiles.Count <= 1;
			}
		}

		public override TileArrowMode ArrowMode
		{
			get
			{
				return this.GetPaintingArrowMode ();
			}
			set
			{
				throw new System.NotImplementedException ();
			}
		}

		public override TileArrow DirectArrow
		{
			get
			{
				return new TileArrow ()
				{
					OutlineColor = this.GetOutlineColor (),
					ThicknessColor = this.GetThicknessColor (),
					SurfaceColor = this.GetSurfaceColor (),
				};
			}
		}

		public override TileArrow ReverseArrow
		{
			get
			{
				return new TileArrow ()
				{
					OutlineColor = this.GetReverseOutlineColor (),
					ThicknessColor = this.GetReverseThicknessColor (),
					SurfaceColor = this.GetReverseSurfaceColor (),
				};
			}
		}


		protected override void OnSelected()
		{
			base.OnSelected ();

			if (this.ParentGroupingTile != null)
			{
				this.ParentGroupingTile.Invalidate ();
			}
		}

		protected override void OnDeselected()
		{
			base.OnDeselected ();

			if (this.ParentGroupingTile != null)
			{
				this.ParentGroupingTile.Invalidate ();
			}
		}


		protected override void OnEntered(MessageEventArgs e)
		{
			base.OnEntered (e);

			if (this.ParentGroupingTile != null)
			{
				this.ParentGroupingTile.Invalidate ();
			}
		}

		protected override void OnExited(MessageEventArgs e)
		{
			base.OnExited (e);

			if (this.ParentGroupingTile != null)
			{
				this.ParentGroupingTile.Invalidate ();
			}
		}



		private TileArrowMode GetPaintingArrowMode()
		{
			if (this.IsReadOnly == false)
			{
				return Widgets.TileArrowMode.None;
			}
			else
			{
				if (this.enteredSensitivity && this.IsEntered && !this.IsSoloContainer && this.IsSelected)
				{
					return Widgets.TileArrowMode.VisibleReverse;
				}

				if (this.enteredSensitivity && this.IsEntered && !this.IsSoloContainer)
				{
					return Widgets.TileArrowMode.VisibleDirect;
				}

				if (this.IsSelected && !this.IsSoloContainer)
				{
					return Widgets.TileArrowMode.VisibleDirect;
				}
			}

			return Widgets.TileArrowMode.None;
		}

		private Color GetSurfaceColor()
		{
			if (this.IsReadOnly == false)
			{
				return Tile.SurfaceEditingColor;
			}
			else
			{
				if (this.enteredSensitivity && this.IsEntered && !this.IsSoloContainer && this.IsSelected)
				{
					return Tile.SurfaceHilitedColor;
				}

				if (this.enteredSensitivity && this.IsEntered && !this.IsSoloContainer)
				{
					return Tile.ThicknessHilitedColor;
				}

				if (this.IsSelected && !this.IsSoloContainer)
				{
					return Tile.SurfaceSelectedContainerColor;
				}
			}

			return Color.Empty;
		}

		private Color GetOutlineColor()
		{
			if (this.IsReadOnly == false)
			{
				return Color.Empty;
			}
			else
			{
				if (this.enteredSensitivity && this.IsEntered && !this.IsSoloContainer)
				{
					return Tile.BorderColor;
				}

				if (this.IsSelected && !this.IsSoloContainer)
				{
					return Tile.BorderColor;
				}
			}

			return Color.Empty;
		}

		private Color GetThicknessColor()
		{
			return Color.Empty;
		}


		private Color GetReverseSurfaceColor()
		{
			if (this.IsReadOnly == false)
			{
				return Color.Empty;
			}
			else
			{
				return Tile.ThicknessHilitedColor;
			}
		}

		private Color GetReverseOutlineColor()
		{
			return Tile.BorderColor;
		}

		private Color GetReverseThicknessColor()
		{
			return Color.Empty;
		}


		private bool arrowEnabled;
		private bool enteredSensitivity;
	}
}

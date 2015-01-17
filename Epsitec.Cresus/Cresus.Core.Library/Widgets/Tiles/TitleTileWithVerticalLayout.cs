﻿//	Copyright © 2010-2012, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Daniel ROUX, Maintainer: Pierre ARNAUD

using Epsitec.Common.Support;
using Epsitec.Common.Support.EntityEngine;
using Epsitec.Common.Support.Extensions;
using Epsitec.Common.Types;
using Epsitec.Common.Drawing;
using Epsitec.Common.Widgets;

using System.Collections.Generic;
using System.Linq;

namespace Epsitec.Cresus.Core.Widgets.Tiles
{
	/// <summary>
	/// The <c>TitleTileWithVerticalLayout</c> class is a variation of the <see cref="StaticTitleTile"/>
	/// where the panel is below the title and occupies the whole available width. Contrast
	/// this with the <see cref="TitleTile"/> where the panel occupies only the right part.
	/// </summary>
	public sealed class TitleTileWithVerticalLayout : StaticTitleTile
	{
		public TitleTileWithVerticalLayout()
		{
			var topFrame = new FrameBox
			{
				Parent = this,
				Dock = DockStyle.Top,
			};

			var bottomFrame = new FrameBox
			{
				Parent = this,
				Dock = DockStyle.Fill,
				Margins = this.ContainerPadding,
			};

			//	Reparent the various frames of the static title tile so that the layout
			//	matches what we need : the main panel below the title, using the full
			//	available width.

			this.leftPanel.Parent  = topFrame;
			this.rightPanel.Parent = topFrame;
			this.mainPanel.Parent  = bottomFrame;
		}


		/// <summary>
		/// Gets the panel located below the icon and the text.
		/// </summary>
		/// <value>The panel.</value>
		public Widget							Panel
		{
			get
			{
				return this.mainPanel;
			}
		}


		public override double GetFullHeight()
		{
			double height = System.Math.Max (StaticTitleTile.TitleHeight, StaticTitleTile.IconMargins + StaticTitleTile.IconSize);

			foreach (var item in this.Items)
			{
				height += item.PreferredHeight;
			}

			return height;
		}

		protected override void UpdateTileArrow()
		{
			this.tileArrow.SetOutlineColors (TileColors.BorderColors);
			this.tileArrow.SetSurfaceColors (TileColors.SurfaceSummaryColors);
			this.tileArrow.MouseHilite = false;
		}
	}
}

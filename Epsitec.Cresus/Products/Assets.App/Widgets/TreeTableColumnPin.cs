﻿//	Copyright © 2013, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Daniel ROUX, Maintainer: Daniel ROUX

using System.Collections.Generic;
using System.Linq;
using Epsitec.Common.Drawing;
using Epsitec.Cresus.Assets.App.Helpers;
using Epsitec.Cresus.Assets.Server.DataFillers;

namespace Epsitec.Cresus.Assets.App.Widgets
{
	/// <summary>
	/// Colonne de TreeTable affichant des icônes.
	/// </summary>
	public class TreeTableColumnPin : AbstractTreeTableColumn
	{
		protected override void PaintBackgroundImplementation(Graphics graphics, Rectangle clipRect)
		{
			base.PaintBackgroundImplementation(graphics, clipRect);

			int y = 0;

			foreach (var c in this.cells)
			{
				var cell = c as TreeTableCellInt;
				System.Diagnostics.Debug.Assert (cell != null);

				//	Dessine le fond.
				var rect = this.GetCellsRect (y);

				graphics.AddFilledRectangle (rect);
				graphics.RenderSolid (this.GetCellColor (y == this.hilitedHoverRow, cell.IsSelected, cell.IsEvent, cell.IsError));

				if (cell.IsUnavailable)
				{
					this.PaintUnavailable (graphics, rect, y, this.hilitedHoverRow);
				}

				//	Dessine la punaise. En mode "unpin", elle n'est dessinée
				//	que lorsque la souris survole la ligne.
				if (cell.Value.HasValue && (cell.Value.Value == 1 || y == this.hilitedHoverRow))
				{
					var textRect = rect;
					textRect.Offset (-1, 1);

					string icon = cell.Value.Value == 1 ? "TreeTable.Pin" : "TreeTable.Unpin";
					var text = Misc.GetRichTextImg (icon, verticalOffset: 0, iconSize: new Size (16, 16));
					this.PaintText (graphics, textRect, text);
				}

				//	Dessine la grille.
				this.PaintGrid (graphics, rect, y, this.hilitedHoverRow);

				y++;
			}
		}

		protected override ContentAlignment RowContentAlignment
		{
			get
			{
				return ContentAlignment.MiddleCenter;
			}
		}
	}
}

//	Copyright © 2013, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Daniel ROUX, Maintainer: Daniel ROUX

using System.Collections.Generic;
using System.Linq;
using Epsitec.Common.Drawing;
using Epsitec.Cresus.Assets.App.Helpers;

namespace Epsitec.Cresus.Assets.App.Widgets
{
	/// <summary>
	/// Ligne de Timeline affichant les noms des jours de la semaine.
	/// Par exemple "Lu", "Ma", "Me", "Je".
	/// Les samedis et dimanches ont une couleur de fond légèrement différente.
	/// </summary>
	public class TimelineRowDaysOfWeek : AbstractTimelineRow
	{
		public void SetCells(TimelineCellDate[] cells)
		{
			this.cells = cells;
			this.Invalidate ();
		}


		protected override void PaintBackgroundImplementation(Graphics graphics, Rectangle clipRect)
		{
			base.PaintBackgroundImplementation (graphics, clipRect);

			if (this.cells == null || this.VisibleCellCount == 0)
			{
				return;
			}

			this.Paint (graphics);
		}

		private void Paint(Graphics graphics)
		{
			int x = 0;
			int index = 0;
			var lastCell = new TimelineCellDate ();  // cellule invalide

			for (int rank = 0; rank <= this.VisibleCellCount; rank++)
			{
				var cell = this.GetCell (rank);
				if (!TimelineRowDaysOfWeek.IsSame (lastCell, cell) && x != rank)
				{
					var rect = this.GetCellsRect (x, rank);
					bool isHover = (this.detectedHoverRank >= x && this.detectedHoverRank < rank);

					this.PaintCellBackground (graphics, rect, lastCell, isHover, index);
					this.PaintCellForeground (graphics, rect, lastCell, isHover, index);

					this.PaintGrid (graphics, rect, index, this.hilitedHoverRank);

					index++;
					x = rank;
				}

				lastCell = cell;
			}
		}

		private void PaintCellBackground(Graphics graphics, Rectangle rect, TimelineCellDate cell, bool isHover, int index)
		{
			//	Dessine le fond.
			var color = TimelineRowDaysOfWeek.GetCellColor (cell, isHover, index);

			if (!color.IsBackground ())
			{
				graphics.AddFilledRectangle (rect);
				graphics.RenderSolid (color);
			}
		}

		private void PaintCellForeground(Graphics graphics, Rectangle rect, TimelineCellDate cell, bool isHover, int index)
		{
			//	Dessine le contenu.
			var text = TimelineRowDaysOfWeek.GetCellText (cell);
			var font = Font.DefaultFont;
			graphics.Color = ColorManager.TextColor;
			graphics.PaintText (rect, text, font, this.FontSize, ContentAlignment.MiddleCenter);
		}

		private static bool IsSame(TimelineCellDate c1, TimelineCellDate c2)
		{
			return TimelineCellDate.IsSameDays (c1, c2);
		}

		private static Color GetCellColor(TimelineCellDate cell, bool isHover, int index)
		{
			if (cell.IsValid)
			{
				if (cell.Date.DayOfWeek == System.DayOfWeek.Saturday ||
					cell.Date.DayOfWeek == System.DayOfWeek.Sunday)
				{
					return ColorManager.GetHolidayColor (false);
				}
				else
				{
					return ColorManager.GetBackgroundColor (false);
				}
			}
			else
			{
				return Color.Empty;
			}
		}

		private static string GetCellText(TimelineCellDate cell)
		{
			//	Retourne le jour sous la forme "lu" ou "ma".
			if (cell.IsValid)
			{
				return cell.Date.ToDayOfWeek ();
			}
			else
			{
				return null;
			}
		}


		private TimelineCellDate GetCell(int rank)
		{
			if (rank < this.VisibleCellCount)
			{
				int index = this.GetListIndex (rank);

				if (index >= 0 && index < this.cells.Length)
				{
					return this.cells[index];
				}
			}

			return new TimelineCellDate ();  // retourne une cellule invalide
		}

		private int GetListIndex(int rank)
		{
			if (rank >= 0 && rank < this.cells.Length)
			{
				int offset = (int) ((double) (this.cells.Length - this.VisibleCellCount) * this.pivot);
				return rank + offset;
			}
			else
			{
				return -1;
			}
		}


		private TimelineCellDate[] cells;
	}
}

﻿//	Copyright © 2013, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Daniel ROUX, Maintainer: Daniel ROUX

using System.Collections.Generic;
using System.Linq;

using Epsitec.Common.Widgets;
using Epsitec.Common.Drawing;
using Epsitec.Cresus.Assets.App.Helpers;
using Epsitec.Cresus.Assets.Server.SimpleEngine;

namespace Epsitec.Cresus.Assets.App.Widgets
{
	public class Calendar : Widget
	{
		public System.DateTime Date
		{
			get
			{
				return this.date;
			}
			set
			{
				if (this.date != value)
				{
					this.date = value;
					this.Invalidate ();
				}
			}
		}

		public System.DateTime? SelectedDate
		{
			get
			{
				return this.selectedDate;
			}
			set
			{
				if (this.selectedDate != value)
				{
					this.selectedDate = value;
					this.Invalidate ();
				}
			}
		}


		protected override void PaintBackgroundImplementation(Graphics graphics, Rectangle clipRect)
		{
			this.PaintTitle (graphics);
			this.PaintDows (graphics);
			this.PaintCells (graphics);
		}

		protected override void OnPressed(MessageEventArgs e)
		{
			if (this.hoverDate.HasValue)
			{
				this.OnDateChanged (this.hoverDate.Value);
			}

			base.OnPressed (e);
		}

		protected override void OnMouseMove(MessageEventArgs e)
		{
			this.HoverDate = this.Detect (e.Point);
			base.OnMouseMove (e);
		}

		protected override void OnExited(MessageEventArgs e)
		{
			this.HoverDate = null;
			base.OnExited (e);
		}


		private System.DateTime? Detect(Point pos)
		{
			for (int rank=0; rank<7*6; rank++)
			{
				var rect = Calendar.GetCellRect (rank);
				if (rect.Contains (pos))
				{
					return this.GetDate (rank);
				}
			}

			return null;
		}

		private System.DateTime? HoverDate
		{
			get
			{
				return this.hoverDate;
			}
			set
			{
				if (this.hoverDate != value)
				{
					this.hoverDate = value;
					this.Invalidate ();
				}
			}
		}


		private void PaintTitle(Graphics graphics)
		{
			var rect = Calendar.TitleRect;

			graphics.AddFilledRectangle (rect);
			graphics.RenderSolid (ColorManager.GetBackgroundColor (false));

			var date = new System.DateTime (this.date.Year, this.date.Month, 1);
			var text = date.ToString ("MMMM yyyy", System.Globalization.DateTimeFormatInfo.CurrentInfo);

			graphics.AddText (rect.Left, rect.Bottom, rect.Width, rect.Height, text, Font.DefaultFont, Calendar.FontSize, ContentAlignment.MiddleCenter);
			graphics.RenderSolid (ColorManager.TextColor);
		}

		private void PaintDows(Graphics graphics)
		{
			for (int rank=0; rank<7; rank++)
			{
				var rect = Calendar.GetDowRect (rank);

				graphics.AddFilledRectangle (rect);
				graphics.RenderSolid (ColorManager.GetBackgroundColor (false));

				var date = this.GetDate (rank+7).Value;
				var text = date.ToString ("ddd", System.Globalization.DateTimeFormatInfo.CurrentInfo);
				if (text.Length > 2)
				{
					text = text.Substring (0, 2);
				}

				graphics.AddText (rect.Left, rect.Bottom, rect.Width, rect.Height, text, Font.DefaultFont, Calendar.FontSize, ContentAlignment.MiddleCenter);
				graphics.RenderSolid (ColorManager.TextColor);
			}
		}

		private void PaintCells(Graphics graphics)
		{
			for (int rank=0; rank<7*6; rank++)
			{
				var date = this.GetDate (rank);
				if (date.HasValue)
				{
					bool selected = date == this.selectedDate;
					bool isHover  = date == this.hoverDate;

					Calendar.PaintCell (graphics, rank, date.Value, selected, isHover);
				}
			}
		}

		private static void PaintCell(Graphics graphics, int rank, System.DateTime date, bool selected, bool isHover)
		{
			var rect = Calendar.GetCellRect (rank);

			var color = ColorManager.GetBackgroundColor (isHover);

			var dow = date.DayOfWeek;
			if (dow == System.DayOfWeek.Saturday || dow == System.DayOfWeek.Sunday)
			{
				color = ColorManager.GetHolidayColor (isHover);
			}

			if (selected && !isHover)
			{
				color = ColorManager.SelectionColor;
			}

			graphics.AddFilledRectangle (rect);
			graphics.RenderSolid (color);

			string text = date.Day.ToString ();
			graphics.AddText (rect.Left, rect.Bottom, rect.Width, rect.Height, text, Font.DefaultFont, Calendar.FontSize, ContentAlignment.MiddleCenter);
			graphics.RenderSolid (ColorManager.TextColor);

			if (date == Timestamp.Now.Date)
			{
				rect.Deflate (0.5);
				graphics.AddRectangle (rect);
				graphics.RenderSolid (ColorManager.TextColor);
			}
		}


		private System.DateTime? GetDate(int rank)
		{
			var firstDate = new System.DateTime (this.date.Year, this.date.Month, 1);
			int firstRank = Calendar.GetFirstRank (firstDate);

			var date = firstDate.AddDays (rank - firstRank);

			if (date.Month == this.date.Month)
			{
				return date;
			}
			else
			{
				return null;
			}
		}

		private static int GetFirstRank(System.DateTime date)
		{
			switch (date.DayOfWeek)
			{
				case System.DayOfWeek.Monday:
					return 0;

				case System.DayOfWeek.Tuesday:
					return 1;

				case System.DayOfWeek.Wednesday:
					return 2;

				case System.DayOfWeek.Thursday:
					return 3;

				case System.DayOfWeek.Friday:
					return 4;

				case System.DayOfWeek.Saturday:
					return 5;

				case System.DayOfWeek.Sunday:
					return 6;

				default:
					return -1;
			}
		}


		private static Rectangle TitleRect
		{
			get
			{
				return new Rectangle (0, (Calendar.CellCountY-1)*Calendar.CellHeight, Calendar.RequiredWidth, Calendar.CellHeight);
			}
		}

		private static Rectangle GetDowRect(int rank)
		{
			int x = rank % Calendar.CellCountX;
			int y = Calendar.CellCountY-2;

			return new Rectangle (x*Calendar.CellWidth, y*Calendar.CellHeight, Calendar.CellWidth, Calendar.CellHeight);
		}

		private static Rectangle GetCellRect(int rank)
		{
			int x = rank % Calendar.CellCountX;
			int y = rank / Calendar.CellCountX;

			y = Calendar.CellCountY - 3 - y;

			return new Rectangle (x*Calendar.CellWidth, y*Calendar.CellHeight, Calendar.CellWidth, Calendar.CellHeight);
		}


		#region Events handler
		private void OnDateChanged(System.DateTime dateTime)
		{
			this.DateChanged.Raise (this, dateTime);
		}

		public event EventHandler<System.DateTime> DateChanged;
		#endregion


		public const int RequiredWidth  = Calendar.CellWidth  * Calendar.CellCountX;
		public const int RequiredHeight = Calendar.CellHeight * Calendar.CellCountY;
		private const int CellWidth     = 18;
		private const int CellHeight    = 18;
		private const int CellCountX    = 7;
		private const int CellCountY    = 8;
		private const double FontSize   = 11.0;

		private System.DateTime					date;
		private System.DateTime?				selectedDate;
		private System.DateTime?				hoverDate;
	}
}

﻿//	Copyright © 2013, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Daniel ROUX, Maintainer: Daniel ROUX

using System.Collections.Generic;
using System.Linq;
using Epsitec.Common.Drawing;
using Epsitec.Cresus.Assets.Core.Helpers;
using Epsitec.Cresus.Assets.Server.DataFillers;
using Epsitec.Cresus.Assets.Server.SimpleEngine;

namespace Epsitec.Cresus.Assets.App.Widgets
{
	/// <summary>
	/// Colonne de TreeTable affichant des montants alignés à droite.
	/// </summary>
	public class TreeTableColumnAmortizedAmount : AbstractTreeTableColumn
	{
		public TreeTableColumnAmortizedAmount(DataAccessor accessor, bool details = false)
		{
			this.accessor = accessor;
			this.details = details;
		}


		protected override void PaintCell(Graphics graphics, Rectangle rect, int y, AbstractTreeTableCell c)
		{
			var cell = c as TreeTableCellAmortizedAmount;
			System.Diagnostics.Debug.Assert (cell != null);

			if (cell.Value.HasValue)
			{
				var textRect = this.GetContentDeflateRectangle (rect);

				string text;
				if (this.details)
				{
					//text = TypeConverters.ComputedAmountToString (cell.Value);  // TODO...
					text = TypeConverters.AmountToString (this.accessor.GetAmortizedAmount (cell.Value));
				}
				else
				{
					text = TypeConverters.AmountToString (this.accessor.GetAmortizedAmount (cell.Value));
				}

				this.PaintText (graphics, textRect, text);
			}
		}

		protected override ContentAlignment RowContentAlignment
		{
			get
			{
				return ContentAlignment.MiddleRight;
			}
		}


		private readonly DataAccessor			accessor;
		private readonly bool					details;
	}
}

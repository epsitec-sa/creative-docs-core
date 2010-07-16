﻿//	Copyright © 2010, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Daniel ROUX, Maintainer: Daniel ROUX

using Epsitec.Common.Debug;
using Epsitec.Common.Dialogs;
using Epsitec.Common.Drawing;
using Epsitec.Common.IO;
using Epsitec.Common.Printing;
using Epsitec.Common.Support;
using Epsitec.Common.Types;
using Epsitec.Common.Widgets;

using Epsitec.Common.Support.EntityEngine;
using Epsitec.Cresus.Core.Entities;

using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Epsitec.Cresus.Core.Printers
{
	/// <summary>
	/// Dessine un BV rose.
	/// </summary>
	public class BvBand : AbstractBvBand
	{
		public BvBand()
			: base ()
		{
		}


		protected override Color LightPinkColor
		{
			get
			{
				return Color.FromHexa ("ffe8e5");  // rose très pâle
			}
		}

		protected override Color DarkPinkColor
		{
			get
			{
				return Color.FromHexa ("ff5948");  // rose
			}
		}


		protected override void PaintFix(IPaintPort port, Point topLeft)
		{
			//	Dessine tous éléments fixes, pour simuler un BV sur un fond blanc.
			base.PaintFix (port, topLeft);

			//	Dessine les traits.
			port.Color = this.DarkPinkColor;
			port.LineWidth = 0.1;

			port.PaintOutline (Path.FromLine (topLeft.X+2, topLeft.Y-67, topLeft.X+57, topLeft.Y-67));
			port.PaintOutline (Path.FromLine (topLeft.X+2, topLeft.Y-73, topLeft.X+57, topLeft.Y-73));
			port.PaintOutline (Path.FromLine (topLeft.X+2, topLeft.Y-79, topLeft.X+57, topLeft.Y-79));

			port.PaintOutline (Path.FromLine (topLeft.X+123, topLeft.Y-55, topLeft.X+204, topLeft.Y-55));
			port.PaintOutline (Path.FromLine (topLeft.X+123, topLeft.Y-61, topLeft.X+204, topLeft.Y-61));
			port.PaintOutline (Path.FromLine (topLeft.X+123, topLeft.Y-67, topLeft.X+204, topLeft.Y-67));
			port.LineWidth = 0.05;
			port.PaintOutline (Path.FromLine (topLeft.X+123, topLeft.Y-73, topLeft.X+204, topLeft.Y-73));

			//	Dessine les cases pour les montants.
			port.LineWidth = 0.1;
			Point pos;

			pos = new Point (topLeft.X+2, topLeft.Y-55);
			for (int i = 0; i < 8; i++)
			{
				this.PaintCell (port, pos);

				if (i == 1 || i == 4)
				{
					BvBand.PaintLittleTriangle (port, new Point (pos.X+4.5, pos.Y+5));
				}

				pos.X += 5;
			}

			pos = new Point (topLeft.X+48, topLeft.Y-55);
			for (int i = 0; i < 2; i++)
			{
				this.PaintCell (port, pos);

				pos.X += 5;
			}

			pos = new Point (topLeft.X+63, topLeft.Y-55);
			for (int i = 0; i < 8; i++)
			{
				this.PaintCell (port, pos);

				if (i == 1 || i == 4)
				{
					BvBand.PaintLittleTriangle (port, new Point (pos.X+4.5, pos.Y+5));
				}

				pos.X += 5;
			}

			pos = new Point (topLeft.X+109, topLeft.Y-55);
			for (int i = 0; i < 2; i++)
			{
				this.PaintCell (port, pos);

				pos.X += 5;
			}

			port.Color = Color.FromBrightness (0);
			port.PaintSurface (Path.FromCircle (topLeft.X+44.5, topLeft.Y-55, 0.4));
			port.PaintSurface (Path.FromCircle (topLeft.X+105.5, topLeft.Y-55, 0.4));

			//	Dessine les textes.
			port.Color = Color.FromBrightness (0);
			port.PaintText (topLeft.X+70, topLeft.Y-76, "105", AbstractBvBand.ocrFont, 4.2);

			port.Color = this.DarkPinkColor;
			port.PaintText (topLeft.X+123, topLeft.Y-7, "Zahlungsweck / Motif versement / Motivo versamento", AbstractBvBand.fixFontRegular, 2.0);
		}

		protected override void PaintContent(IPaintPort port, Point topLeft)
		{
			//	Dessine tous les textes variables.
			base.PaintContent (port, topLeft);

			port.Color = Color.FromBrightness (0);

			AbstractBvBand.PaintText (port, new Rectangle (topLeft.X+8, topLeft.Y-40, 50, 28), ContentAlignment.TopLeft, fixFontBold, 3.5, this.To);
			AbstractBvBand.PaintText (port, new Rectangle (topLeft.X+68, topLeft.Y-40, 50, 28), ContentAlignment.TopLeft, fixFontBold, 3.5, this.To);

			AbstractBvBand.PaintText (port, new Rectangle (topLeft.X+7, topLeft.Y-85, 50, 20), ContentAlignment.TopLeft, fixFontRegular, 2.4, this.From);
			AbstractBvBand.PaintText (port, new Rectangle (topLeft.X+129, topLeft.Y-75, 74, 24), ContentAlignment.TopLeft, fixFontRegular, 3.0, this.From);

			AbstractBvBand.PaintText (port, new Rectangle (topLeft.X+34, topLeft.Y-47, 25, 5), ContentAlignment.TopLeft, fixFontRegular, 3.0, this.EsrCustomerNumber);
			AbstractBvBand.PaintText (port, new Rectangle (topLeft.X+94, topLeft.Y-47, 25, 5), ContentAlignment.TopLeft, fixFontRegular, 3.0, this.EsrCustomerNumber);

			AbstractBvBand.PaintText (port, new Rectangle (topLeft.X+60, topLeft.Y-90, 143, 5), ContentAlignment.TopRight, ocrFont, 4.2, this.FullEsrReferenceNumber);
			AbstractBvBand.PaintText (port, new Rectangle (topLeft.X+60, topLeft.Y-98, 143, 5), ContentAlignment.TopRight, ocrFont, 4.2, this.FullEsrReferenceNumber);

			AbstractBvBand.PaintText (port, new Rectangle (topLeft.X+125, topLeft.Y-29, 50, 19), ContentAlignment.TopLeft, fixFontRegular, 3.5, this.Communication);

			BvBand.PaintPrice (port, new Point (topLeft.X+2, topLeft.Y-55), this.Price, this.NotForUse);
			BvBand.PaintPrice (port, new Point (topLeft.X+63, topLeft.Y-55), this.Price, this.NotForUse);
		}


		private void PaintCell(IPaintPort port, Point pos)
		{
			Rectangle bounds = new Rectangle (pos.X, pos.Y, 4, 5);
			bounds.Inflate (0.2);

			port.Color = Color.FromBrightness (1);
			port.PaintSurface (Path.FromRectangle (bounds));

			port.Color = this.DarkPinkColor;
			port.PaintOutline (Path.FromRectangle (bounds));
		}

		private static void PaintLittleTriangle(IPaintPort port, Point pos)
		{
			//	Dessine un petit triangle 'v' dont on donne la pointe.
			Path path = new Path ();
			path.MoveTo (pos);
			path.LineTo (new Point (pos.X-0.5, pos.Y+1));
			path.LineTo (new Point (pos.X+0.5, pos.Y+1));
			path.Close ();

			port.PaintSurface (path);
		}

		private static void PaintPrice(IPaintPort port, Point bottomLeft, decimal price, bool notForUse)
		{
			if (price == 0.0M && !notForUse)
			{
				return;
			}

			string franc = BvBand.PriceToStringFranc (price);
			string cent  = BvBand.PriceToStringCent (price);

			if (notForUse)
			{
				franc = "XXXXXXXX";
				cent  = "XX";
			}

			for (int i = 0; i < 8; i++)
			{
				Rectangle bounds = new Rectangle (bottomLeft.X+5*i, bottomLeft.Y, 4, 5);
				AbstractBvBand.PaintText (port, bounds, ContentAlignment.MiddleCenter, fixFontBold, 4.5, new string (franc[i], 1));
			}

			for (int i = 0; i < 2; i++)
			{
				Rectangle bounds = new Rectangle (bottomLeft.X+46+5*i, bottomLeft.Y, 4, 5);
				AbstractBvBand.PaintText (port, bounds, ContentAlignment.MiddleCenter, fixFontBold, 4.5, new string (franc[i], 1));
			}
		}

		private static string PriceToStringFranc(decimal price)
		{
			//	Extrait les francs (la partie entière) d'un prix.
			int franc = decimal.ToInt32 (price);
			return franc.ToString ("D8");
		}

		private static string PriceToStringCent(decimal price)
		{
			//	Extrait les centimes (la partie fractionnaire) d'un prix.
			int cent = decimal.ToInt32 (price * 100) - decimal.ToInt32 (price) * 100;
			return cent.ToString ("D2");
		}

	}
}

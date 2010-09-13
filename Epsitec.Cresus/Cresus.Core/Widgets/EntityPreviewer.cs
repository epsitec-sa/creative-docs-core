﻿//	Copyright © 2008, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Daniel ROUX, Maintainer: Daniel ROUX

using Epsitec.Common.Support;
using Epsitec.Common.Types;
using Epsitec.Common.Drawing;
using Epsitec.Common.Widgets;

using Epsitec.Common.Support.EntityEngine;

using System.Collections.Generic;
using System.Linq;

namespace Epsitec.Cresus.Core.Widgets
{
	/// <summary>
	/// Ce widget montre le contenu d'une page dans une zone rectanghlaire.
	/// </summary>
	public class EntityPreviewer : Widget
	{
		public EntityPreviewer()
		{
			this.currentPage = -1;
		}


		public Printers.AbstractDocumentPrinter DocumentPrinter
		{
			get
			{
				return this.documentPrinter;
			}
			set
			{
				this.documentPrinter = value;
			}
		}

		public int CurrentPage
		{
			get
			{
				return this.currentPage;
			}
			set
			{
				if (this.currentPage != value)
				{
					this.currentPage = value;
					this.UpdateTooltip ();
				}
			}
		}

		public string Description
		{
			get
			{
				return this.description;
			}
			set
			{
				if (this.description != value)
				{
					this.description = value;
					this.UpdateTooltip ();
				}
			}
		}


		protected override void PaintBackgroundImplementation(Graphics graphics, Rectangle clipRect)
		{
			base.PaintBackgroundImplementation (graphics, clipRect);

			if (this.documentPrinter != null)
			{
				double sx = this.Client.Bounds.Width  / this.documentPrinter.PageSize.Width;
				double sy = this.Client.Bounds.Height / this.documentPrinter.PageSize.Height;
				double scale = System.Math.Min (sx, sy);

				double offsetX = System.Math.Ceiling ((this.Client.Bounds.Width  - this.documentPrinter.PageSize.Width *scale) / 2);
				double offsetY = System.Math.Ceiling ((this.Client.Bounds.Height - this.documentPrinter.PageSize.Height*scale) / 2);

				//	Dessine le fond d'une page blanche.
				Rectangle bounds = new Rectangle (offsetX, offsetY, System.Math.Floor (this.documentPrinter.PageSize.Width*scale), System.Math.Floor (this.documentPrinter.PageSize.Height*scale));

				graphics.AddFilledRectangle (bounds);
				graphics.RenderSolid (Color.FromBrightness (1));

				//	Dessine l'entité dans la page.
				Transform initial = graphics.Transform;
				graphics.TranslateTransform (offsetX, offsetY);
				graphics.ScaleTransform (scale, scale, 0.0, 0.0);

				this.documentPrinter.CurrentPage = this.currentPage;
				this.documentPrinter.PrintBackgroundCurrentPage (graphics);
				this.documentPrinter.PrintForegroundCurrentPage (graphics);

				graphics.Transform = initial;

				//	Dessine le cadre de la page en dernier, pour recouvrir la page.
				bounds.Deflate (0.5);

				graphics.LineWidth = 1;
				graphics.AddRectangle (bounds);
				graphics.RenderSolid (Color.FromBrightness (0));
			}
		}

		private void UpdateTooltip()
		{
			var builder = new System.Text.StringBuilder ();

			builder.Append ("<font size=\"13\"><b>");
			builder.Append ("Page ");
			builder.Append ((this.currentPage+1).ToString ());
			builder.Append ("</b></font>");

			builder.Append (" (");
			builder.Append (documentPrinter.PageSize.Width.ToString ());
			builder.Append (" × ");
			builder.Append (documentPrinter.PageSize.Height.ToString ());
			builder.Append (" mm)");

			if (!string.IsNullOrEmpty (this.description))
			{
				builder.Append ("<br/>");
				builder.Append (this.description);
			}

			ToolTip.Default.SetToolTip (this, builder.ToString ());
		}


		private Printers.AbstractDocumentPrinter	documentPrinter;
		private int									currentPage;
		private string								description;
	}
}

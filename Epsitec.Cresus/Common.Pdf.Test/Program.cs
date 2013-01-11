﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Epsitec.Common.Drawing;
using Epsitec.Common.Engine.Pdf;
using Epsitec.Common.Widgets;
using Epsitec.Common.Types;

namespace Common.Pdf.Test
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine ("Début du test de Epsitec.Common.Pdf");

			Epsitec.Common.Widgets.Widget.Initialize ();

			var info = new ExportPdfInfo ();
			var export = new Export (info);
			var message = export.ExportToFile ("test.pdf", 2, Program.Renderer);

			if (string.IsNullOrEmpty (message))
			{
				Console.WriteLine ("Export ok");
			}
			else
			{
				Console.WriteLine ("message = " + message);
			}

			Console.WriteLine ("Fin du test de Epsitec.Common.Pdf");
			Console.ReadLine ();
		}

		private static void Renderer(Port port, int page)
		{
			if (page == 1)
			{
				Program.Renderer1 (port);
			}

			if (page == 2)
			{
				Program.Renderer2 (port);
			}
		}

		private static void Renderer1(Port port)
		{
			{
				var path = new Path ();
				path.MoveTo (100.0, 100.0);
				path.LineTo (100.0, 200.0);
				path.LineTo (200.0, 200.0);
				path.LineTo (200.0, 100.0);
				path.Close ();

				port.Color = Color.FromName ("Red");
				port.PaintSurface (path);
			}

			{
				var path = new Path ();
				path.MoveTo (100.0, 1000.0);
				path.LineTo (100.0, 2000.0);
				path.LineTo (1100.0, 2000.0);
				path.LineTo (1100.0, 1000.0);
				path.Close ();

				port.Color = Color.FromName ("Green");
				port.PaintSurface (path);
			}

			port.PaintText (100.0, 400.0, new Size (2000, 100), FormattedText.FromSimpleText ("Plus petit tralala..."), Font.GetFont ("Times New Roman", "Regular"), 50.0);
			port.PaintText (100.0, 300.0, new Size (2000, 100), FormattedText.FromSimpleText ("Grand tralala..."), Font.GetFont ("Arial", "Regular"), 100.0);
		}

		private static void Renderer2(Port port)
		{
			Program.PaintTextBox (port, new Rectangle (100, 2000, 1000, 500), Program.histoire, 30);
			Program.PaintTextBox (port, new Rectangle (100, 1400, 800, 400), Program.histoire, 40);
			Program.PaintTextBox (port, new Rectangle (100, 800, 500, 500), "Voici un texte contenant un mot assez long anti-constitutionnellement pour forcer les césures dans les mots.", 40);

			port.PaintText (100.0, 300.0, new Size (2000, 100), "Times <font size=\"150\">grand</font> et <font color=\"#ff0000\">rouge</font>, super !", Font.GetFont ("Times New Roman", "Regular"), 100.0);
			port.PaintText (100.0, 100.0, new Size (2000, 100), "Arial avec un mot en <b>gras</b> et un autre en <i>italique</i>.", Font.GetFont ("Arial", "Regular"), 50.0);
		}

		private static void PaintTextBox(Port port, Rectangle box, FormattedText text, double fontSize)
		{
			{
				var path = new Path ();
				path.MoveTo (box.Left - 10.0, box.Top + 10.0);
				path.LineTo (box.Left - 10.0, box.Bottom - 20.0);
				path.LineTo (box.Right + 20.0, box.Bottom - 20.0);
				path.LineTo (box.Right + 20.0, box.Top + 10.0);
				path.Close ();

				port.LineWidth = 2.0;
				port.Color = Color.FromName ("Blue");
				port.PaintOutline (path);
			}

			var style = new TextStyle
			{
				Alignment  = ContentAlignment.BottomLeft,
				BreakMode  = TextBreakMode.Hyphenate,
				JustifMode = TextJustifMode.All,
			};

			port.PaintText (box.Left, box.Bottom, box.Size, text, Font.GetFont ("Arial", "Regular"), fontSize, style);
		}

		private static string histoire = "Midi, l'heure du crime ! Un jeune vieillard assis-debout sur une pierre en bois lisait son journal plié en quatre dans sa poche à la lueur d'une bougie éteinte. Le tonnerre grondait en silence et les éclairs brillaient sombres dans la nuit claire. Il monta quatre à quatre les trois marches qui descendaient au grenier et vit par le trou de la serrure bouchée un nègre blanc qui déterrait un mort pour le manger vivant. N'écoutant que son courage de pleutre mal léché, il sortit son épée de fils de fer barbelés et leur coupa la tête au ras des pieds.";
	}
}

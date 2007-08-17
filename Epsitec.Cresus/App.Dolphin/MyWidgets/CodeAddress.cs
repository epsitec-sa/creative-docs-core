//	Copyright � 2003-2007, EPSITEC SA, CH-1092 BELMONT, Switzerland
//	Author: Daniel ROUX, Maintainer: Daniel ROUX

using System.Collections.Generic;
using Epsitec.Common.Widgets;
using Epsitec.Common.Support;
using Epsitec.Common.Drawing;

namespace Epsitec.App.Dolphin.MyWidgets
{
	/// <summary>
	/// Permet d'afficher une ligne pour une adresse.
	/// </summary>
	public class CodeAddress : Widget
	{
		public CodeAddress() : base()
		{
			this.addresses = new List<Address>();
		}

		public CodeAddress(Widget embedder) : this()
		{
			this.SetEmbedder(embedder);
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
			}

			base.Dispose(disposing);
		}


		public void ArrowClear()
		{
			this.addresses.Clear();
			this.Invalidate();
		}

		public void ArrowAdd(Address.Type type, int level, bool error)
		{
			this.addresses.Add(new Address(type, level, error));
			this.Invalidate();
		}

		public int ArrowMaxLevel
		{
			get
			{
				int max = 0;

				foreach (Address address in this.addresses)
				{
					max = System.Math.Max(max, address.Level);
				}

				return max;
			}
		}


		protected override void PaintBackgroundImplementation(Graphics graphics, Rectangle clipRect)
		{
			Rectangle rect = this.Client.Bounds;
			double y = System.Math.Floor(rect.Center.Y);

			foreach (Address address in this.addresses)
			{
				Path path = new Path();
				double x1 = rect.Left + (address.Error ? 12:0);
				double x2 = System.Math.Floor(rect.Right - address.Level*6)-1;
				
				if (address.Error)
				{
					Rectangle box = new Rectangle(rect.Left-1, rect.Bottom, x1-rect.Left, rect.Height);
					box.Deflate(0.5);

					graphics.AddFilledRectangle(box);
					graphics.RenderSolid(Color.FromRgb(1.0, 0.8, 0.0));  // orange

					graphics.AddRectangle(box);
					graphics.RenderSolid(Color.FromBrightness(0.41));  // gris fonc�

					graphics.Color = Color.FromBrightness(0);
					graphics.PaintText(box.Left, box.Bottom+1, box.Width, box.Height, "!", Font.GetFont(Font.DefaultFontFamily, "Bold"), 14, ContentAlignment.MiddleCenter);
				}

				switch (address.AddressType)
				{
					case Address.Type.StartToUp:
						path.MoveTo(x1, y);
						path.LineTo(x2, y);
						path.LineTo(x2, rect.Top);
						break;

					case Address.Type.StartToDown:
						path.MoveTo(x1, y);
						path.LineTo(x2, y);
						path.LineTo(x2, rect.Bottom);
						break;

					case Address.Type.Line:
						path.MoveTo(x2 ,rect.Bottom);
						path.LineTo(x2, rect.Top);
						break;

					case Address.Type.ArrowFromUp:
						path.MoveTo(x2, rect.Top);
						path.LineTo(x2, y);
						path.LineTo(x1, y);

						path.MoveTo(x1, y);
						path.LineTo(x1+10, y-5);
						path.MoveTo(x1, y);
						path.LineTo(x1+10, y+5);
						break;

					case Address.Type.ArrowFromDown:
						path.MoveTo(x2, rect.Bottom);
						path.LineTo(x2, y);
						path.LineTo(x1, y);

						path.MoveTo(x1, y);
						path.LineTo(x1+10, y-5);
						path.MoveTo(x1, y);
						path.LineTo(x1+10, y+5);
						break;

					case Address.Type.Arrow:
						path.MoveTo(x2, y);
						path.LineTo(x1, y);

						path.MoveTo(x1, y);
						path.LineTo(x1+10, y-5);
						path.MoveTo(x1, y);
						path.LineTo(x1+10, y+5);
						break;
				}

				Color color = CodeAddress.colors[address.Level%7];

				if (address.Error)
				{
					rect.Right = x2;
					graphics.Rasterizer.AddOutline(path, 2);
					Geometry.RenderHorizontalGradient(graphics, rect, Color.FromAlphaRgb(0, color.R, color.G, color.B), color);
				}
				else
				{
					graphics.Rasterizer.AddOutline(path, 2);
					graphics.RenderSolid(color);
				}

				path.Dispose();
			}
		}


		protected static readonly Color[] colors =
		{
			Color.FromRgb(0.0, 0.0, 0.0),  // noir
			Color.FromRgb(0.7, 0.0, 0.0),  // rouge
			Color.FromRgb(0.0, 0.7, 0.0),  // vert
			Color.FromRgb(0.0, 0.0, 0.7),  // bleu
			Color.FromRgb(0.7, 0.7, 0.0),  // jaune
			Color.FromRgb(0.7, 0.0, 0.7),  // magenta
			Color.FromRgb(0.0, 0.7, 0.7),  // cyan
		};


		public struct Address
		{
			public enum Type
			{
				None,
				StartToUp,
				StartToDown,
				Line,
				ArrowFromUp,
				ArrowFromDown,
				Arrow,
			}

			public Address(Type type, int level, bool error)
			{
				this.type = type;
				this.level = level;
				this.error = error;
			}

			public Type AddressType
			{
				get
				{
					return this.type;
				}
			}

			public int Level
			{
				get
				{
					return this.level;
				}
			}

			public bool Error
			{
				get
				{
					return this.error;
				}
			}

			private Type type;
			private int level;
			private bool error;
		}


		protected List<Address> addresses;
	}
}

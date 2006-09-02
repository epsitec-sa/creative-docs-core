//	Copyright � 2003-2006, EPSITEC SA, CH-1092 BELMONT, Switzerland
//	Responsable: Pierre ARNAUD

using Epsitec.Common.Support;
using Epsitec.Common.Types;
using Epsitec.Common.Drawing;

namespace Epsitec.Common.Widgets
{
	/// <summary>
	/// La classe ImageShower permet d'afficher une image bitmap.
	/// </summary>
	public class ImageShower : Widget
	{
		public ImageShower()
		{
		}
		
		public ImageShower(Widget embedder) : this()
		{
			this.SetEmbedder(embedder);
		}


		public Drawing.Image DrawingImage
		{
			get
			{
				return this.image;
			}

			set
			{
				this.image = value;
			}
		}




		protected override void PaintBackgroundImplementation(Drawing.Graphics graphics, Drawing.Rectangle clipRect)
		{
			//	Dessine la couleur.
			IAdorner adorner = Widgets.Adorners.Factory.Active;
			Drawing.Rectangle rect = this.Client.Bounds;

			if (this.image == null)
			{
				graphics.AddLine(rect.BottomLeft, rect.TopRight);
				graphics.AddLine(rect.BottomRight, rect.TopLeft);
				graphics.RenderSolid(adorner.ColorBorder);
			}
			else
			{
				double w = this.image.Width;
				double h = this.image.Height;

				if (rect.Width/rect.Height < w/h)
				{
					double hh = rect.Height - rect.Width*h/w;
					rect.Bottom += hh/2;
					rect.Top    -= hh/2;
				}
				else
				{
					double ww = rect.Width - rect.Height*w/h;
					rect.Left  += ww/2;
					rect.Right -= ww/2;
				}
				graphics.Align(ref rect);
				rect.Inflate(1);

				graphics.PaintImage(this.image, rect);

				rect.Deflate(0.5);
				graphics.AddRectangle(rect);
				graphics.RenderSolid(adorner.ColorBorder);
			}
		}

		protected Drawing.Image						image;
	}
}

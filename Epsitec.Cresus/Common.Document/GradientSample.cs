using Epsitec.Common.Widgets;
using Epsitec.Common.Support;

namespace Epsitec.Common.Document
{
	using BundleAttribute = Epsitec.Common.Support.BundleAttribute;
	
	/// <summary>
	/// La classe GradientSample permet de représenter une dégradé.
	/// </summary>
	
	[SuppressBundleSupport]
	
	public class GradientSample : Widgets.AbstractButton
	{
		public GradientSample()
		{
		}
		
		public GradientSample(Widget embedder) : this()
		{
			this.SetEmbedder(embedder);
		}
		
		
		// Dégradé.
		public Properties.Gradient Gradient
		{
			get
			{
				return this.gradient;
			}

			set
			{
				this.gradient = value;
				this.Invalidate();
			}
		}


		// Dessine la couleur.
		protected override void PaintBackgroundImplementation(Drawing.Graphics graphics, Drawing.Rectangle clipRect)
		{
			IAdorner adorner = Epsitec.Common.Widgets.Adorner.Factory.Active;
			Drawing.Rectangle rect = new Drawing.Rectangle(0, 0, this.Client.Width, this.Client.Height);

			if ( this.IsEnabled )
			{
				graphics.AddLine(rect.Left+0.5, rect.Bottom+0.5, rect.Right-0.5, rect.Top-0.5);
				graphics.AddLine(rect.Left+0.5, rect.Top-0.5, rect.Right-0.5, rect.Bottom+0.5);
				graphics.RenderSolid(adorner.ColorBorder);  // dessine la croix

				if ( this.gradient != null )
				{
					Drawing.Path path = new Drawing.Path();
					path.MoveTo(rect.Left, rect.Bottom);
					path.LineTo(rect.Left, rect.Top);
					path.LineTo(rect.Right, rect.Top);
					path.LineTo(rect.Right, rect.Bottom);
					path.Close();

					SurfaceAnchor sa = new SurfaceAnchor(rect);
					gradient.RenderSurface(graphics, null, path, sa);
				}
			}

			rect.Deflate(0.5);
			graphics.AddRectangle(rect);
			graphics.RenderSolid(adorner.ColorBorder);
		}


		protected Properties.Gradient			gradient;
	}
}

using Epsitec.Common.Types;
using Epsitec.Common.Drawing;

[assembly: Epsitec.Common.Types.DependencyClass(typeof(Epsitec.Common.Widgets.FrameBox))]

namespace Epsitec.Common.Widgets
{
	/// <summary>
	/// Summary description for FrameBox.
	/// </summary>
	public class FrameBox : AbstractGroup
	{
		public FrameBox()
		{
		}
		
		public FrameBox(Widget embedder) : this()
		{
			this.SetEmbedder(embedder);
		}


		protected override void PaintBackgroundImplementation(Graphics graphics, Rectangle clipRect)
		{
			if (this.DrawFullFrame)
			{
				IAdorner adorner = Widgets.Adorners.Factory.Active;

				Rectangle rect = this.Client.Bounds;
				rect.Deflate(0.5);
				graphics.AddRectangle(rect);
				graphics.RenderSolid(adorner.ColorBorder);
			}

			if (this.DrawDesignerFrame)
			{
				IAdorner adorner = Widgets.Adorners.Factory.Active;

				Rectangle rect = this.Client.Bounds;
				rect.Deflate(0.5);
				Path path = new Path();
				path.AppendRectangle(rect);
				FrameBox.DrawPathDash(graphics, path, 1, 4, 4, adorner.ColorBorder);
			}
		}

		static protected void DrawPathDash(Graphics graphics, Path path, double width, double dash, double gap, Color color)
		{
			//	Dessine un traitillé simple (dash/gap) le long d'un chemin.
			if (path.IsEmpty)
				return;

			DashedPath dp = new DashedPath();
			dp.Append(path);

			if (dash == 0.0)  // juste un point ?
			{
				dash = 0.00001;
				gap -= dash;
			}
			dp.AddDash(dash, gap);

			using (Path temp = dp.GenerateDashedPath())
			{
				graphics.Rasterizer.AddOutline(temp, width, CapStyle.Square, JoinStyle.Round, 5.0);
				graphics.RenderSolid(color);
			}
		}
	}
}

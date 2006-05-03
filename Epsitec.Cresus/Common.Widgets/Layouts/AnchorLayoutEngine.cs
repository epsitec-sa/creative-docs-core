//	Copyright � 2005-2006, EPSITEC SA, CH-1092 BELMONT, Switzerland
//	Responsable: Pierre ARNAUD

using System.Collections.Generic;

namespace Epsitec.Common.Widgets.Layouts
{
	/// <summary>
	/// Summary description for AnchorLayout.
	/// </summary>
	public sealed class AnchorLayoutEngine : ILayoutEngine
	{
		public void UpdateLayout(Visual container, Drawing.Rectangle rect, IEnumerable<Visual> children)
		{
			foreach (Visual child in children)
			{
				if ((child.Dock != DockStyle.None) ||
					(child.Anchor == AnchorStyles.None))
				{
					//	Saute les widgets qui sont "docked" dans le parent, car ils ont d�j� �t�
					//	positionn�s. Ceux qui ne sont pas ancr�s ne bougent pas non plus.
					
					continue;
				}
				
				AnchorStyles anchor_x = child.Anchor & AnchorStyles.LeftAndRight;
				AnchorStyles anchor_y = child.Anchor & AnchorStyles.TopAndBottom;
				
				Drawing.Rectangle client  = rect;
				Drawing.Margins   margins = child.Margins;

				double x1, x2, y1, y2;

				Drawing.Size size = LayoutContext.GetResultingMeasuredSize (child);

				if (size == Drawing.Size.NegativeInfinity)
				{
					return;
				}

				double dx = size.Width;
				double dy = size.Height;

				if (double.IsNaN (dx))
				{
					dx = child.ActualWidth;		//	TODO: am�liorer
				}
				if (double.IsNaN (dy))
				{
					dy = child.ActualHeight;		//	TODO: am�liorer
				}
				
				switch (anchor_x)
				{
					case AnchorStyles.Left:							//	[x1] fixe � gauche
						x1 = client.Left + margins.Left;
						x2 = x1 + dx;
						break;
					case AnchorStyles.Right:						//	[x2] fixe � droite
						x2 = client.Right - margins.Right;
						x1 = x2 - dx;
						break;
					case AnchorStyles.None:							//	ne touche � rien...
						x1 = child.ActualBounds.Left;
						x2 = child.ActualBounds.Right;
						break;
					case AnchorStyles.LeftAndRight:					//	[x1] fixe � gauche, [x2] fixe � droite
						x1 = client.Left + margins.Left;
						x2 = client.Right - margins.Right;
						break;
					default:
						throw new System.NotSupportedException (string.Format ("AnchorStyle {0} not supported", anchor_x));
				}
				
				switch (anchor_y)
				{
					case AnchorStyles.Bottom:						//	[y1] fixe en bas
						y1 = client.Bottom + margins.Bottom;
						y2 = y1 + dy;
						break;
					case AnchorStyles.Top:							//	[y2] fixe en haut
						y2 = client.Top - margins.Top;
						y1 = y2 - dy;
						break;
					case AnchorStyles.None:							//	ne touche � rien...
						y1 = child.ActualBounds.Bottom;
						y2 = child.ActualBounds.Top;
						break;
					case AnchorStyles.TopAndBottom:					//	[y1] fixe en bas, [y2] fixe en haut
						y1 = client.Bottom + margins.Bottom;
						y2 = client.Top - margins.Top;
						break;
					default:
						throw new System.NotSupportedException (string.Format ("AnchorStyle {0} not supported", anchor_y));
				}
				
				AnchorLayoutEngine.SetChildBounds (child, Drawing.Rectangle.FromPoints (x1, y1, x2, y2));
			}
		}
		
		public void UpdateMinMax(Visual container, IEnumerable<Visual> children, ref Drawing.Size min_size, ref Drawing.Size max_size)
		{
			double min_dx = 0;
			double min_dy = 0;
			double max_dx = double.PositiveInfinity;
			double max_dy = double.PositiveInfinity;

			foreach (Visual child in children)
			{
				if ((child.Dock != DockStyle.None) ||
					(child.Anchor == AnchorStyles.None))
				{
					//	Saute les widgets qui sont "docked" dans le parent, car ils sont trait�s
					//	ailleurs. Ceux qui ne sont pas ancr�s ne contribuent pas non plus.

					continue;
				}

				if (child.Visibility == false)
				{
					continue;
				}

				Drawing.Margins margins = child.Margins;

				Layouts.LayoutMeasure measure_dx = Layouts.LayoutMeasure.GetWidth (child);
				Layouts.LayoutMeasure measure_dy = Layouts.LayoutMeasure.GetHeight (child);

				AnchorStyles anchor = child.Anchor;

				switch (anchor & AnchorStyles.LeftAndRight)
				{
					case AnchorStyles.Left:
						min_dx = System.Math.Max (min_dx, margins.Left + System.Math.Max (measure_dx.Min, measure_dx.Desired));
						max_dx = System.Math.Min (max_dx, margins.Left + System.Math.Min (measure_dx.Max, measure_dx.Desired));
						break;

					case AnchorStyles.Right:
						min_dx = System.Math.Max (min_dx, margins.Right + System.Math.Max (measure_dx.Min, measure_dx.Desired));
						max_dx = System.Math.Min (max_dx, margins.Right + System.Math.Min (measure_dx.Max, measure_dx.Desired));
						break;

					case AnchorStyles.LeftAndRight:
						min_dx = System.Math.Max (min_dx, margins.Width + measure_dx.Min);
						max_dx = System.Math.Min (max_dx, margins.Width + measure_dx.Max);
						break;
				}

				switch (anchor & AnchorStyles.TopAndBottom)
				{
					case AnchorStyles.Bottom:
						min_dy = System.Math.Max (min_dy, margins.Bottom + System.Math.Max (measure_dy.Min, measure_dy.Desired));
						max_dy = System.Math.Min (max_dy, margins.Bottom + System.Math.Min (measure_dy.Max, measure_dy.Desired));
						break;

					case AnchorStyles.Top:
						min_dy = System.Math.Max (min_dy, margins.Top + System.Math.Max (measure_dy.Min, measure_dy.Desired));
						max_dy = System.Math.Min (max_dy, margins.Top + System.Math.Min (measure_dy.Max, measure_dy.Desired));
						break;

					case AnchorStyles.TopAndBottom:
						min_dy = System.Math.Max (min_dy, margins.Height + measure_dy.Min);
						max_dy = System.Math.Min (max_dy, margins.Height + measure_dy.Max);
						break;
				}
			}

			double pad_width  = container.Padding.Width  + container.GetInternalPadding ().Width;
			double pad_height = container.Padding.Height + container.GetInternalPadding ().Height;

			double min_width  = min_dx + pad_width;
			double min_height = min_dy + pad_height;
			double max_width  = max_dx + pad_width;
			double max_height = max_dy + pad_height;

			//	Tous les calculs ont �t� faits en coordonn�es client, il faut donc encore transformer
			//	ces dimensions en coordonn�es parents.

			min_size = Helpers.VisualTree.MapVisualToParent (container, new Drawing.Size (min_width, min_height));
			max_size = Helpers.VisualTree.MapVisualToParent (container, new Drawing.Size (max_width, max_height));

			Layouts.LayoutContext context = Layouts.LayoutContext.GetLayoutContext (container);
			
			if (context != null)
			{
				context.DefineMinWidth (container, min_size.Width);
				context.DefineMinHeight (container, min_size.Height);
				context.DefineMaxWidth (container, max_size.Width);
				context.DefineMaxHeight (container, max_size.Height);
			}
		}
		
		private static void SetChildBounds(Visual child, Drawing.Rectangle bounds)
		{
			double dx = child.PreferredWidth;
			double dy = child.PreferredHeight;

			switch (child.VerticalAlignment)
			{
				case VerticalAlignment.Stretch:
					break;
				case VerticalAlignment.Top:
					bounds.Bottom = bounds.Top - dy;
					break;
				case VerticalAlignment.Center:
					double h = bounds.Height;
					bounds.Top = bounds.Top - (h - dy) / 2;
					bounds.Bottom = bounds.Bottom + (h - dy) / 2;
					break;
				case VerticalAlignment.Bottom:
					bounds.Top = bounds.Bottom + dy;
					break;
			}

			switch (child.HorizontalAlignment)
			{
				case HorizontalAlignment.Stretch:
					break;
				case HorizontalAlignment.Left:
					bounds.Right = bounds.Left + dx;
					break;
				case HorizontalAlignment.Center:
					double w = bounds.Width;
					bounds.Left = bounds.Left + (w - dx) / 2;
					bounds.Right = bounds.Right - (w - dx) / 2;
					break;
				case HorizontalAlignment.Right:
					bounds.Left = bounds.Right - dx;
					break;
			}

			Drawing.Rectangle oldBounds = child.ActualBounds;
			Drawing.Rectangle newBounds = bounds;
			
			child.SetBounds (newBounds);

			if (oldBounds != newBounds)
			{
				child.Arrange (Helpers.VisualTree.FindLayoutContext (child));
			}
		}
	}
}

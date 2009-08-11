//	Copyright � 2004-2008, EPSITEC SA, 1400 Yverdon-les-Bains, Switzerland
//	Responsable: Pierre ARNAUD

namespace Epsitec.Common.Drawing
{
	public delegate RichColor ColorModifierCallback(RichColor color);

	/// <summary>
	/// The IPaintPort interface is used for the Graphics class, but also for
	/// printing (see Common.Printing project).
	/// </summary>
	public interface IPaintPort
	{
		double		LineWidth			{ get; set; }
		JoinStyle	LineJoin			{ get; set; }
		CapStyle	LineCap				{ get; set; }
		double		LineMiterLimit		{ get; set; }
		RichColor	RichColor			{ get; set; }
		Color		Color				{ get; set; }
		RichColor	FinalRichColor		{ get; set; }
		Color		FinalColor			{ get; set; }
		Transform	Transform			{ get; set; }
		FillMode	FillMode			{ get; set; }
		ImageFilter	ImageFilter			{ get; set; }
		Margins		ImageCrop			{ get; set; }
		Size		ImageFinalSize		{ get; set; }
		
		bool		HasEmptyClippingRectangle { get; }
		
		void PushColorModifier(ColorModifierCallback method);
		ColorModifierCallback PopColorModifier();

		RichColor GetFinalColor(RichColor color);
		Color GetFinalColor(Color color);

		void SetClippingRectangle(Rectangle rect);
		Rectangle SaveClippingRectangle();
		void RestoreClippingRectangle(Rectangle rect);
		void ResetClippingRectangle();
		
		void Align(ref double x, ref double y);
		
		void ScaleTransform(double sx, double sy, double cx, double cy);
		void RotateTransformDeg(double angle, double cx, double cy);
		void RotateTransformRad(double angle, double cx, double cy);
		void TranslateTransform(double ox, double oy);
		void MergeTransform(Transform transform);
		
		void PaintOutline(Path path);
		void PaintSurface(Path path);
		
		void PaintGlyphs(Font font, double size, ushort[] glyphs, double[] x, double[] y, double[] sx, double[] sy);
		
		double PaintText(double x, double y, string text, Font font, double size);
		double PaintText(double x, double y, string text, Font font, double size, FontClassInfo[] infos);
		
		void PaintImage(Image bitmap, Rectangle fill);
		void PaintImage(Image bitmap, double fill_x, double fill_y, double fill_width, double fill_height);
		void PaintImage(Image bitmap, Rectangle fill, Point image_origin);
		void PaintImage(Image bitmap, Rectangle fill, Rectangle image_rect);
		void PaintImage(Image bitmap, double fill_x, double fill_y, double fill_width, double fill_height, double image_origin_x, double image_origin_y);
		void PaintImage(Image bitmap, double fill_x, double fill_y, double fill_width, double fill_height, double image_origin_x, double image_origin_y, double image_width, double image_height);
	}
}

using NUnit.Framework;

using Epsitec.Common.Widgets;
using Epsitec.Common.Text;


namespace Epsitec.Common.Drawing
{
	[TestFixture]
	public class TextTest
	{
		[Test] public void CheckPainting()
		{
			Window  window  = new Window ();
			Painter painter = new Painter ();
			
			window.ClientSize     = new Size (500, 400);
			window.WindowLocation = ScreenInfo.Find (new Point (10, 10)).WorkingArea.TopLeft + new Point (100, -100 - window.WindowSize.Height);
			window.Text           = "TextTest/CheckPainting";
			
			painter.Parent = window.Root;
			painter.Dock   = DockStyle.Fill;
			
			
			System.Collections.ArrayList properties = new System.Collections.ArrayList ();
			
			properties.Add (new Text.Properties.FontProperty ("Arial", "Regular"));
			properties.Add (new Text.Properties.FontSizeProperty (12.0, Text.Properties.FontSizeUnits.Points));
			properties.Add (new Text.Properties.MarginsProperty (100, 0, 0, 0, 1.0, 0.0, 0.0, 15, 1, false));
			
			ulong[] text;
			string  words = "Bonjour, ceci est un texte d'exemple permettant de v�rifier le bon fonctionnement des divers algorithmes de d�coupe. Le nombre de mots moyen s'�l�ve � environ 40 mots par paragraphe, ce qui correspond � des paragraphes de taille r�duite.\n";
			
			ICursor cursor = new Text.Cursors.SimpleCursor ();
			
			painter.TextStory.NewCursor (cursor);
			painter.TextStory.ConvertToStyledText (words, properties, out text);
			painter.TextStory.InsertText (cursor, text);
			painter.TextStory.RecycleCursor (cursor);
			
			painter.NotifyTextChanged ();
			
			window.Show ();
		}
		
		
		
		private class Painter : Widget, ITextRenderer
		{
			public Painter()
			{
				this.story  = new TextStory ();
				this.fitter = new TextFitter (this.story);
				this.frame  = new SimpleTextFrame (this.Client.Width, this.Client.Height);
				
				this.fitter.FrameList.InsertAt (0, this.frame);
			}
			
			
			public TextStory					TextStory
			{
				get
				{
					return this.story;
				}
			}
			
			
			public void NotifyTextChanged()
			{
				this.fitter.ClearAllMarks ();
				this.fitter.GenerateAllMarks ();
			}
			
			
			protected override void OnSizeChanged()
			{
				base.OnSizeChanged ();
				
				if (this.frame != null)
				{
					this.frame.Width  = this.Client.Width;
					this.frame.Height = this.Client.Height;
					
					this.fitter.ClearAllMarks ();
					this.fitter.GenerateAllMarks ();
				}
			}

			protected override void PaintBackgroundImplementation(Graphics graphics, Rectangle clip_rect)
			{
				graphics.AddFilledRectangle (0, 0, this.Width, this.Height);
				graphics.RenderSolid (Color.FromName ("White"));
				
				System.Diagnostics.Debug.WriteLine ("Paint called.");
				this.fitter.RenderTextFrame (this.frame, this);
				System.Diagnostics.Debug.WriteLine ("Paint done.");
			}
			
			
			#region ITextRenderer Members
			public bool IsFrameAreaVisible(ITextFrame frame, double x, double y, double width, double height)
			{
				return this.frame == frame;
			}
			
			public void Render(ITextFrame frame, Epsitec.Common.OpenType.Font font, double size, ushort[] glyphs, double[] x, double[] y, double[] sx, double[] sy)
			{
				System.Diagnostics.Debug.WriteLine (string.Format ("Font: {0} {1}pt", font.FontIdentity.FullName, size));
				System.Diagnostics.Debug.WriteLine (string.Format ("Glyphs: {0} starting at {1}:{2}", glyphs.Length, x[0], y[0]));
			}
			#endregion
			
			
			private TextStory					story;
			private TextFitter					fitter;
			private SimpleTextFrame				frame;
		}
	}
}


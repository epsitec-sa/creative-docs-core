using NUnit.Framework;

namespace Epsitec.Common.Widgets
{
	[TestFixture] public class LayoutsGridTest
	{
		[Test] public void CheckAlgorithm()
		{
			Layouts.Grid layout = new Layouts.Grid ();
			
			Widget wa   = new Widget ();
			Widget wa1  = new Widget ();
			Widget wa11 = new Widget ();
			Widget wa2  = new Widget ();
			Widget wa3  = new Widget ();
			
			layout.Columns.Add (new Layouts.Grid.Column (50, 1));
			layout.Columns.Add (new Layouts.Grid.Column (50, 0));
			layout.Columns.Add (new Layouts.Grid.Column (50, 1));
			
			double[] min_dx;
			double[] live_dx;
			
			//	0  1  2  3  4  5  6  7  8  9 10 11 12 13 14 15
			//	[A1 [A11  40]              100][A2         50]
			//	[A3                                    140]
			//	x           x                 x              x
			// (0)         (1)               (2)            (3)
			
			wa1.Parent      = wa;
			wa1.Name        = "A1";
			wa1.MinSize     = new Drawing.Size (100, 20);
			wa1.Dock        = DockStyle.Layout;
			wa1.LayoutFlags = LayoutFlags.IncludeChildren;
			wa1.SetLayoutArgs (0, 2);
			
			wa11.Parent     = wa1;
			wa11.Name       = "A11";
			wa11.MinSize    = new Drawing.Size (40, 20);
			wa11.Dock       = DockStyle.Layout;
			wa11.SetLayoutArgs (0, 1);
			
			wa2.Parent      = wa;
			wa2.Name        = "A2";
			wa2.MinSize     = new Drawing.Size (50, 20);
			wa2.Dock        = DockStyle.Layout;
			wa2.SetLayoutArgs (2, 3);
			
			wa3.Parent      = wa;
			wa3.Name        = "A3";
			wa3.MinSize     = new Drawing.Size (140, 20);
			wa3.LayoutFlags = LayoutFlags.StartNewLine;
			wa3.Dock        = DockStyle.Layout;
			wa3.SetLayoutArgs (0, 3);
			
			layout.Root = wa;
			layout.DesiredWidth  = 100;
			layout.DesiredHeight = 100;
			
			min_dx  = layout.MinColumnWidths;
			live_dx = layout.CurrentColumnWidths;
			
			Assertion.AssertEquals (3, min_dx.Length);
			Assertion.AssertEquals (3, live_dx.Length);
			Assertion.AssertEquals (40, min_dx[0]);
			Assertion.AssertEquals (60, min_dx[1]);
			Assertion.AssertEquals (50, min_dx[2]);
			Assertion.AssertEquals (40, live_dx[0]);
			Assertion.AssertEquals (60, live_dx[1]);
			Assertion.AssertEquals (50, live_dx[2]);
			Assertion.AssertEquals (100, layout.DesiredWidth);
			Assertion.AssertEquals (150, layout.CurrentWidth);
			
			System.Console.Out.WriteLine ("{0} : {1}", wa1.Name,  wa1.Bounds);
			System.Console.Out.WriteLine ("{0} : {1}", wa11.Name, wa11.Bounds);
			System.Console.Out.WriteLine ("{0} : {1}", wa2.Name,  wa2.Bounds);
			System.Console.Out.WriteLine ("{0} : {1}", wa3.Name,  wa3.Bounds);
			
			layout.DesiredWidth = 200;
			live_dx = layout.CurrentColumnWidths;
			
			Assertion.AssertEquals (3, live_dx.Length);
			Assertion.AssertEquals (70, live_dx[0]);
			Assertion.AssertEquals (60, live_dx[1]);
			Assertion.AssertEquals (70, live_dx[2]);
			Assertion.AssertEquals (200, layout.DesiredWidth);
			Assertion.AssertEquals (200, layout.CurrentWidth);
			
			
			wa3.MinSize = new Drawing.Size (160, 20);
			wa3.SetLayoutArgs (0, 3);
			layout.Invalidate ();
			min_dx = layout.MinColumnWidths;
			
			Assertion.AssertEquals (3, min_dx.Length);
			Assertion.AssertEquals (40, min_dx[0]);
			Assertion.AssertEquals (60, min_dx[1]);
			Assertion.AssertEquals (60, min_dx[2]);
			
			wa3.MinSize = new Drawing.Size (80, 20);
			wa3.SetLayoutArgs (1, 2);
			layout.Invalidate ();
			min_dx = layout.MinColumnWidths;
			
			Assertion.AssertEquals (3, min_dx.Length);
			Assertion.AssertEquals (40, min_dx[0]);
			Assertion.AssertEquals (80, min_dx[1]);
			Assertion.AssertEquals (50, min_dx[2]);
		}
		
		[Test] public void CheckDesigner()
		{
			Layouts.Grid layout = new Layouts.Grid ();
			Window       window = new Window ();
			
			Widget wa   = new StaticText ("");		wa.BackColor   = Drawing.Color.FromBrightness (1.0);
			Widget wa1  = new StaticText ("A1");	wa1.BackColor  = Drawing.Color.FromARGB (1.0, 1.0, 1.0, 0.6);	wa1.Height = 40;
			Widget wa2  = new StaticText ("A2");	wa2.BackColor  = Drawing.Color.FromARGB (1.0, 0.6, 1.0, 1.0);
			Widget wa3  = new StaticText ("A3");	wa3.BackColor  = Drawing.Color.FromARGB (1.0, 0.6, 1.0, 0.6);
			Widget wa4  = new StaticText ("A4");	wa4.BackColor  = Drawing.Color.FromARGB (1.0, 1.0, 0.8, 0.6);
			Widget wa5  = new StaticText ("A5");	wa5.BackColor  = Drawing.Color.FromARGB (1.0, 1.0, 0.6, 0.8);	wa5.Height = 60;
			
			layout.Columns.Add (new Layouts.Grid.Column (50, 1));
			layout.Columns.Add (new Layouts.Grid.Column (50, 0));
			layout.Columns.Add (new Layouts.Grid.Column (50, 1));
			
			wa1.Parent      = wa;
			wa1.Name        = "A1";
			wa1.MinSize     = new Drawing.Size (30, 30);
			wa1.Dock        = DockStyle.Layout;
			wa1.LayoutFlags = LayoutFlags.IncludeChildren;
			wa1.SetLayoutArgs (0, 2);
			
			wa2.Parent      = wa;
			wa2.Name        = "A2";
			wa2.MinSize     = new Drawing.Size (30, 30);
			wa2.Dock        = DockStyle.Layout;
			wa2.SetLayoutArgs (2, 3);
			
			wa3.Parent      = wa;
			wa3.Name        = "A3";
			wa3.MinSize     = new Drawing.Size (30, 30);
			wa3.LayoutFlags = LayoutFlags.StartNewLine;
			wa3.Dock        = DockStyle.Layout;
			wa3.SetLayoutArgs (0, 3);
			
			wa4.Parent      = wa;
			wa4.Name        = "A4";
			wa4.MinSize     = new Drawing.Size (30, 30);
			wa4.LayoutFlags = LayoutFlags.StartNewLine;
			wa4.Dock        = DockStyle.Layout;
			wa4.SetLayoutArgs (0, 1);
			
			wa5.Parent      = wa;
			wa5.Name        = "A5";
			wa5.MinSize     = new Drawing.Size (30, 30);
			wa5.Dock        = DockStyle.Layout;
			wa5.LayoutFlags = LayoutFlags.StartNewLine;
			wa5.SetLayoutArgs (1, 3);
			
			layout.Root = wa;
			layout.DesiredWidth  = 200;
			layout.DesiredHeight = 150;
			layout.Invalidate ();
			
			StaticText frame = new StaticText ();
			
			frame.Width     = layout.CurrentWidth + 50;
			frame.Height    = layout.DesiredHeight + 50;
			frame.Parent    = window.Root;
			frame.Location  = new Drawing.Point (10, 10);
			frame.BackColor = Drawing.Color.FromRGB (0, 0, 0.5);
			
			frame.Height = wa.Height + 8;
			wa.Location  = new Drawing.Point (4, 4);
			wa.Parent    = frame;
			frame.Dock   = DockStyle.Fill;
			
			HScroller scroll = new HScroller ();
			
			scroll.Parent = window.Root;
			scroll.Dock   = DockStyle.Bottom;
			scroll.ValueChanged += new EventHandler (CheckDesignerScrollValueChanged);
			scroll.Minimum = 0;
			scroll.Maximum = 100;
			scroll.Display = 70;
			scroll.SmallChange = 10;
			scroll.LargeChange = 25;
			
			this.views[scroll] = wa;
			
			window.ClientSize = new Drawing.Size (frame.Width + 20, frame.Height + 50);
			
			wa.SetClientZoom (1);
			wa.SetClientAngle (0);
			wa.SetClientOffset (0, 0);
			
			window.Show ();
		}
		
		private System.Collections.Hashtable	views = new System.Collections.Hashtable ();
		
		private void CheckDesignerScrollValueChanged(object sender)
		{
			AbstractScroller scroller = sender as AbstractScroller;
			Widget           surface  = this.views[scroller] as Widget;
			
			surface.SetClientOffset (-scroller.Value, 0);
			surface.Invalidate ();
		}
	}
}

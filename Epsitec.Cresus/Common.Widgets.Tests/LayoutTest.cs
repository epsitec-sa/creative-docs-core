using NUnit.Framework;

namespace Epsitec.Common.Widgets
{
	using PropertyChangedEventHandler = Epsitec.Common.Support.EventHandler<Epsitec.Common.Types.DependencyPropertyChangedEventArgs>;
	
	[TestFixture] public class LayoutTest
	{
		[Test]
		public void AutomatedTestEnvironment()
		{
			Epsitec.Common.Widgets.Window.RunningInAutomatedTestEnvironment = true;
		}
		
		[Test]
		public void CheckMeasure()
		{
			Visual a = new Visual ();
			Visual b = new Visual ();
			Visual c1 = new Visual ();
			Visual c2 = new Visual ();

			Visual[] array;

			a.Children.Add (b);
			b.Children.Add (c1);
			b.Children.Add (c2);

			Layouts.LayoutContext context = Helpers.VisualTree.GetLayoutContext (a);
			
			Assert.IsNotNull (context);
			Assert.AreEqual (0, context.MeasureQueueLength);
			
			context.StartNewLayoutPass ();

			Layouts.LayoutContext.AddToMeasureQueue (c1);
			
			Assert.AreEqual (1, context.MeasureQueueLength);

			Assert.IsFalse (a.ContainsLocalValue (Layouts.LayoutMeasure.WidthProperty));
			Assert.IsFalse (a.ContainsLocalValue (Layouts.LayoutMeasure.HeightProperty));
			Assert.IsFalse (b.ContainsLocalValue (Layouts.LayoutMeasure.WidthProperty));
			Assert.IsFalse (b.ContainsLocalValue (Layouts.LayoutMeasure.HeightProperty));
			Assert.IsFalse (c1.ContainsLocalValue (Layouts.LayoutMeasure.WidthProperty));
			Assert.IsFalse (c1.ContainsLocalValue (Layouts.LayoutMeasure.HeightProperty));
			Assert.IsFalse (c2.ContainsLocalValue (Layouts.LayoutMeasure.WidthProperty));
			Assert.IsFalse (c2.ContainsLocalValue (Layouts.LayoutMeasure.HeightProperty));
			
			Layouts.LayoutContext.AddToMeasureQueue (c1);

			Assert.AreEqual (1, context.MeasureQueueLength);

			context.ExecuteMeasure ();
			
			Assert.AreEqual (0, context.MeasureQueueLength);
			
			Assert.IsTrue (c1.ContainsLocalValue (Layouts.LayoutMeasure.WidthProperty));
			Assert.IsTrue (c1.ContainsLocalValue (Layouts.LayoutMeasure.HeightProperty));

			Layouts.LayoutMeasure dxMeasure = Layouts.LayoutMeasure.GetWidth (c1);
			Layouts.LayoutMeasure dyMeasure = Layouts.LayoutMeasure.GetHeight (c1);

			Assert.AreEqual (0, dxMeasure.Min);
			Assert.AreEqual (0, dyMeasure.Min);
			Assert.AreEqual (double.PositiveInfinity, dxMeasure.Max);
			Assert.AreEqual (double.PositiveInfinity, dyMeasure.Max);
			Assert.AreEqual (c1.PreferredWidth, dxMeasure.Desired);
			Assert.AreEqual (c1.PreferredHeight, dyMeasure.Desired);

			c1.MinWidth = 20;

			Assert.AreEqual (1, context.MeasureQueueLength);
			
			c1.MinHeight = 12;
			c1.MaxHeight = 30;

			Assert.AreEqual (1, context.MeasureQueueLength);
			
			context.ExecuteMeasure ();

			Assert.AreEqual (dxMeasure, Layouts.LayoutMeasure.GetWidth (c1));
			Assert.AreEqual (dyMeasure, Layouts.LayoutMeasure.GetHeight (c1));

			Assert.AreEqual (20, dxMeasure.Min);
			Assert.AreEqual (double.PositiveInfinity, dxMeasure.Max);
			Assert.AreEqual (12, dyMeasure.Min);
			Assert.AreEqual (30, dyMeasure.Max);

			//	Au sein d'une m�me passe de layout, un minimum ne peut que cro�tre
			//	et un maximum que diminuer.
			
			c1.MinWidth = 15;
			c1.MaxHeight = 40;
			
			context.ExecuteMeasure ();
			
			Assert.AreEqual (20, dxMeasure.Min);
			Assert.AreEqual (30, dyMeasure.Max);
			
			context.StartNewLayoutPass ();

			c1.MinWidth = 10;
			c1.MaxHeight = 50;

			b.PreferredWidth = 100;

			Assert.AreEqual (2, context.MeasureQueueLength);
			
			array = Types.Collection.ToArray (context.GetMeasureQueue ());

			Assert.AreEqual (c1, array[0]);
			Assert.AreEqual (b, array[1]);

			c2.MinWidth = 40;

			Assert.AreEqual (3, context.MeasureQueueLength);
			
			array = Types.Collection.ToArray (context.GetMeasureQueue ());

			Assert.AreEqual (c1, array[0]);
			Assert.AreEqual (c2, array[1]);
			Assert.AreEqual (b, array[2]);

			context.ExecuteMeasure ();

			Assert.AreEqual (10, dxMeasure.Min);
			Assert.AreEqual (50, dyMeasure.Max);
		}

		[Test]
		public void CheckArrange1()
		{
			Visual a = new Visual ();
			Visual b = new Visual ();
			Visual c1 = new Visual ();
			Visual c2 = new Visual ();

			Visual[] array;

			a.Children.Add (b);
			b.Children.Add (c1);
			b.Children.Add (c2);

			Layouts.LayoutContext context = Helpers.VisualTree.GetLayoutContext (a);

			context.StartNewLayoutPass ();

			b.Dock = DockStyle.Top;

			Assert.AreEqual (0, context.MeasureQueueLength);
			Assert.AreEqual (1, context.ArrangeQueueLength);

			array = Types.Collection.ToArray (context.GetArrangeQueue ());

			Assert.AreEqual (a, array[0]);

			b.Padding = new Drawing.Margins (5, 5, 5, 5);

			Assert.AreEqual (0, context.MeasureQueueLength);
			Assert.AreEqual (2, context.ArrangeQueueLength);

			array = Types.Collection.ToArray (context.GetArrangeQueue ());

			Assert.AreEqual (a, array[0]);
			Assert.AreEqual (b, array[1]);
		}
		
		[Test]
		public void CheckArrange2()
		{
			Visual a = new Visual ();
			Visual b = new Visual ();
			Visual c1 = new Visual ();
			Visual c2 = new Visual ();
			
			Visual[] array;

			a.Children.Add (b);
			b.Children.Add (c1);
			b.Children.Add (c2);

			c1.Dock = DockStyle.Top;
			c2.Dock = DockStyle.Top;

			Layouts.LayoutContext context = Helpers.VisualTree.GetLayoutContext (a);

			context.StartNewLayoutPass ();

			b.Dock = DockStyle.Top;
			c1.MinWidth = 20;
			c1.MinHeight = 10;

			Assert.AreEqual (1, context.MeasureQueueLength);
			Assert.AreEqual (1, context.ArrangeQueueLength);

			context.ExecuteMeasure ();

			Assert.AreEqual (0, context.MeasureQueueLength);
			Assert.AreEqual (2, context.ArrangeQueueLength);
			
			array = Types.Collection.ToArray (context.GetArrangeQueue ());

			Assert.AreEqual (a, array[0]);
			Assert.AreEqual (b, array[1]);
		}

		[Test]
		public void CheckArrange3()
		{
			Visual a = new Visual ();
			Visual b = new Visual ();
			Visual c1 = new Visual ();
			Visual c2 = new Visual ();

			Visual[] array;

			a.Name = "a";
			b.Name = "b";
			c1.Name = "c1";
			c2.Name = "c2";
			
			a.Children.Add (b);
			b.Children.Add (c1);
			b.Children.Add (c2);

			Layouts.LayoutContext context = Helpers.VisualTree.GetLayoutContext (a);

			//	Oublie tout ce qui a pu �tre g�n�r� par la construction de l'arbre
			//	ci-dessus :
			
			context.StartNewLayoutPass ();

			a.Bounds = new Drawing.Rectangle (0, 0, 100, 200);

			b.Dock = DockStyle.Fill;
			b.Padding = new Drawing.Margins (5, 5, 10, 10);
			
			c1.Dock = DockStyle.Top;
			c2.Dock = DockStyle.Top;

			c1.PreferredHeight = 20;
			c2.PreferredHeight = 30;

			array = Types.Collection.ToArray (context.GetMeasureQueue ());

			Assert.AreEqual (3, array.Length);
			Assert.AreEqual (c1, array[0]);
			Assert.AreEqual (c2, array[1]);
			Assert.AreEqual (a, array[2]);

			array = Types.Collection.ToArray (context.GetArrangeQueue ());

			Assert.AreEqual (2, array.Length);
			Assert.AreEqual (a, array[0]);
			Assert.AreEqual (b, array[1]);
			
			context.ExecuteMeasure ();

			array = Types.Collection.ToArray (context.GetArrangeQueue ());

			Assert.AreEqual (2, array.Length);
			Assert.AreEqual (a, array[0]);
			Assert.AreEqual (b, array[1]);

			context.ExecuteArrange ();

			Assert.AreEqual (0, context.ArrangeQueueLength);
			Assert.AreEqual (0, context.MeasureQueueLength);

			System.Console.Out.WriteLine ("a: {0}", a.Bounds);
			System.Console.Out.WriteLine ("b: {0}", b.Bounds);
			System.Console.Out.WriteLine ("c1: {0}", c1.Bounds);
			System.Console.Out.WriteLine ("c2: {0}", c2.Bounds);
		}

		[Test]
		public void CheckRealExample()
		{
			Window window = AdornerTest.CreateAdornerWidgets ();
			
			Layouts.LayoutContext context = Helpers.VisualTree.GetLayoutContext (window.Root);

			System.Console.Out.WriteLine ("Measure: {0} elements", context.MeasureQueueLength);
			System.Console.Out.WriteLine ("Arrange: {0} elements", context.ArrangeQueueLength);

			context.ExecuteMeasure ();

			System.Console.Out.WriteLine ("After Measure, arrange: {0} elements", context.ArrangeQueueLength);

			context.ExecuteArrange ();

			System.Console.Out.WriteLine ("After Arrange, measure: {0} elements", context.MeasureQueueLength);
			System.Console.Out.WriteLine ("After Arrange, arrange: {0} elements", context.ArrangeQueueLength);

			Window.RunInTestEnvironment (window);
		}
	}
}
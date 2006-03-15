﻿using NUnit.Framework;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters.Soap;
using System.Collections.Generic;

[assembly: Epsitec.Common.Types.DependencyClass (typeof (Epsitec.Common.Types.SerializationTest.MyItem))]
[assembly: Epsitec.Common.Types.DependencyClass (typeof (Epsitec.Common.Types.SerializationTest.MySimpleObject))]

namespace Epsitec.Common.Types
{
	public delegate object MyAllocator();
	
	[TestFixture] public class SerializationTest
	{
		[SetUp]
		public void Initialise()
		{
			Serialization.DependencyClassManager.Setup ();
		}

		[Test]
		public void CheckAllocationSpeed()
		{
			System.Type type = typeof (MySimpleObject);
			int steps = 1000*100;

			System.Reflection.Emit.DynamicMethod dm = new System.Reflection.Emit.DynamicMethod ("MyCtor", type, System.Type.EmptyTypes, typeof (SerializationTest).Module, true);
			System.Reflection.Emit.ILGenerator ilgen = dm.GetILGenerator ();
			
			ilgen.Emit (System.Reflection.Emit.OpCodes.Nop);
			ilgen.Emit (System.Reflection.Emit.OpCodes.Newobj, type.GetConstructor (System.Type.EmptyTypes));
			ilgen.Emit (System.Reflection.Emit.OpCodes.Ret);

			MyAllocator allocator = (MyAllocator) dm.CreateDelegate (typeof (MyAllocator));

			System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch ();

			stopwatch.Start ();

			for (int i = 0; i < steps; i++)
			{
				//	allocator: 0.45 μs
				allocator ();
			}

			stopwatch.Stop ();
			System.Console.Out.WriteLine ("DynamicMethod + ILGen: {0} μs, {1} executions.", 1.0M * stopwatch.ElapsedMilliseconds / (steps / 1000), steps);

			stopwatch.Reset ();
			stopwatch.Start ();

			for (int i = 0; i < steps; i++)
			{
				//	Activator: 17.2 μs (38 x plus lent que activator)
				System.Activator.CreateInstance (type);
			}

			stopwatch.Stop ();
			System.Console.Out.WriteLine ("Activator.CreateInstance: {0} μs, {1} executions.", 1.0M * stopwatch.ElapsedMilliseconds / (steps / 1000), steps);

			DependencyObjectType depType = DependencyObjectType.FromSystemType (type);
			depType.CreateEmptyObject ();

			stopwatch.Reset ();
			stopwatch.Start ();

			for (int i = 0; i < steps; i++)
			{
				//	Même méthode que l'allocator ci-dessus...
				depType.CreateEmptyObject ();
			}

			stopwatch.Stop ();
			System.Console.Out.WriteLine ("CreateEmptyObject: {0} μs, {1} executions.", 1.0M * stopwatch.ElapsedMilliseconds / (steps / 1000), steps);
		}

		[Test]
		public void CheckAttributes()
		{
			List<string> names = new List<string> ();

			foreach (System.Type type in DependencyClassAttribute.GetRegisteredTypes (this.GetType ().Assembly))
			{
				names.Add (type.FullName);
			}
			
			string[] array = new string[] { "Epsitec.Common.Types.SerializationTest+MyItem", "Epsitec.Common.Types.SerializationTest+MySimpleObject" };

			Assert.IsTrue (Collection.ContainsAll (names, array));
		}

		[Test]
		public void CheckDependencyClassManager()
		{
			DependencyObjectType t1 = Serialization.DependencyClassManager.Current.FindObjectType ("Epsitec.Common.Types.SerializationTest+MyItem");
			DependencyObjectType t2 = Serialization.DependencyClassManager.Current.FindObjectType ("Epsitec.Common.Types.SerializationTest+MySimpleObject");

			Assert.AreEqual (typeof (MyItem), t1.SystemType);
			Assert.AreEqual (typeof (MySimpleObject), t2.SystemType);
		}

		[Test]
		public void CheckMap()
		{
			Serialization.Map map = new Epsitec.Common.Types.Serialization.Map ();

			Assert.AreEqual (0, map.GetNullId ());
			Assert.AreEqual (0, map.GetId (null));
			Assert.IsNull (map.GetObject (0));
			Assert.AreEqual (1, Collection.Count (map.RecordedObjects));
			Assert.IsTrue (map.IsDefined (null));

			map.Record (null);
			
			Assert.AreEqual (1, Collection.Count (map.RecordedObjects));

			MyItem a = new MyItem ();
			MyItem b = new MyItem ();
			MyItem c = new MyItem ();

			MySimpleObject s1 = new MySimpleObject ();
			MySimpleObject s2 = new MySimpleObject ();
			
			map.Record (a);

			Assert.AreEqual (1, map.GetId (a));
			Assert.AreEqual (a, map.GetObject (1));
			Assert.AreEqual (2, Collection.Count (map.RecordedObjects));
			Assert.IsTrue (map.IsDefined (a));
			Assert.IsTrue (map.IsDefined (1));
			Assert.IsFalse (map.IsDefined (b));
			Assert.IsFalse (map.IsDefined (2));
			Assert.AreEqual (1, Collection.Count (map.RecordedTypes));
			Assert.AreEqual (typeof (MyItem), Collection.ToList (map.RecordedTypes)[0]);
			Assert.AreEqual (-1, map.GetId (b));

			map.Record (b);
			map.Record (b);

			Assert.AreEqual (2, map.GetId (b));
			Assert.AreEqual (b, map.GetObject (2));
			Assert.AreEqual (3, Collection.Count (map.RecordedObjects));

			map.Record (s1);
			map.Record (c);
			map.Record (s2);

			Assert.AreEqual (3, map.GetId (s1));
			Assert.AreEqual (4, map.GetId (c));
			Assert.AreEqual (5, map.GetId (s2));

			Assert.AreEqual (2, Collection.Count (map.RecordedTypes));
			Assert.AreEqual (typeof (MyItem), Collection.ToList (map.RecordedTypes)[0]);
			Assert.AreEqual (typeof (MySimpleObject), Collection.ToList (map.RecordedTypes)[1]);

			Assert.AreEqual (3, Collection.Count (map.GetObjects (typeof (MyItem))));
			Assert.AreEqual (2, Collection.Count (map.GetObjects (typeof (MySimpleObject))));

			Assert.AreEqual (a, Collection.ToList (map.GetObjects (typeof (MyItem)))[0]);
			Assert.AreEqual (b, Collection.ToList (map.GetObjects (typeof (MyItem)))[1]);
			Assert.AreEqual (c, Collection.ToList (map.GetObjects (typeof (MyItem)))[2]);
			
			Assert.AreEqual (s1, Collection.ToList (map.GetObjects (typeof (MySimpleObject)))[0]);
			Assert.AreEqual (s2, Collection.ToList (map.GetObjects (typeof (MySimpleObject)))[1]);
		}

		[Test]
		[ExpectedException (typeof (System.Collections.Generic.KeyNotFoundException))]
		public void CheckMapEx1()
		{
			Serialization.Map map = new Epsitec.Common.Types.Serialization.Map ();
			
			map.GetObject (1);
		}

		[Test]
		public void CheckVisitSerializableNodes()
		{
			MyItem a = new MyItem ();
			MyItem b = new MyItem ();
			MyItem q = new MyItem ();
			MyItem c1 = new MyItem ();
			MyItem c2 = new MyItem ();

			a.AddChild (b);
			a.AddChild (q);
			b.AddChild (c1);
			b.AddChild (c2);

			a.Name = "a";
			b.Name = "b";
			q.Name = "q";
			c1.Name = "c1";
			c2.Name = "c2";

			a.Value = "A";
			b.Value = "B";
			q.Value = "Q";
			c1.Value = "C1";
			c2.Value = "C2";
			
			//	a --+--> b --+--> c1
			//	    |        +--> c2
			//	    +--> q

			Serialization.GraphVisitor visitor = new Serialization.GraphVisitor ();
			
			visitor.VisitSerializableNodes (a);

			List<DependencyObject> objects = new List<DependencyObject> ();
			
			foreach (DependencyObject obj in visitor.Map.RecordedObjects)
			{
				objects.Add (obj);
			}

			Assert.AreEqual (null, objects[0]);
			Assert.AreEqual (a, objects[1]);
			Assert.AreEqual (b, objects[2]);
			Assert.AreEqual (c1, objects[3]);
			Assert.AreEqual (c2, objects[4]);
			Assert.AreEqual (q, objects[5]);
		}
		
		#region Class EventHandlerSupport
		private class EventHandlerSupport
		{
			public string Log
			{
				get
				{
					return this.buffer.ToString ();
				}
			}

			public void RecordEvent(object sender, DependencyPropertyChangedEventArgs e)
			{
				this.buffer.Append (e.PropertyName);
				this.buffer.Append (":");
				this.buffer.Append (e.OldValue);
				this.buffer.Append (",");
				this.buffer.Append (e.NewValue);
				this.buffer.Append (".");
			}
			public void RecordEventAndName(object sender, DependencyPropertyChangedEventArgs e)
			{
				this.buffer.Append (DependencyObjectTree.GetName (sender as DependencyObject));
				this.buffer.Append ("-");
				this.buffer.Append (e.PropertyName);
				this.buffer.Append (":");
				this.buffer.Append (e.OldValue);
				this.buffer.Append (",");
				this.buffer.Append (e.NewValue);
				this.buffer.Append (".");
			}
			public void Clear()
			{
				this.buffer.Length = 0;
			}

			System.Text.StringBuilder buffer = new System.Text.StringBuilder ();
		}
		#endregion

		#region MyItem Class

		public class MyItem : DependencyObject
		{
			public MyItem()
			{
			}
			
			public string						Name
			{
				get
				{
					return this.GetValue (MyItem.NameProperty) as string;
				}
				set
				{
					this.SetValue (MyItem.NameProperty, value);
				}
			}
			public MyItem						Parent
			{
				get
				{
					return this.parent;
				}
			}
			public IList<MyItem>				Children
			{
				get
				{
					return this.children;
				}
			}
			public bool							HasChildren
			{
				get
				{
					return this.children == null ? false : (this.children.Count > 0);
				}
			}
			public string						Value
			{
				get
				{
					return this.GetValue (MyItem.ValueProperty) as string;
				}
				set
				{
					this.SetValue (MyItem.ValueProperty, value);
				}
			}
			public string						Cascade
			{
				get
				{
					return this.GetValue (MyItem.CascadeProperty) as string;
				}
				set
				{
					this.SetValue (MyItem.CascadeProperty, value);
				}
			}
			
			public void AddChild(MyItem item)
			{
				DependencyObjectTreeSnapshot snapshot = DependencyObjectTree.CreateInheritedPropertyTreeSnapshot (item);
				
				if (this.children == null)
				{
					this.children = new List<MyItem> ();
				}
				if (item.parent != null)
				{
					item.parent.children.Remove (item);
				}
				this.children.Add (item);

				item.parent = this;
				
				snapshot.AddNewInheritedProperties (item);
				snapshot.InvalidateDifferentProperties ();
			}
			
			public static object GetValueParent(DependencyObject o)
			{
				MyItem tt = o as MyItem;
				return tt.Parent;
			}
			public static object GetValueChildren(DependencyObject o)
			{
				MyItem tt = o as MyItem;
				if (tt.children == null)
				{
					return new DependencyObject[0];
				}
				else
				{
					DependencyObject[] copy = tt.children.ToArray ();
					return copy;
				}
			}
			public static object GetValueHasChildren(DependencyObject o)
			{
				MyItem tt = o as MyItem;
				return tt.HasChildren;
			}

			public static DependencyProperty NameProperty = DependencyObjectTree.NameProperty.AddOwner (typeof (MyItem));
			public static DependencyProperty ParentProperty = DependencyObjectTree.ParentProperty.AddOwner (typeof (MyItem), new DependencyPropertyMetadata (MyItem.GetValueParent));
			public static DependencyProperty ChildrenProperty = DependencyObjectTree.ChildrenProperty.AddOwner (typeof (MyItem), new DependencyPropertyMetadata (MyItem.GetValueChildren).MakeReadOnlySerializable ());
			public static DependencyProperty HasChildrenProperty = DependencyObjectTree.HasChildrenProperty.AddOwner (typeof (MyItem), new DependencyPropertyMetadata (MyItem.GetValueHasChildren));
			public static DependencyProperty ValueProperty = DependencyProperty.Register ("Value", typeof (string), typeof (MyItem));
			public static DependencyProperty CascadeProperty = DependencyProperty.Register ("Cascade", typeof (string), typeof (MyItem), new DependencyPropertyMetadataWithInheritance (UndefinedValue.Instance));

			MyItem parent;
			List<MyItem> children;
		}
		
		#endregion

		#region MySimpleObject Class

		internal class MySimpleObject : DependencyObject
		{
			public MySimpleObject()
			{
			}
		}

		#endregion
	}
}

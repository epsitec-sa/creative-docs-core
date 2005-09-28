//	Copyright � 2005, EPSITEC SA, CH-1092 BELMONT, Switzerland
//	Responsable: Pierre ARNAUD

namespace Epsitec.Common.Text.Properties
{
	/// <summary>
	/// La classe TabsProperty d�crit une tabulation.
	/// </summary>
	public class TabsProperty : Property
	{
		public TabsProperty()
		{
		}
		
		public TabsProperty(params TabProperty[] tabs)
		{
			string[] tags = new string[tabs.Length];
			
			for (int i = 0; i < tabs.Length; i++)
			{
				tags[i] = tabs[i].TabTag;
			}
			
			this.DefineTabTags (tags);
		}
		
		public TabsProperty(params string[] tab_tags)
		{
			this.DefineTabTags (tab_tags);
		}
		
		
		public override WellKnownType			WellKnownType
		{
			get
			{
				return WellKnownType.Tabs;
			}
		}
		
		public override PropertyType			PropertyType
		{
			get
			{
				return PropertyType.LocalSetting;
			}
		}
		
		public override PropertyAffinity		PropertyAffinity
		{
			get
			{
				return PropertyAffinity.Symbol;
			}
		}


		public override bool					RequiresUniformParagraph
		{
			get
			{
				return true;
			}
		}
		
		
		public string[]							TabTags
		{
			get
			{
				return (string[]) this.tab_tags.Clone ();
			}
		}
		
		
		public override Property EmptyClone()
		{
			return new TabsProperty ();
		}
		
		public override void SerializeToText(System.Text.StringBuilder buffer)
		{
			SerializerSupport.Join (buffer,
				/**/				SerializerSupport.SerializeStringArray (this.tab_tags));
		}
		
		public override void DeserializeFromText(Context context, string text, int pos, int length)
		{
			string[] args = SerializerSupport.Split (text, pos, length);
			
			Debug.Assert.IsTrue (args.Length == 1);
			
			string[] tab_tags = SerializerSupport.DeserializeStringArray (args[0]);
			
			this.tab_tags = tab_tags;
		}
		
		
		public override Property GetCombination(Property property)
		{
			Debug.Assert.IsTrue (property is Properties.TabsProperty);
			
			TabsProperty a = this;
			TabsProperty b = property as TabsProperty;
			
			System.Collections.ArrayList tags = new System.Collections.ArrayList ();
			
			if ((a.tab_tags != null) &&
				(a.tab_tags.Length > 0))
			{
				foreach (string tag in a.tab_tags)
				{
					if (tags.Contains (tag) == false)
					{
						tags.Add (tag);
					}
				}
			}
			
			if ((b.tab_tags != null) &&
				(b.tab_tags.Length > 0))
			{
				foreach (string tag in b.tab_tags)
				{
					if (tags.Contains (tag) == false)
					{
						tags.Add (tag);
					}
				}
			}
			
			TabsProperty c = new TabsProperty ((string[]) tags.ToArray (typeof (string)));
			
			c.DefineVersion (System.Math.Max (a.Version, b.Version));
			
			return c;
		}
		
		
		public override void UpdateContentsSignature(IO.IChecksum checksum)
		{
			checksum.UpdateValue (this.tab_tags);
		}
		
		public override bool CompareEqualContents(object value)
		{
			return TabsProperty.CompareEqualContents (this, value as TabsProperty);
		}
		
		
		private void DefineTabTags(string[] tags)
		{
			this.tab_tags = (string[]) tags.Clone ();
			this.Invalidate ();
		}
		
		
		private static bool CompareEqualContents(TabsProperty a, TabsProperty b)
		{
			return Types.Comparer.Equal (a.tab_tags, b.tab_tags);
		}
		
		
		private string[]						tab_tags;
	}
}

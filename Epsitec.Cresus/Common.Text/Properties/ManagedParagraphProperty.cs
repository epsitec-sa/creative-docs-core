//	Copyright � 2005-2007, EPSITEC SA, CH-1092 BELMONT, Switzerland
//	Responsable: Pierre ARNAUD

namespace Epsitec.Common.Text.Properties
{
	/// <summary>
	/// La classe ManagedParagraphProperty d�crit un paragraphe g�r� par une
	/// class impl�mentant IParagraphManager (en principe une liste � puces,
	/// par exemple) qui g�n�re du texte automatique (AutoText).
	/// </summary>
	public class ManagedParagraphProperty : Property
	{
		public ManagedParagraphProperty()
		{
		}
		
		public ManagedParagraphProperty(string manager_name, string[] manager_parameters)
		{
			this.manager_name       = manager_name;
			this.manager_parameters = manager_parameters == null ? new string[0] : (manager_parameters.Clone () as string[]);
		}
		
		
		public override WellKnownType			WellKnownType
		{
			get
			{
				return WellKnownType.ManagedParagraph;
			}
		}
		
		public override PropertyType			PropertyType
		{
			get
			{
				return PropertyType.CoreSetting;
			}
		}
		
		public override CombinationMode			CombinationMode
		{
			get
			{
				return CombinationMode.Combine;
			}
		}
		
		public override bool					RequiresUniformParagraph
		{
			get
			{
				return true;
			}
		}
		
		
		public string							ManagerName
		{
			get
			{
				return this.manager_name;
			}
		}
		
		public string[]							ManagerParameters
		{
			get
			{
				return this.manager_parameters.Clone () as string[];
			}
		}
		
		
		public static System.Collections.IComparer	Comparer
		{
			get
			{
				return new ManagedParagraphComparer ();
			}
		}
		
		
		public override Property EmptyClone()
		{
			return new ManagedParagraphProperty ();
		}
		
		public override void SerializeToText(System.Text.StringBuilder buffer)
		{
			SerializerSupport.Join (buffer,
				/**/				SerializerSupport.SerializeString (this.manager_name),
				/**/				SerializerSupport.SerializeStringArray (this.manager_parameters));
		}

		public override void DeserializeFromText(TextContext context, string text, int pos, int length)
		{
			string[] args = SerializerSupport.Split (text, pos, length);
			
			Debug.Assert.IsTrue (args.Length == 2);
			
			string   manager_name       = SerializerSupport.DeserializeString (args[0]);
			string[] manager_parameters = SerializerSupport.DeserializeStringArray (args[1]);
			
			this.manager_name       = manager_name;
			this.manager_parameters = manager_parameters;
		}
		
		
		public override Property GetCombination(Property property)
		{
			Debug.Assert.IsTrue (property is Properties.ManagedParagraphProperty);
			
			ManagedParagraphProperty a = this;
			ManagedParagraphProperty b = property as ManagedParagraphProperty;
			ManagedParagraphProperty c = new ManagedParagraphProperty ();
			
			c.manager_name       = b.manager_name;
			c.manager_parameters = b.manager_parameters;
			
			return c;
		}

		
		public override void UpdateContentsSignature(IO.IChecksum checksum)
		{
			checksum.UpdateValue (this.manager_name);
			checksum.UpdateValue (this.manager_parameters);
		}
		
		public override bool CompareEqualContents(object value)
		{
			return ManagedParagraphProperty.CompareEqualContents (this, value as ManagedParagraphProperty);
		}
		
		
		public static ManagedParagraphProperty Find(Property[] properties, string name)
		{
			foreach (Property property in properties)
			{
				if (property.WellKnownType == WellKnownType.ManagedParagraph)
				{
					ManagedParagraphProperty managed = property as ManagedParagraphProperty;
					
					if (managed.ManagerName == name)
					{
						return managed;
					}
				}
			}
			
			return null;
		}
		
		public static ManagedParagraphProperty[] Filter(System.Collections.ICollection properties)
		{
			int count = 0;
			
			foreach (Property property in properties)
			{
				if (property is ManagedParagraphProperty)
				{
					count++;
				}
			}
			
			ManagedParagraphProperty[] filtered = new ManagedParagraphProperty[count];
			
			int index = 0;
			
			foreach (Property property in properties)
			{
				if (property is ManagedParagraphProperty)
				{
					filtered[index++] = property as ManagedParagraphProperty;
				}
			}
			
			System.Diagnostics.Debug.Assert (index == count);
			
			return filtered;
		}
		
		
		private static bool CompareEqualContents(ManagedParagraphProperty a, ManagedParagraphProperty b)
		{
			return a.manager_name == b.manager_name
				&& Types.Comparer.Equal (a.manager_parameters, b.manager_parameters);
		}
		
		
		#region ManagedParagraphComparer Class
		private class ManagedParagraphComparer : System.Collections.IComparer
		{
			#region IComparer Members
			public int Compare(object x, object y)
			{
				Properties.ManagedParagraphProperty px = x as Properties.ManagedParagraphProperty;
				Properties.ManagedParagraphProperty py = y as Properties.ManagedParagraphProperty;
				
				int result = string.Compare (px.manager_name, py.manager_name);
				
				if (result == 0)
				{
					//	TODO: comparer les param�tres...
				}
				
				return result;
			}
			#endregion
		}
		#endregion
		
		private string							manager_name;
		private string[]						manager_parameters;
	}
}

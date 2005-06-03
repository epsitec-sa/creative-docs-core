//	Copyright � 2005, EPSITEC SA, CH-1092 BELMONT, Switzerland
//	Responsable: Pierre ARNAUD

namespace Epsitec.Common.Text.Properties
{
	/// <summary>
	/// Summary description for KeepProperty.
	/// </summary>
	public class KeepProperty : BaseProperty
	{
		public KeepProperty()
		{
		}
		
		public KeepProperty(int start_lines, int end_lines, ParagraphStartMode mode, ThreeState with_next_paragraph)
		{
			this.start_lines  = start_lines;
			this.end_lines    = end_lines;
			
			this.paragraph_start_mode = mode;
			this.with_next_paragraph  = with_next_paragraph;
		}
		
		
		public override WellKnownType			WellKnownType
		{
			get
			{
				return WellKnownType.Keep;
			}
		}
		
		public override PropertyType			PropertyType
		{
			get
			{
				return PropertyType.Style;
			}
		}
		
		
		public int								StartLines
		{
			get
			{
				return this.start_lines;
			}
			set
			{
				if (this.start_lines != value)
				{
					this.start_lines = value;
					this.Invalidate ();
				}
			}
		}
		
		public int								EndLines
		{
			get
			{
				return this.end_lines;
			}
			set
			{
				if (this.end_lines != value)
				{
					this.end_lines = value;
					this.Invalidate ();
				}
			}
		}
		
		public ParagraphStartMode				ParagraphStartMode
		{
			get
			{
				return this.paragraph_start_mode;
			}
			set
			{
				if (this.paragraph_start_mode != value)
				{
					this.paragraph_start_mode = value;
					this.Invalidate ();
				}
			}
		}
		
		public ThreeState						WithNextParagraph
		{
			get
			{
				return this.with_next_paragraph;
			}
			set
			{
				if (this.with_next_paragraph != value)
				{
					this.with_next_paragraph = value;
				}
			}
		}
		
		
		public override void SerializeToText(System.Text.StringBuilder buffer)
		{
			SerializerSupport.Join (buffer,
				/**/				SerializerSupport.SerializeInt (this.start_lines),
				/**/				SerializerSupport.SerializeInt (this.end_lines),
				/**/				SerializerSupport.SerializeEnum (this.paragraph_start_mode),
				/**/				SerializerSupport.SerializeThreeState (this.with_next_paragraph));
		}
		
		public override void DeserializeFromText(Context context, string text, int pos, int length)
		{
			string[] args = SerializerSupport.Split (text, pos, length);
			
			Debug.Assert.IsTrue (args.Length == 4);
			
			int start_lines = SerializerSupport.DeserializeInt (args[0]);
			int end_lines   = SerializerSupport.DeserializeInt (args[1]);
			
			ParagraphStartMode paragraph_start_mode = (ParagraphStartMode) SerializerSupport.DeserializeEnum (typeof (ParagraphStartMode), args[2]);
			ThreeState         with_next_paragraph  = SerializerSupport.DeserializeThreeState (args[3]);
			
			this.start_lines          = start_lines;
			this.end_lines            = end_lines;
			this.paragraph_start_mode = paragraph_start_mode;
			this.with_next_paragraph  = with_next_paragraph;
		}
		
		public override Properties.BaseProperty GetCombination(Properties.BaseProperty property)
		{
			Debug.Assert.IsTrue (property is Properties.KeepProperty);
			
			KeepProperty a = this;
			KeepProperty b = property as KeepProperty;
			KeepProperty c = new KeepProperty ();
			
			c.start_lines = b.start_lines == 0 ? a.start_lines : b.start_lines;
			c.end_lines   = b.end_lines == 0   ? a.end_lines   : b.end_lines;
			
			c.paragraph_start_mode = b.paragraph_start_mode == ParagraphStartMode.Undefined ? a.paragraph_start_mode : b.paragraph_start_mode;
			c.with_next_paragraph  = b.with_next_paragraph == ThreeState.Undefined ? a.with_next_paragraph : b.with_next_paragraph;
			
			return c;
		}
		
		public override void UpdateContentsSignature(IO.IChecksum checksum)
		{
			checksum.UpdateValue (this.start_lines);
			checksum.UpdateValue (this.end_lines);
			checksum.UpdateValue ((int) this.paragraph_start_mode);
			checksum.UpdateValue ((int) this.with_next_paragraph);
		}
		
		public override bool CompareEqualContents(object value)
		{
			return KeepProperty.CompareEqualContents (this, value as KeepProperty);
		}
		
		
		private static bool CompareEqualContents(KeepProperty a, KeepProperty b)
		{
			return a.start_lines == b.start_lines
				&& a.end_lines   == b.end_lines
				&& a.paragraph_start_mode == b.paragraph_start_mode
				&& a.with_next_paragraph  == b.with_next_paragraph;
		}
		
		
		private int								start_lines;
		private int								end_lines;
		
		private ParagraphStartMode				paragraph_start_mode;
		private ThreeState						with_next_paragraph;
	}
}

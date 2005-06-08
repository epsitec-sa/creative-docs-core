//	Copyright � 2005, EPSITEC SA, CH-1092 BELMONT, Switzerland
//	Responsable: Pierre ARNAUD

namespace Epsitec.Common.Text.Properties
{
	/// <summary>
	/// Summary description for BaseProperty.
	/// </summary>
	public abstract class BaseProperty : IContentsSignature, IContentsSignatureUpdater, IContentsComparer, ISerializableAsText
	{
		protected BaseProperty()
		{
		}
		
		
		public virtual long 					Version
		{
			get
			{
				return this.version;
			}
		}
		
		public abstract WellKnownType			WellKnownType
		{
			get;
		}
		
		public abstract PropertyType			PropertyType
		{
			get;
		}
		
		public virtual CombinationMode			CombinationMode
		{
			get
			{
				return CombinationMode.Combine;
			}
		}
		
		public virtual bool						RequiresSpecialCodeProcessing
		{
			get
			{
				return false;
			}
		}
		
		
		public void Invalidate()
		{
			this.contents_signature = 0;
			this.version            = 0;
		}
		
		public void UpdateVersion()
		{
			this.version = StyleVersion.Current;
		}
		
		
		public virtual int GetGlyphForSpecialCode(ulong code)
		{
			return -1;
		}
		
		public abstract Properties.BaseProperty GetCombination(Properties.BaseProperty property);
		
		#region IContentsSignatureUpdater Members
		public abstract void UpdateContentsSignature(IO.IChecksum checksum);
		#endregion
		
		#region IContentsComparer Members
		public abstract bool CompareEqualContents(object value);
		#endregion
		
		#region IContentsSignature Members
		public int GetContentsSignature()
		{
			if (this.contents_signature == 0)
			{
				IO.IChecksum checksum = IO.Checksum.CreateAdler32 ();
				
				this.UpdateContentsSignature (checksum);
				
				int signature = (int) checksum.Value;
				
				//	La signature calcul�e pourrait �tre nulle; dans ce cas, on
				//	l'ajuste pour �viter d'interpr�ter cela comme une absence
				//	de signature :
				
				this.contents_signature = (signature == 0) ? 1 : signature;
			}
			
			return this.contents_signature;
		}
		#endregion
		
		#region ISerializableAsText Members
		public abstract void SerializeToText(System.Text.StringBuilder buffer);
		public abstract void DeserializeFromText(Context context, string text, int pos, int length);
		#endregion
		
		public override string ToString()
		{
			System.Text.StringBuilder buffer = new System.Text.StringBuilder ();
			this.SerializeToText (buffer);
			return buffer.ToString ();
		}
		
		
		public static bool CompareEqualContents(Properties.BaseProperty[] a, Properties.BaseProperty[] b)
		{
			if (a == b)
			{
				return true;
			}
			if (a.properties.Length != b.properties.Length)
			{
				return false;
			}
			
			int n = a.properties.Length;
			
			for (int i = 0; i < n; i++)
			{
				Properties.BaseProperty pa = a[i];
				Properties.BaseProperty pb = b[i];
				
				if (pa.GetType () != pb.GetType ())
				{
					return false;
				}
				if (pa.GetContentsSignature () != pb.GetContentsSignature ())
				{
					return false;
				}
			}
			
			for (int i = 0; i < n; i++)
			{
				Properties.BaseProperty pa = a[i];
				Properties.BaseProperty pb = b[i];
				
				if (pa.CompareEqualContents (pb) == false)
				{
					return false;
				}
			}
			
			return true;
		}
		
		
		protected void DefineVersion(long version)
		{
			this.version = version;
		}
		
		
		private int								contents_signature;
		private long							version;
	}
}

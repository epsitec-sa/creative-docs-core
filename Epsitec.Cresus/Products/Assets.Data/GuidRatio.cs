//	Copyright © 2013, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Daniel ROUX, Maintainer: Daniel ROUX

using System.Collections.Generic;
using System.Linq;
using Epsitec.Cresus.Assets.Data.Helpers;
using Epsitec.Cresus.Assets.Data.Serialization;

namespace Epsitec.Cresus.Assets.Data
{
	public struct GuidRatio : System.IEquatable<GuidRatio>
	{
		public GuidRatio(Guid guid, decimal? ratio)
		{
			this.Guid  = guid;
			this.Ratio = ratio;
		}

		public GuidRatio(System.Xml.XmlReader reader)
		{
			this.Guid  = reader.ReadGuidAttribute    (X.Attr.Guid);
			this.Ratio = reader.ReadDecimalAttribute (X.Attr.Ratio);

			reader.Read ();
		}


		public bool IsEmpty
		{
			get
			{
				return this.Guid.IsEmpty
					&& !this.Ratio.HasValue;
			}
		}


		#region IEquatable<GuidRatio> Members
		public bool Equals(GuidRatio other)
		{
			return this.Guid  == other.Guid
				&& this.Ratio == other.Ratio;
		}
		#endregion

		public override bool Equals(object obj)
		{
			if (obj is GuidRatio)
			{
				return this.Equals ((GuidRatio) obj);
			}
			else
			{
				return false;
			}
		}

		public override int GetHashCode()
		{
			return this.Guid.GetHashCode ()
				 ^ this.Ratio.GetHashCode ();
		}

		public static bool operator ==(GuidRatio a, GuidRatio b)
		{
			return object.Equals (a, b);
		}

		public static bool operator !=(GuidRatio a, GuidRatio b)
		{
			return !(a == b);
		}


		public void Serialize(System.Xml.XmlWriter writer, string name)
		{
			writer.WriteStartElement (name);

			writer.WriteGuidAttribute    (X.Attr.Guid,  this.Guid);
			writer.WriteDecimalAttribute (X.Attr.Ratio, this.Ratio);

			writer.WriteEndElement ();
		}


		public static GuidRatio Empty = new GuidRatio (Guid.Empty, null);

		public readonly Guid					Guid;
		public readonly decimal?				Ratio;
	}
}

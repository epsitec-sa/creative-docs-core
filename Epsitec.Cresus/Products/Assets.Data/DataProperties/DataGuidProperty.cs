//	Copyright � 2013, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Daniel ROUX, Maintainer: Daniel ROUX

using System.Collections.Generic;
using System.Linq;

namespace Epsitec.Cresus.Assets.Data.DataProperties
{
	public class DataGuidProperty : AbstractDataProperty
	{
		public DataGuidProperty(ObjectField field, Guid value)
			: base (field)
		{
			this.Value = value;
		}

		public DataGuidProperty(DataGuidProperty model)
			: base (model)
		{
			this.Value = model.Value;
		}


		public override void Serialize(System.Xml.XmlWriter writer)
		{
			writer.WriteStartElement ("Property.Guid");
			writer.WriteElementString ("Value", this.Value.ToString ());
			writer.WriteEndElement ();
		}


		public readonly Guid Value;
	}
}

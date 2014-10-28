//	Copyright � 2013, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Daniel ROUX, Maintainer: Daniel ROUX

using System.Collections.Generic;
using System.Linq;

namespace Epsitec.Cresus.Assets.Data.DataProperties
{
	public class DataDecimalProperty : AbstractDataProperty
	{
		public DataDecimalProperty(ObjectField field, decimal value)
			: base (field)
		{
			this.Value = value;
		}

		public DataDecimalProperty(DataDecimalProperty model)
			: base (model)
		{
			this.Value = model.Value;
		}


		public override void Serialize(System.Xml.XmlWriter writer)
		{
			writer.WriteStartElement ("Property.Decimal");
			writer.WriteElementString ("Value", this.Value.ToString (System.Globalization.CultureInfo.InvariantCulture));
			writer.WriteEndElement ();
		}


		public readonly decimal Value;
	}
}

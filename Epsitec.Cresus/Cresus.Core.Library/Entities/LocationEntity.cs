//	Copyright � 2010-2012, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Pierre ARNAUD, Maintainer: Pierre ARNAUD

using Epsitec.Common.Types;
using Epsitec.Common.Support.EntityEngine;

using System.Collections.Generic;
using System.Linq;

namespace Epsitec.Cresus.Core.Entities
{
	public partial class LocationEntity
	{
		public override FormattedText GetSummary()
		{
			return TextFormatter.FormatText ("Ville:", this.PostalCode, this.Name, "\n", "Pays: ", this.Country.Name);
		}

		public override FormattedText GetCompactSummary()
		{
			return TextFormatter.FormatText (this.Country.CountryCode, "-", this.PostalCode, this.Name);
		}

		public override IEnumerable<FormattedText> GetFormattedEntityKeywords()
		{
			yield return TextFormatter.FormatText (this.Country.CountryCode);
			yield return TextFormatter.FormatText (this.PostalCode);
			yield return TextFormatter.FormatText (this.Name);
		}

		public override EntityStatus GetEntityStatus ()
		{
			//	We consider a location to be empty if it has neither postal code, nor
			//	location name; a location with just a country or region is still empty.
			using (var a = new EntityStatusAccumulator ())
			{
				a.Accumulate (this.PostalCode.GetEntityStatus ());
				a.Accumulate (this.Name.GetEntityStatus ());

				return a.EntityStatus;
			}
		}
	}
}

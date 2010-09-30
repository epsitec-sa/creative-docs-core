//	Copyright � 2010, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Pierre ARNAUD, Maintainer: Pierre ARNAUD

using Epsitec.Cresus.Core.Controllers.TabIds;
using Epsitec.Common.Types;

using System.Collections.Generic;
using System.Linq;

namespace Epsitec.Cresus.Core.Entities
{
	public partial class TotalDocumentItemEntity
	{
		public override FormattedText GetCompactSummary()
		{
			var desc = this.TextForPrimaryPrice;

			string total;
			if (this.PrimaryPriceBeforeTax.HasValue)
			{
				total = Misc.PriceToString (this.PrimaryPriceBeforeTax);
			}
			else if (this.FixedPriceAfterTax.HasValue)
			{
				total = Misc.PriceToString (this.FixedPriceAfterTax);
			}
			else
			{
				total = Misc.PriceToString (this.PrimaryPriceAfterTax);
			}

			var text = TextFormatter.FormatText (desc, total);

			if (text.IsNullOrEmpty)
			{
				return "<i>Total</i>";
			}
			else
			{
				return text;
			}
		}
	}
}

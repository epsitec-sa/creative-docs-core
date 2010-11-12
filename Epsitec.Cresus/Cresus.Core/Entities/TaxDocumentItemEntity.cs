//	Copyright � 2010, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Pierre ARNAUD, Maintainer: Pierre ARNAUD

using Epsitec.Common.Types;
using Epsitec.Common.Support.EntityEngine;

using Epsitec.Cresus.Core.Controllers.TabIds;
using Epsitec.Cresus.Core.Helpers;

using System.Collections.Generic;
using System.Linq;

namespace Epsitec.Cresus.Core.Entities
{
	public partial class TaxDocumentItemEntity
	{
		public override FormattedText GetCompactSummary()
		{
			var desc = this.Text;
			var tax = Misc.PriceToString (this.ResultingTax);

			var text = TextFormatter.FormatText (desc, tax);

			if (text.IsNullOrEmpty)
			{
				return "<i>TVA</i>";
			}
			else
			{
				return text;
			}
		}

		public override EntityStatus GetEntityStatus()
		{
			return EntityStatus.Valid;
		}
	}
}

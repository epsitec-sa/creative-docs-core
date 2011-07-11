//	Copyright � 2010-2011, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Pierre ARNAUD, Maintainer: Pierre ARNAUD

using Epsitec.Common.Types;
using Epsitec.Common.Support.EntityEngine;
using Epsitec.Common.Support.Extensions;

using Epsitec.Cresus.Core.Controllers.TabIds;
using Epsitec.Cresus.Core.Business.Finance.PriceCalculators;
using Epsitec.Cresus.Core.Business.Finance.PriceCalculators.ItemPriceCalculators;

using System.Collections.Generic;
using System.Linq;
using Epsitec.Cresus.Core.Business;

namespace Epsitec.Cresus.Core.Entities
{
	public partial class SubTotalDocumentItemEntity : ICloneable<SubTotalDocumentItemEntity>
	{
		public override DocumentItemTabId TabId
		{
			get
			{
				return DocumentItemTabId.Price;
			}
		}

		public override FormattedText GetCompactSummary()
		{
			var builder = new TextBuilder ();

			builder.Append ("Sous-total ");
			builder.Append (Misc.PriceToString (this.ResultingPriceBeforeTax));

			if (this.Discount.DiscountRate.HasValue)
			{
				builder.Append (" (apr�s rabais en %)");
			}
			else if (this.Discount.Value.HasValue)
			{
				builder.Append (" (apr�s rabais en francs)");
			}

			return builder.ToFormattedText ();
		}

		public override EntityStatus GetEntityStatus()
		{
			return EntityStatus.Valid;
		}

		public override void Process(IDocumentPriceCalculator priceCalculator)
		{
			priceCalculator.Process (new SubTotalItemPriceCalculator (priceCalculator, this));
		}
		
		#region ICloneable<SubTotalDocumentItemEntity> Members

		void ICloneable<SubTotalDocumentItemEntity>.CopyTo(IBusinessContext businessContext, SubTotalDocumentItemEntity copy)
		{
			copy.Visibility              = this.Visibility;
			copy.AutoGenerated           = this.AutoGenerated;
			copy.GroupIndex              = this.GroupIndex;

			copy.DisplayModes            = this.DisplayModes;
			copy.TextForPrimaryPrice     = this.TextForPrimaryPrice;
			copy.TextForResultingPrice   = this.TextForResultingPrice;
			copy.TextForDiscount         = this.TextForDiscount;
			copy.PrimaryPriceBeforeTax   = this.PrimaryPriceBeforeTax;
			copy.PrimaryTax              = this.PrimaryTax;

			if (this.Discount.IsNotNull ())
			{
				copy.Discount = this.Discount.CloneEntity (businessContext);
			}

			copy.ResultingTax            = this.ResultingTax;
			copy.ResultingPriceBeforeTax = this.ResultingPriceBeforeTax;
			copy.FinalPriceBeforeTax     = this.FinalPriceBeforeTax;
		}

		#endregion
	}
}

//	Copyright � 2010-2011, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Pierre ARNAUD, Maintainer: Pierre ARNAUD

using Epsitec.Common.Types;
using Epsitec.Common.Support.EntityEngine;
using Epsitec.Common.Support.Extensions;

using Epsitec.Cresus.Core.Business;
using Epsitec.Cresus.Core.Business.Finance;
using Epsitec.Cresus.Core.Business.Finance.PriceCalculators;
using Epsitec.Cresus.Core.Business.Finance.PriceCalculators.ItemPriceCalculators;
using Epsitec.Cresus.Core.Controllers.TabIds;

using System.Collections.Generic;
using System.Linq;

namespace Epsitec.Cresus.Core.Entities
{
	public partial class ArticleDocumentItemEntity : ICopyableEntity<ArticleDocumentItemEntity>
	{
		public decimal GetOrderedQuantity()
		{
			var quantities = this.ArticleQuantities.Where (x => x.QuantityColumn.QuantityType == ArticleQuantityType.Ordered);

			return quantities.Sum (x => this.ArticleDefinition.ConvertToBillingUnit (x.Quantity, x.Unit));
		}


		public override FormattedText GetCompactSummary()
		{
			if (this.GetEntityStatus () == EntityStatus.Empty)
			{
				return null;
			}

			var quantity = Helpers.ArticleDocumentItemHelper.GetArticleQuantityAndUnit (this);
			var desc     = this.ArticleDescriptionCache.Lines.FirstOrDefault ().ToSimpleText ();
			var price    = Misc.PriceToString (this.PrimaryLinePriceBeforeTax);

			FormattedText text = TextFormatter.FormatText (quantity, desc, price);

			if (text.IsNullOrEmpty)
			{
				return "<i>Article</i>";
			}
			else
			{
				return text;
			}
		}

		public override EntityStatus GetEntityStatus ()
		{
			using (var a = new EntityStatusAccumulator ())
			{
				a.Accumulate (this.ArticleDefinition.GetEntityStatus ());
				a.Accumulate (this.ArticleParameters.GetEntityStatus ().TreatAsOptional ());
				a.Accumulate (this.ArticleQuantities.Select (x => x.GetEntityStatus ()));

				return a.EntityStatus;
			}
		}

		public override void Process(IDocumentPriceCalculator priceCalculator)
		{
			priceCalculator.Process (new ArticleItemPriceCalculator (priceCalculator, this));
		}

		#region ICloneable<ArticleDocumentItemEntity> Members

		void ICopyableEntity<ArticleDocumentItemEntity>.CopyTo(IBusinessContext businessContext, ArticleDocumentItemEntity copy)
		{
			copy.Attributes                      = this.Attributes;
			copy.GroupIndex                      = this.GroupIndex;
			
			copy.BeginDate                       = this.BeginDate;
			copy.EndDate                         = this.EndDate;
			copy.ArticleDefinition               = this.ArticleDefinition;
			copy.ArticleParameters               = this.ArticleParameters;

			//	TODO: clone ArticleTraceabilityDetails

			copy.VatCode                         = this.VatCode;
			copy.BillingUnitPriceBeforeTax       = this.BillingUnitPriceBeforeTax;
			copy.PrimaryUnitPriceBeforeTax       = this.PrimaryUnitPriceBeforeTax;
			copy.PrimaryLinePriceBeforeTax       = this.PrimaryLinePriceBeforeTax;
			copy.PrimaryLinePriceAfterTax        = this.PrimaryLinePriceAfterTax;
			copy.NeverApplyDiscount              = this.NeverApplyDiscount;

			copy.ArticleQuantities.AddRange (this.ArticleQuantities.Select (x => x.CloneEntity (businessContext)));
			copy.Discounts.AddRange (this.Discounts.Select (x => x.CloneEntity (businessContext)));

			copy.TaxRate1                        = this.TaxRate1;
			copy.TaxRate2                        = this.TaxRate2;
			copy.FixedLinePrice                  = this.FixedLinePrice;
			copy.FixedLinePriceIncludesTaxes     = this.FixedLinePriceIncludesTaxes;
			copy.ResultingLinePriceBeforeTax     = this.ResultingLinePriceBeforeTax;
			copy.ResultingLineTax1               = this.ResultingLineTax1;
			copy.ResultingLineTax2               = this.ResultingLineTax2;
			copy.FinalLinePriceBeforeTax         = this.FinalLinePriceBeforeTax;
			copy.ArticleNameCache                = this.ArticleNameCache;
			copy.ArticleDescriptionCache         = this.ArticleDescriptionCache;
			copy.ReplacementName                 = this.ReplacementName;
			copy.ReplacementDescription          = this.ReplacementDescription;
		}

		#endregion
	}
}

﻿//	Copyright © 2010-2011, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Pierre ARNAUD, Maintainer: Pierre ARNAUD

using Epsitec.Common.Support.EntityEngine;
using Epsitec.Common.Support.Extensions;
using Epsitec.Common.Types;
using Epsitec.Common.Types.Collections;

using Epsitec.Cresus.Core.Business.Finance;
using Epsitec.Cresus.Core.Entities;
using Epsitec.Cresus.Core.Extensions;
using Epsitec.Cresus.Core.Helpers;

using System.Collections.Generic;
using System.Linq;

namespace Epsitec.Cresus.Core.Business.Finance.PriceCalculators.ItemPriceCalculators
{
	/// <summary>
	/// The <c>ArticleItemPriceCalculator</c> class computes the price of an article line.
	/// </summary>
	public class ArticleItemPriceCalculator : AbstractItemPriceCalculator
	{
		public ArticleItemPriceCalculator(IDocumentPriceCalculator priceCalculator, ArticleDocumentItemEntity articleItem)
			: this (priceCalculator.Data, priceCalculator.BusinessContext, priceCalculator.Document, priceCalculator.Metadata, articleItem)
		{
		}

		private ArticleItemPriceCalculator(CoreData data, BusinessContext businessContext, BusinessDocumentEntity document, DocumentMetadataEntity metadata, ArticleDocumentItemEntity articleItem)
		{
			if (articleItem.ArticleAttributes.HasFlag (ArticleDocumentItemAttributes.Dirty))
			{
				ArticleItemPriceCalculator.UpdateArticleItemAttributes (articleItem, document.PriceGroup);
			}
			
			this.data            = data;
			this.businessContext = businessContext;
			this.document        = document;
			this.metadata        = metadata;
			this.articleItem     = articleItem;
			this.articleDef      = this.articleItem.ArticleDefinition.UnwrapNullEntity ();
			this.currencyCode    = this.document.CurrencyCode;
			this.priceGroup      = this.document.PriceGroup.UnwrapNullEntity ();
			this.date            = this.document.PriceRefDate.ToDateTime ();
			
			this.articleAttributes      = this.articleItem.ArticleAttributes;
			this.articleNotDiscountable = this.articleAttributes.HasFlag (ArticleDocumentItemAttributes.ArticleNotDiscountable) ||
										  this.articleAttributes.HasFlag (ArticleDocumentItemAttributes.NeverApplyDiscount);
		}


		public ArticleDocumentItemEntity		ArticleItem
		{
			get
			{
				return this.articleItem;
			}
		}

		public Tax								Tax
		{
			get
			{
				return this.tax;
			}
		}

		public bool								NotDiscountable
		{
			get
			{
				return this.articleNotDiscountable;
			}
		}

		public bool								ProFormaOnly
		{
			get
			{
				return this.articleItem.Attributes.HasFlag (DocumentItemAttributes.ProFormaOnly);
			}
		}

		public void ComputePrice()
		{
			this.UpdateArticleItemAccountingDefinition ();
			this.UpdateArticleItemUnitPrice ();

			Tax tax = null;

			var unitPriceQuantity = this.GetUnitPriceQuantity ();
			var realPriceQuantity = this.GetRealPriceQuantity ();

			if (this.articleItem.UnitPriceBeforeTax1.HasValue)
			{
				tax = this.ComputePriceTaxExclusive (unitPriceQuantity);
			}
			else if (this.articleItem.UnitPriceAfterTax1.HasValue)
			{
				tax = this.ComputePriceTaxInclusive (unitPriceQuantity);
			}

			this.ComputeTotalRevenueAndTax (tax, unitPriceQuantity, realPriceQuantity);

			this.articleItem.ArticleAttributes = this.articleAttributes;
		}


		private void UpdateArticleItemUnitPrice()
		{
			if ((this.articleItem.UnitPriceAfterTax1 == null) &&
				(this.articleItem.UnitPriceBeforeTax1 == null))
			{
				this.ResetUnitPrice ();
			}
		}

		private void UpdateArticleItemAccountingDefinition()
		{
			if (this.articleItem.ArticleAccountingDefinition.IsNull ())
			{
				if (this.articleDef != null)
				{
					this.articleItem.ArticleAccountingDefinition = this.GetArticleAccountingDefinition ().FirstOrDefault ();
				}
			}
		}
		
		private Tax ComputePriceTaxInclusive(decimal unitPriceQuantity)
		{
			Tax tax;
			var value = this.articleItem.UnitPriceAfterTax1.Value;

			value = this.ApplyDiscount (value, DiscountPolicy.OnUnitPrice);
			value = this.ApplyRounding (value, RoundingPolicy.OnUnitPrice);

			if ((this.articleAttributes.HasFlag (ArticleDocumentItemAttributes.FixedUnitPrice2)) &&
				(this.articleItem.UnitPriceAfterTax2.HasValue))
			{
				value = this.articleItem.UnitPriceAfterTax2.Value;
			}
			else
			{
				this.articleItem.UnitPriceAfterTax2 = value;
				this.articleAttributes = this.articleAttributes.ClearFlag (ArticleDocumentItemAttributes.FixedUnitPrice2);
			}

			value = value * unitPriceQuantity;

			if ((this.articleAttributes.HasFlag (ArticleDocumentItemAttributes.FixedLinePrice1)) &&
				(this.articleItem.LinePriceAfterTax1.HasValue))
			{
				value = this.articleItem.LinePriceAfterTax1.Value;
			}
			else
			{
				this.articleItem.LinePriceAfterTax1 = PriceCalculator.ClipPriceValue (value, this.currencyCode);
				this.articleAttributes = this.articleAttributes.ClearFlag (ArticleDocumentItemAttributes.FixedLinePrice1);
			}

			value = this.ApplyDiscount (value, DiscountPolicy.OnLinePrice);
			value = this.ApplyRounding (value, RoundingPolicy.OnLinePrice);

			if ((this.articleAttributes.HasFlag (ArticleDocumentItemAttributes.FixedLinePrice2)) &&
				(this.articleItem.LinePriceAfterTax2.HasValue))
			{
				value = this.articleItem.LinePriceAfterTax2.Value;
			}
			else
			{
				this.articleItem.LinePriceAfterTax2 = value;
				this.articleAttributes = this.articleAttributes.ClearFlag (ArticleDocumentItemAttributes.FixedLinePrice2);
			}

			tax = this.ComputeTax (1000);

			if (tax != null)
			{
				this.articleItem.VatRateA = PriceCalculator.ClipTaxRateValue (tax.GetTaxRate (0).GetValueOrDefault ());
				this.articleItem.VatRateB = PriceCalculator.ClipTaxRateValue (tax.GetTaxRate (1).GetValueOrDefault ());
				this.articleItem.VatRatio = ArticleItemPriceCalculator.GetVatRatio (tax);
			}
			
			return tax;
		}
		
		private Tax ComputePriceTaxExclusive(decimal unitPriceQuantity)
		{
			Tax tax;
			var value = this.articleItem.UnitPriceBeforeTax1.Value;

			value = this.ApplyDiscount (value, DiscountPolicy.OnUnitPrice);
			value = this.ApplyRounding (value, RoundingPolicy.OnUnitPrice);

			if ((this.articleAttributes.HasFlag (ArticleDocumentItemAttributes.FixedUnitPrice2)) &&
				(this.articleItem.UnitPriceBeforeTax2.HasValue))
			{
				value = this.articleItem.UnitPriceBeforeTax2.Value;
			}
			else
			{
				this.articleItem.UnitPriceBeforeTax2 = value;
				this.articleAttributes = this.articleAttributes.ClearFlag (ArticleDocumentItemAttributes.FixedUnitPrice2);
			}

			value = value * unitPriceQuantity;

			if ((this.articleAttributes.HasFlag (ArticleDocumentItemAttributes.FixedLinePrice1)) &&
				(this.articleItem.LinePriceBeforeTax1.HasValue))
			{
				value = this.articleItem.LinePriceBeforeTax1.Value;
			}
			else
			{
				this.articleItem.LinePriceBeforeTax1 = PriceCalculator.ClipPriceValue (value, this.currencyCode);
				this.articleAttributes = this.articleAttributes.ClearFlag (ArticleDocumentItemAttributes.FixedLinePrice1);
			}

			value = this.ApplyDiscount (value, DiscountPolicy.OnLinePrice);
			value = this.ApplyRounding (value, RoundingPolicy.OnLinePrice);

			if ((this.articleAttributes.HasFlag (ArticleDocumentItemAttributes.FixedLinePrice2)) &&
				(this.articleItem.LinePriceBeforeTax2.HasValue))
			{
				value = this.articleItem.LinePriceBeforeTax2.Value;
			}
			else
			{
				this.articleItem.LinePriceBeforeTax2 = value;
				this.articleAttributes = this.articleAttributes.ClearFlag (ArticleDocumentItemAttributes.FixedLinePrice2);
			}

			tax = this.ComputeTax (value);

			if (tax != null)
			{
				this.articleItem.VatRateA = PriceCalculator.ClipTaxRateValue (tax.GetTaxRate (0).GetValueOrDefault ());
				this.articleItem.VatRateB = PriceCalculator.ClipTaxRateValue (tax.GetTaxRate (1).GetValueOrDefault ());
				this.articleItem.VatRatio = ArticleItemPriceCalculator.GetVatRatio (tax);

				value = value + tax.TotalTax;
			}

			//	The price after tax is stored, but should not be made available to the user. It
			//	is only required to internally compute the total revenue.

			this.articleItem.LinePriceAfterTax1 = PriceCalculator.ClipPriceValue (value, this.currencyCode);
			this.articleItem.LinePriceAfterTax2 = this.articleItem.LinePriceAfterTax1;
			
			return tax;
		}

		private void ComputeTotalRevenueAndTax(Tax tax, decimal unitPriceQuantity, decimal realPriceQuantity)
		{
			if (tax == null)
			{
				this.articleItem.TotalRevenueAfterTax          = null;
				this.articleItem.TotalRevenueAccounted = null;

				return;
			}

			decimal? revenueAfterTax  = this.articleItem.LinePriceAfterTax2;
			decimal? revenueBeforeTax = this.articleItem.LinePriceBeforeTax2;

			if ((unitPriceQuantity != 0) &&
				(realPriceQuantity != unitPriceQuantity))
			{
				if (revenueAfterTax.GetValueOrDefault () != 0)
				{
					revenueAfterTax = revenueAfterTax.Value * realPriceQuantity / unitPriceQuantity;
				}
				
				if (revenueBeforeTax.GetValueOrDefault () != 0)
				{
					revenueBeforeTax = revenueBeforeTax.Value * realPriceQuantity / unitPriceQuantity;
				}
			}

			this.articleItem.TotalRevenueAfterTax = PriceCalculator.ClipPriceValue (revenueAfterTax, this.currencyCode);

			if (revenueBeforeTax == null)
			{
				revenueBeforeTax = (tax == null) ? 0 : tax.ComputeExactAmountBeforeTax (this.articleItem.TotalRevenueAfterTax.Value);
			}

			//	By using an unrounded revenue before tax in the tax calculation, we hope that we
			//	will indeed get a tax which, when added to the price before tax, will yield the
			//	same price after tax as the total revenue.
			
			this.tax = this.ComputeTax (revenueBeforeTax.Value);
		}
		
		private static decimal GetVatRatio(Tax tax)
		{
			var a = tax.GetTaxRateAmount (0).Amount;
			var b = tax.GetTaxRateAmount (1).Amount;

			if ((a + b) == 0)
			{
				return 1;
			}
			else
			{
				return a / (a + b);
			}
		}
		
		private decimal ApplyDiscount(decimal value, DiscountPolicy policy)
		{
			foreach (var discount in this.articleItem.Discounts.Where (x => x.DiscountPolicy.Compatible (policy)))
			{
				value = this.ApplyDiscount (value, discount);
				value = this.ApplyRounding (value, discount.RoundingMode);
			}

			return value;
		}

		private decimal ApplyRounding(decimal value, RoundingPolicy policy)
		{
			return PriceCalculator.Round (value, policy, this.currencyCode);
		}

		private decimal ApplyRounding(decimal value, PriceRoundingModeEntity rounding)
		{
			if (rounding.IsNull ())
			{
				return value;
			}
			else
			{
				return rounding.Round (value);
			}
		}


		private void ResetUnitPrice()
		{
			this.ClearPrices ();

			if ((this.articleDef != null) &&
				(this.priceGroup != null))
			{
				var billingMode = this.priceGroup.BillingMode;

				switch (billingMode)
				{
					case BillingMode.IncludingTax:
						this.articleItem.UnitPriceAfterTax1 = this.ComputeUnitPrice ();
						break;

					case BillingMode.ExcludingTax:
						this.articleItem.UnitPriceBeforeTax1 = this.ComputeUnitPrice ();
						break;

					case BillingMode.None:
						break;

					default:
						throw new System.NotSupportedException (string.Format ("{0} not supported", billingMode.GetQualifiedName ()));
				}
			}
		}

		private void ClearPrices()
		{
			this.articleItem.UnitPriceBeforeTax1   = null;
			this.articleItem.UnitPriceBeforeTax2   = null;
			this.articleItem.LinePriceBeforeTax1   = null;
			this.articleItem.LinePriceBeforeTax2   = null;
												   
			this.articleItem.UnitPriceAfterTax1    = null;
			this.articleItem.UnitPriceAfterTax2    = null;
			this.articleItem.LinePriceAfterTax1    = null;
			this.articleItem.LinePriceAfterTax2    = null;

			this.articleItem.TotalRevenueAfterTax          = null;
			this.articleItem.TotalRevenueAccounted = null;
		}
		
		private decimal? ComputeUnitPrice()
		{
			var unitPriceQuantity = this.GetUnitPriceQuantity ();
			var realPriceQuantity = this.GetRealPriceQuantity ();
			var articlePrice      = this.GetArticlePrice (unitPriceQuantity, realPriceQuantity);

			return this.ComputeUnitPrice (articlePrice);
		}

		
		private static void UpdateArticleItemAttributes(ArticleDocumentItemEntity articleItem, PriceGroupEntity priceGroup)
		{
			var articleDef = articleItem.ArticleDefinition;
			var attributes = articleItem.ArticleAttributes;

			System.Diagnostics.Debug.Assert (attributes.HasFlag (ArticleDocumentItemAttributes.Dirty));

			if (attributes.HasFlag (ArticleDocumentItemAttributes.Reset))
			{
				articleItem.VatRateA              = 0;
				articleItem.VatRateB              = 0;
				articleItem.VatRatio              = 1;
				articleItem.UnitPriceBeforeTax1   = null;
				articleItem.UnitPriceBeforeTax2   = null;
				articleItem.UnitPriceAfterTax1    = null;
				articleItem.UnitPriceAfterTax2    = null;
				articleItem.LinePriceBeforeTax1   = null;
				articleItem.LinePriceBeforeTax2   = null;
				articleItem.LinePriceAfterTax1    = null;
				articleItem.LinePriceAfterTax2    = null;
				articleItem.TotalRevenueAfterTax  = null;
				articleItem.TotalRevenueAccounted = null;

				attributes = attributes.ClearFlag (ArticleDocumentItemAttributes.Reset);
			}
			
			bool articleNotDiscountable = false;

			if ((priceGroup.IsNotNull ()) &&
				(priceGroup.NeverApplyDiscount))
			{
				articleNotDiscountable = true;
			}
			else
			{
				articleNotDiscountable = articleDef.IsNotNull () && articleDef.ArticleCategory.IsNotNull () && articleDef.ArticleCategory.NeverApplyDiscount;
			}

			if (articleNotDiscountable)
			{
				attributes = attributes.SetFlag (ArticleDocumentItemAttributes.ArticleNotDiscountable);
			}
			else
			{
				attributes = attributes.ClearFlag (ArticleDocumentItemAttributes.ArticleNotDiscountable);
			}

			attributes.ClearFlag (ArticleDocumentItemAttributes.Dirty);
			
			articleItem.ArticleAttributes = attributes;
		}

		public override void ApplyFinalPriceAdjustment(decimal adjustment)
		{
#if false
			if (this.notDiscountable)
			{
				this.articleItem.FinalLinePriceBeforeTax = this.articleItem.ResultingLinePriceBeforeTax;
			}
			else
			{
				this.articleItem.FinalLinePriceBeforeTax = PriceCalculator.ClipPriceValue (this.articleItem.ResultingLinePriceBeforeTax * adjustment, this.currencyCode);
			}
#endif
		}

		private decimal ApplyDiscount(decimal price, PriceDiscountEntity discount)
		{
			if (discount.IsNull ())
			{
				return price;
			}

			if (discount.DiscountRate.HasValue)
			{
				decimal discountRatio = 1.00M - System.Math.Abs (discount.DiscountRate.Value);
				return price * discountRatio;
			}
			
			if (discount.Value.HasValue)
			{
				decimal discountAmount = System.Math.Abs (discount.Value.Value);
				return System.Math.Max (0, price - discountAmount);
			}
			
			return price;
		}

		private decimal GetUnitPriceQuantity()
		{
			return this.articleItem.GetQuantity (ArticleQuantityType.Ordered);
		}

		private decimal GetRealPriceQuantity()
		{
			switch (this.metadata.DocumentCategory.DocumentType)
			{
				case DocumentType.Invoice:
				case DocumentType.InvoiceProForma:
				case DocumentType.Receipt:
				case DocumentType.CreditMemo:
					return this.articleItem.GetQuantity (ArticleQuantityType.Billed);

				default:
					return this.articleItem.GetQuantity (ArticleQuantityType.Ordered);
			}
		}

		private decimal? ComputeUnitPrice(ArticlePriceEntity articlePrice)
		{
			if (articlePrice == null)
			{
				return null;
			}

			decimal totalPrice = articlePrice.Value;

			var parameterCodesToValues = ArticleParameterHelper.GetArticleParametersValues (this.articleItem);

			foreach (PriceCalculatorEntity priceCalculator in articlePrice.PriceCalculators)
			{
				decimal? price = priceCalculator.ExecutePriceCalculator (parameterCodesToValues);

				//	TODO: If the price is null, then the price was not defined for the calculator. Should we do something about that?
				
				if (price.HasValue)
				{
					totalPrice += price.Value;
				}
			}

			return totalPrice;
		}

		private Tax ComputeTax(decimal articleValue)
		{
			if (this.articleDef.ArticleCategory.IsNull ())
			{
				return null;
			}
			
			TaxCalculator taxCalculator;
			
			if ((this.articleItem.BeginDate.HasValue) &&
				(this.articleItem.EndDate.HasValue))
			{
				taxCalculator = new TaxCalculator (this.data, this.articleItem);
			}
			else
			{
				taxCalculator = new TaxCalculator (this.data, new Date (this.date));
			}
			
			var tax = taxCalculator.ComputeTax (articleValue, this.articleDef.ArticleCategory.VatRateType);
			
			if (tax.RateAmounts.Count > 2)
			{
				throw new System.InvalidOperationException ("Cannot process more than 2 different tax rates");
			}

			return tax;
		}

		private ArticlePriceEntity GetArticlePrice(decimal quantity, decimal fallbackQuantity)
		{
			return this.GetArticlePrices (quantity, fallbackQuantity).FirstOrDefault ().UnwrapNullEntity ();
		}

		private IEnumerable<ArticlePriceEntity> GetArticlePrices(decimal quantity, decimal fallbackQuantity)
		{
			if (quantity == 0)
			{
				quantity = fallbackQuantity == 0 ? 1 : fallbackQuantity;
			}

			return this.articleDef.GetArticlePrices (quantity, this.date, this.currencyCode, this.priceGroup);
		}

		public IEnumerable<ArticleAccountingDefinitionEntity> GetArticleAccountingDefinition()
		{
			if (this.articleDef.ArticleCategory.IsNull ())
			{
				return Enumerable.Empty<ArticleAccountingDefinitionEntity> ();
			}
			
			var accounting = (from def in this.articleDef.ArticleCategory.Accounting
							  where def.CurrencyCode == this.currencyCode
							  where this.date.InRange (def)
							  select def).ToArray ();

			return accounting;
		}

		private readonly CoreData					data;
		private readonly BusinessContext			businessContext;
		private readonly DocumentMetadataEntity		metadata;
		private readonly BusinessDocumentEntity		document;
		private readonly ArticleDocumentItemEntity	articleItem;
		private readonly ArticleDefinitionEntity	articleDef;
		private readonly CurrencyCode				currencyCode;
		private readonly System.DateTime			date;
		private readonly PriceGroupEntity			priceGroup;

		private Tax									tax;
		private readonly bool						articleNotDiscountable;
		private ArticleDocumentItemAttributes		articleAttributes;
	}
}

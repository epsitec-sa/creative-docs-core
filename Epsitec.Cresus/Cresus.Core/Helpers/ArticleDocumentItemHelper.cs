﻿//	Copyright © 2010, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Daniel ROUX, Maintainer: Daniel ROUX

using Epsitec.Common.Support;
using Epsitec.Common.Support.EntityEngine;
using Epsitec.Common.Types;
using Epsitec.Common.Drawing;
using Epsitec.Common.Widgets;

using Epsitec.Cresus.Core.Entities;

using System.Collections.Generic;
using System.Linq;

namespace Epsitec.Cresus.Core.Helpers
{
	public static class ArticleDocumentItemHelper
	{
		public static decimal? GetArticlePrice(ArticleDocumentItemEntity article, System.DateTime date, BusinessLogic.Finance.CurrencyCode currency)
		{
			//	Il peut y avoir plusieurs prix, mais un seul prix à une date donnée pour une monnaie donnée.
			foreach (var price in article.ArticleDefinition.ArticlePrices)
			{
				if (price.BeginDate.HasValue && price.EndDate.HasValue)
				{
					// TODO: Vérifier si on impose les heures 00:00:00 et 23:59:59 ici ou en amont.
					System.DateTime beginDate = new System.DateTime (price.BeginDate.Value.Year, price.BeginDate.Value.Month, price.BeginDate.Value.Day,  0,  0,  0);
					System.DateTime endDate   = new System.DateTime (price.EndDate.Value.Year,   price.EndDate.Value.Month,   price.EndDate.Value.Day,   23, 59, 59);

					if (date >= beginDate && date <= endDate && currency == price.CurrencyCode)
					{
						return price.ValueBeforeTax;
					}
				}
			}

			return null;
		}

		public static string GetArticleQuantityAndUnit(ArticleDocumentItemEntity article)
		{
			foreach (var quantity in article.ArticleQuantities)
			{
				if (quantity.Code == "livré")
				{
					return Misc.FormatUnit (quantity.Quantity, quantity.Unit.Code);
				}
			}

			return null;
		}

		public static decimal? GetArticleQuantity(ArticleDocumentItemEntity article)
		{
			foreach (var quantity in article.ArticleQuantities)
			{
				if (quantity.Code == "livré")
				{
					return quantity.Quantity;
				}
			}

			return null;
		}

		public static string GetArticleId(ArticleDocumentItemEntity article)
		{
			var x = article.ArticleDefinition;

			return UIBuilder.FormatText (x.IdA, "/~", x.IdB, "/~", x.IdC).ToSimpleText ();
		}

		public static string GetArticleDescription(ArticleDocumentItemEntity article)
		{
			if (!string.IsNullOrEmpty (article.ReplacementText))
			{
				return article.ReplacementText;
			}

			if (!string.IsNullOrEmpty (article.ArticleDefinition.LongDescription))
			{
				return article.ArticleDefinition.LongDescription;
			}

			if (!string.IsNullOrEmpty (article.ArticleDefinition.ShortDescription))
			{
				return article.ArticleDefinition.ShortDescription;
			}

			return null;
		}


		public static void UpdatePrices(ArticleDocumentItemEntity article)
		{
			//	Recalcule une ligne d'une facture.
			decimal vatRate = 0.076M;  // TODO: Cette valeur ne devrait pas tomber du ciel !
			var quantity = ArticleDocumentItemHelper.GetArticleQuantity (article);

			if (quantity.HasValue)
			{
				decimal? total = article.PrimaryUnitPriceBeforeTax * quantity.Value;

				if (article.Discounts.Count != 0)  // y a-t-il un rabais de ligne ?
				{
					if (article.Discounts[0].DiscountRate.HasValue)  // rabais en % ?
					{
						total *= 1.0M - article.Discounts[0].DiscountRate.Value;
					}

					if (article.Discounts[0].DiscountAmount.HasValue)  // rabais en francs ?
					{
						total -= article.Discounts[0].DiscountAmount.Value;
					}
				}

				article.FinalLineTax     = total * vatRate;
				article.ResultingLineTax = total * vatRate;

				article.PrimaryLinePriceBeforeTax   = total.Value;
				article.FixedLinePriceBeforeTax     = total;
				article.FinalLinePriceBeforeTax     = total;
				article.ResultingLinePriceBeforeTax = total;
			}
			else
			{
				article.FinalLineTax     = null;
				article.ResultingLineTax = null;

				article.PrimaryLinePriceBeforeTax   = 0;
				article.FixedLinePriceBeforeTax     = null;
				article.FinalLinePriceBeforeTax     = null;
				article.ResultingLinePriceBeforeTax = null;
			}
		}
	}
}

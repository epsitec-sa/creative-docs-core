﻿//	Copyright © 2010, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Daniel ROUX, Maintainer: Daniel ROUX

using Epsitec.Common.Types;
using Epsitec.Common.Types.Converters;
using Epsitec.Common.Widgets;

using Epsitec.Cresus.Core.Entities;
using Epsitec.Cresus.Core.Controllers;
using Epsitec.Cresus.Core.Controllers.DataAccessors;
using Epsitec.Cresus.Core.Widgets;
using Epsitec.Cresus.Core.Widgets.Tiles;
using Epsitec.Cresus.Core.Helpers;

using System.Collections.Generic;
using System.Linq;

namespace Epsitec.Cresus.Core.Controllers.EditionControllers
{
	public class EditionInvoiceDocumentViewController : EditionViewController<Entities.InvoiceDocumentEntity>
	{
		public EditionInvoiceDocumentViewController(string name, Entities.InvoiceDocumentEntity entity)
			: base (name, entity)
		{
		}


		protected override EditionStatus GetEditionStatus()
		{
			if (string.IsNullOrEmpty (this.Entity.IdA) &&
				string.IsNullOrEmpty (this.Entity.IdB) &&
				string.IsNullOrEmpty (this.Entity.IdC))
			{
				return EditionStatus.Empty;
			}

			if (string.IsNullOrEmpty (this.Entity.IdA) &&
				(!string.IsNullOrEmpty (this.Entity.IdB) ||
				 !string.IsNullOrEmpty (this.Entity.IdC)))
			{
				return EditionStatus.Invalid;
			}

			return EditionStatus.Valid;
		}

		protected override void UpdateEmptyEntityStatus(DataLayer.Context.DataContext context, bool isEmpty)
		{
			var entity = this.Entity;
			context.UpdateEmptyEntityStatus (entity, isEmpty);
		}

	
		protected override void CreateUI(TileContainer container)
		{
			this.tileContainer = container;

			using (var builder = new UIBuilder (container, this))
			{
				builder.CreateHeaderEditorTile ();
				builder.CreateEditionTitleTile ("Data.InvoiceDocument", "Facture");

				this.CreateUIMain (builder);

				builder.CreateFooterEditorTile ();
			}

			//	Summary:
			this.TileContainerController = new TileContainerController (this, container);
			var data = this.TileContainerController.DataItems;

			this.CreateUILines (data);
			this.CreateUIBillings (data);
			this.CreateUIComments (data);

			this.TileContainerController.GenerateTiles ();
		}


		private void CreateUIMain(Epsitec.Cresus.Core.UIBuilder builder)
		{
			var tile = builder.CreateEditionTile ();

			builder.CreateTextField      (tile, 150, "Numéro de la facture", Marshaler.Create (() => this.Entity.IdA, x => this.Entity.IdA = x));
			builder.CreateTextField      (tile, 150, "Numéro externe",       Marshaler.Create (() => this.Entity.IdB, x => this.Entity.IdB = x));
			builder.CreateTextField      (tile, 150, "Numéro interne",       Marshaler.Create (() => this.Entity.IdC, x => this.Entity.IdC = x));
			builder.CreateMargin         (tile, horizontalSeparator: true);
			builder.CreateTextField      (tile,   0, "Description",          Marshaler.Create (() => this.Entity.Description, x => this.Entity.Description = x));

#if true
			// TODO: à supprimer un jour...
			var button = builder.CreateButton (tile, 0, "Action", "Recalculer la facture (provisoire)");

			button.Clicked += delegate
			{
				InvoiceDocumentHelper.UpdatePrices (this.Entity);
				this.tileContainer.UpdateAllWidgets ();
			};
#endif
		}

		private void CreateUILines(SummaryDataItems data)
		{
			data.Add (
				new SummaryData
				{
					AutoGroup    = true,
					Name		 = "DocumentItem",
					IconUri		 = "Data.DocumentItems",
					Title		 = UIBuilder.FormatText ("Lignes"),
					CompactTitle = UIBuilder.FormatText ("Lignes"),
					Text		 = CollectionTemplate.DefaultEmptyText,
				});

			var template = new CollectionTemplate<AbstractDocumentItemEntity> ("DocumentItem", data.Controller, this.DataContext)
				.DefineText           (x => UIBuilder.FormatText (GetDocumentItemSummary (x)))
				.DefineCompactText    (x => UIBuilder.FormatText (GetDocumentItemSummary (x)))
				.DefineCreateItem     (this.CreateArticleDocumentItem)  // le bouton [+] crée une ligne d'article
				.DefineCreateGetIndex (this.CreateArticleGetIndex);

			data.Add (CollectionAccessor.Create (this.EntityGetter, x => x.Lines, template));
		}

		private void CreateUIBillings(SummaryDataItems data)
		{
			data.Add (
				new SummaryData
				{
					AutoGroup          = true,
					Name		       = "BillingDetails",
					IconUri		       = "Data.BillingDetails",
					Title		       = UIBuilder.FormatText ("Facturation"),
					CompactTitle       = UIBuilder.FormatText ("Facturation"),
					Text		       = CollectionTemplate.DefaultEmptyText,
				});

			var template = new CollectionTemplate<BillingDetailEntity> ("BillingDetails", data.Controller, this.DataContext)
				.DefineText        (x => UIBuilder.FormatText (GetBillingDetailsSummary (this.Entity, x)))
				.DefineCompactText (x => UIBuilder.FormatText (GetBillingDetailsSummary (this.Entity, x)))
				.DefineSetupItem   (this.SetupBillingDetails);

			data.Add (CollectionAccessor.Create (this.EntityGetter, x => x.BillingDetails, template));
		}

		private void CreateUIComments(SummaryDataItems data)
		{
			SummaryControllers.Common.CreateUIComments (this.DataContext, data, this.EntityGetter, x => x.Comments);
		}


		private ArticleDocumentItemEntity CreateArticleDocumentItem()
		{
			//	Crée un nouvelle ligne dans la facture du type le plus courant, c'est-à-dire ArticleDocumentItemEntity.
			var article = this.DataContext.CreateEmptyEntity<ArticleDocumentItemEntity> ();

			article.Visibility = true;
			article.BeginDate  = this.Entity.CreationDate;
			article.EndDate    = this.Entity.CreationDate;

			return article;
		}

		private int CreateArticleGetIndex()
		{
			//	Retourne l'index où insérer la nouvelle ligne d'article créée avec le bouton [+].
			//	Depuis la dernière ligne, on ignore les lignes de totaux et les articles 'frais de port'.
			int index = this.Entity.Lines.Count;  // insère à la fin par défaut

			while (index > 0)
			{
				var line = this.Entity.Lines[index-1];

				if (line is PriceDocumentItemEntity)
				{
					index--;  // insère avant un (sous-)total
					continue;
				}

				if (ArticleDocumentItemHelper.IsFixedTax (line as ArticleDocumentItemEntity))
				{
					index--;  // insère avant les frais de port
					continue;
				}

				break;
			}

			return index;
		}


		private static string GetDocumentItemSummary(AbstractDocumentItemEntity documentItemEntity)
		{
			if (documentItemEntity is TextDocumentItemEntity)
			{
				return GetTextDocumentItemSummary (documentItemEntity as TextDocumentItemEntity);
			}

			if (documentItemEntity is ArticleDocumentItemEntity)
			{
				return GetArticleDocumentItemSummary (documentItemEntity as ArticleDocumentItemEntity);
			}

			if (documentItemEntity is PriceDocumentItemEntity)
			{
				return GetPriceDocumentItemSummary (documentItemEntity as PriceDocumentItemEntity);
			}

			return null;			
		}

		private static string GetTextDocumentItemSummary(TextDocumentItemEntity x)
		{
			if (string.IsNullOrEmpty (x.Text))
			{
				return "<i>Texte</i>";
			}
			else
			{
				return x.Text;
			}
		}

		private static string GetArticleDocumentItemSummary(ArticleDocumentItemEntity x)
		{
			var quantity = ArticleDocumentItemHelper.GetArticleQuantityAndUnit (x);
			var desc = Misc.FirstLine (ArticleDocumentItemHelper.GetArticleDescription (x));
			var price = Misc.PriceToString (x.PrimaryLinePriceBeforeTax);

			string text = string.Join (" ", quantity, desc, price);

			if (string.IsNullOrEmpty (text))
			{
				return "<i>Article</i>";
			}
			else
			{
				return text;
			}
		}

		private static string GetPriceDocumentItemSummary(PriceDocumentItemEntity x)
		{
			var builder = new System.Text.StringBuilder ();
			bool first = true;

			if (x.Discount.DiscountRate.HasValue)
			{
				if (!first)
				{
					builder.Append (", ");
				}

				builder.Append ("Rabais ");
				builder.Append (Misc.PercentToString (x.Discount.DiscountRate));

				first = false;
			}

			if (x.Discount.DiscountAmount.HasValue)
			{
				if (!first)
				{
					builder.Append (", ");
				}

				builder.Append ("Rabais ");
				builder.Append (Misc.PriceToString (x.Discount.DiscountAmount));

				first = false;
			}

			if (x.FixedPriceAfterTax.HasValue)
			{
				if (!first)
				{
					builder.Append (", ");
				}

				builder.Append ("Montant arrêté ");
				builder.Append (Misc.PriceToString (x.FixedPriceAfterTax));

				first = false;
			}

			if (first)
			{
				builder.Append ("<i>Total</i>");
			}

			return builder.ToString ();
		}


		private static string GetBillingDetailsSummary(InvoiceDocumentEntity invoiceDocument, BillingDetailEntity billingDetails)
		{
			string amount = Misc.PriceToString (billingDetails.AmountDue.Amount);
			string title = Misc.FirstLine (billingDetails.Title);
			string ratio = InvoiceDocumentHelper.GetInstalmentRatio (invoiceDocument, billingDetails, true, false);

			if (ratio == null)
			{
				return UIBuilder.FormatText (amount, title).ToString ();
			}
			else
			{
				return UIBuilder.FormatText (amount, ratio, title).ToString ();
			}
		}

		private void SetupBillingDetails(BillingDetailEntity billingDetails)
		{
			var date = Date.Today;

			billingDetails.AmountDue.Date = date;
			billingDetails.Title = string.Format ("Votre commande du {0}", Misc.GetDateTimeDescription (date.ToDateTime ()));
			billingDetails.EsrCustomerNumber = "01-69444-3";  // compte BVR
			billingDetails.EsrReferenceNumber = "96 13070 01000 02173 50356 73892";  // n° de réf BVR lié
			// TODO: Trouver ces 2 dernières informations de façon plus générale !
		}


		private TileContainer					tileContainer;
	}
}

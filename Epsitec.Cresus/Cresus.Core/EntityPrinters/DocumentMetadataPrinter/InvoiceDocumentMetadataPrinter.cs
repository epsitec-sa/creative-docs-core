﻿//	Copyright © 2010, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Daniel ROUX, Maintainer: Daniel ROUX

using Epsitec.Common.Debug;
using Epsitec.Common.Dialogs;
using Epsitec.Common.Drawing;
using Epsitec.Common.IO;
using Epsitec.Common.Printing;
using Epsitec.Common.Support;
using Epsitec.Common.Types;
using Epsitec.Common.Widgets;

using Epsitec.Common.Support.EntityEngine;
using Epsitec.Cresus.Core.Business;
using Epsitec.Cresus.Core.Documents;
using Epsitec.Cresus.Core.Entities;
using Epsitec.Cresus.Core.Helpers;
using Epsitec.Cresus.Core.Print;
using Epsitec.Cresus.Core.Print.Bands;
using Epsitec.Cresus.Core.Print.Containers;
using Epsitec.Cresus.Core.Print.EntityPrinters;
using Epsitec.Cresus.Core.Resolvers;
using Epsitec.Cresus.Core.Library.Business.ContentAccessors;
using Epsitec.Cresus.Core.Controllers.BusinessDocumentControllers;

using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Epsitec.Cresus.Core.EntityPrinters
{
	public class InvoiceDocumentMetadataPrinter : AbstractDocumentMetadataPrinter
	{
		public InvoiceDocumentMetadataPrinter(IBusinessContext businessContext, AbstractEntity entity, PrintingOptionDictionary options, PrintingUnitDictionary printingUnits)
			: base (businessContext, entity, options, printingUnits)
		{
		}


		protected override Margins PageMargins
		{
			get
			{
				double leftMargin   = this.GetOptionValue (DocumentOption.LeftMargin, 20);
				double rightMargin  = this.GetOptionValue (DocumentOption.RightMargin, 20);
				double topMargin    = this.GetOptionValue (DocumentOption.TopMargin, 20);
				double bottomMargin = this.GetOptionValue (DocumentOption.BottomMargin, 20);

				double h = AbstractDocumentMetadataPrinter.reportHeight;

				if (this.HasIsr && this.HasOption (DocumentOption.IsrPosition, "WithInside"))
				{
					return new Margins (leftMargin, rightMargin, topMargin+h*2, h+InvoiceDocumentMetadataPrinter.marginBeforeIsr+AbstractIsrBand.DefautlSize.Height);
				}
				else
				{
					return new Margins (leftMargin, rightMargin, topMargin+h*2, h+bottomMargin);
				}
			}
		}


		public override void BuildSections()
		{
			base.BuildSections ();

			if (!this.HasIsr || this.HasOption (DocumentOption.IsrPosition, "Without") || this.PreviewMode == Print.PreviewMode.ContinuousPreview)
			{
				if (this.Entity.BillingDetails.Count != 0)
				{
					var billingDetails = this.Entity.BillingDetails[0];
					int firstPage = this.documentContainer.PrepareEmptyPage (PageType.First);

					this.BuildHeader (billingDetails);
					this.BuildArticles ();
					this.BuildConditions (billingDetails);
					this.BuildPages (billingDetails, firstPage);
					this.BuildReportHeaders (firstPage);
					this.BuildReportFooters (firstPage);

					this.documentContainer.Ending (firstPage);
				}
			}
			else
			{
				int documentRank = 0;
				bool onlyTotal = false;
				foreach (var billingDetails in this.Entity.BillingDetails)
				{
					this.documentContainer.DocumentRank = documentRank++;
					int firstPage = this.documentContainer.PrepareEmptyPage (PageType.First);

					this.BuildHeader (billingDetails);
					this.BuildArticles (onlyTotal: onlyTotal);
					this.BuildConditions (billingDetails);
					this.BuildPages (billingDetails, firstPage);
					this.BuildReportHeaders (firstPage);
					this.BuildReportFooters (firstPage);
					this.BuildIsrs (billingDetails, firstPage);

					this.documentContainer.Ending (firstPage);
					onlyTotal = true;
				}
			}
		}


		protected override void InitializeColumns()
		{
			this.tableColumns.Clear ();

			double priceWidth = 13 + this.CellMargin*2;  // largeur standard pour un montant ou une quantité

			if (this.IsColumnsOrderQD)
			{
				this.tableColumns.Add (TableColumnKeys.LineNumber,                new TableColumn ("N°",          priceWidth,   ContentAlignment.MiddleLeft));
				this.tableColumns.Add (TableColumnKeys.MainQuantity,              new TableColumn ("Facturé",     priceWidth,   ContentAlignment.MiddleRight));
				this.tableColumns.Add (TableColumnKeys.AdditionalType,            new TableColumn ("",            priceWidth+3, ContentAlignment.MiddleLeft));
				this.tableColumns.Add (TableColumnKeys.AdditionalQuantity,        new TableColumn ("Autre",       priceWidth,   ContentAlignment.MiddleRight));
				this.tableColumns.Add (TableColumnKeys.AdditionalDate,            new TableColumn ("",            priceWidth+3, ContentAlignment.MiddleLeft));
				this.tableColumns.Add (TableColumnKeys.ArticleId,                 new TableColumn ("Article",     priceWidth,   ContentAlignment.MiddleLeft));
				this.tableColumns.Add (TableColumnKeys.ArticleDescription,        new TableColumn ("Désignation", 0,            ContentAlignment.MiddleLeft));  // seule colonne en mode width = fill

				this.tableColumns.Add (TableColumnKeys.UnitPrice,                 new TableColumn ("p.u. HT",     priceWidth,   ContentAlignment.MiddleRight));
				this.tableColumns.Add (TableColumnKeys.Discount,                  new TableColumn ("Rabais",      priceWidth,   ContentAlignment.MiddleRight));
				this.tableColumns.Add (TableColumnKeys.LinePrice,                 new TableColumn ("Prix HT",     priceWidth,   ContentAlignment.MiddleRight));
				this.tableColumns.Add (TableColumnKeys.Vat,                       new TableColumn ("TVA",         priceWidth,   ContentAlignment.MiddleRight));
				this.tableColumns.Add (TableColumnKeys.Total,                     new TableColumn ("Prix TTC",    priceWidth,   ContentAlignment.MiddleRight));
			}
			else
			{
				this.tableColumns.Add (TableColumnKeys.LineNumber,                new TableColumn ("N°",          priceWidth,   ContentAlignment.MiddleLeft));
				this.tableColumns.Add (TableColumnKeys.ArticleId,                 new TableColumn ("Article",     priceWidth,   ContentAlignment.MiddleLeft));
				this.tableColumns.Add (TableColumnKeys.ArticleDescription,        new TableColumn ("Désignation", 0,            ContentAlignment.MiddleLeft));  // seule colonne en mode width = fill
				this.tableColumns.Add (TableColumnKeys.MainQuantity,              new TableColumn ("Facturé",     priceWidth,   ContentAlignment.MiddleRight));
				this.tableColumns.Add (TableColumnKeys.AdditionalType,            new TableColumn ("",            priceWidth+3, ContentAlignment.MiddleLeft));
				this.tableColumns.Add (TableColumnKeys.AdditionalQuantity,        new TableColumn ("Autre",       priceWidth,   ContentAlignment.MiddleRight));
				this.tableColumns.Add (TableColumnKeys.AdditionalDate,            new TableColumn ("",            priceWidth+3, ContentAlignment.MiddleLeft));

				this.tableColumns.Add (TableColumnKeys.UnitPrice,                 new TableColumn ("p.u. HT",     priceWidth,   ContentAlignment.MiddleRight));
				this.tableColumns.Add (TableColumnKeys.Discount,                  new TableColumn ("Rabais",      priceWidth,   ContentAlignment.MiddleRight));
				this.tableColumns.Add (TableColumnKeys.LinePrice,                 new TableColumn ("Prix HT",     priceWidth,   ContentAlignment.MiddleRight));
				this.tableColumns.Add (TableColumnKeys.Vat,                       new TableColumn ("TVA",         priceWidth,   ContentAlignment.MiddleRight));
				this.tableColumns.Add (TableColumnKeys.Total,                     new TableColumn ("Prix TTC",    priceWidth,   ContentAlignment.MiddleRight));
			}
		}

		protected override DocumentItemAccessorMode DocumentItemAccessorMode
		{
			get
			{
				var mode = DocumentItemAccessorMode.UseMainColumns |
						   DocumentItemAccessorMode.DescriptionIndented |
						   DocumentItemAccessorMode.UseArticleName;  // le nom court suffit sur une facture

				if (this.HasOption (DocumentOption.ArticleAdditionalQuantities))  // imprime les autres quantités ?
				{
					mode |= DocumentItemAccessorMode.AdditionalQuantities;
				}

				return mode;
			}
		}

		protected override void HideColumns(List<DocumentItemAccessor> accessors)
		{
			if (this.HasOption (DocumentOption.LineNumber, "None"))
			{
				this.tableColumns[TableColumnKeys.LineNumber].Visible = false;
			}

			if (!this.HasOption (DocumentOption.ArticleId))
			{
				this.tableColumns[TableColumnKeys.ArticleId].Visible = false;
			}

			if (!this.HasOption (DocumentOption.ArticleAdditionalQuantities))
			{
				this.tableColumns[TableColumnKeys.AdditionalType].Visible = false;
				this.tableColumns[TableColumnKeys.AdditionalQuantity].Visible = false;
				this.tableColumns[TableColumnKeys.AdditionalDate].Visible = false;
			}

			if (AbstractDocumentMetadataPrinter.IsEmptyColumn (accessors, DocumentItemAccessorColumn.Discount))
			{
				this.tableColumns[TableColumnKeys.Discount].Visible = false;
			}

			if (AbstractDocumentMetadataPrinter.IsEmptyColumn (accessors, DocumentItemAccessorColumn.Vat))
			{
				this.tableColumns[TableColumnKeys.Vat].Visible = false;
			}
		}

		protected override int BuildLine(TableBand table, int row, DocumentItemAccessor accessor, AbstractDocumentItemEntity prevLine, AbstractDocumentItemEntity line, AbstractDocumentItemEntity nextLine)
		{
			for (int i = 0; i < accessor.RowsCount; i++)
			{
				if (!this.HasOption (DocumentOption.LineNumber, "None"))
				{
					this.SetTableText (table, row+i, TableColumnKeys.LineNumber, accessor.GetContent (i, DocumentItemAccessorColumn.LineNumber));
				}

				this.SetTableText (table, row+i, TableColumnKeys.MainQuantity, AbstractDocumentMetadataPrinter.GetQuantityAndUnit (accessor, i, DocumentItemAccessorColumn.MainQuantity, DocumentItemAccessorColumn.MainUnit));

				if (this.HasOption (DocumentOption.ArticleAdditionalQuantities))  // imprime les autres quantités ?
				{
					this.SetTableText (table, row+i, TableColumnKeys.AdditionalType, accessor.GetContent (i, DocumentItemAccessorColumn.AdditionalType));
					this.SetTableText (table, row+i, TableColumnKeys.AdditionalQuantity, AbstractDocumentMetadataPrinter.GetQuantityAndUnit (accessor, i, DocumentItemAccessorColumn.AdditionalQuantity, DocumentItemAccessorColumn.AdditionalUnit));
					this.SetTableText (table, row+i, TableColumnKeys.AdditionalDate, AbstractDocumentMetadataPrinter.GetDates (accessor, i, DocumentItemAccessorColumn.AdditionalBeginDate, DocumentItemAccessorColumn.AdditionalEndDate));
				}

				if (this.HasOption (DocumentOption.ArticleId))
				{
					this.SetTableText (table, row+i, TableColumnKeys.ArticleId, accessor.GetContent (i, DocumentItemAccessorColumn.ArticleId));
				}

				this.SetTableText (table, row+i, TableColumnKeys.ArticleDescription, accessor.GetContent (i, DocumentItemAccessorColumn.ArticleDescription));

				this.SetTableText (table, row+i, TableColumnKeys.UnitPrice, accessor.GetContent (i, DocumentItemAccessorColumn.UnitPrice));
				this.SetTableText (table, row+i, TableColumnKeys.Discount, accessor.GetContent (i, DocumentItemAccessorColumn.Discount));
				this.SetTableText (table, row+i, TableColumnKeys.LinePrice, accessor.GetContent (i, DocumentItemAccessorColumn.LinePrice));
				this.SetTableText (table, row+i, TableColumnKeys.Vat, accessor.GetContent (i, DocumentItemAccessorColumn.Vat));

				var total = accessor.GetContent (i, DocumentItemAccessorColumn.Total);
				if (line is EndTotalDocumentItemEntity && i == accessor.RowsCount-1)
				{
					total = total.ApplyBold ();
				}
				this.SetTableText (table, row+i, TableColumnKeys.Total, total);
			}

			int last = row+accessor.RowsCount-1;

			if (line is SubTotalDocumentItemEntity)
			{
				table.SetCellBorder (last, this.GetCellBorder (bottomBold: true));
			}

			if (line is EndTotalDocumentItemEntity)
			{
				if (this.IsWithFrame)
				{
					table.SetCellBorder (last, this.GetCellBorder (topLess: true));
					table.SetCellBorder (this.tableColumns[TableColumnKeys.Total].Rank, last, new CellBorder (CellBorder.BoldWidth));
				}
				else
				{
					table.SetCellBorder (last, this.GetCellBorder (bottomBold: true, topLess: true));
				}
			}

			return accessor.RowsCount;
		}

		protected override void BuildFinish()
		{
			this.RemoveRightBorder (TableColumnKeys.AdditionalType);
			this.RemoveRightBorder (TableColumnKeys.AdditionalQuantity);
		}


		private void BuildConditions(BillingDetailEntity billingDetails)
		{
			//	Met les conditions à la fin de la facture.
			FormattedText conditions = FormattedText.Join (FormattedText.HtmlBreak, billingDetails.Text, billingDetails.AmountDue.PaymentMode.Description);

			if (!conditions.IsNullOrEmpty)
			{
				var band = new TextBand ();
				band.Text = conditions;
				band.FontSize = this.FontSize;

				this.documentContainer.AddFromTop (band, 0);
			}
		}


		private void BuildIsrs(BillingDetailEntity billingDetails, int firstPage)
		{
			if (this.HasOption (DocumentOption.IsrPosition, "WithInside"))
			{
				this.BuildInsideIsrs (billingDetails, firstPage);
			}

			if (this.HasOption (DocumentOption.IsrPosition, "WithOutside"))
			{
				this.BuildOutsideIsr (billingDetails, firstPage);
			}
		}

		private void BuildInsideIsrs(BillingDetailEntity billingDetails, int firstPage)
		{
			//	Met un BVR orangé ou un BV rose en bas de chaque page.
			for (int page = firstPage; page < this.documentContainer.PageCount (); page++)
			{
				this.documentContainer.CurrentPage = page;

				this.BuildIsr (billingDetails, mackle: page != this.documentContainer.PageCount ()-1);
			}
		}

		private void BuildOutsideIsr(BillingDetailEntity billingDetails, int firstPage)
		{
			//	Met un BVR orangé ou un BV rose sur une dernière page séparée.
			var bounds = new Rectangle (Point.Zero, AbstractIsrBand.DefautlSize);

			if (this.documentContainer.PageCount () - firstPage > 1 ||
				this.documentContainer.CurrentVerticalPosition - InvoiceDocumentMetadataPrinter.marginBeforeIsr < bounds.Top ||
				this.HasPrintingUnitDefined (PageType.Single) == false)
			{
				//	On ne prépare pas une nouvelle page si on peut mettre la facture
				//	et le BV sur une seule page !
				this.documentContainer.PrepareEmptyPage (PageType.Isr);
			}

			this.BuildIsr (billingDetails);
		}

		private void BuildIsr(BillingDetailEntity billingDetails, bool mackle=false)
		{
			//	Met un BVR orangé ou un BV rose au bas de la page courante.
			AbstractIsrBand isr;

			if (this.HasOption (DocumentOption.IsrType, "Isr"))
			{
				isr = new IsrBand ();  // BVR orangé
			}
			else
			{
				isr = new IsBand ();  // BV rose
			}

			isr.PaintIsrSimulator = this.HasOption (DocumentOption.IsrFacsimile);
			isr.From = this.Entity.BillToMailContact.GetSummary ();
			isr.To = billingDetails.IsrDefinition.SubscriberAddress;
			isr.Communication = InvoiceDocumentHelper.GetTitle (this.Metadata, this.Entity, billingDetails);

			isr.Slip = new IsrSlip (billingDetails);
			isr.NotForUse = mackle;  // pour imprimer "XXXXX XX" sur un faux BVR

			var bounds = new Rectangle (Point.Zero, AbstractIsrBand.DefautlSize);
			this.documentContainer.AddAbsolute (isr, bounds);
		}


		private bool HasIsr
		{
			//	Indique s'il faut imprimer le BV. Pour cela, il faut que l'unité d'impression soit définie pour le type PageType.Isr.
			//	En mode DocumentOption.IsrPosition = "WithOutside", cela évite d'imprimer à double un BV sur l'imprimante 'Blanc'.
			get
			{
				if (this.HasOption (DocumentOption.IsrPosition, "WithInside"))
				{
					return true;
				}

				if (this.HasOption (DocumentOption.IsrPosition, "WithOutside"))
				{
					if (this.currentPrintingUnit != null)
					{
						var example = new DocumentPrintingUnitsEntity ();
						example.Code = this.currentPrintingUnit.DocumentPrintingUnitCode;

						var documentPrintingUnits = this.businessContext.DataContext.GetByExample<DocumentPrintingUnitsEntity> (example).FirstOrDefault ();

						if (documentPrintingUnits != null)
						{
							var pageTypes = documentPrintingUnits.GetPageTypes ();

							return pageTypes.Contains (PageType.Isr);
						}
					}
				}

				return false;
			}
		}


		private static readonly double		marginBeforeIsr = 10;
	}
}

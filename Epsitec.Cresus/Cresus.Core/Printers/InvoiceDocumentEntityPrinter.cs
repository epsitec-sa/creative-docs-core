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
using Epsitec.Cresus.Core.Entities;
using Epsitec.Cresus.Core.Helpers;

using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Epsitec.Cresus.Core.Printers
{

	public class InvoiceDocumentEntityPrinter : AbstractEntityPrinter<InvoiceDocumentEntity>
	{
		public InvoiceDocumentEntityPrinter(InvoiceDocumentEntity entity)
			: base (entity)
		{
			DocumentType type;

			type = new DocumentType ("BV", "Facture avec BV", "Facture A4 avec un bulletin de versement orange ou rose intégré au bas de chaque page.");
			AbstractEntityPrinter.DocumentTypeAddInvoice (type.DocumentOptions, isBL: false);
			AbstractEntityPrinter.DocumentTypeAddBV      (type.DocumentOptions);
			this.DocumentTypes.Add (type);

			type = new DocumentType ("Simple", "Facture sans BV", "Facture A4 simple sans bulletin de versement.");
			AbstractEntityPrinter.DocumentTypeAddInvoice     (type.DocumentOptions, isBL: false);
			AbstractEntityPrinter.DocumentTypeAddOrientation (type.DocumentOptions);
			AbstractEntityPrinter.DocumentTypeAddMargin      (type.DocumentOptions);
			AbstractEntityPrinter.DocumentTypeAddSpecimen    (type.DocumentOptions);
			this.DocumentTypes.Add (type);

			type = new DocumentType ("BL", "Bulletin de livraison", "Bulletin de livraison A4, sans prix.");
			AbstractEntityPrinter.DocumentTypeAddInvoice     (type.DocumentOptions, isBL: true);
			AbstractEntityPrinter.DocumentTypeAddOrientation (type.DocumentOptions);
			AbstractEntityPrinter.DocumentTypeAddMargin      (type.DocumentOptions);
			AbstractEntityPrinter.DocumentTypeAddBL          (type.DocumentOptions);
			AbstractEntityPrinter.DocumentTypeAddSpecimen    (type.DocumentOptions);
			this.DocumentTypes.Add (type);
		}

		public override string JobName
		{
			get
			{
				return UIBuilder.FormatText ("Facture", this.entity.IdA).ToSimpleText ();
			}
		}

		public override Size PageSize
		{
			get
			{
				if (this.HasDocumentOption ("Orientation.Horizontal"))
				{
					return new Size (297, 210);  // A4 horizontal
				}
				else
				{
					return new Size (210, 297);  // A4 vertical
				}
			}
		}

		public override Margins PageMargins
		{
			get
			{
				double h = this.IsBL ? 0 : InvoiceDocumentEntityPrinter.reportHeight;

				if (this.DocumentTypeSelected == "BV")
				{
					return new Margins (20, 10, 20+h*2, h+10+AbstractBvBand.DefautlSize.Height);
				}
				else
				{
					return new Margins (20, 10, 20+h*2, h+20);
				}
			}
		}

		public override void BuildSections()
		{
			base.BuildSections ();
			this.documentContainer.Clear ();

			if (this.DocumentTypeSelected == "BV")
			{
				this.BuildHeader ();
				this.BuildArticles ();
				this.BuildConditions ();
				this.BuildPages ();
				this.BuildReportHeaders ();
				this.BuildReportFooters ();
				this.BuildBvs ();
			}

			if (this.DocumentTypeSelected == "Simple")
			{
				this.BuildHeader ();
				this.BuildArticles ();
				this.BuildConditions ();
				this.BuildPages ();
				this.BuildReportHeaders ();
				this.BuildReportFooters ();
			}

			if (this.DocumentTypeSelected == "BL")
			{
				this.BuildHeader ();
				this.BuildArticles ();
				this.BuildFooterBL ();
				this.BuildPages ();
			}
		}

		public override void PrintCurrentPage(IPaintPort port)
		{
			base.PrintCurrentPage (port);

			this.documentContainer.Paint (port, this.CurrentPage, this.IsPreview);
		}


		private void BuildHeader()
		{
			//	Ajoute l'en-tête de la facture dans le document.
			var imageBand = new ImageBand ();
			imageBand.Load("logo-cresus.png");
			imageBand.BuildSections (60, 50, 50, 50);
			this.documentContainer.AddAbsolute (imageBand, new Rectangle (20, this.PageSize.Height-10-50, 60, 50));

			var textBand = new TextBand ();
			textBand.Text = "<b>Les logiciels de gestion</b>";
			textBand.Font = font;
			textBand.FontSize = 5.0;
			this.documentContainer.AddAbsolute (textBand, new Rectangle (20, this.PageSize.Height-10-imageBand.GetSectionHeight (0)-10, 80, 10));

			var mailContactBand = new TextBand ();
			mailContactBand.Text = InvoiceDocumentHelper.GetMailContact (this.entity);
			mailContactBand.Font = font;
			mailContactBand.FontSize = fontSize;
			this.documentContainer.AddAbsolute (mailContactBand, new Rectangle (120, this.PageSize.Height-57, 80, 25));

			string concerne = InvoiceDocumentHelper.GetConcerne (this.entity);
			if (!string.IsNullOrEmpty (concerne))
			{
				var concerneBand = new TableBand ();
				concerneBand.ColumnsCount = 2;
				concerneBand.RowsCount = 1;
				concerneBand.PaintFrame = false;
				concerneBand.Font = font;
				concerneBand.FontSize = fontSize;
				concerneBand.CellMargins = new Margins (0);
				concerneBand.SetRelativeColumWidth (0, 15);
				concerneBand.SetRelativeColumWidth (1, 80);
				concerneBand.SetText (0, 0, "Concerne");
				concerneBand.SetText (1, 0, concerne);
				this.documentContainer.AddAbsolute (concerneBand, new Rectangle (20, this.PageSize.Height-67, 100, 15));
			}

			var titleBand = new TextBand ();
			titleBand.Text = InvoiceDocumentHelper.GetTitle (this.entity, this.IsBL);
			titleBand.Font = font;
			titleBand.FontSize = 5.0;
			this.documentContainer.AddAbsolute (titleBand, new Rectangle (20, this.PageSize.Height-82, 90, 10));

			string date = InvoiceDocumentHelper.GetDate (this.entity);
			var dateBand = new TextBand ();
			dateBand.Text = UIBuilder.FormatText ("Crissier, le ", date).ToString ();
			dateBand.Font = font;
			dateBand.FontSize = fontSize;
			this.documentContainer.AddAbsolute (dateBand, new Rectangle (120, this.PageSize.Height-82, 80, 10));
		}

		private void BuildArticles()
		{
			//	Ajoute les articles dans le document.
			this.documentContainer.CurrentVerticalPosition = this.PageSize.Height-87;

			this.tableColumns.Clear ();

			double priceWidth = 13 + this.CellMargin*2;  // largeur standard pour un montant ou une quantité

			if (this.IsColumnsOrderQD)
			{
				this.tableColumns.Add ("Nb",   new TableColumn ("?",           priceWidth,   ContentAlignment.MiddleLeft));  // "Quantité" ou "Livré"
				this.tableColumns.Add ("Suit", new TableColumn ("Suit",        priceWidth,   ContentAlignment.MiddleLeft));
				this.tableColumns.Add ("Date", new TableColumn ("Date",        priceWidth+3, ContentAlignment.MiddleLeft));
				this.tableColumns.Add ("ArId", new TableColumn ("Article",     priceWidth,   ContentAlignment.MiddleLeft));
				this.tableColumns.Add ("Desc", new TableColumn ("Désignation", 0,            ContentAlignment.MiddleLeft));  // seule colonne en mode width = fill
				this.tableColumns.Add ("Rab",  new TableColumn ("Rabais",      priceWidth,   ContentAlignment.MiddleRight));
				this.tableColumns.Add ("PU",   new TableColumn ("p.u. HT",     priceWidth,   ContentAlignment.MiddleRight));
				this.tableColumns.Add ("PT",   new TableColumn ("Prix HT",     priceWidth,   ContentAlignment.MiddleRight));
				this.tableColumns.Add ("TVA",  new TableColumn ("TVA",         priceWidth,   ContentAlignment.MiddleRight));
				this.tableColumns.Add ("Tot",  new TableColumn ("Total",       priceWidth,   ContentAlignment.MiddleRight));
			}
			else
			{
				this.tableColumns.Add ("ArId", new TableColumn ("Article",     priceWidth,   ContentAlignment.MiddleLeft));
				this.tableColumns.Add ("Desc", new TableColumn ("Désignation", 0,            ContentAlignment.MiddleLeft));  // seule colonne en mode width = fill
				this.tableColumns.Add ("Nb",   new TableColumn ("?",           priceWidth,   ContentAlignment.MiddleLeft));  // "Quantité" ou "Livré"
				this.tableColumns.Add ("Suit", new TableColumn ("Suit",        priceWidth,   ContentAlignment.MiddleLeft));
				this.tableColumns.Add ("Date", new TableColumn ("Date",        priceWidth+3, ContentAlignment.MiddleLeft));
				this.tableColumns.Add ("Rab",  new TableColumn ("Rabais",      priceWidth,   ContentAlignment.MiddleRight));
				this.tableColumns.Add ("PU",   new TableColumn ("p.u. HT",     priceWidth,   ContentAlignment.MiddleRight));
				this.tableColumns.Add ("PT",   new TableColumn ("Prix HT",     priceWidth,   ContentAlignment.MiddleRight));
				this.tableColumns.Add ("TVA",  new TableColumn ("TVA",         priceWidth,   ContentAlignment.MiddleRight));
				this.tableColumns.Add ("Tot",  new TableColumn ("Total",       priceWidth,   ContentAlignment.MiddleRight));
			}

			//	Première passe pour déterminer le nombre le lignes du tableau de la facture
			//	ainsi que les colonnes visibles.
			int rowCount = 1;  // déjà 1 pour l'en-tête (titres des colonnes)

			foreach (var line in this.entity.Lines)
			{
				if (line.Visibility)
				{
					bool exist = false;

					if (line is TextDocumentItemEntity)
					{
						exist = this.InitializeColumnTextLine (line as TextDocumentItemEntity);
					}

					if (line is ArticleDocumentItemEntity)
					{
						exist = this.InitializeColumnArticleLine (line as ArticleDocumentItemEntity);
					}

					if (line is PriceDocumentItemEntity)
					{
						exist = this.InitializeColumnPriceLine (line as PriceDocumentItemEntity);
					}

					if (exist)
					{
						rowCount++;
					}
				}
			}

			if (this.IsBL)
			{
				this.tableColumns["Rab"].Visible = false;
				this.tableColumns["PU" ].Visible = false;
				this.tableColumns["PT" ].Visible = false;
				this.tableColumns["TVA"].Visible = false;
				this.tableColumns["Tot"].Visible = false;
			}

			if (!this.HasDocumentOption ("Delayed"))  // n'imprime pas les articles retardés ?
			{
				this.tableColumns["Suit"].Visible = false;
				this.tableColumns["Date"].Visible = false;
			}

			if (!this.HasDocumentOption ("ArticleId"))  // n'imprime pas les numéros d'article ?
			{
				this.tableColumns["ArId"].Visible = false;
			}

			//	Compte et numérote les colonnes visibles.
			this.visibleColumnCount = 0;

			foreach (var column in this.tableColumns.Values)
			{
				if (column.Visible)
				{
					column.Rank = this.visibleColumnCount++;
				}
			}

			//	Deuxième passe pour générer les colonnes et les lignes du tableau.
			this.table = new TableBand ();
			this.table.ColumnsCount = this.visibleColumnCount;
			this.table.RowsCount = rowCount;
			this.table.PaintFrame = this.IsWithFrame;
			this.table.CellMargins = new Margins (this.CellMargin);

			//	Détermine le nom de la colonne "Nb".
			if (this.tableColumns["Suit"].Visible)  // colonne "Suit" visible ?
			{
				this.tableColumns["Nb"].Title = "Livré";  // affiche "Livré", "Suit", "Date"
			}
			else
			{
				this.tableColumns["Nb"].Title = "Quantité";  // affiche "Quantité"
			}

			//	Génère une première ligne d'en-tête (titres des colonnes).
			int row = 0;

			foreach (var column in this.tableColumns.Values)
			{
				if (column.Visible)
				{
					this.table.SetText (column.Rank, row, column.Title);
				}
			}

			this.InitializeRowAlignment (this.table, row);

			row++;

			//	Génère toutes les lignes pour les articles.
			int linePage = this.documentContainer.CurrentPage;
			double lineY = this.documentContainer.CurrentVerticalPosition;

			foreach (var line in this.entity.Lines)
			{
				if (line.Visibility)
				{
					bool exist = false;

					if (line is TextDocumentItemEntity)
					{
						exist = this.BuildTextLine (this.table, row, line as TextDocumentItemEntity);
					}

					if (line is ArticleDocumentItemEntity)
					{
						exist = this.BuildArticleLine (this.table, row, line as ArticleDocumentItemEntity);
					}

					if (line is PriceDocumentItemEntity)
					{
						exist = this.BuildPriceLine (this.table, row, line as PriceDocumentItemEntity, lastLine: row == rowCount-1);
					}

					if (exist)
					{
						this.InitializeRowAlignment (this.table, row);
						row++;
					}
				}
			}

			//	Détermine les largeurs des colonnes.
			double fixedWidth = 0;
			double[] columnWidths = new double[this.visibleColumnCount];
			foreach (var column in this.tableColumns.Values)
			{
				if (column.Visible)
				{
					if (column.Width != 0)  // pas la seule colonne en mode width = fill ?
					{
						double columnWidth = this.table.RequiredColumnWidth (column.Rank) + this.CellMargin*2;

						columnWidths[column.Rank] = columnWidth;
						fixedWidth += columnWidth;
					}
				}
			}

			if (fixedWidth < this.documentContainer.CurrentWidth - 50)  // reste au moins 5cm pour la colonne 'fill' ?
			{
				//	Initialise les largeurs en fonction des contenus réels des colonnes.
				foreach (var column in this.tableColumns.Values)
				{
					if (column.Visible)
					{
						if (column.Width != 0)  // pas la seule colonne en mode width = fill ?
						{
							column.Width = columnWidths[column.Rank];
						}
					}
				}
			}
			else
			{
				//	Initialise les largeurs d'après les estimations initiales.
				fixedWidth = 0;
				foreach (var column in this.tableColumns.Values)
				{
					if (column.Visible)
					{
						if (column.Width != 0)  // pas la seule colonne en mode width = fill ?
						{
							fixedWidth += column.Width;
						}
					}
				}
			}

			foreach (var column in this.tableColumns.Values)
			{
				if (column.Visible)
				{
					double columnWidth = column.Width;

					if (columnWidth == 0)  // seule colonne en mode width = fill ?
					{
						columnWidth = this.documentContainer.CurrentWidth - fixedWidth;  // utilise la largeur restante
					}

					this.table.SetRelativeColumWidth (column.Rank, columnWidth);
				}
			}

			//	Met la grande table dans le document.
			this.tableBounds = this.documentContainer.AddFromTop (this.table, 5.0);

			this.lastRowForEachSection = this.table.GetLastRowForEachSection ();

			// Met un trait horizontal sous l'en-tête.
			var currentPage = this.documentContainer.CurrentPage;
			this.documentContainer.CurrentPage = 0;  // dans la première page

			var h = this.table.GetRowHeight (0);
			this.BuildSeparator (lineY-h);

			this.documentContainer.CurrentPage = currentPage;
		}


		private bool InitializeColumnTextLine(TextDocumentItemEntity line)
		{
			this.tableColumns["Desc"].Visible = true;
			return true;
		}

		private bool InitializeColumnArticleLine(ArticleDocumentItemEntity line)
		{
			if (this.IsBL && this.IsPort (line))
			{
				return false;
			}

			if (!this.IsPrintableArticle (line))
			{
				return false;
			}

			this.tableColumns["ArId"].Visible = true;
			this.tableColumns["Desc"].Visible = true;
			this.tableColumns["Tot" ].Visible = true;

			if (line.VatCode != BusinessLogic.Finance.VatCode.None &&
				line.VatCode != BusinessLogic.Finance.VatCode.Excluded &&
				line.VatCode != BusinessLogic.Finance.VatCode.ZeroRated)
			{
				this.tableColumns["TVA"].Visible = true;
			}

			foreach (var quantity in line.ArticleQuantities)
			{
				if (quantity.Code == "livré")
				{
					this.tableColumns["Nb"].Visible = true;
					this.tableColumns["PU"].Visible = true;
					this.tableColumns["PT"].Visible = true;
				}

				if (quantity.Code == "suivra")
				{
					this.tableColumns["Suit"].Visible = true;
					this.tableColumns["Date"].Visible = true;
				}
			}

			if (line.Discounts.Count != 0)
			{
				this.tableColumns["Rab"].Visible = true;
			}

			return true;
		}

		private bool InitializeColumnPriceLine(PriceDocumentItemEntity line)
		{
			if (this.IsBL)
			{
				return false;
			}

			if (line.Discount.IsActive ())
			{
				this.InitializeColumnDiscountLine (line);
			}
			else
			{
				this.InitializeColumnTotalLine (line);
			}

			return true;
		}

		private void InitializeColumnDiscountLine(PriceDocumentItemEntity line)
		{
			if (line.PrimaryPriceBeforeTax.HasValue)
			{
				this.tableColumns["Rab"].Visible = true;
			}

			this.tableColumns["Desc"].Visible = true;
			this.tableColumns["PT"  ].Visible = true;
		}

		private void InitializeColumnTotalLine(PriceDocumentItemEntity line)
		{
			this.tableColumns["Desc"].Visible = true;
			this.tableColumns["TVA" ].Visible = true;
			this.tableColumns["Tot" ].Visible = true;
		}


		private bool BuildTextLine(TableBand table, int row, TextDocumentItemEntity line)
		{
			string text = string.Concat ("<b>", line.Text, "</b>");
			table.SetText (this.tableColumns["Desc"].Rank, row, text);

			return true;
		}

		private bool BuildArticleLine(TableBand table, int row, ArticleDocumentItemEntity line)
		{
			if (this.IsBL && this.IsPort (line))
			{
				return false;
			}

			if (!this.IsPrintableArticle (line))
			{
				return false;
			}

			string q1 = null;
			string q2 = null;
			string date = null;

			foreach (var quantity in line.ArticleQuantities)
			{
				if (quantity.Code == "livré")
				{
					q1 = Misc.FormatUnit (quantity.Quantity, quantity.Unit.Code);
				}

				if (quantity.Code == "suivra")
				{
					q2 = Misc.AppendLine (q2, Misc.FormatUnit (quantity.Quantity, quantity.Unit.Code));

					if (quantity.ExpectedDate.HasValue)
					{
						date = Misc.AppendLine(date, quantity.ExpectedDate.Value.ToString ());
					}
				}
			}

			string  description = ArticleDocumentItemHelper.GetArticleDescription (line);

			if (q1 != null)
			{
				table.SetText (this.tableColumns["Nb"].Rank, row, q1);
			}

			if (q2 != null)
			{
				table.SetText (this.tableColumns["Suit"].Rank, row, q2);
			}

			if (date != null)
			{
				table.SetText (this.tableColumns["Date"].Rank, row, date);
			}

			table.SetText (this.tableColumns["ArId"].Rank, row, ArticleDocumentItemHelper.GetArticleId (line));
			table.SetText (this.tableColumns["Desc"].Rank, row, description);
			table.SetText (this.tableColumns["PU"  ].Rank, row, Misc.PriceToString (line.PrimaryUnitPriceBeforeTax));

			if (line.ResultingLinePriceBeforeTax.HasValue && line.ResultingLineTax.HasValue)
			{
				decimal beforeTax = line.ResultingLinePriceBeforeTax.Value;
				decimal tax =       line.ResultingLineTax.Value;

				table.SetText (this.tableColumns["PT" ].Rank, row, Misc.PriceToString (beforeTax));
				table.SetText (this.tableColumns["TVA"].Rank, row, Misc.PriceToString (tax));
				table.SetText (this.tableColumns["Tot"].Rank, row, Misc.PriceToString (beforeTax+tax));
			}

			if (line.Discounts.Count != 0)
			{
				if (line.Discounts[0].DiscountRate.HasValue)
				{
					table.SetText (this.tableColumns["Rab"].Rank, row, Misc.PercentToString (line.Discounts[0].DiscountRate.Value));
				}

				if (line.Discounts[0].DiscountAmount.HasValue)
				{
					table.SetText (this.tableColumns["Rab"].Rank, row, Misc.PriceToString (line.Discounts[0].DiscountAmount.Value));
				}
			}

			return true;
		}

		private bool BuildPriceLine(TableBand table, int row, PriceDocumentItemEntity line, bool lastLine)
		{
			if (this.IsBL)
			{
				return false;
			}

			if (line.Discount.IsActive ())
			{
				this.BuildDiscountLine (table, row, line);
			}
			else
			{
				this.BuildTotalLine (table, row, line, lastLine);
			}

			return true;
		}

		private void BuildDiscountLine(TableBand table, int row, PriceDocumentItemEntity line)
		{
			//	Génère les 2 lignes de description.
			string beforeText = line.TextForPrimaryPrice;
			if (string.IsNullOrEmpty (beforeText))
			{
				beforeText = "Total avant rabais";
			}

			string afterText = line.TextForResultingPrice;
			if (string.IsNullOrEmpty (afterText))
			{
				afterText = "Total après rabais";
			}

			string desc = string.Concat (beforeText, "<br/>", afterText);

			//	Génère les 2 lignes de prix.
			string beforePrice = null;
			if (line.PrimaryPriceBeforeTax.HasValue)
			{
				beforePrice = Misc.PriceToString (line.PrimaryPriceBeforeTax.Value);
			}

			string afterPrice = null;
			if (line.ResultingPriceBeforeTax.HasValue)
			{
				afterPrice = Misc.PriceToString (line.ResultingPriceBeforeTax.Value);
			}

			string prix = string.Concat (beforePrice, "<br/>", afterPrice);

			//	Génère les 2 lignes de rabais.
			string rabais = null;
			if (line.Discount.DiscountRate.HasValue)
			{
				rabais = string.Concat ("<br/>", Misc.PercentToString (line.Discount.DiscountRate.Value));
			}

			table.SetText (this.tableColumns["Desc"].Rank, row, desc);
			table.SetText (this.tableColumns["Rab" ].Rank, row, rabais);
			table.SetText (this.tableColumns["PT"  ].Rank, row, prix);

			table.SetUnbreakableRow (row, true);
		}

		private void BuildTotalLine(TableBand table, int row, PriceDocumentItemEntity line, bool lastLine)
		{
			string desc = line.TextForFixedPrice;
			if (string.IsNullOrEmpty (desc))
			{
				desc = "Total arrêté";
			}

			string tva = null;
			if (line.ResultingTax.HasValue)
			{
				tva = Misc.PriceToString (line.ResultingTax.Value);
			}
			
			string total = null;
			if (line.FixedPriceAfterTax.HasValue)
			{
				total = Misc.PriceToString (line.FixedPriceAfterTax.Value);
			}

			if (lastLine)
			{
				desc  = string.Concat ("<b>", desc,  "</b>");
				tva   = string.Concat ("<b>", tva,   "</b>");
				total = string.Concat ("<b>", total, "</b>");
			}

			table.SetText (this.tableColumns["Desc"].Rank, row, desc);
			table.SetText (this.tableColumns["TVA" ].Rank, row, tva);
			table.SetText (this.tableColumns["Tot" ].Rank, row, total);

			if (lastLine)
			{
				table.SetCellBorderWidth (this.tableColumns["Tot"].Rank, row, 0.5);  // met un cadre épais
			}
		}

		private bool IsPrintableArticle(ArticleDocumentItemEntity line)
		{
			if (!this.HasDocumentOption ("Delayed"))  // n'imprime pas les articles retardés ?
			{
				foreach (var quantity in line.ArticleQuantities)
				{
					if (quantity.Code == "livré")
					{
						return true;
					}
				}

				return false;
			}

			return true;
		}


		private void InitializeRowAlignment(TableBand table, int row)
		{
			foreach (var column in this.tableColumns.Values)
			{
				if (column.Visible)
				{
					table.SetAlignment (column.Rank, row, column.Alignment);
				}
			}
		}


		private void BuildConditions()
		{
			//	Met les conditions à la fin de la facture.
			if (this.IsBL)
			{
				return;
			}

			string conditions = InvoiceDocumentHelper.GetConditions (this.entity);

			if (!string.IsNullOrEmpty (conditions))
			{
				var band = new TextBand ();
				band.Text = conditions;

				this.documentContainer.AddFromTop (band, 0);
			}
		}

		private void BuildFooterBL()
		{
			if (this.HasDocumentOption ("BL.Signing"))
			{
				var table = new TableBand ();

				table.ColumnsCount = 2;
				table.RowsCount = 1;
				table.PaintFrame = true;
				table.Font = font;
				table.FontSize = fontSize;
				table.CellMargins = new Margins (2);
				table.SetRelativeColumWidth (0,  60);
				table.SetRelativeColumWidth (1, 100);
				table.SetText (0, 0, "Matériel reçu en bonne et dûe forme");
				table.SetText (1, 0, "Reçu le :<br/><br/>Par :<br/><br/>Signature :<br/><br/><br/>");
				table.SetUnbreakableRow (0, true);

				this.documentContainer.AddToBottom (table, this.PageMargins.Bottom);
			}
		}

		private void BuildPages()
		{
			//	Met les numéros de page.
			double reportHeight = this.IsBL ? 0 : InvoiceDocumentEntityPrinter.reportHeight*2;

			var leftBounds  = new Rectangle (this.PageMargins.Left, this.PageSize.Height-this.PageMargins.Top+reportHeight+1, 80, 5);
			var rightBounds = new Rectangle (this.PageSize.Width-this.PageMargins.Right-80, this.PageSize.Height-this.PageMargins.Top+reportHeight+1, 80, 5);

			for (int page = 1; page < this.documentContainer.PageCount; page++)
			{
				this.documentContainer.CurrentPage = page;

				var leftHeader = new TextBand ();
				leftHeader.Text = InvoiceDocumentHelper.GetTitle (this.entity, this.IsBL);
				leftHeader.Alignment = ContentAlignment.BottomLeft;
				leftHeader.Font = font;
				leftHeader.FontSize = 4.0;

				var rightHeader = new TextBand ();
				rightHeader.Text = string.Format ("page {0}", (page+1).ToString ());
				rightHeader.Alignment = ContentAlignment.BottomRight;
				rightHeader.Font = font;
				rightHeader.FontSize = fontSize;

				this.documentContainer.AddAbsolute (leftHeader, leftBounds);
				this.documentContainer.AddAbsolute (rightHeader, rightBounds);
			}
		}

		private void BuildReportHeaders()
		{
			//	Met un report en haut des pages concernées, avec une répétition de la ligne
			//	d'en-tête (noms des colonnes).
			double width = this.PageSize.Width-this.PageMargins.Left-this.PageMargins.Right;

			for (int page = 1; page < this.documentContainer.PageCount; page++)
			{
				if (page >= this.tableBounds.Count)
				{
					break;
				}

				this.documentContainer.CurrentPage = page;

				var table = new TableBand ();
				table.ColumnsCount = this.visibleColumnCount;
				table.RowsCount = 2;
				table.PaintFrame = this.IsWithFrame;
				table.CellMargins = new Margins (this.CellMargin);

				//	Génère une première ligne d'en-tête (titres des colonnes).
				foreach (var column in this.tableColumns.Values)
				{
					if (column.Visible)
					{
						table.SetRelativeColumWidth (column.Rank, this.table.GetRelativeColumnWidth (column.Rank));
						table.SetText (column.Rank, 0, column.Title);
					}
				}

				//	Génère une deuxième ligne avec les montants à reporter.
				table.SetText (this.tableColumns["Desc"].Rank, 1, "Report");

				decimal sumPT, sumTva, sumTot;
				this.ComputeBottomReports (page-1, out sumPT, out sumTva, out sumTot);
				table.SetText (this.tableColumns["PT" ].Rank, 1, Misc.PriceToString (sumPT));
				table.SetText (this.tableColumns["TVA"].Rank, 1, Misc.PriceToString (sumTva));
				table.SetText (this.tableColumns["Tot"].Rank, 1, Misc.PriceToString (sumTot));

				this.InitializeRowAlignment (table, 0);
				this.InitializeRowAlignment (table, 1);

				var tableBound = this.tableBounds[page];
				double h = table.RequiredHeight (width);
				var bounds = new Rectangle (tableBound.Left, tableBound.Top, width, h);

				this.documentContainer.AddAbsolute (table, bounds);

				// Met un trait horizontal sous l'en-tête.
				h = table.GetRowHeight (0);
				this.BuildSeparator (bounds.Top-h);
			}
		}

		private void BuildReportFooters()
		{
			//	Met un report en bas des pages concernées.
			double width = this.PageSize.Width-this.PageMargins.Left-this.PageMargins.Right;

			for (int page = 0; page < this.documentContainer.PageCount-1; page++)
			{
				if (page >= this.tableBounds.Count-1)
				{
					//	S'il n'y a pas de tableau dans la page suivante, il est inutile de mettre
					//	un report au bas de celle-çi.
					break;
				}

				this.documentContainer.CurrentPage = page;

				var table = new TableBand ();
				table.ColumnsCount = this.visibleColumnCount;
				table.RowsCount = 1;
				table.PaintFrame = this.IsWithFrame;
				table.CellMargins = new Margins (this.CellMargin);

				foreach (var column in this.tableColumns.Values)
				{
					if (column.Visible)
					{
						table.SetRelativeColumWidth (column.Rank, this.table.GetRelativeColumnWidth (column.Rank));
					}
				}

				table.SetText (this.tableColumns["Desc"].Rank, 0, "Reporté");

				decimal sumPT, sumTva, sumTot;
				this.ComputeBottomReports (page, out sumPT, out sumTva, out sumTot);
				table.SetText (this.tableColumns["PT" ].Rank, 0, Misc.PriceToString (sumPT));
				table.SetText (this.tableColumns["TVA"].Rank, 0, Misc.PriceToString (sumTva));
				table.SetText (this.tableColumns["Tot"].Rank, 0, Misc.PriceToString (sumTot));

				this.InitializeRowAlignment (table, 0);

				var tableBound = this.tableBounds[page];
				double h = table.RequiredHeight (width);
				var bounds = new Rectangle (tableBound.Left, tableBound.Bottom-h, width, h);

				this.documentContainer.AddAbsolute (table, bounds);

				// Met un trait horizontal sur le report.
				this.BuildSeparator (bounds.Top);
			}
		}

		private void ComputeBottomReports(int page, out decimal sumPT, out decimal sumTva, out decimal sumTot)
		{
			//	Calcul les reports à montrer en bas d'une page, ou en haut de la suivante.
			sumPT  = 0;
			sumTva = 0;
			sumTot = 0;

			int lastRow = this.lastRowForEachSection[page];

			for (int row = 0; row <= lastRow; row++)
			{
				if (row == 0)  // en-tête ?
				{
					continue;
				}

				AbstractDocumentItemEntity item = this.entity.Lines[row-1];  // -1 à cause de l'en-tête

				if (item is ArticleDocumentItemEntity)
				{
					var article = item as ArticleDocumentItemEntity;

					decimal beforeTax = article.ResultingLinePriceBeforeTax.GetValueOrDefault (0);
					decimal tax =       article.ResultingLineTax           .GetValueOrDefault (0);

					sumPT  += beforeTax;
					sumTva += tax;
					sumTot += beforeTax+tax;
				}
			}
		}


		private void BuildBvs()
		{
			//	Met un BVR orangé ou un BV rose en bas de chaque page.
			var bounds = new Rectangle (Point.Zero, AbstractBvBand.DefautlSize);

			for (int page = 0; page < this.documentContainer.PageCount; page++)
			{
				this.documentContainer.CurrentPage = page;

				AbstractBvBand BV;

				if (this.HasDocumentOption ("BVR"))
				{
					BV = new BvrBand ();  // BVR orangé
				}
				else
				{
					BV = new BvBand ();  // BV rose
				}

				BV.PaintBvSimulator = this.HasDocumentOption ("BV.Simul");
				BV.PaintSpecimen    = this.HasDocumentOption ("BV.Specimen");
				BV.From = InvoiceDocumentHelper.GetMailContact (this.entity);
				BV.To = "EPSITEC SA<br/>1400 Yverdon-les-Bains";
				BV.Communication = InvoiceDocumentHelper.GetTitle (this.entity, this.IsBL);

				if (page == this.documentContainer.PageCount-1)  // dernière page ?
				{
					BV.NotForUse = false;  // c'est LE vrai BV
					BV.Price = InvoiceDocumentHelper.GetAmontDue (this.entity);

					if (this.entity.BillingDetails.Count > 0)
					{
						BV.EsrCustomerNumber  = this.entity.BillingDetails[0].EsrCustomerNumber;
						BV.EsrReferenceNumber = this.entity.BillingDetails[0].EsrReferenceNumber;
					}
				}
				else  // faux BV ?
				{
					BV.NotForUse = true;  // pour imprimer "XXXXX XX"
				}

				this.documentContainer.AddAbsolute (BV, bounds);
			}
		}


		private void BuildSeparator(double y, double width=0.5)
		{
			//	Met un séparateur horizontal.
			var line = new SurfaceBand ()
			{
				Height = width,
			};

			var bounds = new Rectangle (this.PageMargins.Left, y-width/2, this.PageSize.Width-this.PageMargins.Left-this.PageMargins.Right, line.Height);
			this.documentContainer.AddAbsolute (line, bounds);
		}


		private bool IsPort(ArticleDocumentItemEntity article)
		{
			//	Retourne true s'il s'agit des frais de port.
			if (article.ArticleDefinition.IsActive () &&
				article.ArticleDefinition.ArticleCategory.IsActive ())
			{
				return article.ArticleDefinition.ArticleCategory.Name == "Ports/emballages";
			}

			return false;
		}

		private bool IsBL
		{
			get
			{
				return this.DocumentTypeSelected == "BL";
			}
		}

		private bool IsColumnsOrderQD
		{
			get
			{
				return this.HasDocumentOption ("ColumnsOrderQD");
			}
		}

		private double CellMargin
		{
			get
			{
				return this.IsWithFrame ? 1 : 2;
			}
		}

		private bool IsWithFrame
		{
			get
			{
				return this.HasDocumentOption ("WithFrame");
			}
		}


		private static readonly Font font = Font.GetFont ("Arial", "Regular");
		private static readonly double fontSize = 3.0;
		private static readonly double reportHeight = 7.0;

		private TableBand			table;
		private int					visibleColumnCount;
		private int[]				lastRowForEachSection;
		private List<Rectangle>		tableBounds;
	}
}

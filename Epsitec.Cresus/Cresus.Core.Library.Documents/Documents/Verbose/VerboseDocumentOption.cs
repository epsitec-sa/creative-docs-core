﻿//	Copyright © 2010, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Daniel ROUX, Maintainer: Daniel ROUX

using Epsitec.Common.Types;

using Epsitec.Cresus.Core.Library;
using Epsitec.Cresus.Core.Business;

using System.Collections.Generic;
using System.Linq;

namespace Epsitec.Cresus.Core.Documents.Verbose
{
	public class VerboseDocumentOption
	{
		static VerboseDocumentOption()
		{
			VerboseDocumentOption.BuildAll ();
		}


		private VerboseDocumentOption(string title, string group)
		{
			this.Title = title;
			this.Group = group;
		}

		private VerboseDocumentOption(DocumentOption option, string group, bool isGlobal, DocumentOptionValueType type, string description, string defaultValue, params Business.DocumentType[] documentTypes)
		{
			this.Option        = option;
			this.Group         = group;
			this.IsGlobal      = isGlobal;
			this.Type          = type;
			this.Description   = description;
			this.DefaultValue  = defaultValue;
			this.DocumentTypes = documentTypes;
		}

		private VerboseDocumentOption(DocumentOption option, string group, bool isGlobal, IEnumerable<string> enumeration, IEnumerable<string> enumerationDescription, int defaultIndex, params Business.DocumentType[] documentTypes)
		{
			System.Diagnostics.Debug.Assert (enumeration != null && enumerationDescription != null);
			System.Diagnostics.Debug.Assert (enumeration.Count () == enumerationDescription.Count ());

			this.Option                 = option;
			this.Group                  = group;
			this.IsGlobal               = isGlobal;
			this.Type                   = DocumentOptionValueType.Enumeration;
			this.Enumeration            = enumeration;
			this.EnumerationDescription = enumerationDescription;
			this.DefaultValue           = enumeration.ElementAt (defaultIndex);
			this.DocumentTypes          = documentTypes;
		}

		public string Title
		{
			get;
			private set;
		}

		public DocumentOption Option
		{
			get;
			private set;
		}

		public DocumentOptionValueType Type
		{
			get;
			private set;
		}

		public IEnumerable<string> Enumeration
		{
			get;
			private set;
		}

		public string Group
		{
			get;
			private set;
		}

		public string Description
		{
			get;
			private set;
		}

		public IEnumerable<string> EnumerationDescription
		{
			get;
			private set;
		}

		public string DefaultValue
		{
			get;
			private set;
		}

		public bool IsGlobal
		{
			get;
			private set;
		}

		public IEnumerable<Business.DocumentType> DocumentTypes
		{
			get;
			private set;
		}

		public bool IsTitle
		{
			get
			{
				return !string.IsNullOrEmpty (this.Title);
			}
		}

		public string DocumentTypeDescription
		{
			get
			{
				var types = EnumKeyValues.FromEnum<DocumentType> ();
				var strings = new List<string> ();

				foreach (Business.DocumentType type in this.DocumentTypes)
				{
					var t = types.Where (x => x.Key == type).FirstOrDefault ();

					if (t != null)
					{
						strings.Add (string.Concat ("● ", t.Values[0]));
					}
				}

				if (strings.Count == 0)
				{
					strings.Add (string.Concat ("● ", "Tous"));
				}

				return string.Join ("<br/>", strings);
			}
		}


		public static IEnumerable<VerboseDocumentOption> GetAll()
		{
			//	Retourne toutes les options existantes.
			return VerboseDocumentOption.allOptions;
		}

		public static IEnumerable<VerboseDocumentOption> GetDefault()
		{
			//	Retourne les options globales, utilisées avec le réglage des unités d'impression (PrintingUnitsTabPage).
			return VerboseDocumentOption.allOptions.Where (x => x.IsGlobal);
		}


		private static void BuildAll()
		{
			var list = new List<VerboseDocumentOption> ();
			string[] e, d;

			//	Ajoute les options d'impression liées à l'orientation portrait/paysage.
			list.Add (new VerboseDocumentOption ("Orientation du papier", "Orientation"));
			e = new string[] { "Portrait", "Landscape" };
			d = new string[] { "Portrait", "Paysage" };
			list.Add (new VerboseDocumentOption (DocumentOption.Orientation, "Orientation", true, e, d, 0));

			//	Ajoute les options d'impression générales.
			list.Add (new VerboseDocumentOption ("Options générales", "Global"));
			list.Add (new VerboseDocumentOption (DocumentOption.HeaderLogo, "Global", true, DocumentOptionValueType.Boolean, "Imprime le logo de l'entreprise",    "true"));
			list.Add (new VerboseDocumentOption (DocumentOption.Specimen,   "Global", true, DocumentOptionValueType.Boolean, "Incruste la mention SPECIMEN",       "false"));
			list.Add (new VerboseDocumentOption (DocumentOption.Signing,    "Global", true, DocumentOptionValueType.Boolean, "Cartouche pour visa avec signature", "true", Business.DocumentType.OrderBooking, Business.DocumentType.OrderConfirmation, Business.DocumentType.ProductionOrder, Business.DocumentType.ProductionChecklist, Business.DocumentType.ShipmentChecklist, Business.DocumentType.DeliveryNote, Business.DocumentType.Receipt));

			list.Add (new VerboseDocumentOption (DocumentOption.FontSize, "Global", true, DocumentOptionValueType.Size, "Taille de la police", "3"));

			list.Add (new VerboseDocumentOption (DocumentOption.LeftMargin,   "Global", true, DocumentOptionValueType.Distance, "Marge gauche",     "20"));
			list.Add (new VerboseDocumentOption (DocumentOption.RightMargin,  "Global", true, DocumentOptionValueType.Distance, "Marge droite",     "20"));
			list.Add (new VerboseDocumentOption (DocumentOption.TopMargin,    "Global", true, DocumentOptionValueType.Distance, "Marge supérieure", "20"));
			list.Add (new VerboseDocumentOption (DocumentOption.BottomMargin, "Global", true, DocumentOptionValueType.Distance, "Marge inférieure", "20"));

			list.Add (new VerboseDocumentOption ("Aspect des listes", "LayoutFrame"));
			e = new string[] { "Frameless", "WithLine", "WithFrame" };
			d = new string[] { "Espacé, sans encadrements", "Espacé, avec des lignes de séparation", "Serré, avec des encadrements" };
			list.Add (new VerboseDocumentOption (DocumentOption.LayoutFrame, "LayoutFrame", true, e, d, 1));

			list.Add (new VerboseDocumentOption (DocumentOption.GapBeforeGroup, "LayoutFrame", true, DocumentOptionValueType.Boolean, "Interligne supplémentaire entre les groupes", "true"));

			list.Add (new VerboseDocumentOption ("Numérotation", "LineNumber"));
			e = new string[] { "None", "Group", "Line", "Full" };
			d = new string[] { "Aucune", "Numérotation des groupes", "Numérotation des lignes", "Numérotation des groupes et des lignes" };
			list.Add (new VerboseDocumentOption (DocumentOption.LineNumber, "LineNumber", true, e, d, 0));

			//	Ajoute les options d'impression liées aux factures.
			list.Add (new VerboseDocumentOption ("Options pour les factures", "InvoiceOption"));
			list.Add (new VerboseDocumentOption (DocumentOption.ArticleDelayed, "InvoiceOption", false, DocumentOptionValueType.Boolean, "Imprime les articles livrés ultérieurement", "true", Business.DocumentType.Invoice));
			list.Add (new VerboseDocumentOption (DocumentOption.ArticleId,      "InvoiceOption", false, DocumentOptionValueType.Boolean, "Imprime les identificateurs d'article",      "false", Business.DocumentType.Invoice));

			list.Add (new VerboseDocumentOption ("Ordre des colonnes", "ColumnsOrder"));
			e = new string[] { "QD", "DQ" };
			d = new string[] { "Quantité, Désignation, Prix", "Désignation, Quantité, Prix" };
			list.Add (new VerboseDocumentOption (DocumentOption.ColumnsOrder, "ColumnsOrder", false, e, d, 0, Business.DocumentType.Invoice));

			//	Ajoute les options d'impression liées aux BV.
			list.Add (new VerboseDocumentOption ("Type de la facture", "IsrPosition"));
			e = new string[] { "Without", "WithInside", "WithOutside" };
			d = new string[] { "Facture sans BV", "Facture avec BV intégré", "Facture avec BV séparé" };
			list.Add (new VerboseDocumentOption (DocumentOption.IsrPosition, "IsrPosition", false, e, d, 0, Business.DocumentType.Invoice));

			list.Add (new VerboseDocumentOption ("Type de bulletin de versement", "IsrType"));
			e = new string[] { "Isr", "Is" };
			d = new string[] { "BV orange", "BV rose" };
			list.Add (new VerboseDocumentOption (DocumentOption.IsrType, "IsrType", false, e, d, 0, Business.DocumentType.Invoice));

			list.Add (new VerboseDocumentOption ("Mode d'impression du BV", "InvoiceIsrMode"));
			list.Add (new VerboseDocumentOption (DocumentOption.IsrFacsimile, "InvoiceIsrMode", true, DocumentOptionValueType.Boolean, "Fac-similé complet du BV", "true", Business.DocumentType.Invoice));

			//	Ajoute les options pour les clients.
			list.Add (new VerboseDocumentOption ("Données du client à inclure", "Relation"));
			list.Add (new VerboseDocumentOption (DocumentOption.RelationMail,    "Relation", false, DocumentOptionValueType.Boolean,  "Adresses",   "true", Business.DocumentType.RelationSummary));
			list.Add (new VerboseDocumentOption (DocumentOption.RelationTelecom, "Relation", false, DocumentOptionValueType.Boolean,  "Téléphones", "true", Business.DocumentType.RelationSummary));
			list.Add (new VerboseDocumentOption (DocumentOption.RelationUri,     "Relation", false, DocumentOptionValueType.Boolean,  "Emails",     "true", Business.DocumentType.RelationSummary));

			VerboseDocumentOption.allOptions = list;
		}


		private static IEnumerable<VerboseDocumentOption> allOptions;
	}
}

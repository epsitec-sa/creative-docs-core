﻿//	Copyright © 2010, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Daniel ROUX, Maintainer: Daniel ROUX

using Epsitec.Cresus.Core.Controllers;

using System.Collections.Generic;
using System.Linq;

namespace Epsitec.Cresus.Core.DocumentOptionsEditor
{
	public class DocumentOption
	{
		public DocumentOption(string title, string group)
		{
			this.Title = title;
			this.Group = group;
		}

		public DocumentOption(string name, DocumentOptionType type, DocumentOptionWidget widget, string group, string description, string defaultValue, params Business.DocumentType[] documentTypes)
		{
			this.Name          = name;
			this.Type          = type;
			this.Widget        = widget;
			this.Group         = group;
			this.Description   = description;
			this.DefaultValue  = defaultValue;
			this.documentTypes = documentTypes.ToList ();
		}

		public string Title
		{
			get;
			private set;
		}

		public string Name
		{
			get;
			private set;
		}

		public DocumentOptionType Type
		{
			get;
			private set;
		}

		public DocumentOptionWidget Widget
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

		public string DefaultValue
		{
			get;
			private set;
		}

		public List<Business.DocumentType> DocumentTypes
		{
			get
			{
				return this.documentTypes;
			}
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
				var types = Business.Enumerations.GetAllPossibleDocumentType ();
				var strings = new List<string> ();

				foreach (Business.DocumentType type in this.documentTypes)
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


		public static IEnumerable<DocumentOption> GetAllDocumentOptions()
		{
			var list = new List<DocumentOption> ();

			//	Ajoute les options d'impression liées à l'orientation portrait/paysage.
			list.Add (new DocumentOption ("Orientation du papier :", "Orientation"));
			list.Add (new DocumentOption ("OrientationVertical",   DocumentOptionType.Boolean, DocumentOptionWidget.RadioButton, "Orientation", "Portrait", "true"));
			list.Add (new DocumentOption ("OrientationHorizontal", DocumentOptionType.Boolean, DocumentOptionWidget.RadioButton, "Orientation", "Paysage",  "false"));

			//	Ajoute les options d'impression générales.
			list.Add (new DocumentOption ("Options générales :", "Global"));
			list.Add (new DocumentOption ("HeaderLogo", DocumentOptionType.Boolean, DocumentOptionWidget.CheckButton, "Global", "Imprime le logo de l'entreprise", "true"));
			list.Add (new DocumentOption ("Specimen",   DocumentOptionType.Boolean, DocumentOptionWidget.CheckButton, "Global", "Incruste la mention SPECIMEN",    "false"));

			list.Add (new DocumentOption ("Aspect des listes :", "TableAspect"));
			list.Add (new DocumentOption ("LayoutFrameless", DocumentOptionType.Boolean, DocumentOptionWidget.RadioButton, "TableAspect", "Espacé, sans encadrements",             "false"));
			list.Add (new DocumentOption ("LayoutWithLine",  DocumentOptionType.Boolean, DocumentOptionWidget.RadioButton, "TableAspect", "Espacé, avec des lignes de séparation", "true"));
			list.Add (new DocumentOption ("LayoutWithFrame", DocumentOptionType.Boolean, DocumentOptionWidget.RadioButton, "TableAspect", "Serré, avec des encadrements",          "false"));

			//	Ajoute les options d'impression liées aux factures.
			list.Add (new DocumentOption ("Options pour les factures :", "InvoiceOption"));
			list.Add (new DocumentOption ("ArticleDelayed", DocumentOptionType.Boolean, DocumentOptionWidget.CheckButton, "InvoiceOption", "Imprime les articles livrés ultérieurement", "true",  Business.DocumentType.Invoice));
			list.Add (new DocumentOption ("ArticleId",      DocumentOptionType.Boolean, DocumentOptionWidget.CheckButton, "InvoiceOption", "Imprime les identificateurs d'article",      "false", Business.DocumentType.Invoice));

			list.Add (new DocumentOption ("Ordre des colonnes :", "InvoiceColumnsOrder"));
			list.Add (new DocumentOption ("ColumnsOrderQD", DocumentOptionType.Boolean, DocumentOptionWidget.RadioButton, "InvoiceColumnsOrder", "Quantité, Désignation, Prix", "true", Business.DocumentType.Invoice));
			list.Add (new DocumentOption ("ColumnsOrderDQ", DocumentOptionType.Boolean, DocumentOptionWidget.RadioButton, "InvoiceColumnsOrder", "Désignation, Quantité, Prix", "false", Business.DocumentType.Invoice));

			//	Ajoute les options d'impression liées aux BV.
			list.Add (new DocumentOption ("Type de la facture :", "InvoiceESR"));
			list.Add (new DocumentOption ("InvoiceWithInsideESR",  DocumentOptionType.Boolean, DocumentOptionWidget.RadioButton, "InvoiceESR", "Facture avec BV intégré", "false", Business.DocumentType.Invoice));
			list.Add (new DocumentOption ("InvoiceWithOutsideESR", DocumentOptionType.Boolean, DocumentOptionWidget.RadioButton, "InvoiceESR", "Facture avec BV séparé",  "false", Business.DocumentType.Invoice));
			list.Add (new DocumentOption ("InvoiceWithoutESR",     DocumentOptionType.Boolean, DocumentOptionWidget.RadioButton, "InvoiceESR", "Facture sans BV",         "true",  Business.DocumentType.Invoice));

			list.Add (new DocumentOption ("Type de bulletin de versement :", "InvoiceESRType"));
			list.Add (new DocumentOption ("InvoiceWithESR", DocumentOptionType.Boolean, DocumentOptionWidget.RadioButton, "InvoiceESRType", "BVR orange", "true",  Business.DocumentType.Invoice));
			list.Add (new DocumentOption ("InvoiceWithES",  DocumentOptionType.Boolean, DocumentOptionWidget.RadioButton, "InvoiceESRType", "BV rose",    "false", Business.DocumentType.Invoice));

			list.Add (new DocumentOption ("Mode d'impression du BV :", "InvoiceESRMode"));
			list.Add (new DocumentOption ("ESRFacsimile", DocumentOptionType.Boolean, DocumentOptionWidget.CheckButton, "InvoiceESRMode", "Fac-similé complet du BV", "true", Business.DocumentType.Invoice));

			list.Add (new DocumentOption ("Signing", DocumentOptionType.Boolean, DocumentOptionWidget.CheckButton, null, "Cartouche", "true", Business.DocumentType.OrderBooking, Business.DocumentType.OrderConfirmation, Business.DocumentType.ProductionOrder, Business.DocumentType.ProductionChecklist, Business.DocumentType.ShipmentChecklist, Business.DocumentType.DeliveryNote, Business.DocumentType.Receipt));

			//	Ajoute les options pour les clients.
			list.Add (new DocumentOption ("Données du client à inclure :", "Relation"));
			list.Add (new DocumentOption ("RelationMail",    DocumentOptionType.Boolean, DocumentOptionWidget.CheckButton, "Relation", "Adresses",   "true", Business.DocumentType.Summary));
			list.Add (new DocumentOption ("RelationTelecom", DocumentOptionType.Boolean, DocumentOptionWidget.CheckButton, "Relation", "Téléphones", "true", Business.DocumentType.Summary));
			list.Add (new DocumentOption ("RelationUri",     DocumentOptionType.Boolean, DocumentOptionWidget.CheckButton, "Relation", "Emails",     "true", Business.DocumentType.Summary));

			return list;
		}


		private readonly List<Business.DocumentType> documentTypes;
	}
}

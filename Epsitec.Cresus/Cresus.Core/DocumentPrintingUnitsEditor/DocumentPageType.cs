﻿//	Copyright © 2010, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Daniel ROUX, Maintainer: Daniel ROUX

using Epsitec.Cresus.Core.Controllers;

using System.Collections.Generic;
using System.Linq;

namespace Epsitec.Cresus.Core.DocumentPrintingUnitsEditor
{
	public class DocumentPageType
	{
		public DocumentPageType(string name, string job, string description, params Business.DocumentType[] documentTypes)
		{
			this.Name          = name;
			this.Job           = job;
			this.Description   = description;
			this.documentTypes = documentTypes.ToList ();
		}

		public string Job
		{
			get;
			private set;
		}

		public string Name
		{
			get;
			private set;
		}

		public string Description
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

		public string JobNiceDescription
		{
			get
			{
				switch (this.Job)
				{
					case "All":
						return "Ensemble des pages";

					case "Copy":
						return "Copie des pages";

					case "Spec":
						return "Spécifique";

					case "Label":
						return "Etiquettes";

					default:
						return this.Job;
				}
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


		public static IEnumerable<DocumentPageType> GetAllDocumentPageTypes()
		{
			var list = new List<DocumentPageType> ();

			//	Ajoute les unités d'impression de base, qui devraient toujours exister.
			list.Add (new DocumentPageType ("ForAllPages",       "All",   "Pour l'ensemble des pages"));
			list.Add (new DocumentPageType ("ForPagesCopy",      "Copy",  "Pour une copie de l'ensemble des pages"));
			list.Add (new DocumentPageType ("ForSinglePage",     "Spec",  "Pour une page unique"));
			list.Add (new DocumentPageType ("ForFirstPage",      "Spec",  "Pour la première page"));
			list.Add (new DocumentPageType ("ForFollowingPages", "Spec",  "Pour les pages suivantes"));

			//	Ajoute l'unité d'impression spécifique pour les BV.
			list.Add (new DocumentPageType ("ForEsrPage",        "Spec",  "Pour le BV", Business.DocumentType.Invoice));

			//	Ajoute l'unité d'impression spécifique pour les étiquettes.
			list.Add (new DocumentPageType ("ForLabelPage",      "Label", "Pour l'étiquette"));

			return list;
		}


		private readonly List<Business.DocumentType> documentTypes;
	}
}

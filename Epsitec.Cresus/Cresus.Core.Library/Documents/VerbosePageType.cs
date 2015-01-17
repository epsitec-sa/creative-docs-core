﻿//	Copyright © 2010, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Daniel ROUX, Maintainer: Daniel ROUX

using Epsitec.Common.Types;

using Epsitec.Cresus.Core.Business;
using Epsitec.Cresus.Core.Documents;
using Epsitec.Cresus.Core.Library;

using System.Collections.Generic;
using System.Linq;

namespace Epsitec.Cresus.Core.Documents.Verbose
{
	public class VerbosePageType
	{
		static VerbosePageType()
		{
			VerbosePageType.BuildAll ();
		}


		private VerbosePageType(PageType type, string job, string shortDescription, string longDescription, params Business.DocumentType[] documentTypes)
		{
			this.Type             = type;
			this.Job              = job;
			this.ShortDescription = shortDescription;
			this.LongDescription  = longDescription;
			this.DocumentTypes    = documentTypes;
		}

		public string Job
		{
			get;
			private set;
		}

		public PageType Type
		{
			get;
			private set;
		}

		public string ShortDescription
		{
			get;
			private set;
		}

		public string LongDescription
		{
			get;
			private set;
		}

		public IEnumerable<Business.DocumentType> DocumentTypes
		{
			get;
			private set;
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


		public static IEnumerable<VerbosePageType> GetAll()
		{
			return VerbosePageType.allPageTypes;
		}

		private static void BuildAll()
		{
			var list = new List<VerbosePageType> ();

			//	Ajoute les unités d'impression de base, qui devraient toujours exister.
			list.Add (new VerbosePageType (PageType.All,       "All",   "Ensemble des pages", "Pour l'ensemble des pages"));
			list.Add (new VerbosePageType (PageType.Copy,      "Copy",  "Copie des pages",    "Pour une copie de l'ensemble des pages"));
			list.Add (new VerbosePageType (PageType.Single,    "Spec",  "Page unique",        "Pour une page unique"));
			list.Add (new VerbosePageType (PageType.First,     "Spec",  "Première page",      "Pour la première page"));
			list.Add (new VerbosePageType (PageType.Following, "Spec",  "Pages suivantes",    "Pour les pages suivantes"));

			//	Ajoute l'unité d'impression spécifique pour les BV.
			list.Add (new VerbosePageType (PageType.Isr, "Spec", "BV seul", "Pour le BV seul", Business.DocumentType.Invoice));

			//	Ajoute l'unité d'impression spécifique pour les étiquettes.
			list.Add (new VerbosePageType (PageType.Label, "Label", "Etiquette", "Pour l'étiquette"));

			VerbosePageType.allPageTypes = list;
		}


		private class TextFormatterConverter : TextFormatterConverter<PageType>
		{
			protected override FormattedText ToFormattedText(PageType value, System.Globalization.CultureInfo culture, TextFormatterDetailLevel detailLevel)
			{
				var verbose = VerbosePageType.GetAll ().FirstOrDefault (x => x.Type == value);

				if (verbose == null)
				{
					return FormattedText.Empty;
				}
				else
				{
					if (detailLevel == TextFormatterDetailLevel.Full)
					{
						return verbose.LongDescription;
					}
					else
					{
						return verbose.ShortDescription;
					}
				}
			}
		}


		private static List<VerbosePageType> allPageTypes;
	}
}

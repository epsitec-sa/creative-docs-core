﻿//	Copyright © 2013, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Daniel ROUX, Maintainer: Daniel ROUX

using System.Collections.Generic;
using System.Linq;

namespace Epsitec.Cresus.Assets.App.Views
{
	public static class StaticDescriptions
	{
		public static string GetViewTypeIcon(ViewType viewType)
		{
			switch (viewType)
			{
				case ViewType.Assets:
					return "View.Assets";

				case ViewType.Amortizations:
					return "View.Amortizations";

				case ViewType.Ecritures:
					return "View.Ecritures";

				case ViewType.Categories:
					return "View.Categories";

				case ViewType.Groups:
					return "View.Groups";

				case ViewType.Persons:
					return "View.Persons";

				case ViewType.Reports:
					return "View.Reports";

				case ViewType.AssetsSettings:
					return "View.AssetsSettings";

				case ViewType.PersonsSettings:
					return "View.PersonsSettings";

				case ViewType.AccountsSettings:
					return "View.AccountsSettings";

				default:
					return null;
			}
		}

		public static string GetViewTypeDescription(ViewType viewType)
		{
			switch (viewType)
			{
				case ViewType.Assets:
					return "Objets d'immobilisation";

				case ViewType.Amortizations:
					return "Amortissements";

				case ViewType.Ecritures:
					return "Ecritures comptables";

				case ViewType.Categories:
					return "Catégories d'immobilisations";

				case ViewType.Groups:
					return "Groupes";

				case ViewType.Persons:
					return "Personnes";

				case ViewType.Reports:
					return "Rapports et statistiques";

				case ViewType.AssetsSettings:
					//?return "Réglages — Champs des objets d'immobilisations";
					return "Champs des objets d'immobilisations";

				case ViewType.PersonsSettings:
					//?return "Réglages — Champs des personnes";
					return "Champs des personnes";

				case ViewType.AccountsSettings:
					//?return "Réglages — Plan comptable";
					return "Plan comptable";

				default:
					return null;
			}
		}

		public static string GetObjectPageDescription(PageType type)
		{
			switch (type)
			{
				case PageType.OneShot:
					return "Evénement";

				case PageType.Summary:
					return "Résumé";

				case PageType.Asset:
					return "Général";

				case PageType.Persons:
					return "Personnes";

				case PageType.Values:
					return "Valeurs";

				case PageType.Amortization:
					return "Amortissement";

				case PageType.AmortizationPreview:
					return "Calcul de l'amortissement";

				case PageType.Groups:
					return "Regroupements";

				case PageType.Category:
					return "Définitions de la catégorie";

				case PageType.Group:
					return "Définitions du groupe";

				case PageType.Person:
					return "Définitions de la personne";

				case PageType.UserFields:
					return "Définitions du champ";

				case PageType.Account:
					return "Définitions du compte";

				default:
					return null;
			}
		}
	}
}

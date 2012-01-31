﻿//	Copyright © 2010, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Daniel ROUX, Maintainer: Daniel ROUX

using Epsitec.Common.Types;

using Epsitec.Cresus.Core;
using Epsitec.Cresus.Core.Entities;

using System.Collections.Generic;
using System.Linq;

namespace Epsitec.Cresus.Compta.Accessors
{
	public class SearchData : ISettingsData
	{
		public SearchData()
		{
			this.tabsData = new List<SearchTabData> ();
			this.Clear ();
		}


		public void Clear()
		{
			//	Vide les données et prépare une unique ligne.
			this.tabsData.Clear ();
			this.Adjust ();

			this.OrMode = true;
		}

		public void Adjust()
		{
			//	Adapte les données pour avoir une ligne au minimum.
			if (!this.tabsData.Any ())
			{
				this.tabsData.Add (new SearchTabData ());
			}
		}


		public bool Specialist
		{
			//	false -> mode débutant
			//	true  -> mode spécialiste
			get;
			set;
		}


		public List<SearchTabData> TabsData
		{
			//	Retourne toutes les lignes de données.
			get
			{
				return this.tabsData;
			}
		}

		public bool OrMode
		{
			//	false -> mode "and"
			//	true  -> mode "or"
			get;
			set;
		}

		public bool IsEmpty
		{
			get
			{
				foreach (var tab in this.tabsData)
				{
					if (!tab.IsEmpty)
					{
						return false;
					}
				}

				return true;
			}
		}


		public string BeginnerSearch
		{
			//	Texte unique de recherche en mode débutant.
			get
			{
				return this.tabsData[0].SearchText.FromText;
			}
			set
			{
				this.tabsData[0].SearchText.FromText = value;
				this.BeginnerAdjust (false);
			}
		}


		public CatégorieDeCompte BeginnerCatégories
		{
			//	Catégories à filtrer en mode débutant.
			get
			{
				var data = this.BeginnerCatégoriesData;

				if (data == null)
				{
					return CatégorieDeCompte.Tous;
				}
				else
				{
					return Misc.StringToCatégories (data.SearchText.FromText);
				}
			}
			set
			{
				var data = this.BeginnerCatégoriesData;

				if (value == CatégorieDeCompte.Tous || value == CatégorieDeCompte.Inconnu)
				{
					if (data != null)
					{
						this.tabsData.Remove (data);
						this.Adjust ();
					}
				}
				else
				{
					if (data == null)
					{
						//	Si on n'a trouvé aucune catégorie, on en crée une.
						data = new SearchTabData ();
						this.tabsData.Add (data);
					}

					data.Column              = ColumnType.Catégorie;
					data.SearchText.Mode     = SearchMode.Jokers;
					data.SearchText.FromText = Misc.CatégoriesToString (value);

					this.BeginnerAdjust (true);
				}
			}
		}

		private SearchTabData BeginnerCatégoriesData
		{
			get
			{
				return this.tabsData.Where (x => x.Column == ColumnType.Catégorie).FirstOrDefault ();
			}
		}


		public void GetBeginnerDates(out Date? beginDate, out Date? endDate)
		{
			//	Retourne les dates à filtrer en mode débutant.
			var data = this.BeginnerDatesData;

			if (data == null)
			{
				beginDate = null;
				endDate   = null;
			}
			else
			{
				data.SearchText.GetIntervalDates (out beginDate, out endDate);
			}
		}

		public void SetBeginnerDates(Date? beginDate, Date? endDate)
		{
			//	Modifie les dates à filtrer en mode débutant.
			var data = this.BeginnerDatesData;

			if (beginDate == null && endDate == null)
			{
				if (data != null)
				{
					this.tabsData.Remove (data);
					this.Adjust ();
				}
			}
			else
			{
				if (data == null)
				{
					//	Si on n'a trouvé aucune date, on en crée une.
					data = new SearchTabData ();
					this.tabsData.Add (data);
				}

				data.Column              = ColumnType.Date;
				data.SearchText.Mode     = SearchMode.Interval;
				data.SearchText.FromText = Misc.DateToString (beginDate);
				data.SearchText.ToText   = Misc.DateToString (endDate);

				this.BeginnerAdjust (true);
			}
		}

		private SearchTabData BeginnerDatesData
		{
			get
			{
				foreach (var data in this.tabsData)
				{
					Date? beginDate, endDate;
					if (data.SearchText.GetIntervalDates (out beginDate, out endDate))
					{
						return data;
					}
				}

				//	Si on n'a pas trouvé un intervalle de dates, on prend n'importe lequel.
				foreach (var data in this.tabsData)
				{
					if (data.SearchText.Mode == SearchMode.Interval)
					{
						return data;
					}
				}

				return null;
			}
		}


		public void BeginnerAdjust(bool isFilter)
		{
			//	Ajuste les données après une modification en mode débutant.
			//	Il faut supprimer les données surnuméraires, afin d'obtenir un résultat
			//	conforme à ce qui est visible.
			if (isFilter)  // filtre ?
			{
				//	1) Cherche les données effectives.
				var dataCatégories = this.BeginnerCatégoriesData;
				var dataDates      = this.BeginnerDatesData;

				//	2) Supprime toutes les données-
				this.tabsData.Clear ();

				//	3) Puis remet les données effectives, dans le bon ordre.
				if (dataCatégories != null)
				{
					this.tabsData.Add (dataCatégories);
				}

				if (dataDates != null)
				{
					this.tabsData.Add (dataDates);
				}

				//	4) Met au moins une ligne s'il n'y a plus rien.
				this.Adjust ();

				//	Si plusieurs lignes sont utilisées, il faut mettre le mode "and".
				this.OrMode = false;
			}
			else  // recherche ?
			{
				//	Ne conserve que la première ligne.
				while (this.tabsData.Count > 1)
				{
					this.tabsData.RemoveAt (1);
				}
			}
		}


		public static bool DateInRange(Date? date, Date? beginDate, Date? endDate)
		{
			if (date.HasValue)
			{
				if (beginDate.HasValue && date.Value < beginDate.Value)
				{
					return false;
				}

				if (endDate.HasValue && date.Value > endDate.Value)
				{
					return false;
				}
			}

			return true;
		}


		private readonly List<SearchTabData>		tabsData;
	}
}
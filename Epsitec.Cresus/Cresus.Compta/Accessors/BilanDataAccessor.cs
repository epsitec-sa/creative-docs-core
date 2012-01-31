﻿//	Copyright © 2010, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Daniel ROUX, Maintainer: Daniel ROUX

using Epsitec.Common.Types;

using Epsitec.Cresus.Core;
using Epsitec.Cresus.Core.Entities;
using Epsitec.Cresus.Core.Business;

using Epsitec.Cresus.Compta.Controllers;
using Epsitec.Cresus.Compta.Entities;

using System.Collections.Generic;
using System.Linq;

namespace Epsitec.Cresus.Compta.Accessors
{
	/// <summary>
	/// Gère l'accès aux données du bilan de la comptabilité.
	/// </summary>
	public class BilanDataAccessor : AbstractDataAccessor
	{
		public BilanDataAccessor(BusinessContext businessContext, ComptaEntity comptaEntity, List<ColumnMapper> columnMappers, MainWindowController mainWindowController)
			: base (businessContext, comptaEntity, columnMappers, mainWindowController)
		{
			this.options    = this.mainWindowController.GetSettingsOptions<BilanOptions> ("Présentation.Bilan.Options", this.comptaEntity);
			this.searchData = this.mainWindowController.GetSettingsSearchData<SearchData> ("Présentation.Bilan.Search");
			this.filterData = this.mainWindowController.GetSettingsSearchData<SearchData> ("Présentation.Bilan.Filter");

			this.UpdateAfterOptionsChanged ();
		}


		public override void FilterUpdate()
		{
			Date? beginDate, endDate;
			this.filterData.GetBeginnerDates (out beginDate, out endDate);

			if (this.lastBeginDate != beginDate || this.lastEndDate != endDate)
			{
				this.UpdateAfterOptionsChanged ();
			}

			base.FilterUpdate ();
		}

		public override void UpdateAfterOptionsChanged()
		{
			this.readonlyAllData.Clear ();
			this.MinMaxClear ();

			decimal totalGauche = 0;
			decimal totalDroite = 0;

			this.filterData.GetBeginnerDates (out this.lastBeginDate, out this.lastEndDate);
			this.comptaEntity.PlanComptableUpdate (this.lastBeginDate, this.lastEndDate);

			foreach (var compte in this.comptaEntity.PlanComptable.Where (x => x.Catégorie == CatégorieDeCompte.Actif))
			{
				if (this.Options.Profondeur.HasValue && compte.Niveau >= this.Options.Profondeur.Value)
				{
					continue;
				}

				var solde = this.comptaEntity.GetSoldeCompte (compte);

				if (!this.Options.ComptesNuls && solde.GetValueOrDefault () == 0)
				{
					continue;
				}

				var data = new BilanData ();
				this.readonlyAllData.Add (data);

				data.NuméroGauche = compte.Numéro;
				data.TitreGauche  = compte.Titre;
				data.NiveauGauche = compte.Niveau;

				if (this.HasSolde (compte))
				{
					data.SoldeGauche = solde;
					totalGauche += solde.GetValueOrDefault ();
					this.SetMinMaxValue (solde);
				}

				data.BudgetGauche = this.GetBudget (compte);
			}

			int rank = 0;
			foreach (var compte in this.comptaEntity.PlanComptable.Where (x => x.Catégorie == CatégorieDeCompte.Passif))
			{
				if (this.Options.Profondeur.HasValue && compte.Niveau >= this.Options.Profondeur.Value)
				{
					continue;
				}

				var solde = this.comptaEntity.GetSoldeCompte (compte);

				if (!this.Options.ComptesNuls && solde.GetValueOrDefault () == 0)
				{
					continue;
				}

				BilanData data;

				if (rank >= this.readonlyAllData.Count)
				{
					data = new BilanData ();
					this.readonlyAllData.Add (data);
				}
				else
				{
					data = this.readonlyAllData[rank] as BilanData;
				}

				data.NuméroDroite = compte.Numéro;
				data.TitreDroite  = compte.Titre;
				data.NiveauDroite = compte.Niveau;

				if (this.HasSolde (compte))
				{
					data.SoldeDroite = solde;
					totalDroite += solde.GetValueOrDefault ();
					this.SetMinMaxValue (solde);
				}

				data.BudgetDroite = this.GetBudget (compte);

				rank++;
			}

			this.SetBottomSeparatorToPreviousLine ();

			//	Avant-dernière ligne.
			if (totalGauche != totalDroite)
			{
				var data = new BilanData ();

				if (totalGauche < totalDroite)
				{
					data.TitreGauche = "Différence (découvert)";
					data.SoldeGauche = totalDroite - totalGauche;
					data.IsBold      = true;

					totalGauche = totalDroite;

					this.SetMinMaxValue (data.SoldeGauche);
				}

				if (totalGauche > totalDroite)
				{
					data.TitreDroite = "Différence (capital)";
					data.SoldeDroite = totalGauche - totalDroite;
					data.IsBold      = true;

					totalDroite = totalGauche;

					this.SetMinMaxValue (data.SoldeDroite);
				}

				data.NeverFiltered = true;

				this.readonlyAllData.Add (data);
			}

			//	Dernière ligne
			{
				var data = new BilanData ();

				data.SoldeGauche   = totalGauche;
				data.SoldeDroite   = totalDroite;
				data.IsBold        = true;
				data.NeverFiltered = true;

				this.readonlyAllData.Add (data);

				this.SetMinMaxValue (data.SoldeGauche);
				this.SetMinMaxValue (data.SoldeDroite);
			}

			this.FilterUpdate ();
		}

		private bool HasSolde(ComptaCompteEntity compte)
		{
			//	Indique si le solde du compte doit figurer dans le tableau.
			//	Si la profondeur n'est pas spécifiée, on accepte tous les comptes normaux.
			//	Si la profondeur est spécifiée, on accepte les comptes qui ont exactement cette profondeur.
			if (compte.Type == TypeDeCompte.Normal)
			{
				return true;
			}

			if (this.Options.Profondeur.HasValue && compte.Niveau+1 == this.Options.Profondeur.Value)
			{
				return true;
			}

			return false;
		}


		public override FormattedText GetText(int row, ColumnType column, bool all = false)
		{
			var data = this.GetReadOnlyData (row, all) as BilanData;

			if (data == null)
			{
				return FormattedText.Null;
			}

			switch (column)
			{
				case ColumnType.NuméroGauche:
					return data.NuméroGauche;

				case ColumnType.TitreGauche:
					return data.TitreGauche;

				case ColumnType.SoldeGauche:
					return AbstractDataAccessor.GetMontant (data.SoldeGauche);

				case ColumnType.SoldeGraphiqueGauche:
					return this.GetMinMaxText (data.SoldeGauche);

				case ColumnType.BudgetGauche:
					return this.GetBudgetText (data.SoldeGauche, data.BudgetGauche);

				case ColumnType.NuméroDroite:
					return data.NuméroDroite;

				case ColumnType.TitreDroite:
					return data.TitreDroite;

				case ColumnType.SoldeDroite:
					return AbstractDataAccessor.GetMontant (data.SoldeDroite);

				case ColumnType.SoldeGraphiqueDroite:
					return this.GetMinMaxText (data.SoldeDroite);

				case ColumnType.BudgetDroite:
					return this.GetBudgetText (data.SoldeDroite, data.BudgetDroite);

				default:
					return FormattedText.Null;
			}
		}


		private static FormattedText GetNuméro(ComptaCompteEntity compte)
		{
			if (compte == null)
			{
				return JournalDataAccessor.multi;
			}
			else
			{
				return compte.Numéro;
			}
		}

		private BilanOptions Options
		{
			get
			{
				return this.options as BilanOptions;
			}
		}
	}
}
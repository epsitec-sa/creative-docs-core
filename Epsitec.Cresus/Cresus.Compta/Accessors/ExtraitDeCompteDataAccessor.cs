﻿//	Copyright © 2010, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Daniel ROUX, Maintainer: Daniel ROUX

using Epsitec.Common.Types;
using Epsitec.Common.Support.EntityEngine;

using Epsitec.Cresus.Compta.Controllers;
using Epsitec.Cresus.Compta.Entities;
using Epsitec.Cresus.Compta.Helpers;
using Epsitec.Cresus.Compta.Graph;
using Epsitec.Cresus.Compta.Search.Data;
using Epsitec.Cresus.Compta.Options.Data;
using Epsitec.Cresus.Compta.Permanents.Data;

using System.Collections.Generic;
using System.Linq;

namespace Epsitec.Cresus.Compta.Accessors
{
	/// <summary>
	/// Gère l'accès aux données de la balance de vérification de la comptabilité.
	/// </summary>
	public class ExtraitDeCompteDataAccessor : AbstractDataAccessor
	{
		public ExtraitDeCompteDataAccessor(AbstractController controller)
			: base (controller)
		{
			this.permanents = this.mainWindowController.GetSettingsPermanents<ExtraitDeComptePermanents> ("Présentation.ExtraitDeCompte.Permanents", this.compta);
			this.options    = this.mainWindowController.GetSettingsOptions<ExtraitDeCompteOptions> ("Présentation.ExtraitDeCompte.Options", this.compta);
			this.searchData = this.mainWindowController.GetSettingsSearchData ("Présentation.ExtraitDeCompte.Search");
			this.filterData = this.mainWindowController.GetSettingsSearchData ("Présentation.ExtraitDeCompte.Filter");

			this.UpdateAfterOptionsChanged ();
		}


		public override bool IsEditionCreationEnable
		{
			get
			{
				return false;
			}
		}


		public override void UpdateFilter()
		{
			Date? beginDate, endDate;
			this.filterData.GetBeginnerDates (out beginDate, out endDate);

			if (this.lastBeginDate != beginDate || this.lastEndDate != endDate)
			{
				this.UpdateAfterOptionsChanged ();
			}

			base.UpdateFilter ();
		}

		public override void UpdateAfterOptionsChanged()
		{
			this.readonlyAllData.Clear ();

			FormattedText numéroCompte = this.Permanents.NuméroCompte;
			if (numéroCompte.IsNullOrEmpty)
			{
				return;
			}

			this.filterData.GetBeginnerDates (out this.lastBeginDate, out this.lastEndDate);
			this.soldesJournalManager.Initialize (this.période.Journal, this.lastBeginDate, this.lastEndDate);

			var compte = this.compta.PlanComptable.Where (x => x.Numéro == numéroCompte).FirstOrDefault ();
			if (compte == null)
			{
				return;
			}

			foreach (var écriture in this.période.Journal)
			{
				if (!Dates.DateInRange (écriture.Date, this.lastBeginDate, this.lastEndDate))
				{
					continue;
				}

				bool débit  = ExtraitDeCompteDataAccessor.Match (écriture.Débit,  numéroCompte);
				bool crédit = ExtraitDeCompteDataAccessor.Match (écriture.Crédit, numéroCompte);

				if (débit)
				{
					var data = new ExtraitDeCompteData ()
					{
						IsDébit        = true,
						Entity         = écriture,
						Date           = écriture.Date,
						Pièce          = écriture.Pièce,
						Libellé        = écriture.Libellé,
						FullLibelléTVA = écriture.FullLibelléTVA,
						CP             = écriture.Crédit,
						Débit          = écriture.Montant,
						CodeTVA        = écriture.CodeTVA,
						TauxTVA        = écriture.TauxTVA,
						Journal        = écriture.Journal.Nom,
					};

					this.readonlyAllData.Add (data);
				}

				if (crédit)
				{
					var data = new ExtraitDeCompteData ()
					{
						IsDébit        = false,
						Entity         = écriture,
						Date           = écriture.Date,
						Pièce          = écriture.Pièce,
						Libellé        = écriture.Libellé,
						FullLibelléTVA = écriture.FullLibelléTVA,
						CP             = écriture.Débit,
						Crédit         = écriture.Montant,
						CodeTVA        = écriture.CodeTVA,
						TauxTVA        = écriture.TauxTVA,
						Journal        = écriture.Journal.Nom,
					};

					this.readonlyAllData.Add (data);
				}
			}

			this.SetBottomSeparatorToPreviousLine ();

			//	Génère la dernière ligne.
			{
				var data = new ExtraitDeCompteData ()
				{
					Entity         = compte,
					FullLibelléTVA = "Mouvement",
					IsItalic       = true,
					NeverFiltered  = true,
				};

				this.readonlyAllData.Add (data);
			}

			this.UpdateSoldes ();
			this.UpdateFilter ();
		}

		private void UpdateSoldes()
		{
			//	Met à jour l'évolution du solde du compte, visible dans la colonne 'Solde'.
			FormattedText numéroCompte = this.Permanents.NuméroCompte;
			if (numéroCompte.IsNullOrEmpty)
			{
				return;
			}

			var compte = this.compta.PlanComptable.Where (x => x.Numéro == numéroCompte).FirstOrDefault ();
			if (compte == null)
			{
				return;
			}

			decimal sens = (compte.Catégorie == CatégorieDeCompte.Passif ||	compte.Catégorie == CatégorieDeCompte.Produit) ? -1 : 1;

			decimal solde       = 0;
			decimal totalDébit  = 0;
			decimal totalCrédit = 0;

			foreach (var d in this.readonlyAllData)
			{
				var data = d as ExtraitDeCompteData;
				var écriture = data.Entity as ComptaEcritureEntity;

				if (data.NeverFiltered)  // dernière ligne "mouvement" ?
				{
					data.Débit  = totalDébit;
					data.Crédit = totalCrédit;
				}
				else
				{
					if (data.IsDébit)
					{
						solde      += écriture.Montant * sens;
						totalDébit += écriture.Montant;

						data.Solde = solde;
					}
					else
					{
						solde       -= écriture.Montant * sens;
						totalCrédit += écriture.Montant;

						data.Solde = solde;
					}
				}
			}
		}


		protected override void UpdateAfterFilterUpdated()
		{
			//	Appelé après la mise à jour du filtre, pour mettre à jour les données graphiques.
			if (!this.Options.HasGraphics)
			{
				return;
			}

			this.cube.Dimensions = 2;
			this.cube.Clear ();
			this.cube.Mode = GraphicMode.Empilé;

			this.cube.SetTitle (0, 0, this.Permanents.NuméroCompte);

			int y = 0;
			foreach (var d in this.readonlyData)
			{
				var data = d as ExtraitDeCompteData;

				this.cube.SetTitle (1, y, data.Pièce);
				this.cube.SetValue (0, y, data.Solde);

				y++;
			}
		}


		public override FormattedText GetText(int row, ColumnType column, bool all = false)
		{
			var data = this.GetReadOnlyData (row, all) as ExtraitDeCompteData;

			if (data == null)
			{
				return FormattedText.Null;
			}

			var écriture = data.Entity as ComptaEcritureEntity;
			var compte   = data.Entity as ComptaCompteEntity;

			ComptaMonnaieEntity monnaie = null;

			if (écriture != null)
			{
				monnaie = écriture.Monnaie;
			}

			if (compte != null)
			{
				monnaie = compte.Monnaie;
			}

			switch (column)
			{
				case ColumnType.Date:
					if (data.Date.HasValue)
					{
						return data.Date.Value.ToString ();
					}
					else
					{
						return FormattedText.Empty;
					}

				case ColumnType.CP:
					return ExtraitDeCompteDataAccessor.GetNuméro (data.CP);

				case ColumnType.Pièce:
					return data.Pièce;

				case ColumnType.Libellé:
					return data.FullLibelléTVA;

				case ColumnType.Débit:
					return Converters.MontantToString (data.Débit, monnaie);

				case ColumnType.Crédit:
					return Converters.MontantToString (data.Crédit, monnaie);

				case ColumnType.Solde:
					return Converters.MontantToString (data.Solde, monnaie);

				case ColumnType.SoldeGraphique:
					return AbstractDataAccessor.GetGraphicText (row);

				case ColumnType.CodeTVA:
					return JournalEditionLine.GetCodeTVADescription (data.CodeTVA);

				case ColumnType.TauxTVA:
					return Converters.PercentToString (data.TauxTVA);

				case ColumnType.CompteTVA:
					return JournalEditionLine.GetCodeTVACompte (data.CodeTVA);

				case ColumnType.Journal:
					return data.Journal;

				default:
					return FormattedText.Null;
			}
		}


		public override void StartCreationLine()
		{
			this.editionLine.Clear ();

			this.firstEditedRow = -1;
			this.countEditedRow = 1;

			this.isCreation = true;
			this.isModification = false;
			this.justCreated = false;

			this.controller.EditorController.UpdateFieldsEditionData ();
		}

		public override void StartModificationLine(int row)
		{
			this.editionLine.Clear ();

			this.firstEditedRow = row;
			this.countEditedRow = 0;

			if (row >= 0 && row < this.readonlyData.Count)
			{
				var extrait = new ExtraitDeCompteEditionLine (this.controller);
				var data = this.readonlyData[row];
				extrait.EntityToData (data);

				this.editionLine.Add (extrait);
				this.countEditedRow++;
			}

			this.initialCountEditedRow = this.countEditedRow;
			this.isCreation = false;
			this.isModification = true;
			this.justCreated = false;

			this.controller.EditorController.UpdateFieldsEditionData ();
		}

		public override void UpdateEditionLine()
		{
			if (this.isModification)
			{
				this.UpdateModificationData ();
				this.justCreated = false;
			}

			this.SearchUpdate ();
		}

		private void UpdateModificationData()
		{
			int row = this.firstEditedRow;

			var data = this.readonlyData[row] as ExtraitDeCompteData;
			var écriture = data.Entity as ComptaEcritureEntity;
			var initialDate    = écriture.Date;
			var initialPièce   = écriture.Pièce;
			var initialMontant = écriture.Montant;
			var initialJournal = écriture.Journal;

			this.editionLine[0].DataToEntity (data);

			if (écriture.MultiId != 0)  // écriture multiple ?
			{
				if (écriture.Date != initialDate)  // changement de date ?
				{
					this.UpdateMultiDate (écriture);
				}

				if (écriture.Pièce != initialPièce && !this.PlusieursPièces)  // changement de pièce ?
				{
					this.UpdateMultiPièce (écriture);
				}

				if (écriture.Montant != initialMontant)  // changement de montant ?
				{
					this.UpdateMultiMontant (écriture);
				}

				if (écriture.Journal != initialJournal)  // changement de journal ?
				{
					this.UpdateMultiJournal (écriture);
				}
			}

			if (écriture.Date != initialDate)  // changement de date ?
			{
				this.UpdateDate (écriture, data);
			}

			if (écriture.Montant != initialMontant)  // changement de montant ?
			{
				this.UpdateSoldes ();
			}
		}

		private void UpdateMultiDate(ComptaEcritureEntity source)
		{
			foreach (var écriture in this.période.Journal.Where (x => x.MultiId == source.MultiId))
			{
				écriture.Date = source.Date;
			}
		}

		private void UpdateMultiPièce(ComptaEcritureEntity source)
		{
			foreach (var écriture in this.période.Journal.Where (x => x.MultiId == source.MultiId))
			{
				écriture.Pièce = source.Pièce;
			}
		}

		private void UpdateMultiMontant(ComptaEcritureEntity source)
		{
			decimal total = 0;
			ComptaEcritureEntity auto = null;

			foreach (var écriture in this.période.Journal.Where (x => x.MultiId == source.MultiId))
			{
				if (écriture.TotalAutomatique)
				{
					auto = écriture;
				}
				else
				{
					total += écriture.Montant;
				}
			}

			if (auto != null)
			{
				auto.Montant = total;
			}
		}

		private void UpdateMultiJournal(ComptaEcritureEntity source)
		{
			foreach (var écriture in this.période.Journal.Where (x => x.MultiId == source.MultiId))
			{
				écriture.Journal = source.Journal;
			}
		}

		private void UpdateDate(ComptaEcritureEntity écriture, ExtraitDeCompteData extrait)
		{
			this.readonlyData.Remove (extrait);
			int row = this.GetSortedRow (extrait.Date.Value);
			this.readonlyData.Insert (row, extrait);
			this.firstEditedRow = row;

			this.JournalAdjust (écriture);
		}

		private int GetSortedRow(Date date)
		{
			int count = this.readonlyData.Count;
			for (int row = count-1; row >= 0; row--)
			{
				var data = this.readonlyData[row] as ExtraitDeCompteData;

				if (data.Date <= date)
				{
					return row+1;
				}
			}

			return 0;
		}

		private void JournalAdjust(ComptaEcritureEntity écriture)
		{
			//	Ajuste une écriture dans son journal après un changement de date.
			var journal = this.période.Journal;

			int row = journal.IndexOf (écriture);
			int firstRow, countRow;
			JournalDataAccessor.ExploreMulti (journal, row, out firstRow, out countRow);

			var temp = new List<ComptaEcritureEntity> ();

			for (int i = 0; i < countRow; i++)
			{
				temp.Add (journal[firstRow]);
				journal.RemoveAt (firstRow);
			}

			int index = JournalDataAccessor.GetSortedRow (journal, écriture.Date);

			for (int i = 0; i < countRow; i++)
			{
				journal.Insert (index+i, temp[i]);
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

		private new ExtraitDeComptePermanents Permanents
		{
			get
			{
				return this.permanents as ExtraitDeComptePermanents;
			}
		}

		private new ExtraitDeCompteOptions Options
		{
			get
			{
				return this.options as ExtraitDeCompteOptions;
			}
		}


		private static bool Match(ComptaCompteEntity compte, FormattedText numéro)
		{
			//	Retroune true si le compte ou ses fils correspond au numéro.
			while (compte != null && !compte.Numéro.IsNullOrEmpty)
			{
				if (compte.Numéro == numéro)
				{
					return true;
				}

				compte = compte.Groupe;
			}

			return false;
		}

		private bool PlusieursPièces
		{
			//	Retourne true si les écritures multiples peuvent avoir une pièce par ligne.
			get
			{
				return this.controller.SettingsList.GetBool (SettingsType.EcriturePlusieursPièces);
			}
		}

	}
}
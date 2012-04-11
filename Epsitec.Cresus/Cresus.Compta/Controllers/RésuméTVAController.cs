﻿//	Copyright © 2010, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Daniel ROUX, Maintainer: Daniel ROUX

using Epsitec.Common.Drawing;
using Epsitec.Common.Widgets;
using Epsitec.Common.Types;
using Epsitec.Common.Types.Converters;

using Epsitec.Cresus.Core.Business;

using Epsitec.Cresus.Compta.Accessors;
using Epsitec.Cresus.Compta.Entities;
using Epsitec.Cresus.Compta.Helpers;
using Epsitec.Cresus.Compta.Options.Data;
using Epsitec.Cresus.Compta.Options.Controllers;
using Epsitec.Cresus.Compta.Permanents.Data;

using System.Collections.Generic;
using System.Linq;

namespace Epsitec.Cresus.Compta.Controllers
{
	/// <summary>
	/// Ce contrôleur gère le résumé TVA de la comptabilité.
	/// </summary>
	public class RésuméTVAController : AbstractController
	{
		public RésuméTVAController(ComptaApplication app, BusinessContext businessContext, MainWindowController mainWindowController)
			: base (app, businessContext, mainWindowController)
		{
			this.dataAccessor = new RésuméTVADataAccessor (this);

			this.viewSettingsList = this.mainWindowController.GetViewSettingsList ("Présentation.RésuméTVA.ViewSettings");
		}


		public override bool HasShowSearchPanel
		{
			get
			{
				return true;
			}
		}

		public override bool HasShowFilterPanel
		{
			get
			{
				return true;
			}
		}

		public override bool HasShowOptionsPanel
		{
			get
			{
				return true;
			}
		}

		public override bool HasShowInfoPanel
		{
			get
			{
				return false;
			}
		}


		protected override void CreateOptions(FrameBox parent)
		{
			this.optionsController = new RésuméTVAOptionsController (this);
			this.optionsController.CreateUI (parent, this.OptionsChanged);
			this.optionsController.ShowPanel = this.mainWindowController.ShowOptionsPanel;

			this.UpdateColumnMappers ();
		}

		protected override void OptionsChanged()
		{
			this.dataAccessor.UpdateAfterOptionsChanged ();
			this.ClearHilite ();
			this.UpdateColumnMappers ();
			this.UpdateArray ();

			this.UpdateArrayContent ();
			this.UpdateTitle ();
			this.FilterUpdateTopToolbar ();
			this.UpdateViewSettings ();
		}

		protected override void UpdateTitle()
		{
			this.SetTitle ("Résumé TVA");
			this.SetSubtitle (this.période.ShortTitle);
		}


		protected override FormattedText GetArrayText(int row, ColumnType columnType)
		{
			//	Retourne le texte contenu dans une cellule.
			var text = this.dataAccessor.GetText (row, columnType);
			var data = this.dataAccessor.GetReadOnlyData (row) as RésuméTVAData;

			if (columnType == ColumnType.Solde)
			{
				if (!data.NeverFiltered)
				{
					text = FormattedText.Empty;
				}
			}

			return data.Typo (text);
		}


		#region Context menu
		protected override VMenu ContextMenu
		{
			//	Retourne le menu contextuel à utiliser.
			get
			{
				var menu = new VMenu ();

				this.PutContextMenuJournal (menu);
				this.PutContextMenuExtrait (menu);

				return menu;
			}
		}

		private void PutContextMenuJournal(VMenu menu)
		{
			var data = this.dataAccessor.GetReadOnlyData (this.arrayController.SelectedRow) as RésuméTVAData;
			var écriture = data.Entity as ComptaEcritureEntity;

			var item = this.PutContextMenuItem (menu, "Présentation.Journal", "Montre dans le journal", écriture != null);

			item.Clicked += delegate
			{
				var présentation = this.mainWindowController.ShowPrésentation (Res.Commands.Présentation.Journal);

				int row = (présentation.DataAccessor as JournalDataAccessor).GetIndexOf (écriture);
				if (row != -1)
				{
					présentation.SelectedArrayLine = row;
				}
			};
		}

		private void PutContextMenuExtrait(VMenu menu)
		{
			var data = this.dataAccessor.GetReadOnlyData (this.arrayController.SelectedRow) as RésuméTVAData;

			var item = this.PutContextMenuItem (menu, "Présentation.Extrait", string.Format ("Extrait du compte {0}", data.Numéro), !data.Numéro.IsNullOrEmpty);

			item.Clicked += delegate
			{
				var présentation = this.mainWindowController.ShowPrésentation (Res.Commands.Présentation.Extrait);

				var permanent = présentation.DataAccessor.Permanents as ExtraitDeComptePermanents;
				permanent.NuméroCompte = data.Numéro;

				présentation.UpdateAfterChanged ();
			};
		}
		#endregion


		protected override IEnumerable<ColumnMapper> InitialColumnMappers
		{
			get
			{
				yield return new ColumnMapper (ColumnType.Compte,     0.20, ContentAlignment.MiddleLeft,  "Compte");
				yield return new ColumnMapper (ColumnType.CodeTVA,    0.20, ContentAlignment.MiddleLeft,  "Code TVA");
				yield return new ColumnMapper (ColumnType.TauxTVA,    0.20, ContentAlignment.MiddleLeft,  "Taux");
				yield return new ColumnMapper (ColumnType.Date,       0.20, ContentAlignment.MiddleLeft,  "Date");
				yield return new ColumnMapper (ColumnType.Pièce,      0.20, ContentAlignment.MiddleLeft,  "Pièce");
				yield return new ColumnMapper (ColumnType.Compte2,    0.20, ContentAlignment.MiddleLeft,  "Compte");
				yield return new ColumnMapper (ColumnType.Titre,      1.00, ContentAlignment.MiddleLeft,  "Code TVA / Titre du compte");
				yield return new ColumnMapper (ColumnType.Montant,    0.20, ContentAlignment.MiddleRight, "Montant HT");
				yield return new ColumnMapper (ColumnType.MontantTVA, 0.20, ContentAlignment.MiddleRight, "TVA");
				yield return new ColumnMapper (ColumnType.Différence, 0.20, ContentAlignment.MiddleRight, "Diff.");
			}
		}

		protected override void UpdateColumnMappers()
		{
			var options = this.dataAccessor.Options as RésuméTVAOptions;

			this.ShowHideColumn (ColumnType.Compte,     (!options.MontreEcritures && !options.ParCodeTVA) || (options.MontreEcritures && options.ParCodeTVA));
			this.ShowHideColumn (ColumnType.CodeTVA,    (!options.MontreEcritures && options.ParCodeTVA) || (options.MontreEcritures && !options.ParCodeTVA));
			this.ShowHideColumn (ColumnType.TauxTVA,    options.MontreEcritures ||  options.ParCodeTVA);
			this.ShowHideColumn (ColumnType.Date,       options.MontreEcritures);
			this.ShowHideColumn (ColumnType.Pièce,      options.MontreEcritures);
			this.ShowHideColumn (ColumnType.Compte2,    !options.MontreEcritures &&  options.ParCodeTVA);
			this.ShowHideColumn (ColumnType.Différence, options.MontreEcritures);

			if (options.MontreEcritures)
			{
				this.SetColumnDescription (ColumnType.Titre, "Libellé");
			}
			else
			{
				this.SetColumnDescription (ColumnType.Titre, options.ParCodeTVA ? "Titre du compte" : "Code TVA / Titre du compte");
			}

			this.SetColumnDescription (ColumnType.Montant, options.MontantTTC ? "Montant TTC" : "Montant HT");
		}
	}
}

﻿//	Copyright © 2010, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Daniel ROUX, Maintainer: Daniel ROUX

using Epsitec.Common.Drawing;
using Epsitec.Common.Widgets;
using Epsitec.Common.Types;

using Epsitec.Cresus.Core;
using Epsitec.Cresus.Core.Entities;
using Epsitec.Cresus.Core.Controllers;

using Epsitec.Cresus.Compta.Entities;

using System.Collections.Generic;
using System.Linq;

namespace Epsitec.Cresus.Compta.Accessors
{
	/// <summary>
	/// Cette classe décrit les options d'affichage d'un extrait de compte de la comptabilité.
	/// </summary>
	public class ExtraitDeCompteOptions : AbstractOptions
	{
		public override void SetComptaEntity(ComptaEntity compta)
		{
			base.SetComptaEntity (compta);

			//	Utilise le premier compte normal par défaut.
			var compte = this.comptaEntity.PlanComptable.Where (x => x.Type == TypeDeCompte.Normal).FirstOrDefault ();

			if (compte != null)
			{
				this.NuméroCompte = compte.Numéro;
			}

			this.Clear ();
		}


		public override void Clear()
		{
			base.Clear ();

			this.CatégorieMontrée             = CatégorieDeCompte.Tous;
			this.MontreComptesVides           = true;
			this.MontreComptesCentralisateurs = false;
			this.HasGraphics                  = true;
		}


		public FormattedText NuméroCompte
		{
			//	Numéro du compte dont on affiche l'extrait.
			get;
			set;
		}

		public CatégorieDeCompte CatégorieMontrée
		{
			get;
			set;
		}

		public bool MontreComptesVides
		{
			get;
			set;
		}

		public bool MontreComptesCentralisateurs
		{
			get;
			set;
		}

		public bool HasGraphics
		{
			get;
			set;
		}


		protected override void CreateEmpty()
		{
			this.emptyOptions = new ExtraitDeCompteOptions ();
			this.emptyOptions.SetComptaEntity (this.comptaEntity);
		}

		public override bool CompareTo(AbstractOptions other)
		{
			if (!base.CompareTo (other))
			{
				return false;
			}

			var o = other as ExtraitDeCompteOptions;

			return this.CatégorieMontrée == o.CatégorieMontrée &&
				   this.MontreComptesVides == o.MontreComptesVides &&
				   this.MontreComptesCentralisateurs == o.MontreComptesCentralisateurs &&
				   this.HasGraphics == o.HasGraphics;
		}
	}
}

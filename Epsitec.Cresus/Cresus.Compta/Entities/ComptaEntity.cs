//	Copyright © 2011-2012, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Pierre ARNAUD, Maintainer: Pierre ARNAUD

using Epsitec.Common.Types;

using Epsitec.Cresus.Core;
using Epsitec.Cresus.Core.Library;

using System.Collections.Generic;
using System.Linq;

namespace Epsitec.Cresus.Compta.Entities
{
	public partial class ComptaEntity
	{
		public FormattedText JournalRésumé(ComptaJournalEntity journal)
		{
			//	Retourne le résumé d'un journal.
			int totalEcritures = 0;
			int totalPériodes = 0;

			foreach (var période in this.Périodes)
			{
				int total = période.Journal.Where (x => x.Journal == journal).Count ();

				if (total != 0)
				{
					totalEcritures += total;
					totalPériodes++;
				}
			}

			string écrituresRésumé, périodesRésumé;

			if (totalEcritures == 0)
			{
				return "Vide";
			}
			else if (totalEcritures == 1)
			{
				écrituresRésumé = "1 écriture";
			}
			else
			{
				écrituresRésumé = string.Format ("{0} écritures", totalEcritures.ToString ());
			}

			if (totalPériodes == 0)
			{
				périodesRésumé = "aucune période";
			}
			else if (totalPériodes == 1)
			{
				périodesRésumé = "1 période";
			}
			else
			{
				périodesRésumé = string.Format ("{0} périodes", totalPériodes.ToString ());
			}

			return écrituresRésumé + " dans " + périodesRésumé;
		}


		public FormattedText ProchainePièce
		{
			//	A partir de "AB102", retourne "AB103" (par exemple).
			get
			{
				var pièce = this.DernièrePièce;

				if (!pièce.IsNullOrEmpty)
				{
					string p = pièce.ToSimpleText ();
					int i = pièce.Length-1;
					while (i >= 0)
					{
						if (p[i] >= '0' && p[i] <= '9')
						{
							i--;
						}
						else
						{
							break;
						}
					}

					if (i < pièce.Length-1)
					{
						int n;
						if (int.TryParse (p.Substring (i+1), out n))
						{
							return p.Substring (0, i+1) + (n+1).ToString ();
						}
					}
				}

				return pièce;
			}
		}


		#region Niveaux d'imbrications
		public void UpdateNiveauCompte()
		{
			//	Met à jour le niveau d'imbrication (0..n) de tous les comptes.
			foreach (var compte in this.PlanComptable)
			{
				this.UpdateNiveauCompte (compte);
			}
		}

		private void UpdateNiveauCompte(ComptaCompteEntity compte)
		{
			//	Met à jour le niveau d'imbrication (0..n) d'un compte.
			var c = compte;
			int niveau = 0;

			while (c != null && c.Groupe != null && !c.Groupe.Numéro.IsNullOrEmpty)
			{
				c = this.PlanComptable.Where (x => x.Numéro == c.Groupe.Numéro).FirstOrDefault ();

				if (c == null)
				{
					break;
				}

				niveau++;
			}

			if (compte.Niveau != niveau)
			{
				compte.Niveau = niveau;
			}
		}
		#endregion
	}
}

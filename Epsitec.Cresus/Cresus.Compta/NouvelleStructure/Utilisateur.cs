﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Epsitec.Common.Types;

namespace Epsitec.Cresus.Compta.NouvelleStructure
{
	public class Utilisateur : ObjetComptable
	{
		public FormattedText		NomCourt;
		public FormattedText		NomComplet;
		public FormattedText		Commentaire;  // utile ?
		public bool					IdentitéWindows;
		public bool					Désactivé;
		public IntervalleDates		Dates;
		public string				MotDePasse;
		public bool					Admin;
		public string				Présentations;  // ?
		public GénérateurPièce		GénérateurPièce;
	}
}

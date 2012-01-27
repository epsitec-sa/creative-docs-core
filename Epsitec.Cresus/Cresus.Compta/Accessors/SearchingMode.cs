//	Copyright © 2010, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Daniel ROUX, Maintainer: Daniel ROUX

namespace Epsitec.Cresus.Compta.Accessors
{
	public enum SearchingMode
	{
		Fragment,		// contenu partiel
		WholeWord,		// mot entier
		StartsWith,		// au début
		EndsWith,		// à la fin
		WholeContent,	// contenu complet
		Interval,		// intervalle de nombres décimaux ou de dates
		Empty,			// doit être vide
	}
}

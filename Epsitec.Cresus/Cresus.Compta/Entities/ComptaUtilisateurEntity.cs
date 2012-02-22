//	Copyright © 2011-2012, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Daniel ROUX, Maintainer: Daniel ROUX

using Epsitec.Common.Widgets;
using Epsitec.Common.Types;

using Epsitec.Cresus.Compta.Helpers;

using System.Collections.Generic;
using System.Linq;

namespace Epsitec.Cresus.Compta.Entities
{
	public partial class ComptaUtilisateurEntity
	{
		public FormattedText GetSummary()
		{
			if (this.Admin)
			{
				return "Administrateur, toutes les présentations";
			}

			int n = Converters.PrésentationCommandCount (this.Présentations);

			if (n == 0)
			{
				return "Aucune présentation";
			}
			else if (n == 1)
			{
				return "1 présenttion";
			}
			else
			{
				return string.Format ("{0} présentations", n.ToString ());
			}
		}


		public void SetPrésenttion(Command cmd, bool state)
		{
			string p = this.Présentations;
			Converters.SetPrésentationCommand (ref p, cmd, state);
			this.Présentations = p;
		}

		public bool HasPrésentation(Command cmd)
		{
			return Converters.ContainsPrésentationCommand (this.Présentations, cmd);
		}
	}
}

﻿//	Copyright © 2010, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Daniel ROUX, Maintainer: Daniel ROUX

using Epsitec.Common.Drawing;
using Epsitec.Common.Widgets;
using Epsitec.Common.Types;

using Epsitec.Cresus.Compta.Accessors;
using Epsitec.Cresus.Compta.Entities;

using System.Collections.Generic;
using System.Linq;

namespace Epsitec.Cresus.Compta.Options.Data
{
	/// <summary>
	/// Cette classe décrit les options d'affichage du PP de la comptabilité.
	/// </summary>
	public class PPOptions : DoubleOptions
	{
		public override void Clear()
		{
			base.Clear ();

			this.ComparisonShowed = ComparisonShowed.Budget;

			this.graphOptions.TitleText = "Pertes et Profits";
		}


		protected override void CreateEmpty()
		{
			this.emptyOptions = new PPOptions ();
			this.emptyOptions.SetComptaEntity (this.compta);
			this.emptyOptions.Clear ();
		}

		public override AbstractOptions CopyFrom()
		{
			var options = new PPOptions ();
			options.SetComptaEntity (this.compta);
			this.CopyTo (options);
			return options;
		}
	}
}

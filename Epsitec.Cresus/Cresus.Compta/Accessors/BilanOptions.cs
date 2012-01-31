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
	/// Cette classe décrit les options d'affichage du bilan de la comptabilité.
	/// </summary>
	public class BilanOptions : AbstractOptions
	{
		public override void Clear()
		{
			base.Clear ();

			this.ComptesNuls = false;
			this.HasGraphics = false;
		}


		public bool ComptesNuls
		{
			//	Affiche les comptes dont le solde est nul ?
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
			this.emptyOptions = new BilanOptions ();
			this.emptyOptions.SetComptaEntity (this.comptaEntity);
		}

		public override bool CompareTo(AbstractOptions other)
		{
			if (!base.CompareTo (other))
			{
				return false;
			}

			var o = other as BilanOptions;

			return this.ComptesNuls == o.ComptesNuls &&
				   this.HasGraphics == o.HasGraphics;
		}
	}
}

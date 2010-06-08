﻿//	Copyright © 2010, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Daniel ROUX, Maintainer: Daniel ROUX

using Epsitec.Common.Types;
using Epsitec.Common.Types.Converters;

using Epsitec.Cresus.Core;
using Epsitec.Cresus.Core.Widgets;
using Epsitec.Cresus.Core.Widgets.Tiles;

using System.Collections.Generic;
using System.Linq;

namespace Epsitec.Cresus.Core.Controllers.EditionControllers
{
	public class EditionCountryViewController : EditionViewController<Entities.CountryEntity>
	{
		public EditionCountryViewController(string name, Entities.CountryEntity entity)
			: base (name, entity)
		{
		}

		protected override void CreateUI(TileContainer container)
		{
			var builder = new UIBuilder (container, this);

			builder.CreateHeaderEditorTile ();
			builder.CreateEditionGroupingTile ("Data.Mail", "Pays");

			this.CreateUIMain (builder);

			builder.CreateFooterEditorTile ();

			UI.SetInitialFocus (container);
		}


		private void CreateUIMain(UIBuilder builder)
		{
			var tile = builder.CreateEditionTile ();

			builder.CreateTextField (tile,  0, "Pays",                    Marshaler.Create (() => this.Entity.Name, x => this.Entity.Name = x));
			builder.CreateTextField (tile, 25, "Code ISO à deux lettres", Marshaler.Create (() => this.Entity.Code, x => this.Entity.Code = x));
		}
	}
}

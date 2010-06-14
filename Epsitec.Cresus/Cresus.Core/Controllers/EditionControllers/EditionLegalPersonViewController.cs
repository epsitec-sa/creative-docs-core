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
	public class EditionLegalPersonViewController : EditionViewController<Entities.LegalPersonEntity>
	{
		public EditionLegalPersonViewController(string name, Entities.LegalPersonEntity entity)
			: base (name, entity)
		{
		}

		protected override void CreateUI(TileContainer container)
		{
			var builder = new UIBuilder (container, this);

			builder.CreateHeaderEditorTile ();
			builder.CreateEditionTitleTile ("Data.LegalPerson", "Personne morale");

			this.CreateUIMain (builder);

			builder.CreateFooterEditorTile ();

			UI.SetInitialFocus (container);
		}


		private void CreateUIMain(UIBuilder builder)
		{
			var tile = builder.CreateEditionTile ();

			builder.CreateTextField      (tile,   0, "Nom complet", Marshaler.Create (() => this.Entity.Name,       x => this.Entity.Name = x));
			builder.CreateTextField      (tile, 150, "Nom court",   Marshaler.Create (() => this.Entity.ShortName,  x => this.Entity.ShortName = x));
			builder.CreateMargin         (tile, true);
			builder.CreateTextFieldMulti (tile, 100, "Complément",  Marshaler.Create (() => this.Entity.Complement, x => this.Entity.Complement = x));
		}
	}
}

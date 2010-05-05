﻿//	Copyright © 2010, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Daniel ROUX, Maintainer: Daniel ROUX

using System.Collections.Generic;
using System.Linq;
using System.Text;

using Epsitec.Common.Support;
using Epsitec.Common.Support.EntityEngine;
using Epsitec.Common.Types;
using Epsitec.Common.Drawing;
using Epsitec.Common.Widgets;

namespace Epsitec.Cresus.Core.Controllers.EditionControllers
{
	public class EditionLegalPersonViewController : EntityViewController<Entities.LegalPersonEntity>
	{
		public EditionLegalPersonViewController(string name, Entities.LegalPersonEntity entity)
			: base (name, entity)
		{
		}

		public override void CreateUI(Widget container)
		{
			UIBuilder builder = new UIBuilder (container, this);
			
			builder.CreateHeaderEditorTile ();

			var person = this.Entity;
			var group = builder.CreateEditionGroupingTile ("Data.LegalPerson", "Personne morale");

			var accessor = new Accessors.LegalPersonAccessor (null, person as Entities.LegalPersonEntity, false);
			var tile = builder.CreateEditionTile (group, accessor);

			builder.CreateFooterEditorTile ();

			builder.CreateTextField (tile.Container, 0, "Nom complet", accessor.Entity.Name, x => accessor.Entity.Name = x, Validators.StringValidator.Validate);
			builder.CreateTextField (tile.Container, 150, "Nom court", accessor.Entity.ShortName, x => accessor.Entity.ShortName = x, Validators.StringValidator.Validate);
			builder.CreateMargin (tile.Container, true);
			builder.CreateTextFieldMulti (tile.Container, 100, "Complément", accessor.Entity.Complement, x => accessor.Entity.Complement = x, null);

			UI.SetInitialFocus (container);
		}
	}
}

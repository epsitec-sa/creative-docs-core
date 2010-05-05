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
	public class EditionUriSchemeViewController : EntityViewController<Entities.AbstractContactEntity>
	{
		public EditionUriSchemeViewController(string name, Entities.AbstractContactEntity entity)
			: base (name, entity)
		{
		}

		public override void CreateUI(Widget container)
		{
			UIBuilder builder = new UIBuilder (container, this);
			Widgets.GroupingTile group;

			var contact = this.Entity;
			var accessor = new EntitiesAccessors.UriSchemeAccessor (null, contact, false);

			//	Crée les tuiles.
			builder.CreateHeaderEditorTile ();

			group = builder.CreateEditionGroupingTile ("Data.Type", "Type");
			var tile = builder.CreateEditionTile (group, accessor);

			builder.CreateFooterEditorTile ();

			//	Crée le contenu de la tuile d'édition.
			builder.CreateCombo (tile.Container, 100, "Type du moyen de contact", accessor.UriSchemeInitializer, true, false, true, accessor.UriScheme, x => accessor.UriScheme = x, null);

			UI.SetInitialFocus (container);
		}
	}
}

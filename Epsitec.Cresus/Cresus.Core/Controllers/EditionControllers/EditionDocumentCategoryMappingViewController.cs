﻿//	Copyright © 2010, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Daniel ROUX, Maintainer: Daniel ROUX

using Epsitec.Common.Support.EntityEngine;
using Epsitec.Common.Types;
using Epsitec.Common.Types.Converters;

using Epsitec.Cresus.Core;
using Epsitec.Cresus.Core.Entities;
using Epsitec.Cresus.Core.Controllers;
using Epsitec.Cresus.Core.Controllers.DataAccessors;
using Epsitec.Cresus.Core.Widgets;
using Epsitec.Cresus.Core.Widgets.Tiles;

using System.Collections.Generic;
using System.Linq;

namespace Epsitec.Cresus.Core.Controllers.EditionControllers
{
	public class EditionDocumentCategoryMappingViewController : EditionViewController<Entities.DocumentCategoryMappingEntity>
	{
		public EditionDocumentCategoryMappingViewController(string name, Entities.DocumentCategoryMappingEntity entity)
			: base (name, entity)
		{
		}

		protected override void CreateUI()
		{
			using (var builder = new UIBuilder (this))
			{
				builder.CreateHeaderEditorTile ();
				builder.CreateEditionTitleTile ("Data.DocumentCategoryMapping", "Assignation de catégorie");

				this.CreateUIMain (builder);
				this.CreateUICategories (builder);

				builder.CreateFooterEditorTile ();
			}
		}


		private void CreateUIMain(UIBuilder builder)
		{
			var tile = builder.CreateEditionTile ();

			builder.CreateTextField      (tile,   0, "Nom",         Marshaler.Create (() => this.Entity.Name,        x => this.Entity.Name = x));
			builder.CreateTextFieldMulti (tile, 100, "Description", Marshaler.Create (() => this.Entity.Description, x => this.Entity.Description = x));

			builder.CreateMargin (tile, horizontalSeparator: true);

			builder.CreateAutoCompleteTextField (tile, 0, "Type des données imprimables", Marshaler.Create (() => this.Entity.PrintableEntity, x => this.Entity.PrintableEntity = x), Business.Enumerations.GetAllPrintableEntities (), x => TextFormatter.FormatText (x.Values[0]));
		}

		private void CreateUICategories(UIBuilder builder)
		{
			var controller = new SelectionController<DocumentCategoryEntity> (this.BusinessContext)
			{
				CollectionValueGetter    = () => this.Entity.DocumentCategories,
				ToFormattedTextConverter = x => TextFormatter.FormatText (x.Name).IfNullOrEmptyReplaceWith (CollectionTemplate.DefaultEmptyText),
			};

			builder.CreateEditionDetailedItemPicker ("DocumentCategory", this.Entity, "Catégories des options", controller, Business.EnumValueCardinality.Any, ViewControllerMode.Summary);
		}
	}
}

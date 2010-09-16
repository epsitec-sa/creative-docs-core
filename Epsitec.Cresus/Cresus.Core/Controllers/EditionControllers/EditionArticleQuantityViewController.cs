﻿//	Copyright © 2010, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Daniel ROUX, Maintainer: Daniel ROUX

using Epsitec.Common.Support.EntityEngine;
using Epsitec.Common.Types;
using Epsitec.Common.Types.Converters;

using Epsitec.Cresus.Core;
using Epsitec.Cresus.Core.Entities;
using Epsitec.Cresus.Core.Widgets;
using Epsitec.Cresus.Core.Widgets.Tiles;

using Epsitec.Cresus.DataLayer.Context;

using System.Collections.Generic;
using System.Linq;

namespace Epsitec.Cresus.Core.Controllers.EditionControllers
{
	public class EditionArticleQuantityViewController : EditionViewController<Entities.ArticleQuantityEntity>
	{
		public EditionArticleQuantityViewController(string name, Entities.ArticleQuantityEntity entity)
			: base (name, entity)
		{
		}

		protected override void CreateUI()
		{
			using (var builder = new UIBuilder (this))
			{
				builder.CreateHeaderEditorTile ();
				builder.CreateEditionTitleTile ("Data.ArticleQuantity", "Quantité");

				this.CreateUIType          (builder);
				this.CreateUIMain          (builder);
				this.CreateUIUnitOfMeasure (builder);

				builder.CreateFooterEditorTile ();
			}
		}

		protected override EditionStatus GetEditionStatus()
		{
			return EditionStatus.Valid;
		}


		private void CreateUIType(Epsitec.Cresus.Core.UIBuilder builder)
		{
			var controller = new EnumController<Business.ArticleQuantityType> (Business.Enumerations.GetAllPossibleValueArticleQuantityType ())
			{
				ValueGetter = () => this.Entity.QuantityType,
				ValueSetter = x => this.Entity.QuantityType = x,
				ValueToDescriptionConverter = x => TextFormatter.FormatText (x as string[]),
			};

			builder.CreateEditionDetailedItemPicker ("Type", controller);
		}

		private void CreateUIMain(UIBuilder builder)
		{
			var tile = builder.CreateEditionTile ();

			builder.CreateTextField (tile, 100, "Date prévue", Marshaler.Create (() => this.Entity.ExpectedDate, x => this.Entity.ExpectedDate = x));
			builder.CreateMargin    (tile, horizontalSeparator: true);
			builder.CreateTextField (tile,  60, "Quantité",    Marshaler.Create (() => this.Entity.Quantity, x => this.Entity.Quantity = x));
		}

		private void CreateUIUnitOfMeasure(UIBuilder builder)
		{
			var controller = new SelectionController<UnitOfMeasureEntity> (this.BusinessContext)
				{
					ValueGetter         = () => this.Entity.Unit,
					ValueSetter         = x => this.Entity.Unit = x,
					ReferenceController = new ReferenceController (() => this.Entity.Unit, creator: this.CreateNewUnitOfMeasure),

					ToTextArrayConverter     = x => new string[] { TextFormatter.FormatText (x.Name).ToSimpleText (), x.Code },
					ToFormattedTextConverter = x => TextFormatter.FormatText (x.Name, "(", x.Code, ")"),
				};

#if false
			builder.CreateAutoCompleteTextField ("Unité", controller);
#else
			builder.CreateEditionDetailedItemPicker ("Unité", controller, Business.EnumValueCardinality.ExactlyOne);
#endif
		}

		private IEnumerable<UnitOfMeasureEntity> GetUnitOfMeasure()
		{
			//	Retourne les unités appartenant au même groupe que l'article.

			// Retrouve la ligne d'article correspondante (ArticleDocumentItemEntity),
			// qui est donc l'entité parente de l'entité elle-même (UnitOfMeasureEntity):
			var article = this.DataContext.GetEntities ().OfType<ArticleDocumentItemEntity> ().Where (x => x.ArticleQuantities.Contains (this.Entity)).Single ();

			if (article == null)
			{
				return CoreProgram.Application.Data.GetUnitOfMeasure ();
			}
			else
			{
				return CoreProgram.Application.Data.GetUnitOfMeasure ().Where (x => x.Category == article.ArticleDefinition.Units.Category);
			}
		}


		private NewEntityReference CreateNewUnitOfMeasure(DataContext context)
		{
			var title = context.CreateEmptyEntity<UnitOfMeasureEntity> ();
			return title;
		}
	}
}

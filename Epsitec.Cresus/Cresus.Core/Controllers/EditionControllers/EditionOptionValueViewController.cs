﻿//	Copyright © 2010, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Daniel ROUX, Maintainer: Daniel ROUX

using Epsitec.Common.Types;
using Epsitec.Common.Types.Converters;
using Epsitec.Common.Drawing;
using Epsitec.Common.Widgets;

using Epsitec.Cresus.Core;
using Epsitec.Cresus.Core.Entities;
using Epsitec.Cresus.Core.Widgets;
using Epsitec.Cresus.Core.Widgets.Tiles;
using Epsitec.Cresus.Core.TableDesigner;

using Epsitec.Cresus.DataLayer.Context;

using System.Collections.Generic;
using System.Linq;

namespace Epsitec.Cresus.Core.Controllers.EditionControllers
{
	public class EditionOptionValueViewController : EditionViewController<Entities.OptionValueEntity>
	{
		public EditionOptionValueViewController(string name, Entities.OptionValueEntity entity)
			: base (name, entity)
		{
		}

		protected override void CreateUI()
		{
			using (var builder = new UIBuilder (this))
			{
				builder.CreateHeaderEditorTile ();
				builder.CreateEditionTitleTile ("Data.OptionValue", "Option");

				this.CreateUIMain (builder);
				this.CreateUIArticleDefinition (builder);
				//?this.CreateUIParameter (builder);
				this.CreateUIUnit (builder);

				builder.CreateFooterEditorTile ();
			}
		}

		private void CreateUIMain(UIBuilder builder)
		{
			var tile = builder.CreateEditionTile ();

			builder.CreateTextField      (tile,   0, "Nom",         Marshaler.Create (() => this.Entity.Name,        x => this.Entity.Name = x));
			builder.CreateTextFieldMulti (tile, 100, "Description", Marshaler.Create (() => this.Entity.Description, x => this.Entity.Description = x));

			builder.CreateMargin (tile, horizontalSeparator: true);

			builder.CreateTextField (tile, 0, "Quantité", Marshaler.Create (() => this.Entity.Quantity, x => this.Entity.Quantity = x));
		}

		private void CreateUIArticleDefinition(UIBuilder builder)
		{
			var controller = new SelectionController<ArticleDefinitionEntity> (this.BusinessContext)
			{
				ValueGetter         = () => this.Entity.ArticleDefinition,
				ValueSetter         = x => this.Entity.ArticleDefinition = x,
				ReferenceController = new ReferenceController (() => this.Entity.ArticleDefinition, creator: this.CreateNewArticle),
			};

			builder.CreateAutoCompleteTextField ("Article", controller);
		}

#if false
		private void CreateUIParameter(UIBuilder builder)
		{
			var tile = builder.CreateEditionTile ();
			var group = builder.CreateGroup (tile);

			this.parameterController = new ArticleParameterControllers.ValuesArticleParameterController (this.TileContainer, tile);
			this.parameterController.CallbackParameterChanged = this.ParameterChanged;
			this.parameterController.CreateUI (group);
			this.parameterController.UpdateUI (this.Entity);
		}
#endif

		private void CreateUIUnit(UIBuilder builder)
		{
			var controller = new SelectionController<UnitOfMeasureEntity> (this.BusinessContext)
			{
				ValueGetter         = () => this.Entity.Unit,
				ValueSetter         = x => this.Entity.Unit = x,
				ReferenceController = new ReferenceController (() => this.Entity.Unit, creator: this.CreateNewUnit),
			};

			builder.CreateAutoCompleteTextField ("Unité de mesure", controller);
		}


		private NewEntityReference CreateNewArticle(DataContext context)
		{
			return context.CreateEntityAndRegisterAsEmpty<ArticleDefinitionEntity> ();
		}

		private NewEntityReference CreateNewUnit(DataContext context)
		{
			return context.CreateEntityAndRegisterAsEmpty<UnitOfMeasureEntity> ();
		}


#if false
		private void ParameterChanged(AbstractArticleParameterDefinitionEntity parameterDefinitionEntity)
		{
			//	Cette méthode est appelée lorsqu'un paramètre a été changé.
			ArticleParameterControllers.ArticleParameterToolbarController.UpdateTextFieldParameter (this.Entity, this.articleDescriptionTextField);
		}
#endif

	
		private ArticleParameterControllers.ValuesArticleParameterController	parameterController;
		private ArticleParameterControllers.ArticleParameterToolbarController	toolbarController;
		private TextFieldMultiEx												articleDescriptionTextField;
	}
}

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
using Epsitec.Cresus.Core.Helpers;

using Epsitec.Cresus.DataLayer.Context;

using System.Collections.Generic;
using System.Linq;

namespace Epsitec.Cresus.Core.Controllers.EditionControllers
{
	public class EditionEnumValueArticleParameterDefinitionViewController : EditionViewController<Entities.EnumValueArticleParameterDefinitionEntity>
	{
		public EditionEnumValueArticleParameterDefinitionViewController(string name, Entities.EnumValueArticleParameterDefinitionEntity entity)
			: base (name, entity)
		{
		}

		protected override void CreateUI(TileContainer container)
		{
			this.tileContainer = container;

			using (var builder = new UIBuilder (container, this))
			{
				builder.CreateHeaderEditorTile ();
				builder.CreateEditionTitleTile ("Data.ArticleParameter", "Paramètre");

				this.CreateTabBook (builder);
				this.CreateUIMain (builder);

				builder.CreateFooterEditorTile ();
			}
		}


		private void CreateTabBook(UIBuilder builder)
		{
			var tile = builder.CreateEditionTile ();

			builder.CreateMargin (tile, horizontalSeparator: false);

			List<string> pagesDescription = new List<string> ();
			pagesDescription.Add ("Numeric.Valeur nunérique");
			pagesDescription.Add ("Enum.Enumération");
			this.tabBookContainer = builder.CreateTabBook (tile, pagesDescription, "Enum", this.HandleTabBookAction);
		}

		private void HandleTabBookAction(string tabPageName)
		{
			if (tabPageName == "Enum")
			{
				return;
			}

			Common.ChangeEditedParameterEntity (this.tileContainer, this.DataContext, this.Entity, tabPageName);
		}


		private void CreateUIMain(Epsitec.Cresus.Core.UIBuilder builder)
		{
			var tile = builder.CreateEditionTile ();

			builder.CreateTextField (tile, 80, "Code", Marshaler.Create (() => this.Entity.Code, x => this.Entity.Code = x));
			builder.CreateTextField (tile, 0, "Nom", Marshaler.Create (() => this.Entity.Name, x => this.Entity.Name = x));
			builder.CreateAutoCompleteTextField (tile, 0, "Cardinalité", Marshaler.Create (this.Entity, x => x.Cardinality, (x, v) => x.Cardinality = v), BusinessLogic.Enumerations.GetGetAllPossibleItemsValueCardinality (), x => UIBuilder.FormatText (x.Values[0]));

			builder.CreateMargin (tile, horizontalSeparator: true);

			var group = builder.CreateGroup (tile, "Contenu de l'énumération");
			this.parameterController = new EnumValueArticleParameterController (this.tileContainer, this.Entity);
			this.parameterController.CreateUI (group);

			builder.CreateTextField (tile, 0, "Valeur", Marshaler.Create (() => this.Value, x => this.Value = x));
			builder.CreateTextField (tile, 0, "Description courte", Marshaler.Create (() => this.ShortDescription, x => this.ShortDescription = x));
			builder.CreateTextFieldMulti (tile, 78, "Description longue", Marshaler.Create (() => this.LongDescription, x => this.LongDescription = x));

			builder.CreateMargin (tile, horizontalSeparator: true);

			builder.CreateTextField (tile, 80, "Valeur par défaut", Marshaler.Create (() => this.Entity.DefaultValue, x => this.Entity.DefaultValue = x));
		}


		private string Value
		{
			get
			{
				return this.parameterController.SelectedValue;
			}
			set
			{
				this.parameterController.SelectedValue = value;
			}
		}

		private string ShortDescription
		{
			get
			{
				return this.parameterController.SelectedShortDescription;
			}
			set
			{
				this.parameterController.SelectedShortDescription = value;
			}
		}

		private string LongDescription
		{
			get
			{
				return this.parameterController.SelectedLongDescription;
			}
			set
			{
				this.parameterController.SelectedLongDescription = value;
			}
		}


		protected override EditionStatus GetEditionStatus()
		{
			return EditionStatus.Valid;
		}


		private TileContainer							tileContainer;
		private Epsitec.Common.Widgets.FrameBox			tabBookContainer;
		private EnumValueArticleParameterController		parameterController;
	}
}

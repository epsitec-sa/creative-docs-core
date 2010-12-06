﻿//	Copyright © 2010, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Daniel ROUX, Maintainer: Daniel ROUX

using Epsitec.Common.Support.EntityEngine;
using Epsitec.Common.Types;
using Epsitec.Common.Types.Converters;

using Epsitec.Cresus.Core.Widgets;
using Epsitec.Cresus.Core.Entities;
using Epsitec.Cresus.Core.Controllers;
using Epsitec.Cresus.Core.Controllers.DataAccessors;

using System.Collections.Generic;
using System.Linq;

namespace Epsitec.Cresus.Core.Controllers.SummaryControllers
{
	[ControllerSubType (5)]
	public class SummaryPriceCalculatorListViewController : SummaryViewController<ArticleDefinitionEntity>
	{
		public SummaryPriceCalculatorListViewController(string name, ArticleDefinitionEntity entity)
			: base (name, entity)
		{
		}


		protected override void CreateUI()
		{
			using (var data = TileContainerController.Setup (this))
			{
				this.CreateUIPriceCalculators (data);
			}
		}


		private void CreateUIPriceCalculators(SummaryDataItems data)
		{
			data.Add (
				new SummaryData
				{
					AutoGroup    = true,
					Name		 = "PriceCalculators",
					IconUri		 = "Data.Group",
					Title		 = TextFormatter.FormatText ("Tous les calculateurs de prix connus"),
					CompactTitle = TextFormatter.FormatText ("Tous les calculateurs de prix connus"),
					Text		 = CollectionTemplate.DefaultEmptyText,
				});

			var template = new CollectionTemplate<PriceCalculatorEntity> ("PriceCalculators", this.BusinessContext);

			template.DefineText        (x => x.GetSummary ());
			template.DefineCompactText (x => x.GetCompactSummary ());

			data.Add (this.CreateCollectionAccessor (template));
		}
	}
}

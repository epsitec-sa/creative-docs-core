//	Copyright © 2010, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Daniel ROUX, Maintainer: Daniel ROUX

using Epsitec.Common.Widgets;
using Epsitec.Common.Support;
using Epsitec.Common.Types;
using Epsitec.Common.Drawing;

using Epsitec.Cresus.Core.Entities;
using Epsitec.Cresus.Core.Business.Finance.PriceCalculators;

using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;

namespace Epsitec.Cresus.Core.TableDesigner
{
	public class MainController
	{
		public MainController(Core.Business.BusinessContext businessContext, PriceCalculatorEntity priceCalculatorEntity, ArticleDefinitionEntity articleDefinitionEntity)
		{
			this.businessContext         = businessContext;
			this.priceCalculatorEntity   = priceCalculatorEntity;
			this.articleDefinitionEntity = articleDefinitionEntity;

			this.table = this.priceCalculatorEntity.GetPriceTable ();

			//	Extrait les dimensions.
			this.dimensions = new List<DesignerDimension> ();
			this.ExtractDimensions ();

			//	Extrait les valeurs.
			this.values = new DesignerValues ();
			this.ExtractValues ();
		}

		private void ExtractDimensions()
		{
			foreach (var dimension in this.table.Dimensions)
			{
				var dd = new DesignerDimension (dimension);
				dd.Name = this.GetDimensionName (dimension.Code);

				this.dimensions.Add (dd);
			}
		}

		private void ExtractValues()
		{
			foreach (var pair in this.table.Data)
			{
				int[] key = this.table.GetIndexesKey (pair.Key);
				decimal value = pair.Value;

				this.values.SetValue (key, value);
			}
		}


		public void CreateUI(Widget parent)
		{
			var tabBook = new TabBook
			{
				Parent = parent,
				Arrows = TabBookArrows.Right,
				Dock = DockStyle.Fill,
			};

			//	Crée les onglets.
			var basePage = new TabPage
			{
				Name = "base",
				TabTitle = "Général",
			};

			var dimensionsPage = new TabPage
			{
				Name = "dimensions",
				TabTitle = "Définition des axes",
			};

			var valuesPage = new TabPage
			{
				Name = "values",
				TabTitle = "Tabelles de prix",
			};

			tabBook.Items.Add (basePage);
			tabBook.Items.Add (dimensionsPage);
			tabBook.Items.Add (valuesPage);

			tabBook.ActivePage = basePage;

			//	Peuple les onglets.
			var bc = new BaseController (this.priceCalculatorEntity);
			bc.CreateUI (basePage);

			var dc = new DimensionsController (this.articleDefinitionEntity, this.dimensions);
			dc.CreateUI (dimensionsPage);

			var tc = new TableController (this.dimensions, this.values);
			tc.CreateUI (valuesPage);

			//	Connection des événements.
			tabBook.ActivePageChanged += delegate
			{
				if (tabBook.ActivePage.Name == "base")
				{
					bc.Update ();
				}

				if (tabBook.ActivePage.Name == "dimensions")
				{
					dc.Update ();
				}

				if (tabBook.ActivePage.Name == "values")
				{
					tc.Update ();
				}
			};
		}


		public void SaveDesign()
		{
		}


		private string GetDimensionName(string code)
		{
			var p = this.articleDefinitionEntity.ArticleParameterDefinitions.Where (x => x.Code == code).FirstOrDefault ();

			if (p == null)
			{
				return "?";
			}
			else
			{
				return p.Name.ToString ();
			}
		}


		private readonly Core.Business.BusinessContext		businessContext;
		private readonly PriceCalculatorEntity				priceCalculatorEntity;
		private readonly ArticleDefinitionEntity			articleDefinitionEntity;
		private readonly DimensionTable						table;
		private readonly List<DesignerDimension>			dimensions;
		private readonly DesignerValues						values;
	}
}

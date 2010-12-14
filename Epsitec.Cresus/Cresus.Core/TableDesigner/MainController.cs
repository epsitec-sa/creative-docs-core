//	Copyright © 2010, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Daniel ROUX, Maintainer: Daniel ROUX

using Epsitec.Common.Widgets;
using Epsitec.Common.Support;
using Epsitec.Common.Types;
using Epsitec.Common.Drawing;
using Epsitec.Common.Dialogs;

using Epsitec.Cresus.Core.Entities;
using Epsitec.Cresus.Core.Business.Finance.PriceCalculators;

using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;

namespace Epsitec.Cresus.Core.TableDesigner
{
	public class MainController
	{
		public MainController(Window window, Core.Business.BusinessContext businessContext, PriceCalculatorEntity priceCalculatorEntity, ArticleDefinitionEntity articleDefinitionEntity)
		{
			this.window                  = window;
			this.businessContext         = businessContext;
			this.priceCalculatorEntity   = priceCalculatorEntity;
			this.articleDefinitionEntity = articleDefinitionEntity;

			this.dimensionTable = this.priceCalculatorEntity.GetPriceTable ();

			this.table = new DesignerTable ();
			this.ExtractDimensions ();
			this.ExtractValues ();

			if (!this.CheckDimensions ())
			{
				string message = "Un ou plusieurs axes des tabelles de prix ne correspondent plus aux<br/>" +
								 "paramètres de l'article. En conséquence, tous les prix seront effacés.";
				
				MessageDialog.ShowMessage (message, this.window);

				this.table.Values.Clear ();
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

			var footer = new FrameBox
			{
				Parent = parent,
				Dock = DockStyle.Bottom,
				Margins = new Margins (0, 0, 10, 0),
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

			//?tabBook.Items.Add (basePage);
			tabBook.Items.Add (dimensionsPage);
			tabBook.Items.Add (valuesPage);

			if (this.table.Dimensions.Count == 0)
			{
				tabBook.ActivePage = dimensionsPage;
			}
			else
			{
				tabBook.ActivePage = valuesPage;
			}

			//	Peuple les onglets.
			var bc = new BaseController (this.priceCalculatorEntity);
			bc.CreateUI (basePage);

			var dc = new DimensionsController (this.articleDefinitionEntity, this.table);
			dc.CreateUI (dimensionsPage);

			var tc = new TableController (this.table);
			tc.CreateUI (valuesPage);

			//	Peuple le pied de page.
			var cancelButton = new Button
			{
				Parent = footer,
				Text = "Annuler",
				ButtonStyle = Common.Widgets.ButtonStyle.DefaultCancel,
				Dock = DockStyle.Right,
				Margins = new Margins (10, 0, 0, 0),
			};

			var acceptButton = new Button
			{
				Parent = footer,
				Text = "D'accord",
				ButtonStyle = Common.Widgets.ButtonStyle.DefaultAccept,
				Dock = DockStyle.Right,
				Margins = new Margins (10, 0, 0, 0),
			};

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

			acceptButton.Clicked += delegate
			{
				this.SaveDesign ();
				this.CloseWindow ();
			};

			cancelButton.Clicked += delegate
			{
				this.CloseWindow ();
			};
		}


		private void CloseWindow()
		{
			this.window.Hide ();
			this.window.Close ();
		}

		public void SaveDesign()
		{
			var dimensionTable = this.CollectDimensionsAndValues ();
			this.priceCalculatorEntity.SetPriceTable (dimensionTable);
		}


		private bool CheckDimensions()
		{
			//	Vérifie si toutes les dimensions correspondent à un paramètre de l'article.
			//	Si non, la ou les dimensions concernées sont supprimées, et l'appel retourne false.
			bool ok = true;
			int i = 0;

			while (i < this.table.Dimensions.Count)
			{
				var dimension = this.table.Dimensions[i];
				string code = dimension.Code;
				var p = this.articleDefinitionEntity.ArticleParameterDefinitions.Where (x => x.Code == code).FirstOrDefault ();

				if (p == null)
				{
					this.table.Dimensions.RemoveAt (i);
					ok = false;
				}
				else
				{
					i++;
				}
			}

			return ok;
		}


		#region Extract and collect dimensions and values
		private void ExtractDimensions()
		{
			if (this.dimensionTable != null)
			{
				foreach (var dimension in this.dimensionTable.Dimensions)
				{
					var dd = new DesignerDimension (dimension);

					dd.Name        = this.GetDimensionName (dimension.Code);
					dd.Description = this.GetDimensionDescription (dimension.Code);

					this.table.Dimensions.Add (dd);
				}
			}
		}

		private void ExtractValues()
		{
			if (this.dimensionTable != null)
			{
				foreach (var pair in this.dimensionTable.DefinedEntries)
				{
					int[] key = this.dimensionTable.GetIndexesFromKey (pair.Key);
					decimal value = pair.Value;

					this.table.Values.SetValue (key, value);
				}
			}
		}

		private DimensionTable CollectDimensionsAndValues()
		{
			this.table.CleanUp ();

			var abstractDimensions = new List<AbstractDimension> ();

			foreach (var dimension in this.table.Dimensions)
			{
				if (dimension.HasDecimal)
				{
					var n = new NumericDimension (dimension.Code, dimension.RoundingMode, dimension.DecimalPoints);
					abstractDimensions.Add (n);
				}
				else
				{
					var c = new CodeDimension (dimension.Code, dimension.Points);
					abstractDimensions.Add (c);
				}
			}

			var dimensionTable = new DimensionTable (abstractDimensions.ToArray ());

			foreach (var pair in this.table.Values.Data)
			{
				string intKey = pair.Key;
				decimal value = pair.Value;

				string[] stringKey = this.table.IntKeyToStringKeyArray (intKey);

				dimensionTable[stringKey] = value;
			}

			return dimensionTable;
		}

		private FormattedText GetDimensionName(string code)
		{
			var p = this.articleDefinitionEntity.ArticleParameterDefinitions.Where (x => x.Code == code).FirstOrDefault ();

			if (p == null)
			{
				return "?";
			}
			else
			{
				return p.Name;
			}
		}
		
		private FormattedText GetDimensionDescription(string code)
		{
			var p = this.articleDefinitionEntity.ArticleParameterDefinitions.Where (x => x.Code == code).FirstOrDefault ();

			if (p == null)
			{
				return "";
			}
			else
			{
				return p.Description;
			}
		}
		#endregion


		private readonly Window								window;
		private readonly Core.Business.BusinessContext		businessContext;
		private readonly PriceCalculatorEntity				priceCalculatorEntity;
		private readonly ArticleDefinitionEntity			articleDefinitionEntity;
		private readonly DimensionTable						dimensionTable;
		private readonly DesignerTable						table;
	}
}

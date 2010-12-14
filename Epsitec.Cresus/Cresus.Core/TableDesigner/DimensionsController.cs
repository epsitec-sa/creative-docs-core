//	Copyright © 2010, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Daniel ROUX, Maintainer: Daniel ROUX

using Epsitec.Common.Widgets;
using Epsitec.Common.Support;
using Epsitec.Common.Types;
using Epsitec.Common.Drawing;
using Epsitec.Common.Dialogs;

using Epsitec.Cresus.Core.Entities;
using Epsitec.Cresus.Core.Business.Finance.PriceCalculators;
using Epsitec.Cresus.Core.Widgets;

using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;

namespace Epsitec.Cresus.Core.TableDesigner
{
	public class DimensionsController
	{
		public DimensionsController(ArticleDefinitionEntity articleDefinitionEntity, DesignerTable table)
		{
			this.articleDefinitionEntity = articleDefinitionEntity;
			this.table = table;
		}

		public void CreateUI(Widget parent)
		{
			var frame = new FrameBox
			{
				Parent = parent,
				Dock = DockStyle.Fill,
			};

			var dimensionsPane = new FrameBox
			{
				Parent = frame,
				PreferredWidth = 200+1+TileArrow.Breadth,
				Dock = DockStyle.Left,
				Margins = new Margins (10, 10, 10, 10),
			};

			var pointsPane = new FrameBox
			{
				Parent = frame,
				PreferredWidth = 200,
				Dock = DockStyle.Left,
				Margins = new Margins (0, 10, 10, 10),
			};

			this.CreateDimensionUI (dimensionsPane);
			this.CreatePointUI (pointsPane);

			this.UpdateDimensionsList ();
			this.UpdateRoundingPane ();
			this.UpdatePointsList ();
		}

		public void Update()
		{
		}


		private void CreateDimensionUI(Widget parent)
		{
			new StaticText
			{
				Parent = parent,
				PreferredHeight = 20,
				Dock = DockStyle.Top,
				Text = "<font size=\"16\">Axes</font>",
				Margins = new Margins (0, 0, 0, 10),
			};

			//	Crée la liste.
			var tile = new ArrowedFrame
			{
				Parent = parent,
				ArrowDirection = Direction.Right,
				Dock = DockStyle.Fill,
				Padding = new Margins (5, TileArrow.Breadth+5, 5, 5),
			};

			tile.SetSelected (true);  // conteneur orange

			this.dimensionsTable = new CellTable
			{
				Parent = tile,
				StyleH = CellArrayStyles.ScrollMagic | CellArrayStyles.Separator,
				StyleV = CellArrayStyles.ScrollMagic | CellArrayStyles.Separator | CellArrayStyles.SelectLine,
				DefHeight = 24,
				Dock = DockStyle.Fill,
			};

			//	Crée le pied de page.
			this.dimensionsRoundingPane = new GroupBox
			{
				Parent = parent,
				Text = "Type d'arrondi",
				Dock = DockStyle.Bottom,
				Margins = new Margins (0, TileArrow.Breadth, 10, 0),
				Padding = new Margins (10),
			};

			this.dimensionsRadioRoundingNone = new RadioButton
			{
				Parent = this.dimensionsRoundingPane,
				Text = "Pas d'arrondi",
				Name = "None",
				Dock = DockStyle.Top,
			};

			this.dimensionsRadioRoundingNearest = new RadioButton
			{
				Parent = this.dimensionsRoundingPane,
				Text = "Valeur la plus proche",
				Name = "Nearest",
				Dock = DockStyle.Top,
			};

			this.dimensionsRadioRoundingDown = new RadioButton
			{
				Parent = this.dimensionsRoundingPane,
				Text = "Valeur inférieure",
				Name = "Down",
				Dock = DockStyle.Top,
			};

			this.dimensionsRadioRoundingUp = new RadioButton
			{
				Parent = this.dimensionsRoundingPane,
				Text = "Valeur supérieure",
				Name = "Up",
				Dock = DockStyle.Top,
			};

			//	Connexion des événements.
			this.dimensionsTable.SelectionChanged += delegate
			{
				this.DimensionSelectedItemChanged ();
			};

			this.dimensionsRadioRoundingNone   .Clicked += new EventHandler<MessageEventArgs> (this.HandleDimensionsRadioRoundingClicked);
			this.dimensionsRadioRoundingNearest.Clicked += new EventHandler<MessageEventArgs> (this.HandleDimensionsRadioRoundingClicked);
			this.dimensionsRadioRoundingUp     .Clicked += new EventHandler<MessageEventArgs> (this.HandleDimensionsRadioRoundingClicked);
			this.dimensionsRadioRoundingDown   .Clicked += new EventHandler<MessageEventArgs> (this.HandleDimensionsRadioRoundingClicked);
		}

		private void CreatePointUI(Widget parent)
		{
			new StaticText
			{
				Parent = parent,
				PreferredHeight = 20,
				Dock = DockStyle.Top,
				Text = "<font size=\"16\">Points sur l'axe</font>",
				Margins = new Margins (0, 0, 0, 10),
			};

			this.CreatePointsToolbarUI (parent);

			this.pointsScrollList = new ScrollList
			{
				Parent = parent,
				Dock = DockStyle.Fill,
			};

			//	Crée le pied de page.
			var footer = new FrameBox
			{
				Parent = parent,
				Dock = DockStyle.Bottom,
				Margins = new Margins (0, 0, 10, 0),
			};

			var label = new StaticText
			{
				Parent = footer,
				Text = "Valeur du point",
				PreferredWidth = 85,
				Dock = DockStyle.Left,
			};

			this.pointValueField = new TextFieldEx
			{
				Parent = footer,
				DefocusAction = DefocusAction.AutoAcceptOrRejectEdition,
				SwallowEscapeOnRejectEdition = true,
				SwallowReturnOnAcceptEdition = true,
				Dock = DockStyle.Fill,
			};

			//	Connexion des événements.
			this.pointsScrollList.SelectedItemChanged += delegate
			{
				this.PointSelectedItemChanged ();
			};

			this.pointValueField.EditionAccepted += delegate
			{
				this.ChangePointValue ();
			};
		}

		private void CreatePointsToolbarUI(Widget parent)
		{
			//	Crée la toolbar.
			double buttonSize = 19;

			this.pointsToolbar = UIBuilder.CreateMiniToolbar (parent, buttonSize);
			this.pointsToolbar.Margins = new Margins (0, 0, 0, -1);

			this.addPointsButton = new GlyphButton
			{
				Parent = pointsToolbar,
				PreferredSize = new Size (buttonSize*2+1, buttonSize),
				GlyphShape = GlyphShape.Plus,
				Margins = new Margins (0, 0, 0, 0),
				Dock = DockStyle.Left,
			};

			this.removePointsButton = new GlyphButton
			{
				Parent = pointsToolbar,
				PreferredSize = new Size (buttonSize, buttonSize),
				GlyphShape = GlyphShape.Minus,
				Margins = new Margins (1, 0, 0, 0),
				Dock = DockStyle.Left,
			};

			this.upPointsButton = new GlyphButton
			{
				Parent = pointsToolbar,
				PreferredSize = new Size (buttonSize, buttonSize),
				GlyphShape = GlyphShape.ArrowUp,
				Margins = new Margins (10, 0, 0, 0),
				Dock = DockStyle.Left,
			};

			this.downPointsButton = new GlyphButton
			{
				Parent = pointsToolbar,
				PreferredSize = new Size (buttonSize, buttonSize),
				GlyphShape = GlyphShape.ArrowDown,
				Margins = new Margins (1, 0, 0, 0),
				Dock = DockStyle.Left,
			};

			ToolTip.Default.SetToolTip (this.addPointsButton,    "Crée un nouveau point");
			ToolTip.Default.SetToolTip (this.removePointsButton, "Supprime le point sélectionné");
			ToolTip.Default.SetToolTip (this.upPointsButton,     "Monte le point dans la liste");
			ToolTip.Default.SetToolTip (this.downPointsButton,   "Descend le point dans la liste");

			//	Connexion des événements.
			this.addPointsButton.Clicked += delegate
			{
				this.PointItemInserted ();
			};

			this.removePointsButton.Clicked += delegate
			{
				this.PointItemRemoved ();
			};

			this.upPointsButton.Clicked += delegate
			{
				this.PointItemMoved (-1);
			};

			this.downPointsButton.Clicked += delegate
			{
				this.PointItemMoved (1);
			};
		}


		private void UpdateDimensionsList()
		{
			int count = this.articleDefinitionEntity.ArticleParameterDefinitions.Count;

			this.dimensionsTable.SetArraySize (2, count);

			this.dimensionsTable.SetWidthColumn (0, 24);
			this.dimensionsTable.SetWidthColumn (1, 200-24-10);

			for (int row = 0; row < count; row++)
			{
				if (this.dimensionsTable[0, row].IsEmpty)
				{
					string code = this.articleDefinitionEntity.ArticleParameterDefinitions[row].Code;

					var button = new CheckButton
					{
						ActiveState = this.table.GetDimension (code) == null ? ActiveState.No : ActiveState.Yes,
						AutoToggle = false,
						Name = code,
						Dock = DockStyle.Fill,
						Margins = new Margins (5, 0, 0, 0),
					};

					button.Clicked += new EventHandler<MessageEventArgs> (this.HandleCheckButtonClicked);

					this.dimensionsTable[0, row].Insert (button);
				}

				if (this.dimensionsTable[1, row].IsEmpty)
				{
					var label = new StaticText
					{
						ContentAlignment = Common.Drawing.ContentAlignment.MiddleLeft,
						FormattedText = DimensionsController.GetParameterDescription (this.articleDefinitionEntity.ArticleParameterDefinitions[row]),
						Dock = DockStyle.Fill,
						Margins = new Margins (5, 5, 0, 0),
					};

					this.dimensionsTable[1, row].Insert (label);
				}
			}
		}

		private void UpdateRoundingPane()
		{
			var dimension = this.GetDimension;

			if (dimension != null && dimension.HasDecimal)
			{
				this.dimensionsRoundingPane.Visibility = true;

				this.dimensionsRadioRoundingNone   .ActiveState = (dimension.RoundingMode == RoundingMode.None   ) ? ActiveState.Yes : ActiveState.No;
				this.dimensionsRadioRoundingNearest.ActiveState = (dimension.RoundingMode == RoundingMode.Nearest) ? ActiveState.Yes : ActiveState.No;
				this.dimensionsRadioRoundingUp     .ActiveState = (dimension.RoundingMode == RoundingMode.Up     ) ? ActiveState.Yes : ActiveState.No;
				this.dimensionsRadioRoundingDown   .ActiveState = (dimension.RoundingMode == RoundingMode.Down   ) ? ActiveState.Yes : ActiveState.No;
			}
			else
			{
				this.dimensionsRoundingPane.Visibility = false;
			}
		}

		private void UpdatePointsList(int? sel = null)
		{
			if (sel == null)
			{
				sel = this.pointsScrollList.SelectedItemIndex;
			}

			this.pointsScrollList.Items.Clear ();

			var dimension = this.GetDimension;
			var list = this.GetPoints;

			if (list == null)
			{
				this.pointsToolbar.Visibility = false;
			}
			else
			{
				this.pointsToolbar.Visibility = true;

				if (dimension.HasDecimal)
				{
					//	Une liste numérique est intrinsèquement ordonnée. Cela n'a donc pas de sens
					//	de pouvoir modifier l'ordre.
					this.upPointsButton.Visibility = false;
					this.downPointsButton.Visibility = false;
				}
				else
				{
					this.upPointsButton.Visibility = true;
					this.downPointsButton.Visibility = true;
				}

				foreach (var value in list)
				{
					this.pointsScrollList.Items.Add (value);
				}
			}

			this.pointsScrollList.SelectedItemIndex = sel.Value;
			this.UpdateAfterPointSelected ();
		}

		private void UpdateAfterDimensionSelected()
		{
			this.UpdateRoundingPane ();
			this.UpdatePointsList (-1);
		}

		private void UpdateAfterPointSelected()
		{
			int sel = this.pointsScrollList.SelectedItemIndex;
			var dimension = this.GetDimension;

			if (sel == -1 || dimension == null)
			{
				this.pointValueField.Enable = false;
				this.pointValueField.Text = null;
			}
			else
			{
				this.pointValueField.Enable = true;
				this.pointValueField.Text = this.GetDimension.Points.ElementAt (sel);
			}

			this.removePointsButton.Enable = sel != -1;
			this.upPointsButton.Enable     = sel > 0;
			this.downPointsButton.Enable   = sel != -1 && sel < dimension.Points.Count-1;
		}


		private void HandleDimensionsRadioRoundingClicked(object sender, MessageEventArgs e)
		{
			var radio = sender as RadioButton;
			var dimension = this.GetDimension;

			switch (radio.Name)
			{
				case "None":
					dimension.RoundingMode = RoundingMode.None;
					break;

				case "Nearest":
					dimension.RoundingMode = RoundingMode.Nearest;
					break;

				case "Up":
					dimension.RoundingMode = RoundingMode.Up;
					break;

				case "Down":
					dimension.RoundingMode = RoundingMode.Down;
					break;
			}
		}


		private void HandleCheckButtonClicked(object sender, MessageEventArgs e)
		{
			var button = sender as CheckButton;

			if (button.ActiveState == ActiveState.No)
			{
				button.ActiveState = ActiveState.Yes;

				if (!this.CreateDimension (button.Name))
				{
					button.ActiveState = ActiveState.No;
				}
			}
			else
			{
				button.ActiveState = ActiveState.No;

				if (!this.RemoveDimension (button.Name))
				{
					button.ActiveState = ActiveState.Yes;
				}
			}
		}

		private bool CreateDimension(string code)
		{
			if (this.table.Values.Data.Count > 0)
			{
				string message = "Vous allez créer un nouvel axe dans les tabelles de prix.<br/>" +
								 "Tous les prix seront effacés.<br/>" +
								 "Est-ce bien ce que vous désirez ?";

				if (MessageDialog.ShowQuestion (message, this.dimensionsTable.Window) != DialogResult.Yes)
				{
					return false;
				}
			}

			var p = this.articleDefinitionEntity.ArticleParameterDefinitions.Where (x => x.Code == code).FirstOrDefault ();

			if (p == null)
			{
				return false;
			}

			var dimension = new DesignerDimension (p);
			this.table.Dimensions.Add (dimension);
			this.table.Values.Clear ();

			this.dimensionsTable.DeselectAll ();
			this.dimensionsTable.SelectRow (this.GetArticleParameterIndex (code), true);

			this.UpdatePointsList (-1);
			return true;
		}

		private bool RemoveDimension(string code)
		{
			if (this.table.Values.Data.Count > 0)
			{
				string message = "Vous allez supprimer un axe dans les tabelles de prix.<br/>" +
								 "Tous les prix seront effacés.<br/>" +
								 "Est-ce bien ce que vous désirez ?";

				if (MessageDialog.ShowQuestion (message, this.dimensionsTable.Window) != DialogResult.Yes)
				{
					return false;
				}
			}

			var dimension = this.table.GetDimension (code);

			if (dimension == null)
			{
				return false;
			}

			this.table.Dimensions.Remove (dimension);
			this.table.Values.Clear ();

			this.UpdatePointsList (-1);
			return true;
		}

		private void DimensionSelectedItemChanged()
		{
			this.UpdateAfterDimensionSelected ();
		}


		private void PointSelectedItemChanged()
		{
			this.UpdateAfterPointSelected ();
		}

		private void PointItemInserted()
		{
			int sel = this.pointsScrollList.SelectedItemIndex;
			var dimension = this.GetDimension;

			var data = this.table.ExportValues ();

			if (dimension.HasDecimal)
			{
				dimension.Points.Add ("0");
				sel = dimension.Sort (dimension.Points.Count-1);
			}
			else
			{
				sel++;
				dimension.Points.Insert (sel, "Nouveau");
			}

			this.table.ImportValues (data);

			this.UpdatePointsList (sel);

			this.pointValueField.SelectAll ();
			this.pointValueField.Focus ();
		}

		private void PointItemRemoved()
		{
			int sel = this.pointsScrollList.SelectedItemIndex;
			var dimension = this.GetDimension;

			if (sel != -1)
			{
				var data = this.table.ExportValues ();
				dimension.Points.RemoveAt (sel);
				this.table.ImportValues (data);

				if (sel >= dimension.Points.Count)
				{
					sel = dimension.Points.Count-1;
				}

				this.UpdatePointsList (sel);
			}
		}

		private void PointItemMoved(int direction)
		{
			int sel = this.pointsScrollList.SelectedItemIndex;
			var dimension = this.GetDimension;

			if (sel != -1)
			{
				var data = this.table.ExportValues ();

				var t = dimension.Points[sel];
				dimension.Points.RemoveAt (sel);
				dimension.Points.Insert (sel+direction, t);

				this.table.ImportValues (data);

				this.UpdatePointsList (sel+direction);
			}
		}

		private void ChangePointValue()
		{
			int sel = this.pointsScrollList.SelectedItemIndex;
			var dimension = this.GetDimension;
			string value = this.pointValueField.Text;

			if (sel != -1)
			{
				var data = this.table.ExportValues ();

				if (dimension.HasDecimal)
				{
					decimal dv;
					if (decimal.TryParse (value, out dv))
					{
						dimension.Points[sel] = value;
						sel = dimension.Sort (sel);
					}
				}
				else
				{
					dimension.Points[sel] = value;
				}

				this.table.ImportValues (data);
			}

			this.UpdatePointsList (sel);
		}


		private int GetArticleParameterIndex(string code)
		{
			for (int i=0; i<this.articleDefinitionEntity.ArticleParameterDefinitions.Count (); i++)
			{
				if (this.articleDefinitionEntity.ArticleParameterDefinitions[i].Code == code)
				{
					return i;
				}
			}

			return -1;
		}

		private List<string> GetPoints
		{
			get
			{
				var dimension = this.GetDimension;

				if (dimension == null)
				{
					return null;
				}
				else
				{
					return dimension.Points.ToList ();
				}
			}
		}

		private DesignerDimension GetDimension
		{
			get
			{
				int sel = this.dimensionsTable.SelectedRow;
				if (sel == -1)
				{
					return null;
				}

				var button = this.dimensionsTable[0, sel].Children[0] as CheckButton;
				if (button == null || button.ActiveState != ActiveState.Yes)
				{
					return null;
				}

				return this.table.GetDimension (button.Name);
			}
		}


		private static FormattedText GetParameterDescription(AbstractArticleParameterDefinitionEntity parameter)
		{
			var desc = TextFormatter.FormatText ("(~", parameter.Description, "~)");
			return TextFormatter.FormatText (parameter.Name, desc);
		}


		private readonly ArticleDefinitionEntity			articleDefinitionEntity;
		private readonly DesignerTable						table;

		private CellTable									dimensionsTable;
		private GroupBox									dimensionsRoundingPane;
		private RadioButton									dimensionsRadioRoundingNone;
		private RadioButton									dimensionsRadioRoundingNearest;
		private RadioButton									dimensionsRadioRoundingUp;
		private RadioButton									dimensionsRadioRoundingDown;

		private FrameBox									pointsToolbar;
		private GlyphButton									addPointsButton;
		private GlyphButton									removePointsButton;
		private GlyphButton									upPointsButton;
		private GlyphButton									downPointsButton;
		private ScrollList									pointsScrollList;
		
		private TextFieldEx									pointValueField;
	}
}

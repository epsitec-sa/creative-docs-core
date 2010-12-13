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
	public class TableController
	{
		public TableController(DesignerTable table)
		{
			this.table = table;
		}

		public void CreateUI(Widget parent)
		{
			var frame = new FrameBox
			{
				Parent = parent,
				Dock = DockStyle.Fill,
				Margins = new Margins (10),
			};

			this.dimensionsPane = new FrameBox
			{
				Parent = frame,
				PreferredWidth = 24+24+80+100+10+1+TileArrow.Breadth,
				Dock = DockStyle.Left,
				Margins = new Margins (0, 10, 0, 0),
			};

			var rightPane = new FrameBox
			{
				Parent = frame,
				Dock = DockStyle.Fill,
				Margins = new Margins (0, 0, 0, 0),
			};

			var valuesPane = new FrameBox
			{
				Parent = rightPane,
				Dock = DockStyle.Fill,
				Margins = new Margins (0, 0, 0, 0),
			};

			this.toolbar = new FrameBox
			{
				Parent = rightPane,
				Dock = DockStyle.Bottom,
				Margins = new Margins (0, 0, 10, 0),
				Visibility = false,
			};

			this.CreateDimensionsTableUI (this.dimensionsPane);
			this.CreateValuesTableUI (valuesPane);
			this.CreateToolbarUI (this.toolbar);

			this.toolbarButton = new GlyphButton
			{
				Parent = rightPane,
				ButtonStyle = ButtonStyle.Slider,
				GlyphShape = GlyphShape.TriangleDown,
				AutoFocus = false,
				PreferredSize = new Size (20, 20),
				Anchor = AnchorStyles.BottomLeft,
				Margins = new Margins (4, 0, 0, 4),
			};

			ToolTip.Default.SetToolTip (this.toolbarButton, "Montre ou cache le panneau des importations/exportations");

			//	Connexion des événements.
			this.toolbarButton.Clicked += delegate
			{
				this.toolbar.Visibility = !this.toolbar.Visibility;
				this.toolbarButton.GlyphShape = this.toolbar.Visibility ? GlyphShape.TriangleUp : GlyphShape.TriangleDown;
			};

			this.Update ();
		}

		public void Update()
		{
			if (this.table.Dimensions.Count == 0)
			{
				this.columnDimensionSelected = -1;
				this.rowDimensionSelected    = -1;
			}
			else if (this.table.Dimensions.Count == 1)
			{
				//	Avec une seule dimension, on préfère utiliser les lignes.
				this.columnDimensionSelected = -1;
				this.rowDimensionSelected    = 0;
			}
			else
			{
				this.columnDimensionSelected = 0;
				this.rowDimensionSelected    = 1;
			}

			this.UpdateDimensionsTable ();
			this.RefreshDimensionsTable ();

			this.UpdateValuesTable ();
		}


		private void CreateDimensionsTableUI(Widget parent)
		{
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
				StyleH = CellArrayStyles.ScrollMagic | CellArrayStyles.Separator | CellArrayStyles.Header,
				StyleV = CellArrayStyles.ScrollMagic | CellArrayStyles.Separator,
				DefHeight = 24,
				Dock = DockStyle.Fill,
			};
		}

		private void CreateValuesTableUI(Widget parent)
		{
			var left = new FrameBox
			{
				Parent = parent,
				PreferredWidth = TableController.legendHeight,
				Dock = DockStyle.Left,
				Margins = new Margins (0, -1, 0, 0),
			};

			var swapBox = new FrameBox
			{
				Parent = left,
				PreferredSize = new Size (TableController.legendHeight-3, TableController.legendHeight-3),
				Dock = DockStyle.Top,
				Margins = new Margins (0, 3, 0, 2),
				Padding = new Margins (4, 1, 4, 1),
			};

			this.swapButton = new GlyphButton
			{
				Parent = swapBox,
				ButtonStyle = ButtonStyle.Slider,
				GlyphShape = GlyphShape.HorizontalMove,
				AutoFocus = false,
				Dock = DockStyle.Fill,
			};

			ToolTip.Default.SetToolTip (this.swapButton, "Permute les lignes et les colonnes");

			var rowsHeader = new FrameBox
			{
				Parent = left,
				DrawFullFrame = true,
				BackColor = Tile.SurfaceSelectedContainerColors.First (),
				PreferredWidth = TableController.legendHeight,
				Dock = DockStyle.Fill,
			};

			var right = new FrameBox
			{
				Parent = parent,
				Dock = DockStyle.Fill,
			};

			var columnsHeader = new FrameBox
			{
				Parent = right,
				DrawFullFrame = true,
				BackColor = Tile.SurfaceSelectedContainerColors.First (),
				PreferredHeight = TableController.legendHeight,
				Dock = DockStyle.Top,
				Margins = new Margins (0, 0, 0, -1),
			};

			var table = new FrameBox
			{
				Parent = right,
				DrawFullFrame = true,
				BackColor = Tile.SurfaceSelectedContainerColors.First (),
				Dock = DockStyle.Fill,
				Padding = new Margins (5),
			};

			this.valuesTable = new CellTable
			{
				Parent = table,
				DefHeight = 20,
				HeaderWidth = 80,
				Dock = DockStyle.Fill,
			};

			//	Met les textes statiques des légendes dans les bandes correspondantes.
			this.rowsLegend = new VStaticText
			{
				Parent = rowsHeader,
				ContentAlignment = Common.Drawing.ContentAlignment.MiddleCenter,
				Dock = DockStyle.Fill,
			};

			this.columnsLegend = new StaticText
			{
				Parent = columnsHeader,
				ContentAlignment = Common.Drawing.ContentAlignment.MiddleCenter,
				Dock = DockStyle.Fill,
			};

			//	Connexion des événements.
			this.swapButton.Clicked += new EventHandler<MessageEventArgs> (this.HandleSwapButtonClicked);
		}

		private void CreateToolbarUI(Widget parent)
		{
			var exportButton = new Button
			{
				Parent = parent,
				Text = "Exporter",
				Dock = DockStyle.Right,
				Margins = new Margins (10, 0, 0, 0),
			};

			var importButton = new Button
			{
				Parent = parent,
				Text = "Importer",
				Dock = DockStyle.Right,
				Margins = new Margins (10, 0, 0, 0),
			};

			//	Connexion des événements.
			exportButton.Clicked += delegate
			{
				this.Export ();
			};

			importButton.Clicked += delegate
			{
				this.Import ();
			};
		}


		private void UpdateDimensionsTable()
		{
			int count = this.table.Dimensions.Count;

			this.dimensionsPane.Visibility = (count > 2);  // inutile avec moins de 3 dimensions !

			this.dimensionsTable.SetArraySize (4, count);

			this.dimensionsTable.SetWidthColumn (0, 24);
			this.dimensionsTable.SetWidthColumn (1, 24);
			this.dimensionsTable.SetWidthColumn (2, 80);
			this.dimensionsTable.SetWidthColumn (3, 100);

			this.dimensionsTable.SetHeaderTextH (0, "C");
			this.dimensionsTable.SetHeaderTextH (1, "L");
			this.dimensionsTable.SetHeaderTextH (2, "Axe");
			this.dimensionsTable.SetHeaderTextH (3, "Valeur");

			int tabIndex = 1;

			for (int row = 0; row < count; row++)
			{
				if (this.dimensionsTable[0, row].IsEmpty)
				{
					var radio = new RadioButton
					{
						Name = string.Concat ("C", row.ToString (System.Globalization.CultureInfo.InvariantCulture)),
						Dock = DockStyle.Fill,
						Margins = new Margins (5, 0, 0, 0),
					};

					radio.Clicked += new EventHandler<MessageEventArgs> (this.HandleRadioClicked);

					this.dimensionsTable[0, row].Insert (radio);
					this.dimensionsTable[0, row].TabIndex = tabIndex++;
				}

				if (this.dimensionsTable[1, row].IsEmpty)
				{
					var radio = new RadioButton
					{
						Name = string.Concat ("R", row.ToString (System.Globalization.CultureInfo.InvariantCulture)),
						Dock = DockStyle.Fill,
						Margins = new Margins (5, 0, 0, 0),
					};

					radio.Clicked += new EventHandler<MessageEventArgs> (this.HandleRadioClicked);

					this.dimensionsTable[1, row].Insert (radio);
					this.dimensionsTable[1, row].TabIndex = tabIndex++;
				}

				if (this.dimensionsTable[2, row].IsEmpty)
				{
					var label = new StaticText
					{
						ContentAlignment = Common.Drawing.ContentAlignment.MiddleLeft,
						Dock = DockStyle.Fill,
						Margins = new Margins (5, 5, 0, 0),
					};

					this.dimensionsTable[2, row].Insert (label);
					this.dimensionsTable[2, row].TabIndex = tabIndex++;
				}

				if (!this.dimensionsTable[2, row].IsEmpty)
				{
					var label = this.dimensionsTable[2, row].Children[0] as StaticText;

					label.Text = this.table.Dimensions[row].Name.ToString ();
				}

				if (this.dimensionsTable[3, row].IsEmpty)
				{
					var combo = new TextFieldCombo
					{
						Name = row.ToString (System.Globalization.CultureInfo.InvariantCulture),
						IsReadOnly = true,
						Dock = DockStyle.Fill,
						Margins = new Margins (-1),
						TabIndex = tabIndex++,
					};

					combo.ComboClosed += new EventHandler (this.HandleComboClosed);

					this.dimensionsTable[3, row].Insert (combo);
					this.dimensionsTable[3, row].TabIndex = tabIndex++;
				}

				if (!this.dimensionsTable[3, row].IsEmpty)
				{
					var combo = this.dimensionsTable[3, row].Children[0] as TextFieldCombo;

					this.UpdateCombo (combo, row);
				}
			}
		}

		private void UpdateCombo(TextFieldCombo combo, int row)
		{
			combo.Items.Clear ();

			foreach (var point in this.table.Dimensions[row].Points)
			{
				combo.Items.Add (point);
			}

			combo.Text = this.table.Dimensions[row].Points.FirstOrDefault ();
		}

		private void RefreshDimensionsTable()
		{
			int count = this.table.Dimensions.Count;

			for (int row = 0; row < count; row++)
			{
				var radioX = this.dimensionsTable[0, row].Children[0] as RadioButton;
				var radioY = this.dimensionsTable[1, row].Children[0] as RadioButton;
				var combo  = this.dimensionsTable[3, row].Children[0] as TextFieldCombo;

				radioX.ActiveState = (this.columnDimensionSelected == row) ? ActiveState.Yes : ActiveState.No;
				radioY.ActiveState = (this.rowDimensionSelected    == row) ? ActiveState.Yes : ActiveState.No;

				combo.Visibility = (this.columnDimensionSelected != row && this.rowDimensionSelected != row);
			}
		}


		private void UpdateValuesTable()
		{
			int count = this.table.Dimensions.Count;

			this.swapButton.Visibility = (count > 1);  // inutile avec une seule dimension !

			if (count == 0)
			{
				this.valuesTable.StyleH = CellArrayStyles.Separator;
				this.valuesTable.StyleV = CellArrayStyles.Separator;
			}
			else if (count == 1)
			{
				this.valuesTable.StyleH = CellArrayStyles.Separator;
				this.valuesTable.StyleV = CellArrayStyles.ScrollNorm | CellArrayStyles.Header | CellArrayStyles.Separator;
			}
			else
			{
				this.valuesTable.StyleH = CellArrayStyles.ScrollNorm | CellArrayStyles.Header | CellArrayStyles.Separator;
				this.valuesTable.StyleV = CellArrayStyles.ScrollNorm | CellArrayStyles.Header | CellArrayStyles.Separator;
			}

			var columnsDimension = (this.columnDimensionSelected == -1) ? null : this.table.Dimensions[this.columnDimensionSelected];
			var rowsDimension    = (this.rowDimensionSelected    == -1) ? null : this.table.Dimensions[this.rowDimensionSelected];

			this.columnsLegend.Text = (columnsDimension == null) ? null : TableController.GetLegendText (columnsDimension.Name.ToString ());
			this.rowsLegend.Text    = (rowsDimension    == null) ? null : TableController.GetLegendText (rowsDimension.Name.ToString ());

			int totalColumns = (columnsDimension == null) ? 1 : columnsDimension.Points.Count ();
			int totalRows    = (rowsDimension    == null) ? 1 : rowsDimension.Points.Count ();

			this.valuesTable.SetArraySize (totalColumns, totalRows);

			for (int i = 0; i < totalColumns; i++)
			{
				this.valuesTable.SetWidthColumn (i, 80);
				this.valuesTable.SetHeaderTextH (i, (columnsDimension == null) ? null : columnsDimension.Points[i]);
			}

			for (int i = 0; i < totalRows; i++)
			{
				this.valuesTable.SetHeaderTextV (i, (rowsDimension == null) ? null : rowsDimension.Points[i]);
			}

			if (count == 0)
			{
				this.valuesTable.SetArraySize (0, 0);
				return;
			}

			int tabIndex = 1;
			for (int row = 0; row < totalRows; row++)
			{
				for (int column = 0; column < totalColumns; column++)
				{
					if (this.valuesTable[column, row].IsEmpty)
					{
						var field = new TextField
						{
							Name = string.Concat (column.ToString (System.Globalization.CultureInfo.InvariantCulture), ".", row.ToString (System.Globalization.CultureInfo.InvariantCulture)),
							ContentAlignment = Common.Drawing.ContentAlignment.MiddleRight,
							Dock = DockStyle.Fill,
							Margins = new Margins (-1),
							TabIndex = tabIndex++,
						};

						field.TextChanged += new EventHandler (this.HandleFieldTextChanged);

						this.valuesTable[column, row].Insert (field);
						this.valuesTable[column, row].TabIndex = tabIndex++;
					}

					if (!this.valuesTable[column, row].IsEmpty)
					{
						var field = this.valuesTable[column, row].Children[0] as TextField;

						field.Text = this.GetValue (this.GetKey (column, row));
					}
				}
			}
		}


		private void HandleSwapButtonClicked(object sender, MessageEventArgs e)
		{
			int t = this.columnDimensionSelected;
			this.columnDimensionSelected = this.rowDimensionSelected;
			this.rowDimensionSelected = t;

			this.RefreshDimensionsTable ();
			this.UpdateValuesTable ();
		}

		private void HandleRadioClicked(object sender, MessageEventArgs e)
		{
			var radio = sender as RadioButton;
			string column = radio.Name.Substring (0, 1);
			int row = int.Parse (radio.Name.Substring (1));

			if (column == "C")
			{
				this.columnDimensionSelected = row;
			}

			if (column == "R")
			{
				this.rowDimensionSelected = row;
			}

			this.RefreshDimensionsTable ();
			this.UpdateValuesTable ();
		}

		private void HandleComboClosed(object sender)
		{
			var combo = sender as TextFieldCombo;
			int row = int.Parse (combo.Name);

			this.UpdateValuesTable ();
		}

		private void HandleFieldTextChanged(object sender)
		{
			var field = sender as TextField;
			var parts = field.Name.Split ('.');
			int column = int.Parse (parts[0]);
			int row    = int.Parse (parts[1]);

			this.SetValue (this.GetKey (column, row), field.Text);
		}


		#region Import/export
		private void Export()
		{
			string path;
			bool useColumns, useRows;
			if (this.ExportDialog (out path, out useColumns, out useRows))
			{
				string err = this.Export (path, useColumns, useRows);
				if (!string.IsNullOrEmpty (err))
				{
					MessageDialog.ShowError (err, this.dimensionsTable.Window);
				}
			}
		}

		private void Import()
		{
			string path;
			bool useColumns, useRows;
			if (this.ImportDialog (out path, out useColumns, out useRows))
			{
				string err = this.Import (path, useColumns, useRows);
				this.UpdateValuesTable ();

				if (!string.IsNullOrEmpty (err))
				{
					MessageDialog.ShowError (err, this.dimensionsTable.Window);
				}
			}
		}

		private bool ExportDialog(out string path, out bool useColumns, out bool useRows)
		{
			string title = string.Format ("Exportation d'une tabelle de prix (axes {0} et {1})", this.table.Dimensions[this.columnDimensionSelected].Name, this.table.Dimensions[this.rowDimensionSelected].Name);
			var dialog = new Dialogs.TableDesignerFileExportDialog (this.dimensionsTable, title);

			dialog.ShowDialog ();

			if (dialog.Result != Common.Dialogs.DialogResult.Accept)
			{
				path = null;
				useColumns = false;
				useRows = false;
				return false;
			}

			dialog.PathMemorize ();

			path = dialog.FileName;
			useColumns = dialog.UseColumns;
			useRows = dialog.UseRows;
			return true;
		}

		private bool ImportDialog(out string path, out bool useColumns, out bool useRows)
		{
			string title = string.Format ("Importation d'une tabelle de prix (axes {0} et {1})", this.table.Dimensions[this.columnDimensionSelected].Name, this.table.Dimensions[this.rowDimensionSelected].Name);
			var dialog = new Dialogs.TableDesignerFileImportDialog (this.dimensionsTable, title);

			dialog.ShowDialog ();

			if (dialog.Result != Common.Dialogs.DialogResult.Accept)
			{
				path = null;
				useColumns = false;
				useRows = false;
				return false;
			}

			dialog.PathMemorize ();

			path = dialog.FileName;
			useColumns = dialog.UseColumns;
			useRows = dialog.UseRows;
			return true;
		}

		private string Export(string path, bool useColumns, bool useRows)
		{
			var lines = new List<string> ();

			//	Première lignes avec les noms des colonnes.
			if (useColumns)
			{
				var words = new List<string> ();

				if (useRows)
				{
					words.Add ("");
				}

				foreach (var s in this.table.Dimensions[this.columnDimensionSelected].Points)
				{
					words.Add (s);
				}

				lines.Add (string.Join ("\t", words));
			}

			//	Met toutes les valeurs.
			for (int r = 0; r < this.table.Dimensions[this.rowDimensionSelected].Points.Count; r++)
			{
				var words = new List<string> ();

				if (useRows)
				{
					words.Add (this.table.Dimensions[this.rowDimensionSelected].Points[r]);
				}

				for (int c = 0; c < this.table.Dimensions[this.columnDimensionSelected].Points.Count; c++)
				{
					decimal? value = this.table.Values.GetValue (this.GetKey (c, r));

					if (value.HasValue)
					{
						words.Add (value.Value.ToString (System.Globalization.CultureInfo.InvariantCulture));
					}
					else
					{
						words.Add ("");
					}
				}

				lines.Add (string.Join ("\t", words));
			}

			//	Ecrit le fichier.
			try
			{
				System.IO.File.WriteAllLines (path, lines);
			}
			catch (System.Exception ex)
			{
				return ex.Message;
			}

			return null;  // ok
		}

		private string Import(string path, bool useColumns, bool useRows)
		{
			//	Lit le fichier.
			string[] fileLines;
			try
			{
				fileLines = System.IO.File.ReadAllLines (path);
			}
			catch (System.Exception ex)
			{
				return ex.Message;
			}

			//	Ne garde que les lignes non vides.
			string[] lines = fileLines.Where (x => !string.IsNullOrWhiteSpace (x)).ToArray ();

			if (lines.Length <= (useRows ? 1 : 0))
			{
				return "Le fichier est vide.";
			}

			//	Cherche les noms des colonnes.
			var columns = this.table.Dimensions[this.columnDimensionSelected].Points;

			if (useColumns)
			{
				columns = new List<string> ();

				string[] words = lines[0].Split ('\t');

				for (int i = useRows ? 1 : 0; i < words.Length; i++)
				{
					columns.Add (words[i].Trim ());
				}
			}

			//	Cherche les noms des lignes.
			var rows = this.table.Dimensions[this.rowDimensionSelected].Points;

			if (useRows)
			{
				rows = new List<string> ();

				for (int i = useColumns ? 1 : 0; i < lines.Length; i++)
				{
					string[] words = lines[i].Split ('\t');

					rows.Add (words[0].Trim ());
				}
			}

			//	Construit la table des dimensions importées.
			var dimensions = new List<DesignerDimension> ();

			for (int n = 0; n < this.table.Dimensions.Count; n++)
			{
				dimensions.Add (new DesignerDimension (this.table.Dimensions[n]));
			}

			dimensions[this.columnDimensionSelected].Points.Clear ();
			dimensions[this.columnDimensionSelected].Points.AddRange (columns);

			dimensions[this.rowDimensionSelected].Points.Clear ();
			dimensions[this.rowDimensionSelected].Points.AddRange (rows);

			//	Importe les prix.
			for (int r = useColumns ? 1 : 0; r < lines.Length; r++)
			{
				int row = useColumns ? r-1 : r;
				string[] words = lines[r].Split ('\t');

				for (int c = useRows ? 1 : 0; c < words.Length; c++)
				{
					int column = useRows ? c-1 : c;

					decimal value;
					if (decimal.TryParse (words[c].Trim (), out value))
					{
						var stringKey = new List<string> ();

						for (int n = 0; n < this.table.Dimensions.Count; n++)
						{
							int choice = this.GetDimensionChoice (n);
							string k;

							if (choice == TableController.usedForColumn)
							{
								if (column >= columns.Count)
								{
									continue;
								}

								k = columns[column];
							}
							else if (choice == TableController.usedForRow)
							{
								if (row >= rows.Count)
								{
									continue;
								}

								k = rows[row];
							}
							else
							{
								k = this.table.Dimensions[n].Points[choice];
							}

							stringKey.Add (k);
						}

						int[] intKey;
						if (this.table.StringKeyToIntKey (dimensions, string.Join (".", stringKey), out intKey, ref value))
						{
							this.table.Values.SetValue (intKey, value);
						}
					}
				}
			}

			return null;  // ok
		}
		#endregion


		private int[] GetKey(int column, int row)
		{
			var list = new List<int> ();

			for (int n = 0; n < this.table.Dimensions.Count; n++)
			{
				int choice = this.GetDimensionChoice (n);

				if (choice == TableController.usedForColumn)
				{
					list.Add (column);
				}
				else if (choice == TableController.usedForRow)
				{
					list.Add (row);
				}
				else
				{
					list.Add (choice);
				}
			}

			return list.ToArray ();
		}

		private int GetDimensionChoice(int index)
		{
			//	Retourne le choix effectué dans l'interface, pour une dimension donnée.
			var radioX = this.dimensionsTable[0, index].Children[0] as RadioButton;
			var radioY = this.dimensionsTable[1, index].Children[0] as RadioButton;
			var combo  = this.dimensionsTable[3, index].Children[0] as TextFieldCombo;

			if (radioX.ActiveState == ActiveState.Yes)
			{
				return TableController.usedForColumn;
			}

			if (radioY.ActiveState == ActiveState.Yes)
			{
				return TableController.usedForRow;
			}

			int i = this.table.Dimensions[index].Points.IndexOf (combo.Text);

			if (i == -1)
			{
				i = 0;
			}

			return i;
		}


		private string GetValue(int[] indexes)
		{
			decimal? d = this.table.Values.GetValue (indexes);

			return TableController.PriceToString (d);
		}

		private void SetValue(int[] indexes, string value)
		{
			decimal d;
			if (decimal.TryParse (value, out d))
			{
				this.table.Values.SetValue (indexes, d);
			}
			else
			{
				this.table.Values.SetValue (indexes, null);
			}
		}


		private static string GetLegendText(string text)
		{
			return string.Concat ("<font size=\"16\"><b>", text, "</b></font>");
		}

		private static string PriceToString(decimal? value)
		{
			if (!value.HasValue)
			{
				return null;
			}

			DecimalRange decimalRange001 = new DecimalRange (-1000000000M, 1000000000M, 0.01M);
			return decimalRange001.ConvertToString (value.Value);
		}



		private static readonly double						legendHeight = 28;
		private static readonly int							usedForColumn = -2;
		private static readonly int							usedForRow = -3;

		private readonly DesignerTable						table;

		private FrameBox									dimensionsPane;
		private CellTable									dimensionsTable;
		private GlyphButton									swapButton;
		private VStaticText									rowsLegend;
		private StaticText									columnsLegend;
		private CellTable									valuesTable;
		private GlyphButton									toolbarButton;
		private FrameBox									toolbar;

		private int											columnDimensionSelected;
		private int											rowDimensionSelected;
	}
}

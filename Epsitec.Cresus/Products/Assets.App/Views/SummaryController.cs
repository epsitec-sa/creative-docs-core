﻿//	Copyright © 2013, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Daniel ROUX, Maintainer: Daniel ROUX

using System.Collections.Generic;
using System.Linq;
using Epsitec.Common.Drawing;
using Epsitec.Common.Widgets;
using Epsitec.Cresus.Assets.App.Helpers;
using Epsitec.Cresus.Assets.App.Views.FieldControllers;
using Epsitec.Cresus.Assets.App.Widgets;
using Epsitec.Cresus.Assets.Data;

namespace Epsitec.Cresus.Assets.App.Views
{
	/// <summary>
	/// Affiche un tableau résumé contenant des textes (définis dans SummaryControllerTile).
	/// </summary>
	public class SummaryController
	{
		public void CreateUI(Widget parent)
		{
			this.scrollable = new Scrollable
			{
				Parent                 = parent,
				HorizontalScrollerMode = ScrollableScrollerMode.HideAlways,
				VerticalScrollerMode   = ScrollableScrollerMode.ShowAlways,
				Dock                   = DockStyle.Fill,
				Margins                = new Margins (10, 0, 0, 0),
			};

			this.scrollable.Viewport.IsAutoFitting = true;
		}

		public void SetTiles(List<List<SummaryControllerTile?>> tiles, bool isLocked)
		{
			this.tiles = tiles;
			this.isLocked = isLocked;

			this.CreateTiles ();
		}

		private void CreateTiles()
		{
			this.scrollable.Viewport.Children.Clear ();

			int columnsCount = this.ColumnsCount;
			int rowsCount    = this.RowsCount;

			for (int column = 0; column < columnsCount; column++ )
			{
				//	Calcule une largeur permettant d'afficher 6 colonnes.
				//	Les colonnes de données (impaires) occupent plus de place
				//	que celle de description (paires).
				int w = (AbstractView.editionWidth-20-AbstractView.scrollerDefaultBreadth) / 6;
				int width = (column%2 == 0) ? w-20 : w+20;

				var columnFrame = new FrameBox
				{
					Parent         = this.scrollable.Viewport,
					Dock           = DockStyle.Left,
					PreferredWidth = width-1,
				};

				for (int row = 0; row < rowsCount; row++)
				{
					var button = new ColoredButton
					{
						Parent            = columnFrame,
						Name              = SummaryController.PutRowColumn (row, column),
						Dock              = DockStyle.Top,
						PreferredWidth    = width-1,
						PreferredHeight   = AbstractFieldController.lineHeight,
						Margins           = new Margins (0, 1, 0, 1),
						TextBreakMode     = TextBreakMode.Ellipsis | TextBreakMode.Split | TextBreakMode.SingleLine,
						HorizontalMargins = 0,
					};

					this.UpdateButton (button, this.GetTile (column, row));

					button.Clicked += delegate
					{
						int r, c;
						SummaryController.GetRowColumn (button.Name, out r, out c);
						var sct = this.GetTile (c, r);
						if (sct.HasValue && !sct.Value.ReadOnly)
						{
							this.OnTileClicked (r, c);
						}
					};
				}
			}
		}


		private void UpdateButton(ColoredButton button, SummaryControllerTile? tile)
		{
			if (tile.HasValue && !tile.Value.Label)
			{
				if (tile.Value.ReadOnly)
				{
					if (tile.Value.Hilited)
					{
						//	Rectangle bleu sans hover.
						button.NormalColor = AbstractFieldController.GetBackgroundColor (PropertyState.Single, this.isLocked);
					}
					else
					{
						//	Rectangle gris sans hover.
						button.NormalColor = AbstractFieldController.GetBackgroundColor (PropertyState.Synthetic, true);
					}

					button.HoverColor = Color.Empty;
				}
				else
				{
					if (tile.Value.Hilited)
					{
						//	Rectangle bleu avec hover.
						button.NormalColor = AbstractFieldController.GetBackgroundColor (PropertyState.Single, this.isLocked);
					}
					else
					{
						//	Rectangle blanc avec hover.
						button.NormalColor = AbstractFieldController.GetBackgroundColor (PropertyState.Synthetic, this.isLocked);
					}

					button.HoverColor = ColorManager.HoverColor;
				}

				button.Hatch = tile.Value.Hatch;
			}
			else
			{
				//	Rectangle invisible (comme le fond).
				button.NormalColor = Color.Empty;
				button.HoverColor  = Color.Empty;
			}

			if (tile.HasValue)
			{
				button.ContentAlignment = tile.Value.Alignment;

				switch (tile.Value.Alignment)
				{
					case ContentAlignment.TopRight:
					case ContentAlignment.MiddleRight:
					case ContentAlignment.BottomRight:
						button.Text = tile.Value.Text + " ";
						break;

					case ContentAlignment.TopLeft:
					case ContentAlignment.MiddleLeft:
					case ContentAlignment.BottomLeft:
					button.Text = " " + tile.Value.Text;
						break;

					default:
						button.Text = tile.Value.Text;
						break;
				}
			}
			else
			{
				button.Text = null;
			}

			if (tile.HasValue && !string.IsNullOrEmpty (tile.Value.Tootip))
			{
				ToolTip.Default.SetToolTip (button, tile.Value.Tootip);
			}
			else
			{
				ToolTip.Default.ClearToolTip (button);
			}
		}

		private SummaryControllerTile? GetTile(int column, int row)
		{
			if (column < this.tiles.Count)
			{
				var rows = this.tiles[column];

				if (row < rows.Count)
				{
					return rows[row];
				}
			}

			return null;
		}

		private int ColumnsCount
		{
			get
			{
				return this.tiles.Count;
			}
		}

		private int RowsCount
		{
			get
			{
				return this.tiles.Max (column => column.Count);
			}
		}


		private static string PutRowColumn(int row, int column)
		{
			return string.Concat
			(
				row.ToString (System.Globalization.CultureInfo.InstalledUICulture),
				"/",
				column.ToString (System.Globalization.CultureInfo.InstalledUICulture)
			);
		}

		private static void GetRowColumn(string text, out int row, out int column)
		{
			var p = text.Split ('/');
			row    = int.Parse (p[0], System.Globalization.CultureInfo.InstalledUICulture);
			column = int.Parse (p[1], System.Globalization.CultureInfo.InstalledUICulture);
		}


		#region Events handler
		private void OnTileClicked(int row, int column)
		{
			this.TileClicked.Raise (this, row, column);
		}

		public event EventHandler<int, int> TileClicked;
		#endregion


		private Scrollable							scrollable;
		private List<List<SummaryControllerTile?>>	tiles;
		private bool								isLocked;
	}
}

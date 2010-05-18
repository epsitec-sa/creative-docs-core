﻿//	Copyright © 2010, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Daniel ROUX, Maintainer: Daniel ROUX

using System.Collections.Generic;
using System.Linq;
using System.Text;
using Epsitec.Common.Support;
using Epsitec.Common.Types;
using Epsitec.Common.Drawing;
using Epsitec.Common.Widgets;

namespace Epsitec.Cresus.Core.Controllers
{
	public class ViewLayoutController : CoreController
	{
		public ViewLayoutController(string name, Widget container)
			: base (name)
		{
			this.container = container;
			this.columns = new Stack<Widget> ();
		}

		public override IEnumerable<CoreController> GetSubControllers()
		{
			yield break;
		}

		public int ColumnCount
		{
			get
			{
				return this.columns.Count;
			}
		}

		public Widget LastColumn
		{
			get
			{
				return this.columns.Peek ();
			}
		}

		public Widget CreateColumn()
		{
			var column = new Widgets.ContainerTiles ()
			{
				Name = string.Format ("Column{0}", this.ColumnCount),
			};

			this.columns.Push (column);
			this.UpdateColumnLayout ();

			return column;
		}

		public void DeleteColumn()
		{
			if (this.ColumnCount > 0)
			{
				var column = this.columns.Pop ();

				column.Dispose ();
				this.UpdateColumnLayout ();
			}
		}


		private void UpdateColumnLayout()
		{
			this.container.Children.Clear ();

			if (this.columns.Count > 0)
			{
#if false
				double x = 0;
				double overlap = Widgets.ArrowedTileArrow.ArrowBreadth - 3;
				int columnIndex = 0;

				//	Positionne les colonnes de gauche à droite, sauf la dernière.
				foreach (var column in this.columns.Skip (1).Reverse ())
				{
					column.Anchor = AnchorStyles.TopAndBottom | AnchorStyles.Left;
					column.PreferredWidth = ViewLayoutController.WidthCompute (this.columns.Count, columnIndex++);
					column.Margins = new Margins (x, 0, 0, 0);
					x += column.PreferredWidth - overlap;
					ViewLayoutController.SetRightColumn (column, false);
				}

				//	Positionne la dernière colonne de droite.
				var lastColumn = this.columns.Peek ();

				lastColumn.Anchor = AnchorStyles.TopAndBottom | AnchorStyles.LeftAndRight;
				lastColumn.Margins = new Margins (x, 0, 0, 0);
				ViewLayoutController.SetRightColumn (lastColumn, true);

				//	Ajoute les colonnes au parent, mais de droite à gauche.
				foreach (var column in this.columns)
				{
					this.container.Children.Add (column);
				}
#else
				double x = 0;
				double overlap = Widgets.TileArrow.Breadth - 3;
				int columnIndex = 0;

				//	Positionne les colonnes de gauche à droite.
				foreach (var column in this.columns.Reverse ())
				{
					column.Anchor = AnchorStyles.TopAndBottom | AnchorStyles.Left;
					column.PreferredWidth = ViewLayoutController.WidthCompute (this.columns.Count, columnIndex++);
					column.Margins = new Margins (x, 0, 0, 0);
					x += column.PreferredWidth - overlap;

					ViewLayoutController.SetRightColumn (column, columnIndex >= this.columns.Count);
				}

				//	Ajoute les colonnes au parent, mais de droite à gauche.
				foreach (var column in this.columns)
				{
					this.container.Children.Add (column);
				}
#endif
			}
		}

		private static void SetRightColumn(Widget column, bool value)
		{
			if (column is Widgets.ContainerTiles)
			{
				var tilesContainer = column as Widgets.ContainerTiles;
				tilesContainer.IsRightColumn = value;
			}
		}

		private static double WidthCompute(int columnsCount, int columnIndex)
		{
			double width = 250 - 250*(columnsCount-columnIndex-2)*0.5;

			//	A part la première colonne de gauche, les autres colonnes doivent soit avoir une
			//	largeur supérieure ou égale à minimalWidth, soit reduceWidth pour ne montrer que
			//	l'icône, à cause de problèmes de layout !
			//	TODO: Voir ce problème avec Pierre (mail du 30.04.10 12:07)
			if (columnIndex != 0 && width < ViewLayoutController.minimalWidth)
			{
				width = ViewLayoutController.reduceWidth;
			}

			return width;
		}


		private static readonly double minimalWidth = 220;
		private static readonly double reduceWidth = Widgets.GroupingTile.WidthWithOnlyIcon + Widgets.TileArrow.Breadth;


		private readonly Widget container;
		private readonly Stack<Widget> columns;
	}
}

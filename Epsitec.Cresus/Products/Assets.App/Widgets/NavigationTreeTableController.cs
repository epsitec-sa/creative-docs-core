﻿//	Copyright © 2013, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Daniel ROUX, Maintainer: Daniel ROUX

using System.Collections.Generic;
using System.Linq;
using Epsitec.Common.Drawing;
using Epsitec.Common.Widgets;

namespace Epsitec.Cresus.Assets.App.Widgets
{
	/// <summary>
	/// Ce contrôleur inclut une TreeTable et un ascenseur vertical permettant la
	/// navigation en fonction du nombre de lignes RowsCount.
	/// Il cache complètement le TreeTable sous-jacent à l'aide d'une Facade.
	/// </summary>
	public class NavigationTreeTableController
	{
		public void CreateUI(Widget parent, int firstWidth = 200, int rowHeight = 18, int headerHeight = 24, int footerHeight = 24)
		{
			parent.BackColor = ColorManager.TreeTableBackgroundColor;

			this.treeTable = new TreeTable (firstWidth, rowHeight, headerHeight, footerHeight)
			{
				Parent = parent,
				Dock   = DockStyle.Fill,
			};

			this.scroller = new VScroller ()
			{
				Parent     = parent,
				Dock       = DockStyle.Right,
				Margins    = new Margins (0, 0, this.treeTable.VScrollerTopMargin, this.treeTable.VScrollerBottomMargin),
				IsInverted = true,  // le zéro est en haut
			};

			this.treeTable.SizeChanged += delegate
			{
				this.UpdateScroller ();
			};

			this.treeTable.RowClicked += delegate (object sender, int column, int row)
			{
				this.OnRowClicked (column, row);
			};

			this.treeTable.TreeButtonClicked += delegate (object sender, int row, TreeTableFirstType type)
			{
				this.OnTreeButtonClicked (row, type);
			};

			this.scroller.ValueChanged += delegate
			{
				this.OnRowChanged ();
			};

			this.UpdateScroller ();
		}

		public int								RowsCount
		{
			get
			{
				return this.rowsCount;
			}
			set
			{
				if (this.rowsCount != value)
				{
					this.rowsCount = value;
					this.UpdateScroller ();
				}
			}
		}

		public int								TopVisibleRow
		{
			get
			{
				return (int) this.scroller.Value;
			}
		}


		#region TreeTable Facade
		public int								VisibleRowsCount
		{
			get
			{
				return this.treeTable.VisibleRowsCount;
			}
		}

		public TreeTableColumnFirst				ColumnFirst
		{
			get
			{
				return this.treeTable.ColumnFirst;
			}
		}

		public IEnumerable<AbstractTreeTableColumn> Columns
		{
			get
			{
				return this.treeTable.Columns;
			}
		}

		public void SetColumns(List<AbstractTreeTableColumn> columns)
		{
			this.treeTable.SetColumns (columns);
		}
		#endregion


		private void UpdateScroller()
		{
			if (this.treeTable == null || this.scroller == null)
			{
				return;
			}

			var totalRows   = (decimal) this.rowsCount;
			var visibleRows = (decimal) this.treeTable.VisibleRowsCount;

			if (visibleRows < 0)
			{
				return;
			}

			this.scroller.Resolution = 1.0m;
			this.scroller.VisibleRangeRatio = System.Math.Min (visibleRows/totalRows, 1.0m);

			this.scroller.MinValue = 0.0m;
			this.scroller.MaxValue = System.Math.Max ((decimal) this.rowsCount - visibleRows, 0.0m);

			this.scroller.SmallChange = 1.0m;
			this.scroller.LargeChange = visibleRows;

			this.OnRowChanged ();  // met à jour le tableau
		}


		#region Events handler
		private void OnRowChanged()
		{
			if (this.RowChanged != null)
			{
				this.RowChanged (this);
			}
		}

		public delegate void RowChangedEventHandler(object sender);
		public event RowChangedEventHandler RowChanged;


		private void OnRowClicked(int column, int row)
		{
			if (this.RowClicked != null)
			{
				this.RowClicked (this, column, row);
			}
		}

		public delegate void RowClickedEventHandler(object sender, int column, int row);
		public event RowClickedEventHandler RowClicked;


		private void OnTreeButtonClicked(int row, TreeTableFirstType type)
		{
			if (this.TreeButtonClicked != null)
			{
				this.TreeButtonClicked (this, row, type);
			}
		}

		public delegate void TreeButtonClickedEventHandler(object sender, int row, TreeTableFirstType type);
		public event TreeButtonClickedEventHandler TreeButtonClicked;
		#endregion


		private TreeTable treeTable;
		private VScroller scroller;
		private int rowsCount;
	}
}
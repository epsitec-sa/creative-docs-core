﻿//	Copyright © 2013, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Daniel ROUX, Maintainer: Daniel ROUX

using System.Collections.Generic;
using System.Linq;

using Epsitec.Common.Widgets;
using Epsitec.Cresus.Assets.App.Widgets;
using Epsitec.Cresus.Assets.Server.NaiveEngine;

namespace Epsitec.Cresus.Assets.App.Views
{
	public class ObjectsTreeTableController
	{
		public ObjectsTreeTableController(DataAccessor accessor)
		{
			this.accessor = accessor;
			this.timestamp = new Timestamp (System.DateTime.MaxValue, 0);
		}

		public void CreateUI(Widget parent)
		{
			this.topTitle = new TopTitle
			{
				Parent = parent,
			};

			this.topTitle.SetTitle ("Objets d'immobilisation");

			this.CreateTreeTable (parent);
		}


		public int								SelectedRow
		{
			get
			{
				return this.selectedRow;
			}
			set
			{
				if (this.selectedRow != value)
				{
					this.selectedRow = value;

					this.UpdateTreeTableController ();
					this.OnRowClicked (this.selectedRow);
				}
			}
		}

		public Timestamp						Timestamp
		{
			get
			{
				return this.timestamp;
			}
			set
			{
				if (this.timestamp != value)
				{
					this.timestamp = value;

					this.UpdateTreeTableController ();
				}
			}
		}


		private void CreateTreeTable(Widget parent)
		{
			int rowsCount = this.accessor.ObjectsCount;
			this.selectedRow = -1;

			this.controller = new NavigationTreeTableController
			{
				RowsCount = rowsCount,
			};

			var frame = new FrameBox
			{
				Parent = parent,
				Dock   = DockStyle.Fill,
			};

			this.controller.CreateUI (frame, footerHeight: 0);
			this.controller.SetColumns (this.TreeTableColumns, 1);

			//	Pour que le calcul du nombre de lignes visibles soit correct.
			parent.Window.ForceLayout ();

			this.UpdateTreeTableController ();

			//	Connexion des événements.
			this.controller.ContentChanged += delegate (object sender, bool crop)
			{
				this.UpdateTreeTableController (crop);
			};

			this.controller.RowClicked += delegate (object sender, int row)
			{
				this.SelectedRow = this.controller.TopVisibleRow + row;
				this.OnRowClicked (this.SelectedRow);
			};

			this.controller.RowDoubleClicked += delegate (object sender, int row)
			{
				this.SelectedRow = this.controller.TopVisibleRow + row;
				this.OnRowDoubleClicked (this.SelectedRow);
			};

			this.controller.TreeButtonClicked += delegate (object sender, int row, TreeTableTreeType type)
			{
			};
		}

		private TreeTableColumnDescription[] TreeTableColumns
		{
			get
			{
				var list = new List<TreeTableColumnDescription> ();

				list.Add (new TreeTableColumnDescription (TreeTableColumnType.Tree,           180, "Objet"));
				list.Add (new TreeTableColumnDescription (TreeTableColumnType.String,          50, "N°"));
				list.Add (new TreeTableColumnDescription (TreeTableColumnType.String,         120, "Responsable"));
				list.Add (new TreeTableColumnDescription (TreeTableColumnType.String,          60, "Couleur"));
				list.Add (new TreeTableColumnDescription (TreeTableColumnType.String,         200, "Numéro de série"));
				list.Add (new TreeTableColumnDescription (TreeTableColumnType.ComputedAmount, 120, "Valeur comptable"));
				list.Add (new TreeTableColumnDescription (TreeTableColumnType.ComputedAmount, 120, "Valeur assurance"));

				return list.ToArray ();
			}
		}

		private void UpdateTreeTableController(bool crop = true)
		{
			int visibleCount = this.controller.VisibleRowsCount;
			int rowsCount    = this.accessor.ObjectsCount;
			int count        = System.Math.Min (visibleCount, rowsCount);
			int firstRow     = this.controller.TopVisibleRow;
			int selection    = this.selectedRow;

			if (selection != -1)
			{
				//	La sélection ne peut pas dépasser le nombre maximal de lignes.
				selection = System.Math.Min (selection, rowsCount-1);

				//	Si la sélection est hors de la zone visible, on choisit un autre cadrage.
				if (crop && (selection < firstRow || selection >= firstRow+count))
				{
					firstRow = this.controller.GetTopVisibleRow (selection);
				}

				if (this.controller.TopVisibleRow != firstRow)
				{
					this.controller.TopVisibleRow = firstRow;
				}

				selection -= this.controller.TopVisibleRow;
			}

			var cf = new List<TreeTableCellTree> ();
			var c1 = new List<TreeTableCellString> ();
			var c2 = new List<TreeTableCellString> ();
			var c3 = new List<TreeTableCellString> ();
			var c4 = new List<TreeTableCellString> ();
			var c5 = new List<TreeTableCellComputedAmount> ();
			var c6 = new List<TreeTableCellComputedAmount> ();

			for (int i=0; i<count; i++)
			{
				if (firstRow+i >= this.accessor.ObjectsCount)
				{
					break;
				}

				var guid = this.accessor.GetObjectGuid (firstRow+i);
				var properties = this.accessor.GetObjectSyntheticProperties (guid, this.timestamp);

				int level = DataAccessor.GetIntProperty (properties, (int) ObjectField.Level).GetValueOrDefault (-1);

				var type = TreeTableTreeType.Extended;

				if (level == -1)
				{
					type = TreeTableTreeType.None;
				}
				else if (level == 3)
				{
					type = TreeTableTreeType.Final;
				}

				var nom         = DataAccessor.GetStringProperty (properties, (int) ObjectField.Nom);
				var numéro      = DataAccessor.GetStringProperty (properties, (int) ObjectField.Numéro);
				var responsable = DataAccessor.GetStringProperty (properties, (int) ObjectField.Responsable);
				var couleur     = DataAccessor.GetStringProperty (properties, (int) ObjectField.Couleur);
				var série       = DataAccessor.GetStringProperty (properties, (int) ObjectField.NuméroSérie);
				var valeur1     = DataAccessor.GetComputedAmountProperty (properties, (int) ObjectField.Valeur1);
				var valeur2     = DataAccessor.GetComputedAmountProperty (properties, (int) ObjectField.Valeur2);

				var sf = new TreeTableCellTree (true, level, type, nom, isSelected: (i == selection));
				var s1 = new TreeTableCellString (true, numéro, isSelected: (i == selection));
				var s2 = new TreeTableCellString (true, responsable, isSelected: (i == selection));
				var s3 = new TreeTableCellString (true, couleur, isSelected: (i == selection));
				var s4 = new TreeTableCellString (true, série, isSelected: (i == selection));
				var s5 = new TreeTableCellComputedAmount (true, valeur1, isSelected: (i == selection));
				var s6 = new TreeTableCellComputedAmount (true, valeur2, isSelected: (i == selection));

				cf.Add (sf);
				c1.Add (s1);
				c2.Add (s2);
				c3.Add (s3);
				c4.Add (s4);
				c5.Add (s5);
				c6.Add (s6);
			}

			this.controller.SetColumnCells (0, cf.ToArray ());
			this.controller.SetColumnCells (1, c1.ToArray ());
			this.controller.SetColumnCells (2, c2.ToArray ());
			this.controller.SetColumnCells (3, c3.ToArray ());
			this.controller.SetColumnCells (4, c4.ToArray ());
			this.controller.SetColumnCells (5, c5.ToArray ());
			this.controller.SetColumnCells (6, c6.ToArray ());
		}


		#region Events handler
		private void OnRowClicked(int row)
		{
			if (this.RowClicked != null)
			{
				this.RowClicked (this, row);
			}
		}

		public delegate void RowClickedEventHandler(object sender, int row);
		public event RowClickedEventHandler RowClicked;


		private void OnRowDoubleClicked(int row)
		{
			if (this.RowDoubleClicked != null)
			{
				this.RowDoubleClicked (this, row);
			}
		}

		public delegate void RowDoubleClickedEventHandler(object sender, int row);
		public event RowDoubleClickedEventHandler RowDoubleClicked;
		#endregion


		private readonly DataAccessor			accessor;

		private TopTitle						topTitle;
		private NavigationTreeTableController	controller;
		private int								selectedRow;
		private Timestamp						timestamp;
	}
}

﻿//	Copyright © 2013, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Daniel ROUX, Maintainer: Daniel ROUX

using System.Collections.Generic;
using System.Linq;
using Epsitec.Cresus.Assets.Data;
using Epsitec.Cresus.Assets.Server.BusinessLogic;
using Epsitec.Cresus.Assets.Server.SimpleEngine;

namespace Epsitec.Cresus.Assets.Server.DataFillers
{
	public class UserFieldsTreeTableFiller : AbstractTreeTableFiller<GuidNode>
	{
		public UserFieldsTreeTableFiller(DataAccessor accessor, BaseType baseType, INodeGetter<GuidNode> nodeGetter)
			: base (accessor, nodeGetter)
		{
			this.baseType = baseType;
		}


		public override SortingInstructions		DefaultSorting
		{
			get
			{
				return new SortingInstructions (ObjectField.Name, SortedType.Ascending, ObjectField.Unknown, SortedType.None);
			}
		}

		public override int						DefaultDockToLeftCount
		{
			get
			{
				return 1;
			}
		}

		public override TreeTableColumnDescription[] Columns
		{
			get
			{
				var columns = new List<TreeTableColumnDescription> ();

				columns.Add (new TreeTableColumnDescription (ObjectField.Name,                  TreeTableColumnType.String, 150, Res.Strings.UserFieldsTreeTableFiller.Name.ToString ()));
				columns.Add (new TreeTableColumnDescription (ObjectField.UserFieldType,         TreeTableColumnType.String, 100, Res.Strings.UserFieldsTreeTableFiller.Type.ToString ()));
				columns.Add (new TreeTableColumnDescription (ObjectField.UserFieldRequired,     TreeTableColumnType.String,  50, Res.Strings.UserFieldsTreeTableFiller.Required.ToString ()));
				columns.Add (new TreeTableColumnDescription (ObjectField.UserFieldColumnWidth,  TreeTableColumnType.Int,     70, Res.Strings.UserFieldsTreeTableFiller.ColumnWidth.ToString ()));
				columns.Add (new TreeTableColumnDescription (ObjectField.UserFieldLineWidth,    TreeTableColumnType.Int,     70, Res.Strings.UserFieldsTreeTableFiller.LineWidth.ToString ()));
				columns.Add (new TreeTableColumnDescription (ObjectField.UserFieldLineCount,    TreeTableColumnType.Int,     70, Res.Strings.UserFieldsTreeTableFiller.LineCount.ToString ()));
				columns.Add (new TreeTableColumnDescription (ObjectField.UserFieldTopMargin,    TreeTableColumnType.Int,     70, Res.Strings.UserFieldsTreeTableFiller.TopMargin.ToString ()));
				columns.Add (new TreeTableColumnDescription (ObjectField.UserFieldSummaryOrder, TreeTableColumnType.Int,     70, Res.Strings.UserFieldsTreeTableFiller.SummaryOrder.ToString ()));

				return columns.ToArray ();
			}
		}

		public override TreeTableContentItem GetContent(int firstRow, int count, int selection)
		{
			var content = new TreeTableContentItem ();

			for (int i=0; i<8; i++)
			{
				content.Columns.Add (new TreeTableColumnItem ());
			}

			for (int i=0; i<count; i++)
			{
				if (firstRow+i >= this.nodeGetter.Count)
				{
					break;
				}

				var node = this.nodeGetter[firstRow+i];
				var guid = node.Guid;
				var obj  = this.accessor.GetObject (this.baseType, guid);

				var c0 = ObjectProperties.GetObjectPropertyString (obj, null, ObjectField.Name);
				var c1 = ObjectProperties.GetObjectPropertyInt    (obj, null, ObjectField.UserFieldType);
				var c2 = ObjectProperties.GetObjectPropertyInt    (obj, null, ObjectField.UserFieldRequired);
				var c3 = ObjectProperties.GetObjectPropertyInt    (obj, null, ObjectField.UserFieldColumnWidth);
				var c4 = ObjectProperties.GetObjectPropertyInt    (obj, null, ObjectField.UserFieldLineWidth);
				var c5 = ObjectProperties.GetObjectPropertyInt    (obj, null, ObjectField.UserFieldLineCount);
				var c6 = ObjectProperties.GetObjectPropertyInt    (obj, null, ObjectField.UserFieldTopMargin);
				var c7 = ObjectProperties.GetObjectPropertyInt    (obj, null, ObjectField.UserFieldSummaryOrder);

				var t1 = EnumDictionaries.GetFieldTypeName ((FieldType) c1);
				var t2 = (c2 == 1) ? Res.Strings.UserFieldsTreeTableFiller.RequiredYes.ToString () : Res.Strings.UserFieldsTreeTableFiller.RequiredNo.ToString ();

				var cellState = (i == selection) ? CellState.Selected : CellState.None;

				var cell0 = new TreeTableCellString (c0, cellState);
				var cell1 = new TreeTableCellString (t1, cellState);
				var cell2 = new TreeTableCellString (t2, cellState);
				var cell3 = new TreeTableCellInt    (c3, cellState);
				var cell4 = new TreeTableCellInt    (c4, cellState);
				var cell5 = new TreeTableCellInt    (c5, cellState);
				var cell6 = new TreeTableCellInt    (c6, cellState);
				var cell7 = new TreeTableCellInt    (c7, cellState);

				int columnRank = 0;

				content.Columns[columnRank++].AddRow (cell0);
				content.Columns[columnRank++].AddRow (cell1);
				content.Columns[columnRank++].AddRow (cell2);
				content.Columns[columnRank++].AddRow (cell3);
				content.Columns[columnRank++].AddRow (cell4);
				content.Columns[columnRank++].AddRow (cell5);
				content.Columns[columnRank++].AddRow (cell6);
				content.Columns[columnRank++].AddRow (cell7);
			}

			return content;
		}


		private readonly BaseType baseType;
	}
}

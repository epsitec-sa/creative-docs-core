﻿//	Copyright © 2013, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Daniel ROUX, Maintainer: Daniel ROUX

using System.Collections.Generic;
using System.Linq;
using Epsitec.Cresus.Assets.Data;
using Epsitec.Cresus.Assets.Server.BusinessLogic;
using Epsitec.Cresus.Assets.Server.NodeGetters;
using Epsitec.Cresus.Assets.Server.SimpleEngine;

namespace Epsitec.Cresus.Assets.Server.DataFillers
{
	public class SingleAccountsTreeTableFiller : AbstractTreeTableFiller<TreeNode>
	{
		public SingleAccountsTreeTableFiller(DataAccessor accessor, INodeGetter<TreeNode> nodeGetter)
			: base (accessor, nodeGetter)
		{
		}


		public BaseType							BaseType;

		public override SortingInstructions		DefaultSorting
		{
			get
			{
				return new SortingInstructions (ObjectField.Number, SortedType.Ascending, ObjectField.Unknown, SortedType.None);
			}
		}

		public override int						DefaultDockToLeftCount
		{
			get
			{
				return 0;
			}
		}

		public override TreeTableColumnDescription[] Columns
		{
			get
			{
				var columns = new List<TreeTableColumnDescription> ();

				columns.Add (new TreeTableColumnDescription (ObjectField.Number,          TreeTableColumnType.Tree,   SingleAccountsTreeTableFiller.numberWidth));
				columns.Add (new TreeTableColumnDescription (ObjectField.Name,            TreeTableColumnType.String, SingleAccountsTreeTableFiller.titleWidth));
				columns.Add (new TreeTableColumnDescription (ObjectField.AccountCategory, TreeTableColumnType.String, SingleAccountsTreeTableFiller.categoryWidth));

				return columns.ToArray ();
			}
		}

		public override TreeTableContentItem GetContent(int firstRow, int count, int selection)
		{
			var content = new TreeTableContentItem ();

			content.Columns.Add (new TreeTableColumnItem ());
			content.Columns.Add (new TreeTableColumnItem ());
			content.Columns.Add (new TreeTableColumnItem ());

			for (int i=0; i<count; i++)
			{
				if (firstRow+i >= this.nodeGetter.Count)
				{
					break;
				}

				var node    = this.nodeGetter[firstRow+i];
				var level   = node.Level;
				var type    = node.Type;
				var account = this.accessor.GetObject (this.BaseType, node.Guid);

				var number   = ObjectProperties.GetObjectPropertyString (account, this.Timestamp, ObjectField.Number, inputValue: true);
				var name     = ObjectProperties.GetObjectPropertyString (account, this.Timestamp, ObjectField.Name);
				var category = ObjectProperties.GetObjectPropertyInt    (account, this.Timestamp, ObjectField.AccountCategory);
				var accType  = ObjectProperties.GetObjectPropertyInt    (account, this.Timestamp, ObjectField.AccountType);

				string c = category.HasValue ? EnumDictionaries.GetAccountCategoryName ((AccountCategory) category) : null;

				var cellState = (i == selection) ? CellState.Selected : CellState.None;

				if (((AccountType) accType != AccountType.Normal))
				{
					cellState |= CellState.Dimmed;
				}

				var cell1 = new TreeTableCellTree   (level, type, number, cellState);
				var cell2 = new TreeTableCellString (name,                cellState);
				var cell3 = new TreeTableCellString (c,                   cellState);

				content.Columns[0].AddRow (cell1);
				content.Columns[1].AddRow (cell2);
				content.Columns[2].AddRow (cell3);
			}

			return content;
		}


		public const int TotalWidth =
			SingleAccountsTreeTableFiller.numberWidth +
			SingleAccountsTreeTableFiller.titleWidth +
			SingleAccountsTreeTableFiller.categoryWidth;

		private const int numberWidth   = 140;
		private const int titleWidth    = 400;
		private const int categoryWidth =  60;
	}
}

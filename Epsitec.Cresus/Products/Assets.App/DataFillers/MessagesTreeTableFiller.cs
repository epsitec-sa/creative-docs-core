﻿//	Copyright © 2013, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Daniel ROUX, Maintainer: Daniel ROUX

using System.Collections.Generic;
using System.Linq;
using Epsitec.Cresus.Assets.App.NodeGetters;
using Epsitec.Cresus.Assets.Data;
using Epsitec.Cresus.Assets.Server.DataFillers;
using Epsitec.Cresus.Assets.Server.SimpleEngine;

namespace Epsitec.Cresus.Assets.App.DataFillers
{
	public class MessagesTreeTableFiller : AbstractTreeTableFiller<MessageNode>
	{
		public MessagesTreeTableFiller(DataAccessor accessor, INodeGetter<MessageNode> nodeGetter)
			: base (accessor, nodeGetter)
		{
		}


		public string							Title
		{
			get
			{
				return this.title;
			}
			set
			{
				this.title = value;
			}
		}

		public int								Width
		{
			get
			{
				return this.width;
			}
			set
			{
				this.width = value;
			}
		}


		public override SortingInstructions		DefaultSorting
		{
			get
			{
				return new SortingInstructions (ObjectField.LastViewsPin, SortedType.Ascending, ObjectField.Unknown, SortedType.None);
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
				var list = new List<TreeTableColumnDescription> ();

				list.Add (new TreeTableColumnDescription (ObjectField.Description, TreeTableColumnType.String, this.width, this.title));

				return list.ToArray ();
			}
		}

		public override TreeTableContentItem GetContent(int firstRow, int count, int selection)
		{
			var c1 = new TreeTableColumnItem ();

			for (int i=0; i<count; i++)
			{
				if (firstRow+i >= this.nodeGetter.Count)
				{
					break;
				}

				var node = this.nodeGetter[firstRow+i];

				string desc = node.Description;

				var cellState = (i == selection) ? CellState.Selected : CellState.None;

				var s1 = new TreeTableCellString (desc, cellState);

				c1.AddRow (s1);
			}

			var content = new TreeTableContentItem ();

			content.Columns.Add (c1);

			return content;
		}


		private string							title = "Messages";
		private int								width = 300;
	}
}

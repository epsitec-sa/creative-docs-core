﻿//	Copyright © 2013, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Daniel ROUX, Maintainer: Daniel ROUX

using System.Collections.Generic;
using System.Linq;
using Epsitec.Common.Widgets;
using Epsitec.Cresus.Assets.Server.NodeGetters;
using Epsitec.Cresus.Assets.Server.SimpleEngine;

namespace Epsitec.Cresus.Assets.App.Views
{
	public class GroupsGraphicViewController : AbstractGraphicViewController<TreeNode>
	{
		public GroupsGraphicViewController(DataAccessor accessor, BaseType baseType, AbstractToolbarTreeTableController<TreeNode> treeTableController)
			: base (accessor, baseType, treeTableController)
		{
			//	GuidNode -> ParentPositionNode -> LevelNode -> TreeNode
			var primaryNodeGetter = this.accessor.GetNodeGetter (this.baseType);
			this.nodeGetter = new GroupTreeNodeGetter (this.accessor, this.baseType, primaryNodeGetter);

			this.graphicViewState = new GraphicViewState ();
			this.graphicViewState.Fields.Add (ObjectField.Name);
			this.graphicViewState.FontFactors.Add (1.0);

			this.graphicViewMode = GraphicViewMode.AutoWidthAllLines;
		}


		public override void CompactOrExpand(Guid guid)
		{
			int index = this.NodeGetter.SearchBestIndex (guid);
			this.NodeGetter.CompactOrExpand (index);

			this.UpdateData ();
		}

		public override void UpdateData()
		{
			if (this.graphicViewState == null || this.scrollable == null)
			{
				return;
			}

			this.NodeGetter.SetParams (null, this.graphicViewState.SortingInstructions);

			this.scrollable.Viewport.Children.Clear ();

			var parents = new List<Widget> ();
			parents.Add (this.scrollable.Viewport);

			var fields = this.GetFieds ();
			var fontFactors = this.GetFontFactors ();

			foreach (var node in this.NodeGetter.Nodes)
			{
				var level = node.Level;
				var parent = parents[level];

				var texts = this.GetTexts (this.baseType, node.Guid, fields);
				var w = this.CreateNode (parent, node.Guid, node.Level, node.Type, texts, fontFactors);

				if (parents.Count <= level+1)
				{
					parents.Add (null);
				}

				parents[level+1] = w;
			}
		}

		private GroupTreeNodeGetter NodeGetter
		{
			get
			{
				return this.nodeGetter as GroupTreeNodeGetter;
			}
		}
	}
}

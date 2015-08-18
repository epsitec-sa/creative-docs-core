﻿//	Copyright © 2013, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Daniel ROUX, Maintainer: Daniel ROUX

using System.Collections.Generic;
using System.Linq;
using Epsitec.Common.Widgets;
using Epsitec.Common.Support;
using Epsitec.Cresus.Assets.App.Helpers;
using Epsitec.Cresus.Assets.App.Popups;
using Epsitec.Cresus.Assets.App.Views.CommandToolbars;
using Epsitec.Cresus.Assets.App.Views.TreeGraphicControllers;
using Epsitec.Cresus.Assets.Data;
using Epsitec.Cresus.Assets.Server.DataFillers;
using Epsitec.Cresus.Assets.Server.NodeGetters;
using Epsitec.Cresus.Assets.Server.SimpleEngine;
using Epsitec.Common.Drawing;
using Epsitec.Cresus.Assets.Data.DataProperties;
using Epsitec.Cresus.Assets.Server.BusinessLogic;

namespace Epsitec.Cresus.Assets.App.Views.ToolbarControllers
{
	public class GroupsToolbarTreeTableController : AbstractToolbarBothTreesController<TreeNode>, IDirty, System.IDisposable
	{
		public GroupsToolbarTreeTableController(DataAccessor accessor, CommandContext commandContext, BaseType baseType)
			: base (accessor, commandContext, baseType)
		{
			this.title = AbstractView.GetViewTitle (this.accessor, ViewType.Groups);

			//	GuidNode -> ParentPositionNode -> LevelNode -> TreeNode
			this.primaryNodeGetter = this.accessor.GetNodeGetter (BaseType.Groups);
			this.nodeGetter = new GroupTreeNodeGetter (this.accessor, BaseType.Groups, this.primaryNodeGetter);
		}

		public override void Dispose()
		{
			base.Dispose ();
		}


		#region IDirty Members
		public bool DirtyData
		{
			get;
			set;
		}
		#endregion


		protected override void CreateGraphicControllerUI()
		{
			this.graphicController = new GroupsTreeGraphicController (this.accessor, this.baseType);
		}


		public override void UpdateData()
		{
			this.primaryNodeGetter.Update ();
			this.NodeGetter.SetParams (null, this.sortingInstructions);

			this.UpdateController ();
			this.UpdateToolbar ();
		}


		public override bool					IsEmpty
		{
			get
			{
				return this.nodeGetter.Count <= 1;
			}
		}

		protected override int					VisibleSelectedRow
		{
			get
			{
				return this.NodeGetter.AllToVisible (this.selectedRow);
			}
			set
			{
				this.SelectedRow = this.NodeGetter.VisibleToAll (value);
			}
		}

		public override Guid					SelectedGuid
		{
			//	Retourne le Guid de l'objet actuellement sélectionné.
			get
			{
				int sel = this.VisibleSelectedRow;
				if (sel != -1 && sel < this.nodeGetter.Count)
				{
					return this.nodeGetter[sel].Guid;
				}
				else
				{
					return Guid.Empty;
				}
			}
			//	Sélectionne l'objet ayant un Guid donné. Si la ligne correspondante
			//	est cachée, on est assez malin pour sélectionner la prochaine ligne
			//	visible, vers le haut.
			set
			{
				this.VisibleSelectedRow = this.NodeGetter.SearchBestIndex (value);
			}
		}


		protected override void CreateToolbar()
		{
			this.toolbar = new GroupsToolbar (this.accessor, this.commandContext);
			this.ConnectSearch ();
		}

		protected override void CreateNodeFiller()
		{
			this.dataFiller = new GroupsTreeTableFiller (this.accessor, this.nodeGetter)
			{
				Title = this.title,
			};

			TreeTableFiller<TreeNode>.FillColumns (this.treeTableController, this.dataFiller, "View.Groups");

			this.sortingInstructions = TreeTableFiller<TreeNode>.GetSortingInstructions (this.treeTableController);
		}


		[Command (Res.CommandIds.Groups.First)]
		protected override void OnFirst()
		{
			base.OnFirst ();
		}

		[Command (Res.CommandIds.Groups.Prev)]
		protected override void OnPrev()
		{
			base.OnPrev ();
		}

		[Command (Res.CommandIds.Groups.Next)]
		protected override void OnNext()
		{
			base.OnNext ();
		}

		[Command (Res.CommandIds.Groups.Last)]
		protected override void OnLast()
		{
			base.OnLast ();
		}

		[Command (Res.CommandIds.Groups.CompactAll)]
		protected override void OnCompactAll()
		{
			base.OnCompactAll ();
		}

		[Command (Res.CommandIds.Groups.CompactOne)]
		protected override void OnCompactOne()
		{
			base.OnCompactOne ();
		}

		[Command (Res.CommandIds.Groups.ExpandOne)]
		protected override void OnExpandOne()
		{
			base.OnExpandOne ();
		}

		[Command (Res.CommandIds.Groups.ExpandAll)]
		protected override void OnExpandAll()
		{
			base.OnExpandAll ();
		}

		[Command (Res.CommandIds.Groups.Deselect)]
		protected void OnDeselect()
		{
			this.VisibleSelectedRow = -1;
		}

		[Command (Res.CommandIds.Groups.New)]
		protected void OnNew(CommandDispatcher dispatcher, CommandEventArgs e)
		{
			var target = this.toolbar.GetTarget (e);
			this.ShowCreatePopup (target);
		}

		[Command (Res.CommandIds.Groups.Delete)]
		protected void OnDelete(CommandDispatcher dispatcher, CommandEventArgs e)
		{
			var target = this.toolbar.GetTarget (e);

			var groupCount = GroupsLogic.GetAllChildrensGuid (accessor, this.SelectedGuid).Count ();
			if (groupCount > 0)
			{
				string message = string.Format (Res.Strings.ToolbarControllers.GroupsTreeTable.DeleteErrorChildrens.ToString (), groupCount);
				MessagePopup.ShowError (target, message);
				return;
			}

			var assetsCount = AssetsLogic.GetReferencedGroups (this.accessor, this.SelectedGuid).Count ();
			if (assetsCount > 0)
			{
				string message = string.Format (Res.Strings.ToolbarControllers.GroupsTreeTable.DeleteError.ToString (), assetsCount);
				MessagePopup.ShowError (target, message);
				return;
			}

			var name = GroupsLogic.GetShortName (this.accessor, this.SelectedGuid);
			var question = string.Format (Res.Strings.ToolbarControllers.GroupsTreeTable.DeleteQuestion.ToString (), name);

			YesNoPopup.Show (target, question, delegate
			{
				this.accessor.UndoManager.Start ();
				var desc = UndoManager.GetDescription (Res.Commands.Groups.Delete.Description, GroupsLogic.GetShortName (this.accessor, this.SelectedGuid));
				this.accessor.UndoManager.SetDescription (desc);

				this.accessor.RemoveObject (BaseType.Groups, this.SelectedGuid);
				this.UpdateData ();
				this.OnUpdateAfterDelete ();

				this.accessor.UndoManager.SetAfterViewState ();
			});
		}

		[Command (Res.CommandIds.Groups.Copy)]
		protected override void OnCopy(CommandDispatcher dispatcher, CommandEventArgs e)
		{
			base.OnCopy (dispatcher, e);
		}

		[Command (Res.CommandIds.Groups.Paste)]
		protected override void OnPaste(CommandDispatcher dispatcher, CommandEventArgs e)
		{
			this.accessor.UndoManager.Start ();

			base.OnPaste (dispatcher, e);

			var desc = UndoManager.GetDescription (Res.Commands.Groups.Paste.Description, GroupsLogic.GetShortName (this.accessor, this.SelectedGuid));
			this.accessor.UndoManager.SetDescription (desc);
			this.accessor.UndoManager.SetAfterViewState ();
		}

		[Command (Res.CommandIds.Groups.Export)]
		protected override void OnExport(CommandDispatcher dispatcher, CommandEventArgs e)
		{
			base.OnExport (dispatcher, e);
		}


		protected override void ShowContextMenu(Point pos)
		{
			//	Affiche le menu contextuel.
			MenuPopup.Show (this.toolbar, this.treeTableFrame, pos, null,
				Res.Commands.Groups.New,
				Res.Commands.Groups.Delete,
				null,
				Res.Commands.Groups.Copy,
				Res.Commands.Groups.Paste);
		}


		private void ShowCreatePopup(Widget target)
		{
			CreateGroupPopup.Show (target, this.accessor, this.SelectedGuid, delegate (Guid objectParent, IEnumerable<AbstractDataProperty> properties)
			{
				this.CreateObject (objectParent, properties);
			});
		}

		private void CreateObject(Guid parent, IEnumerable<AbstractDataProperty> properties)
		{
			this.accessor.UndoManager.Start ();

			var date = this.accessor.Mandat.StartDate;
			var guid = this.accessor.CreateObject (BaseType.Groups, date, parent, false, properties.ToArray ());
			var obj = this.accessor.GetObject (BaseType.Groups, guid);
			System.Diagnostics.Debug.Assert (obj != null);
			
			this.UpdateData ();

			this.SelectedGuid = guid;
			this.OnUpdateAfterCreate (guid, EventType.Input, Timestamp.Now);  // Timestamp quelconque !

			var desc = UndoManager.GetDescription (Res.Commands.Groups.New.Description, GroupsLogic.GetShortName (this.accessor, obj.Guid));
			this.accessor.UndoManager.SetDescription (desc);
			this.accessor.UndoManager.SetAfterViewState ();
		}

	
		protected override void UpdateToolbar()
		{
			base.UpdateToolbar ();

			int row = this.VisibleSelectedRow;

			this.UpdateSelCommand (Res.Commands.Groups.First, row, this.FirstRowIndex);
			this.UpdateSelCommand (Res.Commands.Groups.Prev,  row, this.PrevRowIndex);
			this.UpdateSelCommand (Res.Commands.Groups.Next,  row, this.NextRowIndex);
			this.UpdateSelCommand (Res.Commands.Groups.Last,  row, this.LastRowIndex);

			bool compactEnable = !this.NodeGetter.IsAllCompacted;
			bool expandEnable  = !this.NodeGetter.IsAllExpanded;

			this.toolbar.SetEnable (Res.Commands.Groups.CompactAll, compactEnable);
			this.toolbar.SetEnable (Res.Commands.Groups.CompactOne, compactEnable);
			this.toolbar.SetEnable (Res.Commands.Groups.ExpandOne,  expandEnable);
			this.toolbar.SetEnable (Res.Commands.Groups.ExpandAll,  expandEnable);

			this.toolbar.SetEnable (Res.Commands.Groups.New,      true);
			this.toolbar.SetEnable (Res.Commands.Groups.Delete,   row > 0);  // groupe racine indestructible
			this.toolbar.SetEnable (Res.Commands.Groups.Deselect, row != -1);

			this.toolbar.SetEnable (Res.Commands.Groups.Copy,   this.IsCopyEnable);
			this.toolbar.SetEnable (Res.Commands.Groups.Paste,  this.accessor.Clipboard.HasObject (this.baseType));
			this.toolbar.SetEnable (Res.Commands.Groups.Export, !this.IsEmpty);
		}


		private GroupTreeNodeGetter NodeGetter
		{
			get
			{
				return this.nodeGetter as GroupTreeNodeGetter;
			}
		}


		private readonly GuidNodeGetter			primaryNodeGetter;
	}
}

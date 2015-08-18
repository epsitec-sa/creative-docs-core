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
using Epsitec.Cresus.Assets.Core.Helpers;
using Epsitec.Cresus.Assets.Data;
using Epsitec.Cresus.Assets.Server.BusinessLogic;
using Epsitec.Cresus.Assets.Server.DataFillers;
using Epsitec.Cresus.Assets.Server.NodeGetters;
using Epsitec.Cresus.Assets.Server.SimpleEngine;
using Epsitec.Common.Drawing;
using Epsitec.Cresus.Assets.Data.DataProperties;

namespace Epsitec.Cresus.Assets.App.Views.ToolbarControllers
{
	public class AssetsToolbarTreeTableController : AbstractToolbarBothTreesController<SortableCumulNode>, IDirty, System.IDisposable
	{
		public AssetsToolbarTreeTableController(DataAccessor accessor, CommandContext commandContext, BaseType baseType)
			: base (accessor, commandContext, baseType)
		{
			this.title = AbstractView.GetViewTitle (this.accessor, ViewType.Assets);

			//	GuidNode -> ParentPositionNode -> LevelNode -> TreeNode -> SortableCumulNode
			this.groupNodeGetter  = this.accessor.GetNodeGetter (BaseType.Groups);
			this.objectNodeGetter = this.accessor.GetNodeGetter (BaseType.Assets);
			this.nodeGetter = new ObjectsNodeGetter (this.accessor, this.groupNodeGetter, this.objectNodeGetter);
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


		public Guid								FilterGuid
		{
			get
			{
				return this.rootGuid;
			}
			set
			{
				if (this.rootGuid != value)
				{
					this.rootGuid = value;
					this.UpdateData ();
				}
			}
		}


		protected override void CreateControllerUI(Widget parent)
		{
			base.CreateControllerUI (parent);

			this.bottomFrame.Visibility = true;
			this.CreateStateAt (this.bottomFrame);
		}

		private void CreateStateAt(Widget parent)
		{
			this.stateAtController = new StateAtController (this.accessor);
			var frame = this.stateAtController.CreateUI (parent);
			frame.Dock = DockStyle.Left;

			this.stateAtController.DateChanged += delegate
			{
				this.SetDate (this.stateAtController.Date);
			};

			this.SetDate (Timestamp.Now.Date);
		}

		protected override void CreateGraphicControllerUI()
		{
			this.graphicController = new AssetsTreeGraphicController (this.accessor, this.baseType);
		}


		public override void UpdateData()
		{
			this.objectNodeGetter.Update ();
			this.NodeGetter.SetParams (this.timestamp, this.rootGuid, Guid.Empty, this.sortingInstructions);
			this.dataFiller.Timestamp = this.timestamp;

			this.UpdateController ();
			this.UpdateToolbar ();
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

		public Timestamp?						SelectedTimestamp
		{
			get
			{
				return this.selectedTimestamp;
			}
			set
			{
				this.selectedTimestamp = value;
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


		private void SetDate(System.DateTime? date)
		{
			//	Choix du timestamp visible dans tout le TreeTable.
			this.timestamp = null;

			if (date.HasValue)
			{
				this.timestamp = new Timestamp (date.Value, 0);
			}

			this.stateAtController.Date = date;

			using (new SaveSelectedGuid (this))
			{
				this.UpdateData ();
			}
		}


		protected override void CreateToolbar()
		{
			this.toolbar = new AssetsToolbar (this.accessor, this.commandContext);
			this.ConnectSearch ();
		}

		protected override void CreateNodeFiller()
		{
			this.dataFiller = new AssetsTreeTableFiller (this.accessor, this.nodeGetter);
			this.UpdateFillerTitle ();

			TreeTableFiller<SortableCumulNode>.FillColumns (this.treeTableController, this.dataFiller, "View.Assets");

			this.sortingInstructions = TreeTableFiller<SortableCumulNode>.GetSortingInstructions (this.treeTableController);
		}

		protected override void UpdateFillerTitle()
		{
			this.dataFiller.Title = this.FullTitle;
		}

		private string FullTitle
		{
			get
			{
				var list = new List<string> ();
				list.Add (this.title);

				if (this.stateAtController != null && this.stateAtController.Date.HasValue)
				{
					list.Add (TypeConverters.DateToString (this.stateAtController.Date));
				}

				return UniversalLogic.NiceJoin (list.ToArray ());
			}
		}


		[Command (Res.CommandIds.Assets.Filter)]
		protected void OnFilter(CommandDispatcher dispatcher, CommandEventArgs e)
		{
			var target = this.toolbar.GetTarget (e);

			FilterPopup.Show (target, this.accessor, this.rootGuid, delegate (Guid selectedGuid)
			{
				this.rootGuid = selectedGuid;

				using (new SaveSelectedGuid (this))
				{
					this.UpdateData ();
				}
			});
		}

		[Command (Res.CommandIds.Assets.Graphic)]
		protected override void OnGraphic()
		{
			base.OnGraphic ();
		}

		[Command (Res.CommandIds.Assets.First)]
		protected override void OnFirst()
		{
			base.OnFirst ();
		}

		[Command (Res.CommandIds.Assets.Prev)]
		protected override void OnPrev()
		{
			base.OnPrev ();
		}

		[Command (Res.CommandIds.Assets.Next)]
		protected override void OnNext()
		{
			base.OnNext ();
		}

		[Command (Res.CommandIds.Assets.Last)]
		protected override void OnLast()
		{
			base.OnLast ();
		}

		[Command (Res.CommandIds.Assets.CompactAll)]
		protected override void OnCompactAll()
		{
			base.OnCompactAll ();
		}

		[Command (Res.CommandIds.Assets.CompactOne)]
		protected override void OnCompactOne()
		{
			base.OnCompactOne ();
		}

		[Command (Res.CommandIds.Assets.ExpandOne)]
		protected override void OnExpandOne()
		{
			base.OnExpandOne ();
		}

		[Command (Res.CommandIds.Assets.ExpandAll)]
		protected override void OnExpandAll()
		{
			base.OnExpandAll ();
		}

		[Command (Res.CommandIds.Assets.Deselect)]
		protected void OnDeselect()
		{
			this.VisibleSelectedRow = -1;
		}

		[Command (Res.CommandIds.Assets.New)]
		protected void OnNew(CommandDispatcher dispatcher, CommandEventArgs e)
		{
			var target = this.toolbar.GetTarget (e);
			this.ShowCreatePopup (target);
		}

		[Command (Res.CommandIds.Assets.Delete)]
		protected void OnDelete(CommandDispatcher dispatcher, CommandEventArgs e)
		{
			var target = this.toolbar.GetTarget (e);

			var name = AssetsLogic.GetSummary (this.accessor, this.SelectedGuid, this.SelectedTimestamp);
			var question = string.Format(Res.Strings.ToolbarControllers.AssetsTreeTable.DeleteQuestion.ToString (), name);

			YesNoPopup.Show (target, question, delegate
			{
				this.accessor.UndoManager.Start ();
				var desc = UndoManager.GetDescription (Res.Commands.Assets.Delete.Description, AssetsLogic.GetSummary (this.accessor, this.SelectedGuid, this.SelectedTimestamp));
				this.accessor.UndoManager.SetDescription (desc);

				this.accessor.RemoveObject (BaseType.Assets, this.SelectedGuid);
				this.UpdateData ();
				this.OnUpdateAfterDelete ();

				this.accessor.UndoManager.SetAfterViewState ();
			});
		}

		[Command (Res.CommandIds.Assets.Copy)]
		protected override void OnCopy(CommandDispatcher dispatcher, CommandEventArgs e)
		{
			//	Copier un objet d'immobilisation requiert un popup pour choisir la date à considérer.
			var target = this.toolbar.GetTarget (e);
			var obj = this.accessor.GetObject (this.baseType, this.SelectedGuid);

			AssetCopyPopup.Show (target, this.accessor, obj, delegate (System.DateTime date)
			{
				var timestamp = new Timestamp (date, 0);
				this.accessor.Clipboard.CopyObject (this.accessor, this.baseType, obj, timestamp);
				this.UpdateToolbar ();
			});
		}

		[Command (Res.CommandIds.Assets.Paste)]
		protected override void OnPaste(CommandDispatcher dispatcher, CommandEventArgs e)
		{
			//	Coller un objet d'immobilisation requiert un popup pour choisir la date d'entrée.
			var target = this.toolbar.GetTarget (e);
			var summary = this.accessor.Clipboard.GetObjectSummary (this.baseType);

			AssetPastePopup.Show (target, this.accessor, summary, delegate (System.DateTime inputDate)
			{
				this.accessor.UndoManager.Start ();

				var obj = this.accessor.Clipboard.PasteObject (this.accessor, this.baseType, inputDate);
				if (obj == null)
				{
					MessagePopup.ShowPasteError (target);
				}
				else
				{
					this.UpdateData ();
					this.SelectedGuid = obj.Guid;
					this.OnUpdateAfterCreate (obj.Guid, EventType.Input, new Timestamp (inputDate, 0));
				}

				var desc = UndoManager.GetDescription (Res.Commands.Assets.Paste.Description, AssetsLogic.GetSummary (this.accessor, this.SelectedGuid, this.SelectedTimestamp));
				this.accessor.UndoManager.SetDescription (desc);
				this.accessor.UndoManager.SetAfterViewState ();
			});
		}

		[Command (Res.CommandIds.Assets.Export)]
		protected override void OnExport(CommandDispatcher dispatcher, CommandEventArgs e)
		{
			base.OnExport (dispatcher, e);
		}


		protected override void ShowContextMenu(Point pos)
		{
			//	Affiche le menu contextuel.
			MenuPopup.Show (this.toolbar, this.treeTableFrame, pos, null,
				Res.Commands.Assets.New,
				Res.Commands.Assets.Delete,
				null,
				Res.Commands.Assets.Copy,
				Res.Commands.Assets.Paste);
		}


		private void ShowCreatePopup(Widget target)
		{
			CreateAssetPopup.Show (target, this.accessor, delegate (bool preInput, System.DateTime date, IEnumerable<AbstractDataProperty> requiredProperties, decimal? value, Guid cat)
			{
				this.CreateAsset (preInput, date, requiredProperties, value, cat);
			});
		}

		private void CreateAsset(bool preInput, System.DateTime date, IEnumerable<AbstractDataProperty> requiredProperties, decimal? value, Guid cat)
		{
			this.accessor.UndoManager.Start ();

			var asset = AssetsLogic.CreateAsset (preInput, this.accessor, date, requiredProperties, value, cat);
			var guid = asset.Guid;

			this.UpdateData ();

			this.SelectedGuid = guid;
			this.SelectedTimestamp = AssetCalculator.GetLastTimestamp (asset);
			
			this.OnUpdateAfterCreate (guid, EventType.Input, this.selectedTimestamp.GetValueOrDefault ());

			var desc = UndoManager.GetDescription (Res.Commands.Assets.New.Description, AssetsLogic.GetSummary (this.accessor, guid, this.SelectedTimestamp));
			this.accessor.UndoManager.SetDescription (desc);
			this.accessor.UndoManager.SetAfterViewState ();
		}


		protected override void UpdateToolbar()
		{
			base.UpdateToolbar ();

			int row = this.VisibleSelectedRow;

			this.toolbar.SetActiveState (Res.Commands.Assets.Filter, !this.rootGuid.IsEmpty);
			this.toolbar.SetActiveState (Res.Commands.Assets.Graphic, this.showGraphic);

			this.UpdateSelCommand (Res.Commands.Assets.First, row, this.FirstRowIndex);
			this.UpdateSelCommand (Res.Commands.Assets.Prev,  row, this.PrevRowIndex);
			this.UpdateSelCommand (Res.Commands.Assets.Next,  row, this.NextRowIndex);
			this.UpdateSelCommand (Res.Commands.Assets.Last,  row, this.LastRowIndex);

			bool compactEnable = !this.NodeGetter.IsAllCompacted;
			bool expandEnable  = !this.NodeGetter.IsAllExpanded;

			this.toolbar.SetEnable (Res.Commands.Assets.CompactAll, compactEnable);
			this.toolbar.SetEnable (Res.Commands.Assets.CompactOne, compactEnable);
			this.toolbar.SetEnable (Res.Commands.Assets.ExpandOne,  expandEnable);
			this.toolbar.SetEnable (Res.Commands.Assets.ExpandAll,  expandEnable);

			this.toolbar.SetEnable (Res.Commands.Assets.New,      true);
			this.toolbar.SetEnable (Res.Commands.Assets.Delete,   row != -1);
			this.toolbar.SetEnable (Res.Commands.Assets.Deselect, row != -1);

			this.toolbar.SetEnable (Res.Commands.Assets.Copy,   this.IsCopyEnable);
			this.toolbar.SetEnable (Res.Commands.Assets.Paste,  this.accessor.Clipboard.HasObject (this.baseType));
			this.toolbar.SetEnable (Res.Commands.Assets.Export, !this.IsEmpty);
		}

		protected override bool IsCopyEnable
		{
			get
			{
				var obj = this.accessor.GetObject (this.baseType, this.SelectedGuid);
				return obj != null;
			}
		}


		private ObjectsNodeGetter NodeGetter
		{
			get
			{
				return this.nodeGetter as ObjectsNodeGetter;
			}
		}


		private GuidNodeGetter						groupNodeGetter;
		private GuidNodeGetter						objectNodeGetter;
		private StateAtController					stateAtController;
		private Timestamp?							selectedTimestamp;
		private Timestamp?							timestamp;
		private Guid								rootGuid;
	}
}

﻿//	Copyright © 2013, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Daniel ROUX, Maintainer: Daniel ROUX

using System.Collections.Generic;
using System.Linq;
using Epsitec.Common.Drawing;
using Epsitec.Common.Widgets;
using Epsitec.Cresus.Assets.App.Helpers;
using Epsitec.Cresus.Assets.App.Popups.StackedControllers;
using Epsitec.Cresus.Assets.App.Widgets;
using Epsitec.Cresus.Assets.Data;
using Epsitec.Cresus.Assets.Server.DataFillers;
using Epsitec.Cresus.Assets.Server.NodeGetters;
using Epsitec.Cresus.Assets.Server.SimpleEngine;

namespace Epsitec.Cresus.Assets.App.Popups
{
	/// <summary>
	/// Choix d'un filtre dans la base de données des groupes.
	/// On y accède à partir du bouton "trombone".
	/// </summary>
	public class FilterPopup : AbstractPopup
	{
		private FilterPopup(DataAccessor accessor, Guid selectedGuid)
		{
			this.accessor = accessor;

			this.controller = new NavigationTreeTableController (this.accessor);

			//	GuidNode -> ParentPositionNode -> LevelNode -> TreeNode
			var primaryNodeGetter = this.accessor.GetNodeGetter (BaseType.Groups);
			this.nodeGetter = new GroupTreeNodeGetter (this.accessor, BaseType.Groups, primaryNodeGetter);

			this.dataFiller = new SingleGroupsTreeTableFiller (this.accessor, this.nodeGetter, FilterPopup.popupWidth);
			this.nodeGetter.SetParams (null, this.dataFiller.DefaultSorting);

			this.visibleSelectedRow = this.nodeGetter.GetNodes ().ToList ().FindIndex (x => x.Guid == selectedGuid);
			this.hasFilter = (this.visibleSelectedRow != -1);

			//	Connexion des événements.
			this.controller.ContentChanged += delegate (object sender, bool crop)
			{
				this.UpdateController (crop);
			};

			this.controller.RowClicked += delegate (object sender, int row, int column)
			{
				this.visibleSelectedRow = this.controller.TopVisibleRow + row;
				this.UpdateController ();

				var node = this.nodeGetter[this.visibleSelectedRow];
				this.OnNavigate (node.Guid);
				this.ClosePopup ();
			};

			this.controller.TreeButtonClicked += delegate (object sender, int row, NodeType type)
			{
				this.OnCompactOrExpand (this.controller.TopVisibleRow + row);
			};
		}


		protected override Size DialogSize
		{
			get
			{
				return this.GetSize ();
			}
		}

		protected override void CreateUI()
		{
			this.CreateTitle (Res.Strings.Popup.Filter.Title.ToString ());
			this.CreateCloseButton ();

			this.CreateController ();
			this.CreateButton ();
		}

		private void CreateController()
		{
			var frame = new FrameBox
			{
				Parent = this.mainFrameBox,
				Dock   = DockStyle.Fill,
			};

			this.controller.CreateUI (frame, headerHeight: 0, footerHeight: 0);
			this.controller.AllowsMovement = false;
			this.controller.AllowsSorting  = false;

			TreeTableFiller<TreeNode>.FillColumns (this.controller, this.dataFiller, "Popup.Filter");
			this.InitialCompact ();
		}

		private void CreateButton()
		{
			if (this.hasFilter)
			{
				var button = new ColoredButton
				{
					Parent          = this.mainFrameBox,
					Text            = Res.Strings.Popup.Filter.GroupCancel.ToString (),
					PreferredHeight = 30,
					Dock            = DockStyle.Bottom,
				};

				button.Clicked += delegate
				{
					this.OnNavigate (Guid.Empty);
					this.ClosePopup ();
				};
			}
		}


		private void InitialCompact()
		{
			var guid = this.SelectedGuid;
			this.nodeGetter.SetLevel (1);
			this.SelectedGuid = guid;
		}

		private void OnCompactOrExpand(int row)
		{
			//	Etend ou compacte une ligne (inverse son mode actuel).
			var guid = this.SelectedGuid;

			this.nodeGetter.CompactOrExpand (row);
			this.UpdateController ();

			this.SelectedGuid = guid;
		}

		private Guid SelectedGuid
		{
			//	Retourne le Guid de l'objet actuellement sélectionné.
			get
			{
				int sel = this.visibleSelectedRow;
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
				this.visibleSelectedRow = this.nodeGetter.SearchBestIndex (value);
				this.UpdateController ();
			}
		}


		private Size GetSize()
		{
			var parent = this.GetParent ();

			double h = parent.ActualHeight
					 - AbstractScroller.DefaultBreadth;

			int dx = FilterPopup.popupWidth
				   + (int) AbstractScroller.DefaultBreadth;

			int dy = (int) (h * 0.5);

			return new Size (dx, dy);
		}

		private void UpdateController(bool crop = true)
		{
			TreeTableFiller<TreeNode>.FillContent (this.controller, this.dataFiller, this.visibleSelectedRow, crop);
		}


		#region Events handler
		private void OnNavigate(Guid guid)
		{
			this.Navigate.Raise (this, guid);
		}

		public event EventHandler<Guid> Navigate;
		#endregion


		#region Helpers
		public static void Show(Widget target, DataAccessor accessor, Guid selectedGuid, System.Action<Guid> action)
		{
			//	Affiche le Popup.
			var popup = new FilterPopup (accessor, selectedGuid);

			popup.Create (target, leftOrRight: true);

			popup.Navigate += delegate (object sender, Guid guid)
			{
				action (guid);
			};
		}
		#endregion

	
		private const int popupWidth = GroupGuidStackedController.ControllerWidth;

		private readonly DataAccessor					accessor;
		private readonly NavigationTreeTableController	controller;
		private readonly GroupTreeNodeGetter			nodeGetter;
		private readonly SingleGroupsTreeTableFiller	dataFiller;
		private  bool									hasFilter;

		private int										visibleSelectedRow;
	}
}
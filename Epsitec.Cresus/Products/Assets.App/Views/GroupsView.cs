﻿//	Copyright © 2013, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Daniel ROUX, Maintainer: Daniel ROUX

using System.Collections.Generic;
using System.Linq;
using Epsitec.Common.Support;
using Epsitec.Common.Widgets;
using Epsitec.Cresus.Assets.App.Widgets;
using Epsitec.Cresus.Assets.Server.SimpleEngine;

namespace Epsitec.Cresus.Assets.App.Views
{
	public class GroupsView : AbstractView
	{
		public GroupsView(DataAccessor accessor, MainToolbar toolbar)
			: base (accessor, toolbar)
		{
			this.baseType = BaseType.Groups;

			this.listController = new GroupsToolbarTreeTableController (this.accessor);
			this.objectEditor   = new ObjectEditor (this.accessor, this.baseType, isTimeless: true);
		}


		public override void Dispose()
		{
			this.mainToolbar.SetCommandState (ToolbarCommand.Edit,          ToolbarCommandState.Hide);
			this.mainToolbar.SetCommandState (ToolbarCommand.Amortissement, ToolbarCommandState.Hide);
			this.mainToolbar.SetCommandState (ToolbarCommand.Accept,        ToolbarCommandState.Hide);
			this.mainToolbar.SetCommandState (ToolbarCommand.Cancel,        ToolbarCommandState.Hide);
		}


		public override void CreateUI(Widget parent)
		{
			var topBox = new FrameBox
			{
				Parent  = parent,
				Dock    = DockStyle.Fill,
			};

			this.listFrameBox = new FrameBox
			{
				Parent = topBox,
				Dock   = DockStyle.Fill,
			};

			this.editFrameBox = new FrameBox
			{
				Parent         = topBox,
				Dock           = DockStyle.Right,
				PreferredWidth = 750,
				BackColor      = ColorManager.GetBackgroundColor (),
			};

			this.listController.CreateUI (this.listFrameBox);
			this.objectEditor.CreateUI (this.editFrameBox);

			this.listController.DirtyData = true;
			this.UpdateUI ();

			//	Connexion des événements de la liste des objets à gauche.
			{
				this.listController.StartEditing += delegate (object sender, EventType eventType, Timestamp timestamp)
				{
					this.OnStartEdit (eventType);
				};

				this.listController.SelectedRowChanged += delegate
				{
					if (this.ignoreChanges.IsZero)
					{
						this.UpdateAfterListChanged ();
					}
				};

				this.listController.RowDoubleClicked += delegate
				{
					this.OnListDoubleClicked ();
				};
			}

			//	Connexion des événements de l'éditeur.
			{
				this.objectEditor.ValueChanged += delegate (object sender, ObjectField field)
				{
					this.UpdateToolbars ();
				};

				this.objectEditor.UpdateData += delegate
				{
					this.UpdateUI ();
				};
			}
		}

		public override void UpdateUI()
		{
			if (!this.isEditing)
			{
				if (this.accessor.EditionAccessor.SaveObjectEdition ())
				{
					this.listController.DirtyData = true;
				}
			}

			this.listController.InUse = true;
			this.editFrameBox.Visibility = this.isEditing;

			//	Met à jour les données des différents contrôleurs.
			using (this.ignoreChanges.Enter ())
			{
				if (this.listController.InUse)
				{
					if (this.listController.DirtyData)
					{
						this.listController.UpdateData ();
						this.listController.SelectedGuid = this.selectedGuid;

						this.listController.DirtyData = false;
					}
					else if (this.listController.SelectedGuid != this.selectedGuid)
					{
						this.listController.SelectedGuid = this.selectedGuid;
					}
				}
			}

			this.UpdateToolbars ();
			this.UpdateEditor ();

			this.OnViewStateChanged (this.ViewState);
		}

	
		public override AbstractViewState ViewState
		{
			get
			{
				return new GroupsViewState
				{
					ViewType     = ViewType.Groups,
					PageType     = this.isEditing ? this.objectEditor.PageType : PageType.Unknown,
					SelectedGuid = this.selectedGuid,
				};
			}
			set
			{
				var viewState = value as GroupsViewState;
				System.Diagnostics.Debug.Assert (viewState != null);

				this.selectedGuid = viewState.SelectedGuid;

				if (viewState.PageType == PageType.Unknown)
				{
					this.isEditing = false;
				}
				else
				{
					this.isEditing = true;
					this.objectEditor.PageType = viewState.PageType;
				}

				this.UpdateUI ();
			}
		}


		public override void OnCommand(ToolbarCommand command)
		{
			base.OnCommand (command);

			switch (command)
			{
				case ToolbarCommand.Edit:
					this.OnStartStopEdit ();
					break;

				case ToolbarCommand.Accept:
					this.OnEditAccept ();
					break;

				case ToolbarCommand.Cancel:
					this.OnEditCancel ();
					break;
			}
		}


		private void OnListDoubleClicked()
		{
			this.OnStartEdit ();
		}

		private void OnStartStopEdit()
		{
			if (!this.isEditing && this.selectedGuid.IsEmpty)
			{
				return;
			}

			this.isEditing = !this.isEditing;
			this.UpdateUI ();
		}

		private void OnStartEdit()
		{
			if (!this.isEditing && this.selectedGuid.IsEmpty)
			{
				return;
			}

			this.isEditing = true;
			this.UpdateUI ();
		}

		private void OnStartEdit(EventType eventType)
		{
			//	Démarre une édition après avoir créé un événement.
			this.isEditing = true;
			this.objectEditor.PageType = this.objectEditor.MainPageType;

			this.UpdateUI ();
		}

		private void OnEditAccept()
		{
			this.isEditing = false;
			this.UpdateUI ();
		}

		private void OnEditCancel()
		{
			this.accessor.EditionAccessor.CancelObjectEdition ();
			this.isEditing = false;
			this.UpdateUI ();
		}


		private void UpdateAfterListChanged()
		{
			this.selectedGuid = this.listController.SelectedGuid;

			if (this.selectedGuid.IsEmpty)
			{
				this.isShowEvents = false;
				this.isEditing    = false;
			}

			this.UpdateUI ();
		}


		private void UpdateEditor()
		{
			var timestamp = this.GetLastTimestamp (this.selectedGuid);

			if (!timestamp.HasValue)
			{
				timestamp = Timestamp.Now;
			}

			this.objectEditor.SetObject (this.selectedGuid, timestamp);
		}


		private void UpdateToolbars()
		{
			if (this.isEditing)
			{
				this.mainToolbar.SetCommandState (ToolbarCommand.Edit, ToolbarCommandState.Activate);

				this.mainToolbar.SetCommandEnable (ToolbarCommand.Accept, this.objectEditor.EditionDirty);
				this.mainToolbar.SetCommandState (ToolbarCommand.Cancel, ToolbarCommandState.Enable);
			}
			else
			{
				this.mainToolbar.SetCommandEnable (ToolbarCommand.Edit, this.IsEditingPossible);

				this.mainToolbar.SetCommandState (ToolbarCommand.Accept, ToolbarCommandState.Hide);
				this.mainToolbar.SetCommandState (ToolbarCommand.Cancel, ToolbarCommandState.Hide);
			}

			this.mainToolbar.SetCommandState (ToolbarCommand.Amortissement, ToolbarCommandState.Enable);
		}

		private bool IsEditingPossible
		{
			get
			{
				return this.listController.SelectedRow != -1;
			}
		}


		private readonly GroupsToolbarTreeTableController	listController;
		private readonly ObjectEditor						objectEditor;

		private FrameBox									listFrameBox;
		private FrameBox									editFrameBox;

		private bool										isShowEvents;
		private bool										isEditing;
		private Guid										selectedGuid;
	}
}

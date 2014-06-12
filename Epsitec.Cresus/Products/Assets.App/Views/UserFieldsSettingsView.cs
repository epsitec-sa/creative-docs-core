﻿//	Copyright © 2013, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Daniel ROUX, Maintainer: Daniel ROUX

using System.Collections.Generic;
using System.Linq;
using Epsitec.Common.Widgets;
using Epsitec.Cresus.Assets.App.Widgets;
using Epsitec.Cresus.Assets.Data;
using Epsitec.Cresus.Assets.Server.SimpleEngine;

namespace Epsitec.Cresus.Assets.App.Views
{
	public class UserFieldsSettingsView : AbstractView
	{
		public UserFieldsSettingsView(DataAccessor accessor, MainToolbar toolbar, BaseType baseType)
			: base (accessor, toolbar)
		{
			this.baseType = baseType;

			this.listController = new UserFieldsToolbarTreeTableController (this.accessor, this.baseType);
			this.objectEditor   = new ObjectEditor (this.accessor, BaseType.UserFields, this.baseType, isTimeless: true);
		}


		public override void Dispose()
		{
			this.mainToolbar.SetCommandState (ToolbarCommand.Edit,   ToolbarCommandState.Hide);
			this.mainToolbar.SetCommandState (ToolbarCommand.Accept, ToolbarCommandState.Hide);
			this.mainToolbar.SetCommandState (ToolbarCommand.Cancel, ToolbarCommandState.Hide);
		}


		public override void CreateUI(Widget parent)
		{
			if (!this.objectEditor.HasError && this.accessor.EditionAccessor.SaveObjectEdition ())
			{
				this.DataChanged ();
			}

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
				PreferredWidth = AbstractView.editionWidth,
				BackColor      = ColorManager.GetBackgroundColor (),
			};

			this.listController.CreateUI (this.listFrameBox);
			this.objectEditor.CreateUI (this.editFrameBox);

			this.DeepUpdateUI ();

			//	Connexion des événements de la liste des objets à gauche.
			{
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

				this.listController.UpdateAfterCreate += delegate (object sender, Guid guid, EventType eventType, Timestamp timestamp)
				{
					this.OnUpdateAfterCreate (guid);
				};

				this.listController.UpdateAfterDelete += delegate
				{
					this.OnUpdateAfterDelete ();
				};
			}

			//	Connexion des événements de l'éditeur.
			{
				this.objectEditor.ValueChanged += delegate (object sender, ObjectField field)
				{
					this.UpdateToolbars ();
				};

				this.objectEditor.DataChanged += delegate
				{
					this.DataChanged ();
				};
			}
		}


		public override void DataChanged()
		{
			this.listController.DirtyData = true;
		}

		public override void DeepUpdateUI()
		{
			this.DataChanged ();
			this.UpdateUI ();
		}

		public override void UpdateUI()
		{
			if (!this.objectEditor.HasError && this.accessor.EditionAccessor.SaveObjectEdition ())
			{
				this.DataChanged ();
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
				ViewType viewType;

				switch (this.baseType)
				{
					case BaseType.Assets:
						viewType = ViewType.AssetsSettings;
						break;

					case BaseType.Persons:
						viewType = ViewType.PersonsSettings;
						break;

					default:
						throw new System.InvalidOperationException (string.Format ("Unknown BaseType {0}", this.baseType.ToString ()));
				}

				return new SettingsViewState
				{
					ViewType     = viewType,
					BaseType     = this.baseType,
					SelectedGuid = this.selectedGuid,
				};
			}
			set
			{
				var viewState = value as SettingsViewState;
				System.Diagnostics.Debug.Assert (viewState != null);

				this.selectedGuid = viewState.SelectedGuid;

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

		private void OnUpdateAfterCreate(Guid guid)
		{
			//	Démarre une édition après avoir créé un contact.
			this.isEditing = true;
			this.selectedGuid = guid;
			this.objectEditor.PageType = this.objectEditor.MainPageType;

			this.DeepUpdateUI ();
		}

		private void OnUpdateAfterDelete()
		{
			this.isEditing = false;
			this.selectedGuid = Guid.Empty;

			this.DeepUpdateUI ();
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
				this.isEditing = false;
			}

			this.UpdateUI ();
		}


		private void UpdateEditor()
		{
			this.objectEditor.SetObject (this.selectedGuid, Timestamp.MaxValue);
		}

		
		private void UpdateToolbars()
		{
			if (this.isEditing)
			{
				this.mainToolbar.SetCommandState (ToolbarCommand.Edit, ToolbarCommandState.Activate);

				this.mainToolbar.SetCommandEnable (ToolbarCommand.Accept, this.objectEditor.EditionDirty && !this.objectEditor.HasError);
				this.mainToolbar.SetCommandState (ToolbarCommand.Cancel, ToolbarCommandState.Enable);
			}
			else
			{
				this.mainToolbar.SetCommandEnable (ToolbarCommand.Edit, this.IsEditingPossible);

				this.mainToolbar.SetCommandState (ToolbarCommand.Accept, ToolbarCommandState.Hide);
				this.mainToolbar.SetCommandState (ToolbarCommand.Cancel, ToolbarCommandState.Hide);
			}
		}

		private bool IsEditingPossible
		{
			get
			{
				return this.listController.SelectedRow != -1;
			}
		}


		private readonly UserFieldsToolbarTreeTableController listController;
		private readonly ObjectEditor						objectEditor;

		private FrameBox									listFrameBox;
		private FrameBox									editFrameBox;

		private bool										isEditing;
		private Guid										selectedGuid;
	}
}

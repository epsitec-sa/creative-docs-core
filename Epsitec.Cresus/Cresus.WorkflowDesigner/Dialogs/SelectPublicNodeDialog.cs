//	Copyright � 2007-2008, EPSITEC SA, 1400 Yverdon-les-Bains, Switzerland
//	Author: Daniel ROUX, Maintainer: Daniel ROUX

using Epsitec.Common.Dialogs;
using Epsitec.Common.Drawing;
using Epsitec.Common.IO;
using Epsitec.Common.Support;
using Epsitec.Common.Support.EntityEngine;
using Epsitec.Common.Types;
using Epsitec.Common.Types.Collections;
using Epsitec.Common.Widgets;

using Epsitec.Cresus.Core.Entities;
using Epsitec.Cresus.Core.Business;
using Epsitec.Cresus.DataLayer.Context;

using System.Collections.Generic;
using System.Linq;

namespace Epsitec.Cresus.WorkflowDesigner.Dialogs
{
	public class SelectPublicNodeDialog : AbstractDialog
	{
		public SelectPublicNodeDialog(Widget parent, BusinessContext businessContext)
		{
			this.parent = parent;
			this.businessContext = businessContext;

			this.workflowNodeEntities = new List<WorkflowNodeEntity> ();
		}


		public WorkflowNodeEntity NodeEntity
		{
			get
			{
				int sel = this.listNodes.SelectedItemIndex;

				if (sel == -1)
				{
					return null;
				}
				else
				{
					var node = this.workflowNodeEntities[sel];
					return this.businessContext.GetLocalEntity (node);
				}
			}
		}


		protected override Window CreateWindow()
		{
			this.CreateUserInterface ("SelectPublicNode", new Size (600, 400), "Choix d'un noeud public", this.parent.Window);
			this.UpdateLists ();
			this.UpdateButtons ();

			window.AdjustWindowSize ();

			return this.window;
		}

		protected Rectangle GetOwnerBounds()
		{
			//	Donne les fronti�res de l'application.
			var w = this.parent.Window;

			return new Rectangle (w.WindowLocation, w.WindowSize);
		}


		private void CreateUserInterface(string name, Size windowSize, string title, Window owner)
		{
			//	Cr�e la fen�tre et tous les widgets pour peupler le dialogue.
			this.window = new Window ();
			this.window.MakeSecondaryWindow ();
			this.window.PreventAutoClose = true;
			this.window.Name = name;
			this.window.Text = title;
			this.window.Owner = owner;
			this.window.Icon = owner == null ? null : this.window.Owner.Icon;
			this.window.ClientSize = windowSize;
			this.window.Root.Padding = new Margins (10);

			this.window.WindowCloseClicked += this.HandleWindowCloseClicked;

			var main = new FrameBox
			{
				Parent = this.window.Root,
				Dock = DockStyle.Fill,
			};

			var footer = new FrameBox
			{
				Parent = this.window.Root,
				Dock = DockStyle.Bottom,
				Margins = new Margins (0, 0, 10, 0),
			};

			this.CreateMain (main);
			this.CreateFooter (footer);
		}

		private void CreateMain(Widget parent)
		{
			var leftBox = new FrameBox
			{
				Parent = parent,
				PreferredWidth = 200,
				Dock = DockStyle.Left,
				Margins = new Margins (0, 0, 0, 0),
			};

			var rightBox = new FrameBox
			{
				Parent = parent,
				Dock = DockStyle.Fill,
				Margins = new Margins (10, 0, 0, 0),
			};

			//	Cr�e la partie de gauche.
			var leftLabel = new StaticText
			{
				Parent = leftBox,
				Text = "Workflows :",
				Dock = DockStyle.Top,
				Margins = new Margins (0, 0, 0, 5),
			};

			this.listEntties = new ScrollList
			{
				Parent = leftBox,
				Dock = DockStyle.Fill,
			};

			this.listEntties.SelectedItemChanged += new EventHandler (this.HandlelistEnttiesSelectedItemChanged);

			//	Cr�e la partie de droite.
			var rightLabel = new StaticText
			{
				Parent = rightBox,
				Text = "Noeuds publics :",
				Dock = DockStyle.Top,
				Margins = new Margins (0, 0, 0, 5),
			};

			this.listNodes = new ScrollList
			{
				Parent = rightBox,
				Dock = DockStyle.Fill,
			};

			this.listNodes.SelectedItemChanged += new EventHandler (this.HandlelistNodesSelectedItemChanged);
		}

		private void CreateFooter(Widget parent)
		{
			this.cancelButton = new Button
			{
				Parent = parent,
				Text = "Annuler",
				Dock = DockStyle.Right,
				Margins = new Margins (10, 0, 0, 0),
			};

			this.cancelButton.Clicked += new EventHandler<MessageEventArgs> (this.HandleCancelButtonClicked);

			this.acceptButton = new Button
			{
				Parent = parent,
				Text = "Choisir",
				Dock = DockStyle.Right,
				Margins = new Margins (10, 0, 0, 0),
			};

			this.acceptButton.Clicked += new EventHandler<MessageEventArgs> (this.HandleAcceptButtonClicked);
		}


		private void HandlelistEnttiesSelectedItemChanged(object sender)
		{
			this.UpdateListNodes ();
			this.UpdateButtons ();
		}

		private void HandlelistNodesSelectedItemChanged(object sender)
		{
			this.UpdateButtons ();
		}

		private void HandleCancelButtonClicked(object sender, MessageEventArgs e)
		{
			this.Result = DialogResult.Cancel;
			this.CloseDialog ();
		}

		private void HandleAcceptButtonClicked(object sender, MessageEventArgs e)
		{
			this.Result = DialogResult.Accept;
			this.CloseDialog ();
		}

		private void HandleWindowCloseClicked(object sender)
		{
			//	Fen�tre ferm�e.
			this.OnDialogClosed ();
			this.CloseDialog ();
		}


		private void UpdateLists()
		{
			this.UpdateListEntities ();
			this.UpdateListNodes ();
		}

		private void UpdateListEntities()
		{
			this.listEntties.Items.Clear ();

			this.workflowDefinitionEntities = this.businessContext.Data.GetAllEntities<WorkflowDefinitionEntity> ().ToList ();
			foreach (var def in this.workflowDefinitionEntities)
			{
				this.listEntties.Items.Add (def.WorkflowName.ToString ());
			}
		}

		private void UpdateListNodes()
		{
			this.workflowNodeEntities.Clear ();
			this.listNodes.Items.Clear ();

			int sel = this.listEntties.SelectedItemIndex;

			if (sel != -1)
			{
				var def = this.workflowDefinitionEntities[sel];
				var list = Entity.DeepSearch (def).Where (x => x is WorkflowNodeEntity).Cast<WorkflowNodeEntity> ().OrderBy (x => x.Name);

				foreach (var entity in list)
				{
					if (entity is WorkflowNodeEntity)
					{
						var node = entity as WorkflowNodeEntity;

						if (node.IsPublic)
						{
							this.workflowNodeEntities.Add (node);
							this.listNodes.Items.Add (this.GetNodeDescription (node));
						}
					}
				}
			}
		}

		private string GetNodeDescription(WorkflowNodeEntity node)
		{
			string text = node.Name.ToString ();

			if (!node.Description.IsNullOrWhiteSpace)
			{
				string desc = node.Description.ToString ().Replace ("<br/>", ", ");
				text += string.Concat (" (", desc, ")");
			}

			return text;
		}

		private void UpdateButtons()
		{
			this.acceptButton.Enable = this.listEntties.SelectedItemIndex != -1 && this.listNodes.SelectedItemIndex != -1;
		}


		private readonly Widget					parent;
		private readonly BusinessContext		businessContext;

		private Window							window;
		private ScrollList						listEntties;
		private ScrollList						listNodes;
		private Button							acceptButton;
		private Button							cancelButton;
		private List<WorkflowDefinitionEntity>	workflowDefinitionEntities;
		private List<WorkflowNodeEntity>		workflowNodeEntities;
	}
}

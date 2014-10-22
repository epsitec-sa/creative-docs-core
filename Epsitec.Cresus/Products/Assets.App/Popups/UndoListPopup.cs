﻿//	Copyright © 2013, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Daniel ROUX, Maintainer: Daniel ROUX

using System.Collections.Generic;
using System.Linq;
using Epsitec.Common.Drawing;
using Epsitec.Common.Widgets;
using Epsitec.Cresus.Assets.App.Helpers;
using Epsitec.Cresus.Assets.App.Widgets;
using Epsitec.Cresus.Assets.Server.SimpleEngine;

namespace Epsitec.Cresus.Assets.App.Popups
{
	/// <summary>
	/// Permet de choisir une ou plusieurs actions à annuler/rétablir.
	/// </summary>
	public class UndoListPopup : AbstractPopup
	{
		private UndoListPopup(DataAccessor accessor, IEnumerable<string> undoList, bool undo)
		{
			this.accessor = accessor;
			this.undoList = undoList.ToArray ();
			this.undo     = undo;

			this.buttons = new List<ColoredButton> ();
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
			this.CreateTitle (" ");  // titre quelconque mais non nul
			this.CreateCloseButton ();

			this.CreateButtons ();
		}

		private void CreateButtons()
		{
			this.buttons.Clear ();
			int rank = 0;

			foreach (var description in this.undoList)
			{
				string text;

				if (this.undo)
				{
					text = string.Format (Res.Strings.Popup.UndoList.Undo.Button.ToString (), description);
				}
				else
				{
					text = string.Format (Res.Strings.Popup.UndoList.Redo.Button.ToString (), description);
				}

				var button = new ColoredButton
				{
					Parent           = this.mainFrameBox,
					Text             = "  " + text,
					ContentAlignment = ContentAlignment.MiddleLeft,
					PreferredHeight  = UndoListPopup.buttonHeight,
					Dock             = DockStyle.Top,
					NormalColor      = ColorManager.EditBackgroundColor,
					HoverColor       = ColorManager.HoverColor,
					SelectedColor    = ColorManager.SelectionColor,
					TabIndex         = rank++,
				};

				this.buttons.Add (button);

				button.Entered += delegate (object sender, MessageEventArgs e)
				{
					var b = sender as ColoredButton;
					this.Update (b.TabIndex);
				};

				button.Exited += delegate (object sender, MessageEventArgs e)
				{
					this.Update (-1);
				};

				button.Clicked += delegate (object sender, MessageEventArgs e)
				{
					var b = sender as ColoredButton;
					this.OnAction (b.TabIndex+1);
					this.ClosePopup ();
				};
			}

			this.Update (-1);
		}


		private void Update(int rank)
		{
			for (int i=0; i<this.buttons.Count; i++)
			{
				this.buttons[i].ActiveState = (i <= rank) ? ActiveState.Yes : ActiveState.No;
			}

			if (rank == -1)
			{
				if (this.undo)
				{
					this.titleStaticText.Text = Res.Commands.Main.UndoList.Description;
				}
				else
				{
					this.titleStaticText.Text = Res.Commands.Main.RedoList.Description;
				}
			}
			else if (rank == 0)  // undo/redo une action ?
			{
				if (this.undo)
				{
					this.titleStaticText.Text = string.Format (Res.Strings.Popup.UndoList.Undo.TitleOne.ToString (), rank+1);
				}
				else
				{
					this.titleStaticText.Text = string.Format (Res.Strings.Popup.UndoList.Redo.TitleOne.ToString (), rank+1);
				}
			}
			else  // undo/redo plusieurs actions ?
			{
				if (this.undo)
				{
					this.titleStaticText.Text = string.Format (Res.Strings.Popup.UndoList.Undo.TitleMany.ToString (), rank+1);
				}
				else
				{
					this.titleStaticText.Text = string.Format (Res.Strings.Popup.UndoList.Redo.TitleMany.ToString (), rank+1);
				}
			}
		}


		private Size GetSize()
		{
			int dx = UndoListPopup.popupWidth
				   + (int) AbstractScroller.DefaultBreadth;

			int dy = AbstractPopup.titleHeight
				   + UndoListPopup.buttonHeight * this.undoList.Length;

			return new Size (dx, dy);
		}


		#region Events handler
		private void OnAction(int count)
		{
			this.Action.Raise (this, count);
		}

		public event EventHandler<int> Action;
		#endregion


		#region Helpers
		public static void Show(Widget target, DataAccessor accessor, IEnumerable<string> undoList, bool undo, System.Action<int> action)
		{
			if (target != null)
			{
				var popup = new UndoListPopup (accessor, undoList, undo);

				popup.Create (target, leftOrRight: false);

				popup.Action += delegate (object sender, int count)
				{
					action (count);
				};
			}
		}
		#endregion


		private const int popupWidth = 200;
		private const int buttonHeight = 18;

		private readonly DataAccessor					accessor;
		private readonly string[]						undoList;
		private readonly bool							undo;
		private readonly List<ColoredButton>			buttons;
	}
}
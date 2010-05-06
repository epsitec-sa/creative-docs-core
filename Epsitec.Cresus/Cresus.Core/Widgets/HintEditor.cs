﻿//	Copyright © 2010, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Daniel ROUX, Maintainer: Daniel ROUX

using Epsitec.Common.Support;
using Epsitec.Common.Types;
using Epsitec.Common.Drawing;
using Epsitec.Common.Widgets;
using Epsitec.Common.Widgets.Helpers;

using System.Collections.Generic;
using System.Linq;

namespace Epsitec.Cresus.Core.Widgets
{
	public class HintEditor : TextFieldEx, Common.Widgets.Collections.IStringCollectionHost, Common.Support.Data.INamedStringSelection
	{
		public HintEditor()
		{
			this.TextDisplayMode = TextFieldDisplayMode.ActiveHint;
			this.DefocusAction = Common.Widgets.DefocusAction.AcceptEdition;

			this.items = new Common.Widgets.Collections.StringCollection (this);
			this.selectedRow = -1;
		}

		public HintEditor(Widget embedder)
			: this ()
		{
			this.SetEmbedder (embedder);
		}

		#region IStringCollectionHost Members

		public void NotifyStringCollectionChanged()
		{
		}

		public Common.Widgets.Collections.StringCollection Items
		{
			get
			{
				return this.items;
			}
		}

		#endregion

		#region INamedStringSelection Members

		public string SelectedName
		{
			get
			{
				if (this.selectedRow == -1)
				{
					return null;
				}

				return this.items.GetName (this.selectedRow);
			}
			set
			{
				if (this.SelectedName != value)
				{
					int index = -1;

					if (value != null)
					{
						index = this.items.FindIndexByName (value);

						if (index < 0)
						{
							throw new System.ArgumentException (string.Format ("No element named '{0}' in list", value));
						}
					}

					this.SelectedIndex = index;
				}
			}
		}

		#endregion

		#region IStringSelection Members

		public int SelectedIndex
		{
			get
			{
				return this.selectedRow;
			}
			set
			{
				if (value != -1)
				{
					value = System.Math.Max (value, 0);
					value = System.Math.Min (value, this.items.Count-1);
				}
				if (value != this.selectedRow)
				{
					this.selectedRow = value;
					this.OnSelectedIndexChanged ();
				}
			}
		}

		public string SelectedItem
		{
			get
			{
				int index = this.SelectedIndex;
				
				if (index < 0)
				{
					return null;
				}

				return this.Items[index];
			}
			set
			{
				this.SelectedIndex = this.Items.IndexOf (value);
			}
		}

		public event EventHandler  SelectedIndexChanged
		{
			add
			{
				this.AddUserEventHandler("SelectedIndexChanged", value);
			}
			remove
			{
				this.RemoveUserEventHandler("SelectedIndexChanged", value);
			}
		}

		#endregion


		public virtual bool IsComboOpen
		{
			get
			{
				return this.menu != null;
			}
		}
		

		private void HintSearching(string typed)
		{
			List<int> menuIndex = new List<int> ();

			typed = Misc.RemoveAccentsToLower (typed);

			if (!string.IsNullOrEmpty (typed))
			{
				//	Met d'abord les textes qui commencent par la proposition.
				for (int i=0; i<this.items.Count; i++)
				{
					var item = this.items[i];

					string name = Misc.RemoveAccentsToLower (item.ToString ());

					if (name.StartsWith (typed))
					{
						menuIndex.Add (i);
					}
				}

				//	Met ensuite les textes qui contiennent la propostion.
				for (int i=0; i<this.items.Count; i++)
				{
					var item = this.items[i];

					string name = Misc.RemoveAccentsToLower (item.ToString ());

					if (!name.StartsWith (typed) && name.Contains (typed))
					{
						menuIndex.Add (i);
					}
				}
			}

			if (menuIndex.Count == 0)
			{
				this.HintText = null;
				this.SetError (!string.IsNullOrEmpty (this.Text));
			}
			else
			{
				int index = menuIndex[0];  // la première proposition

				var item = this.items[index];

				this.SelectedIndex = index;
				this.HintText = item.ToString ();
				this.SetError (false);
			}

			if (menuIndex.Count > 1)
			{
				this.OpenCombo (menuIndex);
			}
		}


		protected virtual void OnSelectedIndexChanged()
		{
			//	Génère un événement pour dire que la sélection dans la liste a changé.
			EventHandler handler = (EventHandler) this.GetUserEventHandler ("SelectedIndexChanged");
			if (handler != null)
			{
				handler (this);
			}
		}

		protected override bool AboutToGetFocus(TabNavigationDir dir, TabNavigationMode mode, out Widget focus)
		{
			// TODO: Ne fonctionne toujours pas.
			this.SelectAll ();
			return base.AboutToGetFocus (dir, mode, out focus);
		}

		protected override void OnTextChanged()
		{
			base.OnTextChanged ();

			this.HintSearching (this.Text);
		}

		protected override void OnEditionAccepted()
		{
			base.OnEditionAccepted ();

			this.Text = this.HintText;
		}



		private void OpenCombo(List<int> menuIndex)
		{
			//	Rend la liste visible et démarre l'interaction.

			if (this.IsComboOpen)
			{
				return;
			}

			CancelEventArgs cancelEvent = new CancelEventArgs ();
			this.OnComboOpening (cancelEvent);

			if (cancelEvent.Cancel)
			{
				return;
			}

			this.menu = this.CreateMenu (menuIndex);

			if (this.menu == null)
			{
				return;
			}

			this.menu.AutoDispose = true;
			this.menu.ShowAsComboList (this, this.MapClientToScreen (new Point (0, 0)), this);

			if (this.scrollList != null)
			{
				this.scrollList.SelectedIndex = this.MapIndexToComboList (0);
				this.scrollList.ShowSelected (ScrollShowMode.Center);
			}

			this.menu.Accepted += this.HandleMenuAccepted;
			this.menu.Rejected += this.HandleMenuRejected;

			if (this.scrollList != null)
			{
				this.scrollList.SelectedIndexChanged += this.HandleScrollerSelectedIndexChanged;
				this.scrollList.SelectionActivated   += this.HandleScrollListSelectionActivated;
			}

			this.OnComboOpened ();
		}

		private void CloseCombo(CloseMode mode)
		{
			//	Ferme la liste (si nécessaire) et valide/rejette la modification
			//	en fonction du mode spécifié.

			if (this.menu.IsMenuOpen)
			{
				switch (mode)
				{
					case CloseMode.Reject:
						this.menu.Behavior.Reject ();
						return;
					case CloseMode.Accept:
						this.menu.Behavior.Accept ();
						return;
				}
			}

			this.menu.Accepted -= this.HandleMenuAccepted;
			this.menu.Rejected -= this.HandleMenuRejected;

			if (this.scrollList != null)
			{
				this.scrollList.SelectionActivated   -= this.HandleScrollListSelectionActivated;
				this.scrollList.SelectedIndexChanged -= this.HandleScrollerSelectedIndexChanged;

				this.scrollList.Dispose ();
				this.scrollList = null;
			}

			//-			this.menu.Dispose ();
			this.menu = null;

			this.SelectAll ();

			if (this.AutoFocus)
			{
				this.Focus ();
			}

			switch (mode)
			{
				case CloseMode.Reject:
					this.RejectEdition ();
					break;
				case CloseMode.Accept:
					this.AcceptEdition ();
					break;
			}

			this.OnComboClosed ();

			if (this.InitialText != this.Text)
			{
				this.OnSelectedIndexChanged ();
			}
		}

		public static void AdjustComboSize(Widget parent, AbstractMenu menu, bool isScrollable)
		{
			menu.AdjustSize ();

			if (isScrollable)
			{
				MenuItem.SetMenuHost (parent, new ScrollableMenuHost (menu));
			}
			else
			{
				MenuItem.SetMenuHost (parent, new FixMenuHost (menu));
			}
		}

		public static void AdjustScrollListWidth(ScrollList scrollList)
		{
			Size size = scrollList.GetBestFitSizeBasedOnContent ();

			scrollList.RowHeight      = size.Height;
			scrollList.PreferredWidth = size.Width;
		}

		private AbstractMenu CreateMenu(List<int> menuIndex)
		{
			var menu = new HintComboMenu ();

			menu.MinWidth = this.ActualWidth;

			this.scrollList = new ScrollList ();
			this.scrollList.ScrollListStyle = ScrollListStyle.Menu;

			menu.Contents = this.scrollList;

			//	Remplit la liste :
			this.CopyItemsToComboList (menuIndex, this.scrollList.Items);

			TextFieldCombo.AdjustScrollListWidth (this.scrollList);
			TextFieldCombo.AdjustComboSize (this, menu, true);

			return menu;
		}

		private void ProcessComboActivatedIndex(int sel)
		{
			//	Cette méthode n'est appelée que lorsque le contenu de la liste déroulée
			//	est validée par un clic de souris, au contraire de ProcessComboSelectedIndex
			//	qui est appelée à chaque changement "visuel".

			int index = this.MapComboListToIndex (sel);

			if (index >= 0)
			{
				this.SelectedIndex = index;
				this.menu.Behavior.Accept ();
			}
		}

		private void ProcessComboSelectedIndex(int sel)
		{
			//	Met à jour le contenu de la combo en cas de changement de sélection
			//	dans la liste.

			this.SelectedIndex = this.MapComboListToIndex (sel);
		}


		private void CopyItemsToComboList(List<int> menuIndex, Common.Widgets.Collections.StringCollection list)
		{
			for (int index = 0; index < menuIndex.Count; index++)
			{
				int i = menuIndex[index];

				string name = this.items.GetName (i);
				string text = this.items[i];

				list.Add (name, text);
			}
		}

		private int MapComboListToIndex(int value)
		{
			return (value < 0) ? -1 : value;
		}

		private int MapIndexToComboList(int value)
		{
			return (value < 0) ? -1 : value;
		}


		#region CloseMode Enumeration
		private enum CloseMode
		{
			Accept,
			Reject
		}
		#endregion


		#region ScrollableMenuHost Class
		protected class ScrollableMenuHost : IMenuHost
		{
			public ScrollableMenuHost(AbstractMenu menu)
			{
				this.menu = menu;
			}


			#region IMenuHost Members
			public void GetMenuDisposition(Widget item, ref Size size, out Point location, out Animation animation)
			{
				//	Détermine la hauteur maximale disponible par rapport à la position
				//	actuelle :
				Point pos = Common.Widgets.Helpers.VisualTree.MapVisualToScreen (item, new Point (0, 0));
				Point hot = Common.Widgets.Helpers.VisualTree.MapVisualToScreen (item, new Point (0, 0));
				ScreenInfo screenInfo  = ScreenInfo.Find (hot);
				Rectangle workingArea = screenInfo.WorkingArea;

				double maxHeight = pos.Y - workingArea.Bottom;

				if (maxHeight > size.Height || maxHeight > 100)
				{
					//	Il y a assez de place pour dérouler le menu vers le bas,
					//	mais il faudra peut-être le raccourcir un bout :
					this.menu.MaxSize = new Size (this.menu.MaxWidth, maxHeight);
					this.menu.AdjustSize ();

					size      = this.menu.ActualSize;
					location  = pos;
					animation = Animation.RollDown;
				}
				else
				{
					//	Il faut dérouler le menu vers le haut.
					pos.Y += item.ActualHeight-2;

					maxHeight = workingArea.Top - pos.Y;

					this.menu.MaxSize = new Size (this.menu.MaxWidth, maxHeight);
					this.menu.AdjustSize ();

					pos.Y += this.menu.ActualHeight;

					size      = this.menu.ActualSize;
					location  = pos;
					animation = Animation.RollUp;
				}

				location.X -= this.menu.MenuShadow.Left;
				location.Y -= size.Height;

				if (location.X + size.Width > workingArea.Right)
				{
					location.X = workingArea.Right - size.Width;
				}
			}
			#endregion

			private AbstractMenu				menu;
		}
		#endregion

		#region FixMenuHost Class
		protected class FixMenuHost : IMenuHost
		{
			public FixMenuHost(AbstractMenu menu)
			{
				this.menu = menu;
			}


			#region IMenuHost Members
			public void GetMenuDisposition(Widget item, ref Size size, out Point location, out Animation animation)
			{
				//	Détermine la hauteur maximale disponible par rapport à la position
				//	actuelle :
				Point pos = Common.Widgets.Helpers.VisualTree.MapVisualToScreen (item, new Point (0, 0));
				Point hot = Common.Widgets.Helpers.VisualTree.MapVisualToScreen (item, new Point (0, 0));
				ScreenInfo screenInfo  = ScreenInfo.Find (hot);
				Rectangle workingArea = screenInfo.WorkingArea;

				this.menu.MaxSize = new Size (this.menu.MaxWidth, workingArea.Height);
				this.menu.AdjustSize ();

				if (this.menu.PreferredHeight < pos.Y-workingArea.Bottom)
				{
					//	Il y a assez de place pour dérouler le menu vers le bas.
					size      = this.menu.PreferredSize;
					location  = pos;
					animation = Animation.RollDown;
				}
				else if (this.menu.PreferredHeight < workingArea.Top-(pos.Y+item.ActualHeight))
				{
					//	Il y a assez de place pour dérouler le menu vers le haut.
					pos.Y += item.ActualHeight;
					pos.Y += this.menu.PreferredHeight;

					size      = this.menu.PreferredSize;
					location  = pos;
					animation = Animation.RollUp;
				}
				else
				{
					//	Il faut dérouler le menu vers le bas, mais depuis en dessus du bouton.
					pos.Y = workingArea.Bottom+this.menu.PreferredHeight;

					size      = this.menu.PreferredSize;
					location  = pos;
					animation = Animation.RollDown;
				}

				location.X -= this.menu.MenuShadow.Left;
				location.Y -= size.Height;

				if (location.X + size.Width > workingArea.Right)
				{
					location.X = workingArea.Right - size.Width;
				}
			}
			#endregion

			private AbstractMenu				menu;
		}
		#endregion


		public event EventHandler<CancelEventArgs> ComboOpening
		{
			add
			{
				this.AddUserEventHandler ("ComboOpening", value);
			}
			remove
			{
				this.RemoveUserEventHandler ("ComboOpening", value);
			}
		}

		public event EventHandler ComboOpened
		{
			add
			{
				this.AddUserEventHandler ("ComboOpened", value);
			}
			remove
			{
				this.RemoveUserEventHandler ("ComboOpened", value);
			}
		}

		public event EventHandler ComboClosed
		{
			add
			{
				this.AddUserEventHandler ("ComboClosed", value);
			}
			remove
			{
				this.RemoveUserEventHandler ("ComboClosed", value);
			}
		}


		private void HandleScrollListSelectionActivated(object sender)
		{
			//	L'utilisateur a cliqué dans la liste pour terminer son choix.

			this.ProcessComboActivatedIndex (this.scrollList.SelectedIndex);
		}

		private void HandleScrollerSelectedIndexChanged(object sender)
		{
			//	L'utilisateur a simplement déplacé la souris dans la liste.

			this.ProcessComboSelectedIndex (this.scrollList.SelectedIndex);
		}

		private void HandleMenuAccepted(object sender)
		{
			this.CloseCombo (CloseMode.Accept);
		}

		private void HandleMenuRejected(object sender)
		{
			this.CloseCombo (CloseMode.Reject);
		}


		private void OnComboOpening(CancelEventArgs e)
		{
			EventHandler<CancelEventArgs> handler = (EventHandler<CancelEventArgs>) this.GetUserEventHandler ("ComboOpening");

			if (handler != null)
			{
				handler (this, e);
			}
		}

		private void OnComboOpened()
		{
			System.Diagnostics.Debug.Assert (this.IsComboOpen == true);

			this.UpdateButtonVisibility ();

			EventHandler handler = (EventHandler) this.GetUserEventHandler ("ComboOpened");

			if (handler != null)
			{
				handler (this);
			}
		}

		private void OnComboClosed()
		{
			System.Diagnostics.Debug.Assert (this.IsComboOpen == false);

			this.UpdateButtonVisibility ();

			EventHandler handler = (EventHandler) this.GetUserEventHandler ("ComboClosed");

			if (handler != null)
			{
				handler (this);
			}
		}

		
		private readonly Common.Widgets.Collections.StringCollection items;
		private int selectedRow;
		private AbstractMenu menu;
		private ScrollList scrollList;
	}
}

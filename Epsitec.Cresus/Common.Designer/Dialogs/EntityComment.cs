using System.Collections.Generic;
using Epsitec.Common.Widgets;
using Epsitec.Common.Support;
using Epsitec.Common.Drawing;
using Epsitec.Common.Types;

namespace Epsitec.Common.Designer.Dialogs
{
	/// <summary>
	/// Dialogue permettant d'�diter le texte d'un commentaire dans l'�diteur d'entit�s.
	/// </summary>
	public class EntityComment : Abstract
	{
		public EntityComment(MainWindow mainWindow) : base(mainWindow)
		{
		}

		public override void Show()
		{
			//	Cr�e et montre la fen�tre du dialogue.
			if ( this.window == null )
			{
				this.window = new Window();
				this.window.MakeSecondaryWindow();
				this.window.PreventAutoClose = true;
				this.WindowInit("EntityComment", 350, 250, true);
				this.window.Text = "Commentaire";  // Res.Strings.Dialog.EntityComment.Title;
				this.window.Owner = this.parentWindow;
				this.window.WindowCloseClicked += new EventHandler(this.HandleWindowCloseClicked);
				this.window.Root.MinSize = new Size(200, 150);
				this.window.Root.Padding = new Margins(8, 8, 8, 8);

				ResizeKnob resize = new ResizeKnob(this.window.Root);
				resize.Anchor = AnchorStyles.BottomRight;
				resize.Margins = new Margins(0, -8, 0, -8);
				ToolTip.Default.SetToolTip(resize, Res.Strings.Dialog.Tooltip.Resize);

				int tabIndex = 0;

				//	Cr�e la toolbar et son contenu.
				this.toolbar = new HToolBar(this.window.Root);
				this.toolbar.Margins = new Margins(0, 0, 0, 3);
				this.toolbar.Dock = DockStyle.Top;

				this.CreateButton("FontBold");
				this.CreateButton("FontItalic");
				this.CreateButton("FontUnderline");

				//	Cr�e le grand pav� de texte �ditable.
				this.fieldText = new TextFieldMulti(this.window.Root);
				this.fieldText.Dock = DockStyle.Fill;
				this.fieldText.TabIndex = tabIndex++;

				//	Boutons de fermeture.
				Widget footer = new Widget(this.window.Root);
				footer.PreferredHeight = 22;
				footer.Margins = new Margins(0, 0, 8, 0);
				footer.Dock = DockStyle.Bottom;

				this.buttonOk = new Button(footer);
				this.buttonOk.PreferredWidth = 75;
				this.buttonOk.Text = Res.Strings.Dialog.Button.OK;
				this.buttonOk.ButtonStyle = ButtonStyle.DefaultAccept;
				this.buttonOk.Dock = DockStyle.Left;
				this.buttonOk.Margins = new Margins(0, 10, 0, 0);
				this.buttonOk.Clicked += new MessageEventHandler(this.HandleButtonOkClicked);
				this.buttonOk.TabIndex = tabIndex++;
				this.buttonOk.TabNavigationMode = TabNavigationMode.ActivateOnTab;

				this.buttonCancel = new Button(footer);
				this.buttonCancel.PreferredWidth = 75;
				this.buttonCancel.Text = Res.Strings.Dialog.Button.Cancel;
				this.buttonCancel.ButtonStyle = ButtonStyle.DefaultCancel;
				this.buttonCancel.Dock = DockStyle.Left;
				this.buttonCancel.Clicked += new MessageEventHandler(this.HandleButtonCloseClicked);
				this.buttonCancel.TabIndex = tabIndex++;
				this.buttonCancel.TabNavigationMode = TabNavigationMode.ActivateOnTab;
			}

			this.UpdateText();

			this.window.ShowDialog();
		}

		public void Initialise(string name)
		{
			this.initialText = name;
			this.selectedText = null;
		}

		public string SelectedText
		{
			get
			{
				return this.selectedText;
			}
		}


		protected void CreateButton(string name)
		{
			IconButton button = new IconButton();
			button.Name = name;
			button.IconName = Misc.Icon(name);
			button.ButtonStyle = ButtonStyle.ActivableIcon;
			button.Clicked += new MessageEventHandler(this.HandleButtonClicked);

			this.toolbar.Items.Add(button);
		}

		protected void UpdateText()
		{
			this.fieldText.Text = this.initialText;
			this.fieldText.SelectAll();
			this.fieldText.Focus();
		}


		private void HandleWindowCloseClicked(object sender)
		{
			this.parentWindow.MakeActive();
			this.window.Hide();
			this.OnClosed();
		}

		private void HandleButtonCloseClicked(object sender, MessageEventArgs e)
		{
			this.parentWindow.MakeActive();
			this.window.Hide();
			this.OnClosed();
		}

		private void HandleButtonOkClicked(object sender, MessageEventArgs e)
		{
			this.parentWindow.MakeActive();
			this.window.Hide();
			this.OnClosed();

			this.selectedText = this.fieldText.Text;
		}

		private void HandleButtonClicked(object sender, MessageEventArgs e)
		{
			IconButton button = sender as IconButton;

			if (button.Name == "FontBold")
			{
				this.fieldText.TextNavigator.SelectionBold = !this.fieldText.TextNavigator.SelectionBold;
			}

			if (button.Name == "FontItalic")
			{
				this.fieldText.TextNavigator.SelectionItalic = !this.fieldText.TextNavigator.SelectionItalic;
			}

			if (button.Name == "FontUnderline")
			{
				this.fieldText.TextNavigator.SelectionUnderline = !this.fieldText.TextNavigator.SelectionUnderline;
			}
		}


		protected string						initialText;
		protected string						selectedText;
		protected HToolBar						toolbar;
		protected TextFieldMulti				fieldText;
		protected Button						buttonOk;
		protected Button						buttonCancel;
	}
}

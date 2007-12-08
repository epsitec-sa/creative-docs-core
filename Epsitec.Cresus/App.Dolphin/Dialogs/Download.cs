//	Copyright © 2003-2007, EPSITEC SA, CH-1092 BELMONT, Switzerland
//	Author: Daniel ROUX, Maintainer: Daniel ROUX

using Epsitec.Common.Widgets;
using Epsitec.Common.Support;
using Epsitec.Common.Drawing;

namespace Epsitec.App.Dolphin.Dialogs
{
	/// <summary>
	/// Dialogue "Télécharger une mise à jour".
	/// </summary>
	public class Download : Abstract
	{
		public Download(DolphinApplication editor) : base(editor)
		{
		}

		public void SetInfo(string version, string url)
		{
			//	Spécifie les informations pour la mise à jour.
			if ( version.EndsWith(".0") )
			{
				version = version.Substring(0, version.Length-2);
			}
			this.version = version;
			this.url = url;
		}

		public override void Show()
		{
			//	Crée et montre la fenêtre du dialogue.
			if ( this.window == null )
			{
				this.window = new Window();
				this.window.MakeFixedSizeWindow();
				this.window.MakeSecondaryWindow();
				this.WindowInit("Download", 250, 120);
				this.window.Text = TextLayout.ConvertToSimpleText(Res.Strings.Dialog.Download.Title);
				this.window.PreventAutoClose = true;
				this.window.Owner = this.application.Window;
				this.window.WindowCloseClicked += new EventHandler(this.HandleWindowDownloadCloseClicked);

				StaticText title = new StaticText(this.window.Root);
				if (string.IsNullOrEmpty(this.url))
				{
					title.Text = Res.Strings.Dialog.Download.UpToDate;
				}
				else
				{
					title.Text = Res.Strings.Dialog.Download.UpdateAvailable;
				}
				title.Dock = DockStyle.Top;
				title.Margins = new Margins(10, 10, 10, 0);
				title.HypertextClicked += new MessageEventHandler(Abstract.HandleLinkHypertextClicked);

				string chip = "<list type=\"fix\" width=\"1.5\"/>";

				string current = string.Format(Res.Strings.Dialog.Download.CurrentVersion, Misc.GetVersion());
				StaticText actual = new StaticText(this.window.Root);
				actual.Text = chip+current;
				actual.Dock = DockStyle.Top;
				actual.Margins = new Margins(10, 10, 10, 0);

				string text;
				if (string.IsNullOrEmpty (this.url))
				{
					text = Res.Strings.Dialog.Download.NoUpdate;
				}
				else
				{
					string link = string.Format(Res.Strings.Dialog.Download.UpdateFound, this.version);
					text = string.Format("<a href=\"{0}\">{1}</a>", this.url, link);
				}
				StaticText url = new StaticText(this.window.Root);
				url.Text = chip+text;
				url.Dock = DockStyle.Top;
				url.Margins = new Margins(10, 10, 0, 0);
				url.HypertextClicked += new MessageEventHandler(Abstract.HandleLinkHypertextClicked);

				//	Bouton de fermeture.
				Button buttonClose = new Button(this.window.Root);
				buttonClose.PreferredWidth = 75;
				buttonClose.Text = Res.Strings.Dialog.Close.Button;
				buttonClose.ButtonStyle = ButtonStyle.DefaultAcceptAndCancel;
				buttonClose.Anchor = AnchorStyles.BottomRight;
				buttonClose.Margins = new Margins(0, 10, 0, 10);
				buttonClose.Clicked += new MessageEventHandler(this.HandleDownloadButtonCloseClicked);
				buttonClose.TabIndex = 1000;
				buttonClose.TabNavigationMode = TabNavigationMode.ActivateOnTab;
			}

			this.window.ShowDialog();
		}

		public override void Save()
		{
			//	Enregistre la position de la fenêtre du dialogue.
			this.WindowSave("Download");
		}


		private void HandleWindowDownloadCloseClicked(object sender)
		{
			this.CloseWindow();
		}

		private void HandleDownloadButtonCloseClicked(object sender, MessageEventArgs e)
		{
			this.CloseWindow();
		}


		protected string				version;
		protected string				url;
	}
}

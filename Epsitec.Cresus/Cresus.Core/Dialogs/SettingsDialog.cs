﻿//	Copyright © 2010, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Daniel ROUX, Maintainer: Daniel ROUX

using Epsitec.Common.Debug;
using Epsitec.Common.Dialogs;
using Epsitec.Common.Drawing;
using Epsitec.Common.IO;
using Epsitec.Common.Printing;
using Epsitec.Common.Support;
using Epsitec.Common.Types;
using Epsitec.Common.Widgets;

using Epsitec.Common.Support.EntityEngine;
using Epsitec.Cresus.Core.Entities;
using Epsitec.Cresus.Core.Printers;
using Epsitec.Cresus.Core.Business.UserManagement;

using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Epsitec.Cresus.Core.Dialogs
{
	/// <summary>
	/// Dialogue pour l'ensemble des réglages globaux.
	/// </summary>
	public class SettingsDialog : AbstractDialog
	{
		public SettingsDialog(CoreApplication application)
		{
			this.application = application;

			this.settingsTabPages = new List<SettingsTabPages.AbstractSettingsTabPage> ();
		}


		protected override Window CreateWindow()
		{
			this.window = new Window ();

			this.SetupWindow ();
			this.SetupWidgets ();
			this.UpdateWidgets ();

			this.window.AdjustWindowSize ();

			return this.window;
		}

		private void SetupWindow()
		{
			this.OwnerWindow = this.application.Window;
			this.window.Icon = this.application.Window.Icon;
			this.window.Text = "Réglages globaux";
			this.window.MakeFixedSizeWindow ();
			this.window.ClientSize = new Size (850, 600);

			this.window.WindowCloseClicked += delegate
			{
				this.RejectChangingsAndClose ();
			};
		}

		private void SetupWidgets()
		{
			bool admin = CoreProgram.Application.UserManager.IsAuthenticatedUserAtPowerLevel (UserPowerLevel.Administrator);

			var frame = new FrameBox
			{
				Parent = this.window.Root,
				Dock = DockStyle.Fill,
				Margins = new Margins (10, 10, 10, 0),
			};

			var footer = new FrameBox
			{
				Parent = this.window.Root,
				PreferredHeight = 20,
				Dock = DockStyle.Bottom,
				Margins = new Margins (10, 10, 10, 10),
			};

			//	Crée les onglets.
			this.tabBook = new TabBook
			{
				Parent = frame,
				Dock = DockStyle.Fill,
			};

			var printerPage = new TabPage
			{
				TabTitle = "Unités d'impression",
				Name = "printer",
			};

			this.tabBook.Items.Add (printerPage);

			TabPage maintenancePage = null;

			if (admin)
			{
				maintenancePage = new TabPage
				{
					TabTitle = "Maintenance",
					Name = "maintenance",
				};

				this.tabBook.Items.Add (maintenancePage);
			}

			this.ActiveLastPage ();

			//	Crée le pied de page.
			this.errorInfo = new StaticText
			{
				Parent = footer,
				ContentAlignment = Common.Drawing.ContentAlignment.MiddleCenter,
				Dock = DockStyle.Fill,
				Margins = new Margins (0, 10, 0, 0),
			};

			this.cancelButton = new Button ()
			{
				Parent = footer,
				Text = "Annuler",
				ButtonStyle = Common.Widgets.ButtonStyle.DefaultCancel,
				Dock = DockStyle.Right,
				Margins = new Margins (10, 0, 0, 0),
				TabIndex = 101,
			};

			this.acceptButton = new Button ()
			{
				Parent = footer,
				Text = "D'accord",
				ButtonStyle = Common.Widgets.ButtonStyle.DefaultAccept,
				Dock = DockStyle.Right,
				TabIndex = 100,
			};

			//	Rempli les onglets.
			var printerSettings = new SettingsTabPages.PrinterUnitsTabPage (this.application);
			printerSettings.CreateUI (printerPage);
			this.settingsTabPages.Add (printerSettings);

			if (admin)
			{
				var maintenanceSettings = new SettingsTabPages.MaintenanceTabPage (this.application);
				maintenanceSettings.CreateUI (maintenancePage);
				this.settingsTabPages.Add (maintenanceSettings);
			}

			foreach (var tab in this.settingsTabPages)
			{
				tab.AcceptStateChanging += new EventHandler (this.HandlerSettingsAcceptStateChanging);
			}

			//	Connection des événements.
			this.acceptButton.Clicked += delegate
			{
				this.AcceptChangingsAndClose ();
			};

			this.cancelButton.Clicked += delegate
			{
				this.RejectChangingsAndClose ();
			};

		}

		private void AcceptChangingsAndClose()
		{
			this.UpdateLastActivedPageName ();

			foreach (var tab in this.settingsTabPages)
			{
				tab.AcceptChangings ();
			}

			this.Result = DialogResult.Accept;
			this.OnDialogClosed ();
			this.CloseDialog ();
		}

		private void RejectChangingsAndClose()
		{
			this.UpdateLastActivedPageName ();

			foreach (var tab in this.settingsTabPages)
			{
				tab.RejectChangings ();
			}

			this.Result = DialogResult.Cancel;
			this.OnDialogClosed ();
			this.CloseDialog ();
		}

		private void HandlerSettingsAcceptStateChanging(object sender)
		{
			//	Si l'un des onglets contient une erreur, on l'affiche et le bouton 'accept' est grisé.
			string errorMessage = null;

			foreach (var tab in this.settingsTabPages)
			{
				if (!string.IsNullOrEmpty (tab.ErrorMessage))
				{
					errorMessage = tab.ErrorMessage;
					break;
				}
			}

			if (string.IsNullOrEmpty (errorMessage))  // ok ?
			{
				this.errorInfo.Text = null;
				this.errorInfo.BackColor = Color.Empty;

				this.acceptButton.Enable = true;
			}
			else  // erreur ?
			{
				this.errorInfo.Text = errorMessage;
				this.errorInfo.BackColor = Color.FromName ("Gold");

				this.acceptButton.Enable = false;
			}
		}

		private void UpdateWidgets()
		{
		}


		private void ActiveLastPage()
		{
			string name = SettingsDialog.lastActivedPageName;

			if (string.IsNullOrEmpty (name))
			{
				name = "printer";  // page par défaut
			}

			var page = this.tabBook.Items.Where (x => x.Name == name).FirstOrDefault ();
			this.tabBook.ActivePage = page;
		}

		private void UpdateLastActivedPageName()
		{
			SettingsDialog.lastActivedPageName = this.tabBook.ActivePage.Name;
		}


		private static string									lastActivedPageName;

		private readonly CoreApplication						application;
		private List<SettingsTabPages.AbstractSettingsTabPage>	settingsTabPages;

		private Window											window;
		private TabBook											tabBook;
		private StaticText										errorInfo;
		private Button											acceptButton;
		private Button											cancelButton;
	}
}

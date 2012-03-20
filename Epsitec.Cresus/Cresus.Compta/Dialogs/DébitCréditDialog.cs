//	Copyright � 2010, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Daniel ROUX, Maintainer: Daniel ROUX

using Epsitec.Common.Widgets;
using Epsitec.Common.Support;
using Epsitec.Common.Drawing;
using Epsitec.Common.Types;

using Epsitec.Cresus.Compta.Controllers;
using Epsitec.Cresus.Compta.Entities;

using System.Collections.Generic;
using System.Linq;

namespace Epsitec.Cresus.Compta.Dialogs
{
	public class D�bitCr�ditDialog : AbstractDialog
	{
		/// <summary>
		/// Demande s'il faut passer la TVA sur le compte au d�bit ou au cr�dit.
		/// </summary>
		/// <param name="controller"></param>
		public D�bitCr�ditDialog(AbstractController controller, ComptaCompteEntity d�bit, ComptaCompteEntity cr�dit)
			: base (controller)
		{
			this.d�bit  = d�bit;
			this.cr�dit = cr�dit;
		}


		public bool IsD�bit
		{
			get;
			private set;
		}

		public bool IsCr�dit
		{
			get;
			private set;
		}
		
		
		public override void Show()
		{
			//	Cr�e et montre la fen�tre du dialogue.
			if ( this.window == null )
			{
				this.window = new Window();
				//?this.window.Icon = this.mainWindowController.Icon;
				this.window.MakeSecondaryWindow ();
				this.window.MakeFixedSizeWindow ();
				this.window.Root.WindowStyles = WindowStyles.HasCloseButton;
				//?this.window.PreventAutoClose = true;
				this.WindowInit ("D�bitCr�ditDialog", 320, 130, true);
				this.window.Text = "Question";
				this.window.Owner = this.parentWindow;
				this.window.Root.Padding = new Margins (10-1, 10-1, 10, 10);

				new StaticText
				{
					Parent  = this.window.Root,
					Text    = "Sur quel compte faut-il passer la TVA ?",
					Dock    = DockStyle.Top,
					Margins = new Margins (1, 1, 0, 0),
				};

				//	Informations (tout en bas).
				var info = new FrameBox
				{
					Parent              = this.window.Root,
					PreferredHeight     = 20+1+20,
					ContainerLayoutMode = ContainerLayoutMode.HorizontalFlow,
					Dock                = DockStyle.Bottom,
					Margins = new Margins (0, 0, 2, 0),
				};

				this.CreateInfo (info, this.d�bit);
				this.CreateInfo (info, this.cr�dit);

				//	Boutons de fermeture (sur les informations).
				var footer = new FrameBox
				{
					Parent              = this.window.Root,
					ContainerLayoutMode = ContainerLayoutMode.HorizontalFlow,
					PreferredHeight     = 30,
					Dock                = DockStyle.Bottom,
				};

				var d�bitButton = new Button
				{
					Parent        = footer,
					FormattedText = FormattedText.Concat ("D�bit").ApplyFontSize (15.0),
					Dock          = DockStyle.Fill,
					Margins       = new Margins (1, 1, 0, 0),
				};

				var cr�ditButton = new Button
				{
					Parent        = footer,
					FormattedText = FormattedText.Concat ("Cr�dit").ApplyFontSize (15.0),
					Dock          = DockStyle.Fill,
					Margins       = new Margins (1, 1, 0, 0),
				};

				d�bitButton.Clicked += delegate
				{
					this.IsD�bit = true;
					this.parentWindow.MakeActive ();
					this.window.Hide ();
					this.OnClosed ();
				};

				cr�ditButton.Clicked += delegate
				{
					this.IsCr�dit = true;
					this.parentWindow.MakeActive ();
					this.window.Hide ();
					this.OnClosed ();
				};
			}

			this.IsD�bit  = false;
			this.IsCr�dit = false;

			this.window.ShowDialog();
		}

		private void CreateInfo(Widget parent, ComptaCompteEntity compte)
		{
			var frame = new FrameBox
			{
				Parent        = parent,
				DrawFullFrame = true,
				Dock          = DockStyle.Fill,
				Margins = new Margins (1, 1, 0, 0),
			};

			new StaticText
			{
				Parent           = frame,
				FormattedText    = (compte == null) ? FormattedText.Empty : compte.Num�ro.ApplyBold (),
				ContentAlignment = ContentAlignment.MiddleLeft,
				PreferredHeight  = 20,
				TextBreakMode    = TextBreakMode.Ellipsis | TextBreakMode.Split | TextBreakMode.SingleLine,
				Dock             = DockStyle.Top,
				Margins          = new Margins (5, 0, 0, 0),
			};

			new Separator
			{
				Parent           = frame,
				IsHorizontalLine = true,
				PreferredHeight  = 1,
				Dock             = DockStyle.Top,
			};

			new StaticText
			{
				Parent           = frame,
				FormattedText    = (compte == null) ? FormattedText.Empty : compte.Titre,
				ContentAlignment = ContentAlignment.MiddleLeft,
				PreferredHeight  = 20,
				TextBreakMode    = TextBreakMode.Ellipsis | TextBreakMode.Split | TextBreakMode.SingleLine,
				Dock             = DockStyle.Top,
				Margins          = new Margins (5, 0, 0, 0),
			};
		}


		private ComptaCompteEntity		d�bit;
		private ComptaCompteEntity		cr�dit;
	}
}

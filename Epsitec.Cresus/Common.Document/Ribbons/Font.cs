using Epsitec.Common.Widgets;
using Epsitec.Common.Support;
using Epsitec.Common.Drawing;
using Epsitec.Common.Text;

namespace Epsitec.Common.Document.Ribbons
{
	/// <summary>
	/// La classe Font permet de choisir la fonte du texte.
	/// </summary>
	[SuppressBundleSupport]
	public class Font : Abstract
	{
		public Font() : base()
		{
			this.title.Text = Res.Strings.Action.Text.Font.Main;

			this.buttonBold       = this.CreateIconButton(Misc.Icon("FontBold"),       Res.Strings.Action.Text.Font.Bold,       new MessageEventHandler(this.HandleButtonBoldClicked));
			this.buttonItalic     = this.CreateIconButton(Misc.Icon("FontItalic"),     Res.Strings.Action.Text.Font.Italic,     new MessageEventHandler(this.HandleButtonItalicClicked));
			this.buttonUnderlined = this.CreateIconButton(Misc.Icon("FontUnderlined"), Res.Strings.Action.Text.Font.Underlined, new MessageEventHandler(this.HandleButtonUnderlinedClicked));

			this.UpdateClientGeometry();
		}
		
		protected override void Dispose(bool disposing)
		{
			if ( disposing )
			{
			}
			
			base.Dispose(disposing);
		}

		public override void SetDocument(DocumentType type, InstallType install, Settings.GlobalSettings gs, Document document)
		{
			if ( this.document != null )
			{
				this.document.TextWrapper.Active.Changed  -= new EventHandler(this.HandleWrapperChanged);
				this.document.TextWrapper.Defined.Changed -= new EventHandler(this.HandleWrapperChanged);
			}

			base.SetDocument(type, install, gs, document);

			if ( this.document != null )
			{
				this.document.TextWrapper.Active.Changed  += new EventHandler(this.HandleWrapperChanged);
				this.document.TextWrapper.Defined.Changed += new EventHandler(this.HandleWrapperChanged);
			}

			this.HandleWrapperChanged(null);
		}

		// Retourne la largeur standard.
		public override double DefaultWidth
		{
			get
			{
				return 200;
			}
		}


		protected void UpdateButtonBold()
		{
			bool enabled = false;
			bool state   = false;

			if ( this.document != null && this.document.TextWrapper.IsAttached )
			{
				string face  = this.document.TextWrapper.Defined.FontFace;
				if ( face == null )
				{
					face = this.document.TextWrapper.Active.FontFace;
				}

				string style = this.document.TextWrapper.Defined.FontStyle;
				if ( style == null )
				{
					style = this.document.TextWrapper.Active.FontStyle;
				}

				OpenType.FontWeight weight = OpenType.FontWeight.Medium;
				if ( face != null && style != null )
				{
					OpenType.Font font = TextContext.GetFont(face, style);
					weight = font.FontIdentity.FontWeight;
				}

				enabled = true;
				state   = ((int)weight > (int)OpenType.FontWeight.Medium);
				state  ^= this.document.TextWrapper.Defined.InvertBold;
			}

			this.buttonBold.Enable = enabled;
			this.buttonBold.ActiveState = state ? ActiveState.Yes : ActiveState.No;
		}

		protected void UpdateButtonItalic()
		{
			bool enabled = false;
			bool state   = false;

			if ( this.document != null && this.document.TextWrapper.IsAttached )
			{
				string face  = this.document.TextWrapper.Defined.FontFace;
				if ( face == null )
				{
					face = this.document.TextWrapper.Active.FontFace;
				}

				string style = this.document.TextWrapper.Defined.FontStyle;
				if ( style == null )
				{
					style = this.document.TextWrapper.Active.FontStyle;
				}

				OpenType.FontStyle italic = OpenType.FontStyle.Normal;
				if ( face != null && style != null )
				{
					OpenType.Font font = TextContext.GetFont(face, style);
					italic = font.FontIdentity.FontStyle;
				}

				enabled = true;
				state   = italic != OpenType.FontStyle.Normal;
				state  ^= this.document.TextWrapper.Defined.InvertItalic;
			}

			this.buttonItalic.Enable = enabled;
			this.buttonItalic.ActiveState = state ? ActiveState.Yes : ActiveState.No;
		}

		protected void UpdateButtonUnderlined()
		{
			bool enabled = false;
			bool state   = false;

			if ( this.document != null && this.document.TextWrapper.IsAttached )
			{
			}

			this.buttonUnderlined.Enable = enabled;
			this.buttonUnderlined.ActiveState = state ? ActiveState.Yes : ActiveState.No;
		}

		
		// Met � jour la g�om�trie.
		protected override void UpdateClientGeometry()
		{
			base.UpdateClientGeometry();

			if ( this.buttonBold == null )  return;

			double dx = this.buttonBold.DefaultWidth;
			double dy = this.buttonBold.DefaultHeight;

			Rectangle rect = this.UsefulZone;
			rect.Height = dy;
			rect.Width = dx;
			rect.Offset(0, dy+5);
			this.buttonBold.Bounds = rect;
			rect.Offset(dx, 0);
			this.buttonItalic.Bounds = rect;
			rect.Offset(dx, 0);
			this.buttonUnderlined.Bounds = rect;
		}


		// Le wrapper associ� a chang�.
		private void HandleWrapperChanged(object sender)
		{
			this.UpdateButtonBold();
			this.UpdateButtonItalic();
			this.UpdateButtonUnderlined();
		}

		private void HandleButtonBoldClicked(object sender, MessageEventArgs e)
		{
			if ( !this.document.TextWrapper.IsAttached )  return;
			this.document.TextWrapper.Defined.InvertBold = !this.document.TextWrapper.Defined.InvertBold;
		}

		private void HandleButtonItalicClicked(object sender, MessageEventArgs e)
		{
			if ( !this.document.TextWrapper.IsAttached )  return;
			this.document.TextWrapper.Defined.InvertItalic = !this.document.TextWrapper.Defined.InvertItalic;
		}

		private void HandleButtonUnderlinedClicked(object sender, MessageEventArgs e)
		{
			if ( !this.document.TextWrapper.IsAttached )  return;
		}


		protected IconButton				buttonBold;
		protected IconButton				buttonItalic;
		protected IconButton				buttonUnderlined;
	}
}

using Epsitec.Common.Widgets;
using Epsitec.Common.Support;
using Epsitec.Common.Drawing;
using Epsitec.Common.Text;

namespace Epsitec.Common.Document.TextPanels
{
	/// <summary>
	/// La classe Box permet de choisir un encadrement.
	/// </summary>
	[SuppressBundleSupport]
	public class Box : Abstract
	{
		public Box(Document document, bool isStyle, StyleCategory styleCategory) : base(document, isStyle, styleCategory)
		{
			this.label.Text = Res.Strings.TextPanel.Box.Title;

			this.fixIcon.Text = Misc.Image("TextBox");
			ToolTip.Default.SetToolTip(this.fixIcon, Res.Strings.TextPanel.Box.Title);

			this.buttonFrame = this.CreateIconButton(Misc.Icon("FontFrame"), Res.Strings.Action.FontFrame, new MessageEventHandler(this.HandleButtonClicked));

			this.buttonClear = this.CreateClearButton(new MessageEventHandler(this.HandleClearClicked));

			this.TextWrapper.Active.Changed  += new EventHandler(this.HandleWrapperChanged);
			this.TextWrapper.Defined.Changed += new EventHandler(this.HandleWrapperChanged);

			this.isNormalAndExtended = true;
			this.UpdateAfterChanging();
		}
		
		protected override void Dispose(bool disposing)
		{
			if ( disposing )
			{
				this.TextWrapper.Active.Changed  -= new EventHandler(this.HandleWrapperChanged);
				this.TextWrapper.Defined.Changed -= new EventHandler(this.HandleWrapperChanged);
			}
			
			base.Dispose(disposing);
		}

		
		public override void UpdateAfterAttach()
		{
			//	Mise � jour apr�s avoir attach� le wrappers.
			this.buttonClear.Visibility = !this.TextWrapper.IsAttachedToDefaultParagraphStyle;
		}


		public override double DefaultHeight
		{
			//	Retourne la hauteur standard.
			get
			{
				double h = this.LabelHeight;

				if ( this.isExtendedSize )  // panneau �tendu ?
				{
					if ( this.IsLabelProperties )  // �tendu/d�tails ?
					{
						h += 105;
					}
					else	// �tendu/compact ?
					{
						h += 80;
					}
				}
				else	// panneau r�duit ?
				{
					h += 30;
				}

				return h;
			}
		}

		protected override void UpdateAfterChanging()
		{
			//	Met � jour apr�s un changement du wrapper.
			base.UpdateAfterChanging();
		}


		
		protected void HandleWrapperChanged(object sender)
		{
			//	Le wrapper associ� a chang�.
			this.UpdateAfterChanging();
		}

		
		protected override void UpdateClientGeometry()
		{
			//	Met � jour la g�om�trie.
			base.UpdateClientGeometry();

			if ( this.buttonFrame == null )  return;

			Rectangle rect = this.UsefulZone;

			if ( this.isExtendedSize )
			{
				Rectangle r = rect;
				r.Bottom = r.Top-20;

				if ( this.IsLabelProperties )
				{
					r.Left = rect.Left;
					r.Width = 20;
					this.buttonFrame.SetManualBounds(r);
					r.Left = rect.Right-20;
					r.Width = 20;
					this.buttonClear.SetManualBounds(r);
				}
				else
				{
					r.Left = rect.Left;
					r.Width = 20;
					this.buttonFrame.SetManualBounds(r);
					r.Left = rect.Right-20;
					r.Width = 20;
					this.buttonClear.SetManualBounds(r);
				}
			}
			else
			{
				Rectangle r = rect;
				r.Bottom = r.Top-20;

				r.Left = rect.Left;
				r.Width = 20;
				this.buttonFrame.SetManualBounds(r);
				r.Left = rect.Right-20;
				r.Width = 20;
				this.buttonClear.SetManualBounds(r);
			}
		}


		private void HandleButtonClicked(object sender, MessageEventArgs e)
		{
			if ( this.ignoreChanged )  return;
			if ( !this.TextWrapper.IsAttached )  return;
		}

		private void HandleClearClicked(object sender, MessageEventArgs e)
		{
			if ( this.ignoreChanged )  return;
			if ( !this.TextWrapper.IsAttached )  return;

			this.TextWrapper.SuspendSynchronizations();
			this.TextWrapper.Defined.ClearTextBox();
			this.TextWrapper.DefineOperationName("TextBoxClear", Res.Strings.TextPanel.Clear);
			this.TextWrapper.ResumeSynchronizations();
		}

		
		protected IconButton				buttonFrame;
		protected IconButton				buttonClear;
	}
}

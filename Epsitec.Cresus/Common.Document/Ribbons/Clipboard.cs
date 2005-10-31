using Epsitec.Common.Widgets;
using Epsitec.Common.Support;
using Epsitec.Common.Drawing;

namespace Epsitec.Common.Document.Ribbons
{
	/// <summary>
	/// La classe Clipboard permet de g�rer le presse-papiers.
	/// </summary>
	[SuppressBundleSupport]
	public class Clipboard : Abstract
	{
		public Clipboard() : base()
		{
			this.title.Text = Res.Strings.Action.Clipboard;

			this.buttonCut   = this.CreateIconButton("Cut",   Misc.Icon("Cut"),    Res.Strings.Action.Cut);
			this.buttonCopy  = this.CreateIconButton("Copy",  Misc.Icon("Copy"),   Res.Strings.Action.Copy);
			this.buttonPaste = this.CreateIconButton("Paste", Misc.Icon("Paste2"), Res.Strings.Action.Paste);
			
			this.UpdateClientGeometry();
		}
		
		protected override void Dispose(bool disposing)
		{
			if ( disposing )
			{
			}
			
			base.Dispose(disposing);
		}

		// Retourne la largeur standard.
		public override double DefaultWidth
		{
			get
			{
				return 8 + 22 + 4 + 22*1.5;
			}
		}


		// Met � jour la g�om�trie.
		protected override void UpdateClientGeometry()
		{
			base.UpdateClientGeometry();

			if ( this.buttonCut == null )  return;

			double dx = this.buttonCut.DefaultWidth;
			double dy = this.buttonCut.DefaultHeight;

			Rectangle rect = this.UsefulZone;
			rect.Width  = dx;
			rect.Height = dy;
			rect.Offset(0, dy+5);
			this.buttonCut.Bounds = rect;
			rect.Offset(0, -dy-5);
			this.buttonCopy.Bounds = rect;

			rect = this.UsefulZone;
			rect.Width  = dx*1.5;
			rect.Height = dy*1.5;
			rect.Offset(dx+4, dy*0.5);
			this.buttonPaste.Bounds = rect;
		}


		protected IconButton				buttonCut;
		protected IconButton				buttonCopy;
		protected IconButton				buttonPaste;
	}
}

using Epsitec.Common.Widgets;
using Epsitec.Common.Support;
using Epsitec.Common.Drawing;

namespace Epsitec.Common.Designer.Ribbons
{
	/// <summary>
	/// La classe Clipboard permet de g�rer le presse-papiers.
	/// </summary>
	[SuppressBundleSupport]
	public class Clipboard : Abstract
	{
		public Clipboard() : base()
		{
			this.title.Text = Res.Strings.Ribbon.Section.Clipboard;

			this.buttonCut   = this.CreateIconButton("Cut");
			this.buttonCopy  = this.CreateIconButton("Copy");
			this.buttonPaste = this.CreateIconButton("Paste", "Large");
			
			this.UpdateClientGeometry();
		}
		
		protected override void Dispose(bool disposing)
		{
			if ( disposing )
			{
			}
			
			base.Dispose(disposing);
		}

		public override double DefaultWidth
		{
			//	Retourne la largeur standard.
			get
			{
				return 8 + 22 + 4 + 22*1.5;
			}
		}


		protected override void UpdateClientGeometry()
		{
			//	Met � jour la g�om�trie.
			base.UpdateClientGeometry();

			if ( this.buttonCut == null )  return;

			double dx = this.buttonCut.DefaultWidth;
			double dy = this.buttonCut.DefaultHeight;

			Rectangle rect = this.UsefulZone;
			rect.Width  = dx;
			rect.Height = dy;
			rect.Offset(0, dy+5);
			this.buttonCut.SetManualBounds(rect);
			rect.Offset(0, -dy-5);
			this.buttonCopy.SetManualBounds(rect);

			rect = this.UsefulZone;
			rect.Width  = dx*1.5;
			rect.Height = dy*1.5;
			rect.Offset(dx+4, dy*0.5);
			this.buttonPaste.SetManualBounds(rect);
		}


		protected IconButton				buttonCut;
		protected IconButton				buttonCopy;
		protected IconButton				buttonPaste;
	}
}

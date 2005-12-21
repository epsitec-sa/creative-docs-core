using Epsitec.Common.Widgets;
using Epsitec.Common.Support;
using Epsitec.Common.Drawing;

namespace Epsitec.Common.Document.Ribbons
{
	/// <summary>
	/// La classe Select permet de g�rer la s�lection.
	/// </summary>
	[SuppressBundleSupport]
	public class Select : Abstract
	{
		public Select() : base()
		{
			this.title.Text = Res.Strings.Action.SelectMain;

			this.buttonDelete    = this.CreateIconButton("Delete", "2");
			this.buttonDuplicate = this.CreateIconButton("Duplicate", "2");

			this.separatorV = new IconSeparator(this);
			this.separatorH = new IconSeparator(this);
			this.separatorH.IsHorizontal = false;
			
			this.buttonDeselectAll  = this.CreateIconButton("DeselectAll");
			this.buttonSelectAll    = this.CreateIconButton("SelectAll");
			this.buttonSelectInvert = this.CreateIconButton("SelectInvert");
			this.buttonHideSel      = this.CreateIconButton("HideSel");
			this.buttonHideRest     = this.CreateIconButton("HideRest");
			this.buttonHideCancel   = this.CreateIconButton("HideCancel");
			this.buttonHideHalf     = this.CreateIconButton("HideHalf");
			
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
				return 8 + 22*1.5*2 + this.separatorWidth + 22*4;
			}
		}


		protected override void UpdateClientGeometry()
		{
			//	Met � jour la g�om�trie.
			base.UpdateClientGeometry();

			if ( this.buttonDelete == null )  return;

			double dx = this.buttonDelete.DefaultWidth;
			double dy = this.buttonDelete.DefaultHeight;

			Rectangle rect = this.UsefulZone;
			rect.Left += dx*1.5*2;
			rect.Width = this.separatorWidth;
			this.separatorV.Bounds = rect;

			rect = this.UsefulZone;
			rect.Left += dx*1.5*2+this.separatorWidth*0.5;
			rect.Bottom += dy;
			rect.Height = 5;
			this.separatorH.Bounds = rect;

			rect = this.UsefulZone;
			rect.Width  = dx*1.5;
			rect.Height = dy*1.5;
			rect.Offset(0, dy*0.5);
			this.buttonDelete.Bounds = rect;
			rect.Offset(dx*1.5, 0);
			this.buttonDuplicate.Bounds = rect;

			rect = this.UsefulZone;
			rect.Width  = dx;
			rect.Height = dy;
			rect.Offset(dx*1.5*2+this.separatorWidth, dy+5);
			this.buttonDeselectAll.Bounds = rect;
			rect.Offset(dx, 0);
			this.buttonSelectAll.Bounds = rect;
			rect.Offset(dx, 0);
			this.buttonSelectInvert.Bounds = rect;

			rect = this.UsefulZone;
			rect.Width  = dx;
			rect.Height = dy;
			rect.Offset(dx*1.5*2+this.separatorWidth, 0);
			this.buttonHideSel.Bounds = rect;
			rect.Offset(dx, 0);
			this.buttonHideRest.Bounds = rect;
			rect.Offset(dx, 0);
			this.buttonHideCancel.Bounds = rect;
			rect.Offset(dx, 0);
			this.buttonHideHalf.Bounds = rect;
		}


		protected IconButton				buttonDelete;
		protected IconButton				buttonDuplicate;
		protected IconSeparator				separatorV;
		protected IconSeparator				separatorH;
		protected IconButton				buttonDeselectAll;
		protected IconButton				buttonSelectAll;
		protected IconButton				buttonSelectInvert;
		protected IconButton				buttonHideSel;
		protected IconButton				buttonHideRest;
		protected IconButton				buttonHideCancel;
		protected IconButton				buttonHideHalf;
	}
}

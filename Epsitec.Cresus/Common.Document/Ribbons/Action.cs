using Epsitec.Common.Widgets;
using Epsitec.Common.Support;
using Epsitec.Common.Drawing;

namespace Epsitec.Common.Document.Ribbons
{
	/// <summary>
	/// La classe Action permet de g�rer les groupes.
	/// </summary>
	[SuppressBundleSupport]
	public class Action : Abstract
	{
		public Action() : base()
		{
			this.title.Text = "Actions";

			this.buttonSettings = this.CreateIconButton("Settings", Misc.Icon("Settings"), Res.Strings.Action.Settings);
			this.buttonInfos = this.CreateIconButton("Infos", Misc.Icon("Infos"), Res.Strings.Action.Infos);
			this.buttonPageStack = this.CreateIconButton("PageStack", Misc.Icon("PageStack"), Res.Strings.Action.PageStack);
			this.buttonUpdate = this.CreateIconButton("UpdateApplication", Misc.Icon("Update"), Res.Strings.Action.Update);
			this.buttonAbout = this.CreateIconButton("AboutApplication", Misc.Icon("About"), Res.Strings.Action.About);
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
				return 8 + 22*3;
			}
		}


		// Met � jour la g�om�trie.
		protected override void UpdateClientGeometry()
		{
			base.UpdateClientGeometry();

			if ( this.buttonSettings == null )  return;

			double dx = this.buttonSettings.DefaultWidth;
			double dy = this.buttonSettings.DefaultHeight;

			Rectangle rect = this.UsefulZone;
			rect.Width  = dx;
			rect.Height = dy;
			rect.Offset(0, dy);
			this.buttonSettings.Bounds = rect;
			rect.Offset(dx, 0);
			this.buttonInfos.Bounds = rect;
			rect.Offset(dx, 0);
			this.buttonPageStack.Bounds = rect;

			rect = this.UsefulZone;
			rect.Width  = dx;
			rect.Height = dy;
			rect.Offset(dx, 0);
			this.buttonUpdate.Bounds = rect;
			rect.Offset(dx, 0);
			this.buttonAbout.Bounds = rect;
		}


		protected IconButton				buttonSettings;
		protected IconButton				buttonInfos;
		protected IconButton				buttonPageStack;
		protected IconButton				buttonUpdate;
		protected IconButton				buttonAbout;
	}
}

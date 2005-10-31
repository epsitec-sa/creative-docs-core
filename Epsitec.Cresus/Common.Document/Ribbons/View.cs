using Epsitec.Common.Widgets;
using Epsitec.Common.Support;
using Epsitec.Common.Drawing;

namespace Epsitec.Common.Document.Ribbons
{
	/// <summary>
	/// La classe View d�finit les modes d'affichage.
	/// </summary>
	[SuppressBundleSupport]
	public class View : Abstract
	{
		public View() : base()
		{
			this.title.Text = Res.Strings.Action.ViewMain;

			this.buttonPreview = this.CreateIconButton("Preview", Misc.Icon("Preview"), Res.Strings.Action.Preview, true);
			this.buttonGrid = this.CreateIconButton("Grid", Misc.Icon("Grid"), Res.Strings.Action.Grid, true);
			this.buttonMagnet = this.CreateIconButton("Magnet", Misc.Icon("Magnet"), Res.Strings.Action.Magnet, true);
			this.buttonRulers = this.CreateIconButton("Rulers", Misc.Icon("Rulers"), Res.Strings.Action.Rulers, true);
			this.buttonLabels = this.CreateIconButton("Labels", Misc.Icon("Labels"), Res.Strings.Action.Labels, true);
			this.buttonAggregates = this.CreateIconButton("Aggregates", Misc.Icon("Aggregates"), Res.Strings.Action.Aggregates, true);
			this.buttonLabelProperties = this.CreateIconButton("LabelProperties", Misc.Icon("LabelProperties"), Res.Strings.Action.LabelProperties, true);
			
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
				return 8 + 22*4;
			}
		}


		// Met � jour la g�om�trie.
		protected override void UpdateClientGeometry()
		{
			base.UpdateClientGeometry();

			if ( this.buttonPreview == null )  return;

			double dx = this.buttonPreview.DefaultWidth;
			double dy = this.buttonPreview.DefaultHeight;

			Rectangle rect = this.UsefulZone;
			rect.Width  = dx;
			rect.Height = dy;
			rect.Offset(0, dy+5);
			this.buttonPreview.Bounds = rect;
			rect.Offset(dx, 0);
			this.buttonGrid.Bounds = rect;
			rect.Offset(dx, 0);
			this.buttonMagnet.Bounds = rect;
			rect.Offset(dx, 0);
			this.buttonRulers.Bounds = rect;

			rect = this.UsefulZone;
			rect.Width  = dx;
			rect.Height = dy;
			this.buttonLabels.Bounds = rect;
			rect.Offset(dx, 0);
			this.buttonAggregates.Bounds = rect;
			rect.Offset(dx*2, 0);
			this.buttonLabelProperties.Bounds = rect;
		}


		protected IconButton				buttonPreview;
		protected IconButton				buttonGrid;
		protected IconButton				buttonMagnet;
		protected IconButton				buttonRulers;
		protected IconButton				buttonLabels;
		protected IconButton				buttonAggregates;
		protected IconButton				buttonLabelProperties;
	}
}

using Epsitec.Common.Widgets;
using Epsitec.Common.Support;
using Epsitec.Common.Pictogram.Data;

namespace Epsitec.Common.Pictogram.Widgets
{
	/// <summary>
	/// La classe PanelBool permet de choisir une valeur bool�enne.
	/// </summary>
	public class PanelBool : AbstractPanel
	{
		public PanelBool()
		{
			this.button = new CheckButton(this);
			this.button.ActiveStateChanged += new EventHandler(this.ButtonActiveStateChanged);
		}
		
		public PanelBool(Widget embedder) : this()
		{
			this.SetEmbedder(embedder);
		}
		
		protected override void Dispose(bool disposing)
		{
			if ( disposing )
			{
				this.button.ActiveStateChanged -= new EventHandler(this.ButtonActiveStateChanged);
			}
			
			base.Dispose(disposing);
		}

		
		// Propri�t� -> widget.
		public override void SetProperty(AbstractProperty property)
		{
			base.SetProperty(property);
			this.button.Text = this.text;

			PropertyBool p = property as PropertyBool;
			if ( p == null )  return;

			this.button.ActiveState = p.Bool ? WidgetState.ActiveYes : WidgetState.ActiveNo;
		}

		// Widget -> propri�t�.
		public override AbstractProperty GetProperty()
		{
			PropertyBool p = new PropertyBool();
			base.GetProperty(p);

			p.Bool = ( this.button.ActiveState == WidgetState.ActiveYes );
			return p;
		}


		// Met � jour la g�om�trie.
		protected override void UpdateClientGeometry()
		{
			base.UpdateClientGeometry();

			if ( this.button == null )  return;

			Drawing.Rectangle rect = this.Client.Bounds;
			rect.Left += this.extendedZoneWidth;
			rect.Inflate(-5, -5);
			this.button.Bounds = rect;
		}
		
		// Une valeur a �t� chang�e.
		private void ButtonActiveStateChanged(object sender)
		{
			this.OnChanged();
		}


		protected CheckButton				button;
	}
}

namespace Epsitec.Common.Widgets
{
	public enum ButtonStyle
	{
		Flat,							// pas de cadre, ni de relief
		Normal,							// bouton normal
		Scroller,						// bouton pour Scroller
		Combo,							// bouton pour TextFieldCombo
		UpDown,							// bouton pour TextFieldUpDown
		ToolItem,						// bouton pour barre d'ic�ne
		ListItem,						// bouton pour liste
		DefaultActive,					// bouton pour l'action par d�faut (OK)
	}
	
	/// <summary>
	/// La class Button repr�sente un bouton standard.
	/// </summary>
	public class Button : AbstractButton
	{
		public Button()
		{
			this.buttonStyle = ButtonStyle.Normal;
		}
		
		public Button(string text)
		{
			this.buttonStyle = ButtonStyle.Normal;
			this.Text = text;
		}
		
		public Button(Widget embedder) : this()
		{
			this.SetEmbedder(embedder);
		}
		
		
		// Retourne la hauteur standard d'un bouton.
		public override double DefaultHeight
		{
			get
			{
				return this.DefaultFontHeight+10;
			}
		}

		public ButtonStyle ButtonStyle
		{
			get
			{
				return this.buttonStyle;
			}

			set
			{
				if ( this.buttonStyle != value )
				{
					this.buttonStyle = value;
					this.Invalidate();
				}
			}
		}

		// Dessine le bouton.
		protected override void PaintBackgroundImplementation(Drawing.Graphics graphics, Drawing.Rectangle clipRect)
		{
			IAdorner adorner = Widgets.Adorner.Factory.Active;

			Drawing.Rectangle rect  = new Drawing.Rectangle(0, 0, this.Client.Width, this.Client.Height);
			WidgetState       state = this.PaintState;
			Drawing.Point     pos   = new Drawing.Point(0, 0);
			
			if ( (state & WidgetState.Enabled) == 0 )
			{
				state &= ~WidgetState.Focused;
				state &= ~WidgetState.Entered;
				state &= ~WidgetState.Engaged;
			}
			adorner.PaintButtonBackground(graphics, rect, state, Direction.Down, this.buttonStyle);
			adorner.PaintButtonTextLayout(graphics, pos, this.textLayout, state, this.buttonStyle);
		}
		
		
		protected ButtonStyle			buttonStyle;
	}
}

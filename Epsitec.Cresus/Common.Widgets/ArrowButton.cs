namespace Epsitec.Common.Widgets
{
	/// <summary>
	/// La classe ArrowButton dessine un bouton fl�che.
	/// </summary>
	public class ArrowButton : Button
	{
		public ArrowButton()
		{
			this.direction = Direction.None;
			this.internalState &= ~InternalState.AutoFocus;
			this.internalState &= ~InternalState.Focusable;
		}
		
		public Direction Direction
		{
			get
			{
				return this.direction;
			}

			set
			{
				this.direction = value;
			}
		}
		
		public override double DefaultWidth
		{
			get
			{
				return 17;
			}
		}
		
		public override double DefaultHeight
		{
			get
			{
				return 17;
			}
		}
		

		// Dessine le bouton.
		protected override void PaintBackgroundImplementation(Drawing.Graphics graphics, Drawing.Rectangle clipRect)
		{
			IAdorner adorner = Widgets.Adorner.Factory.Active;

			Drawing.Rectangle rect = new Drawing.Rectangle(0, 0, this.Client.Width, this.Client.Height);
			adorner.PaintButtonBackground(graphics, rect, this.PaintState, this.RootDirection, this.buttonStyle);
			adorner.PaintArrow(graphics, rect, this.PaintState, this.RootDirection, this.direction);
		}

		
		protected Direction				direction;
	}
}

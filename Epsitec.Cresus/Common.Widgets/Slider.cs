namespace Epsitec.Common.Widgets
{
	/// <summary>
	/// La classe Slider impl�mente un curseur de r�glage.
	/// </summary>
	public class Slider : Widget
	{
		public Slider()
		{
			IAdorner adorner = Widgets.Adorner.Factory.Active;
			this.color = Drawing.Color.Empty;
		}
		
		public Slider(Widget embedder) : this()
		{
			this.SetEmbedder(embedder);
		}

		public bool HasFrame
		{
			get { return this.frame; }
			set { this.frame = value; }
		}

		// Valeur num�rique repr�sent�e.
		public double Value
		{
			get
			{
				return this.sliderValue;
			}

			set
			{
				if ( this.sliderValue != value )
				{
					this.sliderValue = value;
					this.OnValueChanged();
					this.Invalidate();
				}
			}
		}

		// Valeur num�rique minimale possible.
		public double MinRange
		{
			get
			{
				return this.minRange;
			}

			set
			{
				this.minRange = value;
			}
		}
		
		// Valeur num�rique maximale possible.
		public double MaxRange
		{
			get
			{
				return this.maxRange;
			}

			set
			{
				this.maxRange = value;
			}
		}
		
		// Couleur du slider.
		public Drawing.Color Color
		{
			get
			{
				return this.color;
			}

			set
			{
				if ( this.color != value )
				{
					this.color = value;
					this.Invalidate();
				}
			}
		}
		
		// Gestion d'un �v�nement.
		protected override void ProcessMessage(Message message, Drawing.Point pos)
		{
			switch ( message.Type )
			{
				case MessageType.MouseDown:
					this.mouseDown = true;
					this.MouseDown(pos);
					break;
				
				case MessageType.MouseMove:
					if ( this.mouseDown )
					{
						this.MouseMove(pos);
					}
					break;

				case MessageType.MouseUp:
					if ( this.mouseDown )
					{
						this.MouseUp(pos);
						this.mouseDown = false;
					}
					break;
			}
			
			message.Consumer = this;
		}

		protected void MouseDown(Drawing.Point pos)
		{
			this.Value = this.Detect(pos);
		}

		protected void MouseMove(Drawing.Point pos)
		{
			this.Value = this.Detect(pos);
		}

		protected void MouseUp(Drawing.Point pos)
		{
			this.Value = this.Detect(pos);
		}

		protected double Detect(Drawing.Point pos)
		{
			IAdorner adorner = Widgets.Adorner.Factory.Active;
			Drawing.Rectangle rect = this.Client.Bounds;
			rect.Left  += adorner.GeometrySliderLeftMargin;
			rect.Right += adorner.GeometrySliderRightMargin;
			rect.Inflate(-this.margin, -this.margin);

			double val = this.minRange+(pos.X-rect.Left)*(this.maxRange-this.minRange)/rect.Width;
			val = System.Math.Max(val, this.minRange);
			val = System.Math.Min(val, this.maxRange);
			val = System.Math.Floor(val+0.5);
			return val;
		}

		// G�n�re un �v�nement pour dire que le slider a chang�.
		protected virtual void OnValueChanged()
		{
			if ( this.ValueChanged != null )  // qq'un �coute ?
			{
				this.ValueChanged(this);
			}
		}

		public event Support.EventHandler ValueChanged;


		protected override void PaintBackgroundImplementation(Drawing.Graphics graphics, Drawing.Rectangle clipRect)
		{
			IAdorner adorner = Widgets.Adorner.Factory.Active;

			Drawing.Rectangle rect  = this.Client.Bounds;
			WidgetState       state = this.PaintState;

			double width = rect.Width;
			rect.Left   += adorner.GeometrySliderLeftMargin;
			rect.Right  += adorner.GeometrySliderRightMargin;
			rect.Bottom += adorner.GeometrySliderBottomMargin;
			
			if ( this.frame )
			{
				adorner.PaintTextFieldBackground(graphics, rect, state, TextFieldStyle.Multi, false);
			}
			else
			{
				graphics.AddLine(1, rect.Top-0.5, width-1, rect.Top-0.5);
				graphics.RenderSolid(adorner.ColorTextSliderBorder(this.IsEnabled));
			}

			if ( this.IsEnabled )
			{
				rect.Inflate(-this.margin, -this.margin);
				
				Drawing.Color front = this.color.IsEmpty ? adorner.ColorCaption : this.color;
				Drawing.Color back  = this.BackColor.IsEmpty ? adorner.ColorWindow : this.BackColor;
				
				if ( !this.frame && rect.Left > 1 )
				{
					Drawing.Path path = new Drawing.Path();
					path.MoveTo(1, rect.Top);
					path.LineTo(rect.Left, rect.Top);
					path.LineTo(rect.Left, rect.Bottom);
					path.CurveTo(1, rect.Bottom, 1, rect.Top);
					graphics.Rasterizer.AddSurface(path);
					graphics.RenderSolid(front);
				}
				
				graphics.AddFilledRectangle(rect);
				graphics.RenderSolid(back);
				rect.Width *= (this.Value-this.minRange)/(this.maxRange-this.minRange);
				graphics.AddFilledRectangle(rect);
				graphics.RenderSolid(front);
			}
		}

		
		private double						sliderValue = 0;
		protected double					minRange = 0;
		protected double					maxRange = 100;
		protected Drawing.Color				color;
		protected bool						mouseDown = false;
		protected double					margin = 1;
		protected bool						frame = true;
	}
}

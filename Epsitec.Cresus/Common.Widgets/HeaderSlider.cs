namespace Epsitec.Common.Widgets
{
	public enum HeaderSliderStyle
	{
		Top,			// bouton dans en-t�te sup�rieure
		Left,			// bouton dans en-t�te gauche
	}
	
	/// <summary>
	/// La class HeaderSlider repr�sente un bouton d'un en-t�te de tableau,
	/// permettant de modifier une largeur de colonne ou une hauteur de ligne.
	/// </summary>
	public class HeaderSlider : AbstractButton
	{
		public HeaderSlider()
		{
			this.InternalState &= ~InternalState.Engageable;
			this.headerSliderStyle = HeaderSliderStyle.Top;
		}
		
		public HeaderSlider(Widget embedder) : this()
		{
			this.SetEmbedder(embedder);
		}
		
		// Type du bouton.
		public HeaderSliderStyle Style
		{
			get
			{
				return this.headerSliderStyle;
			}

			set
			{
				if ( this.headerSliderStyle != value )
				{
					this.headerSliderStyle = value;
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
					this.OnDragStarted(new MessageEventArgs(message, pos));
					break;
				
				case MessageType.MouseMove:
					if ( this.mouseDown )
					{
						this.OnDragMoved(new MessageEventArgs(message, pos));
					}
					break;

				case MessageType.MouseUp:
					if ( this.mouseDown )
					{
						this.OnDragEnded(new MessageEventArgs(message, pos));
						this.mouseDown = false;
					}
					break;
			}
			
			message.Consumer = this;
		}

		// Le slider va �tre d�plac�.
		protected virtual void OnDragStarted(MessageEventArgs e)
		{
			if ( this.DragStarted != null )  // qq'un �coute ?
			{
				if ( e != null )
				{
					e.Message.Consumer = this;
				}
				
				this.DragStarted(this, e);
			}
		}

		// Le slider est d�plac�.
		protected virtual void OnDragMoved(MessageEventArgs e)
		{
			if ( this.DragMoved != null )  // qq'un �coute ?
			{
				if ( e != null )
				{
					e.Message.Consumer = this;
				}
				
				this.DragMoved(this, e);
			}
		}

		// Le slider est fini de d�placer.
		protected virtual void OnDragEnded(MessageEventArgs e)
		{
			if ( this.DragEnded != null )  // qq'un �coute ?
			{
				if ( e != null )
				{
					e.Message.Consumer = this;
				}
				
				this.DragEnded(this, e);
			}
		}


		// Dessine le bouton.
		protected override void PaintBackgroundImplementation(Drawing.Graphics graphics, Drawing.Rectangle clipRect)
		{
			IAdorner adorner = Widgets.Adorner.Factory.Active;

			Drawing.Rectangle rect  = new Drawing.Rectangle(0, 0, this.Client.Width, this.Client.Height);
			WidgetState       state = this.PaintState;
			
			if ( (state & WidgetState.Entered) != 0 || this.mouseDown )
			{
				state |= WidgetState.Entered;
				state &= ~WidgetState.Focused;
				adorner.PaintButtonBackground(graphics, rect, state, Direction.Up, ButtonStyle.Normal);
			}
		}
		

		public event MessageEventHandler	DragStarted;
		public event MessageEventHandler	DragMoved;
		public event MessageEventHandler	DragEnded;
		
		protected HeaderSliderStyle			headerSliderStyle;
		protected bool						mouseDown = false;
	}
}

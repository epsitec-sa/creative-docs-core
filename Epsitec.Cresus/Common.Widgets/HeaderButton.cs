namespace Epsitec.Common.Widgets
{
	public enum HeaderButtonStyle
	{
		Top,			// bouton dans en-t�te sup�rieure
		Left,			// bouton dans en-t�te gauche
	}
	
	/// <summary>
	/// La class HeaderButton repr�sente un bouton d'un en-t�te de tableau.
	/// </summary>
	public class HeaderButton : AbstractButton
	{
		public HeaderButton()
		{
			//this.internal_state &= ~InternalState.Engageable;
			this.internal_state &= ~InternalState.AutoFocus;
			this.internal_state &= ~InternalState.Focusable;
			this.headerButtonStyle = HeaderButtonStyle.Top;
		}
		
		// Bouton dans en-t�te sup�rieure ou gauche ?
		public HeaderButtonStyle HeaderButtonStyle
		{
			get
			{
				return this.headerButtonStyle;
			}

			set
			{
				if ( this.headerButtonStyle != value )
				{
					this.headerButtonStyle = value;
					this.Invalidate();
				}
			}
		}

		// Bouton statique ou dynamique ?
		public bool Dynamic
		{
			get
			{
				return this.dynamic;
			}

			set
			{
				this.dynamic = value;
			}
		}

		// Rang de la ligne ou de la colonne associ� au bouton.
		public int Rank
		{
			get
			{
				return this.rank;
			}

			set
			{
				this.rank = value;
			}
		}

		// Choix pour le triangle du bouton.
		public int SortMode
		{
			get
			{
				return this.sortMode;
			}

			set
			{
				if ( this.sortMode != value )
				{
					this.sortMode = value;
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
			Direction         dir   = this.RootDirection;
			Drawing.Point     pos   = new Drawing.Point(0, 0);

			if ( !this.dynamic )
			{
				state &= ~WidgetState.Engaged;
				state &= ~WidgetState.Entered;
			}
			
			Direction type = Direction.None;
			if ( this.headerButtonStyle == HeaderButtonStyle.Top )
			{
				type = Direction.Up;
			}
			if ( this.headerButtonStyle == HeaderButtonStyle.Left )
			{
				type = Direction.Left;
			}
			adorner.PaintHeaderBackground(graphics, rect, state, dir, type);
			adorner.PaintButtonTextLayout(graphics, pos, this.text_layout, state, dir, ButtonStyle.Flat);

			if ( this.sortMode != 0 )  // triangle ?
			{
				type = Direction.None;

				if ( this.headerButtonStyle == HeaderButtonStyle.Top )
				{
					rect.Right = rect.Left+rect.Height;
					rect.Width  *= 0.75;
					rect.Height *= 0.75;

					if ( this.sortMode > 0 )
					{
						type = Direction.Down;
					}
					else
					{
						type = Direction.Up;
						rect.Offset(0, rect.Height/3);
					}
				}
				if ( this.headerButtonStyle == HeaderButtonStyle.Left )
				{
					double dim = 14;
					rect.Left   = rect.Right-dim;
					rect.Bottom = rect.Height/2-dim/2;
					rect.Top    = rect.Bottom+dim;

					if ( this.sortMode > 0 )
					{
						type = Direction.Right;
					}
					else
					{
						type = Direction.Left;
					}
				}
				adorner.PaintArrow(graphics, rect, state, dir, type);
			}
		}
		
		protected HeaderButtonStyle			headerButtonStyle;
		protected bool						dynamic = false;
		protected int						sortMode = 0;
		protected int						rank;
	}
}

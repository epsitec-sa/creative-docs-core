namespace Epsitec.Common.Widgets
{
	public enum ScrollListStyle
	{
		Flat,							// pas de cadre, ni de relief
		Normal,							// bouton normal
		Simple,							// cadre tout simple
	}

	public enum ScrollListShow
	{
		Extremity,		// d�placement minimal aux extr�mit�s
		Middle,			// d�placement central
	}

	public enum ScrollListAdjust
	{
		MoveUp,			// d�place le haut
		MoveDown,		// d�place le bas
	}

	/// <summary>
	/// La classe ScrollList r�alise une liste d�roulante simple.
	/// </summary>
	public class ScrollList : Widget
	{
		public ScrollList()
		{
			this.internalState |= InternalState.AutoFocus;
			this.internalState |= InternalState.Focusable;

			this.scrollListStyle = ScrollListStyle.Normal;
			this.lineHeight = this.DefaultFontHeight+1;
			this.scroller = new VScroller();
			this.scroller.IsInverted = true;
			this.scroller.Moved += new EventHandler(this.HandleScroller);
			this.Children.Add(this.scroller);
		}

		protected override void Dispose(bool disposing)
		{
			if ( disposing )
			{
				System.Diagnostics.Debug.WriteLine("Dispose ScrollList " + this.Text);
				
				this.scroller.Moved -= new EventHandler(this.HandleScroller);
			}
			
			base.Dispose(disposing);
		}


		public ScrollListStyle ScrollListStyle
		{
			get
			{
				return this.scrollListStyle;
			}

			set
			{
				if ( this.scrollListStyle != value )
				{
					this.scrollListStyle = value;
					this.Invalidate();
				}
			}
		}

		public bool ComboMode
		{
			set
			{
				this.isComboList = value;
			}

			get
			{
				return this.isComboList;
			}
		}
		
		public Scroller Scroller
		{
			get { return this.scroller; }
		}


		// Vide toute la liste.
		public void Reset()
		{
			this.firstLine = 0;
			this.selectedLine = -1;
			this.list.Clear();
			this.isDirty = true;
			this.Invalidate();
			this.OnSelectedIndexChanged();
		}

		// Ajoute un texte � la fin de la liste.
		public void AddText(string text)
		{
			this.list.Add(text);
			this.isDirty = true;
			this.Invalidate();
		}

		// Donne un texte de la liste.
		public string GetText(int index)
		{
			if ( index < 0 || index >= this.list.Count )  return "";
			return (string)this.list[index];
		}

		// Ligne s�lectionn�e, -1 si aucune.
		public int SelectedIndex
		{
			get
			{
				return this.selectedLine;
			}

			set
			{
				if ( value != -1 )
				{
					value = System.Math.Max(value, 0);
					value = System.Math.Min(value, this.list.Count-1);
				}
				if ( value != this.selectedLine )
				{
					this.selectedLine = value;
					this.isDirty = true;
					this.Invalidate();
					if ( !this.isComboList )
					{
						this.OnSelectedIndexChanged();
					}
				}
			}
		}

		// Premi�re ligne visible.
		public int FirstLine
		{
			get
			{
				return this.firstLine;
			}

			set
			{
				value = System.Math.Max(value, 0);
				value = System.Math.Min(value, System.Math.Max(this.list.Count-this.visibleLines, 0));
				if ( value != this.firstLine )
				{
					this.firstLine = value;
					this.UpdateTextlayouts();
					this.Invalidate();
				}
			}
		}

		// Indique si la ligne s�lectionn�e est visible.
		public bool IsShowSelect()
		{
			if ( this.selectedLine == -1 )  return true;
			if ( this.selectedLine >= this.firstLine &&
				 this.selectedLine <  this.firstLine+this.visibleLines )  return true;
			return false;
		}

		// Rend la ligne s�lectionn�e visible.
		public void ShowSelect(ScrollListShow mode)
		{
			if ( this.selectedLine == -1 )  return;

			int fl = this.FirstLine;
			if ( mode == ScrollListShow.Extremity )
			{
				if ( this.selectedLine < this.firstLine )
				{
					fl = this.selectedLine;
				}
				if ( this.selectedLine > this.firstLine+this.visibleLines-1 )
				{
					fl = this.selectedLine-(this.visibleLines-1);
				}
			}
			if ( mode == ScrollListShow.Middle )
			{
				int display = System.Math.Min(this.visibleLines, this.list.Count);
				fl = System.Math.Min(this.selectedLine+display/2, this.list.Count-1);
				fl = System.Math.Max(fl-display+1, 0);
			}
			this.FirstLine = fl;
		}

		// Ajuste la hauteur pour afficher pile un nombre entier de lignes.
		public bool AdjustToMultiple(ScrollListAdjust mode)
		{
			double h = this.Height-this.margin*2;
			int nbLines = (int)(h/this.lineHeight);
			double adjust = h - nbLines*this.lineHeight;
			if ( adjust == 0 )  return false;

			if ( mode == ScrollListAdjust.MoveUp )
			{
				this.Top = System.Math.Floor(this.Top-adjust);
			}
			if ( mode == ScrollListAdjust.MoveDown )
			{
				this.Bottom = System.Math.Floor(this.Bottom+adjust);
			}
			this.Invalidate();
			return true;
		}

		// Ajuste la hauteur pour afficher exactement le nombre de lignes contenues.
		public bool AdjustToContent(ScrollListAdjust mode, double hMin, double hMax)
		{
			double h = this.lineHeight*this.list.Count+this.margin*2;
			double hope = h;
			h = System.Math.Max(h, hMin);
			h = System.Math.Min(h, hMax);
			if ( h == this.Height )  return false;

			if ( mode == ScrollListAdjust.MoveUp )
			{
				this.Top = this.Bottom+h;
			}
			if ( mode == ScrollListAdjust.MoveDown )
			{
				this.Bottom = this.Top-h;
			}
			this.Invalidate();
			if ( h != hope )  AdjustToMultiple(mode);
			return true;
		}


		// Appel� lorsque l'ascenseur a boug�.
		private void HandleScroller(object sender)
		{
			this.FirstLine = (int)this.scroller.Value;
			//this.SetFocused(true);
		}


		// Gestion d'un �v�nement.
		protected override void ProcessMessage(Message message, Drawing.Point pos)
		{
			switch ( message.Type )
			{
				case MessageType.MouseDown:
					this.mouseDown = true;
					this.MouseSelect(pos.Y);
					break;
				
				case MessageType.MouseMove:
					if ( this.mouseDown || this.isComboList )
					{
						this.MouseSelect(pos.Y);
					}
					break;

				case MessageType.MouseUp:
					if ( this.mouseDown )
					{
						this.MouseSelect(pos.Y);
						if ( this.isComboList )
						{
							this.OnSelectedIndexChanged();
						}
						this.mouseDown = false;
					}
					break;

				case MessageType.KeyDown:
					//System.Diagnostics.Debug.WriteLine("KeyDown "+message.KeyChar+" "+message.KeyCode);
					ProcessKeyDown(message.KeyCode, message.IsShiftPressed, message.IsCtrlPressed);
					break;
			}
			
			message.Consumer = this;
		}

		// S�lectionne la ligne selon la souris.
		protected bool MouseSelect(double pos)
		{
			pos = this.Client.Height-pos;
			int line = (int)((pos-this.margin)/this.lineHeight);
			if ( line < 0 || line >= this.visibleLines )  return false;
			this.SelectedIndex = this.firstLine+line;
			return true;
		}

		// Gestion d'une touche press�e avec KeyDown dans la liste.
		protected void ProcessKeyDown(int key, bool isShiftPressed, bool isCtrlPressed)
		{
			int		sel;

			switch ( key )
			{
				case (int)System.Windows.Forms.Keys.Up:
					sel = this.SelectedIndex-1;
					if ( sel >= 0 )
					{
						this.SelectedIndex = sel;
						if ( !this.IsShowSelect() )  this.ShowSelect(ScrollListShow.Extremity);
					}
					break;

				case (int)System.Windows.Forms.Keys.Down:
					sel = this.SelectedIndex+1;
					if ( sel < this.list.Count )
					{
						this.SelectedIndex = sel;
						if ( !this.IsShowSelect() )  this.ShowSelect(ScrollListShow.Extremity);
					}
					break;

				case (int)System.Windows.Forms.Keys.Return:
				case (int)System.Windows.Forms.Keys.Space:
					if ( this.isComboList )
					{
						this.OnSelectedIndexChanged();
					}
					break;
			}
		}


		// Met � jour l'ascenseur en fonction de la liste.
		protected void UpdateScroller()
		{
			if ( this.scroller == null )  return;

			if ( !this.isDirty )  return;
			this.isDirty = false;

			int total = this.list.Count;
			if ( total <= this.visibleLines )
			{
				this.scroller.Hide();
			}
			else
			{
				this.scroller.Show();
				this.scroller.Range = total-this.visibleLines;
				this.scroller.Display = this.scroller.Range*((double)this.visibleLines/total);
				this.scroller.Value = this.firstLine;
				this.scroller.SmallChange = 1;
				this.scroller.LargeChange = this.visibleLines/2;
			}

			this.UpdateClientGeometry();
		}

		// Met � jour les textes.
		protected void UpdateTextlayouts()
		{
			int max = System.Math.Min(this.visibleLines, this.list.Count);
			for ( int i=0 ; i<max ; i++ )
			{
				if ( this.textLayouts[i] == null )
				{
					this.textLayouts[i] = new TextLayout();
				}
				this.textLayouts[i].Text = (string)this.list[i+this.firstLine];
				this.textLayouts[i].Font = this.DefaultFont;
				this.textLayouts[i].FontSize = this.DefaultFontSize;
				this.textLayouts[i].LayoutSize = new Drawing.Size(this.Width-this.margin*2-this.rightMargin, this.lineHeight);
			}
		}


		// Met � jour la g�om�trie de l'ascenseur de la liste.
		protected override void UpdateClientGeometry()
		{
			base.UpdateClientGeometry();
			
			if ( this.lineHeight == 0 )  return;

			Drawing.Rectangle rect = this.Bounds;
			this.margin = 3;
			rect.Inflate(-this.margin, -this.margin);

			this.visibleLines = (int)(rect.Height/this.lineHeight);
			this.textLayouts = new TextLayout[this.visibleLines];

			if ( this.scroller == null )
			{
				this.rightMargin = 0;
			}
			else
			{
				if ( this.scroller.IsVisible )
				{
					this.rightMargin = this.scroller.Width;
					Drawing.Rectangle aRect = new Drawing.Rectangle(this.margin+rect.Width-this.rightMargin, this.margin, this.rightMargin, rect.Height);
					this.scroller.Bounds = aRect;
				}
				else
				{
					this.rightMargin = 0;
				}
			}

			this.UpdateTextlayouts();
		}


		// G�n�re un �v�nement pour dire que la s�lection dans la liste a chang�.
		protected virtual void OnSelectedIndexChanged()
		{
			if ( this.SelectedIndexChanged != null )  // qq'un �coute ?
			{
				this.SelectedIndexChanged(this);
			}
		}


		// Dessine la liste.
		protected override void PaintBackgroundImplementation(Drawing.Graphics graphics, Drawing.Rectangle clipRect)
		{
			IAdorner adorner = Widgets.Adorner.Factory.Active;

			Drawing.Rectangle rect  = new Drawing.Rectangle(0, 0, this.Client.Width, this.Client.Height);
			WidgetState       state = this.PaintState;
			Direction         dir   = this.RootDirection;
			
			TextFieldStyle style = TextFieldStyle.Normal;
			switch ( this.scrollListStyle )
			{
				case ScrollListStyle.Flat:    style = TextFieldStyle.Flat;    break;
				case ScrollListStyle.Simple:  style = TextFieldStyle.Simple;  break;
			}
			adorner.PaintTextFieldBackground(graphics, rect, state, dir, style);

			this.UpdateScroller();
			Drawing.Point pos = new Drawing.Point(this.margin, rect.Height-this.margin-this.lineHeight);
			int max = System.Math.Min(this.visibleLines, this.list.Count);
			for ( int i=0 ; i<max ; i++ )
			{
				if ( this.textLayouts[i] == null )  break;

				Drawing.Color color = Drawing.Color.Empty;

				if ( i+this.firstLine == this.selectedLine &&
					(state&WidgetState.Enabled) != 0 )
				{
					Drawing.Rectangle[] rects = new Drawing.Rectangle[1];
					rects[0].Left   = this.margin;
					rects[0].Right  = this.Client.Width-this.margin-this.rightMargin-(this.scroller.IsVisible?2:0);
					rects[0].Bottom = pos.Y;
					rects[0].Top    = pos.Y+this.lineHeight;
					adorner.PaintTextSelectionBackground(graphics, new Drawing.Point(0,0), rects);

					color = Drawing.Color.FromName("ActiveCaptionText");
					state |= WidgetState.Selected;
				}
				else
				{
					state &= ~WidgetState.Selected;
				}

				//this.textLayouts[i].Paint(pos, graphics, Drawing.Rectangle.Empty, color);
				adorner.PaintButtonTextLayout(graphics, pos, this.textLayouts[i], state, dir, ButtonStyle.ListItem);
				pos.Y -= this.lineHeight;
			}
		}


		public event EventHandler SelectedIndexChanged;

		protected ScrollListStyle				scrollListStyle;
		protected bool							isComboList = false;
		protected bool							isDirty;
		protected bool							mouseDown = false;
		protected System.Collections.ArrayList	list = new System.Collections.ArrayList();
		protected TextLayout[]					textLayouts;
		protected double						margin;
		protected double						rightMargin;
		protected double						lineHeight;
		protected Scroller						scroller;
		protected int							visibleLines;
		protected int							firstLine = 0;
		protected int							selectedLine = -1;
	}
}

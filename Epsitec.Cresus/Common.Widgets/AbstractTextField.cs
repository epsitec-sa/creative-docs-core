namespace Epsitec.Common.Widgets
{
	using Keys = System.Windows.Forms.Keys;
	using BundleAttribute  = Epsitec.Common.Support.BundleAttribute;
	
	public enum TextFieldStyle
	{
		Flat,							// pas de cadre, ni de relief, fond blanc
		Normal,							// ligne �ditable normale
		Simple,							// cadre tout simple
		Static,							// comme Flat mais fond transparent, s�lectionnable, pas �ditable...
	}
	
	
	/// <summary>
	/// La classe TextField impl�mente la ligne �ditable, tout en permettant
	/// aussi de r�aliser l'�quivalent de la ComboBox Windows.
	/// </summary>
	public abstract class AbstractTextField : Widget
	{
		static AbstractTextField()
		{
			TextField.flashTimer.Tick += new System.EventHandler(TextField.HandleFlashTimer);
		}
		
		
		public AbstractTextField()
		{
			this.dockMargins = new Drawing.Margins (2, 2, 2, 2);

			this.internalState |= InternalState.AutoFocus;
			this.internalState |= InternalState.AutoEngage;
			this.internalState |= InternalState.Focusable;
			this.internalState |= InternalState.Engageable;
			this.textStyle = TextFieldStyle.Normal;

			this.ResetCursor();
			this.MouseCursor = MouseCursor.AsIBeam;

			this.CreateTextLayout();
		}
		

		protected override void Dispose(bool disposing)
		{
			if ( disposing )
			{
				System.Diagnostics.Debug.WriteLine("Dispose TextField " + this.Text);
				
				if (TextField.blinking == this)
				{
					TextField.blinking = null;
				}
			}
			
			base.Dispose(disposing);
		}

		
		// Mode pour la ligne �ditable.
		[ Bundle ("ro") ] public bool IsReadOnly
		{
			get
			{
				return this.isReadOnly;
			}

			set
			{
				if ( this.isReadOnly != value )
				{
					this.isReadOnly = value;
					this.MouseCursor = this.isReadOnly ? MouseCursor.AsArrow : MouseCursor.AsIBeam;
				}
			}
		}

		// Retourne la hauteur standard d'une ligne �ditable.
		public override double DefaultHeight
		{
			get
			{
				return this.DefaultFontHeight + 2*AbstractTextField.Margin;
			}
		}

		// Texte �dit�.
		public override string Text
		{
			get
			{
				return base.Text;
			}

			set
			{
				if ( value.Length > this.maxChar )
				{
					value = value.Substring(0, this.maxChar);
				}
				
				System.Diagnostics.Debug.Assert(this.textLayout != null);
				
				if ( base.Text != value )
				{
					base.Text = value;
					this.Invalidate();
					this.CursorScroll();
				}
			}
		}
		
		protected override void DisposeTextLayout()
		{
			// Ne fait rien, on veut s'assurer que le TextLayout associ� avec le
			// TextField n'est jamais d�truit du vivant du TextField.
			this.textLayout.Text = "";
		}


		// Nombre max de caract�res dans la ligne �dit�e.
		public int MaxChar
		{
			get
			{
				return this.maxChar;
			}

			set
			{
				this.maxChar = value;
			}
		}


		public virtual double LeftMargin
		{
			get
			{
				return this.leftMargin;
			}
		}
		
		public virtual double RightMargin
		{
			get
			{
				return this.rightMargin;
			}
		}
		
		public TextFieldStyle TextFieldStyle
		{
			get
			{
				return this.textStyle;
			}

			set
			{
				if ( this.textStyle != value )
				{
					this.textStyle = value;
					this.Invalidate();
				}
			}
		}

		// Position du curseur d'�dition.
		public int Cursor
		{
			get
			{
				return this.cursorTo;
			}

			set
			{
				value = System.Math.Max(value, 0);
				value = System.Math.Min(value, this.Text.Length);

				if ( value != this.cursorFrom && value != this.cursorTo )
				{
					this.cursorFrom = value;
					this.cursorTo   = value;
					this.CursorScroll();
					this.Invalidate();
				}
			}
		}
		
		public int CursorFrom
		{
			get
			{
				return this.cursorFrom;
			}

			set
			{
				value = System.Math.Max(value, 0);
				value = System.Math.Min(value, this.Text.Length);

				if ( value != this.cursorFrom )
				{
					this.cursorFrom = value;
					this.CursorScroll();
					this.Invalidate();
				}
			}
		}
		
		public int CursorTo
		{
			get
			{
				return this.cursorTo;
			}

			set
			{
				value = System.Math.Max(value, 0);
				value = System.Math.Min(value, this.Text.Length);

				if ( value != this.cursorTo )
				{
					this.cursorTo = value;
					this.CursorScroll();
					this.Invalidate();
				}
			}
		}

		// S�lectione tous les caract�res.
		public void SelectAll()
		{
			this.cursorFrom = 0;
			this.cursorTo = this.Text.Length;
			this.CursorScroll();
			this.Invalidate();
		}

		protected override void UpdateLayoutSize()
		{
			if ( this.textLayout != null )
			{
				double dx = this.Client.Width - AbstractTextField.Margin*2 - this.rightMargin - this.leftMargin;
				double dy = this.Client.Height - AbstractTextField.Margin*2;
				this.realSize = new Drawing.Size(dx, dy);
				this.textLayout.Alignment = this.Alignment;
				this.textLayout.LayoutSize = new Drawing.Size(AbstractTextField.Infinity, dy);

				if ( this.textLayout.Text != null )
				{
					this.CursorScroll();
				}
			}
		}

		// Retourne l'alignement par d�faut d'un bouton.
		public override Drawing.ContentAlignment DefaultAlignment
		{
			get
			{
				return Drawing.ContentAlignment.TopLeft;
			}
		}


		// G�re le temps �coul� pour faire clignoter un curseur.
		protected static void HandleFlashTimer(object source, System.EventArgs e)
		{
			TextField.showCursor = !TextField.showCursor;
			
			if ( TextField.blinking != null )
			{
				TextField.blinking.FlashCursor();
			}
		}
		
		protected override void OnFocused()
		{
			base.OnFocused();
			TextField.blinking = this;
			this.ResetCursor();
		}

		protected override void OnDefocused()
		{
			TextField.blinking = null;
			base.OnDefocused();
		}

		
		// Fait clignoter le curseur.
		protected void FlashCursor()
		{
			this.Invalidate();
		}

		// Allume le curseur au prochain affichage.
		protected void ResetCursor()
		{
			if ( this.IsFocused && this.WindowFrame.Focused )
			{
				TextField.flashTimer.Interval = SystemInformation.CursorBlinkDelay;
				TextField.flashTimer.Stop();
				TextField.flashTimer.Start();  // restart du timer
				TextField.showCursor = true;  // avec le curseur visible
			}
		}



		// Gestion d'un �v�nement.
		protected override void ProcessMessage(Message message, Drawing.Point pos)
		{
			pos.X -= AbstractTextField.Margin;
			pos.Y -= AbstractTextField.Margin;
			pos += this.scrollOffset;

			switch ( message.Type )
			{
				case MessageType.MouseDown:
					this.mouseDown = true;
					this.BeginPress(pos);
					break;
				
				case MessageType.MouseMove:
					if ( this.mouseDown )
					{
						this.MovePress(pos);
					}
					break;

				case MessageType.MouseUp:
					if ( this.mouseDown )
					{
						this.EndPress(pos);
						this.mouseDown = false;
					}
					break;

				case MessageType.KeyDown:
					this.ProcessKeyDown(message.KeyCodeAsKeys, message.IsShiftPressed, message.IsCtrlPressed);
					break;

				case MessageType.KeyPress:
					this.ProcessKeyPress(message.KeyChar);
					break;
			}
			
			message.Consumer = this;
		}

		// Appel� lorsque le bouton de la souris est press�.
		protected void BeginPress(Drawing.Point pos)
		{
			System.Diagnostics.Debug.Assert(this.textLayout != null);
			
			int detect = this.textLayout.DetectIndex(pos);
			if ( detect != -1 )
			{
				this.cursorFrom = detect;
				this.cursorTo   = detect;
				this.Invalidate();
			}
		}

		// Appel� lorsque la souris est d�plac�e, bouton press�.
		protected void MovePress(Drawing.Point pos)
		{
			System.Diagnostics.Debug.Assert(this.textLayout != null);
			
			int detect = this.textLayout.DetectIndex(pos);
			if ( detect != -1 )
			{
				this.cursorTo = detect;
				this.Invalidate();
			}
		}

		// Appel� lorsque le bouton de la souris est rel�ch�.
		protected void EndPress(Drawing.Point pos)
		{
			System.Diagnostics.Debug.Assert(this.textLayout != null);
			
			int detect = this.textLayout.DetectIndex(pos);
			if ( detect != -1 )
			{
				this.cursorTo = detect;
				this.Invalidate();
			}
		}

		// Gestion d'une touche press�e avec KeyDown dans le texte.
		protected virtual void ProcessKeyDown(Keys key, bool isShiftPressed, bool isCtrlPressed)
		{
			switch ( key )
			{
				case Keys.Back:
					this.DeleteCharacter(-1);
					break;

				case Keys.Delete:
					this.DeleteCharacter(1);
					break;

				case Keys.Escape:
					break;

				case Keys.Home:
					this.MoveCursor(-1000000, isShiftPressed, false);  // recule beaucoup
					break;

				case Keys.End:
					this.MoveCursor(1000000, isShiftPressed, false);  // avance beaucoup
					break;

				case Keys.PageUp:
					this.MoveCursor(-1000000, isShiftPressed, false);  // recule beaucoup
					break;

				case Keys.PageDown:
					this.MoveCursor(1000000, isShiftPressed, false);  // avance beaucoup
					break;

				case Keys.Left:
					this.MoveCursor(-1, isShiftPressed, isCtrlPressed);
					break;

				case Keys.Right:
					this.MoveCursor(1, isShiftPressed, isCtrlPressed);
					break;

				case Keys.Up:
					this.MoveCursor(-1, isShiftPressed, isCtrlPressed);
					break;

				case Keys.Down:
					this.MoveCursor(1, isShiftPressed, isCtrlPressed);
					break;
			}
		}

		// Gestion d'une touche press�e avec KeyPress dans le texte.
		protected void ProcessKeyPress(int key)
		{
			if ( key >= 32 )  // TODO: � v�rifier ...
			{
				this.InsertCharacter((char)key);
			}
		}

		// Ins�re un caract�re.
		protected bool InsertCharacter(char character)
		{
			return this.InsertString(TextLayout.ConvertToTaggedText (character));
		}

		// Ins�re une cha�ne correspondant � un caract�re ou un tag (jamais plus).
		protected bool InsertString(string ins)
		{
			System.Diagnostics.Debug.Assert(this.textLayout != null);
			if ( this.isReadOnly )  return false;
			this.DeleteSelectedCharacter(false);

			if ( this.Text.Length+ins.Length > this.maxChar )  return false;

			int cursor = this.textLayout.FindOffsetFromIndex(this.cursorTo);
			string text = this.Text;
			text = text.Insert(cursor, ins);
			this.Text = text;
			this.cursorTo ++;
			this.cursorFrom = this.cursorTo;
			this.Invalidate();
			this.ResetCursor();
			this.CursorScroll();
			this.OnTextChanged();
			this.OnTextInserted();
			return true;
		}

		// Supprime le caract�re � gauche ou � droite du curseur.
		protected bool DeleteCharacter(int dir)
		{
			System.Diagnostics.Debug.Assert(this.textLayout != null);
			if ( this.isReadOnly )  return false;
			if ( this.DeleteSelectedCharacter(true) )  return false;

			int cursor = this.textLayout.FindOffsetFromIndex(this.cursorTo);

			if ( dir < 0 )  // � gauche du curseur ?
			{
				if ( cursor <= 0 )  return false;

				string text = this.Text;
				int len = this.textLayout.RecedeTag(cursor);
				text = text.Remove(cursor-len, len);
				this.Text = text;
				this.cursorTo --;
				this.cursorFrom = this.cursorTo;
				this.Invalidate();
				this.ResetCursor();
				this.CursorScroll();
				this.OnTextChanged();
				this.OnTextDeleted();
			}
			else	// � droite du curseur ?
			{
				if ( cursor >= this.Text.Length )  return false;

				string text = this.Text;
				int len = this.textLayout.AdvanceTag(cursor);
				text = text.Remove(cursor, len);
				this.Text = text;
				this.Invalidate();
				this.ResetCursor();
				this.CursorScroll();
				this.OnTextChanged();
				this.OnTextDeleted();
			}

			return true;
		}

		// Supprime les caract�res s�lectionn�s dans le texte.
		protected bool DeleteSelectedCharacter(bool signalTextChanged)
		{
			System.Diagnostics.Debug.Assert(this.textLayout != null);
			if ( this.isReadOnly )  return false;
			
			int cursorFrom = this.textLayout.FindOffsetFromIndex(this.cursorFrom);
			int cursorTo   = this.textLayout.FindOffsetFromIndex(this.cursorTo);

			int from = System.Math.Min(cursorFrom, cursorTo);
			int to   = System.Math.Max(cursorFrom, cursorTo);

			if ( from == to )  return false;

			string text = this.Text;
			text = text.Remove(from, to-from);
			this.Text = text;
			from = this.textLayout.FindIndexFromOffset(from);
			this.cursorTo   = from;
			this.cursorFrom = from;
			this.Invalidate();
			this.ResetCursor();
			this.CursorScroll();
			if ( signalTextChanged )  this.OnTextChanged();
			this.OnTextDeleted();
			return true;
		}

		// Indique si un caract�re est un s�parateur pour les d�placements
		// avec Ctrl+fl�che.
		protected bool IsWordSeparator(char character)
		{
			character = System.Char.ToUpper(character);
			if ( character == '_' )  return false;
			if ( character >= 'A' && character <= 'Z' )  return false;
			if ( character >= '0' && character <= '9' )  return false;
			return true;
		}

		// D�place le curseur au d�but ou � la fin d'une ligne.
		protected bool MoveExtremity(int move, bool isShiftPressed, bool isCtrlPressed)
		{
			System.Diagnostics.Debug.Assert(this.textLayout != null);
			
			if ( isCtrlPressed )  // d�but/fin du texte ?
			{
				return this.MoveCursor(move*1000000, isShiftPressed, false);
			}

			double posx;
			if ( move < 0 )  posx = 0;
			else             posx = this.textLayout.LayoutSize.Width;
			int cursor = this.textLayout.DetectIndex(posx, this.cursorLine);
			if ( cursor == -1 )  return false;
			this.cursorTo = cursor;
			if ( !isShiftPressed )
			{
				this.cursorFrom = cursor;
			}
			this.CursorScroll();
			this.Invalidate();
			this.ResetCursor();
			return true;
		}

		// D�place le curseur par lignes.
		protected bool MoveLine(int move, bool isShiftPressed, bool isCtrlPressed)
		{
			System.Diagnostics.Debug.Assert(this.textLayout != null);
			
			int cursor = this.textLayout.DetectIndex(this.cursorPosX, this.cursorLine+move);
			if ( cursor == -1 )  return false;
			this.cursorTo = cursor;
			if ( !isShiftPressed )
			{
				this.cursorFrom = cursor;
			}
			this.CursorScroll();
			this.Invalidate();
			this.ResetCursor();
			return true;
		}

		// D�place le curseur.
		protected bool MoveCursor(int move, bool isShiftPressed, bool isCtrlPressed)
		{
			int cursor = this.cursorTo;
			string simple = TextLayout.ConvertToSimpleText(this.Text);

			if ( isCtrlPressed )  // d�placement par mots ?
			{
				if ( move < 0 )
				{
					while ( cursor > 0 )
					{
						if ( !this.IsWordSeparator(simple[cursor-1]) )  break;
						cursor --;
					}
					while ( cursor > 0 )
					{
						if ( this.IsWordSeparator(simple[cursor-1]) )  break;
						cursor --;
					}
				}
				else
				{
					while ( cursor < simple.Length )
					{
						if ( this.IsWordSeparator(simple[cursor]) )  break;
						cursor ++;
					}
					while ( cursor < simple.Length )
					{
						if ( !this.IsWordSeparator(simple[cursor]) )  break;
						cursor ++;
					}
				}
			}
			else	// d�placement par caract�res ?
			{
				cursor += move;
			}

			cursor = System.Math.Max(cursor, 0);
			cursor = System.Math.Min(cursor, simple.Length);
			if ( cursor == this.cursorTo && cursor == this.cursorFrom )  return false;
			this.cursorTo = cursor;
			if ( !isShiftPressed )
			{
				this.cursorFrom = cursor;
			}
			this.CursorScroll();
			this.Invalidate();
			this.ResetCursor();
			return true;
		}


		// G�n�re un �v�nement pour dire que le texte a chang� (ajout ou suppression).
		protected virtual void OnTextChanged()
		{
			if ( this.TextChanged != null )  // qq'un �coute ?
			{
				this.TextChanged(this);
			}
		}

		// G�n�re un �v�nement pour dire que le texte a chang� (ajout).
		protected virtual void OnTextInserted()
		{
			if ( this.TextInserted != null )  // qq'un �coute ?
			{
				this.TextInserted(this);
			}
		}

		// G�n�re un �v�nement pour dire que le texte a chang� (suppression).
		protected virtual void OnTextDeleted()
		{
			if ( this.TextDeleted != null )  // qq'un �coute ?
			{
				this.TextDeleted(this);
			}
		}


		// Calcule le scrolling pour que le curseur soit visible.
		protected void CursorScroll()
		{
			System.Diagnostics.Debug.Assert(this.textLayout != null);
			
			if ( this.mouseDown )  return;

			this.scrollOffset = new Drawing.Point(0, 0);

			Drawing.Rectangle cursor = this.textLayout.FindTextCursor(this.cursorTo, out this.cursorLine);
			this.cursorPosX = (cursor.Left+cursor.Right)/2;

			Drawing.Point end = this.textLayout.FindTextEnd();
			
			this.CursorScrollTextEnd(end, cursor);
		}
		
		protected virtual void CursorScrollTextEnd(Drawing.Point end, Drawing.Rectangle cursor)
		{
			double offset = cursor.Right;
			offset += this.realSize.Width/2;
			offset  = System.Math.Min (offset, end.X);
			offset -= this.realSize.Width;
			offset  = System.Math.Max (offset, 0);
			this.scrollOffset.X = offset;
		}

		protected override void PaintBackgroundImplementation(Drawing.Graphics graphics, Drawing.Rectangle clipRect)
		{
			// Dessine le texte en cours d'�dition.
			System.Diagnostics.Debug.Assert(this.textLayout != null);
			
			IAdorner adorner = Widgets.Adorner.Factory.Active;

			Drawing.Rectangle rect   = new Drawing.Rectangle(0, 0, this.Client.Width, this.Client.Height);
			WidgetState       state  = this.PaintState;
			Direction         dir    = this.RootDirection;
			Drawing.Point     pos    = new Drawing.Point(AbstractTextField.Margin, AbstractTextField.Margin);
			double            button = (this.rightMargin == 0) ? 0 : (this.rightMargin + 1);
			
			pos -= this.scrollOffset;
			
			if (this.isCombo)
			{
				adorner.PaintTextFieldBackground(graphics, rect, state, dir, this.textStyle, false);
			}
			else
			{
				adorner.PaintTextFieldBackground(graphics, rect, state, dir, this.textStyle, this.isReadOnly);
			}
			
			Drawing.Rectangle rSaveClip = graphics.SaveClippingRectangle ();
			Drawing.Rectangle rClip = rect;
			rClip.Inflate(-2, -2);
			rClip = this.MapClientToRoot (rClip);
			graphics.SetClippingRectangle(rClip);

			if ( this.IsFocused )
			{
				bool visibleCursor = false;
				
				int from = System.Math.Min(this.cursorFrom, this.cursorTo);
				int to   = System.Math.Max(this.cursorFrom, this.cursorTo);
				
				if (this.isCombo && this.isReadOnly)
				{
					Drawing.Rectangle[] rects = new Drawing.Rectangle[1];
					rects[0] = rect;
					rects[0].Inflate(-3, -3);
					rects[0].Right -= button;
					adorner.PaintTextSelectionBackground(graphics, new Drawing.Point (0, 0), rects);
					adorner.PaintGeneralTextLayout(graphics, pos, this.textLayout, state&~WidgetState.Focused, dir);
					adorner.PaintFocusBox(graphics, rects[0]);
				}
				else if ( from == to )
				{
					adorner.PaintGeneralTextLayout(graphics, pos, this.textLayout, state&~WidgetState.Focused, dir);
					visibleCursor = TextField.showCursor && this.WindowFrame.Focused;
				}
				else
				{
					Drawing.Rectangle[] rects = this.textLayout.FindTextRange(from, to);
					adorner.PaintTextSelectionBackground(graphics, pos, rects);
					adorner.PaintGeneralTextLayout(graphics, pos, this.textLayout, state&~WidgetState.Focused, dir);
				}


				// Dessine le curseur.
				if ( !this.isReadOnly )
				{
					Drawing.Rectangle rCursor = this.textLayout.FindTextCursor(this.cursorTo, out this.cursorLine);
					this.cursorPosX = (rCursor.Left+rCursor.Right)/2;
					double x = rCursor.Left;
					double y = rCursor.Bottom;
					graphics.Align(ref x, ref y);
					rCursor.Left = x;
					rCursor.Right = x+1;
					adorner.PaintTextCursor(graphics, pos, rCursor, visibleCursor);
				}
			}
			else
			{
				adorner.PaintGeneralTextLayout(graphics, pos, this.textLayout, state&~WidgetState.Focused, dir);
			}

			graphics.RestoreClippingRectangle(rSaveClip);
		}


		public event EventHandler TextChanged;
		public event EventHandler TextInserted;
		public event EventHandler TextDeleted;
		
		
		protected static readonly double		Margin = 4;
		protected static readonly double		Infinity = 1000000;
		
		protected bool							isReadOnly = false;
		protected bool							isCombo = false;
		protected double						leftMargin = 0;
		protected double						rightMargin = 0;
		protected Drawing.Size					realSize;
		protected Drawing.Point					scrollOffset = new Drawing.Point(0, 0);
		protected TextFieldStyle				textStyle;
		protected int							cursorFrom = 0;
		protected int							cursorTo = 0;
		protected int							cursorLine;
		protected double						cursorPosX;
		protected int							maxChar = 1000;
		protected bool							mouseDown = false;
		
		protected static System.Windows.Forms.Timer	flashTimer = new System.Windows.Forms.Timer();
		protected static bool					showCursor = true;
		
		protected static AbstractTextField		blinking;
	}
}

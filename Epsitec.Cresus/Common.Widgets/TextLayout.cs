namespace Epsitec.Common.Widgets
{
	public class AnchorEventArgs : System.EventArgs
	{
		public AnchorEventArgs(double x, double y, double dx, double dy, int index)
		{
			this.rect  = new Drawing.Rectangle(x, y, dx, dy);
			this.index = index;
		}
		
		
		public Drawing.Rectangle				Bounds
		{
			get
			{
				return this.rect;
			}
		}
		
		public int								Index
		{
			get
			{
				return this.index;
			}
		}
				
		
		private Drawing.Rectangle				rect;
		private int								index;
	}
	
	public delegate void AnchorEventHandler(object sender, AnchorEventArgs e);


	/// <summary>
	/// La classe TextLayout permet de stocker et d'afficher des contenus
	/// riches (un sous-ensemble tr�s restreint de HTML).
	/// </summary>
	public class TextLayout
	{
		public TextLayout()
		{
		}
		

		public string							Text
		{
			// Texte associ�, contenant des commandes HTML.
			get
			{
				return this.text;
			}

			set
			{
				if ( value == null )
				{
					value = "";
				}
				
				if ( this.text != value )
				{
					int offsetError;
					if ( TextLayout.CheckSyntax(value, out offsetError) )
					{
						this.text = value;
						this.textLength = value.Length;
						this.isDirty = true;
					}
					else
					{
						throw new System.FormatException("Syntax error at char " + offsetError.ToString());
					}
				}
			}
		}
		
		public int								TextLength
		{
			get { return this.textLength; }
		}
		
		public Drawing.Font						Font
		{
			// Fonte par d�faut.
			get
			{
				return this.font;
			}

			set
			{
				if ( value != this.font )
				{
					this.font = value;
					this.isDirty = true;
				}
			}
		}
		
		public double							FontSize
		{
			// Taille de la fonte par d�faut.
			get
			{
				return this.fontSize;
			}

			set
			{
				if ( this.fontSize != value )
				{
					this.fontSize = value;
					this.isDirty = true;
				}
			}
		}

		public static Drawing.Color				AnchorColor
		{
			// Couleur pour les liens.
			get { return TextLayout.anchorColor; }
			set { TextLayout.anchorColor = value; }
		}

		public Drawing.ContentAlignment			Alignment
		{
			// Alignement du texte dans le rectangle.
			get
			{
				return this.alignment;
			}

			set
			{
				if ( this.alignment != value )
				{
					this.alignment = value;
					this.isDirty = true;
				}
			}
		}

		public Drawing.TextBreakMode			BreakMode
		{
			// Mode de c�sure.
			get { return this.breakMode; }
			set { this.breakMode = value; }
		}
		
		public Drawing.Size						LayoutSize
		{
			// Dimensions du rectangle.
			get
			{
				return this.layoutSize;
			}

			set
			{
				if ( this.layoutSize != value )
				{
					this.layoutSize = value;
					this.isDirty = true;
				}
			}
		}

		public Drawing.Size						SingleLineSize
		{
			// Retourne les dimensions du texte ind�pendament de LayoutSize,
			// s'il est mis sur une seule ligne.
			get
			{
				Drawing.Size originalSize = this.LayoutSize;
				Drawing.ContentAlignment originalAlignment = this.Alignment;

				this.LayoutSize = new Drawing.Size(TextLayout.Infinite, TextLayout.Infinite);
				this.Alignment  = Drawing.ContentAlignment.TopLeft;

				Drawing.Point end = this.FindTextEnd();
				
				this.LayoutSize = originalSize;
				this.Alignment  = originalAlignment;

				return new Drawing.Size(end.X, TextLayout.Infinite-end.Y);
			}
		}
		
		public int								TotalLineCount
		{
			// Retourne le nombre de lignes total dans le layout courant
			// (y compris les lignes qui d�bordent).
			get
			{
				this.UpdateLayout();
				return this.totalLine;
			}
		}
		
		public int								VisibleLineCount
		{
			// Retourne le nombre de lignes visibles dans le layout courant
			// (sans compter les lignes qui d�bordent).
			get
			{
				this.UpdateLayout();
				return this.visibleLine;
			}
		}
		
		public Drawing.Rectangle				TotalRectangle
		{
			// Retourne le rectangle englobant du layout courant; ce
			// rectangle comprend toutes les lignes, m�me celles qui d�bordent.
			get { return this.GetRectangleBounds(true); }
		}
		
		public Drawing.Rectangle				VisibleRectangle
		{
			// Retourne le rectangle englobant du layout courant; ce
			// rectangle comprend uniquement les lignes visibles.
			get { return this.GetRectangleBounds(false); }
		}
		
		public Drawing.Rectangle				StandardRectangle
		{
			// Retourne le rectangle standard englobant du layout courant; ce
			// rectangle ne d�pend pas de la hauteur des lettres du texte.
			// Le rectangle aura la m�me hauteur avec "ace" ou "Ap".
			get
			{
				this.UpdateLayout();

				Drawing.Rectangle totalRect = Drawing.Rectangle.Empty;
				foreach ( JustifBlock block in this.blocks )
				{
					if ( !block.visible )  continue;

					Drawing.Rectangle blockRect = new Drawing.Rectangle();
					blockRect.Left  = 0;
					blockRect.Right = block.width;
					if ( block.image )
					{
						blockRect.Top    = block.imageAscender;
						blockRect.Bottom = block.imageDescender;
					}
					else
					{
						blockRect.Top    = block.fontSize*block.font.Ascender;
						blockRect.Bottom = block.fontSize*block.font.Descender;
					}
					blockRect.Offset(block.pos.X, block.pos.Y);
					totalRect.MergeWith(blockRect);
				}
				return totalRect;
			}
		}
		
		public Drawing.IImageProvider			ImageProvider
		{
			// Gestionnaire d'images.
			get { return this.imageProvider; }
			set { this.imageProvider = value; }
		}


		protected Drawing.Rectangle GetRectangleBounds(bool all)
		{
			// Retourne le rectangle englobant du layout, en tenant compte de
			// toutes les lignes (all=true) ou seulement des lignes visibles (all=false).
			// Si le texte est align� sur le bord gauche, rectangle.Left n'est pas
			// forc�ment �gal � 0.
			this.UpdateLayout();

			Drawing.Rectangle totalRect = Drawing.Rectangle.Empty;
			foreach ( JustifBlock block in this.blocks )
			{
				if ( !all && !block.visible )  continue;

				Drawing.Rectangle blockRect;
				if ( block.image )
				{
					blockRect = new Drawing.Rectangle();
					blockRect.Left   = 0;
					blockRect.Right  = block.width;
					blockRect.Top    = block.imageAscender;
					blockRect.Bottom = block.imageDescender;
				}
				else
				{
					blockRect = block.font.GetTextBounds(block.text);
					blockRect.Scale(block.fontSize);
				}
				blockRect.Offset(block.pos.X, block.pos.Y);
				totalRect.MergeWith(blockRect);
			}
			return totalRect;
		}


		// S�lectionne tout le texte.
		public void SelectAll(TextLayout.Context context)
		{
			context.CursorFrom = 0;
			context.CursorTo   = this.Text.Length;
		}

		// S�lectionne toute la ligne.
		public void SelectLine(TextLayout.Context context)
		{
			this.MoveExtremity(context, -1, false);
			int from = context.CursorFrom;
			this.MoveExtremity(context, 1, false);
			context.CursorFrom = from;
		}

		// S�lectionne tout le mot.
		public void SelectWord(TextLayout.Context context)
		{
			string simple = TextLayout.ConvertToSimpleText(this.Text);

			while ( context.CursorFrom > 0 )
			{
				if ( this.IsWordSeparator(simple[context.CursorFrom-1]) )  break;
				context.CursorFrom --;
			}

			while ( context.CursorTo < simple.Length )
			{
				if ( this.IsWordSeparator(simple[context.CursorTo]) )  break;
				context.CursorTo ++;
			}
		}

		public bool DeleteSelection(TextLayout.Context context)
		{
			// Supprime les caract�res s�lectionn�s dans le texte.
			int cursorFrom = this.FindOffsetFromIndex(context.CursorFrom);
			int cursorTo   = this.FindOffsetFromIndex(context.CursorTo);
			
			int from = System.Math.Min(cursorFrom, cursorTo);
			int to   = System.Math.Max(cursorFrom, cursorTo);
			
			if ( from == to )  return false;
			
			string text = this.Text;
			text = text.Remove(from, to-from);
			from = this.FindIndexFromOffset(from);
			context.CursorTo   = from;
			context.CursorFrom = from;
			this.Text = text;
			
			return true;
		}

		public bool ReplaceSelection(TextLayout.Context context, string ins)
		{
			// Ins�re une cha�ne correspondant � un caract�re ou un tag (jamais plus).
			int cursorFrom = this.FindOffsetFromIndex(context.CursorFrom);
			int cursorTo   = this.FindOffsetFromIndex(context.CursorTo);
			
			int from = System.Math.Min(cursorFrom, cursorTo);
			int to   = System.Math.Max(cursorFrom, cursorTo);
			
			string text = this.Text;
			
			if ( from < to )
			{
				text = text.Remove(from, to-from);
				from = this.FindIndexFromOffset(from);
				context.CursorTo   = from;
				context.CursorFrom = from;
			}
			
			if ( this.Text.Length+ins.Length > context.MaxChar )
			{
				this.Text = text;
				return false;
			}
			
			int cursor = this.FindOffsetFromIndex(context.CursorTo);
			text = text.Insert(cursor, ins);
			this.Text = text;
			context.CursorTo   = this.FindIndexFromOffset(cursor + ins.Length);
			context.CursorFrom = context.CursorTo;
			return true;
		}

		public bool InsertCharacter(TextLayout.Context context, char character)
		{
			// Ins�re un caract�re.
			return this.ReplaceSelection(context, TextLayout.ConvertToTaggedText(character));
		}

		public bool DeleteCharacter(TextLayout.Context context, int dir)
		{
			// Supprime le caract�re � gauche ou � droite du curseur.
			if ( this.DeleteSelection(context) )  return false;

			int cursor = this.FindOffsetFromIndex(context.CursorTo);

			if ( dir < 0 )  // � gauche du curseur ?
			{
				if ( cursor <= 0 )  return false;

				string text = this.Text;
				int len = this.RecedeTag(cursor);
				text = text.Remove(cursor-len, len);
				context.CursorTo --;
				context.CursorFrom = context.CursorTo;
				this.Text = text;
			}
			else	// � droite du curseur ?
			{
				if ( cursor >= this.Text.Length )  return false;

				string text = this.Text;
				int len = this.AdvanceTag(cursor);
				text = text.Remove(cursor, len);
				this.Text = text;
			}
			return true;
		}
		
		
		public bool MoveLine(TextLayout.Context context, int move, bool select)
		{
			// D�place le curseur par lignes.
			int cursor = this.DetectIndex(context.CursorPosX, context.CursorLine+move);
			if ( cursor == -1 )  return false;

			context.CursorTo = cursor;
			if ( !select )  context.CursorFrom = cursor;
			return true;
		}

		public bool MoveExtremity(TextLayout.Context context, int move, bool select)
		{
			// D�place le curseur au d�but ou � la fin d'une ligne.
			double posx;
			if ( move < 0 )  posx = 0;
			else             posx = this.LayoutSize.Width;
			int cursor = this.DetectIndex(posx, context.CursorLine);
			if ( cursor == -1 )  return false;

			context.CursorTo = cursor;
			if ( !select )  context.CursorFrom = cursor;
			return true;
		}

		protected bool IsWordSeparator(char character)
		{
			// Indique si un caract�re est un s�parateur pour les d�placements
			// avec Ctrl+fl�che.
			character = System.Char.ToLower(character);
			if ( character == '_' ||
				 character == '�' || character == '�' || character == '�' || character == '�' ||
				 character == '�' ||
				 character == '�' || character == '�' || character == '�' || character == '�' ||
				 character == '�' || character == '�' || character == '�' || character == '�' ||
				 character == '�' || character == '�' || character == '�' || character == '�' ||
				 character == '�' || character == '�' || character == '�' || character == '�' )  return false;
			// TODO: g�n�raliser avec tous les accents exotiques ?
			if ( character >= 'a' && character <= 'z' )  return false;
			if ( character >= '0' && character <= '9' )  return false;
			return true;
		}

		public bool MoveCursor(TextLayout.Context context, int move, bool select, bool word)
		{
			// D�place le curseur.
			int cursor = context.CursorTo;
			string simple = TextLayout.ConvertToSimpleText(this.Text);

			if ( word )  // d�placement par mots ?
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
			if ( cursor == context.CursorTo && cursor == context.CursorFrom )  return false;

			context.CursorTo = cursor;
			if ( !select )  context.CursorFrom = cursor;
			return true;
		}


		public void Paint(Drawing.Point pos, Drawing.IPaintPort graphics)
		{
			// Dessine le texte, en fonction du layout...
			// Si une couleur est donn�e avec uniqueColor, tout le texte est peint
			// avec cette couleur, en ignorant les <font color=...>.
			this.Paint(pos, graphics, Drawing.Rectangle.Infinite, Drawing.Color.Empty, Drawing.GlyphPaintStyle.Normal);
		}

		public void Paint(Drawing.Point pos, Drawing.IPaintPort graphics, Drawing.Rectangle clipRect, Drawing.Color uniqueColor, Drawing.GlyphPaintStyle paintStyle)
		{
			this.UpdateLayout();

			IAdorner adorner = Adorner.Factory.Active;
			foreach ( JustifBlock block in this.blocks )
			{
				if ( !block.visible )  continue;

				if ( block.image )
				{
					Drawing.Image image = this.imageProvider.GetImage(block.text);
					
					if ( image == null )
					{
						throw new System.FormatException(string.Format("<img> tag references unknown image '{0}' while painting. Current directory is {1}.", block.text, System.IO.Directory.GetCurrentDirectory()));
					}
					
					if ( image.IsPaintStyleDefined(paintStyle) )
					{
						image = image.GetImageForPaintStyle(paintStyle);
					}
					
					image.DefineZoom(graphics.GetTransformZoom());
					image.DefineColor(uniqueColor);
					image.DefineAdorner(adorner);
					
					double dx = image.Width;
					double dy = image.Height;
					double ix = pos.X+block.pos.X;
					double iy = pos.Y+block.pos.Y+block.imageDescender;
					
					if ( block.anchor )
					{
						this.OnAnchor(new AnchorEventArgs(ix, iy, dx, dy, block.beginIndex));
					}
					
					graphics.Align(ref ix, ref iy);
					graphics.PaintImage(image.BitmapImage, ix, iy, dx, dy, 0, 0, image.Width, image.Height);
					continue;
				}

				Drawing.Color color;
				if ( uniqueColor.IsEmpty )
				{
					if ( block.anchor )
					{
						color = TextLayout.anchorColor;
					}
					else
					{
						color = block.fontColor;
					}
				}
				else
				{
					color = uniqueColor;
				}
				
				double x = pos.X+block.pos.X;
				double y = pos.Y+block.pos.Y;
				
				if ( block.anchor )
				{
					double ascender  = block.font.Ascender * block.fontSize;
					double descender = block.font.Descender * block.fontSize;
					
					this.OnAnchor(new AnchorEventArgs(x, y+descender, block.width, ascender-descender, block.beginIndex));
				}

				graphics.Color = color;
				graphics.PaintText(x, y, block.text, block.font, block.fontSize);

				if ( block.underline )
				{
					double p1x = pos.X+block.pos.X;
					double p2x = p1x+block.width;
					double py = pos.Y+block.pos.Y;
					JustifLine line = (JustifLine)this.lines[block.indexLine];
					py += line.descender/2;
					graphics.Align(ref p1x, ref py);
					graphics.Align(ref p2x, ref py);
					py -= 0.5;  // pour feinter l'anti-aliasing !
					graphics.LineWidth = 1;
					graphics.Color = color;
					graphics.PaintOutline(Drawing.Path.FromLine(p1x, py, p2x, py));
				}
			}
		}
		
		
		protected void OnAnchor(AnchorEventArgs e)
		{
			if ( this.Anchor != null )
			{
				this.Anchor(this, e);
			}
		}
		
		
		public int DetectIndex(Drawing.Point pos)
		{
			// Trouve l'index dans le texte interne qui correspond � la
			// position indiqu�e. Retourne -1 en cas d'�chec.
			this.UpdateLayout();

			pos.Y = System.Math.Max(pos.Y, 0);
			pos.Y = System.Math.Min(pos.Y, this.layoutSize.Height);
			pos.X = System.Math.Max(pos.X, 0);
			pos.X = System.Math.Min(pos.X, this.layoutSize.Width);
			
			foreach ( JustifLine line in this.lines )
			{
				if ( !line.visible )  continue;

				if ( pos.Y <= line.pos.Y+line.ascender  &&
					 pos.Y >= line.pos.Y+line.descender )
				{
					for ( int j=line.firstBlock ; j<=line.lastBlock ; j++ )
					{
						JustifBlock block = (JustifBlock)this.blocks[j];

						double width = block.width;
						if ( j == this.blocks.Count-1 )  // dernier bloc ?
						{
							width = this.layoutSize.Width-block.pos.X;
						}
						else
						{
							JustifBlock nextBlock = (JustifBlock)this.blocks[j+1];
							if ( nextBlock.bol )
							{
								width = this.layoutSize.Width-block.pos.X;
							}
						}

						if ( pos.X >= block.pos.X       &&
							 pos.X <= block.pos.X+width )
						{
							if ( block.image )
							{
								return block.beginIndex;
							}
							else
							{
								double[] charsWidth;
								block.font.GetTextCharEndX(block.text, out charsWidth);
								double left = 0;
								double right;
								for ( int k=0 ; k<charsWidth.Length ; k++ )
								{
									right = charsWidth[k]*block.fontSize;
									if ( pos.X-line.pos.X <= left+(right-left)/2 )
									{
										return block.beginIndex+k;
									}
									left = right;
								}
								return block.beginIndex+charsWidth.Length;
							}
						}
					}
				}
			}
			return -1;
		}
		
		public int DetectIndex(double posx, int posLine)
		{
			// Trouve l'index dans le texte interne qui correspond � la
			// position indiqu�e. Retourne -1 en cas d'�chec.
			this.UpdateLayout();
			
			posx = System.Math.Max(posx, 0);
			posx = System.Math.Min(posx, this.layoutSize.Width);

			foreach ( JustifLine line in this.lines )
			{
				if ( !line.visible )  continue;

				if ( posLine == line.rank )
				{
					for ( int j=line.firstBlock ; j<=line.lastBlock ; j++ )
					{
						JustifBlock block = (JustifBlock)this.blocks[j];

						double width = block.width;
						if ( j == this.blocks.Count-1 )  // dernier bloc ?
						{
							width = this.layoutSize.Width-block.pos.X;
						}
						else
						{
							JustifBlock nextBlock = (JustifBlock)this.blocks[j+1];
							if ( nextBlock.bol )
							{
								width = this.layoutSize.Width-block.pos.X;
							}
						}

						if ( posx >= block.pos.X       &&
							 posx <= block.pos.X+width )
						{
							if ( block.image )
							{
								return block.beginIndex;
							}
							else
							{
								double[] charsWidth;
								block.font.GetTextCharEndX(block.text, out charsWidth);
								double left = 0;
								double right;
								for ( int k=0 ; k<charsWidth.Length ; k++ )
								{
									right = charsWidth[k]*block.fontSize;
									if ( posx-line.pos.X <= left+(right-left)/2 )
									{
										return block.beginIndex+k;
									}
									left = right;
								}
								return block.beginIndex+charsWidth.Length;
							}
						}
					}
				}
			}
			return -1;
		}

		public string DetectAnchor(Drawing.Point pos)
		{
			// D�tecte s'il y a un lien hypertexte dans la liste des
			// tags actifs � la position en question. Si oui, extrait la cha�ne
			// de l'argument href, en supprimant les guillemets.
			int index = this.DetectIndex(pos);
			return this.FindAnchor(index);
		}
		
		public string FindAnchor(int index)
		{
			if ( index < 0 )  return null;
			int offset = this.FindOffsetFromIndex(index);
			if ( offset < 0 )  return null;
			
			string[] tags;
			if ( !this.AnalyseTagsAtOffset(offset, out tags) )  return null;
			for ( int i=0 ; i<tags.Length ; i++ )
			{
				if ( !tags[i].StartsWith("<a ") )  continue;
				int beginIndex = tags[i].IndexOf("href=\"");
				if ( beginIndex < 0 )  continue;
				beginIndex += 6;
				int endIndex = tags[i].IndexOf("\"", beginIndex);
				if ( endIndex < 0 )  continue;
				return tags[i].Substring(beginIndex, endIndex-beginIndex);
			}
			return null;
		}
		

		protected double IndexToPosX(JustifBlock block, int index)
		{
			// Retourne la position horizontale correspondant � un index dans un bloc.
			if ( index <= block.beginIndex )  return block.pos.X;
			if ( index >  block.endIndex   )  return block.pos.X+block.width;
			double[] charsWidth;
			block.font.GetTextCharEndX(block.text, out charsWidth);
			return block.pos.X+charsWidth[index-block.beginIndex-1]*block.fontSize;
		}

		
		public Drawing.Rectangle[] FindTextRange(Drawing.Point pos, int indexBegin, int indexEnd)
		{
			// Retourne un tableau avec les rectangles englobant le texte
			// sp�cifi� par son d�but et sa fin. Il y a un rectangle par ligne.
			if ( indexBegin >= indexEnd )  return new Drawing.Rectangle[0];

			this.UpdateLayout();

			System.Collections.ArrayList list = new System.Collections.ArrayList();
			Drawing.Rectangle rect = new Drawing.Rectangle();
			rect.Top    = -TextLayout.Infinite;
			rect.Bottom =  TextLayout.Infinite;
			rect.Left   =  TextLayout.Infinite;
			rect.Right  = -TextLayout.Infinite;
			foreach ( JustifBlock block in this.blocks )
			{
				JustifLine line = (JustifLine)this.lines[block.indexLine];
				if ( !block.visible )  continue;

				int localBegin = System.Math.Max(indexBegin, block.beginIndex);
				int localEnd   = System.Math.Min(indexEnd,   block.endIndex  );

				if ( localBegin >= localEnd )  continue;

				double top    = line.pos.Y+line.ascender;
				double bottom = line.pos.Y+line.descender;

				if ( rect.Top    != top    ||
					 rect.Bottom != bottom )  // rectangle dans autre ligne ?
				{
					if ( rect.Top > -TextLayout.Infinite && rect.Left < TextLayout.Infinite )
					{
						list.Add(rect);
					}

					rect.Top    = top;
					rect.Bottom = bottom;
					rect.Left   =  TextLayout.Infinite;
					rect.Right  = -TextLayout.Infinite;
				}

				if ( block.image )
				{
					rect.Left  = System.Math.Min(rect.Left,  block.pos.X);
					rect.Right = System.Math.Max(rect.Right, block.pos.X+block.width);
				}
				else
				{
					rect.Left  = System.Math.Min(rect.Left,  IndexToPosX(block, localBegin));
					rect.Right = System.Math.Max(rect.Right, IndexToPosX(block, localEnd  ));
				}
			}
			
			if ( rect.Top > -TextLayout.Infinite && rect.Left < TextLayout.Infinite )
			{
				list.Add(rect);
			}
			
			Drawing.Rectangle[] rects = new Drawing.Rectangle[list.Count];
			list.CopyTo(rects);
			
			for ( int i=0 ; i<rects.Length ; i++ )
			{
				rects[i].Offset(pos.X, pos.Y);
			}
					
			return rects;
		}
		
		public Drawing.Point GetLineOrigin(int line)
		{
			Drawing.Point pos;
			double ascender, descender, width;
			
			if ( this.GetLineGeometry(line, out pos, out ascender, out descender, out width) )
			{
				return pos;
			}
			
			return Drawing.Point.Empty;
		}
		
		public bool GetLineGeometry(int line, out Drawing.Point pos, out double ascender, out double descender, out double width)
		{
			this.UpdateLayout();
			
			if ( line >= 0 && line < this.lines.Count )
			{
				JustifLine info = (JustifLine)this.lines[line];
				
				pos       = info.pos;
				ascender  = info.ascender;
				descender = info.descender;
				width     = info.width;
				
				return true;
			}
			
			pos       = Drawing.Point.Empty;
			ascender  = 0;
			descender = 0;
			width     = 0;
			
			return false;
		}
		
		
		
		public Drawing.Rectangle FindTextCursor(int index, out int rankLine)
		{
			// Retourne le rectangle correspondant au curseur.
			// Indique �galement le num�ro de la ligne (0..n).
			this.UpdateLayout();

			rankLine = -1;
			foreach ( JustifBlock block in this.blocks )
			{
				JustifLine line = (JustifLine)this.lines[block.indexLine];
				if ( !block.visible )  continue;

				if ( index >= block.beginIndex && index <= block.endIndex )
				{
					Drawing.Rectangle rect = new Drawing.Rectangle();
					rect.Top    = line.pos.Y+line.ascender;
					rect.Bottom = line.pos.Y+line.descender;
					if ( block.image )
					{
						rect.Left  = block.pos.X;
						rect.Right = block.pos.X+block.width;
					}
					else
					{
						rect.Left  = IndexToPosX(block, index);
						rect.Right = rect.Left;
					}
					rankLine = line.rank;
					return rect;
				}
			}
			
			return Drawing.Rectangle.Empty;
		}

		public Drawing.Point FindTextEnd()
		{
			// Retourne le coin inf�rieur/droite du dernier caract�re.
			this.UpdateLayout();

			if ( this.blocks.Count == 0 )
			{
				return new Drawing.Point();
			}

			JustifBlock block = (JustifBlock)this.blocks[this.blocks.Count-1];
			Drawing.Point pos = new Drawing.Point(block.pos.X+block.width, block.pos.Y+block.font.Descender*block.fontSize);
			return pos;
		}
		
		
		public int AdvanceTag(int offset)
		{
			// Si on est au d�but d'un tag, donne la longueur jusqu'� la fin.
			if ( offset >= this.textLength )  return 0;

			if ( this.text[offset] == '<' )  // tag <xx> ?
			{
				int initial = offset;
				while ( this.text[offset] != '>' )
				{
					offset ++;
					if ( offset >= this.textLength )  break;
				}
				return offset-initial+1;
			}

			if ( this.text[offset] == '&' )  // tag &xx; ?
			{
				int initial = offset;
				while ( this.text[offset] != ';' )
				{
					offset ++;
					if ( offset >= this.textLength )  break;
				}
				return offset-initial+1;
			}

			return 1;
		}

		public int RecedeTag(int offset)
		{
			// Si on est � la fin d'un tag, donne la longueur jusqu'au d�but.
			if ( offset <= 0 )  return 0;
			offset --;

			if ( this.text[offset] == '>' )  // tag <xx> ?
			{
				int initial = offset;
				while ( this.text[offset] != '<' )
				{
					offset --;
					if ( offset == 0 )  break;
				}
				return initial-offset+1;
			}

			if ( this.text[offset] == ';' )  // tag &xx; ?
			{
				int initial = offset;
				while ( this.text[offset] != '&' )
				{
					if ( offset == 0 || initial-offset > 10 || this.text[offset-1] == ';' )  return 1;
					offset --;
				}
				return initial-offset+1;
			}

			return 1;
		}


		public int FindOffsetFromIndex(int textIndex)
		{
			// Retourne l'offset dans le texte interne, correspondant � l'index
			// sp�cifi� pour le texte sans tags. On saute tous les tags qui pr�c�dent
			// le caract�re indiqu� (textIndex=0 => premier caract�re non tag dans
			// le texte).
			int    index = 0;
			int    beginOffset;
			int    endOffset = 0;
			
			while ( endOffset <= this.textLength )
			{
				if ( endOffset == this.textLength )  return endOffset;
				beginOffset = endOffset;

				if ( this.text[endOffset] == '<' )
				{
					int length = this.text.IndexOf(">", endOffset)-endOffset+1;
					if ( length < 0 )  return -1;
					endOffset += length;

					string tag = this.text.Substring(beginOffset, length).ToLower();
					if ( tag != "<br/>" )  continue;
				}
				else if ( this.text[endOffset] == '&' )
				{
					int length = this.text.IndexOf(";", endOffset)-endOffset+1;
					if ( length < 0 )  return -1;
					endOffset += length;
				}
				else
				{
					endOffset ++;
				}

				if ( index == textIndex )  return beginOffset;
				index ++;
			}
			
			return -1;
		}
		
		public int FindIndexFromOffset(int taggedTextOffset)
		{
			// Retourne l'index dans le texte propre, correspondant � l'offset
			// sp�cifi� dans le texte avec tags.
			int    index = 0;
			int    beginOffset;

			if ( taggedTextOffset == 0 )  return index;

			int endOffset = 0;
			
			while ( endOffset < this.textLength )
			{
				beginOffset = endOffset;

				if ( this.text[endOffset] == '<' )
				{
					int length = this.text.IndexOf(">", endOffset)-endOffset+1;
					if ( length < 0 )  return -1;
					endOffset += length;

					string tag = this.text.Substring(beginOffset, length).ToLower();
					if ( tag == "<br/>" )
					{
						index ++;
					}
				}
				else if ( this.text[endOffset] == '&' )
				{
					int length = this.text.IndexOf(";", endOffset)-endOffset+1;
					if ( length < 0 )  return -1;
					endOffset += length;
					index ++;
				}
				else
				{
					endOffset ++;
					index ++;
				}

				if ( endOffset >= taggedTextOffset )  return index;
			}
			
			return -1;
		}
		
		
		protected static bool DeleteTagsList(string endTag, System.Collections.ArrayList list)
		{
			// Enl�ve un tag � la fin de la liste.
			System.Diagnostics.Debug.Assert(endTag.StartsWith("</"));
			System.Diagnostics.Debug.Assert(endTag.EndsWith(">"));

			endTag = endTag.Substring(2, endTag.Length-3).ToLower();  // </b> -> b

			for ( int i=list.Count-1 ; i>=0 ; i-- )
			{
				string s = (string)list[i];
				if ( s.IndexOf(endTag) == 1 )
				{
					list.RemoveAt(i);
					return true;
				}
			}
			return false;
		}

		
		public bool AnalyseTagsAtOffset(int offset, out string[] tags)
		{
			// Parcourt le texte et accumule les informations sur les tags <>
			// reconnus.
			if ( offset < 0 || offset > this.textLength )
			{
				tags = null;
				return false;
			}

			System.Collections.Hashtable parameters;
			System.Collections.ArrayList list = new System.Collections.ArrayList();
			int    beginOffset;
			int    endOffset = 0;
			while ( endOffset < offset )
			{
				beginOffset = endOffset;
				TextLayout.Tag tag = TextLayout.ParseTag(this.text, ref endOffset, out parameters);
				if ( tag == TextLayout.Tag.None )  continue;

				string sTag = this.text.Substring(beginOffset, endOffset-beginOffset);

				switch ( tag )
				{
					case TextLayout.Tag.Bold:
					case TextLayout.Tag.Italic:
					case TextLayout.Tag.Underline:
					case TextLayout.Tag.Mnemonic:
					case TextLayout.Tag.Font:
					case TextLayout.Tag.Anchor:
						list.Add(sTag);
						break;

					case TextLayout.Tag.BoldEnd:
					case TextLayout.Tag.ItalicEnd:
					case TextLayout.Tag.UnderlineEnd:
					case TextLayout.Tag.MnemonicEnd:
					case TextLayout.Tag.FontEnd:
					case TextLayout.Tag.AnchorEnd:
						TextLayout.DeleteTagsList(sTag, list);
						break;
				}
			}

			tags = new string[list.Count];
			list.CopyTo(tags);
			return true;
		}
		
		
		public static char AnalyseEntityChar(string text, ref int offset)
		{
			// Retourne le caract�re � un offset quelconque, en interpr�tant les
			// commandes &...;
			if ( text[offset] == '&' )
			{
				int length = text.IndexOf(";", offset)-offset+1;
				
				if ( length < 3 )
				{
					throw new System.FormatException(string.Format("Invalid entity found(too short)."));
				}
				
				char code;
				string entity = text.Substring(offset, length).ToLower();
				
				switch ( entity )
				{
					case "&lt;":    code = '<';   break;
					case "&gt;":    code = '>';   break;
					case "&amp;":   code = '&';   break;
					case "&quot;":  code = '"';   break;
					case "&apos;":  code = '\'';  break;
					
					default:
						if ( entity.StartsWith("&#") )
						{
							entity = entity.Substring(2, entity.Length-3);
							code   = (char)System.Int32.Parse(entity, System.Globalization.CultureInfo.InvariantCulture);
						}
						else
						{
							throw new System.FormatException(string.Format("Invalid entity {0} found.", entity));
						}
						break;
				}
				
				offset += length;
				
				return code;
			}
			
			return text[offset++];
		}
		
		public static Tag ParseTag(string text, ref int offset, out System.Collections.Hashtable parameters)
		{
			// Avance d'un caract�re ou d'un tag dans le texte.
			System.Diagnostics.Debug.Assert(text != null);
			parameters = null;
			
			if ( offset < text.Length && text[offset] == '<' )
			{
				int length = text.IndexOf(">", offset)-offset+1;

				if ( length <= 0 )
				{
					return Tag.SyntaxError;
				}
				else
				{
					string tag = text.Substring(offset, length);
					string tagLower = tag.ToLower();
					
					offset += length;
					
					switch ( tagLower )
					{
						case "<br/>":    return Tag.LineBreak;
						case "<b>":      return Tag.Bold;
						case "<i>":      return Tag.Italic;
						case "<u>":      return Tag.Underline;
						case "<m>":      return Tag.Mnemonic;
						
						case "</b>":     return Tag.BoldEnd;
						case "</i>":     return Tag.ItalicEnd;
						case "</u>":     return Tag.UnderlineEnd;
						case "</m>":     return Tag.MnemonicEnd;
						
						case "</a>":     return Tag.AnchorEnd;
						case "</font>":  return Tag.FontEnd;
					}
					
					int space = tag.IndexOf(" ");
					
					if ( space > 0 )
					{
						string pfx = tagLower.Substring(0, space);
						string end = tag.Remove(0, space).TrimStart(' ');
						Tag tagId = Tag.Unknown;
						
						string close = ">";
						
						switch ( pfx )
						{
							case "<a":     tagId = Tag.Anchor;  close = ">";   break;
							case "<img":   tagId = Tag.Image;   close = "/>";  break;
							case "<font":  tagId = Tag.Font;    close = ">";   break;
						}
						
						if ( !end.EndsWith(close) )
						{
							return Tag.SyntaxError;
						}
						
						// Enl�ve la fin du tag, comme �a on n'a r�ellement plus que les arguments.
						string arg = end.Remove(end.Length-close.Length, close.Length);
						parameters = new System.Collections.Hashtable();
						
						string argName;
						string argValue;
						int pos = 0;
						while ( pos < arg.Length )
						{
							int	i;

							while ( pos < arg.Length && arg[pos] == ' ' )  pos++;

							i = arg.IndexOf("=\"", pos);
							if ( i < 0 )  break;
							i -= pos;
							argName = arg.Substring(pos, i);
							pos += i+2;

							i = arg.IndexOf("\"", pos);
							if ( i < 0 )  break;
							i -= pos;
							argValue = arg.Substring(pos, i);
							pos += i+1;

							parameters[argName] = argValue;
						}
						return tagId;
					}
					
					return Tag.Unknown;
				}
			}
			
			if ( offset >= text.Length )
			{
				return Tag.Ending;
			}

			offset ++;
			return Tag.None;
		}
		
		public static char ExtractMnemonic(string text)
		{
			// Trouve la s�quence <m>x</m> dans le texte et retourne le premier caract�re
			// de x comme code mn�monique (en majuscules).
			System.Diagnostics.Debug.Assert(text != null);
			System.Collections.Hashtable parameters;

			int    offset = 0;
			while ( offset < text.Length )
			{
				TextLayout.Tag tag = TextLayout.ParseTag(text, ref offset, out parameters);
				if ( tag == TextLayout.Tag.Mnemonic )
				{
					return System.Char.ToUpper(text[offset]);
				}
			}
			
			return '\0';  // rien trouv�
		}
		
		public static string ConvertToTaggedText(string text)
		{
			return TextLayout.ConvertToTaggedText(text, false);
		}
		
		public static string ConvertToTaggedText(char c)
		{
			return TextLayout.ConvertToTaggedText(new string(c, 1), false);
		}
		
		public static string ConvertToTaggedText(string text, bool autoMnemonic)
		{
			// Convertit le texte simple en un texte compatible avec les tags. Supprime
			// toute occurrence de "<", "&" et ">" dans le texte.
			System.Diagnostics.Debug.Assert(text != null);
			if ( autoMnemonic )
			{
				// Cherche les occurrences de "&" dans le texte et g�re comme suit:
				// - Remplace "&x" par "<m>x</m>" (le tag <m> sp�cifie un code mn�monique)
				// - Remplace "&&" par "&"

				System.Text.StringBuilder buffer = new System.Text.StringBuilder();
				
				for ( int pos=0 ; pos<text.Length ; pos++ )
				{
					if ( text[pos] == '&' && pos < text.Length-1 )
					{
						if ( text[pos+1] == '&' )
						{
							buffer.Append("&amp;");
						}
						else
						{
							buffer.Append("<m>");
							buffer.Append(text[pos+1]);
							buffer.Append("</m>");
						}
						pos ++;
						continue;
					}
					
					switch ( text[pos] )
					{
						case '&':   buffer.Append("&amp;");    break;
						case '<':   buffer.Append("&lt;");     break;
						case '>':   buffer.Append("&gt;");     break;
						case '\"':  buffer.Append("&quot;");   break;
						case '\'':  buffer.Append("&apos;");   break;
						case '\n':  buffer.Append("<br/>");    break;
						case '\r':                             break;
						case '\t':  buffer.Append(" ");        break;
						default:    buffer.Append(text[pos]);  break;
					}
				}
				
				return buffer.ToString();
			}
			else
			{
				System.Text.StringBuilder buffer = new System.Text.StringBuilder();
				
				for ( int i=0 ; i<text.Length ; i++ )
				{
					char c = text[i];
					switch ( c )
					{
						case '&':   buffer.Append("&amp;");   break;
						case '<':   buffer.Append("&lt;");    break;
						case '>':   buffer.Append("&gt;");    break;
						case '\"':  buffer.Append("&quot;");  break;
						case '\'':  buffer.Append("&apos;");  break;
						case '\n':  buffer.Append("<br/>");   break;
						case '\r':                            break;
						case '\t':  buffer.Append(' ');       break;
						default:    buffer.Append(c);         break;
					}
				}
				
				return buffer.ToString();
			}
		}
		
		public static string ConvertToSimpleText(string text)
		{
			return TextLayout.ConvertToSimpleText(text, "");
		}
		
		public static string ConvertToSimpleText(string text, string imageReplacement)
		{
			// Epure le texte en supprimant les tags <> et en rempla�ant les
			// tags &gt; et &lt; (et autres) par leurs caract�res �quivalents.
			// En plus, les images sont remplac�es par le texte 'imageReplacement'
			System.Diagnostics.Debug.Assert(text != null);
			System.Text.StringBuilder buffer = new System.Text.StringBuilder();

			for ( int offset=0 ; offset<text.Length ; )
			{
				if ( text[offset] == '<' )
				{
					int length = text.IndexOf(">", offset)-offset+1;
					if ( length > 0 )
					{
						string tag = text.Substring(offset, length);
						string tagLower = tag.ToLower();
					
						offset += length;

						if ( tagLower.IndexOf("<img ") == 0 )
						{
							buffer.Append(imageReplacement);
						}
						if ( tagLower.IndexOf("<br") == 0 )
						{
							buffer.Append("\n");
						}
					}
				}
				else if ( text[offset] == '&' )
				{
					buffer.Append(TextLayout.AnalyseEntityChar(text, ref offset));
				}
				else
				{
					buffer.Append(text[offset++]);
				}
			}

			return buffer.ToString();
		}


		protected void UpdateLayout()
		{
			// Met � jour le layout si n�cessaire.
			if ( this.isDirty && this.layoutSize.Width > 0 )
			{
				this.JustifBlocks();
				this.JustifLines();
				this.isDirty = false;
			}
		}

		protected void JustifBlocks()
		{
			// Met � jour this.blocks en fonction du texte, de la fonte et des dimensions.
			System.Collections.Stack		fontStack;
			FontItem						fontItem;
			System.Text.StringBuilder		buffer;
			System.Collections.Hashtable	parameters;

			this.blocks.Clear();
			fontStack = new System.Collections.Stack();
			buffer = new System.Text.StringBuilder();
			bool stringExist = false;

			// Pr�pare la fonte initiale par d�faut.
			fontItem = new FontItem(this);
			fontItem.fontName  = this.font.FaceName;
			fontItem.fontSize  = this.fontSize;
			fontItem.fontColor = Drawing.Color.FromBrightness(0);  // noir
			fontItem.bold      = 0;
			fontItem.italic    = 0;
			fontItem.underline = 0;
			fontItem.anchor    = 0;

			fontStack.Push(fontItem);  // push la fonte initiale (jamais de pop)

			// Si le texte n'existe pas, met quand m�me un bloc vide,
			// afin de voir appara�tre le curseur (FindTextCursor).
			if ( this.textLength == 0 )
			{
				fontItem = (FontItem)fontStack.Peek();
				Drawing.Font blockFont = fontItem.RetFont();

				JustifBlock block = new JustifBlock();
				block.bol        = true;
				block.eol        = false;
				block.image      = false;
				block.text       = "";
				block.beginIndex = 0;
				block.endIndex   = 0;
				block.indexLine  = 0;
				block.font       = blockFont;
				block.fontSize   = fontItem.fontSize;
				block.fontColor  = fontItem.fontColor;
				block.underline  = false;
				block.anchor     = false;
				block.width      = 0;
				block.pos        = new Drawing.Point(0,0);
				block.visible    = false;
				this.blocks.Add(block);
				return;
			}

			double restWidth = this.layoutSize.Width;
			bool bol = true;

			int		beginOffset;
			int		endOffset = 0;
			int		index = 0;
			int		textIndex = 0;
			while ( endOffset <= this.textLength )
			{
				beginOffset = endOffset;
				TextLayout.Tag tag = TextLayout.ParseTag(this.text, ref endOffset, out parameters);

				if ( tag != TextLayout.Tag.None &&
					 (tag == TextLayout.Tag.LineBreak || stringExist) )
				{
					fontItem = (FontItem)fontStack.Peek();
					Drawing.Font blockFont = fontItem.RetFont();
					
					bool startNewLine = false;
					
					if ( buffer.Length == 0 )
					{
						if ( this.blocks.Count > 0 )
						{
							JustifBlock block = this.blocks[this.blocks.Count-1] as JustifBlock;
							
							if ( bol && block.beginIndex == block.endIndex )
							{
								// Ligne vide cr��e par un tag <BR/>. On peut l'utiliser sans
								// vergogne, � moins que ce ne soit � second <BR/> qui vient
								// d'�tre reconnu.
								if ( tag == TextLayout.Tag.LineBreak )
								{
									startNewLine = true;
								}
							}
							else
							{
								// Ligne vide, pas de </BR> imm�diatement avant. Commence donc
								// un nouveau bloc.
								startNewLine = true;
							}
						}
						else
						{
							// Ligne vide, la premi�re de la liste...
							startNewLine = true;
						}
					}
					
					if ( startNewLine )
					{
						JustifBlock block = new JustifBlock();
						block.bol        = bol;
						block.eol        = (tag == TextLayout.Tag.LineBreak);
						block.image      = false;
						block.text       = "";
						block.beginIndex = index;
						block.endIndex   = index;
						block.indexLine  = 0;
						block.font       = blockFont;
						block.fontSize   = fontItem.fontSize;
						block.fontColor  = fontItem.fontColor;
						block.underline  = fontItem.underline > 0;
						block.anchor     = fontItem.anchor > 0;
						block.width      = 0;
						block.pos        = new Drawing.Point(0,0);
						block.visible    = false;
						this.blocks.Add(block);
					}
					else
					{
						//Drawing.TextBreakMode mode = bol ? Drawing.TextBreakMode.Split : Drawing.TextBreakMode.None;
						Drawing.TextBreakMode mode = this.breakMode;
						
						if ( !bol )
						{
							mode &= ~Drawing.TextBreakMode.Split;
						}
						
						Drawing.TextBreak tb = new Drawing.TextBreak(blockFont, buffer.ToString(), fontItem.fontSize, mode);

						string breakText;
						double breakWidth;
						int    breakChars;
						
						while ( tb.GetNextBreak(restWidth, out breakText, out breakWidth, out breakChars) )
						{
							if ( breakWidth == 0 )  // pas la place ?
							{
								if ( restWidth == this.layoutSize.Width )  break;
								restWidth = this.layoutSize.Width;
								bol = true;
								continue;
							}

							JustifBlock block;
							JustifBlock previous = (this.blocks.Count < 1) ? null : this.blocks[this.blocks.Count-1] as JustifBlock;
							if ( previous != null      &&
								 previous.text == ""   &&
								 previous.eol == false )
							{
								//	R�-utilise le bloc pr�c�dent, puisqu'il est vide...
								block = previous;
							}
							else
							{
								block = new JustifBlock();
								this.blocks.Add(block);
							}
							block.bol        = bol;
							block.eol        = false;
							block.image      = false;
							block.text       = breakText;
							block.beginIndex = textIndex;
							block.endIndex   = textIndex+breakText.Length;
							block.indexLine  = 0;
							block.font       = blockFont;
							block.fontSize   = fontItem.fontSize;
							block.fontColor  = fontItem.fontColor;
							block.underline  = fontItem.underline > 0;
							block.anchor     = fontItem.anchor > 0;
							block.width      = breakWidth;
							block.pos        = new Drawing.Point(0,0);
							block.visible    = false;

							textIndex += breakChars;

							if ( tb.MoreText )  // reste encore du texte ?
							{
								restWidth = this.layoutSize.Width;
								bol = true;
							}
							else
							{
								restWidth -= breakWidth;
								bol = false;
							}
						}
					}

					buffer = new System.Text.StringBuilder();
					stringExist = false;
				}

				if ( tag == TextLayout.Tag.Ending )  break;

				switch ( tag )
				{
					case TextLayout.Tag.Font:
						if ( parameters != null )
						{
							fontItem = ((FontItem)fontStack.Peek()).Copy();

							if ( parameters.ContainsKey("face") )
							{
								fontItem.fontName = (string)parameters["face"];
							}
							if ( parameters.ContainsKey("size") )
							{
								string s = parameters["size"] as string;
								
								if ( s.EndsWith("%") )
								{
									fontItem.fontSize = System.Double.Parse(s.Substring(0, s.Length-1)) * this.fontSize / 100.0;
								}
								else
								{
									fontItem.fontSize = System.Double.Parse(s);
								}
							}
							if ( parameters.ContainsKey("color") )
							{
								string s = parameters["color"] as string;
								Drawing.Color color = Drawing.Color.FromName(s);
								if ( !color.IsEmpty )  fontItem.fontColor = color;
							}

							fontStack.Push(fontItem);
						}
						break;

					case TextLayout.Tag.FontEnd:
						if ( fontStack.Count > 1 )
						{
							fontStack.Pop();
						}
						break;

					case TextLayout.Tag.Bold:
						fontItem = (FontItem)fontStack.Peek();
						fontItem.bold ++;
						break;
					case TextLayout.Tag.BoldEnd:
						fontItem = (FontItem)fontStack.Peek();
						fontItem.bold --;
						break;

					case TextLayout.Tag.Italic:
						fontItem = (FontItem)fontStack.Peek();
						fontItem.italic ++;
						break;
					case TextLayout.Tag.ItalicEnd:
						fontItem = (FontItem)fontStack.Peek();
						fontItem.italic --;
						break;

					case TextLayout.Tag.Underline:
					case TextLayout.Tag.Mnemonic:
						fontItem = (FontItem)fontStack.Peek();
						fontItem.underline ++;
						break;
					case TextLayout.Tag.UnderlineEnd:
					case TextLayout.Tag.MnemonicEnd:
						fontItem = (FontItem)fontStack.Peek();
						fontItem.underline --;
						break;

					case TextLayout.Tag.Anchor:
						fontItem = (FontItem)fontStack.Peek();
						fontItem.anchor ++;
						fontItem.underline ++;
						break;
					case TextLayout.Tag.AnchorEnd:
						fontItem = (FontItem)fontStack.Peek();
						fontItem.anchor --;
						fontItem.underline --;
						break;

					case TextLayout.Tag.LineBreak:
						restWidth = this.layoutSize.Width;
						bol = true;
						stringExist = true;
						index ++;
						break;

					case TextLayout.Tag.Image:
						if ( parameters != null && this.imageProvider != null )
						{
							if ( parameters.ContainsKey("src") )
							{
								string imageName = parameters["src"] as string;
								Drawing.Image image = this.imageProvider.GetImage(imageName);
								
								if ( image == null )
								{
									throw new System.FormatException(string.Format("<img> tag references unknown image '{0}'.", imageName));
								}
								
								double dx = image.Width;
								double dy = image.Height;

								if ( dx > restWidth )
								{
									restWidth = this.layoutSize.Width;
									bol = true;
								}

								fontItem = (FontItem)fontStack.Peek();
								Drawing.Font blockFont = fontItem.RetFont();

								double fontAscender  = blockFont.Ascender;
								double fontDescender = blockFont.Descender;
								double fontHeight    = fontAscender-fontDescender;

								JustifBlock block = new JustifBlock();
								block.bol            = bol;
								block.image          = true;
								block.text           = imageName;
								block.beginIndex     = index;
								block.endIndex       = endOffset-beginOffset;
								block.indexLine      = 0;
								block.font           = blockFont;
								block.fontSize       = fontItem.fontSize;
								block.fontColor      = fontItem.fontColor;
								block.underline      = fontItem.underline > 0;
								block.anchor         = fontItem.anchor > 0;
								block.width          = dx;
								
								if (image.IsOriginDefined)
								{
									block.imageAscender  = image.Height - image.Origin.Y;
									block.imageDescender = -image.Origin.Y;
								}
								else
								{
									block.imageAscender  = dy*fontAscender/fontHeight;
									block.imageDescender = dy*fontDescender/fontHeight;
								}
								
								block.pos     = new Drawing.Point(0,0);
								block.visible = false;
								this.blocks.Add(block);

								restWidth -= dx;
								bol = false;
							}
						}
						break;

					case TextLayout.Tag.None:
						if ( buffer.Length == 0 )  textIndex = index;
						endOffset = beginOffset;
						char c = TextLayout.AnalyseEntityChar(this.text, ref endOffset);
						buffer.Append(c);
						stringExist = true;
						index ++;
						break;
				}
			}
		}

		protected void JustifLines()
		{
			// Met � jour this.lines en fonction de this.blocks.
			// D�termine la position des blocs en fonction de l'alignement.
			// D�termine �galement quels sont les blocs et les lignes visibles.
			this.lines.Clear();

			this.totalLine = 0;
			this.visibleLine = 0;

			Drawing.Point pos = new Drawing.Point(0,0);
			pos.Y += this.layoutSize.Height;  // coin sup/gauche

			JustifBlock	block;
			JustifLine	line;

			double totalHeight = 0;
			double overflow = 0;
			int i = 0;
			while ( i < this.blocks.Count )
			{
				// Avance tous les blocs de la ligne.
				double width = 0;
				double height = 0;
				double ascender = 0;
				double descender = 0;
				int j = i;
				while ( true )
				{
					block = (JustifBlock)this.blocks[j];
					block.indexLine = totalLine;
					width += block.width;
					if ( block.image )
					{
						height    = System.Math.Max(height,    block.imageAscender-block.imageDescender);
						ascender  = System.Math.Max(ascender,  block.imageAscender);
						descender = System.Math.Min(descender, block.imageDescender);
					}
					else
					{
						height    = System.Math.Max(height,    block.font.LineHeight*block.fontSize);
						ascender  = System.Math.Max(ascender,  block.font.Ascender  *block.fontSize);
						descender = System.Math.Min(descender, block.font.Descender *block.fontSize);
					}

					j ++;
					if ( j >= this.blocks.Count )  break;
					block = (JustifBlock)this.blocks[j];
					if ( block.bol )  break;  // break si d�but nouvelle ligne
				}

				bool visible;
				this.totalLine ++;
//?				if ( pos.Y-ascender+descender >= 0 )
				if ( pos.Y-ascender+descender >= 0 || this.totalLine == 1 )
				{
					visible = true;
					this.visibleLine ++;
					totalHeight += height;
					overflow = height-(ascender-descender);
				}
				else
				{
					visible = false;
				}

				switch ( this.alignment )
				{
					case Drawing.ContentAlignment.TopLeft:
					case Drawing.ContentAlignment.MiddleLeft:
					case Drawing.ContentAlignment.BottomLeft:
						pos.X = 0;
						break;
				
					case Drawing.ContentAlignment.TopCenter:
					case Drawing.ContentAlignment.MiddleCenter:
					case Drawing.ContentAlignment.BottomCenter:
						pos.X = (this.layoutSize.Width-width)/2;
						break;
				
					case Drawing.ContentAlignment.TopRight:
					case Drawing.ContentAlignment.MiddleRight:
					case Drawing.ContentAlignment.BottomRight:
						pos.X = this.layoutSize.Width-width;
						break;
				}
				pos.Y -= ascender;  // sur la ligne de base

				line = new JustifLine();
				line.firstBlock = i;
				line.lastBlock  = j-1;
				line.rank       = this.totalLine-1;
				line.pos        = pos;
				line.width      = width;
				line.height     = height;
				line.ascender   = ascender;
				line.descender  = descender;
				line.visible    = visible;
				this.lines.Add(line);

				for ( int k=i ; k<j ; k++ )
				{
					block = (JustifBlock)this.blocks[k];
					block.pos = pos;
					block.visible = visible;
					pos.X += block.width;
				}

				pos.Y += ascender;
				pos.Y -= height;  // position haut de la ligne suivante

				i = j;  // index d�but ligne suivante
			}

			// Effectue l'alignement vertical.
			totalHeight -= overflow;
			double offset = 0;
			switch ( this.alignment )
			{
				case Drawing.ContentAlignment.TopLeft:
				case Drawing.ContentAlignment.TopCenter:
				case Drawing.ContentAlignment.TopRight:
					offset = 0;
					break;

				case Drawing.ContentAlignment.MiddleLeft:
				case Drawing.ContentAlignment.MiddleCenter:
				case Drawing.ContentAlignment.MiddleRight:
					offset = (this.layoutSize.Height-totalHeight)/2;
					break;
				
				case Drawing.ContentAlignment.BottomLeft:
				case Drawing.ContentAlignment.BottomCenter:
				case Drawing.ContentAlignment.BottomRight:
					offset = this.layoutSize.Height-totalHeight;
					break;
			}
			if ( offset != 0 )  // alignement Middle* ou Bottom* ?
			{
				for ( i=0 ; i<this.blocks.Count ; i++ )
				{
					block = (JustifBlock)this.blocks[i];
					block.pos.Y -= offset;  // descend le bloc
				}
				for ( i=0 ; i<this.lines.Count ; i++ )
				{
					line = (JustifLine)this.lines[i];
					line.pos.Y -= offset;  // descend la ligne
				}
			}
		}

		
		public void DebugDumpJustif(System.IO.TextWriter stream)
		{
			// Affiche le contenu du tableau this.blocks, pour le debug.
			stream.WriteLine("Total blocks = " + this.blocks.Count);
			foreach ( JustifBlock block in this.blocks )
			{
				string bol = block.bol ? "BOL: " : "";
				
				stream.WriteLine(bol + block.font.FullName + " " + block.fontSize + "     " + "pos=" + block.pos.X + ";" + block.pos.Y + " width=" + block.width + "     " + "\"" + block.text + "\"");
			}
		}


		public static bool CheckSyntax(string text, out int offsetError)
		{
			// V�rifie la syntaxe d'un texte.
			System.Collections.Hashtable parameters;
			System.Collections.ArrayList list = new System.Collections.ArrayList();
			int    beginOffset;
			for ( int endOffset=0 ; endOffset<text.Length ; )
			{
				beginOffset = endOffset;

				if ( text[endOffset] == '&' )
				{
					int length = text.IndexOf(";", endOffset)-endOffset+1;
					if ( length < 3 )
					{
						offsetError = beginOffset;
						return false;
					}
				
					string entity = text.Substring(endOffset, length).ToLower();
					switch ( entity )
					{
						case "&amp;":   break;
						case "&lt;":    break;
						case "&gt;":    break;
						case "&quot;":  break;
						case "&apos;":  break;
					
						default:
							if ( entity.StartsWith("&#") )
							{
								for ( int i=2 ; i<entity.Length-1 ; i++ )
								{
									char c = entity[i];
									
									if ( c < '0' || c > '9' )
									{
										offsetError = beginOffset;
										return false;
									}
								}
							}
							else
							{
								offsetError = beginOffset;
								return false;
							}
							break;
					}
				
					endOffset += length;
					continue;
				}

				TextLayout.Tag tag = TextLayout.ParseTag(text, ref endOffset, out parameters);
				if ( tag == TextLayout.Tag.None )  continue;

				if ( tag == TextLayout.Tag.SyntaxError ||
					 tag == TextLayout.Tag.Unknown     )
				{
					offsetError = beginOffset;
					return false;
				}

				string sTag = text.Substring(beginOffset, endOffset-beginOffset);

				switch ( tag )
				{
					case TextLayout.Tag.Bold:
					case TextLayout.Tag.Italic:
					case TextLayout.Tag.Underline:
					case TextLayout.Tag.Mnemonic:
					case TextLayout.Tag.Font:
					case TextLayout.Tag.Anchor:
						list.Add(sTag);
						break;

					case TextLayout.Tag.BoldEnd:
					case TextLayout.Tag.ItalicEnd:
					case TextLayout.Tag.UnderlineEnd:
					case TextLayout.Tag.MnemonicEnd:
					case TextLayout.Tag.FontEnd:
					case TextLayout.Tag.AnchorEnd:
						if ( !TextLayout.DeleteTagsList(sTag, list) )
						{
							offsetError = beginOffset;
							return false;
						}
						break;
				}
			}

			if ( list.Count != 0 )
			{
				offsetError = text.Length;
				return false;
			}

			offsetError = -1;
			return true;
		}
		

		// Tous les tags possibles.
		public enum Tag
		{
			None,							// pas un tag
			Unknown,						// tag pas reconnu
			SyntaxError,					// syntaxe du tag pas correcte
			Ending,							// fin du texte
			
			LineBreak,						// <br/>
			Bold,		BoldEnd,			// <b>...</b>
			Italic,		ItalicEnd,			// <i>...</i>
			Underline,	UnderlineEnd,		// <u>...</u>
			Mnemonic,	MnemonicEnd,		// <m>...</m>  --> comme <u>...</u>
			Font,		FontEnd,			// <font ...>...</font>
			Anchor,		AnchorEnd,			// <a href="x">...</a>
			Image,							// <img src="x"/>
		}
		
		
		// Fonte servant � refl�ter les commandes HTML rencontr�es.
		// Un stack de FontItem est cr��.
		protected class FontItem
		{
			public FontItem(TextLayout host)
			{
				this.host = host;
			}
			
			public FontItem Copy()
			{
				return this.MemberwiseClone() as FontItem;
			}

			public Drawing.Font RetFont()
			{
				Drawing.Font	font;
				string			fontStyle;

				if ( this.bold > 0 && this.italic == 0 )
				{
					fontStyle = "Bold";
				}
				else if ( this.bold == 0 && this.italic > 0 )
				{
					fontStyle = "Italic";
				}
				else if ( this.bold > 0 && this.italic > 0 )
				{
					fontStyle = "Bold Italic";
				}
				else
				{
					fontStyle = "Regular";
				}

				font = Drawing.Font.GetFont(this.fontName, fontStyle);
				
				if ( font == null )
				{
					font = Drawing.Font.GetFont(this.fontName, "Regular");
				}
				
				if ( font == null )
				{
					font = this.host.font;
				}
				
				return font;
			}

			
			protected TextLayout				host;
			
			public string						fontName;
			public double						fontSize;
			public Drawing.Color				fontColor;
			public int							bold;		// gras si > 0
			public int							italic;		// italique si > 0
			public int							underline;	// soulign� si > 0
			public int							anchor;		// lien si > 0
		}

		// Descripteur d'un bloc de texte. Tous les caract�res du bloc ont
		// la m�me fonte, m�me taille et m�me couleur.
		protected class JustifBlock
		{
			public bool							bol;		// begin of line
			public bool							eol;		// line ends with <br/>
			public bool							image;		// image bitmap
			public string						text;
			public int							beginIndex;
			public int							endIndex;
			public int							indexLine;	// index dans this.lines
			public Drawing.Font					font;
			public double						fontSize;
			public Drawing.Color				fontColor;
			public bool							underline;
			public bool							anchor;
			public double						width;		// largeur du bloc
			public double						imageAscender;
			public double						imageDescender;
			public Drawing.Point				pos;		// sur la ligne de base
			public bool							visible;
		}

		// Descripteur d'une ligne de texte. Une ligne est compos�e
		// d'un ou plusieurs blocs.
		protected class JustifLine
		{
			public int							firstBlock;	// index du premier bloc
			public int							lastBlock;	// index du dernier bloc
			public int							rank;		// rang de la ligne (0..n)
			public Drawing.Point				pos;		// position sur la ligne de base
			public double						width;		// largeur occup�e par la ligne
			public double						height;		// interligne
			public double						ascender;	// hauteur en-dessus de la ligne de base (+)
			public double						descender;	// hauteur en-dessous de la ligne de base (-)
			public bool							visible;
		}

		public class Context
		{
			public int							CursorFrom = 0;
			public int							CursorTo   = 0;
			public int							CursorLine = 0;
			public double						CursorPosX = 0;
			
			public int							MaxChar = 1000;
		}
		
		public event AnchorEventHandler			Anchor;

		
		
		protected bool							isDirty;
		protected string						text;
		protected int							textLength;
		protected Drawing.Font					font			= Drawing.Font.DefaultFont;
		protected double						fontSize		= Drawing.Font.DefaultFontSize;
		protected Drawing.Size					layoutSize;
		protected Drawing.TextBreakMode			breakMode		= Drawing.TextBreakMode.Split;
		protected int							totalLine;
		protected int							visibleLine;
		protected Drawing.IImageProvider		imageProvider	= Support.ImageProvider.Default;
		protected Drawing.ContentAlignment		alignment		= Drawing.ContentAlignment.TopLeft;
		protected System.Collections.ArrayList	blocks			= new System.Collections.ArrayList();
		protected System.Collections.ArrayList	lines			= new System.Collections.ArrayList();
		protected static Drawing.Color			anchorColor		= new Drawing.Color(0,0,1);
		
		public const double						Infinite		= 1000000;
	}
}

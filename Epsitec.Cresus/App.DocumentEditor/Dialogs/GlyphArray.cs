using Epsitec.Common.Drawing;
using Epsitec.Common.Text;
using Epsitec.Common.OpenType;

namespace Epsitec.Common.Widgets
{
	/// <summary>
	/// La classe GlyphArray permet de choisir le glyphe Unicode d'une police
	/// dans un tableau avec un ascenseur vertical. Afin d'�viter de cr�er un
	/// grand nombre de widgets, les diff�rentes cellules ne sont pas programm�es
	/// avec des instances de widgets, mais directement dessin�es ici.
	/// </summary>
	public class GlyphArray : Widget
	{
		public GlyphArray()
		{
			this.AutoFocus  = true;
			this.AutoEngage = true;
			this.AutoDoubleClick = true;
			
			this.InternalState |= InternalState.Focusable;
			this.InternalState |= InternalState.Engageable;
			
			this.scroller = new VScroller(this);
			this.scroller.MinValue          = 0.0M;
			this.scroller.MaxValue          = 1.0M;
			this.scroller.VisibleRangeRatio = 0.1M;
			this.scroller.IsInverted        = true;
			this.scroller.ValueChanged += new Support.EventHandler(this.HandleScrollerValueChanged);
		}
		
		public GlyphArray(Widget embedder) : this()
		{
			this.SetEmbedder(embedder);
		}
		
		
		protected override void Dispose(bool disposing)
		{
			if ( disposing )
			{
				this.scroller.ValueChanged -= new Support.EventHandler(this.HandleScrollerValueChanged);
				this.scroller.Dispose();
				this.scroller = null;
			}
			
			base.Dispose(disposing);
		}


		// Taille d'une cellule. GlyphArray s'arrange pour que les cellules
		// soient � peu pr�s carr�es. La taille d'une cellule d�termine le
		// nombre de cellules qu'il sera possible de placer horizontalement
		// et verticalement.
		public double CellSize
		{
			get
			{
				return this.cellSize;
			}

			set
			{
				if ( this.cellSize != value )
				{
					this.cellSize = value;
					this.Invalidate();
				}
			}
		}

		// Choix de la fonte.
		public void SetFont(string fontFace, string fontStyle)
		{
			if ( this.fontFace != fontFace || this.fontStyle != fontStyle )
			{
				this.fontFace = fontFace;
				this.fontStyle = fontStyle;
				this.UpdateUnicodes();
				this.scroller.Value = 0.0M;
				this.Invalidate();
			}
		}

		// Choix d'une liste de glyphes (caract�res alternatifs).
		public void SetGlyphAlternates(OpenType.Font font, int code, int glyph, ushort[] alternates)
		{
			this.code = code;
			this.glyphsMode = true;

			if ( font == null )
			{
				this.fontFace = "";
				this.fontStyle = "";
				this.glyphs = new int[0];  // alloue le tableau
				this.SelectedIndex = -1;
			}
			else
			{
				this.fontFace  = font.FontIdentity.InvariantFaceName;
				this.fontStyle = font.FontIdentity.InvariantStyleName;

				System.Collections.ArrayList list = new System.Collections.ArrayList();

				int normal = font.GetGlyphIndex(code);
				list.Add(normal);
				int sel = 0;

				if ( alternates != null )
				{
					for ( int i=0 ; i<alternates.Length ; i++ )
					{
						int a = (int) alternates[i];
						if ( !list.Contains(a) )
						{
							list.Add(a);
							if ( a == glyph )
							{
								sel = list.Count-1;
							}
						}
					}
				}

				this.glyphs = (int[]) list.ToArray(typeof(int));

				this.SelectedIndex = sel;
			}

			this.scroller.Value = 0.0M;
			this.Invalidate();
		}

		// Nom de la fonte.
		public string FontFace
		{
			get
			{
				return this.fontFace;
			}
		}

		// Nom du style de la fonte.
		public string FontStyle
		{
			get
			{
				return this.fontStyle;
			}
		}

		// Code unicode du glyph.
		public int Code
		{
			get
			{
				return this.code;
			}
		}

		// Glyph s�lectionn� � ins�rer.
		public int SelectedGlyph
		{
			get
			{
				System.Diagnostics.Debug.Assert(this.glyphsMode);
				if ( this.selectedIndex == -1 )  return 0;
				return this.glyphs[this.selectedIndex];
			}
		}


		// Choix de l'index du glyphe s�lectionn�.
		public int SelectedIndex
		{
			get
			{
				return this.selectedIndex;
			}

			set
			{
				if ( this.selectedIndex != value )
				{
					this.selectedIndex = value;
					this.OnChangeSelected();
					this.Invalidate();
				}
			}
		}

		// Retourne le caract�re Unicode correspondant � un index.
		public int IndexToUnicode(int index)
		{
			System.Diagnostics.Debug.Assert(!this.glyphsMode);
			if ( index < 0 || index >= this.unicodes.Length )  return 0;
			return this.unicodes[index];
		}

		// Retourne l'index correspondant � un caract�re Unicode.
		public int UnicodeToIndex(int code)
		{
			System.Diagnostics.Debug.Assert(!this.glyphsMode);
			for ( int i=0 ; i<this.unicodes.Length ; i++ )
			{
				if ( code == this.unicodes[i] )  return i;
			}
			return -1;
		}


		// Met � jour la liste des glyphes Unicode en fonction de la fonte.
		protected void UpdateUnicodes()
		{
			this.unicodes = null;

			Drawing.Font font = GlyphArray.GetFont(this.fontFace, this.fontStyle);
			if ( font == null )  return;

			bool isSymbol = false;
			OpenType.Font otFont = TextContext.GetFont(this.fontFace, this.fontStyle);
			if ( otFont != null )
			{
				isSymbol = otFont.FontIdentity.IsSymbolFont;
			}

			int total = 0;
			int ii = 0;
			for ( int pass=0 ; pass<2 ; pass++ )
			{
				int first = isSymbol ? 0xF000 : 0x0020;
				int last  = isSymbol ? 0xF0FF : 0xFB06;

				for ( int i=first ; i<=last ; i++ )
				{
					if ( !isSymbol )
					{
						if ( i >= 0x0400 && i <= 0x04FF )  continue;  // cyrillique ?
						if ( i >= 0x0500 && i <= 0x05FF )  continue;  // aram�nien et h�breu ?
						if ( i >= 0x0600 && i <= 0x06FF )  continue;  // arabe ?
						if ( i >= 0x0900 && i <= 0x09FF )  continue;  // bengali ?
						if ( i >= 0x0A00 && i <= 0x0AFF )  continue;  // pendjabi ?
						if ( i >= 0x0B00 && i <= 0x0BFF )  continue;  // tamoul ?
						if ( i >= 0x0C00 && i <= 0x0CFF )  continue;  // t�lougou ?
						if ( i >= 0x0D00 && i <= 0x0DFF )  continue;  // malayalam ?
						if ( i >= 0x0E00 && i <= 0x0EFF )  continue;  // tha� ?
						if ( i >= 0x1000 && i <= 0x10FF )  continue;  // g�orgien ?
						if ( i >= 0x1100 && i <= 0x11FF )  continue;  // jamos ?
						if ( i >= 0x3000 && i <= 0x30FF )  continue;  // katakana ?
						if ( i >= 0x3100 && i <= 0x31FF )  continue;  // bopomofo ?
						if ( i >= 0xE000 && i <= 0xF0FF )  continue;  // r�serv� ?
					}

					int glyph = font.GetGlyphIndex(i);
					if ( glyph != 0 )
					{
						if ( pass == 0 )  // 1�re passe ?
						{
							total ++;  // compte le nombre de glyphes existants
						}
						if ( pass == 1 )  // 2�me passe ?
						{
							this.unicodes[ii++] = i;  // remplit le tableau
						}
					}
				}

				if ( pass == 0 )  // fin de la 1�re passe ?
				{
					this.unicodes = new int[total];  // alloue le tableau
				}
			}
			this.glyphsMode = false;
		}


		// Met � jour la g�om�trie de l'ascenseur.
		protected override void UpdateClientGeometry()
		{
			base.UpdateClientGeometry();

			if ( this.scroller != null )
			{
				this.scroller.Bounds = new Rectangle(this.Client.Width-this.scroller.Width, 0, this.scroller.Width, this.Client.Height);
			}
		}
		
		
		// Appel� lorsque l'ascenseur est d�plac�.
		private void HandleScrollerValueChanged(object sender)
		{
			System.Diagnostics.Debug.Assert(this.scroller == sender);
			this.Invalidate();  // redessine le tableau de glyphes
		}
		
		
		// Gestion des �v�nements.
		protected override void ProcessMessage(Message message, Point pos)
		{
			if ( !this.IsEnabled )  return;

			switch ( message.Type )
			{
				case MessageType.MouseDown:
					this.mouseDown = true;
					this.SelectedIndex = this.Detect(pos);
					break;
				
				case MessageType.MouseMove:
					if ( this.mouseDown )
					{
						this.SelectedIndex = this.Detect(pos);
					}
					break;

				case MessageType.MouseUp:
					this.mouseDown = false;
					break;

				case MessageType.MouseWheel:
					if ( message.Wheel < 0 )  this.scroller.Value += this.scroller.SmallChange;
					if ( message.Wheel > 0 )  this.scroller.Value -= this.scroller.SmallChange;
					break;

				case MessageType.KeyDown:
					if ( !this.ProcessKeyEvent(message) )
					{
						return;
					}
					break;
				
				default:
					return;
			}
			
			message.Consumer = this;
		}

		// Gestion d'une touche clavier press�e.
		protected virtual bool ProcessKeyEvent(Message message)
		{
			switch ( message.KeyCode )
			{
				case KeyCode.ArrowLeft:   return this.MoveSelectedCell(-1);
				case KeyCode.ArrowRight:  return this.MoveSelectedCell( 1);
				case KeyCode.ArrowUp:     return this.MoveSelectedCell(-2);
				case KeyCode.ArrowDown:   return this.MoveSelectedCell( 2);

				default:
					return false;
			}
		}

		// D�place la cellule s�lectionn�e.
		protected bool MoveSelectedCell(int move)
		{
			int sel = this.selectedIndex;
			if ( sel == -1 )  return false;

			int dx = this.TotalCellVisibleX();

			switch ( move )
			{
				case -1:  sel --;     break;
				case  1:  sel ++;     break;
				case -2:  sel -= dx;  break;
				case  2:  sel += dx;  break;

				default:
					return false;
			}

			sel = System.Math.Max(sel, 0);
			sel = System.Math.Min(sel, this.TotalCell-1);
			this.SelectedIndex = sel;
			this.ShowSelectedCell();

			return true;
		}

		// Si n�cessaire, bouge l'ascenseur pour montrer la cellule s�lectionn�e.
		public void ShowSelectedCell()
		{
			int sel = this.selectedIndex;
			if ( sel == -1 )  return;

			int dx = this.TotalCellVisibleX();
			int dy = this.TotalCellVisibleY();

			for ( int i=0 ; i<1000 ; i++ )
			{
				int first = this.First();

				if ( sel < first )
				{
					this.scroller.Value -= this.scroller.SmallChange;
					continue;
				}

				if ( sel >= first+dx*dy )
				{
					this.scroller.Value += this.scroller.SmallChange;
					continue;
				}

				break;
			}
		}

		// D�tection du glyphe vis�.
		protected int Detect(Point pos)
		{
			Rectangle area = this.DrawingArea();
			if ( !area.Contains(pos) )  return -1;

			int dx = this.TotalCellVisibleX();
			int dy = this.TotalCellVisibleY();

			int x = (int) ((pos.X-area.Left)/this.CellWidth());
			int y = (int) ((pos.Y-area.Bottom)/this.CellHeight());
			y = dy-y-1;

			int cell = this.First() + dx*y + x;
			if ( cell >= this.TotalCell )  return -1;
			return cell;
		}


		// Peinture du widget.
		protected override void PaintForegroundImplementation(Graphics graphics, Rectangle clipRect)
		{
			IAdorner    adorner = Widgets.Adorners.Factory.Active;
			WidgetState state   = this.PaintState;
			
			Rectangle area = this.DrawingArea();
			int dx = this.TotalCellVisibleX();
			int dy = this.TotalCellVisibleY();
			double cellWidth  = this.CellWidth();
			double cellHeight = this.CellHeight();

			// Dessine les glyphes.
			if ( this.unicodes != null || this.glyphs != null )
			{
				Drawing.Font font = GlyphArray.GetFont(this.fontFace, this.fontStyle);
				double fontSize = this.cellSize*0.6;
				int first = this.First();

				for ( int y=0 ; y<dy ; y++ )
				{
					for ( int x=0 ; x<dx ; x++ )
					{
						Rectangle rect = new Rectangle();

						rect.Left = area.Left + cellWidth*x;
						rect.Width = cellWidth;

						rect.Bottom = area.Top - cellHeight*(y+1);
						rect.Height = cellHeight;

						WidgetState cellState = WidgetState.Enabled;
						if ( first == this.selectedIndex && first < this.TotalCell )
						{
							TextLayout.SelectedArea[] areas = new TextLayout.SelectedArea[1];
							areas[0] = new TextLayout.SelectedArea();
							areas[0].Rect = rect;
							adorner.PaintTextSelectionBackground(graphics, areas, state);

							cellState |= WidgetState.Selected;
						}

						if ( font != null && first < this.TotalCell )
						{
							if ( this.glyphsMode )
							{
								int glyph = (int) this.glyphs[first++];
								double width = font.GetGlyphAdvance(glyph)*fontSize;
								graphics.Rasterizer.AddGlyph(font, glyph, rect.Center.X-width/2.0, rect.Bottom+rect.Height*0.35, fontSize);
								graphics.RenderSolid(adorner.ColorText(cellState));
							}
							else
							{
								char c = (char) this.unicodes[first++];
								int glyph = font.GetGlyphIndex(c);
								double width = font.GetGlyphAdvance(glyph)*fontSize;
								graphics.AddText(rect.Center.X-width/2.0, rect.Bottom+rect.Height*0.35, c.ToString(), font, fontSize);
								graphics.RenderSolid(adorner.ColorText(cellState));
							}
						}
						else
						{
							Rectangle inside = rect;
							inside.Deflate(0.5);
							graphics.AddLine(inside.BottomLeft, inside.TopRight);
							graphics.AddLine(inside.TopLeft, inside.BottomRight);
							graphics.RenderSolid(adorner.ColorBorder);  // x
						}
					}
				}
			}

			// Dessine le quadrillage et le cadre.
			for ( int x=1 ; x<dx ; x++ )
			{
				double posx = System.Math.Floor(area.Left + cellWidth*x) + 0.5;
				graphics.AddLine(posx, area.Bottom+1.0, posx, area.Top-1.0);
			}

			for ( int y=1 ; y<dy ; y++ )
			{
				double posy = System.Math.Floor(area.Bottom + cellHeight*y) + 0.5;
				graphics.AddLine(area.Left+1.0, posy, area.Right-1.0, posy);
			}

			area.Deflate(0.5);
			graphics.AddRectangle(area);

			graphics.RenderSolid(adorner.ColorBorder);
		}


		// En fonction de la position de l'ascenseur (0..1), retourne le
		// premier glyphe visible.
		protected int First()
		{
			if ( this.unicodes == null && this.glyphs == null )  return 0;

			int dx = this.TotalCellVisibleX();
			int dy = this.TotalCellVisibleY();

			int total = (this.TotalCell+dx-1)/dx*dx;
			int max = total-dx*dy;

			if ( max <= 0 )
			{
				this.scroller.SetEnabled(false);
				return 0;
			}
			else
			{
				this.scroller.SetEnabled(true);
				this.scroller.SmallChange = (decimal) ((double)dx/max);
				this.scroller.LargeChange = (decimal) ((double)dx*dy/max);
				this.scroller.VisibleRangeRatio = (decimal) ((double)dx*dy/total);

				int first = (int) (this.scroller.DoubleValue*max);
				return (first+dx/2)/dx*dx;
			}
		}

		// Largeur d'une cellule.
		protected double CellWidth()
		{
			Rectangle rect = this.DrawingArea();
			return rect.Width/this.TotalCellVisibleX();
		}

		// Hauteur d'une cellule.
		protected double CellHeight()
		{
			Rectangle rect = this.DrawingArea();
			return rect.Height/this.TotalCellVisibleY();
		}

		// Nombre de cellules visibles horizontalement.
		protected int TotalCellVisibleX()
		{
			Rectangle rect = this.DrawingArea();
			int total = (int) (rect.Width/this.cellSize);
			if ( total == 0 )  total++;
			return total;
		}

		// Nombre de cellules visibles verticalement.
		protected int TotalCellVisibleY()
		{
			Rectangle rect = this.DrawingArea();
			int total = (int) (rect.Height/this.cellSize);
			if ( total == 0 )  total++;
			return total;
		}

		// Rectangle o� dessiner les cellules.
		protected Rectangle DrawingArea()
		{
			Rectangle rect = this.Client.Bounds;
			rect.Right -= this.scroller.Width+1.0;
			return rect;
		}


		// Retourne le nombre total de cases.
		protected int TotalCell
		{
			get
			{
				if ( this.glyphsMode )  return this.glyphs.Length;
				else                    return this.unicodes.Length;
			}
		}


		// Donne une fonte d'apr�s son nom.
		protected static Drawing.Font GetFont(string fontFace, string fontStyle)
		{
			Drawing.Font font = Drawing.Font.GetFont(fontFace, fontStyle);

			if ( font == null )
			{
				font = Drawing.Font.GetFontFallback(fontFace);
			}
			
			return font;
		}


		// Appel� lorsque le glyphe s�lectionn� change.
		protected virtual void OnChangeSelected()
		{
			if ( this.ChangeSelected != null )
			{
				this.ChangeSelected(this);
			}
		}

		public event Support.EventHandler		ChangeSelected;
		
		protected double						cellSize = 25;
		protected VScroller						scroller;
		protected string						fontFace;
		protected string						fontStyle;
		protected int							selectedIndex = -1;
		protected int[]							unicodes;
		protected int[]							glyphs;
		protected bool							glyphsMode = false;
		protected int							code;
		protected bool							mouseDown = false;
	}
}

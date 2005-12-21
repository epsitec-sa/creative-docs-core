using Epsitec.Common.Widgets;
using Epsitec.Common.Drawing;
using Epsitec.Common.Support;
using Epsitec.Common.Text;

namespace Epsitec.Common.Document.Widgets
{
	public struct Tab
	{
		public string			Tag;
		public double			Pos;
		public TextTabType		Type;
		public bool				Shared;
		public bool				Zombie;
	}

	/// <summary>
	/// La classe HRuler impl�mente la r�gle horizontale.
	/// </summary>
	public class HRuler : AbstractRuler
	{
		public HRuler() : base(false)
		{
			this.invalidTab.Tag = null;
			this.invalidTab.Pos = 0;
			this.invalidTab.Type = TextTabType.None;
			this.invalidTab.Zombie = false;
		}
		
		public HRuler(Widget embedder) : this()
		{
			this.SetEmbedder(embedder);
		}
		
		
		public override double DefaultHeight
		{
			get
			{
				return AbstractRuler.defaultBreadth;
			}
		}


		public override void WrappersAttach()
		{
			//	Attache la r�gle aux wrappers.
			this.document.TextWrapper.Active.Changed  += new EventHandler(this.HandleWrapperChanged);
			this.document.ParagraphWrapper.Active.Changed += new EventHandler(this.HandleWrapperChanged);
//-			this.document.TextWrapper.Defined.Changed += new EventHandler(this.HandleWrapperChanged);
		}

		public override void WrappersDetach()
		{
			//	D�tache la r�gle des wrappers.
			this.document.TextWrapper.Active.Changed  -= new EventHandler(this.HandleWrapperChanged);
			this.document.ParagraphWrapper.Active.Changed -= new EventHandler(this.HandleWrapperChanged);
//-			this.document.TextWrapper.Defined.Changed -= new EventHandler(this.HandleWrapperChanged);
		}

		protected void HandleWrapperChanged(object sender)
		{
			//	Le wrapper associ� a chang�.
			if ( this.editObject == null )  return;

			double leftFirst, leftBody, right;
			this.GetMargins(out leftFirst, out leftBody, out right);

			Drawing.Rectangle bbox = this.editObject.BoundingBoxThin;
			leftFirst = bbox.Left  + leftFirst;
			leftBody  = bbox.Left  + leftBody;
			right     = bbox.Right - right;

			if ( this.marginLeftFirst != leftFirst )
			{
				this.marginLeftFirst = leftFirst;
				this.Invalidate();
			}

			if ( this.marginLeftBody != leftBody )
			{
				this.marginLeftBody = leftBody;
				this.Invalidate();
			}

			if ( this.marginRight != right )
			{
				this.marginRight = right;
				this.Invalidate();
			}
		}

		protected void GetMargins(out double leftFirst, out double leftBody, out double right)
		{
			leftFirst = this.document.ParagraphWrapper.Active.LeftMarginFirst;
			leftBody  = this.document.ParagraphWrapper.Active.LeftMarginBody;
			right     = this.document.ParagraphWrapper.Active.RightMarginBody;
		}

		
		public Tab[] Tabs
		{
			//	Liste des tabulateurs.
			get
			{
				return this.tabs;
			}

			set
			{
				if ( this.isDragging || !HRuler.TabCompare(this.tabs, value) )
				{
					this.tabs = value;
					this.Invalidate();
				}
			}
		}


		protected override void InvalidateBoxMarker()
		{
			//	Invalide la zone contenant le marqueur.
			if ( !this.markerVisible )  return;

			Rectangle rect = this.Client.Bounds;
			double posx = this.DocumentToScreen(this.marker);
			rect.Left  = posx-4;
			rect.Right = posx+4;

			if ( rect.IntersectsWith(this.Client.Bounds) )
			{
				this.invalidateBox.MergeWith(rect);
			}
		}


		protected void PaintGrad(Graphics graphics, Rectangle clipRect)
		{
			//	Dessine la graduation.
			IAdorner adorner = Common.Widgets.Adorners.Factory.Active;

			Rectangle rect = this.Client.Bounds;
			graphics.Align(ref rect);

			graphics.Color = adorner.ColorTextFieldBorder(this.IsEnabled);
			Font font = Font.GetFont("Tahoma", "Regular");

			double space = 3.0;
			double mul = 1.0;
			if ( this.ppm == 254.0 )
			{
				space = 1.0;
				mul = 2.54;
			}

			double scale = (this.ending-this.starting)/rect.Width;
			double step = System.Math.Pow(10.0, System.Math.Ceiling(System.Math.Log(scale*space, 10.0)))*mul;
			double grad = System.Math.Floor(this.starting/step)*step;

			graphics.SolidRenderer.Color = adorner.ColorText(this.PaintState);
			while ( grad < this.ending )
			{
				double posx = (grad-this.starting)/scale;
				int rank = (int) (System.Math.Floor(grad/step+0.5));

				if ( posx >= clipRect.Left-1.0  &&
					 posx <= clipRect.Right+1.0 )
				{
					double h = rect.Height;
					if ( rank%10 == 0 )  h *= 1.0;
					else if ( rank% 5 == 0 )  h *= 0.4;
					else                      h *= 0.2;
					graphics.AddLine(posx, 0, posx, h);
				}

				if ( rank%10 == 0 && posx <= clipRect.Right )
				{
					double value = grad/this.ppm;
					value *= 1000000.0;
					value = System.Math.Floor(value+0.5);  // arrondi � la 6�me d�cimale
					value /= 1000000.0;
					string text = value.ToString();

					double size = rect.Height*0.6;
					Rectangle bounds = font.GetTextBounds(text);
					bounds.Scale(size);
					bounds.Offset(posx+2, 0);

					if ( bounds.IntersectsWith(clipRect) )
					{
						graphics.PaintText(posx+2, rect.Top-size, text, font, size);
					}
				}
				
				grad += step;
			}
			graphics.RenderSolid(adorner.ColorText(this.PaintState));
		}

		protected void PaintMarker(Graphics graphics)
		{
			//	Dessine le marqueur de position de la souris.
			if ( !this.markerVisible )  return;

			if ( this.marker < this.starting ||
				 this.marker > this.ending   )  return;

			IAdorner adorner = Common.Widgets.Adorners.Factory.Active;
			Rectangle rect = this.Client.Bounds;
			graphics.Align(ref rect);

			double posx = this.DocumentToScreen(this.marker);

			Path path = new Path();
			path.MoveTo(posx, 1);
			path.LineTo(posx-4, rect.Top);
			path.LineTo(posx+4, rect.Top);
			path.Close();

			graphics.Rasterizer.AddSurface(path);
			graphics.RenderSolid(adorner.ColorCaption);

			graphics.Rasterizer.AddOutline(path);
			graphics.RenderSolid(adorner.ColorTextFieldBorder(this.IsEnabled));
		}

		protected void PaintMarginLeftFirst(Graphics graphics, bool hilite)
		{
			//	Dessine la marge gauche pour la premi�re ligne.
			if ( !this.edited )  return;

			if ( this.marginLeftFirst < this.starting ||
				 this.marginLeftFirst > this.ending   )  return;

			IAdorner adorner = Common.Widgets.Adorners.Factory.Active;
			Rectangle rect = this.Client.Bounds;

			double posx = this.GetHandleHorizontalPos("LeftFirst");
			rect.Left  = posx-3;
			rect.Right = posx+3;
			graphics.Align(ref rect);

			Path path = new Path();
			path.MoveTo((rect.Left+rect.Right)/2, rect.Bottom+8);
			path.LineTo(rect.Left-0.5,  rect.Top-2);
			path.LineTo(rect.Left-0.5,  rect.Top-0);
			path.LineTo(rect.Right+0.5, rect.Top-0);
			path.LineTo(rect.Right+0.5, rect.Top-2);
			path.Close();

			graphics.Rasterizer.AddSurface(path);
			graphics.RenderSolid(this.ColorBackgroundMargins(hilite));

			graphics.Rasterizer.AddOutline(path);
			graphics.RenderSolid(this.ColorBorderMargins);
		}

		protected void PaintMarginLeftBody(Graphics graphics, bool hilite)
		{
			//	Dessine la marge gauche pour le corps du texte.
			if ( !this.edited )  return;

			if ( this.marginLeftBody < this.starting ||
				 this.marginLeftBody > this.ending   )  return;

			IAdorner adorner = Common.Widgets.Adorners.Factory.Active;
			Rectangle rect = this.Client.Bounds;

			double posx = this.GetHandleHorizontalPos("LeftBody");
			rect.Left  = posx-3;
			rect.Right = posx+3;
			graphics.Align(ref rect);

			Path path = new Path();
			path.MoveTo((rect.Left+rect.Right)/2, rect.Top-4);
			path.LineTo(rect.Left-0.5,  rect.Bottom+6);
			path.LineTo(rect.Left-0.5,  rect.Bottom+0);
			path.LineTo(rect.Right+0.5, rect.Bottom+0);
			path.LineTo(rect.Right+0.5, rect.Bottom+6);
			path.Close();

			graphics.Rasterizer.AddSurface(path);
			graphics.RenderSolid(this.ColorBackgroundMargins(hilite));

			graphics.Rasterizer.AddOutline(path);
			graphics.AddLine(rect.Left-0.5, rect.Bottom+4.5, rect.Right+0.5, rect.Bottom+4.5);
			graphics.RenderSolid(this.ColorBorderMargins);
		}

		protected void PaintMarginRight(Graphics graphics, bool hilite)
		{
			//	Dessine la marge droite.
			if ( !this.edited )  return;

			if ( this.marginRight < this.starting ||
				 this.marginRight > this.ending   )  return;

			IAdorner adorner = Common.Widgets.Adorners.Factory.Active;
			Rectangle rect = this.Client.Bounds;

			double posx = this.GetHandleHorizontalPos("Right");
			rect.Left  = posx-3;
			rect.Right = posx+3;
			graphics.Align(ref rect);

			Path path = new Path();
			path.MoveTo((rect.Left+rect.Right)/2, rect.Top-4);
			path.LineTo(rect.Left-0.5,  rect.Bottom+6);
			path.LineTo(rect.Left-0.5,  rect.Bottom+0);
			path.LineTo(rect.Right+0.5, rect.Bottom+0);
			path.LineTo(rect.Right+0.5, rect.Bottom+6);
			path.Close();

			graphics.Rasterizer.AddSurface(path);
			graphics.RenderSolid(this.ColorBackgroundMargins(hilite));

			graphics.Rasterizer.AddOutline(path);
			graphics.RenderSolid(this.ColorBorderMargins);
		}

		protected void PaintTabs(Graphics graphics)
		{
			//	Dessine tous les tabulateurs.
			if ( !this.edited )  return;
			if ( this.tabs == null || this.tabs.Length == 0 )  return;

			IAdorner adorner = Common.Widgets.Adorners.Factory.Active;

			for ( int pass=1 ; pass<=3 ; pass++ )
			{
				if ( pass == 2 && this.draggingTabDest == null )  continue;

				foreach ( Tab tab in this.tabs )
				{
					if ( tab.Tag == this.draggingTabToDelete )  continue;
				
					Rectangle rect = this.Client.Bounds;

					double posx = this.GetHandleHorizontalPos(tab);
					rect.Left  = posx-rect.Height/2;
					rect.Right = posx+rect.Height/2;
					graphics.Align(ref rect);
					rect.Bottom += 3.0;

					Color colorBack = Color.FromBrightness(1);

					if ( tab.Tag == this.HiliteHandle )  // tabulateur survol� par la souris ?
					{
						colorBack = this.ColorBackgroundMargins(true);
					}
					else
					{
						colorBack = tab.Zombie ? DrawingContext.ColorTabZombie : this.ColorBackgroundEdited;
					}

					Color colorGlyph = Color.FromBrightness(0);  // noir
					if ( colorBack.GetBrightness() <= 0.4 )  // couleur fonc�e ?
					{
						colorGlyph = Color.FromBrightness(1);  // blanc
					}
					if ( tab.Shared )
					{
						colorGlyph = Color.FromRGB(1.0, 0.5, 0.0);  // orange pour bien voir
					}

					if ( pass == 1 )
					{
						graphics.AddFilledRectangle(rect);
						graphics.RenderSolid(colorBack);
					}
					else if ( pass == 2 )
					{
						if ( tab.Tag == this.draggingTabDest )
						{
							graphics.AddFilledCircle(rect.Center, rect.Width/2);
							graphics.RenderSolid(this.ColorBackgroundMargins(true));

							graphics.AddCircle(rect.Center, rect.Width/2);
							graphics.RenderSolid(colorGlyph);
						}
					}
					else if ( pass == 3 )
					{
						rect.Inflate(5);
						rect.Offset(0.5, 0);

						Common.Widgets.GlyphShape glyph = HRuler.ConvType2Glyph(tab.Type);
						Common.Widgets.WidgetState state = Common.Widgets.WidgetState.Enabled;
						adorner.PaintGlyph(graphics, rect, state, colorGlyph, glyph, Common.Widgets.PaintTextStyle.Button);
					}
				}
			}
		}

		protected void PaintTabChoice(Graphics graphics)
		{
			//	Dessine le "bouton" pour choisir le type de tabulateur.
			IAdorner adorner = Common.Widgets.Adorners.Factory.Active;

			Rectangle rect = this.Client.Bounds;
			rect.Width = rect.Height;

			graphics.AddFilledRectangle(rect);
			graphics.RenderSolid(this.ColorBackgroundEdited);

			rect.Deflate(0.5);
			rect.Width += 1;
			graphics.AddRectangle(rect);
			graphics.RenderSolid(adorner.ColorTextFieldBorder(this.IsEnabled));
			rect.Width -= 1;
			rect.Inflate(0.5);
			rect.Bottom += 3.0;
			rect.Inflate(5);
			rect.Offset(0.5, -1);

			Common.Widgets.GlyphShape glyph = HRuler.ConvType2Glyph(this.tabToCreate);
			adorner.PaintGlyph(graphics, rect, Common.Widgets.WidgetState.Enabled, glyph, Common.Widgets.PaintTextStyle.Button);
		}
		
		protected override void PaintBackgroundImplementation(Graphics graphics, Rectangle clipRect)
		{
			//	Dessine toute la r�gle.
			IAdorner adorner = Common.Widgets.Adorners.Factory.Active;
			
			Rectangle rect = this.Client.Bounds;
			graphics.AddFilledRectangle(rect);
			graphics.RenderSolid(this.ColorBackground);  // dessine le fond

			if ( this.edited )
			{
				Rectangle zone = rect;
				zone.Left  = this.DocumentToScreen(this.limitLow);
				zone.Right = this.DocumentToScreen(this.limitHigh);
				graphics.AddFilledRectangle(zone);
				graphics.RenderSolid(this.ColorBackgroundEdited);
			}

			this.PaintGrad(graphics, clipRect);
			this.PaintMarker(graphics);

			if ( this.edited )  // �dition en cours ?
			{
				this.PaintTabs(graphics);
				this.PaintMarginLeftFirst(graphics, this.HiliteHandle == "LeftFirst" || this.HiliteHandle == "FirstBody");
				this.PaintMarginLeftBody (graphics, this.HiliteHandle == "LeftBody"  || this.HiliteHandle == "FirstBody");
				this.PaintMarginRight    (graphics, this.HiliteHandle == "Right");
				this.PaintTabChoice(graphics);
			}

			rect.Deflate(0.5);
			graphics.AddRectangle(rect);  // dessine le cadre
			graphics.RenderSolid(adorner.ColorTextFieldBorder(this.IsEnabled));
		}


		protected string[] DetectTabs(Point pos, string exclude)
		{
			//	D�tecte les tabulateurs vis�s par la souris.
			int total = 0;
			for ( int i=0 ; i<this.tabs.Length ; i++ )
			{
				Tab tab = this.tabs[i];
				if ( tab.Tag == exclude )  continue;

				double posx = this.GetHandleHorizontalPos(tab);
				if ( pos.X < posx-5 || pos.X > posx+5 )  continue;
				
				total ++;
			}
			if ( total == 0 )  return null;

			string[] list = new string[total];
			int j = 0;
			for ( int i=0 ; i<this.tabs.Length ; i++ )
			{
				Tab tab = this.tabs[i];
				if ( tab.Tag == exclude )  continue;

				double posx = this.GetHandleHorizontalPos(tab);
				if ( pos.X < posx-5 || pos.X > posx+5 )  continue;
				
				list[j++] = tab.Tag;
			}

			return list;
		}

		protected override string DraggingDetect(Point pos, string exclude)
		{
			//	D�tecte la poign�e vis�e par la souris.
			if ( !this.edited )  return null;
			if ( this.editObject == null )  return null;

			if ( this.DetectHandle("Right",     pos) )  return "Right";
			if ( this.DetectHandle("FirstBody", pos) )  return "FirstBody";
			if ( this.DetectHandle("LeftBody",  pos) )  return "LeftBody";
			if ( this.DetectHandle("LeftFirst", pos) )  return "LeftFirst";

			for ( int i=this.tabs.Length-1 ; i>=0 ; i-- )
			{
				Tab tab = this.tabs[i];
				if ( tab.Tag == exclude )  continue;

				double posx = this.GetHandleHorizontalPos(tab);
				if ( pos.X < posx-5 || pos.X > posx+5 )  continue;
				
				return tab.Tag;
			}
			return null;
		}

		protected bool DetectHandle(string handle, Point pos)
		{
			//	D�tecte si la souris est sur une poign�e.
			double posx = this.GetHandleHorizontalPos(handle);
			if ( pos.X < posx-5 || pos.X > posx+5 )  return false;

			if ( handle == "LeftFirst" && pos.Y < this.Client.Bounds.Top-4    )  return false;
			if ( handle == "LeftBody"  && pos.Y > this.Client.Bounds.Bottom+8 )  return false;
			if ( handle == "FirstBody" && pos.Y > this.Client.Bounds.Bottom+4 )  return false;

			return true;
		}

		protected override void DraggingStart(ref string handle, Point pos)
		{
			//	D�but du drag d'une poign�e.
			this.draggingTabToDelete = null;
			this.draggingTabDest = null;
			this.draggingFirstMove = true;
			
			if ( handle == null )
			{
				Rectangle rect = this.Client.Bounds;
				rect.Width = rect.Height;
				if ( rect.Contains(pos) )  // change le type de tabulateur � ins�rer ?
				{
					Point posMenu = this.MapClientToScreen(new Point(0, 1));
					VMenu menu = this.CreateMenu(new MessageEventHandler(this.HandleMenuPressed));
					if ( menu == null )  return;
					menu.Host = this;
					menu.ShowAsContextMenu(this.Window, posMenu);
				}
				else	// pose un nouveau tabulateur ?
				{
					Drawing.Rectangle bbox = this.editObject.BoundingBoxThin;
					double tabPos = this.ScreenToDocument(pos.X);
					tabPos = tabPos - bbox.Left;
					tabPos = this.SnapGrid(tabPos);
					handle = this.editObject.NewTextTab(tabPos, this.tabToCreate);
					this.draggingOffset = 0.0;
					this.draggingFirstMove = false;
				}
			}
			else	// d�place un tabulateur existant ?
			{
				double initial = this.GetHandleHorizontalPos(handle);
				this.draggingOffset = initial - pos.X;
			}
		}

		protected override void DraggingMove(ref string handle, Point pos)
		{
			//	D�place une poign�e.
			if ( handle == null )  return;

			pos.X += this.draggingOffset;

			if ( handle == "LeftFirst" )  // left first ?
			{
				double posBody = this.GetHandleHorizontalPos("LeftBody");
				if ( System.Math.Abs(pos.X-posBody) < 3 )
				{
					pos.X = posBody;  // magn�tise pile sur le left body
				}
			}

			if ( this.IsTab(handle) )  // d�placement d'un tab ?
			{
				Rectangle rect = this.Client.Bounds;
				if ( pos.Y < rect.Bottom || pos.Y > rect.Top )  // hors de la r�gle ?
				{
					this.draggingTabToDelete = handle;
					this.draggingTabDest = null;
				}
				else
				{
					this.draggingTabToDelete = null;
					this.draggingTabDest = this.DraggingDetect(pos, handle);
				}
			}

			this.SetHandleHorizontalPos(ref handle, pos);

			this.draggingFirstMove = false;
			
			pos.X = this.GetHandleHorizontalPos(handle);
			pos = this.document.Modifier.ActiveViewer.ScreenToInternal(pos);
			this.document.Modifier.ActiveViewer.MarkerVertical = (this.draggingTabToDelete == null) ? pos.X : double.NaN;
		}

		protected override void DraggingEnd(ref string handle, Point pos)
		{
			//	Fin du drag d'une poign�e.
			if ( handle == null )  return;

			this.document.Modifier.ActiveViewer.MarkerVertical = double.NaN;

			if ( this.IsTab(handle) )  // fin du d�placement d'un tab ?
			{
				Rectangle rect = this.Client.Bounds;
				if ( pos.Y < rect.Bottom || pos.Y > rect.Top )  // hors de la r�gle ?
				{
					this.editObject.DeleteTextTab(handle);
				}
				else
				{
					string[] list = this.DetectTabs(pos, handle);
					if ( list != null )
					{
						this.editObject.RenameTextTabs(list, handle);
					}
				}
				
				this.draggingTabToDelete = null;
				this.draggingTabDest = null;
			}
		}

		protected double GetHandleHorizontalPos(Tab tab)
		{
			//	Donne la position d'un tabulateur.
			return this.DocumentToScreen(tab.Pos);
		}

		protected double GetHandleHorizontalPos(string handle)
		{
			//	Donne la position d'une poign�e quelconque (marge ou tabulateur).
			if ( handle == "LeftFirst" )  return this.DocumentToScreen(this.marginLeftFirst);
			if ( handle == "LeftBody"  )  return this.DocumentToScreen(this.marginLeftBody);
			if ( handle == "FirstBody" )  return this.DocumentToScreen(this.marginLeftBody);
			if ( handle == "Right"     )  return this.DocumentToScreen(this.marginRight);

			Tab tab = this.GetTab(handle);
			return this.DocumentToScreen(tab.Pos);
		}

		protected void SetHandleHorizontalPos(ref string handle, Point pos)
		{
			//	Modifie la position d'une poign�e quelconque (marge ou tabulateur).
			Drawing.Rectangle bbox = this.editObject.BoundingBoxThin;

			Tab tab = this.GetTab(handle);
			if ( tab.Tag == null )
			{
				if ( handle == "LeftFirst" )
				{
					double leftFirst = this.ScreenToDocument(pos.X);
					leftFirst = leftFirst - bbox.Left;
					leftFirst = this.SnapGrid(leftFirst);
					leftFirst = System.Math.Max(leftFirst, 0);

					this.document.ParagraphWrapper.SuspendSynchronisations();
					this.document.ParagraphWrapper.Defined.LeftMarginFirst = leftFirst;
					this.document.ParagraphWrapper.Defined.MarginUnits     = Common.Text.Properties.SizeUnits.Points;
					this.document.ParagraphWrapper.ResumeSynchronisations();
				}

				if ( handle == "LeftBody" )
				{
					double leftBody = this.ScreenToDocument(pos.X);
					leftBody = leftBody - bbox.Left;
					leftBody = this.SnapGrid(leftBody);
					leftBody = System.Math.Max(leftBody, 0);

					this.document.ParagraphWrapper.SuspendSynchronisations();
					this.document.ParagraphWrapper.Defined.LeftMarginBody = leftBody;
					this.document.ParagraphWrapper.Defined.MarginUnits    = Common.Text.Properties.SizeUnits.Points;
					this.document.ParagraphWrapper.ResumeSynchronisations();
				}

				if ( handle == "FirstBody" )
				{
					double initialLeftFirst, initialLeftBody, initialRight;
					this.GetMargins(out initialLeftFirst, out initialLeftBody, out initialRight);

					double leftBody = this.ScreenToDocument(pos.X);
					leftBody = leftBody - bbox.Left;
					leftBody = this.SnapGrid(leftBody);
					leftBody = System.Math.Max(leftBody, 0);

					double leftFirst = initialLeftFirst + (leftBody-initialLeftBody);
					leftFirst = this.SnapGrid(leftFirst);
					leftFirst = System.Math.Max(leftFirst, 0);

					this.document.ParagraphWrapper.SuspendSynchronisations();
					this.document.ParagraphWrapper.Defined.LeftMarginFirst = leftFirst;
					this.document.ParagraphWrapper.Defined.LeftMarginBody  = leftBody;
					this.document.ParagraphWrapper.Defined.MarginUnits     = Common.Text.Properties.SizeUnits.Points;
					this.document.ParagraphWrapper.ResumeSynchronisations();
				}

				if ( handle == "Right" )
				{
					double right = this.ScreenToDocument(pos.X);
					right = bbox.Right - right;
					right = this.SnapGrid(right);
					right = System.Math.Max(right, 0);

					this.document.ParagraphWrapper.SuspendSynchronisations();
					this.document.ParagraphWrapper.Defined.RightMarginFirst = right;
					this.document.ParagraphWrapper.Defined.RightMarginBody  = right;
					this.document.ParagraphWrapper.Defined.MarginUnits      = Common.Text.Properties.SizeUnits.Points;
					this.document.ParagraphWrapper.ResumeSynchronisations();
				}
			}
			else	// tabulateur ?
			{
				double tabPos;
				TextTabType type;
				this.editObject.GetTextTab(handle, out tabPos, out type);
				
				tabPos = this.ScreenToDocument(pos.X);
				tabPos = tabPos - bbox.Left;
				tabPos = this.SnapGrid(tabPos);
				tabPos = System.Math.Max(tabPos, 0);
				
				this.editObject.SetTextTab(ref handle, tabPos, type, this.draggingFirstMove);
			}
		}

		protected bool IsTab(string tag)
		{
			//	Indique si un tag ou un handle correspond � un tabulateur.
			foreach ( Tab tab in this.tabs )
			{
				if ( tab.Tag == tag )  return true;
			}
			return false;
		}

		protected Tab GetTab(string tag)
		{
			//	Donne un tabulateur Tab d'apr�s son tag ou handle.
			foreach ( Tab tab in this.tabs )
			{
				if ( tab.Tag == tag )  return tab;
			}
			return this.invalidTab;
		}

		protected double SnapGrid(double value)
		{
			//	Conversion d'une position relative dans le texte selon la grille.
			if ( this.document == null )  return value;

			Point pos = new Point(value, 0);
			this.document.Modifier.ActiveViewer.DrawingContext.SnapGrid(ref pos);
			return pos.X;
		}


		protected double DocumentToScreen(double value)
		{
			//	Conversion d'une position dans le document en position en pixel dans l'�cran.
			double scale = (this.ending-this.starting)/this.Client.Bounds.Width;
			return (value-this.starting)/scale;
		}

		protected double ScreenToDocument(double value)
		{
			//	Conversion d'une position en pixel dans l'�cran en position dans le document.
			double scale = (this.ending-this.starting)/this.Client.Bounds.Width;
			return value*scale + this.starting;
		}


		protected static bool TabCompare(Tab[] list1, Tab[] list2)
		{
			//	Compare deux listes de tabulateurs.
			//	Retourne true si les deux listes sont �quivalentes (m�me contenu).
			if ( list1 == null && list2 == null )  return true;
			if ( list1 == null && list2 != null )  return false;
			if ( list1 != null && list2 == null )  return false;
			if ( list1.Length  != list2.Length  )  return false;

			for ( int i=0 ; i<list1.Length ; i++ )
			{
				if ( list1[i].Pos    != list2[i].Pos    )  return false;
				if ( list1[i].Type   != list2[i].Type   )  return false;
				if ( list1[i].Zombie != list2[i].Zombie )  return false;
				if ( list1[i].Shared != list2[i].Shared )  return false;
			}
			return true;
		}


		protected override string GetTooltipEditedText(Point pos)
		{
			//	Donne le texte du tooltip d'�dition en fonction de la position.
			Drawing.Rectangle bbox = this.editObject.BoundingBoxThin;
			double x = this.ScreenToDocument(pos.X) - bbox.Left;

			if ( this.isDragging )  // d�placement en cours ?
			{
				if ( this.draggingTabDest == null )
				{
					x = this.SnapGrid(x);
					return this.document.Modifier.RealToString(x);
				}
				else
				{
					return Res.Strings.Action.Text.Ruler.TabMerge;
				}
			}

			Rectangle rect = this.Client.Bounds;
			rect.Width = rect.Height;
			if ( rect.Contains(pos) )  // change le type de tabulateur � ins�rer ?
			{
				return Res.Strings.Action.Text.Ruler.TabChoice;
			}
			
			string handle = this.DraggingDetect(pos);

			if ( handle == "LeftFirst" )  return Res.Strings.Action.Text.Ruler.HandleLeftFirst;
			if ( handle == "LeftBody"  )  return Res.Strings.Action.Text.Ruler.HandleLeftBody;
			if ( handle == "FirstBody" )  return Res.Strings.Action.Text.Ruler.HandleFirstBody;
			if ( handle == "Right"     )  return Res.Strings.Action.Text.Ruler.HandleRight;

			Tab tab = this.GetTab(handle);
			if ( tab.Tag != null )
			{
				if ( tab.Zombie )
				{
					return Res.Strings.Action.Text.Ruler.TabZombie;
				}
				else
				{
					return HRuler.ConvType2String(tab.Type);
				}
			}

			if ( x >= 0.0 && x <= bbox.Width )
			{
				return Res.Strings.Action.Text.Ruler.TabCreate;
			}

			return null;  // pas de tooltip
		}


		#region Conversions
		protected static Common.Widgets.GlyphShape ConvType2Glyph(TextTabType type)
		{
			//	Conversion d'un type de tabulateur en ic�ne � mettre dans la r�gle.
			switch ( type )
			{
				case Drawing.TextTabType.Left:    return Common.Widgets.GlyphShape.TabLeft;
				case Drawing.TextTabType.Right:   return Common.Widgets.GlyphShape.TabRight;
				case Drawing.TextTabType.Center:  return Common.Widgets.GlyphShape.TabCenter;
				case Drawing.TextTabType.Indent:  return Common.Widgets.GlyphShape.TabIndent;
				default:                          return Common.Widgets.GlyphShape.TabDecimal;
			}
		}

		protected static string ConvType2String(TextTabType type)
		{
			//	Conversion d'un type de tabulateur en texte clair du genre "Tabulateur sur point (.)".
			switch ( type )
			{
				case Drawing.TextTabType.Left:    return Res.Strings.Action.Text.Ruler.TabLeft;
				case Drawing.TextTabType.Right:   return Res.Strings.Action.Text.Ruler.TabRight;
				case Drawing.TextTabType.Center:  return Res.Strings.Action.Text.Ruler.TabCenter;
				case Drawing.TextTabType.Indent:  return Res.Strings.Action.Text.Ruler.TabIndent;
			}

			string mark = HRuler.ConvType2Mark(type);
			if ( mark == null )  return "";
			string text = Misc.GetUnicodeName((int)mark[0]);
			string sample = TextLayout.ConvertToTaggedText(mark);
			return string.Format(Res.Strings.Action.Text.Ruler.TabDecimal, text, sample);
		}

		protected static string ConvType2Name(TextTabType type)
		{
			//	Conversion d'un type de tabulateur en nom interne.
			return type.ToString();
		}

		protected static TextTabType ConvName2Type(string name)
		{
			//	Conversion d'un nom interne en type de tabulateur.
			if ( name == null )  return TextTabType.None;
			return (TextTabType) System.Enum.Parse(typeof(TextTabType), name);
		}

		public static string ConvType2Mark(TextTabType type)
		{
			//	Conversion d'un type de tabulateur en marque � utiliser pour les tabulateurs d�cimaux (dockingMark).
			switch ( type )
			{
				case Drawing.TextTabType.DecimalDot:       return ".";
				case Drawing.TextTabType.DecimalComma:     return ",";
				case Drawing.TextTabType.DecimalColon:     return ":";
				case Drawing.TextTabType.DecimalSColon:    return ";";
				case Drawing.TextTabType.DecimalAdd:       return "+";
				case Drawing.TextTabType.DecimalSub:       return "-";
				case Drawing.TextTabType.DecimalMul:       return "*";
				case Drawing.TextTabType.DecimalDiv:       return "/";
				case Drawing.TextTabType.DecimalSpace:     return " ";
				case Drawing.TextTabType.DecimalAt:        return "@";
				case Drawing.TextTabType.DecimalP100:      return "%";
				case Drawing.TextTabType.DecimalEq:        return "=";
				case Drawing.TextTabType.DecimalLt:        return "<";
				case Drawing.TextTabType.DecimalGt:        return ">";
				case Drawing.TextTabType.DecimalAmp:       return "&";
				case Drawing.TextTabType.DecimalQuot:      return "\"";
				case Drawing.TextTabType.DecimalNumber:    return "#";
				case Drawing.TextTabType.DecimalApos:      return "'";
				case Drawing.TextTabType.DecimalVert:      return "|";
				case Drawing.TextTabType.DecimalTilde:     return "~";
				case Drawing.TextTabType.DecimalExclam:    return "!";
				case Drawing.TextTabType.DecimalQuestion:  return "?";
				case Drawing.TextTabType.DecimalOpPar:     return "(";
				case Drawing.TextTabType.DecimalClPar:     return ")";
				case Drawing.TextTabType.DecimalOpSBr:     return "[";
				case Drawing.TextTabType.DecimalClSBr:     return "]";
				case Drawing.TextTabType.DecimalOpCBr:     return "{";
				case Drawing.TextTabType.DecimalClCBr:     return "}";
				case Drawing.TextTabType.DecimalEuro:      return "�";
				case Drawing.TextTabType.DecimalDollar:    return "$";
				case Drawing.TextTabType.DecimalLivre:     return "�";
				case Drawing.TextTabType.DecimalCopy:      return "�";
				case Drawing.TextTabType.DecimalReg:       return "�";
				case Drawing.TextTabType.DecimalTM:        return "�";
			}
			return null;
		}

		public static TextTabType ConvMark2Type(string mark)
		{
			//	Conversion d'une marque de tabulateur d�cimal (dockingMark) en type de tabulateur.
			switch ( mark )
			{
				case ".":  return Drawing.TextTabType.DecimalDot;
				case ",":  return Drawing.TextTabType.DecimalComma;
				case ":":  return Drawing.TextTabType.DecimalColon;
				case ";":  return Drawing.TextTabType.DecimalSColon;
				case "+":  return Drawing.TextTabType.DecimalAdd;
				case "-":  return Drawing.TextTabType.DecimalSub;
				case "*":  return Drawing.TextTabType.DecimalMul;
				case "/":  return Drawing.TextTabType.DecimalDiv;
				case " ":  return Drawing.TextTabType.DecimalSpace;
				case "@":  return Drawing.TextTabType.DecimalAt;
				case "%":  return Drawing.TextTabType.DecimalP100;
				case "=":  return Drawing.TextTabType.DecimalEq;
				case "<":  return Drawing.TextTabType.DecimalLt;
				case ">":  return Drawing.TextTabType.DecimalGt;
				case "&":  return Drawing.TextTabType.DecimalAmp;
				case "\"": return Drawing.TextTabType.DecimalQuot;
				case "#":  return Drawing.TextTabType.DecimalNumber;
				case "'":  return Drawing.TextTabType.DecimalApos;
				case "|":  return Drawing.TextTabType.DecimalVert;
				case "~":  return Drawing.TextTabType.DecimalTilde;
				case "!":  return Drawing.TextTabType.DecimalExclam;
				case "?":  return Drawing.TextTabType.DecimalQuestion;
				case "(":  return Drawing.TextTabType.DecimalOpPar;
				case ")":  return Drawing.TextTabType.DecimalClPar;
				case "[":  return Drawing.TextTabType.DecimalOpSBr;
				case "]":  return Drawing.TextTabType.DecimalClSBr;
				case "{":  return Drawing.TextTabType.DecimalOpCBr;
				case "}":  return Drawing.TextTabType.DecimalClCBr;
				case "�":  return Drawing.TextTabType.DecimalEuro;
				case "$":  return Drawing.TextTabType.DecimalDollar;
				case "�":  return Drawing.TextTabType.DecimalLivre;
				case "�":  return Drawing.TextTabType.DecimalCopy;
				case "�":  return Drawing.TextTabType.DecimalReg;
				case "�":  return Drawing.TextTabType.DecimalTM;
			}
			return Drawing.TextTabType.None;
		}

		protected static string ConvType2Image(TextTabType type)
		{
			//	Conversion d'un type de tabulateur en tag xml "image" � mettre dans un texte.
			switch ( type )
			{
				case Drawing.TextTabType.Left:    return Misc.Image("TabLeft");
				case Drawing.TextTabType.Right:   return Misc.Image("TabRight");
				case Drawing.TextTabType.Center:  return Misc.Image("TabCenter");
				case Drawing.TextTabType.Indent:  return Misc.Image("TabIndent");
			}
			return Misc.Image("TabDecimal");
		}
		#endregion


		#region Menu
		protected VMenu CreateMenu(MessageEventHandler message)
		{
			//	Cr�e le menu pour choisir un tabulateur.
			VMenu menuMath = new VMenu();
			this.CreateMenu(menuMath, TextTabType.DecimalAdd,  message);
			this.CreateMenu(menuMath, TextTabType.DecimalSub,  message);
			this.CreateMenu(menuMath, TextTabType.DecimalMul,  message);
			this.CreateMenu(menuMath, TextTabType.DecimalDiv,  message);
			this.CreateMenu(menuMath, TextTabType.DecimalP100, message);
			menuMath.Items.Add(new MenuSeparator());
			this.CreateMenu(menuMath, TextTabType.DecimalEq,   message);
			this.CreateMenu(menuMath, TextTabType.DecimalLt,   message);
			this.CreateMenu(menuMath, TextTabType.DecimalGt,   message);

			VMenu menuBus = new VMenu();
			this.CreateMenu(menuBus, TextTabType.DecimalEuro,   message);
			this.CreateMenu(menuBus, TextTabType.DecimalDollar, message);
			this.CreateMenu(menuBus, TextTabType.DecimalLivre,  message);
			menuBus.Items.Add(new MenuSeparator());
			this.CreateMenu(menuBus, TextTabType.DecimalCopy,   message);
			this.CreateMenu(menuBus, TextTabType.DecimalReg,    message);
			this.CreateMenu(menuBus, TextTabType.DecimalTM,     message);

			VMenu menuSign = new VMenu();
			this.CreateMenu(menuSign, TextTabType.DecimalColon,    message);
			this.CreateMenu(menuSign, TextTabType.DecimalSColon,   message);
			menuSign.Items.Add(new MenuSeparator());
			this.CreateMenu(menuSign, TextTabType.DecimalAt,       message);
			this.CreateMenu(menuSign, TextTabType.DecimalAmp,      message);
			this.CreateMenu(menuSign, TextTabType.DecimalNumber,   message);
			this.CreateMenu(menuSign, TextTabType.DecimalQuot,     message);
			this.CreateMenu(menuSign, TextTabType.DecimalApos,     message);
			this.CreateMenu(menuSign, TextTabType.DecimalVert,     message);
			this.CreateMenu(menuSign, TextTabType.DecimalTilde,    message);
			menuSign.Items.Add(new MenuSeparator());
			this.CreateMenu(menuSign, TextTabType.DecimalExclam,   message);
			this.CreateMenu(menuSign, TextTabType.DecimalQuestion, message);

			VMenu menuPar = new VMenu();
			this.CreateMenu(menuPar, TextTabType.DecimalOpPar, message);
			this.CreateMenu(menuPar, TextTabType.DecimalClPar, message);
			menuPar.Items.Add(new MenuSeparator());
			this.CreateMenu(menuPar, TextTabType.DecimalOpSBr, message);
			this.CreateMenu(menuPar, TextTabType.DecimalClSBr, message);
			menuPar.Items.Add(new MenuSeparator());
			this.CreateMenu(menuPar, TextTabType.DecimalOpCBr, message);
			this.CreateMenu(menuPar, TextTabType.DecimalClCBr, message);

			VMenu menu = new VMenu();
			this.CreateMenu(menu, TextTabType.Left,   message);
			this.CreateMenu(menu, TextTabType.Right,  message);
			this.CreateMenu(menu, TextTabType.Center, message);
			this.CreateMenu(menu, TextTabType.Indent, message);
			menu.Items.Add(new MenuSeparator());
			this.CreateMenu(menu, TextTabType.DecimalDot,   message);
			this.CreateMenu(menu, TextTabType.DecimalComma, message);
			this.CreateMenu(menu, TextTabType.DecimalSpace, message);
			this.CreateMenu(menu, menuMath, Res.Strings.Action.Text.Ruler.TabDecimalMath);
			this.CreateMenu(menu, menuBus,  Res.Strings.Action.Text.Ruler.TabDecimalBus);
			this.CreateMenu(menu, menuSign, Res.Strings.Action.Text.Ruler.TabDecimalSign);
			this.CreateMenu(menu, menuPar,  Res.Strings.Action.Text.Ruler.TabDecimalPar);

			menu.AdjustSize();
			return menu;
		}

		protected void CreateMenu(VMenu menu, TextTabType type, MessageEventHandler message)
		{
			//	Cr�e une case du menu pour choisir un tabulateur.
			string text = HRuler.ConvType2String(type);
			string name = HRuler.ConvType2Name(type);
			string icon = Misc.Icon("RadioNo");
			if ( type == this.tabToCreate )
			{
				icon = Misc.Icon("RadioYes");
				text = Misc.Bold(text);
			}
			text = HRuler.ConvType2Image(type) + " " + text;

			MenuItem item = new MenuItem("", icon, text, "", name);

			if ( message != null )
			{
				item.Pressed += message;
			}
			
			menu.Items.Add(item);
		}

		protected void CreateMenu(VMenu menu, VMenu subMenu, string text)
		{
			//	Cr�e un case pour un sous-menu.
			string icon = Misc.Icon("RadioNo");
			foreach ( MenuItem subItem in subMenu.Items )
			{
				if ( HRuler.ConvName2Type(subItem.Name) == this.tabToCreate )
				{
					icon = Misc.Icon("RadioYes");
					text = Misc.Bold(text);
					break;
				}
			}
			text = HRuler.ConvType2Image(TextTabType.Decimal) + " " + text;

			MenuItem item = new MenuItem("", icon, text, "");
			item.Submenu = subMenu;
			menu.Items.Add(item);
		}

		private void HandleMenuPressed(object sender, MessageEventArgs e)
		{
			//	Appel� lorsqu'une case du menu est press�e.
			MenuItem item = sender as MenuItem;
			this.tabToCreate = HRuler.ConvName2Type(item.Name);
			this.Invalidate();
		}
		#endregion

		
		protected double					marginLeftFirst = 0.0;
		protected double					marginLeftBody = 0.0;
		protected double					marginRight = 0.0;
		protected Tab[]						tabs = null;
		protected Tab						invalidTab;

		protected Drawing.TextTabType		tabToCreate = Drawing.TextTabType.Left;
		protected double					draggingOffset = 0.0;
		protected string					draggingTabToDelete = null;
		protected string					draggingTabDest = null;
		protected bool						draggingFirstMove = false;
	}
}

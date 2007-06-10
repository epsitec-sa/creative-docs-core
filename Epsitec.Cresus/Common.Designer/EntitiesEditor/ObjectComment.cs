using System.Collections.Generic;
using Epsitec.Common.Widgets;
using Epsitec.Common.Support;
using Epsitec.Common.Types;
using Epsitec.Common.Drawing;

namespace Epsitec.Common.Designer.EntitiesEditor
{
	/// <summary>
	/// Bo�te pour repr�senter une entit�.
	/// </summary>
	public class ObjectComment : AbstractObject
	{
		protected enum AttachMode
		{
			None,
			Left,
			Right,
			Bottom,
			Top,
		}


		public ObjectComment(Editor editor) : base(editor)
		{
			this.isVisible = true;
			this.boxColor = MainColor.Yellow;

			this.textLayoutTitle = new TextLayout();
			this.textLayoutTitle.DefaultFontSize = 14;
			this.textLayoutTitle.BreakMode = TextBreakMode.SingleLine | TextBreakMode.Ellipsis;
			this.textLayoutTitle.Alignment = ContentAlignment.MiddleCenter;
			this.textLayoutTitle.Text = "<b>Commentaire</b>";

			this.textLayoutComment = new TextLayout();
			this.textLayoutComment.DefaultFontSize = 10;
			this.textLayoutComment.BreakMode = TextBreakMode.Hyphenate | TextBreakMode.Split;
			this.textLayoutComment.Text = "Commentaire libre, que vous pouvez modifier � volont�.";
		}


		public string Text
		{
			//	Texte du commentaire.
			get
			{
				return this.textLayoutComment.Text;
			}
			set
			{
				this.textLayoutComment.Text = value;
			}
		}

		public AbstractObject AttachObject
		{
			//	Object li�e (bo�te ou connection).
			get
			{
				return this.attachObject;
			}
			set
			{
				this.attachObject = value;
			}
		}

		public override Rectangle Bounds
		{
			//	Retourne la bo�te de l'objet.
			//	Attention: le dessin peut d�border, par exemple pour l'ombre.
			get
			{
				return this.isVisible ? this.bounds : Rectangle.Empty;
			}
		}

		public Rectangle InternalBounds
		{
			//	Retourne la bo�te de l'objet.
			get
			{
				return this.bounds;
			}
		}

		public void SetBounds(Rectangle bounds)
		{
			//	Modifie la bo�te de l'objet.
			this.bounds = bounds;
		}

		public bool IsVisible
		{
			//	Est-ce que le commentaire est visible.
			get
			{
				return this.isVisible;
			}
			set
			{
				if (this.isVisible != value)
				{
					this.isVisible = value;

					this.editor.UpdateAfterCommentChanged();
				}
			}
		}


		protected override string GetToolTipText(ActiveElement element)
		{
			//	Retourne le texte pour le tooltip.
			if (this.isDraggingMove || this.isDraggingWidth || this.isDraggingAttach)
			{
				return null;  // pas de tooltip
			}

			return base.GetToolTipText(element);
		}

		public override bool MouseMove(Point pos)
		{
			//	Met en �vidence la bo�te selon la position de la souris.
			//	Si la souris est dans cette bo�te, retourne true.
			if (this.isDraggingMove)
			{
				Rectangle bounds = this.bounds;

				bounds.Offset(pos-this.draggingPos);
				this.draggingPos = pos;

				this.SetBounds(bounds);
				this.editor.Invalidate();
				return true;
			}
			else if (this.isDraggingWidth)
			{
				Rectangle bounds = this.bounds;

				bounds.Right = pos.X;
				bounds.Width = System.Math.Max(bounds.Width, AbstractObject.commentMinWidth);

				this.SetBounds(bounds);
				this.UpdateHeight();
				this.editor.Invalidate();
				return true;
			}
			else if (this.isDraggingAttach)
			{
				ObjectConnection connection = this.attachObject as ObjectConnection;

				double attach = connection.PointToAttach(pos);
				if (attach != 0)
				{
					Point oldPos = connection.PositionConnectionComment;
					connection.Field.CommentAttach = attach;
					Point newPos = connection.PositionConnectionComment;

					Rectangle bounds = this.bounds;
					bounds.Offset(newPos-oldPos);
					this.SetBounds(bounds);  // d�place le commentaire

					this.editor.Invalidate();
				}
				return true;
			}
			else
			{
				return base.MouseMove(pos);
			}
		}

		public override void MouseDown(Point pos)
		{
			//	Le bouton de la souris est press�.
			if (this.hilitedElement == ActiveElement.CommentMove)
			{
				this.isDraggingMove = true;
				this.draggingPos = pos;
				this.editor.LockObject(this);
			}

			if (this.hilitedElement == ActiveElement.CommentWidth)
			{
				this.isDraggingWidth = true;
				this.editor.LockObject(this);
			}

			if (this.hilitedElement == ActiveElement.CommentAttachToConnection)
			{
				this.isDraggingAttach = true;
				this.editor.LockObject(this);
			}
		}

		public override void MouseUp(Point pos)
		{
			//	Le bouton de la souris est rel�ch�.
			if (this.isDraggingMove)
			{
				this.isDraggingMove = false;
				this.editor.LockObject(null);
				this.editor.UpdateAfterCommentChanged();
			}
			else if (this.isDraggingWidth)
			{
				this.isDraggingWidth = false;
				this.editor.LockObject(null);
				this.editor.UpdateAfterCommentChanged();
			}
			else if (this.isDraggingAttach)
			{
				this.isDraggingAttach = false;
				this.editor.LockObject(null);
				this.editor.UpdateAfterCommentChanged();
			}
			else
			{
				if (this.hilitedElement == ActiveElement.CommentClose)
				{
					this.IsVisible = false;
				}

				if (this.hilitedElement == ActiveElement.CommentEdit)
				{
					this.EditComment();
				}

				if (this.hilitedElement == ActiveElement.CommentColorButton1)
				{
					this.BackgroundMainColor = MainColor.Yellow;
				}

				if (this.hilitedElement == ActiveElement.CommentColorButton2)
				{
					this.BackgroundMainColor = MainColor.Pink;
				}

				if (this.hilitedElement == ActiveElement.CommentColorButton3)
				{
					this.BackgroundMainColor = MainColor.Purple;
				}

				if (this.hilitedElement == ActiveElement.CommentColorButton4)
				{
					this.BackgroundMainColor = MainColor.Cyan;
				}
			}
		}

		protected override bool MouseDetect(Point pos, out ActiveElement element, out int fieldRank)
		{
			//	D�tecte l'�l�ment actif vis� par la souris.
			element = ActiveElement.None;
			fieldRank = -1;

			if (pos.IsZero || !this.isVisible)
			{
				return false;
			}

			//	Souris dans le bouton pour modifier la largeur ?
			if (this.DetectRoundButton(this.PositionWidthButton, pos))
			{
				element = ActiveElement.CommentWidth;
				return true;
			}

			//	Souris dans le bouton de fermeture ?
			if (this.DetectRoundButton(this.PositionCloseButton, pos))
			{
				element = ActiveElement.CommentClose;
				return true;
			}

			//	Souris dans le bouton de d�placer l'attache ?
			if (this.DetectRoundButton(this.PositionAttachToConnectionButton, pos))
			{
				element = ActiveElement.CommentAttachToConnection;
				return true;
			}

			//	Souris dans l'en-t�te ?
			if (this.HeaderRectangle.Contains(pos))
			{
				element = ActiveElement.CommentMove;
				return true;
			}

			//	Souris dans le bouton des couleurs ?
			if (this.DetectSquareButton(this.PositionColorButton(0), pos))
			{
				element = ActiveElement.CommentColorButton1;
				return true;
			}

			if (this.DetectSquareButton(this.PositionColorButton(1), pos))
			{
				element = ActiveElement.CommentColorButton2;
				return true;
			}

			if (this.DetectSquareButton(this.PositionColorButton(2), pos))
			{
				element = ActiveElement.CommentColorButton3;
				return true;
			}

			if (this.DetectSquareButton(this.PositionColorButton(3), pos))
			{
				element = ActiveElement.CommentColorButton4;
				return true;
			}

			//	Souris dans la bo�te ?
			if (this.bounds.Contains(pos))
			{
				element = ActiveElement.CommentEdit;
				return true;
			}

			return false;
		}


		public void EditComment()
		{
			//	Modifie le texte du commentaire.
			Module module = this.editor.Module;
			string text = this.textLayoutComment.Text;
			text = module.MainWindow.DlgEntityComment(text);
			if (text != null)
			{
				this.textLayoutComment.Text = text;
				this.UpdateHeight();
				this.editor.UpdateAfterCommentChanged();
			}
		}

		public void UpdateHeight()
		{
			//	Adapte la hauteur du commentaire en fonction de sa largeur et du contenu.
			Rectangle rect = this.bounds;
			rect.Deflate(ObjectComment.textMargin);
			this.textLayoutComment.LayoutSize = rect.Size;

			double h = System.Math.Floor(this.textLayoutComment.FindTextHeight()+1);
			h += ObjectComment.textMargin*2;

			if (this.GetAttachMode() == AttachMode.Bottom)
			{
				this.bounds.Height = h;
			}
			else
			{
				this.bounds.Bottom = this.bounds.Top-h;
			}
		}


		public override void DrawBackground(Graphics graphics)
		{
			//	Dessine le fond de l'objet.
		}

		public override void DrawForeground(Graphics graphics)
		{
			//	Dessine le dessus de l'objet.
			if (!this.isVisible)
			{
				return;
			}
			
			Rectangle rect;
			Rectangle rh = Rectangle.Empty;
			if (this.hilitedElement != ActiveElement.None)
			{
				rh = this.HeaderRectangle;
			}

			//	Dessine l'ombre.
			rect = this.bounds;
			if (!this.isDraggingMove && !this.isDraggingWidth && !this.isDraggingAttach)
			{
				rect = Rectangle.Union(rect, rh);
			}
			rect.Inflate(2);
			rect.Offset(8, -8);
			this.DrawShadow(graphics, rect, 8, 8, 0.2);

			//	Dessine l'en-t�te.
			if (!rh.IsEmpty)
			{
				rect = rh;
				rect.Inflate(0.5);
				graphics.AddFilledRectangle(rect);
				graphics.RenderSolid(this.ColorCommentHeader(this.hilitedElement == ActiveElement.CommentMove, this.isDraggingMove || this.isDraggingWidth));
				graphics.AddRectangle(rect);
				graphics.RenderSolid(Color.FromBrightness(0));

				rect.Width -= rect.Height;
				rect.Offset(0, 1);
				this.textLayoutTitle.LayoutSize = rect.Size;
				this.textLayoutTitle.Paint(rect.BottomLeft, graphics, Rectangle.MaxValue, Color.FromBrightness(1), GlyphPaintStyle.Normal);
			}

			//	Dessine la bo�te vide avec la queue (bulle de bd).
			Path path = this.GetFramePath();
			graphics.Rasterizer.AddSurface(path);
			graphics.RenderSolid(this.ColorComment(this.hilitedElement != ActiveElement.None));
			graphics.Rasterizer.AddOutline(path);
			graphics.RenderSolid(Color.FromBrightness(0));

			//	Dessine le texte.
			rect = this.bounds;
			rect.Deflate(ObjectComment.textMargin);
			this.textLayoutComment.LayoutSize = rect.Size;
			this.textLayoutComment.Paint(rect.BottomLeft, graphics, Rectangle.MaxValue, Color.FromBrightness(0), GlyphPaintStyle.Normal);

			//	Dessine le bouton de fermeture.
			if (!rh.IsEmpty)
			{
				if (this.hilitedElement == ActiveElement.CommentClose)
				{
					this.DrawRoundButton(graphics, this.PositionCloseButton, AbstractObject.buttonRadius, GlyphShape.Close, true, false);
				}
				else if (this.IsHeaderHilite && !this.isDraggingMove && !this.isDraggingWidth && !this.isDraggingAttach)
				{
					this.DrawRoundButton(graphics, this.PositionCloseButton, AbstractObject.buttonRadius, GlyphShape.Close, false, false);
				}
			}

			//	Dessine le bouton des couleurs.
			if (this.hilitedElement == ActiveElement.CommentColorButton1)
			{
				this.DrawSquareButton(graphics, this.PositionColorButton(0), MainColor.Yellow, this.boxColor == MainColor.Yellow, true);
			}
			else if (this.IsHeaderHilite)
			{
				this.DrawSquareButton(graphics, this.PositionColorButton(0), MainColor.Yellow, this.boxColor == MainColor.Yellow, false);
			}

			if (this.hilitedElement == ActiveElement.CommentColorButton2)
			{
				this.DrawSquareButton(graphics, this.PositionColorButton(1), MainColor.Pink, this.boxColor == MainColor.Pink, true);
			}
			else if (this.IsHeaderHilite)
			{
				this.DrawSquareButton(graphics, this.PositionColorButton(1), MainColor.Pink, this.boxColor == MainColor.Pink, false);
			}

			if (this.hilitedElement == ActiveElement.CommentColorButton3)
			{
				this.DrawSquareButton(graphics, this.PositionColorButton(2), MainColor.Purple, this.boxColor == MainColor.Purple, true);
			}
			else if (this.IsHeaderHilite)
			{
				this.DrawSquareButton(graphics, this.PositionColorButton(2), MainColor.Purple, this.boxColor == MainColor.Purple, false);
			}

			if (this.hilitedElement == ActiveElement.CommentColorButton4)
			{
				this.DrawSquareButton(graphics, this.PositionColorButton(3), MainColor.Cyan, this.boxColor == MainColor.Cyan, true);
			}
			else if (this.IsHeaderHilite)
			{
				this.DrawSquareButton(graphics, this.PositionColorButton(3), MainColor.Cyan, this.boxColor == MainColor.Cyan, false);
			}

			//	Dessine le bouton pour modifier la largeur.
			if (this.hilitedElement == ActiveElement.CommentWidth)
			{
				this.DrawRoundButton(graphics, this.PositionWidthButton, AbstractObject.buttonRadius, GlyphShape.HorizontalMove, true, false);
			}
			else if (this.IsHeaderHilite && !this.isDraggingMove && !this.isDraggingWidth && !this.isDraggingAttach)
			{
				this.DrawRoundButton(graphics, this.PositionWidthButton, AbstractObject.buttonRadius, GlyphShape.HorizontalMove, false, false);
			}

			//	Dessine le bouton pour d�placer l'attache.
			Point p = this.PositionAttachToConnectionButton;
			if (!p.IsZero)
			{
				if (this.hilitedElement == ActiveElement.CommentAttachToConnection)
				{
					this.DrawRoundButton(graphics, p, AbstractObject.buttonRadius, "C", true, false);
				}
				else if (this.IsHeaderHilite && !this.isDraggingMove && !this.isDraggingWidth && !this.isDraggingAttach)
				{
					this.DrawRoundButton(graphics, p, AbstractObject.buttonRadius, "C", false, false);
				}
			}
		}

		protected bool IsHeaderHilite
		{
			//	Indique si la souris est dans l'en-t�te.
			get
			{
				return (this.hilitedElement == ActiveElement.CommentEdit ||
						this.hilitedElement == ActiveElement.CommentMove ||
						this.hilitedElement == ActiveElement.CommentClose ||
						this.hilitedElement == ActiveElement.CommentColorButton1 ||
						this.hilitedElement == ActiveElement.CommentColorButton2 ||
						this.hilitedElement == ActiveElement.CommentColorButton3 ||
						this.hilitedElement == ActiveElement.CommentColorButton4 ||
						this.hilitedElement == ActiveElement.CommentAttachToConnection);
			}
		}


		protected Path GetFramePath()
		{
			//	Retourne le chemin du cadre du commentaire (rectangle avec �ventuellement une queue,
			//	comme une bulle de bd).
			Path path = new Path();

			AttachMode mode = this.GetAttachMode();
			Point himself = this.GetAttachHimself(mode);
			Point other = this.GetAttachOther(mode);
			
			Rectangle bounds = this.bounds;
			bounds.Inflate(0.5);

			if (mode == AttachMode.None || himself.IsZero || other.IsZero)
			{
				path.AppendRectangle(bounds);
			}
			else if (mode == AttachMode.Left)
			{
				Point h1 = himself;
				Point h2 = himself;

				h1.Y -= ObjectComment.queueThickness;
				h2.Y += ObjectComment.queueThickness;
				
				if (h1.Y < bounds.Bottom)
				{
					h2.Y += bounds.Bottom-h1.Y;
					h1.Y = bounds.Bottom;
				}
				
				if (h2.Y > bounds.Top)
				{
					h1.Y -= h2.Y-bounds.Top;
					h2.Y = bounds.Top;
				}

				path.MoveTo(other);
				path.LineTo(h1);
				path.LineTo(bounds.BottomLeft);
				path.LineTo(bounds.BottomRight);
				path.LineTo(bounds.TopRight);
				path.LineTo(bounds.TopLeft);
				path.LineTo(h2);
				path.Close();
			}
			else if (mode == AttachMode.Right)
			{
				Point h1 = himself;
				Point h2 = himself;

				h1.Y -= ObjectComment.queueThickness;
				h2.Y += ObjectComment.queueThickness;
				
				if (h1.Y < bounds.Bottom)
				{
					h2.Y += bounds.Bottom-h1.Y;
					h1.Y = bounds.Bottom;
				}
				
				if (h2.Y > bounds.Top)
				{
					h1.Y -= h2.Y-bounds.Top;
					h2.Y = bounds.Top;
				}

				path.MoveTo(other);
				path.LineTo(h1);
				path.LineTo(bounds.BottomRight);
				path.LineTo(bounds.BottomLeft);
				path.LineTo(bounds.TopLeft);
				path.LineTo(bounds.TopRight);
				path.LineTo(h2);
				path.Close();
			}
			else if (mode == AttachMode.Bottom)
			{
				Point h1 = himself;
				Point h2 = himself;

				h1.X -= ObjectComment.queueThickness;
				h2.X += ObjectComment.queueThickness;
				
				if (h1.X < bounds.Left)
				{
					h2.X += bounds.Left-h1.X;
					h1.X = bounds.Left;
				}
				
				if (h2.X > bounds.Right)
				{
					h1.X -= h2.X-bounds.Right;
					h2.X = bounds.Right;
				}

				path.MoveTo(other);
				path.LineTo(h1);
				path.LineTo(bounds.BottomLeft);
				path.LineTo(bounds.TopLeft);
				path.LineTo(bounds.TopRight);
				path.LineTo(bounds.BottomRight);
				path.LineTo(h2);
				path.Close();
			}
			else if (mode == AttachMode.Top)
			{
				Point h1 = himself;
				Point h2 = himself;

				h1.X -= ObjectComment.queueThickness;
				h2.X += ObjectComment.queueThickness;
				
				if (h1.X < bounds.Left)
				{
					h2.X += bounds.Left-h1.X;
					h1.X = bounds.Left;
				}
				
				if (h2.X > bounds.Right)
				{
					h1.X -= h2.X-bounds.Right;
					h2.X = bounds.Right;
				}

				path.MoveTo(other);
				path.LineTo(h1);
				path.LineTo(bounds.TopLeft);
				path.LineTo(bounds.BottomLeft);
				path.LineTo(bounds.BottomRight);
				path.LineTo(bounds.TopRight);
				path.LineTo(h2);
				path.Close();
			}

			return path;
		}

		protected Point GetAttachHimself(AttachMode mode)
		{
			//	Retourne le point d'attache sur le commentaire.
			Point pos = Point.Zero;

			if (mode != AttachMode.None)
			{
				Rectangle bounds = this.bounds;
				bounds.Inflate(0.5);

				if (this.attachObject is ObjectBox)
				{
					ObjectBox box = this.attachObject as ObjectBox;

					if (mode == AttachMode.Left || mode == AttachMode.Right)
					{
						pos.X = (mode == AttachMode.Left) ? bounds.Left : bounds.Right;

						double miny = System.Math.Max(box.Bounds.Bottom, bounds.Bottom);
						double maxy = System.Math.Min(box.Bounds.Top, bounds.Top);

						if (miny <= maxy)
						{
							pos.Y = (miny+maxy)/2;
						}
						else
						{
							pos.Y = (bounds.Top < box.Bounds.Top) ? bounds.Top : bounds.Bottom;
						}
					}

					if (mode == AttachMode.Bottom || mode == AttachMode.Top)
					{
						pos.Y = (mode == AttachMode.Bottom) ? bounds.Bottom : bounds.Top;

						double minx = System.Math.Max(box.Bounds.Left, bounds.Left);
						double maxx = System.Math.Min(box.Bounds.Right, bounds.Right);

						if (minx <= maxx)
						{
							pos.X = (minx+maxx)/2;
						}
						else
						{
							pos.X = (bounds.Right < box.Bounds.Right) ? bounds.Right : bounds.Left;
						}
					}
				}

				if (this.attachObject is ObjectConnection)
				{
					ObjectConnection connection = this.attachObject as ObjectConnection;
					Point attach = connection.PositionConnectionComment;

					if (mode == AttachMode.Left || mode == AttachMode.Right)
					{
						pos.X = (mode == AttachMode.Left) ? bounds.Left : bounds.Right;

						if (attach.Y < bounds.Bottom)
						{
							pos.Y = bounds.Bottom;
						}
						else if (attach.Y > bounds.Top)
						{
							pos.Y = bounds.Top;
						}
						else
						{
							pos.Y = attach.Y;
						}
					}

					if (mode == AttachMode.Bottom || mode == AttachMode.Top)
					{
						pos.Y = (mode == AttachMode.Bottom) ? bounds.Bottom : bounds.Top;

						if (attach.X < bounds.Left)
						{
							pos.X = bounds.Left;
						}
						else if (attach.X > bounds.Right)
						{
							pos.X = bounds.Right;
						}
						else
						{
							pos.X = attach.X;
						}
					}
				}
			}

			return pos;
		}

		protected Point GetAttachOther(AttachMode mode)
		{
			//	Retourne le point d'attache sur l'objet li� (bo�te ou commentaire).
			Point pos = Point.Zero;

			if (mode != AttachMode.None)
			{
				Rectangle bounds = this.bounds;
				bounds.Inflate(0.5);

				if (this.attachObject is ObjectBox)
				{
					ObjectBox box = this.attachObject as ObjectBox;
					Point himself = this.GetAttachHimself(mode);

					if (mode == AttachMode.Left || mode == AttachMode.Right)
					{
						pos.X = (mode == AttachMode.Left) ? box.Bounds.Right : box.Bounds.Left;

						if (himself.Y < box.Bounds.Bottom)
						{
							pos.Y = box.Bounds.Bottom;
						}
						else if (himself.Y > box.Bounds.Top)
						{
							pos.Y = box.Bounds.Top;
						}
						else
						{
							pos.Y = himself.Y;
						}
					}

					if (mode == AttachMode.Bottom || mode == AttachMode.Top)
					{
						pos.Y = (mode == AttachMode.Bottom) ? box.Bounds.Top : box.Bounds.Bottom;

						if (himself.X < box.Bounds.Left)
						{
							pos.X = box.Bounds.Left;
						}
						else if (himself.X > box.Bounds.Right)
						{
							pos.X = box.Bounds.Right;
						}
						else
						{
							pos.X = himself.X;
						}
					}
				}

				if (this.attachObject is ObjectConnection)
				{
					ObjectConnection connection = this.attachObject as ObjectConnection;
					pos = connection.PositionConnectionComment;
				}
			}

			return pos;
		}

		protected AttachMode GetAttachMode()
		{
			//	Cherche d'o� doit partir la queue du commentaire (de quel c�t�).
			if (this.attachObject is ObjectBox)
			{
				ObjectBox box = this.attachObject as ObjectBox;

				if (!this.bounds.IntersectsWith(box.Bounds))
				{
					if (this.bounds.Bottom >= box.Bounds.Top)  // commentaire en dessus ?
					{
						return AttachMode.Bottom;
					}
					
					if (this.bounds.Top <= box.Bounds.Bottom)  // commentaire en dessous ?
					{
						return AttachMode.Top;
					}

					if (this.bounds.Left >= box.Bounds.Right)  // commentaire � droite ?
					{
						return AttachMode.Left;
					}

					if (this.bounds.Right <= box.Bounds.Left)  // commentaire � gauche ?
					{
						return AttachMode.Right;
					}
				}
			}

			if (this.attachObject is ObjectConnection)
			{
				ObjectConnection connection = this.attachObject as ObjectConnection;
				Point attach = connection.PositionConnectionComment;
				if (!attach.IsZero && !this.bounds.Contains(attach))
				{
					if (this.bounds.Bottom >= attach.Y)  // commentaire en dessus ?
					{
						return AttachMode.Bottom;
					}
					
					if (this.bounds.Top <= attach.Y)  // commentaire en dessous ?
					{
						return AttachMode.Top;
					}

					if (this.bounds.Left >= attach.X)  // commentaire � droite ?
					{
						return AttachMode.Left;
					}

					if (this.bounds.Right <= attach.Y)  // commentaire � gauche ?
					{
						return AttachMode.Right;
					}
				}
			}

			return AttachMode.None;
		}


		protected Rectangle HeaderRectangle
		{
			get
			{
				Rectangle rect = this.bounds;
				rect.Bottom = rect.Top;
				rect.Height = ObjectComment.commentHeaderHeight;
				return rect;
			}
		}

		protected Point PositionCloseButton
		{
			//	Retourne la position du bouton de fermeture.
			get
			{
				Rectangle rect = this.HeaderRectangle;
				return new Point(rect.Right-rect.Height/2, rect.Center.Y);
			}
		}

		protected Point PositionWidthButton
		{
			//	Retourne la position du bouton pour modifier la largeur.
			get
			{
				return new Point(this.bounds.Right, this.bounds.Center.Y);
			}
		}

		protected Point PositionAttachToConnectionButton
		{
			//	Retourne la position du bouton pour modifier l'attache � la connection.
			get
			{
				if (this.attachObject != null && this.attachObject is ObjectConnection)
				{
					ObjectConnection connection = this.attachObject as ObjectConnection;
					return connection.PositionConnectionComment;
				}
				else
				{
					return Point.Zero;
				}
			}
		}

		protected Point PositionColorButton(int rank)
		{
			//	Retourne la position du bouton pour choisir la couleur.
			return new Point(this.bounds.Left+AbstractObject.buttonSquare+(AbstractObject.buttonSquare+0.5)*rank*2, this.bounds.Bottom-1-AbstractObject.buttonSquare);
		}

		protected Color ColorComment(bool hilited)
		{
			if (hilited)
			{
				return this.GetColorLighter(this.GetColorMain(), 0.3);
			}
			else
			{
				return this.GetColorLighter(this.GetColorMain(), 0.2);
			}
		}

		protected Color ColorCommentHeader(bool hilited, bool dragging)
		{
			if (dragging)
			{
				return this.GetColorMain(0.9);
			}
			else if (hilited)
			{
				return this.GetColorLighter(this.GetColorMain(), 0.9);
			}
			else
			{
				return this.GetColorLighter(this.GetColorMain(), 0.7);
			}
		}


		protected static readonly double commentHeaderHeight = 24;
		protected static readonly double textMargin = 5;
		protected static readonly double queueThickness = 5;

		protected Rectangle bounds;
		protected AbstractObject attachObject;
		protected bool isVisible;
		protected TextLayout textLayoutTitle;
		protected TextLayout textLayoutComment;

		protected bool isDraggingMove;
		protected bool isDraggingWidth;
		protected bool isDraggingAttach;
		protected Point draggingPos;
	}
}

using System.Collections.Generic;
using Epsitec.Common.Widgets;
using Epsitec.Common.Support;
using Epsitec.Common.Drawing;

namespace Epsitec.Common.Designer.EntitiesEditor
{
	/// <summary>
	/// Bo�te pour repr�senter une entit�.
	/// </summary>
	public class ObjectBox : AbstractObject
	{
		public enum ConnectionAnchor
		{
			Left,
			Right,
			Bottom,
			Top,
		}

		protected enum ActiveElement
		{
			None,
			ExtendButton,
			Header,
			Field,
		}


		public ObjectBox(Editor editor) : base(editor)
		{
#if true // provisoire
			this.fields = new List<string>();
#endif

			this.isExtended = false;
			this.hilitedElement = ActiveElement.None;
		}


		public string Title
		{
			//	Titre au sommet de la bo�te.
			get
			{
				return this.title;
			}
			set
			{
				if (this.title != value)
				{
					this.title = value;
				}
			}
		}

		public void SetContent(string content)
		{
			//	Provisoire...
			string[] list = content.Split(';');

			foreach (string text in list)
			{
				this.fields.Add(text);
			}
		}

		public bool IsExtended
		{
			//	Etat de la bo�te (compact ou �tendu).
			//	En mode compact, seul le titre est visible.
			//	En mode �tendu, les champs sont visibles.
			get
			{
				return this.isExtended;
			}
			set
			{
				if (this.isExtended != value)
				{
					this.isExtended = value;
					this.editor.Invalidate();
				}
			}
		}

		public bool IsReadyForDragging
		{
			//	Est-ce que la bo�te est survol�e par la souris ?
			//	Si la bo�te est survol�e, on peut la d�placer globalement.
			get
			{
				return this.hilitedElement == ActiveElement.Header;
			}
		}


		public double GetBestHeight()
		{
			//	Retourne la hauteur requise selon le nombre de champs d�finis.
			if (this.isExtended)
			{
				return ObjectBox.headerHeight + (ObjectBox.fieldHeight+1)*this.fields.Count + ObjectBox.footerHeight + 15;
			}
			else
			{
				return ObjectBox.headerHeight;
			}
		}

		public double GetConnectionVerticalPosition(int rank)
		{
			//	Retourne la position verticale pour un trait de liaison.
			if (this.isExtended && rank < this.fields.Count)
			{
				Rectangle rect = this.GetFieldBounds(rank);
				return rect.Center.Y;
			}
			else
			{
				return this.bounds.Center.Y;
			}
		}

		public Point GetConnectionDestination(double posv, ConnectionAnchor anchor)
		{
			//	Retourne la position o� accrocher la destination.
			Rectangle bounds = this.bounds;

			switch (anchor)
			{
				case ConnectionAnchor.Left:
					if (posv >= bounds.Bottom+ObjectBox.roundRectRadius && posv <= bounds.Top-ObjectBox.roundRectRadius)
					{
						return new Point(bounds.Left, posv);
					}
					else
					{
						return new Point(bounds.Left, bounds.Center.Y);
					}

				case ConnectionAnchor.Right:
					if (posv >= bounds.Bottom+ObjectBox.roundRectRadius && posv <= bounds.Top-ObjectBox.roundRectRadius)
					{
						return new Point(bounds.Right, posv);
					}
					else
					{
						return new Point(bounds.Right, bounds.Center.Y);
					}

				case ConnectionAnchor.Bottom:
					return new Point(bounds.Center.X, bounds.Bottom);

				case ConnectionAnchor.Top:
					return new Point(bounds.Center.X, bounds.Top);
			}

			return Point.Zero;
		}


		public bool MouseHilite(Point pos)
		{
			//	Met en �vidence la bo�te selon la position de la souris.
			//	Si la souris est dans cette bo�te, retourne true.
			ActiveElement element;
			int fieldRank;
			this.MouseDetect(pos, out element, out fieldRank);

			if (this.hilitedElement != element || this.hilitedFieldRank != fieldRank)
			{
				this.hilitedElement = element;
				this.hilitedFieldRank = fieldRank;
				this.editor.Invalidate();
			}

			return (this.hilitedElement != ActiveElement.None);
		}

		public void MouseDown(Point pos)
		{
			//	Le bouton de la souris est press�.
		}

		public void MouseUp(Point pos)
		{
			//	Le bouton de la souris est rel�ch�.
			if (this.hilitedElement == ActiveElement.ExtendButton)
			{
				this.IsExtended = !this.IsExtended;
				this.editor.UpdateAfterGeometryChanged(this);
			}
		}

		protected bool MouseDetect(Point pos, out ActiveElement element, out int fieldRank)
		{
			//	D�tecte l'�l�ment actif vis� par la souris.
			element = ActiveElement.None;
			fieldRank = -1;

			if (pos.IsZero || !this.bounds.Contains(pos))
			{
				return false;
			}

			//	Souris dans le bouton compact/�tendu ?
			Point center = new Point(this.bounds.Right-ObjectBox.buttonRadius-5, this.bounds.Top-ObjectBox.headerHeight/2);
			double d = Point.Distance(center, pos);
			if (d <= ObjectBox.buttonRadius+2)
			{
				element = ActiveElement.ExtendButton;
				return true;
			}

			//	Souris dans l'en-t�te ?
			if (pos.Y >= this.bounds.Top-ObjectBox.headerHeight ||
				pos.Y <= this.bounds.Bottom+ObjectBox.footerHeight)
			{
				element = ActiveElement.Header;
				return true;
			}

			//	Souris dans un champ ?
			for (int i=0; i<this.fields.Count; i++)
			{
				Rectangle rect = this.GetFieldBounds(i);
				if (rect.Contains(pos))
				{
					element = ActiveElement.Field;
					fieldRank = i;
					return true;
				}
			}

			return false;
		}

		protected Rectangle GetFieldBounds(int rank)
		{
			//	Retourne le rectangle occup� par un champ.
			Rectangle rect = this.bounds;

			rect.Bottom = rect.Top - ObjectBox.headerHeight - ObjectBox.fieldHeight*(rank+1) - 12;
			rect.Height = ObjectBox.fieldHeight;

			return rect;
		}


		public override void Draw(Graphics graphics)
		{
			Rectangle rect;

			//	Dessine l'objet.
			IAdorner adorner = Common.Widgets.Adorners.Factory.Active;

			//	Dessine l'ombre.
			rect = this.bounds;
			rect.Offset(ObjectBox.shadowOffset, -(ObjectBox.shadowOffset));
			this.PaintShadow(graphics, rect, ObjectBox.roundRectRadius+ObjectBox.shadowOffset, (int)ObjectBox.shadowOffset, 0.2);

			//	Construit le chemin du cadre arrondi.
			rect = this.bounds;
			rect.Deflate(1);
			Path path = this.PathRoundRectangle(rect, ObjectBox.roundRectRadius);

			//	Dessine l'int�rieur en blanc.
			graphics.Rasterizer.AddSurface(path);
			graphics.RenderSolid(Color.FromBrightness(1));

			//	Dessine l'int�rieur en d�grad�.
			graphics.Rasterizer.AddSurface(path);
			Color c1 = adorner.ColorCaption;
			Color c2 = adorner.ColorCaption;
			c1.A = this.IsReadyForDragging ? 0.6 : 0.4;
			c2.A = this.IsReadyForDragging ? 0.2 : 0.1;
			this.RenderHorizontalGradient(graphics, this.bounds, c1, c2);

			//	Dessine en blanc la zone pour les champs.
			if (this.isExtended)
			{
				Rectangle inside = new Rectangle(this.bounds.Left+1, this.bounds.Bottom+ObjectBox.footerHeight, this.bounds.Width-2, this.bounds.Height-ObjectBox.footerHeight-ObjectBox.headerHeight);
				graphics.AddFilledRectangle(inside);
				graphics.RenderSolid(Color.FromBrightness(1));
				graphics.AddFilledRectangle(inside);
				Color ci1 = adorner.ColorCaption;
				Color ci2 = adorner.ColorCaption;
				ci1.A = this.IsReadyForDragging ? 0.2 : 0.1;
				ci2.A = 0.0;
				this.RenderHorizontalGradient(graphics, inside, ci1, ci2);

				Rectangle shadow = new Rectangle(this.bounds.Left+1, this.bounds.Top-ObjectBox.headerHeight-8, this.bounds.Width-2, 8);
				graphics.AddFilledRectangle(shadow);
				this.RenderVerticalGradient(graphics, shadow, Color.FromAlphaRgb(0.0, 0, 0, 0), Color.FromAlphaRgb(0.3, 0, 0, 0));
			}

			Color hiliteColor = adorner.ColorCaption;
			hiliteColor.A = 0.1;

			//	Dessine le titre.
			Font font = Font.GetFont("Tahoma", "Bold");

			graphics.AddText(this.bounds.Left+4, this.bounds.Top-ObjectBox.headerHeight+2, this.bounds.Width-ObjectBox.buttonRadius*2-5-6, ObjectBox.headerHeight-2, this.title, font, 14, ContentAlignment.MiddleCenter);
			graphics.RenderSolid(Color.FromBrightness(0));

			//	Dessine le bouton compact/�tendu.
			Point center = new Point(this.bounds.Right-ObjectBox.buttonRadius-5, this.bounds.Top-ObjectBox.headerHeight/2);

			graphics.AddFilledCircle(center, ObjectBox.buttonRadius);
			graphics.RenderSolid(this.hilitedElement == ActiveElement.ExtendButton ? hiliteColor : Color.FromBrightness(1));

			graphics.AddCircle(center, ObjectBox.buttonRadius);
			graphics.RenderSolid(Color.FromBrightness(0));

			rect = new Rectangle(center.X-ObjectBox.buttonRadius, center.Y-ObjectBox.buttonRadius, ObjectBox.buttonRadius*2, ObjectBox.buttonRadius*2);
			adorner.PaintGlyph(graphics, rect, WidgetPaintState.Enabled, this.isExtended ? GlyphShape.ArrowUp : GlyphShape.ArrowDown, PaintTextStyle.Button);

			//	Dessine les noms des champs.
			if (this.isExtended)
			{
				font = Font.GetFont("Tahoma", "Regular");

				Color color = Color.FromBrightness(0.9);
				if (this.IsReadyForDragging)
				{
					color = adorner.ColorCaption;
					color.A = 0.3;
				}

				for (int i=0; i<this.fields.Count; i++)
				{
					rect = this.GetFieldBounds(i);

					if (this.hilitedElement == ActiveElement.Field && this.hilitedFieldRank == i)
					{
						graphics.AddFilledRectangle(rect);
						graphics.RenderSolid(hiliteColor);
					}

					graphics.AddText(rect.Left+10, rect.Bottom, rect.Width-20, ObjectBox.fieldHeight, this.fields[i], font, 11, ContentAlignment.MiddleLeft);
					graphics.RenderSolid(Color.FromBrightness(0));

					graphics.AddLine(rect.Left, rect.Bottom+0.5, rect.Right, rect.Bottom+0.5);
					graphics.RenderSolid(color);

					rect.Offset(0, -ObjectBox.fieldHeight);
				}
			}

			//	Dessine le cadre en noir.
			graphics.Rasterizer.AddOutline(path, 2);
			if (this.isExtended)
			{
				graphics.AddLine(this.bounds.Left+2, this.bounds.Top-ObjectBox.headerHeight-0.5, this.bounds.Right-2, this.bounds.Top-ObjectBox.headerHeight-0.5);
				graphics.AddLine(this.bounds.Left+2, this.bounds.Bottom+ObjectBox.footerHeight+0.5, this.bounds.Right-2, this.bounds.Bottom+ObjectBox.footerHeight+0.5);
			}
			graphics.RenderSolid(this.IsReadyForDragging ? adorner.ColorCaption : Color.FromBrightness(0));
		}




		protected static readonly double roundRectRadius = 12;
		protected static readonly double shadowOffset = 6;
		protected static readonly double headerHeight = 32;
		protected static readonly double footerHeight = 10;
		protected static readonly double buttonRadius = 8;
		protected static readonly double fieldHeight = 20;

		protected bool isExtended;
		protected ActiveElement hilitedElement;
		protected int hilitedFieldRank;
		protected string title;
		protected List<string> fields;
	}
}

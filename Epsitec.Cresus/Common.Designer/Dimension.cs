using Epsitec.Common.Widgets;
using Epsitec.Common.Support;
using Epsitec.Common.Drawing;

namespace Epsitec.Common.Designer
{
	/// <summary>
	/// Description d'une cote pour PanelEditor.
	/// </summary>
	public class Dimension
	{
		//	Types possibles pour les différentes cotes.
		public enum Type
		{
			None,
			Width,
			Height,
			MarginLeft,
			MarginRight,
			MarginBottom,
			MarginTop,
			PaddingLeft,
			PaddingRight,
			PaddingBottom,
			PaddingTop,
			GridColumn,
			GridColumnAddBefore,
			GridColumnAddAfter,
			GridColumnRemove,
			GridRow,
			GridRowAddBefore,
			GridRowAddAfter,
			GridRowRemove,
			GridWidth,
			GridHeight,
			GridWidthMode,
			GridHeightMode,
			GridMarginLeft,
			GridMarginRight,
			GridMarginBottom,
			GridMarginTop,
		}


		public Dimension(MyWidgets.PanelEditor editor, Widget obj, Type type)
		{
			//	Crée une cote.
			this.editor = editor;
			this.objectModifier = editor.ObjectModifier;
			this.context = editor.Context;

			this.obj = obj;
			this.type = type;
			this.columnOrRow = -1;
		}

		public Dimension(MyWidgets.PanelEditor editor, Widget obj, Type type, int columnOrRow)
		{
			//	Crée une cote.
			this.editor = editor;
			this.objectModifier = editor.ObjectModifier;
			this.context = editor.Context;

			this.obj = obj;
			this.type = type;
			this.columnOrRow = columnOrRow;
		}


		public Widget Object
		{
			//	Retourne l'objet coté.
			get
			{
				return this.obj;
			}
		}

		public Type DimensionType
		{
			//	Retourne le type d'une cote.
			get
			{
				return this.type;
			}
		}

		public int ColumnOrRow
		{
			//	Retourne le rang de la ligne ou de la colonne (selon le type).
			get
			{
				return this.columnOrRow;
			}
		}


		public Rectangle GetBounds(bool isHilited)
		{
			//	Retourne le rectangle englobant la cote.
			Path path = this.GeometryBackground(null);
			Rectangle bounds = path.ComputeBounds();

			if (isHilited)
			{
				path = this.GeometryHilitedSurface(null);
				bounds = Rectangle.Union(bounds, path.ComputeBounds());
			}

			bounds.Inflate(1);  // à cause des différents Misc.AlignForLine
			return bounds;
		}


		public void DrawBackground(Graphics graphics)
		{
			//	Dessine une cote.
			Color border = Color.FromBrightness(0.5);  // gris

			Path path = this.GeometryBackground(graphics);
			if (path != null)
			{
				graphics.Rasterizer.AddSurface(path);
				graphics.RenderSolid(this.BackgroundColor);

				graphics.Rasterizer.AddOutline(path);
				graphics.RenderSolid(border);
			}

			this.DrawOutline(graphics, border);
			this.DrawText(graphics, this.GeometryTextBox);
		}

		protected void DrawOutline(Graphics graphics, Color color)
		{
			//	Dessine les traits supplémentaires et les ressorts d'une cote.
			Rectangle bounds = this.objectModifier.GetActualBounds(this.obj);
			Margins margins = this.objectModifier.GetMargins(this.obj);
			Rectangle ext = bounds;
			ext.Inflate(this.objectModifier.GetMargins(this.obj));
			Margins padding = this.objectModifier.GetPadding(this.obj);
			Rectangle inside = this.objectModifier.GetFinalPadding(this.obj);

			Rectangle r, box;
			Path path;
			Point p1, p2;
			double value;

			box = this.GeometryTextBox;
			Misc.AlignForLine(graphics, ref box);

			switch (this.type)
			{
				case Type.Width:
					value = this.Value;
					if (value != bounds.Width)  // forme complexe ?
					{
						r = box;
						double half = System.Math.Max(System.Math.Floor(value/2), 5);
						double middle = System.Math.Floor(r.Center.X)+0.5;
						r.Left = middle-half;
						r.Width = half*2;
						half = System.Math.Floor(value/2);
						p1 = new Point(middle-half, ext.Bottom);
						p2 = new Point(p1.X+half*2, ext.Bottom);
						path = new Path();
						path.MoveTo(p1);
						path.LineTo(r.TopLeft);
						path.LineTo(r.BottomLeft);
						path.LineTo(r.BottomRight);
						path.LineTo(r.TopRight);
						path.LineTo(p2);
						path.Close();
						graphics.Rasterizer.AddOutline(path);
						graphics.RenderSolid(color);

						this.DrawSpring(graphics, new Point(box.Left, box.Center.Y), new Point(r.Left, box.Center.Y), color);
						this.DrawSpring(graphics, new Point(box.Right, box.Center.Y), new Point(r.Right, box.Center.Y), color);
					}
					break;

				case Type.Height:
					value = this.Value;
					if (value != bounds.Height)  // forme complexe ?
					{
						r = box;
						double half = System.Math.Max(System.Math.Floor(value/2), 5);
						double middle = System.Math.Floor(r.Center.Y)+0.5;
						r.Bottom = middle-half;
						r.Height = half*2;
						half = System.Math.Floor(value/2);
						p1 = new Point(ext.Right, middle-half);
						p2 = new Point(ext.Right, p1.Y+half*2);
						path = new Path();
						path.MoveTo(p1);
						path.LineTo(r.BottomLeft);
						path.LineTo(r.BottomRight);
						path.LineTo(r.TopRight);
						path.LineTo(r.TopLeft);
						path.LineTo(p2);
						path.Close();
						graphics.Rasterizer.AddOutline(path);
						graphics.RenderSolid(color);

						this.DrawSpring(graphics, new Point(box.Center.X, box.Bottom), new Point(box.Center.X, r.Bottom), color);
						this.DrawSpring(graphics, new Point(box.Center.X, box.Top), new Point(box.Center.X, r.Top), color);
					}
					break;

				case Type.GridWidth:
					value = this.Value;
					if (value != box.Width && !this.IsPercent)  // forme complexe ?
					{
						r = box;
						double half = System.Math.Max(System.Math.Floor(value/2), 5);
						double middle = System.Math.Floor(r.Center.X)+0.5;
						r.Left = middle-half;
						r.Width = half*2;
						half = System.Math.Floor(value/2);
						p1 = new Point(middle-half, inside.Top);
						p2 = new Point(p1.X+half*2, inside.Top);
						path = new Path();
						path.MoveTo(p1);
						path.LineTo(r.BottomLeft);
						path.LineTo(r.TopLeft);
						path.LineTo(r.TopRight);
						path.LineTo(r.BottomRight);
						path.LineTo(p2);
						path.Close();
						graphics.Rasterizer.AddOutline(path);
						graphics.RenderSolid(color);

						this.DrawSpring(graphics, new Point(box.Left, box.Center.Y), new Point(r.Left, box.Center.Y), color);
						this.DrawSpring(graphics, new Point(box.Right, box.Center.Y), new Point(r.Right, box.Center.Y), color);
					}
					break;

				case Type.GridHeight:
					value = this.Value;
					if (value != box.Height && !this.IsPercent)  // forme complexe ?
					{
						r = box;
						double half = System.Math.Max(System.Math.Floor(value/2), 5);
						double middle = System.Math.Floor(r.Center.Y)+0.5;
						r.Bottom = middle-half;
						r.Height = half*2;
						half = System.Math.Floor(value/2);
						p1 = new Point(inside.Left, middle-half);
						p2 = new Point(inside.Left, p1.Y+half*2);
						path = new Path();
						path.MoveTo(p1);
						path.LineTo(r.BottomRight);
						path.LineTo(r.BottomLeft);
						path.LineTo(r.TopLeft);
						path.LineTo(r.TopRight);
						path.LineTo(p2);
						path.Close();
						graphics.Rasterizer.AddOutline(path);
						graphics.RenderSolid(color);

						this.DrawSpring(graphics, new Point(box.Center.X, box.Bottom), new Point(box.Center.X, r.Bottom), color);
						this.DrawSpring(graphics, new Point(box.Center.X, box.Top), new Point(box.Center.X, r.Top), color);
					}
					break;

				case Type.GridColumn:
					graphics.AddLine(box.Left, ext.Top, box.Left, inside.Top);
					graphics.AddLine(box.Right, ext.Top, box.Right, inside.Top);
					graphics.RenderSolid(color);
					break;

				case Type.GridMarginLeft:
					graphics.AddLine(box.Right, ext.Top, box.Right, inside.Top);
					graphics.RenderSolid(color);
					break;

				case Type.GridMarginRight:
					graphics.AddLine(box.Left, ext.Top, box.Left, inside.Top);
					graphics.RenderSolid(color);
					break;

				case Type.GridRow:
					graphics.AddLine(ext.Left, box.Top, inside.Left, box.Top);
					graphics.AddLine(ext.Left, box.Bottom, inside.Left, box.Bottom);
					graphics.RenderSolid(color);
					break;

				case Type.GridMarginTop:
					graphics.AddLine(ext.Left, box.Bottom, inside.Left, box.Bottom);
					graphics.RenderSolid(color);
					break;

				case Type.GridMarginBottom:
					graphics.AddLine(ext.Left, box.Top, inside.Left, box.Top);
					graphics.RenderSolid(color);
					break;
			}
		}

		public void DrawDimension(Graphics graphics)
		{
			//	Dessine la marque de longueur d'une cote.
			Rectangle bounds = this.objectModifier.GetActualBounds(this.obj);
			Margins margins = this.objectModifier.GetMargins(this.obj);
			Rectangle ext = bounds;
			ext.Inflate(this.objectModifier.GetMargins(this.obj));
			Rectangle inside = this.objectModifier.GetFinalPadding(this.obj);

			Rectangle r, box;
			Point p1, p2;
			double value;

			box = this.GeometryTextBox;
			Misc.AlignForLine(graphics, ref box);

			switch (this.type)
			{
				case Type.Width:
					r = box;
					r.Top = ext.Bottom;

					value = this.Value;
					if (value == bounds.Width)  // forme rectangulaire simple ?
					{
						p1 = new Point(box.Right, ext.Bottom);
						p2 = new Point(box.Left, ext.Bottom);
					}
					else
					{
						double half = System.Math.Floor(value/2);
						double middle = System.Math.Floor(r.Center.X)+0.5;
						p1 = new Point(middle-half, ext.Bottom);
						p2 = new Point(p1.X+half*2, ext.Bottom);
					}
					this.DrawLine(graphics, p1, p2);
					break;

				case Type.Height:
					r = box;
					r.Left = ext.Right;

					value = this.Value;
					if (value == bounds.Height)  // forme rectangulaire simple ?
					{
						p1 = new Point(ext.Right, box.Top);
						p2 = new Point(ext.Right, box.Bottom);
					}
					else
					{
						double half = System.Math.Floor(value/2);
						double middle = System.Math.Floor(r.Center.Y)+0.5;
						p1 = new Point(ext.Right, middle-half);
						p2 = new Point(ext.Right, p1.Y+half*2);
					}
					this.DrawLine(graphics, p1, p2);
					break;

				case Type.MarginLeft:
					p2 = new Point(box.Right, ext.Bottom);
					p1 = new Point(box.Right-margins.Left, ext.Bottom);
					this.DrawLine(graphics, p1, p2);
					break;

				case Type.MarginRight:
					p1 = new Point(box.Left, ext.Bottom);
					p2 = new Point(box.Left+margins.Right, ext.Bottom);
					this.DrawLine(graphics, p1, p2);
					break;

				case Type.MarginBottom:
					p2 = new Point(ext.Right, box.Top);
					p1 = new Point(ext.Right, box.Top-margins.Bottom);
					this.DrawLine(graphics, p1, p2);
					break;

				case Type.MarginTop:
					p1 = new Point(ext.Right, box.Bottom);
					p2 = new Point(ext.Right, box.Bottom+margins.Top);
					this.DrawLine(graphics, p1, p2);
					break;

				case Type.GridWidth:
					if (!this.IsPercent)
					{
						r = box;
						r.Bottom = inside.Top;

						value = this.Value;
						if (value == box.Width)  // forme rectangulaire simple ?
						{
							p1 = new Point(box.Right, inside.Top);
							p2 = new Point(box.Left, inside.Top);
						}
						else
						{
							double half = System.Math.Floor(value/2);
							double middle = System.Math.Floor(r.Center.X)+0.5;
							p1 = new Point(middle-half, inside.Top);
							p2 = new Point(p1.X+half*2, inside.Top);
						}
						this.DrawLine(graphics, p1, p2);
					}
					break;

				case Type.GridHeight:
					if (!this.IsPercent)
					{
						r = box;
						r.Right = inside.Left;

						value = this.Value;
						if (value == bounds.Height)  // forme rectangulaire simple ?
						{
							p1 = new Point(inside.Left, box.Top);
							p2 = new Point(inside.Left, box.Bottom);
						}
						else
						{
							double half = System.Math.Floor(value/2);
							double middle = System.Math.Floor(r.Center.Y)+0.5;
							p1 = new Point(inside.Left, middle-half);
							p2 = new Point(inside.Left, p1.Y+half*2);
						}
						this.DrawLine(graphics, p1, p2);
					}
					break;

				case Type.GridMarginLeft:
					p2 = new Point(box.Right, inside.Top);
					p1 = new Point(box.Right-this.Value, inside.Top);
					this.DrawLine(graphics, p1, p2);
					break;

				case Type.GridMarginRight:
					p1 = new Point(box.Left, inside.Top);
					p2 = new Point(box.Left+this.Value, inside.Top);
					this.DrawLine(graphics, p1, p2);
					break;

				case Type.GridMarginBottom:
					p2 = new Point(inside.Left, box.Top);
					p1 = new Point(inside.Left, box.Top-this.Value);
					this.DrawLine(graphics, p1, p2);
					break;

				case Type.GridMarginTop:
					p1 = new Point(inside.Left, box.Bottom);
					p2 = new Point(inside.Left, box.Bottom+this.Value);
					this.DrawLine(graphics, p1, p2);
					break;
			}
		}

		public void DrawHilite(Graphics graphics, bool dark)
		{
			//	Dessine la cote survolée par la souris.
			Rectangle bounds = this.objectModifier.GetActualBounds(this.obj);
			double alpha = dark ? 1.0 : 0.5;
			Color hilite = Color.FromAlphaRgb(alpha, 255.0/255.0, 124.0/255.0, 37.0/255.0);
			Color border = Color.FromBrightness(0);

			Rectangle box = this.GeometryTextBox;
			Misc.AlignForLine(graphics, ref box);

			//	Dessine la forme avec les bosses up/down.
			Path path = this.GeometryHilitedSurface(graphics);
			graphics.Rasterizer.AddSurface(path);
			graphics.RenderSolid(hilite);
			graphics.Rasterizer.AddOutline(path);
			graphics.RenderSolid(border);

			this.DrawOutline(graphics, border);  // redessine les ressorts par dessus
			this.DrawText(graphics, box);  // redessine la valeur par dessus

			//	Dessine les signes +/-.
			if (!this.IsSimpleRectangle)
			{
				double t = 20;

				Point p = new Point(box.Center.X-1, box.Top+t/4-2);
				Misc.AlignForLine(graphics, ref p);
				graphics.AddLine(p.X-2, p.Y, p.X+2, p.Y);
				graphics.AddLine(p.X, p.Y-2, p.X, p.Y+2);  // croix pour '+'

				p = new Point(box.Center.X-1, box.Bottom-t/4+1);
				Misc.AlignForLine(graphics, ref p);
				graphics.AddLine(p.X-2, p.Y, p.X+2, p.Y);  // trait pour '-'

				graphics.RenderSolid(border);
			}
		}


		public double Value
		{
			//	Valeur réelle représentée par la cote.
			get
			{
				switch (this.type)
				{
					case Type.Width:
						return this.objectModifier.GetWidth(this.obj);

					case Type.Height:
						return this.objectModifier.GetHeight(this.obj);

					case Type.MarginLeft:
						return this.objectModifier.GetMargins(this.obj).Left;

					case Type.MarginRight:
						return this.objectModifier.GetMargins(this.obj).Right;

					case Type.MarginBottom:
						return this.objectModifier.GetMargins(this.obj).Bottom;

					case Type.MarginTop:
						return this.objectModifier.GetMargins(this.obj).Top;

					case Type.PaddingLeft:
						return this.objectModifier.GetPadding(this.obj).Left;

					case Type.PaddingRight:
						return this.objectModifier.GetPadding(this.obj).Right;

					case Type.PaddingBottom:
						return this.objectModifier.GetPadding(this.obj).Bottom;

					case Type.PaddingTop:
						return this.objectModifier.GetPadding(this.obj).Top;

					case Type.GridWidth:
						if (this.objectModifier.GetGridColumnMode(this.obj, this.columnOrRow) == ObjectModifier.GridMode.Auto)
						{
							return this.objectModifier.GetGridColumnMinWidth(this.obj, this.columnOrRow);
						}
						else
						{
							return this.objectModifier.GetGridColumnWidth(this.obj, this.columnOrRow);
						}

					case Type.GridHeight:
						if (this.objectModifier.GetGridRowMode(this.obj, this.columnOrRow) == ObjectModifier.GridMode.Auto)
						{
							return this.objectModifier.GetGridRowMinHeight(this.obj, this.columnOrRow);
						}
						else
						{
							return this.objectModifier.GetGridRowHeight(this.obj, this.columnOrRow);
						}

					case Type.GridMarginLeft:
						return this.objectModifier.GetGridColumnLeftBorder(this.obj, this.columnOrRow);

					case Type.GridMarginRight:
						return this.objectModifier.GetGridColumnRightBorder(this.obj, this.columnOrRow);

					case Type.GridMarginBottom:
						return this.objectModifier.GetGridRowBottomBorder(this.obj, this.columnOrRow);

					case Type.GridMarginTop:
						return this.objectModifier.GetGridRowTopBorder(this.obj, this.columnOrRow);

					default:
						return 0;
				}
			}

			set
			{
				Margins m;

				switch (this.type)
				{
					case Type.Width:
						value = System.Math.Max(value, 0);
						this.objectModifier.SetWidth(this.obj, value);
						break;

					case Type.Height:
						value = System.Math.Max(value, 0);
						this.objectModifier.SetHeight(this.obj, value);
						break;

					case Type.MarginLeft:
						value = System.Math.Max(value, 0);
						m = this.objectModifier.GetMargins(this.obj);
						m.Left = value;
						this.objectModifier.SetMargins(this.obj, m);
						break;

					case Type.MarginRight:
						value = System.Math.Max(value, 0);
						m = this.objectModifier.GetMargins(this.obj);
						m.Right = value;
						this.objectModifier.SetMargins(this.obj, m);
						break;

					case Type.MarginBottom:
						value = System.Math.Max(value, 0);
						m = this.objectModifier.GetMargins(this.obj);
						m.Bottom = value;
						this.objectModifier.SetMargins(this.obj, m);
						break;

					case Type.MarginTop:
						value = System.Math.Max(value, 0);
						m = this.objectModifier.GetMargins(this.obj);
						m.Top = value;
						this.objectModifier.SetMargins(this.obj, m);
						break;

					case Type.PaddingLeft:
						value = System.Math.Max(value, 0);
						m = this.objectModifier.GetPadding(this.obj);
						m.Left = value;
						this.objectModifier.SetPadding(this.obj, m);
						break;

					case Type.PaddingRight:
						value = System.Math.Max(value, 0);
						m = this.objectModifier.GetPadding(this.obj);
						m.Right = value;
						this.objectModifier.SetPadding(this.obj, m);
						break;

					case Type.PaddingBottom:
						value = System.Math.Max(value, 0);
						m = this.objectModifier.GetPadding(this.obj);
						m.Bottom = value;
						this.objectModifier.SetPadding(this.obj, m);
						break;

					case Type.PaddingTop:
						value = System.Math.Max(value, 0);
						m = this.objectModifier.GetPadding(this.obj);
						m.Top = value;
						this.objectModifier.SetPadding(this.obj, m);
						break;

					case Type.GridWidth:
						value = System.Math.Max(value, 0);
						if (this.objectModifier.GetGridColumnMode(this.obj, this.columnOrRow) == ObjectModifier.GridMode.Auto)
						{
							this.objectModifier.SetGridColumnMinWidth(this.obj, this.columnOrRow, value);
						}
						else
						{
							this.objectModifier.SetGridColumnWidth(this.obj, this.columnOrRow, value);
						}
						break;

					case Type.GridHeight:
						value = System.Math.Max(value, 0);
						if (this.objectModifier.GetGridRowMode(this.obj, this.columnOrRow) == ObjectModifier.GridMode.Auto)
						{
							this.objectModifier.SetGridRowMinHeight(this.obj, this.columnOrRow, value);
						}
						else
						{
							this.objectModifier.SetGridRowHeight(this.obj, this.columnOrRow, value);
						}
						break;

					case Type.GridMarginLeft:
						value = System.Math.Max(value, 0);
						this.objectModifier.SetGridColumnLeftBorder(this.obj, this.columnOrRow, value);
						break;

					case Type.GridMarginRight:
						value = System.Math.Max(value, 0);
						this.objectModifier.SetGridColumnRightBorder(this.obj, this.columnOrRow, value);
						break;

					case Type.GridMarginBottom:
						value = System.Math.Max(value, 0);
						this.objectModifier.SetGridRowBottomBorder(this.obj, this.columnOrRow, value);
						break;

					case Type.GridMarginTop:
						value = System.Math.Max(value, 0);
						this.objectModifier.SetGridRowTopBorder(this.obj, this.columnOrRow, value);
						break;
				}

				this.editor.Invalidate();
			}
		}

		public bool Detect(Point mouse)
		{
			//	Détecte si la souris est dans la cote.
#if false
			return this.GeometryTextBox.Contains(mouse);
#else
			Path path = this.GeometryBackground(null);
			return InsideSurface.Contains(path, mouse);
#endif
		}


		protected void DrawLine(Graphics graphics, Point p1, Point p2)
		{
			//	Dessine le trait d'une cote.
			double d = Point.Distance(p1, p2);

			if (d < 1)
			{
				graphics.AddFilledCircle(p1, 2);
				graphics.RenderSolid(Color.FromBrightness(0));
				return;
			}

			double e = 3;
			double i = 1;

			if (p1.Y == p2.Y)  // horizontal ?
			{
				Size se = new Size(0, e);
				Size si = new Size(0, i);

				p1.Y += 0.5;
				p2.Y += 0.5;

				graphics.AddLine(p1, p2);
				if (d > 1)
				{
					graphics.AddLine(p1-si, p1+se);
					graphics.AddLine(p2-si, p2+se);
				}
				graphics.RenderSolid(Color.FromBrightness(0));
			}

			if (p1.X == p2.X)  // vertical ?
			{
				Size se = new Size(e, 0);
				Size si = new Size(i, 0);

				p1.X += 0.5;
				p2.X += 0.5;

				graphics.AddLine(p1, p2);
				if (d > 1)
				{
					graphics.AddLine(p1-si, p1+se);
					graphics.AddLine(p2-si, p2+se);
				}
				graphics.RenderSolid(Color.FromBrightness(0));
			}
		}

		protected void DrawSpring(Graphics graphics, Point p1, Point p2, Color color)
		{
			//	Dessine un petit ressort horizontal ou vertical d'une cote.
			if (Point.Distance(p1, p2) < 8)
			{
				graphics.AddLine(p1, p2);
			}
			else
			{
				Point p1a = Point.Scale(p1, p2, Dimension.attachmentScale);
				Point p2a = Point.Scale(p2, p1, Dimension.attachmentScale);

				graphics.AddLine(p1, p1a);
				graphics.AddLine(p2, p2a);

				double dim = Dimension.attachmentThickness;
				double length = Point.Distance(p1a, p2a);
				int loops = (int) (length/(dim*2));
				loops = System.Math.Max(loops, 1);
				Misc.AddSpring(graphics, p1a, p2a, dim, loops);
			}

			graphics.RenderSolid(color);

			graphics.AddFilledCircle(p1, 1.5);
			graphics.AddFilledCircle(p2, 1.5);
			graphics.RenderSolid(color);
		}

		protected void DrawText(Graphics graphics, Rectangle box)
		{
			//	Dessine la valeur d'une cote avec des petits caractères.
			if (this.type == Type.Height       ||
				this.type == Type.MarginBottom ||
				this.type == Type.MarginTop    ||
				this.type == Type.PaddingRight )  // texte vertical ?
			{
				Point center = box.Center;
				Transform it = graphics.Transform;
				graphics.RotateTransformDeg(-90, center.X, center.Y);
				graphics.AddText(box.Left, box.Bottom, box.Width, box.Height, this.StringValue, Font.DefaultFont, 9.0, ContentAlignment.MiddleCenter);
				graphics.Transform = it;
			}
			else if (this.type == Type.PaddingLeft      ||
					 this.type == Type.GridRow          ||
					 this.type == Type.GridHeight       ||
					 this.type == Type.GridMarginBottom ||
					 this.type == Type.GridMarginTop    )
			{
				Point center = box.Center;
				Transform it = graphics.Transform;
				graphics.RotateTransformDeg(90, center.X, center.Y);
				graphics.AddText(box.Left, box.Bottom, box.Width, box.Height, this.StringValue, Font.DefaultFont, 9.0, ContentAlignment.MiddleCenter);
				graphics.Transform = it;
			}
			else  // texte horizontal ?
			{
				graphics.AddText(box.Left, box.Bottom, box.Width, box.Height, this.StringValue, Font.DefaultFont, 9.0, ContentAlignment.MiddleCenter);
			}

			graphics.RenderSolid(Color.FromRgb(0, 0, 0));
		}

		protected string StringValue
		{
			//	Retourne la chaîne à afficher comme valeur de la cote.
			get
			{
				if (this.IsPercent)
				{
					return string.Concat(this.Value.ToString(System.Globalization.CultureInfo.InvariantCulture), "%");
				}

				switch (this.type)
				{
					case Type.GridColumn:
						return Dimension.ToAlpha(this.columnOrRow);  // A..ZZZ

					case Type.GridRow:
						return (this.columnOrRow+1).ToString(System.Globalization.CultureInfo.InvariantCulture);  // 1..n

					case Type.GridWidthMode:
						return Dimension.ToAlpha(this.objectModifier.GetGridColumnMode(this.obj, this.columnOrRow));

					case Type.GridHeightMode:
						return Dimension.ToAlpha(this.objectModifier.GetGridRowMode(this.obj, this.columnOrRow));

					case Type.GridColumnAddBefore:
					case Type.GridColumnAddAfter:
					case Type.GridRowAddBefore:
					case Type.GridRowAddAfter:
						return "+";

					case Type.GridColumnRemove:
					case Type.GridRowRemove:
						return "−";

					default:
						int i = (int) System.Math.Floor(this.Value+0.5);
						return i.ToString();
				}
			}
		}

		protected static string ToAlpha(int n)
		{
			//	Retourne un nombre en une chaîne en base 26 (donc A..Z, AA..AZ, BA..BZ, etc.).
			string text = "";

			do
			{
				int digit = n%26;
				char c = (char) ('A'+digit);
				text = text.Insert(0, c.ToString());

				n /= 26;
			}
			while (n != 0);

			return text;
		}

		protected static string ToAlpha(ObjectModifier.GridMode mode)
		{
			//	Retourne le mini-texte correspondant au mode de ligne/colonne d'un tableau.
			//	Ce texte est affiché dans un minuscule carré de 12x12 pixels.
			switch (mode)
			{
				case ObjectModifier.GridMode.Auto:          return "●";
				case ObjectModifier.GridMode.Absolute:      return "#";
				case ObjectModifier.GridMode.Proportional:  return "%";
				default:                                    return "";
			}
		}

		protected bool IsSimpleRectangle
		{
			get
			{
				return (this.type == Type.GridColumn         ||
						this.type == Type.GridRow            ||
						this.type == Type.GridWidthMode      ||
						this.type == Type.GridHeightMode     ||
						this.type == Type.GridColumnAddAfter ||
						this.type == Type.GridColumnAddBefore||
						this.type == Type.GridColumnRemove   ||
						this.type == Type.GridRowAddAfter    ||
						this.type == Type.GridRowAddBefore   ||
						this.type == Type.GridRowRemove      );
			}
		}

		protected bool IsPercent
		{
			//	Indique si la cote représente une valeur en pourcents.
			get
			{
				if (this.type == Type.GridWidth &&
					this.objectModifier.GetGridColumnMode(this.obj, this.columnOrRow) == ObjectModifier.GridMode.Proportional)
				{
					return true;
				}

				if (this.type == Type.GridHeight &&
					this.objectModifier.GetGridRowMode(this.obj, this.columnOrRow) == ObjectModifier.GridMode.Proportional)
				{
					return true;
				}

				return false;
			}
		}

		protected bool IsGridSelected
		{
			//	Indique si la ligne/colonne correspondante dans le tableau est sélectionnée.
			get
			{
				if (this.type == Type.GridColumn || this.type == Type.GridRow)
				{
					GridSelection gs = GridSelection.Get(this.obj);
					if (gs != null)
					{
						GridSelection.Unit unit = (this.type == Type.GridColumn) ? GridSelection.Unit.Column : GridSelection.Unit.Row;
						return gs.Search(unit, this.columnOrRow) != -1;
					}
				}

				return false;
			}
		}


		protected Color BackgroundColor
		{
			//	Retourne la couleur de fond.
			get
			{
				switch (this.type)
				{
					case Type.Width:
					case Type.Height:
					case Type.GridWidth:
					case Type.GridHeight:
					case Type.GridWidthMode:
					case Type.GridHeightMode:
						return Color.FromAlphaRgb(0.8, 255.0/255.0, 180.0/255.0, 130.0/255.0);  // rouge

					case Type.MarginLeft:
					case Type.MarginRight:
					case Type.MarginBottom:
					case Type.MarginTop:
					case Type.GridMarginLeft:
					case Type.GridMarginRight:
					case Type.GridMarginBottom:
					case Type.GridMarginTop:
						return Color.FromAlphaRgb(0.8, 255.0/255.0, 220.0/255.0, 130.0/255.0);  // orange

					case Type.PaddingLeft:
					case Type.PaddingRight:
					case Type.PaddingBottom:
					case Type.PaddingTop:
						return Color.FromAlphaRgb(0.8, 255.0/255.0, 255.0/255.0, 130.0/255.0);  // jaune

					case Type.GridColumn:
					case Type.GridRow:
						if (this.IsGridSelected)
						{
							return Color.FromRgb(255.0/255.0, 255.0/255.0, 200.0/255.0);
						}
						else
						{
							return Misc.AlphaColor(PanelsContext.ColorHiliteSurface, 0.8) ;
						}

					case Type.GridColumnAddBefore:
					case Type.GridColumnAddAfter:
					case Type.GridColumnRemove:
					case Type.GridRowAddBefore:
					case Type.GridRowAddAfter:
					case Type.GridRowRemove:
						return Color.FromRgb(255.0/255.0, 255.0/255.0, 200.0/255.0);

					default:
						return Color.FromBrightness(1);
				}
			}
		}

		protected Path GeometryHilitedSurface(Graphics graphics)
		{
			//	Retourne le chemin de la surface pour la mise en évidence.
			Rectangle box = this.GeometryTextBox;
			Misc.AlignForLine(graphics, ref box);
			double t = 20;

			Path path = new Path();

			if (this.IsSimpleRectangle)
			{
				path.AppendRectangle(box);
			}
			else
			{
				path.MoveTo(box.TopLeft);
				path.LineTo(box.Center.X-t/2, box.Top);
				path.LineTo(box.Center.X, box.Top+t/2);  // bosse '^' supérieure, pour '+'
				path.LineTo(box.Center.X+t/2, box.Top);
				path.LineTo(box.TopRight);
				path.LineTo(box.BottomRight);
				path.LineTo(box.Center.X+t/2, box.Bottom);
				path.LineTo(box.Center.X, box.Bottom-t/2);  // bosse 'v' inférieure, pour '-'
				path.LineTo(box.Center.X-t/2, box.Bottom);
				path.LineTo(box.BottomLeft);
				path.Close();
			}

			return path;
		}

		protected Path GeometryBackground(Graphics graphics)
		{
			//	Retourne le chemin de la surface de fond.
			Rectangle bounds = this.objectModifier.GetActualBounds(this.obj);
			Margins margins = this.objectModifier.GetMargins(this.obj);
			Rectangle ext = bounds;
			ext.Inflate(this.objectModifier.GetMargins(this.obj));
			Margins padding = this.objectModifier.GetPadding(this.obj);
			Rectangle inside = this.objectModifier.GetFinalPadding(this.obj);

			Rectangle box = this.GeometryTextBox;
			Misc.AlignForLine(graphics, ref box);
			Misc.AlignForLine(graphics, ref bounds);
			Misc.AlignForLine(graphics, ref ext);
			Misc.AlignForLine(graphics, ref inside);

			Path path = null;
			Rectangle r;
			Point p;

			switch (this.type)
			{
				case Type.Width:
					r = box;
					r.Top = ext.Bottom;
					path = new Path();
					path.AppendRectangle(r);
					break;

				case Type.Height:
					r = box;
					r.Left = ext.Right;
					path = new Path();
					path.AppendRectangle(r);
					break;

				case Type.MarginLeft:
					p = new Point(box.Right, ext.Bottom);
					path = new Path();
					path.MoveTo(p);
					p.Y = box.Bottom;
					path.LineTo(p);
					p.X -= box.Width;
					path.LineTo(p);
					p.Y += box.Height;
					path.LineTo(p);
					p.Y = ext.Bottom;
					p.X = box.Right-margins.Left;
					path.LineTo(p);
					path.Close();
					break;

				case Type.MarginRight:
					p = new Point(box.Left, ext.Bottom);
					path = new Path();
					path.MoveTo(p);
					p.Y = box.Bottom;
					path.LineTo(p);
					p.X += box.Width;
					path.LineTo(p);
					p.Y += box.Height;
					path.LineTo(p);
					p.Y = ext.Bottom;
					p.X = box.Left+margins.Right;
					path.LineTo(p);
					path.Close();
					break;

				case Type.MarginBottom:
					p = new Point(ext.Right, box.Top);
					path = new Path();
					path.MoveTo(p);
					p.X = box.Right;
					path.LineTo(p);
					p.Y -= box.Height;
					path.LineTo(p);
					p.X -= box.Width;
					path.LineTo(p);
					p.X = ext.Right;
					p.Y = box.Top-margins.Bottom;
					path.LineTo(p);
					path.Close();
					break;

				case Type.MarginTop:
					p = new Point(ext.Right, box.Bottom);
					path = new Path();
					path.MoveTo(p);
					p.X = box.Right;
					path.LineTo(p);
					p.Y += box.Height;
					path.LineTo(p);
					p.X -= box.Width;
					path.LineTo(p);
					p.X = ext.Right;
					p.Y = box.Bottom+margins.Top;
					path.LineTo(p);
					path.Close();
					break;

				case Type.PaddingLeft:
					path = new Path();
					path.MoveTo(bounds.Left, inside.Center.Y);
					path.LineTo(box.BottomLeft);
					path.LineTo(box.BottomRight);
					path.LineTo(box.TopRight);
					path.LineTo(box.TopLeft);
					path.Close();
					break;

				case Type.PaddingRight:
					path = new Path();
					path.MoveTo(bounds.Right, inside.Center.Y);
					path.LineTo(box.BottomRight);
					path.LineTo(box.BottomLeft);
					path.LineTo(box.TopLeft);
					path.LineTo(box.TopRight);
					path.Close();
					break;

				case Type.PaddingBottom:
					path = new Path();
					path.MoveTo(inside.Center.X, bounds.Bottom);
					path.LineTo(box.BottomLeft);
					path.LineTo(box.TopLeft);
					path.LineTo(box.TopRight);
					path.LineTo(box.BottomRight);
					path.Close();
					break;

				case Type.PaddingTop:
					path = new Path();
					path.MoveTo(inside.Center.X, bounds.Top);
					path.LineTo(box.TopLeft);
					path.LineTo(box.BottomLeft);
					path.LineTo(box.BottomRight);
					path.LineTo(box.TopRight);
					path.Close();
					break;

				case Type.GridColumn:
					r = box;
					r.Bottom = this.IsGridSelected ? inside.Top : ext.Top;
					path = new Path();
					path.AppendRectangle(r);
					break;

				case Type.GridRow:
					r = box;
					r.Right = this.IsGridSelected ? inside.Left : ext.Left;
					path = new Path();
					path.AppendRectangle(r);
					break;

				case Type.GridWidth:
					r = box;
					r.Bottom = ext.Top;
					path = new Path();
					path.AppendRectangle(r);
					break;

				case Type.GridHeight:
					r = box;
					r.Right = ext.Left;
					path = new Path();
					path.AppendRectangle(r);
					break;

				case Type.GridWidthMode:
					path = new Path();
					path.AppendRectangle(box);
					break;

				case Type.GridHeightMode:
					path = new Path();
					path.AppendRectangle(box);
					break;

				case Type.GridMarginLeft:
					p = new Point(box.Right, ext.Top);
					path = new Path();
					path.MoveTo(p);
					p.Y = box.Top;
					path.LineTo(p);
					p.X -= box.Width;
					path.LineTo(p);
					p.Y -= box.Height;
					path.LineTo(p);
					p.Y = ext.Top;
					p.X = box.Right-this.Value;
					path.LineTo(p);
					path.Close();
					break;

				case Type.GridMarginRight:
					p = new Point(box.Left, ext.Top);
					path = new Path();
					path.MoveTo(p);
					p.Y = box.Top;
					path.LineTo(p);
					p.X += box.Width;
					path.LineTo(p);
					p.Y -= box.Height;
					path.LineTo(p);
					p.Y = ext.Top;
					p.X = box.Left+this.Value;
					path.LineTo(p);
					path.Close();
					break;

				case Type.GridMarginBottom:
					p = new Point(ext.Left, box.Top);
					path = new Path();
					path.MoveTo(p);
					p.X = box.Left;
					path.LineTo(p);
					p.Y -= box.Height;
					path.LineTo(p);
					p.X += box.Width;
					path.LineTo(p);
					p.X = ext.Left;
					p.Y = box.Top-this.Value;
					path.LineTo(p);
					path.Close();
					break;

				case Type.GridMarginTop:
					p = new Point(ext.Left, box.Bottom);
					path = new Path();
					path.MoveTo(p);
					p.X = box.Left;
					path.LineTo(p);
					p.Y += box.Height;
					path.LineTo(p);
					p.X += box.Width;
					path.LineTo(p);
					p.X = ext.Left;
					p.Y = box.Bottom+this.Value;
					path.LineTo(p);
					path.Close();
					break;

				case Type.GridColumnAddBefore:
				case Type.GridColumnAddAfter:
				case Type.GridColumnRemove:
				case Type.GridRowAddBefore:
				case Type.GridRowAddAfter:
				case Type.GridRowRemove:
					path = new Path();
					path.AppendRectangle(box);
					break;
			}

			return path;
		}

		protected Rectangle GeometryTextBox
		{
			//	Retourne le rectangle pour le texte d'une cote.
			get
			{
				Rectangle bounds = this.objectModifier.GetActualBounds(this.obj);
				Margins margins = this.objectModifier.GetMargins(this.obj);
				Rectangle ext = bounds;
				ext.Inflate(this.objectModifier.GetMargins(this.obj));
				Rectangle inside = this.objectModifier.GetFinalPadding(this.obj);

				double d = 26;
				double h = 12;
				double e = 10;
				double pw = 20;
				double ph = 12;
				double l;

				Rectangle box;

				switch (this.type)
				{
					case Type.Width:
						box = bounds;
						box.Bottom = ext.Bottom-d;
						box.Height = h;
						return box;

					case Type.Height:
						box = bounds;
						box.Left = ext.Right+d-h;
						box.Width = h;
						return box;

					case Type.MarginLeft:
						l = System.Math.Max(e, margins.Left);
						return new Rectangle(bounds.Left-l, ext.Bottom-d, l, h);

					case Type.MarginRight:
						l = System.Math.Max(e, margins.Right);
						return new Rectangle(bounds.Right, ext.Bottom-d, l, h);

					case Type.MarginTop:
						l = System.Math.Max(e, margins.Top);
						return new Rectangle(ext.Right+d-h, bounds.Top, h, l);

					case Type.MarginBottom:
						l = System.Math.Max(e, margins.Bottom);
						return new Rectangle(ext.Right+d-h, bounds.Bottom-l, h, l);

					case Type.PaddingLeft:
						box = this.objectModifier.GetFinalPadding(this.obj);
						return new Rectangle(inside.Left, inside.Center.Y-pw/2, ph, pw);

					case Type.PaddingRight:
						box = this.objectModifier.GetFinalPadding(this.obj);
						return new Rectangle(inside.Right-ph, inside.Center.Y-pw/2, ph, pw);

					case Type.PaddingTop:
						box = this.objectModifier.GetFinalPadding(this.obj);
						return new Rectangle(inside.Center.X-pw/2, inside.Top-ph, pw, ph);

					case Type.PaddingBottom:
						box = this.objectModifier.GetFinalPadding(this.obj);
						return new Rectangle(inside.Center.X-pw/2, inside.Bottom, pw, ph);

					case Type.GridColumn:
						box = this.objectModifier.GetGridCellArea(this.obj, this.columnOrRow, 0, 1, 1);
						box.Bottom = ext.Top+d;
						box.Height = h;
						return box;

					case Type.GridRow:
						box = this.objectModifier.GetGridCellArea(this.obj, 0, this.columnOrRow, 1, 1);
						box.Left = ext.Left-d-h;
						box.Width = h;
						return box;

					case Type.GridWidth:
						box = this.objectModifier.GetGridCellArea(this.obj, this.columnOrRow, 0, 1, 1);
						box.Left  += this.objectModifier.GetGridColumnLeftBorder(this.obj, this.columnOrRow);
						box.Right -= this.objectModifier.GetGridColumnRightBorder(this.obj, this.columnOrRow);
						box.Bottom = ext.Top+d-h;
						box.Height = h;
						return box;

					case Type.GridHeight:
						box = this.objectModifier.GetGridCellArea(this.obj, 0, this.columnOrRow, 1, 1);
						box.Bottom += this.objectModifier.GetGridRowBottomBorder(this.obj, this.columnOrRow);
						box.Top    -= this.objectModifier.GetGridRowTopBorder(this.obj, this.columnOrRow);
						box.Left = ext.Left-d;
						box.Width = h;
						return box;

					case Type.GridWidthMode:
						box = this.objectModifier.GetGridCellArea(this.obj, this.columnOrRow, 0, 1, 1);
						box.Left = box.Center.X-h/2;
						box.Width = h;
						box.Bottom = ext.Top;
						box.Height = h;
						return box;

					case Type.GridHeightMode:
						box = this.objectModifier.GetGridCellArea(this.obj, 0, this.columnOrRow, 1, 1);
						box.Bottom = box.Center.Y-h/2;
						box.Height = h;
						box.Left = ext.Left-h;
						box.Width = h;
						return box;

					case Type.GridMarginLeft:
						box = this.objectModifier.GetGridCellArea(this.obj, this.columnOrRow, 0, 1, 1);
						box.Bottom = ext.Top+d-h;
						box.Height = h;
						l = System.Math.Max(e, this.Value);
						box.Left += this.objectModifier.GetGridColumnLeftBorder(this.obj, this.columnOrRow);
						box.Left -= l;
						box.Width = l;
						return box;

					case Type.GridMarginRight:
						box = this.objectModifier.GetGridCellArea(this.obj, this.columnOrRow, 0, 1, 1);
						box.Bottom = ext.Top+d-h;
						box.Height = h;
						l = System.Math.Max(e, this.Value);
						box.Right -= this.objectModifier.GetGridColumnRightBorder(this.obj, this.columnOrRow);
						box.Left = box.Right;
						box.Width = l;
						return box;

					case Type.GridMarginTop:
						box = this.objectModifier.GetGridCellArea(this.obj, 0, this.columnOrRow, 1, 1);
						box.Left = ext.Left-d;
						box.Width = h;
						l = System.Math.Max(e, this.Value);
						box.Top -= this.objectModifier.GetGridRowTopBorder(this.obj, this.columnOrRow);
						box.Bottom = box.Top;
						box.Height = l;
						return box;

					case Type.GridMarginBottom:
						box = this.objectModifier.GetGridCellArea(this.obj, 0, this.columnOrRow, 1, 1);
						box.Left = ext.Left-d;
						box.Width = h;
						l = System.Math.Max(e, this.Value);
						box.Bottom += this.objectModifier.GetGridRowBottomBorder(this.obj, this.columnOrRow);
						box.Bottom -= l;
						box.Height = l;
						return box;

					case Type.GridColumnRemove:
						box = this.objectModifier.GetGridCellArea(this.obj, this.columnOrRow, 0, 1, 1);
						box.Bottom = ext.Top+d;
						box.Height = h;
						box.Left -= 10*2;
						box.Width = 10;
						return box;

					case Type.GridColumnAddBefore:
						box = this.objectModifier.GetGridCellArea(this.obj, this.columnOrRow, 0, 1, 1);
						box.Bottom = ext.Top+d;
						box.Height = h;
						box.Left -= 10;
						box.Width = 10;
						return box;

					case Type.GridColumnAddAfter:
						box = this.objectModifier.GetGridCellArea(this.obj, this.columnOrRow, 0, 1, 1);
						box.Bottom = ext.Top+d;
						box.Height = h;
						box.Left = box.Right;
						box.Width = 10;
						return box;

					case Type.GridRowRemove:
						box = this.objectModifier.GetGridCellArea(this.obj, 0, this.columnOrRow, 1, 1);
						box.Left = ext.Left-d-h;
						box.Width = h;
						box.Bottom = box.Top+10;
						box.Height = 10;
						return box;

					case Type.GridRowAddBefore:
						box = this.objectModifier.GetGridCellArea(this.obj, 0, this.columnOrRow, 1, 1);
						box.Left = ext.Left-d-h;
						box.Width = h;
						box.Bottom = box.Top;
						box.Height = 10;
						return box;

					case Type.GridRowAddAfter:
						box = this.objectModifier.GetGridCellArea(this.obj, 0, this.columnOrRow, 1, 1);
						box.Left = ext.Left-d-h;
						box.Width = h;
						box.Bottom -= 10;
						box.Height = 10;
						return box;

					default:
						return Rectangle.Empty;
				}
			}
		}


		public static readonly double		margin = 38;
		protected static readonly double	attachmentThickness = 2.0;
		protected static readonly double	attachmentScale = 0.3;

		protected MyWidgets.PanelEditor		editor;
		protected ObjectModifier			objectModifier;
		protected PanelsContext				context;
		protected Widget					obj;
		protected Type						type;
		protected int						columnOrRow;
	}
}

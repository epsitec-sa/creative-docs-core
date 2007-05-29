using System.Collections.Generic;
using Epsitec.Common.Widgets;
using Epsitec.Common.Support;
using Epsitec.Common.Drawing;
using Epsitec.Common.Types;

namespace Epsitec.Common.Designer.EntitiesEditor
{
	/// <summary>
	/// Widget permettant d'�diter graphiquement des entit�s.
	/// </summary>
	public class Editor : Widget
	{
		protected enum PushDirection
		{
			Automatic,
			Left,
			Right,
			Bottom,
			Top,
		}


		public Editor()
		{
			this.boxes = new List<ObjectBox>();
			this.connections = new List<ObjectConnection>();
			this.zoom = 1;
			this.areaOffset = Point.Zero;
		}

		public Editor(Widget embedder) : this()
		{
			this.SetEmbedder(embedder);
		}
		
		
		protected override void Dispose(bool disposing)
		{
			if ( disposing )
			{
			}
			
			base.Dispose(disposing);
		}


		public Module Module
		{
			get
			{
				return this.module;
			}
			set
			{
				this.module = value;
			}
		}


		public void AddBox(ObjectBox box)
		{
			box.Bounds = new Rectangle(20+(Editor.defaultWidth+40)*this.boxes.Count, this.areaSize.Height-20-30*this.boxes.Count-100, Editor.defaultWidth, 100);
			box.IsExtended = (this.boxes.Count == 0);

			this.boxes.Add(box);
		}

		public void AddConnection(ObjectConnection connection)
		{
			this.connections.Add(connection);
		}

		public void Clear()
		{
			this.boxes.Clear();
			this.connections.Clear();
		}


		public Size AreaSize
		{
			//	Dimensions de la surface pour repr�senter les bo�tes et les liaisons.
			get
			{
				return this.areaSize;
			}
			set
			{
				if (this.areaSize != value)
				{
					this.areaSize = value;
					this.Invalidate();
				}
			}
		}

		public double Zoom
		{
			//	Zoom pour repr�senter les bo�tes et les liaisons.
			get
			{
				return this.zoom;
			}
			set
			{
				if (this.zoom != value)
				{
					this.zoom = value;
					this.Invalidate();
				}
			}
		}

		public Point AreaOffset
		{
			//	Offset de la zone visible, d�termin�e par les ascenseurs.
			get
			{
				return this.areaOffset;
			}
			set
			{
				if (this.areaOffset != value)
				{
					this.areaOffset = value;
					this.Invalidate();
				}
			}
		}


		public void UpdateGeometry()
		{
			//	Met � jour la g�om�trie de toutes les bo�tes et de toutes les liaisons.
			this.UpdateBoxes();
			this.UpdateConnections();
		}

		public void UpdateAfterGeometryChanged(ObjectBox box)
		{
			//	Appel� lorsque la g�om�trie d'une bo�te a chang� (changement compact/�tendu).
			this.UpdateBoxes();  // adapte la taille selon compact/�tendu
			
			this.PushBoxesInside(Editor.pushMargin);
			this.PushLayout(box, PushDirection.Automatic, Editor.pushMargin);
			this.PushBoxesInside(Editor.pushMargin);
			this.UpdateConnections();
		}

		protected void UpdateBoxes()
		{
			//	Met � jour la g�om�trie de toutes les bo�tes.
			foreach (ObjectBox box in this.boxes)
			{
				Rectangle bounds = box.Bounds;
				double top = bounds.Top;
				double h = box.GetBestHeight();
				bounds.Bottom = top-h;
				bounds.Height = h;
				box.Bounds = bounds;
			}
		}

		protected void UpdateConnections()
		{
			//	Met � jour la g�om�trie de toutes les liaisons.
#if false
			//	TODO: provisoire !
			this.UpdateConnection(this.connections[0], this.boxes[0], 2, this.boxes[1], FieldRelation.Reference);  // lien client
			this.UpdateConnection(this.connections[1], this.boxes[0], 3, this.boxes[2], FieldRelation.Collection);  // lien articles
			this.UpdateConnection(this.connections[2], this.boxes[0], 5, this.boxes[3], FieldRelation.Inclusion);  // lien rabais
#endif

			this.connections.Clear();

			foreach (ObjectBox box in this.boxes)
			{
				for (int i=0; i<box.Fields.Count; i++)
				{
					ObjectBox.Field field = box.Fields[i];

					if (field.Relation != FieldRelation.None)
					{
						double posv = box.GetConnectionVerticalPosition(i);
						Point pos = new Point(box.Bounds.Right-1, posv);

						ObjectConnection connection = new ObjectConnection(this);
						connection.Points.Add(pos);
						this.AddConnection(connection);
					}
				}
			}

			this.Invalidate();
		}

		protected void UpdateConnection(ObjectConnection connection, ObjectBox src, int srcRank, ObjectBox dst, FieldRelation relation)
		{
			//	Met � jour la g�om�trie d'une liaison.
			connection.Bounds = this.Client.Bounds;
			connection.Relation = relation;

			Rectangle srcBounds = src.Bounds;
			Rectangle dstBounds = dst.Bounds;

			//	Calcul des rectangles plus petits, pour les tests d'intersections.
			Rectangle srcBoundsLittle = srcBounds;
			Rectangle dstBoundsLittle = dstBounds;
			srcBoundsLittle.Deflate(2);
			dstBoundsLittle.Deflate(2);

			connection.Points.Clear();

			double v = src.GetConnectionVerticalPosition(srcRank);
			if (!double.IsNaN(v) && !srcBounds.IntersectsWith(dstBounds))
			{
				Point p = new Point(0, v);

				if (dstBounds.Center.X > srcBounds.Right+Editor.connectionDetour)  // destination � droite ?
				{
					Point start = new Point(srcBounds.Right-1, p.Y);
					connection.Points.Add(start);

					if (dstBounds.Top < start.Y-Editor.connectionDetour)  // destination plus basse ?
					{
						Point end = dst.GetConnectionDestination(start.Y, ObjectBox.ConnectionAnchor.Top);
						connection.Points.Add(new Point(end.X, start.Y));
						connection.Points.Add(end);
					}
					else if (dstBounds.Bottom > start.Y+Editor.connectionDetour)  // destination plus haute ?
					{
						Point end = dst.GetConnectionDestination(start.Y, ObjectBox.ConnectionAnchor.Bottom);
						connection.Points.Add(new Point(end.X, start.Y));
						connection.Points.Add(end);
					}
					else
					{
						Point end = dst.GetConnectionDestination(start.Y, ObjectBox.ConnectionAnchor.Left);
						if (start.Y != end.Y && end.X-start.X > Editor.connectionDetour)
						{
							connection.Points.Add(new Point((start.X+end.X)/2, start.Y));
							connection.Points.Add(new Point((start.X+end.X)/2, end.Y));
						}
						connection.Points.Add(end);
					}
				}
				else if (dstBounds.Center.X < srcBounds.Left-Editor.connectionDetour)  // destination � gauche ?
				{
					Point start = new Point(srcBounds.Left+1, p.Y);
					connection.Points.Add(start);

					if (dstBounds.Top < start.Y-Editor.connectionDetour)  // destination plus basse ?
					{
						Point end = dst.GetConnectionDestination(start.Y, ObjectBox.ConnectionAnchor.Top);
						connection.Points.Add(new Point(end.X, start.Y));
						connection.Points.Add(end);
					}
					else if (dstBounds.Bottom > start.Y+Editor.connectionDetour)  // destination plus haute ?
					{
						Point end = dst.GetConnectionDestination(start.Y, ObjectBox.ConnectionAnchor.Bottom);
						connection.Points.Add(new Point(end.X, start.Y));
						connection.Points.Add(end);
					}
					else
					{
						Point end = dst.GetConnectionDestination(start.Y, ObjectBox.ConnectionAnchor.Right);
						if (start.Y != end.Y && start.X-end.X > Editor.connectionDetour)
						{
							connection.Points.Add(new Point((start.X+end.X)/2, start.Y));
							connection.Points.Add(new Point((start.X+end.X)/2, end.Y));
						}
						connection.Points.Add(end);
					}
				}
				else if (dstBounds.Center.X > srcBounds.Center.X)  // destination � droite � cheval ?
				{
					Point start = new Point(srcBounds.Right-1, p.Y);
					connection.Points.Add(start);

					Point end = dst.GetConnectionDestination(start.Y, ObjectBox.ConnectionAnchor.Right);
					double posx = System.Math.Max(start.X, end.X)+Editor.connectionDetour;
					connection.Points.Add(new Point(posx, start.Y));
					connection.Points.Add(new Point(posx, end.Y));
					connection.Points.Add(end);
				}
				else  // destination � gauche � cheval ?
				{
					Point start = new Point(srcBounds.Left+1, p.Y);
					connection.Points.Add(start);

					Point end = dst.GetConnectionDestination(start.Y, ObjectBox.ConnectionAnchor.Left);
					double posx = System.Math.Min(start.X, end.X)-Editor.connectionDetour;
					connection.Points.Add(new Point(posx, start.Y));
					connection.Points.Add(new Point(posx, end.Y));
					connection.Points.Add(end);
				}
			}
		}


		protected void PushLayout(ObjectBox exclude, PushDirection direction, double margin)
		{
			//	Pousse les bo�tes pour �viter tout chevauchement.
			//	Une bo�te peut �tre pouss�e hors de la surface de dessin.
			for (int max=0; max<100; max++)
			{
				bool push = false;

				for (int i=0; i<this.boxes.Count; i++)
				{
					ObjectBox box = this.boxes[i];

					ObjectBox inter = this.PushSearch(box, exclude, margin);
					if (inter != null)
					{
						push = true;
						this.PushAction(box, inter, direction, margin);
						this.PushLayout(inter, direction, margin);
					}
				}

				if (!push)
				{
					break;
				}
			}
		}

		protected ObjectBox PushSearch(ObjectBox box, ObjectBox exclude, double margin)
		{
			//	Cherche une bo�te qui chevauche 'box'.
			Rectangle rect = box.Bounds;
			rect.Inflate(margin);

			for (int i=0; i<this.boxes.Count; i++)
			{
				ObjectBox obj = this.boxes[i];

				if (obj != box && obj != exclude)
				{
					if (obj.Bounds.IntersectsWith(rect))
					{
						return obj;
					}
				}
			}

			return null;
		}

		protected void PushAction(ObjectBox box, ObjectBox inter, PushDirection direction, double margin)
		{
			//	Pousse 'inter' pour venir apr�s 'box' selon la direction choisie.
			Rectangle rect = inter.Bounds;

			double dr = box.Bounds.Right - rect.Left + margin;
			double dl = rect.Right - box.Bounds.Left + margin;
			double dt = box.Bounds.Top - rect.Bottom + margin;
			double db = rect.Top - box.Bounds.Bottom + margin;

			if (direction == PushDirection.Automatic)
			{
				double min = System.Math.Min(System.Math.Min(dr, dl), System.Math.Min(dt, db));

					 if (min == dr)  direction = PushDirection.Right;
				else if (min == dl)  direction = PushDirection.Left;
				else if (min == dt)  direction = PushDirection.Top;
				else                 direction = PushDirection.Bottom;
			}

			if (direction == PushDirection.Right)
			{
				rect.Offset(dr, 0);
			}

			if (direction == PushDirection.Left)
			{
				rect.Offset(-dl, 0);
			}

			if (direction == PushDirection.Top)
			{
				rect.Offset(0, dt);
			}

			if (direction == PushDirection.Bottom)
			{
				rect.Offset(0, -db);
			}

			inter.Bounds = rect;
		}


		protected void PushBoxesInside(double margin)
		{
			//	Remet les bo�tes dans la surface de dessin.
			for (int i=0; i<this.boxes.Count; i++)
			{
				ObjectBox box = this.boxes[i];
				Rectangle bounds = box.Bounds;

				if (bounds.Left < margin)
				{
					bounds.Offset(margin-bounds.Left, 0);
				}

				if (bounds.Right > this.areaSize.Width-margin)
				{
					bounds.Offset(this.areaSize.Width-margin-bounds.Right, 0);
				}

				if (bounds.Bottom < margin)
				{
					bounds.Offset(0, margin-bounds.Bottom);
				}

				if (bounds.Top > this.areaSize.Height-margin)
				{
					bounds.Offset(0, this.areaSize.Height-margin-bounds.Top);
				}

				if (bounds != box.Bounds)
				{
					box.Bounds = bounds;
					this.PushLayout(box, PushDirection.Automatic, Editor.pushMargin);
				}
			}
		}

		protected void RecenterBoxes(double margin)
		{
			//	Si des bo�tes d�passent de la surface de dessin, recentre le tout.
			Rectangle bounds = this.ComputeBoxBounds();

			if (bounds.Left < margin)
			{
				this.MoveBoxes(margin-bounds.Left, 0);
			}

			if (bounds.Right > this.areaSize.Width-margin)
			{
				this.MoveBoxes(this.areaSize.Width-margin-bounds.Right, 0);
			}

			if (bounds.Bottom < margin)
			{
				this.MoveBoxes(0, margin-bounds.Bottom);
			}

			if (bounds.Top > this.areaSize.Height-margin)
			{
				this.MoveBoxes(0, this.areaSize.Height-margin-bounds.Top);
			}
		}

		protected Rectangle ComputeBoxBounds()
		{
			//	Retourne le rectangle englobant toutes les bo�tes.
			Rectangle bounds = Rectangle.Empty;

			for (int i=0; i<this.boxes.Count; i++)
			{
				ObjectBox box = this.boxes[i];
				bounds = Rectangle.Union(bounds, box.Bounds);
			}

			return bounds;
		}

		protected void MoveBoxes(double dx, double dy)
		{
			//	D�place toutes les bo�tes.
			for (int i=0; i<this.boxes.Count; i++)
			{
				ObjectBox box = this.boxes[i];

				Rectangle bounds = box.Bounds;
				bounds.Offset(dx, dy);
				box.Bounds = bounds;
			}
		}


		protected override void ProcessMessage(Message message, Point pos)
		{
			pos = this.ConvWidgetToEditor(pos);

			switch (message.MessageType)
			{
				case MessageType.MouseMove:
					if (this.isDragging)
					{
						this.MouseDraggingMove(pos);
					}
					else
					{
						this.MouseHilite(pos);
					}
					message.Consumer = this;
					break;

				case MessageType.MouseDown:
					this.MouseDown(pos);
					message.Consumer = this;
					break;

				case MessageType.MouseUp:
					this.MouseUp(pos);
					message.Consumer = this;
					break;
			}
		}

		protected Point ConvWidgetToEditor(Point pos)
		{
			//	Conversion d'une coordonn�e dans l'espace normal des widgets vers l'espace de l'�diteur,
			//	qui varie selon les ascenseurs (AreaOffset) et le zoom.
			pos.Y = this.Client.Size.Height-pos.Y;
			pos /= this.zoom;
			pos += this.areaOffset;
			pos.Y = this.areaSize.Height-pos.Y;

			return pos;
		}

		protected Point ConvEditorToWidget(Point pos)
		{
			//	Conversion d'une coordonn�e dans l'espace de l'�diteur vers l'espace normal des widgets.
			pos.Y = this.areaSize.Height-pos.Y;
			pos -= this.areaOffset;
			pos *= this.zoom;
			pos.Y = this.Client.Size.Height-pos.Y;

			return pos;
		}

		protected void MouseHilite(Point pos)
		{
			//	Met en �vidence tous les widgets selon la position vis�e par la souris.
			//	L'objet � l'avant-plan a la priorit�.
			for (int i=this.connections.Count-1; i>=0; i--)
			{
				AbstractObject obj = this.connections[i];

				if (obj.MouseHilite(pos))
				{
					pos = Point.Zero;  // si on �tait dans cet objet -> plus aucun hilite pour les objets plac�s dessous
				}
			}

			for (int i=this.boxes.Count-1; i>=0; i--)
			{
				AbstractObject obj = this.boxes[i];

				if (obj.MouseHilite(pos))
				{
					pos = Point.Zero;  // si on �tait dans cet objet -> plus aucun hilite pour les objets plac�s dessous
				}
			}
		}

		protected void MouseDown(Point pos)
		{
			//	D�but du d�placement d'une bo�te.
			ObjectBox box = this.DetectBox(pos);

			if (box != null)
			{
				if (box.IsReadyForDragging)
				{
					this.draggingBox = box;
					this.draggingPos = pos;
					this.isDragging = true;
				}
				else
				{
					box.MouseDown(pos);
				}
			}
		}

		protected void MouseDraggingMove(Point pos)
		{
			//	D�placement d'une bo�te.
			Rectangle bounds = this.draggingBox.Bounds;

			bounds.Offset(pos-this.draggingPos);
			this.draggingPos = pos;

			this.draggingBox.Bounds = bounds;
			this.UpdateConnections();
		}

		protected void MouseUp(Point pos)
		{
			//	Fin du d�placement d'une bo�te.
			if (this.isDragging)
			{
				this.PushBoxesInside(Editor.pushMargin);
				this.PushLayout(this.draggingBox, PushDirection.Automatic, Editor.pushMargin);
				this.RecenterBoxes(Editor.pushMargin);
				this.PushBoxesInside(Editor.pushMargin);
				this.UpdateConnections();

				this.draggingBox = null;
				this.isDragging = false;
			}
			else
			{
				ObjectBox box = this.DetectBox(pos);
				if (box != null)
				{
					box.MouseUp(pos);
				}
			}
		}

		protected ObjectBox DetectBox(Point pos)
		{
			//	D�tecte la bo�te vis�e par la souris.
			//	La bo�te � l'avant-plan a la priorit�.
			for (int i=this.boxes.Count-1; i>=0; i--)
			{
				ObjectBox box = this.boxes[i];

				if (box.Bounds.Contains(pos))
				{
					return box;
				}
			}

			return null;
		}



		protected override void PaintBackgroundImplementation(Graphics graphics, Rectangle clipRect)
		{
			IAdorner adorner = Common.Widgets.Adorners.Factory.Active;
			Rectangle rect;

			Transform initialTransform = graphics.Transform;
			graphics.TranslateTransform(-this.areaOffset.X*this.zoom, this.Client.Bounds.Height-(this.areaSize.Height-this.areaOffset.Y)*this.zoom);
			graphics.ScaleTransform(this.zoom, this.zoom, 0, 0);

			//	Dessine la surface de dessin.
			rect = new Rectangle(0, 0, this.areaSize.Width, this.areaSize.Height);
			graphics.AddFilledRectangle(rect);  // surface de dessin
			graphics.RenderSolid(Color.FromBrightness(1));

			//	Dessine les surfaces hors de la zone utile.
			Point bl = this.ConvWidgetToEditor(this.Client.Bounds.BottomLeft);
			Point tr = this.ConvWidgetToEditor(this.Client.Bounds.TopRight);

			rect = new Rectangle(this.areaSize.Width, bl.Y, tr.X-this.areaSize.Width, tr.Y-bl.Y);
			if (!rect.IsSurfaceZero)
			{
				graphics.AddFilledRectangle(rect);  // � droite
			}
			
			rect = new Rectangle(0, bl.Y, this.areaSize.Width, -bl.Y);
			if (!rect.IsSurfaceZero)
			{
				graphics.AddFilledRectangle(rect);  // en bas
			}

			Color colorOver = adorner.ColorBorder;
			colorOver.A = 0.3;
			graphics.RenderSolid(colorOver);

			//	Dessine tous les objets.
			foreach (AbstractObject obj in this.boxes)
			{
				obj.Draw(graphics);
			}

			foreach (AbstractObject obj in this.connections)
			{
				obj.Draw(graphics);
			}

			graphics.Transform = initialTransform;

			//	Dessine le cadre.
			rect = this.Client.Bounds;
			rect.Deflate(0.5);
			graphics.AddRectangle(rect);
			graphics.RenderSolid(adorner.ColorBorder);
		}



		protected static readonly double defaultWidth = 180;
		protected static readonly double connectionDetour = 30;
		protected static readonly double pushMargin = 10;

		protected Module module;
		protected List<ObjectBox> boxes;
		protected List<ObjectConnection> connections;
		protected Size areaSize;
		protected double zoom;
		protected Point areaOffset;
		protected bool isDragging;
		protected Point draggingPos;
		protected ObjectBox draggingBox;
	}
}

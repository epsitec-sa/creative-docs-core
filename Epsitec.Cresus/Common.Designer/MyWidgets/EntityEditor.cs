using System.Collections.Generic;
using Epsitec.Common.Widgets;
using Epsitec.Common.Support;
using Epsitec.Common.Drawing;
using Epsitec.Common.Types;

namespace Epsitec.Common.Designer.MyWidgets
{
	/// <summary>
	/// Widget permettant d'�diter graphiquement des entit�s.
	/// </summary>
	public class EntityEditor : Widget
	{
		protected enum PushDirection
		{
			Automatic,
			Left,
			Right,
			Bottom,
			Top,
		}


		public EntityEditor()
		{
			this.boxes = new List<MyWidgets.EntityBox>();
			this.connections = new List<MyWidgets.EntityConnection>();
		}

		public EntityEditor(Widget embedder) : this()
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


		public void AddBox(MyWidgets.EntityBox box)
		{
			box.GeometryChanged += new EventHandler(this.HandleBoxGeometryChanged);
			box.SetParent(this);
			box.SetManualBounds(new Rectangle(20+(EntityEditor.defaultWidth+40)*this.boxes.Count, this.ActualHeight-20-30*this.boxes.Count-100, EntityEditor.defaultWidth, 100));

			this.boxes.Add(box);
		}

		public void AddConnection(MyWidgets.EntityConnection connection)
		{
			connection.SetParent(this);
			this.connections.Add(connection);
		}


		public void UpdateGeometry()
		{
			//	Met � jour la g�om�trie de toutes les bo�tes et de toutes les liaisons.
			this.UpdateBoxes();
			this.UpdateConnections();
		}

		protected void UpdateBoxes()
		{
			//	Met � jour la g�om�trie de toutes les bo�tes.
			foreach (MyWidgets.EntityBox box in this.boxes)
			{
				Rectangle bounds = box.ActualBounds;
				double top = bounds.Top;
				double h = box.GetBestHeight();
				bounds.Bottom = top-h;
				bounds.Height = h;
				box.SetManualBounds(bounds);
			}
		}

		protected void UpdateConnections()
		{
			//	Met � jour la g�om�trie de toutes les liaisons.
			//	TODO: provisoire !
			this.UpdateConnection(this.connections[0], this.boxes[0], 2, this.boxes[1], FieldRelation.Reference);  // lien client
			this.UpdateConnection(this.connections[1], this.boxes[0], 3, this.boxes[2], FieldRelation.Collection);  // lien articles
			this.UpdateConnection(this.connections[2], this.boxes[0], 5, this.boxes[3], FieldRelation.Inclusion);  // lien rabais
			this.Invalidate();
		}

		protected void UpdateConnection(MyWidgets.EntityConnection connection, MyWidgets.EntityBox src, int srcRank, MyWidgets.EntityBox dst, FieldRelation relation)
		{
			//	Met � jour la g�om�trie d'une liaison.
			connection.SetManualBounds(this.Client.Bounds);
			connection.Relation = relation;

			Rectangle srcBounds = src.ActualBounds;
			Rectangle dstBounds = dst.ActualBounds;

			//	Calcul des rectangles plus petits, pour les tests d'intersections.
			Rectangle srcBoundsLittle = srcBounds;
			Rectangle dstBoundsLittle = dstBounds;
			srcBoundsLittle.Deflate(2);
			dstBoundsLittle.Deflate(2);

			double v = src.GetConnectionVerticalPosition(srcRank);
			if (double.IsNaN(v))
			{
				connection.Visibility = false;
			}
			else
			{
				connection.Visibility = true;
				connection.Points.Clear();

				Point p = new Point(0, v);
				p = src.MapClientToParent(p);

#if true
				if (dstBounds.Center.X > srcBounds.Right+EntityEditor.connectionDetour)  // destination � droite ?
				{
					Point start = new Point(srcBounds.Right-1, p.Y);
					connection.Points.Add(start);

					if (dstBounds.Top < start.Y-EntityEditor.connectionDetour)  // destination plus basse ?
					{
						Point end = dst.GetConnectionDestination(start.Y, EntityBox.ConnectionAnchor.Top);
						connection.Points.Add(new Point(end.X, start.Y));
						connection.Points.Add(end);
					}
					else if (dstBounds.Bottom > start.Y+EntityEditor.connectionDetour)  // destination plus haute ?
					{
						Point end = dst.GetConnectionDestination(start.Y, EntityBox.ConnectionAnchor.Bottom);
						connection.Points.Add(new Point(end.X, start.Y));
						connection.Points.Add(end);
					}
					else
					{
						Point end = dst.GetConnectionDestination(start.Y, EntityBox.ConnectionAnchor.Left);
						if (start.Y != end.Y && end.X-start.X > EntityEditor.connectionDetour)
						{
							connection.Points.Add(new Point((start.X+end.X)/2, start.Y));
							connection.Points.Add(new Point((start.X+end.X)/2, end.Y));
						}
						connection.Points.Add(end);
					}
				}
				else if (dstBounds.Center.X < srcBounds.Left-EntityEditor.connectionDetour)  // destination � gauche ?
				{
					Point start = new Point(srcBounds.Left+1, p.Y);
					connection.Points.Add(start);

					if (dstBounds.Top < start.Y-EntityEditor.connectionDetour)  // destination plus basse ?
					{
						Point end = dst.GetConnectionDestination(start.Y, EntityBox.ConnectionAnchor.Top);
						connection.Points.Add(new Point(end.X, start.Y));
						connection.Points.Add(end);
					}
					else if (dstBounds.Bottom > start.Y+EntityEditor.connectionDetour)  // destination plus haute ?
					{
						Point end = dst.GetConnectionDestination(start.Y, EntityBox.ConnectionAnchor.Bottom);
						connection.Points.Add(new Point(end.X, start.Y));
						connection.Points.Add(end);
					}
					else
					{
						Point end = dst.GetConnectionDestination(start.Y, EntityBox.ConnectionAnchor.Right);
						if (start.Y != end.Y && start.X-end.X > EntityEditor.connectionDetour)
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

					Point end = dst.GetConnectionDestination(start.Y, EntityBox.ConnectionAnchor.Right);
					double posx = System.Math.Max(start.X, end.X)+EntityEditor.connectionDetour;
					connection.Points.Add(new Point(posx, start.Y));
					connection.Points.Add(new Point(posx, end.Y));
					connection.Points.Add(end);
				}
				else  // destination � gauche � cheval ?
				{
					Point start = new Point(srcBounds.Left+1, p.Y);
					connection.Points.Add(start);

					Point end = dst.GetConnectionDestination(start.Y, EntityBox.ConnectionAnchor.Left);
					double posx = System.Math.Min(start.X, end.X)-EntityEditor.connectionDetour;
					connection.Points.Add(new Point(posx, start.Y));
					connection.Points.Add(new Point(posx, end.Y));
					connection.Points.Add(end);
				}
#endif

#if false
				double min = double.MaxValue;
				MyWidgets.EntityBox.ConnectionAnchor best = EntityBox.ConnectionAnchor.Left;
				bool right = true;
				bool over = false;
				Point bestEnd = Point.Zero;

				//	Cherche la meilleure liaison possible, parmi huit possibilit�s.
				foreach (MyWidgets.EntityBox.ConnectionAnchor anchor in System.Enum.GetValues(typeof(MyWidgets.EntityBox.ConnectionAnchor)))
				{
					Point end = dst.GetConnectionDestination(p.Y, anchor);
					Point start;
					double d;
					bool bad;

					start = new Point(srcBounds.Right-1, p.Y);
					d = Point.Distance(start, end);
					bad = false;
					if (Geometry.IsOver(srcBoundsLittle, start, end, 20))
					{
						d *= 20;  // tr�s mauvais si chevauchement avec la source
						bad = true;
					}
					if (Geometry.IsOver(dstBoundsLittle, start, end, 20))
					{
						d *= 10;  // mauvais si chevauchement avec la destination
						bad = true;
					}
					if (d < min)
					{
						min = d;
						best = anchor;
						right = true;
						over = bad;
						bestEnd = end;
					}

					start = new Point(srcBounds.Left+1, p.Y);
					d = Point.Distance(start, end);
					bad = false;
					if (Geometry.IsOver(srcBoundsLittle, start, end, 20))
					{
						d *= 20;  // tr�s mauvais si chevauchement avec la source
						bad = true;
					}
					if (Geometry.IsOver(dstBoundsLittle, start, end, 20))
					{
						d *= 10;  // mauvais si chevauchement avec la destination
						bad = true;
					}
					if (d < min)
					{
						min = d;
						best = anchor;
						right = false;
						over = bad;
						bestEnd = end;
					}
				}

				connection.Points.Add(new Point(right ? srcBounds.Right-1 : srcBounds.Left+1, p.Y));

				if (over)
				{
					if (right)
					{
						double posx = System.Math.Max(srcBounds.Right, bestEnd.X)+EntityEditor.connectionDetour;

						double ov = 0;
						if (best == EntityBox.ConnectionAnchor.Bottom)  ov = -EntityEditor.connectionDetour;
						if (best == EntityBox.ConnectionAnchor.Top   )  ov =  EntityEditor.connectionDetour;

						connection.Points.Add(new Point(posx, p.Y));
						connection.Points.Add(new Point(posx, bestEnd.Y+ov));
					}
					else
					{
						double posx = System.Math.Min(srcBounds.Left, bestEnd.X)-EntityEditor.connectionDetour;

						double ov = 0;
						if (best == EntityBox.ConnectionAnchor.Bottom)  ov = -EntityEditor.connectionDetour;
						if (best == EntityBox.ConnectionAnchor.Top   )  ov =  EntityEditor.connectionDetour;

						connection.Points.Add(new Point(posx, p.Y));
						connection.Points.Add(new Point(posx, bestEnd.Y+ov));
					}
				}

				connection.Points.Add(bestEnd);
#endif
			}
		}


		protected void PushLayout(MyWidgets.EntityBox exclude, PushDirection direction, double margin)
		{
			//	Pousse les bo�tes pour �viter tout chevauchement.
			for (int max=0; max<100; max++)
			{
				bool push = false;

				for (int i=0; i<this.Children.Count; i++)
				{
					Widget widget = this.Children[i] as Widget;

					if (widget is MyWidgets.EntityBox)
					{
						MyWidgets.EntityBox box = widget as MyWidgets.EntityBox;
						MyWidgets.EntityBox inter = this.PushSearch(box, exclude, margin);
						if (inter != null)
						{
							push = true;
							this.PushAction(box, inter, direction, margin);
							this.PushLayout(inter, direction, margin);
						}
					}
				}

				if (!push)
				{
					break;
				}
			}
		}

		protected MyWidgets.EntityBox PushSearch(MyWidgets.EntityBox box, MyWidgets.EntityBox exclude, double margin)
		{
			//	Cherche une bo�te qui chevauche 'box'.
			Rectangle rect = box.ActualBounds;
			rect.Inflate(margin);

			for (int i=0; i<this.Children.Count; i++)
			{
				Widget widget = this.Children[i] as Widget;

				if (widget is MyWidgets.EntityBox && widget != box && widget != exclude)
				{
					MyWidgets.EntityBox b = widget as MyWidgets.EntityBox;

					if (b.ActualBounds.IntersectsWith(rect))
					{
						return b;
					}
				}
			}

			return null;
		}

		protected void PushAction(MyWidgets.EntityBox box, MyWidgets.EntityBox inter, PushDirection direction, double margin)
		{
			//	Pousse 'inter' pour venir apr�s 'box'.
			Rectangle rect = inter.ActualBounds;

			double dr = box.ActualBounds.Right - rect.Left + margin;
			double dl = rect.Right - box.ActualBounds.Left + margin;
			double dt = box.ActualBounds.Top - rect.Bottom + margin;
			double db = rect.Top - box.ActualBounds.Bottom + margin;

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

			inter.SetManualBounds(rect);
		}


		protected void RecenterBoxes(double margin)
		{
			//	Si des bo�tes d�passent de la surface de dessin, recentre le tout.
			Rectangle bounds = this.ComputeBoxBounds();

			if (bounds.Left < margin)
			{
				this.MoveBoxes(margin-bounds.Left, 0);
			}

			if (bounds.Right > this.ActualWidth-margin)
			{
				this.MoveBoxes(this.ActualWidth-margin-bounds.Right, 0);
			}

			if (bounds.Bottom < margin)
			{
				this.MoveBoxes(0, margin-bounds.Bottom);
			}

			if (bounds.Top > this.ActualHeight-margin)
			{
				this.MoveBoxes(0, this.ActualHeight-margin-bounds.Top);
			}
		}

		protected Rectangle ComputeBoxBounds()
		{
			//	Retourne le rectangle englobant toutes les bo�tes.
			Rectangle bounds = Rectangle.Empty;

			for (int i=0; i<this.Children.Count; i++)
			{
				Widget widget = this.Children[i] as Widget;

				if (widget is MyWidgets.EntityBox)
				{
					MyWidgets.EntityBox box = widget as MyWidgets.EntityBox;
					bounds = Rectangle.Union(bounds, box.ActualBounds);
				}
			}

			return bounds;
		}

		protected void MoveBoxes(double dx, double dy)
		{
			//	D�place toutes les bo�tes.
			for (int i=0; i<this.Children.Count; i++)
			{
				Widget widget = this.Children[i] as Widget;

				if (widget is MyWidgets.EntityBox)
				{
					MyWidgets.EntityBox box = widget as MyWidgets.EntityBox;

					Rectangle bounds = box.ActualBounds;
					bounds.Offset(dx, dy);
					box.SetManualBounds(bounds);
				}
			}
		}


		protected override void ProcessMessage(Message message, Point pos)
		{
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
					this.MouseDraggingBegin(pos);
					message.Consumer = this;
					break;

				case MessageType.MouseUp:
					this.MouseDraggingEnd(pos);
					message.Consumer = this;
					break;
			}
		}

		protected void MouseHilite(Point pos)
		{
			//	Met en �vidence tous les widgets selon la position vis�e par la souris.
			//	La bo�te � l'avant-plan a la priorit�.
			for (int i=this.Children.Count-1; i>=0; i--)
			{
				Widget widget = this.Children[i] as Widget;

				if (widget is MyWidgets.EntityBox)
				{
					MyWidgets.EntityBox box = widget as MyWidgets.EntityBox;
					if (box.Hilite(pos))
					{
						pos = Point.Zero;  // si on �tait dans cette bo�te -> plus aucun hilite pour les bo�tes plac�es dessous
					}
				}
			}
		}

		protected MyWidgets.EntityBox DetectBox(Point pos)
		{
			//	D�tecte la bo�te vis�e par la souris.
			//	La bo�te � l'avant-plan a la priorit�.
			for (int i=this.Children.Count-1; i>=0; i--)
			{
				Widget widget = this.Children[i] as Widget;

				if (widget is MyWidgets.EntityBox)
				{
					MyWidgets.EntityBox box = widget as MyWidgets.EntityBox;
					if (box.ActualBounds.Contains(pos))
					{
						return box;
					}
				}
			}

			return null;
		}

		protected void MouseDraggingBegin(Point pos)
		{
			MyWidgets.EntityBox box = this.DetectBox(pos);

			if (box != null && box.IsHilited)
			{
				this.draggingBox = box;
				this.draggingPos = pos;
				this.isDragging = true;
			}
		}

		protected void MouseDraggingMove(Point pos)
		{
			Rectangle bounds = this.draggingBox.ActualBounds;

			bounds.Offset(pos-this.draggingPos);
			this.draggingPos = pos;

			this.draggingBox.SetManualBounds(bounds);
			this.UpdateConnections();
		}

		protected void MouseDraggingEnd(Point pos)
		{
			this.PushLayout(this.draggingBox, PushDirection.Automatic, EntityEditor.pushMargin);
			this.RecenterBoxes(EntityEditor.pushMargin);
			this.UpdateConnections();

			this.draggingBox = null;
			this.isDragging = false;
		}


		private void HandleBoxGeometryChanged(object sender)
		{
			//	Appel� lorsque la g�om�trie d'une bo�te a chang� (changement compact/�tendu).
			MyWidgets.EntityBox box = sender as MyWidgets.EntityBox;
			this.UpdateBoxes();
			this.PushLayout(box, PushDirection.Bottom, EntityEditor.pushMargin);
			this.UpdateConnections();
		}



		protected static readonly double defaultWidth = 180;
		protected static readonly double connectionDetour = 30;
		protected static readonly double pushMargin = 10;

		protected List<MyWidgets.EntityBox> boxes;
		protected List<MyWidgets.EntityConnection> connections;
		protected bool isDragging;
		protected Point draggingPos;
		protected MyWidgets.EntityBox draggingBox;
	}
}

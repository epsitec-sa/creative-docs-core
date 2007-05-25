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
			box.SetManualBounds(new Rectangle(20+(180+40)*this.boxes.Count, 1000-20-100, 180, 100));

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
			foreach (MyWidgets.EntityBox box in this.boxes)
			{
				Rectangle bounds = box.ActualBounds;
				double top = bounds.Top;
				double h = box.GetBestHeight();
				bounds.Bottom = top-h;
				bounds.Height = h;
				box.SetManualBounds(bounds);
			}

			//	TODO: provisoire !
			this.UpdateConnection(this.connections[0], this.boxes[0], 2, this.boxes[1], FieldRelation.Reference);  // lien client
			this.UpdateConnection(this.connections[1], this.boxes[0], 3, this.boxes[2], FieldRelation.Collection);  // lien articles
			this.UpdateConnection(this.connections[2], this.boxes[0], 5, this.boxes[3], FieldRelation.Inclusion);  // lien rabais
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

				Point p = new Point(0, v);
				p = src.MapClientToParent(p);

				double min = double.MaxValue;
				MyWidgets.EntityBox.ConnectionAnchor best = EntityBox.ConnectionAnchor.Left;
				bool right = true;

				//	Cherche la meilleure liaison possible, parmi huit possibilit�s.
				foreach (MyWidgets.EntityBox.ConnectionAnchor anchor in System.Enum.GetValues(typeof(MyWidgets.EntityBox.ConnectionAnchor)))
				{
					Point end = dst.GetConnectionDestination(p.Y, anchor);
					Point start;
					double d;

					start = new Point(srcBounds.Right-1, p.Y);
					d = Point.Distance(start, end);
					if (Geometry.IsOver(srcBoundsLittle, start, end, 20))  d *= 20;  // tr�s mauvais si chevauchement avec la source
					if (Geometry.IsOver(dstBoundsLittle, start, end, 20))  d *= 10;  // mauvais si chevauchement avec la destination
					if (d < min)
					{
						min = d;
						best = anchor;
						right = true;
					}

					start = new Point(srcBounds.Left+1, p.Y);
					d = Point.Distance(start, end);
					if (Geometry.IsOver(srcBoundsLittle, start, end, 20))  d *= 20;  // tr�s mauvais si chevauchement avec la source
					if (Geometry.IsOver(dstBoundsLittle, start, end, 20))  d *= 10;  // mauvais si chevauchement avec la destination
					if (d < min)
					{
						min = d;
						best = anchor;
						right = false;
					}
				}

				connection.Source = new Point(right ? srcBounds.Right-1 : srcBounds.Left+1, p.Y);
				connection.Destination = dst.GetConnectionDestination(p.Y, best);
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
			this.UpdateGeometry();
		}

		protected void MouseDraggingEnd(Point pos)
		{
			this.draggingBox = null;
			this.isDragging = false;
		}


		private void HandleBoxGeometryChanged(object sender)
		{
			//	Appel� lorsque la g�om�trie d'une bo�te a chang� (changement compact/�tendu).
			this.UpdateGeometry();
		}



		protected List<MyWidgets.EntityBox> boxes;
		protected List<MyWidgets.EntityConnection> connections;
		protected bool isDragging;
		protected Point draggingPos;
		protected MyWidgets.EntityBox draggingBox;
	}
}

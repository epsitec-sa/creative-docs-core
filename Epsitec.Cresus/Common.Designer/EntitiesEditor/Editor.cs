using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using Epsitec.Common.Widgets;
using Epsitec.Common.Support;
using Epsitec.Common.Drawing;
using Epsitec.Common.Types;

namespace Epsitec.Common.Designer.EntitiesEditor
{
	/// <summary>
	/// Widget permettant d'�diter graphiquement des entit�s.
	/// </summary>
	public class Editor : Widget, Widgets.Helpers.IToolTipHost
	{
		public enum ModifyMode
		{
			Locked,
			Partial,
			Unlocked,
		}

		protected enum MouseCursorType
		{
			Unknown,
			Arrow,
			Finger,
			Grid,
			Move,
			Hand,
			IBeam,
			Locate,
		}

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
			this.AutoEngage = false;
			this.AutoFocus  = true;
			this.InternalState |= InternalState.Focusable;
			this.InternalState |= InternalState.Engageable;

			this.boxes = new List<ObjectBox>();
			this.connections = new List<ObjectConnection>();
			this.comments = new List<ObjectComment>();
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


		public Viewers.Entities Entities
		{
			get
			{
				return this.entities;
			}
			set
			{
				this.entities = value;
			}
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

		public VScroller VScroller
		{
			get
			{
				return this.vscroller;
			}
			set
			{
				this.vscroller = value;
			}
		}


		public void AddBox(ObjectBox box)
		{
			//	Ajoute une nouvelle bo�te dans l'�diteur. Elle est positionn�e toujours au m�me endroit,
			//	avec une hauteur nulle. La hauteur sera de toute fa�on adapt�e par UpdateBoxes().
			//	La position initiale n'a pas d'importance. La premi�re bo�te ajout�e (la bo�te racine)
			//	est positionn�e par RedimArea(). La position des autres est de toute fa�on recalcul�e en
			//	fonction de la bo�te parent.
			box.SetBounds(new Rectangle(0, 0, Editor.defaultWidth, 0));
			box.IsExtended = true;

			this.boxes.Add(box);
			this.UpdateAfterOpenOrCloseBox();
		}

		public int BoxCount
		{
			//	Retourne le nombre de bo�tes existantes.
			get
			{
				return this.boxes.Count;
			}
		}

		public List<ObjectBox> Boxes
		{
			//	Retourne la liste des bo�tes.
			get
			{
				return this.boxes;
			}
		}

		public ObjectBox RootBox
		{
			//	Retourne la bo�te racine.
			get
			{
				return this.boxes[0];
			}
		}

		public ObjectBox SearchBox(string title)
		{
			//	Cherche une bo�te d'apr�s son titre.
			foreach (ObjectBox box in this.boxes)
			{
				if (box.Title == title)
				{
					return box;
				}
			}

			return null;
		}

		public void AddConnection(ObjectConnection connection)
		{
			//	Ajoute une nouvelle liaison dans l'�diteur.
			this.connections.Add(connection);
		}

		public void AddComment(ObjectComment comment)
		{
			//	Ajoute une nouvelle liaison dans l'�diteur.
			this.comments.Add(comment);
		}

		public void Clear()
		{
			//	Supprime toutes les bo�tes et toutes les liaisons de l'�diteur.
			this.boxes.Clear();
			this.connections.Clear();
			this.comments.Clear();
			this.LockObject(null);
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

		public bool IsScrollerEnable
		{
			//	Indique si un (ou deux) ascenseurs sont actifs.
			get
			{
				return this.isScrollerEnable;
			}
			set
			{
				this.isScrollerEnable = value;
			}
		}


		public void UpdateGeometry()
		{
			//	Met � jour la g�om�trie de toutes les bo�tes et de toutes les liaisons.
			this.UpdateBoxes();
			this.UpdateConnections();
		}

		public void UpdateAfterCommentChanged()
		{
			//	Appel� lorsqu'un commentaire a chang�.
			this.RedimArea();

			this.UpdateConnections();
			this.RedimArea();

			this.UpdateDimmed();
		}

		public void UpdateAfterGeometryChanged(ObjectBox box)
		{
			//	Appel� lorsque la g�om�trie d'une bo�te a chang� (changement compact/�tendu).
			this.UpdateBoxes();  // adapte la taille selon compact/�tendu
			this.PushLayout(box, PushDirection.Automatic, Editor.pushMargin);
			this.RedimArea();

			this.UpdateConnections();
			this.RedimArea();

			this.UpdateDimmed();
		}

		public void UpdateAfterMoving(ObjectBox box)
		{
			//	Appel� lorsqu'une bo�te a �t� boug�e.
			this.PushLayout(box, PushDirection.Automatic, Editor.pushMargin);
			this.RedimArea();

			this.UpdateConnections();
			this.RedimArea();
		}

		public void UpdateAfterAddOrRemoveConnection(ObjectBox box)
		{
			//	Appel� lorsqu'une liaison a �t� ajout�e ou supprim�e.
			this.UpdateBoxes();
			this.PushLayout(box, PushDirection.Automatic, Editor.pushMargin);
			this.RedimArea();

			this.CreateConnections();
			this.RedimArea();

			this.UpdateConnections();
			this.RedimArea();

			this.UpdateDimmed();
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
				box.SetBounds(bounds);
			}
		}

		public void UpdateConnections()
		{
			//	Met � jour la g�om�trie de toutes les liaisons.
			this.CommentsMemorise();

			foreach (ObjectBox box in this.boxes)
			{
				for (int i=0; i<box.Fields.Count; i++)
				{
					Field field = box.Fields[i];

					if (field.Relation != FieldRelation.None)
					{
						ObjectConnection connection = field.Connection;
						if (connection != null)
						{
							connection.Points.Clear();

							if (field.IsExplored)
							{
								this.UpdateConnection(connection, box, field.Rank, field.DstBox);
							}
							else
							{
								field.RouteClear();

								//	Important: toujours le point droite en premier !
								double posv = box.GetConnectionSrcVerticalPosition(i);
								connection.Points.Add(new Point(box.Bounds.Right-1, posv));
								connection.Points.Add(new Point(box.Bounds.Left+1, posv));
								connection.Field.Route = Field.RouteType.Close;
							}
						}
					}
				}
			}

			//	R�parti astucieusement le point d'arriv� en haut ou en bas d'une bo�te de toutes les
			//	connections de type Bt ou Bb, pour �viter que deux connections n'arrivent sur le m�me point.
			//	Les croisements sont minimis�s.
			foreach (ObjectBox box in this.boxes)
			{
				box.ConnectionListBt.Clear();
				box.ConnectionListBb.Clear();
				box.ConnectionListC.Clear();
				box.ConnectionListD.Clear();
			}

			foreach (ObjectConnection connection in this.connections)
			{
				if (connection.Field.DstBox != null && connection.Field.Route == Field.RouteType.Bt)
				{
					connection.Field.DstBox.ConnectionListBt.Add(connection);
				}

				if (connection.Field.DstBox != null && connection.Field.Route == Field.RouteType.Bb)
				{
					connection.Field.DstBox.ConnectionListBb.Add(connection);
				}

				if (connection.Field.DstBox != null && connection.Field.Route == Field.RouteType.C)
				{
					connection.Field.DstBox.ConnectionListC.Add(connection);
				}

				if (connection.Field.DstBox != null && connection.Field.Route == Field.RouteType.D)
				{
					connection.Field.DstBox.ConnectionListD.Add(connection);
				}
			}

			foreach (ObjectBox box in this.boxes)
			{
				this.ShiftConnectionsB(box, box.ConnectionListBt);
				this.ShiftConnectionsB(box, box.ConnectionListBb);
				this.ShiftConnectionsC(box, box.ConnectionListC);
				this.ShiftConnectionsD(box, box.ConnectionListD);
			}

			foreach (ObjectBox box in this.boxes)
			{
				box.ConnectionListBt.Clear();
				box.ConnectionListBb.Clear();
				box.ConnectionListC.Clear();
				box.ConnectionListD.Clear();
			}

			//	Adapte toutes les commentaires.
			foreach (ObjectComment comment in this.comments)
			{
				if (comment.AttachObject is ObjectConnection)
				{
					ObjectConnection connection = comment.AttachObject as ObjectConnection;

					Point oldPos = connection.Field.CommentPosition;
					Point newPos = connection.PositionConnectionComment;

					if (!oldPos.IsZero && !newPos.IsZero)
					{
						Rectangle rect = connection.Field.CommentBounds;
						rect.Offset(newPos-oldPos);
						comment.SetBounds(rect);  // d�place le commentaire
					}
				}
			}

			this.Invalidate();
		}

		protected void UpdateConnection(ObjectConnection connection, ObjectBox src, int srcRank, ObjectBox dst)
		{
			//	Met � jour la g�om�trie d'une liaison.
			Rectangle srcBounds = src.Bounds;
			Rectangle dstBounds = dst.Bounds;

			//	Calcul des rectangles plus petits, pour les tests d'intersections.
			Rectangle srcBoundsLittle = srcBounds;
			Rectangle dstBoundsLittle = dstBounds;
			srcBoundsLittle.Deflate(2);
			dstBoundsLittle.Deflate(2);

			connection.Points.Clear();
			connection.Field.RouteClear();

			double v = src.GetConnectionSrcVerticalPosition(srcRank);
			if (src == dst)  // connection � soi-m�me ?
			{
				Point p = new Point(srcBounds.Right-1, v);
				connection.Points.Add(p);

				p.X += 30;
				connection.Points.Add(p);

				p.Y -= 10;
				connection.Points.Add(p);

				p.X -= 30;
				connection.Points.Add(p);

				connection.Field.Route = Field.RouteType.Himself;
			}
			else if (!srcBounds.IntersectsWith(dstBounds))
			{
				Point p = new Point(0, v);

				if (dstBounds.Center.X > srcBounds.Right+Editor.connectionDetour/3)  // destination � droite ?
				{
					Point start = new Point(srcBounds.Right-1, p.Y);
					connection.Points.Add(start);

					if (dstBounds.Top < start.Y-Editor.connectionDetour)  // destination plus basse ?
					{
						Point end = dst.GetConnectionDstPosition(start.Y, ObjectBox.ConnectionAnchor.Top);
						connection.Points.Add(new Point(end.X, start.Y));
						connection.Points.Add(end);
						connection.Field.Route = Field.RouteType.Bb;
					}
					else if (dstBounds.Bottom > start.Y+Editor.connectionDetour)  // destination plus haute ?
					{
						Point end = dst.GetConnectionDstPosition(start.Y, ObjectBox.ConnectionAnchor.Bottom);
						connection.Points.Add(new Point(end.X, start.Y));
						connection.Points.Add(end);
						connection.Field.Route = Field.RouteType.Bt;
					}
					else
					{
						Point end = dst.GetConnectionDstPosition(start.Y, ObjectBox.ConnectionAnchor.Left);
						if (start.Y != end.Y && end.X-start.X > Editor.connectionDetour)
						{
							connection.Points.Add(Point.Zero);  // (*)
							connection.Points.Add(Point.Zero);  // (*)
							connection.Points.Add(end);
							connection.Field.Route = Field.RouteType.C;
						}
						else
						{
							connection.Points.Add(end);
							connection.Field.Route = Field.RouteType.A;
						}
					}
				}
				else if (dstBounds.Center.X < srcBounds.Left-Editor.connectionDetour/3)  // destination � gauche ?
				{
					Point start = new Point(srcBounds.Left+1, p.Y);
					connection.Points.Add(start);

					if (dstBounds.Top < start.Y-Editor.connectionDetour)  // destination plus basse ?
					{
						Point end = dst.GetConnectionDstPosition(start.Y, ObjectBox.ConnectionAnchor.Top);
						connection.Points.Add(new Point(end.X, start.Y));
						connection.Points.Add(end);
						connection.Field.Route = Field.RouteType.Bb;
					}
					else if (dstBounds.Bottom > start.Y+Editor.connectionDetour)  // destination plus haute ?
					{
						Point end = dst.GetConnectionDstPosition(start.Y, ObjectBox.ConnectionAnchor.Bottom);
						connection.Points.Add(new Point(end.X, start.Y));
						connection.Points.Add(end);
						connection.Field.Route = Field.RouteType.Bt;
					}
					else
					{
						Point end = dst.GetConnectionDstPosition(start.Y, ObjectBox.ConnectionAnchor.Right);
						if (start.Y != end.Y && start.X-end.X > Editor.connectionDetour)
						{
							connection.Points.Add(Point.Zero);  // (*)
							connection.Points.Add(Point.Zero);  // (*)
							connection.Points.Add(end);
							connection.Field.Route = Field.RouteType.C;
						}
						else
						{
							connection.Points.Add(end);
							connection.Field.Route = Field.RouteType.A;
						}
					}
				}
				else if (connection.Field.IsAttachToRight)  // destination � droite � cheval ?
				{
					Point start = new Point(srcBounds.Right-1, p.Y);
					Point end = dst.GetConnectionDstPosition(start.Y, ObjectBox.ConnectionAnchor.Right);

					connection.Points.Add(start);
					connection.Points.Add(Point.Zero);  // (*)
					connection.Points.Add(Point.Zero);  // (*)
					connection.Points.Add(end);
					connection.Field.Route = Field.RouteType.D;
				}
				else  // destination � gauche � cheval ?
				{
					Point start = new Point(srcBounds.Left+1, p.Y);
					Point end = dst.GetConnectionDstPosition(start.Y, ObjectBox.ConnectionAnchor.Left);

					connection.Points.Add(start);
					connection.Points.Add(Point.Zero);  // (*)
					connection.Points.Add(Point.Zero);  // (*)
					connection.Points.Add(end);
					connection.Field.Route = Field.RouteType.D;
				}
			}
		}

		// (*)	Sera calcul� par ObjectConnection.UpdateRoute !

		protected void ShiftConnectionsB(ObjectBox box, List<ObjectConnection> connections)
		{
			//	Met � jour une liste de connections de type Bt ou Bb, afin qu'aucune connection
			//	n'arrive au m�me endroit.
			connections.Sort(new Comparers.Connection());  // tri pour minimiser les croisements

			double space = (box.Bounds.Width/(connections.Count+1.0))*0.75;

			for (int i=0; i<connections.Count; i++)
			{
				ObjectConnection connection = connections[i];

				int count = connection.Points.Count;
				if (count > 2)
				{
					double dx = space * (i-(connections.Count-1.0)/2);
					double px = connection.Points[count-1].X+dx;

					if (connection.IsRightDirection)
					{
						px = System.Math.Max(px, connection.Points[0].X+8);
					}
					else
					{
						px = System.Math.Min(px, connection.Points[0].X-8);
					}

					connection.Points[count-1] = new Point(px, connection.Points[count-1].Y);
					connection.Points[count-2] = new Point(px, connection.Points[count-2].Y);
					connection.UpdateRoute();
				}
			}
		}

		protected void ShiftConnectionsC(ObjectBox box, List<ObjectConnection> connections)
		{
			//	Met � jour une liste de connections de type C, afin qu'aucune connection
			//	n'arrive au m�me endroit.
			connections.Sort(new Comparers.Connection());  // tri pour minimiser les croisements

			double spaceX = 5;
			double spaceY = 12;

			for (int i=0; i<connections.Count; i++)
			{
				ObjectConnection connection = connections[i];

				if (connection.Points.Count == 4)
				{
					double dx = box.IsExtended ? (connection.IsRightDirection ^ connection.Points[0].Y > connection.Points[connection.Points.Count-1].Y ? spaceX*i : -spaceX*i) : 0;
					double dy = box.IsExtended ? spaceY*i : 0;
					connection.Points[1] = new Point(connection.Points[1].X+dx, connection.Points[1].Y   );
					connection.Points[2] = new Point(connection.Points[2].X+dx, connection.Points[2].Y-dy);
					connection.Points[3] = new Point(connection.Points[3].X,    connection.Points[3].Y-dy);
				}
			}
		}

		protected void ShiftConnectionsD(ObjectBox box, List<ObjectConnection> connections)
		{
			//	Met � jour une liste de connections de type D, afin qu'aucune connection
			//	n'arrive au m�me endroit.
			connections.Sort(new Comparers.Connection());  // tri pour minimiser les croisements

			double spaceX = 5;
			double spaceY = 12;

			for (int i=0; i<connections.Count; i++)
			{
				ObjectConnection connection = connections[i];

				if (connection.Points.Count == 4)
				{
					double dx = connection.IsRightDirection ? spaceX*i : -spaceX*i;
					double dy = box.IsExtended ? spaceY*i : 0;
					connection.Points[1] = new Point(connection.Points[1].X+dx, connection.Points[1].Y   );
					connection.Points[2] = new Point(connection.Points[2].X+dx, connection.Points[2].Y-dy);
					connection.Points[3] = new Point(connection.Points[3].X,    connection.Points[3].Y-dy);
				}
			}
		}

		public void CreateConnections()
		{
			//	Cr�e (ou recr�e) toutes les liaisons n�cessaires.
			this.CommentsMemorise();

			//	Supprime tous les commentaires li�s aux connections.
			int j = 0;
			while (j < this.comments.Count)
			{
				ObjectComment comment = this.comments[j];

				if (comment.AttachObject is ObjectConnection)
				{
					this.comments.RemoveAt(j);
				}
				else
				{
					j++;
				}
			}
			
			this.connections.Clear();  // supprime toutes les connections existantes

			foreach (ObjectBox box in this.boxes)
			{
				for (int i=0; i<box.Fields.Count; i++)
				{
					Field field = box.Fields[i];

					if (field.Relation != FieldRelation.None)
					{
						//	Si la liaison est ouverte sur une bo�te qui n'existe plus,
						//	consid�re la liaison comme ferm�e !
						if (field.IsExplored)
						{
							if (!this.boxes.Contains(field.DstBox))
							{
								field.IsExplored = false;
							}
						}

						ObjectConnection connection = new ObjectConnection(this);
						connection.Field = field;
						connection.BackgroundMainColor = box.BackgroundMainColor;
						field.Connection = connection;
						this.AddConnection(connection);
					}
				}
			}

			//	Recr�e tous les commentaires li�s aux connections.
			foreach (ObjectConnection connection in this.connections)
			{
				if (connection.Field.HasComment && connection.Field.IsExplored)
				{
					connection.Comment = new ObjectComment(this);
					connection.Comment.AttachObject = connection;
					connection.Comment.Text = connection.Field.CommentText;
					connection.Comment.BackgroundMainColor = connection.Field.CommentMainColor;
					connection.Comment.SetBounds(connection.Field.CommentBounds);

					this.AddComment(connection.Comment);
				}
			}

			this.Invalidate();
		}

		protected void CommentsMemorise()
		{
			//	M�morise l'�tat de tous les commentaires li�s � des connections.
			foreach (ObjectConnection connection in this.connections)
			{
				connection.Field.HasComment = false;
			}

			foreach (ObjectComment comment in this.comments)
			{
				if (comment.AttachObject is ObjectConnection)
				{
					ObjectConnection connection = comment.AttachObject as ObjectConnection;

					connection.Field.HasComment = true;
					connection.Field.CommentText = comment.Text;
					connection.Field.CommentMainColor = comment.BackgroundMainColor;

					Point pos = connection.PositionConnectionComment;
					if (!pos.IsZero)
					{
						connection.Field.CommentPosition = pos;
					}

					if (!comment.Bounds.IsEmpty)
					{
						connection.Field.CommentBounds = comment.Bounds;
					}
				}
			}
		}

		protected void UpdateDimmed()
		{
			//	Met en estomp� toutes les connections qui partent ou qui arrivent sur une entit� estomp�e.
			foreach (ObjectConnection connection in this.connections)
			{
				connection.IsDimmed = false;
			}

			foreach (ObjectConnection connection in this.connections)
			{
				if (connection.Field.IsExplored)
				{
					if (connection.Field.SrcBox != null && connection.Field.SrcBox.IsDimmed)
					{
						connection.IsDimmed = true;
					}
					else if (connection.Field.DstBox != null && connection.Field.DstBox.IsDimmed)
					{
						connection.IsDimmed = true;
					}
				}
				else
				{
					Module dstModule = this.module.DesignerApplication.SearchModule(connection.Field.Destination);
					Module currentModule = this.module.DesignerApplication.CurrentModule;

					connection.IsDimmed = (dstModule != currentModule);
				}

				if (connection.Comment != null)
				{
					connection.Comment.IsDimmed = connection.IsDimmed;
				}
			}

			foreach (ObjectBox box in this.boxes)
			{
				if (box.Comment != null)
				{
					box.Comment.IsDimmed = box.IsDimmed;
				}
			}
		}


		public bool IsEmptyArea(Rectangle area)
		{
			//	Retourne true si une zone est enti�rement vide (aucune bo�te, on ignore les connections).
			foreach (ObjectBox box in this.boxes)
			{
				if (box.Bounds.IntersectsWith(area))
				{
					return false;
				}
			}

			return true;
		}

		public void CloseBox(ObjectBox box)
		{
			//	Ferme une bo�te et toutes les bo�tes li�es, en essayant de fermer le moins possible de bo�tes.
			//	La strat�gie utilis�e est la suivante:
			//	1. On ferme la bo�te demand�e.
			//	2. Parmi toutes les bo�tes restantes, on regarde si une bo�te est isol�e, c'est-�-dire si
			//	   elle n'est plus reli�e � la racine. Si oui, on la d�truit.
			//	3. Tant qu'on a d�truit au moins une bo�te, on recommence au point 2.
			if (box != null && box.IsRoot)
			{
				return;  // on ne d�truit jamais la bo�te racine
			}

			if (box != null)
			{
				this.CloseOneBox(box);  // supprime la bo�te demand�e
				this.CloseConnections(box);  // supprime ses connections
			}

			foreach (ObjectBox abox in this.boxes)
			{
				abox.IsConnectedToRoot = false;
				abox.Parents.Clear();
			}

			foreach (ObjectBox abox in this.boxes)
			{
				foreach (Field field in abox.Fields)
				{
					ObjectBox dstBox = field.DstBox;
					if (dstBox != null)
					{
						dstBox.Parents.Add(abox);
					}
				}
			}

			foreach (ObjectBox abox in this.boxes)
			{
				List<ObjectBox> visited = new List<ObjectBox>();
				visited.Add(abox);
				this.ExploreConnectedToRoot(visited, abox);

				bool toRoot = false;
				foreach (ObjectBox vbox in visited)
				{
					if (vbox == this.boxes[0])
					{
						toRoot = true;
						break;
					}
				}

				if (toRoot)
				{
					foreach (ObjectBox vbox in visited)
					{
						vbox.IsConnectedToRoot = true;
					}
				}
			}

			bool removed;
			do
			{
				removed = false;
				int i = 1;  // on saute toujours la bo�te racine
				while (i < this.boxes.Count)
				{
					box = this.boxes[i];
					if (box.IsConnectedToRoot)  // bo�te li�e � la racine ?
					{
						i++;
					}
					else  // bo�te isol�e ?
					{
						this.CloseOneBox(box);  // supprime la bo�te isol�e
						this.CloseConnections(box);  // supprime ses connections
						removed = true;
					}
				}
			}
			while (removed);  // recommence tant qu'on a d�truit quelque chose

			foreach (ObjectBox abox in this.boxes)
			{
				abox.IsConnectedToRoot = false;
				abox.Parents.Clear();
			}

			this.UpdateAfterOpenOrCloseBox();
			this.DirtySerialization = true;
		}

		protected void CloseOneBox(ObjectBox box)
		{
			if (box.Comment != null)
			{
				this.comments.Remove(box.Comment);
				box.Comment = null;
			}

			this.boxes.Remove(box);  // supprime la bo�te demand�e
		}

		protected void ExploreConnectedToRoot(List<ObjectBox> visited, ObjectBox root)
		{
			//	Cherche r�cursivement tous les objets depuis 'root'.
			foreach (Field field in root.Fields)
			{
				ObjectBox dstBox = field.DstBox;
				if (dstBox != null)
				{
					if (!visited.Contains(dstBox))
					{
						visited.Add(dstBox);
						this.ExploreConnectedToRoot(visited, dstBox);
					}
				}
			}

			foreach (ObjectBox srcBox in root.Parents)
			{
				if (!visited.Contains(srcBox))
				{
					visited.Add(srcBox);
					this.ExploreConnectedToRoot(visited, srcBox);
				}
			}
		}

		protected void CloseConnections(ObjectBox removedBox)
		{
			//	Parcourt toutes les connections de toutes les bo�tes, pour fermer toutes
			//	les connections sur la bo�te supprim�e.
			foreach (ObjectBox box in this.boxes)
			{
				foreach (Field field in box.Fields)
				{
					if (field.DstBox == removedBox)
					{
						field.DstBox = null;
						field.IsExplored = false;
					}
				}
			}
		}

		protected void UpdateAfterOpenOrCloseBox()
		{
			//	Appel� apr�s avoir ajout� ou supprim� une bo�te.
			foreach (ObjectBox box in this.boxes)
			{
				box.UpdateAfterOpenOrCloseBox();
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

			inter.SetBounds(rect);
		}


		protected void RedimArea()
		{
			//	Recalcule les dimensions de la surface de travail, en fonction du contenu.
			Rectangle rect = this.ComputeObjectsBounds();
			rect.Inflate(Editor.frameMargin);
			this.MoveObjects(-rect.Left, -rect.Bottom);

			this.AreaSize = rect.Size;
			this.OnAreaSizeChanged();
		}

		protected Rectangle ComputeObjectsBounds()
		{
			//	Retourne le rectangle englobant tous les objets.
			Rectangle bounds = Rectangle.Empty;

			foreach (ObjectBox box in this.boxes)
			{
				bounds = Rectangle.Union(bounds, box.Bounds);
			}

			foreach (ObjectConnection connection in this.connections)
			{
				bounds = Rectangle.Union(bounds, connection.Bounds);
			}

			foreach (ObjectComment comment in this.comments)
			{
				bounds = Rectangle.Union(bounds, comment.Bounds);
			}

			return bounds;
		}

		protected void MoveObjects(double dx, double dy)
		{
			//	D�place tous les objets.
			foreach (ObjectBox box in this.boxes)
			{
				box.Move(dx, dy);
			}

			foreach (ObjectConnection connection in this.connections)
			{
				connection.Move(dx, dy);
			}

			foreach (ObjectComment comment in this.comments)
			{
				comment.Move(dx, dy);
			}
		
#if false
			for (int i=0; i<this.boxes.Count; i++)
			{
				ObjectBox box = this.boxes[i];

				Rectangle bounds = box.Bounds;
				bounds.Offset(dx, dy);
				box.SetBounds(bounds);
			}
#endif
		}


		protected override void ProcessMessage(Message message, Point pos)
		{
			this.brutPos = pos;
			pos = this.ConvWidgetToEditor(pos);

			this.lastMessageType = message.MessageType;
			this.lastMessagePos = pos;

			System.Diagnostics.Debug.WriteLine(string.Format("Type={0}", message.MessageType));

			switch (message.MessageType)
			{
				case MessageType.KeyDown:
				case MessageType.KeyUp:
					//	Ne consomme l'�v�nement que si on l'a bel et bien reconnu ! Evite
					//	qu'on ne mange les raccourcis clavier g�n�raux (Alt-F4, CTRL-S, ...)
					break;

				case MessageType.MouseMove:
					this.MouseMove(message, pos);
					message.Consumer = this;
					break;

				case MessageType.MouseDown:
					this.MouseDown(message, pos);
					message.Consumer = this;
					break;

				case MessageType.MouseUp:
					this.MouseUp(message, pos);
					message.Consumer = this;
					break;

				case MessageType.MouseLeave:
					this.MouseMove(message, Point.Zero);
					break;

				case MessageType.MouseWheel:
					if (message.IsControlPressed)
					{
						double zoom = this.zoom;
						if (message.Wheel < 0)  zoom -= 0.1;
						if (message.Wheel > 0)  zoom += 0.1;
						zoom = System.Math.Max(zoom, Viewers.Entities.zoomMin);
						zoom = System.Math.Min(zoom, Viewers.Entities.zoomMax);
						if (this.zoom != zoom)
						{
							this.Zoom = zoom;
							this.OnZoomChanged();
						}
					}
					else
					{
						if (message.Wheel < 0)  this.vscroller.Value += this.vscroller.SmallChange;
						if (message.Wheel > 0)  this.vscroller.Value -= this.vscroller.SmallChange;
					}
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

		protected void MouseMove(Message message, Point pos)
		{
			//	Met en �vidence tous les widgets selon la position vis�e par la souris.
			//	L'objet � l'avant-plan a la priorit�.
			if (this.isAreaMoving)
			{
				Point offset = new Point();
				offset.X = this.areaMovingInitialOffset.X-(this.brutPos.X-this.areaMovingInitialPos.X)/this.zoom;
				offset.Y = this.areaMovingInitialOffset.Y+(this.brutPos.Y-this.areaMovingInitialPos.Y)/this.zoom;
				this.AreaOffset = offset;
				this.OnAreaOffsetChanged();
			}
			else if (this.lockObject != null)
			{
				this.lockObject.MouseMove(message, pos);
			}
			else
			{
				AbstractObject fly = null;

				for (int i=this.comments.Count-1; i>=0; i--)
				{
					AbstractObject obj = this.comments[i];
					if (obj.MouseMove(message, pos))
					{
						fly = obj;
						pos = Point.Zero;  // si on �tait dans cet objet -> plus aucun hilite pour les objets plac�s dessous
					}
				}

				for (int i=this.connections.Count-1; i>=0; i--)
				{
					AbstractObject obj = this.connections[i];
					if (obj.MouseMove(message, pos))
					{
						fly = obj;
						pos = Point.Zero;  // si on �tait dans cet objet -> plus aucun hilite pour les objets plac�s dessous
					}
				}

				for (int i=this.boxes.Count-1; i>=0; i--)
				{
					AbstractObject obj = this.boxes[i];
					if (obj.MouseMove(message, pos))
					{
						fly = obj;
						pos = Point.Zero;  // si on �tait dans cet objet -> plus aucun hilite pour les objets plac�s dessous
					}
				}

				MouseCursorType type = MouseCursorType.Unknown;

				if (fly == null)
				{
					if (this.IsScrollerEnable)
					{
						type = MouseCursorType.Hand;
					}
					else
					{
						type = MouseCursorType.Arrow;
					}
				}
				else
				{
					if (fly.HilitedElement == AbstractObject.ActiveElement.BoxHeader)
					{
						if (this.IsLocateActionHeader(message))
						{
							ObjectBox box = fly as ObjectBox;
							if (box != null && !box.IsRoot)
							{
								type = MouseCursorType.Locate;
							}
							else
							{
								type = MouseCursorType.Arrow;
							}
						}
						else
						{
							if (this.BoxCount > 1)
							{
								type = MouseCursorType.Move;
							}
							else
							{
								type = MouseCursorType.Arrow;
							}
						}
					}
					else if (fly.HilitedElement == AbstractObject.ActiveElement.None ||
							 fly.HilitedElement == AbstractObject.ActiveElement.BoxInside ||
							 fly.HilitedElement == AbstractObject.ActiveElement.ConnectionHilited)
					{
						type = MouseCursorType.Arrow;
					}
					else if (fly.HilitedElement == AbstractObject.ActiveElement.BoxFieldName ||
							 fly.HilitedElement == AbstractObject.ActiveElement.BoxFieldType)
					{
						if (this.IsLocateAction(message))
						{
							type = MouseCursorType.Locate;
						}
						else
						{
							type = MouseCursorType.Grid;
						}
					}
					else if (fly.HilitedElement == AbstractObject.ActiveElement.BoxMembership)
					{
						if (this.IsLocateAction(message))
						{
							type = MouseCursorType.Locate;
						}
						else
						{
							type = MouseCursorType.Arrow;
						}
					}
					else if (fly.HilitedElement == AbstractObject.ActiveElement.CommentEdit)
					{
						type = MouseCursorType.IBeam;
					}
					else if (fly.HilitedElement == AbstractObject.ActiveElement.CommentMove)
					{
						type = MouseCursorType.Move;
					}
					else
					{
						type = MouseCursorType.Finger;
					}
				}

				this.ChangeMouseCursor(type);
				this.hilitedObject = fly;
			}
		}

		protected void MouseDown(Message message, Point pos)
		{
			//	D�but du d�placement d'une bo�te.
			if (this.lastCursor == MouseCursorType.Hand)
			{
				this.isAreaMoving = true;
				this.areaMovingInitialPos = this.brutPos;
				this.areaMovingInitialOffset = this.areaOffset;
			}
			else
			{
				AbstractObject obj = this.DetectObject(pos);
				if (obj != null)
				{
					obj.MouseDown(message, pos);
				}
			}
		}

		protected void MouseUp(Message message, Point pos)
		{
			//	Fin du d�placement d'une bo�te.
			if (this.isAreaMoving)
			{
				this.isAreaMoving = false;
			}
			else
			{
				AbstractObject obj = this.DetectObject(pos);
				if (obj != null)
				{
					obj.MouseUp(message, pos);
				}
			}
		}


		public bool IsLocateAction(Message message)
		{
			//	Indique si l'action d�bouche sur une op�ration de navigation.
			return (message.IsControlPressed || this.CurrentModifyMode != ModifyMode.Unlocked);
		}

		public bool IsLocateActionHeader(Message message)
		{
			//	Indique si l'action d�bouche sur une op�ration de navigation (pour BoxHeader).
			return (message.IsControlPressed || this.CurrentModifyMode == ModifyMode.Locked);
		}

		public ModifyMode CurrentModifyMode
		{
			//	Retourne le mode de travail courant.
			get
			{
				if (this.module.DesignerApplication.IsEditLocked)
				{
					if (this.entities.SubView == 3)
					{
						return ModifyMode.Partial;
					}
					else
					{
						return ModifyMode.Locked;
					}
				}
				else
				{
					return ModifyMode.Unlocked;
				}
			}
		}


		public void LockObject(AbstractObject obj)
		{
			//	Indique l'objet en cours de drag.
			this.lockObject = obj;
		}

		protected AbstractObject DetectObject(Point pos)
		{
			//	D�tecte l'objet vis� par la souris.
			//	L'objet � l'avant-plan a la priorit�.
			for (int i=this.comments.Count-1; i>=0; i--)
			{
				ObjectComment comment = this.comments[i];

				if (comment.IsReadyForAction)
				{
					return comment;
				}
			}

			for (int i=this.connections.Count-1; i>=0; i--)
			{
				ObjectConnection connection = this.connections[i];

				if (connection.IsReadyForAction)
				{
					return connection;
				}
			}

			for (int i=this.boxes.Count-1; i>=0; i--)
			{
				ObjectBox box = this.boxes[i];

				if (box.IsReadyForAction)
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

			//	Dessine l'arri�re-plan de tous les objets.
			foreach (AbstractObject obj in this.boxes)
			{
				obj.DrawBackground(graphics);
			}

			foreach (AbstractObject obj in this.connections)
			{
				obj.DrawBackground(graphics);
			}

			foreach (AbstractObject obj in this.comments)
			{
				obj.DrawBackground(graphics);
			}

			//	Dessine l'avant plan tous les objets.
			foreach (AbstractObject obj in this.boxes)
			{
				obj.DrawForeground(graphics);
			}

			foreach (AbstractObject obj in this.connections)
			{
				obj.DrawForeground(graphics);
			}

			foreach (AbstractObject obj in this.comments)
			{
				obj.DrawForeground(graphics);
			}

			graphics.Transform = initialTransform;

			//	Dessine le cadre.
			rect = this.Client.Bounds;
			rect.Deflate(0.5);
			graphics.AddRectangle(rect);
			graphics.RenderSolid(adorner.ColorBorder);
		}


		#region Serialization
		public bool DirtySerialization
		{
			//	Indique si la s�rialisation du layout est n�cessaire. Si le layout est modifi�,
			//	il faut activer la commande Enregistrer du module, car le layout est s�rialis�
			//	avec le module.
			get
			{
				return this.dirtySerialization;
			}
			set
			{
				if (this.dirtySerialization != value)
				{
					this.dirtySerialization = value;

					if (this.dirtySerialization)
					{
						this.module.AccessEntities.IsDirty = true;
					}

					this.OnDirtySerializationChanged();
				}
			}
		}

		public string Serialize()
		{
			//	S�rialise la vue �dit�e et retourne le r�sultat dans un string.
			System.Text.StringBuilder buffer = new System.Text.StringBuilder();
			System.IO.StringWriter stringWriter = new System.IO.StringWriter(buffer);
			XmlTextWriter writer = new XmlTextWriter(stringWriter);
			writer.Formatting = Formatting.None;

			this.WriteXml(writer);

			writer.Flush();
			writer.Close();
			return buffer.ToString();
		}

		public void Deserialize(string data)
		{
			//	D�s�rialise la vue � partir d'un string de donn�es.
			System.IO.StringReader stringReader = new System.IO.StringReader(data);
			XmlTextReader reader = new XmlTextReader(stringReader);
			
			this.ReadXml(reader);

			reader.Close();
		}

		protected void WriteXml(XmlWriter writer)
		{
			//	S�rialise toutes les bo�tes.
			writer.WriteStartDocument();

			writer.WriteStartElement(Xml.Boxes);
			foreach (ObjectBox box in this.boxes)
			{
				box.WriteXml(writer);
			}
			writer.WriteEndElement();
			
			writer.WriteEndDocument();
		}

		protected void ReadXml(XmlReader reader)
		{
			//	D�s�rialise toutes les bo�tes.
			this.Clear();

			while (reader.ReadToFollowing(Xml.Box))
			{
				ObjectBox box = new ObjectBox(this);
				box.ReadXml(reader);
				this.boxes.Add(box);
			}

			foreach (ObjectBox box in this.boxes)
			{
				box.AdjustAfterRead();
			}

			this.CloseBox(null);  // voir ObjectBox.AdjustAfterRead, commentaire (*)
			this.UpdateAfterAddOrRemoveConnection(null);
			this.UpdateAfterOpenOrCloseBox();
		}
		#endregion

		#region Helpers.IToolTipHost
		public object GetToolTipCaption(Point pos)
		{
			//	Donne l'objet (string ou widget) pour le tooltip en fonction de la position.
			return this.GetTooltipEditedText(pos);
		}

		protected string GetTooltipEditedText(Point pos)
		{
			//	Donne le texte du tooltip en fonction de la position.
			if (this.hilitedObject == null)
			{
				return null;  // pas de tooltip
			}
			else
			{
				pos = this.ConvWidgetToEditor(pos);
				return this.hilitedObject.GetToolTipText(pos);
			}
		}
		#endregion

		#region MouseCursor
		protected void ChangeMouseCursor(MouseCursorType cursor)
		{
			//	Change le sprite de la souris.
			if (cursor == this.lastCursor)
			{
				return;
			}

			this.lastCursor = cursor;

			switch ( cursor )
			{
				case MouseCursorType.Finger:
					this.SetMouseCursorImage(ref this.mouseCursorFinger, Misc.Icon("CursorFinger"));
					break;

				case MouseCursorType.Grid:
					this.SetMouseCursorImage(ref this.mouseCursorGrid, Misc.Icon("CursorGrid"));
					break;

				case MouseCursorType.Move:
					this.MouseCursor = MouseCursor.AsSizeAll;
					break;

				case MouseCursorType.Hand:
					this.SetMouseCursorImage(ref this.mouseCursorHand, Misc.Icon("CursorHand"));
					break;

				case MouseCursorType.IBeam:
					this.MouseCursor = MouseCursor.AsIBeam;
					break;

				case MouseCursorType.Locate:
					this.SetMouseCursorImage(ref this.mouseCursorLocate, Misc.Icon("CursorLocate"));
					break;

				default:
					this.MouseCursor = MouseCursor.AsArrow;
					break;
			}

			if (this.Window != null)
			{
				this.Window.MouseCursor = this.MouseCursor;
			}
		}

		protected void SetMouseCursorImage(ref Image image, string name)
		{
			//	Choix du sprite de la souris.
			if (image == null)
			{
				image = ImageProvider.Default.GetImage(name, Resources.DefaultManager);
			}
			
			this.MouseCursor = MouseCursor.FromImage(image);
		}
		#endregion

		#region Events handler
		protected virtual void OnAreaSizeChanged()
		{
			//	G�n�re un �v�nement pour dire que les dimensions ont chang�.
			EventHandler handler = (EventHandler) this.GetUserEventHandler("AreaSizeChanged");
			if (handler != null)
			{
				handler(this);
			}
		}

		public event Support.EventHandler AreaSizeChanged
		{
			add
			{
				this.AddUserEventHandler ("AreaSizeChanged", value);
			}
			remove
			{
				this.RemoveUserEventHandler ("AreaSizeChanged", value);
			}
		}

		protected virtual void OnAreaOffsetChanged()
		{
			//	G�n�re un �v�nement pour dire que l'offset de la surface de travail a chang�.
			EventHandler handler = (EventHandler) this.GetUserEventHandler("AreaOffsetChanged");
			if (handler != null)
			{
				handler(this);
			}
		}

		public event Support.EventHandler AreaOffsetChanged
		{
			add
			{
				this.AddUserEventHandler ("AreaOffsetChanged", value);
			}
			remove
			{
				this.RemoveUserEventHandler ("AreaOffsetChanged", value);
			}
		}

		protected virtual void OnZoomChanged()
		{
			//	G�n�re un �v�nement pour dire que l'offset de la surface de travail a chang�.
			EventHandler handler = (EventHandler) this.GetUserEventHandler("ZoomChanged");
			if (handler != null)
			{
				handler(this);
			}
		}

		public event Support.EventHandler ZoomChanged
		{
			add
			{
				this.AddUserEventHandler ("ZoomChanged", value);
			}
			remove
			{
				this.RemoveUserEventHandler ("ZoomChanged", value);
			}
		}

		protected virtual void OnDirtySerializationChanged()
		{
			//	G�n�re un �v�nement pour dire que l'�tat de la s�rialisation a chang�.
			EventHandler handler = (EventHandler) this.GetUserEventHandler("DirtySerializationChanged");
			if (handler != null)
			{
				handler(this);
			}
		}

		public event Support.EventHandler DirtySerializationChanged
		{
			add
			{
				this.AddUserEventHandler ("DirtySerializationChanged", value);
			}
			remove
			{
				this.RemoveUserEventHandler ("DirtySerializationChanged", value);
			}
		}
		#endregion


		public static readonly double defaultWidth = 200;
		public static readonly double connectionDetour = 30;
		public static readonly double pushMargin = 10;
		protected static readonly double frameMargin = 40;

		protected Module module;
		protected Viewers.Entities entities;
		protected List<ObjectBox> boxes;
		protected List<ObjectConnection> connections;
		protected List<ObjectComment> comments;
		protected Size areaSize;
		protected double zoom;
		protected Point areaOffset;
		protected AbstractObject lockObject;
		protected bool isScrollerEnable;
		protected Point brutPos;
		protected MessageType lastMessageType;
		protected Point lastMessagePos;
		protected bool isAreaMoving;
		protected Point areaMovingInitialPos;
		protected Point areaMovingInitialOffset;
		protected MouseCursorType lastCursor = MouseCursorType.Unknown;
		protected Image mouseCursorFinger;
		protected Image mouseCursorHand;
		protected Image mouseCursorGrid;
		protected Image mouseCursorLocate;
		protected VScroller vscroller;
		protected AbstractObject hilitedObject;
		protected bool dirtySerialization;
	}
}

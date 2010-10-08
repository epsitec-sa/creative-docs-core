//	Copyright � 2010, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Daniel ROUX, Maintainer: Daniel ROUX

using Epsitec.Common.Widgets;
using Epsitec.Common.Support;
using Epsitec.Common.Support.EntityEngine;
using Epsitec.Common.Types;
using Epsitec.Common.Drawing;

using Epsitec.Cresus.Core.Entities;

using System.Xml;
using System.Xml.Serialization;

using System.Collections.Generic;
using System.Linq;

namespace Epsitec.Cresus.WorkflowDesigner.Objects
{
	public class ObjectEdge : AbstractObject
	{
		public ObjectEdge(Editor editor, AbstractEntity entity)
			: base (editor, entity)
		{
			System.Diagnostics.Debug.Assert (this.Entity != null);

			this.points = new List<Point> ();
		}


		public Edge Edge
		{
			//	Champ de r�f�rence pour la connection.
			get
			{
				return this.edge;
			}
			set
			{
				this.edge = value;
			}
		}

		public ObjectComment Comment
		{
			//	Commentaire li�.
			get
			{
				return this.comment;
			}
			set
			{
				this.comment = value;
			}
		}

		public List<Point> Points
		{
			//	Retourne la liste des points. Si la connection est ferm�e, il s'agit des points
			//	droite et gauche. Aurement, il s'agit d'un nombre variable de points.
			get
			{
				return this.points;
			}
		}

		public bool IsSrcHilited
		{
			//	Indique si la bo�te source est survol�e par la souris.
			get
			{
				return this.isSrcHilited;
			}
			set
			{
				this.isSrcHilited = value;
			}
		}


		public override Rectangle Bounds
		{
			//	Retourne la bo�te de l'objet.
			get
			{
				Rectangle bounds = Rectangle.Empty;

				if (this.edge.IsSourceExpanded)
				{
					foreach (Point p in this.points)
					{
						bounds = Rectangle.Union(bounds, new Rectangle(p, Size.Zero));
					}
				}

				return bounds;
			}
		}

		public override void Move(double dx, double dy)
		{
			//	D�place l'objet.
			for (int i=0; i<this.points.Count; i++)
			{
				this.points[i] = new Point(this.points[i].X+dx, this.points[i].Y+dy);
			}
		}

		public bool IsRightDirection
		{
			//	Retourne la direction effective dans laquelle part la connection.
			//	A ne pas confondre avec Edge.IsAttachToRight !
			get
			{
				if (this.points.Count < 2)
				{
					return true;
				}
				else
				{
					return this.points[0].X < this.points[1].X;
				}
			}
		}


		protected override string GetToolTipText(ActiveElement element, int fieldRank)
		{
			//	Retourne le texte pour le tooltip.
			if (this.isDraggingRoute)
			{
				return null;  // pas de tooltip
			}

#if false
			switch (element)
			{
				case AbstractObject.ActiveElement.ConnectionComment:
					if (this.comment == null)
					{
						return Res.Strings.Entities.Action.ConnectionComment3;
					}
					else if (!this.comment.IsVisible)
					{
						return string.Format(Res.Strings.Entities.Action.ConnectionComment2, this.comment.Text);
					}
					else
					{
						return Res.Strings.Entities.Action.ConnectionComment1;
					}
			}
#endif

			return base.GetToolTipText(element, fieldRank);
		}

		public override bool MouseMove(Message message, Point pos)
		{
			//	La souris est boug�e.
			if (this.isDraggingRoute)
			{
				this.RouteMove(pos);
				return true;
			}
			else
			{
				return base.MouseMove(message, pos);
			}
		}

		public override void MouseDown(Message message, Point pos)
		{
			//	Le bouton de la souris est press�.
			if (this.hilitedElement == ActiveElement.EdgeMove1 ||
				this.hilitedElement == ActiveElement.EdgeMove2)
			{
				this.isDraggingRoute = true;
				this.editor.LockObject(this);
			}
		}

		public override void MouseUp(Message message, Point pos)
		{
			//	Le bouton de la souris est rel�ch�.
			if (this.isDraggingRoute)
			{
				this.isDraggingRoute = false;
				this.editor.UpdateAfterGeometryChanged(null);
				this.editor.LockObject(null);
				this.editor.SetLocalDirty ();
			}

			if (this.hilitedElement == ActiveElement.EdgeOpenLeft ||
				this.hilitedElement == ActiveElement.EdgeOpenRight)
			{
				this.edge.IsExplored = true;

				var node = this.editor.SearchNode (this.Entity.NextNode.Code);
				if (node == null)
				{
					//	Ouvre la connection sur une nouvelle bo�te.
					node = new ObjectNode (this.editor, this.Entity.NextNode);
					node.BackgroundMainColor = this.boxColor;

					this.edge.DstNode = node;
					this.edge.IsAttachToRight = (this.hilitedElement == ActiveElement.EdgeOpenRight);

					this.editor.AddNode (node);
					this.editor.UpdateGeometry ();

					ObjectNode src = this.edge.SrcNode;
					//	Essaie de trouver une place libre, pour d�placer le moins possible d'�l�ments.
					Rectangle bounds;
					double posv = src.GetEdgeSrcVerticalPosition (this.edge.Index) - (Editor.edgeDetour+12);

					if (this.hilitedElement == ActiveElement.EdgeOpenLeft)
					{
						bounds = new Rectangle (src.Bounds.Left-50-node.Bounds.Width, posv-node.Bounds.Height, node.Bounds.Width, node.Bounds.Height);
						bounds.Inflate (50, Editor.pushMargin);

						for (int i=0; i<1000; i++)
						{
							if (this.editor.IsEmptyArea (bounds))
							{
								break;
							}
							bounds.Offset (-1, 0);
						}

						bounds.Deflate (50, Editor.pushMargin);
					}
					else
					{
						bounds = new Rectangle (src.Bounds.Right+50, posv-node.Bounds.Height, node.Bounds.Width, node.Bounds.Height);
						bounds.Inflate (50, Editor.pushMargin);

						for (int i=0; i<1000; i++)
						{
							if (this.editor.IsEmptyArea (bounds))
							{
								break;
							}
							bounds.Offset (1, 0);
						}

						bounds.Deflate (50, Editor.pushMargin);
					}
					bounds = this.editor.NodeGridAlign (bounds);
					node.SetBounds (bounds);
				}
				else
				{
					//	Ouvre la connection sur une bo�te existante.
					this.edge.DstNode = node;
					this.edge.IsAttachToRight = (this.hilitedElement == ActiveElement.EdgeOpenRight);
				}

				this.editor.UpdateAfterAddOrRemoveEdge (node);
				this.editor.SetLocalDirty ();
			}

			if (this.hilitedElement == ActiveElement.EdgeClose)
			{
				ObjectNode dst = this.edge.DstNode;
				this.edge.IsExplored = false;
				this.edge.DstNode = null;
				this.editor.CloseNode(null);
				this.editor.UpdateAfterAddOrRemoveEdge(null);
			}

			if (this.hilitedElement == ActiveElement.EdgeComment)
			{
				this.AddComment();
			}
		}

		protected override bool MouseDetect(Point pos, out ActiveElement element, out int fieldRank)
		{
			//	D�tecte l'�l�ment actif vis� par la souris.
			element = ActiveElement.None;
			fieldRank = -1;

			if (pos.IsZero || this.points.Count == 0 || this.editor.CurrentModifyMode == Editor.ModifyMode.Locked)
			{
				return false;
			}

			//	Souris dans la pastille ronde du d�part de la connection ?
			if (this.edge.IsSourceExpanded)
			{
				if (this.edge.IsExplored)
				{
					if (this.DetectRoundButton(pos, this.points[0]))
					{
						element = ActiveElement.EdgeClose;
						return true;
					}
				}
				else
				{
					if (this.DetectRoundButton(pos, this.points[0]))
					{
						element = ActiveElement.EdgeOpenRight;
						return true;
					}

					if (this.DetectRoundButton(pos, this.points[1]))
					{
						element = ActiveElement.EdgeOpenLeft;
						return true;
					}
				}
			}

			//	Souris dans le bouton pour commenter la connection.
			if (this.IsEdgeCommentButton && this.DetectRoundButton(pos, this.PositionEdgeComment))
			{
				element = ActiveElement.EdgeComment;
				return true;
			}

			//	Souris dans le bouton pour d�placer le point milieu ?
			if (this.DetectRoundButton(pos, this.PositionRouteMove1))
			{
				element = ActiveElement.EdgeMove1;
				return true;
			}

			if (this.DetectRoundButton(pos, this.PositionRouteMove2))
			{
				element = ActiveElement.EdgeMove2;
				return true;
			}

			//	Souris le long de la connection ?
			if (DetectOver(pos, 4))
			{
				element = ActiveElement.EdgeHilited;
				return true;
			}

			return false;
		}

		protected bool DetectOver(Point pos, double margin)
		{
			//	D�tecte si la souris est le long de la connection.
			if (this.points.Count >= 2 && this.edge.IsExplored)
			{
				for (int i=0; i<this.points.Count-1; i++)
				{
					Point p1 = this.points[i];
					Point p2 = this.points[i+1];

					if (Point.Distance(p1, pos) <= margin ||
						Point.Distance(p2, pos) <= margin)
					{
						return true;
					}

					Point p = Point.Projection(p1, p2, pos);
					if (Point.Distance(p, pos) <= margin && Geometry.IsInside(p1, p2, p))
					{
						return true;
					}
				}
			}

			return false;
		}


		protected void AddComment()
		{
			//	Ajoute un commentaire � la connection.
			if (this.comment == null)
			{
				this.comment = new ObjectComment(this.editor, this.Entity);
				this.comment.AttachObject = this;
				this.comment.BackgroundMainColor = this.edge.CommentMainColor;
				this.comment.Text = this.edge.CommentText;

				Point attach = this.PositionEdgeComment;
				Rectangle rect;

				if (attach.X > this.edge.SrcNode.Bounds.Right)  // connection sur la droite ?
				{
					rect = new Rectangle(attach.X+20, attach.Y+20, Editor.defaultWidth, 50);  // hauteur arbitraire
				}
				else  // connection sur la gauche ?
				{
					rect = new Rectangle(attach.X-20-Editor.defaultWidth, attach.Y+20, Editor.defaultWidth, 50);  // hauteur arbitraire
				}

				this.comment.SetBounds(rect);
				this.comment.UpdateHeight();  // adapte la hauteur en fonction du contenu

				this.editor.AddComment(this.comment);
				this.editor.UpdateAfterCommentChanged();

				this.comment.EditComment();  // �dite tout de suite le texte du commentaire
			}
			else
			{
				this.comment.IsVisible = !this.comment.IsVisible;
			}

			this.editor.SetLocalDirty ();
		}

		
		public override void DrawBackground(Graphics graphics)
		{
			//	Dessine l'objet.
			IAdorner adorner = Common.Widgets.Adorners.Factory.Active;

			if (this.points.Count >= 2 && this.edge.IsExplored && (this.edge.Route != Edge.RouteType.Himself || this.edge.IsSourceExpanded))
			{
				Point start = this.points[0];
				if (this.edge.IsSourceExpanded)
				{
					start = Point.Move(start, this.points[1], AbstractObject.bulletRadius);
				}

				graphics.LineWidth = 2;
				for (int i=0; i<this.points.Count-1; i++)
				{
					Point p1 = (i==0) ? start : this.points[i];
					Point p2 = this.points[i+1];

					if (i == 0)
					{
						AbstractObject.DrawStartingArrow(graphics, p1, p2);
					}

					graphics.AddLine(p1, p2);

					if (i == this.points.Count-2)
					{
						AbstractObject.DrawEndingArrow(graphics, p1, p2);
					}
				}
				graphics.LineWidth = 1;

				Color color = this.GetColor(0);
				if (this.hilitedElement == ActiveElement.EdgeHilited)
				{
					color = this.GetColorMain();
				}
				graphics.RenderSolid(color);
			}

			if (this.points.Count == 2 && !this.edge.IsExplored && this.edge.IsSourceExpanded)
			{
				//	Dessine le moignon de liaison.
				Point start = this.points[0];
				Point end = new Point(start.X+AbstractObject.lengthClose, start.Y);

				graphics.LineWidth = 2;
				graphics.AddLine(start, end);
				AbstractObject.DrawEndingArrow(graphics, start, end);
				graphics.LineWidth = 1;

				Color color = this.GetColor(0);
				if (this.hilitedElement == ActiveElement.EdgeHilited)
				{
					color = this.GetColorMain();
				}
				graphics.RenderSolid(color);
			}

			if (this.points.Count != 0 && this.edge.IsSourceExpanded)
			{
				//	Dessine les cercles aux points de d�part.
				for (int i=0; i<this.points.Count; i++)
				{
					Point start = this.points[i];
					GlyphShape shape = GlyphShape.None;

					bool hilite = false;
					if (this.hilitedElement == ActiveElement.EdgeOpenLeft)
					{
						hilite = (i == 1);
						shape = GlyphShape.ArrowLeft;
					}
					else if (this.hilitedElement == ActiveElement.EdgeOpenRight)
					{
						hilite = (i == 0);
						shape = GlyphShape.ArrowRight;
					}
					else if (this.hilitedElement == ActiveElement.EdgeClose)
					{
						if (i != 0)  break;
						hilite = true;
						shape = GlyphShape.Close;
					}
					else
					{
						if (this.edge.IsExplored && i != 0)  break;
						if (!this.isSrcHilited && i != 0)  break;
					}

					if (hilite)
					{
						this.DrawRoundButton(graphics, start, AbstractObject.buttonRadius, shape, true, false);
					}
					else
					{
						if (this.hilitedElement == ActiveElement.EdgeHilited)
						{
							this.DrawRoundButton(graphics, start, AbstractObject.buttonRadius, GlyphShape.Close, false, false);
						}
						else
						{
							this.DrawRoundButton(graphics, start, AbstractObject.bulletRadius, GlyphShape.None, false, false);
						}
					}
				}
			}

			//	Dessine le bouton pour commenter la connection.
			Point p = this.PositionEdgeComment;
			if (!p.IsZero && this.IsEdgeCommentButton)
			{
				if (this.hilitedElement == ActiveElement.EdgeComment)
				{
					this.DrawRoundButton(graphics, p, AbstractObject.buttonRadius, "C", true, false);
				}
				if (this.hilitedElement == ActiveElement.EdgeHilited)
				{
					this.DrawRoundButton(graphics, p, AbstractObject.buttonRadius, "C", false, false);
				}
			}

			//	Dessine le bouton pour d�placer le point milieu.
			Point m = this.PositionRouteMove1;
			if (!m.IsZero)
			{
				if (this.hilitedElement == ActiveElement.EdgeMove1)
				{
					this.DrawRoundButton(graphics, m, AbstractObject.buttonRadius, GlyphShape.HorizontalMove, true, false);
				}
				if (this.hilitedElement == ActiveElement.EdgeHilited)
				{
					this.DrawRoundButton(graphics, m, AbstractObject.buttonRadius, GlyphShape.HorizontalMove, false, false);
				}
			}

			m = this.PositionRouteMove2;
			if (!m.IsZero)
			{
				if (this.hilitedElement == ActiveElement.EdgeMove2)
				{
					this.DrawRoundButton(graphics, m, AbstractObject.buttonRadius, GlyphShape.HorizontalMove, true, false);
				}
				if (this.hilitedElement == ActiveElement.EdgeHilited)
				{
					this.DrawRoundButton(graphics, m, AbstractObject.buttonRadius, GlyphShape.HorizontalMove, false, false);
				}
			}
		}


		protected bool IsEdgeCommentButton
		{
			//	Indique s'il faut affiche le bouton pour montrer le commentaire.
			//	Si un commentaire est visible, il ne faut pas montrer le bouton, car il y a d�j�
			//	le bouton CommentAttachToEdge pour d�placer le point d'attache.
			get
			{
				return (this.comment == null || !this.comment.IsVisible);
			}
		}

		public Point PositionEdgeComment
		{
			//	Retourne la position du bouton pour commenter la connection, ou pour d�placer
			//	le point d'attache lorsque le commentaire existe.
			get
			{
				if (this.edge.IsSourceExpanded && this.edge.IsExplored && this.points.Count >= 2)
				{
					return this.AttachToPoint(this.edge.CommentAttach);
				}

				return Point.Zero;
			}
		}

		protected Point AttachToPoint(double d)
		{
			//	Conversion d'une distance le long de la connection en position.
			//	Une distance positive commence depuis le d�but de la connection.
			//	Une distance n�gative commence depuis la fin de la connection.
			if (this.points.Count < 2)
			{
				return Point.Zero;
			}

			double total = 0;
			for (int i=0; i<this.points.Count-1; i++)
			{
				total += Point.Distance(this.points[i], this.points[i+1]);
			}

			bool fromBegin = true;
			if (d < 0)  // attach� depuis la fin ?
			{
				d = d+total;
				fromBegin = false;
			}

			d = System.Math.Min(d, total-AbstractObject.minAttach);
			d = System.Math.Max(d, AbstractObject.minAttach);

			if (fromBegin)  // attach� depuis le d�but ?
			{
				for (int i=0; i<this.points.Count-1; i++)
				{
					double len = Point.Distance(this.points[i], this.points[i+1]);
					if (d < len)
					{
						return Point.Move(this.points[i], this.points[i+1], d);
					}
					else
					{
						d -= len;
					}
				}
			}
			else  // attach� depuis la fin ?
			{
				d = total-d;
				for (int i=this.points.Count-2; i>=0; i--)
				{
					double len = Point.Distance(this.points[i], this.points[i+1]);
					if (d < len)
					{
						return Point.Move(this.points[i+1], this.points[i], d);
					}
					else
					{
						d -= len;
					}
				}
			}

			return Point.Move(this.points[this.points.Count-1], this.points[this.points.Count-2], AbstractObject.minAttach);
		}

		public double PointToAttach(Point p)
		{
			//	Conversion d'une position le long de la connection en distance depuis le d�but (si positif)
			//	ou depuis la fin (si n�gatif).
			if (this.points.Count < 2)
			{
				return 0;
			}

			double total = 0;
			double min = double.MaxValue;
			int j = -1;
			for (int i=0; i<this.points.Count-1; i++)
			{
				total += Point.Distance(this.points[i], this.points[i+1]);
				Point pi = Point.Projection(this.points[i], this.points[i+1], p);
				if (Geometry.IsInside(this.points[i], this.points[i+1], pi))
				{
					double di = Point.Distance(pi, p);
					if (di < min)
					{
						min = di;
						j = i;
					}
				}
			}

			if (j == -1)
			{
				return 0;
			}

			Point pj = Point.Projection(this.points[j], this.points[j+1], p);
			double dj = Point.Distance(this.points[j], pj);

			for (int i=0; i<j; i++)
			{
				dj += Point.Distance(this.points[i], this.points[i+1]);
			}

			dj = System.Math.Max(dj, AbstractObject.minAttach);
			dj = System.Math.Min(dj, total-AbstractObject.minAttach);

			if (dj > total/2)  // plus proche de la fin ?
			{
				dj = dj-total;  // attach� depuis la fin (valeur n�gative)
			}

			return dj;
		}


		protected Point PositionRouteMove1
		{
			//	Retourne la position du bouton pour modifier le routage.
			get
			{
				if (this.edge.Route == Edge.RouteType.A)
				{
					if (this.points.Count == 6)
					{
						return this.points[2];
					}

					if (this.points.Count == 2)
					{
						if (Point.Distance(this.points[0], this.points[1]) >= 75)
						{
							return Point.Scale(this.points[0], this.points[1], this.edge.RouteRelativeAX1);
						}
					}
				}

				if (this.edge.Route == Edge.RouteType.Bt || this.edge.Route == Edge.RouteType.Bb)
				{
					if (this.points.Count == 5)
					{
						return this.points[2];
					}
					
					if (this.points.Count == 3)
					{
						if (Point.Distance(this.points[0], this.points[1]) >= 50 && Point.Distance(this.points[1], this.points[2]) >= 50)
						{
							return this.points[1];
						}
					}
				}

				if (this.edge.Route == Edge.RouteType.C)
				{
					if (this.points.Count == 4)
					{
						if (Point.Distance(this.points[0], this.points[3]) >= 75)
						{
							return this.points[1];
						}
					}
				}

				if (this.edge.Route == Edge.RouteType.D)
				{
					if (this.points.Count == 4)
					{
						return this.points[1];
					}
				}

				return Point.Zero;
			}
		}

		protected Point PositionRouteMove2
		{
			//	Retourne la position du bouton pour modifier le routage.
			get
			{
				if (this.edge.Route == Edge.RouteType.A)
				{
					if (this.points.Count == 6)
					{
						return this.points[3];
					}
					
					if (this.points.Count == 2)
					{
						if (Point.Distance(this.points[0], this.points[1]) >= 75)
						{
							return Point.Scale(this.points[0], this.points[1], this.edge.RouteRelativeAX2);
						}
					}
				}

				return Point.Zero;
			}
		}

		protected void RouteMove(Point pos)
		{
			//	Modifie le routage en fonction du choix de l'utilisateur.
			if (pos.IsZero)
			{
				return;
			}

			Point oldPos = this.PositionEdgeComment;  // point d'attache avant re-routage

			if (this.edge.Route == Edge.RouteType.A)
			{
				if (this.hilitedElement == ActiveElement.EdgeMove1)
				{
					this.edge.RouteRelativeAX1 = (pos.X-this.points[0].X)/(this.points[this.points.Count-1].X-this.points[0].X);
				}
				else
				{
					this.edge.RouteRelativeAX2 = (pos.X-this.points[0].X)/(this.points[this.points.Count-1].X-this.points[0].X);
				}

				this.edge.RouteAbsoluteAY = pos.Y-this.points[0].Y;
			}

			if (this.edge.Route == Edge.RouteType.Bt || this.edge.Route == Edge.RouteType.Bb)
			{
				this.edge.RouteRelativeBX = (pos.X-this.points[this.points.Count-1].X)/(this.points[0].X-this.points[this.points.Count-1].X);
				this.edge.RouteRelativeBY = (pos.Y-this.points[0].Y)/(this.points[this.points.Count-1].Y-this.points[0].Y);
			}

			if (this.edge.Route == Edge.RouteType.C)
			{
				this.edge.RouteRelativeCX = (pos.X-this.points[0].X)/(this.points[3].X-this.points[0].X);
			}

			if (this.edge.Route == Edge.RouteType.D)
			{
				if (this.edge.IsAttachToRight)
				{
					double px = System.Math.Max(this.points[0].X, this.points[3].X) + Editor.edgeDetour;
					this.edge.RouteAbsoluteDX = pos.X-px;
				}
				else
				{
					double px = System.Math.Min(this.points[0].X, this.points[3].X) - Editor.edgeDetour;
					this.edge.RouteAbsoluteDX = px-pos.X;
				}
			}

			Point newPos = this.PositionEdgeComment;  // point d'attache apr�s re-routage

			if (this.comment != null)
			{
				Rectangle bounds = this.comment.Bounds;
				bounds.Offset(newPos-oldPos);
				this.comment.SetBounds(bounds);  // d�place le commentaire
			}
		}

		public void UpdateRoute()
		{
			//	Met � jour le routage de la connection, dans les cas ou le routage d�pend des choix de l'utilisateur.
			retry:
			if (this.edge.Route == Edge.RouteType.A)
			{
				if (this.edge.RouteAbsoluteAY == 0)
				{
					if (this.points.Count == 6)
					{
						this.points.RemoveAt(1);
						this.points.RemoveAt(1);
						this.points.RemoveAt(1);
						this.points.RemoveAt(1);
					}
				}
				else
				{
					if (this.points.Count == 2)
					{
						this.points.Insert(1, Point.Zero);
						this.points.Insert(1, Point.Zero);
						this.points.Insert(1, Point.Zero);
						this.points.Insert(1, Point.Zero);
					}

					double d = this.points[5].X-this.points[0].X;
					double d1 = d*this.edge.RouteRelativeAX1;
					double d2 = d*this.edge.RouteRelativeAX2;

					d1 -= d;
					d2 -= d;
					if (d2 > 0)
					{
						d2 = System.Math.Max(d2, ObjectEdge.arrowMinimalLength);
						if (d2 > d1)
						{
							this.edge.RouteAbsoluteAYClear();  // revient � un cas simple, puis recommencer le routage
							goto retry;
						}
					}
					else
					{
						d2 = -System.Math.Max(-d2, ObjectEdge.arrowMinimalLength);
						if (d2 < d1)
						{
							this.edge.RouteAbsoluteAYClear();  // revient � un cas simple, puis recommencer le routage
							goto retry;
						}
					}
					d1 += d;
					d2 += d;

					double px1 = this.points[0].X + d1;
					double px2 = this.points[0].X + d2;
					double py = this.points[0].Y + this.edge.RouteAbsoluteAY;
					this.points[1] = new Point(px1, this.points[0].Y);
					this.points[2] = new Point(px1, py);
					this.points[3] = new Point(px2, py);
					this.points[4] = new Point(px2, this.points[0].Y);
				}
			}

			if (this.edge.Route == Edge.RouteType.Bt || this.edge.Route == Edge.RouteType.Bb)
			{
				if (this.edge.RouteRelativeBX == 0 || this.edge.RouteRelativeBY == 0)
				{
					if (this.points.Count == 5)
					{
						this.points.RemoveAt(1);
						this.points.RemoveAt(1);
					}
					this.points[1] = new Point(this.points[2].X, this.points[0].Y);
				}
				else
				{
					if (this.points.Count == 3)
					{
						this.points.Insert(1, Point.Zero);
						this.points.Insert(1, Point.Zero);
					}

					double d = this.points[4].Y-this.points[0].Y;
					double d1 = d*this.edge.RouteRelativeBY;

					d1 -= d;
					if (d1 > 0)
					{
						d1 = System.Math.Max(d1, ObjectEdge.arrowMinimalLength);
					}
					else
					{
						d1 = -System.Math.Max(-d1, ObjectEdge.arrowMinimalLength);
					}
					d1 += d;

					double px = this.points[4].X + (this.points[0].X-this.points[4].X)*this.edge.RouteRelativeBX;
					double py = this.points[0].Y + d1;
					this.points[1] = new Point(px, this.points[0].Y);
					this.points[2] = new Point(px, py);
					this.points[3] = new Point(this.points[4].X, py);
				}
			}

			if (this.edge.Route == Edge.RouteType.C)
			{
				//	Met � jour les points milieu de la connection.
				double d = this.points[3].X-this.points[0].X;
				double d1 = d*this.edge.RouteRelativeCX;

				d1 -= d;
				if (d1 > 0)
				{
					d1 = System.Math.Max(d1, ObjectEdge.arrowMinimalLength);
				}
				else
				{
					d1 = -System.Math.Max(-d1, ObjectEdge.arrowMinimalLength);
				}
				d1 += d;

				double px = this.points[0].X + d1;
				this.points[1] = new Point(px, this.points[0].Y);
				this.points[2] = new Point(px, this.points[3].Y);
			}

			if (this.edge.Route == Edge.RouteType.D)
			{
				double px;
				if (this.edge.IsAttachToRight)
				{
					px = System.Math.Max(this.points[0].X, this.points[3].X) + Editor.edgeDetour;
					px += this.edge.RouteAbsoluteDX;
				}
				else
				{
					px = System.Math.Min(this.points[0].X, this.points[3].X) - Editor.edgeDetour;
					px -= this.edge.RouteAbsoluteDX;
				}
				this.points[1] = new Point(px, this.points[0].Y);
				this.points[2] = new Point(px, this.points[3].Y);
			}
		}


		private WorkflowEdgeEntity Entity
		{
			get
			{
				return this.entity as WorkflowEdgeEntity;
			}
		}

	
		protected static readonly double arrowMinimalLength = 25;

		private Edge edge;
		private List<Point> points;
		private bool isSrcHilited;
		private bool isDraggingRoute;
		private ObjectComment comment;
	}
}

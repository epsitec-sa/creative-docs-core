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
	public class ObjectEdge : LinkableObject
	{
		public ObjectEdge(Editor editor, AbstractEntity entity)
			: base (editor, entity)
		{
			System.Diagnostics.Debug.Assert (this.Entity != null);

			this.title = new TextLayout ();
			this.title.DefaultFontSize = 12;
			this.title.Alignment = ContentAlignment.MiddleCenter;
			this.title.BreakMode = TextBreakMode.Ellipsis | TextBreakMode.Split | TextBreakMode.SingleLine;

			this.subtitle = new TextLayout ();
			this.subtitle.DefaultFontSize = 9;
			this.subtitle.Alignment = ContentAlignment.MiddleCenter;
			this.subtitle.BreakMode = TextBreakMode.Ellipsis | TextBreakMode.Split | TextBreakMode.SingleLine;

			this.description = new TextLayout ();
			this.description.DefaultFontSize = 9;
			this.description.Alignment = ContentAlignment.MiddleLeft;
			this.description.BreakMode = TextBreakMode.Hyphenate;

			this.UpdateTitle ();
			this.UpdateSubtitle ();
			this.UpdateDescription ();

			this.SetBounds (new Rectangle (Point.Zero, ObjectEdge.frameSize));

			//	Cr�e la liaison unique.
			this.CreateInitialLinks ();
		}


		public string Title
		{
			//	Titre au sommet de la bo�te (nom du noeud).
			get
			{
				return this.titleString;
			}
			set
			{
				if (this.titleString != value)
				{
					this.titleString = value;
					this.title.Text = Misc.Bold (this.titleString);
				}
			}
		}

		public string Subtitle
		{
			//	Sous-titre au sommet de la bo�te (nom de l'action).
			get
			{
				return this.subtitleString;
			}
			set
			{
				if (this.subtitleString != value)
				{
					this.subtitleString = value;
					this.subtitle.Text = this.subtitleString;
				}
			}
		}

		public string Description
		{
			//	Titre au sommet de la bo�te (nom du noeud).
			get
			{
				return this.descriptionString;
			}
			set
			{
				if (this.descriptionString != value)
				{
					this.descriptionString = value;
					this.description.Text = this.descriptionString;
				}
			}
		}

		public override Rectangle Bounds
		{
			//	Retourne la bo�te de l'objet.
			//	Attention: le dessin peut d�border, par exemple pour l'ombre.
			get
			{
				return this.bounds;
			}
		}

		public override Rectangle ExtendedBounds
		{
			//	Retourne la bo�te de l'objet, �ventuellement agrandie si l'objet est �tendu.
			get
			{
				var box = this.bounds;

				if (this.isExtended)
				{
					box = new Rectangle (box.Left, box.Bottom-ObjectEdge.extendedHeight, box.Width, box.Height+ObjectEdge.extendedHeight);
				}

				return box;
			}
		}

		public override void Move(double dx, double dy)
		{
			//	D�place l'objet.
			this.bounds.Offset(dx, dy);
			this.UpdateButtonsGeometry ();
		}


		public override void CreateInitialLinks()
		{
			this.objectLinks.Clear ();
	
			var link = new ObjectLink (this.editor, this.Entity);
			link.SrcObject = this;
			link.DstObject = this.editor.SearchInitialObject (this.Entity.NextNode);  // null si n'existe pas (et donc moignon o--->)

			if (link.DstObject == null)
			{
				link.SetStumpAngle (0);
			}

			this.objectLinks.Add (link);
		}


		public override void SetBoundsAtEnd(Point start, Point end)
		{
			double a = Point.ComputeAngleRad (start, end);
			double d = System.Math.Abs (ObjectEdge.frameSize.Width*System.Math.Cos (a) + ObjectEdge.frameSize.Height*System.Math.Sin (a)) * 0.5;

			Point center = Point.Move (end, start, -d);
			Rectangle rect = new Rectangle (center.X-ObjectEdge.frameSize.Width/2, center.Y-ObjectEdge.frameSize.Height/2, ObjectEdge.frameSize.Width, ObjectEdge.frameSize.Height);

			this.SetBounds (rect);
		}


		public override void RemoveEntityLink(LinkableObject dst)
		{
			System.Diagnostics.Debug.Assert (dst.AbstractEntity is WorkflowNodeEntity);
			System.Diagnostics.Debug.Assert (this.Entity.NextNode == dst.AbstractEntity as WorkflowNodeEntity);

			this.Entity.NextNode = null;
		}

		public override void AddEntityLink(LinkableObject dst)
		{
			System.Diagnostics.Debug.Assert (dst.AbstractEntity is WorkflowNodeEntity);

			this.Entity.NextNode = dst.AbstractEntity as WorkflowNodeEntity;
		}


		public override Vector GetLinkVector(double angle, bool isDst)
		{
			double xMargin = ObjectEdge.frameSize.Height/2 * (isDst ? 2.5 : 1.5);
			double yMargin = ObjectEdge.frameSize.Height/2;

			Point c = this.bounds.Center;
			Point p = Transform.RotatePointDeg (c, angle, new Point (c.X+this.bounds.Width+this.bounds.Height, c.Y));

			Segment s = this.GetIntersect (c, p);
			if (s != null)
			{
				if (s.anchor == LinkAnchor.Left || s.anchor == LinkAnchor.Right)
				{
					s.intersection.Y = System.Math.Max (s.intersection.Y, s.p1.Y+yMargin);
					s.intersection.Y = System.Math.Min (s.intersection.Y, s.p2.Y-yMargin);
				}

				if (s.anchor == LinkAnchor.Bottom || s.anchor == LinkAnchor.Top)
				{
					s.intersection.X = System.Math.Max (s.intersection.X, s.p1.X+xMargin);
					s.intersection.X = System.Math.Min (s.intersection.X, s.p2.X-xMargin);
				}

				Point i = new Point (System.Math.Floor (s.intersection.X), System.Math.Floor (s.intersection.Y));

				switch (s.anchor)
				{
					case LinkAnchor.Left:
						return new Vector (i, new Size (-1, 0));

					case LinkAnchor.Right:
						return new Vector (i, new Size (1, 0));

					case LinkAnchor.Bottom:
						return new Vector (i, new Size (0, -1));

					case LinkAnchor.Top:
						return new Vector (i, new Size (0, 1));
				}
			}

			return Vector.Zero;
		}

		public override Point GetLinkStumpPos(double angle)
		{
			Point c = this.bounds.Center;
			Point p = Transform.RotatePointDeg (c, angle, new Point (c.X+this.bounds.Width+this.bounds.Height, c.Y));

			Segment s = this.GetIntersect (c, p);
			if (s != null)
			{
				return Point.Move (s.intersection, c, -AbstractObject.lengthStumpLink);
			}

			return this.bounds.Center;
		}

		private Segment GetIntersect(Point c, Point p)
		{
			Point i;

			i = Geometry.IsIntersect (c, p, this.bounds.BottomRight, this.bounds.TopRight);
			if (!i.IsZero)
			{
				return new Segment (i, this.bounds.BottomRight, this.bounds.TopRight, LinkAnchor.Right);
			}

			i = Geometry.IsIntersect (c, p, this.bounds.BottomLeft, this.bounds.BottomRight);
			if (!i.IsZero)
			{
				return new Segment (i, this.bounds.BottomLeft, this.bounds.BottomRight, LinkAnchor.Bottom);
			}

			i = Geometry.IsIntersect (c, p, this.bounds.BottomLeft,  this.bounds.TopLeft);
			if (!i.IsZero)
			{
				return new Segment (i, this.bounds.BottomLeft, this.bounds.TopLeft, LinkAnchor.Left);
			}

			i = Geometry.IsIntersect (c, p, this.bounds.TopLeft, this.bounds.TopRight);
			if (!i.IsZero)
			{
				return new Segment (i, this.bounds.TopLeft, this.bounds.TopRight, LinkAnchor.Top);
			}

			return null;
		}

		private class Segment
		{
			public Segment(Point intersection, Point p1, Point p2, LinkAnchor anchor)
			{
				this.intersection = intersection;
				this.p1           = p1;
				this.p2           = p2;
				this.anchor       = anchor;
			}

			public Point		intersection;
			public Point		p1;
			public Point		p2;
			public LinkAnchor	anchor;
		}


		public override void AcceptEdition()
		{
			if (this.editingElement == ActiveElement.EdgeHeader)
			{
				this.Entity.Name = this.editingTextField.Text;
				this.Entity.TransitionAction = this.editingTextField2.Text;
				this.UpdateTitle ();
				this.UpdateSubtitle ();
			}

			if (this.editingElement == ActiveElement.EdgeEditDescription)
			{
				this.Entity.Description = this.editingTextField.Text;
				this.UpdateDescription ();
			}

			this.editor.UpdateObjects ();
			this.StopEdition ();
		}

		public override void CancelEdition()
		{
			this.StopEdition ();
		}

		public override void StartEdition()
		{
			this.StartEdition (ActiveElement.EdgeHeader);
		}

		private void StartEdition(ActiveElement element)
		{
			Rectangle rect = Rectangle.Empty;
			string text = null;
			string text2 = null;

			if (element == ActiveElement.EdgeHeader)
			{
				rect = this.RectangleTitle;
				rect.Deflate (4, 6);

				text = this.Entity.Name.ToString ();
				text2 = (this.Entity.TransitionAction == null) ? "" : this.Entity.TransitionAction;

				this.editingTextField  = new TextField ();
				this.editingTextField2 = new TextFieldCombo ();
			}

			if (element == ActiveElement.EdgeEditDescription)
			{
				rect = this.RectangleDescription;
				rect.Inflate (6);

				text = this.Entity.Description.ToString ();

				var field = new TextFieldMulti ();
				field.ScrollerVisibility = false;
				this.editingTextField = field;
			}

			if (rect.IsEmpty)
			{
				return;
			}

			this.editingElement = element;

			Point p1 = this.editor.ConvEditorToWidget (rect.TopLeft);
			Point p2 = this.editor.ConvEditorToWidget (rect.BottomRight);
			double width  = System.Math.Max (p2.X-p1.X, 175);
			double height = System.Math.Max (p1.Y-p2.Y, 20);

			rect = new Rectangle (new Point (p1.X, p1.Y-height), new Size (width, height));

			this.editingTextField.Parent = this.editor;
			this.editingTextField.SetManualBounds (rect);
			this.editingTextField.Text = text;
			this.editingTextField.TabIndex = 1;
			this.editingTextField.SelectAll ();
			this.editingTextField.Focus ();

			if (text2 != null)
			{
				rect.Offset (0, -rect.Height+1);

				this.editingTextField2.Parent = this.editor;
				this.editingTextField2.SetManualBounds (rect);
				this.editingTextField2.Text = text2;
				this.editingTextField2.TabIndex = 2;

				var combo = this.editingTextField2 as TextFieldCombo;
				combo.Items.Add ("WorkflowAction.NewAffair");
				combo.Items.Add ("WorkflowAction.NewOffer");
				combo.Items.Add ("WorkflowAction.NewOfferVariant");
				combo.Items.Add ("etc. (� terminer)");
			}

			this.editor.EditingObject = this;
			this.editor.ClearHilited ();
			this.UpdateButtonsState ();
		}

		private void StopEdition()
		{
			this.editor.Children.Remove (this.editingTextField);
			this.editingTextField = null;

			if (this.editingTextField2 != null)
			{
				this.editor.Children.Remove (this.editingTextField2);
				this.editingTextField2 = null;
			}

			this.editor.EditingObject = null;
			this.UpdateButtonsState ();
		}


		protected override string GetToolTipText(ActiveElement element)
		{
			//	Retourne le texte pour le tooltip.
			if (this.isDragging || this.isChangeWidth)
			{
				return null;  // pas de tooltip
			}

			switch (element)
			{
				case ActiveElement.EdgeChangeWidth:
					return "Change la largeur de la bo�te";

				case ActiveElement.EdgeClose:
					return "Supprime la transition";

				case ActiveElement.EdgeComment:
					return (this.comment == null) ? "Ajoute un commentaire � la transition" : "Ferme le commentaire";

				case ActiveElement.EdgeType:
					if (this.Entity.TransitionType == Core.Business.WorkflowTransitionType.Default)
					{
						return "Change de \"normal\" � \"call\"";
					}
					else if (this.Entity.TransitionType == Core.Business.WorkflowTransitionType.Call)
					{
						return "Change de \"call\" � \"fork\"";
					}
					else if (this.Entity.TransitionType == Core.Business.WorkflowTransitionType.Fork)
					{
						return "Change de \"fork\" � \"normal\"";
					}
					break;

				case ActiveElement.EdgeExtend:
					return this.isExtended ? "R�duit la bo�te" : "Etend la bo�te";

				case ActiveElement.EdgeColor1:
					return "Jaune";

				case ActiveElement.EdgeColor2:
					return "Orange";

				case ActiveElement.EdgeColor3:
					return "Rouge";

				case ActiveElement.EdgeColor4:
					return "Lilas";

				case ActiveElement.EdgeColor5:
					return "Violet";

				case ActiveElement.EdgeColor6:
					return "Bleu";

				case ActiveElement.EdgeColor7:
					return "Vert";

				case ActiveElement.EdgeColor8:
					return "Gris";
			}

			return base.GetToolTipText (element);
		}

		public override bool MouseMove(Message message, Point pos)
		{
			//	Met en �vidence la bo�te selon la position de la souris.
			//	Si la souris est dans cette bo�te, retourne true.
			if (this.isMouseDown && !this.isDragging && pos != this.initialPos)
			{
				this.isDragging = true;
				this.UpdateButtonsState ();
				this.draggingOffset = this.initialPos-this.bounds.BottomLeft;
				this.editor.Invalidate ();
				this.editor.LockObject (this);
			}

			if (this.isDragging)
			{
				this.DraggingMouseMove (pos);
				return true;
			}
			
			if (this.isChangeWidth)
			{
				Rectangle bounds = this.Bounds;
				bounds.Width = this.editor.GridAlign (System.Math.Max (pos.X-this.changeWidthPos+this.changeWidthInitial, 120));
				this.SetBounds (bounds);
				this.editor.UpdateLinks ();
				return true;
			}

			return base.MouseMove (message, pos);
		}

		public override void MouseDown(Message message, Point pos)
		{
			//	Le bouton de la souris est press�.
			base.MouseDown (message, pos);

			if (this.hilitedElement == ActiveElement.EdgeHeader && this.editor.LinkableObjectsCount > 1)
			{
				this.isDragging = true;
				this.UpdateButtonsState ();
				this.draggingOffset = pos-this.bounds.BottomLeft;
				this.editor.Invalidate();
				this.editor.LockObject(this);
			}

			if (this.hilitedElement == ActiveElement.EdgeChangeWidth)
			{
				this.isChangeWidth = true;
				this.UpdateButtonsState ();
				this.changeWidthPos = pos.X;
				this.changeWidthInitial = this.bounds.Width;
				this.editor.LockObject (this);
			}
		}

		public override void MouseUp(Message message, Point pos)
		{
			//	Le bouton de la souris est rel�ch�.
			base.MouseUp (message, pos);

			if (pos == this.initialPos)
			{
				if (this.hilitedElement == ActiveElement.EdgeHeader ||
					this.hilitedElement == ActiveElement.EdgeEditDescription)
				{
					if (this.isDragging)
					{
						this.editor.UpdateAfterGeometryChanged (this);
						this.isDragging = false;
						this.UpdateButtonsState ();
						this.editor.LockObject (null);
						this.editor.SetLocalDirty ();
					}

					this.StartEdition (this.hilitedElement);
					return;
				}
			}

			if (this.isDragging)
			{
				this.editor.UpdateAfterGeometryChanged (this);
				this.isDragging = false;
				this.UpdateButtonsState ();
				this.editor.LockObject (null);
				this.editor.SetLocalDirty ();
				return;
			}

			if (this.isChangeWidth)
			{
				this.editor.UpdateAfterGeometryChanged (this);
				this.isChangeWidth = false;
				this.UpdateButtonsState ();
				this.editor.LockObject (null);
				this.editor.SetLocalDirty ();
				return;
			}

			if (this.hilitedElement == ActiveElement.EdgeExtend)
			{
				if (this.IsExtended)
				{
					this.IsExtended = false;
				}
				else
				{
					this.editor.CompactAll ();
					this.IsExtended = true;
				}
				this.editor.UpdateAfterCommentChanged ();
			}

			if (this.hilitedElement == ActiveElement.EdgeClose)
			{
				this.editor.CloseObject (this);
				this.editor.UpdateAfterGeometryChanged (null);
			}

			if (this.hilitedElement == ActiveElement.EdgeComment)
			{
				this.AddComment ();
			}

			if (this.hilitedElement == ActiveElement.EdgeType)
			{
				if (this.Entity.TransitionType == Core.Business.WorkflowTransitionType.Default)
				{
					this.Entity.TransitionType = Core.Business.WorkflowTransitionType.Call;
				}
				else if (this.Entity.TransitionType == Core.Business.WorkflowTransitionType.Call)
				{
					this.Entity.TransitionType = Core.Business.WorkflowTransitionType.Fork;
				}
				else if (this.Entity.TransitionType == Core.Business.WorkflowTransitionType.Fork)
				{
					this.Entity.TransitionType = Core.Business.WorkflowTransitionType.Default;
				}
			}

			if (this.hilitedElement == ActiveElement.EdgeColor1)
			{
				this.BackgroundColorItem = ColorItem.Yellow;
			}

			if (this.hilitedElement == ActiveElement.EdgeColor2)
			{
				this.BackgroundColorItem = ColorItem.Orange;
			}

			if (this.hilitedElement == ActiveElement.EdgeColor3)
			{
				this.BackgroundColorItem = ColorItem.Red;
			}

			if (this.hilitedElement == ActiveElement.EdgeColor4)
			{
				this.BackgroundColorItem = ColorItem.Lilac;
			}

			if (this.hilitedElement == ActiveElement.EdgeColor5)
			{
				this.BackgroundColorItem = ColorItem.Purple;
			}

			if (this.hilitedElement == ActiveElement.EdgeColor6)
			{
				this.BackgroundColorItem = ColorItem.Blue;
			}

			if (this.hilitedElement == ActiveElement.EdgeColor7)
			{
				this.BackgroundColorItem = ColorItem.Green;
			}

			if (this.hilitedElement == ActiveElement.EdgeColor8)
			{
				this.BackgroundColorItem = ColorItem.Grey;
			}
		}

		public override ActiveElement MouseDetectBackground(Point pos)
		{
			//	D�tecte l'�l�ment actif vis� par la souris.
			if (this.RectangleTitle.Contains (pos))
			{
				return ActiveElement.EdgeHeader;
			}

			if (this.isExtended && this.RectangleDescription.Contains (pos))
			{
				return ActiveElement.EdgeEditDescription;
			}

			if (this.ExtendedBounds.Contains (pos))
			{
				return ActiveElement.EdgeInside;
			}

			return ActiveElement.None;
		}

		public override ActiveElement MouseDetectForeground(Point pos)
		{
			//	D�tecte l'�l�ment actif vis� par la souris.
			if (this.editor.CurrentModifyMode != Editor.ModifyMode.Locked)
			{
				return this.DetectButtons (pos);
			}

			return ActiveElement.None;
		}


		public override string DebugInformations
		{
			get
			{
				return string.Format ("Edge: {0} {1}", this.Entity.Name.ToString (), this.DebugInformationsObjectLinks);
			}
		}

		public override string DebugInformationsBase
		{
			get
			{
				return string.Format ("{0}", this.Entity.Name.ToString ());
			}
		}

	
		private void AddComment()
		{
			//	Ajoute un commentaire � la bo�te.
			if (this.comment == null)
			{
				this.comment = new ObjectComment (this.editor, this.Entity);
				this.comment.AttachObject = this;

				Rectangle rect = new Rectangle (this.bounds.Left, this.bounds.Top+40, System.Math.Max (this.bounds.Width, AbstractObject.infoMinWidth), 20);
				this.comment.SetBounds (rect);
				this.comment.UpdateHeight ();  // adapte la hauteur en fonction du contenu

				this.editor.AddBalloon (this.comment);
				this.editor.UpdateAfterCommentChanged ();
			}
			else
			{
				this.editor.RemoveBalloon (this.comment);
				this.comment = null;
			}

			this.editor.SetLocalDirty ();
		}


		public override void DrawBackground(Graphics graphics)
		{
			//	Dessine le fond de l'objet.
			//	H�ritage	->	Traitill�
			//	Interface	->	Trait plein avec o---
			Rectangle rect;
			Color c1, c2;

			bool dragging = (this.hilitedElement == ActiveElement.EdgeHeader || this.hilitedElement == ActiveElement.EdgeEditDescription || this.isHilitedForLinkChanging);
			Color colorFrame = dragging ? this.colorFactory.GetColorMain () : this.colorFactory.GetColor (0);

			var extendedRect = new Rectangle (this.bounds.Left, this.bounds.Bottom-ObjectEdge.extendedHeight, this.bounds.Width, this.bounds.Height+ObjectEdge.extendedHeight);
			extendedRect.Deflate (2, 0);

			//	Dessine le bo�te �tendue.
			if (this.isExtended)
			{
				//	Dessine l'ombre.
				rect = extendedRect;
				rect.Offset (AbstractObject.shadowOffset, -(AbstractObject.shadowOffset));
				this.DrawRoundShadow (graphics, rect, ObjectEdge.frameSize.Height/2, (int) AbstractObject.shadowOffset, 0.2);

				rect = extendedRect;
				rect.Deflate (0.5);
				Path extendedPath = this.PathRoundRectangle (rect, ObjectEdge.frameSize.Height/2);

				//	Dessine l'int�rieur en blanc.
				graphics.Rasterizer.AddSurface (extendedPath);
				graphics.RenderSolid (this.colorFactory.GetColor (1));

				//	Dessine l'int�rieur en d�grad�.
				graphics.Rasterizer.AddSurface (extendedPath);
				c1 = this.colorFactory.GetColorMain (dragging ? 0.3 : 0.2);
				c2 = this.colorFactory.GetColorMain (dragging ? 0.1 : 0.0);
				this.RenderHorizontalGradient (graphics, this.bounds, c1, c2);

				graphics.Rasterizer.AddOutline (extendedPath, 1);
				graphics.RenderSolid (colorFrame);
			}

			//	Dessine l'ombre.
			rect = this.bounds;
			rect.Offset (AbstractObject.shadowOffset, -(AbstractObject.shadowOffset));
			this.DrawEdgeShadow (graphics, rect, (int) AbstractObject.shadowOffset, 0.2);

			//	Construit le chemin du cadre.
			rect = this.bounds;
			rect.Deflate(1);
			Path path = this.PathEdgeRectangle (rect);

			//	Dessine l'int�rieur en blanc.
			graphics.Rasterizer.AddSurface(path);
			graphics.RenderSolid (this.colorFactory.GetColor (1));

			//	Dessine l'int�rieur en d�grad�.
			graphics.Rasterizer.AddSurface(path);
			c1 = this.colorFactory.GetColorMain (dragging ? 0.8 : 0.4);
			c2 = this.colorFactory.GetColorMain (dragging ? 0.4 : 0.1);
			this.RenderHorizontalGradient(graphics, this.bounds, c1, c2);

			//	Dessine le titre.
			Color titleColor = dragging ? this.colorFactory.GetColor (1) : this.colorFactory.GetColor (0);

			rect = this.RectangleTitle;
			rect.Deflate (16, 2);

			if (string.IsNullOrEmpty (this.subtitleString))
			{
				this.title.LayoutSize = rect.Size;
				this.title.Paint (rect.BottomLeft, graphics, Rectangle.MaxValue, titleColor, GlyphPaintStyle.Normal);
			}
			else
			{
				var r = new Rectangle (rect.Left, rect.Bottom+12, rect.Width, rect.Height-12);
				this.title.LayoutSize = r.Size;
				this.title.Paint (r.BottomLeft, graphics, Rectangle.MaxValue, titleColor, GlyphPaintStyle.Normal);

				r = new Rectangle (rect.Left, rect.Bottom, rect.Width, 20);
				this.subtitle.LayoutSize = r.Size;
				this.subtitle.Paint (r.BottomLeft, graphics, Rectangle.MaxValue, titleColor, GlyphPaintStyle.Normal);
			}

			//	Dessine la description.
			if (this.isExtended)
			{
				Color descriptionColor = this.colorFactory.GetColor (0);

				rect = this.RectangleDescription;
				this.description.LayoutSize = rect.Size;
				this.description.Paint (rect.BottomLeft, graphics, Rectangle.MaxValue, descriptionColor, GlyphPaintStyle.Normal);
			}

			//	Dessine le cadre en noir.
			graphics.Rasterizer.AddOutline (path, 2);
			graphics.RenderSolid (colorFrame);

			if (this.Entity.TransitionType == Core.Business.WorkflowTransitionType.Call)
			{
				rect = this.bounds;
				rect.Deflate (0, 3.5);
				double r = rect.Height/2;
				graphics.AddLine (rect.Left+r,  rect.Bottom-2, rect.Left+r,  rect.Top+2);
				graphics.AddLine (rect.Right-r, rect.Bottom-2, rect.Right-r, rect.Top+2);

				graphics.RenderSolid (colorFrame);
			}

			if (this.Entity.TransitionType == Core.Business.WorkflowTransitionType.Fork)
			{
				rect = this.bounds;
				rect.Inflate (2.5);
				path = this.PathEdgeRectangle (rect);

				graphics.Rasterizer.AddOutline (path, 1);
				graphics.RenderSolid (colorFrame);
			}
		}

		public override void DrawForeground(Graphics graphics)
		{
			//	Dessine tous les boutons.
			this.DrawButtons (graphics);
		}


		private bool IsHeaderHilite
		{
			//	Indique si la souris est dans l'en-t�te.
			get
			{
				if (this.editor.CurrentModifyMode == Editor.ModifyMode.Locked)
				{
					return false;
				}

				return (this.hilitedElement == ActiveElement.EdgeHeader ||
						this.hilitedElement == ActiveElement.EdgeEditDescription ||
						this.hilitedElement == ActiveElement.EdgeInside ||
						this.hilitedElement == ActiveElement.EdgeComment ||
						this.hilitedElement == ActiveElement.EdgeType ||
						this.hilitedElement == ActiveElement.EdgeColor1 ||
						this.hilitedElement == ActiveElement.EdgeColor2 ||
						this.hilitedElement == ActiveElement.EdgeColor3 ||
						this.hilitedElement == ActiveElement.EdgeColor4 ||
						this.hilitedElement == ActiveElement.EdgeColor5 ||
						this.hilitedElement == ActiveElement.EdgeColor6 ||
						this.hilitedElement == ActiveElement.EdgeColor7 ||
						this.hilitedElement == ActiveElement.EdgeColor8 ||
						this.hilitedElement == ActiveElement.EdgeExtend ||
						this.hilitedElement == ActiveElement.EdgeClose ||
						this.hilitedElement == ActiveElement.EdgeChangeWidth);
			}
		}

		private Rectangle RectangleTitle
		{
			get
			{
				return this.bounds;
			}
		}

		private Rectangle RectangleDescription
		{
			get
			{
				var rect = new Rectangle (this.bounds.Left, this.bounds.Bottom-ObjectEdge.extendedHeight, this.bounds.Width, ObjectEdge.extendedHeight);
				rect.Deflate (11, 8);

				return rect;
			}
		}

		private Point PositionExtendButton
		{
			//	Retourne la position du bouton pour �tendre.
			get
			{
				return new Point (this.bounds.Left+ActiveButton.buttonRadius+6, this.bounds.Center.Y);
			}
		}

		private Point PositionChangeWidthButton
		{
			//	Retourne la position du bouton pour changer la largeur.
			get
			{
				return new Point (this.bounds.Right-1, this.bounds.Bottom-ActiveButton.buttonRadius-4);
			}
		}

		private Point PositionCommentButton
		{
			//	Retourne la position du bouton pour montrer le commentaire.
			get
			{
				return new Point (this.bounds.Left+ActiveButton.buttonRadius+6, this.bounds.Bottom-ObjectEdge.extendedHeight+ActiveButton.buttonRadius+4);
			}
		}

		private Point PositionTypeButton
		{
			//	Retourne la position du bouton pour montrer le type.
			get
			{
				return new Point (this.bounds.Left+ActiveButton.buttonRadius*3+8, this.bounds.Bottom-ObjectEdge.extendedHeight+ActiveButton.buttonRadius+4);
			}
		}

		private Point PositionCloseButton
		{
			//	Retourne la position du bouton pour fermer.
			get
			{
				return new Point (this.bounds.Right-ActiveButton.buttonRadius-6, this.bounds.Bottom-ObjectEdge.extendedHeight+ActiveButton.buttonRadius+4);
			}
		}

		private Point PositionColorButton(int rank)
		{
			//	Retourne la position du bouton pour choisir la couleur.
			return new Point (this.bounds.Left-1+(ActiveButton.buttonSquare+0.5)*(rank+1)*2, this.bounds.Bottom-7);
		}

		private string GetGroupTooltip(int rank)
		{
			//	Retourne le tooltip � afficher pour un groupe.
			return null;  // pas de tooltip
		}


		private void UpdateTitle()
		{
			//	Met � jour le titre du noeud.
			this.Title = this.Entity.Name.ToString ();
		}

		private void UpdateSubtitle()
		{
			//	Met � jour le sous-titre du noeud.
			this.Subtitle = this.Entity.TransitionAction;
		}

		private void UpdateDescription()
		{
			//	Met � jour le sous-titre du noeud.
			this.Description = this.Entity.Description.ToString ();
		}


		public WorkflowEdgeEntity Entity
		{
			get
			{
				return this.entity as WorkflowEdgeEntity;
			}
		}


		protected override void CreateButtons()
		{
			this.buttons.Add (new ActiveButton (ActiveElement.EdgeExtend,      this.colorFactory, GlyphShape.ArrowUp,        this.UpdateButtonGeometryExtend,      this.UpdateButtonStateExtend));
			this.buttons.Add (new ActiveButton (ActiveElement.EdgeClose,       this.colorFactory, GlyphShape.Close,          this.UpdateButtonGeometryClose,       this.UpdateButtonStateClose));
			this.buttons.Add (new ActiveButton (ActiveElement.EdgeComment,     this.colorFactory, "C",                       this.UpdateButtonGeometryComment,     this.UpdateButtonStateComment));
			this.buttons.Add (new ActiveButton (ActiveElement.EdgeType,        this.colorFactory, "T",                       this.UpdateButtonGeometryType,        this.UpdateButtonStateType));
			this.buttons.Add (new ActiveButton (ActiveElement.EdgeChangeWidth, this.colorFactory, GlyphShape.HorizontalMove, this.UpdateButtonGeometryChangeWidth, this.UpdateButtonStateChangeWidth));

			this.buttons.Add (new ActiveButton (ActiveElement.EdgeColor1, this.colorFactory, ColorItem.Yellow, this.UpdateButtonGeometryColor, this.UpdateButtonStateColor));
			this.buttons.Add (new ActiveButton (ActiveElement.EdgeColor2, this.colorFactory, ColorItem.Orange, this.UpdateButtonGeometryColor, this.UpdateButtonStateColor));
			this.buttons.Add (new ActiveButton (ActiveElement.EdgeColor3, this.colorFactory, ColorItem.Red,    this.UpdateButtonGeometryColor, this.UpdateButtonStateColor));
			this.buttons.Add (new ActiveButton (ActiveElement.EdgeColor4, this.colorFactory, ColorItem.Lilac,  this.UpdateButtonGeometryColor, this.UpdateButtonStateColor));
			this.buttons.Add (new ActiveButton (ActiveElement.EdgeColor5, this.colorFactory, ColorItem.Purple, this.UpdateButtonGeometryColor, this.UpdateButtonStateColor));
			this.buttons.Add (new ActiveButton (ActiveElement.EdgeColor6, this.colorFactory, ColorItem.Blue,   this.UpdateButtonGeometryColor, this.UpdateButtonStateColor));
			this.buttons.Add (new ActiveButton (ActiveElement.EdgeColor7, this.colorFactory, ColorItem.Green,  this.UpdateButtonGeometryColor, this.UpdateButtonStateColor));
			this.buttons.Add (new ActiveButton (ActiveElement.EdgeColor8, this.colorFactory, ColorItem.Grey,   this.UpdateButtonGeometryColor, this.UpdateButtonStateColor));
		}

		private void UpdateButtonGeometryExtend(ActiveButton button)
		{
			button.Center = this.PositionExtendButton;
		}

		private void UpdateButtonGeometryClose(ActiveButton button)
		{
			button.Center = this.PositionCloseButton;
		}

		private void UpdateButtonGeometryComment(ActiveButton button)
		{
			button.Center = this.PositionCommentButton;
		}

		private void UpdateButtonGeometryType(ActiveButton button)
		{
			button.Center = this.PositionTypeButton;
		}

		private void UpdateButtonGeometryChangeWidth(ActiveButton button)
		{
			button.Center = this.PositionChangeWidthButton;
		}

		private void UpdateButtonGeometryColor(ActiveButton button)
		{
			int rank = button.Element - ActiveElement.EdgeColor1;

			button.Center = this.PositionColorButton (rank);
		}

		private void UpdateButtonStateExtend(ActiveButton button)
		{
			button.State.Hilited = this.hilitedElement == button.Element;
			button.Glyph = this.isExtended ? GlyphShape.ArrowUp : GlyphShape.ArrowDown;
			button.State.Visible = this.IsHeaderHilite && !this.IsDragging;
		}

		private void UpdateButtonStateClose(ActiveButton button)
		{
			button.State.Hilited = this.hilitedElement == button.Element;
			button.State.Visible = this.IsHeaderHilite && !this.IsDragging && this.isExtended;
			button.State.Detectable = this.isExtended;
		}

		private void UpdateButtonStateComment(ActiveButton button)
		{
			button.State.Hilited = this.hilitedElement == button.Element;
			button.State.Visible = this.IsHeaderHilite && !this.IsDragging && this.isExtended;
			button.State.Detectable = this.isExtended;
		}

		private void UpdateButtonStateType(ActiveButton button)
		{
			button.State.Hilited = this.hilitedElement == button.Element;
			button.State.Visible = this.IsHeaderHilite && !this.IsDragging && this.isExtended;
			button.State.Detectable = this.isExtended;
		}

		private void UpdateButtonStateChangeWidth(ActiveButton button)
		{
			button.State.Hilited = this.hilitedElement == button.Element;
			button.State.Visible = button.State.Hilited || (this.IsHeaderHilite && !this.IsDragging && this.isExtended);
			button.State.Detectable = this.isExtended;
		}

		private void UpdateButtonStateColor(ActiveButton button)
		{
			button.State.Hilited = this.hilitedElement == button.Element;
			button.State.Selected = this.colorFactory.ColorItem == button.ColorItem;
			button.State.Visible = this.IsHeaderHilite && !this.IsDragging && this.isExtended;
			button.State.Detectable = this.isExtended;
		}

		private bool IsDragging
		{
			get
			{
				return this.isDragging || this.isChangeWidth || this.editor.IsEditing;
			}
		}


		#region Serialization
		public void WriteXml(XmlWriter writer)
		{
			//	S�rialise toutes les informations de la bo�te et de ses champs.
#if false
			writer.WriteStartElement(Xml.Box);
			
			writer.WriteElementString(Xml.Druid, this.cultureMap.Id.ToString());
			writer.WriteElementString(Xml.Bounds, this.bounds.ToString());
			writer.WriteElementString(Xml.IsExtended, this.isExtended.ToString(System.Globalization.CultureInfo.InvariantCulture));

			if (this.columnsSeparatorRelative1 != 0.5)
			{
				writer.WriteElementString(Xml.ColumnsSeparatorRelative1, this.columnsSeparatorRelative1.ToString(System.Globalization.CultureInfo.InvariantCulture));
			}
			
			writer.WriteElementString(Xml.Color, this.boxColor.ToString());

			foreach (Field field in this.fields)
			{
				field.WriteXml(writer);
			}

			if (this.comment != null && this.comment.IsVisible)  // commentaire associ� ?
			{
				this.comment.WriteXml(writer);
			}
			
			if (this.info != null && this.info.IsVisible)  // informations associ�es ?
			{
				this.info.WriteXml(writer);
			}
			
			writer.WriteEndElement();
#endif
		}

		public void ReadXml(XmlReader reader)
		{
#if false
			this.fields.Clear();

			reader.Read();

			while (true)
			{
				if (reader.NodeType == XmlNodeType.Element)
				{
					string name = reader.LocalName;

					if (name == Xml.Field)
					{
						Field field = new Field(this.editor);
						field.ReadXml(reader);
						reader.Read();
						this.fields.Add(field);
					}
					else if (name == Xml.Comment)
					{
						this.comment = new ObjectComment(this.editor);
						this.comment.ReadXml(reader);
						this.comment.AttachObject = this;
						this.comment.UpdateHeight();  // adapte la hauteur en fonction du contenu
						this.editor.AddComment(this.comment);
						reader.Read();
					}
					else if (name == Xml.Info)
					{
						this.info = new ObjectInfo(this.editor);
						this.info.ReadXml(reader);
						this.info.AttachObject = this;
						this.info.UpdateHeight();  // adapte la hauteur en fonction du contenu
						this.editor.AddInfo(this.info);
						reader.Read();
					}
					else
					{
						string element = reader.ReadElementString();

						if (name == Xml.Druid)
						{
							Druid druid = Druid.Parse(element);
							if (druid.IsValid)
							{
								Module module = this.SearchModule(druid);
								this.cultureMap = module.AccessEntities.Accessor.Collection[druid];
							}
						}
						else if (name == Xml.Bounds)
						{
							this.bounds = Rectangle.Parse(element);
						}
						else if (name == Xml.IsExtended)
						{
							this.isExtended = bool.Parse(element);
						}
						else if (name == Xml.ColumnsSeparatorRelative1)
						{
							this.columnsSeparatorRelative1 = double.Parse(element);
						}
						else if (name == Xml.Color)
						{
							this.boxColor = (ColorItem) System.Enum.Parse(typeof(ColorItem), element);
						}
						else
						{
							throw new System.NotSupportedException(string.Format("Unexpected XML node {0} found in box", name));
						}
					}
				}
				else if (reader.NodeType == XmlNodeType.EndElement)
				{
					System.Diagnostics.Debug.Assert(reader.Name == Xml.Box);
					break;
				}
				else
				{
					reader.Read();
				}
			}
#endif
		}

		public void AdjustAfterRead()
		{
			//	Ajuste le contenu de la bo�te apr�s sa d�s�rialisation.
		}
		#endregion


		public static readonly Size				frameSize = new Size (200, 36);
		private static readonly double			extendedHeight = 80;

		private string							titleString;
		private TextLayout						title;
		private string							subtitleString;
		private TextLayout						subtitle;
		private string							descriptionString;
		private TextLayout						description;

		private bool							isDragging;
		private bool							isChangeWidth;
		private double							changeWidthPos;
		private double							changeWidthInitial;

		private ActiveElement					editingElement;
		private AbstractTextField				editingTextField;
		private AbstractTextField				editingTextField2;
	}
}

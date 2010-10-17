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
	public class ObjectNode : LinkableObject
	{
		public ObjectNode(Editor editor, AbstractEntity entity)
			: base (editor, entity)
		{
			System.Diagnostics.Debug.Assert (this.Entity != null);

			this.title = new TextLayout();
			this.title.DefaultFontSize = 24;
			this.title.Alignment = ContentAlignment.MiddleCenter;
			this.title.BreakMode = TextBreakMode.Ellipsis | TextBreakMode.Split | TextBreakMode.SingleLine;

			this.isRoot = false;

			if (this.Entity.Name.IsNullOrWhiteSpace)
			{
				this.TitleNumber = this.editor.GetNodeTitleNumbrer ();
			}

			this.UpdateTitle ();

			this.SetBounds (new Rectangle (0, 0, ObjectNode.frameRadius*2, ObjectNode.frameRadius*2));
		}


		public int TitleNumber
		{
			//	Titre au sommet de la bo�te (nom du noeud).
			get
			{
				int value;
				if (int.TryParse (this.Entity.Name.ToSimpleText (), out value))
				{
					return value;
				}
				else
				{
					return -1;
				}
			}
			set
			{
				this.Entity.Name = value.ToString ();
				this.UpdateTitle ();
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

		public override void Move(double dx, double dy)
		{
			//	D�place l'objet.
			this.bounds.Offset(dx, dy);
			this.UpdateButtonsGeometry ();
		}

		public override void CreateInitialLinks()
		{
			this.objectLinks.Clear ();

			foreach (var entityEdge in this.Entity.Edges)
			{
				var link = new ObjectLink (this.editor, this.Entity);
				link.SrcObject = this;
				link.DstObject = this.editor.SearchInitialObject (entityEdge);

				this.objectLinks.Add (link);
			}
		}


		public override void SetBoundsAtEnd(Point start, Point end)
		{
			Point center = Point.Move (end, start, -ObjectNode.frameRadius);
			Rectangle rect = new Rectangle (center.X-ObjectNode.frameRadius, center.Y-ObjectNode.frameRadius, ObjectNode.frameRadius*2, ObjectNode.frameRadius*2);

			this.SetBounds (rect);
		}


		public override void RemoveEntityLink(LinkableObject dst)
		{
			System.Diagnostics.Debug.Assert (dst.AbstractEntity is WorkflowEdgeEntity);
			System.Diagnostics.Debug.Assert (this.Entity.Edges.Contains (dst.AbstractEntity as WorkflowEdgeEntity));

			this.Entity.Edges.Remove (dst.AbstractEntity as WorkflowEdgeEntity);
		}

		public override void AddEntityLink(LinkableObject dst)
		{
			System.Diagnostics.Debug.Assert (dst.AbstractEntity is WorkflowEdgeEntity);

			this.Entity.Edges.Add (dst.AbstractEntity as WorkflowEdgeEntity);
		}


		public override Vector GetLinkVector(double angle, bool isDst)
		{
			Point c = this.bounds.Center;
			double r = this.isRoot ? ObjectNode.frameRadius+2 : ObjectNode.frameRadius;

			Point p1 = Transform.RotatePointDeg (c, angle, new Point (c.X+r, c.Y));
			Point p2 = Transform.RotatePointDeg (c, angle, new Point (c.X+r+1, c.Y));

			return new Vector (p1, p2);
		}

		public override Point GetLinkStumpPos(double angle)
		{
			Point c = this.bounds.Center;
			return Transform.RotatePointDeg (c, angle, new Point (c.X+ObjectNode.frameRadius+ObjectLink.lengthStumpLink, c.Y));
		}


		public bool IsRoot
		{
			//	Indique s'il s'agit de la bo�te racine, c'est-�-dire de la bo�te affich�e avec un cadre gras.
			get
			{
				return this.isRoot;
			}
			set
			{
				if (this.isRoot != value)
				{
					this.isRoot = value;

					this.editor.Invalidate();
				}
			}
		}

		public bool IsConnectedToRoot
		{
			//	Indique si cet objet est connect� � la racine (flag temporaire).
			get
			{
				return this.isConnectedToRoot;
			}
			set
			{
				this.isConnectedToRoot = value;
			}
		}


		public override void AcceptEdition()
		{
			this.Entity.Name = this.editingTextField.Text;
			this.UpdateTitle ();

			this.StopEdition ();
		}

		public override void CancelEdition()
		{
			this.StopEdition ();
		}

		private void StartEdition(ActiveElement element)
		{
			Rectangle rect = this.RectangleEditName;

			Point p1 = this.editor.ConvEditorToWidget (rect.TopLeft);
			Point p2 = this.editor.ConvEditorToWidget (rect.BottomRight);
			double width  = System.Math.Max (p2.X-p1.X, 30);
			double height = System.Math.Max (p1.Y-p2.Y, 20);
			
			rect = new Rectangle (new Point (p1.X, p1.Y-height), new Size (width, height));

			this.editingTextField = new TextField ();
			this.editingTextField.Parent = this.editor;
			this.editingTextField.SetManualBounds (rect);
			this.editingTextField.Text = this.Entity.Name.ToString ();
			this.editingTextField.TabIndex = 1;
			this.editingTextField.SelectAll ();
			this.editingTextField.Focus ();

			this.editor.EditingObject = this;
			this.hilitedElement = ActiveElement.None;
		}

		private void StopEdition()
		{
			this.editor.Children.Remove (this.editingTextField);
			this.editingTextField = null;

			this.editor.EditingObject = null;
		}


		protected override string GetToolTipText(ActiveElement element)
		{
			//	Retourne le texte pour le tooltip.
			if (this.isDragging)
			{
				return null;  // pas de tooltip
			}

			return base.GetToolTipText(element);
		}

		public override bool MouseMove(Message message, Point pos)
		{
			//	Met en �vidence la bo�te selon la position de la souris.
			//	Si la souris est dans cette bo�te, retourne true.
			if (this.isDragging)
			{
				this.DraggingMouseMove (pos);
				return true;
			}

			return base.MouseMove (message, pos);
		}

		public override void MouseDown(Message message, Point pos)
		{
			//	Le bouton de la souris est press�.
			base.MouseDown (message, pos);

			this.initialPos = pos;

			if (this.hilitedElement == ActiveElement.NodeHeader && this.editor.LinkableObjectsCount > 1)
			{
				this.isDragging = true;
				this.draggingOffset = pos-this.bounds.BottomLeft;
				this.editor.Invalidate();
				this.editor.LockObject(this);
			}
		}

		public override void MouseUp(Message message, Point pos)
		{
			//	Le bouton de la souris est rel�ch�.
			base.MouseUp (message, pos);

			if (pos == this.initialPos)
			{
				if (this.hilitedElement == ActiveElement.NodeHeader)
				{
					if (this.isDragging)
					{
						this.editor.UpdateAfterGeometryChanged (this);
						this.isDragging = false;
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
				this.editor.LockObject (null);
				this.editor.SetLocalDirty ();
				return;
			}

			if (this.hilitedElement == ActiveElement.NodeOpenLink)
			{
				//	Cr�e un moignon de lien o--->
				var link = new ObjectLink (this.editor, this.entity);
				link.SrcObject = this;
				link.SetStumpAngle (this.ComputeBestStumpAngle ());
				link.UpdateLink ();

				this.objectLinks.Add (link);
				this.editor.UpdateAfterGeometryChanged (null);
			}

			if (this.hilitedElement == ActiveElement.NodeClose)
			{
				if (!this.isRoot)
				{
					this.editor.CloseObject(this);
					this.editor.UpdateAfterGeometryChanged (null);
				}
			}

			if (this.hilitedElement == ActiveElement.NodeComment)
			{
				this.AddComment();
			}

			if (this.hilitedElement == ActiveElement.NodeColor1)
			{
				this.BackgroundMainColor = MainColor.Yellow;
			}

			if (this.hilitedElement == ActiveElement.NodeColor2)
			{
				this.BackgroundMainColor = MainColor.Orange;
			}

			if (this.hilitedElement == ActiveElement.NodeColor3)
			{
				this.BackgroundMainColor = MainColor.Red;
			}

			if (this.hilitedElement == ActiveElement.NodeColor4)
			{
				this.BackgroundMainColor = MainColor.Lilac;
			}

			if (this.hilitedElement == ActiveElement.NodeColor5)
			{
				this.BackgroundMainColor = MainColor.Purple;
			}

			if (this.hilitedElement == ActiveElement.NodeColor6)
			{
				this.BackgroundMainColor = MainColor.Blue;
			}

			if (this.hilitedElement == ActiveElement.NodeColor7)
			{
				this.BackgroundMainColor = MainColor.Green;
			}

			if (this.hilitedElement == ActiveElement.NodeColor8)
			{
				this.BackgroundMainColor = MainColor.Grey;
			}
		}

		public override ActiveElement MouseDetectBackground(Point pos)
		{
			//	D�tecte l'�l�ment actif vis� par la souris.
			ActiveElement element = base.MouseDetectBackground (pos);
			if (element != ActiveElement.None)
			{
				return element;
			}

			if (this.bounds.Contains (pos))
			{
				return ActiveElement.NodeHeader;
			}

			if (this.bounds.Contains (pos))
			{
				return ActiveElement.NodeInside;
			}

			return ActiveElement.None;
		}

		public override ActiveElement MouseDetectForeground(Point pos)
		{
			//	D�tecte l'�l�ment actif vis� par la souris.
			ActiveElement element = base.MouseDetectForeground (pos);
			if (element != ActiveElement.None)
			{
				return element;
			}

			if (this.editor.CurrentModifyMode != Editor.ModifyMode.Locked)
			{
				return this.DetectButtons (pos);
			}

			return ActiveElement.None;
		}

		public override bool IsMousePossible(ActiveElement element)
		{
			//	Indique si l'op�ration est possible.
			return true;
		}


		public override string DebugInformations
		{
			get
			{
				return string.Format ("Node: {0} {1}", this.Entity.Name.ToString (), this.DebugInformationsObjectLinks);
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

				Rectangle rect = this.bounds;
				rect.Top = rect.Top+50;
				rect.Width = 200;
				this.comment.SetBounds (rect);
				this.comment.UpdateHeight ();  // adapte la hauteur en fonction du contenu

				this.editor.AddComment (this.comment);
				this.editor.UpdateAfterCommentChanged ();

				this.comment.EditComment ();  // �dite tout de suite le texte du commentaire
			}
			else
			{
				this.comment.IsVisible = !this.comment.IsVisible;
			}

			this.editor.SetLocalDirty ();
		}


		public override void DrawBackground(Graphics graphics)
		{
			//	Dessine le fond de l'objet.
			//	H�ritage	->	Traitill�
			//	Interface	->	Trait plein avec o---
			Rectangle rect;

			bool dragging = (this.hilitedElement == ActiveElement.NodeHeader || this.isHilitedForLinkChanging);

			//	Dessine l'ombre.
			rect = this.bounds;
			if (this.isRoot)
			{
				rect.Inflate(2);
			}
			rect.Offset(ObjectNode.shadowOffset, -(ObjectNode.shadowOffset));
			this.DrawNode2Shadow (graphics, rect, (int) ObjectNode.shadowOffset, 0.2);

			//	Construit le chemin du cadre.
			rect = this.bounds;
			rect.Deflate(1);
			Path path = this.PathNode2Rectangle (rect);

			//	Dessine l'int�rieur en blanc.
			graphics.Rasterizer.AddSurface(path);
			graphics.RenderSolid(this.colorEngine.GetColor(1));

			//	Dessine l'int�rieur en d�grad�.
			graphics.Rasterizer.AddSurface(path);
			Color c1 = this.colorEngine.GetColorMain (dragging ? 0.8 : 0.4);
			Color c2 = this.colorEngine.GetColorMain (dragging ? 0.4 : 0.1);
			this.RenderHorizontalGradient(graphics, this.bounds, c1, c2);

			Color colorLine = this.colorEngine.GetColor (0.9);
			if (dragging)
			{
				colorLine = this.colorEngine.GetColorMain (0.3);
			}

			Color colorFrame = dragging ? this.colorEngine.GetColorMain () : this.colorEngine.GetColor (0);

			//	Dessine le titre.
			Color titleColor = dragging ? this.colorEngine.GetColor (1) : this.colorEngine.GetColor (0);

			rect = this.bounds;
			rect.Offset (0, 2);
			this.title.LayoutSize = rect.Size;
			this.title.Paint(rect.BottomLeft, graphics, Rectangle.MaxValue, titleColor, GlyphPaintStyle.Normal);

			//	Dessine le cadre en noir.
			graphics.Rasterizer.AddOutline (path, this.isRoot ? 6 : 2);
			graphics.RenderSolid (colorFrame);
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

				return (this.hilitedElement == ActiveElement.NodeHeader ||
						this.hilitedElement == ActiveElement.NodeComment ||
						this.hilitedElement == ActiveElement.NodeColor1 ||
						this.hilitedElement == ActiveElement.NodeColor2 ||
						this.hilitedElement == ActiveElement.NodeColor3 ||
						this.hilitedElement == ActiveElement.NodeColor4 ||
						this.hilitedElement == ActiveElement.NodeColor5 ||
						this.hilitedElement == ActiveElement.NodeColor6 ||
						this.hilitedElement == ActiveElement.NodeColor7 ||
						this.hilitedElement == ActiveElement.NodeColor8 ||
						this.hilitedElement == ActiveElement.NodeOpenLink ||
						this.hilitedElement == ActiveElement.NodeClose);
			}
		}

		private Rectangle RectangleEditName
		{
			get
			{
				Rectangle rect = this.bounds;
				rect.Deflate (15, 20);

				return rect;
			}
		}

		private Point PositionCommentButton
		{
			//	Retourne la position du bouton pour montrer le commentaire.
			get
			{
				return new Point (this.bounds.Center.X-AbstractObject.buttonRadius-1, this.bounds.Top-AbstractObject.buttonRadius-9);
			}
		}

		private Point PositionCloseButton
		{
			//	Retourne la position du bouton pour fermer.
			get
			{
				return new Point (this.bounds.Center.X+AbstractObject.buttonRadius+1, this.bounds.Top-AbstractObject.buttonRadius-9);
			}
		}

		private Point PositionOpenLinkButton
		{
			//	Retourne la position du bouton pour ouvrir.
			//	Le bouton est plac� dans la direction o� sera ouvert la connexion.
			get
			{
				if (!this.HasNoneDstObject)
				{
					Point c = this.bounds.Center;
					double a = this.ComputeBestStumpAngle ();

					return Transform.RotatePointDeg (c, a, new Point (c.X+ObjectNode.frameRadius, c.Y));
				}
				else
				{
					return Point.Zero;
				}
			}
		}

		private Point PositionColorButton(int rank)
		{
			//	Retourne la position du bouton pour choisir la couleur.
			double y = this.bounds.Bottom;

			Point p = this.PositionOpenLinkButton;
			if (!p.IsZero)
			{
				//	Sous le bouton 'OpenLink', avec un petit chevauchement.
				y = System.Math.Min (y, p.Y-AbstractObject.buttonRadius-2);
			}

			return new Point (this.bounds.Center.X + (2*AbstractObject.buttonSquare+1)*(rank-3.5) + 0.5, y);
		}

		private string GetGroupTooltip(int rank)
		{
			//	Retourne le tooltip � afficher pour un groupe.
			return null;  // pas de tooltip
		}


		private void UpdateTitle()
		{
			//	Met � jour le titre du noeud.
			this.title.Text = Misc.Bold (this.Entity.Name.ToString ());
		}


		public WorkflowNodeEntity Entity
		{
			get
			{
				return this.entity as WorkflowNodeEntity;
			}
		}


		protected override void CreateButtons()
		{
			this.buttons.Add (new ActiveButton (ActiveElement.NodeOpenLink, this.colorEngine, GlyphShape.Plus,  this.UpdateButtonGeometryOpenLink, this.UpdateButtonStateOpenLink));
			this.buttons.Add (new ActiveButton (ActiveElement.NodeClose,    this.colorEngine, GlyphShape.Close, this.UpdateButtonGeometryClose,    this.UpdateButtonStateClose));
			this.buttons.Add (new ActiveButton (ActiveElement.NodeComment,  this.colorEngine, "C",              this.UpdateButtonGeometryComment,  this.UpdateButtonStateComment));

			this.buttons.Add (new ActiveButton (ActiveElement.NodeColor1, this.colorEngine, MainColor.Yellow, this.UpdateButtonGeometryColor, this.UpdateButtonStateColor));
			this.buttons.Add (new ActiveButton (ActiveElement.NodeColor2, this.colorEngine, MainColor.Orange, this.UpdateButtonGeometryColor, this.UpdateButtonStateColor));
			this.buttons.Add (new ActiveButton (ActiveElement.NodeColor3, this.colorEngine, MainColor.Red,    this.UpdateButtonGeometryColor, this.UpdateButtonStateColor));
			this.buttons.Add (new ActiveButton (ActiveElement.NodeColor4, this.colorEngine, MainColor.Lilac,  this.UpdateButtonGeometryColor, this.UpdateButtonStateColor));
			this.buttons.Add (new ActiveButton (ActiveElement.NodeColor5, this.colorEngine, MainColor.Purple, this.UpdateButtonGeometryColor, this.UpdateButtonStateColor));
			this.buttons.Add (new ActiveButton (ActiveElement.NodeColor6, this.colorEngine, MainColor.Blue,   this.UpdateButtonGeometryColor, this.UpdateButtonStateColor));
			this.buttons.Add (new ActiveButton (ActiveElement.NodeColor7, this.colorEngine, MainColor.Green,  this.UpdateButtonGeometryColor, this.UpdateButtonStateColor));
			this.buttons.Add (new ActiveButton (ActiveElement.NodeColor8, this.colorEngine, MainColor.Grey,   this.UpdateButtonGeometryColor, this.UpdateButtonStateColor));
		}

		private void UpdateButtonGeometryOpenLink(ActiveButton button)
		{
			button.Center = this.PositionOpenLinkButton;
		}

		private void UpdateButtonGeometryClose(ActiveButton button)
		{
			button.Center = this.PositionCloseButton;
		}

		private void UpdateButtonGeometryComment(ActiveButton button)
		{
			button.Center = this.PositionCommentButton;
		}

		private void UpdateButtonGeometryColor(ActiveButton button)
		{
			int rank = button.Element - ActiveElement.NodeColor1;

			button.Center = this.PositionColorButton (rank);
		}

		private void UpdateButtonStateOpenLink(ActiveButton button)
		{
			button.State.Hilited = this.hilitedElement == button.Element;
			button.State.Visible = this.IsHeaderHilite && !this.isDragging;
		}

		private void UpdateButtonStateClose(ActiveButton button)
		{
			button.State.Enable = !this.isRoot;
			button.State.Hilited = this.hilitedElement == button.Element;
			button.State.Visible = this.IsHeaderHilite && !this.isDragging;
		}

		private void UpdateButtonStateComment(ActiveButton button)
		{
			button.State.Hilited = this.hilitedElement == button.Element;
			button.State.Visible = this.IsHeaderHilite && !this.isDragging;
		}

		private void UpdateButtonStateColor(ActiveButton button)
		{
			button.State.Hilited = this.hilitedElement == button.Element;
			button.State.Selected = this.colorEngine.MainColor == button.Color;
			button.State.Visible = this.IsHeaderHilite && !this.isDragging;
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
							this.boxColor = (MainColor) System.Enum.Parse(typeof(MainColor), element);
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


		private static readonly double			frameRadius = 30;
		private static readonly double			shadowOffset = 6;

		private bool							isRoot;
		private bool							isConnectedToRoot;
		private TextLayout						title;

		private bool							isDragging;
		private Point							initialPos;

		private AbstractTextField				editingTextField;
	}
}

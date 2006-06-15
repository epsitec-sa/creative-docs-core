using Epsitec.Common.Drawing;
using Epsitec.Common.Widgets;

namespace Epsitec.Common.Designer
{
	/// <summary>
	/// La classe ObjectModifier permet de g�rer les 'widgets' de Designer.
	/// </summary>
	public class ObjectModifier
	{
		public enum Placement
		{
			Anchored,
			VerticalDocked,
			HorizontalDocked,
			VerticalStacked,
			HorizontalStacked,
		}

		public enum AnchoredHorizontalAttachment
		{
			Left,
			Right,
			Fill,
		}

		public enum AnchoredVerticalAttachment
		{
			Bottom,
			Top,
			Fill,
		}

		public enum DockedHorizontalAttachment
		{
			Left,
			Right,
			Fill,
		}

		public enum DockedVerticalAttachment
		{
			Bottom,
			Top,
			Fill,
		}

		public enum DockedHorizontalAlignment
		{
			Stretch,
			Center,
			Left,
			Right,
		}

		public enum DockedVerticalAlignment
		{
			Stretch,
			Center,
			Bottom,
			Top,
		}


		public ObjectModifier(MyWidgets.PanelEditor panelEditor)
		{
			//	Constructeur unique.
			this.panelEditor = panelEditor;
			this.container = this.panelEditor.Panel;
		}


		public Placement GetChildrenPlacement(Widget obj)
		{
			//	Retourne le mode de placement des enfants de l'objet.
			//	Uniquement pour les objects AbstractGroup.
			AbstractGroup group = obj as AbstractGroup;
			System.Diagnostics.Debug.Assert(group != null);

			if (group.ChildrenLayoutMode == Widgets.Layouts.LayoutMode.Anchored)
			{
				return Placement.Anchored;
			}

			if (group.ChildrenLayoutMode == Widgets.Layouts.LayoutMode.Docked)
			{
				if (group.ContainerLayoutMode == ContainerLayoutMode.HorizontalFlow)
				{
					return Placement.HorizontalDocked;
				}
				else
				{
					return Placement.VerticalDocked;
				}
			}

			throw new System.Exception("Not supported.");
		}

		public void SetChildrenPlacement(Widget obj, Placement mode)
		{
			//	Choix du mode de placement des enfants de l'objet.
			//	Uniquement pour les objects AbstractGroup.
			AbstractGroup group = obj as AbstractGroup;
			System.Diagnostics.Debug.Assert(group != null);

			switch (mode)
			{
				case Placement.Anchored:
					group.ChildrenLayoutMode = Widgets.Layouts.LayoutMode.Anchored;
					break;

				case Placement.HorizontalDocked:
					group.ChildrenLayoutMode = Widgets.Layouts.LayoutMode.Docked;
					group.ContainerLayoutMode = ContainerLayoutMode.HorizontalFlow;
					break;

				case Placement.VerticalDocked:
					group.ChildrenLayoutMode = Widgets.Layouts.LayoutMode.Docked;
					group.ContainerLayoutMode = ContainerLayoutMode.VerticalFlow;
					break;
			}
		}


		public Rectangle GetBounds(Widget obj)
		{
			//	Retourne la position et les dimensions de l'objet.
			obj.Window.ForceLayout();
			Rectangle bounds = obj.Client.Bounds;

			while (obj != this.container)
			{
				bounds = obj.MapClientToParent(bounds);
				obj = obj.Parent;
			}

			return bounds;
		}

		public void SetBounds(Widget obj, Rectangle bounds)
		{
			//	Choix de la position et des dimensions de l'objet.
			//	Uniquement pour les objets Anchored.
			Placement placement = this.GetParentPlacement(obj);
			System.Diagnostics.Debug.Assert(placement == Placement.Anchored);

			bounds.Normalise();

			if (bounds.Width < obj.MinWidth)
			{
				bounds.Width = obj.MinWidth;
			}

			if (bounds.Height < obj.MinHeight)
			{
				bounds.Height = obj.MinHeight;
			}

			obj.Window.ForceLayout();
			Widget parent = obj.Parent;
			while (parent != this.container)
			{
				bounds = parent.MapParentToClient(bounds);
				parent = parent.Parent;
			}

			parent = obj.Parent;
			Rectangle box = parent.ActualBounds;
			Margins margins = obj.Margins;
			Margins padding = parent.Padding + parent.GetInternalPadding();
			AnchoredHorizontalAttachment ha = this.GetAnchoredHorizontalAttachment(obj);
			AnchoredVerticalAttachment va = this.GetAnchoredVerticalAttachment(obj);

			if (ha == AnchoredHorizontalAttachment.Left || ha == AnchoredHorizontalAttachment.Fill)
			{
				double px = bounds.Left;
				px -= padding.Left;
				px = System.Math.Max(px, 0);
				margins.Left = px;
			}

			if (ha == AnchoredHorizontalAttachment.Right || ha == AnchoredHorizontalAttachment.Fill)
			{
				double px = box.Width - bounds.Right;
				px -= padding.Right;
				px = System.Math.Max(px, 0);
				margins.Right = px;
			}

			if (va == AnchoredVerticalAttachment.Bottom || va == AnchoredVerticalAttachment.Fill)
			{
				double py = bounds.Bottom;
				py -= padding.Bottom;
				py = System.Math.Max(py, 0);
				margins.Bottom = py;
			}

			if (va == AnchoredVerticalAttachment.Top || va == AnchoredVerticalAttachment.Fill)
			{
				double py = box.Height - bounds.Top;
				py -= padding.Top;
				py = System.Math.Max(py, 0);
				margins.Top = py;
			}

			obj.Margins = margins;
			obj.PreferredSize = bounds.Size;

			this.Invalidate();
		}


		public Margins GetMargins(Widget obj)
		{
			//	Retourne les marges de l'objet.
			//	Uniquement pour les objets Docked.
			Placement placement = this.GetParentPlacement(obj);
			System.Diagnostics.Debug.Assert(placement == Placement.HorizontalDocked || placement == Placement.VerticalDocked);

			return obj.Margins;
		}

		public void SetMargins(Widget obj, Margins margins)
		{
			//	Choix des marges de l'objet.
			//	Uniquement pour les objets Docked.
			Placement placement = this.GetParentPlacement(obj);
			System.Diagnostics.Debug.Assert(placement == Placement.HorizontalDocked || placement == Placement.VerticalDocked);

			if (obj.Margins != margins)
			{
				obj.Margins = margins;
				this.Invalidate();
			}
		}


		public double GetWidth(Widget obj)
		{
			//	Retourne la largeur de l'objet.
			//	Uniquement pour les objets HorizontalDocked.
			Placement placement = this.GetParentPlacement(obj);
			System.Diagnostics.Debug.Assert(placement == Placement.HorizontalDocked);

			return obj.PreferredWidth;
		}

		public void SetWidth(Widget obj, double width)
		{
			//	Choix de la largeur de l'objet.
			//	Uniquement pour les objets VerticalDocked.
			Placement placement = this.GetParentPlacement(obj);
			System.Diagnostics.Debug.Assert(placement == Placement.HorizontalDocked);

			if (obj.PreferredWidth != width)
			{
				obj.PreferredWidth = width;
				this.Invalidate();
			}
		}


		public double GetHeight(Widget obj)
		{
			//	Retourne la hauteur de l'objet.
			//	Uniquement pour les objets VerticalDocked.
			Placement placement = this.GetParentPlacement(obj);
			System.Diagnostics.Debug.Assert(placement == Placement.VerticalDocked);

			return obj.PreferredHeight;
		}

		public void SetHeight(Widget obj, double height)
		{
			//	Choix de la hauteur de l'objet.
			//	Uniquement pour les objets HorizontalDocked.
			Placement placement = this.GetParentPlacement(obj);
			System.Diagnostics.Debug.Assert(placement == Placement.VerticalDocked);

			if (obj.PreferredHeight != height)
			{
				obj.PreferredHeight = height;
				this.Invalidate();
			}
		}


		public int GetZOrder(Widget obj)
		{
			//	Retourne l'ordre de l'objet.
			return obj.ZOrder;
		}

		public void SetZOrder(Widget obj, int order)
		{
			//	Choix de l'ordre de l'objet.
			if (obj.ZOrder != order)
			{
				obj.ZOrder = order;
				this.Invalidate();
			}
		}


		#region Anchored
		public AnchoredVerticalAttachment GetAnchoredVerticalAttachment(Widget obj)
		{
			//	Retourne l'attachement vertical de l'objet.
			//	Uniquement pour les objets Anchored.
			Placement placement = this.GetParentPlacement(obj);
			System.Diagnostics.Debug.Assert(placement == Placement.Anchored);

			AnchorStyles style = obj.Anchor;
			bool bottom = ((style & AnchorStyles.Bottom) != 0);
			bool top    = ((style & AnchorStyles.Top   ) != 0);

			if (bottom && top)  return AnchoredVerticalAttachment.Fill;
			if (bottom       )  return AnchoredVerticalAttachment.Bottom;
			if (top          )  return AnchoredVerticalAttachment.Top;

			throw new System.Exception("Not supported.");
		}

		public void SetAnchoredVerticalAttachment(Widget obj, AnchoredVerticalAttachment attachment)
		{
			//	Choix de l'attachement vertical de l'objet.
			//	Uniquement pour les objets Anchored.
			Placement placement = this.GetParentPlacement(obj);
			System.Diagnostics.Debug.Assert(placement == Placement.Anchored);

			AnchorStyles style = obj.Anchor;

			switch (attachment)
			{
				case AnchoredVerticalAttachment.Bottom:
					style |=  AnchorStyles.Bottom;
					style &= ~AnchorStyles.Top;
					break;

				case AnchoredVerticalAttachment.Top:
					style |=  AnchorStyles.Top;
					style &= ~AnchorStyles.Bottom;
					break;

				case AnchoredVerticalAttachment.Fill:
					style |=  AnchorStyles.Bottom;
					style |=  AnchorStyles.Top;
					break;
			}

			if (obj.Anchor != style)
			{
				obj.Anchor = style;
				this.Invalidate();
			}
		}


		public AnchoredHorizontalAttachment GetAnchoredHorizontalAttachment(Widget obj)
		{
			//	Retourne l'attachement horizontal de l'objet.
			//	Uniquement pour les objets Anchored.
			Placement placement = this.GetParentPlacement(obj);
			System.Diagnostics.Debug.Assert(placement == Placement.Anchored);

			AnchorStyles style = obj.Anchor;
			bool left  = ((style & AnchorStyles.Left ) != 0);
			bool right = ((style & AnchorStyles.Right) != 0);

			if (left && right)  return AnchoredHorizontalAttachment.Fill;
			if (left         )  return AnchoredHorizontalAttachment.Left;
			if (right        )  return AnchoredHorizontalAttachment.Right;

			throw new System.Exception("Not supported.");
		}

		public void SetAnchoredHorizontalAttachment(Widget obj, AnchoredHorizontalAttachment attachment)
		{
			//	Choix de l'attachement horizontal de l'objet.
			//	Uniquement pour les objets Anchored.
			Placement placement = this.GetParentPlacement(obj);
			System.Diagnostics.Debug.Assert(placement == Placement.Anchored);

			AnchorStyles style = obj.Anchor;

			switch (attachment)
			{
				case AnchoredHorizontalAttachment.Left:
					style |=  AnchorStyles.Left;
					style &= ~AnchorStyles.Right;
					break;

				case AnchoredHorizontalAttachment.Right:
					style |=  AnchorStyles.Right;
					style &= ~AnchorStyles.Left;
					break;

				case AnchoredHorizontalAttachment.Fill:
					style |=  AnchorStyles.Left;
					style |=  AnchorStyles.Right;
					break;
			}

			if (obj.Anchor != style)
			{
				obj.Anchor = style;
				this.Invalidate();
			}
		}
		#endregion


		#region Docked
		public DockedVerticalAttachment GetDockedVerticalAttachment(Widget obj)
		{
			//	Retourne l'attachement vertical de l'objet.
			//	Uniquement pour les objets VerticalDocked.
			Placement placement = this.GetParentPlacement(obj);
			System.Diagnostics.Debug.Assert(placement == Placement.VerticalDocked);

			DockStyle style = obj.Dock;
			if (style == DockStyle.Fill  )  return DockedVerticalAttachment.Fill;
			if (style == DockStyle.Bottom)  return DockedVerticalAttachment.Bottom;
			if (style == DockStyle.Top   )  return DockedVerticalAttachment.Top;

			throw new System.Exception("Not supported.");
		}

		public void SetDockedVerticalAttachment(Widget obj, DockedVerticalAttachment attachment)
		{
			//	Choix de l'attachement vertical de l'objet.
			//	Uniquement pour les objets VerticalDocked.
			Placement placement = this.GetParentPlacement(obj);
			System.Diagnostics.Debug.Assert(placement == Placement.VerticalDocked);

			DockStyle style = obj.Dock;

			switch (attachment)
			{
				case DockedVerticalAttachment.Bottom:
					style = DockStyle.Bottom;
					break;

				case DockedVerticalAttachment.Top:
					style = DockStyle.Top;
					break;

				case DockedVerticalAttachment.Fill:
					style = DockStyle.Fill;
					break;
			}

			if (obj.Dock != style)
			{
				obj.Dock = style;
				this.Invalidate();
			}
		}


		public DockedHorizontalAttachment GetDockedHorizontalAttachment(Widget obj)
		{
			//	Retourne l'attachement horizontal de l'objet.
			//	Uniquement pour les objets HorizontalDocked.
			Placement placement = this.GetParentPlacement(obj);
			System.Diagnostics.Debug.Assert(placement == Placement.HorizontalDocked);

			DockStyle style = obj.Dock;
			if (style == DockStyle.Fill )  return DockedHorizontalAttachment.Fill;
			if (style == DockStyle.Left )  return DockedHorizontalAttachment.Left;
			if (style == DockStyle.Right)  return DockedHorizontalAttachment.Right;

			throw new System.Exception("Not supported.");
		}

		public void SetDockedHorizontalAttachment(Widget obj, DockedHorizontalAttachment attachment)
		{
			//	Choix de l'attachement horizontal de l'objet.
			//	Uniquement pour les objets HorizontalDocked.
			Placement placement = this.GetParentPlacement(obj);
			System.Diagnostics.Debug.Assert(placement == Placement.HorizontalDocked);

			DockStyle style = obj.Dock;

			switch (attachment)
			{
				case DockedHorizontalAttachment.Left:
					style = DockStyle.Left;
					break;

				case DockedHorizontalAttachment.Right:
					style = DockStyle.Right;
					break;

				case DockedHorizontalAttachment.Fill:
					style = DockStyle.Fill;
					break;
			}

			if (obj.Dock != style)
			{
				obj.Dock = style;
				this.Invalidate();
			}
		}


		public DockedHorizontalAlignment GetDockedHorizontalAlignment(Widget obj)
		{
			//	Retourne l'alignement horizontal de l'objet.
			//	Uniquement pour les objets VerticalDocked.
			Placement placement = this.GetParentPlacement(obj);
			System.Diagnostics.Debug.Assert(placement == Placement.VerticalDocked);

			HorizontalAlignment ha = obj.HorizontalAlignment;
			if (ha == HorizontalAlignment.Stretch)  return DockedHorizontalAlignment.Stretch;
			if (ha == HorizontalAlignment.Center )  return DockedHorizontalAlignment.Center;
			if (ha == HorizontalAlignment.Left   )  return DockedHorizontalAlignment.Left;
			if (ha == HorizontalAlignment.Right  )  return DockedHorizontalAlignment.Right;

			throw new System.Exception("Not supported.");
		}

		public void SetDockedHorizontalAlignment(Widget obj, DockedHorizontalAlignment alignment)
		{
			//	Choix de l'alignement horizontal de l'objet.
			//	Uniquement pour les objets VerticalDocked.
			Placement placement = this.GetParentPlacement(obj);
			System.Diagnostics.Debug.Assert(placement == Placement.VerticalDocked);

			HorizontalAlignment ha = obj.HorizontalAlignment;

			switch (alignment)
			{
				case DockedHorizontalAlignment.Stretch:
					ha = HorizontalAlignment.Stretch;
					break;

				case DockedHorizontalAlignment.Center:
					ha = HorizontalAlignment.Center;
					break;

				case DockedHorizontalAlignment.Left:
					ha = HorizontalAlignment.Left;
					break;

				case DockedHorizontalAlignment.Right:
					ha = HorizontalAlignment.Right;
					break;
			}

			if (obj.HorizontalAlignment != ha)
			{
				obj.HorizontalAlignment = ha;
				this.Invalidate();
			}
		}


		public DockedVerticalAlignment GetDockedVerticalAlignment(Widget obj)
		{
			//	Retourne l'alignement vertical de l'objet.
			//	Uniquement pour les objets HorizontalDocked.
			Placement placement = this.GetParentPlacement(obj);
			System.Diagnostics.Debug.Assert(placement == Placement.HorizontalDocked);

			VerticalAlignment va = obj.VerticalAlignment;
			if (va == VerticalAlignment.Stretch)  return DockedVerticalAlignment.Stretch;
			if (va == VerticalAlignment.Center )  return DockedVerticalAlignment.Center;
			if (va == VerticalAlignment.Bottom )  return DockedVerticalAlignment.Bottom;
			if (va == VerticalAlignment.Top    )  return DockedVerticalAlignment.Top;

			throw new System.Exception("Not supported.");
		}

		public void SetDockedVerticalAlignment(Widget obj, DockedVerticalAlignment alignment)
		{
			//	Choix de l'alignement vertical de l'objet.
			//	Uniquement pour les objets HorizontalDocked.
			Placement placement = this.GetParentPlacement(obj);
			System.Diagnostics.Debug.Assert(placement == Placement.HorizontalDocked);

			VerticalAlignment va = obj.VerticalAlignment;

			switch (alignment)
			{
				case DockedVerticalAlignment.Stretch:
					va = VerticalAlignment.Stretch;
					break;

				case DockedVerticalAlignment.Center:
					va = VerticalAlignment.Center;
					break;

				case DockedVerticalAlignment.Bottom:
					va = VerticalAlignment.Bottom;
					break;

				case DockedVerticalAlignment.Top:
					va = VerticalAlignment.Top;
					break;
			}

			if (obj.VerticalAlignment != va)
			{
				obj.VerticalAlignment = va;
				this.Invalidate();
			}
		}
		#endregion


		protected Placement GetParentPlacement(Widget obj)
		{
			//	Retourne le mode de placement du parent d'un objet.
			System.Diagnostics.Debug.Assert(obj != this.container);
			return this.GetChildrenPlacement(obj.Parent);
		}

		protected void Invalidate()
		{
			//	Invalide le PanelEditor.
			this.panelEditor.Invalidate();
		}


		protected MyWidgets.PanelEditor				panelEditor;
		protected UI.Panel							container;
	}
}

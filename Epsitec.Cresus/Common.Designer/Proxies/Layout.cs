using System.Collections.Generic;

using Epsitec.Common.Types;
using Epsitec.Common.Widgets;
using Epsitec.Common.Drawing;

namespace Epsitec.Common.Designer.Proxies
{
	public class Layout : Abstract
	{
		public Layout(Widget widget, ObjectModifier objectModifier) : base(widget, objectModifier)
		{
		}

		public override int Rank
		{
			//	Retourne le rang de ce proxy parmi la liste de tous les proxies.
			//	Plus le num�ro est petit, plus le proxy appara�tra haut dans la
			//	liste.
			get
			{
				return 2;
			}
		}

		public override string IconName
		{
			get
			{
				return "PropertyLayout";
			}
		}

		public override double DataColumnWidth
		{
			get
			{
				return 80;
			}
		}

		public ObjectModifier.ChildrenPlacement ChildrenPlacement
		{
			get
			{
				return (ObjectModifier.ChildrenPlacement) this.GetValue(Layout.ChildrenPlacementProperty);
			}
			set
			{
				this.SetValue(Layout.ChildrenPlacementProperty, value);
			}
		}

		public ObjectModifier.AnchoredHorizontalAttachment AnchoredHorizontalAttachment
		{
			get
			{
				return (ObjectModifier.AnchoredHorizontalAttachment) this.GetValue(Layout.AnchoredHorizontalAttachmentProperty);
			}
			set
			{
				this.SetValue(Layout.AnchoredHorizontalAttachmentProperty, value);
			}
		}

		public ObjectModifier.AnchoredVerticalAttachment AnchoredVerticalAttachment
		{
			get
			{
				return (ObjectModifier.AnchoredVerticalAttachment) this.GetValue(Layout.AnchoredVerticalAttachmentProperty);
			}
			set
			{
				this.SetValue(Layout.AnchoredVerticalAttachmentProperty, value);
			}
		}

		public ObjectModifier.DockedHorizontalAttachment DockedHorizontalAttachment
		{
			get
			{
				return (ObjectModifier.DockedHorizontalAttachment) this.GetValue(Layout.DockedHorizontalAttachmentProperty);
			}
			set
			{
				this.SetValue(Layout.DockedHorizontalAttachmentProperty, value);
			}
		}

		public ObjectModifier.DockedVerticalAttachment DockedVerticalAttachment
		{
			get
			{
				return (ObjectModifier.DockedVerticalAttachment) this.GetValue(Layout.DockedVerticalAttachmentProperty);
			}
			set
			{
				this.SetValue(Layout.DockedVerticalAttachmentProperty, value);
			}
		}

		public ObjectModifier.DockedHorizontalAlignment DockedHorizontalAlignment
		{
			get
			{
				return (ObjectModifier.DockedHorizontalAlignment) this.GetValue(Layout.DockedHorizontalAlignmentProperty);
			}
			set
			{
				this.SetValue(Layout.DockedHorizontalAlignmentProperty, value);
			}
		}

		public ObjectModifier.DockedVerticalAlignment DockedVerticalAlignment
		{
			get
			{
				return (ObjectModifier.DockedVerticalAlignment) this.GetValue(Layout.DockedVerticalAlignmentProperty);
			}
			set
			{
				this.SetValue(Layout.DockedVerticalAlignmentProperty, value);
			}
		}


		protected override void InitialisePropertyValues()
		{
			//	Cette m�thode est appel�e par Proxies.Abstract quand on connecte
			//	le premier widget avec le proxy.
			
			//	Recopie localement les diverses propri�t�s du widget s�lectionn�
			//	pour pouvoir ensuite travailler dessus :
			if (this.objectModifier.IsChildrenPlacement(this.widgets[0]))
			{
				ObjectModifier.ChildrenPlacement cp = this.objectModifier.GetChildrenPlacement(this.widgets[0]);

				this.ChildrenPlacement = cp;
			}

			if (this.objectModifier.IsChildrenAnchored(this.widgets[0].Parent))
			{
				ObjectModifier.AnchoredHorizontalAttachment ha = this.objectModifier.GetAnchoredHorizontalAttachment(this.widgets[0]);
				ObjectModifier.AnchoredVerticalAttachment va = this.objectModifier.GetAnchoredVerticalAttachment(this.widgets[0]);

				this.AnchoredHorizontalAttachment = ha;
				this.AnchoredVerticalAttachment = va;
			}

			if (this.objectModifier.IsDockedHorizontalAttachment(this.widgets[0]))
			{
				ObjectModifier.DockedHorizontalAttachment ha = this.objectModifier.GetDockedHorizontalAttachment(this.widgets[0]);

				this.DockedHorizontalAttachment = ha;
			}

			if (this.objectModifier.IsDockedVerticalAttachment(this.widgets[0]))
			{
				ObjectModifier.DockedVerticalAttachment va = this.objectModifier.GetDockedVerticalAttachment(this.widgets[0]);

				this.DockedVerticalAttachment = va;
			}

			if (this.objectModifier.IsDockedHorizontalAlignment(this.widgets[0]))
			{
				ObjectModifier.DockedHorizontalAlignment ha = this.objectModifier.GetDockedHorizontalAlignment(this.widgets[0]);

				this.DockedHorizontalAlignment = ha;
			}

			if (this.objectModifier.IsDockedVerticalAlignment(this.widgets[0]))
			{
				ObjectModifier.DockedVerticalAlignment va = this.objectModifier.GetDockedVerticalAlignment(this.widgets[0]);

				this.DockedVerticalAlignment = va;
			}
		}

		private static void NotifyChildrenPlacementChanged(DependencyObject o, object oldValue, object newValue)
		{
			//	Cette m�thode est appel�e � la suite de la modification d'une de
			//	nos propri�t�s de d�finition pour permettre de mettre � jour les
			//	widgets connect�s :
			Layout that = (Layout) o;
			ObjectModifier.ChildrenPlacement cp = that.ChildrenPlacement;

			foreach (Widget obj in that.widgets)
			{
				that.objectModifier.SetChildrenPlacement(obj, cp);
			}
		}

		private static void NotifyAnchoredHorizontalAttachmentChanged(DependencyObject o, object oldValue, object newValue)
		{
			//	Cette m�thode est appel�e � la suite de la modification d'une de
			//	nos propri�t�s de d�finition pour permettre de mettre � jour les
			//	widgets connect�s :
			Layout that = (Layout) o;
			ObjectModifier.AnchoredHorizontalAttachment cp = that.AnchoredHorizontalAttachment;

			foreach (Widget obj in that.widgets)
			{
				that.objectModifier.SetAnchoredHorizontalAttachment(obj, cp);
			}
		}

		private static void NotifyAnchoredVerticalAttachmentChanged(DependencyObject o, object oldValue, object newValue)
		{
			//	Cette m�thode est appel�e � la suite de la modification d'une de
			//	nos propri�t�s de d�finition pour permettre de mettre � jour les
			//	widgets connect�s :
			Layout that = (Layout) o;
			ObjectModifier.AnchoredVerticalAttachment cp = that.AnchoredVerticalAttachment;

			foreach (Widget obj in that.widgets)
			{
				that.objectModifier.SetAnchoredVerticalAttachment(obj, cp);
			}
		}

		private static void NotifyDockedHorizontalAttachmentChanged(DependencyObject o, object oldValue, object newValue)
		{
			//	Cette m�thode est appel�e � la suite de la modification d'une de
			//	nos propri�t�s de d�finition pour permettre de mettre � jour les
			//	widgets connect�s :
			Layout that = (Layout) o;
			ObjectModifier.DockedHorizontalAttachment cp = that.DockedHorizontalAttachment;

			foreach (Widget obj in that.widgets)
			{
				that.objectModifier.SetDockedHorizontalAttachment(obj, cp);
			}
		}

		private static void NotifyDockedVerticalAttachmentChanged(DependencyObject o, object oldValue, object newValue)
		{
			//	Cette m�thode est appel�e � la suite de la modification d'une de
			//	nos propri�t�s de d�finition pour permettre de mettre � jour les
			//	widgets connect�s :
			Layout that = (Layout) o;
			ObjectModifier.DockedVerticalAttachment cp = that.DockedVerticalAttachment;

			foreach (Widget obj in that.widgets)
			{
				that.objectModifier.SetDockedVerticalAttachment(obj, cp);
			}
		}

		private static void NotifyDockedHorizontalAlignmentChanged(DependencyObject o, object oldValue, object newValue)
		{
			//	Cette m�thode est appel�e � la suite de la modification d'une de
			//	nos propri�t�s de d�finition pour permettre de mettre � jour les
			//	widgets connect�s :
			Layout that = (Layout) o;
			ObjectModifier.DockedHorizontalAlignment cp = that.DockedHorizontalAlignment;

			foreach (Widget obj in that.widgets)
			{
				that.objectModifier.SetDockedHorizontalAlignment(obj, cp);
			}
		}

		private static void NotifyDockedVerticalAlignmentChanged(DependencyObject o, object oldValue, object newValue)
		{
			//	Cette m�thode est appel�e � la suite de la modification d'une de
			//	nos propri�t�s de d�finition pour permettre de mettre � jour les
			//	widgets connect�s :
			Layout that = (Layout) o;
			ObjectModifier.DockedVerticalAlignment cp = that.DockedVerticalAlignment;

			foreach (Widget obj in that.widgets)
			{
				that.objectModifier.SetDockedVerticalAlignment(obj, cp);
			}
		}


		public static readonly DependencyProperty ChildrenPlacementProperty = DependencyProperty.Register("ChildrenPlacement", typeof(ObjectModifier.ChildrenPlacement), typeof(Layout), new DependencyPropertyMetadata(ObjectModifier.ChildrenPlacement.Anchored, Layout.NotifyChildrenPlacementChanged));
		public static readonly DependencyProperty AnchoredHorizontalAttachmentProperty = DependencyProperty.Register("AnchoredHorizontalAttachment", typeof(ObjectModifier.AnchoredHorizontalAttachment), typeof(Layout), new DependencyPropertyMetadata(ObjectModifier.AnchoredHorizontalAttachment.Left, Layout.NotifyAnchoredHorizontalAttachmentChanged));
		public static readonly DependencyProperty AnchoredVerticalAttachmentProperty = DependencyProperty.Register("AnchoredVerticalAttachment", typeof(ObjectModifier.AnchoredVerticalAttachment), typeof(Layout), new DependencyPropertyMetadata(ObjectModifier.AnchoredVerticalAttachment.Bottom, Layout.NotifyAnchoredVerticalAttachmentChanged));
		public static readonly DependencyProperty DockedHorizontalAttachmentProperty = DependencyProperty.Register("DockedHorizontalAttachment", typeof(ObjectModifier.DockedHorizontalAttachment), typeof(Layout), new DependencyPropertyMetadata(ObjectModifier.DockedHorizontalAttachment.Left, Layout.NotifyDockedHorizontalAttachmentChanged));
		public static readonly DependencyProperty DockedVerticalAttachmentProperty = DependencyProperty.Register("DockedVerticalAttachment", typeof(ObjectModifier.DockedVerticalAttachment), typeof(Layout), new DependencyPropertyMetadata(ObjectModifier.DockedVerticalAttachment.Bottom, Layout.NotifyDockedVerticalAttachmentChanged));
		public static readonly DependencyProperty DockedHorizontalAlignmentProperty = DependencyProperty.Register("DockedHorizontalAlignment", typeof(ObjectModifier.DockedHorizontalAlignment), typeof(Layout), new DependencyPropertyMetadata(ObjectModifier.DockedHorizontalAlignment.Stretch, Layout.NotifyDockedHorizontalAlignmentChanged));
		public static readonly DependencyProperty DockedVerticalAlignmentProperty = DependencyProperty.Register("DockedVerticalAlignment", typeof(ObjectModifier.DockedVerticalAlignment), typeof(Layout), new DependencyPropertyMetadata(ObjectModifier.DockedVerticalAlignment.Stretch, Layout.NotifyDockedVerticalAlignmentChanged));
	}
}

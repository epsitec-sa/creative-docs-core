using Epsitec.Common.Widgets;
using Epsitec.Common.Support;
using Epsitec.Common.Drawing;

namespace Epsitec.Common.Designer
{
	/// <summary>
	/// Contient le contexte commun � tous les Viewers.
	/// </summary>
	public class PanelsContext
	{
		public PanelsContext()
		{
		}


		public string Tool
		{
			get
			{
				return this.tool;
			}
			set
			{
				this.tool = value;
			}
		}

		public bool ShowGrid
		{
			get
			{
				return this.showGrid;
			}
			set
			{
				this.showGrid = value;
			}
		}

		public bool ShowConstrain
		{
			get
			{
				return this.showConstrain;
			}
			set
			{
				this.showConstrain = value;
			}
		}

		public bool ShowAttachment
		{
			get
			{
				return this.showAttachment;
			}
			set
			{
				this.showAttachment = value;
			}
		}

		public bool ShowExpand
		{
			get
			{
				return this.showExpand;
			}
			set
			{
				this.showExpand = value;
			}
		}

		public bool ShowZOrder
		{
			get
			{
				return this.showZOrder;
			}
			set
			{
				this.showZOrder = value;

				if (this.showZOrder)
				{
					this.showTabIndex = false;
				}
			}
		}

		public bool ShowTabIndex
		{
			get
			{
				return this.showTabIndex;
			}
			set
			{
				this.showTabIndex = value;

				if (this.showTabIndex)
				{
					this.showZOrder = false;
				}
			}
		}

		public double GridStep
		{
			get
			{
				return this.gridStep;
			}
			set
			{
				this.gridStep = value;
			}
		}

		public double ConstrainMargin
		{
			get
			{
				return this.constrainMargin;
			}
			set
			{
				this.constrainMargin = value;
			}
		}

		public Size ConstrainSpacing
		{
			get
			{
				return this.constrainSpacing;
			}
			set
			{
				this.constrainSpacing = value;
			}
		}

		public Margins ConstrainGroupMargins
		{
			get
			{
				return this.constrainGroupMargins;
			}
			set
			{
				this.constrainGroupMargins = value;
			}
		}

		public double Leading
		{
			get
			{
				return this.leading;
			}
			set
			{
				this.leading = value;
			}
		}

		public double MinimalSize
		{
			get
			{
				return this.minimalSize;
			}
			set
			{
				this.minimalSize = value;
			}
		}

		public double ZOrderThickness
		{
			get
			{
				return this.zOrderThickness;
			}
			set
			{
				this.zOrderThickness = value;
			}
		}


		#region Static colors
		static public Color ColorHiliteOutline
		{
			//	Couleur lorsqu'un objet est survol� par la souris.
			get
			{
				IAdorner adorner = Epsitec.Common.Widgets.Adorners.Factory.Active;
				return Color.FromColor(adorner.ColorCaption, 0.8);
			}
		}

		static public Color ColorHiliteSurface
		{
			//	Couleur lorsqu'un objet est survol� par la souris.
			get
			{
				IAdorner adorner = Epsitec.Common.Widgets.Adorners.Factory.Active;
				return Color.FromColor(adorner.ColorCaption, 0.4);
			}
		}

		static public Color ColorHiliteParent
		{
			//	Couleur lorsqu'un objet parent est survol� par la souris.
			get
			{
				IAdorner adorner = Epsitec.Common.Widgets.Adorners.Factory.Active;
				return adorner.ColorCaption;
			}
		}

		static public Color ColorOutsurface
		{
			//	Couleur pour la surface hors du panneau.
			get
			{
				return Color.FromAlphaRgb(0.2, 0.5, 0.5, 0.5);
			}
		}

		static public Color ColorZOrder
		{
			//	Couleur pour les chiffres du ZOrder.
			get
			{
				return Color.FromRgb(1, 0, 0);
			}
		}

		static public Color ColorTabIndex
		{
			//	Couleur pour les chiffres de l'ordre pour la touche Tab.
			get
			{
				return Color.FromRgb(0, 0, 1);
			}
		}

		static public Color ColorAttachment
		{
			//	Couleur pour repr�senter un ressort.
			get
			{
				return Color.FromRgb(0, 0, 1);
			}
		}

		static public Color ColorGrid1
		{
			//	Couleur pour la grille magn�tique primaire (division principale).
			get
			{
				return Color.FromAlphaRgb(0.2, 0.4, 0.4, 0.4);
			}
		}

		static public Color ColorGrid2
		{
			//	Couleur pour la grille magn�tique secondaire (subdivision).
			get
			{
				return Color.FromAlphaRgb(0.2, 0.7, 0.7, 0.7);
			}
		}

		static public Color ColorHandleNormal
		{
			//	Couleur pour une poign�e.
			get
			{
				return Color.FromRgb(1, 0, 0);
			}
		}

		static public Color ColorHandleHilited
		{
			//	Couleur lorsqu'un objet est survol� par la souris.
			get
			{
				IAdorner adorner = Epsitec.Common.Widgets.Adorners.Factory.Active;
				return adorner.ColorCaption;
			}
		}
		#endregion


		protected string				tool = "ToolSelect";
		protected bool					showGrid = false;
		protected bool					showConstrain = true;
		protected bool					showAttachment = true;
		protected bool					showExpand = false;
		protected bool					showZOrder = false;
		protected bool					showTabIndex = false;
		protected double				gridStep = 10;
		protected double				constrainMargin = 5;
		protected Size					constrainSpacing = new Size(10, 5);
		protected Margins				constrainGroupMargins = new Margins(12, 12, 12+7, 12);
		protected double				leading = 30;
		protected double				minimalSize = 3;
		protected double				zOrderThickness = 2;
	}
}

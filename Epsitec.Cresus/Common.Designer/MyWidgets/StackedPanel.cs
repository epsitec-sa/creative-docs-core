using Epsitec.Common.Widgets;
using Epsitec.Common.Widgets.Layouts;
using Epsitec.Common.Support;
using Epsitec.Common.Drawing;

namespace Epsitec.Common.Designer.MyWidgets
{
	/// <summary>
	/// La classe StackedPanel est la classe de base pour tous les panels empil�s.
	/// </summary>
	public class StackedPanel : Widget
	{
		public StackedPanel()
		{
			this.Padding = new Margins(15+10, 10, 5-1, 5);

			this.container = new Widget(this);
			this.container.Dock = DockStyle.Fill;
			this.container.TabIndex = 0;
			this.container.TabNavigation = TabNavigationMode.ForwardTabPassive;

			this.Entered += new MessageEventHandler(this.HandleMouseEntered);
			this.Exited += new MessageEventHandler(this.HandleMouseExited);
		}
		
		public StackedPanel(Widget embedder) : this()
		{
			this.SetEmbedder(embedder);
		}

		protected override void Dispose(bool disposing)
		{
			if ( disposing )
			{
				this.Entered -= new MessageEventHandler(this.HandleMouseEntered);
				this.Exited -= new MessageEventHandler(this.HandleMouseExited);
			}
			
			base.Dispose(disposing);
		}


		public bool IsLeftPart
		{
			//	true  -> panneau de gauche pour la culture primaire
			//	false -> panneau de droite pour la culture secondaire
			get
			{
				return this.isLeftPart;
			}

			set
			{
				this.isLeftPart = value;
			}
		}

		public bool IsNewSection
		{
			//	Indique si le panneau d�bute une nouvelle section.
			get
			{
				return this.isNewSection;
			}

			set
			{
				if (this.isNewSection != value)
				{
					this.isNewSection = value;
					this.UpdateExtendButton();
				}
			}
		}

		public GlyphButton ExtendButton
		{
			//	Retourne l'�ventuel bouton permettant d'�tendre le panneau.
			get
			{
				return this.extendButton;
			}
		}

		public GlyphShape ExtendShape
		{
			//	Aspect du bouton permettant d'�tendre le panneau.
			//	GlyphShape.None correspond � un bouton inexistant.
			get
			{
				if (this.extendButton == null)
				{
					return GlyphShape.None;
				}
				else
				{
					return this.extendButton.GlyphShape;
				}
			}

			set
			{
				if (value == GlyphShape.None)
				{
					if (this.extendButton != null)
					{
						this.extendButton.Dispose();
					}

					this.extendButton = null;
				}
				else
				{
					if (this.extendButton == null)
					{
						this.extendButton = new GlyphButton(this);
						this.extendButton.Anchor = AnchorStyles.TopLeft;
						this.extendButton.PreferredSize = new Size(16, 16);
						this.UpdateExtendButton();
					}

					this.extendButton.GlyphShape = value;
				}
			}
		}

		protected void UpdateExtendButton()
		{
			//	Met � jour la position du bouton permettant d'�tendre le panneau.
			if (this.extendButton != null)
			{
				double left = this.Padding.Left;
				double top = this.Padding.Top + (this.isNewSection ? 1 : 0);
				this.extendButton.Margins = new Margins(-left, 0, -top, 0);
			}
		}

		public string Title
		{
			//	Texte du titre affich� en haut � gauche du panneau.
			get
			{
				return this.title;
			}

			set
			{
				if (this.title != value)
				{
					this.title = value;

					if (string.IsNullOrEmpty(this.title))
					{
						this.titleLayout = null;
					}
					else
					{
						ToolTip.Default.SetToolTip(this, this.title);

						if (this.titleLayout == null)
						{
							this.titleLayout = new TextLayout();
							this.titleLayout.Alignment = ContentAlignment.MiddleCenter;
						}

						this.titleLayout.Text = string.Concat("<font size=\"75%\">", this.title, "</font>");
					}
				}
			}
		}

		public Color BackgroundColor
		{
			//	Couleur du fond du panneau.
			get
			{
				return this.backgroundColor;
			}

			set
			{
				if (this.backgroundColor != value)
				{
					this.backgroundColor = value;
					this.Invalidate();
				}
			}
		}

		public Widget Container
		{
			//	Donne le container � utiliser comme parent pour tous les enfants.
			get
			{
				return this.container;
			}
		}

		
		private void HandleMouseEntered(object sender, MessageEventArgs e)
		{
			//	La souris est entr�e dans le panneau.
		}

		private void HandleMouseExited(object sender, MessageEventArgs e)
		{
			//	La souris est sortie du panneau.
		}


		protected override void PaintBackgroundImplementation(Graphics graphics, Rectangle clipRect)
		{
			IAdorner adorner = Epsitec.Common.Widgets.Adorners.Factory.Active;
			Rectangle rect = this.Client.Bounds;

			if (!this.backgroundColor.IsEmpty)  // pas un s�parateur ?
			{
				graphics.AddFilledRectangle(rect);
				graphics.RenderSolid(this.backgroundColor);

				Rectangle r = rect;
				r.Width = 15;
				graphics.AddFilledRectangle(r);
				graphics.RenderSolid(this.backgroundColor);
			}

			rect.Deflate(0.5, 0.5);
			graphics.AddLine(rect.Left-0.5, rect.Bottom, rect.Right+0.5, rect.Bottom);  // - en bas

			if (!this.backgroundColor.IsEmpty)  // pas un s�parateur ?
			{
				graphics.AddLine(rect.Left+15, rect.Bottom-0.5, rect.Left+15, rect.Top+0.5);  // | +15 � gauche
			}

			if (this.isLeftPart)
			{
				graphics.AddLine(rect.Right, rect.Bottom-0.5, rect.Right, rect.Top+0.5);  // | � droite
			}

			graphics.RenderSolid(adorner.ColorBorder);

			if (this.titleLayout != null)
			{
				double w = rect.Height-2;
				if (this.extendButton != null)
				{
					w -= this.extendButton.PreferredHeight;
				}

				if (w > 22)
				{
					Point center = new Point(rect.Left+14, rect.Bottom+1);
					Transform it = graphics.Transform;
					graphics.RotateTransformDeg(90, center.X, center.Y);
					this.titleLayout.LayoutSize = new Size(w, 15);
					this.titleLayout.Paint(center, graphics);
					graphics.Transform = it;
				}
			}
		}


		protected Color						backgroundColor = Color.Empty;
		protected bool						isLeftPart = true;
		protected bool						isNewSection = false;
		protected string					title;
		protected TextLayout				titleLayout;
		protected GlyphButton				extendButton;
		protected Widget					container;
	}
}

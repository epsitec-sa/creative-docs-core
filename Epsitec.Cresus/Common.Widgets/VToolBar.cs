namespace Epsitec.Common.Widgets
{
	/// <summary>
	/// La classe VToolBar permet de r�aliser des tool bars verticales.
	/// </summary>
	public class VToolBar : Widget, Helpers.IWidgetCollectionHost
	{
		public VToolBar()
		{
			this.items = new Helpers.WidgetCollection(this);
			
			IconButton button = new IconButton();
			this.defaultButtonWidth = button.DefaultWidth;
			this.defaultButtonHeight = button.DefaultHeight;
			button.Dispose();

			double m = (this.DefaultWidth-this.defaultButtonWidth)/2;
			this.DockMargins = new Drawing.Margins(m, m, m, m);
		}
		
		public VToolBar(Widget embedder) : this()
		{
			this.SetEmbedder(embedder);
		}


		public override double				DefaultWidth
		{
			// Retourne la largeur standard d'une barre.
			get
			{
				return 28;
			}
		}

		public Helpers.WidgetCollection		Items
		{
			get { return this.items; }
		}

		
		protected override void Dispose(bool disposing)
		{
			if ( disposing )
			{
				Widget[] items = new Widget[this.items.Count];
				this.items.CopyTo(items, 0);
				this.items.Clear();
				
				foreach ( Widget item in items )
				{
					item.Dispose();
				}
				
				this.items.Dispose();
				this.items = null;
			}
			
			base.Dispose(disposing);
		}

		protected override void PaintBackgroundImplementation(Drawing.Graphics graphics, Drawing.Rectangle clipRect)
		{
			// Dessine la barre.
			IAdorner adorner = Widgets.Adorner.Factory.Active;

			Drawing.Rectangle rect  = new Drawing.Rectangle(0, 0, this.Client.Width, this.Client.Height);
			adorner.PaintToolBackground(graphics, rect, WidgetState.None, Direction.Left);
		}


		#region Interface IBundleSupport
		public override void RestoreFromBundle(Epsitec.Common.Support.ObjectBundler bundler, Epsitec.Common.Support.ResourceBundle bundle)
		{
			base.RestoreFromBundle (bundler, bundle);
			
			Support.ResourceBundle.FieldList item_list = bundle["items"].AsList;
			
			if (item_list != null)
			{
				//	Notre bundle contient une liste de sous-bundles contenant les descriptions des
				//	items composant le menu.
				
				foreach (Support.ResourceBundle.Field field in item_list)
				{
					Support.ResourceBundle item_bundle = field.AsBundle;
					Widget                 item_widget = bundler.CreateFromBundle (item_bundle) as Widget;
					
					this.Items.Add (item_widget);
				}
			}
		}
		#endregion
		
		#region IWidgetCollectionHost Members
		Helpers.WidgetCollection Helpers.IWidgetCollectionHost.GetWidgetCollection()
		{
			return this.Items;
		}
		
		public void NotifyInsertion(Widget widget)
		{
			widget.Dock = DockStyle.Top;
			widget.SetEmbedder (this);
		}

		public void NotifyRemoval(Widget widget)
		{
			this.Children.Remove (widget);
		}
		
		public void NotifyPostRemoval(Widget widget)
		{
		}
		#endregion
		
		protected Helpers.WidgetCollection	items;
		
		protected double					defaultButtonWidth;
		protected double					defaultButtonHeight;
	}
}

using System;

namespace Epsitec.Common.Widgets
{
	/// <summary>
	/// La classe TabPage représente une page du TabBook.
	/// </summary>
	public class TabPage : AbstractGroup
	{
		public TabPage()
		{
			this.tabButton = new TabButton(null);
			this.tabButton.Alignment = Drawing.ContentAlignment.MiddleCenter;
			
			this.TabNavigation = Widget.TabNavigationMode.ForwardTabPassive;
		}
		
		public TabPage(Widget embedder) : this()
		{
			this.SetEmbedder(embedder);
		}

		
		public string							TabTitle
		{
			get
			{
				return this.tabButton.Text;
			}

			set
			{
				this.tabButton.Text = value;
			}
		}

		public Drawing.Rectangle				TabBounds
		{
			get
			{
				return this.tabButton.Bounds;
			}

			set
			{
				this.tabButton.Bounds = value;
			}
		}

		public Drawing.Size						TabSize
		{
			get
			{
				TextLayout tl = new TextLayout(this.ResourceManager);
				
				tl.Text = this.tabButton.Text;
				
				return tl.SingleLineSize;
			}
		}
		
		public TabButton						TabButton
		{
			get
			{
				return this.tabButton;
			}
		}
		
		public TabBook							Book
		{
			get
			{
				TabBook book = this.Parent as TabBook;
				return book;
			}
		}
		
		public Direction						Direction
		{
			get
			{
				TabBook book = this.Book;
				
				if ( book == null )
				{
					return Direction.None;
				}
				
				return book.Direction;
			}
		}
		public int								Rank
		{
			get
			{
				return this.rank;
			}

			set
			{
				if ( this.rank != value )
				{
					this.rank     = value;
					this.TabIndex = this.rank;
					this.OnRankChanged();
				}
			}
		}
		
		
		public event Support.EventHandler		RankChanged;
		
		protected virtual void OnRankChanged()
		{
			if ( this.RankChanged != null )
			{
				this.RankChanged(this);
			}
		}

		
		// Dessine l'onglet.
		protected override void PaintBackgroundImplementation(Drawing.Graphics graphics, Drawing.Rectangle clipRect)
		{
			IAdorner adorner = Widgets.Adorner.Factory.Active;
		}
		

		protected int							rank;
		protected TabButton						tabButton;
	}
}

namespace Epsitec.Common.Widgets
{
	public enum MenuType
	{
		Horizontal,						// ligne horizontale de textes seuls
		Vertical,						// liste verticale compl�te
	}

	/// <summary>
	/// La classe Menu repr�sente un menu horizontal ou vertical.
	/// </summary>
	public class Menu : Widget
	{
		public enum TypeDeveloped
		{
			Close,
			Delay,
			Quick,
		}

		public Menu(MenuType type)
		{
			this.type = type;
		}

		protected override void Dispose(bool disposing)
		{
			if ( disposing )
			{
				System.Diagnostics.Debug.WriteLine("Dispose Menu " + this.Text);
				
				for ( int i=0 ; i<this.array.Length ; i++ )
				{
					Widget widget = this.array[i];
					MenuItem item = widget as MenuItem;
					if ( item != null && item.Submenu != null )
					{
						item.Submenu.Dispose();
					}
					this.array[i].Dispose();
					this.array[i] = null;
				}
				this.array = null;
			}
			
			base.Dispose(disposing);
		}


		// Retourne la hauteur standard d'un menu.
		public override double DefaultHeight
		{
			get
			{
				return this.DefaultFontHeight + 6 + this.margin*2;
			}
		}

		// Rectangle parent.
		public Drawing.Rectangle ParentRect
		{
			get
			{
				return this.parentRect;
			}

			set
			{
				this.parentRect = value;
			}
		}

		// Indique s'il faut d�velopper les sous-menus.
		public TypeDeveloped MenuDeveloped
		{
			get
			{
				return this.menuDeveloped;
			}

			set
			{
				this.menuDeveloped = value;
			}
		}

		// Ajoute une case.
		public int InsertItem(string iconName, string mainText, string shortKey)
		{
			MenuItem item = new MenuItem(this.type);
			item.OnlyText = false;
			item.IconName = iconName;
			item.MainText = mainText;
			item.ShortKey = shortKey;
			return this.Insert(item);
		}

		// Ajoute une case.
		public int InsertItem(string name)
		{
			MenuItem item = new MenuItem(this.type);
			item.OnlyText = true;
			item.MainText = name;
			return this.Insert(item);
		}

		// Ajoute un s�parateur -----.
		public int InsertSep()
		{
			MenuItem item = new MenuItem(this.type);
			item.Separator = true;
			return this.Insert(item);
		}

		// Ajoute une case.
		public int Insert(MenuItem cell)
		{
			int rank = this.totalUsed;
			this.AllocateArray(rank+1);
			this.array[rank].Dispose();
			this.array[rank] = cell;
			this.Children.Add(cell);
			this.totalUsed ++;
			this.isDirty = true;

			cell.Pressed += new MessageEventHandler(this.HandleCellPressed);
			cell.Entered += new MessageEventHandler(this.HandleCellEntered);
			cell.Exited  += new MessageEventHandler(this.HandleCellExited);

			return rank;
		}

		// Modifie une case.
		public void Modify(int rank, MenuItem cell)
		{
			this.AllocateArray(rank+1);
			this.array[rank].Dispose();
			this.array[rank] = cell;
			this.Children.Add(cell);
			this.isDirty = true;
		}

		// Objet occupant une case.		
		public MenuItem this[int rank]
		{
			get
			{
				System.Diagnostics.Debug.Assert(this.array[rank] != null);
				return this.array[rank];
			}
			
			set
			{
				System.Diagnostics.Debug.Assert(this.array[rank] != null);
				if ( value == null )  value = new MenuItem(this.type);
				
				this.array[rank] = value;
			}
		}

		// Sp�cifie le nombre de cases qui seront contenues dans la barre.
		// Cet appel est facultatif.
		public void SetSize(int max)
		{
			this.AllocateArray(max);
		}

		// Dimensionne le tableau des cases si n�cessaire.
		protected void AllocateArray(int max)
		{
			if ( this.array == null )
			{
				this.array = new MenuItem[0];  // alloue un tableau vide
			}

			if ( max <= this.array.Length )  return;  // d�j� assez grand ?

			MenuItem[] newArray = new MenuItem[max];  // nouveau tableau
			for ( int i=0 ; i<max ; i++ )
			{
				if ( i < this.array.Length )
				{
					newArray[i] = this.array[i];
				}
				else
				{
					newArray[i] = new MenuItem(this.type);
					newArray[i].Size = new Drawing.Size(0, 0);
				}
			}
			this.array = newArray;
		}

		// Ajuste les dimensions du menu selon son contenu.
		// Il faut appeler AdjustSize apr�s avoir fini tous les InsertItem.
		public void AdjustSize()
		{
			if ( this.type == MenuType.Vertical )
			{
				this.Size = this.RequiredSize;
			}
		}

		// Donne les dimensions n�cessaires pour tout le menu.
		protected Drawing.Size RequiredSize
		{
			get
			{
				double maxWidth = 0;
				foreach ( MenuItem cell in this.array )
				{
					maxWidth = System.Math.Max(maxWidth, cell.IconWidth);
				}
				this.iconWidth = maxWidth;

				Drawing.Size size = new Drawing.Size(0, 0);
				foreach ( MenuItem cell in this.array )
				{
					cell.IconWidth = maxWidth;
					Drawing.Size rs = cell.RequiredSize;
					size.Width = System.Math.Max(size.Width, rs.Width);
					size.Height += rs.Height;
				}
				size.Width  += this.margin*2;
				size.Height += this.margin*2;
				return size;
			}
		}

		// Met � jour si n�cessaire.
		protected void Update()
		{
			if ( !this.isDirty )  return;
			this.UpdateClientGeometry();
		}

		// Met � jour la g�om�trie du menu.
		protected override void UpdateClientGeometry()
		{
			base.UpdateClientGeometry();

			if ( this.array == null )  return;

			if ( this.type == MenuType.Horizontal )
			{
				double x = this.margin;
				foreach ( MenuItem cell in this.array )
				{
					Drawing.Size rs = cell.RequiredSize;
					Drawing.Rectangle rect = new Drawing.Rectangle();
					rect.Left   = x;
					rect.Right  = x+rs.Width;
					rect.Bottom = this.margin;
					rect.Top    = this.Height-this.margin;
					cell.Bounds = rect;
					x += rs.Width;
				}
			}

			if ( this.type == MenuType.Vertical )
			{
				double y = this.Height-this.margin;
				foreach ( MenuItem cell in this.array )
				{
					Drawing.Size rs = cell.RequiredSize;
					y -= rs.Height;
					Drawing.Rectangle rect = new Drawing.Rectangle();
					rect.Left   = this.margin;
					rect.Right  = this.Width-this.margin;
					rect.Bottom = y;
					rect.Top    = y+rs.Height;
					cell.Bounds = rect;
				}
			}

			this.isDirty = false;
		}


		// Gestion d'un �v�nement.
		protected override void ProcessMessage(Message message, Drawing.Point pos)
		{
			switch ( message.Type )
			{
				case MessageType.KeyDown:
					ProcessKeyDown(message.KeyCode);
					break;
			}
			
			message.Consumer = this;
		}

		// Gestion d'une touche press�e avec KeyDown dans le menu.
		protected void ProcessKeyDown(int key)
		{
			Menu parent = this.parentMenu;

			switch ( key )
			{
				case (int)System.Windows.Forms.Keys.Up:
					System.Diagnostics.Debug.WriteLine("ProcessKeyDown.Up "+this.Name);
					this.OtherMenuItem(-1);
					break;

				case (int)System.Windows.Forms.Keys.Down:
					System.Diagnostics.Debug.WriteLine("ProcessKeyDown.Down "+this.Name);
					this.OtherMenuItem(1);
					break;

				case (int)System.Windows.Forms.Keys.Left:
					System.Diagnostics.Debug.WriteLine("ProcessKeyDown.Left "+this.Name);
					if ( parent != null )
					{
						if ( parent.type == MenuType.Horizontal )
						{
							parent.CloseSubmenu();
							parent.OtherMenuItem(-1);
							MenuItem item = parent[parent.RetSelectMenuItem()];
							if ( parent.OpenSubmenu(item, true) )
							{
								parent.submenu.SelectMenuItem(0);
							}
						}
						else
						{
							if ( !this.CloseMenuItem() )
							{
								parent.CloseSubmenu();
							}
						}
					}
					break;

				case (int)System.Windows.Forms.Keys.Right:
					System.Diagnostics.Debug.WriteLine("ProcessKeyDown.Right "+this.Name);
					if ( parent == null )
					{
						this.OpenMenuItem();
					}
					else
					{
						int sel = this.RetSelectMenuItem();
						if ( sel == -1 || this[sel].Submenu == null )
						{
							while ( parent.parentMenu != null )  parent = parent.parentMenu;
							if ( parent.type == MenuType.Horizontal )
							{
								parent.CloseSubmenu();
								parent.OtherMenuItem(1);
								MenuItem item = parent[parent.RetSelectMenuItem()];
								if ( parent.OpenSubmenu(item, true) )
								{
									parent.submenu.SelectMenuItem(0);
								}
							}
						}
						else
						{
							if ( !this.OpenMenuItem() )
							{
								parent.CloseSubmenu();
								parent.OtherMenuItem(1);
							}
						}
					}
					break;

				case (int)System.Windows.Forms.Keys.Return:
				case (int)System.Windows.Forms.Keys.Space:
					break;

				case (int)System.Windows.Forms.Keys.Escape:
					System.Diagnostics.Debug.WriteLine("ProcessKeyDown.Escape "+this.Name);
					this.CloseAll();  // TODO: pourquoi �a plante avec Esc ?
					break;
			}
		}

		// S�lectionne la case suivante ou pr�c�dente.
		protected void OtherMenuItem(int dir)
		{
			int sel = this.RetSelectMenuItem();
			if ( sel == -1 )
			{
				sel = 0;
			}
			else
			{
				for ( int i=0 ; i<this.totalUsed ; i++ )
				{
					sel += dir;
					if ( sel >= this.totalUsed )
					{
						sel = 0;
					}
					if ( sel < 0 )
					{
						sel = this.totalUsed-1;
					}
					if ( !this[sel].Separator )  break;
				}
			}
			this.SelectMenuItem(sel);
		}

		// Ouvre le sous-menu correspondant � la case s�lectionn�e.
		protected bool OpenMenuItem()
		{
			int sel = this.RetSelectMenuItem();
			if ( sel == -1 )  return false;
			if ( !this.OpenSubmenu(this[sel], true) )  return false;
			this.submenu.SelectMenuItem(0);
			return true;
		}

		// Ferme le sous-menu contenant la case s�lectionn�e.
		protected bool CloseMenuItem()
		{
			if ( this.parentMenu == null )  return false;
			return this.parentMenu.CloseSubmenu();
		}

		// Retourne la case s�lectionn�e dans un menu.
		protected int RetSelectMenuItem()
		{
			int max = this.totalUsed;
			for ( int i=0 ; i<max ; i++ )
			{
				MenuItem mi = this[i];
				if ( mi.ItemType != MenuItemType.Deselect )  return i;
			}
			return -1;
		}

		// S�lectionne une (et une seule) case dans un menu.
		protected void SelectMenuItem(int rank)
		{
			int max = this.totalUsed;
			for ( int i=0 ; i<max ; i++ )
			{
				MenuItem mi = this[i];
				if ( i == rank )  // case s�lectionn�e ?
				{
					mi.ItemType = this.isActivated ? MenuItemType.Select : MenuItemType.Parent;
				}
				else
				{
					mi.ItemType = MenuItemType.Deselect;
				}
			}
		}

		// S�lectionne une (et une seule) case dans un menu.
		protected void SelectMenuItem(MenuItem item)
		{
			int max = this.totalUsed;
			for ( int i=0 ; i<max ; i++ )
			{
				MenuItem mi = this[i];
				if ( mi == item )  // case s�lectionn�e ?
				{
					mi.ItemType = this.isActivated ? MenuItemType.Select : MenuItemType.Parent;
				}
				else
				{
					mi.ItemType = MenuItemType.Deselect;
				}
			}
		}

		// Ferme compl�tement le menu et tous les sous-menus.
		protected void CloseAll()
		{
			Menu root = this;
			while ( root.parentMenu != null )  root = root.parentMenu;
			System.Diagnostics.Debug.WriteLine("CloseAll "+root.Name+" "+this.Name);

			root.CloseSubmenu();
			root.SelectMenuItem(null);
			root.menuDeveloped = TypeDeveloped.Close;
		}

		// Ouvre le sous-menu correspondant � un item.
		protected bool OpenSubmenu(MenuItem item, bool forceQuick)
		{
			if ( this.submenu == item.Submenu )  return false;
			bool closed = this.CloseSubmenu();
			this.submenu = item.Submenu;
			if ( this.submenu == null )  return false;
			System.Diagnostics.Debug.WriteLine("OpenSubmenu "+this.submenu.Name);

			this.isActivated = false;
			this.SelectMenuItem(item);  // s�lectionne la case parent

			this.submenu.isActivated = true;
			this.submenu.SelectMenuItem(null);  // d�s�lectionne tout
			this.submenu.MenuDeveloped = TypeDeveloped.Delay;
			this.submenu.parentMenu = this;

			Drawing.Point pos = new Drawing.Point(0, 0);

			if ( this.type == MenuType.Horizontal )
			{
				Drawing.Rectangle pRect = item.Bounds;
				pRect.Offset(-pRect.Left, -pRect.Bottom);
				pRect.Offset(this.submenu.Left, this.submenu.Bottom);
				this.submenu.ParentRect = pRect;

				pos = item.MapClientToRoot(new Drawing.Point(0, -this.submenu.Height));
			}

			if ( this.type == MenuType.Vertical )
			{
				this.submenu.ParentRect = Drawing.Rectangle.Empty;

				pos = item.MapClientToRoot(new Drawing.Point(item.Width, item.Height-this.submenu.Height+1));
#if false
				// TODO: Pourquoi la premi�re case est mal positionn�e verticalement ?
				// TODO: c'est MapWindowToScreen aui foire !
				Drawing.Point pp = item.WindowFrame.MapWindowToScreen(pos);
				System.Diagnostics.Debug.WriteLine("pos: "+item.Height+" "+this.submenu.Height+" "+item.Bottom+" "+pp.Y);
#endif
			}

			this.window = new WindowFrame();
			this.window.MakeFramelessWindow();
			//this.window.Owner = this.WindowFrame;
			pos = item.WindowFrame.MapWindowToScreen(pos);
			this.window.WindowBounds = new Drawing.Rectangle(pos.X, pos.Y, this.submenu.Width, this.submenu.Height);
			WindowFrame.ApplicationDeactivated += new EventHandler(this.HandleApplicationDeactivated);
			this.window.Root.Children.Add(this.submenu);

			Animation anim = Animation.None;
			if ( this.type == MenuType.Vertical || !closed )  anim = Animation.FadeIn;
			if ( forceQuick )  anim = Animation.None;
			this.window.AnimateShow(anim);
			this.submenu.SetFocused(true);
			return true;
		}

		// Ferme le sous-menu ouvert.
		protected bool CloseSubmenu()
		{
			if ( this.window == null )  return false;
			
			System.Diagnostics.Debug.WriteLine("CloseSubmenu "+this.submenu.Name);
			System.Diagnostics.Debug.Assert(this.window.Root.HasChildren);
			
			this.submenu.isActivated = false;
			this.submenu.CloseSubmenu();  // ferme les sous-menus (reccursif)
			this.submenu.SelectMenuItem(null);
			
			WindowFrame.ApplicationDeactivated -= new EventHandler(this.HandleApplicationDeactivated);
			this.window.Root.Children.Clear();
			this.window.Dispose();
			this.window = null;
			this.submenu = null;

			this.isActivated = true;
			this.SelectMenuItem(this.RetSelectMenuItem());
			return true;
		}

		// Conversion d'une coordonn�e �cran -> parent du widget.
		protected Drawing.Point MapScreenToClient(Widget widget, Drawing.Point pos)
		{
			pos = widget.WindowFrame.MapScreenToWindow(pos);
			pos = widget.MapRootToClient(pos);
			pos = widget.MapClientToParent(pos);
			return pos;
		}

		// Cherche dans quel menu ou sous-menu est la souris.
		protected Menu SearchMenu(Drawing.Point mouse)
		{
			Drawing.Point pos;
			pos = this.MapScreenToClient(this, mouse);
			if ( this.HitTest(pos) )  return this;

			Menu sub = this;
			while ( true )
			{
				sub = sub.submenu;
				if ( sub == null )  break;

				pos = this.MapScreenToClient(sub, mouse);
				if ( sub.HitTest(pos) )  return sub;
			}

			return null;
		}

		// Case du menu cliqu�e.
		private void HandleCellPressed(object sender, MessageEventArgs e)
		{
			System.Diagnostics.Debug.WriteLine("HandleCellPressed "+this.menuDeveloped);
			if ( this.menuDeveloped != TypeDeveloped.Close )
			{
				if ( this.parentMenu == null )
				{
					WindowFrame.MessageFilter -= new Epsitec.Common.Widgets.MessageHandler(this.HandlerMessageFilter);
					this.menuDeveloped = TypeDeveloped.Close;
					this.CloseSubmenu();
				}
			}
			else
			{
				WindowFrame.MessageFilter += new Epsitec.Common.Widgets.MessageHandler(this.HandlerMessageFilter);
				this.menuDeveloped = TypeDeveloped.Quick;
				MenuItem item = (MenuItem)sender;
				this.parentMenu = null;
				this.SetFocused(true);
				this.OpenSubmenu(item, false);
			}
		}

		private void HandlerMessageFilter(object sender, Message message)
		{
			if ( this.menuDeveloped == TypeDeveloped.Close )  return;
			WindowFrame window = sender as WindowFrame;

			switch ( message.Type )
			{
				case MessageType.MouseDown:
					Drawing.Point mouse = window.MapWindowToScreen(message.Cursor);
					Menu menu = this.SearchMenu(mouse);
					if ( menu == null )
					{
						this.CloseAll();
						message.Consumer = this;  // TODO: comment bouffer aussi le MouseUp ?
					}
					else
					{
						if ( menu.parentMenu != null )
						{
							Menu sub = this;
							while ( sub.submenu != null )  sub = sub.submenu;
							sub.SetFocused(true);  // TODO: il faudrait pouvoir ignorer ce clic !!!
							message.Consumer = this;
						}
					}
					break;
			}
		}

		private void HandleCellEntered(object sender, MessageEventArgs e)
		{
			MenuItem item = (MenuItem)sender;
			this.SelectMenuItem(item);
			int sel = this.RetSelectMenuItem();

			if ( this.menuDeveloped != TypeDeveloped.Close )
			{
				System.Diagnostics.Debug.WriteLine("HandleCellEntered "+this.Name+":"+item.MainText);
				this.OpenSubmenu(item, false);
			}
		}
		
		private void HandleCellExited(object sender, MessageEventArgs e)
		{
			if ( this.isActivated )
			{
				this.SelectMenuItem(null);
			}
		}
		
		private void HandleApplicationDeactivated(object sender)
		{
			// TODO: pourquoi ce n'est pas toujours appel� ?
			System.Diagnostics.Debug.WriteLine("HandleApplicationDeactivated");
			this.CloseAll();
		}


		// Dessine le menu.
		protected override void PaintBackgroundImplementation(Drawing.Graphics graphics, Drawing.Rectangle clipRect)
		{
			IAdorner adorner = Widgets.Adorner.Factory.Active;

			Drawing.Rectangle rect = new Drawing.Rectangle(0, 0, this.Client.Width, this.Client.Height);
			WidgetState       state = this.PaintState;
			Direction         dir   = this.RootDirection;

			this.Update();

			if ( this.type == MenuType.Vertical )
			{
				double iw = 0;
				if ( this.iconWidth > 10 )
				{
					iw = this.iconWidth+3;
				}
				adorner.PaintMenuBackground(graphics, rect, state, dir, this.parentRect, iw);
			}
		}


		protected MenuType				type;
		protected bool					isDirty;
		protected bool					isActivated = true;  // dernier menu (feuille)
		protected double				margin = 2;
		protected MenuItem[]			array;			// tableau des cases
		protected int					totalUsed;
		protected TypeDeveloped			menuDeveloped = TypeDeveloped.Close;
		protected WindowFrame			window;
		protected Menu					submenu;
		protected Menu					parentMenu;
		protected double				iconWidth;
		protected Drawing.Rectangle		parentRect;
	}
}

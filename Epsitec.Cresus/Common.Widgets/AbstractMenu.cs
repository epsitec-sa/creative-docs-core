namespace Epsitec.Common.Widgets
{
	using CommandDispatcher = Support.CommandDispatcher;
	
	public enum MenuType
	{
		Invalid,
		Vertical,
		Horizontal
	}
	
	/// <summary>
	/// La classe Menu repr�sente un menu horizontal ou vertical.
	/// </summary>
	[Support.SuppressBundleSupport]
	public abstract class AbstractMenu : Widget, Helpers.IWidgetCollectionHost
	{
		protected AbstractMenu(MenuType type)
		{
			IAdorner adorner = Widgets.Adorner.Factory.Active;
			this.margins = adorner.GeometryMenuMargins;
			this.shadow  = adorner.GeometryMenuShadow;

			this.type  = type;
			this.items = new MenuItemCollection(this);
			this.items.AutoEmbedding = true;
			this.timer = new Timer();
			this.timer.TimeElapsed += new Support.EventHandler(this.HandleTimerTimeElapsed);
		}
		
		
		public bool								IsHorizontal
		{
			get { return this.type == MenuType.Horizontal; }
		}

		public bool								IsVertical
		{
			get { return this.type == MenuType.Vertical; }
		}

		
		public Support.ICommandDispatcherHost	Host
		{
			get { return this.host; }
			set { this.host = value; }
		}
		
		public override CommandDispatcher		CommandDispatcher
		{
			get
			{
				Support.ICommandDispatcherHost host = this.GetHost ();
				
				if (host != null)
				{
					return host.CommandDispatcher;
				}
				
				return base.CommandDispatcher;
			}
		}
		
		public override double					DefaultHeight
		{
			get
			{
				return this.DefaultFontHeight + 6 + this.margin*2;
			}
		}

		public Drawing.Rectangle				ParentRect
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

		public Drawing.Size						RequiredSize
		{
			get
			{
				// Retourne les dimensions n�cessaires pour tout le menu vertical.
				double maxWidth = 0;
				foreach ( MenuItem cell in this.items )
				{
					maxWidth = System.Math.Max(maxWidth, cell.IconWidth);
				}
				this.iconWidth = maxWidth;

				Drawing.Size size = new Drawing.Size(0, 0);
				foreach ( MenuItem cell in this.items )
				{
					cell.IconWidth = maxWidth;
					Drawing.Size rs = cell.RequiredSize;
					size.Width = System.Math.Max(size.Width, rs.Width);
					size.Height += rs.Height;
				}
				size.Width  += this.shadow.Width  + this.margins.Width;
				size.Height += this.shadow.Height + this.margins.Height;
				return size;
			}
		}

		
		public MenuItemCollection				Items
		{
			get
			{
				return this.items;
			}
		}

		public int								SelectedIndex
		{
			get
			{
				// Retourne la case s�lectionn�e dans un menu.
				for (int i = 0; i < this.items.Count; i++)
				{
					if (this.items[i].ItemType != MenuItemType.Deselect)
					{
						return i;
					}
				}
				return -1;
			}
			set
			{
				// S�lectionne une (et une seule) case dans un menu.
				for (int i = 0; i < this.items.Count; i++)
				{
					if (i == value)
					{
						this.items[i].ItemType = this.isActive ? MenuItemType.Select : MenuItemType.Parent;
					}
					else
					{
						this.items[i].ItemType = MenuItemType.Deselect;
					}
				}
			}
		}
		
		
		public static bool						IsMenuDeveloped
		{
			get
			{
				// Indique si au moins un menu est actuellement ouvert (on ne compte pas
				// le menu horizontal au sommet d'une fen�tre).
				return AbstractMenu.menuDeveloped;
			}
		}
		
		public static AbstractMenu				RootMenu
		{
			get
			{
				AbstractMenu root = AbstractMenu.menuRoot;
				
				if ( root == null )
				{
					return null;
				}
				
				while ( root.parentMenu != null )
				{
					root = root.parentMenu;
				}
				
				return root;
			}
		}
		
		public static Window					RootWindow
		{
			get
			{
				AbstractMenu menu   = AbstractMenu.RootMenu;
				Window       window = null;
				
				if ( menu != null )
				{
					window = menu.Window;
					
					while ( window.IsOwned )
					{
						window = window.Owner;
					}
				}
				
				return window;
			}
		}

		
		public void AdjustSize()
		{
			// Ajuste les dimensions du menu selon son contenu.
			// Il faut appeler AdjustSize apr�s avoir fini de remplir le menu vertical.
			if ( this.IsVertical )
			{
				IAdorner adorner = Widgets.Adorner.Factory.Active;
				this.margins = adorner.GeometryMenuMargins;
				this.shadow  = adorner.GeometryMenuShadow;
				this.Size    = this.RequiredSize;
				this.isDirty = true;
			}
		}

		public void ShowAsContextMenu(Window owner, Drawing.Point pos)
		{
			// Affiche un menu contextuel dont on sp�cifie le coin sup�rieur/gauche.
			
			Window lastWindow = Message.State.LastWindow;
			
			if ( lastWindow != null )
			{
				lastWindow.MouseCursor = MouseCursor.Default;
			}
			
			pos.Y -= this.Height;
			pos.X -= this.shadow.Left;
			pos.Y += this.shadow.Top;

			this.window = new Window();
			this.window.MakeFramelessWindow();
			this.window.MakeFloatingWindow();
			this.window.Owner = owner;
			this.window.CommandDispatcher = owner.CommandDispatcher;
			this.window.Name = "ContextMenu";
			IAdorner adorner = Widgets.Adorner.Factory.Active;
			if ( adorner.AlphaVMenu < 1.0 )
			{
				this.window.MakeLayeredWindow();
				this.window.Alpha = adorner.AlphaVMenu;
				this.window.Root.BackColor = Drawing.Color.Transparent;
			}
			this.window.DisableMouseActivation();
			this.window.WindowBounds = new Drawing.Rectangle(pos.X, pos.Y, this.Width, this.Height);
			
			AbstractMenu.RegisterFilter(this);

			this.window.Root.Children.Add(this);
			this.window.AnimateShow(Animation.FadeIn);
			this.SetFocused(true);
			
			//	TODO: v�rifier que lorsque le menu est referm�, les deux event handlers sont
			//	bien supprim�s correctement...
		}

		
		#region Interface IBundleSupport
		public override void RestoreFromBundle(Support.ObjectBundler bundler, Support.ResourceBundle bundle)
		{
			base.RestoreFromBundle (bundler, bundle);
			this.items.RestoreFromBundle ("items", bundler, bundle);
			this.AdjustSize ();
		}
		
		public override void SerializeToBundle(Support.ObjectBundler bundler, Support.ResourceBundle bundle)
		{
			base.SerializeToBundle (bundler, bundle);
			this.items.SerializeToBundle ("items", bundler, bundle);
		}
		#endregion
		
		#region Serialization support
		protected override bool ShouldSerializeLocation()
		{
			return false;
		}
		
		protected override bool ShouldSerializeSize()
		{
			return false;
		}
		#endregion
		
		protected void Update()
		{
			// Met � jour si n�cessaire.
			if ( !this.isDirty )  return;
			this.UpdateClientGeometry();
		}

		protected Support.ICommandDispatcherHost GetHost()
		{
			if (this.host != null)
			{
				return this.host;
			}
			if (this.parentMenu != null)
			{
				this.parentMenu.GetHost ();
			}
			if (this.Window != null)
			{
				return this.Window;
			}
			
			return null;
		}
		
		
		protected override void Dispose(bool disposing)
		{
			if ( disposing )
			{
				MenuItem[] items = new MenuItem[this.items.Count];
				this.items.CopyTo(items, 0);
				this.items.Clear();
				
				foreach ( MenuItem item in items )
				{
					item.Dispose ();
				}
				
				this.timer.TimeElapsed -= new Support.EventHandler(this.HandleTimerTimeElapsed);
				this.timer.Dispose();
				this.items.Dispose();
				
				this.items = null;
				this.timer = null;
				
				this.host = null;
			}
			
			base.Dispose(disposing);
		}

		protected override void UpdateClientGeometry()
		{
			base.UpdateClientGeometry();
			
			if ( this.items == null ) return;
			
			// Met � jour la g�om�trie du menu.
			
			IAdorner adorner = Widgets.Adorner.Factory.Active;
			this.margins = adorner.GeometryMenuMargins;
			this.shadow  = adorner.GeometryMenuShadow;

			if ( this.IsHorizontal )
			{
				double x = this.margin;
				foreach ( MenuItem cell in this.items )
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
			else
			{
				double y = this.Height-this.shadow.Top-this.margins.Top;
				foreach ( MenuItem cell in this.items )
				{
					Drawing.Size rs = cell.RequiredSize;
					y -= rs.Height;
					Drawing.Rectangle rect = new Drawing.Rectangle();
					rect.Left   = this.shadow.Left+this.margins.Left;
					rect.Right  = this.Width-this.shadow.Right-this.margins.Right;
					rect.Bottom = y;
					rect.Top    = y+rs.Height;
					cell.Bounds = rect;
				}
			}

			this.isDirty = false;
		}

		protected override void OnAdornerChanged()
		{
			this.UpdateClientGeometry();
			base.OnAdornerChanged();
		}


		protected override void ProcessMessage(Message message, Drawing.Point pos)
		{
			switch ( message.Type )
			{
				case MessageType.KeyDown:
					if ( message.IsAltPressed == false &&
						 message.IsCtrlPressed == false &&
						 message.IsShiftPressed == false &&
						 this.ProcessKeyDown(message) )
					{
						message.Consumer = this;
					}
					break;
			}
		}

		protected virtual  bool ProcessKeyDown(Message message)
		{
			// Gestion d'une touche press�e avec KeyDown dans le menu.
			
			AbstractMenu parent = this.parentMenu;
			
			switch ( message.KeyCode )
			{
				case KeyCode.ArrowUp:
					this.SelectOtherMenuItem(-1);
					break;

				case KeyCode.ArrowDown:
					this.SelectOtherMenuItem(1);
					break;

				case KeyCode.ArrowLeft:
					if ( parent != null )
					{
						if ( parent.IsHorizontal )
						{
							parent.CloseSubmenu();
							parent.SelectOtherMenuItem(-1);
							MenuItem item = parent.Items[parent.SelectedIndex];
							if ( parent.OpenSubmenu(item, true) )
							{
								parent.submenu.SelectedIndex = 0;
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

				case KeyCode.ArrowRight:
					if ( parent == null )
					{
						this.OpenMenuItem();
					}
					else
					{
						int sel = this.SelectedIndex;
						if ( sel == -1 || this.items[sel].Submenu == null )
						{
							parent = AbstractMenu.RootMenu;
							if ( parent.IsHorizontal )
							{
								parent.CloseSubmenu();
								parent.SelectOtherMenuItem(1);
								MenuItem item = parent.Items[parent.SelectedIndex];
								if ( parent.OpenSubmenu(item, true) )
								{
									parent.submenu.SelectedIndex = 0;
								}
							}
						}
						else
						{
							if ( !this.OpenMenuItem() )
							{
								parent.CloseSubmenu();
								parent.SelectOtherMenuItem(1);
							}
						}
					}
					break;

				default:
					IFeel feel = Feel.Factory.Active;
					
					if ( feel.TestSelectItemKey (message) )
					{
						int sel = this.SelectedIndex;
						
						if ( this.items[sel].Submenu == null && !this.items[sel].Separator )
						{
							AbstractMenu.ValidateAndExecuteCommand();
						}
						break;
					}
					
					if ( feel.TestCancelKey (message) )
					{
						AbstractMenu.CloseAll();
						break;
					}
					
					return false;
			}
			
			return true;
		}

		
		protected bool OpenMenuItem()
		{
			// Ouvre le sous-menu correspondant � la case s�lectionn�e.
			int sel = this.SelectedIndex;
			if ( sel == -1 )  return false;
			if ( !this.OpenSubmenu(this.items[sel], true) )  return false;
			this.submenu.SelectedIndex = 0;
			return true;
		}

		protected bool CloseMenuItem()
		{
			// Ferme le sous-menu contenant la case s�lectionn�e.
			if ( this.parentMenu == null )  return false;
			return this.parentMenu.CloseSubmenu();
		}
		
		protected void SelectOtherMenuItem(int dir)
		{
			// S�lectionne la case suivante ou pr�c�dente.
			
			int sel = this.SelectedIndex;
			if ( sel == -1 )
			{
				sel = 0;
			}
			else
			{
				for ( int i=0 ; i<this.items.Count ; i++ )
				{
					sel += dir;
					if ( sel >= this.items.Count )
					{
						sel = 0;
					}
					if ( sel < 0 )
					{
						sel = this.items.Count-1;
					}
					if ( !this.items[sel].Separator )  break;
				}
			}
			this.SelectedIndex = sel;
		}

		
		protected static void ValidateAndExecuteCommand()
		{
			AbstractMenu menu = AbstractMenu.menuLastLeaf;
			
			if ( menu == null )  menu = AbstractMenu.RootMenu;
			
			if ( menu.SelectedIndex >= 0 )
			{
				// L'utilisateur a s�lectionn� une commande dans un menu valide. On doit
				// encore g�n�rer la commande ad hoc.
				
				MenuItem item = menu.Items[menu.SelectedIndex];
				
				item.ExecuteCommand ();
			}
			
			AbstractMenu.CloseAll();
		}
		
		protected static void CloseAll()
		{
			// Ferme compl�tement tous les menus ouverts.
			AbstractMenu root = AbstractMenu.RootMenu;
			
			root.CloseSubmenu();
			root.SelectedIndex = -1;
			
			// Il faut d�s-enregistrer la m�me instance que celle qui avait �t� enregistr�e
			// au d�part, sinon on se retrouve avec un filtre qui tra�ne...
			
			AbstractMenu.UnregisterFilter();
		}

		
		private bool OpenSubmenu(MenuItem item, bool forceQuick)
		{
			// Ouvre le sous-menu correspondant � un item.
			
			if ( this.submenu == item.Submenu )  return false;
			bool closed = this.CloseSubmenu();
			this.submenu = item.Submenu;
			if ( this.submenu == null )  return false;

			this.isActive = false;
			this.SelectedIndex = item.Index;  // s�lectionne la case parent

			this.submenu.AdjustSize();
			this.submenu.isActive = true;
			this.submenu.SelectedIndex = -1;  // d�s�lectionne tout
			this.submenu.parentMenu = this;
			this.submenu.parentItem = item;

			Drawing.Point pos    = new Drawing.Point(0, 0);
			Drawing.Point offset = new Drawing.Point(0, 0);

			if ( this.IsHorizontal )
			{
				Drawing.Rectangle pRect = item.Bounds;
				pRect.Offset(-pRect.Left, -pRect.Bottom);
				pRect.Offset(this.submenu.Left, this.submenu.Bottom);
				this.submenu.ParentRect = pRect;

				offset.Y = -this.submenu.Height;
				offset.X = -this.shadow.Left;
			}
			else
			{
				this.submenu.ParentRect = Drawing.Rectangle.Empty;

				Drawing.Point test = new Drawing.Point(item.Width, 0);
				test = this.MapClientToScreen(test);
				test.X += this.submenu.Width;
				ScreenInfo si = ScreenInfo.Find(test);
				Drawing.Rectangle wa = si.WorkingArea;

				if ( test.X <= wa.Right )  // sous-menu � droite ?
				{
					pos.X = item.Width;
					pos.Y = item.Height;
					offset.Y = 1-this.submenu.Height;
				}
				else	// sous-menu � gauche ?
				{
					pos.Y = item.Height;
					offset.X =  -this.submenu.Width;
					offset.Y = 1-this.submenu.Height;
				}
				pos.X -= this.shadow.Left;
				pos.Y += this.shadow.Top;
			}

			pos = item.MapClientToScreen(pos) + offset;
			
			this.window = new Window();
			this.window.MakeFramelessWindow();
			this.window.MakeFloatingWindow();
			this.window.Owner = item.Window;
			this.window.CommandDispatcher = item.Window.CommandDispatcher;
			this.window.Name = "Menu";
			IAdorner adorner = Widgets.Adorner.Factory.Active;
			if ( this.submenu.IsVertical && adorner.AlphaVMenu < 1.0 )
			{
				this.window.MakeLayeredWindow();
				this.window.Alpha = adorner.AlphaVMenu;
				this.window.Root.BackColor = Drawing.Color.Transparent;
			}
			this.window.DisableMouseActivation();
			this.window.WindowBounds = new Drawing.Rectangle(pos.X, pos.Y, this.submenu.Width, this.submenu.Height);
			
			this.submenu.Parent = this.window.Root;
			AbstractMenu.RegisterFilter(this.submenu);
			 
			Animation anim = Animation.None;
			if ( this.IsVertical || !closed )  anim = Animation.FadeIn;
			if ( forceQuick )  anim = Animation.None;
			this.window.AnimateShow(anim);
			this.submenu.SetFocused(true);
			
			return true;
		}

		private bool CloseSubmenu()
		{
			// Ferme le sous-menu ouvert.
			
			if ( this.window == null )  return false;
			
			System.Diagnostics.Debug.Assert(this.window.Root.HasChildren);
			
			if ( this.submenu != null )
			{
				if (AbstractMenu.menuRoot == this.submenu)
				{
					AbstractMenu.menuRoot = null;
				}
				
				this.submenu.isActive = false;
				this.submenu.CloseSubmenu();  // ferme les sous-menus (recursif)
				this.submenu.SelectedIndex = -1;
				this.submenu.parentMenu = null;
				this.submenu.parentItem = null;
			}
			
			this.window.Root.Children.Clear();
			this.submenu = null;
			
			if ( this.Window != null )
			{
				if ( Window.IsApplicationActive )
				{
					this.Window.MakeActive();
				}
			
				// Ce menu devient la derni�re feuille de l'arbre des menus...
				AbstractMenu.menuLastLeaf = this;
			
				this.isActive = true;
				this.SelectedIndex = this.SelectedIndex;
			}
			
			this.window.Dispose();
			this.window = null;
			return true;
		}

		
		private static AbstractMenu DetectMenu(Drawing.Point mouse)
		{
			// Cherche dans quel menu ou sous-menu est la souris.
			
			AbstractMenu menu = AbstractMenu.RootMenu;
			Drawing.Point pos;
			pos = menu.MapScreenToParent(mouse);
			if ( menu.HitTest(pos) )  return menu;

			while ( true )
			{
				menu = menu.submenu;
				if ( menu == null )  break;

				pos = menu.MapScreenToParent(mouse);
				if ( menu.HitTest(pos) )  return menu;
			}

			return null;
		}

		private static MenuItem     SearchItem(Drawing.Point mouse, AbstractMenu menu)
		{
			// Cherche dans quel item d'un menu est la souris.
			foreach ( MenuItem cell in menu.items )
			{
				Drawing.Point pos;
				pos = cell.MapScreenToParent(mouse);
				if ( cell.HitTest(pos) )  return cell;
			}

			return null;
		}

		
		private static void MessageFilter(object sender, Message message)
		{
			if ( !AbstractMenu.menuDeveloped )
			{
				System.Diagnostics.Debug.Assert ( AbstractMenu.menuDeveloped );
			}
			
			Window window = sender as Window;

			Drawing.Point	mouse;
			AbstractMenu	menu;

			switch ( message.Type )
			{
				case MessageType.MouseDown:
					mouse = window.MapWindowToScreen(message.Cursor);
					menu = AbstractMenu.DetectMenu(mouse);
					if ( menu == null )
					{
						AbstractMenu.CloseAll();
						
						// On n'indique qu'un message est consomm� que s'il concerne
						// la partie client de la fen�tre...						
						if ( !message.NonClient )
						{
							message.Swallowed = true;
						}
					}
					else
					{
						MenuItem cell = AbstractMenu.SearchItem(mouse, menu);
						if ( cell == null )
						{
							AbstractMenu.CloseAll();
							message.Swallowed = true;
						}
					}
					break;

				case MessageType.MouseUp:
					mouse = window.MapWindowToScreen(message.Cursor);
					menu = AbstractMenu.DetectMenu(mouse);
					if ( menu != null )
					{
						MenuItem cell = AbstractMenu.SearchItem(mouse, menu);
						if ( cell != null )
						{
							if ( cell.Submenu == null && !cell.Separator )
							{
								AbstractMenu.ValidateAndExecuteCommand();
								message.Swallowed = true;
							}
						}
					}
					break;
				
				case MessageType.MouseEnter:
				case MessageType.MouseMove:
					mouse = window.MapWindowToScreen(message.Cursor);
					menu = AbstractMenu.DetectMenu(mouse);
					if ( menu == null )
					{
						message.Swallowed = true;
					}
					break;
			}
		}

		private static void HandleApplicationDeactivated(object sender)
		{
			AbstractMenu.CloseAll();
		}

		
		private void HandleCellPressed(object sender, MessageEventArgs e)
		{
			// Case du menu cliqu�e.
			if ( AbstractMenu.menuDeveloped )
			{
				if ( this.parentMenu == null )
				{
					AbstractMenu.ValidateAndExecuteCommand();
				}
				else
				{
					MenuItem item = (MenuItem)sender;
					this.OpenSubmenu(item, false);
				}
			}
			else
			{
				Window window = AbstractMenu.RootWindow;
				
				if ( window == null )
				{
					window = this.Window;
				}
				
				if ( AbstractMenu.initiallyFocusedWidget == null &&
					 window != null )
				{
					AbstractMenu.initiallyFocusedWidget = window.FocusedWidget;
					System.Diagnostics.Debug.WriteLine(string.Format("Last focus on {0}, saved.", AbstractMenu.initiallyFocusedWidget));
				}
				
				MenuItem item = sender as MenuItem;
				
				this.parentMenu = null;
				this.parentItem = null;
				this.SetFocused(true);
				this.OpenSubmenu(item, false);
			}
		}

		private void HandleCellEntered(object sender, MessageEventArgs e)
		{
			MenuItem item = (MenuItem)sender;
			this.SelectedIndex = item.Index;
			int sel = this.SelectedIndex;

			if ( AbstractMenu.menuDeveloped )
			{
				if ( this.IsHorizontal )
				{
					this.OpenSubmenu(item, false);
				}
				else
				{
					if ( this.submenu == null )
					{
						// Il n'y a pas de sous-menu visible. Chaque fois que l'utilisateur
						// arrive sur une une autre ligne du menu, on remet � z�ro le compteur.
						this.delayedMenuItem = item;
						
						this.timer.Suspend();
						this.timer.Delay = SystemInformation.MenuShowDelay;
						this.timer.Start();
					}
					else
					{
						// Il y a un sous-menu visible. On d�marre le timer une seule fois,
						// lorsqu'une nouvelle ligne est activ�e (c'est forc�ment une autre
						// que celle qui a ouvert le sous-menu), et on se rappelle de la ligne
						// active.
						// Quand le temps est �coul�, on ouvre le sous-menu de la ligne active,
						// pour autant qu'il y en ait une (chaque fois que la souris sort d'une
						// ligne, on en prend note aussi).
						// Pour que �a marche, il faut aussi que lorsque la souris retourne
						// dans le sous-menu, �a active la bonne ligne dans le menu parent
						// (voir OnEntered).
						this.delayedMenuItem = item;
						
						if ( this.timer.State != TimerState.Running )
						{
							this.timer.Suspend();
							this.timer.Delay = SystemInformation.MenuShowDelay;
							this.timer.Start();
						}
					}
				}
			}
		}
		
		private void HandleCellExited(object sender, MessageEventArgs e)
		{
			MenuItem item = (MenuItem)sender;
			this.delayedMenuItem = null;
			
			if ( this.isActive )
			{
				this.SelectedIndex = -1;
			}
		}
		
		private void HandleTimerTimeElapsed(object sender)
		{
			if ( this.delayedMenuItem != null )
			{
				this.OpenSubmenu(this.delayedMenuItem, false);
			}
		}

		
		protected override void OnEntered(MessageEventArgs e)
		{
			base.OnEntered(e);
			
			if ( this.parentMenu != null )
			{
				this.parentMenu.SelectedIndex = this.parentItem.Index;
				
				//	TODO: faire de m�me avec les parents du parent, etc. ?
			}
		}

		protected override void PaintBackgroundImplementation(Drawing.Graphics graphics, Drawing.Rectangle clipRect)
		{
			System.Diagnostics.Debug.Assert (this.GetHost () != null, "No Host defined for menu.",
				/**/						 "The menu you are trying to display has no associated command dispatcher host.\n"+
				/**/						 "Use AbstractMenu.Host to define it when you setup the menu.");
			
			IAdorner adorner = Widgets.Adorner.Factory.Active;

			Drawing.Rectangle rect = this.Client.Bounds;
			WidgetState       state = this.PaintState;

			this.Update();

			if ( this.IsVertical )
			{
				double iw = 0;
				if ( this.iconWidth > 10 )
				{
					iw = this.iconWidth+3;
				}
				adorner.PaintMenuBackground(graphics, rect, state, Direction.Down, this.parentRect, iw);
			}
		}

		
		private static void RegisterFilter(AbstractMenu root)
		{
			if ( AbstractMenu.menuDeveloped == false )
			{
				Window.ApplicationDeactivated += new Support.EventHandler(AbstractMenu.HandleApplicationDeactivated);
				Window.MessageFilter += new MessageHandler(AbstractMenu.MessageFilter);
				AbstractMenu.menuDeveloped = true;
				AbstractMenu.menuRoot      = root;
				AbstractMenu.menuLastLeaf  = root;
				
				Window window = AbstractMenu.RootWindow;
				
				if ( AbstractMenu.initiallyFocusedWidget == null &&
					 window != null )
				{
					AbstractMenu.initiallyFocusedWidget = window.FocusedWidget;
					System.Diagnostics.Debug.WriteLine(string.Format("Last focus on {0}, saved.", AbstractMenu.initiallyFocusedWidget));
				}
			}
			else if ( AbstractMenu.menuRoot == null )
			{
				AbstractMenu.menuRoot = root;
			}
		}
		
		private static void UnregisterFilter()
		{
			if ( AbstractMenu.menuDeveloped )
			{
				Window.ApplicationDeactivated -= new Support.EventHandler(AbstractMenu.HandleApplicationDeactivated);
				Window.MessageFilter -= new MessageHandler(AbstractMenu.MessageFilter);
				AbstractMenu.menuDeveloped = false;
				AbstractMenu.menuLastLeaf  = null;
				AbstractMenu.menuRoot      = null;
				
				if ( AbstractMenu.initiallyFocusedWidget != null )
				{
					AbstractMenu.initiallyFocusedWidget.SetFocused(true);
					AbstractMenu.initiallyFocusedWidget = null;
				}
			}
		}
		
		
		#region IWidgetCollectionHost Members
		Helpers.WidgetCollection Helpers.IWidgetCollectionHost.GetWidgetCollection()
		{
			return this.Items;
		}
		
		public void NotifyInsertion(Widget widget)
		{
			MenuItem item = widget as MenuItem;
			
			this.Children.Add (item);
			this.isDirty = true;
			
			item.SetMenuType (this.type);
			
			item.Pressed += new MessageEventHandler(this.HandleCellPressed);
			item.Entered += new MessageEventHandler(this.HandleCellEntered);
			item.Exited  += new MessageEventHandler(this.HandleCellExited);
		}

		public void NotifyRemoval(Widget widget)
		{
			MenuItem item = widget as MenuItem;
			
			item.SetMenuType (MenuType.Invalid);
			
			item.Pressed -= new MessageEventHandler(this.HandleCellPressed);
			item.Entered -= new MessageEventHandler(this.HandleCellEntered);
			item.Exited  -= new MessageEventHandler(this.HandleCellExited);
			
			this.Children.Remove (item);
			this.isDirty = true;
		}
		
		public void NotifyPostRemoval(Widget widget)
		{
		}
		#endregion
		
		#region MenuItemCollection Class
		public class MenuItemCollection : Helpers.WidgetCollection
		{
			public MenuItemCollection(AbstractMenu menu) : base(menu)
			{
			}
			
			public new MenuItem this[int index]
			{
				get
				{
					return base[index] as MenuItem;
				}
			}
			
			public new MenuItem this[string name]
			{
				get
				{
					return base[name] as MenuItem;
				}
			}
		}

		#endregion
		
		protected MenuType							type;
		
		private bool								isDirty;
		private bool								isActive = true;	// dernier menu (feuille)
		private double								margin = 2;			// pour menu horizontal
		private Drawing.Margins						margins = new Drawing.Margins(2,2,2,2);
		private Drawing.Margins						shadow  = new Drawing.Margins(0,0,0,0);
		private MenuItemCollection					items;
		private Window								window;
		private Support.ICommandDispatcherHost		host;
		private Timer								timer;
		private AbstractMenu						submenu;
		private AbstractMenu						parentMenu;
		private MenuItem							parentItem;
		private double								iconWidth;
		private Drawing.Rectangle					parentRect;
		private MenuItem							delayedMenuItem;
		
		private static bool							menuDeveloped;
		private static AbstractMenu					menuLastLeaf;
		private static AbstractMenu					menuRoot;
		
		private static Widget						initiallyFocusedWidget;
	}
}

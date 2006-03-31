//	Copyright � 2003-2006, EPSITEC SA, CH-1092 BELMONT, Switzerland
//	Responsable: Pierre ARNAUD

using Epsitec.Common.Support;

namespace Epsitec.Common.Widgets
{
	using PropertyChangedEventHandler = Epsitec.Common.Support.EventHandler<Epsitec.Common.Types.DependencyPropertyChangedEventArgs>;
	
	using Win32Api = Epsitec.Common.Widgets.Platform.Win32Api;
	
	/// <summary>
	/// La classe Window repr�sente une fen�tre du syst�me d'exploitation. Ce
	/// n'est pas un widget en tant que tel: Window.Root d�finit le widget � la
	/// racine de la fen�tre.
	/// </summary>
	public class Window : Types.DependencyObject, Support.Data.IContainer, ICommandDispatcherHost, Support.Data.IPropertyProvider
	{
		public Window()
		{
			this.Initialise (new WindowRoot (this));
		}
		
		internal Window(WindowRoot root)
		{
			this.Initialise (root);
		}
		
		
		private void Initialise(WindowRoot root)
		{
			this.components = new Support.Data.ComponentCollection (this);
			
			this.root   = root;
			this.window = new Platform.Window (this);
			this.timer  = new Timer ();
			
			this.root.Size = new Drawing.Size (this.window.ClientSize);
			this.root.Name = "Root";
			this.root.MinSizeChanged += new PropertyChangedEventHandler (this.HandleRootMinSizeChanged);
			
			this.timer.TimeElapsed += new EventHandler (this.HandleTimeElapsed);
			this.timer.AutoRepeat = 0.050;
			
			Window.windows.Add (new System.WeakReference (this));
		}
		
		
		public void Run()
		{
			System.Windows.Forms.Application.Run (this.window);
		}
		
		public void Quit()
		{
			System.Windows.Forms.Application.Exit ();
		}
		
		
		public static void InvalidateAll(Window.InvalidateReason reason)
		{
			for (int i = 0; i < Window.windows.Count; )
			{
				System.WeakReference weak_ref = Window.windows[i] as System.WeakReference;
				
				Window target = weak_ref.Target as Window;
				
				if ((target == null) || (target.IsDisposed))
				{
					Window.windows.RemoveAt (i);
				}
				else
				{
					WindowRoot root = target.Root;
					
					switch (reason)
					{
						case InvalidateReason.Generic:
							root.Invalidate ();
							break;
						
						case InvalidateReason.AdornerChanged:
							root.NotifyAdornerChanged ();
							root.Invalidate ();
							break;
						
						case InvalidateReason.CultureChanged:
							root.NotifyCultureChanged ();
							break;
					}
					
					i++;
				}
			}
		}
		
		
		public static void GrabScreen(Drawing.Image bitmap, int x, int y)
		{
			Win32Api.GrabScreen (bitmap, x, y);
		}
		
		public static Window FindFirstLiveWindow()
		{
			for (int i = 0; i < Window.windows.Count; )
			{
				System.WeakReference weak_ref = Window.windows[i] as System.WeakReference;
				
				Window target = weak_ref.Target as Window;
				
				if ((target == null) || (target.IsDisposed))
				{
					Window.windows.RemoveAt (i);
				}
				else
				{
					return target;
				}
			}
			
			return null;
		}
		
		public static Window[] FindFromPosition(Drawing.Point pos)
		{
			System.Collections.ArrayList list = new	System.Collections.ArrayList ();
			
			for (int i = 0; i < Window.windows.Count; )
			{
				System.WeakReference weak_ref = Window.windows[i] as System.WeakReference;
				
				Window target = weak_ref.Target as Window;
				
				if ((target == null) || (target.IsDisposed))
				{
					Window.windows.RemoveAt (i);
				}
				else
				{
					if (target.WindowBounds.Contains (pos))
					{
						list.Add (target);
					}
					
					i++;
				}
			}
			
			return (Window[]) list.ToArray (typeof (Window));
		}
		
		public static Window FindFromText(string text)
		{
			for (int i = 0; i < Window.windows.Count; )
			{
				System.WeakReference weak_ref = Window.windows[i] as System.WeakReference;
				
				Window target = weak_ref.Target as Window;
				
				if ((target == null) || (target.IsDisposed))
				{
					Window.windows.RemoveAt (i);
				}
				else
				{
					if (target.Text == text)
					{
						return target;
					}
					
					i++;
				}
			}
			
			return null;
		}
		
		public static Window FindFromHandle(System.IntPtr handle)
		{
			for (int i = 0; i < Window.windows.Count; )
			{
				System.WeakReference weak_ref = Window.windows[i] as System.WeakReference;
				
				Window target = weak_ref.Target as Window;
				
				if ((target == null) || (target.IsDisposed))
				{
					Window.windows.RemoveAt (i);
				}
				else
				{
					if (target.window.Handle == handle)
					{
						return target;
					}
					
					i++;
				}
			}
			
			return null;
		}
		
		public static Window FindFromName(string name)
		{
			for (int i = 0; i < Window.windows.Count; )
			{
				System.WeakReference weak_ref = Window.windows[i] as System.WeakReference;
				
				Window target = weak_ref.Target as Window;
				
				if ((target == null) || (target.IsDisposed))
				{
					Window.windows.RemoveAt (i);
				}
				else
				{
					if (target.Name == name)
					{
						return target;
					}
					
					i++;
				}
			}
			
			return null;
		}
		
		
		public static void PumpEvents()
		{
			System.Windows.Forms.Application.DoEvents ();
		}
		
		
		public void MakeTopLevelWindow()
		{
			this.window.MakeTopLevelWindow ();
		}
		
		public void MakeFramelessWindow()
		{
			this.window.MakeFramelessWindow ();
		}
		
		public void MakeLayeredWindow()
		{
			this.window.IsLayered = true;
		}
		
		public void MakeLayeredWindow(bool layered)
		{
			this.window.IsLayered = layered;
		}
		
		public void MakeFixedSizeWindow()
		{
			this.window.MakeFixedSizeWindow ();
		}
		
		public void MakeButtonlessWindow()
		{
			this.window.MakeButtonlessWindow ();
		}
		
		public void MakeSecondaryWindow()
		{
			this.window.MakeSecondaryWindow ();
		}
		
		public void MakeToolWindow()
		{
			this.window.MakeToolWindow ();
		}
		
		public void MakeFloatingWindow()
		{
			this.window.MakeFloatingWindow ();
		}
		
		
		public void MakeActive()
		{
			if (! this.IsDisposed)
			{
				this.window.Activate ();
			}
		}
		
		public void MakeFocused()
		{
			if (! this.IsDisposed)
			{
				this.window.Focus ();
			}
		}
		
		
		public void DisableMouseActivation()
		{
			this.window.IsMouseActivationEnabled = false;
		}

		public bool StartWindowManagerOperation(Platform.WindowManagerOperation op)
		{
			if (this.window != null)
			{
				return this.window.StartWindowManagerOperation (op);
			}
			return false;
		}
		
		public virtual void Show()
		{
			this.AsyncValidation ();
			
			if (this.show_count == 0)
			{
				this.show_count++;
				this.root.InternalUpdateGeometry ();
				this.root.Invalidate ();
			}
			
			if (this.IsVisible == false)
			{
				this.OnAboutToShowWindow ();
			}
			
			this.window.ShowWindow ();
		}
		
		public virtual void ShowDialog()
		{
			this.AsyncValidation ();
			
			if (this.show_count == 0)
			{
				this.show_count++;
				this.root.InternalUpdateGeometry ();
			}
			
			if (this.IsVisible == false)
			{
				this.OnAboutToShowWindow ();
			}
			
			this.window.ShowDialogWindow ();
		}
		
		public virtual void Hide()
		{
			if (this.IsVisible)
			{
				this.OnAboutToHideWindow ();
				this.window.Hide ();
			}
		}
		
		public virtual void Close()
		{
			if (this.IsVisible)
			{
				this.OnAboutToHideWindow ();
			}
			
			this.window.Close ();
		}
		
		
		public void SynchronousRepaint()
		{
			if (this.window != null)
			{
				this.window.SynchronousRepaint ();
			}
		}
		
		
		public void AnimateShow(Animation animation)
		{
			if (this.IsVisible == false)
			{
				this.OnAboutToShowWindow ();
			}
			this.window.AnimateShow (animation, this.WindowBounds);
		}
		
		public void AnimateShow(Animation animation, Drawing.Rectangle bounds)
		{
			if (this.IsVisible == false)
			{
				this.OnAboutToShowWindow ();
			}
			this.window.AnimateShow (animation, bounds);
		}

		public void AnimateHide(Animation animation)
		{
			if (this.IsVisible)
			{
				this.OnAboutToHideWindow ();
			}
			this.window.AnimateHide (animation, this.WindowBounds);
		}
		
		
		
		public WindowRoot						Root
		{
			get
			{
				return this.root;
			}
		}
		
		public Window							Owner
		{
			get
			{
				return this.owner;
			}
			set
			{
				if (this.owner != value)
				{
					this.owner = value;
					
					if (value == null)
					{
						this.window.Owner = null;
					}
					else
					{
						this.window.Owner = null;
						
						if (this.IsVisible)
						{
							this.window.Owner = this.owner.window;
						}
					}
					
					Helpers.VisualTree.InvalidateCommandDispatcher (this);
				}
			}
		}
		
		public Window[]							OwnedWindows
		{
			get
			{
				System.Collections.ArrayList list = new System.Collections.ArrayList ();
				
				if ((this.window != null) &&
					(this.window.IsDisposed == false))
				{
					foreach (Platform.Window owned in this.window.FindOwnedWindows ())
					{
						if ((owned.IsDisposed == false) &&
							(owned.HostingWidgetWindow != null))
						{
							list.Add (owned.HostingWidgetWindow);
						}
					}
				}
				
				return (Window[]) list.ToArray (typeof (Window));
			}
		}
		
		
		public Widget							FocusedWidget
		{
			get
			{
				return this.focused_widget;
			}
			set
			{
				if (this.focused_widget != value)
				{
					Widget old_focus = this.focused_widget;
					Widget new_focus = value;
					
					this.focused_widget = null;
					
					if (old_focus != null)
					{
						old_focus.SetFocused (false);
					}
					
					this.focused_widget = new_focus;
					
					if (new_focus != null)
					{
						new_focus.SetFocused (true);
					}
					
					this.OnFocusedWidgetChanged ();
				}
			}
		}
		
		public Widget							EngagedWidget
		{
			get { return this.engaged_widget; }
			set
			{
				if (this.engaged_widget != value)
				{
					Widget old_engage = this.engaged_widget;
					Widget new_engage = value;
					
					this.engaged_widget = null;
					
					if (old_engage != null)
					{
						old_engage.SetEngaged (false);
						this.timer.Stop ();
					}
					
					this.engaged_widget = new_engage;
					
					if (new_engage != null)
					{
						new_engage.SetEngaged (true);
						
						if (new_engage.AutoRepeat)
						{
							this.timer.Stop ();
							this.timer.AutoRepeat = SystemInformation.InitialKeyboardDelay;
							this.timer.Start ();
						}
					}
				}
			}
		}
		
		public Widget							CapturingWidget
		{
			get
			{
				return this.capturing_widget;
			}
		}
		
		public IPaintFilter						PaintFilter
		{
			get
			{
				return this.paint_filter;
			}
			set
			{
				this.paint_filter = value;
			}
		}
		
		public MouseCursor						MouseCursor
		{
			set
			{
				this.window.Cursor = (value == null) ? null : value.GetPlatformCursor ();
				this.window_cursor = value;
			}
		}
		
		
		public bool								IsSyncPaintDisabled
		{
			get
			{
				return (this.window == null) || (this.window.PreventSyncPaint);
			}
		}

		public bool								IsVisible
		{
			get
			{
				if ((this.window != null) &&
					(this.window_is_visible))
				{
					return true;
				}
				
				return false;
			}
		}
		
		public bool								IsActive
		{
			get
			{
				return this.window.IsActive;
			}
		}
		
		public bool								IsFrozen
		{
			get { return (this.window == null) || this.window.IsFrozen; }
		}
		
		public bool								IsDisposed
		{
			get { return (this.window == null); }
		}
		
		public bool								IsFocused
		{
			get
			{
				if ((this.window != null) &&
					(this.window.IsAnimatingActiveWindow))
				{
					return true;
				}
				
				return this.window_is_focused;
			}
		}
		
		public bool								IsOwned
		{
			get
			{
				return (this.owner != null);
			}
		}
		
		public bool								IsFullScreen
		{
			get
			{
				return (this.window != null) && (this.window.WindowState == System.Windows.Forms.FormWindowState.Maximized);
			}
			set
			{
				if (this.window != null)
				{
					this.window.WindowState = value ? System.Windows.Forms.FormWindowState.Maximized : System.Windows.Forms.FormWindowState.Normal;
				}
			}
		}
		
		public bool								IsMinimized
		{
			get
			{
				return (this.window != null) && (this.window.WindowState == System.Windows.Forms.FormWindowState.Minimized);
			}
		}
		
		public bool								IsToolWindow
		{
			get
			{
				return this.window.IsToolWindow;
			}
		}
		
		public bool								IsSizeMoveInProgress
		{
			get
			{
				return (this.window != null) && (this.window.IsSizeMoveInProgress);
			}
		}
		
		public double							Alpha
		{
			get { return this.window.Alpha; }
			set { this.window.Alpha = value; }
		}
		
		public Drawing.Rectangle				WindowBounds
		{
			get
			{
				return this.window.WindowBounds;
			}
			set
			{
				this.window_location_set = true;
				this.window.WindowBounds = value;
			}
		}
		
		public Drawing.Image					Icon
		{
			get { return this.window.Icon; }
			set { this.window.Icon = value; }
		}
		
		
		internal WindowStyles					WindowStyles
		{
			get
			{
				return this.window.WindowStyles;
			}
			set
			{
				this.window.WindowStyles = value;
			}
		}
		
		internal WindowType						WindowType
		{
			get
			{
				return this.window.WindowType;
			}
			set
			{
				this.window.WindowType = value;
			}
		}
		
		internal WindowMode						WindowMode
		{
			get
			{
				return this.window.WindowMode;
			}
			set
			{
				this.window.WindowMode = value;
			}
		}
		
		
		#region ICommandDispatcherHost Members
		public CommandDispatcher[]				CommandDispatchers
		{
			get
			{
				return CommandDispatcher.ToArray (this.dispatcher);
			}
		}
		#endregion
		
		public bool								PreventAutoClose
		{
			get
			{
				return this.window.PreventAutoClose;
			}
			set
			{
				this.window.PreventAutoClose = value;
			}
		}
		
		public bool								PreventAutoQuit
		{
			get
			{
				return this.window.PreventAutoQuit;
			}
			set
			{
				this.window.PreventAutoQuit = value;
			}
		}
		
		
		public bool								IsValidDropTarget
		{
			get
			{
				return this.window.AllowDrop;
			}
			set
			{
				this.window.AllowDrop = value;
			}
		}
		
		
		public static bool						IsApplicationActive
		{
			get { return Platform.Window.IsApplicationActive; }
		}
		
		
		public static int						DebugAliveWindowsCount
		{
			get
			{
				int n = 0;
				
				foreach (System.WeakReference weak_ref in Window.windows)
				{
					if (weak_ref.IsAlive)
					{
						n++;
					}
				}
				
				return n;
			}
		}
		
		public static Window[]					DebugAliveWindows
		{
			get
			{
				Window[] windows = new Window[Window.DebugAliveWindowsCount];
				
				int i = 0;
				
				foreach (System.WeakReference weak_ref in Window.windows)
				{
					if (weak_ref.IsAlive)
					{
						windows[i++] = weak_ref.Target as Window;
					}
				}
				
				return windows;
			}
		}
		
		public string							DebugWindowHandle
		{
			get
			{
				return this.window == null ? "<null>" : this.window.Handle.ToInt64 ().ToString ("X");
			}
		}
		
		
		public Drawing.Rectangle				PlatformBounds
		{
			get
			{
				return new Drawing.Rectangle (this.window.Bounds);
			}
		}
		
		public Drawing.Point					PlatformLocation
		{
			get
			{
				return new Drawing.Point (this.window.Location);
			}
			set
			{
				this.window_location_set = true;
				this.window.Location = new System.Drawing.Point ((int)(value.X + 0.5), (int)(value.Y + 0.5));
			}
		}
		
		internal Platform.Window				PlatformWindow
		{
			get
			{
				return this.window;
			}
		}
		
		public object							PlatformWindowObject
		{
			get
			{
				return this.window;
			}
		}
		
		
		public Drawing.Point					WindowLocation
		{
			get
			{
				return this.window.WindowLocation;
			}
			set
			{
				this.window_location_set = true;
				this.window.WindowLocation = value;
			}
		}
		
		public Drawing.Size						WindowSize
		{
			get
			{
				return this.window.WindowSize;
			}
			set
			{
				if (this.window_location_set == false)
				{
					//	L'utilisateur n'a jamais positionn� sa fen�tre et le syst�me
					//	dans son immense bont� nous a propos� une origine. Si nous
					//	changeons sa taille avec notre syst�me de coordonn�es, le
					//	sommet ne sera plus l� o� l'OS aurait voulu qu'il soit. Il
					//	faut donc repositionner en m�me temps que l'on redimensionne
					//	la fen�tre :
					
					Drawing.Rectangle bounds = this.WindowBounds;
					
					bounds.Bottom = bounds.Top - value.Height;
					bounds.Width  = value.Width;
					
					this.WindowBounds        = bounds;
					this.window_location_set = false;
				}
				else
				{
					this.window.WindowSize = value;
				}
			}
		}
		
		public Drawing.Rectangle				WindowPlacementNormalBounds
		{
			get
			{
				if (this.window == null)
				{
					return Drawing.Rectangle.Empty;
				}
				
				return this.window.WindowPlacementNormalBounds;
			}
		}

		public Drawing.Size						ClientSize
		{
			get
			{
				return new Drawing.Size (this.window.ClientSize);
			}
			set
			{
				if (this.window != null)
				{
					Drawing.Size window_size = this.window.WindowSize;
					Drawing.Size client_size = this.ClientSize;
					
					this.WindowSize = new Drawing.Size (value.Width - client_size.Width + window_size.Width, value.Height - client_size.Height + window_size.Height);
				}
			}
		}
		
		[Bundle]			public string		Text
		{
			get { return this.text; }
			set { this.window.Text = this.text = value; }
		}

		[Bundle]			public string		Name
		{
			get { return this.name; }
			set { this.window.Name = this.name = value; }
		}

		public static bool						RunningInAutomatedTestEnvironment
		{
			get
			{
				return Window.is_running_in_automated_test_environment;
			}
			set
			{
				//	En mettant cette propri�t� � true, l'appel RunInTestEnvironment
				//	ne d�marrera pas de "message loop", ce qui �vite qu'une batterie
				//	de tests automatique (NUnit) ne se bloque apr�s l'affichage d'une
				//	fen�tre.
				
				if (Window.is_running_in_automated_test_environment != value)
				{
					Window.is_running_in_automated_test_environment = value;
				}
			}
		}


		public static void RunInTestEnvironment(Window window)
		{
			//	Cette m�thode doit �tre appel�e dans des tests bas�s sur NUnit, lorsque
			//	l'on d�sire que la fen�tre reste visible jusqu'� sa fermeture manuelle.
			
			//	Il est conseill� de rajouter un test nomm� AutomatedTestEnvironment qui
			//	met la propri�t� RunningInAutomatedTestEnvironment � true; ainsi, si on
			//	ex�cute un test global (Run sans avoir s�lectionn� de test sp�cifique),
			//	RunInTestEnvironment ne bloquera pas.
			
			if (Window.RunningInAutomatedTestEnvironment)
			{
				System.Windows.Forms.Application.DoEvents ();
			}
			else
			{
				System.Windows.Forms.Application.Run (window.PlatformWindow);
			}
		}
		
		
		#region IPropertyProvider Members
		public string[] GetPropertyNames()
		{
			if (this.property_hash == null)
			{
				return new string[0];
			}
			
			string[] names = new string[this.property_hash.Count];
			this.property_hash.Keys.CopyTo (names, 0);
			System.Array.Sort (names);
			
			return names;
		}
		
		public void SetProperty(string key, object value)
		{
			if (this.property_hash == null)
			{
				this.property_hash = new System.Collections.Hashtable ();
			}
			
			this.property_hash[key] = value;
		}
		
		public object GetProperty(string key)
		{
			if (this.property_hash != null)
			{
				return this.property_hash[key];
			}
			
			return null;
		}
		
		public bool IsPropertyDefined(string key)
		{
			if (this.property_hash != null)
			{
				return this.property_hash.Contains (key);
			}
			
			return false;
		}
		
		public void ClearProperty(string key)
		{
			if (this.property_hash != null)
			{
				this.property_hash.Remove (key);
			}
		}
		#endregion
		
		#region IContainer Members
		public void NotifyComponentInsertion(Support.Data.ComponentCollection collection, Support.Data.IComponent component)
		{
		}

		public void NotifyComponentRemoval(Support.Data.ComponentCollection collection, Support.Data.IComponent component)
		{
		}

		
		public Support.Data.ComponentCollection	Components
		{
			get
			{
				return this.components;
			}
		}
		#endregion
		
		
		public void AttachCommandDispatcher(CommandDispatcher value)
		{
			if (this.dispatcher != value)
			{
				System.Diagnostics.Debug.Assert (this.dispatcher == null);
				
				this.dispatcher = value;
				
				if (this.dispatcher != null)
				{
					this.dispatcher.ValidationRuleBecameDirty += new Support.EventHandler (this.HandleValidationRuleBecameDirty);
				}
				
				Helpers.VisualTree.InvalidateCommandDispatcher (this);
			}
		}
		
		public void DetachCommandDispatcher(CommandDispatcher value)
		{
			if (this.dispatcher != null)
			{
				System.Diagnostics.Debug.Assert (this.dispatcher == value);
				
				this.dispatcher.ValidationRuleBecameDirty -= new Support.EventHandler (this.HandleValidationRuleBecameDirty);
				
				Helpers.VisualTree.InvalidateCommandDispatcher (this);
			}
		}
		
		
		public void AttachLogicalFocus(Widget widget)
		{
			this.logical_focus_list.Remove (widget);
			this.logical_focus_list.Add (widget);
		}
		
		public void DetachLogicalFocus(Widget widget)
		{
			this.logical_focus_list.Remove (widget);
		}
		
		
		public Widget FindLogicalFocus()
		{
			foreach (Widget widget in this.logical_focus_list)
			{
				if ((widget.IsVisible) &&
					(widget.IsEnabled))
				{
					return widget;
				}
			}
			
			return null;
		}
		
		public bool RestoreLogicalFocus()
		{
			Widget focus = this.FindLogicalFocus ();
							
			if ((focus != null) &&
				(focus.CanFocus) &&
				(focus.IsFocused != true))
			{
				focus.Focus ();
				return true;
			}
			
			return false;
		}
		
		
		internal void PlatformWindowDisposing()
		{
			if (this.is_disposed == false)
			{
				this.window = null;
				this.Dispose ();
			}
		}
		
		
		protected override void Dispose(bool disposing)
		{
			if (this.is_disposed)
			{
				System.Diagnostics.Debug.WriteLine (string.Format ("Disposing window {0} which has already been disposed", this.Name));
				return;
			}
			
			this.is_disposed = true;
				
			if (Widget.DebugDispose)
			{
				System.Diagnostics.Debug.WriteLine (string.Format ("Disposing window {0}, still {1} windows alive", this.Name, Window.DebugAliveWindowsCount));
			}
			
			if (disposing)
			{
				this.OnWindowDisposing ();
				
				if (this.IsVisible)
				{
					this.OnAboutToHideWindow ();
				}
				
				if (this.cmd_queue.Count > 0)
				{
					//	Il y a encore des commandes dans la queue d'ex�cution. Il faut soit les transmettre
					//	� une autre fen�tre encore en vie, soit les ex�cuter tout de suite.
					
					Window helper = this.Owner;
					
					if (helper == null)
					{
						helper = Window.FindFirstLiveWindow ();
					}
					
					if (helper == null)
					{
						this.DispatchQueuedCommands ();
					}
					else
					{
						while (this.cmd_queue.Count > 0)
						{
							QueueItem item = this.cmd_queue.Dequeue () as QueueItem;
							helper.QueueCommand (item);
						}
					}
				}
				
				if (this.dispatcher != null)
				{
					this.DetachCommandDispatcher (this.dispatcher);
					this.dispatcher = null;
				}
				
				if (this.root != null)
				{
					this.root.MinSizeChanged -= new PropertyChangedEventHandler (this.HandleRootMinSizeChanged);
					this.root.Dispose ();
				}
				
				if (this.window != null)
				{
					Platform.Window[] owned = this.window.FindOwnedWindows ();
					
					for (int i = 0; i < owned.Length; i++)
					{
						owned[i].HostingWidgetWindow.Dispose ();
					}
					
					if (this.window.IsActive && Window.IsApplicationActive)
					{
						//	Si la fen�tre est active au moment de sa destruction, Windows a tendance
						//	� se comporter de mani�re �trange. On va donc se d�p�cher d'activer une
						//	autre fen�tre (le propri�taire fera l'affaire) :
						
						Platform.Window owner = this.window.Owner as Platform.Window;
						
						if (owner != null)
						{
							if (Widget.DebugDispose)
							{
								System.Diagnostics.Debug.WriteLine ("Disposing active window.");
							}
							
							owner.Activate ();
						}
					}
					
					this.window.ResetHostingWidgetWindow ();
					this.window.Dispose ();
				}
				
				this.timer.TimeElapsed -= new EventHandler (this.HandleTimeElapsed);
				this.timer.Dispose ();
				
				this.timer  = null;
				this.root   = null;
				this.window = null;
				this.owner  = null;
				
				this.last_in_widget   = null;
				this.capturing_widget = null;
				this.capturing_button = MouseButtons.None;
				this.focused_widget   = null;
				this.engaged_widget   = null;;
				
				if (this.components.Count > 0)
				{
					Support.Data.IComponent[] components = new Support.Data.IComponent[this.components.Count];
					this.components.CopyTo (components, 0);
					
					//	S'il y a des composants attach�s, on les d�truit aussi. Si l'utilisateur
					//	ne d�sire pas que ses composants soient d�truits, il doit les d�tacher
					//	avant de faire le Dispose de la fen�tre !
					
					for (int i = 0; i < components.Length; i++)
					{
						Support.Data.IComponent component = components[i];
						this.components.Remove (component);
						component.Dispose ();
					}
				}
				
				this.components.Dispose ();
				this.components = null;
				
				if (Message.CurrentState.LastWindow == this)
				{
					Message.ClearLastWindow ();
				}
				
				this.OnWindowDisposed ();
			}
		}
		
		
		internal void ResetWindow()
		{
			this.window = null;
		}
		
		
		protected void OnAsyncNotification()
		{
			if (this.AsyncNotification != null)
			{
				this.AsyncNotification (this);
			}
		}
		
		
		protected virtual void OnWindowDisposing()
		{
			if (this.WindowDisposing != null)
			{
				this.WindowDisposing (this);
			}
		}
		
		protected virtual void OnWindowDisposed()
		{
			if (this.WindowDisposed != null)
			{
				this.WindowDisposed (this);
			}
		}
		
		protected virtual void OnAboutToShowWindow()
		{
			if ((this.owner != null) &&
				(this.window != null))
			{
				this.window.Owner = this.owner.window;
			}
			
			this.SyncMinSizeWithWindowRoot ();
			
			if (this.AboutToShowWindow != null)
			{
				this.AboutToShowWindow (this);
			}
		}
		
		protected virtual void OnAboutToHideWindow()
		{
			if (this.AboutToHideWindow != null)
			{
				this.AboutToHideWindow (this);
			}
		}
		
		
		internal void OnWindowActivated()
		{
			if (this.WindowActivated != null)
			{
				this.WindowActivated (this);
			}
		}
		
		internal void OnWindowDeactivated()
		{
			if (this.WindowDeactivated != null)
			{
				this.WindowDeactivated (this);
			}
		}
		
		internal void OnWindowAnimationEnded()
		{
			if (this.WindowAnimationEnded != null)
			{
				this.WindowAnimationEnded (this);
			}
		}
		
		internal void OnWindowShown()
		{
			System.Diagnostics.Debug.Assert (! this.window_is_visible);

			Types.DependencyObjectTreeSnapshot snapshot = Types.DependencyObjectTree.CreatePropertyTreeSnapshot (this.Root, Visual.IsVisibleProperty);
			
			this.window_is_visible = true;
			
			if ((this.owner != null) &&
				(this.window != null))
			{
				this.window.Owner = this.owner.window;
			}
			
			snapshot.InvalidateDifferentProperties ();
			
			if (this.WindowShown != null)
			{
				this.WindowShown (this);
			}

			this.root.NotifyWindowIsVisibleChanged ();
		}
		
		internal void OnWindowHidden()
		{
			System.Diagnostics.Debug.Assert (this.window_is_visible);

			Types.DependencyObjectTreeSnapshot snapshot = Types.DependencyObjectTree.CreatePropertyTreeSnapshot (this.Root, Visual.IsVisibleProperty);
			
			this.window_is_visible = false;
			
			if ((this.owner != null) &&
				(this.window != null))
			{
				this.window.Owner = null;
			}
			
			snapshot.InvalidateDifferentProperties ();
			
			if (this.WindowHidden != null)
			{
				this.WindowHidden (this);
			}
			
			this.root.NotifyWindowIsVisibleChanged ();
		}

		internal void OnWindowClosed()
		{
			if (this.WindowClosed != null)
			{
				this.WindowClosed (this);
			}
		}

		internal void OnWindowCloseClicked()
		{
			if (this.WindowCloseClicked != null)
			{
				this.WindowCloseClicked (this);
			}
		}

		internal void OnApplicationActivated()
		{
			if (Window.ApplicationActivated != null)
			{
				Window.ApplicationActivated (this);
			}
		}
		
		internal void OnApplicationDeactivated()
		{
			if (Window.ApplicationDeactivated != null)
			{
				Window.ApplicationDeactivated (this);
			}
		}
		
		
		public bool								IsSubmenuOpen
		{
			get
			{
				Window[] windows = this.OwnedWindows;
				
				foreach (Window window in windows)
				{
					if (window is MenuWindow)
					{
						return true;
					}
				}
			
				return false;
			}
		}
				
		internal void NotifyWindowFocused()
		{
//-			System.Diagnostics.Debug.WriteLine ("Platform Window focused");
			
			if (this.window_is_focused == false)
			{
				if (this.focused_widget != null)
				{
					Types.DependencyObjectTreeSnapshot snapshot = Types.DependencyObjectTree.CreatePropertyTreeSnapshot (this.focused_widget, Visual.IsFocusedProperty);
					
					this.window_is_focused = true;
					this.focused_widget.Invalidate (Widgets.InvalidateReason.FocusedChanged);
				
					snapshot.InvalidateDifferentProperties ();
				}
				else
				{
					this.window_is_focused = true;
				}
				
//-				System.Diagnostics.Debug.WriteLine ("Window focused");
				
				this.OnWindowFocused ();
			}
		}
		
		internal void NotifyWindowDefocused()
		{
//-			System.Diagnostics.Debug.WriteLine ("Platform Window de-focused");
			
			if ((this.window_is_focused == true) &&
				(this.IsSubmenuOpen == false))
			{
				if (this.focused_widget != null)
				{
					Types.DependencyObjectTreeSnapshot snapshot = Types.DependencyObjectTree.CreatePropertyTreeSnapshot (this.focused_widget, Visual.IsFocusedProperty);
					
					this.window_is_focused = false;
					this.focused_widget.Invalidate (Widgets.InvalidateReason.FocusedChanged);
					
					snapshot.InvalidateDifferentProperties ();
				}
				else
				{
					this.window_is_focused = false;
				}
				
//-				System.Diagnostics.Debug.WriteLine ("Window de-focused");
				
				this.OnWindowDefocused ();
				
				if ((this.owner != null) &&
					(this.owner.window.Focused == false))
				{
					this.owner.NotifyWindowDefocused ();
				}
			}
		}
		
		
		protected virtual void OnWindowFocused()
		{
			if (this.WindowFocused != null)
			{
				this.WindowFocused (this);
			}
		}
		
		protected virtual void OnWindowDefocused()
		{
			if (this.WindowDefocused != null)
			{
				this.WindowDefocused (this);
			}
		}
		
		
		internal void OnWindowDragEntered(WindowDragEventArgs e)
		{
			if (this.WindowDragEntered != null)
			{
				this.WindowDragEntered (this, e);
			}
		}
		
		internal void OnWindowDragLeft()
		{
			if (this.WindowDragLeft != null)
			{
				this.WindowDragLeft (this);
			}
		}
		
		internal void OnWindowDragDropped(WindowDragEventArgs e)
		{
			if (this.WindowDragDropped != null)
			{
				this.WindowDragDropped (this, e);
			}
		}
		
		internal void OnWindowSizeMoveStatusChanged()
		{
			if (this.WindowSizeMoveStatusChanged != null)
			{
				this.WindowSizeMoveStatusChanged (this);
			}
		}
		
		
		internal bool FilterMessage(Message message)
		{
			if (Window.MessageFilter != null)
			{
				Window.MessageFilter (this, message);
					
				//	Si le message a �t� "absorb�" par le filtre, il ne faut en aucun
				//	cas le transmettre plus loin.
					
				if (message.Handled)
				{
					if (message.Swallowed && !this.IsFrozen)
					{
						//	Le message a �t� mang�. Il faut donc aussi manger le message
						//	correspondant si les messages viennent par paire.
							
						switch (message.Type)
						{
							case MessageType.MouseDown:
								this.window.FilterMouseMessages = true;
								break;
								
							case MessageType.KeyDown:
								this.window.FilterKeyMessages = true;
								break;
						}
					}
						
					return true;
				}
			}
			
			return false;
		}
		
		
		protected virtual void OnFocusedWidgetChanged()
		{
			if (this.FocusedWidgetChanged != null)
			{
				this.FocusedWidgetChanged (this);
			}
		}
		
		
		#region QueueItem class
		protected class QueueItem
		{
			public QueueItem(object source, string command, ICommandDispatcherHost dispatcher)
			{
				this.source      = source;
				this.command     = command;
				this.dispatchers = dispatcher.CommandDispatchers;
			}
			
			public QueueItem(Widget source)
			{
				this.source      = source;
				this.command     = source.Command;
				this.dispatchers = Helpers.VisualTree.GetAllDispatchers (source);
			}
			
			public QueueItem(Widget source, CommandState command)
			{
				this.source      = source;
				this.command     = command.Name;
				this.dispatchers = Helpers.VisualTree.GetAllDispatchers (source);
			}
			
			
			public object						Source
			{
				get
				{
					return this.source;
				}
			}
			
			public string						Command
			{
				get
				{
					return this.command;
				}
			}
			
			public CommandDispatcher[]			CommandDispatchers
			{
				get
				{
					return this.dispatchers;
				}
			}
			
			
			protected object					source;
			protected string					command;
			protected CommandDispatcher[]		dispatchers;
		}
		#endregion
		
		public void QueueCommand(Widget source)
		{
			this.QueueCommand (new QueueItem (source));
		}
		
		public void QueueCommand(Widget source, CommandState command)
		{
			this.QueueCommand (new QueueItem (source, command));
		}
		
		public void QueueCommand(object source, string command)
		{
			this.QueueCommand (source, command, this);
		}
		
		public void QueueCommand(object source, string command, ICommandDispatcherHost dispatcher)
		{
			this.QueueCommand (new QueueItem (source, command, dispatcher));
		}
		
		
		protected void QueueCommand(QueueItem item)
		{
			this.cmd_queue.Enqueue (item);
			
			if (this.cmd_queue.Count == 1)
			{
				if (this.window != null)
				{
					this.window.SendQueueCommand ();
				}
			}
			
			if (this.window == null)
			{
				this.DispatchQueuedCommands ();
			}
		}
		
		
		public void AsyncDispose()
		{
			this.is_dispose_queued = true;
			this.window.SendQueueCommand ();
		}
		
		public void AsyncNotify()
		{
			if (this.is_async_notification_queued == false)
			{
				this.is_async_notification_queued = true;
				this.window.SendQueueCommand ();
			}
		}
		
		public void AsyncValidation()
		{
			if (this.pending_validation == false)
			{
				this.pending_validation = true;
				this.window.SendValidation ();
			}
		}
		
		
		public static void ResetMouseCursor()
		{
			Window window = Message.CurrentState.LastWindow;
			
			if (window != null)
			{
				window.MouseCursor = MouseCursor.Default;
			}
		}
		
		
		private void ReleaseCapturingWidget()
		{
			Widget       widget = this.capturing_widget;
			MouseButtons button = this.capturing_button;
			
			this.capturing_widget = null;
			this.capturing_button = MouseButtons.None;
			
			this.window.Capture = false;
			
			if ((widget != null) &&
				(button != MouseButtons.None))
			{
				widget.DispatchDummyMouseUpEvent (button, this.capturing_cursor);
			}
		}
		
		private void HandleValidationRuleBecameDirty(object sender)
		{
			this.AsyncValidation ();
		}
		
		
		internal void DispatchQueuedCommands()
		{
			while (this.cmd_queue.Count > 0)
			{
				QueueItem item    = this.cmd_queue.Dequeue () as QueueItem;
				object    source  = item.Source;
				string    command = item.Command;
				
				CommandDispatcher[] dispatchers = item.CommandDispatchers;
				
//-				System.Diagnostics.Debug.Assert (dispatchers.Length > 0);
				
				CommandDispatcher.Dispatch (dispatchers, command, source);
			}
			
			if (this.is_dispose_queued)
			{
				this.Dispose ();
			}
			if (this.is_async_notification_queued)
			{
				this.is_async_notification_queued = false;
				this.OnAsyncNotification ();
			}
		}
		
		internal void DispatchValidation()
		{
			this.pending_validation = false;
			CommandDispatcher.SyncAllValidationRules ();
		}
		
		internal void DispatchMessage(Message message)
		{
			this.DispatchMessage (message, null);
		}
		
		internal void DispatchMessage(Message message, Widget root)
		{
			if (this.IsFrozen || (message == null))
			{
				return;
			}
			
			if (message.IsMouseType)
			{
				if (this.capturing_widget == null)
				{
					//	C'est un message souris. Nous allons commencer par v�rifier si tous les widgets
					//	encore marqu�s comme IsEntered contiennent effectivement encore la souris. Si non,
					//	on les retire de la liste en leur signalant qu'ils viennent de perdre la souris.
					
					Widget.UpdateEntered (this, message);
				}
				else
				{
					//	Un widget a captur� les �v�nements souris. Il ne faut donc g�rer l'�tat Entered
					//	uniquement pour ce widget-l�.
					
					Widget.UpdateEntered (this, this.capturing_widget, message);
				}
				
				this.last_in_widget = this.DetectWidget (message.Cursor);
			}
			
			message.InWidget = this.last_in_widget;
			message.Consumer = null;
			
			if (this.capturing_widget == null)
			{
				//	La capture des �v�nements souris n'est pas active. Si un widget a le focus, il va
				//	recevoir les �v�nements clavier en priorit� (message.FilterOnlyFocused = true).
				//	Dans les autres cas, les �v�nements sont simplement achemin�s de widget en widget,
				//	en utilisant une approche en profondeur d'abord.
				
				Drawing.Point pos = message.Cursor;
				
				
				if (root == null)
				{
					root = this.root;
				}
				else
				{
					pos = root.MapRootToClient (pos);
					pos = root.MapClientToParent (pos);
				}
				
				root.MessageHandler (message, pos);
				
				if (this.IsDisposed)
				{
					return;
				}
				
				Window window = this;
				
				while (message.Handled == false)
				{
					//	Le message n'a pas �t� consomm�. Regarde si nous avons � faire
					//	� une fen�tre cha�n�e avec un parent.
					
					pos  = Helpers.VisualTree.MapParentToScreen (root, pos);
					root = MenuWindow.GetParentWidget (window);
					
					if ((root == null) ||
						(root.IsVisible == false))
					{
						break;
					}
					
					pos    = Helpers.VisualTree.MapScreenToParent (root, pos);
					window = root.Window;
					
					root.MessageHandler (message, pos);
				}
				
				this.window.Capture = false;
			}
			else
			{
				message.FilterNoChildren = true;
				message.Captured         = true;
				
				this.capturing_widget.MessageHandler (message);
			}
			
			if (this.IsDisposed) return;
			
			this.PostProcessMessage (message);
		}
		
		
		internal void ReleaseCapture()
		{
			this.ReleaseCapturingWidget ();
		}
		
		internal void FocusWidget(Widget widget)
		{
			if ((widget != null) &&
				(widget.IsFocused == false) &&
				(widget.CanFocus) &&
				(widget != this.focused_widget))
			{
				//	On va r�aliser un changement de focus. Mais pour cela, il faut que le widget
				//	ayant le focus actuellement, ainsi que le widget candidat pour l'obtention du
				//	focus soient d'accord...
				
				if ((this.focused_widget == null) ||
					(this.focused_widget.InternalAboutToLoseFocus (Widget.TabNavigationDir.None, Widget.TabNavigationMode.Passive)))
				{
					Widget focus;
					
					if (widget.InternalAboutToGetFocus (Widget.TabNavigationDir.None, Widget.TabNavigationMode.Passive, out focus))
					{
						this.FocusedWidget = focus;
					}
				}
			}
		}
		
		internal void PostProcessMessage(Message message)
		{
			Widget consumer = message.Consumer;
			
			if (message.Type == MessageType.KeyUp)
			{
				if (this.EngagedWidget != null)
				{
					this.EngagedWidget.SimulateReleased ();
					this.EngagedWidget.SimulateClicked ();
					this.EngagedWidget = null;
				}
			}
			
			if (this.capturing_widget != null)
			{
				if (this.capturing_widget.IsVisible == false)
				{
					//	Il faut terminer la capture si le widget n'est plus visible,
					//	sinon on risque de ne plus jamais recevoir d'�v�nements pour
					//	les autres widgets.
					
					this.ReleaseCapturingWidget ();
				}
			}
			
			if (consumer != null)
			{
				switch (message.Type)
				{
					case MessageType.MouseDown:
						if ((consumer.AutoCapture) ||
							(message.ForceCapture))
						{
							this.capturing_widget = consumer;
							this.capturing_button = message.Button;
							this.capturing_cursor = message.Cursor;
							this.window.Capture = true;
						}
						else
						{
							this.window.Capture = false;
						}
						
						if (consumer.AutoFocus)
						{
							this.FocusWidget (consumer);
						}
						
						if (message.IsLeftButton)
						{
							if ((consumer.AutoEngage) &&
								(consumer.IsEngaged == false) &&
								(consumer.CanEngage))
							{
								this.EngagedWidget = consumer;
								this.initially_engaged_widget = consumer;
							}
						}
						break;
					
					case MessageType.MouseUp:
						this.capturing_widget = null;
						this.capturing_button = MouseButtons.None;
						this.window.Capture = false;
						
						if (message.IsLeftButton)
						{
							if ((consumer.AutoToggle) &&
								(consumer.IsEnabled) &&
								(consumer.IsEntered) &&
								(!consumer.IsFrozen))
							{
								//	Change l'�tat du bouton...
								
								consumer.Toggle ();
							}
							
							this.EngagedWidget = null;
							this.initially_engaged_widget = null;
						}
						break;
					
					case MessageType.MouseLeave:
						if (consumer.IsEngaged)
						{
							this.EngagedWidget = null;
						}
						break;
					
					case MessageType.MouseEnter:
						if ((Message.CurrentState.IsLeftButton) &&
							(Message.CurrentState.IsSameWindowAsButtonDown) &&
							(this.initially_engaged_widget == consumer) &&
							(consumer.AutoEngage) &&
							(consumer.IsEngaged == false) &&
							(consumer.CanEngage))
						{
							this.EngagedWidget = consumer;
						}
						break;
				}
			}
			else if (! message.Handled)
			{
				Shortcut shortcut = Shortcut.FromMessage (message);
				
				if (shortcut != null)
				{
					if (this.root.ShortcutHandler (shortcut))
					{
						message.Handled   = true;
						message.Swallowed = true;
					}
				}
				
				if ((! message.Handled) &&
					(this.owner != null))
				{
					//	Le message n'a pas �t� trait�. Peut-�tre qu'il pourrait int�resser la fen�tre
					//	parent; pour l'instant, on poubellise simplement l'�v�nement, car le faire
					//	remonter m'a caus� passablement de surprises...
				}
			}
			
			if (message.Swallowed)
			{
				//	Le message a �t� mang�. Il faut donc aussi manger le message
				//	correspondant si les messages viennent par paire.
						
				switch (message.Type)
				{
					case MessageType.MouseDown:
						this.window.FilterMouseMessages = true;
						this.capturing_widget = null;
						this.capturing_button = MouseButtons.None;
						break;
							
					case MessageType.KeyDown:
						this.window.FilterKeyMessages = true;
						break;
				}
			}
		}
		
		internal void RefreshGraphics(Drawing.Graphics graphics, Drawing.Rectangle repaint, Drawing.Rectangle[] strips)
		{
			if (strips.Length > 1)
			{
				//	On doit repeindre toute une s�rie de rectangles :
				
				for (int i = 0; i < strips.Length; i++)
				{
					graphics.Transform = new Drawing.Transform ();
					graphics.ResetClippingRectangle ();
					graphics.SetClippingRectangle (strips[i]);
					
//-					System.Diagnostics.Debug.WriteLine (string.Format ("Strip {0} : {1}", i, strips[i].ToString ()));
					
					this.Root.PaintHandler (graphics, strips[i], this.paint_filter);
				}
				
//-				System.Diagnostics.Debug.WriteLine ("Done");
				
				graphics.Transform = new Drawing.Transform ();
				graphics.ResetClippingRectangle ();
				graphics.SetClippingRectangle (repaint);
			}
			else
			{
				graphics.Transform = new Drawing.Transform ();
				graphics.ResetClippingRectangle ();
				graphics.SetClippingRectangle (repaint);
				
				this.Root.PaintHandler (graphics, repaint, this.paint_filter);
			}
			
			while (this.post_paint_queue.Count > 0)
			{
				Window.PostPaintRecord record = this.post_paint_queue.Dequeue () as Window.PostPaintRecord;
				record.Paint (graphics);
			}
		}
		
		
		protected Widget DetectWidget(Drawing.Point pos)
		{
			Widget child = this.root.FindChild (pos);
			return (child == null) ? this.root : child;
		}
		
		
		public void MarkForRepaint()
		{
			if (this.window != null)
			{
				this.window.MarkForRepaint ();
			}
		}
		
		public void MarkForRepaint(Drawing.Rectangle rect)
		{
			if (this.window != null)
			{
				this.window.MarkForRepaint (rect);
			}
		}
		
		
		public void QueuePostPaintHandler(Window.IPostPaintHandler handler, Drawing.Graphics graphics, Drawing.Rectangle repaint)
		{
			this.post_paint_queue.Enqueue (new Window.PostPaintRecord (handler, graphics, repaint));
		}
		
		
		public Drawing.Point MapWindowToScreen(Drawing.Point point)
		{
			int x = (int)(point.X + 0.5);
			int y = this.window.ClientSize.Height-1 - (int)(point.Y + 0.5);
			
			System.Drawing.Point pt = this.window.PointToScreen (new System.Drawing.Point (x, y));
			
			double xx = this.window.MapFromWinFormsX (pt.X);
			double yy = this.window.MapFromWinFormsY (pt.Y)-1;
			
			return new Drawing.Point (xx, yy);
		}
		
		public Drawing.Point MapScreenToWindow(Drawing.Point point)
		{
			int x = this.window.MapToWinFormsX (point.X);
			int y = this.window.MapToWinFormsY (point.Y)-1;
			
			System.Drawing.Point pt = this.window.PointToClient (new System.Drawing.Point (x, y));
			
			double xx = pt.X;
			double yy = this.window.ClientSize.Height-1 - pt.Y;
			
			return new Drawing.Point (xx, yy);
		}
		
		
		protected void HandleTimeElapsed(object sender)
		{
			if (this.engaged_widget != null)
			{
				this.timer.AutoRepeat = SystemInformation.KeyboardRepeatPeriod;
				
				if (this.engaged_widget.IsEngaged)
				{
					this.engaged_widget.FireStillEngaged ();
					return;
				}
			}
			
			this.timer.Stop ();
		}

		protected void HandleRootMinSizeChanged(object sender, Types.DependencyPropertyChangedEventArgs e)
		{
			if (this.IsVisible)
			{
				this.SyncMinSizeWithWindowRoot ();
			}
		}
		
		private void SyncMinSizeWithWindowRoot()
		{
			int width  = (int) (this.root.RealMinSize.Width + 0.5);
			int height = (int) (this.root.RealMinSize.Height + 0.5);
			
			width  += this.window.Size.Width  - this.window.ClientSize.Width;
			height += this.window.Size.Height - this.window.ClientSize.Height;
			
			this.window.MinimumSize = new System.Drawing.Size (width, height);
		}

		
		#region PostPaint related definitions
		public interface IPostPaintHandler
		{
			void Paint(Drawing.Graphics graphics, Drawing.Rectangle repaint);
		}
		
		protected class PostPaintRecord
		{
			public PostPaintRecord(Window.IPostPaintHandler handler, Drawing.Graphics graphics, Drawing.Rectangle repaint)
			{
				this.handler   = handler;
				this.repaint   = repaint;
				this.clipping  = graphics.SaveClippingRectangle ();
				this.transform = graphics.Transform;
			}
			
			public void Paint(Drawing.Graphics graphics)
			{
				graphics.RestoreClippingRectangle (this.clipping);
				graphics.Transform = this.transform;
				
				handler.Paint (graphics, this.repaint);
			}
			
			Window.IPostPaintHandler		handler;
			Drawing.Rectangle				repaint;
			Drawing.Rectangle				clipping;
			Drawing.Transform				transform;
		}
		#endregion
		
		public event EventHandler				AsyncNotification;
		
		public event EventHandler				WindowActivated;
		public event EventHandler				WindowDeactivated;
		public event EventHandler				WindowShown;
		public event EventHandler				WindowHidden;
		public event EventHandler				WindowClosed;
		public event EventHandler				WindowCloseClicked;
		public event EventHandler				WindowAnimationEnded;
		public event EventHandler				WindowFocused;
		public event EventHandler				WindowDefocused;
		public event EventHandler				WindowDisposed;
		public event EventHandler				WindowDisposing;
		public event EventHandler				WindowSizeMoveStatusChanged;
		
		public event EventHandler				FocusedWidgetChanged;
		
		public event EventHandler				AboutToShowWindow;
		public event EventHandler				AboutToHideWindow;
		
		public event WindowDragEventHandler		WindowDragEntered;
		public event EventHandler				WindowDragLeft;
		public event WindowDragEventHandler		WindowDragDropped;
		
		public static event MessageHandler		MessageFilter;
		public static event EventHandler		ApplicationActivated;
		public static event EventHandler		ApplicationDeactivated;
		
		public enum InvalidateReason
		{
			Generic,
			AdornerChanged,
			CultureChanged
		}
		
		private string							name;
		private string							text;
		
		private Platform.Window					window;
		private Window							owner;
		private WindowRoot						root;
		private bool							window_is_visible;
		private bool							window_is_focused;
		private bool							window_location_set;
		
		private int								show_count;
		private Widget							last_in_widget;
		private Widget							capturing_widget;
		private MouseButtons					capturing_button;
		private Drawing.Point					capturing_cursor;
		private Widget							focused_widget;
		private Widget							engaged_widget;
		private Widget							initially_engaged_widget;
		private Timer							timer;
		private MouseCursor						window_cursor;
		private System.Collections.ArrayList	logical_focus_list = new System.Collections.ArrayList ();
		
		private CommandDispatcher				dispatcher;
		private System.Collections.Queue		cmd_queue = new System.Collections.Queue ();
		private bool							is_dispose_queued;
		private bool							is_async_notification_queued;
		private bool							is_disposed;
		private bool							pending_validation;
		private IPaintFilter					paint_filter;
		
		private System.Collections.Queue		post_paint_queue = new System.Collections.Queue ();
		private System.Collections.Hashtable	property_hash;
		
		private Support.Data.ComponentCollection components;
		
		static System.Collections.ArrayList		windows = new System.Collections.ArrayList ();
		static bool								is_running_in_automated_test_environment;
	}
}

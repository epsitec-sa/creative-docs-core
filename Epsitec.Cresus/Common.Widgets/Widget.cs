namespace Epsitec.Common.Widgets
{
	using ContentAlignment = Drawing.ContentAlignment;
	
	[System.Flags] public enum AnchorStyles
	{
		None			= 0,
		Top				= 1,
		Bottom			= 2,
		Left			= 4,
		Right			= 8,
			
		LeftAndRight	= Left + Right,
		TopAndBottom	= Top + Bottom,
	}
	
	[System.Flags] public enum WidgetState
	{
		ActiveNo		= 0,
		ActiveYes		= 1,
		ActiveMaybe		= 2,
		ActiveMask		= ActiveNo | ActiveYes | ActiveMaybe,
		
		
		None			= 0x00000000,		//	=> neutre
		Enabled			= 0x00010000,		//	=> pas gris�
		Focused			= 0x00020000,		//	=> re�oit les �v�nements clavier
		Entered			= 0x00040000,		//	=> contient la souris
		Selected		= 0x00080000,		//	=> s�lectionn�
		Engaged			= 0x00100000,		//	=> pression en cours
	}
	
	
	/// <summary>
	/// 
	/// </summary>
	public class Widget : System.IDisposable
	{
		public Widget()
		{
			this.internal_state |= InternalState.Visible;
			this.internal_state |= InternalState.AutoCapture;
			this.internal_state |= InternalState.AutoMnemonic;
			
			this.widget_state |= WidgetState.Enabled;
			
			this.anchor = AnchorStyles.Left | AnchorStyles.Top;
			this.back_color = new Drawing.Color (0.9);
			this.fore_color = new Drawing.Color (0.0);
		}
		
		public void Dispose()
		{
			this.Dispose (true);
			System.GC.SuppressFinalize (this);
		}
		
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.SetEntered (false);
			}
		}

		
		public AnchorStyles					Anchor
		{
			get { return this.anchor; }
			set { this.anchor = value; }
		}
		
		public Drawing.Color				BackColor
		{
			get { return this.back_color; }
			set
			{
				if (this.back_color != value)
				{
					this.back_color = value;
					this.Invalidate ();
				}
			}
		}
		
		public Drawing.Color				ForeColor
		{
			get { return this.fore_color; }
			set
			{
				if (this.fore_color != value)
				{
					this.fore_color = value;
					this.Invalidate ();
				}
			}
		}
		
		
		public double						Top
		{
			get { return this.y2; }
			set { this.SetBounds (this.x1, this.y1, this.x2, value); }
		}
		
		public double						Left
		{
			get { return this.x1; }
			set { this.SetBounds (value, this.y1, this.x2, this.y2); }
		}
		
		public double						Bottom
		{
			get { return this.y1; }
			set { this.SetBounds (this.x1, value, this.x2, this.y2); }
		}
		
		public double						Right
		{
			get { return this.x2; }
			set { this.SetBounds (this.x1, this.y1, value, this.y2); }
		}
		
		public Drawing.Rectangle			Bounds
		{
			get { return new Drawing.Rectangle (this.x1, this.y1, this.x2 - this.x1, this.y2 - this.y1); }
			set { this.SetBounds (value.X, value.Y, value.X + value.Width, value.Y + value.Height); }
		}
		
		public Drawing.Point				Location
		{
			get { return new Drawing.Point (this.x1, this.y1); }
			set { this.SetBounds (value.X, value.Y, value.X + this.x2 - this.x1, value.Y + this.y2 - this.y1); }
		}
		
		public Drawing.Size					Size
		{
			get { return new Drawing.Size (this.x2 - this.x1, this.y2 - this.y1); }
			set { this.SetBounds (this.x1, this.y1, this.x1 + value.Width, this.y1 + value.Height); }
		}
		
		public double						Width
		{
			get { return this.x2 - this.x1; }
			set { this.SetBounds (this.x1, this.y1, this.x1 + value, this.y2); }
		}
		
		public double						Height
		{
			get { return this.y2 - this.y1; }
			set { this.SetBounds (this.x1, this.y1, this.x2, this.y1 + value); }
		}
		
		public ContentAlignment				Alignment
		{
			get { return this.alignment; }
			set
			{
				if (this.alignment != value)
				{
					this.alignment = value;
					this.UpdateLayoutSize ();
					this.Invalidate ();
				}
			}
		}
		
		public ClientInfo					Client
		{
			get { return this.client_info; }
		}
		
		
		public void SuspendLayout()
		{
			lock (this)
			{
				this.suspend_counter++;
			}
		}
		
		public void ResumeLayout()
		{
			lock (this)
			{
				if (this.suspend_counter > 0)
				{
					this.suspend_counter--;
				}
				
				if (this.suspend_counter == 0)
				{
					if ((this.internal_state & InternalState.ChildrenChanged) != 0)
					{
						this.internal_state -= InternalState.ChildrenChanged;
						this.OnChildrenChanged (System.EventArgs.Empty);
					}
				}
			}
		}
		
		public void SetClientAngle(int angle)
		{
			this.client_info.SetAngle (angle);
			this.UpdateClientGeometry ();
		}
		
		public void SetClientZoom(double zoom)
		{
			this.client_info.SetZoom (zoom);
			this.UpdateClientGeometry ();
		}
		
		
		public virtual ContentAlignment		DefaultAlignment
		{
			get { return ContentAlignment.MiddleLeft; }
		}
		
		public virtual Drawing.Font			DefaultFont
		{
			get { return Drawing.Font.GetFont ("Tahoma", "Regular"); }
		}
		
		public virtual double				DefaultFontSize
		{
			get { return 10.6; }
		}
		
		public virtual double				DefaultWidth
		{
			get { return 80; }
		}
		public virtual double				DefaultHeight
		{
			get { return 20; }
		}
#if false
		public bool							CausesValidation
		{
			get;
			set;
		}
#endif
		public virtual bool					IsEnabled
		{
			get
			{
				if ((this.widget_state & WidgetState.Enabled) == 0)
				{
					return false;
				}
				if (this.parent != null)
				{
					return this.parent.IsEnabled;
				}
				
				return true;
			}
		}
		
		public virtual bool					IsFrozen
		{
			get
			{
				if ((this.internal_state & InternalState.Frozen) != 0)
				{
					return true;
				}
				if (this.parent != null)
				{
					return this.parent.IsFrozen;
				}
				
				return false;
			}
		}
		
		public virtual bool					IsVisible
		{
			get
			{
				if (((this.internal_state & InternalState.Visible) == 0) ||
					(this.parent == null))
				{
					return false;
				}
				
				return this.parent.IsVisible;
			}
		}

		public bool							IsFocused
		{
			get { return (this.widget_state & WidgetState.Focused) != 0; }
		}
		
		public bool							IsEntered
		{
			get { return (this.widget_state & WidgetState.Entered) != 0; }
		}
		
		public bool							IsSelected
		{
			get { return (this.widget_state & WidgetState.Selected) != 0; }
		}
		
		public bool							IsEngaged
		{
			get { return (this.widget_state & WidgetState.Engaged) != 0; }
		}
		
		public bool							AutoCapture
		{
			get { return (this.internal_state & InternalState.AutoCapture) != 0; }
			set
			{
				if (value)
				{
					this.internal_state |= InternalState.AutoCapture;
				}
				else
				{
					this.internal_state &= ~InternalState.AutoCapture;
				}
			}
		}
		
		public bool							AutoFocus
		{
			get { return (this.internal_state & InternalState.AutoFocus) != 0; }
			set
			{
				if (value)
				{
					this.internal_state |= InternalState.AutoFocus;
				}
				else
				{
					this.internal_state &= ~InternalState.AutoFocus;
				}
			}
		}
		
		public bool							AutoEngage
		{
			get { return (this.internal_state & InternalState.AutoEngage) != 0; }
			set
			{
				if (value)
				{
					this.internal_state |= InternalState.AutoEngage;
				}
				else
				{
					this.internal_state &= ~InternalState.AutoEngage;
				}
			}
		}
		
		public bool							AutoToggle
		{
			get { return (this.internal_state & InternalState.AutoToggle) != 0; }
			set
			{
				if (value)
				{
					this.internal_state |= InternalState.AutoToggle;
				}
				else
				{
					this.internal_state &= InternalState.AutoToggle;
				}
			}
		}
		
		public bool							AutoMnemonic
		{
			get { return (this.internal_state & InternalState.AutoMnemonic) != 0; }
		}
		
		public virtual WidgetState			State
		{
			get { return this.widget_state; }
		}
		
		public virtual WidgetState			ActiveState
		{
			get { return this.widget_state & WidgetState.ActiveMask; }
			set
			{
				WidgetState active = this.widget_state & WidgetState.ActiveMask;
				if (active != value)
				{
					this.widget_state &= ~WidgetState.ActiveMask;
					this.widget_state |= value & WidgetState.ActiveMask;
					this.OnActiveStateChanged (System.EventArgs.Empty);
					this.Invalidate ();
				}
			}
		}
		public bool							ContainsFocus
		{
			get
			{
				if (this.IsFocused)
				{
					return true;
				}
				
				if (this.Children.Count > 0)
				{
					Widget[] children = this.Children.Widgets;
					int  children_num = children.Length;
					
					for (int i = 0; i < children_num; i++)
					{
						if (children[i].ContainsFocus)
						{
							return true;
						}
					}
				}
				
				return false;
			}
		}
		
		public bool							CanFocus
		{
			get { return ((this.internal_state & InternalState.Focusable) != 0) && !this.IsFrozen; }
		}
		
		public bool							CanSelect
		{
			get { return ((this.internal_state & InternalState.Selectable) != 0) && !this.IsFrozen; }
		}
		
		public bool							CanEngage
		{
			get { return ((this.internal_state & InternalState.Engageable) != 0) && this.IsEnabled && !this.IsFrozen; }
		}
		
		public bool							AcceptThreeState
		{
			get { return (this.internal_state & InternalState.AcceptThreeState) != 0; }
			set
			{
				if (value)
				{
					this.internal_state |= InternalState.AcceptThreeState;
				}
				else
				{
					this.internal_state &= ~InternalState.AcceptThreeState;
				}
			}
		}
		
		public bool							AcceptTaggedText
		{
			get { return (this.internal_state & InternalState.AcceptTaggedText) != 0; }
		}
		
		
		public WidgetCollection				Children
		{
			get
			{
				if (this.children == null)
				{
					lock (this)
					{
						if (this.children == null)
						{
							this.CreateWidgetCollection ();
						}
					}
				}
				
				return this.children;
			}
		}
		
		public Widget						Parent
		{
			get { return this.parent; }
			
			set
			{
				if (value != this.parent)
				{
					if (value == null)
					{
						this.parent.Children.Remove (this);
					}
					else
					{
						value.Children.Add (this);
					}
				}
			}
		}
		
		public virtual WindowFrame			WindowFrame
		{
			get
			{
				Widget root = this.RootParent;
				
				if ((root == null) ||
					(root == this))
				{
					return null;
				}
				
				return root.WindowFrame;
			}
		}
		
		public virtual Widget				RootParent
		{
			get
			{
				Widget widget = this;
				
				while (widget.parent != null)
				{
					widget = widget.parent;
				}
				
				return widget;
			}
		}
		
		public int							RootAngle
		{
			get
			{
				Widget widget = this;
				int    angle  = 0;
				
				while (widget != null)
				{
					angle += widget.Client.Angle;
					widget = widget.parent;
				}
				
				return angle % 360;
			}
		}
		
		public Direction					RootDirection
		{
			get
			{
				switch (this.RootAngle)
				{
					case 0:		return Direction.Up;
					case 90:	return Direction.Left;
					case 180:	return Direction.Down;
					case 270:	return Direction.Right;
				}
				
				return Direction.None;
			}
		}
		
		
		public bool							HasChildren
		{
			get { return (this.children != null) && (this.children.Count > 0); }
		}
		
		public bool							HasParent
		{
			get { return this.parent != null; }
		}
		
		public string						Name
		{
			get
			{
				if ((this.name == null) || (this.name.Length == 0))
				{
					return "";
				}
				
				return this.name;
			}
			
			set
			{
				if ((value == null) || (value.Length == 0))
				{
					this.name = null;
				}
				else
				{
					this.name = value;
				}
			}
		}

		public virtual string				Text
		{
			get
			{
				if ((this.text == null) || (this.text.Length == 0))
				{
					return "";
				}
				
				return this.text;
			}
			
			set
			{
				if ((value == null) || (value.Length == 0))
				{
					this.text = null;
					this.UpdateLayoutText ();
					this.Shortcut.Mnemonic = (char) 0;
				}
				else
				{
					this.text = value;
					
					if (this.text_layout == null)
					{
						this.CreateTextLayout ();
					}
					
					this.UpdateLayoutText ();
					this.Shortcut.Mnemonic = this.Mnemonic;
				}
			}
		}
		
		public char							Mnemonic
		{
			get
			{
				string text = this.Text;
				
				if ((text != null) &&
					(this.AutoMnemonic))
				{
					if (this.AcceptTaggedText)
					{
						//	Le texte stock� dans le widget n'est pas un texte simple, mais
						//	un texte format� avec des tags. Le code mn�monique est pr�fix�
						//	par des tags <m>..</m>.
						
						return TextLayout.ExtractMnemonic (text);
					}
					else
					{
						int max = text.Length - 1;
						for (int i = 0; i < max; i++)
						{
							if ((text[i] == '&') && (text[i+1] != '&'))
							{
								char mnemonic = text[i+1];
								mnemonic = System.Char.ToUpper (mnemonic, System.Globalization.CultureInfo.CurrentCulture);
								return mnemonic;
							}
						}
					}
				}
				
				return (char) 0;
			}
		}
		
		public int							TabIndex
		{
			get { return this.tab_index; }
			set { this.tab_index = value; }
		}
		
		public TabNavigationMode			TabNavigation
		{
			get { return this.tab_navigation_mode; }
			set { this.tab_navigation_mode = value; }
		}
		
		public Shortcut						Shortcut
		{
			get
			{
				if (this.shortcut == null)
				{
					this.shortcut = new Shortcut ();
				}
				
				return this.shortcut;
			}
		}
		
		
		public event PaintEventHandler		PaintBackground;
		public event PaintEventHandler		PaintForeground;
		public event System.EventHandler	ChildrenChanged;
		public event System.EventHandler	LayoutChanged;
		
		public event MessageEventHandler	Clicked;
		public event MessageEventHandler	DoubleClicked;
		public event System.EventHandler	ShortcutPressed;
		
		public event System.EventHandler	Focused;
		public event System.EventHandler	Defocused;
		public event System.EventHandler	Selected;
		public event System.EventHandler	Deselected;
		public event System.EventHandler	ActiveStateChanged;
		
		
		//	Cursor
		//	FindNextWidget/FindPrevWidget
		
		public virtual void Hide()
		{
			this.SetVisible (false);
		}
		
		public virtual void Show()
		{
			this.SetVisible (true);
		}
		
		public virtual void Toggle()
		{
			if (this.AcceptThreeState)
			{
				switch (this.ActiveState)
				{
					case WidgetState.ActiveYes:
						this.ActiveState = WidgetState.ActiveMaybe;
						break;
					case WidgetState.ActiveMaybe:
						this.ActiveState = WidgetState.ActiveNo;
						break;
					case WidgetState.ActiveNo:
						this.ActiveState = WidgetState.ActiveYes;
						break;
				}
			}
			else
			{
				switch (this.ActiveState)
				{
					case WidgetState.ActiveYes:
					case WidgetState.ActiveMaybe:
						this.ActiveState = WidgetState.ActiveNo;
						break;
					case WidgetState.ActiveNo:
						this.ActiveState = WidgetState.ActiveYes;
						break;
				}
			}
		}
		
		
		public virtual void SetVisible(bool visible)
		{
			if ((this.internal_state & InternalState.Visible) == 0)
			{
				if (visible)
				{
					this.internal_state |= InternalState.Visible;
					this.Invalidate ();
				}
			}
			else
			{
				if (!visible)
				{
					this.internal_state &= ~ InternalState.Visible;
					this.Invalidate ();
				}
			}
		}
		
		public virtual void SetEnabled(bool enabled)
		{
			if ((this.widget_state & WidgetState.Enabled) == 0)
			{
				if (enabled)
				{
					this.widget_state |= WidgetState.Enabled;
					this.Invalidate ();
				}
			}
			else
			{
				if (!enabled)
				{
					this.widget_state &= ~ WidgetState.Enabled;
					this.Invalidate ();
				}
			}
		}
		
		public virtual void SetFrozen(bool frozen)
		{
			if ((this.internal_state & InternalState.Frozen) == 0)
			{
				if (frozen)
				{
					this.internal_state |= InternalState.Frozen;
					this.Invalidate ();
				}
			}
			else
			{
				if (!frozen)
				{
					this.internal_state &= ~ InternalState.Frozen;
					this.Invalidate ();
				}
			}
		}
		
		public virtual void SetFocused(bool focused)
		{
			WindowFrame frame = this.WindowFrame;
			
			if (frame == null)
			{
				return;
			}
			
			if ((this.widget_state & WidgetState.Focused) == 0)
			{
				if (focused)
				{
					this.widget_state |= WidgetState.Focused;
					frame.FocusedWidget = this;
					this.OnFocused (System.EventArgs.Empty);
					this.Invalidate ();
				}
			}
			else
			{
				if (!focused)
				{
					this.widget_state &= ~ WidgetState.Focused;
					frame.FocusedWidget = null;
					this.OnDefocused (System.EventArgs.Empty);
					this.Invalidate ();
				}
			}
		}
		
		public virtual void SetSelected(bool selected)
		{
			if ((this.widget_state & WidgetState.Selected) == 0)
			{
				if (selected)
				{
					this.widget_state |= WidgetState.Selected;
					this.Invalidate ();
					this.OnSelected (System.EventArgs.Empty);
				}
			}
			else
			{
				if (!selected)
				{
					this.widget_state &= ~WidgetState.Selected;
					this.Invalidate ();
					this.OnDeselected (System.EventArgs.Empty);
				}
			}
		}
		
		public virtual void SetEngaged(bool engaged)
		{
			WindowFrame frame = this.WindowFrame;
			
			if (frame == null)
			{
				return;
			}
			
			if ((this.widget_state & WidgetState.Engaged) == 0)
			{
				if (engaged)
				{
					this.widget_state |= WidgetState.Engaged;
					frame.EngagedWidget = this;
					this.Invalidate ();
				}
			}
			else
			{
				if (!engaged)
				{
					this.widget_state &= ~ WidgetState.Engaged;
					frame.EngagedWidget = null;
					this.Invalidate ();
				}
			}
		}
		
		
		protected void SetEntered(bool entered)
		{
			if (this.IsEntered != entered)
			{
				WindowFrame frame = this.WindowFrame;
				Message message = null;
				
				if (entered)
				{
					Widget.entered_widgets.Add (this);
					this.widget_state |= WidgetState.Entered;
					
					message = Message.FromMouseEvent (MessageType.MouseEnter, null, null);
				}
				else
				{
					Widget.entered_widgets.Remove (this);
					this.widget_state &= ~ WidgetState.Entered;
					
					message = Message.FromMouseEvent (MessageType.MouseLeave, null, null);
				}
				
				this.MessageHandler (message);
				frame.PostProcessMessage (message);
			}
		}
		
		public static void UpdateEntered(Message message)
		{
			int index = Widget.entered_widgets.Count;
			
			while (index > 0)
			{
				index--;
				
				if (index < Widget.entered_widgets.Count)
				{
					Widget widget = Widget.entered_widgets[index] as Widget;
					
					Drawing.Point point_in_widget = widget.MapRootToClient (message.Cursor);
					
					if ((point_in_widget.X < 0) ||
						(point_in_widget.Y < 0) ||
						(point_in_widget.X >= widget.Client.Width) ||
						(point_in_widget.Y >= widget.Client.Height))
					{
						widget.SetEntered (false);
					}
				}
			}
		}
		
		
		public virtual bool HitTest(Drawing.Point point)
		{
			if ((point.X >= this.x1) &&
				(point.X <  this.x2) &&
				(point.Y >= this.y1) &&
				(point.Y <  this.y2))
			{
				return true;
			}
			
			return false;
		}
		
		
		public virtual Drawing.Rectangle GetPaintBounds()
		{
			return new Drawing.Rectangle (0, 0, this.client_info.width, this.client_info.height);
		}
		
		public virtual void Invalidate()
		{
			this.Invalidate (this.GetPaintBounds ());
		}
		
		public virtual void Invalidate(Drawing.Rectangle rect)
		{
			rect = this.MapClientToParent (rect);
			
			if (this.parent != null)
			{
				this.parent.Invalidate (rect);
			}
			else
			{
				System.Diagnostics.Debug.WriteLine ("Widget.Invalidate has no parent");
			}
		}
		
		
		public virtual Drawing.Point MapParentToClient(Drawing.Point point)
		{
			double x = point.X - this.x1;
			double y = point.Y - this.y1;
			
			double angle = this.client_info.angle * System.Math.PI / 180.0;
			double zoom  = this.client_info.zoom;
			
			System.Diagnostics.Debug.Assert (zoom > 0.0f);
			System.Diagnostics.Debug.Assert ((angle >= 0) && (angle < 360));
			
			if (angle != 0)
			{
				double sin = System.Math.Sin (angle);
				double cos = System.Math.Cos (angle);
				
				x -= (this.x2 - this.x1) / 2;
				y -= (this.y2 - this.y1) / 2;
				
				double xr = (x * cos - y * sin) / zoom;
				double yr = (x * sin + y * cos) / zoom;
				
				x = xr + this.client_info.width / 2;
				y = yr + this.client_info.height / 2;
			}
			else
			{
				x /= zoom;
				y /= zoom;
			}
			
			return new Drawing.Point (x, y);
		}
		
		public virtual Drawing.Point MapClientToParent(Drawing.Point point)
		{
			double x = point.X;
			double y = point.Y;
			
			double angle = this.client_info.angle * System.Math.PI / 180.0;
			double zoom  = this.client_info.zoom;
			
			System.Diagnostics.Debug.Assert (zoom > 0.0f);
			System.Diagnostics.Debug.Assert ((angle >= 0) && (angle < 360));
			
			if (angle != 0)
			{
				double sin = System.Math.Sin (angle);
				double cos = System.Math.Cos (angle);
				
				x -= this.client_info.width / 2;
				y -= this.client_info.height / 2;
				
				double xr = (  x * cos + y * sin) * zoom;
				double yr = (- x * sin + y * cos) * zoom;
				
				x = xr + (this.x2 - this.x1) / 2;
				y = yr + (this.y2 - this.y1) / 2;
			}
			else
			{
				x *= zoom;
				y *= zoom;
			}
			
			return new Drawing.Point (x + this.x1, y + this.y1);
		}
		
		public virtual Drawing.Point MapRootToClient(Drawing.Point point)
		{
			Widget parent = this.Parent;
			
			//	Le plus simple est d'utiliser la r�cursion, afin de commencer la conversion depuis la
			//	racine, puis d'enfant en enfant jusqu'au widget final.
			
			if (parent != null)
			{
				point = parent.MapRootToClient (point);
			}
			
			return this.MapParentToClient (point);
		}
		
		public virtual Drawing.Point MapClientToRoot(Drawing.Point point)
		{
			Widget iter = this;
			
			//	On a le choix entre une solution r�cursive et une solution it�rative. La version
			//	it�rative devrait �tre un petit peu plus rapide ici.
			
			while (iter != null)
			{
				point = iter.MapClientToParent (point);
				iter = iter.Parent;
			}
			
			return point;
		}
		
		
		public virtual Drawing.Rectangle MapParentToClient(Drawing.Rectangle rect)
		{
			//	La conversion du rectangle du parent vers le client se fait en traitant le rectangle
			//	comme un bounding-box. En effet, si une rotation intervient, le rectangle r�sultant
			//	n'est plus align� sur les axes Ox/Oy, et il n'a plus de sens; on calcule donc le
			//	nouveau rectangle align�, englobant le rectangle transform�.
			
			double x1 = rect.Left   - this.x1;
			double y1 = rect.Bottom - this.y1;
			double x2 = rect.Right  - this.x1;
			double y2 = rect.Top    - this.y1;
			
			double angle = this.client_info.angle * System.Math.PI / 180.0;
			double zoom  = this.client_info.zoom;
			
			System.Diagnostics.Debug.Assert (zoom > 0.0f);
			System.Diagnostics.Debug.Assert ((angle >= 0) && (angle < 360));
			
			if (angle != 0)
			{
				double sin = System.Math.Sin (angle);
				double cos = System.Math.Cos (angle);
				
				double cx = (this.x2 - this.x1) / 2;
				double cy = (this.y2 - this.y1) / 2;
				
				x1 -= cx;		x2 -= cx;
				y1 -= cy;		y2 -= cy;
				
				//	La rotation d'un rectangle peut �tre p�rilleuse... Il faut donc consid�rer les quatre
				//	coins, et prendre en fin de compte les extr�mes.
				
				double xr1 = (x1 * cos - y1 * sin);
				double yr1 = (x1 * sin + y1 * cos);
				double xr2 = (x2 * cos - y2 * sin);
				double yr2 = (x2 * sin + y2 * cos);
				
				double xr3 = (x1 * cos - y2 * sin);
				double yr3 = (x1 * sin + y2 * cos);
				double xr4 = (x2 * cos - y1 * sin);
				double yr4 = (x2 * sin + y1 * cos);
				
				xr1 = System.Math.Min (xr1, System.Math.Min (xr2, System.Math.Min (xr3, xr4)));
				xr2 = System.Math.Max (xr1, System.Math.Max (xr2, System.Math.Max (xr3, xr4)));
				yr1 = System.Math.Min (yr1, System.Math.Min (yr2, System.Math.Min (yr3, yr4)));
				yr2 = System.Math.Max (yr1, System.Math.Max (yr2, System.Math.Max (yr3, yr4)));
				
				xr1 /= zoom;
				yr1 /= zoom;
				xr2 /= zoom;
				yr2 /= zoom;
				
				cx = this.client_info.width / 2;
				cy = this.client_info.height / 2;
				
				x1 = xr1 + cx;		x2 = xr2 + cx;
				y1 = yr1 + cy;		y2 = yr2 + cy;
			}
			else
			{
				x1 /= zoom;		x2 /= zoom;
				y1 /= zoom;		y2 /= zoom;
			}
			
			rect.X = x1;
			rect.Y = y1;
			
			rect.Width  = x2 - x1;
			rect.Height = y2 - y1;
			
			return rect;
		}
		
		public virtual Drawing.Rectangle MapClientToParent(Drawing.Rectangle rect)
		{
			//	La conversion du rectangle du client vers le parent se fait en traitant le rectangle
			//	comme un bounding-box. En effet, si une rotation intervient, le rectangle r�sultant
			//	n'est plus align� sur les axes Ox/Oy, et il n'a plus de sens; on calcule donc le
			//	nouveau rectangle align�, englobant le rectangle transform�.
			
			double x1 = rect.Left;
			double y1 = rect.Bottom;
			double x2 = rect.Right;
			double y2 = rect.Top;
			
			double angle = this.client_info.angle * System.Math.PI / 180.0;
			double zoom  = this.client_info.zoom;
			
			System.Diagnostics.Debug.Assert (zoom > 0.0f);
			System.Diagnostics.Debug.Assert ((angle >= 0) && (angle < 360));
			
			if (angle != 0)
			{
				double sin = System.Math.Sin (angle);
				double cos = System.Math.Cos (angle);
				
				double cx = this.client_info.width / 2;
				double cy = this.client_info.height / 2;
				
				x1 -= cx;		x2 -= cx;
				y1 -= cy;		y2 -= cy;
				
				//	La rotation d'un rectangle peut �tre p�rilleuse... Il faut donc consid�rer les quatre
				//	coins, et prendre en fin de compte les extr�mes.
				
				double xr1 = ( x1 * cos + y1 * sin);
				double yr1 = (-x1 * sin + y1 * cos);
				double xr2 = ( x2 * cos + y2 * sin);
				double yr2 = (-x2 * sin + y2 * cos);
				
				double xr3 = ( x1 * cos + y2 * sin);
				double yr3 = (-x1 * sin + y2 * cos);
				double xr4 = ( x2 * cos + y1 * sin);
				double yr4 = (-x2 * sin + y1 * cos);
				
				xr1 = System.Math.Min (xr1, System.Math.Min (xr2, System.Math.Min (xr3, xr4)));
				xr2 = System.Math.Max (xr1, System.Math.Max (xr2, System.Math.Max (xr3, xr4)));
				yr1 = System.Math.Min (yr1, System.Math.Min (yr2, System.Math.Min (yr3, yr4)));
				yr2 = System.Math.Max (yr1, System.Math.Max (yr2, System.Math.Max (yr3, yr4)));
				
				xr1 *= zoom;
				yr1 *= zoom;
				xr2 *= zoom;
				yr2 *= zoom;
				
				
				cx = (this.x2 - this.x1) / 2;
				cy = (this.y2 - this.y1) / 2;
				
				x1 = xr1 + cx;		x2 = xr2 + cx;
				y1 = yr1 + cy;		y2 = yr2 + cy;
			}
			else
			{
				x1 *= zoom;		x2 *= zoom;
				y1 *= zoom;		y2 *= zoom;
			}
			
			rect.X = x1 + this.x1;
			rect.Y = y1 + this.y1;
			
			rect.Width  = x2 - x1;
			rect.Height = y2 - y1;
			
			return rect;
		}

		
		public virtual Epsitec.Common.Drawing.Transform GetRootToClientTransform()
		{
			Widget iter = this;
			
			Drawing.Transform full_transform  = new Drawing.Transform ();
			Drawing.Transform local_transform = new Drawing.Transform ();
			
			while (iter != null)
			{
				local_transform.Reset ();
				iter.MergeTransformToClient (local_transform);
				
				//	Les transformations de la racine au client doivent s'appliquer en commen�ant par
				//	la racine. Comme nous remontons la hi�rarchie des widgets en sens inverse, il nous
				//	suffit d'utiliser la multiplication post-fixe pour arriver au m�me r�sultat :
				//
				//	 T = Tn * ... * T2 * T1 * T0, P' = T * P
				//
				//	avec Ti la transformation pour le widget 'i', o� i=0 correspond � la racine,
				//	P le point en coordonn�es racine et P' le point en coordonn�es client.
				
				full_transform.MultiplyByPostfix (local_transform);
				iter = iter.Parent;
			}
			
			return full_transform;
		}
		
		public virtual Epsitec.Common.Drawing.Transform GetClientToRootTransform()
		{
			Widget iter = this;
			
			Drawing.Transform full_transform  = new Drawing.Transform ();
			Drawing.Transform local_transform = new Drawing.Transform ();
			
			while (iter != null)
			{
				local_transform.Reset ();
				iter.MergeTransformToParent (local_transform);
				
				//	Les transformations du client � la racine doivent s'appliquer en commen�ant par
				//	le client. Comme nous remontons la hi�rarchie des widgets dans ce sens l�, il nous
				//	suffit d'utiliser la multiplication normale pour arriver � ce r�sultat :
				//
				//	 T = T0 * T1 * T2 * ... * Tn, P' = T * P
				//
				//	avec Ti la transformation pour le widget 'i', o� i=0 correspond � la racine.
				//	P le point en coordonn�es client et P' le point en coordonn�es racine.
				
				full_transform.MultiplyBy (local_transform);
				iter = iter.Parent;
			}
			
			return full_transform;
		}
		
		
		public virtual void MergeTransformToClient(Epsitec.Common.Drawing.Transform t)
		{
			double scale = 1 / this.client_info.zoom;
			
			t.Translate (- this.x1, - this.y1);
			t.Translate (- this.Width / 2, - this.Height / 2);
			t.Rotate (this.client_info.angle);
			t.Scale (scale);
			t.Translate (this.client_info.width / 2, this.client_info.height / 2);
			t.Round ();
		}
		
		public virtual void MergeTransformToParent(Epsitec.Common.Drawing.Transform t)
		{
			double scale = this.client_info.zoom;
			
			t.Translate (- this.client_info.width / 2, - this.client_info.height / 2);
			t.Scale (scale);
			t.Rotate (- this.client_info.angle);
			t.Translate (this.Width / 2, this.Height / 2);
			t.Translate (this.x1, this.y1);
			t.Round ();
		}
		
		
		public Widget FindChild(Drawing.Point point)
		{
			return this.FindChild (point, ChildFindMode.SkipHidden);
		}
		
		public virtual Widget FindChild(Drawing.Point point, ChildFindMode mode)
		{
			if (this.Children.Count == 0)
			{
				return null;
			}
			
			Widget[] children = this.Children.Widgets;
			int  children_num = children.Length;
			
			for (int i = 0; i < children_num; i++)
			{
				Widget widget = children[children_num-1 - i];
				
				System.Diagnostics.Debug.Assert (widget != null);
				
				if (mode != ChildFindMode.All)
				{
					if ((mode & ChildFindMode.SkipDisabled) != 0)
					{
						if (widget.IsEnabled == false)
						{
							continue;
						}
					}
					else if ((mode & ChildFindMode.SkipHidden) != 0)
					{
						if (widget.IsVisible == false)
						{
							continue;
						}
					}
				}
				
				if (widget.HitTest (point))
				{
					if ((mode & ChildFindMode.SkipTransparent) != 0)
					{
						//	TODO: v�rifier que le point en question n'est pas transparent
					}
					
					return widget;
				}
			}
			
			return null;
		}
		
		
		protected virtual void SetBounds(double x1, double y1, double x2, double y2)
		{
			if ((x1 == this.x1) && (y1 == this.y1) && (x2 == this.x2) && (y2 == this.y2))
			{
				return;
			}
			
			this.Invalidate ();
			
			this.x1 = x1;
			this.y1 = y1;
			this.x2 = x2;
			this.y2 = y2;
			
			this.Invalidate ();
			this.UpdateClientGeometry ();
			this.UpdateLayoutSize ();
		}
		
		
		protected virtual void UpdateLayoutSize()
		{
			if (this.text_layout != null)
			{
				this.text_layout.Alignment  = this.Alignment;
				this.text_layout.LayoutSize = this.Size;
			}
		}
		
		protected virtual void UpdateLayoutText()
		{
			if (this.text_layout != null)
			{
				if (this.AcceptTaggedText)
				{
					this.text_layout.Text = this.Text;
				}
				else
				{
					//	Le widget n'accepte pas de texte format� en entr�e; on doit donc
					//	s'assurer que le texte pass� � TextLayout est conforme.
					
					this.text_layout.Text = TextLayout.ConvertToTaggedText (this.Text, this.AutoMnemonic);
				}
			}
		}
		
		
		protected virtual void UpdateClientGeometry()
		{
			System.Diagnostics.Debug.Assert (this.layout_info == null);
			
			this.layout_info = new LayoutInfo (this.client_info.width, this.client_info.height);
			
			try
			{
				double zoom = this.client_info.zoom;
				
				double dx = (this.x2 - this.x1) / zoom;
				double dy = (this.y2 - this.y1) / zoom;
				
				switch (this.client_info.angle)
				{
					case 0:
					case 180:
						this.client_info.SetSize (dx, dy);
						break;
					
					case 90:
					case 270:
						this.client_info.SetSize (dy, dx);
						break;
					
					default:
						double angle = this.client_info.angle * System.Math.PI / 180.0;
						double cos = System.Math.Cos (angle);
						double sin = System.Math.Sin (angle);
						this.client_info.SetSize (cos*cos*dx + sin*sin*dy, sin*sin*dx + cos*cos*dy);
						break;
				}
				
				this.UpdateChildrenLayout ();
				this.OnLayoutChanged (System.EventArgs.Empty);
			}
			finally
			{
				this.layout_info = null;
			}
		}
		
		protected virtual void UpdateChildrenLayout()
		{
			System.Diagnostics.Debug.Assert (this.client_info != null);
			System.Diagnostics.Debug.Assert (this.layout_info != null);
			
			if (this.HasChildren)
			{
				double width_diff  = this.client_info.width  - this.layout_info.OriginalWidth;
				double height_diff = this.client_info.height - this.layout_info.OriginalHeight;
				
				foreach (Widget child in this.children)
				{
					AnchorStyles anchor_x = (AnchorStyles) child.Anchor & AnchorStyles.LeftAndRight;
					AnchorStyles anchor_y = (AnchorStyles) child.Anchor & AnchorStyles.TopAndBottom;
					
					double x1 = child.x1;
					double x2 = child.x2;
					double y1 = child.y1;
					double y2 = child.y2;
					
					switch (anchor_x)
					{
						case AnchorStyles.Left:							//	[x1] fixe � gauche
							break;
						case AnchorStyles.Right:						//	[x2] fixe � droite
							x1 += width_diff;
							x2 += width_diff;
							break;
						case AnchorStyles.None:							//	[x1] et [x2] mobiles (centr�)
							x1 += width_diff / 2.0f;
							x2 += width_diff / 2.0f;
							break;
						case AnchorStyles.LeftAndRight:					//	[x1] fixe � gauche, [x2] fixe � droite
							x2 += width_diff;
							break;
					}
					
					switch (anchor_y)
					{
						case AnchorStyles.Bottom:						//	[y1] fixe en bas
							break;
						case AnchorStyles.Top:							//	[y2] fixe en haut
							y1 += height_diff;
							y2 += height_diff;
							break;
						case AnchorStyles.None:							//	[y1] et [y2] mobiles (centr�)
							y1 += height_diff / 2.0f;
							y2 += height_diff / 2.0f;
							break;
						case AnchorStyles.TopAndBottom:					//	[y1] fixe en bas, [y2] fixe en haut
							y2 += height_diff;
							break;
					}
					
					child.SetBounds (x1, y1, x2, y2);
				}
			}
		}
		
		
		public virtual void PaintHandler(Drawing.Graphics graphics, Drawing.Rectangle repaint)
		{
			if (this.PaintCheckClipping (repaint))
			{
				repaint = this.MapParentToClient (repaint);
				
				Drawing.Transform original_transform = graphics.SaveTransform ();
				Drawing.Transform graphics_transform = new Drawing.Transform (original_transform);
				
				this.MergeTransformToParent (graphics_transform);
				
				graphics.Transform = graphics_transform;
			
				try
				{
					PaintEventArgs local_paint_args = new PaintEventArgs (graphics, repaint);
					
					//	Peint l'arri�re-plan du widget. En principe, tout va dans l'arri�re plan, sauf
					//	si l'on d�sire r�aliser des effets de transparence par dessus le dessin des
					//	widgets enfants.
					
					this.OnPaintBackground (local_paint_args);
					
					//	Peint tous les widgets enfants, en commen�ant par le num�ro 0, lequel se trouve
					//	derri�re tous les autres, etc. On saute les widgets qui ne sont pas visibles.
					
					if (this.Children.Count > 0)
					{
						Widget[] children = this.Children.Widgets;
						int  children_num = children.Length;
						
						for (int i = 0; i < children_num; i++)
						{
							Widget widget = children[i];
						
							System.Diagnostics.Debug.Assert (widget != null);
						
							if (widget.IsVisible)
							{
								widget.PaintHandler (graphics, repaint);
							}
						}
					}
				
					//	Peint l'avant-plan du widget, � n'utiliser que pour faire un "effet" sp�cial
					//	apr�s coup.
					
					this.OnPaintForeground (local_paint_args);
				}
				finally
				{
					graphics.Transform = original_transform;
				}
			}
		}
		
		protected virtual void PaintBackgroundImplementation(Drawing.Graphics graphics, Drawing.Rectangle clip_rect)
		{
			//	Impl�menter le dessin du fond dans cette m�thode.
		}
		
		protected virtual void PaintForegroundImplementation(Drawing.Graphics graphics, Drawing.Rectangle clip_rect)
		{
			//	Impl�menter le dessin des enjoliveurs additionnels dans cette m�thode.
		}
		
		protected virtual bool PaintCheckClipping(Drawing.Rectangle repaint)
		{
			return repaint.IntersectsWith (this.Bounds);
		}
		
		
		public void MessageHandler(Message message)
		{
			Drawing.Point point = message.Cursor;
			
			point = this.MapRootToClient (point);
			point = this.MapClientToParent (point);
			
			this.MessageHandler (message, point);
		}
		
		public virtual void MessageHandler(Message message, Drawing.Point pos)
		{
			this.PreProcessMessage (message, pos);
			
			//	En premier lieu, si le message peut �tre transmis aux descendants de ce widget, passe
			//	en revue ceux-ci dans l'ordre inverse de leur affichage (commence par le widget qui est
			//	visuellement au sommet).
			
			if ((message.FilterNoChildren == false) &&
				(message.Handled == false) &&
				(this.Children.Count > 0))
			{
				Drawing.Point client_pos = this.MapParentToClient (pos);
				
				Widget[] children = this.Children.Widgets;
				int  children_num = children.Length;
				
				for (int i = 0; i < children_num; i++)
				{
					Widget widget = children[children_num-1 - i];
					
					if ((widget.IsEnabled) &&
						(widget.IsFrozen == false) &&
						((message.FilterOnlyFocused == false) || (widget.ContainsFocus)) &&
						((message.FilterOnlyOnHit == false) || (widget.HitTest (client_pos))))
					{
						if (message.IsMouseType)
						{
							//	C'est un message souris. V�rifions d'abord si le widget contenait d�j�
							//	la souris auparavant.
							
							if (widget.IsEntered == false)
							{
								widget.SetEntered (true);
							}
						}
						
						widget.MessageHandler (message, client_pos);
						
						if (message.Handled)
						{
							break;
						}
					}
				}
			}
			else if ((message.Handled == false) &&
				/**/ (message.Captured) &&
				/**/ (message.IsMouseType))
			{
				if ((this.IsEntered == false) &&
					(message.InWidget == this))
				{
					this.SetEntered (true);
				}
			}
			
			if (message.Handled == false)
			{
				this.DispatchMessage (message, pos);
			}
			
			this.PostProcessMessage (message, pos);
		}
		
		protected virtual void DispatchMessage(Message message, Drawing.Point pos)
		{
			if (message.Type == MessageType.MouseUp)
			{
				switch (message.ButtonDownCount)
				{
					case 1:	this.OnClicked (new MessageEventArgs (message, pos));		break;
					case 2:	this.OnDoubleClicked (new MessageEventArgs (message, pos));	break;
				}
			}
			
			this.ProcessMessage (message, pos);
		}
		
		protected virtual void PreProcessMessage(Message message, Drawing.Point pos)
		{
			//	...appel� avant que l'�v�nement ne soit trait�...
		}
		
		protected virtual void ProcessMessage(Message message, Drawing.Point pos)
		{
			//	...appel� pour traiter l'�v�nement...
		}
		
		protected virtual void PostProcessMessage(Message message, Drawing.Point pos)
		{
			//	...appel� apr�s que l'�v�nement ait �t� trait�...
		}
		
		
		public virtual bool ShortcutHandler(Shortcut shortcut)
		{
			return this.ShortcutHandler (shortcut, true);
		}
		
		protected virtual bool ShortcutHandler(Shortcut shortcut, bool execute_focused)
		{
			Widget[] children = this.Children.Widgets;
			int  children_num = children.Length;
			
			if (execute_focused)
			{
				for (int i = 0; i < children_num; i++)
				{
					Widget widget = children[children_num-1 - i];
				
					if ((widget.IsEnabled) &&
						(widget.IsFrozen == false) &&
						(widget.ContainsFocus))
					{
						if (widget.ShortcutHandler (shortcut))
						{
							return true;
						}
					}
				}
			}
			
			if (this.ProcessShortcut (shortcut))
			{
				return true;
			}
			
			for (int i = 0; i < children_num; i++)
			{
				Widget widget = children[children_num-1 - i];
				
				if ((widget.IsEnabled) &&
					(widget.IsFrozen == false))
				{
					if (widget.ShortcutHandler (shortcut, false))
					{
						return true;
					}
				}
			}
			
			return false;
		}
		
		protected virtual bool ProcessShortcut(Shortcut shortcut)
		{
			if ((this.shortcut != null) &&
				(this.shortcut.Match (shortcut)))
			{
				this.OnShortcutPressed (System.EventArgs.Empty);
				return true;
			}
			
			return false;
		}
		
		
		protected virtual void CreateWidgetCollection()
		{
			this.children = new WidgetCollection (this);
		}
		
		protected virtual void CreateTextLayout()
		{
			this.text_layout = new TextLayout ();
			
			this.text_layout.Font       = this.DefaultFont;
			this.text_layout.FontSize   = this.DefaultFontSize;
			
			this.UpdateLayoutSize ();
			this.UpdateLayoutText ();
		}
		
		protected TextLayout			text_layout;

		protected virtual void OnPaintBackground(PaintEventArgs e)
		{
			if (this.PaintBackground != null)
			{
				e.Suppress = false;
				
				this.PaintBackground (this, e);
				
				if (e.Suppress)
				{
					return;
				}
			}
			
			this.PaintBackgroundImplementation (e.Graphics, e.ClipRectangle);
		}
		
		protected virtual void OnPaintForeground(PaintEventArgs e)
		{
			if (this.PaintForeground != null)
			{
				e.Suppress = false;
				
				this.PaintForeground (this, e);
				
				if (e.Suppress)
				{
					return;
				}
			}
			
			this.PaintForegroundImplementation (e.Graphics, e.ClipRectangle);
		}
		
		protected virtual void OnChildrenChanged(System.EventArgs e)
		{
			this.Invalidate ();
			
			if (this.ChildrenChanged != null)
			{
				this.ChildrenChanged (this, e);
			}
		}
		
		protected virtual void OnLayoutChanged(System.EventArgs e)
		{
			if (this.LayoutChanged != null)
			{
				this.LayoutChanged (this, e);
			}
		}
		
		
		protected virtual void OnClicked(MessageEventArgs e)
		{
			if (this.Clicked != null)
			{
				e.Message.Consumer = this;
				this.Clicked (this, e);
			}
		}
		
		protected virtual void OnDoubleClicked(MessageEventArgs e)
		{
			if (this.DoubleClicked != null)
			{
				e.Message.Consumer = this;
				this.DoubleClicked (this, e);
			}
		}
		
		protected virtual void OnShortcutPressed(System.EventArgs e)
		{
			if (this.ShortcutPressed != null)
			{
				this.ShortcutPressed (this, e);
			}
		}
		
		
		protected virtual void OnFocused(System.EventArgs e)
		{
			if (this.Focused != null)
			{
				this.Focused (this, e);
			}
		}
		
		protected virtual void OnDefocused(System.EventArgs e)
		{
			if (this.Defocused != null)
			{
				this.Defocused (this, e);
			}
		}
		
		protected virtual void OnSelected(System.EventArgs e)
		{
			if (this.Selected != null)
			{
				this.Selected (this, e);
			}
		}
		
		protected virtual void OnDeselected(System.EventArgs e)
		{
			if (this.Deselected != null)
			{
				this.Deselected (this, e);
			}
		}
		
		protected virtual void OnActiveStateChanged(System.EventArgs e)
		{
			if (this.ActiveStateChanged != null)
			{
				this.ActiveStateChanged (this, e);
			}
		}
		
		
		[System.Flags] protected enum InternalState
		{
			None				= 0,
			ChildrenChanged		= 0x00000001,
			
			Focusable			= 0x00000010,
			Selectable			= 0x00000020,
			Engageable			= 0x00000040,		//	=> peut �tre enfonc� par une pression
			Frozen				= 0x00000080,		//	=> n'accepte aucun �v�nement
			Visible				= 0x00000100,
			AcceptThreeState	= 0x00000200,
			AcceptTaggedText	= 0x00000400,
			
			AutoCapture			= 0x00010000,
			AutoFocus			= 0x00020000,
			AutoEngage			= 0x00040000,
			AutoToggle			= 0x00080000,
			AutoMnemonic		= 0x00100000,
		}
		
		[System.Flags] public enum TabNavigationMode
		{
			Passive				= 0,
			
			ActivateOnTab		= 0x00000001,
			ActivateOnCursorX	= 0x00000002,
			ActivateOnCursorY	= 0x00000004,
			ActivateOnCursor	= ActivateOnCursorX + ActivateOnCursorY,
			ActivateOnPage		= 0x00000008,
		}
		
		[System.Flags] public enum ChildFindMode
		{
			All				= 0,
			SkipHidden		= 1,
			SkipDisabled	= 2,
			SkipTransparent	= 4
		}
		
		
		public class ClientInfo
		{
			internal ClientInfo()
			{
			}
			
			internal void SetSize(double width, double height)
			{
				this.width  = width;
				this.height = height;
			}
			
			internal void SetAngle(int angle)
			{
				angle = angle % 360;
				this.angle = (angle < 0) ? (short) (angle + 360) : (short) (angle);
			}
			
			internal void SetZoom(double zoom)
			{
				System.Diagnostics.Debug.Assert (zoom > 0.0f);
				this.zoom = zoom;
			}
			
			public double					Width
			{
				get { return this.width; }
			}
			
			public double					Height
			{
				get { return this.height; }
			}
			
			public Drawing.Size				Size
			{
				get { return new Drawing.Size (this.width, this.height); }
			}
			
			public int						Angle
			{
				get { return this.angle; }
			}
			
			public double					Zoom
			{
				get { return this.zoom; }
			}
			
			internal double					width	= 0.0;
			internal double					height	= 0.0;
			internal short					angle	= 0;
			internal double					zoom	= 1.0;
		}
		
		public class WidgetCollection : System.Collections.IList
		{
			public WidgetCollection(Widget widget)
			{
				this.list   = new System.Collections.ArrayList ();
				this.widget = widget;
			}
			
			
			public Widget[]					Widgets
			{
				get
				{
					if (this.array == null)
					{
						this.array = new Widget[this.list.Count];
						this.list.CopyTo (this.array);
					}
					
					return this.array;
				}
			}
			
			private void PreInsert(object widget)
			{
				if (widget is Widget)
				{
					this.PreInsert (widget as Widget);
				}
				else
				{
					throw new System.ArgumentException ("Widget");
				}
			}
			
			private void PreInsert(Widget widget)
			{
				if (widget.parent != null)
				{
					Widget parent = widget.parent;
					parent.Children.Remove (widget);
					System.Diagnostics.Debug.Assert (widget.parent == null);
				}
				widget.parent = this.widget;
			}
			
			private void PostInsert(object widget)
			{
				if (this.widget.suspend_counter == 0)
				{
					this.widget.OnChildrenChanged (System.EventArgs.Empty);
				}
				else
				{
					this.widget.internal_state |= InternalState.ChildrenChanged;
				}
			}
			
			private void PreRemove(object widget)
			{
				if (widget is Widget)
				{
					this.PreRemove (widget as Widget);
				}
				else
				{
					throw new System.ArgumentException ("Widget");
				}
			}
			
			private void PreRemove(Widget widget)
			{
				System.Diagnostics.Debug.Assert (widget.parent == this.widget);
				widget.parent = null;
			}
			
			private void NotifyChanged()
			{
				if (this.widget.suspend_counter == 0)
				{
					this.widget.OnChildrenChanged (System.EventArgs.Empty);
				}
				else
				{
					this.widget.internal_state |= InternalState.ChildrenChanged;
				}
			}
			
			
			
			#region IList Members
			
			public bool IsReadOnly
			{
				get	{ return false; }
			}
			
			public object this[int index]
			{
				get	{ return this.list[index]; }
				set	{ throw new System.NotSupportedException ("Widget"); }
			}
			
			public void RemoveAt(int index)
			{
				System.Diagnostics.Debug.Assert (this.list[index] != null);
				this.PreRemove (this.list[index]);
				this.list.RemoveAt (index);
				this.NotifyChanged ();
			}
			
			public void Insert(int index, object value)
			{
				throw new System.NotSupportedException ("Widget");
			}
			
			public void Remove(object value)
			{
				this.PreRemove (value);
				this.list.Remove (value);
				this.NotifyChanged ();
			}
			
			public bool Contains(object value)
			{
				return this.list.Contains (value);
			}
			
			public void Clear()
			{
				while (this.Count > 0)
				{
					this.RemoveAt (this.Count - 1);
				}
			}
			
			public int IndexOf(object value)
			{
				return this.list.IndexOf (value);
			}
			
			public int Add(object value)
			{
				this.PreInsert (value);
				int result = this.list.Add (value);
				this.NotifyChanged ();
				return result;
			}
			
			public bool IsFixedSize
			{
				get	{ return false; }
			}

			#endregion

			#region ICollection Members

			public bool IsSynchronized
			{
				get { return false; }
			}
			
			public int Count
			{
				get	{ return this.list.Count; }
			}

			public void CopyTo(System.Array array, int index)
			{
				this.list.CopyTo (array, index);
			}
			
			public object SyncRoot
			{
				get { return this.list.SyncRoot; }
			}

			#endregion

			#region IEnumerable Members

			public System.Collections.IEnumerator GetEnumerator()
			{
				return this.list.GetEnumerator ();
			}
			
			#endregion
			
			System.Collections.ArrayList	list;
			Widget[]						array;
			Widget							widget;
		}
		
		
		protected class LayoutInfo
		{
			internal LayoutInfo(double width, double height)
			{
				this.width  = width;
				this.height = height;
			}
			
			public double					OriginalWidth
			{
				get { return this.width; }
			}
			
			public double					OriginalHeight
			{
				get { return this.height; }
			}
			
			private double					width, height;
		}
		
		
		
		protected AnchorStyles				anchor;
		protected Drawing.Color				back_color;
		protected Drawing.Color				fore_color;
		protected double					x1, y1, x2, y2;
		protected ClientInfo				client_info = new ClientInfo ();
		
		protected WidgetCollection			children;
		protected Widget					parent;
		protected string					name;
		protected string					text;
		protected ContentAlignment			alignment;
		protected LayoutInfo				layout_info;
		protected InternalState				internal_state;
		protected WidgetState				widget_state;
		protected int						suspend_counter;
		protected int						tab_index;
		protected TabNavigationMode			tab_navigation_mode;
		protected Shortcut					shortcut;
		
		static System.Collections.ArrayList	entered_widgets = new System.Collections.ArrayList ();
	}
}

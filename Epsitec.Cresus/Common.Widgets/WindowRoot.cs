namespace Epsitec.Common.Widgets
{
	/// <summary>
	/// La classe WindowRoot impl�mente le fond de chaque fen�tre. L'utilisateur obtient
	/// en g�n�ral une instance de WindowRoot en appelant Window.Root.
	/// </summary>
	public class WindowRoot : AbstractGroup
	{
		protected WindowRoot()
		{
			this.InternalState |= InternalState.PossibleContainer;
			this.InternalState |= InternalState.AutoDoubleClick;
		}
		
		
		public WindowRoot(Window window) : this ()
		{
			this.window   = window;
			this.is_ready = true;
		}
		
		
		public override bool						IsVisible
		{
			get { return true; }
		}
		
		public override Window						Window
		{
			get { return this.window; }
		}
		
		public override Support.CommandDispatcher	CommandDispatcher
		{
			get
			{
				if (this.window != null)
				{
					return this.window.CommandDispatcher;
				}
				
				return null;
			}
		}
		
		
		#region IBundleSupport Members
		public override string						PublicClassName
		{
			get { return "Window"; }
		}
		
		public override void RestoreFromBundle(Support.ObjectBundler bundler, Support.ResourceBundle bundle)
		{
			this.window = new Window (this);
			
			base.RestoreFromBundle (bundler, bundle);
			
			this.window.Name       = this.Name;
			this.window.ClientSize = this.Size;
			this.window.Text       = this.Text;
			
			if (bundle["icon"].Type == Support.ResourceFieldType.Data)
			{
				this.window.Icon = Support.ImageProvider.Default.GetImage ("res:" + bundle["icon"].AsString);
			}
			
			this.is_ready = true;
		}
		#endregion
		
		public override void Invalidate()
		{
			System.Diagnostics.Debug.Assert (this.Parent == null);
			
			if (this.window != null)
			{
				this.window.MarkForRepaint (this.Bounds);
			}
		}
		
		public override void Invalidate(Drawing.Rectangle rect)
		{
			System.Diagnostics.Debug.Assert (this.Parent == null);
			
			if (this.window != null)
			{
				this.window.MarkForRepaint (this.MapClientToParent (rect));
			}
		}
		
		
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.window = null;
			}
			
			base.Dispose (disposing);
		}

		protected override bool ShortcutHandler(Shortcut shortcut, bool execute_focused)
		{
			if ((base.ShortcutHandler (shortcut, execute_focused) == false) &&
				(this.window != null))
			{
				//	Le raccourci clavier n'a pas �t� consomm�. Il faut voir si le raccourci clavier
				//	est attach� � une commande globale.
				
				//	TODO: g�re les commandes globales
				
				Widget focused = this.window.FocusedWidget;
				
				if (focused == null)
				{
					return false;
				}
				
				TabNavigationMode mode = TabNavigationMode.Passive;
				TabNavigationDir  dir  = TabNavigationDir.None;
				
				switch (shortcut.KeyCode)
				{
					case KeyCode.Tab:
						mode = TabNavigationMode.ActivateOnTab;
						dir  = Message.State.IsShiftPressed ? TabNavigationDir.Backwards : TabNavigationDir.Forwards;
						break;
				}
				
				if ((mode != TabNavigationMode.Passive) &&
					(dir != TabNavigationDir.None))
				{
					//	Navigue dans la hi�rarchie...
					
					Widget find = focused.FindTabWidget (dir, mode);
					
					if (find != null)
					{
						if (find != focused)
						{
							Widget focus;
							
							if (focused.AboutToLoseFocus (dir, mode) &&
								find.AboutToGetFocus (dir, mode, out focus))
							{
								focus.SetFocused (true);
							}
						}
					}
				}
				
				return false;
			}
			
			return true;
		}
		
		protected override void OnTextChanged()
		{
			base.OnTextChanged ();
			
			if (this.window != null)
			{
				this.window.Text = this.Text;
			}
		}
		
		protected override void OnNameChanged()
		{
			base.OnNameChanged ();
			
			if (this.window != null)
			{
				this.window.Name = this.Name;
			}
		}
		
		protected override void PaintBackgroundImplementation(Drawing.Graphics graphics, Drawing.Rectangle clip_rect)
		{
			if (this.is_ready == false)
			{
				return;
			}
			
			double dx = this.Client.Width;
			double dy = this.Client.Height;
			
			double x1 = System.Math.Max (clip_rect.Left, 0);
			double y1 = System.Math.Max (clip_rect.Bottom, 0);
			double x2 = System.Math.Min (clip_rect.Right, dx);
			double y2 = System.Math.Min (clip_rect.Top, dy);
			
			if (this.BackColor.IsValid)
			{
				if (this.BackColor.A != 1.0)
				{
					graphics.Pixmap.Erase (new System.Drawing.Rectangle ((int) x1, (int) y1, (int) x2 - (int) x1, (int) y2 - (int) y1));
				}
				if (this.BackColor.A > 0.0)
				{
					graphics.SolidRenderer.Color = this.BackColor;
					graphics.AddFilledRectangle (x1, y1, x2-x1, y2-y1);
					graphics.RenderSolid ();
				}
			}
			else
			{
				IAdorner adorner = Widgets.Adorner.Factory.Active;
				Drawing.Rectangle rect = new Drawing.Rectangle(x1, y1, x2-x1, y2-y1);
				adorner.PaintWindowBackground(graphics, this.Client.Bounds, rect, WidgetState.None);
			}
		}
		
		
		internal void NotifyAdornerChanged()
		{
			this.HandleAdornerChanged ();
		}
		
		
		protected Window							window;
		protected bool								is_ready;
	}
}

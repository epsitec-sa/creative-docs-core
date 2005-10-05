//	Copyright � 2005, EPSITEC SA, CH-1092 BELMONT, Switzerland
//	Responsable: Pierre ARNAUD

using Epsitec.Common.Types;
using Epsitec.Common.Widgets.Helpers;

namespace Epsitec.Common.Widgets
{
	/// <summary>
	/// Visual.
	/// </summary>
	public class Visual : Types.Object
	{
		public Visual()
		{
		}
		
		
		public string							Name
		{
			get
			{
				return (string) this.GetValue (Visual.NameProperty);
			}
			set
			{
				this.SetValue (Visual.NameProperty, value);
			}
		}
		
		public Visual							Parent
		{
			get
			{
				if (this.parent_layer == null)
				{
					return null;
				}
				else
				{
					return this.parent_layer.Visual;
				}
			}
		}
		
		public Layouts.Layer					ParentLayer
		{
			get
			{
				return this.parent_layer;
			}
		}
		
		
		public AnchorStyles						Anchor
		{
			get
			{
				return (AnchorStyles) this.GetValue (Visual.AnchorProperty);
			}
			set
			{
				this.SetValue (Visual.AnchorProperty, value);
			}
		}
		
		public Drawing.Margins					AnchorMargins
		{
			get
			{
				return (Drawing.Margins) this.GetValue (Visual.AnchorMarginsProperty);
			}
			set
			{
				this.SetValue (Visual.AnchorMarginsProperty, value);
			}
		}
		
		public DockStyle						Dock
		{
			get
			{
				return (DockStyle) this.GetValue (Visual.DockProperty);
			}
			set
			{
				this.SetValue (Visual.DockProperty, value);
			}
		}
		
		public Drawing.Margins					DockPadding
		{
			get
			{
				return (Drawing.Margins) this.GetValue (Visual.DockPaddingProperty);
			}
			set
			{
				this.SetValue (Visual.DockPaddingProperty, value);
			}
		}
		
		public Drawing.Margins					DockMargins
		{
			get
			{
				return (Drawing.Margins) this.GetValue (Visual.DockMarginsProperty);
			}
			set
			{
				this.SetValue (Visual.DockMarginsProperty, value);
			}
		}
		
		public ContainerLayoutMode				ContainerLayoutMode
		{
			get
			{
				return (ContainerLayoutMode) this.GetValue (Visual.ContainerLayoutModeProperty);
			}
			set
			{
				this.SetValue (Visual.ContainerLayoutModeProperty, value);
			}
		}
		
		
		public Drawing.Rectangle				Bounds
		{
			get
			{
				return Drawing.Rectangle.FromCorners (this.x1, this.y1, this.x2, this.y2);
			}
			set
			{
				if ((this.x1 != value.Left) ||
					(this.x2 != value.Right) ||
					(this.y1 != value.Bottom) ||
					(this.y2 != value.Top))
				{
					Visual parent = this.Parent;
					
					this.SuspendLayout ();
					
					if (parent == null)
					{
						this.PreferredSize = value.Size;
						
						this.SetBounds (value);
					}
					else
					{
						Drawing.Size host = parent.Client.Size;
						
						this.PreferredSize = value.Size;
						this.AnchorMargins = new Drawing.Margins (value.Left, host.Width - value.Right, host.Height - value.Top, value.Bottom);
					}
					
					this.ResumeLayout ();
				}
			}
		}
		
		public Drawing.Size						PreferredSize
		{
			get
			{
				return new Drawing.Size (this.preferred_width, this.preferred_height);
			}
			set
			{
				if ((this.preferred_width != value.Width) ||
					(this.preferred_height != value.Height))
				{
					this.SuspendLayout ();
					
					Drawing.Size old_size = this.PreferredSize;
					Drawing.Size new_size = value;
					
					this.preferred_width  = value.Width;
					this.preferred_height = value.Height;
					
					this.InvalidateProperty (Visual.PreferredSizeProperty, old_size, new_size);
					
					this.ResumeLayout ();
				}
			}
		}
		
		public Widget.ClientInfo				Client
		{
			get
			{
				return new Widget.ClientInfo (this.x2 - this.x1, this.y2 - this.y1);
			}
		}
		
		
		public virtual Drawing.Margins			InternalPadding
		{
			get
			{
				return Drawing.Margins.Zero;
			}
		}
		
		
		public double							Left
		{
			get
			{
				return this.Bounds.Left;
			}
			set
			{
				Drawing.Rectangle bounds = this.Bounds;
				
				bounds.Left = value;
				
				this.Bounds = bounds;
			}
		}
		
		public double							Right
		{
			get
			{
				return this.Bounds.Right;
			}
			set
			{
				Drawing.Rectangle bounds = this.Bounds;
				
				bounds.Right = value;
				
				this.Bounds = bounds;
			}
		}
		
		public double							Top
		{
			get
			{
				return this.Bounds.Top;
			}
			set
			{
				Drawing.Rectangle bounds = this.Bounds;
				
				bounds.Top = value;
				
				this.Bounds = bounds;
			}
		}
		
		public double							Bottom
		{
			get
			{
				return this.Bounds.Bottom;
			}
			set
			{
				Drawing.Rectangle bounds = this.Bounds;
				
				bounds.Bottom = value;
				
				this.Bounds = bounds;
			}
		}
		
		public double							Width
		{
			get
			{
				return this.Bounds.Width;
			}
			set
			{
				Drawing.Rectangle bounds = this.Bounds;
				
				bounds.Width = value;
				
				this.Bounds = bounds;
			}
		}
		
		public double							Height
		{
			get
			{
				return this.Bounds.Height;
			}
			set
			{
				Drawing.Rectangle bounds = this.Bounds;
				
				bounds.Height = value;
				
				this.Bounds = bounds;
			}
		}
		
		public Drawing.Size						Size
		{
			get
			{
				return this.Bounds.Size;
			}
			set
			{
				Drawing.Rectangle bounds = this.Bounds;
				
				bounds.Size = value;
				
				this.Bounds = bounds;
			}
		}
		
		public Drawing.Point					Location
		{
			get
			{
				return this.Bounds.Location;
			}
			set
			{
				Drawing.Rectangle bounds = this.Bounds;
				
				bounds.Location = value;
				
				this.Bounds = bounds;
			}
		}
		
		
		public Drawing.Size						MinSize
		{
			get
			{
				return (Drawing.Size) this.GetValue (Visual.MinSizeProperty);
			}
			set
			{
				this.SetValue (Visual.MinSizeProperty, value);
			}
		}
		
		public Drawing.Size						MaxSize
		{
			get
			{
				return (Drawing.Size) this.GetValue (Visual.MaxSizeProperty);
			}
			set
			{
				this.SetValue (Visual.MaxSizeProperty, value);
			}
		}
		
		
		public Drawing.Size						ResultingMinSize
		{
			get
			{
				//	TODO: tenir compte de la taille des enfants
				return this.MinSize;
			}
		}
		
		public Drawing.Size						ResultingMaxSize
		{
			get
			{
				//	TODO: tenir compte de la taille des enfants
				return this.MaxSize;
			}
		}
		
		
		public bool								Visibility
		{
			get
			{
				return (bool) this.GetValue (Visual.VisibilityProperty);
			}
			set
			{
				this.SetValue (Visual.VisibilityProperty, value);
			}
		}
		
		public bool								IsVisible
		{
			get
			{
				return VisualTree.IsVisible (this);
			}
		}
		
		
		public bool								AutoCapture
		{
			get
			{
				return (bool) this.GetValue (Visual.AutoCaptureProperty);
			}
			set
			{
				this.SetValue (Visual.AutoCaptureProperty, value);
			}
		}
		
		public bool								AutoFocus
		{
			get
			{
				return (bool) this.GetValue (Visual.AutoFocusProperty);
			}
			set
			{
				this.SetValue (Visual.AutoFocusProperty, value);
			}
		}
		
		public bool								AutoEngage
		{
			get
			{
				return (bool) this.GetValue (Visual.AutoEngageProperty);
			}
			set
			{
				this.SetValue (Visual.AutoEngageProperty, value);
			}
		}
		
		public bool								AutoRepeat
		{
			get
			{
				return (bool) this.GetValue (Visual.AutoRepeatProperty);
			}
			set
			{
				this.SetValue (Visual.AutoRepeatProperty, value);
			}
		}
		
		public bool								AutoToggle
		{
			get
			{
				return (bool) this.GetValue (Visual.AutoToggleProperty);
			}
			set
			{
				this.SetValue (Visual.AutoToggleProperty, value);
			}
		}
		
		public bool								AutoRadio
		{
			get
			{
				return (bool) this.GetValue (Visual.AutoRadioProperty);
			}
			set
			{
				this.SetValue (Visual.AutoRadioProperty, value);
			}
		}
		
		
		
		internal bool							HasLayerCollection
		{
			get
			{
				return this.layer_collection == null ? false : true;
			}
		}
		
		
		internal void SetParentLayer(Layouts.Layer parent_layer)
		{
			this.parent_layer = parent_layer;
		}
		
		internal void SetBounds(Drawing.Rectangle bounds)
		{
			this.x1 = bounds.Left;
			this.x2 = bounds.Right;
			this.y1 = bounds.Bottom;
			this.y2 = bounds.Top;
		}
		
		internal Collections.LayerCollection GetLayerCollection()
		{
			if (this.layer_collection == null)
			{
				lock (this)
				{
					if (this.layer_collection == null)
					{
						this.layer_collection = new Collections.LayerCollection (this);
					}
				}
			}
			
			return this.layer_collection;
		}
		
		
		internal void NotifyChildrenChanged(Layouts.Layer layer)
		{
		}
		
		internal void NotifyLayoutChanged()
		{
		}
		
		internal void NotifyParentLayoutChanged()
		{
			Visual parent = this.Parent;
			
			if (parent != null)
			{
				parent.NotifyLayoutChanged ();
			}
		}
		
		public void SuspendLayout()
		{
			this.suspend_layout_counter++;
		}
		
		public void ResumeLayout()
		{
			this.suspend_layout_counter--;
		}
		
		static Visual()
		{
		}
		
		private static object GetParentValue(Object o)
		{
			Visual that = o as Visual;
			return that.Parent;
		}
		
		private static object GetParentLayerValue(Object o)
		{
			Visual that = o as Visual;
			return that.ParentLayer;
		}
		
		private static object GetBoundsValue(Object o)
		{
			Visual that = o as Visual;
			return that.Bounds;
		}
		
		private static void SetBoundsValue(Object o, object value)
		{
			Visual that = o as Visual;
			that.Bounds = (Drawing.Rectangle) value;
		}
		
		private static object GetPreferredSizeValue(Object o)
		{
			Visual that = o as Visual;
			return that.PreferredSize;
		}
		
		private static void SetPreferredSizeValue(Object o, object value)
		{
			Visual that = o as Visual;
			that.PreferredSize = (Drawing.Size) value;
		}
		
		
		public static readonly Property NameProperty				= Property.Register ("Name", typeof (string), typeof (Visual));
		public static readonly Property ParentProperty				= Property.RegisterReadOnly ("Parent", typeof (Visual), typeof (Visual), new PropertyMetadata (new GetValueOverrideCallback (Visual.GetParentValue)));
		public static readonly Property ParentLayerProperty			= Property.RegisterReadOnly ("ParentLayer", typeof (Layouts.Layer), typeof (Visual), new PropertyMetadata (new GetValueOverrideCallback (Visual.GetParentLayerValue)));
		
		public static readonly Property AnchorProperty				= Property.Register ("Anchor", typeof (AnchorStyles), typeof (Visual), new VisualPropertyMetadata (AnchorStyles.None, VisualPropertyFlags.AffectsParentLayout));
		public static readonly Property AnchorMarginsProperty		= Property.Register ("AnchorMargins", typeof (Drawing.Margins), typeof (Visual), new VisualPropertyMetadata (Drawing.Margins.Zero, VisualPropertyFlags.AffectsParentLayout));
		public static readonly Property DockProperty				= Property.Register ("Dock", typeof (DockStyle), typeof (Visual), new VisualPropertyMetadata (DockStyle.None, VisualPropertyFlags.AffectsParentLayout));
		public static readonly Property DockPaddingProperty			= Property.Register ("DockPadding", typeof (Drawing.Margins), typeof (Visual), new VisualPropertyMetadata (Drawing.Margins.Zero, VisualPropertyFlags.AffectsParentLayout));
		public static readonly Property DockMarginsProperty			= Property.Register ("DockMargins", typeof (Drawing.Margins), typeof (Visual), new VisualPropertyMetadata (Drawing.Margins.Zero, VisualPropertyFlags.AffectsParentLayout));
		public static readonly Property ContainerLayoutModeProperty	= Property.Register ("ContainerLayoutMode", typeof (ContainerLayoutMode), typeof (Visual), new VisualPropertyMetadata (ContainerLayoutMode.VerticalFlow, VisualPropertyFlags.AffectsLayout));
		
		public static readonly Property BoundsProperty				= Property.Register ("Bounds", typeof (Drawing.Rectangle), typeof (Visual), new PropertyMetadata (new GetValueOverrideCallback (Visual.GetBoundsValue), new SetValueOverrideCallback (Visual.SetBoundsValue)));
		public static readonly Property PreferredSizeProperty		= Property.Register ("PreferredSize", typeof (Drawing.Size), typeof (Visual), new PropertyMetadata (new GetValueOverrideCallback (Visual.GetPreferredSizeValue), new SetValueOverrideCallback (Visual.SetPreferredSizeValue)));
		public static readonly Property MinSizeProperty				= Property.Register ("MinSize", typeof (Drawing.Size), typeof (Visual), new VisualPropertyMetadata (Drawing.Size.Empty, VisualPropertyFlags.AffectsParentLayout));
		public static readonly Property MaxSizeProperty				= Property.Register ("MaxSize", typeof (Drawing.Size), typeof (Visual), new VisualPropertyMetadata (Drawing.Size.Infinite, VisualPropertyFlags.AffectsParentLayout));
		
		public static readonly Property VisibilityProperty			= Property.Register ("Visibility", typeof (bool), typeof (Visual), new VisualPropertyMetadata (true, VisualPropertyFlags.AffectsParentLayout));
		
		public static readonly Property AutoCaptureProperty			= Property.Register ("AutoCapture", typeof (bool), typeof (Visual), new PropertyMetadata (true));
		public static readonly Property AutoFocusProperty			= Property.Register ("AutoFocus", typeof (bool), typeof (Visual), new PropertyMetadata (false));
		public static readonly Property AutoEngageProperty			= Property.Register ("AutoEngage", typeof (bool), typeof (Visual), new PropertyMetadata (false));
		public static readonly Property AutoRepeatProperty			= Property.Register ("AutoRepeat", typeof (bool), typeof (Visual), new PropertyMetadata (false));
		public static readonly Property AutoToggleProperty			= Property.Register ("AutoToggle", typeof (bool), typeof (Visual), new PropertyMetadata (false));
		public static readonly Property AutoRadioProperty			= Property.Register ("AutoRadio", typeof (bool), typeof (Visual), new PropertyMetadata (false));
		
		
		public static readonly Property ChildrenProperty = Property.Register ("Children", typeof (Collections.VisualCollection), typeof (Visual));
		
		
		private short							suspend_layout_counter;
		
		private double							x1, y1, x2, y2;
		private double							preferred_width, preferred_height;
		private Collections.LayerCollection		layer_collection;
		private Layouts.Layer					parent_layer;
	}
}

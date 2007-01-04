//	Copyright © 2006-2007, EPSITEC SA, CH-1092 BELMONT, Switzerland
//	Responsable: Pierre ARNAUD

using Epsitec.Common.Types;
using Epsitec.Common.UI;
using Epsitec.Common.Widgets;

using System.Collections.Generic;

[assembly: DependencyClass (typeof (Placeholder))]

namespace Epsitec.Common.UI
{
	/// <summary>
	/// La classe Placeholder représente un conteneur utilisé par des widgets
	/// intelligents, remplis par data binding.
	/// </summary>
	public class Placeholder : AbstractPlaceholder, Widgets.Layouts.IGridPermeable
	{
		public Placeholder()
		{
			Application.QueueAsyncCallback (this.CreateUserInterface);
		}
		
		public Placeholder(Widget embedder) : this ()
		{
			this.SetEmbedder (embedder);
		}


		public MiniPanel						EditPanel
		{
			get
			{
				if (this.editPanel == null)
				{
					this.editPanel = new MiniPanel ();
				}
				
				return this.editPanel;
			}
		}

		public string							Controller
		{
			get
			{
				return (string) this.GetValue (Placeholder.ControllerProperty);
			}
			set
			{
				this.SetValue (Placeholder.ControllerProperty, value);
			}
		}

		public string							ControllerParameter
		{
			get
			{
				return (string) this.GetValue (Placeholder.ControllerParameterProperty);
			}
			set
			{
				this.SetValue (Placeholder.ControllerParameterProperty, value);
			}
		}

		public Widgets.Layouts.IGridPermeable	ControllerIGridPermeable
		{
			get
			{
				Widgets.Layouts.IGridPermeable helper = null;

				if ((this.controller == null) &&
					(this.controllerName != null))
				{
					IController temp = Controllers.Factory.CreateController (this.controllerName, this.controllerParameter);

					if (temp != null)
					{
						helper = temp.GetGridPermeableLayoutHelper ();
					}
				}
				else if (this.controller != null)
				{
					helper = this.controller.GetGridPermeableLayoutHelper ();
				}
				if (helper == null)
				{
					helper = Placeholder.noOpGridPermeableHelper;
				}

				return helper;
			}
		}

		public Verbosity						Verbosity
		{
			get
			{
				return (Verbosity) this.GetValue (Placeholder.VerbosityProperty);
			}
			set
			{
				this.SetValue (Placeholder.VerbosityProperty, value);
			}
		}

		public bool								IsReadOnlyValueBinding
		{
			get
			{
				Binding binding = this.ValueBinding;
				BindingMode mode = binding == null ? BindingMode.None : binding.Mode;

				if ((mode == BindingMode.OneTime) ||
					(mode == BindingMode.OneWay))
				{
					return true;
				}
				else
				{
					return false;
				}
			}
		}

		public void SimulateEdition()
		{
			object value = this.Value;
			this.InvalidateProperty (Placeholder.ValueProperty, value, value);
		}

		public bool GetMinSpan(Widget parent, int column, int row, out int minColumnSpan, out int minRowSpan)
		{
			minColumnSpan = 0;
			minRowSpan = 0;
			
			if (parent == null)
			{
				return false;
			}
			if (this.ValueBinding == null)
			{
				return false;
			}
			if (this.Controller == null)
			{
				return false;
			}

			Widget savedParent = this.Parent;
			object savedValue = this.Value;

			this.SetParent (parent);
			this.SyncQueuedCalls ();

			if ((this.Value != UndefinedValue.Instance) &&
				(this.Value != UnknownValue.Instance) &&
				(this.Value != null))
			{
				minColumnSpan = 1;
				minRowSpan = 1;

				this.ControllerIGridPermeable.UpdateGridSpan (ref minColumnSpan, ref minRowSpan);
			}

			this.SetParent (null);
			this.Value = savedValue;
			this.SetParent (savedParent);
			
			return (minColumnSpan > 0) && (minRowSpan > 0);
		}

		public static void SimulateEdition(Widget root)
		{
			foreach (Placeholder placeholder in root.FindAllChildren (delegate (Widget widget) { return widget is Placeholder; }))
			{
				placeholder.SimulateEdition ();
			}
		}
		
		protected override void LayoutArrange(Widgets.Layouts.ILayoutEngine engine)
		{
			Widget parent = this.Parent;
			
			if ((parent != null) &&
				(Widgets.Layouts.GridLayoutEngine.GetColumn (this) >= 0) &&
				(Widgets.Layouts.GridLayoutEngine.GetRow (this) >= 0))
			{
				Widgets.Layouts.ILayoutEngine parentEngine = Widgets.Layouts.LayoutEngine.GetLayoutEngine (parent);

				if (parentEngine is Widgets.Layouts.GridLayoutEngine)
				{
					//	This placeholder is in a grid. No need to arrange the children;
					//	they get arranged by the grid itself !
					
					return;
				}
			}
			
			base.LayoutArrange (engine);
		}

		protected override void OnValueBindingChanged()
		{
			this.UpdateController ();
			base.OnValueBindingChanged ();
		}

		protected override void OnBoundsChanged(Drawing.Rectangle oldValue, Drawing.Rectangle newValue)
		{
			base.OnBoundsChanged (oldValue, newValue);

			if (this.editPanel != null)
			{
				PanelStack panelStack = PanelStack.GetPanelStack (this);

				this.editPanel.PanelStack = panelStack;
				this.editPanel.Aperture = Widgets.Helpers.VisualTree.MapVisualToAncestor (this, panelStack, this.Client.Bounds);
			}
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.DisposeUserInterface ();
			}

			base.Dispose (disposing);
		}

		private void UpdateController()
		{
			string oldControllerName = this.controllerName;
			string newControllerName = null;
			string oldControllerParameter = this.controllerParameter;
			string newControllerParameter = null;
			
			if (this.Controller == "*")
			{
				BindingExpression expression = this.ValueBindingExpression;
				
				if (expression != null)
				{
					Controllers.Factory.GetDefaultController (expression, out newControllerName, out newControllerParameter);
				}
			}
			else
			{
				newControllerName      = this.Controller;
				newControllerParameter = this.ControllerParameter;
			}
			
			if ((newControllerName != oldControllerName) ||
				(newControllerParameter != oldControllerParameter))
			{
				this.controllerName      = newControllerName;
				this.controllerParameter = newControllerParameter;
				
				if (this.controller == null)
				{
					if (this.controllerName != null)
					{
						Application.QueueAsyncCallback (this.CreateUserInterface);
					}
				}
				else
				{
					Application.QueueAsyncCallback (this.RecreateUserInterface);
				}
			}
		}

		private void UpdateVerbosity()
		{
			if (this.controller != null)
			{
				Application.QueueAsyncCallback (this.RecreateUserInterface);
			}
		}

		private void DisposeUserInterface()
		{
			if (this.controller != null)
			{
				this.controller.DisposeUserInterface ();
				this.controller = null;
			}
		}

		private void CreateUserInterface()
		{
			if ((this.controller == null) &&
				(this.controllerName != null))
			{
				this.controller = Controllers.Factory.CreateController (this.controllerName, this.controllerParameter);

				if (this.controller != null)
				{
					this.controller.Placeholder = this;
					this.controller.CreateUserInterface ();

					object value = this.Value;

					if (value != UndefinedValue.Instance)
					{
						this.controller.RefreshUserInterface (UndefinedValue.Instance, value);
					}
				}
			}
		}

		private void RecreateUserInterface()
		{
			if (this.controller != null)
			{
				this.DisposeUserInterface ();
				this.CreateUserInterface ();
			}
		}

		protected override void UpdateValueType(object oldValueType, object newValueType)
		{
			this.UpdateController ();
			base.UpdateValueType (oldValueType, newValueType);
			
			if (this.controller != null)
			{
				Application.QueueAsyncCallback (this.RecreateUserInterface);
			}
		}

		protected override void UpdateValueName(string oldValueName, string newValueName)
		{
			base.UpdateValueName (oldValueName, newValueName);
			
			if (this.controller != null)
			{
				Application.QueueAsyncCallback (this.RecreateUserInterface);
			}
		}

		protected override void UpdateValue(object oldValue, object newValue)
		{
			base.UpdateValue (oldValue, newValue);

			this.SyncQueuedCalls ();

			if (this.controller != null)
			{
				this.controller.RefreshUserInterface (oldValue, newValue);
			}
		}

		private void SyncQueuedCalls()
		{
			if (Application.HasQueuedAsyncCallback (this.CreateUserInterface))
			{
				Application.RemoveQueuedAsyncCallback (this.CreateUserInterface);
				this.CreateUserInterface ();
			}

			if (Application.HasQueuedAsyncCallback (this.RecreateUserInterface))
			{
				Application.RemoveQueuedAsyncCallback (this.RecreateUserInterface);
				this.RecreateUserInterface ();
			}
		}

		#region IGridPermeable Members

		IEnumerable<Widgets.Layouts.PermeableCell> Widgets.Layouts.IGridPermeable.GetChildren(int column, int row, int columnSpan, int rowSpan)
		{
			return this.ControllerIGridPermeable.GetChildren (column, row, columnSpan, rowSpan);
		}

		bool Widgets.Layouts.IGridPermeable.UpdateGridSpan(ref int columnSpan, ref int rowSpan)
		{
			return this.ControllerIGridPermeable.UpdateGridSpan (ref columnSpan, ref rowSpan);
		}

		#endregion

		#region NoOpGridPermeableHelper Class

		private class NoOpGridPermeableHelper : Widgets.Layouts.IGridPermeable
		{
			#region IGridPermeable Members

			public IEnumerable<Widgets.Layouts.PermeableCell> GetChildren(int column, int row, int columnSpan, int rowSpan)
			{
				yield break;
			}

			public bool UpdateGridSpan(ref int columnSpan, ref int rowSpan)
			{
				return false;
			}

			#endregion
		}
		
		#endregion

		static Placeholder()
		{
			Controllers.Factory.Setup ();
		}

		private static void NotifyControllerChanged(DependencyObject o, object oldValue, object newValue)
		{
			Placeholder that = (Placeholder) o;

			that.UpdateController ();
		}

		private static void NotifyVerbosityChanged(DependencyObject o, object oldValue, object newValue)
		{
			Placeholder that = (Placeholder) o;

			that.UpdateVerbosity ();
		}

		public static readonly DependencyProperty ControllerProperty = DependencyProperty.Register ("Controller", typeof (string), typeof (Placeholder), new DependencyPropertyMetadata ("*", Placeholder.NotifyControllerChanged));
		public static readonly DependencyProperty ControllerParameterProperty = DependencyProperty.Register ("ControllerParameter", typeof (string), typeof (Placeholder), new DependencyPropertyMetadata (Placeholder.NotifyControllerChanged));
		public static readonly DependencyProperty VerbosityProperty = DependencyProperty.Register ("Verbosity", typeof (Verbosity), typeof (Placeholder), new DependencyPropertyMetadata (Verbosity.Default, Placeholder.NotifyVerbosityChanged));

		static readonly NoOpGridPermeableHelper	noOpGridPermeableHelper = new NoOpGridPermeableHelper ();
		
		private IController						controller;
		private string							controllerName;
		private string							controllerParameter;
		private MiniPanel						editPanel;
	}
}
     
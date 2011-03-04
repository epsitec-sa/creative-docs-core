//	Copyright � 2011, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Pierre ARNAUD, Maintainer: Pierre ARNAUD

using System.Collections.Generic;

namespace Epsitec.Cresus.Core.Library
{
	public abstract class CoreApp : Epsitec.Common.Widgets.Application, ICoreComponentHost<CoreAppComponent>, ICoreComponentHost<ICoreManualComponent>
	{
		protected CoreApp()
		{
			this.components = new CoreComponentHostImplementation<CoreAppComponent> ();
			this.manualComponents = new CoreComponentHostImplementation<ICoreManualComponent> ();
			
			Factories.CoreAppComponentFactory.RegisterComponents (this);
		}


		public virtual void SetupApplication()
		{
			Factories.CoreAppComponentFactory.SetupComponents (this.components.GetComponents ());
		}

		public virtual T FindComponent<T>()
			where T : class, ICoreComponent
		{
			ICoreComponentHost<CoreAppComponent> host = this;

			if (host.ContainsComponent (typeof (T)))
			{
				return host.GetComponent (typeof (T)) as T;
			}

			if (this.manualComponents.ContainsComponent (typeof (T)))
			{
				return this.manualComponents.GetComponent (typeof (T)) as T;
			}

			return null;
		}

		#region ICoreComponentHost<CoreAppComponent> Members

		public bool ContainsComponent<T>() where T : CoreAppComponent
		{
			return this.components.ContainsComponent<T> ();
		}

		public T GetComponent<T>() where T : CoreAppComponent
		{
			return this.components.GetComponent<T> ();
		}

		IEnumerable<CoreAppComponent> ICoreComponentHost<CoreAppComponent>.GetComponents()
		{
			return this.components.GetComponents ();
		}

		CoreAppComponent ICoreComponentHost<CoreAppComponent>.GetComponent(System.Type type)
		{
			return this.components.GetComponent (type);
		}

		bool ICoreComponentHost<CoreAppComponent>.ContainsComponent(System.Type type)
		{
			return this.components.ContainsComponent (type);
		}

		void ICoreComponentHost<CoreAppComponent>.RegisterComponent<T>(T component)
		{
			this.components.RegisterComponent<T> (component);
		}

		void ICoreComponentHost<CoreAppComponent>.RegisterComponent(System.Type type, CoreAppComponent component)
		{
			this.components.RegisterComponent (type, component);
		}

		void ICoreComponentHost<CoreAppComponent>.RegisterComponentAsDisposable(System.IDisposable component)
		{
			this.components.RegisterComponentAsDisposable (component);
		}

		#endregion

		#region ICoreComponentHost<ICoreComponent> Members

		T ICoreComponentHost<ICoreManualComponent>.GetComponent<T>()
		{
			return this.manualComponents.GetComponent<T> ();
		}

		ICoreManualComponent ICoreComponentHost<ICoreManualComponent>.GetComponent(System.Type type)
		{
			return this.manualComponents.GetComponent (type);
		}

		IEnumerable<ICoreManualComponent> ICoreComponentHost<ICoreManualComponent>.GetComponents()
		{
			return this.manualComponents.GetComponents ();
		}

		bool ICoreComponentHost<ICoreManualComponent>.ContainsComponent<T>()
		{
			return this.manualComponents.ContainsComponent<T> ();
		}

		bool ICoreComponentHost<ICoreManualComponent>.ContainsComponent(System.Type type)
		{
			return this.manualComponents.ContainsComponent (type);
		}

		void ICoreComponentHost<ICoreManualComponent>.RegisterComponent(System.Type type, ICoreManualComponent component)
		{
			this.manualComponents.RegisterComponent (type, component);
		}

		public void RegisterComponent<T>(T component)
			where T : ICoreManualComponent
		{
			this.manualComponents.RegisterComponent<T> (component);
		}

		void ICoreComponentHost<ICoreManualComponent>.RegisterComponentAsDisposable(System.IDisposable disposable)
		{
			this.manualComponents.RegisterComponentAsDisposable (disposable);
		}


		#endregion

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.components.Dispose ();
			}

			base.Dispose (disposing);
		}

		private readonly CoreComponentHostImplementation<CoreAppComponent> components;
		private readonly CoreComponentHostImplementation<ICoreManualComponent> manualComponents;
	}
}

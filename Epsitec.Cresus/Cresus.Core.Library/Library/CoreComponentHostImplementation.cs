﻿//	Copyright © 2011-2013, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Pierre ARNAUD, Maintainer: Pierre ARNAUD

using Epsitec.Common.Support.Extensions;

using System.Collections.Generic;
using System.Linq;

namespace Epsitec.Cresus.Core.Library
{
	/// <summary>
	/// The <c>CoreComponentHostImplementation</c> class provides a standard implementation
	/// of <see cref="ICoreComponentHost"/>.
	/// </summary>
	/// <typeparam name="TComponent">The base type of all the <see cref="ICoreComponent"/> derived components.</typeparam>
	public sealed class CoreComponentHostImplementation<TComponent> : ICoreComponentHost<TComponent>, System.IDisposable
		where TComponent : class, ICoreComponent
	{
		public CoreComponentHostImplementation()
		{
			this.components = new Dictionary<string, TComponent> ();
			this.registeredComponents = new List<TComponent> ();
			this.disposableComponents = new Stack<System.IDisposable> ();
			this.activeComponents = new List<TComponent> ();
		}

		public void ActivateComponent(TComponent component)
		{
			System.Diagnostics.Debug.Assert (this.registeredComponents.Contains (component));
			System.Diagnostics.Debug.Assert (this.components.ContainsKey (component.GetType ().FullName));

			this.components[component.GetType ().FullName] = component;

			this.activeComponents.Remove (component);
			this.activeComponents.Insert (0, component);
		}

		public T FindActiveComponent<T>()
			where T : TComponent
		{
			return this.activeComponents.OfType<T> ().FirstOrDefault ();
		}
		
		public void RegisterComponents(IEnumerable<TComponent> components)
		{
			foreach (var component in components)
			{
				this.RegisterComponent (component.GetType (), component);
			}
		}

		#region ICoreComponentHost<TComponent> Members

		public T GetComponent<T>()
			where T : TComponent
		{
			return (T) this.GetComponent (typeof (T));
		}

		public TComponent GetComponent(System.Type type)
		{
			TComponent component;

			if (this.components.TryGetValue (type.FullName, out component))
			{
				return component;
			}

			if ((type.IsInterface) ||
				(type.IsClass))
			{
				//	We are requesting a component which implements the specified interface or
				//	derives from a base class; search for it sequentially :

				component = this.registeredComponents.Where (x => type.IsAssignableFrom (x.GetType ())).FirstOrDefault ();

				if (component != null)
				{
					return component;
				}
			}


			throw new System.ArgumentException (string.Format ("The specified component {0} does not exist", type.FullName));
		}

		public IEnumerable<TComponent> GetComponents()
		{
			return this.registeredComponents;
		}

		public bool ContainsComponent<T>()
			where T : TComponent
		{
			return this.ContainsComponent (typeof (T));
		}

		public void RegisterComponent<T>(T component)
			where T : TComponent
		{
			this.RegisterComponent (typeof (T), component);
		}

		public bool ContainsComponent(System.Type type)
		{
			if (this.components.ContainsKey (type.FullName))
			{
				return true;
			}
			
			if ((type.IsInterface) ||
				(type.IsClass))
			{
				//	We are requesting a component which implements the specified interface or
				//	derives from a base class; search for it sequentially :

				return this.registeredComponents.Any (x => type.IsAssignableFrom (x.GetType ()));
			}

			return false;
		}

		public void RegisterComponent(System.Type type, TComponent component)
		{
			type.ThrowIfNull ("type");
			component.ThrowIfNull ("component", string.Format ("No component instance for type {0}", type.FullName));

			this.registeredComponents.Add (component);
			this.components[type.FullName] = component;
		}

		public void RegisterComponentAsDisposable(System.IDisposable component)
		{
			component.ThrowIfNull ("component", "No component instance with IDisposable interface");
			
			this.disposableComponents.Push (component);
		}

		#endregion

		#region IDisposable Members

		public void Dispose()
		{
			while (this.disposableComponents.Count > 0)
			{
				this.disposableComponents.Pop ().Dispose ();
			}
		}

		#endregion
		
		
		private readonly Dictionary<string, TComponent>	components;
		private readonly List<TComponent>				registeredComponents;
		private readonly Stack<System.IDisposable>		disposableComponents;
		private readonly List<TComponent>				activeComponents;
	}
}

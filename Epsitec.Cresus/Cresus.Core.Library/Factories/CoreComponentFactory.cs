//	Copyright � 2011, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Pierre ARNAUD, Maintainer: Pierre ARNAUD

using Epsitec.Cresus.Core.Library;
using Epsitec.Cresus.Core.Resolvers;

using System.Collections.Generic;
using System.Linq;

namespace Epsitec.Cresus.Core.Factories
{
	/// <summary>
	/// The <c>CoreComponentFactory</c> class provides methods to register and setup
	/// components.
	/// </summary>
	/// <typeparam name="THost">The type of the host.</typeparam>
	/// <typeparam name="TFactory">The type of the factory interface (used by the resolver).</typeparam>
	/// <typeparam name="TComponent">The type of the component.</typeparam>
	public abstract class CoreComponentFactory<THost, TFactory, TComponent>
		where THost : class, ICoreComponentHost<TComponent>
		where TFactory : class, ICoreComponentFactory<THost, TComponent>
		where TComponent : CoreComponent<THost, TComponent>, ICoreComponent
	{
		public static void RegisterComponents(THost host)
		{
			var factories = CoreComponentFactoryResolver<TFactory>.Resolve ();

			bool again = true;

			while (again)
			{
				again = false;

				foreach (var factory in factories)
				{
					var type = factory.GetComponentType ();

					if (host.ContainsComponent (type))
					{
						continue;
					}

					if (factory.CanCreate (host))
					{
						host.RegisterComponent (type, factory.Create (host));
						again = true;
					}
				}
			}
		}

		public static void SetupComponents(IEnumerable<TComponent> componentCollection)
		{
			var components = componentCollection.ToList ();

			bool again = true;

			while (again)
			{
				again = false;

				foreach (var component in components)
				{
					if (component.IsSetupPending)
					{
						if (component.CanExecuteSetupPhase ())
						{
							component.ExecuteSetupPhase ();

							CoreComponentFactory<THost, TFactory, TComponent>.RegisterDisposableComponent (component);
							again = true;
						}
					}
				}
			}

			System.Diagnostics.Debug.Assert (components.All (x => x.IsSetupPending == false));
		}

		private static void RegisterDisposableComponent(TComponent component)
		{
			var data = component.Host;
			var disposable = component.GetDisposable ();

			if (disposable != null)
			{
				data.RegisterComponentAsDisposable (disposable);
			}
		}
	}
}

//	Copyright � 2006, EPSITEC SA, CH-1092 BELMONT, Switzerland
//	Responsable: Pierre ARNAUD

using Epsitec.Common.Types;
using System.Collections.Generic;

namespace Epsitec.Common.Widgets.Controllers
{
	using Assembly=System.Reflection.Assembly;

	public static class Factory
	{
		/// <summary>
		/// Creates the controller based on its name and parameter.
		/// </summary>
		/// <param name="name">The controller name.</param>
		/// <param name="parameter">The optional controller parameter.</param>
		/// <returns>An object implementing <see cref="T:IController"/> or
		/// <c>null</c> if the specified controller cannot be found.</returns>
		public static IController CreateController(string name, string parameter)
		{
			Record record;
			
			if (Factory.types.TryGetValue (name, out record))
			{
				return record.CreateController (parameter);
			}
			
			return null;
		}

		public static void Setup()
		{
		}

		#region Setup and Run-Time Analysis Methods

		static Factory()
		{
			Factory.domain     = System.AppDomain.CurrentDomain;
			Factory.assemblies = new List<Assembly> ();
			Factory.types      = new Dictionary<string, Record> ();

			Assembly[] assemblies = Factory.domain.GetAssemblies ();

			Factory.domain.AssemblyLoad += new System.AssemblyLoadEventHandler (Factory.HandleDomainAssemblyLoad);

			foreach (Assembly assembly in assemblies)
			{
				Factory.Analyse (assembly);
			}
		}

		private static void Analyse(Assembly assembly)
		{
			foreach (System.Type type in ControllerAttribute.GetRegisteredTypes (assembly))
			{
				string name = type.Name;
				Record record = new Record (type);
				
				Factory.types[name] = record;
			}
		}

		private static void HandleDomainAssemblyLoad(object sender, System.AssemblyLoadEventArgs args)
		{
			Factory.assemblies.Add (args.LoadedAssembly);
			Factory.Analyse (args.LoadedAssembly);
		}

		#endregion

		#region Private Record Structure

		private struct Record
		{
			public Record(System.Type type)
			{
				this.exclusion = new object ();
				this.allocator = null;
				this.type = type;
			}
			
			public IController CreateController(string parameter)
			{
				if (this.allocator == null)
				{
					lock (this.exclusion)
					{
						if (this.allocator == null)
						{
							this.BuildDynamicAllocator ();
						}
					}
				}

				return this.allocator (parameter);
			}
			
			private void BuildDynamicAllocator()
			{
				//	See DependencyObjectType.BuildDynamicAllocator

				System.Type[] constructorArgumentTypes = new System.Type[] { typeof (string) };

				System.Reflection.Module module = typeof (Factory).Module;
				System.Reflection.Emit.DynamicMethod dynamicMethod = new System.Reflection.Emit.DynamicMethod ("DynamicAllocator", this.type, constructorArgumentTypes, module, true);
				System.Reflection.Emit.ILGenerator ilGen = dynamicMethod.GetILGenerator ();

				ilGen.Emit (System.Reflection.Emit.OpCodes.Nop);
				ilGen.Emit (System.Reflection.Emit.OpCodes.Ldarg_0);
				ilGen.Emit (System.Reflection.Emit.OpCodes.Newobj, this.type.GetConstructor (constructorArgumentTypes));
				ilGen.Emit (System.Reflection.Emit.OpCodes.Ret);

				this.allocator = (Allocator) dynamicMethod.CreateDelegate (typeof (Allocator));
			}
			
			private delegate IController Allocator(string parameter);
			
			private object exclusion;
			private Allocator allocator;
			private System.Type type;
		}

		#endregion

		private static System.AppDomain domain;
		private static List<Assembly> assemblies;
		private static Dictionary<string, Record> types;
	}
}

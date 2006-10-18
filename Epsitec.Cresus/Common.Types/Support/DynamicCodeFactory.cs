//	Copyright � 2006, EPSITEC SA, CH-1092 BELMONT, Switzerland
//	Responsable: Pierre ARNAUD

using System.Collections.Generic;

namespace Epsitec.Common.Support
{
	using OpCodes=System.Reflection.Emit.OpCodes;

	public delegate void PropertySetter(object target, object value);
	public delegate object PropertyGetter(object target);
	
	public delegate T Allocator<T> ();
	public delegate T Allocator<T, P> (P value);

	/// <summary>
	/// The <c>DynamicCodeFactory</c> class generates dynamic methods used to
	/// access properties.
	/// </summary>
	public static class DynamicCodeFactory
	{
		public static PropertySetter CreatePropertySetter(System.Type type, string propertyName)
		{
			return DynamicCodeFactory.CreatePropertySetter (type.GetProperty (propertyName));
		}
		
		public static PropertySetter CreatePropertySetter(System.Reflection.PropertyInfo propertyInfo)
		{
			System.Reflection.MethodInfo method = propertyInfo.GetSetMethod (false);

			if ((method == null) ||
				(propertyInfo.CanWrite == false))
			{
				return null;
			}

			System.Type   hostType  = propertyInfo.DeclaringType;
			System.Type   propType  = propertyInfo.PropertyType;
			System.Type[] arguments = new System.Type[2];
			
			arguments[0] = typeof (object);
			arguments[1] = typeof (object);

			string name = string.Concat ("_PropertySetter_", propertyInfo.Name);

			System.Reflection.Emit.DynamicMethod setter;
			setter = new System.Reflection.Emit.DynamicMethod (name, null, arguments, hostType);

			System.Reflection.Emit.ILGenerator generator = setter.GetILGenerator ();
			
			generator.Emit (OpCodes.Ldarg_0);
			generator.Emit (OpCodes.Castclass, propertyInfo.DeclaringType);
			generator.Emit (OpCodes.Ldarg_1);

			if (propType.IsClass)
			{
				generator.Emit (OpCodes.Castclass, propType);
			}
			else if (propType.IsValueType)
			{
				generator.Emit (OpCodes.Unbox_Any, propType);
			}
			else
			{
				throw new System.InvalidOperationException ("Invalid code path");
			}

			generator.EmitCall (OpCodes.Callvirt, method, null);
			generator.Emit (OpCodes.Ret);
			
			return (PropertySetter) setter.CreateDelegate (typeof (PropertySetter));
		}

		public static PropertyGetter CreatePropertyGetter(System.Type type, string propertyName)
		{
			return DynamicCodeFactory.CreatePropertyGetter (type.GetProperty (propertyName));
		}
		
		public static PropertyGetter CreatePropertyGetter(System.Reflection.PropertyInfo propertyInfo)
		{
			System.Reflection.MethodInfo method = propertyInfo.GetGetMethod ();

			if ((method == null) ||
				(propertyInfo.CanRead == false))
			{
				return null;
			}

			System.Type   hostType  = propertyInfo.DeclaringType;
			System.Type   propType  = propertyInfo.PropertyType;
			System.Type[] arguments = new System.Type[1];
			
			arguments[0] = typeof (object);
			
			string name = string.Concat ("_PropertyGetter_", propertyInfo.Name);

			System.Reflection.Emit.DynamicMethod getter;
			getter = new System.Reflection.Emit.DynamicMethod (name, typeof (object), arguments, hostType);

			System.Reflection.Emit.ILGenerator generator = getter.GetILGenerator ();
			
			generator.Emit (OpCodes.Ldarg_0);
			generator.Emit (OpCodes.Castclass, propertyInfo.DeclaringType);
			generator.EmitCall (OpCodes.Callvirt, method, null);

			if (propType.IsClass)
			{
				//	No conversion needed.
			}
			else if (propType.IsValueType)
			{
				generator.Emit (OpCodes.Box, propType);
			}
			else
			{
				throw new System.InvalidOperationException ("Invalid code path");
			}

			generator.Emit (OpCodes.Ret);

			return (PropertyGetter) getter.CreateDelegate (typeof (PropertyGetter));
		}

		public static Allocator<T> CreateAllocator<T>()
		{
			return DynamicCodeFactory.CreateAllocator<T> (typeof (T));
		}

		public static Allocator<T> CreateAllocator<T>(System.Type type)
		{
			//	Create a small piece of dynamic code which does simply "new T()"
			//	for the underlying system type. This code relies on lightweight
			//	code generation and results in a very fast dynamic allocator.

			string name = string.Concat ("_DynamicAllocator_", type.Name);

			System.Reflection.Module module = type.Module;
			System.Reflection.Emit.DynamicMethod allocator = new System.Reflection.Emit.DynamicMethod (name, type, System.Type.EmptyTypes, module, true);
			System.Reflection.Emit.ILGenerator generator = allocator.GetILGenerator ();
			System.Reflection.ConstructorInfo constructor = type.GetConstructor (System.Type.EmptyTypes);

			if (constructor == null)
			{
				throw new System.InvalidOperationException (string.Format ("Class {0} has no constructor", type.Name));
			}

			generator.Emit (OpCodes.Nop);
			generator.Emit (OpCodes.Newobj, constructor);
			generator.Emit (OpCodes.Ret);

			return (Allocator<T>) allocator.CreateDelegate (typeof (Allocator<T>));
		}

		public static Allocator<T, P> CreateAllocator<T, P>()
		{
			return DynamicCodeFactory.CreateAllocator<T, P> (typeof (T));
		}
		
		public static Allocator<T, P> CreateAllocator<T, P>(System.Type type)
		{
			System.Type[] constructorArgumentTypes = new System.Type[] { typeof (P) };
			
			string name = string.Concat ("_DynamicAllocator_", type.Name);

			System.Reflection.Module module = type.Module;
			System.Reflection.Emit.DynamicMethod allocator = new System.Reflection.Emit.DynamicMethod (name, type, constructorArgumentTypes, module, true);
			System.Reflection.Emit.ILGenerator generator = allocator.GetILGenerator ();
			System.Reflection.ConstructorInfo constructor = type.GetConstructor (constructorArgumentTypes);

			if (constructor == null)
			{
				throw new System.InvalidOperationException (string.Format ("Class {0} has no matching constructor", type.Name));
			}

			generator.Emit (OpCodes.Nop);
			generator.Emit (OpCodes.Ldarg_0);
			generator.Emit (OpCodes.Newobj, constructor);
			generator.Emit (OpCodes.Ret);

			return (Allocator<T, P>) allocator.CreateDelegate (typeof (Allocator<T, P>));
		}
	}
}

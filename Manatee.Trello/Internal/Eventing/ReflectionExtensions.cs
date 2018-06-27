using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Manatee.Trello.Internal.Eventing
{
	internal static class ReflectionExtensions
	{
		public static bool IsAssignableFrom(this Type type, Type otherType)
		{
#if NET45
			return type.IsAssignableFrom(otherType);
#else
			return type.GetTypeInfo().IsAssignableFrom(otherType.GetTypeInfo());
#endif
		}

		public static IEnumerable<Type> GetInterfaces(this Type type)
		{
#if NET45
			return type.GetInterfaces();
#else
			return type.GetTypeInfo().ImplementedInterfaces;
#endif
		}

		public static bool IsGenericType(this Type type)
		{
#if NET45
			return type.IsGenericType;
#else
			return type.GetTypeInfo().IsGenericType;
#endif
		}

		public static IEnumerable<Type> GetGenericArguments(this Type type)
		{
#if NET45
			return type.GetGenericArguments();
#else
			return type.GetTypeInfo().GenericTypeArguments;
#endif
		}

		public static MethodInfo GetMethod(this Type type, string methodName, params Type[] typeParams)
		{
#if NET45
			return type.GetMethod(methodName, typeParams);
#else
			return type.GetTypeInfo().DeclaredMethods
			           .FirstOrDefault(m => m.Name == methodName &&
			                                m.GetParameters()
			                                 .Select(p => p.ParameterType)
			                                 .SequenceEqual(typeParams));
#endif
		}

		public static IEnumerable<ConstructorInfo> GetConstructors(this Type type)
		{
#if NET45
			return type.GetConstructors();
#else
			return type.GetTypeInfo().DeclaredConstructors;
#endif
		}
	}
}
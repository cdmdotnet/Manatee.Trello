using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Manatee.Trello.Internal.Eventing
{
	internal static class ReflectionExtensions
	{
		public static bool IsGenericType(this Type type)
		{
			return type.GetTypeInfo().IsGenericType;
		}

		public static MethodInfo GetMethod(this Type type, string methodName, params Type[] typeParams)
		{
			return type.GetTypeInfo().DeclaredMethods
			           .FirstOrDefault(m => m.Name == methodName &&
			                                m.GetParameters()
			                                 .Select(p => p.ParameterType)
			                                 .SequenceEqual(typeParams));
		}
	}
}
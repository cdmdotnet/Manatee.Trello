/***************************************************************************************

	Copyright 2012 Greg Dennis

	   Licensed under the Apache License, Version 2.0 (the "License");
	   you may not use this file except in compliance with the License.
	   You may obtain a copy of the License at

		 http://www.apache.org/licenses/LICENSE-2.0

	   Unless required by applicable law or agreed to in writing, software
	   distributed under the License is distributed on an "AS IS" BASIS,
	   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
	   See the License for the specific language governing permissions and
	   limitations under the License.
 
	File Name:		RestExecuteHandler.cs
	Namespace:		Manatee.Trello.Internal
	Class Name:		RestExecuteHandler
	Purpose:		Calls Execute() on the IRestProvider implementation and
					caches the MethodInfo for reuse.

***************************************************************************************/

using System;
using System.Collections.Generic;
using System.Reflection;
using Manatee.Trello.Rest;

namespace Manatee.Trello.Internal
{
	internal class RestExecuteHandler : IRestExecuteHandler
	{
		private readonly Dictionary<Type, MethodInfo> _methods;

		public static IRestExecuteHandler Default { get; private set; }

		static RestExecuteHandler()
		{
			Default = new RestExecuteHandler();
		}
		private RestExecuteHandler()
		{
			_methods = new Dictionary<Type, MethodInfo>();
		}

		public IRestResponse Execute(IRestClient client, Type type, IRestRequest request)
		{
			MethodInfo method;
			if (!_methods.ContainsKey(type))
			{
				method = GetMethod(type);
				_methods.Add(type, method);
			}
			else
			{
				method = _methods[type];
			}
			return Execute(client, method, request);
		}

		private static IRestResponse Execute(IRestClient client, MethodInfo method, IRestRequest request)
		{
			return (IRestResponse) method.Invoke(client, new object[] {request});
		}
		private MethodInfo GetMethod(Type type)
		{
			var genericMethod = typeof (IRestClient).GetMethod("Execute", new[] {typeof (IRestRequest)});
			var typedMethod = genericMethod.MakeGenericMethod(type);
			return typedMethod;
		}
	}
}
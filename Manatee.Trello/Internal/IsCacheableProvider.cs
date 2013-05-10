/***************************************************************************************

	Copyright 2013 Little Crab Solutions

	   Licensed under the Apache License, Version 2.0 (the "License");
	   you may not use this file except in compliance with the License.
	   You may obtain a copy of the License at

		 http://www.apache.org/licenses/LICENSE-2.0

	   Unless required by applicable law or agreed to in writing, software
	   distributed under the License is distributed on an "AS IS" BASIS,
	   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
	   See the License for the specific language governing permissions and
	   limitations under the License.
 
	File Name:		IsCacheableProvider.cs
	Namespace:		Manatee.Trello.Internal
	Class Name:		IsCacheableProvider
	Purpose:		Determines if a given type should be cached.

***************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;

namespace Manatee.Trello.Internal
{
	internal class IsCacheableProvider
	{
		private static readonly IsCacheableProvider _default;
		private static readonly Dictionary<Type, bool> _registry;

		public static IsCacheableProvider Default { get { return _default; } }

		static IsCacheableProvider()
		{
			_default = new IsCacheableProvider();
			_registry = new Dictionary<Type, bool>
			            	{
								{typeof(Action), true},
								{typeof(Board), true},
								{typeof(Card), true},
								{typeof(CheckList), true},
								{typeof(List), true},
								{typeof(Member), true},
								{typeof(Notification), true},
								{typeof(Organization), true},
			            	};
		}
		private IsCacheableProvider() {}

		public bool IsCacheable<T>()
		{
			return IsCacheable(typeof (T));
		}
		public bool IsCacheable(Type type)
		{
			var baseTypes = GetBaseTypes(type);
			var registeredType = baseTypes.FirstOrDefault(_registry.ContainsKey);
			return (registeredType != null) && _registry[registeredType];
		}

		private static IEnumerable<Type> GetBaseTypes(Type type)
		{
			var baseType = type;
			while (baseType != null)
			{
				yield return baseType;
				baseType = baseType.BaseType;
			}
		}
	}
}
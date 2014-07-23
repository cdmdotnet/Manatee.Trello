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
 
	File Name:		SimpleCache.cs
	Namespace:		Manatee.Trello.Internal.Caching
	Class Name:		SimpleCache
	Purpose:		Simple implementation of the ICache interface.

***************************************************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Manatee.Trello.Contracts;

namespace Manatee.Trello.Internal.Caching
{
	internal class SimpleCache : ICache
	{
		private readonly List<object> _list;

		public SimpleCache()
		{
			_list = new List<object>();
		}

		public void Add(object obj)
		{
			if (_list.Contains(obj)) return;
			_list.Add(obj);
		}
		public T Find<T>(Func<T, bool> match)
		{
			return _list.OfType<T>().FirstOrDefault(match);
		}
		public void Remove(object obj)
		{
			_list.Remove(obj);
		}
		public void Clear()
		{
			_list.Clear();
		}
		public IEnumerator GetEnumerator()
		{
			return _list.GetEnumerator();
		}
	}
}

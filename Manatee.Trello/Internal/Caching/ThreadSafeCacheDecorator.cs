/***************************************************************************************

	Copyright 2013 Greg Dennis

	   Licensed under the Apache License, Version 2.0 (the "License");
	   you may not use this file except in compliance with the License.
	   You may obtain a copy of the License at

		 http://www.apache.org/licenses/LICENSE-2.0

	   Unless required by applicable law or agreed to in writing, software
	   distributed under the License is distributed on an "AS IS" BASIS,
	   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
	   See the License for the specific language governing permissions and
	   limitations under the License.
 
	File Name:		ThreadSafeCache.cs
	Namespace:		Manatee.Trello.Internal.Caching
	Class Name:		ThreadSafeCache
	Purpose:		Adds thread safe operation to an ICache implementation.

***************************************************************************************/

using System;
using System.Collections;
using Manatee.Trello.Contracts;

namespace Manatee.Trello.Internal.Caching
{
	internal class ThreadSafeCacheDecorator : ICache
	{
		private readonly ICache _innerCache;
		private readonly object _lock;

		public ThreadSafeCacheDecorator(ICache innerCache)
		{
			_innerCache = innerCache;
			_lock = new object();
		}

		public void Add(object obj)
		{
			lock (_lock)
			{
				_innerCache.Add(obj);
			}
		}
		public T Find<T>(Func<T, bool> match)
		{
			lock (_lock)
			{
				return _innerCache.Find(match);
			}
		}
		public void Remove(object obj)
		{
			lock (_lock)
			{
				_innerCache.Remove(obj);
			}
		}
		public void Clear()
		{
			lock (_lock)
			{
				_innerCache.Clear();
			}
		}
		public IEnumerator GetEnumerator()
		{
			return _innerCache.GetEnumerator();
		}
	}
}
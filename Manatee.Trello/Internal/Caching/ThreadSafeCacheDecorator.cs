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
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

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

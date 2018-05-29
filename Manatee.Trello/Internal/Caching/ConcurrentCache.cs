using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Manatee.Trello.Internal.Caching
{
	internal class ConcurrentCache : ICache
	{
		private readonly ConcurrentDictionary<string, ICacheable> _collection;

		public ConcurrentCache()
		{
			_collection = new ConcurrentDictionary<string, ICacheable>();
		}

		public void Add(ICacheable obj)
		{
			if (_collection.ContainsKey(obj.Id))
				TrelloConfiguration.Log.Info($"Discovered second instance of {obj.GetType().Name} with ID {obj.Id}");
			_collection[obj.Id] = obj;
		}

		public T Find<T>(string id)
			where T : class, ICacheable
		{
			return _collection.TryGetValue(id, out var obj) ? obj as T : null;
		}

		public void Remove(ICacheable obj)
		{
			_collection.TryRemove(obj.Id, out _);
		}

		public void Clear()
		{
			_collection.Clear();
		}

		public IEnumerator<ICacheable> GetEnumerator()
		{
			return _collection.Values.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Manatee.Trello.Implementation;
using Manatee.Trello.Rest;

namespace Manatee.Trello
{
	// TODO: Review all entities for unchanging properties, such as Url, and remove calls to VerifyNotExpired() and Refresh() implementations
	public class TrelloService
	{
		internal readonly EntityService Api;

		private readonly List<EntityBase> _cache;

		public TrelloService(string authKey, string authToken)
		{
			Api = new EntityService(authKey, authToken);
			_cache = new List<EntityBase>();
		}
		~TrelloService()
		{
			_cache.ForEach(i => i.Svc = null);
		}

		public T Retrieve<T>(string id) where T : EntityBase, new()
		{
			if (string.IsNullOrWhiteSpace(id)) return null;
			T item = _cache.OfType<T>().FirstOrDefault(i => i.Match(id));
			if (item != null) return item;
			item = Api.GetEntity<T>(id);
			item.Svc = this;
			return item;
		}
		internal List<TContent> RetrieveContent<TSource, TContent>(string sourceId)
			where TSource : EntityBase
			where TContent : EquatableExpiringObject
		{
			if (string.IsNullOrWhiteSpace(sourceId)) return null;
			var items = Api.GetContents<TSource, TContent>(sourceId);
			var ofType = _cache.OfType<TContent>();
			if (ofType.Any())
			{
				var cacheItems = ofType.Where(items.Contains);
				items = cacheItems.Union(items).ToList();
			}
			return items;
		}
	}
}

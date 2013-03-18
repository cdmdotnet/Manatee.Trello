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
 
	File Name:		TrelloService.cs
	Namespace:		Manatee.Trello
	Class Name:		TrelloService
	Purpose:		Represents a member (user) on Trello.com.

***************************************************************************************/
using System.Collections.Generic;
using System.Linq;
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
			if (item != null)
				item.Svc = this;
			return item;
		}
		internal List<TContent> RetrieveContent<TSource, TContent>(string sourceId)
			where TSource : EntityBase
			where TContent : ExpiringObject
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

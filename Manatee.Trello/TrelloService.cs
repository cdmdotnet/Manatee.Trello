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
	public class TrelloService
	{
		internal readonly TrelloRest Api;

		private readonly List<EntityBase> _cache;

		public TrelloService(string authKey, string authToken)
		{
			Api = new TrelloRest(authKey, authToken);
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
			var request = new Request<T>(id);
			item = Api.Get(request);
			if (item != null)
				item.Svc = this;
			_cache.Add(item);
			return item;
		}
		public void Flush()
		{
			var remove = _cache.Where(e => e.IsExpired).ToList();
			remove.ForEach(e => _cache.Remove(e));
		}

		internal IEnumerable<TEntity> RetrieveContent<TOwner, TEntity>(string sourceId)
			where TOwner : EntityBase
			where TEntity : ExpiringObject, new()
		{
			if (string.IsNullOrWhiteSpace(sourceId)) return null;
			var request = new CollectionRequest<TOwner, TEntity>(sourceId);
			var items = Api.Get(request);
			var ofType = _cache.OfType<TEntity>();
			if (ofType.Any())
			{
				var cacheItems = ofType.Where(items.Contains);
				items = cacheItems.Union(items).ToList();
				_cache.AddRange(items.Except(ofType).Cast<EntityBase>());
			}
			return items;
		}
		//internal TEntity PostAndCache<TEntity, TRequest>(TRequest request)
		//    where TEntity : EntityBase, new()
		//    where TRequest : Request<TEntity>
		//{
		//    var retVal = _Api.Post<TEntity, TRequest>(request);
		//    var entity = retVal as EntityBase;
		//    _cache.Add(entity);
		//    return retVal;
		//}
		//internal TEntity PostAndCache<TOwner, TEntity, TRequest>(TRequest request)
		//    where TOwner : EntityBase
		//    where TEntity : OwnedEntityBase<TOwner>, new()
		//    where TRequest : Request<TOwner, TEntity>
		//{
		//    var retVal = _Api.Post<TOwner, TEntity, TRequest>(request);
		//    var entity = retVal as EntityBase;
		//    _cache.Add(entity);
		//    return retVal;
		//}
		//internal TEntity PutAndCache<TOwner, TEntity, TRequest>(TRequest request)
		//    where TOwner : EntityBase
		//    where TEntity : OwnedEntityBase<TOwner>, new()
		//    where TRequest : Request<TOwner, TEntity>
		//{
		//    var retVal = _Api.Put<TOwner, TEntity, TRequest>(request);
		//    var entity = retVal as EntityBase;
		//    _cache.Add(entity);
		//    return retVal;
		//}
	}
}

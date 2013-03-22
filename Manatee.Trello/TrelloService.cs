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
using System;
using System.Collections.Generic;
using System.Linq;
using Manatee.Trello.Implementation;
using Manatee.Trello.Rest;

namespace Manatee.Trello
{
	public class TrelloService
	{
		internal readonly TrelloRest Api;

		private readonly List<ExpiringObject> _cache;

		public TrelloService(string authKey, string authToken)
		{
			Api = new TrelloRest(authKey, authToken);
			_cache = new List<ExpiringObject>();
		}
		~TrelloService()
		{
			_cache.ForEach(i => i.Svc = null);
		}

		public T Retrieve<T>(string id) where T : ExpiringObject, new()
		{
			if (string.IsNullOrWhiteSpace(id)) return null;
			T item = _cache.OfType<T>().FirstOrDefault(i => i.Match(id));
			if (item != null) return item;
			return Execute(() => Api.Get(new Request<T>(id)));
		}
		public void Flush()
		{
			var remove = _cache.Where(e => e.IsExpired).ToList();
			remove.ForEach(e => _cache.Remove(e));
		}

		internal IEnumerable<T> Get<T>(CollectionRequest<T> request)
			where T : ExpiringObject, new()
		{
			var items = Api.Get(request);
			var newItems = items.Except(_cache).Where(e => e != null);
			_cache.AddRange(newItems);
			return items;
		}
		internal T PutAndCache<T>(Request<T> request)
			where T : ExpiringObject, new()
		{
			return Execute(() => Api.Put(request));
		}
		internal T PostAndCache<T>(Request<T> request)
			where T : ExpiringObject, new()
		{
			return Execute(() => Api.Post(request));
		}
		internal T DeleteFromCache<T>(Request<T> request)
			where T : ExpiringObject, new()
		{
			var retVal = Api.Delete(request);
			_cache.Remove(retVal);
			return retVal;
		}

		private T Execute<T>(Func<T> func)
			where T : ExpiringObject
		{
			var retVal = func();
			if (retVal != null)
			{
				retVal.Svc = this;
				if (!_cache.Contains(retVal))
					_cache.Add(retVal);
			}
			return retVal;
		}
	}
}

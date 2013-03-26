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
	Purpose:		Provides an interface to retrieving data from Trello.com and
					maintains a cache of all retrieved items.

***************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using Manatee.Trello.Contracts;
using Manatee.Trello.Implementation;
using Manatee.Trello.Rest;

namespace Manatee.Trello
{
	/// <summary>
	/// Provides an interface to retrieving data from Trello.com.
	/// </summary>
	public class TrelloService
	{
		internal readonly TrelloRest Api;

		private readonly List<ExpiringObject> _cache;

		/// <summary>
		/// Gets and sets the IRestClientProvider to be used by the service.
		/// </summary>
		public IRestClientProvider RestClientProvider
		{
			get { return Api.RestClientProvider; }
			set { Api.RestClientProvider = value; }
		}

		/// <summary>
		/// Creates a new instance of the TrelloService class.
		/// </summary>
		/// <param name="authKey"></param>
		/// <param name="authToken"></param>
		public TrelloService(string authKey, string authToken = null)
		{
			Api = new TrelloRest(authKey, authToken);
			_cache = new List<ExpiringObject>();
		}
		/// <summary>
		/// Allows an object to try to free resources and perform other cleanup operations before it is reclaimed by garbage collection.
		/// </summary>
		~TrelloService()
		{
			_cache.ForEach(i => i.Svc = null);
		}

		/// <summary>
		/// Retrieves the specified object from Trello.com and caches it.
		/// </summary>
		/// <typeparam name="T">The type of object to retrieve.</typeparam>
		/// <param name="id">The id of the object to retrieve.</param>
		/// <returns>The requested object or null if the object could not be found.</returns>
		/// <remarks>
		/// Will return null if the supplied ID does not match the type of object.  In the case of
		/// Members, the member's username may be supplied instead of their ID.
		/// </remarks>
		public T Retrieve<T>(string id) where T : ExpiringObject, new()
		{
			if (string.IsNullOrWhiteSpace(id)) return null;
			T item = _cache.OfType<T>().FirstOrDefault(i => i.Match(id));
			if (item != null) return item;
			return Execute(() => Api.Get(new RestSharpRequest<T>(id)));
		}
		/// <summary>
		/// Clears the cache of all retrieved items.
		/// </summary>
		public void Flush()
		{
			var remove = _cache.Where(e => e.IsExpired).ToList();
			remove.ForEach(e => _cache.Remove(e));
		}

		internal IEnumerable<T> Get<T>(IRestCollectionRequest<T> request)
			where T : ExpiringObject, new()
		{
			var items = Api.Get(request);
			foreach (var item in items)
			{
				if (!_cache.OfType<T>().Contains(item))
					_cache.Add(item);
			}
			return items;
		}
		internal T PutAndCache<T>(RestSharpRequest<T> request)
			where T : ExpiringObject, new()
		{
			return Execute(() => Api.Put(request));
		}
		internal T PostAndCache<T>(RestSharpRequest<T> request)
			where T : ExpiringObject, new()
		{
			return Execute(() => Api.Post(request));
		}
		internal T DeleteFromCache<T>(RestSharpRequest<T> request)
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

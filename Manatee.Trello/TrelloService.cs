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
using System.Net;
using Manatee.Trello.Contracts;
using Manatee.Trello.Internal;
using Manatee.Trello.Json;
using Manatee.Trello.Rest;

namespace Manatee.Trello
{
	/// <summary>
	/// Provides an interface to retrieving data from Trello.com.
	/// </summary>
	public class TrelloService : ITrelloService
	{
		private readonly string _appKey;
		private string _userToken;
		private ITrelloRest _api;
		private ICache _cache;
		private Member _me;

		/// <summary>
		/// Allows the TrelloService instance to access data as if it was the member
		/// who provided the token.
		/// </summary>
		public string UserToken
		{
			get { return _userToken; }
			set
			{
				Validate.NonEmptyString(value);
				_userToken = value;
				Api.UserToken = _userToken;
				_me = null;
			}
		}
		/// <summary>
		/// Provides caching for retrieved entities.
		/// </summary>
		public ICache Cache
		{
			get { return _cache ?? (_cache = new SimpleCache()); }
			set
			{
				if (value == null)
					throw new ArgumentNullException("value");
				if (_cache != null)
					throw new InvalidOperationException("Cache already set.");
				_cache = value;
			}
		}
		/// <summary>
		/// Gets the Member object associated with the provided AppKey.
		/// </summary>
		public Member Me
		{
			get
			{
				if (UserToken == null)
					return null;
				return (_me == null) ? _me : (_me = GetMe());
			}
		}
		/// <summary>
		/// Gets and sets the IRestClientProvider to be used by the service.
		/// </summary>
		public IRestClientProvider RestClientProvider
		{
			get { return Api.RestClientProvider; }
			set { Api.RestClientProvider = value; }
		}
		/// <summary>
		/// Facilitates calling the Trello API.
		/// </summary>
		/// <remarks>
		/// Provided for testing.  It is not recommended that this is used.
		/// </remarks>
		public ITrelloRest Api
		{
			get { return _api ?? (_api = new TrelloRest(_appKey, _userToken)); }
		}
		/// <summary>
		/// Gets the exception generated from the previous call, if any.
		/// </summary>
		/// <remarks>
		/// When a method returns null, check this method for any errors.
		/// </remarks>
		public Exception LastCallError { get; private set; }

		/// <summary>
		/// Creates a new instance of the TrelloService class.
		/// </summary>
		/// <param name="appKey"></param>
		/// <param name="userToken"></param>
		public TrelloService(string appKey, string userToken = null)
		{
			Validate.NonEmptyString(appKey);
			_appKey = appKey;
			_userToken = userToken;
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
		public T Retrieve<T>(string id)
			where T : ExpiringObject, new()
		{
			Validate.NonEmptyString(id);
			T entity;
			if (Cache != null)
			{
				entity = Cache.Find<T>(e => e.Matches(id));
				if (entity != null) return entity;
				entity = Verify<T>(id);
			}
			else
				entity = Verify<T>(id);
			return entity;
		}
		/// <summary>
		/// Searches actions, boards, cards, members and organizations for a provided
		/// query string.
		/// </summary>
		/// <param name="query">The query string.</param>
		/// <returns>An object which contains the results of the query.</returns>
		public SearchResults Search(string query)
		{
			Validate.NonEmptyString(query);
			var endpoint = new Endpoint(new[] {"search"});
			var request = RestClientProvider.RequestProvider.Create(endpoint.ToString());
			request.AddParameter("query", query);
			request.AddParameter("action_fields", "id");
			request.AddParameter("board_fields", "id");
			request.AddParameter("card_fields", "id");
			request.AddParameter("member_fields", "id");
			request.AddParameter("organization_fields", "id");
			return new SearchResults(this, Api.Get<IJsonSearchResults>(request));
		}
		/// <summary>
		/// Searches for members whose names or usernames match a provided query string.
		/// </summary>
		/// <param name="query">The query string.</param>
		/// <param name="limit">The maximum number of results to return.</param>
		/// <returns>A collection of members.</returns>
		public IEnumerable<Member> SearchMembers(string query, int limit = 0)
		{
			Validate.NonEmptyString(query);
			var endpoint = new Endpoint(Enumerable.Empty<string>());
			endpoint.Append("search");
			endpoint.Append("members");
			var request = Api.RequestProvider.Create(endpoint.ToString());
			request.AddParameter("query", query);
			if (limit > 0)
				request.AddParameter("limit", limit);
			var reply = Api.Get<List<IJsonMember>>(request);
			foreach (var jsonMember in reply)
			{
				var entity = new Member {Svc = this};
				entity.ApplyJson(jsonMember);
				yield return entity;
			}
		}
		/// <summary>
		/// Returns a string that represents the current object.
		/// </summary>
		/// <returns>
		/// A string that represents the current object.
		/// </returns>
		/// <filterpriority>2</filterpriority>
		public override string ToString()
		{
			return string.Format("Key: {0}, Token: {1}", Api.AppKey, Api.UserToken);
		}

		private T Verify<T>(string id)
			where T : ExpiringObject, new()
		{
			T entity = null;
			LastCallError = null;
			try
			{
				if (typeof(T).IsAssignableFrom(typeof(Token)))
				{
					entity = new Token(id) {Svc = this} as T;
				}
				else entity = new T {Id = id, Svc = this};
				entity.VerifyNotExpired();
				if (typeof(T).IsAssignableFrom(typeof(Action)))
				{
					entity = ActionProvider.Default.Parse(entity as Action) as T;
				}
				else if (typeof(T).IsAssignableFrom(typeof(Notification)))
				{
					entity = NotificationProvider.Default.Parse(entity as Notification) as T;
				}
				return entity;
			}
			catch (Exception e)
			{
				LastCallError = e;
				Cache.Remove(entity);
				return null;
			}
		}

		private Member GetMe()
		{
			Member member = null;
			LastCallError = null;
			try
			{
				var endpoint = EndpointGenerator.Default.Generate(member);
				endpoint.Append("me");
				var request = Api.RequestProvider.Create(endpoint.ToString());
				var json = Api.Get<IJsonMember>(request);
				if (json == null) return null;
				var jsonCacheable = json as IJsonCacheable;
				if (Cache != null)
				{
					var cached = Cache.Find<Member>(e => e.Matches(jsonCacheable.Id));
					if (cached != null) return cached;
				}
				member = new Member();
				member.ApplyJson(json);
				member.Svc = this;
				return member;
			}
			catch (Exception e)
			{
				LastCallError = e;
				Cache.Remove(member);
				return null;
			}
		}
	}
}

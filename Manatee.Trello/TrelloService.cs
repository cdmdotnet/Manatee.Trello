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
		private readonly string _authKey;
		private string _authToken;
		private ITrelloRest _api;
		private ICache _cache;
		private Member _me;

		/// <summary>
		/// Allows the TrelloService instance to access data as if it was the member
		/// who provided the token.
		/// </summary>
		public string AuthToken
		{
			get { return _authToken; }
			set
			{
				Validate.NonEmptyString(value);
				_authToken = value;
				Api.AuthToken = _authToken;
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
		/// Gets the Member object associated with the provided AuthKey.
		/// </summary>
		public Member Me
		{
			get
			{
				if (AuthToken == null)
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
			get { return _api ?? (_api = new TrelloRest(_authKey, _authToken)); }
		}
		
		/// <summary>
		/// Creates a new instance of the TrelloService class.
		/// </summary>
		/// <param name="authKey"></param>
		/// <param name="authToken"></param>
		public TrelloService(string authKey, string authToken = null)
		{
			_authKey = authKey;
			_authToken = authToken;
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
			if (string.IsNullOrWhiteSpace(id)) return null;
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
		/// Returns a string that represents the current object.
		/// </summary>
		/// <returns>
		/// A string that represents the current object.
		/// </returns>
		/// <filterpriority>2</filterpriority>
		public override string ToString()
		{
			return string.Format("Key: {0}, Token: {1}", Api.AuthKey, Api.AuthToken);
		}

		private T Verify<T>(string id)
			where T : ExpiringObject, new()
		{
			T entity = null;
			try
			{
				entity = new T {Id = id, Svc = this};
				entity.VerifyNotExpired();
				return entity;
			}
			catch
			{
				Cache.Remove(entity);
				return null;
			}
		}

		private Member GetMe()
		{
			var member = new Member();
			var endpoint = EndpointGenerator.Default.Generate(member);
			endpoint.Append("me");
			var request = Api.RequestProvider.Create<IJsonMember>(endpoint.ToString());
			var json = Api.Get(request);
			if (json == null) return null;
			member.ApplyJson(json);
			return member;
		}
	}
}

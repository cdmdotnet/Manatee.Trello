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
using Manatee.Trello.Implementation;
using Manatee.Trello.Rest;

namespace Manatee.Trello
{
	/// <summary>
	/// Provides an interface to retrieving data from Trello.com.
	/// </summary>
	public class TrelloService : ITrelloService
	{
		private readonly string _authKey;
		private readonly string _authToken;
		private ITrelloRest _api;

		/// <summary>
		/// Gets and sets the IRestClientProvider to be used by the service.
		/// </summary>
		public IRestClientProvider RestClientProvider
		{
			get { return Api.RestClientProvider; }
			set { Api.RestClientProvider = value; }
		}

		private ITrelloRest Api
		{
			get { return _api ?? (_api = new CachingTrelloRest(new TrelloRest(_authKey, _authToken), new ActionProvider(), new NotificationProvider())); }
			set
			{
				if (value == null)
					throw new ArgumentNullException("value");
				if (_api != null)
					throw new InvalidOperationException("Api already set.");
				_api = value;
			}
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
		public T Retrieve<T>(string id) where T : ExpiringObject, new()
		{
			if (string.IsNullOrWhiteSpace(id)) return null;
			return Api.Get(Api.RequestProvider.Create<T>(id));
		}
	}
}

﻿/***************************************************************************************

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

using System.Collections.Generic;
using System.Linq;
using Manatee.Trello.Contracts;
using Manatee.Trello.Exceptions;
using Manatee.Trello.Internal;
using Manatee.Trello.Internal.RequestProcessing;
using Manatee.Trello.Json;

namespace Manatee.Trello
{
	/// <summary>
	/// Provides an interface to retrieving data from Trello.com.
	/// </summary>
	public class TrelloService : ITrelloService
	{
		private readonly ITrelloServiceConfiguration _configuration;
		private readonly IRestRequestProcessor _requestProcessor;
		private readonly ITrelloRest _api;
		private readonly IValidator _validator;
		private readonly IEntityFactory _entityFactory;
		private Member _me;

		/// <summary>
		/// Allows the TrelloService instance to access data as if it was the member
		/// who provided the token.
		/// </summary>
		public string UserToken
		{
			get { return Api.UserToken; }
			set { Api.UserToken = value; }
		}
		/// <summary>
		/// Gets the Member object associated with the provided AppKey.
		/// </summary>
		public Member Me
		{
			get
			{
				return _me ?? (_me = GetMe());
			}
		}
		/// <summary>
		/// Provides a set of options for use by a single ITrelloService instance.
		/// </summary>
		public ITrelloServiceConfiguration Configuration { get { return _configuration; } }
		ITrelloRest ITrelloService.Api { get { return _api; } }
		IValidator ITrelloService.Validator { get { return _validator; } }
		private ITrelloRest Api { get { return _api; } }

		/// <summary>
		/// Creates a new instance of the TrelloService class using a given configuration.
		/// </summary>
		/// <param name="configuration">A configuration object.</param>
		/// <param name="appKey">The application key.</param>
		/// <param name="userToken">The user token.</param>
		public TrelloService(ITrelloServiceConfiguration configuration, string appKey, string userToken = null)
		{
			_validator = new Validator(this);
			_validator.ArgumentNotNull(configuration, "configuration");
			_validator.NonEmptyString(appKey);
			_configuration = configuration;
			_requestProcessor = new RestRequestProcessor(_configuration.Log, new Queue<QueuableRestRequest>(), _configuration.RestClientProvider, appKey, userToken);
			_api = new TrelloRest(_configuration.Log, _requestProcessor, appKey, userToken);
			_entityFactory = new EntityFactory();
		}
		internal TrelloService(ITrelloServiceConfiguration configuration,
		                       IValidator validator,
		                       IRestRequestProcessor requestProcessor,
		                       ITrelloRest api,
		                       IEntityFactory entityFactory)
		{
			_configuration = configuration;
			_validator = validator;
			_requestProcessor = requestProcessor;
			_api = api;
			_entityFactory = entityFactory;
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
			_validator.NonEmptyString(id);
			T entity;
			if (_configuration.Cache != null)
				entity = _configuration.Cache.Find(e => e.Matches(id), () => Verify<T>(id));
			else
				entity = Verify<T>(id);
			return entity;
		}
		/// <summary>
		/// Searches actions, boards, cards, members and organizations for a provided
		/// query string.
		/// </summary>
		/// <param name="query">The query string.</param>
		/// <param name="context">The items in which to perform the search.</param>
		/// <param name="modelTypes">The model types to return.  Can be combined using the '|' operator.</param>
		/// <returns>An object which contains the results of the query.</returns>
		public SearchResults Search(string query, List<ExpiringObject> context = null, SearchModelType modelTypes = SearchModelType.All)
		{
			_validator.NonEmptyString(query);
			var endpoint = new Endpoint(new[] { "search" });
			var request = _configuration.RestClientProvider.RequestProvider.Create(endpoint.ToString());
			request.AddParameter("query", query);
			request.AddParameter("action_fields", "id");
			request.AddParameter("board_fields", "id");
			request.AddParameter("card_fields", "id");
			request.AddParameter("member_fields", "id");
			request.AddParameter("organization_fields", "id");
			if (context != null)
			{
				var results = ConstructContextParameter<Board>(context);
				if (!string.IsNullOrEmpty(results))
					request.AddParameter("idBoards", results);
				results = ConstructContextParameter<Card>(context);
				if (!string.IsNullOrEmpty(results))
					request.AddParameter("idCards", results);
				results = ConstructContextParameter<Organization>(context);
				if (!string.IsNullOrEmpty(results))
					request.AddParameter("idOrganizations", results);
			}
			request.AddParameter("modelTypes", ConstructSearchModelTypeParameter(modelTypes));
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
			_validator.NonEmptyString(query);
			var endpoint = new Endpoint(new[] {"search", "members"});
			var request = _configuration.RestClientProvider.RequestProvider.Create(endpoint.ToString());
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
		/// Instructs the service to stop sending requests.
		/// </summary>
		public void HoldRequests()
		{
			_requestProcessor.IsActive = false;
		}
		/// <summary>
		/// Instructs the service to continue sending requests.
		/// </summary>
		public void ResumeRequests()
		{
			_requestProcessor.IsActive = true;
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
			catch
			{
				_configuration.Cache.Remove(entity);
				throw;
			}
		}
		private Member GetMe()
		{
			if (UserToken == null)
				_configuration.Log.Error(new ReadOnlyAccessException("A valid user token must be supplied to retrieve the 'Me' object."));
			var endpoint = new Endpoint(new[] { Member.TypeKey, "me" });
			var request = _configuration.RestClientProvider.RequestProvider.Create(endpoint.ToString());
			request.AddParameter("fields","id");
			var json = Api.Get<IJsonMember>(request);
			if (json == null) return null;
			return Verify<Member>(json.Id);
		}
		private static string ConstructSearchModelTypeParameter(SearchModelType types)
		{
			return types.ToLowerString().Replace(" ", string.Empty);
		}
		private static string ConstructContextParameter<T>(IEnumerable<ExpiringObject> models)
			where T : ExpiringObject
		{
			return string.Join(",", models.OfType<T>().Take(24).Select(m => m.Id));
		}
	}
}

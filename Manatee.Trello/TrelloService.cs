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
using Manatee.Trello.Exceptions;
using Manatee.Trello.Internal;
using Manatee.Trello.Internal.Bootstrapping;
using Manatee.Trello.Internal.DataAccess;
using Manatee.Trello.Internal.Genesis;
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
		private readonly IEntityFactory _entityFactory;
		private readonly IJsonRepository _jsonRepository;
		private readonly IValidator _validator;
		private Member _me;

		/// <summary>
		/// Allows the TrelloService instance to access data as if it was the member
		/// who provided the token.
		/// </summary>
		public string UserToken
		{
			get { return _requestProcessor.UserToken; }
			set { _requestProcessor.UserToken = value; }
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

		/// <summary>
		/// Creates a new instance of the TrelloService class using a given configuration.
		/// </summary>
		/// <param name="configuration">A configuration object.</param>
		/// <param name="appKey">The application key.</param>
		/// <param name="userToken">The user token.</param>
		public TrelloService(ITrelloServiceConfiguration configuration, string appKey, string userToken = null)
		{
			if (configuration == null) throw new ArgumentNullException("configuration");
			_configuration = configuration;
			var bootstrapper = new Bootstrapper();
			bootstrapper.Initialize(this, configuration, appKey, userToken);
			_requestProcessor = bootstrapper.RequestProcessor;
			_jsonRepository = bootstrapper.JsonRepository;
			_validator = bootstrapper.Validator;
			_entityFactory = bootstrapper.EntityFactory;
		}
		internal TrelloService(ITrelloServiceConfiguration configuration,
							   IValidator validator,
							   IEntityFactory entityFactory,
							   IRestRequestProcessor requestProcessor,
							   IJsonRepository jsonRepository)
		{
			_configuration = configuration;
			_validator = validator;
			_entityFactory = entityFactory;
			_requestProcessor = requestProcessor;
			_jsonRepository = jsonRepository;
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
			var endpoint = new Endpoint(new[] {"search"});
			var parameters = new Dictionary<string, object>
				{
					{"query", query},
					{"action_fields", "id"},
					{"board_fields", "id"},
					{"card_fields", "id"},
					{"member_fields", "id"},
					{"organization_fields", "id"},
				};
			if (context != null)
			{
				var results = ConstructContextParameter<Board>(context);
				if (!string.IsNullOrEmpty(results))
					parameters.Add("idBoards", results);
				results = ConstructContextParameter<Card>(context);
				if (!string.IsNullOrEmpty(results))
					parameters.Add("idCards", results);
				results = ConstructContextParameter<Organization>(context);
				if (!string.IsNullOrEmpty(results))
					parameters.Add("idOrganizations", results);
			}
			parameters.Add("modelTypes", ConstructSearchModelTypeParameter(modelTypes));
			return new SearchResults(this, _jsonRepository.Get<IJsonSearchResults>(endpoint.ToString(), parameters));
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
			var parameters = new Dictionary<string, object>
				{
					{"query", query},
				};
			if (limit > 0)
				parameters.Add("limit", limit);
			var reply = _jsonRepository.Get<List<IJsonMember>>(endpoint.ToString(), parameters);
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
			return string.Format("Key: {0}, Token: {1}", _requestProcessor.AppKey, _requestProcessor.UserToken);
		}

		private T Verify<T>(string id)
			where T : ExpiringObject
		{
			T entity = null;
			try
			{
				entity = _entityFactory.CreateEntity<T>(id);
				return entity;
			}
			catch
			{
				if (_configuration.Cache != null)
					_configuration.Cache.Remove(entity);
				throw;
			}
		}
		private Member GetMe()
		{
			if (UserToken == null)
				_configuration.Log.Error(new ReadOnlyAccessException("A valid user token must be supplied to retrieve the 'Me' object."));
			var endpoint = new Endpoint(new[] {"members", "me"});
			var parameters = new Dictionary<string, object>
				{
					{"fields", "id"},
				};
			var json = _jsonRepository.Get<IJsonMember>(endpoint.ToString(), parameters);
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

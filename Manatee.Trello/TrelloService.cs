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

namespace Manatee.Trello
{
	/// <summary>
	/// Provides an interface to retrieving data from Trello.com.
	/// </summary>
	public class TrelloService : ITrelloService
	{
		private readonly ITrelloServiceConfiguration _configuration;
		private readonly IRestRequestProcessor _requestProcessor;
		private readonly IEntityRepository _entityRepository;
		private readonly IValidator _validator;
		private readonly IEndpointFactory _endpointFactory;
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
			get { return _me ?? (_me = GetMe()); }
		}

		/// <summary>
		/// Creates a new instance of the TrelloService class using a given configuration.
		/// </summary>
		/// <param name="configuration">A configuration object.</param>
		/// <param name="auth">The authorization object for this service.</param>
		public TrelloService(ITrelloServiceConfiguration configuration, TrelloAuthorization auth)
		{
			if (configuration == null) throw new ArgumentNullException("configuration");
			_configuration = configuration;
			var bootstrapper = new Bootstrapper();
			bootstrapper.Initialize(this, configuration, auth);
			_requestProcessor = bootstrapper.RequestProcessor;
			_validator = bootstrapper.Validator;
			_entityRepository = bootstrapper.EntityRepository;
			_endpointFactory = bootstrapper.EndpointFactory;
		}
		internal TrelloService(ITrelloServiceConfiguration configuration,
							   IValidator validator,
							   IEntityRepository entityRepository,
							   IRestRequestProcessor requestProcessor,
							   IEndpointFactory endpointFactory)
		{
			_configuration = configuration;
			_validator = validator;
			_entityRepository = entityRepository;
			_requestProcessor = requestProcessor;
			_endpointFactory = endpointFactory;
		}
		/// <summary>
		/// Allows an object to try to free resources and perform other cleanup operations before it is reclaimed by garbage collection.
		/// </summary>
		~TrelloService()
		{
			_requestProcessor.ShutDown();
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
			T entity = Verify<T>(id);
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
			var parameters = new Dictionary<string, object> {{"query", query}};
			foreach (var parameter in RestParameterRepository.GetParameters<SearchResults>())
			{
				parameters[parameter.Key] = parameter.Value;
			}
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
			parameters.Add("modelTypes", modelTypes.ToParameterString());
			return _entityRepository.Download<SearchResults>(EntityRequestType.Service_Read_Search, parameters);
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
			var memberSearchList = new ExpiringList<Member>(null, EntityRequestType.Service_Read_SearchMembers)
				{
					Log = _configuration.Log,
					EntityRepository = _entityRepository,
					Validator = _validator
				};
			memberSearchList.Parameters.Add("query", query);
			memberSearchList.Refresh();
			return memberSearchList;
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
			var requestType = _endpointFactory.GetRequestType<T>();
			if (requestType == EntityRequestType.Unsupported) return null;
			var parameters = new Dictionary<string, object> {{"_id", id}};
			foreach (var parameter in RestParameterRepository.GetParameters<T>())
			{
				parameters[parameter.Key] = parameter.Value;
			}
			T entity = _entityRepository.Download<T>(requestType, parameters);
			return entity;
		}
		private Member GetMe()
		{
			if (UserToken == null)
				_configuration.Log.Error(new ReadOnlyAccessException("A valid user token must be supplied to retrieve the 'Me' object."));
			var parameters = RestParameterRepository.GetParameters<Member>()
				.ToDictionary<KeyValuePair<string, string>, string, object>(k => k.Key, v => v.Value);
			return _entityRepository.Download<Member>(EntityRequestType.Service_Read_Me, parameters);
		}
		private static string ConstructContextParameter<T>(IEnumerable<ExpiringObject> models)
			where T : ExpiringObject
		{
			return string.Join(",", models.OfType<T>().Take(24).Select(m => m.Id));
		}
	}
}

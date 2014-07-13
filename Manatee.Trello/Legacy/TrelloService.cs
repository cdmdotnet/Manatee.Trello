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
		private readonly TrelloAuthorization _auth;
		private readonly IEntityRepository _entityRepository;
		private readonly IValidator _validator;
		private Me _me;

		/// <summary>
		/// Allows the TrelloService instance to access data as if it was the member
		/// who provided the token.
		/// </summary>
		public string UserToken
		{
			get { return _auth.UserToken; }
			set { _auth.UserToken = value; }
		}
		/// <summary>
		/// Gets the Member object associated with the provided AppKey.
		/// </summary>
		public Me Me
		{
			get { return _me ?? (_me = GetMe()); }
		}
		/// <summary>
		/// Gets or sets whether entities are allowed to update themselves.  A value of false implies that
		/// updates will be performed via webhook notifications or manually.
		/// </summary>
		public bool AllowSelfUpdate
		{
			get { return _entityRepository.AllowSelfUpdate; }
			set { _entityRepository.AllowSelfUpdate = value; }
		}
		/// <summary>
		/// Gets and sets the global duration setting for all auto-refreshing objects.
		/// </summary>
		public TimeSpan ItemDuration { get; set; }

		/// <summary>
		/// Creates a new instance of the TrelloService class using a given configuration.
		/// </summary>
		/// <param name="auth">The authorization object for this service.</param>
		public TrelloService(TrelloAuthorization auth)
		{
			_auth = auth;
			ItemDuration = TimeSpan.FromSeconds(30);
			var bootstrapper = new Bootstrapper();
			bootstrapper.Initialize(this, auth);
			_validator = bootstrapper.Validator;
			_entityRepository = bootstrapper.EntityRepository;
		}
		internal TrelloService(IValidator validator,
							   IEntityRepository entityRepository)
		{
			_validator = validator;
			_entityRepository = entityRepository;
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
		public SearchResults Search(string query, IEnumerable<ExpiringObject> context = null, SearchModelType modelTypes = SearchModelType.All)
		{
			_validator.NonEmptyString(query);
			var parameters = new Dictionary<string, object> {{"query", query}};
			foreach (var parameter in RestParameterRepository.GetParameters<SearchResults>())
			{
				parameters[parameter.Key] = parameter.Value;
			}
			if (context != null)
			{
				var contextList = context as IList<ExpiringObject> ?? context.ToList();
				var results = ConstructContextParameter<Board>(contextList);
				if (!string.IsNullOrEmpty(results))
					parameters.Add("idBoards", results);
				results = ConstructContextParameter<Card>(contextList);
				if (!string.IsNullOrEmpty(results))
					parameters.Add("idCards", results);
				results = ConstructContextParameter<Organization>(contextList);
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
			var memberSearchList = new ExpiringCollection<Member>(null, EntityRequestType.Service_Read_SearchMembers)
				{
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
		// TODO: Move these to TrelloServiceConfiguration
		public void HoldRequests()
		{
			RestRequestProcessor.IsActive = false;
		}
		/// <summary>
		/// Instructs the service to continue sending requests.
		/// </summary>
		public void ResumeRequests()
		{
			RestRequestProcessor.IsActive = true;
		}
		/// <summary>
		/// Processes a received webhook notification.
		/// </summary>
		/// <param name="body"></param>
		public void ProcessWebhookNotification(string body)
		{
			if (TrelloServiceConfiguration.Deserializer == null)
				throw new Exception("Configuration.Deserializer must be set in order to handle webhook notifications.");
			if (TrelloServiceConfiguration.Cache == null)
				throw new Exception("Configuration.Cache must be set in order to handle webhook notifications.");
			var request = new InnerRestResponse<IJsonWebhookNotification> {Content = body};
			var json = TrelloServiceConfiguration.Deserializer.Deserialize(request).Action;
			var action = EntityFactory.CreateEntity<Action>();
			action.EntityRepository = _entityRepository;
			action.Validator = _validator;
			action.ApplyJson(json);
			foreach (var entity in TrelloServiceConfiguration.Cache.OfType<ICanWebhook>())
			{
				entity.ApplyAction(action);
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
			return string.Format("Key: {0}, Token: {1}", _auth.AppKey, _auth.UserToken);
		}

		private T Verify<T>(string id)
			where T : ExpiringObject
		{
			var requestType = EndpointFactory.GetRequestType<T>();
			if (requestType == EntityRequestType.Unsupported) return null;
			var parameters = new Dictionary<string, object> {{"_id", id}};
			foreach (var parameter in RestParameterRepository.GetParameters<T>())
			{
				parameters[parameter.Key] = parameter.Value;
			}
			T entity = _entityRepository.Download<T>(requestType, parameters);
			return entity;
		}
		private Me GetMe()
		{
			if (UserToken == null)
				TrelloServiceConfiguration.Log.Error(new ReadOnlyAccessException("A valid user token must be supplied to retrieve the 'Me' object."));
			var parameters = RestParameterRepository.GetParameters<Member>().ToDictionary<KeyValuePair<string, string>, string, object>(k => k.Key, v => v.Value);
			return _entityRepository.Download<Me>(EntityRequestType.Service_Read_Me, parameters);
		}
		private static string ConstructContextParameter<T>(IEnumerable<ExpiringObject> models)
			where T : ExpiringObject
		{
			return models.OfType<T>().Take(24).Select(m => m.Id).Join(",");
		}
	}
}

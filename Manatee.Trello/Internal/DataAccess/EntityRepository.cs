/***************************************************************************************

	Copyright 2013 Greg Dennis

	   Licensed under the Apache License, Version 2.0 (the "License");
	   you may not use this file except in compliance with the License.
	   You may obtain a copy of the License at

		 http://www.apache.org/licenses/LICENSE-2.0

	   Unless required by applicable law or agreed to in writing, software
	   distributed under the License is distributed on an "AS IS" BASIS,
	   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
	   See the License for the specific language governing permissions and
	   limitations under the License.
 
	File Name:		EntityRepository.cs
	Namespace:		Manatee.Trello.Internal.DataAccess
	Class Name:		EntityRepository
	Purpose:		Implements IEntityRepository.

***************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using Manatee.Trello.Contracts;
using Manatee.Trello.Internal.Genesis;
using Manatee.Trello.Internal.RequestProcessing;
using Manatee.Trello.Json;

namespace Manatee.Trello.Internal.DataAccess
{
	internal class EntityRepository : IEntityRepository
	{
		private static readonly Dictionary<Type, Func<TrelloAuthorization, Endpoint, IDictionary<string, object>, object>> _repositoryMethods;
		private static readonly Dictionary<Type, Action<ICache, ExpiringObject, object>> _applyJsonMethods;

		private readonly OfflineChangeQueue _offlineChangeQueue;
		private readonly TimeSpan _entityDuration;
		private readonly IValidator _validator;
		private readonly TrelloAuthorization _auth;

		public TimeSpan EntityDuration { get { return _entityDuration; } }

		static EntityRepository()
		{
			_repositoryMethods = new Dictionary<Type, Func<TrelloAuthorization, Endpoint, IDictionary<string, object>, object>>
				{
					{typeof (Action), JsonRepository.Execute<IJsonAction>},
					{typeof (Attachment), JsonRepository.Execute<IJsonAttachment>},
					{typeof (Badges), JsonRepository.Execute<IJsonBadges>},
					{typeof (Board), JsonRepository.Execute<IJsonBoard>},
					{typeof (BoardPersonalPreferences), JsonRepository.Execute<IJsonBoardPersonalPreferences>},
					{typeof (BoardPreferences), JsonRepository.Execute<IJsonBoardPreferences>},
					{typeof (Card), JsonRepository.Execute<IJsonCard>},
					{typeof (CheckItem), JsonRepository.Execute<IJsonCheckItem>},
					{typeof (CheckList), JsonRepository.Execute<IJsonCheckList>},
					{typeof (Label), JsonRepository.Execute<IJsonLabel>},
					{typeof (LabelNames), JsonRepository.Execute<IJsonLabelNames>},
					{typeof (List), JsonRepository.Execute<IJsonList>},
					{typeof (Me), JsonRepository.Execute<IJsonMember>},
					{typeof (Member), JsonRepository.Execute<IJsonMember>},
					{typeof (MemberPreferences), JsonRepository.Execute<IJsonMemberPreferences>},
					{typeof (MemberSession), JsonRepository.Execute<IJsonMemberSession>},
					{typeof (Notification), JsonRepository.Execute<IJsonNotification>},
					{typeof (Organization), JsonRepository.Execute<IJsonOrganization>},
					{typeof (SearchResults), JsonRepository.Execute<IJsonSearchResults>},
					{typeof (Token), JsonRepository.Execute<IJsonToken>},
					{typeof (Webhook<Board>), JsonRepository.Execute<IJsonWebhook>},
					{typeof (Webhook<Card>), JsonRepository.Execute<IJsonWebhook>},
					{typeof (Webhook<CheckItem>), JsonRepository.Execute<IJsonWebhook>},
					{typeof (Webhook<CheckList>), JsonRepository.Execute<IJsonWebhook>},
					{typeof (Webhook<List>), JsonRepository.Execute<IJsonWebhook>},
					{typeof (Webhook<Member>), JsonRepository.Execute<IJsonWebhook>},
					{typeof (Webhook<Organization>), JsonRepository.Execute<IJsonWebhook>},
					{typeof (IEnumerable<Action>), JsonRepository.Execute<List<IJsonAction>>},
					{typeof (IEnumerable<Attachment>), JsonRepository.Execute<List<IJsonAttachment>>},
					{typeof (IEnumerable<Board>), JsonRepository.Execute<List<IJsonBoard>>},
					{typeof (IEnumerable<BoardMembership>), JsonRepository.Execute<List<IJsonBoardMembership>>},
					{typeof (IEnumerable<Card>), JsonRepository.Execute<List<IJsonCard>>},
					{typeof (IEnumerable<CheckItem>), JsonRepository.Execute<List<IJsonCheckItem>>},
					{typeof (IEnumerable<CheckList>), JsonRepository.Execute<List<IJsonCheckList>>},
					{typeof (IEnumerable<Label>), JsonRepository.Execute<List<IJsonLabel>>},
					{typeof (IEnumerable<List>), JsonRepository.Execute<List<IJsonList>>},
					{typeof (IEnumerable<Member>), JsonRepository.Execute<List<IJsonMember>>},
					{typeof (IEnumerable<MemberSession>), JsonRepository.Execute<List<IJsonMemberSession>>},
					{typeof (IEnumerable<Notification>), JsonRepository.Execute<List<IJsonNotification>>},
					{typeof (IEnumerable<Organization>), JsonRepository.Execute<List<IJsonOrganization>>},
					{typeof (IEnumerable<OrganizationMembership>), JsonRepository.Execute<List<IJsonOrganizationMembership>>},
					{typeof (IEnumerable<Token>), JsonRepository.Execute<List<IJsonToken>>},
				};
			_applyJsonMethods = new Dictionary<Type, Action<ICache, ExpiringObject, object>>
				{
					{typeof (Action), ApplyJson<Action, IJsonAction>},
					{typeof (Attachment), ApplyJson<Attachment, IJsonAttachment>},
					{typeof (Board), ApplyJson<Board, IJsonBoard>},
					{typeof (BoardMembership), ApplyJson<BoardMembership, IJsonBoardMembership>},
					{typeof (Card), ApplyJson<Card, IJsonCard>},
					{typeof (CheckItem), ApplyJson<CheckItem, IJsonCheckItem>},
					{typeof (CheckList), ApplyJson<CheckList, IJsonCheckList>},
					{typeof (Label), ApplyJson<Label, IJsonLabel>},
					{typeof (List), ApplyJson<List, IJsonList>},
					{typeof (Member), ApplyJson<Member, IJsonMember>},
					{typeof (MemberSession), ApplyJson<MemberSession, IJsonMemberSession>},
					{typeof (Notification), ApplyJson<Notification, IJsonNotification>},
					{typeof (Organization), ApplyJson<Organization, IJsonOrganization>},
					{typeof (OrganizationMembership), ApplyJson<Organization, IJsonOrganizationMembership>},
					{typeof (Token), ApplyJson<Token, IJsonToken>},
				};
		}
		public EntityRepository(TimeSpan entityDuration, IValidator validator, TrelloAuthorization auth)
		{
			_entityDuration = entityDuration;
			_validator = validator;
			_auth = auth;
			_offlineChangeQueue = new OfflineChangeQueue();
			NetworkMonitor.ConnectionStatusChanged += NetworkStatusChanged;
		}

		public bool Refresh<T>(T entity, EntityRequestType request)
			where T : ExpiringObject
		{
			var endpoint = EndpointFactory.Build(request, entity.Parameters);
			var json = _repositoryMethods[typeof (T)](_auth, endpoint, entity.Parameters);
			entity.Parameters.Clear();
			if (json == null) return false;
			entity.ApplyJson(json);
			return true;
		}
		public bool RefreshCollection<T>(ExpiringObject obj, EntityRequestType request)
			where T : ExpiringObject, IEquatable<T>, IComparable<T>
		{
			var list = obj as ExpiringCollection<T>;
			if (list == null) return false;
			foreach (var parameter in RestParameterRepository.GetParameters<T>())
			{
				list.Parameters[parameter.Key] = parameter.Value;
			}
			var endpoint = EndpointFactory.Build(request, list.Parameters);
			var json = _repositoryMethods[typeof(IEnumerable<T>)](_auth, endpoint, list.Parameters);
			list.Parameters.Clear();
			if (json == null) return false;
			_applyJsonMethods[typeof(T)](TrelloServiceConfiguration.Cache, list, json);
			return true;
		}
		public T Download<T>(EntityRequestType request, IDictionary<string, object> parameters)
			where T : ExpiringObject
		{
			T entity = null;
			try
			{
				var id = parameters.SingleOrDefault(kvp => kvp.Key.In("_id")).Value;
				Func<T> query = () => DownloadFromSource<T>(request, parameters);
				entity = id == null ? query() : TrelloServiceConfiguration.Cache.Find(e => e.Matches(id.ToString()), query);
				entity.EntityRepository = this;
				entity.PropagateDependencies();
				return entity;
			}
			catch
			{
				TrelloServiceConfiguration.Cache.Remove(entity);
				throw;
			}
		}
		public IEnumerable<T> GenerateList<T>(ExpiringObject owner, EntityRequestType request, string filter, IDictionary<string, object> customParameters)
			where T : ExpiringObject, IEquatable<T>, IComparable<T>
		{
			return new ExpiringCollection<T>(owner, request) {Filter = filter, AdditionalParameters = customParameters};
		}
		public void Upload(EntityRequestType request, IDictionary<string, object> parameters)
		{
			var endpoint = EndpointFactory.Build(request, parameters);
			JsonRepository.Execute(_auth, endpoint, parameters);
			parameters.Clear();
		}
		public bool AllowSelfUpdate { get; set; }

		private static void ApplyJson<T, TJson>(ICache cache, ExpiringObject obj, object json)
			where T : ExpiringObject, IEquatable<T>, IComparable<T>
		{
			var list = obj as ExpiringCollection<T>;
			if (list == null) return;
			var jsonList = json as List<TJson>;
			if (jsonList == null) return;
			Func<TJson, T> createNew = j =>
				{
					var entity = EntityFactory.CreateEntity<T>();
					entity.Owner = list.Owner;
					entity.EntityRepository = list.EntityRepository;
					entity.Validator = list.Validator;
					entity.PropagateDependencies();
					entity.ApplyJson(j);
					return entity;
				};
			var found = jsonList.Select(je => new {Json = je, Entity = cache.Find<ExpiringObject>(e => e.EqualsJson(je))})
			                    .Where(map => map.Entity != null)
			                    .ToList();
			var entities = jsonList.Select(je => cache.Find(e => e.EqualsJson(je), () => createNew(je)))
			                       .ToList();
			foreach (var entity in found)
			{
				entity.Entity.ApplyJson(entity.Json);
			}

			list.Update(entities);
		}
		private T DownloadFromSource<T>(EntityRequestType request, IDictionary<string, object> parameters)
			where T : ExpiringObject
		{
			var endpoint = EndpointFactory.Build(request, parameters);
			var json = _repositoryMethods[typeof(T)](_auth, endpoint, parameters);
			var entity = EntityFactory.CreateEntity<T>();
			entity.Validator = _validator;
			entity.EntityRepository = this;
			entity.PropagateDependencies();
			if (json == null)
			{
				_offlineChangeQueue.Enqueue(entity, endpoint, parameters);
			}
			else
			{
				entity.ApplyJson(json);
			}
			parameters.Clear();
			return entity;
		}
		private void NetworkStatusChanged()
		{
			OfflineChange change;
			var failedChanges = new List<OfflineChange>();
			while ((change = _offlineChangeQueue.Dequeue()) != null)
			{
				var entity = change.Entity;
				var json = _repositoryMethods[entity.GetType()](_auth, change.Endpoint, change.Parameters);
				if (json == null)
				{
					failedChanges.Add(change);
				}
				else
				{
					var id = entity.Id;
					entity.ApplyJson(json);
					if (entity.Id != id)
						_offlineChangeQueue.ResolveId(id, entity.Id);
				}
			}
			_offlineChangeQueue.Requeue(failedChanges);
		}
	}
}
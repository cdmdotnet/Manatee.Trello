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
using Manatee.Trello.Contracts;
using Manatee.Trello.Internal.Genesis;
using Manatee.Trello.Internal.RequestProcessing;
using Manatee.Trello.Json;

namespace Manatee.Trello.Internal.DataAccess
{
	internal class EntityRepository : IEntityRepository
	{
		private static readonly Dictionary<Type, Func<IJsonRepository, Endpoint, IDictionary<string, object>, object>> _repositoryMethods;
		private static readonly Dictionary<Type, Action<IEntityFactory, ExpiringObject, object>> _applyJsonMethods;

		private readonly IJsonRepository _jsonRepository;
		private readonly IEndpointFactory _endpointFactory;
		private readonly IEntityFactory _entityFactory;
		private readonly IOfflineChangeQueue _offlineChangeQueue;
		private readonly TimeSpan _entityDuration;

		public TimeSpan EntityDuration { get { return _entityDuration; } }

		static EntityRepository()
		{
			_repositoryMethods = new Dictionary<Type, Func<IJsonRepository, Endpoint, IDictionary<string, object>, object>>
				{
					{typeof (Action), (r, e, d) => r.Execute<IJsonAction>(e, d)},
					{typeof (Attachment), (r, e, d) => r.Execute<IJsonAttachment>(e, d)},
					{typeof (Badges), (r, e, d) => r.Execute<IJsonBadges>(e, d)},
					{typeof (Board), (r, e, d) => r.Execute<IJsonBoard>(e, d)},
					{typeof (BoardPersonalPreferences), (r, e, d) => r.Execute<IJsonBoardPersonalPreferences>(e, d)},
					{typeof (BoardPreferences), (r, e, d) => r.Execute<IJsonBoardPreferences>(e, d)},
					{typeof (Card), (r, e, d) => r.Execute<IJsonCard>(e, d)},
					{typeof (CheckItem), (r, e, d) => r.Execute<IJsonCheckItem>(e, d)},
					{typeof (CheckList), (r, e, d) => r.Execute<IJsonCheckList>(e, d)},
					{typeof (Label), (r, e, d) => r.Execute<IJsonLabel>(e, d)},
					{typeof (LabelNames), (r, e, d) => r.Execute<IJsonLabelNames>(e, d)},
					{typeof (List), (r, e, d) => r.Execute<IJsonList>(e, d)},
					{typeof (Member), (r, e, d) => r.Execute<IJsonMember>(e, d)},
					{typeof (MemberPreferences), (r, e, d) => r.Execute<IJsonMemberPreferences>(e, d)},
					{typeof (MemberSession), (r, e, d) => r.Execute<IJsonMemberSession>(e, d)},
					{typeof (Notification), (r, e, d) => r.Execute<IJsonNotification>(e, d)},
					{typeof (Organization), (r, e, d) => r.Execute<IJsonOrganization>(e, d)},
					{typeof (SearchResults), (r, e, d) => r.Execute<IJsonSearchResults>(e, d)},
					{typeof (Token), (r, e, d) => r.Execute<IJsonToken>(e, d)},
					{typeof (Webhook<Board>), (r, e, d) => r.Execute<IJsonWebhook>(e, d)},
					{typeof (Webhook<Card>), (r, e, d) => r.Execute<IJsonWebhook>(e, d)},
					{typeof (Webhook<CheckItem>), (r, e, d) => r.Execute<IJsonWebhook>(e, d)},
					{typeof (Webhook<CheckList>), (r, e, d) => r.Execute<IJsonWebhook>(e, d)},
					{typeof (Webhook<List>), (r, e, d) => r.Execute<IJsonWebhook>(e, d)},
					{typeof (Webhook<Member>), (r, e, d) => r.Execute<IJsonWebhook>(e, d)},
					{typeof (Webhook<Organization>), (r, e, d) => r.Execute<IJsonWebhook>(e, d)},
					{typeof (IEnumerable<Action>), (r, e, d) => r.Execute<List<IJsonAction>>(e, d)},
					{typeof (IEnumerable<Attachment>), (r, e, d) => r.Execute<List<IJsonAttachment>>(e, d)},
					{typeof (IEnumerable<Board>), (r, e, d) => r.Execute<List<IJsonBoard>>(e, d)},
					{typeof (IEnumerable<BoardMembership>), (r, e, d) => r.Execute<List<IJsonBoardMembership>>(e, d)},
					{typeof (IEnumerable<Card>), (r, e, d) => r.Execute<List<IJsonCard>>(e, d)},
					{typeof (IEnumerable<CheckItem>), (r, e, d) => r.Execute<List<IJsonCheckItem>>(e, d)},
					{typeof (IEnumerable<CheckList>), (r, e, d) => r.Execute<List<IJsonCheckList>>(e, d)},
					{typeof (IEnumerable<Label>), (r, e, d) => r.Execute<List<IJsonLabel>>(e, d)},
					{typeof (IEnumerable<List>), (r, e, d) => r.Execute<List<IJsonList>>(e, d)},
					{typeof (IEnumerable<Member>), (r, e, d) => r.Execute<List<IJsonMember>>(e, d)},
					{typeof (IEnumerable<MemberSession>), (r, e, d) => r.Execute<List<IJsonMemberSession>>(e, d)},
					{typeof (IEnumerable<Notification>), (r, e, d) => r.Execute<List<IJsonNotification>>(e, d)},
					{typeof (IEnumerable<Organization>), (r, e, d) => r.Execute<List<IJsonOrganization>>(e, d)},
					{typeof (IEnumerable<OrganizationMembership>), (r, e, d) => r.Execute<List<IJsonOrganizationMembership>>(e, d)},
					{typeof (IEnumerable<Token>), (r, e, d) => r.Execute<List<IJsonToken>>(e, d)},
				};
			_applyJsonMethods = new Dictionary<Type, Action<IEntityFactory, ExpiringObject, object>>
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
		public EntityRepository(IJsonRepository jsonRepository, IEndpointFactory endpointFactory, IEntityFactory entityFactory,
								IOfflineChangeQueue offlineChangeQueue, TimeSpan entityDuration)
		{
			_jsonRepository = jsonRepository;
			_endpointFactory = endpointFactory;
			_entityFactory = entityFactory;
			_offlineChangeQueue = offlineChangeQueue;
			_entityDuration = entityDuration;
		}

		public bool Refresh<T>(T entity, EntityRequestType request)
			where T : ExpiringObject
		{
			var endpoint = _endpointFactory.Build(request, entity.Parameters);
			var json = _repositoryMethods[typeof (T)](_jsonRepository, endpoint, entity.Parameters);
			entity.Parameters.Clear();
			if (json == null) return false;
			entity.ApplyJson(json);
			return true;
		}
		public bool RefreshCollection<T>(ExpiringObject obj, EntityRequestType request)
			where T : ExpiringObject, IEquatable<T>, IComparable<T>
		{
			var list = obj as ExpiringList<T>;
			if (list == null) return false;
			foreach (var parameter in RestParameterRepository.GetParameters<T>())
			{
				list.Parameters[parameter.Key] = parameter.Value;
			}
			var endpoint = _endpointFactory.Build(request, list.Parameters);
			var json = _repositoryMethods[typeof(IEnumerable<T>)](_jsonRepository, endpoint, list.Parameters);
			list.Parameters.Clear();
			if (json == null) return false;
			_applyJsonMethods[typeof(T)](_entityFactory, list, json);
			return true;
		}
		public T Download<T>(EntityRequestType request, IDictionary<string, object> parameters)
			where T : ExpiringObject
		{
			var endpoint = _endpointFactory.Build(request, parameters);
			var json = _repositoryMethods[typeof(T)](_jsonRepository, endpoint, parameters);
			var entity = _entityFactory.CreateEntity<T>();
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
		public IEnumerable<T> GenerateList<T>(ExpiringObject owner, EntityRequestType request, string filter, string fields)
			where T : ExpiringObject, IEquatable<T>, IComparable<T>
		{
			return new ExpiringList<T>(owner, request) {Fields = fields, Filter = filter};
		}
		public void Upload(EntityRequestType request, IDictionary<string, object> parameters)
		{
			var endpoint = _endpointFactory.Build(request, parameters);
			_jsonRepository.Execute<object>(endpoint, parameters);
			parameters.Clear();
		}
		public void NetworkStatusChanged(object sender, EventArgs e)
		{
			OfflineChange change;
			var failedChanges = new List<OfflineChange>();
			while ((change = _offlineChangeQueue.Dequeue()) != null)
			{
				var entity = change.Entity;
				var json = _repositoryMethods[entity.GetType()](_jsonRepository, change.Endpoint, change.Parameters);
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
		public bool AllowSelfUpdate { get; set; }

		private static void ApplyJson<T, TJson>(IEntityFactory entityFactory, ExpiringObject obj, object json)
			where T : ExpiringObject, IEquatable<T>, IComparable<T>
		{
			var list = obj as ExpiringList<T>;
			if (list == null) return;
			var jsonList = json as List<TJson>;
			if (jsonList == null) return;
			var entities = new List<T>();
			foreach (var jsonEntity in jsonList)
			{
				var entity = entityFactory.CreateEntity<T>();
				entity.Owner = list.Owner;
				entity.EntityRepository = list.EntityRepository;
				entity.PropagateDependencies();
				entity.ApplyJson(jsonEntity);
				entities.Add(entity);
			}
			list.Update(entities);
		}
	}
}
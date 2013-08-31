/***************************************************************************************

	Copyright 2012 Greg Dennis

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
using Manatee.Trello.Json;
using Manatee.Trello.Rest;

namespace Manatee.Trello.Internal.DataAccess
{
	internal class EntityRepository : IEntityRepository
	{
		private static readonly Dictionary<Type, Func<IJsonRepository, Endpoint, IDictionary<string, object>, object>> _repositoryMethods;
		private static readonly Dictionary<Type, Action<IEntityFactory, ExpiringObject, object>> _applyJsonMethods;

		private readonly IJsonRepository _jsonRepository;
		private readonly IEndpointFactory _endpointFactory;
		private readonly IEntityFactory _entityFactory;
		private readonly TimeSpan _entityDuration;

		public TimeSpan EntityDuration { get { return _entityDuration; } }

		static EntityRepository()
		{
			_repositoryMethods = new Dictionary<Type, Func<IJsonRepository, Endpoint, IDictionary<string, object>, object>>
				{
					{typeof (Action), Call<IJsonAction>},
					{typeof (Attachment), Call<IJsonAttachment>},
					{typeof (Badges), Call<IJsonBadges>},
					{typeof (Board), Call<IJsonBoard>},
					{typeof (BoardPersonalPreferences), Call<IJsonBoardPersonalPreferences>},
					{typeof (BoardPreferences), Call<IJsonBoardPreferences>},
					{typeof (Card), Call<IJsonCard>},
					{typeof (CheckItem), Call<IJsonCheckItem>},
					{typeof (CheckList), Call<IJsonCheckList>},
					{typeof (Label), Call<IJsonLabel>},
					{typeof (LabelNames), Call<IJsonLabelNames>},
					{typeof (List), Call<IJsonList>},
					{typeof (Member), Call<IJsonMember>},
					{typeof (MemberPreferences), Call<IJsonMemberPreferences>},
					{typeof (MemberSession), Call<IJsonMemberSession>},
					{typeof (Notification), Call<IJsonNotification>},
					{typeof (Organization), Call<IJsonOrganization>},
					{typeof (SearchResults), Call<IJsonSearchResults>},
					{typeof (Token), Call<IJsonToken>},
					{typeof (IEnumerable<Action>), Call<List<IJsonAction>>},
					{typeof (IEnumerable<Attachment>), Call<List<IJsonAttachment>>},
					{typeof (IEnumerable<Board>), Call<List<IJsonBoard>>},
					{typeof (IEnumerable<Card>), Call<List<IJsonCard>>},
					{typeof (IEnumerable<CheckItem>), Call<List<IJsonCheckItem>>},
					{typeof (IEnumerable<CheckList>), Call<List<IJsonCheckList>>},
					{typeof (IEnumerable<Label>), Call<List<IJsonLabel>>},
					{typeof (IEnumerable<List>), Call<List<IJsonList>>},
					{typeof (IEnumerable<Member>), Call<List<IJsonMember>>},
					{typeof (IEnumerable<MemberSession>), Call<List<IJsonMemberSession>>},
					{typeof (IEnumerable<Notification>), Call<List<IJsonNotification>>},
					{typeof (IEnumerable<Organization>), Call<List<IJsonOrganization>>},
					{typeof (IEnumerable<Token>), Call<List<IJsonToken>>},
				};
			_applyJsonMethods = new Dictionary<Type, Action<IEntityFactory, ExpiringObject, object>>
				{
					{typeof (Action), ApplyJson<Action, IJsonAction>},
					{typeof (Attachment), ApplyJson<Attachment, IJsonAttachment>},
					{typeof (Board), ApplyJson<Board, IJsonBoard>},
					{typeof (Card), ApplyJson<Card, IJsonCard>},
					{typeof (CheckItem), ApplyJson<CheckItem, IJsonCheckItem>},
					{typeof (CheckList), ApplyJson<CheckList, IJsonCheckList>},
					{typeof (Label), ApplyJson<Label, IJsonLabel>},
					{typeof (List), ApplyJson<List, IJsonList>},
					{typeof (Member), ApplyJson<Member, IJsonMember>},
					{typeof (MemberSession), ApplyJson<MemberSession, IJsonMemberSession>},
					{typeof (Notification), ApplyJson<Notification, IJsonNotification>},
					{typeof (Organization), ApplyJson<Organization, IJsonOrganization>},
					{typeof (Token), ApplyJson<Token, IJsonToken>},
				};
		}
		public EntityRepository(IJsonRepository jsonRepository, IEndpointFactory endpointFactory, IEntityFactory entityFactory, TimeSpan entityDuration)
		{
			_jsonRepository = jsonRepository;
			_endpointFactory = endpointFactory;
			_entityFactory = entityFactory;
			_entityDuration = entityDuration;
		}

		public void Refresh<T>(T entity, EntityRequestType request)
			where T : ExpiringObject
		{
			var endpoint = _endpointFactory.Build(request, entity.Parameters);
			var json = _repositoryMethods[typeof (T)](_jsonRepository, endpoint, entity.Parameters);
			entity.ApplyJson(json);
			entity.Parameters.Clear();
		}
		public void RefreshCollecion<T>(ExpiringObject obj, EntityRequestType request, IDictionary<string, object> parameters)
			where T : ExpiringObject, IEquatable<T>, IComparable<T>
		{
			var list = obj as ExpiringList<T>;
			foreach (var parameter in RestParameterRepository.GetParameters<T>())
			{
				list.Parameters.Add(parameter.Key, parameter.Value);
			}
			var endpoint = _endpointFactory.Build(request, parameters);
			var json = _repositoryMethods[typeof(IEnumerable<T>)](_jsonRepository, endpoint, parameters);
			parameters.Clear();
			_applyJsonMethods[typeof (T)](_entityFactory, list, json);
		}
		public T Download<T>(EntityRequestType request, IDictionary<string, object> parameters)
			where T : ExpiringObject
		{
			var endpoint = _endpointFactory.Build(request, parameters);
			var json = _repositoryMethods[typeof(T)](_jsonRepository, endpoint, parameters);
			var entity = _entityFactory.CreateEntity<T>();
			entity.EntityRepository = this;
			entity.PropagateDependencies();
			entity.ApplyJson(json);
			entity.ForceNotExpired();
			parameters.Clear();
			return entity;
		}
		public void Upload(EntityRequestType request, IDictionary<string, object> parameters)
		{
			var endpoint = _endpointFactory.Build(request, parameters);
			Call<object>(_jsonRepository, endpoint, parameters);
			parameters.Clear();
		}

		private static T Call<T>(IJsonRepository repository, Endpoint endpoint, IDictionary<string, object> parameters)
			where T : class
		{
			switch (endpoint.Method)
			{
				case RestMethod.Get:
					return repository.Get<T>(endpoint.ToString(), parameters);
				case RestMethod.Put:
					return repository.Put<T>(endpoint.ToString(), parameters);
				case RestMethod.Post:
					return repository.Post<T>(endpoint.ToString(), parameters);
				case RestMethod.Delete:
					return repository.Delete<T>(endpoint.ToString());
				default:
					throw new ArgumentOutOfRangeException("endpoint.Method");
			}
		}
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
				//entity.ForceNotExpired();
				entities.Add(entity);
			}
			list.Update(entities);
		}
	}
}
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
 
	File Name:		EntityFactory.cs
	Namespace:		Manatee.Trello.Internal
	Class Name:		EntityFactory
	Purpose:		Creates entities given a JSON entity type.

***************************************************************************************/

using System;
using System.Collections.Generic;
using Manatee.Trello.Contracts;
using Manatee.Trello.Internal.DataAccess;
using Manatee.Trello.Json;

namespace Manatee.Trello.Internal.Genesis
{
	internal class EntityFactory : IEntityFactory
	{
		private readonly ITrelloService _svc;
		private readonly IValidator _validator;
		private readonly IJsonRepository _entityRepository;
		private readonly Dictionary<Type, Func<ExpiringObject>> _map;

		public EntityFactory(ITrelloService svc, IValidator validator, IJsonRepository entityRepository)
		{
			if (validator == null) throw new ArgumentNullException("validator");
			_validator = validator;
			_entityRepository = entityRepository;
			_validator.ArgumentNotNull(svc, "svc");
			_svc = svc;

			_map = new Dictionary<Type, Func<ExpiringObject>>
				{
					{typeof (IJsonAction), () => InitializeEntity(new Action())},
					{typeof (IJsonAttachment), () => InitializeEntity(new Attachment())},
					{typeof (IJsonBadges), () => InitializeEntity(new Badges())},
					{typeof (IJsonBoard), () => InitializeEntity(new Board())},
					{typeof (IJsonBoardMembership), () => InitializeEntity(new BoardMembership())},
					{typeof (IJsonBoardPersonalPreferences), () => InitializeEntity(new BoardPersonalPreferences())},
					{typeof (IJsonBoardPreferences), () => InitializeEntity(new BoardPreferences())},
					{typeof (IJsonCard), () => InitializeEntity(new Card())},
					{typeof (IJsonCheckItem), () => InitializeEntity(new CheckItem())},
					{typeof (IJsonCheckList), () => InitializeEntity(new CheckList())},
					{typeof (IJsonLabel), () => InitializeEntity(new Label())},
					{typeof (IJsonLabelNames), () => InitializeEntity(new LabelNames())},
					{typeof (IJsonList), () => InitializeEntity(new List())},
					{typeof (IJsonMember), () => InitializeEntity(new Member())},
					{typeof (IJsonMemberPreferences), () => InitializeEntity(new MemberPreferences())},
					{typeof (IJsonMemberSession), () => InitializeEntity(new MemberSession())},
					{typeof (IJsonNotification), () => InitializeEntity(new Notification())},
					{typeof (IJsonOrganization), () => InitializeEntity(new Organization())},
					{typeof (IJsonOrganizationMembership), () => InitializeEntity(new OrganizationMembership())},
					{typeof (IJsonOrganizationPreferences), () => InitializeEntity(new OrganizationPreferences())},

					{typeof (Action), () => InitializeEntity(new Action())},
					{typeof (Attachment), () => InitializeEntity(new Attachment())},
					{typeof (Badges), () => InitializeEntity(new Badges())},
					{typeof (Board), () => InitializeEntity(new Board())},
					{typeof (BoardMembership), () => InitializeEntity(new BoardMembership())},
					{typeof (BoardPersonalPreferences), () => InitializeEntity(new BoardPersonalPreferences())},
					{typeof (BoardPreferences), () => InitializeEntity(new BoardPreferences())},
					{typeof (Card), () => InitializeEntity(new Card())},
					{typeof (CheckItem), () => InitializeEntity(new CheckItem())},
					{typeof (CheckList), () => InitializeEntity(new CheckList())},
					{typeof (Label), () => InitializeEntity(new Label())},
					{typeof (LabelNames), () => InitializeEntity(new LabelNames())},
					{typeof (List), () => InitializeEntity(new List())},
					{typeof (Member), () => InitializeEntity(new Member())},
					{typeof (MemberPreferences), () => InitializeEntity(new MemberPreferences())},
					{typeof (MemberSession), () => InitializeEntity(new MemberSession())},
					{typeof (Notification), () => InitializeEntity(new Notification())},
					{typeof (Organization), () => InitializeEntity(new Organization())},
					{typeof (OrganizationMembership), () => InitializeEntity(new OrganizationMembership())},
					{typeof (OrganizationPreferences), () => InitializeEntity(new OrganizationPreferences())},
				};
		}

		public ExpiringObject CreateEntity(Type jsonType)
		{
			if (!_map.ContainsKey(jsonType)) return null;
			var entity = _map[jsonType]();
			return entity;
		}
		public T CreateEntity<T>(string id)
			where T : ExpiringObject
		{
			T entity;
			if (typeof (T).IsAssignableFrom(typeof (Token)))
			{
				entity = new Token(id) {Svc = _svc} as T;
			}
			else entity = _map[typeof (T)]() as T;
			entity.Id = id;
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

		private ExpiringObject InitializeEntity(ExpiringObject entity)
		{
			entity.Validator = _validator;
			entity.JsonRepository = _entityRepository;
			entity.Svc = _svc;
			return entity;
		}
	}
}
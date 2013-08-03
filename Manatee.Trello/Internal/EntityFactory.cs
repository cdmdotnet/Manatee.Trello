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
using Manatee.Trello.Json;

namespace Manatee.Trello.Internal
{
	internal class EntityFactory : IEntityFactory
	{
		private static readonly Dictionary<Type, Func<ExpiringObject>> _map;

		static EntityFactory()
		{
			_map = new Dictionary<Type, Func<ExpiringObject>>
				{
					{typeof (IJsonAction), () => new Action()},
					{typeof (IJsonAttachment), () => new Attachment()},
					{typeof (IJsonBadges), () => new Badges()},
					{typeof (IJsonBoard), () => new Board()},
					{typeof (IJsonBoardMembership), () => new BoardMembership()},
					{typeof (IJsonBoardPersonalPreferences), () => new BoardPersonalPreferences()},
					{typeof (IJsonBoardPreferences), () => new BoardPreferences()},
					{typeof (IJsonCard), () => new Card()},
					{typeof (IJsonCheckItem), () => new CheckItem()},
					{typeof (IJsonCheckList), () => new CheckList()},
					{typeof (IJsonLabel), () => new Label()},
					{typeof (IJsonLabelNames), () => new LabelNames()},
					{typeof (IJsonList), () => new List()},
					{typeof (IJsonMember), () => new Member()},
					{typeof (IJsonMemberPreferences), () => new MemberPreferences()},
					{typeof (IJsonMemberSession), () => new MemberSession()},
					{typeof (IJsonNotification), () => new Notification()},
					{typeof (IJsonOrganization), () => new Organization()},
					{typeof (IJsonOrganizationMembership), () => new OrganizationMembership()},
					{typeof (IJsonOrganizationPreferences), () => new OrganizationPreferences()},
					{typeof (IJsonToken), () => new Token()}
				};
		}

		public ExpiringObject CreateEntity(Type jsonType)
		{
			if (!_map.ContainsKey(jsonType)) return null;
			var entity = _map[jsonType]();
			return entity;
		}

	}
}
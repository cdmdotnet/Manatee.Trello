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
 
	File Name:		EntityFactory.cs
	Namespace:		Manatee.Trello.Internal.Genesis
	Class Name:		EntityFactory
	Purpose:		Creates entities given a JSON entity type.

***************************************************************************************/

using System;
using System.Collections.Generic;
using Manatee.Trello.Contracts;

namespace Manatee.Trello.Internal.Genesis
{
	internal static class EntityFactory
	{
		private static readonly Dictionary<Type, Func<ExpiringObject>> _map;

		static EntityFactory()
		{
			_map = new Dictionary<Type, Func<ExpiringObject>>
				{
					{typeof (Action), () => new Action()},
					{typeof (Attachment), () => new Attachment()},
					{typeof (Badges), () => new Badges()},
					{typeof (Board), () => new Board()},
					{typeof (BoardMembership), () => new BoardMembership()},
					{typeof (BoardPersonalPreferences), () => new BoardPersonalPreferences()},
					{typeof (BoardPreferences), () => new BoardPreferences()},
					{typeof (Card), () => new Card()},
					{typeof (CheckItem), () => new CheckItem()},
					{typeof (CheckList), () => new CheckList()},
					{typeof (Label), () => new Label()},
					{typeof (LabelNames), () => new LabelNames()},
					{typeof (List), () => new List()},
					{typeof (Me), () => new Me()},
					{typeof (Member), () => new Member()},
					{typeof (MemberPreferences), () => new MemberPreferences()},
					{typeof (MemberSession), () => new MemberSession()},
					{typeof (Notification), () => new Notification()},
					{typeof (Organization), () => new Organization()},
					{typeof (OrganizationMembership), () => new OrganizationMembership()},
					{typeof (OrganizationPreferences), () => new OrganizationPreferences()},
					{typeof (SearchResults), () => new SearchResults()},
					{typeof (Token), () => new Token()},
					{typeof (Webhook<Board>), () => new Webhook<Board>()},
					{typeof (Webhook<Card>), () => new Webhook<Card>()},
					{typeof (Webhook<CheckItem>), () => new Webhook<CheckItem>()},
					{typeof (Webhook<CheckList>), () => new Webhook<CheckList>()},
					{typeof (Webhook<List>), () => new Webhook<List>()},
					{typeof (Webhook<Member>), () => new Webhook<Member>()},
					{typeof (Webhook<Organization>), () => new Webhook<Organization>()},
				};
		}

		public static T CreateEntity<T>()
			where T : ExpiringObject
		{
			T entity = _map[typeof(T)]() as T;
			return entity;
		}
	}
}
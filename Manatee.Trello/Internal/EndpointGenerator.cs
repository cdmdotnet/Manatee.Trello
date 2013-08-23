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
 
	File Name:		EndpointGenerator.cs
	Namespace:		Manatee.Trello.Internal
	Class Name:		EndpointGenerator
	Purpose:		Creates the request string based on a list of objects
					submitted to it.

***************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using Manatee.Trello.Contracts;

namespace Manatee.Trello.Internal
{
	internal class EndpointGenerator
	{
		private static readonly EndpointGenerator _default;
		private static readonly Dictionary<Type, string> _primaryKeys;
		private static readonly Dictionary<Type, string> _secondaryKeys;
		private static readonly Dictionary<Type, Func<ExpiringObject, Endpoint>> _generators;

		public static EndpointGenerator Default { get { return _default; } }

		static EndpointGenerator()
		{
			_default = new EndpointGenerator();
			_primaryKeys = new Dictionary<Type, string>
				{
					{typeof (Action), "actions"},
					{typeof (Attachment), "attachments"},
					{typeof (Badges), "badges"},
					{typeof (Board), "boards"},
					{typeof (BoardInvitation), "boardsInvited"},
					{typeof (BoardMembership), "memberships"},
					{typeof (BoardPersonalPreferences), "myPrefs"},
					{typeof (BoardPreferences), "prefs"},
					{typeof (Card), "cards"},
					{typeof (CheckItem), "checkItems"},
					{typeof (CheckList), "checklists"},
					{typeof (InvitedMember), "membersInvited"},
					{typeof (Label), "labels"},
					{typeof (LabelNames), "labelNames"},
					{typeof (List), "lists"},
					{typeof (Member), "members"},
					{typeof (MemberPreferences), "prefs"},
					{typeof (MemberSession), "sessions"},
					{typeof (Notification), "notifications"},
					{typeof (Organization), "organizations"},
					{typeof (OrganizationInvitation), "organizationsInvited"},
					{typeof (OrganizationMembership), "memberships"},
					{typeof (OrganizationPreferences), "prefs"},
					{typeof (PinnedBoard), "idBoardsPinned"},
					{typeof (PremiumOrganization), "idPremOrgsAdmin"},
					{typeof (VotingMember), "membersVoted"},
				};
			_secondaryKeys = new Dictionary<Type, string>
				{
					{typeof (Action), "actions"},
					{typeof (Attachment), "attachments"},
					{typeof (Badges), "badges"},
					{typeof (Board), "idBoard"},
					{typeof (BoardInvitation), "boardsInvited"},
					{typeof (BoardMembership), "memberships"},
					{typeof (BoardPersonalPreferences), "myPrefs"},
					{typeof (BoardPreferences), "prefs"},
					{typeof (Card), "idCard"},
					{typeof (CheckItem), "checkItems"},
					{typeof (CheckList), "checklists"},
					{typeof (InvitedMember), "membersInvited"},
					{typeof (Label), "labels"},
					{typeof (LabelNames), "labelNames"},
					{typeof (List), "idList"},
					{typeof (Member), "idMembers"},
					{typeof (MemberPreferences), "prefs"},
					{typeof (MemberSession), "sessions"},
					{typeof (Notification), "notifications"},
					{typeof (Organization), "organizations"},
					{typeof (OrganizationInvitation), "organizationsInvited"},
					{typeof (OrganizationMembership), "memberships"},
					{typeof (OrganizationPreferences), "prefs"},
					{typeof (PinnedBoard), "idBoardsPinned"},
					{typeof (PremiumOrganization), "idPremOrgsAdmin"},
					{typeof (VotingMember), "membersVoted"},
				};
			_generators = new Dictionary<Type, Func<ExpiringObject, Endpoint>>
				{
					{typeof (Action), GetBasicEndpoint},
					{typeof (Attachment), GetOwnerEndpoint},
					{typeof (Badges), GetOwnerEndpoint},
					{typeof (Board), GetBasicEndpoint},
					{typeof (BoardInvitation), GetOwnerEndpoint},
					{typeof (BoardMembership), GetOwnerEndpoint},
					{typeof (BoardPersonalPreferences), GetOwnerEndpoint},
					{typeof (BoardPreferences), GetOwnerEndpoint},
					{typeof (Card), GetBasicEndpoint},
					{typeof (CheckItem), GetOwnerEndpoint},
					{typeof (CheckList), GetBasicEndpoint},
					{typeof (InvitedMember), GetOwnerEndpoint},
					{typeof (Label), GetOwnerEndpoint},
					{typeof (LabelNames), GetOwnerEndpoint},
					{typeof (List), GetBasicEndpoint},
					{typeof (Member), GetBasicEndpoint},
					{typeof (MemberPreferences), GetOwnerEndpoint},
					{typeof (MemberSession), GetOwnerEndpoint},
					{typeof (Notification), GetBasicEndpoint},
					{typeof (Organization), GetBasicEndpoint},
					{typeof (OrganizationInvitation), GetOwnerEndpoint},
					{typeof (OrganizationMembership), GetOwnerEndpoint},
					{typeof (OrganizationPreferences), GetOwnerEndpoint},
					{typeof (PinnedBoard), GetOwnerEndpoint},
					{typeof (PremiumOrganization), GetOwnerEndpoint},
					{typeof (VotingMember), GetOwnerEndpoint},
				};
		}
		private EndpointGenerator() {}

		public Endpoint Generate<T>(T obj)
			where T : ExpiringObject
		{
			var type = typeof (T);
			if (!_generators.ContainsKey(type))
				throw new Exception(string.Format("Type {0} is not properly mapped in the EndpointGenerator.  Please contact administrator for support.", type.Name));
			return _generators[type](obj);
		}
		public Endpoint GenerateForList<T>(ExpiringObject owner)
			where T : ExpiringObject
		{
			return new Endpoint(GetPrimaryKey(owner), owner.Id, GetPrimaryKey(typeof(T)));
		}
		public Endpoint Generate(params ExpiringObject[] tokens)
		{
			var segments = new List<string>();
			foreach (var token in tokens)
			{
				segments.Add(token.PrimaryKey);
				if (token.KeyId != null)
					segments.Add(token.KeyId);
			}
			return new Endpoint(segments);
		}
		public Endpoint Generate2(ExpiringObject first, params ExpiringObject[] tokens)
		{
			var segments = new List<string> {first.PrimaryKey};
			if (first.KeyId != null)
				segments.Add(first.KeyId);
			foreach (var token in tokens)
			{
				segments.Add(token.SecondaryKey);
				if (token.Id != null)
					segments.Add(token.Id);
			}
			return new Endpoint(segments);
		}

		private static string GetPrimaryKey(ExpiringObject obj)
		{
			return _primaryKeys[obj.GetType()];
		}
		private static string GetPrimaryKey(Type type)
		{
			return _primaryKeys[type];
		}
		private static string GetSecondaryKey(ExpiringObject obj)
		{
			return _secondaryKeys[obj.GetType()];
		}
		private static Endpoint GetBasicEndpoint(ExpiringObject obj)
		{
			return new Endpoint(GetPrimaryKey(obj), obj.Id);
		}
		private static Endpoint GetOwnerEndpoint(ExpiringObject obj)
		{
			return new Endpoint(GetPrimaryKey(obj.Owner), obj.Owner.Id, GetSecondaryKey(obj), obj.Id);
		}
	}
}

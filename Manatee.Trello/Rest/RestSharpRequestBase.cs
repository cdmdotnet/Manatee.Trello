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
 
	File Name:		RequestBase.cs
	Namespace:		Manatee.Trello.Rest
	Class Name:		RequestBase
	Purpose:		Base class for RESTful requests.

***************************************************************************************/
using System;
using System.Collections.Generic;
using Manatee.Trello.Implementation;
using RestSharp;

namespace Manatee.Trello.Rest
{
	internal abstract class RestSharpRequestBase : RestRequest
	{
		protected static readonly Dictionary<Type, string> SectionStrings =
			new Dictionary<Type, string>
				{
					{typeof (Action), "actions"},
					{typeof (ActionData), "data"},
					{typeof (Attachment), "attachments"},
					{typeof (Badges), "badges"},
					{typeof (Board), "boards"},
					{typeof (BoardMembership), "memberships"},
					{typeof (BoardPersonalPreferences), "myPrefs"},
					{typeof (BoardPreferences), "prefs"},
					{typeof (Card), "cards"},
					{typeof (CheckItem), "checkItems"},
					{typeof (CheckItemState), "checkItemStates"},
					{typeof (CheckList), "checklists"},
					{typeof (InvitedBoard), "idBoardsInvited"},
					{typeof (InvitedOrganization), "idOrganizationsInvited"},
					{typeof (Label), "labels"},
					{typeof (LabelNames), "labelNames"},
					{typeof (List), "lists"},
					{typeof (Member), "members"},
					{typeof (MemberPreferences), "prefs"},
					{typeof (Notification), "notifications"},
					{typeof (Organization), "organizations"},
					{typeof (OrganizationPreferences), "prefs"},
					{typeof (PinnedBoard), "idBoardsPinned"},
					{typeof (PremiumOrganization), "idPremOrgsAdmin"},
					{typeof (VotingMember), "membersVoted"},
				};

		protected RestSharpRequestBase(string path) : base(path)
		{
			RequestFormat = DataFormat.Json;
			DateFormat = "yyyy-MM-ddTHH:mm:ss.fffZ";
		}
	}
}

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
 
	File Name:		RestParameterRepository.cs
	Namespace:		Manatee.Trello.Internal.DataAccess
	Class Name:		RestParameterRepository
	Purpose:		Manages default REST parameters for all entity types.

***************************************************************************************/

using System;
using System.Collections.Generic;
using Manatee.Trello.Contracts;

namespace Manatee.Trello.Internal.DataAccess
{
	internal static class RestParameterRepository
	{
		private static readonly Dictionary<Type, Dictionary<string, string>> _parameterSets;

		static RestParameterRepository()
		{
			_parameterSets = new Dictionary<Type, Dictionary<string, string>>
				{
					{typeof (Action), new Dictionary<string, string>
						{
							{"fields", "idMemberCreator,data,type,date"},
							{"entities", "false"},
							{"memberCreator", "false"},
							{"member", "false"},
						}},
					{typeof(Board), new Dictionary<string, string>
						{
							{"fields", "name,desc,closed,idOrganization,pinned,url,subscribed"},
							{"actions", "none"},
							{"cards", "none"},
							{"lists", "none"},
							{"members", "none"},
							{"checklists", "none"},
							{"organization", "false"},
							{"myPrefs", "false"},
						}},
					{typeof(Card), new Dictionary<string, string>
						{
							{"fields", "closed,dateLastActivity,desc,due,idBoard,idList,idShort,idAttachmentCover,manualCoverAttachment,name,pos,url,subscribed"},
							{"actions", "none"},
							{"attachments", "false"},
							{"badges", "false"},
							{"members", "false"},
							{"membersVoted", "false"},
							{"checkItemStates", "false"},
							{"checkLists", "false"},
							{"board", "false"},
							{"list", "false"},
						}},
					{typeof(CheckList), new Dictionary<string, string>
						{
							{"fields", "name,idBoard,idCard,pos"},
							{"cards", "none"},
							{"checkItems", "none"},
						}},
					{typeof(List), new Dictionary<string, string>
						{
							{"fields", "name,closed,idBoard,pos,subscribed"},
							{"cards", "none"},
						}},
					{typeof(Member), new Dictionary<string, string>
						{
							{"fields", "avatarHash,bio,fullName,initials,memberType,status,url,username,avatarSource,confirmed,email,gravatarHash,loginTypes,newEmail,oneTimeMessagesDismissed,status,trophies,uploadedAvatarHash"},
							{"actions", "none"},
							{"cards", "none"},
							{"boards", "none"},
							{"boardsInvited", "none"},
							{"organizations", "none"},
							{"organizationsInvited", "none"},
							{"notifications", "none"},
							{"tokens", "none"},
						}},
					{typeof(Notification), new Dictionary<string, string>
						{
							{"fields", "unread,type,date,data,idMemberCreator"},
							{"entities", "false"},
							{"memberCreator", "false"},
							{"board", "false"},
							{"list", "false"},
							{"card", "false"},
							{"organization", "false"},
							{"member", "false"},
						}},
					{typeof(Organization), new Dictionary<string, string>
						{
							{"fields", "name,displayName,desc,invited,powerUps,url,website,logoHash,premiumFeatures"},
							{"paid_account", "true"},
							{"actions", "none"},
							{"members", "none"},
							{"membersInvited", "none"},
							{"boards", "none"},
							{"memberships", "none"},
						}},
					{typeof(SearchResults), new Dictionary<string, string>
						{
							{"action_fields", "id"},
							{"board_fields", "id"},
							{"card_fields", "id"},
							{"member_fields", "id"},
							{"organization_fields", "id"},
						}},
				};
		}

		public static Dictionary<string, string> GetParameters<T>()
			where T : ExpiringObject
		{
			return GetParameters(typeof (T));
		}
		public static Dictionary<string, string> GetParameters(Type type)
		{
			return _parameterSets.ContainsKey(type)
					   ? _parameterSets[type]
					   : new Dictionary<string, string>();
		}
	}
}
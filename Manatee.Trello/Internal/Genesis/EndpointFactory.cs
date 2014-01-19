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
 
	File Name:		EndpointFactory.cs
	Namespace:		Manatee.Trello.Internal.Genesis
	Class Name:		EndpointFactory
	Purpose:		Implements IEndpointFactory.

***************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using Manatee.Trello.Rest;

namespace Manatee.Trello.Internal.Genesis
{
	internal class EndpointFactory : IEndpointFactory
	{
		private static readonly Dictionary<EntityRequestType, Func<Endpoint>> _library;
		private static readonly Dictionary<Type, EntityRequestType> _refreshRequestTypeMap;

		static EndpointFactory()
		{
			_library = new Dictionary<EntityRequestType, Func<Endpoint>>
				{
					{EntityRequestType.Action_Read_Refresh, () => new Endpoint(RestMethod.Get, new[]{"actions","_id"})},
					{EntityRequestType.Action_Write_Delete, () => new Endpoint(RestMethod.Delete, new[]{"actions","_id"})},
					{EntityRequestType.Attachment_Read_Refresh, () => new Endpoint(RestMethod.Get, new[]{"cards","_cardId","attachments","_id"})},
					{EntityRequestType.Attachment_Write_Delete, () => new Endpoint(RestMethod.Delete, new[]{"cards","_cardId","attachments","_id"})},
					{EntityRequestType.Badges_Read_Refresh, () => new Endpoint(RestMethod.Get, new[]{"cards","_cardId","badges"})},
					{EntityRequestType.Board_Read_Actions, () => new Endpoint(RestMethod.Get, new[]{"boards","_id","actions"})},
					{EntityRequestType.Board_Read_Cards, () => new Endpoint(RestMethod.Get, new[]{"boards","_id","cards"})},
					{EntityRequestType.Board_Read_Checklists, () => new Endpoint(RestMethod.Get, new[]{"boards","_id","checklists"})},
					{EntityRequestType.Board_Read_InvitedMembers, () => new Endpoint(RestMethod.Get, new[]{"boards","_id","membersInvited"})},
					{EntityRequestType.Board_Read_Lists, () => new Endpoint(RestMethod.Get, new[]{"boards","_id","lists"})},
					{EntityRequestType.Board_Read_Members, () => new Endpoint(RestMethod.Get, new[]{"boards","_id","members"})},
					{EntityRequestType.Board_Read_Memberships, () => new Endpoint(RestMethod.Get, new[]{"boards","_id","memberships"})},
					{EntityRequestType.Board_Read_Refresh, () => new Endpoint(RestMethod.Get, new[]{"boards","_id"})},
					{EntityRequestType.Board_Write_AddList, () => new Endpoint(RestMethod.Post, new[]{"lists"})},
					{EntityRequestType.Board_Write_AddOrUpdateMember, () => new Endpoint(RestMethod.Put, new[]{"boards","_id","members"})},
					{EntityRequestType.Board_Write_Description, () => new Endpoint(RestMethod.Put, new[]{"boards","_id"})},
					{EntityRequestType.Board_Write_InviteMember, () => new Endpoint(RestMethod.Post, new[]{string.Empty})},
					{EntityRequestType.Board_Write_IsClosed, () => new Endpoint(RestMethod.Put, new[]{"boards","_id"})},
					{EntityRequestType.Board_Write_IsPinned, () => new Endpoint(RestMethod.Put, new[]{"boards","_id"})},
					{EntityRequestType.Board_Write_IsSubscribed, () => new Endpoint(RestMethod.Put, new[]{"boards","_id"})},
					{EntityRequestType.Board_Write_MarkAsViewed, () => new Endpoint(RestMethod.Post, new[]{"boards","_id","markAsViewed"})},
					{EntityRequestType.Board_Write_Name, () => new Endpoint(RestMethod.Put, new[]{"boards","_id"})},
					{EntityRequestType.Board_Write_Organization, () => new Endpoint(RestMethod.Put, new[]{"boards","_id"})},
					{EntityRequestType.Board_Write_RemoveMember, () => new Endpoint(RestMethod.Delete, new[]{"boards","_id","members","_memberId"})},
					{EntityRequestType.Board_Write_RescindInvitation, () => new Endpoint(RestMethod.Delete, new[]{string.Empty})},
					{EntityRequestType.BoardMembership_Read_Refresh, () => new Endpoint(RestMethod.Get, new[]{"boards","_boardId","memberships","_id"})},
					{EntityRequestType.BoardPersonalPreferences_Read_Refresh, () => new Endpoint(RestMethod.Get, new[]{"boards","_boardId","myPrefs"})},
					{EntityRequestType.BoardPersonalPreferences_Write_ShowListGuide, () => new Endpoint(RestMethod.Put, new[]{"boards","_boardId","myPrefs","showListGuide"})},
					{EntityRequestType.BoardPersonalPreferences_Write_ShowSidebar, () => new Endpoint(RestMethod.Put, new[]{"boards","_boardId","myPrefs","showSidebar"})},
					{EntityRequestType.BoardPersonalPreferences_Write_ShowSidebarActivity, () => new Endpoint(RestMethod.Put, new[]{"boards","_boardId","myPrefs","showSidebarActivity"})},
					{EntityRequestType.BoardPersonalPreferences_Write_ShowSidebarBoardActions, () => new Endpoint(RestMethod.Put, new[]{"boards","_boardId","myPrefs","showSidebarBoardActions"})},
					{EntityRequestType.BoardPersonalPreferences_Write_ShowSidebarMembers, () => new Endpoint(RestMethod.Put, new[]{"boards","_boardId","myPrefs","showSidebarMembers"})},
					{EntityRequestType.BoardPreferences_Read_Refresh, () => new Endpoint(RestMethod.Get, new[]{"boards","_boardId","prefs"})},
					{EntityRequestType.BoardPreferences_Write_AllowsSelfJoin, () => new Endpoint(RestMethod.Put, new[]{"boards","_boardId","prefs","selfJoin"})},
					{EntityRequestType.BoardPreferences_Write_Comments, () => new Endpoint(RestMethod.Put, new[]{"boards","_boardId","prefs","comments"})},
					{EntityRequestType.BoardPreferences_Write_Invitations, () => new Endpoint(RestMethod.Put, new[]{"boards","_boardId","prefs","invitations"})},
					{EntityRequestType.BoardPreferences_Write_PermissionLevel, () => new Endpoint(RestMethod.Put, new[]{"boards","_boardId","prefs","permissionLevel"})},
					{EntityRequestType.BoardPreferences_Write_ShowCardCovers, () => new Endpoint(RestMethod.Put, new[]{"boards","_boardId","prefs","cardCovers"})},
					{EntityRequestType.BoardPreferences_Write_Voting, () => new Endpoint(RestMethod.Put, new[]{"boards","_boardId","prefs","voting"})},
					{EntityRequestType.Card_Read_Actions, () => new Endpoint(RestMethod.Get, new[]{"cards","_id","actions"})},
					{EntityRequestType.Card_Read_Attachments, () => new Endpoint(RestMethod.Get, new[]{"cards","_id","attachments"})},
					{EntityRequestType.Card_Read_CheckItems, () => new Endpoint(RestMethod.Get, new[]{"cards","_id","checkItems"})},
					{EntityRequestType.Card_Read_CheckLists, () => new Endpoint(RestMethod.Get, new[]{"cards","_id","checklists"})},
					{EntityRequestType.Card_Read_Labels, () => new Endpoint(RestMethod.Get, new[]{"cards","_id","labels"})},
					{EntityRequestType.Card_Read_Members, () => new Endpoint(RestMethod.Get, new[]{"cards","_id","members"})},
					{EntityRequestType.Card_Read_Refresh, () => new Endpoint(RestMethod.Get, new[]{"cards","_id"})},
					{EntityRequestType.Card_Read_VotingMembers, () => new Endpoint(RestMethod.Get, new[]{"cards","_id","membersVoted"})},
					{EntityRequestType.Card_Write_AddAttachment, () => new Endpoint(RestMethod.Post, new[]{"cards","_id","attachments"})},
					{EntityRequestType.Card_Write_AddChecklist, () => new Endpoint(RestMethod.Post, new[]{"checklists"})},
					{EntityRequestType.Card_Write_AddComment, () => new Endpoint(RestMethod.Post, new[]{"cards","_id","actions","comments"})},
					{EntityRequestType.Card_Write_ApplyLabel, () => new Endpoint(RestMethod.Post, new[]{"cards","_id","labels"})},
					{EntityRequestType.Card_Write_AssignMember, () => new Endpoint(RestMethod.Post, new[]{"cards","_id","idMembers"})},
					{EntityRequestType.Card_Write_ClearNotifications, () => new Endpoint(RestMethod.Post, new[]{"cards","_id","markAssociatedNotificationsRead"})},
					{EntityRequestType.Card_Write_Delete, () => new Endpoint(RestMethod.Delete, new[]{"cards","_id"})},
					{EntityRequestType.Card_Write_Description, () => new Endpoint(RestMethod.Put, new[]{"cards","_id"})},
					{EntityRequestType.Card_Write_DueDate, () => new Endpoint(RestMethod.Put, new[]{"cards","_id"})},
					{EntityRequestType.Card_Write_IsClosed, () => new Endpoint(RestMethod.Put, new[]{"cards","_id"})},
					{EntityRequestType.Card_Write_IsSubscribed, () => new Endpoint(RestMethod.Put, new[]{"cards","_id"})},
					{EntityRequestType.Card_Write_Move, () => new Endpoint(RestMethod.Put, new[]{"cards","_id"})},
					{EntityRequestType.Card_Write_Name, () => new Endpoint(RestMethod.Put, new[]{"cards","_id"})},
					{EntityRequestType.Card_Write_Position, () => new Endpoint(RestMethod.Put, new[]{"cards","_id"})},
					{EntityRequestType.Card_Write_RemoveLabel, () => new Endpoint(RestMethod.Delete, new[]{"cards","_id","labels","_color"})},
					{EntityRequestType.Card_Write_RemoveMember, () => new Endpoint(RestMethod.Delete, new[]{"cards","_id","members","_memberId"})},
					{EntityRequestType.Card_Write_WarnWhenUpcoming, () => new Endpoint(RestMethod.Put, new[]{"cards","_id"})},
					{EntityRequestType.CheckItem_Read_Refresh, () => new Endpoint(RestMethod.Get, new[]{"checklists","_checkListId","checkItems","_id"})},
					{EntityRequestType.CheckItem_Write_Delete, () => new Endpoint(RestMethod.Delete, new[]{"checklists","_checkListId","checkItems","_id"})},
					{EntityRequestType.CheckItem_Write_Name, () => new Endpoint(RestMethod.Put, new[]{"cards","_cardId","checklist","_checkListId","checkItem","_id","name"})},
					{EntityRequestType.CheckItem_Write_Position, () => new Endpoint(RestMethod.Put, new[]{"cards","_cardId","checklist","_checkListId","checkItem","_id","pos"})},
					{EntityRequestType.CheckItem_Write_State, () => new Endpoint(RestMethod.Put, new[]{"cards","_cardId","checklist","_checkListId","checkItem","_id","state"})},
					{EntityRequestType.CheckList_Read_CheckItems, () => new Endpoint(RestMethod.Get, new[]{"checklists","_id","checkItems"})},
					{EntityRequestType.CheckList_Read_Refresh, () => new Endpoint(RestMethod.Get, new[]{"checklists","_id"})},
					{EntityRequestType.CheckList_Write_AddCheckItem, () => new Endpoint(RestMethod.Post, new[]{"checklists","_id","checkItems"})},
					{EntityRequestType.CheckList_Write_Card, () => new Endpoint(RestMethod.Put, new[]{"checklists","_id"})},
					{EntityRequestType.CheckList_Write_Delete, () => new Endpoint(RestMethod.Delete, new[]{"checklists","_id"})},
					{EntityRequestType.CheckList_Write_Name, () => new Endpoint(RestMethod.Put, new[]{"checklists","_id"})},
					{EntityRequestType.CheckList_Write_Position, () => new Endpoint(RestMethod.Put, new[]{"checklists","_id"})},
					{EntityRequestType.Label_Read_Refresh, () => new Endpoint(RestMethod.Get, new[]{string.Empty})},
					{EntityRequestType.LabelNames_Read_Refresh, () => new Endpoint(RestMethod.Get, new[]{"board","_boardId","labelNames"})},
					{EntityRequestType.LabelNames_Write_Blue, () => new Endpoint(RestMethod.Put, new[]{"board","_boardId","labelNames","_color"})},
					{EntityRequestType.LabelNames_Write_Green, () => new Endpoint(RestMethod.Put, new[]{"board","_boardId","labelNames","_color"})},
					{EntityRequestType.LabelNames_Write_Orange, () => new Endpoint(RestMethod.Put, new[]{"board","_boardId","labelNames","_color"})},
					{EntityRequestType.LabelNames_Write_Purple, () => new Endpoint(RestMethod.Put, new[]{"board","_boardId","labelNames","_color"})},
					{EntityRequestType.LabelNames_Write_Red, () => new Endpoint(RestMethod.Put, new[]{"board","_boardId","labelNames","_color"})},
					{EntityRequestType.LabelNames_Write_Yellow, () => new Endpoint(RestMethod.Put, new[]{"board","_boardId","labelNames","_color"})},
					{EntityRequestType.List_Read_Actions, () => new Endpoint(RestMethod.Get, new[]{"lists","_id","actions"})},
					{EntityRequestType.List_Read_Cards, () => new Endpoint(RestMethod.Get, new[]{"lists","_id","cards"})},
					{EntityRequestType.List_Read_Refresh, () => new Endpoint(RestMethod.Get, new[]{"lists","_id"})},
					{EntityRequestType.List_Write_AddCard, () => new Endpoint(RestMethod.Post, new[]{"cards"})},
					{EntityRequestType.List_Write_Board, () => new Endpoint(RestMethod.Put, new[]{"lists","_id"})},
					{EntityRequestType.List_Write_Delete, () => new Endpoint(RestMethod.Delete, new[]{"lists","_id"})},
					{EntityRequestType.List_Write_IsClosed, () => new Endpoint(RestMethod.Put, new[]{"lists","_id"})},
					{EntityRequestType.List_Write_IsSubscribed, () => new Endpoint(RestMethod.Put, new[]{"lists","_id"})},
					{EntityRequestType.List_Write_Move, () => new Endpoint(RestMethod.Put, new[]{"lists","_id"})},
					{EntityRequestType.List_Write_Name, () => new Endpoint(RestMethod.Put, new[]{"lists","_id"})},
					{EntityRequestType.List_Write_Position, () => new Endpoint(RestMethod.Put, new[]{"lists","_id"})},
					{EntityRequestType.Member_Read_Actions, () => new Endpoint(RestMethod.Get, new[]{"members","_id","actions"})},
					{EntityRequestType.Member_Read_Boards, () => new Endpoint(RestMethod.Get, new[]{"members","_id","boards"})},
					{EntityRequestType.Member_Read_Cards, () => new Endpoint(RestMethod.Get, new[]{"members","_id","cards"})},
					{EntityRequestType.Member_Read_InvitedBoards, () => new Endpoint(RestMethod.Get, new[]{"members","_id","boardsInvited"})},
					{EntityRequestType.Member_Read_InvitedOrganizations, () => new Endpoint(RestMethod.Get, new[]{"members","_id","organizationsInvited"})},
					{EntityRequestType.Member_Read_Notifications, () => new Endpoint(RestMethod.Get, new[]{"members","_id","notifications"})},
					{EntityRequestType.Member_Read_Organizations, () => new Endpoint(RestMethod.Get, new[]{"members","_id","idOrganizations"})},
					{EntityRequestType.Member_Read_Refresh, () => new Endpoint(RestMethod.Get, new[]{"members","_id"})},
					{EntityRequestType.Member_Read_Sessions, () => new Endpoint(RestMethod.Get, new[]{"members","_id","sessions"})},
					{EntityRequestType.Member_Read_StarredBoards, () => new Endpoint(RestMethod.Get, new[]{"members","_id","boardStars"})},
					{EntityRequestType.Member_Read_Tokens, () => new Endpoint(RestMethod.Get, new[]{"members","_id","tokens"})},
					{EntityRequestType.Member_Write_AvatarSource, () => new Endpoint(RestMethod.Put, new[]{"members","_id"})},
					{EntityRequestType.Member_Write_Bio, () => new Endpoint(RestMethod.Put, new[]{"members","_id"})},
					{EntityRequestType.Member_Write_ClearNotifications, () => new Endpoint(RestMethod.Post, new[]{"notifications","all","read"})},
					{EntityRequestType.Member_Write_CreateBoard, () => new Endpoint(RestMethod.Post, new[]{"boards"})},
					{EntityRequestType.Member_Write_CreateOrganization, () => new Endpoint(RestMethod.Post, new[]{"organizations"})},
					{EntityRequestType.Member_Write_FullName, () => new Endpoint(RestMethod.Put, new[]{"members","_id"})},
					{EntityRequestType.Member_Write_Initials, () => new Endpoint(RestMethod.Put, new[]{"members","_id"})},
					{EntityRequestType.Member_Write_PinBoard, () => new Endpoint(RestMethod.Post, new[]{"members","_id","idBoardsPinned"})},
					{EntityRequestType.Member_Write_RescindVoteForCard, () => new Endpoint(RestMethod.Delete, new[]{"cards","_cardId","membersVoted","_id"})},
					{EntityRequestType.Member_Write_UnpinBoard, () => new Endpoint(RestMethod.Delete, new[]{"members","_id","idBoardsPinned","_boardId"})},
					{EntityRequestType.Member_Write_Username, () => new Endpoint(RestMethod.Put, new[]{"members","_id"})},
					{EntityRequestType.Member_Write_VoteForCard, () => new Endpoint(RestMethod.Post, new[]{"cards","_cardId","membersVoted"})},
					{EntityRequestType.MemberPreferences_Read_Refresh, () => new Endpoint(RestMethod.Get, new[]{"members","_memberId","prefs"})},
					{EntityRequestType.MemberPreferences_Write_ColorBlind, () => new Endpoint(RestMethod.Put, new[]{"members","_memberId","prefs","colorBlind"})},
					{EntityRequestType.MemberPreferences_Write_MinutesBeforeDeadlineToNotify, () => new Endpoint(RestMethod.Put, new[]{"members","_memberId","prefs","minutesBeforeDeadlineToNotify"})},
					{EntityRequestType.MemberPreferences_Write_MinutesBetweenSummaries, () => new Endpoint(RestMethod.Put, new[]{"members","_memberId","prefs","minutesBetweenSummaries"})},
					{EntityRequestType.MemberPreferences_Write_SendSummaries, () => new Endpoint(RestMethod.Put, new[]{"members","_memberId","prefs","sendSummaries"})},
					{EntityRequestType.MemberSession_Write_Delete, () => new Endpoint(RestMethod.Delete, new[]{"members","_memberId","sessions"})},
					{EntityRequestType.Notification_Read_Refresh, () => new Endpoint(RestMethod.Get, new[]{"notifications","_id"})},
					{EntityRequestType.Notification_Write_IsUnread, () => new Endpoint(RestMethod.Put, new[]{"notifications","_id","unread"})},
					{EntityRequestType.Organization_Read_Actions, () => new Endpoint(RestMethod.Get, new[]{"organizations","_id","actions"})},
					{EntityRequestType.Organization_Read_Boards, () => new Endpoint(RestMethod.Get, new[]{"organizations","_id","boards"})},
					{EntityRequestType.Organization_Read_InvitedMembers, () => new Endpoint(RestMethod.Get, new[]{"organizations","_id","membersInvited"})},
					{EntityRequestType.Organization_Read_Members, () => new Endpoint(RestMethod.Get, new[]{"organizations","_id","members"})},
					{EntityRequestType.Organization_Read_Memberships, () => new Endpoint(RestMethod.Get, new[]{"organizations","_id","memberships"})},
					{EntityRequestType.Organization_Read_Refresh, () => new Endpoint(RestMethod.Get, new[]{"organizations","_id"})},
					{EntityRequestType.Organization_Write_AddOrUpdateMember, () => new Endpoint(RestMethod.Put, new[]{"organizations","_id","members","_memberId"})},
					{EntityRequestType.Organization_Write_CreateBoard, () => new Endpoint(RestMethod.Post, new[]{"boards"})},
					{EntityRequestType.Organization_Write_Delete, () => new Endpoint(RestMethod.Delete, new[]{"organizations","_id"})},
					{EntityRequestType.Organization_Write_Description, () => new Endpoint(RestMethod.Put, new[]{"organizations","_id"})},
					{EntityRequestType.Organization_Write_DisplayName, () => new Endpoint(RestMethod.Put, new[]{"organizations","_id"})},
					{EntityRequestType.Organization_Write_InviteMember, () => new Endpoint(RestMethod.Post, new[]{string.Empty})},
					{EntityRequestType.Organization_Write_Name, () => new Endpoint(RestMethod.Put, new[]{"organizations","_id"})},
					{EntityRequestType.Organization_Write_RemoveMember, () => new Endpoint(RestMethod.Delete, new[]{"organizations","_id","members","_memberId"})},
					{EntityRequestType.Organization_Write_RescindInvitation, () => new Endpoint(RestMethod.Delete, new[]{string.Empty})},
					{EntityRequestType.Organization_Write_Website, () => new Endpoint(RestMethod.Put, new[]{"organizations","_id"})},
					{EntityRequestType.OrganizationMembership_Read_Refresh, () => new Endpoint(RestMethod.Get, new[]{"organizations","_organizationId","memberships","_id"})},
					{EntityRequestType.OrganizationPreferences_Read_Refresh, () => new Endpoint(RestMethod.Get, new[]{"organizations","_organizationId","prefs"})},
					{EntityRequestType.OrganizationPreferences_Write_AssociatedDomain, () => new Endpoint(RestMethod.Put, new[]{"organizations","_organizationId","prefs","associatedDomain"})},
					{EntityRequestType.OrganizationPreferences_Write_ExternalMembersDisabled, () => new Endpoint(RestMethod.Put, new[]{"organizations","_organizationId","prefs","externalMembersDisabled"})},
					{EntityRequestType.OrganizationPreferences_Write_OrgInviteRestrict, () => new Endpoint(RestMethod.Put, new[]{"organizations","_organizationId","prefs","orgInviteRestrict"})},
					{EntityRequestType.OrganizationPreferences_Write_OrgVisibleBoardVisibility, () => new Endpoint(RestMethod.Put, new[]{"organizations","_organizationId","prefs","boardVisibilityRestrict","org"})},
					{EntityRequestType.OrganizationPreferences_Write_PermissionLevel, () => new Endpoint(RestMethod.Put, new[]{"organizations","_organizationId","prefs","permissionLevel"})},
					{EntityRequestType.OrganizationPreferences_Write_PrivateBoardVisibility, () => new Endpoint(RestMethod.Put, new[]{"organizations","_organizationId","prefs","boardVisibilityRestrict","private"})},
					{EntityRequestType.OrganizationPreferences_Write_PublicBoardVisibility, () => new Endpoint(RestMethod.Put, new[]{"organizations","_organizationId","prefs","boardVisibilityRestrict","public"})},
					{EntityRequestType.Service_Read_Me, () => new Endpoint(RestMethod.Get, new[]{"members","me"})},
					{EntityRequestType.Service_Read_Search, () => new Endpoint(RestMethod.Get, new[]{"search"})},
					{EntityRequestType.Service_Read_SearchMembers, () => new Endpoint(RestMethod.Get, new[]{"search","members"})},
					{EntityRequestType.Token_Read_Refresh, () => new Endpoint(RestMethod.Get, new[]{"tokens","_token"})},
					{EntityRequestType.Token_Write_Delete, () => new Endpoint(RestMethod.Delete, new[]{"tokens","_token"})},
					{EntityRequestType.Webhook_Read_Refresh, () => new Endpoint(RestMethod.Get, new[]{"webhooks","_id"})},
					{EntityRequestType.Webhook_Write_Active, () => new Endpoint(RestMethod.Put, new[]{"webhooks","_id"})},
					{EntityRequestType.Webhook_Write_CallbackUrl, () => new Endpoint(RestMethod.Put, new[]{"webhooks","_id"})},
					{EntityRequestType.Webhook_Write_Delete, () => new Endpoint(RestMethod.Delete, new[]{"webhooks","_id"})},
					{EntityRequestType.Webhook_Write_Description, () => new Endpoint(RestMethod.Put, new[]{"webhooks","_id"})},
					{EntityRequestType.Webhook_Write_Entity, () => new Endpoint(RestMethod.Put, new[]{"webhooks"})},
				};
			_refreshRequestTypeMap = new Dictionary<Type, EntityRequestType>
				{
					{typeof (Action), EntityRequestType.Action_Read_Refresh},
					{typeof (Board), EntityRequestType.Board_Read_Refresh},
					{typeof (Card), EntityRequestType.Card_Read_Refresh},
					{typeof (CheckList), EntityRequestType.CheckList_Read_Refresh},
					{typeof (List), EntityRequestType.List_Read_Refresh},
					{typeof (Member), EntityRequestType.Member_Read_Refresh},
					{typeof (Notification), EntityRequestType.Notification_Read_Refresh},
					{typeof (Organization), EntityRequestType.Organization_Read_Refresh},
					{typeof (Token), EntityRequestType.Token_Read_Refresh},
					{typeof (Webhook<>), EntityRequestType.Webhook_Read_Refresh},
				};
		}

		public Endpoint Build(EntityRequestType requestType, IDictionary<string, object> parameters)
		{
			return BuildUrl(requestType, parameters);
		}
		public EntityRequestType GetRequestType<T>()
		{
			if (_refreshRequestTypeMap.ContainsKey(typeof (T)))
				return _refreshRequestTypeMap[typeof (T)];
			return EntityRequestType.Unsupported;
		}

		private static Endpoint BuildUrl(EntityRequestType requestType, IDictionary<string, object> parameters)
		{
			var endpoint = _library[requestType]();
			var requiredParameters = endpoint.Where(p => p.StartsWith("_")).ToList();
			foreach (var parameter in requiredParameters)
			{
				if (!parameters.ContainsKey(parameter))
					throw new Exception("Attempted to build endpoint with incomplete parameter collection.");
				var value = parameters[parameter] ?? string.Empty;
				endpoint.Resolve(parameter, value.ToString());
				parameters.Remove(parameter);
			}
			return endpoint;
		}
	}
}
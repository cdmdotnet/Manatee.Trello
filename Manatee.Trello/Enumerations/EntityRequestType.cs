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
 
	File Name:		EntityRequestType.cs
	Namespace:		Manatee.Trello
	Class Name:		EntityRequestType
	Purpose:		Enumerates the various types of requests, including which kind
					of entity is submitting the request and the desired operation.

***************************************************************************************/

namespace Manatee.Trello
{
	/// <summary>
	/// Enumerates the various types of requests, including which kind of entity
	/// is submitting the request and the desired operation.
	/// </summary>
	/// <remarks>
	/// This enumeration is exposed solely for unit testing purposes.
	/// </remarks>
	public enum EntityRequestType
	{
		/// <summary />
		Unsupported,
		/// <summary />
		Action_Write_Delete,
		/// <summary />
		Action_Read_Refresh,
		/// <summary />
		Attachment_Write_Delete,
		/// <summary />
		Attachment_Read_Refresh,
		/// <summary />
		Badges_Read_Refresh,
		/// <summary />
		Board_Read_Actions,
		/// <summary />
		Board_Write_AddList,
		/// <summary />
		Board_Write_AddOrUpdateMember,
		/// <summary />
		Board_Read_Cards,
		/// <summary />
		Board_Read_Checklists,
		/// <summary />
		Board_Write_Description,
		/// <summary />
		Board_Read_InvitedMembers,
		/// <summary />
		Board_Write_InviteMember,
		/// <summary />
		Board_Write_IsClosed,
		/// <summary />
		Board_Write_IsPinned,
		/// <summary />
		Board_Write_IsSubscribed,
		/// <summary />
		Board_Read_Lists,
		/// <summary />
		Board_Write_MarkAsViewed,
		/// <summary />
		Board_Read_Members,
		/// <summary />
		Board_Read_Memberships,
		/// <summary />
		Board_Write_Name,
		/// <summary />
		Board_Write_Organization,
		/// <summary />
		Board_Read_Refresh,
		/// <summary />
		Board_Write_RemoveMember,
		/// <summary />
		Board_Write_RescindInvitation,
		/// <summary />
		BoardMembership_Read_Refresh,
		/// <summary />
		BoardPersonalPreferences_Read_Refresh,
		/// <summary />
		BoardPersonalPreferences_Write_ShowListGuide,
		/// <summary />
		BoardPersonalPreferences_Write_ShowSidebar,
		/// <summary />
		BoardPersonalPreferences_Write_ShowSidebarActivity,
		/// <summary />
		BoardPersonalPreferences_Write_ShowSidebarBoardActions,
		/// <summary />
		BoardPersonalPreferences_Write_ShowSidebarMembers,
		/// <summary />
		BoardPreferences_Write_AllowsSelfJoin,
		/// <summary />
		BoardPreferences_Write_Comments,
		/// <summary />
		BoardPreferences_Write_Invitations,
		/// <summary />
		BoardPreferences_Write_PermissionLevel,
		/// <summary />
		BoardPreferences_Read_Refresh,
		/// <summary />
		BoardPreferences_Write_ShowCardCovers,
		/// <summary />
		BoardPreferences_Write_Voting,
		/// <summary />
		Card_Read_Actions,
		/// <summary />
		Card_Write_AddAttachment,
		/// <summary />
		Card_Write_AddChecklist,
		/// <summary />
		Card_Write_AddComment,
		/// <summary />
		Card_Write_ApplyLabel,
		/// <summary />
		Card_Write_AssignMember,
		/// <summary />
		Card_Read_Attachments,
		/// <summary />
		Card_Read_CheckItems,
		/// <summary />
		Card_Read_Checklists,
		/// <summary />
		Card_Write_ClearNotifications,
		/// <summary />
		Card_Write_Delete,
		/// <summary />
		Card_Write_Description,
		/// <summary />
		Card_Write_DueDate,
		/// <summary />
		Card_Write_IsClosed,
		/// <summary />
		Card_Write_IsSubscribed,
		/// <summary />
		Card_Read_Labels,
		/// <summary />
		Card_Read_Members,
		/// <summary />
		Card_Write_Move,
		/// <summary />
		Card_Write_Name,
		/// <summary />
		Card_Write_Position,
		/// <summary />
		Card_Read_Refresh,
		/// <summary />
		Card_Write_RemoveLabel,
		/// <summary />
		Card_Write_RemoveMember,
		/// <summary />
		Card_Read_VotingMembers,
		/// <summary />
		Card_Write_WarnWhenUpcoming,
		/// <summary />
		CheckItem_Write_Delete,
		/// <summary />
		CheckItem_Write_Name,
		/// <summary />
		CheckItem_Write_Position,
		/// <summary />
		CheckItem_Read_Refresh,
		/// <summary />
		CheckItem_Write_State,
		/// <summary />
		CheckList_Write_AddCheckItem,
		/// <summary />
		CheckList_Write_Card,
		/// <summary />
		CheckList_Read_CheckItems,
		/// <summary />
		CheckList_Write_Delete,
		/// <summary />
		CheckList_Write_Name,
		/// <summary />
		CheckList_Write_Position,
		/// <summary />
		CheckList_Read_Refresh,
		/// <summary />
		Label_Read_Refresh,
		/// <summary />
		LabelNames_Write_Blue,
		/// <summary />
		LabelNames_Write_Green,
		/// <summary />
		LabelNames_Write_Orange,
		/// <summary />
		LabelNames_Write_Purple,
		/// <summary />
		LabelNames_Write_Red,
		/// <summary />
		LabelNames_Read_Refresh,
		/// <summary />
		LabelNames_Write_Yellow,
		/// <summary />
		List_Read_Actions,
		/// <summary />
		List_Write_AddCard,
		/// <summary />
		List_Write_Board,
		/// <summary />
		List_Read_Cards,
		/// <summary />
		List_Write_Delete,
		/// <summary />
		List_Write_IsClosed,
		/// <summary />
		List_Write_IsSubscribed,
		/// <summary />
		List_Write_Move,
		/// <summary />
		List_Write_Name,
		/// <summary />
		List_Write_Position,
		/// <summary />
		List_Read_Refresh,
		/// <summary />
		Member_Read_Actions,
		/// <summary />
		Member_Write_AvatarSource,
		/// <summary />
		Member_Write_Bio,
		/// <summary />
		Member_Read_Boards,
		/// <summary />
		Member_Read_Cards,
		/// <summary />
		Member_Write_ClearNotifications,
		/// <summary />
		Member_Write_CreateBoard,
		/// <summary />
		Member_Write_CreateOrganization,
		/// <summary />
		Member_Write_FullName,
		/// <summary />
		Member_Write_Initials,
		/// <summary />
		Member_Read_InvitedBoards,
		/// <summary />
		Member_Read_InvitedOrganizations,
		/// <summary />
		Member_Read_Notifications,
		/// <summary />
		Member_Read_Organization,
		/// <summary />
		Member_Write_PinBoard,
		/// <summary />
		Member_Read_Refresh,
		/// <summary />
		Member_Write_RescindVoteForCard,
		/// <summary />
		Member_Read_Sessions,
		/// <summary />
		Member_Read_Tokens,
		/// <summary />
		Member_Write_UnpinBoard,
		/// <summary />
		Member_Write_Username,
		/// <summary />
		Member_Write_VoteForCard,
		/// <summary />
		MemberPreferences_Write_ColorBlind,
		/// <summary />
		MemberPreferences_Write_MinutesBeforeDeadlineToNotify,
		/// <summary />
		MemberPreferences_Write_MinutesBetweenSummaries,
		/// <summary />
		MemberPreferences_Read_Refresh,
		/// <summary />
		MemberPreferences_Write_SendSummaries,
		/// <summary />
		MemberSession_Write_Delete,
		/// <summary />
		Notification_Write_IsUnread,
		/// <summary />
		Notification_Read_Refresh,
		/// <summary />
		Organization_Read_Actions,
		/// <summary />
		Organization_Write_AddOrUpdateMember,
		/// <summary />
		Organization_Read_Boards,
		/// <summary />
		Organization_Write_CreateBoard,
		/// <summary />
		Organization_Write_Delete,
		/// <summary />
		Organization_Write_Description,
		/// <summary />
		Organization_Write_DisplayName,
		/// <summary />
		Organization_Read_InvitedMembers,
		/// <summary />
		Organization_Write_InviteMember,
		/// <summary />
		Organization_Read_Members,
		/// <summary />
		Organization_Read_Memberships,
		/// <summary />
		Organization_Write_Name,
		/// <summary />
		Organization_Read_Refresh,
		/// <summary />
		Organization_Write_RemoveMember,
		/// <summary />
		Organization_Write_RescindInvitation,
		/// <summary />
		Organization_Write_Website,
		/// <summary />
		OrganizationMembership_Read_Refresh,
		/// <summary />
		OrganizationPreferences_Write_AssociatedDomain,
		/// <summary />
		OrganizationPreferences_Write_ExternalMembersDisabled,
		/// <summary />
		OrganizationPreferences_Write_OrgInviteRestrict,
		/// <summary />
		OrganizationPreferences_Write_OrgVisibleBoardVisibility,
		/// <summary />
		OrganizationPreferences_Write_PermissionLevel,
		/// <summary />
		OrganizationPreferences_Write_PrivateBoardVisibility,
		/// <summary />
		OrganizationPreferences_Write_PublicBoardVisibility,
		/// <summary />
		OrganizationPreferences_Read_Refresh,
		/// <summary />
		Service_Read_Me,
		/// <summary />
		Service_Read_Search,
		/// <summary />
		Service_Read_SearchMembers,
		/// <summary />
		Token_Write_Delete,
		/// <summary />
		Token_Read_Refresh,
	}
}
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
 
	File Name:		EntityRequestType.cs
	Namespace:		Manatee.Trello.Enumerations.DataAccess
	Class Name:		EntityRequestType
	Purpose:		Enumerates the various types of requests, including which kind
					of entity is submitting the request and the desired operation.

***************************************************************************************/

namespace Manatee.Trello.Internal.DataAccess
{
	internal enum EntityRequestType
	{
#pragma warning disable 1591
		Unsupported,
		Action_Read_Refresh,
		Action_Write_Delete,
		Attachment_Write_Delete,
		Board_Read_Actions,
		Board_Read_Cards,
		Board_Read_CardsForMember,
		Board_Read_Labels,
		Board_Read_Lists,
		Board_Read_Members,
		Board_Read_Memberships,
		Board_Read_PersonalPrefs,
		Board_Read_Refresh,
		Board_Write_Update,
		Board_Write_AddLabel,
		Board_Write_AddList,
		Board_Write_AddOrUpdateMember,
		Board_Write_PersonalPrefs,
		Board_Write_RemoveLabel,
		Board_Write_RemoveMember,
		BoardMembership_Read_Refresh,
		BoardMembership_Write_Update,
		Card_Read_Actions,
		Card_Read_Attachments,
		Card_Read_CheckLists,
		Card_Read_Members,
		Card_Read_MembersVoted,
		Card_Read_Refresh,
		Card_Read_Stickers,
		//Card_Read_VotingMembers,
		Card_Write_Update,
		Card_Write_AddAttachment,
		Card_Write_AddChecklist,
		Card_Write_AddComment,
		Card_Write_AddLabel,
		Card_Write_AddSticker,
		Card_Write_AssignMember,
		Card_Write_Delete,
		Card_Write_RemoveLabel,
		Card_Write_RemoveMember,
		//Card_Write_WarnWhenUpcoming,
		CheckItem_Read_Refresh,
		CheckItem_Write_Delete,
		CheckItem_Write_Update,
		CheckList_Read_CheckItems,
		CheckList_Read_Refresh,
		CheckList_Write_AddCheckItem,
		CheckList_Write_Delete,
		CheckList_Write_Update,
		Label_Read_Refresh,
		Label_Write_Delete,
		Label_Write_Update,
		List_Read_Actions,
		List_Read_Cards,
		List_Read_Refresh,
		List_Write_AddCard,
		List_Write_Update,
		Member_Read_Actions,
		Member_Read_Boards,
		Member_Read_Cards,
		//Member_Read_InvitedBoards,
		//Member_Read_InvitedOrganizations,
		Member_Read_Notifications,
		Member_Read_Organizations,
		Member_Read_Refresh,
		//Member_Read_Sessions,
		Member_Write_CreateBoard,
		Member_Write_CreateOrganization,
		Member_Write_Update,
		//Member_Write_VoteForCard,
		//MemberSession_Write_Delete,
		Notification_Read_Refresh,
		Notification_Write_Update,
		Organization_Read_Actions,
		Organization_Read_Boards,
		Organization_Read_Members,
		Organization_Read_Memberships,
		Organization_Read_Refresh,
		Organization_Write_AddOrUpdateMember,
		Organization_Write_CreateBoard,
		Organization_Write_Delete,
		Organization_Write_RemoveMember,
		Organization_Write_Update,
		OrganizationMembership_Read_Refresh,
		OrganizationMembership_Write_Update,
		OrganizationPreferences_Read_Refresh,
		Service_Read_Me,
		Service_Read_Search,
		Service_Read_SearchMembers,
		Serivce_Read_TypeQuery,
		Sticker_Write_Delete,
		Sticker_Write_Update,
		Token_Read_Refresh,
		Token_Write_Delete,
		Webhook_Read_Refresh,
		Webhook_Write_Delete,
		Webhook_Write_Entity,
		Webhook_Write_Update
#pragma warning restore 1591
	}
}
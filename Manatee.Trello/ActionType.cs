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
 
	File Name:		ActionType.cs
	Namespace:		Manatee.Trello
	Class Name:		ActionType
	Purpose:		Enumerates known types of actions on Trello.com.

***************************************************************************************/
namespace Manatee.Trello
{
	public enum ActionType
	{
		Unknown = -1,
		AddAttachmentToCard,
		AddChecklistToCard,
		AddMemberToBoard,
		AddMemberToCard,
		AddMemberToOrganization,
		AddToOrganizationBoard,
		CommentCard,
		CopyCommentCard,
		ConvertToCardFromCheckItem,
		CopyBoard,
		CreateBoard,
		CreateCard,
		CopyCard,
		CreateList,
		CreateOrganization,
		DeleteAttachmentFromCard,
		DeleteBoardInvitation,
		DeleteOrganizationInvitation,
		MakeAdminOfBoard,
		MakeNormalMemberOfBoard,
		MakeNormalMemberOfOrganization,
		MakeObserverOfBoard,
		MemberJoinedTrello,
		MoveCardFromBoard,
		MoveListFromBoard,
		MoveCardToBoard,
		MoveListToBoard,
		RemoveAdminFromBoard,
		RemoveAdminFromOrganization,
		RemoveChecklistFromCard,
		RemoveFromOrganizationBoard,
		RemoveMemberFromCard,
		UnconfirmedBoardInvitation,
		UnconfirmedOrganizationInvitation,
		UpdateBoard,
		UpdateCard,
		UpdateCheckItemStateOnCard,
		UpdateChecklist,
		UpdateMember,
		UpdateOrganization,
		UpdateCardIdList,
		UpdateCardClosed,
		UpdateCardDesc,
		UpdateCardName,
	}
}
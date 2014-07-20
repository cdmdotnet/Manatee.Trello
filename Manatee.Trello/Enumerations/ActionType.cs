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

using System.ComponentModel;

namespace Manatee.Trello
{
	///<summary>
	/// Enumerates known types of Actions.
	///</summary>
	public enum ActionType
	{
		/// <summary>
		/// Not recognized.  May have been created since the current version of this API.
		/// </summary>
		Unknown,
		/// <summary>
		/// Indicates an attachment was added to a card.
		/// </summary>
		[Description("addAttachmentToCard")]
		AddAttachmentToCard,
		/// <summary>
		/// Indicates a checklist was added to a card.
		/// </summary>
		[Description("addChecklistToCard")]
		AddChecklistToCard,
		/// <summary>
		/// Indicates a member was added to a board.
		/// </summary>
		[Description("addMemberToBoard")]
		AddMemberToBoard,
		/// <summary>
		/// Indicates a member was added to a card.
		/// </summary>
		[Description("addMemberToCard")]
		AddMemberToCard,
		/// <summary>
		/// Indicates a member was added to an organization.
		/// </summary>
		[Description("addMemberToOrganization")]
		AddMemberToOrganization,
		/// <summary>
		/// Indicates a organization was added to a board.
		/// </summary>
		[Description("addToOrganizationBoard")]
		AddToOrganizationBoard,
		/// <summary>
		/// Indicates a comment was added to a card.
		/// </summary>
		[Description("commentCard")]
		CommentCard,
		/// <summary>
		/// Indicates a comment was copied from one card to another.
		/// </summary>
		[Description("copyCommentCard")]
		CopyCommentCard,
		/// <summary>
		/// Indicates a checklist item was converted to card.
		/// </summary>
		[Description("convertToCardFromCheckItem")]
		ConvertToCardFromCheckItem,
		/// <summary>
		/// Indicates a board was copied.
		/// </summary>
		[Description("copyBoard")]
		CopyBoard,
		/// <summary>
		/// Indicates a card was copied.
		/// </summary>
		[Description("copyCard")]
		CopyCard,
		/// <summary>
		/// Indicates a board was created.
		/// </summary>
		[Description("createBoard")]
		CreateBoard,
		/// <summary>
		/// Indicates a card was created.
		/// </summary>
		[Description("createCard")]
		CreateCard,
		/// <summary>
		/// Indicates a list was created.
		/// </summary>
		[Description("createList")]
		CreateList,
		/// <summary>
		/// Indicates an organization was created.
		/// </summary>
		[Description("createOrganization")]
		CreateOrganization,
		/// <summary>
		/// Indicates an attachment was deleted from a card.
		/// </summary>
		[Description("deleteAttachmentFromCard")]
		DeleteAttachmentFromCard,
		/// <summary>
		/// Indicates an invitation to a board was rescinded.
		/// </summary>
		[Description("deleteBoardInvitation")]
		DeleteBoardInvitation,
		/// <summary>
		/// Indicates a card was deleted.
		/// </summary>
		[Description("deleteCard")]
		DeleteCard,
		/// <summary>
		/// Indicates an invitation to an organization was rescinded.
		/// </summary>
		[Description("deleteOrganizationInvitation")]
		DeleteOrganizationInvitation,
		/// <summary>
		/// Indicates a member was made an admin of a board.
		/// </summary>
		[Description("makeAdminOfBoard")]
		MakeAdminOfBoard,
		/// <summary>
		/// Indicates a member was made a normal member of a board.
		/// </summary>
		[Description("makeNormalMemberOfBoard")]
		MakeNormalMemberOfBoard,
		/// <summary>
		/// Indicates a member was made a normal member of an organization.
		/// </summary>
		[Description("makeNormalMemberOfOrganization")]
		MakeNormalMemberOfOrganization,
		/// <summary>
		/// Indicates a member was made an observer of a board.
		/// </summary>
		[Description("makeObserverOfBoard")]
		MakeObserverOfBoard,
		/// <summary>
		/// Indicates a member joined Trello.
		/// </summary>
		[Description("memberJoinedTrello")]
		MemberJoinedTrello,
		/// <summary>
		/// Indicates a card was moved from one board to another.
		/// </summary>
		[Description("moveCardFromBoard")]
		MoveCardFromBoard,
		/// <summary>
		/// Indicates a list was moved from one board to another.
		/// </summary>
		[Description("moveListFromBoard")]
		MoveListFromBoard,
		/// <summary>
		/// Indicates a card was moved from one board to another.
		/// </summary>
		[Description("moveCardToBoard")]
		MoveCardToBoard,
		/// <summary>
		/// Indicates a list was moved from one board to another.
		/// </summary>
		[Description("roveListToBoard")]
		MoveListToBoard,
		/// <summary>
		/// Indicates an admin member was removed from a board.
		/// </summary>
		[Description("removeAdminFromBoard")]
		RemoveAdminFromBoard,
		/// <summary>
		/// Indicates an admin member was removed from an organization.
		/// </summary>
		[Description("removeAdminFromOrganization")]
		RemoveAdminFromOrganization,
		/// <summary>
		/// Indicates a checklist was removed from a card.
		/// </summary>
		[Description("removeChecklistFromCard")]
		RemoveChecklistFromCard,
		/// <summary>
		/// Indicates an organization was removed from a board.
		/// </summary>
		[Description("removeFromOrganizationBoard")]
		RemoveFromOrganizationBoard,
		/// <summary>
		/// Indicates a member was removed from a board.
		/// </summary>
		[Description("removeMemberFromBoard")]
		RemoveMemberFromBoard,
		/// <summary>
		/// Indicates a member was removed from a card.
		/// </summary>
		[Description("removeMemberFromCard")]
		RemoveMemberFromCard,
		/// <summary>
		/// Indicates an invitation to a board was created.
		/// </summary>
		[Description("unconfirmedBoardInvitation")]
		UnconfirmedBoardInvitation,
		/// <summary>
		/// Indicates an invitation to an organization was created.
		/// </summary>
		[Description("unconfirmedOrganizationInvitation")]
		UnconfirmedOrganizationInvitation,
		/// <summary>
		/// Indicates a board was updated.
		/// </summary>
		[Description("updateBoard")]
		UpdateBoard,
		/// <summary>
		/// Indicates a card was updated.
		/// </summary>
		[Description("updateCard")]
		UpdateCard,
		/// <summary>
		/// Indicates a checklist item was checked or unchecked.
		/// </summary>
		[Description("updateCheckItemStateOnCard")]
		UpdateCheckItemStateOnCard,
		/// <summary>
		/// Indicates a checklist was updated.
		/// </summary>
		[Description("updateChecklist")]
		UpdateChecklist,
		/// <summary>
		/// Indicates a member was updated.
		/// </summary>
		[Description("updateMember")]
		UpdateMember,
		/// <summary>
		/// Indicates an organization was updated.
		/// </summary>
		[Description("updateOrganization")]
		UpdateOrganization,
		/// <summary>
		/// Indicates a card was moved to a new list.
		/// </summary>
		[Description("updateCardIdList")]
		UpdateCardIdList,
		/// <summary>
		/// Indicates a card was archived or unarchived.
		/// </summary>
		[Description("updateCardClosed")]
		UpdateCardClosed,
		/// <summary>
		/// Indicates a card description was updated.
		/// </summary>
		[Description("updateCardDesc")]
		UpdateCardDesc,
		/// <summary>
		/// Indicates a card name was updated.
		/// </summary>
		[Description("updateCardName")]
		UpdateCardName,
		/// <summary>
		/// Indicates a member voted on a card.
		/// </summary>
		[Description("voteOnCard")]
		VoteOnCard,
		/// <summary>
		/// Indicates a member updated a list.
		/// </summary>
		[Description("updateList")]
		UpdateList
	}
}
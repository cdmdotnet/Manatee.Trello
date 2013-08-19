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
	///<summary>
	/// Enumerates known types of Actions.
	///</summary>
	public enum ActionType
	{
		/// <summary>
		/// Not recognized.  May have been created since the current version of this API.
		/// </summary>
		Unknown = -1,
		/// <summary>
		/// Indicates an attachment was added to a card.
		/// </summary>
		AddAttachmentToCard,
		/// <summary>
		/// Indicates a checklist was added to a card.
		/// </summary>
		AddChecklistToCard,
		/// <summary>
		/// Indicates a member was added to a board.
		/// </summary>
		AddMemberToBoard,
		/// <summary>
		/// Indicates a member was added to a card.
		/// </summary>
		AddMemberToCard,
		/// <summary>
		/// Indicates a member was added to an organization.
		/// </summary>
		AddMemberToOrganization,
		/// <summary>
		/// Indicates a organization was added to a board.
		/// </summary>
		AddToOrganizationBoard,
		/// <summary>
		/// Indicates a comment was added to a card.
		/// </summary>
		CommentCard,
		/// <summary>
		/// Indicates a comment was copied from one card to another.
		/// </summary>
		CopyCommentCard,
		/// <summary>
		/// Indicates a checklist item was converted to card.
		/// </summary>
		ConvertToCardFromCheckItem,
		/// <summary>
		/// Indicates a board was copied.
		/// </summary>
		CopyBoard,
		/// <summary>
		/// Indicates a card was copied.
		/// </summary>
		CopyCard,
		/// <summary>
		/// Indicates a board was created.
		/// </summary>
		CreateBoard,
		/// <summary>
		/// Indicates a card was created.
		/// </summary>
		CreateCard,
		/// <summary>
		/// Indicates a list was created.
		/// </summary>
		CreateList,
		/// <summary>
		/// Indicates an organization was created.
		/// </summary>
		CreateOrganization,
		/// <summary>
		/// Indicates an attachment was deleted from a card.
		/// </summary>
		DeleteAttachmentFromCard,
		/// <summary>
		/// Indicates an invitation to a board was rescinded.
		/// </summary>
		DeleteBoardInvitation,
		/// <summary>
		/// Indicates a card was deleted.
		/// </summary>
		DeleteCard,
		/// <summary>
		/// Indicates an invitation to an organization was rescinded.
		/// </summary>
		DeleteOrganizationInvitation,
		/// <summary>
		/// Indicates a member was made an admin of a board.
		/// </summary>
		MakeAdminOfBoard,
		/// <summary>
		/// Indicates a member was made a normal member of a board.
		/// </summary>
		MakeNormalMemberOfBoard,
		/// <summary>
		/// Indicates a member was made a normal member of an organization.
		/// </summary>
		MakeNormalMemberOfOrganization,
		/// <summary>
		/// Indicates a member was made an observer of a board.
		/// </summary>
		MakeObserverOfBoard,
		/// <summary>
		/// Indicates a member joined Trello.
		/// </summary>
		MemberJoinedTrello,
		/// <summary>
		/// Indicates a card was moved from one board to another.
		/// </summary>
		MoveCardFromBoard,
		/// <summary>
		/// Indicates a list was moved from one board to another.
		/// </summary>
		MoveListFromBoard,
		/// <summary>
		/// Indicates a card was moved from one board to another.
		/// </summary>
		MoveCardToBoard,
		/// <summary>
		/// Indicates a list was moved from one board to another.
		/// </summary>
		MoveListToBoard,
		/// <summary>
		/// Indicates an admin member was removed from a board.
		/// </summary>
		RemoveAdminFromBoard,
		/// <summary>
		/// Indicates an admin member was removed from an organization.
		/// </summary>
		RemoveAdminFromOrganization,
		/// <summary>
		/// Indicates a checklist was removed from a card.
		/// </summary>
		RemoveChecklistFromCard,
		/// <summary>
		/// Indicates an organization was removed from a board.
		/// </summary>
		RemoveFromOrganizationBoard,
		/// <summary>
		/// Indicates a member was removed from a board.
		/// </summary>
		RemoveMemberFromBoard,
		/// <summary>
		/// Indicates a member was removed from a card.
		/// </summary>
		RemoveMemberFromCard,
		/// <summary>
		/// Indicates an invitation to a board was created.
		/// </summary>
		UnconfirmedBoardInvitation,
		/// <summary>
		/// Indicates an invitation to an organization was created.
		/// </summary>
		UnconfirmedOrganizationInvitation,
		/// <summary>
		/// Indicates a board was updated.
		/// </summary>
		UpdateBoard,
		/// <summary>
		/// Indicates a card was updated.
		/// </summary>
		UpdateCard,
		/// <summary>
		/// Indicates a checklist item was checked or unchecked.
		/// </summary>
		UpdateCheckItemStateOnCard,
		/// <summary>
		/// Indicates a checklist was updated.
		/// </summary>
		UpdateChecklist,
		/// <summary>
		/// Indicates a member was updated.
		/// </summary>
		UpdateMember,
		/// <summary>
		/// Indicates an organization was updated.
		/// </summary>
		UpdateOrganization,
		/// <summary>
		/// Indicates a card was moved to a new list.
		/// </summary>
		UpdateCardIdList,
		/// <summary>
		/// Indicates a card was archived or unarchived.
		/// </summary>
		UpdateCardClosed,
		/// <summary>
		/// Indicates a card description was updated.
		/// </summary>
		UpdateCardDesc,
		/// <summary>
		/// Indicates a card name was updated.
		/// </summary>
		UpdateCardName,
	}
}
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
	/// Enumerates known types of <see cref="Action"/>s.
	///</summary>
	public enum ActionType
	{
		/// <summary>
		/// Not recognized.  May have been created since the current version of this API.
		/// </summary>
		Unknown,
		/// <summary>
		/// Indicates an <see cref="Attachment"/> was added to a <see cref="Card"/>.
		/// </summary>
		[Description("addAttachmentToCard")]
		AddAttachmentToCard,
		/// <summary>
		/// Indicates a <see cref="CheckList"/> was added to a <see cref="Card"/>.
		/// </summary>
		[Description("addChecklistToCard")]
		AddChecklistToCard,
		/// <summary>
		/// Indicates a <see cref="Member"/> was added to a <see cref="Board"/>.
		/// </summary>
		[Description("addMemberToBoard")]
		AddMemberToBoard,
		/// <summary>
		/// Indicates a <see cref="Member"/> was added to a <see cref="Card"/>.
		/// </summary>
		[Description("addMemberToCard")]
		AddMemberToCard,
		/// <summary>
		/// Indicates a <see cref="Member"/> was added to an <see cref="Organization"/>.
		/// </summary>
		[Description("addMemberToOrganization")]
		AddMemberToOrganization,
		/// <summary>
		/// Indicates a <see cref="Organization"/> was added to a <see cref="Board"/>.
		/// </summary>
		[Description("addToOrganizationBoard")]
		AddToOrganizationBoard,
		/// <summary>
		/// Indicates a comment was added to a <see cref="Card"/>.
		/// </summary>
		[Description("commentCard")]
		CommentCard,
		/// <summary>
		/// Indicates a comment was copied from one <see cref="Card"/> to another.
		/// </summary>
		[Description("copyCommentCard")]
		CopyCommentCard,
		/// <summary>
		/// Indicates a <see cref="CheckList"/> item was converted to <see cref="Card"/>.
		/// </summary>
		[Description("convertToCardFromCheckItem")]
		ConvertToCardFromCheckItem,
		/// <summary>
		/// Indicates a <see cref="Board"/> was copied.
		/// </summary>
		[Description("copyBoard")]
		CopyBoard,
		/// <summary>
		/// Indicates a <see cref="Card"/> was copied.
		/// </summary>
		[Description("copyCard")]
		CopyCard,
		/// <summary>
		/// Indicates a <see cref="Board"/> was created.
		/// </summary>
		[Description("createBoard")]
		CreateBoard,
		/// <summary>
		/// Indicates a <see cref="Card"/> was created.
		/// </summary>
		[Description("createCard")]
		CreateCard,
		/// <summary>
		/// Indicates a <see cref="List"/> was created.
		/// </summary>
		[Description("createList")]
		CreateList,
		/// <summary>
		/// Indicates an <see cref="Organization"/> was created.
		/// </summary>
		[Description("createOrganization")]
		CreateOrganization,
		/// <summary>
		/// Indicates an <see cref="Attachment"/> was deleted from a <see cref="Card"/>.
		/// </summary>
		[Description("deleteAttachmentFromCard")]
		DeleteAttachmentFromCard,
		/// <summary>
		/// Indicates an invitation to a <see cref="Board"/> was rescinded.
		/// </summary>
		[Description("deleteBoardInvitation")]
		DeleteBoardInvitation,
		/// <summary>
		/// Indicates a <see cref="Card"/> was deleted.
		/// </summary>
		[Description("deleteCard")]
		DeleteCard,
		/// <summary>
		/// Indicates an invitation to an <see cref="Organization"/> was rescinded.
		/// </summary>
		[Description("deleteOrganizationInvitation")]
		DeleteOrganizationInvitation,
		/// <summary>
		/// Indicates a <see cref="Member"/> was made an admin of a <see cref="Board"/>.
		/// </summary>
		[Description("makeAdminOfBoard")]
		MakeAdminOfBoard,
		/// <summary>
		/// Indicates a <see cref="Member"/> was made a normal <see cref="Member"/> of a <see cref="Board"/>.
		/// </summary>
		[Description("makeNormalMemberOfBoard")]
		MakeNormalMemberOfBoard,
		/// <summary>
		/// Indicates a <see cref="Member"/> was made a normal <see cref="Member"/> of an <see cref="Organization"/>.
		/// </summary>
		[Description("makeNormalMemberOfOrganization")]
		MakeNormalMemberOfOrganization,
		/// <summary>
		/// Indicates a <see cref="Member"/> was made an observer of a <see cref="Board"/>.
		/// </summary>
		[Description("makeObserverOfBoard")]
		MakeObserverOfBoard,
		/// <summary>
		/// Indicates a <see cref="Member"/> joined Trello.
		/// </summary>
		[Description("memberJoinedTrello")]
		MemberJoinedTrello,
		/// <summary>
		/// Indicates a <see cref="Card"/> was moved from one <see cref="Board"/> to another.
		/// </summary>
		[Description("moveCardFromBoard")]
		MoveCardFromBoard,
		/// <summary>
		/// Indicates a <see cref="List"/> was moved from one <see cref="Board"/> to another.
		/// </summary>
		[Description("moveListFromBoard")]
		MoveListFromBoard,
		/// <summary>
		/// Indicates a <see cref="Card"/> was moved from one <see cref="Board"/> to another.
		/// </summary>
		[Description("moveCardToBoard")]
		MoveCardToBoard,
		/// <summary>
		/// Indicates a <see cref="List"/> was moved from one <see cref="Board"/> to another.
		/// </summary>
		[Description("roveListToBoard")]
		MoveListToBoard,
		/// <summary>
		/// Indicates an admin <see cref="Member"/> was removed from a <see cref="Board"/>.
		/// </summary>
		[Description("removeAdminFromBoard")]
		RemoveAdminFromBoard,
		/// <summary>
		/// Indicates an admin <see cref="Member"/> was removed from an <see cref="Organization"/>.
		/// </summary>
		[Description("removeAdminFromOrganization")]
		RemoveAdminFromOrganization,
		/// <summary>
		/// Indicates a <see cref="CheckList"/> was removed from a <see cref="Card"/>.
		/// </summary>
		[Description("removeChecklistFromCard")]
		RemoveChecklistFromCard,
		/// <summary>
		/// Indicates an <see cref="Organization"/> was removed from a <see cref="Board"/>.
		/// </summary>
		[Description("removeFromOrganizationBoard")]
		RemoveFromOrganizationBoard,
		/// <summary>
		/// Indicates a <see cref="Member"/> was removed from a <see cref="Board"/>.
		/// </summary>
		[Description("removeMemberFromBoard")]
		RemoveMemberFromBoard,
		/// <summary>
		/// Indicates a <see cref="Member"/> was removed from a <see cref="Card"/>.
		/// </summary>
		[Description("removeMemberFromCard")]
		RemoveMemberFromCard,
		/// <summary>
		/// Indicates an invitation to a <see cref="Board"/> was created.
		/// </summary>
		[Description("unconfirmedBoardInvitation")]
		UnconfirmedBoardInvitation,
		/// <summary>
		/// Indicates an invitation to an <see cref="Organization"/> was created.
		/// </summary>
		[Description("unconfirmedOrganizationInvitation")]
		UnconfirmedOrganizationInvitation,
		/// <summary>
		/// Indicates a <see cref="Board"/> was updated.
		/// </summary>
		[Description("updateBoard")]
		UpdateBoard,
		/// <summary>
		/// Indicates a <see cref="Card"/> was updated.
		/// </summary>
		[Description("updateCard")]
		UpdateCard,
		/// <summary>
		/// Indicates a <see cref="CheckItem"/> was checked or unchecked.
		/// </summary>
		[Description("updateCheckItemStateOnCard")]
		UpdateCheckItemStateOnCard,
		/// <summary>
		/// Indicates a <see cref="CheckList"/> was updated.
		/// </summary>
		[Description("updateChecklist")]
		UpdateChecklist,
		/// <summary>
		/// Indicates a <see cref="Member"/> was updated.
		/// </summary>
		[Description("updateMember")]
		UpdateMember,
		/// <summary>
		/// Indicates an <see cref="Organization"/> was updated.
		/// </summary>
		[Description("updateOrganization")]
		UpdateOrganization,
		/// <summary>
		/// Indicates a <see cref="Card"/> was moved to a new <see cref="List"/>.
		/// </summary>
		[Description("updateCardIdList")]
		UpdateCardIdList,
		/// <summary>
		/// Indicates a <see cref="Card"/> was archived or unarchived.
		/// </summary>
		[Description("updateCardClosed")]
		UpdateCardClosed,
		/// <summary>
		/// Indicates a <see cref="Card"/> description was updated.
		/// </summary>
		[Description("updateCardDesc")]
		UpdateCardDesc,
		/// <summary>
		/// Indicates a <see cref="Card"/> name was updated.
		/// </summary>
		[Description("updateCardName")]
		UpdateCardName,
		/// <summary>
		/// Indicates a <see cref="Member"/> voted on a <see cref="Card"/>.
		/// </summary>
		[Description("voteOnCard")]
		VoteOnCard,
		/// <summary>
		/// Indicates a <see cref="Member"/> updated a <see cref="List"/>.
		/// </summary>
		[Description("updateList")]
		UpdateList
	}
}
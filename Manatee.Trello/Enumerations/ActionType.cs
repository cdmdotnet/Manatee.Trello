/***************************************************************************************

	Copyright 2013 Little Crab Solutions

	   Licensed under the Apache License = 1L << 1, Version 2.0 (the "License");
	   you may not use this file except in compliance with the License.
	   You may obtain a copy of the License at

		 http://www.apache.org/licenses/LICENSE-2.0

	   Unless required by applicable law or agreed to in writing = 1L << 1, software
	   distributed under the License is distributed on an "AS IS" BASIS = 1L << 1,
	   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND = 1L << 1, either express or implied.
	   See the License for the specific language governing permissions and
	   limitations under the License.
 
	File Name:		ActionType.cs
	Namespace:		Manatee.Trello
	Class Name:		ActionType
	Purpose:		Enumerates known types of actions on Trello.com.

***************************************************************************************/

using System;
using System.ComponentModel;

namespace Manatee.Trello
{
	///<summary>
	/// Enumerates known types of <see cref="Action"/>s.
	///</summary>
	[Flags]
	public enum ActionType : long
	{
		/// <summary>
		/// Not recognized.  May have been created since the current version of this API.
		/// </summary>
		/// <remarks>This value is not supported by Trello's API.</remarks>
		Unknown,
		/// <summary>
		/// Indicates an <see cref="Attachment"/> was added to a <see cref="Card"/>.
		/// </summary>
		[Description("addAttachmentToCard")]
		AddAttachmentToCard = 1L << 0,
		/// <summary>
		/// Indicates a <see cref="CheckList"/> was added to a <see cref="Card"/>.
		/// </summary>
		[Description("addChecklistToCard")]
		AddChecklistToCard = 1L << 1,
		/// <summary>
		/// Indicates a <see cref="Member"/> was added to a <see cref="Board"/>.
		/// </summary>
		[Description("addMemberToBoard")]
		AddMemberToBoard = 1L << 2,
		/// <summary>
		/// Indicates a <see cref="Member"/> was added to a <see cref="Card"/>.
		/// </summary>
		[Description("addMemberToCard")]
		AddMemberToCard = 1L << 3,
		/// <summary>
		/// Indicates a <see cref="Member"/> was added to an <see cref="Organization"/>.
		/// </summary>
		[Description("addMemberToOrganization")]
		AddMemberToOrganization = 1L << 4,
		/// <summary>
		/// Indicates a <see cref="Organization"/> was added to a <see cref="Board"/>.
		/// </summary>
		[Description("addToOrganizationBoard")]
		AddToOrganizationBoard = 1L << 5,
		/// <summary>
		/// Indicates a comment was added to a <see cref="Card"/>.
		/// </summary>
		[Description("commentCard")]
		CommentCard = 1L << 6,
		/// <summary>
		/// Indicates a <see cref="CheckList"/> item was converted to <see cref="Card"/>.
		/// </summary>
		[Description("convertToCardFromCheckItem")]
		ConvertToCardFromCheckItem = 1L << 7,
		/// <summary>
		/// Indicates a <see cref="Board"/> was copied.
		/// </summary>
		[Description("copyBoard")]
		CopyBoard = 1L << 8,
		/// <summary>
		/// Indicates a <see cref="Card"/> was copied.
		/// </summary>
		[Description("copyCard")]
		CopyCard = 1L << 9,
		/// <summary>
		/// Indicates a comment was copied from one <see cref="Card"/> to another.
		/// </summary>
		[Description("copyCommentCard")]
		CopyCommentCard = 1L << 10,
		/// <summary>
		/// Indicates a <see cref="Board"/> was created.
		/// </summary>
		[Description("createBoard")]
		CreateBoard = 1L << 11,
		/// <summary>
		/// Indicates a <see cref="Card"/> was created.
		/// </summary>
		[Description("createCard")]
		CreateCard = 1L << 12,
		/// <summary>
		/// Indicates a <see cref="List"/> was created.
		/// </summary>
		[Description("createList")]
		CreateList = 1L << 13,
		/// <summary>
		/// Indicates an <see cref="Organization"/> was created.
		/// </summary>
		[Description("createOrganization")]
		CreateOrganization = 1L << 14,
		/// <summary>
		/// Indicates an <see cref="Attachment"/> was deleted from a <see cref="Card"/>.
		/// </summary>
		[Description("deleteAttachmentFromCard")]
		DeleteAttachmentFromCard = 1L << 15,
		/// <summary>
		/// Indicates an invitation to a <see cref="Board"/> was rescinded.
		/// </summary>
		[Description("deleteBoardInvitation")]
		DeleteBoardInvitation = 1L << 16,
		/// <summary>
		/// Indicates a <see cref="Card"/> was deleted.
		/// </summary>
		[Description("deleteCard")]
		DeleteCard = 1L << 17,
		/// <summary>
		/// Indicates an invitation to an <see cref="Organization"/> was rescinded.
		/// </summary>
		[Description("deleteOrganizationInvitation")]
		DeleteOrganizationInvitation = 1L << 18,
		/// <summary>
		/// Indicates a power-up was disabled.
		/// </summary>
		[Description("disablePowerUp")]
		DisablePowerUp = 1 << 19,
		/// <summary>
		/// Indicates a <see cref="Card"/> was created via email.
		/// </summary>
		[Description("emailCard")]
		EmailCard = 1 << 20,
		/// <summary>
		/// Indicates a power-up was enabled.
		/// </summary>
		[Description("enablePowerUp")]
		EnablePowerUp = 1 << 21,
		/// <summary>
		/// Indicates a <see cref="Member"/> was made an admin of a <see cref="Board"/>.
		/// </summary>
		[Description("makeAdminOfBoard")]
		MakeAdminOfBoard = 1L << 22,
		/// <summary>
		/// Indicates a <see cref="Member"/> was made a normal <see cref="Member"/> of a <see cref="Board"/>.
		/// </summary>
		[Description("makeNormalMemberOfBoard")]
		MakeNormalMemberOfBoard = 1L << 23,
		/// <summary>
		/// Indicates a <see cref="Member"/> was made a normal <see cref="Member"/> of an <see cref="Organization"/>.
		/// </summary>
		[Description("makeNormalMemberOfOrganization")]
		MakeNormalMemberOfOrganization = 1L << 24,
		/// <summary>
		/// Indicates a <see cref="Member"/> was made an observer of a <see cref="Board"/>.
		/// </summary>
		[Description("makeObserverOfBoard")]
		MakeObserverOfBoard = 1L << 25,
		/// <summary>
		/// Indicates a <see cref="Member"/> joined Trello.
		/// </summary>
		[Description("memberJoinedTrello")]
		MemberJoinedTrello = 1L << 26,
		/// <summary>
		/// Indicates a <see cref="Card"/> was moved from one <see cref="Board"/> to another.
		/// </summary>
		[Description("moveCardFromBoard")]
		MoveCardFromBoard = 1L << 27,
		/// <summary>
		/// Indicates a <see cref="Card"/> was moved from one <see cref="Board"/> to another.
		/// </summary>
		[Description("moveCardToBoard")]
		MoveCardToBoard = 1L << 28,
		/// <summary>
		/// Indicates a <see cref="List"/> was moved from one <see cref="Board"/> to another.
		/// </summary>
		[Description("moveListFromBoard")]
		MoveListFromBoard = 1L << 29,
		/// <summary>
		/// Indicates a <see cref="List"/> was moved from one <see cref="Board"/> to another.
		/// </summary>
		[Description("moveListToBoard")]
		MoveListToBoard = 1L << 30,
		/// <summary>
		/// Indicates a <see cref="CheckList"/> was removed from a <see cref="Card"/>.
		/// </summary>
		[Description("removeChecklistFromCard")]
		RemoveChecklistFromCard = 1L << 31,
		/// <summary>
		/// Indicates an <see cref="Organization"/> was removed from a <see cref="Board"/>.
		/// </summary>
		[Description("removeFromOrganizationBoard")]
		RemoveFromOrganizationBoard = 1L << 32,
		/// <summary>
		/// Indicates a <see cref="Member"/> was removed from a <see cref="Card"/>.
		/// </summary>
		[Description("removeMemberFromCard")]
		RemoveMemberFromCard = 1L << 33,
		/// <summary>
		/// Indicates an invitation to a <see cref="Board"/> was created.
		/// </summary>
		[Description("unconfirmedBoardInvitation")]
		UnconfirmedBoardInvitation = 1L << 34,
		/// <summary>
		/// Indicates an invitation to an <see cref="Organization"/> was created.
		/// </summary>
		[Description("unconfirmedOrganizationInvitation")]
		UnconfirmedOrganizationInvitation = 1L << 35,
		/// <summary>
		/// Indicates a <see cref="Board"/> was updated.
		/// </summary>
		[Description("updateBoard")]
		UpdateBoard = 1L << 36,
		/// <summary>
		/// Indicates a <see cref="Card"/> was updated.
		/// </summary>
		[Description("updateCard")]
		UpdateCard = 1L << 37,
		/// <summary>
		/// Indicates a <see cref="Card"/> was archived or unarchived.
		/// </summary>
		[Description("updateCard:closed")]
		UpdateCardClosed = 1L << 38,
		/// <summary>
		/// Indicates a <see cref="Card"/> description was updated.
		/// </summary>
		[Description("updateCard:desc")]
		UpdateCardDesc = 1L << 39,
		/// <summary>
		/// Indicates a <see cref="Card"/> was moved to a new <see cref="List"/>.
		/// </summary>
		[Description("updateCard:idList")]
		UpdateCardIdList = 1L << 40,
		/// <summary>
		/// Indicates a <see cref="Card"/> name was updated.
		/// </summary>
		[Description("updateCard:name")]
		UpdateCardName = 1L << 41,
		/// <summary>
		/// Indicates a <see cref="CheckItem"/> was checked or unchecked.
		/// </summary>
		[Description("updateCheckItemStateOnCard")]
		UpdateCheckItemStateOnCard = 1L << 42,
		/// <summary>
		/// Indicates a <see cref="CheckList"/> was updated.
		/// </summary>
		[Description("updateChecklist")]
		UpdateChecklist = 1L << 43,
		/// <summary>
		/// Indicates a <see cref="Member"/> updated a <see cref="List"/>.
		/// </summary>
		[Description("updateList")]
		UpdateList = 1L << 44,
		/// <summary>
		/// Indicates a <see cref="Member"/> archived a <see cref="List"/>.
		/// </summary>
		[Description("updateList:closed")]
		UpdateListClosed = 1L << 45,
		/// <summary>
		/// Indicates a <see cref="Member"/> updated the name of a <see cref="List"/>.
		/// </summary>
		[Description("updateList:name")]
		UpdateListName = 1L << 46,
		/// <summary>
		/// Indicates a <see cref="Member"/> was updated.
		/// </summary>
		[Description("updateMember")]
		UpdateMember = 1L << 47,
		/// <summary>
		/// Indicates an <see cref="Organization"/> was updated.
		/// </summary>
		[Description("updateOrganization")]
		UpdateOrganization = 1L << 48,
		/// <summary>
		/// Indictes the default set of values returned by <see cref="Card.Actions"/>.
		/// </summary>
		DefaultForCardActions = CommentCard | UpdateCardIdList,
		/// <summary>
		/// Indicates all action types
		/// </summary>
		[Description("all")]
		All = (1L << 49) - 1,
	}
}
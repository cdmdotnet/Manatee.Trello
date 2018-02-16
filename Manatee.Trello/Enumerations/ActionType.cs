using System;
using System.ComponentModel.DataAnnotations;

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
		[Display(Description="addAttachmentToCard")]
		AddAttachmentToCard = 1L << 0,
		/// <summary>
		/// Indicates a <see cref="CheckList"/> was added to a <see cref="Card"/>.
		/// </summary>
		[Display(Description="addChecklistToCard")]
		AddChecklistToCard = 1L << 1,
		/// <summary>
		/// Indicates a <see cref="Member"/> was added to a <see cref="Board"/>.
		/// </summary>
		[Display(Description="addMemberToBoard")]
		AddMemberToBoard = 1L << 2,
		/// <summary>
		/// Indicates a <see cref="Member"/> was added to a <see cref="Card"/>.
		/// </summary>
		[Display(Description="addMemberToCard")]
		AddMemberToCard = 1L << 3,
		/// <summary>
		/// Indicates a <see cref="Member"/> was added to an <see cref="Organization"/>.
		/// </summary>
		[Display(Description="addMemberToOrganization")]
		AddMemberToOrganization = 1L << 4,
		/// <summary>
		/// Indicates a <see cref="Organization"/> was added to a <see cref="Board"/>.
		/// </summary>
		[Display(Description="addToOrganizationBoard")]
		AddToOrganizationBoard = 1L << 5,
		/// <summary>
		/// Indicates a comment was added to a <see cref="Card"/>.
		/// </summary>
		[Display(Description="commentCard")]
		CommentCard = 1L << 6,
		/// <summary>
		/// Indicates a <see cref="CheckItem"/> item was converted to <see cref="Card"/>.
		/// </summary>
		[Display(Description="convertToCardFromCheckItem")]
		ConvertToCardFromCheckItem = 1L << 7,
		/// <summary>
		/// Indicates a <see cref="Board"/> was copied.
		/// </summary>
		[Display(Description="copyBoard")]
		CopyBoard = 1L << 8,
		/// <summary>
		/// Indicates a <see cref="Card"/> was copied.
		/// </summary>
		[Display(Description="copyCard")]
		CopyCard = 1L << 9,
		/// <summary>
		/// Indicates a comment was copied from one <see cref="Card"/> to another.
		/// </summary>
		[Display(Description="copyCommentCard")]
		CopyCommentCard = 1L << 10,
		/// <summary>
		/// Indicates a <see cref="Board"/> was created.
		/// </summary>
		[Display(Description="createBoard")]
		CreateBoard = 1L << 11,
		/// <summary>
		/// Indicates a <see cref="Card"/> was created.
		/// </summary>
		[Display(Description="createCard")]
		CreateCard = 1L << 12,
		/// <summary>
		/// Indicates a <see cref="List"/> was created.
		/// </summary>
		[Display(Description="createList")]
		CreateList = 1L << 13,
		/// <summary>
		/// Indicates an <see cref="Organization"/> was created.
		/// </summary>
		[Display(Description="createOrganization")]
		CreateOrganization = 1L << 14,
		/// <summary>
		/// Indicates an <see cref="Attachment"/> was deleted from a <see cref="Card"/>.
		/// </summary>
		[Display(Description="deleteAttachmentFromCard")]
		DeleteAttachmentFromCard = 1L << 15,
		/// <summary>
		/// Indicates an invitation to a <see cref="Board"/> was rescinded.
		/// </summary>
		[Display(Description="deleteBoardInvitation")]
		DeleteBoardInvitation = 1L << 16,
		/// <summary>
		/// Indicates a <see cref="Card"/> was deleted.
		/// </summary>
		[Display(Description="deleteCard")]
		DeleteCard = 1L << 17,
		/// <summary>
		/// Indicates an invitation to an <see cref="Organization"/> was rescinded.
		/// </summary>
		[Display(Description="deleteOrganizationInvitation")]
		DeleteOrganizationInvitation = 1L << 18,
		/// <summary>
		/// Indicates a power-up was disabled.
		/// </summary>
		[Display(Description="disablePowerUp")]
		DisablePowerUp = 1 << 19,
		/// <summary>
		/// Indicates a <see cref="Card"/> was created via email.
		/// </summary>
		[Display(Description="emailCard")]
		EmailCard = 1 << 20,
		/// <summary>
		/// Indicates a power-up was enabled.
		/// </summary>
		[Display(Description="enablePowerUp")]
		EnablePowerUp = 1 << 21,
		/// <summary>
		/// Indicates a <see cref="Member"/> was made an admin of a <see cref="Board"/>.
		/// </summary>
		[Display(Description="makeAdminOfBoard")]
		MakeAdminOfBoard = 1L << 22,
		/// <summary>
		/// Indicates a <see cref="Member"/> was made a normal <see cref="Member"/> of a <see cref="Board"/>.
		/// </summary>
		[Display(Description="makeNormalMemberOfBoard")]
		MakeNormalMemberOfBoard = 1L << 23,
		/// <summary>
		/// Indicates a <see cref="Member"/> was made a normal <see cref="Member"/> of an <see cref="Organization"/>.
		/// </summary>
		[Display(Description="makeNormalMemberOfOrganization")]
		MakeNormalMemberOfOrganization = 1L << 24,
		/// <summary>
		/// Indicates a <see cref="Member"/> was made an observer of a <see cref="Board"/>.
		/// </summary>
		[Display(Description="makeObserverOfBoard")]
		MakeObserverOfBoard = 1L << 25,
		/// <summary>
		/// Indicates a <see cref="Member"/> joined Trello.
		/// </summary>
		[Display(Description="memberJoinedTrello")]
		MemberJoinedTrello = 1L << 26,
		/// <summary>
		/// Indicates a <see cref="Card"/> was moved from one <see cref="Board"/> to another.
		/// </summary>
		[Display(Description="moveCardFromBoard")]
		MoveCardFromBoard = 1L << 27,
		/// <summary>
		/// Indicates a <see cref="Card"/> was moved from one <see cref="Board"/> to another.
		/// </summary>
		[Display(Description="moveCardToBoard")]
		MoveCardToBoard = 1L << 28,
		/// <summary>
		/// Indicates a <see cref="List"/> was moved from one <see cref="Board"/> to another.
		/// </summary>
		[Display(Description="moveListFromBoard")]
		MoveListFromBoard = 1L << 29,
		/// <summary>
		/// Indicates a <see cref="List"/> was moved from one <see cref="Board"/> to another.
		/// </summary>
		[Display(Description="moveListToBoard")]
		MoveListToBoard = 1L << 30,
		/// <summary>
		/// Indicates a <see cref="CheckList"/> was removed from a <see cref="Card"/>.
		/// </summary>
		[Display(Description="removeChecklistFromCard")]
		RemoveChecklistFromCard = 1L << 31,
		/// <summary>
		/// Indicates an <see cref="Organization"/> was removed from a <see cref="Board"/>.
		/// </summary>
		[Display(Description="removeFromOrganizationBoard")]
		RemoveFromOrganizationBoard = 1L << 32,
		/// <summary>
		/// Indicates a <see cref="Member"/> was removed from a <see cref="Card"/>.
		/// </summary>
		[Display(Description="removeMemberFromCard")]
		RemoveMemberFromCard = 1L << 33,
		/// <summary>
		/// Indicates an invitation to a <see cref="Board"/> was created.
		/// </summary>
		[Display(Description="unconfirmedBoardInvitation")]
		UnconfirmedBoardInvitation = 1L << 34,
		/// <summary>
		/// Indicates an invitation to an <see cref="Organization"/> was created.
		/// </summary>
		[Display(Description="unconfirmedOrganizationInvitation")]
		UnconfirmedOrganizationInvitation = 1L << 35,
		/// <summary>
		/// Indicates a <see cref="Board"/> was updated.
		/// </summary>
		[Display(Description="updateBoard")]
		UpdateBoard = 1L << 36,
		/// <summary>
		/// Indicates a <see cref="Card"/> was updated.
		/// </summary>
		[Display(Description="updateCard")]
		UpdateCard = 1L << 37,
		/// <summary>
		/// Indicates a <see cref="Card"/> was archived or unarchived.
		/// </summary>
		[Display(Description="updateCard:closed")]
		UpdateCardClosed = 1L << 38,
		/// <summary>
		/// Indicates a <see cref="Card"/> description was updated.
		/// </summary>
		[Display(Description="updateCard:desc")]
		UpdateCardDesc = 1L << 39,
		/// <summary>
		/// Indicates a <see cref="Card"/> was moved to a new <see cref="List"/>.
		/// </summary>
		[Display(Description="updateCard:idList")]
		UpdateCardIdList = 1L << 40,
		/// <summary>
		/// Indicates a <see cref="Card"/> name was updated.
		/// </summary>
		[Display(Description="updateCard:name")]
		UpdateCardName = 1L << 41,
		/// <summary>
		/// Indicates a <see cref="CheckItem"/> was checked or unchecked.
		/// </summary>
		[Display(Description="updateCheckItemStateOnCard")]
		UpdateCheckItemStateOnCard = 1L << 42,
		/// <summary>
		/// Indicates a <see cref="CheckList"/> was updated.
		/// </summary>
		[Display(Description="updateChecklist")]
		UpdateChecklist = 1L << 43,
		/// <summary>
		/// Indicates a<see cref="List"/> was updated.
		/// </summary>
		[Display(Description="updateList")]
		UpdateList = 1L << 44,
		/// <summary>
		/// Indicates a <see cref="List"/> was archived.
		/// </summary>
		[Display(Description="updateList:closed")]
		UpdateListClosed = 1L << 45,
		/// <summary>
		/// Indicates the name of a <see cref="List"/> was updated.
		/// </summary>
		[Display(Description="updateList:name")]
		UpdateListName = 1L << 46,
		/// <summary>
		/// Indicates a <see cref="Member"/> was updated.
		/// </summary>
		[Display(Description="updateMember")]
		UpdateMember = 1L << 47,
		/// <summary>
		/// Indicates an <see cref="Organization"/> was updated.
		/// </summary>
		[Display(Description="updateOrganization")]
		UpdateOrganization = 1L << 48,
		/// <summary>
		/// Indicates a plugin was enabled.
		/// </summary>
		[Display(Description="enablePlugin")]
		EnablePlugin = 1L << 49,
		/// <summary>
		/// Indicates a plugin was disabled.
		/// </summary>
		[Display(Description="disablePlugin")]
		DisablePlugin = 1L << 50,
		/// <summary>
		/// Indictes the default set of values returned by <see cref="Card.Actions"/>.
		/// </summary>
		DefaultForCardActions = CommentCard | UpdateCardIdList,
		/// <summary>
		/// Indicates all action types
		/// </summary>
		[Display(Description="all")]
		All = (1L << 51) - 1,
	}
}
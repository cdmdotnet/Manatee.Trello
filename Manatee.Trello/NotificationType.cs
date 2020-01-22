using System;
using System.ComponentModel.DataAnnotations;

namespace Manatee.Trello
{
	///<summary>
	/// Enumerates known types of notifications.
	///</summary>
	[Flags]
	public enum NotificationType
	{
		/// <summary>
		/// Not recognized.  May have been created since the current version of this API.
		/// </summary>
		Unknown,
		/// <summary>
		/// Indicates an attachment was added to a card.
		/// </summary>
		[Display(Description="addedAttachmentToCard")]
		AddedAttachmentToCard = 1 << 0,
		/// <summary>
		/// Indicates the current member was added to a board.
		/// </summary>
		[Display(Description="addedToBoard")]
		AddedToBoard = 1 << 1,
		/// <summary>
		/// Indicates the current member was added to a card.
		/// </summary>
		[Display(Description="addedToCard")]
		AddedToCard = 1 << 2,
		/// <summary>
		/// Indicates the current member was added to an organization.
		/// </summary>
		[Display(Description="addedToOrganization")]
		AddedToOrganization = 1 << 3,
		/// <summary>
		/// Indicates another member was added to an card.
		/// </summary>
		[Display(Description="addedMemberToCard")]
		AddedMemberToCard = 1 << 4,
		/// <summary>
		/// Indicates the current member was added to a board as an admin.
		/// </summary>
		[Display(Description="addAdminToBoard")]
		AddAdminToBoard = 1 << 5,
		/// <summary>
		/// Indicates the current member was added to an organization as an admin.
		/// </summary>
		[Display(Description="addAdminToOrganization")]
		AddAdminToOrganization = 1 << 6,
		/// <summary>
		/// Indicates a card was changed.
		/// </summary>
		[Display(Description="changeCard")]
		ChangeCard = 1 << 7,
		/// <summary>
		/// Indicates a board was closed.
		/// </summary>
		[Display(Description="closeBoard")]
		CloseBoard = 1 << 8,
		/// <summary>
		/// Indicates another member commented on a card.
		/// </summary>
		[Display(Description="commentCard")]
		CommentCard = 1 << 9,
		/// <summary>
		/// Indicates another member created a card.
		/// </summary>
		[Display(Description="createdCard")]
		CreatedCard = 1 << 10,
		/// <summary>
		/// Indicates the current member was removed from a board.
		/// </summary>
		[Display(Description="removedFromBoard")]
		RemovedFromBoard = 1 << 11,
		/// <summary>
		/// Indicates the current member was removed from a card.
		/// </summary>
		[Display(Description="removedFromCard")]
		RemovedFromCard = 1 << 12,
		/// <summary>
		/// Indicates another member was removed from a card.
		/// </summary>
		[Display(Description="removedMemberFromCard")]
		RemovedMemberFromCard = 1 << 13,
		/// <summary>
		/// Indicates the current member was removed from an organization.
		/// </summary>
		[Display(Description="removedFromOrganization")]
		RemovedFromOrganization = 1 << 14,
		/// <summary>
		/// Indicates the current member was mentioned on a card.
		/// </summary>
		[Display(Description="mentionedOnCard")]
		MentionedOnCard = 1 << 15,
		/// <summary>
		/// Indicates a checklist item was updated.
		/// </summary>
		[Display(Description="updateCheckItemStateOnCard")]
		UpdateCheckItemStateOnCard = 1 << 16,
		/// <summary>
		/// Indicates the current member was made an admin of a board.
		/// </summary>
		[Display(Description="makeAdminOfBoard")]
		MakeAdminOfBoard = 1 << 17,
		/// <summary>
		/// Indicates the current member was made an admin of an organization.
		/// </summary>
		[Display(Description="makeAdminOfOrganization")]
		MakeAdminOfOrganization = 1 << 18,
		/// <summary>
		/// Indicates a card due date is approaching.
		/// </summary>
		[Display(Description="cardDueSoon")]
		CardDueSoon = 1 << 19,
		/// <summary>
		/// Indicates the current member added an attachment to a card.
		/// </summary>
		[Display(Description = "addAttachmentToCard")]
		AddAttachmentToCard = 1 << 20,
		/// <summary>
		/// Indicates the current member joined Trello.
		/// </summary>
		[Display(Description = "memberJoinedTrello")]
		MemberJoinedTrello = 1 << 21,
		/// <summary>
		/// Indicates the current member joined Trello.
		/// </summary>
		[Display(Description = "reactionAdded")]
		ReactionAdded = 1 << 22,
		/// <summary>
		/// Indicates the current member joined Trello.
		/// </summary>
		[Display(Description = "reactionRemoved")]
		ReactionRemoved = 1 << 23,
		/// <summary>
		/// Indicates the current member joined Trello.
		/// </summary>
		[Display(Description = "reopenBoard")]
		ReopenBoard = 1 << 24,
		/// <summary>
		/// Indicates all notification types.
		/// </summary>
		[Display(Description="all")]
		All = (1 << 25) - 1,
	}
}
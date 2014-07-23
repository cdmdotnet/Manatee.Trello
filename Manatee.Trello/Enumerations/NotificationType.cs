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
 
	File Name:		NotificationType.cs
	Namespace:		Manatee.Trello
	Class Name:		NotificationType
	Purpose:		Enumerates known types of notifications on Trello.com.

***************************************************************************************/

using System.ComponentModel;

namespace Manatee.Trello
{
	///<summary>
	/// Enumerates known types of notifications.
	///</summary>
	public enum NotificationType
	{
		/// <summary>
		/// Not recognized.  May have been created since the current version of this API.
		/// </summary>
		Unknown,
		/// <summary>
		/// Indicates an attachment was added to a card.
		/// </summary>
		[Description("addedAttachmentToCard")]
		AddedAttachmentToCard,
		/// <summary>
		/// Indicates the current member was added to a board.
		/// </summary>
		[Description("addedToBoard")]
		AddedToBoard,
		/// <summary>
		/// Indicates the current member was added to a card.
		/// </summary>
		[Description("addedToCard")]
		AddedToCard,
		/// <summary>
		/// Indicates the current member was added to an organization.
		/// </summary>
		[Description("addedToOrganization")]
		AddedToOrganization,
		/// <summary>
		/// Indicates another member was added to an card.
		/// </summary>
		[Description("addedMemberToCard")]
		AddedMemberToCard,
		/// <summary>
		/// Indicates the current member was added to a board as an admin.
		/// </summary>
		[Description("addAdminToBoard")]
		AddAdminToBoard,
		/// <summary>
		/// Indicates the current member was added to an organization as an admin.
		/// </summary>
		[Description("addAdminToOrganization")]
		AddAdminToOrganization,
		/// <summary>
		/// Indicates a card was changed.
		/// </summary>
		[Description("changeCard")]
		ChangeCard,
		/// <summary>
		/// Indicates a board was closed.
		/// </summary>
		[Description("closeBoard")]
		CloseBoard,
		/// <summary>
		/// Indicates another member commented on a card.
		/// </summary>
		[Description("commentCard")]
		CommentCard,
		/// <summary>
		/// Indicates another member created a card.
		/// </summary>
		[Description("createdCard")]
		CreatedCard,
		/// <summary>
		/// Indicates the current member was invited to a board.
		/// </summary>
		[Description("invitedToBoard")]
		InvitedToBoard,
		/// <summary>
		/// Indicates the current member was invited to an organization.
		/// </summary>
		[Description("invitedToOrganization")]
		InvitedToOrganization,
		/// <summary>
		/// Indicates the current member was removed from a board.
		/// </summary>
		[Description("removedFromBoard")]
		RemovedFromBoard,
		/// <summary>
		/// Indicates the current member was removed from a card.
		/// </summary>
		[Description("removedFromCard")]
		RemovedFromCard,
		/// <summary>
		/// Indicates another member was removed from a card.
		/// </summary>
		[Description("removedMemberFromCard")]
		RemovedMemberFromCard,
		/// <summary>
		/// Indicates the current member was removed from an organization.
		/// </summary>
		[Description("removedFromOrganization")]
		RemovedFromOrganization,
		/// <summary>
		/// Indicates the current member was mentioned on a card.
		/// </summary>
		[Description("mentionedOnCard")]
		MentionedOnCard,
		/// <summary>
		/// Indicates the current member is unconfirmed and invited to a board. ?
		/// </summary>
		[Description("unconfirmedInvitedToBoard")]
		UnconfirmedInvitedToBoard,
		/// <summary>
		/// Indicates the current member is unconfirmed and invited to an organization. ?
		/// </summary>
		[Description("unconfirmedInvitedToOrganization")]
		UnconfirmedInvitedToOrganization,
		/// <summary>
		/// Indicates a checklist item was updated.
		/// </summary>
		[Description("updateCheckItemStateOnCard")]
		UpdateCheckItemStateOnCard,
		/// <summary>
		/// Indicates the current member was made an admin of a board.
		/// </summary>
		[Description("makeAdminOfBoard")]
		MakeAdminOfBoard,
		/// <summary>
		/// Indicates the current member was made an admin of an organization.
		/// </summary>
		[Description("makeAdminOfOrganization")]
		MakeAdminOfOrganization,
		/// <summary>
		/// Indicates a card due date is approaching.
		/// </summary>
		[Description("cardDueSoon")]
		CardDueSoon		
	}
}
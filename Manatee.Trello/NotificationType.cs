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
		Unknown = -1,
		/// <summary>
		/// Indicates an attachment was added to a card.
		/// </summary>
		AddedAttachmentToCard,
		/// <summary>
		/// Indicates the current member was added to a board.
		/// </summary>
		AddedToBoard,
		/// <summary>
		/// Indicates the current member was added to a card.
		/// </summary>
		AddedToCard,
		/// <summary>
		/// Indicates the current member was added to an organization.
		/// </summary>
		AddedToOrganization,
		/// <summary>
		/// Indicates another member was added to an card.
		/// </summary>
		AddedMemberToCard,
		/// <summary>
		/// Indicates the current member was added to a board as an admin.
		/// </summary>
		AddAdminToBoard,
		/// <summary>
		/// Indicates the current member was added to an organization as an admin.
		/// </summary>
		AddAdminToOrganization,
		/// <summary>
		/// Indicates a card was changed.
		/// </summary>
		ChangeCard,
		/// <summary>
		/// Indicates a board was closed.
		/// </summary>
		CloseBoard,
		/// <summary>
		/// Indicates another member commented on a card.
		/// </summary>
		CommentCard,
		/// <summary>
		/// Indicates another member created a card.
		/// </summary>
		CreatedCard,
		/// <summary>
		/// Indicates the current member was invited to a board.
		/// </summary>
		InvitedToBoard,
		/// <summary>
		/// Indicates the current member was invited to an organization.
		/// </summary>
		InvitedToOrganization,
		/// <summary>
		/// Indicates the current member was removed from a board.
		/// </summary>
		RemovedFromBoard,
		/// <summary>
		/// Indicates the current member was removed from a card.
		/// </summary>
		RemovedFromCard,
		/// <summary>
		/// Indicates another member was removed from a card.
		/// </summary>
		RemovedMemberFromCard,
		/// <summary>
		/// Indicates the current member was removed from an organization.
		/// </summary>
		RemovedFromOrganization,
		/// <summary>
		/// Indicates the current member was mentioned on a card.
		/// </summary>
		MentionedOnCard,
		/// <summary>
		/// Indicates the current member is unconfirmed and invited to a board. ?
		/// </summary>
		UnconfirmedInvitedToBoard,
		/// <summary>
		/// Indicates the current member is unconfirmed and invited to an organization. ?
		/// </summary>
		UnconfirmedInvitedToOrganization,
		/// <summary>
		/// Indicates a checklist item was updated.
		/// </summary>
		UpdateCheckItemStateOnCard,
		/// <summary>
		/// Indicates the current member was made an admin of a board.
		/// </summary>
		MakeAdminOfBoard,
		/// <summary>
		/// Indicates the current member was made an admin of an organization.
		/// </summary>
		MakeAdminOfOrganization,
		/// <summary>
		/// Indicates a card due date is approaching.
		/// </summary>
		CardDueSoon		
	}
}
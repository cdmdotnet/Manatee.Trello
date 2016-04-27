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

using System;
using System.ComponentModel;

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
		[Description("addedAttachmentToCard")]
		AddedAttachmentToCard = 1 << 0,
		/// <summary>
		/// Indicates the current member was added to a board.
		/// </summary>
		[Description("addedToBoard")]
		AddedToBoard = 1 << 1,
		/// <summary>
		/// Indicates the current member was added to a card.
		/// </summary>
		[Description("addedToCard")]
		AddedToCard = 1 << 2,
		/// <summary>
		/// Indicates the current member was added to an organization.
		/// </summary>
		[Description("addedToOrganization")]
		AddedToOrganization = 1 << 3,
		/// <summary>
		/// Indicates another member was added to an card.
		/// </summary>
		[Description("addedMemberToCard")]
		AddedMemberToCard = 1 << 4,
		/// <summary>
		/// Indicates the current member was added to a board as an admin.
		/// </summary>
		[Description("addAdminToBoard")]
		AddAdminToBoard = 1 << 5,
		/// <summary>
		/// Indicates the current member was added to an organization as an admin.
		/// </summary>
		[Description("addAdminToOrganization")]
		AddAdminToOrganization = 1 << 6,
		/// <summary>
		/// Indicates a card was changed.
		/// </summary>
		[Description("changeCard")]
		ChangeCard = 1 << 7,
		/// <summary>
		/// Indicates a board was closed.
		/// </summary>
		[Description("closeBoard")]
		CloseBoard = 1 << 8,
		/// <summary>
		/// Indicates another member commented on a card.
		/// </summary>
		[Description("commentCard")]
		CommentCard = 1 << 9,
		/// <summary>
		/// Indicates another member created a card.
		/// </summary>
		[Description("createdCard")]
		CreatedCard = 1 << 10,
		/// <summary>
		/// Indicates the current member was removed from a board.
		/// </summary>
		[Description("removedFromBoard")]
		RemovedFromBoard = 1 << 11,
		/// <summary>
		/// Indicates the current member was removed from a card.
		/// </summary>
		[Description("removedFromCard")]
		RemovedFromCard = 1 << 12,
		/// <summary>
		/// Indicates another member was removed from a card.
		/// </summary>
		[Description("removedMemberFromCard")]
		RemovedMemberFromCard = 1 << 13,
		/// <summary>
		/// Indicates the current member was removed from an organization.
		/// </summary>
		[Description("removedFromOrganization")]
		RemovedFromOrganization = 1 << 14,
		/// <summary>
		/// Indicates the current member was mentioned on a card.
		/// </summary>
		[Description("mentionedOnCard")]
		MentionedOnCard = 1 << 15,
		/// <summary>
		/// Indicates a checklist item was updated.
		/// </summary>
		[Description("updateCheckItemStateOnCard")]
		UpdateCheckItemStateOnCard = 1 << 16,
		/// <summary>
		/// Indicates the current member was made an admin of a board.
		/// </summary>
		[Description("makeAdminOfBoard")]
		MakeAdminOfBoard = 1 << 17,
		/// <summary>
		/// Indicates the current member was made an admin of an organization.
		/// </summary>
		[Description("makeAdminOfOrganization")]
		MakeAdminOfOrganization = 1 << 18,
		/// <summary>
		/// Indicates a card due date is approaching.
		/// </summary>
		[Description("cardDueSoon")]
		CardDueSoon = 1 << 19,
		/// <summary>
		/// Indicates all notification types.
		/// </summary>
		[Description("all")]
		All = (1 << 20) - 1,
	}
}
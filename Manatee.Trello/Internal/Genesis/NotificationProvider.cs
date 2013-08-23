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
 
	File Name:		NotificationProvider.cs
	Namespace:		Manatee.Trello.Implementation
	Class Name:		NotificationProvider
	Purpose:		Provides transation services between the more generic
					Notification class and its more specific derived classes
					based on the Type property.

***************************************************************************************/

using System;
using System.Collections.Generic;

namespace Manatee.Trello.Internal.Genesis
{
	internal class NotificationProvider : IEntityProvider<Notification>
	{
		private static readonly Dictionary<NotificationType, Type> _typeMap;
		private static readonly NotificationProvider _default;

		public static NotificationProvider Default { get { return _default; } }

		static NotificationProvider()
		{
			_typeMap = new Dictionary<NotificationType, Type>
			           	{
			           		{NotificationType.AddedAttachmentToCard, typeof (AddedAttachmentToCardNotification)},
			           		{NotificationType.AddedToBoard, typeof (AddedToBoardNotification)},
			           		{NotificationType.AddedToCard, typeof (AddedToCardNotification)},
			           		{NotificationType.AddedToOrganization, typeof (AddedToOrganizationNotification)},
			           		{NotificationType.AddedMemberToCard, typeof (AddedMemberToCardNotification)},
			           		{NotificationType.AddAdminToBoard, typeof (AddAdminToBoardNotification)},
			           		{NotificationType.AddAdminToOrganization, typeof (AddAdminToOrganizationNotification)},
			           		{NotificationType.ChangeCard, typeof (ChangeCardNotification)},
			           		{NotificationType.CloseBoard, typeof (CloseBoardNotification)},
			           		{NotificationType.CommentCard, typeof (CommentCardNotification)},
			           		{NotificationType.CreatedCard, typeof (CreateCardNotification)},
			           		{NotificationType.InvitedToBoard, typeof (InvitedToBoardNotification)},
			           		{NotificationType.InvitedToOrganization, typeof (InvitedToOrganizationNotification)},
			           		{NotificationType.RemovedFromBoard, typeof (RemovedFromBoardNotification)},
			           		{NotificationType.RemovedFromCard, typeof (RemovedFromCardNotification)},
			           		{NotificationType.RemovedMemberFromCard, typeof (RemovedMemberFromCardNotification)},
			           		{NotificationType.RemovedFromOrganization, typeof (RemovedFromOrganizationNotification)},
			           		{NotificationType.MentionedOnCard, typeof (MentionedOnCardNotification)},
			           		{NotificationType.UnconfirmedInvitedToBoard, typeof (UnconfirmedInvitedToBoardNotification)},
			           		{NotificationType.UnconfirmedInvitedToOrganization, typeof (UnconfirmedInvitedToOrganizationNotification)},
			           		{NotificationType.UpdateCheckItemStateOnCard, typeof (UpdateCheckItemStateOnCardNotification)},
			           		{NotificationType.MakeAdminOfBoard, typeof (MakeAdminOfBoardNotification)},
			           		{NotificationType.MakeAdminOfOrganization, typeof (MakeAdminOfOrganizationNotification)},
			           		{NotificationType.CardDueSoon, typeof (CardDueSoonNotification)},
			           	};
			_default = new NotificationProvider();
		}

		public Notification Parse(Notification obj)
		{
			if (obj.Type == NotificationType.Unknown) return obj;
			var type = _typeMap[obj.Type];
			var newObj = (Notification) Activator.CreateInstance(type, new[] {obj});
			return newObj;
		}
	}
}

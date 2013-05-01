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
					Action class and its more specific derived classes
					based on the Type property.

***************************************************************************************/
using System;
using System.Collections.Generic;

namespace Manatee.Trello.Internal
{
	internal class ActionProvider : IEntityProvider<Action>
	{
		private static readonly Dictionary<ActionType, Type> _typeMap;

		static ActionProvider()
		{
			_typeMap = new Dictionary<ActionType, Type>
			           	{
			           		{ActionType.AddAttachmentToCard, typeof (AddAttachmentToCardAction)},
			           		{ActionType.AddChecklistToCard, typeof (AddCheckListToCardAction)},
			           		{ActionType.AddMemberToBoard, typeof (AddMemberToBoardAction)},
			           		{ActionType.AddMemberToCard, typeof (AddMemberToCardAction)},
			           		{ActionType.AddMemberToOrganization, typeof (AddMemberToOrganizationAction)},
			           		{ActionType.AddToOrganizationBoard, typeof (AddToOrganizationBoardAction)},
			           		{ActionType.CommentCard, typeof (CommentCardAction)},
			           		{ActionType.CopyCommentCard, typeof (CopyCommentCardAction)},
			           		{ActionType.ConvertToCardFromCheckItem, typeof (ConvertToCardFromCheckItemAction)},
			           		{ActionType.CopyBoard, typeof (CopyBoardAction)},
			           		{ActionType.CopyCard, typeof (CopyCardAction)},
			           		{ActionType.CreateBoard, typeof (CreateBoardAction)},
			           		{ActionType.CreateCard, typeof (CreateCardAction)},
			           		{ActionType.CreateList, typeof (CreateListAction)},
			           		{ActionType.CreateOrganization, typeof (CreateOrganizationAction)},
			           		{ActionType.DeleteAttachmentFromCard, typeof (DeleteAttachmentFromCardAction)},
			           		{ActionType.DeleteBoardInvitation, typeof (DeleteBoardInvitationAction)},
			           		{ActionType.DeleteOrganizationInvitation, typeof (DeleteOrganizationInvitationAction)},
			           		{ActionType.MakeAdminOfBoard, typeof (MakeAdminOfBoardAction)},
			           		{ActionType.MakeNormalMemberOfBoard, typeof (MakeNormalMemberOfBoardAction)},
			           		{ActionType.MakeNormalMemberOfOrganization, typeof (MakeNormalMemberOfOrganizationAction)},
			           		{ActionType.MakeObserverOfBoard, typeof (MakeObserverOfBoardAction)},
			           		{ActionType.MemberJoinedTrello, typeof (MemberJoinedTrelloAction)},
			           		{ActionType.MoveCardFromBoard, typeof (MoveCardFromBoardAction)},
			           		{ActionType.MoveListFromBoard, typeof (MoveListFromBoardAction)},
			           		{ActionType.MoveCardToBoard, typeof (MoveCardToBoardAction)},
			           		{ActionType.MoveListToBoard, typeof (MoveListToBoardAction)},
			           		{ActionType.RemoveAdminFromBoard, typeof (RemoveAdminFromBoardAction)},
			           		{ActionType.RemoveAdminFromOrganization, typeof (RemoveAdminFromOrganizationAction)},
			           		{ActionType.RemoveChecklistFromCard, typeof (RemoveChecklistFromCardAction)},
			           		{ActionType.RemoveFromOrganizationBoard, typeof (RemoveFromOrganizationBoardAction)},
			           		{ActionType.RemoveMemberFromCard, typeof (RemoveMemberFromCardAction)},
			           		{ActionType.UnconfirmedBoardInvitation, typeof (UnconfirmedBoardInvitationAction)},
			           		{ActionType.UnconfirmedOrganizationInvitation, typeof (UnconfirmedOrganizationInvitationAction)},
			           		{ActionType.UpdateBoard, typeof (UpdateBoardAction)},
			           		{ActionType.UpdateCard, typeof (UpdateCardAction)},
			           		{ActionType.UpdateCheckItemStateOnCard, typeof (UpdateCheckItemStateOnCardAction)},
			           		{ActionType.UpdateChecklist, typeof (UpdateChecklistAction)},
			           		{ActionType.UpdateMember, typeof (UpdateMemberAction)},
			           		{ActionType.UpdateOrganization, typeof (UpdateOrganizationAction)},
			           		{ActionType.UpdateCardIdList, typeof (UpdateCardIdListAction)},
			           		{ActionType.UpdateCardClosed, typeof (UpdateCardClosedAction)},
			           		{ActionType.UpdateCardDesc, typeof (UpdateCardDescAction)},
			           		{ActionType.UpdateCardName, typeof (UpdateCardNameAction)},
			           	};
		}

		public Action Parse(Action obj)
		{
			if (obj.Type == ActionType.Unknown) return obj;
			var type = _typeMap[obj.Type];
			var newObj = (Action) Activator.CreateInstance(type, new[] {obj});
			return newObj;
		}
	}
}

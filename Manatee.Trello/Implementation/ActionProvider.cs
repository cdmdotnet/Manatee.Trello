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

namespace Manatee.Trello.Implementation
{
	internal class ActionProvider : IEntityProvider<Action>
	{
		private static readonly Dictionary<ActionType, Type> _typeMap;

		static ActionProvider()
		{
			_typeMap = new Dictionary<ActionType, Type>
			           	{
			           		{ActionType.AddAttachmentToCard, null},
			           		{ActionType.AddChecklistToCard, null},
			           		{ActionType.AddMemberToBoard, typeof (AddMemberToBoardAction)},
			           		{ActionType.AddMemberToCard, typeof (AddMemberToCardAction)},
			           		{ActionType.AddMemberToOrganization, null},
			           		{ActionType.AddToOrganizationBoard, typeof (AddToOrganizationBoardAction)},
			           		{ActionType.CommentCard, typeof (CommentCardAction)},
			           		{ActionType.CopyCommentCard, null},
			           		{ActionType.ConvertToCardFromCheckItem, null},
			           		{ActionType.CopyBoard, null},
			           		{ActionType.CopyCard, null},
			           		{ActionType.CreateBoard, null},
			           		{ActionType.CreateCard, typeof (CreateCardAction)},
			           		{ActionType.CreateList, typeof (CreateListAction)},
			           		{ActionType.CreateOrganization, null},
			           		{ActionType.DeleteAttachmentFromCard, null},
			           		{ActionType.DeleteBoardInvitation, null},
			           		{ActionType.DeleteOrganizationInvitation, null},
			           		{ActionType.MakeAdminOfBoard, typeof (MakeAdminOfBoardAction)},
			           		{ActionType.MakeNormalMemberOfBoard, typeof (MakeNormalMemberOfBoardAction)},
			           		{ActionType.MakeNormalMemberOfOrganization, null},
			           		{ActionType.MakeObserverOfBoard, null},
			           		{ActionType.MemberJoinedTrello, null},
			           		{ActionType.MoveCardFromBoard, typeof (MoveCardFromBoardAction)},
			           		{ActionType.MoveListFromBoard, typeof (MoveListFromBoardAction)},
			           		{ActionType.MoveCardToBoard, typeof (MoveCardToBoardAction)},
			           		{ActionType.MoveListToBoard, typeof (MoveListToBoardAction)},
			           		{ActionType.RemoveAdminFromBoard, null},
			           		{ActionType.RemoveAdminFromOrganization, null},
			           		{ActionType.RemoveChecklistFromCard, null},
			           		{ActionType.RemoveFromOrganizationBoard, typeof (RemoveFromOrganizationBoardAction)},
			           		{ActionType.RemoveMemberFromCard, typeof (RemoveMemberFromCardAction)},
			           		{ActionType.UnconfirmedBoardInvitation, null},
			           		{ActionType.UnconfirmedOrganizationInvitation, null},
			           		{ActionType.UpdateBoard, typeof (UpdateBoardAction)},
			           		{ActionType.UpdateCard, typeof (UpdateCardAction)},
			           		{ActionType.UpdateCheckItemStateOnCard, typeof (UpdateCheckItemStateOnCardAction)},
			           		{ActionType.UpdateChecklist, null},
			           		{ActionType.UpdateMember, null},
			           		{ActionType.UpdateOrganization, null},
			           		{ActionType.UpdateCardIdList, null},
			           		{ActionType.UpdateCardClosed, null},
			           		{ActionType.UpdateCardDesc, null},
			           		{ActionType.UpdateCardName, null},
			           	};
		}

		public Action Parse(Action obj)
		{
			var type = _typeMap[obj.Type];
			var newObj = (Action) Activator.CreateInstance(type, new[] {obj});
			return newObj;
		}
	}
}

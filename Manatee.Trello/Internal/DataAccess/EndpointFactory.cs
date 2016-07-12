/***************************************************************************************

	Copyright 2013 Greg Dennis

	   Licensed under the Apache License, Version 2.0 (the "License");
	   you may not use this file except in compliance with the License.
	   You may obtain a copy of the License at

		 http://www.apache.org/licenses/LICENSE-2.0

	   Unless required by applicable law or agreed to in writing, software
	   distributed under the License is distributed on an "AS IS" BASIS,
	   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
	   See the License for the specific language governing permissions and
	   limitations under the License.
 
	File Name:		EndpointFactory.cs
	Namespace:		Manatee.Trello.Internal.Genesis
	Class Name:		EndpointFactory
	Purpose:		Implements IEndpointFactory.

***************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using Manatee.Trello.Rest;
// ReSharper disable ThrowingSystemException

namespace Manatee.Trello.Internal.DataAccess
{
	internal static class EndpointFactory
	{
		private static readonly Dictionary<EntityRequestType, Func<Endpoint>> _library;

		static EndpointFactory()
		{
			_library = new Dictionary<EntityRequestType, Func<Endpoint>>
				{
					{EntityRequestType.Action_Read_Refresh, () => new Endpoint(RestMethod.Get, "actions", "_id")},
					{EntityRequestType.Action_Write_Delete, () => new Endpoint(RestMethod.Delete, "actions", "_id")},
					{EntityRequestType.Attachment_Write_Delete, () => new Endpoint(RestMethod.Delete, "cards", "_cardId", "attachments", "_id")},
					{EntityRequestType.Board_Read_Actions, () => new Endpoint(RestMethod.Get, "boards", "_id", "actions")},
					{EntityRequestType.Board_Read_Cards, () => new Endpoint(RestMethod.Get, "boards", "_id", "cards")},
					{EntityRequestType.Board_Read_CardsForMember, () => new Endpoint(RestMethod.Get, "boards", "_id", "members", "_idMember", "cards")},
					{EntityRequestType.Board_Read_Labels, () => new Endpoint(RestMethod.Get, "boards", "_id", "labels")},
					{EntityRequestType.Board_Read_Lists, () => new Endpoint(RestMethod.Get, "boards", "_id", "lists")},
					{EntityRequestType.Board_Read_Members, () => new Endpoint(RestMethod.Get, "boards", "_id", "members")},
					{EntityRequestType.Board_Read_Memberships, () => new Endpoint(RestMethod.Get, "boards", "_id", "memberships")},
					{EntityRequestType.Board_Read_PersonalPrefs, () => new Endpoint(RestMethod.Get, "boards", "_id", "myPrefs")},
					{EntityRequestType.Board_Read_Refresh, () => new Endpoint(RestMethod.Get, "boards", "_id")},
					{EntityRequestType.Board_Write_AddLabel, () => new Endpoint(RestMethod.Post, "labels")},
					{EntityRequestType.Board_Write_AddList, () => new Endpoint(RestMethod.Post, "lists")},
					{EntityRequestType.Board_Write_AddOrUpdateMember, () => new Endpoint(RestMethod.Put, "boards", "_id", "members", "_memberId")},
					{EntityRequestType.Board_Write_PersonalPrefs, () => new Endpoint(RestMethod.Put, "boards", "_id", "myPrefs")},
					{EntityRequestType.Board_Write_RemoveLabel, () => new Endpoint(RestMethod.Delete, "boards", "_id", "labels", "_labelId")},
					{EntityRequestType.Board_Write_RemoveMember, () => new Endpoint(RestMethod.Delete, "boards", "_id", "members", "_memberId")},
					{EntityRequestType.Board_Write_Update, () => new Endpoint(RestMethod.Put, "boards", "_id")},
					{EntityRequestType.BoardMembership_Read_Refresh, () => new Endpoint(RestMethod.Get, "boards", "_boardId", "memberships", "_id")},
					{EntityRequestType.BoardMembership_Write_Update, () => new Endpoint(RestMethod.Put, "boards", "_boardId", "memberships", "_id")},
					{EntityRequestType.Card_Read_Actions, () => new Endpoint(RestMethod.Get, "cards", "_id", "actions")},
					{EntityRequestType.Card_Read_Attachments, () => new Endpoint(RestMethod.Get, "cards", "_id", "attachments")},
					{EntityRequestType.Card_Read_CheckLists, () => new Endpoint(RestMethod.Get, "cards", "_id", "checklists")},
					{EntityRequestType.Card_Read_Members, () => new Endpoint(RestMethod.Get, "cards", "_id", "members")},
					{EntityRequestType.Card_Read_MembersVoted, () => new Endpoint(RestMethod.Get, "cards", "_id", "membersVoted")},
					{EntityRequestType.Card_Read_Refresh, () => new Endpoint(RestMethod.Get, "cards", "_id")},
					{EntityRequestType.Card_Read_Stickers, () => new Endpoint(RestMethod.Get, "cards", "_id", "stickers")},
					{EntityRequestType.Card_Write_AddAttachment, () => new Endpoint(RestMethod.Post, "cards", "_id", "attachments")},
					{EntityRequestType.Card_Write_AddChecklist, () => new Endpoint(RestMethod.Post, "checklists")},
					{EntityRequestType.Card_Write_AddComment, () => new Endpoint(RestMethod.Post, "cards", "_id", "actions", "comments")},
					{EntityRequestType.Card_Write_AddLabel, () => new Endpoint(RestMethod.Post, "cards", "_id", "idLabels")},
					{EntityRequestType.Card_Write_AddSticker, () => new Endpoint(RestMethod.Post, "cards", "_id", "stickers")},
					{EntityRequestType.Card_Write_AssignMember, () => new Endpoint(RestMethod.Post, "cards", "_id", "idMembers")},
					{EntityRequestType.Card_Write_Delete, () => new Endpoint(RestMethod.Delete, "cards", "_id")},
					{EntityRequestType.Card_Write_RemoveLabel, () => new Endpoint(RestMethod.Delete, "cards", "_id", "idlabels", "_labelId")},
					{EntityRequestType.Card_Write_RemoveMember, () => new Endpoint(RestMethod.Delete, "cards", "_id", "members", "_memberId")},
					{EntityRequestType.Card_Write_Update, () => new Endpoint(RestMethod.Put, "cards", "_id")},
					{EntityRequestType.CheckItem_Read_Refresh, () => new Endpoint(RestMethod.Get, "checklists", "_checklistId", "checkItems", "_id")},
					{EntityRequestType.CheckItem_Write_Delete, () => new Endpoint(RestMethod.Delete, "checklists", "_checklistId", "checkItems", "_id")},
					{EntityRequestType.CheckItem_Write_Update, () => new Endpoint(RestMethod.Put, "cards", "_cardId", "checklist", "_checklistId", "checkItem", "_id")},
					{EntityRequestType.CheckList_Read_Refresh, () => new Endpoint(RestMethod.Get, "checklists", "_id")},
					{EntityRequestType.CheckList_Read_CheckItems, () => new Endpoint(RestMethod.Get, "checklists", "_id", "checkItems")},
					{EntityRequestType.CheckList_Write_AddCheckItem, () => new Endpoint(RestMethod.Post, "checklists", "_id", "checkItems")},
					{EntityRequestType.CheckList_Write_Delete, () => new Endpoint(RestMethod.Delete, "checklists", "_id")},
					{EntityRequestType.CheckList_Write_Update, () => new Endpoint(RestMethod.Put, "checklists", "_id")},
					{EntityRequestType.Label_Read_Refresh, () => new Endpoint(RestMethod.Get, "labels", "_id")},
					{EntityRequestType.Label_Write_Delete, () => new Endpoint(RestMethod.Delete, "labels", "_id")},
					{EntityRequestType.Label_Write_Update, () => new Endpoint(RestMethod.Put, "labels", "_id")},
					{EntityRequestType.List_Read_Actions, () => new Endpoint(RestMethod.Get, "lists", "_id", "actions")},
					{EntityRequestType.List_Read_Cards, () => new Endpoint(RestMethod.Get, "lists", "_id", "cards")},
					{EntityRequestType.List_Read_Refresh, () => new Endpoint(RestMethod.Get, "lists", "_id")},
					{EntityRequestType.List_Write_AddCard, () => new Endpoint(RestMethod.Post, "cards")},
					{EntityRequestType.List_Write_Update, () => new Endpoint(RestMethod.Put, "lists", "_id")},
					{EntityRequestType.Member_Read_Actions, () => new Endpoint(RestMethod.Get, "members", "_id", "actions")},
					{EntityRequestType.Member_Read_Boards, () => new Endpoint(RestMethod.Get, "members", "_id", "boards")},
					{EntityRequestType.Member_Read_Cards, () => new Endpoint(RestMethod.Get, "members", "_id", "cards")},
					{EntityRequestType.Member_Read_Notifications, () => new Endpoint(RestMethod.Get, "members", "_id", "notifications")},
					{EntityRequestType.Member_Read_Organizations, () => new Endpoint(RestMethod.Get, "members", "_id", "organizations")},
					{EntityRequestType.Member_Read_Refresh, () => new Endpoint(RestMethod.Get, "members", "_id")},
					{EntityRequestType.Member_Write_CreateBoard, () => new Endpoint(RestMethod.Post, "boards")},
					{EntityRequestType.Member_Write_CreateOrganization, () => new Endpoint(RestMethod.Post, "organizations")},
					{EntityRequestType.Member_Write_Update, () => new Endpoint(RestMethod.Put, "members", "_id")},
					{EntityRequestType.Notification_Read_Refresh, () => new Endpoint(RestMethod.Get, "notifications", "_id")},
					{EntityRequestType.Notification_Write_Update, () => new Endpoint(RestMethod.Put, "notifications", "_id")},
					{EntityRequestType.Organization_Read_Actions, () => new Endpoint(RestMethod.Get, "organizations", "_id", "actions")},
					{EntityRequestType.Organization_Read_Boards, () => new Endpoint(RestMethod.Get, "organizations", "_id", "boards")},
					{EntityRequestType.Organization_Read_Members, () => new Endpoint(RestMethod.Get, "organizations", "_id", "members")},
					{EntityRequestType.Organization_Read_Memberships, () => new Endpoint(RestMethod.Get, "organizations", "_id", "memberships")},
					{EntityRequestType.Organization_Read_Refresh, () => new Endpoint(RestMethod.Get, "organizations", "_id")},
					{EntityRequestType.Organization_Write_AddOrUpdateMember, () => new Endpoint(RestMethod.Put, "organizations", "_id", "members", "_memberId")},
					{EntityRequestType.Organization_Write_CreateBoard, () => new Endpoint(RestMethod.Post, "boards")},
					{EntityRequestType.Organization_Write_Delete, () => new Endpoint(RestMethod.Delete, "organizations", "_id")},
					{EntityRequestType.Organization_Write_RemoveMember, () => new Endpoint(RestMethod.Delete, "organizations", "_id", "members", "_memberId")},
					{EntityRequestType.Organization_Write_Update, () => new Endpoint(RestMethod.Put, "organizations", "_id")},
					{EntityRequestType.OrganizationMembership_Read_Refresh, () => new Endpoint(RestMethod.Get, "organizations", "_organizationId", "memberships", "_id")},
					{EntityRequestType.OrganizationMembership_Write_Update, () => new Endpoint(RestMethod.Put, "organizations", "_organizationId", "memberships", "_id")},
					{EntityRequestType.OrganizationPreferences_Read_Refresh, () => new Endpoint(RestMethod.Get, "organizations", "_id", "prefs")},
					{EntityRequestType.Service_Read_Me, () => new Endpoint(RestMethod.Get, "members", "me")},
					{EntityRequestType.Service_Read_Search, () => new Endpoint(RestMethod.Get, "search")},
					{EntityRequestType.Service_Read_SearchMembers, () => new Endpoint(RestMethod.Get, "search", "members")},
					{EntityRequestType.Serivce_Read_TypeQuery, () => new Endpoint(RestMethod.Get, "type", "_id")},
					{EntityRequestType.Sticker_Write_Delete, () => new Endpoint(RestMethod.Delete, "cards", "_cardId", "stickers", "_id")},
					{EntityRequestType.Sticker_Write_Update, () => new Endpoint(RestMethod.Put, "cards", "_cardId", "stickers", "_id")},
					{EntityRequestType.Token_Read_Refresh, () => new Endpoint(RestMethod.Get, "tokens", "_token")},
					{EntityRequestType.Token_Write_Delete, () => new Endpoint(RestMethod.Delete, "tokens", "_token")},
					{EntityRequestType.Webhook_Read_Refresh, () => new Endpoint(RestMethod.Get, "webhooks", "_id")},
					{EntityRequestType.Webhook_Write_Delete, () => new Endpoint(RestMethod.Delete, "webhooks", "_id")},
					{EntityRequestType.Webhook_Write_Entity, () => new Endpoint(RestMethod.Put, "webhooks")},
					{EntityRequestType.Webhook_Write_Update, () => new Endpoint(RestMethod.Put, "webhooks", "_id")},
				};
		}

		public static Endpoint Build(EntityRequestType requestType, IDictionary<string, object> parameters = null)
		{
			return BuildUrl(requestType, parameters);
		}

		private static Endpoint BuildUrl(EntityRequestType requestType, IDictionary<string, object> parameters)
		{
			var endpoint = _library[requestType]();
			var requiredParameters = endpoint.Where(p => p.StartsWith("_")).ToList();
			if (parameters != null)
			{
				foreach (var parameter in requiredParameters)
				{
					if (!parameters.ContainsKey(parameter))
						throw new Exception("Attempted to build endpoint with incomplete parameter collection.");
					var value = parameters[parameter] ?? string.Empty;
					endpoint.Resolve(parameter, value.ToString());
				}
			}
			else if (requiredParameters.Any())
				throw new Exception("Attempted to build endpoint with incomplete parameter collection.");
			return endpoint;
		}
	}
}
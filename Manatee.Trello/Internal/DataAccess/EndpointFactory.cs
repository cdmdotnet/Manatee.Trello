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

namespace Manatee.Trello.Internal.DataAccess
{
	internal static class EndpointFactory
	{
		private static readonly Dictionary<EntityRequestType, Func<Endpoint>> _library;

		static EndpointFactory()
		{
			_library = new Dictionary<EntityRequestType, Func<Endpoint>>
				{
					{EntityRequestType.Action_Read_Refresh, () => new Endpoint(RestMethod.Get, new[]{"actions","_id"})},
					{EntityRequestType.Attachment_Write_Delete, () => new Endpoint(RestMethod.Delete, new[]{"cards","_cardId","attachments","_id"})},
					{EntityRequestType.Board_Read_Actions, () => new Endpoint(RestMethod.Get, new[]{"boards","_id","actions"})},
					{EntityRequestType.Board_Read_Cards, () => new Endpoint(RestMethod.Get, new[]{"boards","_id","cards"})},
					{EntityRequestType.Board_Read_Lists, () => new Endpoint(RestMethod.Get, new[]{"boards","_id","lists"})},
					{EntityRequestType.Board_Read_Members, () => new Endpoint(RestMethod.Get, new[]{"boards","_id","members"})},
					{EntityRequestType.Board_Read_Memberships, () => new Endpoint(RestMethod.Get, new[]{"boards","_id","memberships"})},
					{EntityRequestType.Board_Read_Refresh, () => new Endpoint(RestMethod.Get, new[]{"boards","_id"})},
					{EntityRequestType.Board_Write_AddList, () => new Endpoint(RestMethod.Post, new[]{"lists"})},
					{EntityRequestType.Board_Write_AddOrUpdateMember, () => new Endpoint(RestMethod.Put, new[]{"boards","_id","members", "_memberId"})},
					{EntityRequestType.Board_Write_RemoveMember, () => new Endpoint(RestMethod.Delete, new[]{"boards","_id","members","_memberId"})},
					{EntityRequestType.Board_Write_Update, () => new Endpoint(RestMethod.Put, new[]{"boards", "_id"})},
					{EntityRequestType.BoardMembership_Read_Refresh, () => new Endpoint(RestMethod.Get, new[]{"boards","_boardId","memberships","_id"})},
					{EntityRequestType.BoardMembership_Write_Update, () => new Endpoint(RestMethod.Put, new[]{"boards","_boardId","memberships","_id"})},
					//{EntityRequestType.BoardPersonalPreferences_Read_Refresh, () => new Endpoint(RestMethod.Get, new[]{"boards","_boardId","myPrefs"})},
					//{EntityRequestType.BoardPersonalPreferences_Write_ShowListGuide, () => new Endpoint(RestMethod.Put, new[]{"boards","_boardId","myPrefs","showListGuide"})},
					//{EntityRequestType.BoardPersonalPreferences_Write_ShowSidebar, () => new Endpoint(RestMethod.Put, new[]{"boards","_boardId","myPrefs","showSidebar"})},
					//{EntityRequestType.BoardPersonalPreferences_Write_ShowSidebarActivity, () => new Endpoint(RestMethod.Put, new[]{"boards","_boardId","myPrefs","showSidebarActivity"})},
					//{EntityRequestType.BoardPersonalPreferences_Write_ShowSidebarBoardActions, () => new Endpoint(RestMethod.Put, new[]{"boards","_boardId","myPrefs","showSidebarBoardActions"})},
					//{EntityRequestType.BoardPersonalPreferences_Write_ShowSidebarMembers, () => new Endpoint(RestMethod.Put, new[]{"boards","_boardId","myPrefs","showSidebarMembers"})},
					{EntityRequestType.Card_Read_Actions, () => new Endpoint(RestMethod.Get, new[]{"cards","_id","actions"})},
					{EntityRequestType.Card_Read_Attachments, () => new Endpoint(RestMethod.Get, new[]{"cards","_id","attachments"})},
					{EntityRequestType.Card_Read_Members, () => new Endpoint(RestMethod.Get, new[]{"cards","_id","members"})},
					{EntityRequestType.Card_Read_Refresh, () => new Endpoint(RestMethod.Get, new[]{"cards","_id"})},
					{EntityRequestType.Card_Write_AddAttachment, () => new Endpoint(RestMethod.Post, new[]{"cards","_id","attachments"})},
					{EntityRequestType.Card_Write_AddChecklist, () => new Endpoint(RestMethod.Post, new[]{"checklists"})},
					{EntityRequestType.Card_Write_AddComment, () => new Endpoint(RestMethod.Post, new[]{"cards","_id","actions","comments"})},
					{EntityRequestType.Card_Write_AssignMember, () => new Endpoint(RestMethod.Post, new[]{"cards","_id","idMembers"})},
					{EntityRequestType.Card_Write_Delete, () => new Endpoint(RestMethod.Delete, new[]{"cards","_id"})},
					{EntityRequestType.Card_Write_RemoveMember, () => new Endpoint(RestMethod.Delete, new[]{"cards","_id","members","_memberId"})},
					{EntityRequestType.Card_Write_Update, () => new Endpoint(RestMethod.Put, new[]{"cards", "_id"})},
					{EntityRequestType.CheckItem_Read_Refresh, () => new Endpoint(RestMethod.Get, new[]{"checklists","_checkListId","checkItems","_id"})},
					{EntityRequestType.CheckItem_Write_Delete, () => new Endpoint(RestMethod.Delete, new[]{"checklists","_checkListId","checkItems","_id"})},
					{EntityRequestType.CheckItem_Write_Update, () => new Endpoint(RestMethod.Put, new[]{"cards","_cardId","checklist","_checkListId","checkItem","_id"})},
					{EntityRequestType.CheckList_Read_Refresh, () => new Endpoint(RestMethod.Get, new[]{"checklists","_id"})},
					{EntityRequestType.CheckList_Write_AddCheckItem, () => new Endpoint(RestMethod.Post, new[]{"checklists","_id","checkItems"})},
					{EntityRequestType.CheckList_Write_Delete, () => new Endpoint(RestMethod.Delete, new[]{"checklists","_id"})},
					{EntityRequestType.CheckList_Write_Update, () => new Endpoint(RestMethod.Put, new[]{"checklists","_id"})},
					{EntityRequestType.List_Read_Actions, () => new Endpoint(RestMethod.Get, new[]{"lists","_id","actions"})},
					{EntityRequestType.List_Read_Cards, () => new Endpoint(RestMethod.Get, new[]{"lists","_id","cards"})},
					{EntityRequestType.List_Read_Refresh, () => new Endpoint(RestMethod.Get, new[]{"lists","_id"})},
					{EntityRequestType.List_Write_AddCard, () => new Endpoint(RestMethod.Post, new[]{"cards"})},
					{EntityRequestType.List_Write_Update, () => new Endpoint(RestMethod.Put, new[]{"lists", "_id"})},
					{EntityRequestType.Member_Read_Actions, () => new Endpoint(RestMethod.Get, new[]{"members","_id","actions"})},
					{EntityRequestType.Member_Read_Boards, () => new Endpoint(RestMethod.Get, new[]{"members","_id","boards"})},
					{EntityRequestType.Member_Read_Organizations, () => new Endpoint(RestMethod.Get, new[]{"members","_id","organizations"})},
					{EntityRequestType.Member_Read_Refresh, () => new Endpoint(RestMethod.Get, new[]{"members","_id"})},
					{EntityRequestType.Member_Write_CreateBoard, () => new Endpoint(RestMethod.Post, new[]{"boards"})},
					{EntityRequestType.Member_Write_CreateOrganization, () => new Endpoint(RestMethod.Post, new[]{"organizations"})},
					{EntityRequestType.Member_Write_Update, () => new Endpoint(RestMethod.Put, new[]{"members", "_id"})},
					//{EntityRequestType.Notification_Read_Refresh, () => new Endpoint(RestMethod.Get, new[]{"notifications","_id"})},
					//{EntityRequestType.Notification_Write_IsUnread, () => new Endpoint(RestMethod.Put, new[]{"notifications","_id","unread"})},
					{EntityRequestType.Organization_Read_Actions, () => new Endpoint(RestMethod.Get, new[]{"organizations","_id","actions"})},
					{EntityRequestType.Organization_Read_Boards, () => new Endpoint(RestMethod.Get, new[]{"organizations","_id","boards"})},
					{EntityRequestType.Organization_Read_Members, () => new Endpoint(RestMethod.Get, new[]{"organizations","_id","members"})},
					{EntityRequestType.Organization_Read_Memberships, () => new Endpoint(RestMethod.Get, new[]{"organizations","_id","memberships"})},
					{EntityRequestType.Organization_Read_Refresh, () => new Endpoint(RestMethod.Get, new[]{"organizations","_id"})},
					{EntityRequestType.Organization_Write_AddOrUpdateMember, () => new Endpoint(RestMethod.Put, new[]{"organizations","_id","members","_memberId"})},
					{EntityRequestType.Organization_Write_CreateBoard, () => new Endpoint(RestMethod.Post, new[]{"boards"})},
					{EntityRequestType.Organization_Write_Delete, () => new Endpoint(RestMethod.Delete, new[]{"organizations","_id"})},
					{EntityRequestType.Organization_Write_RemoveMember, () => new Endpoint(RestMethod.Delete, new[]{"organizations","_id","members","_memberId"})},
					{EntityRequestType.Organization_Write_Update, () => new Endpoint(RestMethod.Put, new[]{"organizations", "_id"})},
					{EntityRequestType.OrganizationMembership_Read_Refresh, () => new Endpoint(RestMethod.Get, new[]{"organizations","_organizationId","memberships","_id"})},
					{EntityRequestType.OrganizationMembership_Write_Update, () => new Endpoint(RestMethod.Put, new[]{"organizations","_organizationId","memberships","_id"})},
					{EntityRequestType.Service_Read_Me, () => new Endpoint(RestMethod.Get, new[]{"members","me"})},
					{EntityRequestType.Service_Read_Search, () => new Endpoint(RestMethod.Get, new[]{"search"})},
					{EntityRequestType.Service_Read_SearchMembers, () => new Endpoint(RestMethod.Get, new[]{"search","members"})},
					{EntityRequestType.Token_Read_Refresh, () => new Endpoint(RestMethod.Get, new[]{"tokens","_token"})},
					{EntityRequestType.Token_Write_Delete, () => new Endpoint(RestMethod.Delete, new[]{"tokens","_token"})},
					{EntityRequestType.Webhook_Read_Refresh, () => new Endpoint(RestMethod.Get, new[]{"webhooks","_id"})},
					{EntityRequestType.Webhook_Write_Delete, () => new Endpoint(RestMethod.Delete, new[]{"webhooks","_id"})},
					{EntityRequestType.Webhook_Write_Entity, () => new Endpoint(RestMethod.Put, new[]{"webhooks"})},
					{EntityRequestType.Webhook_Write_Update, () => new Endpoint(RestMethod.Put, new[]{"webhooks", "_id"})},
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
					parameters.Remove(parameter);
				}
			}
			else if (requiredParameters.Any())
				throw new Exception("Attempted to build endpoint with incomplete parameter collection.");
			return endpoint;
		}
	}
}
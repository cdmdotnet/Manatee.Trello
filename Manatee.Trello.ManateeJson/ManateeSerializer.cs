﻿/***************************************************************************************

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
 
	File Name:		Serializer.cs
	Namespace:		Manatee.Trello.ManateeJson
	Class Name:		Serializer
	Purpose:		Wrapper class for the Manatee.Json.Serializer which implements
					ISerializer and IDeserializer.

***************************************************************************************/

using System;
using System.Reflection;
using Manatee.Json;
using Manatee.Json.Serialization;
using Manatee.Trello.ManateeJson.Entities;
using Manatee.Trello.Json;
using Manatee.Trello.Rest;

namespace Manatee.Trello.ManateeJson
{
	/// <summary>
	/// Wrapper class for the Manatee.Json.Serializer for use with RestSharp.
	/// </summary>
	public class ManateeSerializer : ISerializer, IDeserializer
	{
		private readonly JsonSerializer _serializer;
		private readonly MethodInfo _method;

		static ManateeSerializer()
		{
			InitializeTypeRegistry();
			InitializeAbstractionMap();
		}
		/// <summary>
		/// Creates and initializes a new instance of the ManateeJsonSerializer class.
		/// </summary>
		public ManateeSerializer()
		{
			_serializer = new JsonSerializer
				{
					Options =
						{
							EnumSerializationFormat = EnumSerializationFormat.AsName,
							FlagsEnumSeparator = ","
						}
				};
			_method = _serializer.GetType().GetMethod("Serialize");
		}

		/// <summary>
		/// Serializes an object to JSON.
		/// </summary>
		/// <param name="obj">The object to serialize.</param>
		/// <returns>An equivalent JSON string.</returns>
		public string Serialize(object obj)
		{
			var method = _method.MakeGenericMethod(new[] {obj.GetType()});
			var json = method.Invoke(_serializer, new[] {obj});
			var text = json.ToString();
			return text;
		}
		/// <summary>
		/// Attempts to deserialize a RESTful response to the indicated type.
		/// </summary>
		/// <typeparam name="T">The type of object expected.</typeparam>
		/// <param name="response">The response object which contains the JSON to deserialize.</param>
		/// <returns>The requested object, if JSON is valid; null otherwise.</returns>
		public T Deserialize<T>(IRestResponse<T> response)
		{
			var json = JsonValue.Parse(response.Content);
			T obj = _serializer.Deserialize<T>(json);
			return obj;
		}
		/// <summary>
		/// Attempts to deserialize a RESTful response to the indicated type.
		/// </summary>
		/// <typeparam name="T">The type of object expected.</typeparam>
		/// <param name="content">A string which contains the JSON to deserialize.</param>
		/// <returns>The requested object, if JSON is valid; null otherwise.</returns>
		public T Deserialize<T>(string content)
		{
			var json = JsonValue.Parse(content);
			T obj = _serializer.Deserialize<T>(json);
			return obj;
		}

		private static void InitializeTypeRegistry()
		{
			JsonSerializationTypeRegistry.RegisterListType<int>();
			JsonSerializationTypeRegistry.RegisterListType<string>();
			JsonSerializationTypeRegistry.RegisterListType<IJsonAction>();
			JsonSerializationTypeRegistry.RegisterListType<IJsonAttachment>();
			JsonSerializationTypeRegistry.RegisterListType<IJsonAttachmentPreview>();
			JsonSerializationTypeRegistry.RegisterListType<IJsonBoard>();
			JsonSerializationTypeRegistry.RegisterListType<IJsonBoardMembership>();
			JsonSerializationTypeRegistry.RegisterListType<IJsonCard>();
			JsonSerializationTypeRegistry.RegisterListType<IJsonCheckItem>();
			JsonSerializationTypeRegistry.RegisterListType<IJsonCheckList>();
			JsonSerializationTypeRegistry.RegisterListType<IJsonLabel>();
			JsonSerializationTypeRegistry.RegisterListType<IJsonList>();
			JsonSerializationTypeRegistry.RegisterListType<IJsonMember>();
			JsonSerializationTypeRegistry.RegisterListType<IJsonMemberSession>();
			JsonSerializationTypeRegistry.RegisterListType<IJsonNotification>();
			JsonSerializationTypeRegistry.RegisterListType<IJsonOrganization>();
			JsonSerializationTypeRegistry.RegisterListType<IJsonOrganizationMembership>();
			JsonSerializationTypeRegistry.RegisterListType<IJsonToken>();
			JsonSerializationTypeRegistry.RegisterListType<IJsonTokenPermission>();
			JsonSerializationTypeRegistry.RegisterNullableType<DateTime>();
		}
		private static void InitializeAbstractionMap()
		{
			JsonSerializationAbstractionMap.Map<IJsonAction, ManateeAction>();
			JsonSerializationAbstractionMap.Map<IJsonActionData, ManateeActionData>();
			JsonSerializationAbstractionMap.Map<IJsonActionOldData, ManateeActionOldData>();
			JsonSerializationAbstractionMap.Map<IJsonAttachment, ManateeAttachment>();
			JsonSerializationAbstractionMap.Map<IJsonAttachmentPreview, ManateeAttachmentPreview>();
			JsonSerializationAbstractionMap.Map<IJsonBadges, ManateeBadges>();
			JsonSerializationAbstractionMap.Map<IJsonBoard, ManateeBoard>();
			JsonSerializationAbstractionMap.Map<IJsonBoardMembership, ManateeBoardMembership>();
			JsonSerializationAbstractionMap.Map<IJsonBoardPersonalPreferences, ManateeBoardPersonalPreferences>();
			JsonSerializationAbstractionMap.Map<IJsonBoardPreferences, ManateeBoardPreferences>();
			JsonSerializationAbstractionMap.Map<IJsonBoardVisibilityRestrict, ManateeBoardVisibilityRestrict>();
			JsonSerializationAbstractionMap.Map<IJsonCard, ManateeCard>();
			JsonSerializationAbstractionMap.Map<IJsonCheckItem, ManateeCheckItem>();
			JsonSerializationAbstractionMap.Map<IJsonCheckList, ManateeCheckList>();
			JsonSerializationAbstractionMap.Map<IJsonLabel, ManateeLabel>();
			JsonSerializationAbstractionMap.Map<IJsonLabelNames, ManateeLabelNames>();
			JsonSerializationAbstractionMap.Map<IJsonList, ManateeList>();
			JsonSerializationAbstractionMap.Map<IJsonMember, ManateeMember>();
			JsonSerializationAbstractionMap.Map<IJsonMemberSearch, ManateeMemberSearch>();
			JsonSerializationAbstractionMap.Map<IJsonMemberSession, ManateeMemberSession>();
			JsonSerializationAbstractionMap.Map<IJsonMemberPreferences, ManateeMemberPreferences>();
			JsonSerializationAbstractionMap.Map<IJsonNotification, ManateeNotification>();
			JsonSerializationAbstractionMap.Map<IJsonNotificationData, ManateeNotificationData>();
			JsonSerializationAbstractionMap.Map<IJsonNotificationOldData, ManateeNotificationOldData>();
			JsonSerializationAbstractionMap.Map<IJsonOrganization, ManateeOrganization>();
			JsonSerializationAbstractionMap.Map<IJsonOrganizationMembership, ManateeOrganizationMembership>();
			JsonSerializationAbstractionMap.Map<IJsonOrganizationPreferences, ManateeOrganizationPreferences>();
			JsonSerializationAbstractionMap.Map<IJsonParameter, ManateeParameter>();
			JsonSerializationAbstractionMap.Map<IJsonPosition, ManateePosition>();
			JsonSerializationAbstractionMap.Map<IJsonSearch, ManateeSearch>();
			JsonSerializationAbstractionMap.Map<IJsonToken, ManateeToken>();
			JsonSerializationAbstractionMap.Map<IJsonTokenPermission, ManateeTokenPermission>();
			JsonSerializationAbstractionMap.Map<IJsonWebhook, ManateeWebhook>();
			JsonSerializationAbstractionMap.Map<IJsonWebhookNotification, ManateeWebhookNotification>();
		}
	}
}

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
 
	File Name:		ManateeNotification.cs
	Namespace:		Manatee.Trello.ManateeJson.Entities
	Class Name:		ManateeNotification
	Purpose:		Implements IJsonNotification for Manatee.Json.

***************************************************************************************/
using System;
using Manatee.Json;
using Manatee.Json.Serialization;
using Manatee.Trello.Json;

namespace Manatee.Trello.ManateeJson.Entities
{
	internal class ManateeNotification : IJsonNotification, IJsonSerializable
	{
		public string Id { get; set; }
		public bool? Unread { get; set; }
		public NotificationType? Type { get; set; }
		public DateTime? Date { get; set; }
		public IJsonNotificationData Data { get; set; }
		public IJsonMember MemberCreator { get; set; }

		public void FromJson(JsonValue json, JsonSerializer serializer)
		{
			if (json.Type != JsonValueType.Object) return;
			var obj = json.Object;
			Id = obj.TryGetString("id");
			MemberCreator = obj.Deserialize<IJsonMember>(serializer, "idMemberCreator");
			Data = obj.Deserialize<IJsonNotificationData>(serializer, "data");
			Unread = obj.TryGetBoolean("unread");
			Type = obj.Deserialize<NotificationType?>(serializer, "type");
#if IOS
			var dateString = obj.TryGetString("date");
			DateTime date;
			if (DateTime.TryParse(dateString, out date))
				Date = date;
#else
			Date = obj.Deserialize<DateTime?>(serializer, "date");
#endif
		}
		public JsonValue ToJson(JsonSerializer serializer)
		{
			var json = new JsonObject();
			Id.Serialize(json, serializer, "id");
			Unread.Serialize(json, serializer, "unread");
			return json;
		}
	}
}

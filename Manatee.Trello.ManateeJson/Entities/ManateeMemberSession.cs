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
 
	File Name:		ManateeMemberSession.cs
	Namespace:		Manatee.Trello.ManateeJson.Entities
	Class Name:		ManateeMemberSession
	Purpose:		Implements IJsonMemberSession for Manatee.Json.

***************************************************************************************/
using System;
using Manatee.Json;
using Manatee.Json.Serialization;
using Manatee.Trello.Json;

namespace Manatee.Trello.ManateeJson.Entities
{
	internal class ManateeMemberSession : IJsonMemberSession, IJsonSerializable
	{
		public bool? IsCurrent { get; set; }
		public bool? IsRecent { get; set; }
		public string Id { get; set; }
		public DateTime? DateCreated { get; set; }
		public DateTime? DateExpires { get; set; }
		public DateTime? DateLastUsed { get; set; }
		public string IpAddress { get; set; }
		public string Type { get; set; }
		public string UserAgent { get; set; }

		public void FromJson(JsonValue json, JsonSerializer serializer)
		{
			if (json.Type != JsonValueType.Object) return;
			var obj = json.Object;
			Id = obj.TryGetString("id");
			IsCurrent = obj.TryGetBoolean("isCurrent");
			IsRecent = obj.TryGetBoolean("isRecent");
#if IOS
			var dateString = obj.TryGetString("dateCreated");
			DateTime date;
			if (DateTime.TryParse(dateString, out date))
				DateCreated = date;
			dateString = obj.TryGetString("dateExpires");
			if (DateTime.TryParse(dateString, out date))
				DateExpires = date;
			dateString = obj.TryGetString("dateLastUsed");
			if (DateTime.TryParse(dateString, out date))
				DateLastUsed = date;
#else
			DateCreated = obj.Deserialize<DateTime?>(serializer, "dateCreated");
			DateExpires = obj.Deserialize<DateTime?>(serializer, "dateExpires");
			DateLastUsed = obj.Deserialize<DateTime?>(serializer, "dateLastUsed");
#endif
			IpAddress = obj.TryGetString("ipAddress");
			Type = obj.TryGetString("type");
			UserAgent = obj.TryGetString("userAgent");
		}
		public JsonValue ToJson(JsonSerializer serializer)
		{
			return null;
		}
	}
}
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
using Manatee.Json.Enumerations;
using Manatee.Json.Extensions;
using Manatee.Json.Serialization;
using Manatee.Trello.Json;

namespace Manatee.Trello.ManateeJson.Entities
{
	internal class ManateeMemberSession : IJsonMemberSession, IJsonCompatible
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

		public void FromJson(JsonValue json)
		{
			if (json.Type != JsonValueType.Object) return;
			var obj = json.Object;
			Id = obj.TryGetString("id");
			IsCurrent = obj.TryGetBoolean("isCurrent");
			IsRecent = obj.TryGetBoolean("isRecent");
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
			IpAddress = obj.TryGetString("ipAddress");
			Type = obj.TryGetString("type");
			UserAgent = obj.TryGetString("userAgent");
		}
		public JsonValue ToJson()
		{
			return new JsonObject
			       	{
			       		{"isCurrent", IsCurrent},
			       		{"isRecent", IsRecent},
			       		{"id", Id},
			       		{"dateCreated", DateCreated.HasValue ? DateCreated.Value.ToString("yyyy-MM-ddTHH:mm:ss.fffZ") : JsonValue.Null},
			       		{"dateExpires", DateExpires.HasValue ? DateExpires.Value.ToString("yyyy-MM-ddTHH:mm:ss.fffZ") : JsonValue.Null},
			       		{"dateLastUsed", DateLastUsed.HasValue ? DateLastUsed.Value.ToString("yyyy-MM-ddTHH:mm:ss.fffZ") : JsonValue.Null},
			       		{"ipAddress", IpAddress},
			       		{"type", Type},
			       		{"userAgent", UserAgent},
			       	};
		}
	}
}
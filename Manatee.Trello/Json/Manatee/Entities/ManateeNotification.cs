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
	Namespace:		Manatee.Trello.Json.Manatee.Entities
	Class Name:		ManateeNotification
	Purpose:		Implements IJsonNotification for Manatee.Json.

***************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Manatee.Json;
using Manatee.Json.Enumerations;
using Manatee.Json.Extensions;
using Manatee.Json.Serialization;

namespace Manatee.Trello.Json.Manatee.Entities
{
	internal class ManateeNotification : IJsonNotification, IJsonCompatible
	{
		public string Id { get; set; }
		public bool? Unread { get; set; }
		public string Type { get; set; }
		public DateTime? Date { get; set; }
		public object Data { get; set; }
		public string IdMemberCreator { get; set; }

		public void FromJson(JsonValue json)
		{
			if (json.Type != JsonValueType.Object) return;
			var obj = json.Object;
			Id = obj.TryGetString("id");
			Unread = obj.TryGetBoolean("unread");
			Type = obj.TryGetString("type");
			var dateString = obj.TryGetString("date");
			DateTime date;
			if (DateTime.TryParse(dateString, out date))
				Date = date;
			Data = obj.TryGetObject("data");
			IdMemberCreator = obj.TryGetString("idMemberCreator");
		}
		public JsonValue ToJson()
		{
			return new JsonObject
			       	{
			       		{"id", Id},
			       		{"unread", Unread},
			       		{"type", Type},
			       		{"date", Date.HasValue ? Date.Value.ToString("yyyy-MM-ddTHH:mm:ss.fffZ") : JsonValue.Null},
			       		{"data", Data as JsonObject},
			       		{"idMemberCreator", IdMemberCreator},
			       	};
		}
	}
}

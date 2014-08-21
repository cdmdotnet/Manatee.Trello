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
 
	File Name:		ManateeBadges.cs
	Namespace:		Manatee.Trello.ManateeJson.Entities
	Class Name:		ManateeBadges
	Purpose:		Implements IJsonBadges for Manatee.Json.

***************************************************************************************/
using System;
using Manatee.Json;
using Manatee.Json.Serialization;
using Manatee.Trello.Json;

namespace Manatee.Trello.ManateeJson.Entities
{
	internal class ManateeBadges : IJsonBadges, IJsonSerializable
	{
		public int? Votes { get; set; }
		public bool? ViewingMemberVoted { get; set; }
		public bool? Subscribed { get; set; }
		public string Fogbugz { get; set; }
		public DateTime? Due { get; set; }
		public bool? Description { get; set; }
		public int? Comments { get; set; }
		public int? CheckItemsChecked { get; set; }
		public int? CheckItems { get; set; }
		public int? Attachments { get; set; }

		public void FromJson(JsonValue json, JsonSerializer serializer)
		{
			if (json.Type != JsonValueType.Object) return;
			var obj = json.Object;
			Votes = (int?) obj.TryGetNumber("votes");
			ViewingMemberVoted = obj.TryGetBoolean("viewingMemberVoted");
			Subscribed = obj.TryGetBoolean("subscribed");
			Fogbugz = obj.TryGetString("fogbugz");
#if IOS
			var dateString = obj.TryGetString("due");
			DateTime date;
			if (DateTime.TryParse(dateString, out date))
				Due = date;
#else
			Due = obj.Deserialize<DateTime?>(serializer, "due");
#endif
			Description = obj.TryGetBoolean("description");
			Comments = (int?)obj.TryGetNumber("comments");
			CheckItemsChecked = (int?)obj.TryGetNumber("checkItemsChecked");
			CheckItems = (int?)obj.TryGetNumber("checkItems");
			Attachments = (int?)obj.TryGetNumber("attachments");
		}
		public JsonValue ToJson(JsonSerializer serializer)
		{
			return null;
		}
	}
}

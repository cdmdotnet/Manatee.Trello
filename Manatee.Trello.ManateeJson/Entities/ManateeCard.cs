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
 
	File Name:		ManateeCard.cs
	Namespace:		Manatee.Trello.ManateeJson.Entities
	Class Name:		ManateeCard
	Purpose:		Implements IJsonCard for Manatee.Json.

***************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using Manatee.Json;
using Manatee.Json.Serialization;
using Manatee.Trello.Json;

namespace Manatee.Trello.ManateeJson.Entities
{
	internal class ManateeCard : IJsonCard, IJsonSerializable
	{
		public string Id { get; set; }
		public IJsonBadges Badges { get; set; }
		public bool? Closed { get; set; }
		public DateTime? DateLastActivity { get; set; }
		public string Desc { get; set; }
		public DateTime? Due { get; set; }
		public IJsonBoard Board { get; set; }
		public IJsonList List { get; set; }
		public int? IdShort { get; set; }
		public string IdAttachmentCover { get; set; }
		public List<IJsonLabel> Labels { get; set; }
		public bool? ManualCoverAttachment { get; set; }
		public string Name { get; set; }
		public IJsonPosition Pos { get; set; }
		public string Url { get; set; }
		public string ShortUrl { get; set; }
		public bool? Subscribed { get; set; }
		public IJsonCard CardSource { get; set; }
		public object UrlSource { get; set; }

		public void FromJson(JsonValue json, JsonSerializer serializer)
		{
			switch (json.Type)
			{
				case JsonValueType.Object:
					var obj = json.Object;
					Id = obj.TryGetString("id");
					Badges = obj.Deserialize<IJsonBadges>(serializer, "badges");
					Closed = obj.TryGetBoolean("closed");
#if IOS
					var dateString = obj.TryGetString("dateLastActivity");
					DateTime date;
					if (DateTime.TryParse(dateString, out date))
						DateLastActivity = date;
					dateString = obj.TryGetString("due");
					if (DateTime.TryParse(dateString, out date))
						Due = date;
#else
					DateLastActivity = obj.Deserialize<DateTime?>(serializer, "dateLastActivity");
					Due = obj.Deserialize<DateTime?>(serializer, "due");
#endif
					Desc = obj.TryGetString("desc");
					Board = obj.Deserialize<IJsonBoard>(serializer, "idBoard");
					List = obj.Deserialize<IJsonList>(serializer, "idList");
					IdShort = (int?) obj.TryGetNumber("idShort");
					IdAttachmentCover = obj.TryGetString("idAttachmentCover");
					Labels = obj.Deserialize<List<IJsonLabel>>(serializer, "labels");
					ManualCoverAttachment = obj.TryGetBoolean("manualAttachmentCover");
					Name = obj.TryGetString("name");
					Pos = obj.Deserialize<IJsonPosition>(serializer, "pos");
					Url = obj.TryGetString("url");
					ShortUrl = obj.TryGetString("shortUrl");
					Subscribed = obj.TryGetBoolean("subscribed");
					break;
				case JsonValueType.String:
					Id = json.String;
					break;
			}
		}
		public JsonValue ToJson(JsonSerializer serializer)
		{
			var json = new JsonObject();
			Id.Serialize(json, serializer, "id");
			Closed.Serialize(json, serializer, "closed");
			Desc.Serialize(json, serializer, "desc");
			Due.Serialize(json, serializer, "due");
			List.SerializeId(json, "idList");
			Name.Serialize(json, serializer, "name");
			Pos.Serialize(json, serializer, "pos");
			Subscribed.Serialize(json, serializer, "subscribed");
			CardSource.SerializeId(json, "idCardSource");
			UrlSource.Serialize(json, serializer, "urlSource");
			// Don't serialize the Label collection because Trello wants a comma-sparated list
			if (Labels != null)
				Labels.Select(l => l.Color).Combine().Serialize(json, serializer, "labels");
			return json;
		}
	}
}

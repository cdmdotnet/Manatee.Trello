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
 
	File Name:		ManateeAttachment.cs
	Namespace:		Manatee.Trello.ManateeJson.Entities
	Class Name:		ManateeAttachment
	Purpose:		Implements IJsonAttachment for Manatee.Json.

***************************************************************************************/
using System;
using System.Collections.Generic;
using Manatee.Json;
using Manatee.Json.Serialization;
using Manatee.Trello.Json;

namespace Manatee.Trello.ManateeJson.Entities
{
	internal class ManateeAttachment : IJsonAttachment, IJsonSerializable
	{
		public string Id { get; set; }
		public int? Bytes { get; set; }
		public DateTime? Date { get; set; }
		public IJsonMember Member { get; set; }
		public bool? IsUpload { get; set; }
		public string MimeType { get; set; }
		public string Name { get; set; }
		public List<IJsonAttachmentPreview> Previews { get; set; }
		public string Url { get; set; }

		public void FromJson(JsonValue json, JsonSerializer serializer)
		{
			if (json.Type != JsonValueType.Object) return;
			var obj = json.Object;
			Id = obj.TryGetString("id");
			Bytes = (int?) obj.TryGetNumber("bytes");
			Date = obj.Deserialize<DateTime?>(serializer, "date");
			Member = obj.Deserialize<IJsonMember>(serializer, "idMember");
			IsUpload = obj.TryGetBoolean("isUpload");
			MimeType = obj.TryGetString("mimeType");
			Name = obj.TryGetString("name");
			Previews = obj.Deserialize<List<IJsonAttachmentPreview>>(serializer, "previews");
			Url = obj.TryGetString("url");
		}
		public JsonValue ToJson(JsonSerializer serializer)
		{
			var json = new JsonObject
			       	{
			       		{"id", Id},
			       		{"bytes", Bytes},
			       		{"date", serializer.Serialize(Date)},
			       		{"isUpload", IsUpload},
			       		{"mimeType", MimeType},
			       		{"name", Name},
			       		{"previews", serializer.Serialize(Previews)},
			       		{"url", Url}
			       	};
			Member.SerializeId(json, serializer, "idMember");
			return json;
		}
	}
}

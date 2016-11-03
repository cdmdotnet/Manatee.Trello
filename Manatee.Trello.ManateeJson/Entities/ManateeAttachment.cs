﻿using System;
using System.Collections.Generic;
using System.Globalization;
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
		public List<IJsonImagePreview> Previews { get; set; }
		public string Url { get; set; }

		public void FromJson(JsonValue json, JsonSerializer serializer)
		{
			if (json.Type != JsonValueType.Object) return;
			var obj = json.Object;
			Id = obj.TryGetString("id");
			Bytes = (int?) obj.TryGetNumber("bytes");
#if IOS
			var dateString = obj.TryGetString("date");
			DateTime date;
			if (DateTime.TryParseExact(dateString, "yyyy-MM-ddThh:mm:ss.fffZ", CultureInfo.CurrentCulture, DateTimeStyles.None, out date))
				Date = date.ToLocalTime();
#else
			Date = obj.Deserialize<DateTime?>(serializer, "date");
#endif
			Member = obj.Deserialize<IJsonMember>(serializer, "idMember");
			IsUpload = obj.TryGetBoolean("isUpload");
			MimeType = obj.TryGetString("mimeType");
			Name = obj.TryGetString("name");
			Previews = obj.Deserialize<List<IJsonImagePreview>>(serializer, "previews");
			Url = obj.TryGetString("url");
		}
		public JsonValue ToJson(JsonSerializer serializer)
		{
			return null;
		}
	}
}

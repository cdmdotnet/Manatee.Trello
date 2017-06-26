using System;
using System.Globalization;
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
			DateCreated = obj.Deserialize<DateTime?>(serializer, "dateCreated");
			DateExpires = obj.Deserialize<DateTime?>(serializer, "dateExpires");
			DateLastUsed = obj.Deserialize<DateTime?>(serializer, "dateLastUsed");
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
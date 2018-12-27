using System;
using System.Collections.Generic;
using Manatee.Json;
using Manatee.Json.Serialization;

namespace Manatee.Trello.Json.Entities
{
	internal class ManateeToken : IJsonToken, IJsonSerializable
	{
		public string Id { get; set; }
		public string TokenValue { get; set; }
		public string Identifier { get; set; }
		public IJsonMember Member { get; set; }
		public DateTime? DateCreated { get; set; }
		public DateTime? DateExpires { get; set; }
		public List<IJsonTokenPermission> Permissions { get; set; }

		public void FromJson(JsonValue json, JsonSerializer serializer)
		{
			if (json.Type != JsonValueType.Object) return;
			var obj = json.Object;
			Id = obj.TryGetString("id");
			Identifier = obj.TryGetString("identifier");
			Member = obj.Deserialize<IJsonMember>(serializer, "member") ?? obj.Deserialize<IJsonMember>(serializer, "idMember");
			DateCreated = obj.Deserialize<DateTime?>(serializer, "dateCreated");
			DateExpires = obj.Deserialize<DateTime?>(serializer, "dateExpires");
			Permissions = obj.Deserialize<List<IJsonTokenPermission>>(serializer, "permissions");
		}
		public JsonValue ToJson(JsonSerializer serializer)
		{
			return null;
		}
	}
}
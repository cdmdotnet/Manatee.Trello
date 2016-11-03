using System;
using System.Collections.Generic;
using System.Globalization;
using Manatee.Json;
using Manatee.Json.Serialization;
using Manatee.Trello.Json;

namespace Manatee.Trello.ManateeJson.Entities
{
	internal class ManateeToken : IJsonToken, IJsonSerializable
	{
		public string Id { get; set; }
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
			Member = obj.Deserialize<IJsonMember>(serializer, "idMember");
#if IOS
			var dateString = obj.TryGetString("dateCreated");
			DateTime date;
			if (DateTime.TryParseExact(dateString, "yyyy-MM-ddThh:mm:ss.fffZ", CultureInfo.CurrentCulture, DateTimeStyles.None, out date))
				DateCreated = date.ToLocalTime();
			dateString = obj.TryGetString("dateExpires");
			if (DateTime.TryParseExact(dateString, "yyyy-MM-ddThh:mm:ss.fffZ", CultureInfo.CurrentCulture, DateTimeStyles.None, out date))
				DateExpires = date.ToLocalTime();
#else
			DateCreated = obj.Deserialize<DateTime?>(serializer, "dateCreated");
			DateExpires = obj.Deserialize<DateTime?>(serializer, "dateExpires");
#endif
			Permissions = obj.Deserialize<List<IJsonTokenPermission>>(serializer, "permissions");
		}
		public JsonValue ToJson(JsonSerializer serializer)
		{
			return null;
		}
	}
}
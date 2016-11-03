using System;
using System.Globalization;
using Manatee.Json;
using Manatee.Json.Serialization;
using Manatee.Trello.Json;

namespace Manatee.Trello.ManateeJson.Entities
{
	internal class ManateeNotification : IJsonNotification, IJsonSerializable
	{
		public string Id { get; set; }
		public bool? Unread { get; set; }
		public NotificationType? Type { get; set; }
		public DateTime? Date { get; set; }
		public IJsonNotificationData Data { get; set; }
		public IJsonMember MemberCreator { get; set; }

		public void FromJson(JsonValue json, JsonSerializer serializer)
		{
			if (json.Type != JsonValueType.Object) return;
			var obj = json.Object;
			Id = obj.TryGetString("id");
			MemberCreator = obj.Deserialize<IJsonMember>(serializer, "idMemberCreator");
			Data = obj.Deserialize<IJsonNotificationData>(serializer, "data");
			Unread = obj.TryGetBoolean("unread");
			Type = obj.Deserialize<NotificationType?>(serializer, "type");
#if IOS
			var dateString = obj.TryGetString("date");
			DateTime date;
			if (DateTime.TryParseExact(dateString, "yyyy-MM-ddThh:mm:ss.fffZ", CultureInfo.CurrentCulture, DateTimeStyles.None, out date))
				Date = date.ToLocalTime();
#else
			Date = obj.Deserialize<DateTime?>(serializer, "date");
#endif
		}
		public JsonValue ToJson(JsonSerializer serializer)
		{
			var json = new JsonObject();
			Id.Serialize(json, serializer, "id");
			Unread.Serialize(json, serializer, "unread");
			return json;
		}
	}
}

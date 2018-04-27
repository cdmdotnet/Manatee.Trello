using System;
using Manatee.Json;
using Manatee.Json.Serialization;

namespace Manatee.Trello.Json.Entities
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
			Date = obj.Deserialize<DateTime?>(serializer, "date");
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

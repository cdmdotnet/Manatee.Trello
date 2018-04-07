using Manatee.Json;
using Manatee.Json.Serialization;

namespace Manatee.Trello.Json.Entities
{
	internal class ManateeWebhookNotification : IJsonWebhookNotification, IJsonSerializable
	{
		public IJsonAction Action { get; set; }

		public void FromJson(JsonValue json, JsonSerializer serializer)
		{
			if (json.Type != JsonValueType.Object) return;
			var obj = json.Object;
			Action = obj.Deserialize<IJsonAction>(serializer, "action");
		}
		public JsonValue ToJson(JsonSerializer serializer)
		{
			return null;
		}
	}
}
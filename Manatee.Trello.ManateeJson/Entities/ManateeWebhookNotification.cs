using Manatee.Json;
using Manatee.Json.Serialization;
using Manatee.Trello.Json;

namespace Manatee.Trello.ManateeJson.Entities
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
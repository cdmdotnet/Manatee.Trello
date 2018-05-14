using Manatee.Json;
using Manatee.Json.Serialization;

namespace Manatee.Trello.Json.Entities
{
	internal class ManateeCustomFieldDisplayInfo : IJsonCustomFieldDisplayInfo, IJsonSerializable
	{
		public bool? CardFront { get; set; }

		public void FromJson(JsonValue json, JsonSerializer serializer)
		{
			if (json.Type != JsonValueType.Object) return;
			var obj = json.Object;
			CardFront = obj.TryGetBoolean("cardFront");
		}

		public JsonValue ToJson(JsonSerializer serializer)
		{
			var obj = new JsonObject();

			CardFront.Serialize(obj, serializer, "cardFront");

			return obj;
		}
	}
}
using Manatee.Json;
using Manatee.Json.Serialization;

namespace Manatee.Trello.Json.Entities
{
	internal class ManateeParameter : IJsonParameter, IJsonSerializable
	{
		public string String { get; set; }
		public bool? Boolean { get; set; }
		public object Object { get; set; }

		public void FromJson(JsonValue json, JsonSerializer serializer)
		{
			if (json.Type != JsonValueType.Object) return;
			String = json.Object.TryGetString("value");
		}
		public JsonValue ToJson(JsonSerializer serializer)
		{
			var json = new JsonObject();

			if (Boolean.HasValue)
				json.Add("value", Boolean);
			else if (Object != null)
				json.Add("value", serializer.Serialize(Object));
			else
				json.Add("value", String);

			return json;
		}
	}
}
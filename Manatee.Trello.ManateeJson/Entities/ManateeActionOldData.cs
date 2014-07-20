using Manatee.Json;
using Manatee.Json.Serialization;
using Manatee.Trello.Json;

namespace Manatee.Trello.ManateeJson.Entities
{
	internal class ManateeActionOldData : IJsonActionOldData, IJsonSerializable
	{
		public string Desc { get; set; }
		public IJsonList List { get; set; }
		public double? Pos { get; set; }
		public string Text { get; set; }
		public bool? Closed { get; set; }

		public void FromJson(JsonValue json, JsonSerializer serializer)
		{
			if (json.Type != JsonValueType.Object) return;
			var obj = json.Object;
			Desc = obj.TryGetString("desc");
			List = obj.Deserialize<IJsonList>(serializer, "list");
			Pos = obj.TryGetNumber("pos");
			Text = obj.TryGetString("text");
			Closed = obj.TryGetBoolean("closed");
		}
		public JsonValue ToJson(JsonSerializer serializer)
		{
			var json = new JsonObject
				{
					{"desc", Desc},
					{"pos", Pos.HasValue ? Pos : JsonValue.Null},
					{"text", Text},
					{"closed", Closed}
				};
			List.Serialize(json, serializer, "list");
			return json;
		}
	}
}
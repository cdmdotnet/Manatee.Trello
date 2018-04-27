using Manatee.Json;
using Manatee.Json.Serialization;

namespace Manatee.Trello.Json.Entities
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
			Text = obj.TryGetString("text");
			Pos = obj.TryGetNumber("pos");
			Closed = obj.TryGetBoolean("closed");
		}
		public JsonValue ToJson(JsonSerializer serializer)
		{
			return null;
		}
	}
}
using Manatee.Json;
using Manatee.Json.Serialization;
using Manatee.Trello.Json;

namespace Manatee.Trello.ManateeJson.Entities
{
	internal class ManateeImagePreview : IJsonImagePreview, IJsonSerializable
	{
		public int? Width { get; set; }
		public int? Height { get; set; }
		public string Url { get; set; }
		public string Id { get; set; }
		public bool? Scaled { get; set; }

		public void FromJson(JsonValue json, JsonSerializer serializer)
		{
			if (json.Type != JsonValueType.Object) return;
			var obj = json.Object;
			Width = (int?) obj.TryGetNumber("width");
			Height = (int?)obj.TryGetNumber("height");
			Url = obj.TryGetString("url");
			Id = obj.TryGetString("id");
			Scaled = obj.TryGetBoolean("scaled");
		}
		public JsonValue ToJson(JsonSerializer serializer)
		{
			return null;
		}
	}
}
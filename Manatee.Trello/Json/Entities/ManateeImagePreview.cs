using Manatee.Json;
using Manatee.Json.Serialization;

namespace Manatee.Trello.Json.Entities
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
			Id = obj.TryGetString("id") ?? obj.TryGetString("_id");
			Width = (int?) obj.TryGetNumber("width");
			Height = (int?)obj.TryGetNumber("height");
			Url = obj.TryGetString("url");
			Scaled = obj.TryGetBoolean("scaled");
		}
		public JsonValue ToJson(JsonSerializer serializer)
		{
			return null;
		}
	}
}
using System;
using Manatee.Json;
using Manatee.Json.Enumerations;
using Manatee.Json.Serialization;
using Manatee.Trello.Implementation;

namespace Manatee.Trello
{
	public class AttachmentPreview : IJsonCompatible
	{
		public string Id { get; set; }
		public int Height { get; set; }
		public string Url { get; set; }
		public int Width { get; set; }

		public void FromJson(JsonValue json)
		{
			if (json == null) return;
			if (json.Type != JsonValueType.Object) return;
			var obj = json.Object;
			Id = obj.TryGetString("id");
			Height = (int) obj.TryGetNumber("height");
			Url = obj.TryGetString("url");
			Width = (int) obj.TryGetNumber("width");
		}
		public JsonValue ToJson()
		{
			var json = new JsonObject
			           	{
			           		{"id", Id},
			           		{"height", Height},
			           		{"url", Url},
			           		{"width", Width}
			           	};
			return json;
		}
	}
}
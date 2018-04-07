using System.Collections.Generic;
using Manatee.Json;
using Manatee.Json.Serialization;

namespace Manatee.Trello.Json.Entities
{
	internal class ManateeSticker : IJsonSticker, IJsonSerializable
	{
		public string Id { get; set; }
		public double? Left { get; set; }
		public string Name { get; set; }
		public List<IJsonImagePreview> Previews { get; set; }
		public int? Rotation { get; set; }
		public double? Top { get; set; }
		public string Url { get; set; }
		public int? ZIndex { get; set; }

		public void FromJson(JsonValue json, JsonSerializer serializer)
		{
			switch (json.Type)
			{
				case JsonValueType.Object:
					var obj = json.Object;
					Id = obj.TryGetString("id");
					Left = obj.TryGetNumber("left");
					Name = obj.TryGetString("image");
					Previews = obj.Deserialize<List<IJsonImagePreview>>(serializer, "imageScaled");
					Rotation = (int?) obj.TryGetNumber("rotate");
					Top = obj.TryGetNumber("top");
					Url = obj.TryGetString("imageUrl");
					ZIndex = (int?) obj.TryGetNumber("zIndex");
					break;
				case JsonValueType.String:
					Id = json.String;
					break;
			}
		}
		public JsonValue ToJson(JsonSerializer serializer)
		{
			var json = new JsonObject();

			Id.Serialize(json, serializer, "id");
			Left.Serialize(json, serializer, "left");
			Name.Serialize(json, serializer, "image");
			Rotation.Serialize(json, serializer, "rotate");
			Top.Serialize(json, serializer, "top");
			ZIndex.Serialize(json, serializer, "zIndex");

			return json;
		}
	}
}
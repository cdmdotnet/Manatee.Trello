using System.Collections.Generic;
using Manatee.Json;
using Manatee.Json.Serialization;

namespace Manatee.Trello.Json.Entities
{
	internal class ManateeBoardBackground : IJsonBoardBackground, IJsonSerializable
	{
		public string Id { get; set; }
		public string BottomColor { get; set; }
		public BoardBackgroundBrightness? Brightness { get; set; }
		public string Color { get; set; }
		public string Image { get; set; }
		public List<IJsonImagePreview> ImageScaled { get; set; }
		public bool? Tile { get; set; }
		public string TopColor { get; set; }
		public bool ValidForMerge { get; set; }

		public void FromJson(JsonValue json, JsonSerializer serializer)
		{
			switch (json.Type)
			{
				case JsonValueType.Object:
					var obj = json.Object;
					Id = obj.TryGetString("background");
					BottomColor = obj.TryGetString("backgroundBottomColor");
					Brightness = obj.Deserialize<BoardBackgroundBrightness?>(serializer, "backgroundBrightness");
					Color = obj.TryGetString("backgroundColor");
					Image = obj.TryGetString("backgroundImage");
					ImageScaled = obj.Deserialize<List<IJsonImagePreview>>(serializer, "backgroundImageScaled");
					Tile = obj.TryGetBoolean("backgroundTile");
					TopColor = obj.TryGetString("backgroundTopColor");
					ValidForMerge = true;
					break;
				case JsonValueType.String:
					Id = json.String;
					break;
			}
		}
		public JsonValue ToJson(JsonSerializer serializer)
		{
			return Id;
		}
	}
}

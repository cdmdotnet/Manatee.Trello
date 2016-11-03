﻿using System.Collections.Generic;
using Manatee.Json;
using Manatee.Json.Serialization;
using Manatee.Trello.Json;

namespace Manatee.Trello.ManateeJson.Entities
{
	internal class ManateeBoardBackground : IJsonBoardBackground, IJsonSerializable
	{
		public string Id { get; set; }
		public string Color { get; set; }
		public string Image { get; set; }
		public List<IJsonImagePreview> ImageScaled { get; set; }
		public bool? Tile { get; set; }

		public void FromJson(JsonValue json, JsonSerializer serializer)
		{
			switch (json.Type)
			{
				case JsonValueType.Object:
					var obj = json.Object;
					Id = obj.TryGetString("background");
					Color = obj.TryGetString("backgroundColor");
					Image = obj.TryGetString("backgroundImage");
					ImageScaled = obj.Deserialize<List<IJsonImagePreview>>(serializer, "backgroundImageScaled");
					Tile = obj.TryGetBoolean("backgroundTile");
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

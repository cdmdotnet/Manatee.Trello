using System;
using Manatee.Json;
using Manatee.Json.Serialization;

namespace Manatee.Trello.Json.Entities
{
	internal class ManateeCustomDropDownOption : IJsonCustomDropDownOption, IJsonSerializable
	{
		public string Id { get; set; }
		public IJsonCustomFieldDefinition Field { get; set; }
		public string Text { get; set; }
		public LabelColor? Color { get; set; }
		public IJsonPosition Pos { get; set; }
		public bool ValidForMerge { get; set; }

		public void FromJson(JsonValue json, JsonSerializer serializer)
		{
			switch (json.Type)
			{
				case JsonValueType.Object:
					var obj = json.Object;
					Id = obj.TryGetString("id");
					Field = obj.Deserialize<IJsonCustomFieldDefinition>(serializer, "idCustomField");
					Text = obj.TryGetObject("value")?.TryGetString("text");
					Color = obj.Deserialize<LabelColor?>(serializer, "color");
					Pos = obj.Deserialize<IJsonPosition>(serializer, "pos");
					ValidForMerge = true;
					break;
				case JsonValueType.String:
					Id = json.String;
					break;
			}
		}

		public JsonValue ToJson(JsonSerializer serializer)
		{
			throw new NotImplementedException();
		}
	}
}
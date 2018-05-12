using Manatee.Json;
using Manatee.Json.Serialization;

namespace Manatee.Trello.Json.Entities
{

//	{
//	idModel: "5a00adcebe1991022b4a4bb4",
//	modelType: "board",
//	name: "My Dropdown",
//	options: [,
//	{

//		color: "none",
//		value: {

//			text: "First Option"

//		}, 
//		pos: 1024
//	},
//	{
//	color: "none",
//	value: {
//		text: "Second Option"
//	}, 
//	pos: 2048
//}
//],
//pos: "bottom",
//type: "list",
//display_cardFront: false
//}

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
			var obj = new JsonObject
				{
					["value"] = new JsonObject{["text"] = Text},
				};
			Color.Serialize(obj, serializer, "color");
			Pos.Serialize(obj, serializer, "pos");

			return obj;
		}
	}
}
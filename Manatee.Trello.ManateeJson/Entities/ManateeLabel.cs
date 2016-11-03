using Manatee.Json;
using Manatee.Json.Serialization;
using Manatee.Trello.Json;

namespace Manatee.Trello.ManateeJson.Entities
{
	internal class ManateeLabel : IJsonLabel, IJsonSerializable
	{
		public IJsonBoard Board { get; set; }
		public LabelColor? Color { get; set; }
		public bool ForceNullColor { get; set; }
		public string Id { get; set; }
		public string Name { get; set; }
		public int? Uses { get; set; }

		public void FromJson(JsonValue json, JsonSerializer serializer)
		{
			switch (json.Type)
			{
				case JsonValueType.Object:
					var obj = json.Object;
					Board = obj.Deserialize<IJsonBoard>(serializer, "idBoard");
					Color = obj.Deserialize<LabelColor?>(serializer, "color");
					Id = obj.TryGetString("id");
					Name = obj.TryGetString("name");
					Uses = (int?) obj.TryGetNumber("uses");
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
			Board.SerializeId(json, "idBoard");
			if (Color.HasValue)
				Color.Serialize(json, serializer, "color");
			else if (ForceNullColor)
				json["color"] = JsonValue.Null;
			Name.Serialize(json, serializer, "name");
			return json;
		}
	}
}

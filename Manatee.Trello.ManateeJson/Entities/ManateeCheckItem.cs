using Manatee.Json;
using Manatee.Json.Serialization;
using Manatee.Trello.Json;

namespace Manatee.Trello.ManateeJson.Entities
{
	internal class ManateeCheckItem : IJsonCheckItem, IJsonSerializable
	{
		public string Id { get; set; }
		public CheckItemState? State { get; set; }
		public string Name { get; set; }
		public IJsonPosition Pos { get; set; }

		public void FromJson(JsonValue json, JsonSerializer serializer)
		{
			if (json.Type != JsonValueType.Object) return;
			var obj = json.Object;
			Id = obj.TryGetString("id");
			State = obj.Deserialize<CheckItemState?>(serializer, "state");
			Name = obj.TryGetString("name");
			Pos = obj.Deserialize<IJsonPosition>(serializer, "pos");
		}
		public JsonValue ToJson(JsonSerializer serializer)
		{
			var json = new JsonObject();
			Id.Serialize(json, serializer, "id");
			Name.Serialize(json, serializer, "name");
			Pos.Serialize(json, serializer, "pos");
			State.Serialize(json, serializer, "state");
			return json;
		}
	}
}

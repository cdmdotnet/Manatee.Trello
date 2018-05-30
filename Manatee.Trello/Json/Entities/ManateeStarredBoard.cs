using Manatee.Json;
using Manatee.Json.Serialization;

namespace Manatee.Trello.Json.Entities
{
	internal class ManateeStarredBoard : IJsonStarredBoard, IJsonSerializable
	{
		public string Id { get; set; }
		public IJsonBoard Board { get; set; }
		public IJsonPosition Pos { get; set; }

		public void FromJson(JsonValue json, JsonSerializer serializer)
		{
			if (json.Type != JsonValueType.Object) return;
			var obj = json.Object;
			Id = obj.TryGetString("id");
			Board = obj.Deserialize<IJsonBoard>(serializer, "idBoard");
			Pos = obj.Deserialize<IJsonPosition>(serializer, "pos");
		}

		public JsonValue ToJson(JsonSerializer serializer)
		{
			var obj = new JsonObject();
			
			Pos.Serialize(obj, serializer, "pos");

			return obj;
		}
	}
}
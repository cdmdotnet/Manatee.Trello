using System.Collections.Generic;
using Manatee.Json;
using Manatee.Json.Serialization;

namespace Manatee.Trello.Json.Entities
{
	internal class ManateeList : IJsonList, IJsonSerializable
	{
		public string Id { get; set; }
		public string Name { get; set; }
		public bool? Closed { get; set; }
		public IJsonBoard Board { get; set; }
		public IJsonPosition Pos { get; set; }
		public bool? Subscribed { get; set; }
		public List<IJsonAction> Actions { get; set; }
		public List<IJsonCard> Cards { get; set; }
		public bool ValidForMerge { get; set; }

		public void FromJson(JsonValue json, JsonSerializer serializer)
		{
			switch (json.Type)
			{
				case JsonValueType.Object:
					var obj = json.Object;
					Id = obj.TryGetString("id");
					Name = obj.TryGetString("name");
					Closed = obj.TryGetBoolean("closed");
					Board = obj.Deserialize<IJsonBoard>(serializer, "board") ?? obj.Deserialize<IJsonBoard>(serializer, "idBoard");
					Pos = obj.Deserialize<IJsonPosition>(serializer, "pos");
					Subscribed = obj.TryGetBoolean("subscribed");
					Actions = obj.Deserialize<List<IJsonAction>>(serializer, "actions");
					Cards = obj.Deserialize<List<IJsonCard>>(serializer, "cards");
					ValidForMerge = true;
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
			Closed.Serialize(json, serializer, "closed");
			Name.Serialize(json, serializer, "name");
			Pos.Serialize(json, serializer, "pos");
			Subscribed.Serialize(json, serializer, "subscribed");
			return json;
		}
	}
}

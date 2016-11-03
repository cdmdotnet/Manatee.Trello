﻿using System.Collections.Generic;
using Manatee.Json;
using Manatee.Json.Serialization;
using Manatee.Trello.Json;

namespace Manatee.Trello.ManateeJson.Entities
{
	internal class ManateeCheckList : IJsonCheckList, IJsonSerializable
	{
		public string Id { get; set; }
		public string Name { get; set; }
		public IJsonBoard Board { get; set; }
		public IJsonCard Card { get; set; }
		public List<IJsonCheckItem> CheckItems { get; set; }
		public IJsonPosition Pos { get; set; }
		public IJsonCheckList CheckListSource { get; set; }

		public void FromJson(JsonValue json, JsonSerializer serializer)
		{
			if (json.Type != JsonValueType.Object) return;
			var obj = json.Object;
			Id = obj.TryGetString("id");
			Name = obj.TryGetString("name");
			Board = obj.Deserialize<IJsonBoard>(serializer, "idBoard");
			Card = obj.Deserialize<IJsonCard>(serializer, "idCard");
			CheckItems = obj.Deserialize<List<IJsonCheckItem>>(serializer, "checkItems");
			Pos = obj.Deserialize<IJsonPosition>(serializer, "pos");
		}
		public JsonValue ToJson(JsonSerializer serializer)
		{
			var json = new JsonObject();
			Id.Serialize(json, serializer, "id");
			Card.SerializeId(json, "idCard");
			Name.Serialize(json, serializer, "name");
			Pos.Serialize(json, serializer, "pos");
			CheckListSource.SerializeId(json, "idChecklistSource");
			return json;
		}
	}
}

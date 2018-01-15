using System;
using Manatee.Json;
using Manatee.Json.Serialization;
using Manatee.Trello.Json;

namespace Manatee.Trello.ManateeJson.Entities
{
	internal class ManateePowerUp : IJsonPowerUp, IJsonSerializable
	{
		public string Id { get; set; }
		public string Name { get; set; }
		public bool? Public { get; set; }
		public string Url { get; set; }

		public void FromJson(JsonValue json, JsonSerializer serializer)
		{
			if (json.Type != JsonValueType.Object) return;
			var obj = json.Object;
			Id = obj.TryGetString("id");
			Name = obj.TryGetString("name");
			Public = obj.TryGetBoolean("public");
			Url = obj.TryGetString("url");
		}
		public JsonValue ToJson(JsonSerializer serializer)
		{
			throw new NotImplementedException();
		}
	}
}

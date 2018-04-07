using System;
using Manatee.Json;
using Manatee.Json.Serialization;

namespace Manatee.Trello.Json.Entities
{
	internal class ManateePowerUpData : IJsonPowerUpData, IJsonSerializable
	{
		public string Id { get; set; }
		public string PluginId { get; set; }
		public string Value { get; set; }

		public void FromJson(JsonValue json, JsonSerializer serializer)
		{
			if (json.Type != JsonValueType.Object) return;
			var obj = json.Object;
			Id = obj.TryGetString("id");
			PluginId = obj.TryGetString("idPlugin");
			Value = obj.TryGetString("value");
		}
		public JsonValue ToJson(JsonSerializer serializer)
		{
			throw new NotImplementedException();
		}
	}
}

using System.Collections.Generic;
using Manatee.Json;
using Manatee.Json.Serialization;

namespace Manatee.Trello.Json.Entities
{
	internal class ManateeBatch : IJsonBatch, IJsonSerializable
	{
		public List<IJsonBatchItem> Items { get; set; }

		public void FromJson(JsonValue json, JsonSerializer serializer)
		{
			if (json.Type != JsonValueType.Array) return;
			var arr = json.Array;
			Items = serializer.Deserialize<List<IJsonBatchItem>>(arr);
		}

		public JsonValue ToJson(JsonSerializer serializer)
		{
			throw new System.NotImplementedException();
		}
	}
}

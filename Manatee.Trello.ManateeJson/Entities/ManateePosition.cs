using Manatee.Json;
using Manatee.Json.Serialization;
using Manatee.Trello.Json;

namespace Manatee.Trello.ManateeJson.Entities
{
	internal class ManateePosition : IJsonPosition, IJsonSerializable
	{
		public double? Explicit { get; set; }
		public string Named { get; set; }

		public void FromJson(JsonValue json, JsonSerializer serializer)
		{
			switch (json.Type)
			{
				case JsonValueType.Number:
					Explicit = json.Number;
					break;
				case JsonValueType.String:
					Named = json.String;
					break;
			}
		}
		public JsonValue ToJson(JsonSerializer serializer)
		{
			return Named.IsNullOrWhiteSpace()
				       ? (Explicit ?? JsonValue.Null)
				       : Named;
		}
	}
}
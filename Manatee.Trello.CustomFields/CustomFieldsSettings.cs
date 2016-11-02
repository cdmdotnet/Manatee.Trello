using System.Collections.Generic;
using Manatee.Json;
using Manatee.Json.Serialization;

namespace Manatee.Trello.CustomFields
{
	public class CustomFieldsSettings : IJsonSerializable
	{
		public string ButtonText { get; private set; }
		public IEnumerable<CustomFieldDefinition> Fields { get; private set; }

		public void FromJson(JsonValue json, JsonSerializer serializer)
		{
			if (json.Type != JsonValueType.Object) return;
			var obj = json.Object;
			ButtonText = obj.TryGetString("btn");
			Fields = serializer.Deserialize<List<CustomFieldDefinition>>(obj.TryGetArray("fields"));
		}
		public JsonValue ToJson(JsonSerializer serializer)
		{
			throw new System.NotImplementedException();
		}
	}
}
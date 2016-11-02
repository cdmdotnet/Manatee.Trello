using Manatee.Json;
using Manatee.Json.Serialization;

namespace Manatee.Trello.CustomFields
{
	public class CustomFieldDefinition : IJsonSerializable
	{
		internal string Id { get; private set; }
		public string Name { get; private set; }
		public FieldType? Type { get; private set; }
		public bool? ShowBadge { get; private set; }

		public void FromJson(JsonValue json, JsonSerializer serializer)
		{
			if (json.Type != JsonValueType.Object) return;
			var obj = json.Object;
			Id = obj.TryGetString("id");
			Name = obj.TryGetString("n");
			Type = (FieldType?) obj.TryGetNumber("t");
			ShowBadge = (int?) obj.TryGetNumber("b") == 1;
		}
		public JsonValue ToJson(JsonSerializer serializer)
		{
			throw new System.NotImplementedException();
		}
	}
}
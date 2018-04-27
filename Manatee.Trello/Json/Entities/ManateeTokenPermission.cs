using Manatee.Json;
using Manatee.Json.Serialization;

namespace Manatee.Trello.Json.Entities
{
	internal class ManateeTokenPermission : IJsonTokenPermission, IJsonSerializable
	{
		public string IdModel { get; set; }
		public TokenModelType? ModelType { get; set; }
		public bool? Read { get; set; }
		public bool? Write { get; set; }

		public void FromJson(JsonValue json, JsonSerializer serializer)
		{
			if (json.Type != JsonValueType.Object) return;
			var obj = json.Object;
			IdModel = obj.TryGetString("idModel");
			ModelType = obj.Deserialize<TokenModelType?>(serializer, "modelType");
			Read = obj.TryGetBoolean("read");
			Write = obj.TryGetBoolean("write");
		}
		public JsonValue ToJson(JsonSerializer serializer)
		{
			return null;
		}
	}
}
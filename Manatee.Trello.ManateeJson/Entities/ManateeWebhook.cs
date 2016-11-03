using Manatee.Json;
using Manatee.Json.Serialization;
using Manatee.Trello.Json;

namespace Manatee.Trello.ManateeJson.Entities
{
	internal class ManateeWebhook : IJsonWebhook, IJsonSerializable
	{
		public string Id { get; set; }
		public string Description { get; set; }
		public string IdModel { get; set; }
		public string CallbackUrl { get; set; }
		public bool? Active { get; set; }

		public void FromJson(JsonValue json, JsonSerializer serializer)
		{
			if (json.Type != JsonValueType.Object) return;
			var obj = json.Object;
			Id = obj.TryGetString("id");
			Description = obj.TryGetString("description");
			IdModel = obj.TryGetString("idModel");
			CallbackUrl = obj.TryGetString("callbackURL");
			Active = obj.TryGetBoolean("active");
		}
		public JsonValue ToJson(JsonSerializer serializer)
		{
			var json = new JsonObject();
			Id.Serialize(json, serializer, "id");
			Description.Serialize(json, serializer, "description");
			IdModel.Serialize(json, serializer, "idModel");
			CallbackUrl.Serialize(json, serializer, "callbackURL");
			Active.Serialize(json, serializer, "active");
			return json;
		}
	}
}
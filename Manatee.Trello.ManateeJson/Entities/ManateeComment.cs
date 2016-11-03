using Manatee.Json;
using Manatee.Json.Serialization;
using Manatee.Trello.Json;

namespace Manatee.Trello.ManateeJson.Entities
{
	internal class ManateeComment : ManateeAction, IJsonComment
	{
		public string Text { get; set; }

		public override JsonValue ToJson(JsonSerializer serializer)
		{
			var json = new JsonObject();

			Text.Serialize(json, serializer, "text");

			return json;
		}
	}
}
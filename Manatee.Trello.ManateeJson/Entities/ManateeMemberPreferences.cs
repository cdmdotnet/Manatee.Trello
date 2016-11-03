using Manatee.Json;
using Manatee.Json.Serialization;
using Manatee.Trello.Json;

namespace Manatee.Trello.ManateeJson.Entities
{
	internal class ManateeMemberPreferences : IJsonMemberPreferences, IJsonSerializable
	{
		public int? MinutesBetweenSummaries { get; set; }
		public bool? ColorBlind { get; set; }

		public void FromJson(JsonValue json, JsonSerializer serializer)
		{
			if (json.Type != JsonValueType.Object) return;
			var obj = json.Object;
			MinutesBetweenSummaries = (int?) obj.TryGetNumber("minutesBetweenSummaries");
			ColorBlind = obj.TryGetBoolean("colorBlind");
		}
		public JsonValue ToJson(JsonSerializer serializer)
		{
			return null;
		}
	}
}

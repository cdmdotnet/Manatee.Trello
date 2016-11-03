using Manatee.Json;
using Manatee.Json.Serialization;
using Manatee.Trello.Json;

namespace Manatee.Trello.ManateeJson.Entities
{
	internal class ManateeBoardMembership : IJsonBoardMembership, IJsonSerializable
	{
		public string Id { get; set; }
		public IJsonMember Member { get; set; }
		public BoardMembershipType? MemberType { get; set; }
		public bool? Deactivated { get; set; }

		public void FromJson(JsonValue json, JsonSerializer serializer)
		{
			if (json.Type != JsonValueType.Object) return;
			var obj = json.Object;
			Id = obj.TryGetString("id");
			Member = obj.Deserialize<IJsonMember>(serializer, "idMember");
			MemberType = obj.Deserialize<BoardMembershipType?>(serializer, "memberType");
			Deactivated = obj.TryGetBoolean("deactivated");
		}
		public JsonValue ToJson(JsonSerializer serializer)
		{
			var json = new JsonObject();
			Id.Serialize(json, serializer, "id");
			MemberType.Serialize(json, serializer, "type");
			Member.SerializeId(json, "idMember");
			return json;
		}
	}
}

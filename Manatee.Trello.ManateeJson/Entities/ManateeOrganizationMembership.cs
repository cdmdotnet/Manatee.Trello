using Manatee.Json;
using Manatee.Json.Serialization;
using Manatee.Trello.Json;

namespace Manatee.Trello.ManateeJson.Entities
{
	internal class ManateeOrganizationMembership : IJsonOrganizationMembership, IJsonSerializable
	{
		public string Id { get; set; }
		public IJsonMember Member { get; set; }
		public OrganizationMembershipType? MemberType { get; set; }
		public bool? Unconfirmed { get; set; }

		public void FromJson(JsonValue json, JsonSerializer serializer)
		{
			if (json.Type != JsonValueType.Object) return;
			var obj = json.Object;
			Id = obj.TryGetString("id");
			Member = obj.Deserialize<IJsonMember>(serializer, "idMember");
			MemberType = obj.Deserialize<OrganizationMembershipType?>(serializer, "memberType");
			Unconfirmed = obj.TryGetBoolean("unconfirmed");
		}
		public JsonValue ToJson(JsonSerializer serializer)
		{
			var json = new JsonObject();
			Id.Serialize(json, serializer, "id");
			Member.SerializeId(json, "idMember");
			MemberType.Serialize(json, serializer, "type");
			return json;
		}
	}
}
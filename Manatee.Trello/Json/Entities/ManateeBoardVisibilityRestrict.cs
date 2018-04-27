using Manatee.Json;
using Manatee.Json.Serialization;

namespace Manatee.Trello.Json.Entities
{
	internal class ManateeBoardVisibilityRestrict : IJsonBoardVisibilityRestrict, IJsonSerializable
	{
		public OrganizationBoardVisibility? Public { get; set; }
		public OrganizationBoardVisibility? Org { get; set; }
		public OrganizationBoardVisibility? Private { get; set; }

		public void FromJson(JsonValue json, JsonSerializer serializer)
		{
			if (json.Type != JsonValueType.Object) return;
			var obj = json.Object;
			Public = obj.Deserialize<OrganizationBoardVisibility?>(serializer, "public");
			Org = obj.Deserialize<OrganizationBoardVisibility?>(serializer, "org");
			Private = obj.Deserialize<OrganizationBoardVisibility?>(serializer, "private");
		}
		public JsonValue ToJson(JsonSerializer serializer)
		{
			return null;
		}
	}
}

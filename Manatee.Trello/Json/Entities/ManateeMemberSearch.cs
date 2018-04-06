using System.Collections.Generic;
using System.Linq;
using Manatee.Json;
using Manatee.Json.Serialization;

namespace Manatee.Trello.Json.Entities
{
	internal class ManateeMemberSearch : IJsonMemberSearch, IJsonSerializable
	{
		public List<IJsonMember> Members { get; set; }
		public IJsonBoard Board { get; set; }
		public int? Limit { get; set; }
		public bool? OnlyOrgMembers { get; set; }
		public IJsonOrganization Organization { get; set; }
		public string Query { get; set; }

		public void FromJson(JsonValue json, JsonSerializer serializer)
		{
			// This is the only return value that is an array at the root.
			if (json.Type != JsonValueType.Array) return;
			var array = json.Array;
			Members = array.Select(serializer.Deserialize<IJsonMember>).ToList();
		}
		public JsonValue ToJson(JsonSerializer serializer)
		{
			var json = new JsonObject
				{
					{"query", Query}
				};
			Limit.Serialize(json, serializer, "limit");
			Board.SerializeId(json, "idBoard");
			if (Organization != null)
			{
				Organization.SerializeId(json, "idOrganization");
				OnlyOrgMembers.Serialize(json, serializer, "onlyOrgMembers");
			}
			return json;
		}
	}
}
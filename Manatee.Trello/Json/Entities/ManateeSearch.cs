using System.Collections.Generic;
using System.Linq;
using Manatee.Json;
using Manatee.Json.Serialization;

namespace Manatee.Trello.Json.Entities
{
	internal class ManateeSearch : IJsonSearch, IJsonSerializable
	{
		public List<IJsonAction> Actions { get; set; }
		public List<IJsonBoard> Boards { get; set; }
		public List<IJsonCard> Cards { get; set; }
		public List<IJsonMember> Members { get; set; }
		public List<IJsonOrganization> Organizations { get; set; }
		public string Query { get; set; }
		public List<IJsonCacheable> Context { get; set; }
		public SearchModelType? Types { get; set; }
		public int? Limit { get; set; }
		public bool Partial { get; set; }

		public void FromJson(JsonValue json, JsonSerializer serializer)
		{
			if (json.Type != JsonValueType.Object) return;
			var obj = json.Object;
			Actions = obj.Deserialize<List<IJsonAction>>(serializer, "actions");
			Boards = obj.Deserialize<List<IJsonBoard>>(serializer, "boards");
			Cards = obj.Deserialize<List<IJsonCard>>(serializer, "cards");
			Members = obj.Deserialize<List<IJsonMember>>(serializer, "members");
			Organizations = obj.Deserialize<List<IJsonOrganization>>(serializer, "organizations");
		}
		public JsonValue ToJson(JsonSerializer serializer)
		{
			var json = new JsonObject
				{
					{"query", Query},
				};
			Types.Serialize(json, serializer, "types");
			Limit.Serialize(json, serializer, "boards_limit");
			Limit.Serialize(json, serializer, "cards_limit");
			Limit.Serialize(json, serializer, "organizations_limit");
			Limit.Serialize(json, serializer, "members_limit");
			Partial.Serialize(json, serializer, "partial");
			if (Context != null)
			{
				TryAddContext<IJsonCard>(json, "idCards");
				TryAddContext<IJsonBoard>(json, "idBoards");
				TryAddContext<IJsonOrganization>(json, "idOrganizations");
			}
			return json;
		}

		private void TryAddContext<T>(JsonObject json, string key)
			where T : IJsonCacheable
		{
			var ids = string.Join(",", Context.OfType<T>().Select(o => o.Id));
			if (!string.IsNullOrWhiteSpace(ids))
				json[key] = ids;
		}
	}
}
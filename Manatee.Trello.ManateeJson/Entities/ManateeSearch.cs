/***************************************************************************************

	Copyright 2013 Little Crab Solutions

	   Licensed under the Apache License, Version 2.0 (the "License");
	   you may not use this file except in compliance with the License.
	   You may obtain a copy of the License at

		 http://www.apache.org/licenses/LICENSE-2.0

	   Unless required by applicable law or agreed to in writing, software
	   distributed under the License is distributed on an "AS IS" BASIS,
	   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
	   See the License for the specific language governing permissions and
	   limitations under the License.
 
	File Name:		ManateeSearchResults.cs
	Namespace:		Manatee.Trello.ManateeJson.Entities
	Class Name:		ManateeSearchResults
	Purpose:		Implements IJsonLabelNames for Manatee.Json.

***************************************************************************************/
using System.Collections.Generic;
using System.Linq;
using Manatee.Json;
using Manatee.Json.Serialization;
using Manatee.Trello.Json;

namespace Manatee.Trello.ManateeJson.Entities
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
			var ids = Context.OfType<T>().Select(o => o.Id).Join(",");
			if (!ids.IsNullOrWhiteSpace())
				json[key] = ids;
		}
	}
}
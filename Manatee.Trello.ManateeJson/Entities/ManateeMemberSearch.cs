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
 
	File Name:		ManateeMemberSearch.cs
	Namespace:		Manatee.Trello.ManateeJson.Entities
	Class Name:		ManateeMemberSearch
	Purpose:		Implements IJsonMemberSearch for Manatee.Json.

***************************************************************************************/
using System.Collections.Generic;
using System.Linq;
using Manatee.Json;
using Manatee.Json.Serialization;
using Manatee.Trello.Json;

namespace Manatee.Trello.ManateeJson.Entities
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
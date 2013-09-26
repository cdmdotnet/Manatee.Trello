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
using System;
using System.Collections.Generic;
using System.Linq;
using Manatee.Json;
using Manatee.Json.Enumerations;
using Manatee.Json.Extensions;
using Manatee.Json.Serialization;
using Manatee.Trello.Json;

namespace Manatee.Trello.ManateeJson.Entities
{
	internal class ManateeSearchResults : IJsonSearchResults, IJsonCompatible
	{
		public List<string> ActionIds { get; set; }
		public List<string> BoardIds { get; set; }
		public List<string> CardIds { get; set; }
		public List<string> MemberIds { get; set; }
		public List<string> OrganizationIds { get; set; }

		public void FromJson(JsonValue json)
		{
			if (json.Type != JsonValueType.Object) return;
			var obj = json.Object;
			ActionIds = GetIds(obj, "actions");
			BoardIds = GetIds(obj, "boards");
			CardIds = GetIds(obj, "cards");
			MemberIds = GetIds(obj, "members");
			OrganizationIds = GetIds(obj, "organizations");
		}
		public JsonValue ToJson()
		{
			throw new NotImplementedException();
		}
		private static List<string> GetIds(JsonObject obj, string key)
		{
			var array = obj.TryGetArray(key);
			return array == null ? new List<string>() : array.Select(a => a.Object.TryGetString("id")).ToList();
		}
	}
}
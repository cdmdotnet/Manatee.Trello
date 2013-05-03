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
	Namespace:		Manatee.Trello.Json.Manatee.Entities
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

namespace Manatee.Trello.Json.Manatee.Entities
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
			ActionIds = obj.TryGetArray("actions").Select(a => a.Object.TryGetString("id")).ToList();
			BoardIds = obj.TryGetArray("boards").Select(a => a.Object.TryGetString("id")).ToList();
			CardIds = obj.TryGetArray("cards").Select(a => a.Object.TryGetString("id")).ToList();
			MemberIds = obj.TryGetArray("members").Select(a => a.Object.TryGetString("id")).ToList();
			OrganizationIds = obj.TryGetArray("organizations").Select(a => a.Object.TryGetString("id")).ToList();
		}
		public JsonValue ToJson()
		{
			throw new NotImplementedException();
		}
	}
}
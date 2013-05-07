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
 
	File Name:		ManateeToken.cs
	Namespace:		Manatee.Trello.Json.Manatee.Entities
	Class Name:		ManateeToken
	Purpose:		Implements IJsonToken for Manatee.Json.

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
	internal class ManateeToken : IJsonToken, IJsonCompatible
	{
		public string Id { get; set; }
		public string Identifier { get; set; }
		public string IdMember { get; set; }
		public DateTime? DateCreated { get; set; }
		public DateTime? DateExpires { get; set; }
		public List<IJsonTokenPermission> Permissions { get; set; }

		public void FromJson(JsonValue json)
		{
			if (json.Type != JsonValueType.Object) return;
			var obj = json.Object;
			Id = obj.TryGetString("id");
			Identifier = obj.TryGetString("identifier");
			IdMember = obj.TryGetString("idMember");
			var dateString = obj.TryGetString("dateCreated");
			DateTime date;
			if (DateTime.TryParse(dateString, out date))
				DateCreated = date;
			dateString = obj.TryGetString("dateExpires");
			if (DateTime.TryParse(dateString, out date))
				DateExpires = date;
			var perms = obj.TryGetArray("permissions");
			if (perms != null)
				Permissions = perms.FromJson<ManateeTokenPermission>()
								   .Cast<IJsonTokenPermission>()
								   .ToList();
		}
		public JsonValue ToJson()
		{
			return new JsonObject
			       	{
			       		{"id", Id},
			       		{"identifier", Identifier},
			       		{"idMember", IdMember},
			       		{"dateCreated", DateCreated.HasValue ? DateCreated.Value.ToString("yyyy-MM-ddTHH:mm:ss.fffZ") : JsonValue.Null},
			       		{"dateExpires", DateExpires.HasValue ? DateExpires.Value.ToString("yyyy-MM-ddTHH:mm:ss.fffZ") : JsonValue.Null},
			       		{"permissions", Permissions.Cast<ManateeTokenPermission>().ToJson()},
			       	};
		}
	}
}
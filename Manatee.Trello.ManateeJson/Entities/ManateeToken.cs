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
	Namespace:		Manatee.Trello.ManateeJson.Entities
	Class Name:		ManateeToken
	Purpose:		Implements IJsonToken for Manatee.Json.

***************************************************************************************/
using System;
using System.Collections.Generic;
using Manatee.Json;
using Manatee.Json.Serialization;
using Manatee.Trello.Json;

namespace Manatee.Trello.ManateeJson.Entities
{
	internal class ManateeToken : IJsonToken, IJsonSerializable
	{
		public string Id { get; set; }
		public string Identifier { get; set; }
		public IJsonMember Member { get; set; }
		public DateTime? DateCreated { get; set; }
		public DateTime? DateExpires { get; set; }
		public List<IJsonTokenPermission> Permissions { get; set; }

		public void FromJson(JsonValue json, JsonSerializer serializer)
		{
			if (json.Type != JsonValueType.Object) return;
			var obj = json.Object;
			Id = obj.TryGetString("id");
			Identifier = obj.TryGetString("identifier");
			Member = obj.Deserialize<IJsonMember>(serializer, "idMember");
			DateCreated = obj.Deserialize<DateTime?>(serializer, "dateCreated");
			DateExpires = obj.Deserialize<DateTime?>(serializer, "dateExpires");
			Permissions = obj.Deserialize<List<IJsonTokenPermission>>(serializer, "permissions");
		}
		public JsonValue ToJson(JsonSerializer serializer)
		{
			return null;
		}
	}
}
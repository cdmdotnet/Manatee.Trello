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
 
	File Name:		ManateeList.cs
	Namespace:		Manatee.Trello.Json.Manatee.Entities
	Class Name:		ManateeList
	Purpose:		Implements IJsonList for Manatee.Json.

***************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Manatee.Json;
using Manatee.Json.Enumerations;
using Manatee.Json.Extensions;
using Manatee.Json.Serialization;

namespace Manatee.Trello.Json.Manatee.Entities
{
	internal class ManateeList : IJsonList, IJsonCompatible
	{
		public string Id { get; set; }
		public string Name { get; set; }
		public bool? Closed { get; set; }
		public string IdBoard { get; set; }
		public double? Pos { get; set; }
		public bool? Subscribed { get; set; }

		public void FromJson(JsonValue json)
		{
			if (json.Type != JsonValueType.Object) return;
			var obj = json.Object;
			Id = obj.TryGetString("id");
			Name = obj.TryGetString("name");
			Closed = obj.TryGetBoolean("closed");
			IdBoard = obj.TryGetString("idBoard");
			Pos = obj.TryGetNumber("pos");
			Subscribed = obj.TryGetBoolean("subscribed");
		}
		public JsonValue ToJson()
		{
			return new JsonObject
			       	{
			       		{"id", Id},
			       		{"name", Name},
			       		{"closed", Closed},
			       		{"idBoard", IdBoard},
			       		{"pos", Pos},
			       		{"subscribed", Subscribed},
			       	};
		}
	}
}

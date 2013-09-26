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
 
	File Name:		ManateeCheckItem.cs
	Namespace:		Manatee.Trello.ManateeJson.Entities
	Class Name:		ManateeCheckItem
	Purpose:		Implements IJsonCheckItem for Manatee.Json.

***************************************************************************************/

using Manatee.Json;
using Manatee.Json.Enumerations;
using Manatee.Json.Extensions;
using Manatee.Json.Serialization;
using Manatee.Trello.Json;

namespace Manatee.Trello.ManateeJson.Entities
{
	internal class ManateeCheckItem : IJsonCheckItem, IJsonCompatible
	{
		public string Id { get; set; }
		public string State { get; set; }
		public string Name { get; set; }
		public double? Pos { get; set; }

		public void FromJson(JsonValue json)
		{
			if (json.Type != JsonValueType.Object) return;
			var obj = json.Object;
			Id = obj.TryGetString("id");
			State = obj.TryGetString("state");
			Name = obj.TryGetString("name");
			Pos = obj.TryGetNumber("pos");
		}
		public JsonValue ToJson()
		{
			return new JsonObject
			       	{
			       		{"id", Id},
			       		{"state", State},
			       		{"name", Name},
			       		{"pos", Pos},
			       	};
		}
	}
}

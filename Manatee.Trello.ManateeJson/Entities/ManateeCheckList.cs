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
 
	File Name:		ManateeCheckList.cs
	Namespace:		Manatee.Trello.ManateeJson.Entities
	Class Name:		ManateeCheckList
	Purpose:		Implements IJsonCheckList for Manatee.Json.

***************************************************************************************/

using Manatee.Json;
using Manatee.Json.Serialization;
using Manatee.Trello.Json;

namespace Manatee.Trello.ManateeJson.Entities
{
	internal class ManateeCheckList : IJsonCheckList, IJsonSerializable
	{
		public string Id { get; set; }
		public string Name { get; set; }
		public string IdBoard { get; set; }
		public string IdCard { get; set; }
		public double? Pos { get; set; }

		public void FromJson(JsonValue json, JsonSerializer serializer)
		{
			if (json.Type != JsonValueType.Object) return;
			var obj = json.Object;
			Id = obj.TryGetString("id");
			Name = obj.TryGetString("name");
			IdBoard = obj.TryGetString("idBoard");
			IdCard = obj.TryGetString("idCard");
			Pos = obj.TryGetNumber("pos");
		}
		public JsonValue ToJson(JsonSerializer serializer)
		{
			return new JsonObject
			       	{
			       		{"id", Id},
			       		{"name", Name},
			       		{"idBoard", IdBoard},
			       		{"idCard", IdCard},
			       		{"pos", Pos},
			       	};
		}
	}
}

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
 
	File Name:		ManateeLabel.cs
	Namespace:		Manatee.Trello.ManateeJson.Entities
	Class Name:		ManateeLabel
	Purpose:		Implements IJsonLabel for Manatee.Json.

***************************************************************************************/

using Manatee.Json;
using Manatee.Json.Serialization;
using Manatee.Trello.Json;

namespace Manatee.Trello.ManateeJson.Entities
{
	internal class ManateeLabel : IJsonLabel, IJsonSerializable
	{
		public IJsonBoard Board { get; set; }
		public LabelColor? Color { get; set; }
		public string Id { get; set; }
		public string Name { get; set; }
		public int? Uses { get; set; }

		public void FromJson(JsonValue json, JsonSerializer serializer)
		{
			if (json.Type != JsonValueType.Object) return;
			var obj = json.Object;
			Board = obj.Deserialize<IJsonBoard>(serializer, "idBoard");
			Color = obj.Deserialize<LabelColor?>(serializer, "color");
			Id = obj.TryGetString("id");
			Name = obj.TryGetString("name");
			Uses = (int?) obj.TryGetNumber("uses");
		}
		public JsonValue ToJson(JsonSerializer serializer)
		{
			var json = new JsonObject();
			Color.Serialize(json, serializer, "color");
			Id.Serialize(json, serializer, "id");
			Name.Serialize(json, serializer, "name");
			return json;
		}
	}
}

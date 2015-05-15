/***************************************************************************************

	Copyright 2015 Greg Dennis

	   Licensed under the Apache License, Version 2.0 (the "License");
	   you may not use this file except in compliance with the License.
	   You may obtain a copy of the License at

		 http://www.apache.org/licenses/LICENSE-2.0

	   Unless required by applicable law or agreed to in writing, software
	   distributed under the License is distributed on an "AS IS" BASIS,
	   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
	   See the License for the specific language governing permissions and
	   limitations under the License.
 
	File Name:		ManateeParameter.cs
	Namespace:		Manatee.Trello.ManateeJson.Entities
	Class Name:		ManateeParameter
	Purpose:		Implements IJsonParameter for Manatee.Json. 

***************************************************************************************/

using Manatee.Json;
using Manatee.Json.Serialization;
using Manatee.Trello.Json;

namespace Manatee.Trello.ManateeJson.Entities
{
	internal class ManateeParameter : IJsonParameter, IJsonSerializable
	{
		public string String { get; set; }
		public bool? Boolean { get; set; }

		public void FromJson(JsonValue json, JsonSerializer serializer)
		{
			if (json.Type != JsonValueType.Object) return;
			String = json.Object.TryGetString("value");
		}
		public JsonValue ToJson(JsonSerializer serializer)
		{
			var json = new JsonObject();

			if (Boolean.HasValue)
				json.Add("value", Boolean);
			else
				json.Add("value", String);

			return json;
		}
	}
}
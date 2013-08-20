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
 
	File Name:		ManateeLabelNames.cs
	Namespace:		Manatee.Trello.ManateeJson.Entities
	Class Name:		ManateeLabelNames
	Purpose:		Implements IJsonLabelNames for Manatee.Json.

***************************************************************************************/

using Manatee.Json;
using Manatee.Json.Enumerations;
using Manatee.Json.Extensions;
using Manatee.Json.Serialization;
using Manatee.Trello.Json;

namespace Manatee.Trello.ManateeJson.Entities
{
	public class ManateeLabelNames : IJsonLabelNames, IJsonCompatible
	{
		public string Red { get; set; }
		public string Orange { get; set; }
		public string Yellow { get; set; }
		public string Green { get; set; }
		public string Blue { get; set; }
		public string Purple { get; set; }

		public void FromJson(JsonValue json)
		{
			if (json.Type != JsonValueType.Object) return;
			var obj = json.Object;
			Red = obj.TryGetString("red");
			Orange = obj.TryGetString("orange");
			Yellow = obj.TryGetString("yellow");
			Green = obj.TryGetString("green");
			Blue = obj.TryGetString("blue");
			Purple = obj.TryGetString("purple");
		}
		public JsonValue ToJson()
		{
			return new JsonObject
			       	{
			       		{"red", Red},
			       		{"orange", Orange},
			       		{"yellow", Yellow},
			       		{"green", Green},
			       		{"blue", Blue},
			       		{"purple", Purple},
			       	};
		}
	}
}

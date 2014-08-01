/***************************************************************************************

	Copyright 2014 Greg Dennis

	   Licensed under the Apache License, Version 2.0 (the "License");
	   you may not use this file except in compliance with the License.
	   You may obtain a copy of the License at

		 http://www.apache.org/licenses/LICENSE-2.0

	   Unless required by applicable law or agreed to in writing, software
	   distributed under the License is distributed on an "AS IS" BASIS,
	   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
	   See the License for the specific language governing permissions and
	   limitations under the License.
 
	File Name:		ManateePosition.cs
	Namespace:		Manatee.Trello.ManateeJson.Entities
	Class Name:		ManateePosition
	Purpose:		Implements IJsonPosition for Manatee.Json.

***************************************************************************************/

using Manatee.Json;
using Manatee.Json.Serialization;
using Manatee.Trello.Json;

namespace Manatee.Trello.ManateeJson.Entities
{
	internal class ManateePosition : IJsonPosition, IJsonSerializable
	{
		public double? Explicit { get; set; }
		public string Named { get; set; }

		public void FromJson(JsonValue json, JsonSerializer serializer)
		{
			switch (json.Type)
			{
				case JsonValueType.Number:
					Explicit = json.Number;
					break;
				case JsonValueType.String:
					Named = json.String;
					break;
			}
		}
		public JsonValue ToJson(JsonSerializer serializer)
		{
			return Named.IsNullOrWhiteSpace()
				       ? (Explicit.HasValue
					          ? Explicit
					          : JsonValue.Null)
				       : Named;
		}
	}
}
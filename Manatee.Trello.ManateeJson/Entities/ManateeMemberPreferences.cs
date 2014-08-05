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
 
	File Name:		ManateeMemberPreferences.cs
	Namespace:		Manatee.Trello.ManateeJson.Entities
	Class Name:		ManateeMemberPreferences
	Purpose:		Implements IJsonMemberPreferences for Manatee.Json.

***************************************************************************************/

using Manatee.Json;
using Manatee.Json.Serialization;
using Manatee.Trello.Json;

namespace Manatee.Trello.ManateeJson.Entities
{
	internal class ManateeMemberPreferences : IJsonMemberPreferences, IJsonSerializable
	{
		public int? MinutesBetweenSummaries { get; set; }
		public bool? ColorBlind { get; set; }

		public void FromJson(JsonValue json, JsonSerializer serializer)
		{
			if (json.Type != JsonValueType.Object) return;
			var obj = json.Object;
			MinutesBetweenSummaries = (int?) obj.TryGetNumber("minutesBetweenSummaries");
			ColorBlind = obj.TryGetBoolean("colorBlind");
		}
		public JsonValue ToJson(JsonSerializer serializer)
		{
			return null;
		}
	}
}

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
 
	File Name:		ManateeAction.cs
	Namespace:		Manatee.Trello.ManateeJson.Entities
	Class Name:		ManateeAction
	Purpose:		Implements IJsonAction for Manatee.Json.

***************************************************************************************/

using System;
using Manatee.Json;
using Manatee.Json.Serialization;
using Manatee.Trello.Json;

namespace Manatee.Trello.ManateeJson.Entities
{
	internal class ManateeAction : IJsonAction, IJsonSerializable
	{
		public string Id { get; set; }
		public IJsonMember MemberCreator { get; set; }
		public IJsonActionData Data { get; set; }
		public ActionType Type { get; set; }
		public DateTime? Date { get; set; }

		public virtual void FromJson(JsonValue json, JsonSerializer serializer)
		{
			if (json.Type != JsonValueType.Object) return;
			var obj = json.Object;
			Id = obj.TryGetString("id");
			MemberCreator = obj.Deserialize<IJsonMember>(serializer, "idMemberCreator");
			Data = obj.Deserialize<IJsonActionData>(serializer, "data");
			Type = serializer.Deserialize<ActionType>(obj.TryGetString("type"));
#if IOS
			var dateString = obj.TryGetString("date");
			DateTime date;
			if (DateTime.TryParse(dateString, out date))
				Date = date;
#else
			Date = obj.Deserialize<DateTime?>(serializer, "date");
#endif
		}
		public virtual JsonValue ToJson(JsonSerializer serializer)
		{
			return null;
		}
	}
}

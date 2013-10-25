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
 
	File Name:		ManateeBoard.cs
	Namespace:		Manatee.Trello.ManateeJson.Entities
	Class Name:		ManateeBoard
	Purpose:		Implements IJsonBoard for Manatee.Json.

***************************************************************************************/

using Manatee.Json;
using Manatee.Json.Serialization;
using Manatee.Trello.Json;

namespace Manatee.Trello.ManateeJson.Entities
{
	internal class ManateeBoard : IJsonBoard, IJsonCompatible
	{
		public string Id { get; set; }
		public string Name { get; set; }
		public string Desc { get; set; }
		public bool? Closed { get; set; }
		public string IdOrganization { get; set; }
		public bool? Pinned { get; set; }
		public string Url { get; set; }
		public bool? Subscribed { get; set; }

		public void FromJson(JsonValue json)
		{
			if (json.Type != JsonValueType.Object) return;
			var obj = json.Object;
			Id = obj.TryGetString("id");
			Name = obj.TryGetString("name");
			Desc = obj.TryGetString("desc");
			Closed = obj.TryGetBoolean("closed");
			IdOrganization = obj.TryGetString("idOrganization");
			Pinned = obj.TryGetBoolean("pinned");
			Url = obj.TryGetString("url");
			Subscribed = obj.TryGetBoolean("subscribed");
		}
		public JsonValue ToJson()
		{
			return new JsonObject
			       	{
			       		{"id", Id},
			       		{"name", Name},
			       		{"desc", Desc},
			       		{"closed", Closed},
			       		{"idOrganization", IdOrganization},
			       		{"pinned", Pinned},
			       		{"url", Url},
			       		{"subscribed", Subscribed},
			       	};
		}
	}
}

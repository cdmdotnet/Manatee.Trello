/***************************************************************************************

	Copyright 2012 Greg Dennis

	   Licensed under the Apache License, Version 2.0 (the "License");
	   you may not use this file except in compliance with the License.
	   You may obtain a copy of the License at

		 http://www.apache.org/licenses/LICENSE-2.0

	   Unless required by applicable law or agreed to in writing, software
	   distributed under the License is distributed on an "AS IS" BASIS,
	   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
	   See the License for the specific language governing permissions and
	   limitations under the License.
 
	File Name:		ManateeWebhook.cs
	Namespace:		Manatee.Trello.ManateeJson.Entities
	Class Name:		ManateeWebhook
	Purpose:		Implements IJsonWebhook for Manatee.Json.

***************************************************************************************/

using Manatee.Json;
using Manatee.Json.Serialization;
using Manatee.Trello.Json;

namespace Manatee.Trello.ManateeJson.Entities
{
	internal class ManateeWebhook : IJsonWebhook, IJsonCompatible
	{
		public string Id { get; set; }
		public string Description { get; set; }
		public string IdModel { get; set; }
		public string CallbackUrl { get; set; }
		public bool? Active { get; set; }

		public void FromJson(JsonValue json)
		{
			if (json.Type != JsonValueType.Object) return;
			var obj = json.Object;
			Id = obj.TryGetString("id");
			Description = obj.TryGetString("description");
			IdModel = obj.TryGetString("idModel");
			CallbackUrl = obj.TryGetString("callbackURL");
			Active = obj.TryGetBoolean("active");
		}
		public JsonValue ToJson()
		{
			return new JsonObject
				{
					{"id", Id},
					{"description", Description},
					{"idModel", IdModel},
					{"callbackURL", CallbackUrl},
					{"active", Active},
				};
		}
	}
}
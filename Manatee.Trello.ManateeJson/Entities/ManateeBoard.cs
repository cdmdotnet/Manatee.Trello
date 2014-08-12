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
	internal class ManateeBoard : IJsonBoard, IJsonSerializable
	{
		public string Id { get; set; }
		public string Name { get; set; }
		public string Desc { get; set; }
		public bool? Closed { get; set; }
		public IJsonOrganization Organization { get; set; }
		public IJsonLabelNames LabelNames { get; set; }
		public IJsonBoardPreferences Prefs { get; set; }
		public string Url { get; set; }
		public bool? Subscribed { get; set; }
		public IJsonBoard BoardSource { get; set; }

		public void FromJson(JsonValue json, JsonSerializer serializer)
		{
			switch (json.Type)
			{
				case JsonValueType.Object:
				{
					var obj = json.Object;
					Id = obj.TryGetString("id");
					Name = obj.TryGetString("name");
					Desc = obj.TryGetString("desc");
					Closed = obj.TryGetBoolean("closed");
					Organization = obj.Deserialize<IJsonOrganization>(serializer, "idOrganization");
					LabelNames = obj.Deserialize<IJsonLabelNames>(serializer, "labelNames");
					Prefs = obj.Deserialize<IJsonBoardPreferences>(serializer, "prefs");
					Url = obj.TryGetString("url");
					Subscribed = obj.TryGetBoolean("subscribed");
				}
					break;
				case JsonValueType.String:
					Id = json.String;
					break;
			}
		}
		public JsonValue ToJson(JsonSerializer serializer)
		{
			var json = new JsonObject();
			Id.Serialize(json, serializer, "id");
			Name.Serialize(json, serializer, "name");
			Desc.Serialize(json, serializer, "desc");
			Closed.Serialize(json, serializer, "closed");
			Subscribed.Serialize(json, serializer, "subscribed");
			Organization.SerializeId(json, "idOrganization");
			BoardSource.SerializeId(json, "idBoardSource");
			// Don't serialize the LabelNames or Preferences collections because Trello wants individual properties.
			if (LabelNames != null)
			{
				json.Add("labelNames/green", LabelNames.Green.IsNullOrWhiteSpace() ? JsonValue.Null : LabelNames.Green);
				json.Add("labelNames/yellow", LabelNames.Yellow.IsNullOrWhiteSpace() ? JsonValue.Null : LabelNames.Yellow);
				json.Add("labelNames/orange", LabelNames.Orange.IsNullOrWhiteSpace() ? JsonValue.Null : LabelNames.Orange);
				json.Add("labelNames/red", LabelNames.Red.IsNullOrWhiteSpace() ? JsonValue.Null : LabelNames.Red);
				json.Add("labelNames/purple", LabelNames.Purple.IsNullOrWhiteSpace() ? JsonValue.Null : LabelNames.Purple);
				json.Add("labelNames/blue", LabelNames.Blue.IsNullOrWhiteSpace() ? JsonValue.Null : LabelNames.Blue);
			}
			if (Prefs != null)
			{
				json.Add("prefs/permissionLevel", serializer.Serialize(Prefs.PermissionLevel));
				json.Add("prefs/selfJoin", !Prefs.SelfJoin.HasValue ? JsonValue.Null : Prefs.SelfJoin);
				json.Add("prefs/cardCovers", !Prefs.CardCovers.HasValue ? JsonValue.Null : Prefs.CardCovers);
				json.Add("prefs/invitations", serializer.Serialize(Prefs.Invitations));
				json.Add("prefs/voting", serializer.Serialize(Prefs.Voting));
				json.Add("prefs/comments", serializer.Serialize(Prefs.Comments));
			}
			return json;
		}
	}
}

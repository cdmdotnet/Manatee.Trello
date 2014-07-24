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
 
	File Name:		ManateeOrganization.cs
	Namespace:		Manatee.Trello.ManateeJson.Entities
	Class Name:		ManateeOrganization
	Purpose:		Implements IJsonOrganization for Manatee.Json.

***************************************************************************************/

using System.Collections.Generic;
using Manatee.Json;
using Manatee.Json.Serialization;
using Manatee.Trello.Json;

namespace Manatee.Trello.ManateeJson.Entities
{
	internal class ManateeOrganization : IJsonOrganization, IJsonSerializable
	{
		public string Id { get; set; }
		public string Name { get; set; }
		public string DisplayName { get; set; }
		public string Desc { get; set; }
		public string Url { get; set; }
		public string Website { get; set; }
		public string LogoHash { get; set; }
		public List<int> PowerUps { get; set; }
		public bool? PaidAccount { get; set; }
		public List<string> PremiumFeatures { get; set; }
		public IJsonOrganizationPreferences Prefs { get; set; }

		public void FromJson(JsonValue json, JsonSerializer serializer)
		{
			switch (json.Type)
			{
				case JsonValueType.Object:
					var obj = json.Object;
					Id = obj.TryGetString("id");
					Name = obj.TryGetString("name");
					DisplayName = obj.TryGetString("displayName");
					Desc = obj.TryGetString("desc");
					Url = obj.TryGetString("url");
					Website = obj.TryGetString("website");
					LogoHash = obj.TryGetString("logoHash");
					PowerUps = obj.Deserialize<List<int>>(serializer, "powerUps");
					PaidAccount = obj.TryGetBoolean("paid_account");
					PremiumFeatures = obj.Deserialize<List<string>>(serializer, "premiumFeatures");
					Prefs = obj.Deserialize<IJsonOrganizationPreferences>(serializer, "prefs");
					break;
				case JsonValueType.String:
					Id = json.String;
					break;
			}
		}
		public JsonValue ToJson(JsonSerializer serializer)
		{
			var json = new JsonObject
				{
					{"id", Id},
					{"name", Name},
					{"displayName", DisplayName},
					{"desc", Desc},
					{"url", Url},
					{"website", Website},
					{"logoHash", LogoHash},
					{"powerUps", serializer.Serialize(PowerUps)},
					{"paid_account", PaidAccount},
					{"premiumFeatures", serializer.Serialize(PremiumFeatures)}
				};
			if (Prefs != null)
			{
				json.Add("prefs/permissionLevel", serializer.Serialize(Prefs.PermissionLevel));
				json.Add("prefs/orgInviteRestrict", serializer.Serialize(Prefs.OrgInviteRestrict));
				json.Add("prefs/associatedDomain", Prefs.AssociatedDomain.IsNullOrWhiteSpace() ? JsonValue.Null : Prefs.AssociatedDomain);
				json.Add("prefs/boardVisibilityRestrict/private", serializer.Serialize(Prefs.BoardVisibilityRestrict.Private));
				json.Add("prefs/boardVisibilityRestrict/org", serializer.Serialize(Prefs.BoardVisibilityRestrict.Org));
				json.Add("prefs/boardVisibilityRestrict/public", serializer.Serialize(Prefs.BoardVisibilityRestrict.Public));
			}
			return json;
		}
	}
}

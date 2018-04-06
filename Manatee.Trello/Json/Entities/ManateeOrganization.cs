using System.Collections.Generic;
using Manatee.Json;
using Manatee.Json.Serialization;

namespace Manatee.Trello.Json.Entities
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
			var json = new JsonObject();
			Id.Serialize(json, serializer, "id");
			Name.Serialize(json, serializer, "name");
			DisplayName.Serialize(json, serializer, "displayName");
			Desc.Serialize(json, serializer, "desc");
			Website.Serialize(json, serializer, "website");
			if (Prefs != null)
			{
				Prefs.PermissionLevel.Serialize(json, serializer, "prefs/permissionLevel");
				Prefs.OrgInviteRestrict.Serialize(json, serializer, "prefs/orgInviteRestrict");
				if (string.IsNullOrWhiteSpace(Prefs.AssociatedDomain))
					json.Add("prefs/associatedDomain", JsonValue.Null);
				else
					Prefs.AssociatedDomain.Serialize(json, serializer, "prefs/associatedDomain");
				if (Prefs.BoardVisibilityRestrict != null)
				{
					Prefs.BoardVisibilityRestrict.Private.Serialize(json, serializer, "prefs/boardVisibilityRestrict/private");
					Prefs.BoardVisibilityRestrict.Org.Serialize(json, serializer, "prefs/boardVisibilityRestrict/org");
					Prefs.BoardVisibilityRestrict.Public.Serialize(json, serializer, "prefs/boardVisibilityRestrict/public");
				}
			}
			return json;
		}
	}
}

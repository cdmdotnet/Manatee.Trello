using System.Collections.Generic;
using Manatee.Json;
using Manatee.Json.Serialization;

namespace Manatee.Trello.Json.Entities
{
	internal class ManateeOrganizationPreferences : IJsonOrganizationPreferences, IJsonSerializable
	{
		public OrganizationPermissionLevel? PermissionLevel { get; set; }
		public List<object> OrgInviteRestrict { get; set; }
		public bool? ExternalMembersDisabled { get; set; }
		public string AssociatedDomain { get; set; }
		public IJsonBoardVisibilityRestrict BoardVisibilityRestrict { get; set; }

		public void FromJson(JsonValue json, JsonSerializer serializer)
		{
			if (json.Type != JsonValueType.Object) return;
			var obj = json.Object;
			if (obj.ContainsKey("permissionLevel") && obj["permissionLevel"].Type == JsonValueType.Object)
				obj = obj["permissionLevel"].Object["prefs"].Object;

			PermissionLevel = obj.Deserialize<OrganizationPermissionLevel?>(serializer, "permissionLevel");
			//OrgInviteRestrict = obj.TryGetArray("orgInviteRestrict").Cast<object>().ToList();
			ExternalMembersDisabled = obj.TryGetBoolean("externalMembersDisabled");
			AssociatedDomain = obj.TryGetString("associatedDomain");
			BoardVisibilityRestrict = obj.Deserialize<IJsonBoardVisibilityRestrict>(serializer, "boardVisibilityRestrict") ?? new ManateeBoardVisibilityRestrict();
		}
		public JsonValue ToJson(JsonSerializer serializer)
		{
			return null;
		}
	}
}

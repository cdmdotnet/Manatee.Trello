using System.Collections.Generic;
using System.Linq;
using Manatee.Json;
using Manatee.Json.Enumerations;
using Manatee.Trello.Implementation;

namespace Manatee.Trello
{
	//{
	//   "boardVisibilityRestrict":{
	//   },
	//   "permissionLevel":"public",
	//   "orgInviteRestrict":[
	//   ],
	//   "externalMembersDisabled":false
	//}
	public class OrganizationPreferences : OwnedEntityBase<Organization>
	{
		private static readonly OneToOneMap<OrganizationPermissionLevel, string> _permissionLevelMap;

		private string _apiPermissionLevel;
		private object _boardVisibilityRestrict;
		private bool? _externalMembersDisabled;
		private List<object> _orgInviteRestrict;
		private OrganizationPermissionLevel _permissionLevel;

		// TODO: Determine structure of this object
		public object BoardVisibilityRestrict
		{
			get
			{
				VerifyNotExpired();
				return _boardVisibilityRestrict;
			}
			set { _boardVisibilityRestrict = value; }
		}
		public bool? ExternalMembersDisabled
		{
			get
			{
				VerifyNotExpired();
				return _externalMembersDisabled;
			}
			set { _externalMembersDisabled = value; }
		}
		// TODO: Determine contents of this array
		public List<object> OrgInviteRestrict
		{
			get
			{
				VerifyNotExpired();
				return _orgInviteRestrict;
			}
			set { _orgInviteRestrict = value; }
		}
		public OrganizationPermissionLevel PermissionLevel
		{
			get { return _permissionLevel; }
			set
			{
				_permissionLevel = value;
				UpdateApiPermissionLevel();
			}
		}

		static OrganizationPreferences()
		{
			_permissionLevelMap = new OneToOneMap<OrganizationPermissionLevel, string>
			                      	{
			                      		{OrganizationPermissionLevel.Private, "private"},
			                      		{OrganizationPermissionLevel.Public, "public"},
			                      	};
		}
		public OrganizationPreferences() {}
		public OrganizationPreferences(TrelloService svc, Organization owner)
			: base(svc, owner) {}

		public override void FromJson(JsonValue json)
		{
			if (json == null) return;
			if (json.Type != JsonValueType.Object) return;
			var obj = json.Object;
			//_boardVisibilityRestrict = obj.TryGetObject("boardVisibilityRestrict").FromJson<?>();
			_externalMembersDisabled = obj.TryGetBoolean("externalMembersDisabled");
			//_orgInviteRestrict = obj.TryGetArray("orgInviteRestrict").FromJson<?>();
			_apiPermissionLevel = obj.TryGetString("permissionLevel");
			UpdatePermissionLevel();
		}
		public override JsonValue ToJson()
		{
			var json = new JsonObject
			           	{
			           		//{"boardVisibilityRestrict", _boardVisibilityRestrict},
			           		{"externalMembersDisabled", _externalMembersDisabled.HasValue ? _externalMembersDisabled.Value : JsonValue.Null},
			           		//{"orgInviteRestrict", _orgInviteRestrict},
			           		{"permissionLevel", _apiPermissionLevel}
			           	};
			return json;
		}
		public override bool Equals(EquatableExpiringObject other)
		{
			return true;
		}

		internal override void Refresh(EquatableExpiringObject entity)
		{
			var prefs = entity as OrganizationPreferences;
			if (prefs == null) return;
			_boardVisibilityRestrict = prefs._orgInviteRestrict;
			_externalMembersDisabled = prefs._externalMembersDisabled;
			_orgInviteRestrict = prefs._orgInviteRestrict;
			_apiPermissionLevel = prefs._apiPermissionLevel;
			UpdatePermissionLevel();

		}
		internal override bool Match(string id)
		{
			return false;
		}

		protected override void Refresh()
		{
			var entity = Svc.Api.GetOwnedEntity<Board, BoardPreferences>(Owner.Id);
			Refresh(entity);
		}
		protected override void PropigateSerivce() {}

		private void UpdatePermissionLevel()
		{
			_permissionLevel = _permissionLevelMap.Any(kvp => kvp.Value == _apiPermissionLevel)
			                   	? _permissionLevelMap[_apiPermissionLevel]
			                   	: OrganizationPermissionLevel.Unknown;
		}
		private void UpdateApiPermissionLevel()
		{
			if (_permissionLevelMap.Any(kvp => kvp.Key == _permissionLevel))
				_apiPermissionLevel = _permissionLevelMap[_permissionLevel];
		}
	}

	public enum OrganizationPermissionLevel
	{
		Unknown = -1,
		Private,
		Public
	}
}

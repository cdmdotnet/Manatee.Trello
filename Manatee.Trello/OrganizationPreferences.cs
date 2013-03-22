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
 
	File Name:		OrganizationPreferences.cs
	Namespace:		Manatee.Trello
	Class Name:		OrganizationPreferences
	Purpose:		Represents available preference settings for an organization
					on Trello.com.

***************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using Manatee.Json;
using Manatee.Json.Enumerations;
using Manatee.Trello.Implementation;
using Manatee.Trello.Rest;

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
	public class OrganizationPreferences : JsonCompatibleExpiringObject
	{
		private static readonly OneToOneMap<OrganizationPermissionLevelType, string> _permissionLevelMap;

		private string _apiPermissionLevel;
		private object _boardVisibilityRestrict;
		private bool? _externalMembersDisabled;
		private List<object> _orgInviteRestrict;
		private OrganizationPermissionLevelType _permissionLevel;

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
		public OrganizationPermissionLevelType PermissionLevel
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
			_permissionLevelMap = new OneToOneMap<OrganizationPermissionLevelType, string>
			                      	{
			                      		{OrganizationPermissionLevelType.Private, "private"},
			                      		{OrganizationPermissionLevelType.Public, "public"},
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

		internal override void Refresh(ExpiringObject entity)
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

		protected override void Get()
		{
			var entity = Svc.Api.Get(new Request<OrganizationPreferences>(new[] {Owner, this}));
			Refresh(entity);
		}
		protected override void PropigateSerivce() {}

		private void UpdatePermissionLevel()
		{
			_permissionLevel = _permissionLevelMap.Any(kvp => kvp.Value == _apiPermissionLevel)
			                   	? _permissionLevelMap[_apiPermissionLevel]
			                   	: OrganizationPermissionLevelType.Unknown;
		}
		private void UpdateApiPermissionLevel()
		{
			if (_permissionLevelMap.Any(kvp => kvp.Key == _permissionLevel))
				_apiPermissionLevel = _permissionLevelMap[_permissionLevel];
		}
	}
}

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
using Manatee.Json.Extensions;
using Manatee.Trello.Contracts;
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
	/// <summary>
	/// Represents available preference settings for an organization.
	/// </summary>
	public class OrganizationPreferences : JsonCompatibleExpiringObject
	{
		private static readonly OneToOneMap<OrganizationPermissionLevelType, string> _permissionLevelMap;

		private string _apiPermissionLevel;
		private string _associatedDomain;
		private BoardVisibilityRestrict _boardVisibilityRestrict;
		private bool? _externalMembersDisabled;
		private List<object> _orgInviteRestrict;
		private OrganizationPermissionLevelType _permissionLevel = OrganizationPermissionLevelType.Unknown;

		/// <summary>
		/// Gets and sets the Google Apps domain.
		/// </summary>
		public string AssociatedDomain
		{
			get
			{
				VerifyNotExpired();
				return _associatedDomain;
			}
			set
			{
				Validate.Writable(Svc);
				if (_associatedDomain == value) return;
				_associatedDomain = value ?? string.Empty;
				Parameters.Add("value", _associatedDomain);
				Put("associatedDomain");
			}
		}
		/// <summary>
		/// ?
		/// </summary>
		public bool? ExternalMembersDisabled
		{
			get
			{
				VerifyNotExpired();
				return _externalMembersDisabled;
			}
			set
			{
				Validate.Writable(Svc);
				if (_externalMembersDisabled == value) return;
				Validate.Nullable(value);
				_externalMembersDisabled = value;
				Parameters.Add("value", _externalMembersDisabled);
				Put("externalMembersDisabled");
			}
		}
		// TODO: Determine contents of this array
		internal List<object> OrgInviteRestrict
		{
			get
			{
				VerifyNotExpired();
				return _orgInviteRestrict;
			}
			set
			{
				Validate.Writable(Svc);
				if (_orgInviteRestrict == value) return;
				_orgInviteRestrict = value;
			}
		}
		/// <summary>
		/// Gets and sets the visibility of Org-visible boards owned by the organization.
		/// </summary>
		public BoardPermissionLevelType OrgVisibleBoardVisibility
		{
			get
			{
				VerifyNotExpired();
				return _boardVisibilityRestrict.Org;
			}
			set
			{
				Validate.Writable(Svc);
				if (_boardVisibilityRestrict.Org == value) return;
				_boardVisibilityRestrict.Org = value;
				Parameters.Add("value", _boardVisibilityRestrict.Org);
				Put("boardVisibilityRestrict/org");
			}
		}
		/// <summary>
		/// Gets and sets who may view the organization.
		/// </summary>
		public OrganizationPermissionLevelType PermissionLevel
		{
			get
			{
				VerifyNotExpired();
				return _permissionLevel;
			}
			set
			{
				Validate.Writable(Svc);
				if (_permissionLevel == value) return;
				_permissionLevel = value;
				UpdateApiPermissionLevel();
				Parameters.Add("value", _apiPermissionLevel);
				Put("permissionLevel");
			}
		}
		/// <summary>
		/// Gets and sets the visibility of private boards owned by the organization.
		/// </summary>
		public BoardPermissionLevelType PrivateBoardVisibility
		{
			get
			{
				VerifyNotExpired();
				return _boardVisibilityRestrict.Private;
			}
			set
			{
				Validate.Writable(Svc);
				if (_boardVisibilityRestrict.Private == value) return;
				_boardVisibilityRestrict.Private = value;
				Parameters.Add("value", _boardVisibilityRestrict.Private);
				Put("boardVisibilityRestrict/private");
			}
		}
		/// <summary>
		/// Gets and sets the visibility of publicly-visible boards owned by the organization.
		/// </summary>
		public BoardPermissionLevelType PublicBoardVisibility
		{
			get
			{
				VerifyNotExpired();
				return _boardVisibilityRestrict.Public;
			}
			set
			{
				Validate.Writable(Svc);
				if (_boardVisibilityRestrict.Public == value) return;
				_boardVisibilityRestrict.Public = value;
				Parameters.Add("value", _boardVisibilityRestrict.Public);
				Put("boardVisibilityRestrict/public");
			}
		}
		internal override string Key { get { return "prefs"; } }

		static OrganizationPreferences()
		{
			_permissionLevelMap = new OneToOneMap<OrganizationPermissionLevelType, string>
			                      	{
			                      		{OrganizationPermissionLevelType.Private, "private"},
			                      		{OrganizationPermissionLevelType.Public, "public"},
			                      	};
		}
		/// <summary>
		/// Creates a new instance of the OrganizationPreferences class.
		/// </summary>
		public OrganizationPreferences()
		{
			_boardVisibilityRestrict = new BoardVisibilityRestrict();
		}
		internal OrganizationPreferences(ITrelloRest svc, Organization owner)
			: base(svc, owner)
		{
			_boardVisibilityRestrict = new BoardVisibilityRestrict();
		}

		/// <summary>
		/// Builds an object from a JsonValue.
		/// </summary>
		/// <param name="json">The JsonValue representation of the object.</param>
		public override void FromJson(JsonValue json)
		{
			if (json == null) return;
			if (json.Type != JsonValueType.Object) return;
			var obj = json.Object;
			_associatedDomain = obj.TryGetString("associatedDomain");
			_boardVisibilityRestrict.FromJson(obj.TryGetObject("boardVisibilityRestrict"));
			_externalMembersDisabled = obj.TryGetBoolean("externalMembersDisabled");
			//_orgInviteRestrict = obj.TryGetArray("orgInviteRestrict").FromJson<?>();
			_apiPermissionLevel = obj.TryGetString("permissionLevel");
			UpdatePermissionLevel();
			_isInitialized = true;
		}
		/// <summary>
		/// Converts an object to a JsonValue.
		/// </summary>
		/// <returns>
		/// The JsonValue representation of the object.
		/// </returns>
		public override JsonValue ToJson()
		{
			if (!_isInitialized) VerifyNotExpired();
			var json = new JsonObject
			           	{
			           		{"associatedDomain", _associatedDomain},
			           		{"boardVisibilityRestrict", _boardVisibilityRestrict.ToJson()},
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
			_boardVisibilityRestrict = prefs._boardVisibilityRestrict;
			_externalMembersDisabled = prefs._externalMembersDisabled;
			_orgInviteRestrict = prefs._orgInviteRestrict;
			_apiPermissionLevel = prefs._apiPermissionLevel;
			UpdatePermissionLevel();
			_isInitialized = true;
		}

		/// <summary>
		/// Retrieves updated data from the service instance and refreshes the object.
		/// </summary>
		protected override void Get()
		{
			var entity = Svc.Get(Svc.RequestProvider.Create<OrganizationPreferences>(new[] {Owner, this}));
			Refresh(entity);
		}
		/// <summary>
		/// Propigates the service instance to the object's owned objects.
		/// </summary>
		protected override void PropigateService() {}

		private void Put(string extension)
		{
			if (Svc == null)
			{
				Parameters.Clear();
				return;
			}
			var request = Svc.RequestProvider.Create<OrganizationPreferences>(new[] { Owner, this }, this, extension);
			Svc.Put(request);
		}
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

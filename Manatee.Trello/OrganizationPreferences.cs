﻿/***************************************************************************************

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
using System.Collections.Generic;
using System.Linq;
using Manatee.Trello.Contracts;
using Manatee.Trello.Internal;
using Manatee.Trello.Internal.Json;
using Manatee.Trello.Json;

namespace Manatee.Trello
{
	/// <summary>
	/// Represents available preference settings for an organization.
	/// </summary>
	public class OrganizationPreferences : ExpiringObject
	{
		private static readonly OneToOneMap<OrganizationPermissionLevelType, string> _permissionLevelMap;

		private IJsonOrganizationPreferences _jsonOrganizationPreferences;
		private BoardVisibilityRestrict _boardVisibilityRestrict;
		private OrganizationPermissionLevelType _permissionLevel = OrganizationPermissionLevelType.Unknown;

		/// <summary>
		/// Gets or sets the Google Apps domain.
		/// </summary>
		public string AssociatedDomain
		{
			get
			{
				VerifyNotExpired();
				return (_jsonOrganizationPreferences == null) ? null : _jsonOrganizationPreferences.AssociatedDomain;
			}
			set
			{
				Validator.Writable();
				if (_jsonOrganizationPreferences == null) return;
				if (_jsonOrganizationPreferences.AssociatedDomain == value) return;
				_jsonOrganizationPreferences.AssociatedDomain = value ?? string.Empty;
				Parameters.Add("value", _jsonOrganizationPreferences.AssociatedDomain);
				Put(EntityRequestType.OrganizationPreferences_Write_AssociatedDomain);
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
				return (_jsonOrganizationPreferences == null) ? null : _jsonOrganizationPreferences.ExternalMembersDisabled;
			}
			set
			{
				Validator.Writable();
				Validator.Nullable(value);
				if (_jsonOrganizationPreferences == null) return;
				if (_jsonOrganizationPreferences.ExternalMembersDisabled == value) return;
				_jsonOrganizationPreferences.ExternalMembersDisabled = value;
				Parameters.Add("value", _jsonOrganizationPreferences.ExternalMembersDisabled);
				Put(EntityRequestType.OrganizationPreferences_Write_ExternalMembersDisabled);
			}
		}
		// TODO: Determine contents of this array
		internal List<object> OrgInviteRestrict
		{
			get
			{
				VerifyNotExpired();
				return (_jsonOrganizationPreferences == null) ? null : _jsonOrganizationPreferences.OrgInviteRestrict;
			}
			set
			{
				Validator.Writable();
				if (_jsonOrganizationPreferences == null) return;
				if (_jsonOrganizationPreferences.OrgInviteRestrict == value) return;
				_jsonOrganizationPreferences.OrgInviteRestrict = value;
				Put(EntityRequestType.OrganizationPreferences_Write_OrgInviteRestrict);
			}
		}
		/// <summary>
		/// Gets or sets the visibility of Org-visible boards owned by the organization.
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
				Validator.Writable();
				if (_jsonOrganizationPreferences == null) return;
				if (_boardVisibilityRestrict.Org == value) return;
				_boardVisibilityRestrict.Org = value;
				Parameters.Add("value", _boardVisibilityRestrict.Org.ToLowerString());
				Put(EntityRequestType.OrganizationPreferences_Write_OrgVisibleBoardVisibility);
			}
		}
		/// <summary>
		/// Gets or sets who may view the organization.
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
				Validator.Writable();
				if (_jsonOrganizationPreferences == null) return;
				if (_permissionLevel == value) return;
				_permissionLevel = value;
				UpdateApiPermissionLevel();
				Parameters.Add("value", _jsonOrganizationPreferences.PermissionLevel);
				Put(EntityRequestType.OrganizationPreferences_Write_PermissionLevel);
			}
		}
		/// <summary>
		/// Gets or sets the visibility of private boards owned by the organization.
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
				Validator.Writable();
				if (_jsonOrganizationPreferences == null) return;
				if (_boardVisibilityRestrict.Private == value) return;
				_boardVisibilityRestrict.Private = value;
				Parameters.Add("value", _boardVisibilityRestrict.Private.ToLowerString());
				Put(EntityRequestType.OrganizationPreferences_Write_PrivateBoardVisibility);
			}
		}
		/// <summary>
		/// Gets or sets the visibility of publicly-visible boards owned by the organization.
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
				Validator.Writable();
				if (_jsonOrganizationPreferences == null) return;
				if (_boardVisibilityRestrict.Public == value) return;
				_boardVisibilityRestrict.Public = value;
				Parameters.Add("value", _boardVisibilityRestrict.Public.ToLowerString());
				Put(EntityRequestType.OrganizationPreferences_Write_PublicBoardVisibility);
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
		/// <summary>
		/// Creates a new instance of the OrganizationPreferences class.
		/// </summary>
		public OrganizationPreferences()
		{
			_jsonOrganizationPreferences = new InnerJsonOrganizationPreferences();
		}
		internal OrganizationPreferences(Organization owner)
			: this()
		{
			Owner = owner;
		}

		/// <summary>
		/// Retrieves updated data from the service instance and refreshes the object.
		/// </summary>
		public override bool Refresh()
		{
			Parameters.Add("_id", Id);
			AddDefaultParameters();
			EntityRepository.Refresh(this, EntityRequestType.OrganizationPreferences_Read_Refresh);
			return true;
		}

		internal override void ApplyJson(object obj)
		{
			_jsonOrganizationPreferences = (IJsonOrganizationPreferences)obj;
			_boardVisibilityRestrict = new BoardVisibilityRestrict(_jsonOrganizationPreferences.BoardVisibilityRestrict);
			UpdatePermissionLevel();
		}

		private void Put(EntityRequestType requestType)
		{
			Parameters.Add("_id", Id);
			EntityRepository.Upload(requestType, Parameters);
		}
		private void UpdatePermissionLevel()
		{
			_permissionLevel = _permissionLevelMap.Any(kvp => kvp.Value == _jsonOrganizationPreferences.PermissionLevel)
			                   	? _permissionLevelMap[_jsonOrganizationPreferences.PermissionLevel]
			                   	: OrganizationPermissionLevelType.Unknown;
		}
		private void UpdateApiPermissionLevel()
		{
			if (_permissionLevelMap.Any(kvp => kvp.Key == _permissionLevel))
				_jsonOrganizationPreferences.PermissionLevel = _permissionLevelMap[_permissionLevel];
		}
	}
}

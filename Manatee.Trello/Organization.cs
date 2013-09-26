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
 
	File Name:		Organization.cs
	Namespace:		Manatee.Trello
	Class Name:		Organization
	Purpose:		Represents an organization on Trello.com.

***************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using Manatee.Trello.Contracts;
using Manatee.Trello.Internal;
using Manatee.Trello.Internal.Json;
using Manatee.Trello.Json;

namespace Manatee.Trello
{
	/// <summary>
	/// Represents an organization.
	/// </summary>
	public class Organization : ExpiringObject, IEquatable<Organization>, IComparable<Organization>
	{
		private IJsonOrganization _jsonOrganization;
		private readonly ExpiringList<Action> _actions;
		private readonly ExpiringList<Board> _boards;
		private readonly ExpiringList<Member> _invitedMembers;
		private readonly ExpiringList<Member> _members;
		private readonly ExpiringList<OrganizationMembership> _memberships;
		private readonly OrganizationPreferences _preferences;
		private bool _isDeleted;

		///<summary>
		/// Enumerates all actions associated with this organization.
		///</summary>
		public IEnumerable<Action> Actions { get { return _isDeleted ? Enumerable.Empty<Action>() : _actions; } }
		/// <summary>
		/// Enumerates the boards owned by the organization.
		/// </summary>
		public IEnumerable<Board> Boards { get { return _isDeleted ? Enumerable.Empty<Board>() : _boards; } }
		/// <summary>
		/// Gets or sets the description for the organization.
		/// </summary>
		public string Description
		{
			get
			{
				if (_isDeleted) return null;
				VerifyNotExpired();
				return _jsonOrganization.Desc;
			}
			set
			{
				if (_isDeleted) return;
				Validator.Writable();
				if (_jsonOrganization.Desc == value) return;
				_jsonOrganization.Desc = value;
				Parameters.Add("desc", _jsonOrganization.Desc ?? string.Empty);
				Upload(EntityRequestType.Organization_Write_Description);
			}
		}
		/// <summary>
		/// Gets or sets the name to be displayed for the organization.
		/// </summary>
		public string DisplayName
		{
			get
			{
				if (_isDeleted) return null;
				VerifyNotExpired();
				return _jsonOrganization.DisplayName;
			}
			set
			{
				if (_isDeleted) return;
				Validator.Writable();
				if (_jsonOrganization.DisplayName == value) return;
				_jsonOrganization.DisplayName = Validator.MinStringLength(value, 4, "DisplayName");
				Parameters.Add("displayName", _jsonOrganization.DisplayName);
				Upload(EntityRequestType.Organization_Write_DisplayName);
			}
		}
		/// <summary>
		/// Gets a unique identifier (not necessarily a GUID).
		/// </summary>
		public override string Id
		{
			get { return _jsonOrganization.Id; }
			internal set { _jsonOrganization.Id = value; }
		}
		/// <summary>
		/// Enumerates all members who have received invitations to this organization.
		/// </summary>
		public IEnumerable<Member> InvitedMembers { get { return _invitedMembers; } }
		/// <summary>
		/// Gets whether this organization has paid features.
		/// </summary>
		internal bool? IsPaidAccount { get { return _jsonOrganization.PaidAccount; } }
		/// <summary>
		/// Gets the organization's logo hash.
		/// </summary>
		public string LogoHash
		{
			get
			{
				if (_isDeleted) return null;
				VerifyNotExpired();
				return _jsonOrganization.LogoHash;
			}
		}
		/// <summary>
		/// Enumerates the members who belong to the organization.
		/// </summary>
		public IEnumerable<Member> Members { get { return _isDeleted ? Enumerable.Empty<Member>() : _members; } }
		/// <summary>
		/// Enumerates the members who belong to the organization.
		/// </summary>
		public IEnumerable<OrganizationMembership> Memberships { get { return _isDeleted ? Enumerable.Empty<OrganizationMembership>() : _memberships; } }
		/// <summary>
		/// Gets or sets the name of the organization.
		/// </summary>
		public string Name
		{
			get
			{
				if (_isDeleted) return null;
				VerifyNotExpired();
				return _jsonOrganization.Name;
			}
			set
			{
				if (_isDeleted) return;
				Validator.Writable();
				if (_jsonOrganization.Name == value) return;
				_jsonOrganization.Name = Validator.OrgName(value);
				Parameters.Add("name", _jsonOrganization.Name);
				Upload(EntityRequestType.Organization_Write_Name);
			}
		}
		/// <summary>
		/// Enumerates the powerups obtained by the organization.
		/// </summary>
		public IEnumerable<int> PowerUps
		{
			get
			{
				if (_isDeleted) return Enumerable.Empty<int>();
				VerifyNotExpired();
				return _jsonOrganization.PowerUps;
			}
		}
		///<summary>
		/// Gets the set of preferences for the organization.
		///</summary>
		public OrganizationPreferences Preferences { get { return _isDeleted ? null : _preferences; } }
		/// <summary>
		/// Gets a collection of premium features available to the organization.
		/// </summary>
		public IEnumerable<string> PremiumFeatures
		{
			get
			{
				if (_isDeleted) return Enumerable.Empty<string>();
				VerifyNotExpired();
				return _jsonOrganization.PremiumFeatures;
			}
		}
		/// <summary>
		/// Gets the URL to the organization's profile.
		/// </summary>
		public string Url { get { return (_jsonOrganization == null) ? null : _jsonOrganization.Url; } }
		/// <summary>
		/// Gets or sets the organization's website.
		/// </summary>
		public string Website
		{
			get
			{
				if (_isDeleted) return null;
				VerifyNotExpired();
				return _jsonOrganization.Website;
			}
			set
			{
				if (_isDeleted) return;
				Validator.Writable();
				Validator.Url(value);
				if (_jsonOrganization.Website == value) return;
				_jsonOrganization.Website = value;
				Parameters.Add("website", _jsonOrganization.Website ?? string.Empty);
				Upload(EntityRequestType.Organization_Write_Website);
			}
		}
		/// <summary>
		/// Gets whether this entity represents an actual entity on Trello.
		/// </summary>
		public override bool IsStubbed { get { return _jsonOrganization is InnerJsonOrganization; } }

		/// <summary>
		/// Creates a new instance of the Organization class.
		/// </summary>
		public Organization()
		{
			_jsonOrganization = new InnerJsonOrganization();
			_actions = new ExpiringList<Action>(this, EntityRequestType.Organization_Read_Actions);
			_boards = new ExpiringList<Board>(this, EntityRequestType.Organization_Read_Boards);
			_invitedMembers = new ExpiringList<Member>(this, EntityRequestType.Organization_Read_InvitedMembers);
			_members = new ExpiringList<Member>(this, EntityRequestType.Organization_Read_Members);
			_memberships = new ExpiringList<OrganizationMembership>(this, EntityRequestType.Organization_Read_Memberships);
			_preferences = new OrganizationPreferences(this);
		}

		///<summary>
		/// Adds an existing member to the organization or updates the permissions of a member already in the organization.
		///</summary>
		///<param name="member">The member</param>
		///<param name="type">The permission level for the member</param>
		public void AddOrUpdateMember(Member member, OrganizationMembershipType type = OrganizationMembershipType.Normal)
		{
			if (_isDeleted) return;
			Validator.Writable();
			Validator.Entity(member);
			Parameters["_id"] = Id;
			Parameters.Add("_memberId", member.Id);
			Parameters.Add("type", type.ToLowerString());
			EntityRepository.Upload(EntityRequestType.Organization_Write_AddOrUpdateMember, Parameters);
			_members.Add(member);
			_members.MarkForUpdate();
			_memberships.MarkForUpdate();
			_actions.MarkForUpdate();
		}
		// / <summary>
		// /  Adds a new or existing member to the organization or updates the permissions of a member already in the organization.
		// / </summary>
		// / <param name="fullName"></param>
		// / <param name="type">The permission level for the member</param>
		// / <param name="emailAddress"></param>
		//public Member AddOrUpdateMember(string emailAddress, string fullName, OrganizationMembershipType type = OrganizationMembershipType.Normal)
		//{
		//	throw new NotImplementedException("The functionality to add a non-Trello member to to organizations has been temporarily disabled.");
		//	if (_isDeleted) return null;
		//	Validator.Writable();
		//	Validator.NonEmptyString(emailAddress);
		//	Validator.NonEmptyString(fullName);
		//	Member member = null;
		//	if (member != null)
		//	{
		//		AddOrUpdateMember(member, type);
		//		_members.MarkForUpdate();
		//		_memberships.MarkForUpdate();
		//		_actions.MarkForUpdate();
		//	}
		//	return member;
		//}
		/// <summary>
		/// Creates a board for the organization, owned by the current member.
		/// </summary>
		/// <param name="name">The name of the board.</param>
		/// <returns>The newly-created Board object.</returns>
		public Board CreateBoard(string name)
		{
			Validator.Writable();
			Validator.NonEmptyString(name);
			Parameters.Add("name", name);
			Parameters.Add("idOrganization", Id);
			var board = EntityRepository.Download<Board>(EntityRequestType.Organization_Write_CreateBoard, Parameters);
			UpdateDependencies(board);
			_boards.Add(board);
			_boards.MarkForUpdate();
			return board;
		}
		/// <summary>
		/// Deletes the organization.  This cannot be undone.
		/// </summary>
		public void Delete()
		{
			if (_isDeleted) return;
			Validator.Writable();
			Parameters["_id"] = Id;
			EntityRepository.Upload(EntityRequestType.Organization_Write_Delete, Parameters);
			foreach (var member in _members)
			{
				member.OrganizationsList.MarkForUpdate();
			}
			_isDeleted = true;
		}
		/// <summary>
		/// Extends an invitation to the organization to another member.
		/// </summary>
		/// <param name="member">The member to invite.</param>
		/// <param name="type">The level of membership offered.</param>
		internal void InviteMember(Member member, BoardMembershipType type = BoardMembershipType.Normal)
		{
			if (_isDeleted) return;
			Validator.Writable();
			Validator.Entity(member);
			Log.Error(new NotSupportedException("Inviting members to organizations is not yet supported by the Trello API."));
		}
		///<summary>
		/// Removes a member from the organization.
		///</summary>
		///<param name="member"></param>
		public void RemoveMember(Member member)
		{
			if (_isDeleted) return;
			Validator.Writable();
			Validator.Entity(member);
			Parameters["_id"] = Id;
			Parameters.Add("_memberId", member);
			EntityRepository.Upload(EntityRequestType.Organization_Write_RemoveMember, Parameters);
			_members.Remove(member);
			_members.MarkForUpdate();
			_memberships.MarkForUpdate();
			_actions.MarkForUpdate();
		}
		/// <summary>
		/// Rescinds an existing invitation to the organization.
		/// </summary>
		/// <param name="member"></param>
		internal void RescindInvitation(Member member)
		{
			if (_isDeleted) return;
			Validator.Writable();
			Validator.Entity(member);
			Log.Error(new NotSupportedException("Inviting members to organizations is not yet supported by the Trello API."));
		}
		/// <summary>
		/// Indicates whether the current object is equal to another object of the same type.
		/// </summary>
		/// <returns>
		/// true if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.
		/// </returns>
		/// <param name="other">An object to compare with this object.</param>
		public bool Equals(Organization other)
		{
			return Id == other.Id;
		}
		/// <summary>
		/// Determines whether the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>.
		/// </summary>
		/// <returns>
		/// true if the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>; otherwise, false.
		/// </returns>
		/// <param name="obj">The object to compare with the current object. </param><filterpriority>2</filterpriority>
		public override bool Equals(object obj)
		{
			if (!(obj is Organization)) return false;
			return Equals((Organization) obj);
		}
		/// <summary>
		/// Serves as a hash function for a particular type. 
		/// </summary>
		/// <returns>
		/// A hash code for the current <see cref="T:System.Object"/>.
		/// </returns>
		/// <filterpriority>2</filterpriority>
		public override int GetHashCode()
		{
			return Id.GetHashCode();
		}
		/// <summary>
		/// Compares the current object with another object of the same type.
		/// </summary>
		/// <returns>
		/// A value that indicates the relative order of the objects being compared. The return value has the following meanings: Value Meaning Less than zero This object is less than the <paramref name="other"/> parameter.Zero This object is equal to <paramref name="other"/>. Greater than zero This object is greater than <paramref name="other"/>. 
		/// </returns>
		/// <param name="other">An object to compare with this object.</param>
		public int CompareTo(Organization other)
		{
			var order = string.Compare(DisplayName, other.DisplayName, StringComparison.InvariantCulture);
			return order;
		}
		/// <summary>
		/// Returns a string that represents the current object.
		/// </summary>
		/// <returns>
		/// A string that represents the current object.
		/// </returns>
		/// <filterpriority>2</filterpriority>
		public override string ToString()
		{
			return DisplayName;
		}
		/// <summary>
		/// Retrieves updated data from the service instance and refreshes the object.
		/// </summary>
		public override bool Refresh()
		{
			if (_isDeleted) return false;
			Parameters["_id"] = Id;
			AddDefaultParameters();
			return EntityRepository.Refresh(this, EntityRequestType.Organization_Read_Refresh);
		}

		internal override void ApplyJson(object obj)
		{
			_jsonOrganization = (IJsonOrganization)obj;
			Expires = DateTime.Now + EntityRepository.EntityDuration;
		}
		internal override bool Matches(string id)
		{
			return (Id == id) || (Name == id);
		}
		internal override void PropagateDependencies()
		{
			UpdateDependencies(_actions);
			UpdateDependencies(_boards);
			UpdateDependencies(_invitedMembers);
			UpdateDependencies(_members);
			UpdateDependencies(_memberships);
			UpdateDependencies(_preferences);
		}
		internal void ForceDeleted(bool deleted)
		{
			_isDeleted = deleted;
		}

		private void Upload(EntityRequestType requestType)
		{
			Parameters["_id"] = Id;
			EntityRepository.Upload(requestType, Parameters);
			_actions.MarkForUpdate();
		}
	}
}

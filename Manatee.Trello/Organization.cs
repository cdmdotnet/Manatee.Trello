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
using Manatee.Trello.Contracts;
using Manatee.Trello.Internal;
using Manatee.Trello.Internal.Json;
using Manatee.Trello.Json;

namespace Manatee.Trello
{
	//{
	//   "id":"50d4eb07a1b0902152003329",
	//   "name":"littlecrabsolutions",
	//   "displayName":"Little Crab Solutions",
	//   "desc":"",
	//   "url":"https://trello.com/littlecrabsolutions",
	//   "website":null,
	//   "logoHash":null,
	//   "powerUps":[
	//   ]
	//}
	/// <summary>
	/// Represents an organization.
	/// </summary>
	public class Organization : ExpiringObject, IEquatable<Organization>
	{
		private IJsonOrganization _jsonOrganization;
		private readonly ExpiringList<Action, IJsonAction> _actions;
		private readonly ExpiringList<Board, IJsonBoard> _boards;
		private readonly ExpiringList<Member, IJsonMember> _members;
		private readonly OrganizationPreferences _preferences;

		///<summary>
		/// Enumerates all actions associated with this organization.
		///</summary>
		public IEnumerable<Action> Actions { get { return _actions; } }
		/// <summary>
		/// Enumerates the boards owned by the organization.
		/// </summary>
		public IEnumerable<Board> Boards { get { return _boards; } }
		/// <summary>
		/// Gets or sets the description for the organization.
		/// </summary>
		public string Description
		{
			get
			{
				VerifyNotExpired();
				return (_jsonOrganization == null) ? null : _jsonOrganization.Desc;
			}
			set
			{
				Validate.Writable(Svc);
				if (_jsonOrganization == null) return;
				if (_jsonOrganization.Desc == value) return;
				_jsonOrganization.Desc = value ?? string.Empty;
				Parameters.Add("desc", _jsonOrganization.Desc);
				Put();
			}
		}
		/// <summary>
		/// Gets or sets the name to be displayed for the organization.
		/// </summary>
		public string DisplayName
		{
			get
			{
				VerifyNotExpired();
				return (_jsonOrganization == null) ? null : _jsonOrganization.DisplayName;
			}
			set
			{
				Validate.Writable(Svc);
				if (_jsonOrganization == null) return;
				if (_jsonOrganization.DisplayName == value) return;
				_jsonOrganization.DisplayName = Validate.MinStringLength(value, 4, "DisplayName");
				Parameters.Add("displayName", _jsonOrganization.DisplayName);
				Put();
			}
		}
		/// <summary>
		/// Gets a unique identifier (not necessarily a GUID).
		/// </summary>
		public override string Id
		{
			get { return _jsonOrganization != null ? _jsonOrganization.Id : base.Id; }
			internal set
			{
				if (_jsonOrganization != null)
					_jsonOrganization.Id = value;
				base.Id = value;
			}
		}
		/// <summary>
		/// Gets the organization's logo hash.
		/// </summary>
		public string LogoHash
		{
			get
			{
				VerifyNotExpired();
				return (_jsonOrganization == null) ? null : _jsonOrganization.LogoHash;
			}
		}
		/// <summary>
		/// Enumerates the members who belong to the organization.
		/// </summary>
		public IEnumerable<Member> Members { get { return _members; } }
		/// <summary>
		/// Gets or sets the name of the organization.
		/// </summary>
		public string Name
		{
			get
			{
				VerifyNotExpired();
				return (_jsonOrganization == null) ? null : _jsonOrganization.Name;
			}
			set
			{
				Validate.Writable(Svc);
				if (_jsonOrganization == null) return;
				if (_jsonOrganization.Name == value) return;
				_jsonOrganization.Name = Validate.OrgName(Api, value);
				Parameters.Add("name", _jsonOrganization.Name);
				Put();
			}
		}
		/// <summary>
		/// Enumerates the powerups obtained by the organization.
		/// </summary>
		public IEnumerable<int> PowerUps
		{
			get
			{
				VerifyNotExpired();
				return (_jsonOrganization == null) ? null : _jsonOrganization.PowerUps;
			}
		}
		///<summary>
		/// Gets the set of preferences for the organization.
		///</summary>
		public OrganizationPreferences Preferences { get { return _preferences; } }
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
				VerifyNotExpired();
				return (_jsonOrganization == null) ? null : _jsonOrganization.Website;
			}
			set
			{
				Validate.Writable(Svc);
				if (_jsonOrganization == null) return;
				if (_jsonOrganization.Website == value) return;
				_jsonOrganization.Website = value ?? string.Empty;
				Parameters.Add("website", _jsonOrganization.Website);
				Put();
			}
		}

		internal override string Key { get { return "organizations"; } }
		/// <summary>
		/// Gets whether the entity is a cacheable item.
		/// </summary>
		protected override bool Cacheable { get { return true; } }

		/// <summary>
		/// Creates a new instance of the Organization class.
		/// </summary>
		public Organization()
		{
			_jsonOrganization = new InnerJsonOrganization();
			_actions = new ExpiringList<Action, IJsonAction>(this, "actions");
			_boards = new ExpiringList<Board, IJsonBoard>(this, "boards");
			_members = new ExpiringList<Member, IJsonMember>(this, "members");
			_preferences = new OrganizationPreferences(this);
		}

		/// <summary>
		/// Creates a board in the organization.
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public Board AddBoard(string name)
		{
			if (Svc == null) return null;
			Validate.Writable(Svc);
			Validate.NonEmptyString(name);
			var board = new Board();
			var endpoint = EndpointGenerator.Default.Generate(board);
			var request = Api.RequestProvider.Create(endpoint.ToString());
			request.AddParameter("name", name);
			board.ApplyJson(Api.Post<IJsonBoard>(request));
			_boards.MarkForUpdate();
			return board;
		}
		///<summary>
		/// Adds a member to the organization or updates the permissions of an existing member.
		///</summary>
		///<param name="member">The member</param>
		///<param name="type">The permission level for the member</param>
		public void AddOrUpdateMember(Member member, BoardMembershipType type = BoardMembershipType.Normal)
		{
			if (Svc == null) return;
			Validate.Writable(Svc);
			Validate.Entity(member);
			var endpoint = EndpointGenerator.Default.Generate(this, member);
			var request = Api.RequestProvider.Create(endpoint.ToString());
			request.AddParameter("type", type.ToLowerString());
			Api.Put<IJsonOrganization>(request);
			_members.MarkForUpdate();
			_actions.MarkForUpdate();
		}
		/// <summary>
		/// Deletes the organization.  This cannot be undone.
		/// </summary>
		public void Delete()
		{
			if (Svc == null) return;
			Validate.Writable(Svc);
			var endpoint = EndpointGenerator.Default.Generate(this);
			var request = Api.RequestProvider.Create(endpoint.ToString());
			Api.Delete<IJsonOrganization>(request);
		}
		/// <summary>
		/// Extends an invitation to the organization to another member.
		/// </summary>
		/// <param name="member">The member to invite.</param>
		/// <param name="type">The level of membership offered.</param>
		internal void InviteMember(Member member, BoardMembershipType type = BoardMembershipType.Normal)
		{
			if (Svc == null) return;
			Validate.Writable(Svc);
			Validate.Entity(member);
			throw new NotSupportedException("Inviting members to organizations is not yet supported by the Trello API.");
		}
		///<summary>
		/// Removes a member from the organization.
		///</summary>
		///<param name="member"></param>
		public void RemoveMember(Member member)
		{
			if (Svc == null) return;
			Validate.Writable(Svc);
			Validate.Entity(member);
			var endpoint = EndpointGenerator.Default.Generate(this, member);
			var request = Api.RequestProvider.Create(endpoint.ToString());
			Api.Delete<IJsonOrganization>(request);
		}
		/// <summary>
		/// Rescinds an existing invitation to the organization.
		/// </summary>
		/// <param name="member"></param>
		internal void RescindInvitation(Member member)
		{
			if (Svc == null) return;
			Validate.Writable(Svc);
			Validate.Entity(member);
			throw new NotSupportedException("Inviting members to organizations is not yet supported by the Trello API.");
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
			return base.GetHashCode();
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
		protected override void Refresh()
		{
			var endpoint = EndpointGenerator.Default.Generate(this);
			var request = Api.RequestProvider.Create(endpoint.ToString());
			ApplyJson(Api.Get<IJsonOrganization>(request));
		}
		/// <summary>
		/// Propigates the service instance to the object's owned objects.
		/// </summary>
		protected override void PropigateService()
		{
			_actions.Svc = Svc;
			_boards.Svc = Svc;
			_members.Svc = Svc;
			_preferences.Svc = Svc;
		}

		internal override void ApplyJson(object obj)
		{
			_jsonOrganization = (IJsonOrganization) obj;
		}
		internal override bool Matches(string id)
		{
			return (Id == id) || (Name == id);
		}

		private void Put()
		{
			if (Svc == null)
			{
				Parameters.Clear();
				return;
			}
			var endpoint = EndpointGenerator.Default.Generate(this);
			var request = Api.RequestProvider.Create(endpoint.ToString());
			foreach (var parameter in Parameters)
			{
				request.AddParameter(parameter.Key, parameter.Value);
			}
			Api.Put<IJsonOrganization>(request);
			_actions.MarkForUpdate();
		}
	}
}

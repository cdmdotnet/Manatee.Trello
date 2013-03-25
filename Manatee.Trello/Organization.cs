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
using System.Linq;
using Manatee.Json;
using System.Collections.Generic;
using Manatee.Json.Enumerations;
using Manatee.Json.Extensions;
using Manatee.Trello.Implementation;
using Manatee.Trello.Rest;

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
	public class Organization : JsonCompatibleExpiringObject, IEquatable<Organization>
	{
		private readonly ExpiringList<Organization, Action> _actions;
		private readonly ExpiringList<Organization, Board> _boards;
		private string _description;
		private string _displayName;
		private string _logoHash;
		private readonly ExpiringList<Organization, Member> _members;
		private string _name;
		private List<string> _powerUps;
		private readonly OrganizationPreferences _preferences;
		private string _url;
		private string _website;

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
				return _description;
			}
			set
			{
				_description = value;
				Parameters.Add("desc", _description);
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
				return _displayName;
			}
			set
			{
				_displayName = value;
				Parameters.Add("displayName", _displayName);
				Put();
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
				return _logoHash;
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
				return _name;
			}
			set
			{
				_name = value;
				Parameters.Add("name", _name);
				Put();
			}
		}
		/// <summary>
		/// Enumerates the powerups obtained by the organization.
		/// </summary>
		public IEnumerable<string> PowerUps
		{
			get
			{
				VerifyNotExpired();
				return _powerUps;
			}
		}
		///<summary>
		/// Gets the set of preferences for the organization.
		///</summary>
		public OrganizationPreferences Preferences { get { return _preferences; } }
		/// <summary>
		/// Gets the URL to the organization's profile.
		/// </summary>
		public string Url
		{
			get
			{
				VerifyNotExpired();
				return _url;
			}
		}
		/// <summary>
		/// Gets or sets the organization's website.
		/// </summary>
		public string Website
		{
			get
			{
				VerifyNotExpired();
				return _website;
			}
			set
			{
				_website = value;
				Parameters.Add("website", _website);
				Put();
			}
		}

		/// <summary>
		/// Creates a new instance of the Organization class.
		/// </summary>
		public Organization()
		{
			_actions = new ExpiringList<Organization, Action>(this);
			_boards = new ExpiringList<Organization, Board>(this);
			_members = new ExpiringList<Organization, Member>(this);
			_preferences = new OrganizationPreferences();
		}
		internal Organization(TrelloService svc, string id)
			: base(svc, id)
		{
			_actions = new ExpiringList<Organization, Action>(svc, this);
			_boards = new ExpiringList<Organization, Board>(svc, this);
			_members = new ExpiringList<Organization, Member>(svc, this);
			_preferences = new OrganizationPreferences(svc, this);
		}

		/// <summary>
		/// Creates a board in the organization.
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public Board AddBoard(string name)
		{
			var board = Svc.PostAndCache(new Request<Board>(new[] {new Board()}));
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
			var request = new Request<Member>(new ExpiringObject[] {this, member}, this);
			Parameters.Add("type", type.ToLowerString());
			Svc.PutAndCache(request);
			_members.MarkForUpdate();
			_actions.MarkForUpdate();
		}
		/// <summary>
		/// Deletes the organization.  This cannot be undone.
		/// </summary>
		public void Delete()
		{
			Svc.DeleteFromCache(new Request<Organization>(Id));
		}
		/// <summary>
		/// Extends an invitation to the organization to another member.
		/// </summary>
		/// <param name="member">The member to invite.</param>
		/// <param name="type">The level of membership offered.</param>
		private void InviteMember(Member member, BoardMembershipType type = BoardMembershipType.Normal)
		{
			throw new NotSupportedException("Inviting members to organizations is not yet supported by the Trello API.");
		}
		///<summary>
		/// Removes a member from the organization.
		///</summary>
		///<param name="member"></param>
		public void RemoveMember(Member member)
		{
			Svc.DeleteFromCache(new Request<Board>(new ExpiringObject[] {this, member}));
		}
		/// <summary>
		/// Rescinds an existing invitation to the organization.
		/// </summary>
		/// <param name="member"></param>
		private void RescindInvitation(Member member)
		{
			throw new NotSupportedException("Inviting members to organizations is not yet supported by the Trello API.");
		}
		public override void FromJson(JsonValue json)
		{
			if (json == null) return;
			if (json.Type != JsonValueType.Object) return;
			var obj = json.Object;
			Id = obj.TryGetString("id");
			_description = obj.TryGetString("desc");
			_displayName = obj.TryGetString("displayName");
			_logoHash = obj.TryGetString("logoHash");
			_name = obj.TryGetString("name");
			var powerUps = obj.TryGetArray("powerUps");
			if (powerUps != null)
				_powerUps = powerUps.Select(v => v.Type == JsonValueType.Null ? null : v.String).ToList();
			_url = obj.TryGetString("url");
			_website = obj.TryGetString("website");
		}
		public override JsonValue ToJson()
		{
			var json = new JsonObject
			           	{
			           		{"id", Id},
			           		{"desc", _description},
			           		{"displayName", _displayName},
			           		{"logoHash", _logoHash},
			           		{"name", _name},
			           		{"powerUps", _powerUps.ToJson()},
			           		{"url", _url},
			           		{"website", _website}
			           	};
			return json;
		}
		public bool Equals(Organization other)
		{
			return Id == other.Id;
		}

		internal override void Refresh(ExpiringObject entity)
		{
			var org = entity as Organization;
			if (org == null) return;
			_description = org._description;
			_displayName = org._displayName;
			_logoHash = org._logoHash;
			_name = org._name;
			_powerUps = org._powerUps;
			_url = org._url;
			_website = org._website;
		}
		internal override bool Match(string id)
		{
			return Id == id;
		}

		protected override void Get()
		{
			var entity = Svc.Api.Get(new Request<Organization>(Id));
			Refresh(entity);
		}
		protected override void PropigateSerivce()
		{
			_actions.Svc = Svc;
			_boards.Svc = Svc;
			_members.Svc = Svc;
			_preferences.Svc = Svc;
		}

		private void Put()
		{
			Svc.PutAndCache(new Request<Organization>(this));
			_actions.MarkForUpdate();
		}
	}
}

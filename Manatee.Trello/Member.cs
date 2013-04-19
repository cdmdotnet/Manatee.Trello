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
 
	File Name:		Member.cs
	Namespace:		Manatee.Trello
	Class Name:		Member
	Purpose:		Represents a member (user) on Trello.com.

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
	//   "id":"514464db3fa062da6e00254f",
	//   "avatarHash":null,
	//   "bio":"This is a service account for the Little Crab Solutions organization.",
	//   "fullName":"Little Crab Solutions",
	//   "initials":"LS",
	//   "memberType":"normal",
	//   "status":"active",
	//   "url":"https://trello.com/s_littlecrabsolutions",
	//   "username":"s_littlecrabsolutions",
	//   "avatarSource":"none",
	//   "confirmed":true,
	//   "email":null,
	//   "gravatarHash":"946866d028bf7a004a51ca57f7cc1cf2",
	//   "idBoards":[
	//      "514464db3fa062da6e002550",
	//      "5144051cbd0da6681200201e"
	//   ],
	//   "idBoardsInvited":[

	//   ],
	//   "idBoardsPinned":[
	//      "514464db3fa062da6e002550",
	//      "5144051cbd0da6681200201e"
	//   ],
	//   "idOrganizations":[
	//      "50d4eb07a1b0902152003329"
	//   ],
	//   "idOrganizationsInvited":[

	//   ],
	//   "idPremOrgsAdmin":[

	//   ],
	//   "loginTypes":null,
	//   "prefs":{
	//      "sendSummaries":true,
	//      "minutesBetweenSummaries":60,
	//      "minutesBeforeDeadlineToNotify":1440,
	//      "colorBlind":false
	//   },
	//   "trophies":[

	//   ],
	//   "uploadedAvatarHash":null
	//}
	/// <summary>
	/// Represents a member (user).
	/// </summary>
	public class Member : JsonCompatibleExpiringObject, IEquatable<Member>
	{
		private static readonly OneToOneMap<MemberStatusType, string> _statusMap;

		private readonly ExpiringList<Member, Action> _actions;
		private string _apiStatus;
		private string _avatarHash;
		private string _avatarSource;
		private string _bio;
		private readonly ExpiringList<Member, Board> _boards;
		private bool? _confirmed;
		private string _email;
		private string _fullName;
		private string _gravatarHash;
		private string _initials;
		private readonly ExpiringList<Member, InvitedBoard> _invitedBoards;
		private readonly ExpiringList<Member, InvitedOrganization> _invitedOrganizations;
		private List<string> _loginTypes;
		private string _memberType;
		private readonly ExpiringList<Member, Notification> _notifications;
		private readonly ExpiringList<Member, Organization> _organizations;
		private readonly ExpiringList<Member, PinnedBoard> _pinnedBoards;
		private readonly MemberPreferences _preferences;
		private readonly ExpiringList<Member, PremiumOrganization> _premiumOrganizations;
		private MemberStatusType _status;
		private List<string> _trophies;
		private string _uploadedAvatarHash;
		private string _url;
		private string _username;

		///<summary>
		/// Enumerates all actions associated with this member.
		///</summary>
		public IEnumerable<Action> Actions { get { return _actions; } }
		/// <summary>
		/// Gets the member's avatar hash.
		/// </summary>
		public string AvatarHash
		{
			get
			{
				VerifyNotExpired();
				return _avatarHash;
			}
		}
		/// <summary>
		/// Gets and sets the source URL for the member's avatar.
		/// </summary>
		public string AvatarSource
		{
			get
			{
				VerifyNotExpired();
				return _avatarSource;
			}
			set
			{
				Validate.Writable(Svc);
				if (_avatarSource == value) return;
				_avatarSource = value ?? string.Empty;
				Parameters.Add("avatarSource", _avatarSource);
				Put();
			}
		}
		/// <summary>
		/// Gets and sets the bio of the member.
		/// </summary>
		public string Bio
		{
			get
			{
				VerifyNotExpired();
				return _bio;
			}
			set
			{
				Validate.Writable(Svc);
				if (_bio == value) return;
				_bio = value ?? string.Empty;
				Parameters.Add("bio", _bio);
				Put();
			}
		}
		/// <summary>
		/// Enumerates the boards owned by the member.
		/// </summary>
		public IEnumerable<Board> Boards { get { return _boards; } }
		/// <summary>
		/// Gets whether the member is confirmed.
		/// </summary>
		public bool? Confirmed
		{
			get
			{
				VerifyNotExpired();
				return _confirmed;
			}
		}
		/// <summary>
		/// Gets the member's registered email address.
		/// </summary>
		public string Email
		{
			get
			{
				VerifyNotExpired();
				return _email;
			}
		}
		/// <summary>
		/// Gets the member's full name.
		/// </summary>
		public string FullName
		{
			get
			{
				VerifyNotExpired();
				return _fullName;
			}
			set
			{
				Validate.Writable(Svc);
				if (_fullName == value) return;
				_fullName = Validate.MinStringLength(value, 4, "FullName");
				Parameters.Add("fullName", _fullName);
				Put();
			}
		}
		/// <summary>
		/// Gets the member's Gravatar hash.
		/// </summary>
		public string GravatarHash
		{
			get
			{
				VerifyNotExpired();
				return _gravatarHash;
			}
		}
		/// <summary>
		/// Gets or sets the member's initials.
		/// </summary>
		public string Initials
		{
			get
			{
				VerifyNotExpired();
				return _initials;
			}
			set
			{
				Validate.Writable(Svc);
				if (_initials == value) return;
				_initials = Validate.StringLengthRange(value, 1, 3, "Initials");
				Parameters.Add("initials", _initials);
				Put();
			}
		}
		/// <summary>
		/// Enumerates the boards to which the member has been invited to join.
		/// </summary>
		public IEnumerable<Board> InvitedBoards { get { return _invitedBoards; } }
		/// <summary>
		/// Enumerates the organizations to which the member has been invited to join.
		/// </summary>
		public IEnumerable<Organization> InvitedOrganizations { get { return _invitedOrganizations; } }
		/// <summary>
		/// Enumerates the login types for the member.
		/// </summary>
		public IEnumerable<string> LoginTypes
		{
			get
			{
				VerifyNotExpired();
				return _loginTypes;
			}
		}
		/// <summary>
		/// Gets the type of member.
		/// </summary>
		public string MemberType
		{
			get
			{
				VerifyNotExpired();
				return _memberType;
			}
		}
		/// <summary>
		/// Enumerates the member's notifications.
		/// </summary>
		public IEnumerable<Notification> Notifications { get { return _notifications; } }
		/// <summary>
		/// Enumerates the organizations to which the member belongs.
		/// </summary>
		public IEnumerable<Organization> Organizations { get { return _organizations; } }
		/// <summary>
		/// Enumerates the boards the member has pinnned to their boards menu.
		/// </summary>
		public IEnumerable<Board> PinnedBoards { get { return _pinnedBoards; } }
		///<summary>
		/// Gets the set of preferences for the member.
		///</summary>
		public MemberPreferences Preferences { get { return _preferences; } }
		/// <summary>
		/// Enumerates the premium organizations to which the member belongs.
		/// </summary>
		public IEnumerable<Organization> PremiumOrganizations { get { return _premiumOrganizations; } }
		/// <summary>
		/// Gets the member's activity status.
		/// </summary>
		public MemberStatusType Status
		{
			get
			{
				VerifyNotExpired();
				return _status;
			}
		}
		/// <summary>
		/// Enumerates the trophies obtained by the member.
		/// </summary>
		public IEnumerable<string> Trophies
		{
			get
			{
				VerifyNotExpired();
				return _trophies;
			}
		}
		/// <summary>
		/// Gets the user's uploaded avatar hash.
		/// </summary>
		public string UploadedAvatarHash
		{
			get
			{
				VerifyNotExpired();
				return _uploadedAvatarHash;
			}
		}
		/// <summary>
		/// Gets the URL to the member's profile.
		/// </summary>
		public string Url { get { return _url; } }
		/// <summary>
		/// Gets or sets the member's username.
		/// </summary>
		public string Username
		{
			get
			{
				VerifyNotExpired();
				return _username;
			}
			set
			{
				Validate.Writable(Svc);
				if (_username == value) return;
				_username = Validate.UserName(Svc, value);
				Parameters.Add("username", _username);
				Put();
			}
		}

		internal override string Key { get { return "members"; } }

		static Member()
		{
			_statusMap = new OneToOneMap<MemberStatusType, string>
			           	{
			           		{MemberStatusType.Disconnected, "disconnected"},
			           		{MemberStatusType.Idle, "idle"},
			           		{MemberStatusType.Active, "active"},
			           	};
		}
		/// <summary>
		/// Creates a new instance of the Member class.
		/// </summary>
		public Member()
		{
			_actions = new ExpiringList<Member, Action>(this);
			_premiumOrganizations = new ExpiringList<Member, PremiumOrganization>(this);
			_boards = new ExpiringList<Member, Board>(this);
			_invitedBoards = new ExpiringList<Member, InvitedBoard>(this);
			_invitedOrganizations = new ExpiringList<Member, InvitedOrganization>(this);
			_notifications = new ExpiringList<Member, Notification>(this);
			_organizations = new ExpiringList<Member, Organization>(this);
			_pinnedBoards = new ExpiringList<Member, PinnedBoard>(this);
			_preferences = new MemberPreferences(null, this);
		}
		internal Member(ITrelloRest svc, string id)
			: base(svc, id)
		{
			_actions = new ExpiringList<Member, Action>(svc, this);
			_premiumOrganizations = new ExpiringList<Member, PremiumOrganization>(svc, this);
			_boards = new ExpiringList<Member, Board>(svc, this);
			_invitedBoards = new ExpiringList<Member, InvitedBoard>(svc, this);
			_invitedOrganizations = new ExpiringList<Member, InvitedOrganization>(svc, this);
			_notifications = new ExpiringList<Member, Notification>(svc, this);
			_organizations = new ExpiringList<Member, Organization>(svc, this);
			_pinnedBoards = new ExpiringList<Member, PinnedBoard>(svc, this);
			_preferences = new MemberPreferences(svc, this);
		}

		/// <summary>
		/// Marks all unread notifications for the member as read.
		/// </summary>
		public void ClearNotifications()
		{
			if (Svc == null) return;
			Validate.Writable(Svc);
			Svc.Post(Svc.RequestProvider.Create<Member>(new ExpiringObject[] { new Notification() }, urlExtension: "all/read"));
		}
		/// <summary>
		/// Creates a personal board for the current member.
		/// </summary>
		/// <param name="name">The name of the board.</param>
		/// <returns>The newly-created Board object.</returns>
		public Board CreateBoard(string name)
		{
			if (Svc == null) return null;
			Validate.Writable(Svc);
			Validate.NonEmptyString(name);
			Parameters.Add("name", name);
			return Svc.Post(Svc.RequestProvider.Create<Board>(new ExpiringObject[] {new Board()}));
		}
		/// <summary>
		/// Creates an organization administered by the current member.
		/// </summary>
		/// <param name="displayName">The display name of the organization.</param>
		/// <returns>The newly-created Organization object.</returns>
		public Organization CreateOrganization(string displayName)
		{
			if (Svc == null) return null;
			Validate.Writable(Svc);
			Validate.NonEmptyString(displayName);
			Parameters.Add("displayName", displayName);
			return Svc.Post(Svc.RequestProvider.Create<Organization>(new ExpiringObject[] {new Organization()}));
		}
		/// <summary>
		/// Adds a board to the member's boards menu.
		/// </summary>
		/// <param name="board">The board to pin.</param>
		public void PinBoard(Board board)
		{
			if (Svc == null) return;
			Validate.Writable(Svc);
			Validate.Entity(board);
			Parameters.Add("value", board.Id);
			Svc.Post(Svc.RequestProvider.Create<Member>(new ExpiringObject[] {this, new PinnedBoard()}, this));
		}
		/// <summary>
		/// Removes the member's vote from a card.
		/// </summary>
		/// <param name="card"></param>
		public void RescindVoteForCard(Card card)
		{
			if (Svc == null) return;
			Validate.Writable(Svc);
			Validate.Entity(card);
			Svc.Delete(Svc.RequestProvider.Create<Member>(new ExpiringObject[] { card, new VotingMember { Id = Id } }));
		}
		/// <summary>
		/// Removes a board from the member's boards menu.
		/// </summary>
		/// <param name="board"></param>
		public void UnpinBoard(Board board)
		{
			if (Svc == null) return;
			Validate.Writable(Svc);
			Validate.Entity(board);
			Svc.Delete(Svc.RequestProvider.Create<Member>(new ExpiringObject[] { this, new PinnedBoard { Id = board.Id } }));
		}
		/// <summary>
		/// Applies the member's vote to a card.
		/// </summary>
		/// <param name="card"></param>
		public void VoteForCard(Card card)
		{
			if (Svc == null) return;
			Validate.Writable(Svc);
			Validate.Entity(card);
			Parameters.Add("value", Id);
			Svc.Post(Svc.RequestProvider.Create<Member>(new ExpiringObject[] { card, new VotingMember() }, this));
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
			Id = obj.TryGetString("id");
			_avatarHash = obj.TryGetString("avatarHash");
			_avatarSource = obj.TryGetString("avatarSource");
			_bio = obj.TryGetString("bio");
			_confirmed = obj.TryGetBoolean("confirmed");
			_email = obj.TryGetString("email");
			_fullName = obj.TryGetString("fullName");
			_gravatarHash = obj.TryGetString("gravatarHash");
			_initials = obj.TryGetString("initials");
			var logins = obj.TryGetArray("loginTypes");
			if (logins != null)
				_loginTypes = logins.Select(v => v.Type == JsonValueType.Null ? null : v.String).ToList();
			_memberType = obj.TryGetString("memberType");
			_apiStatus = obj.TryGetString("status");
			var trophies = obj.TryGetArray("trophies");
			if (trophies != null)
				_trophies = trophies.Select(v => v.Type == JsonValueType.Null ? null : v.String).ToList();
			_uploadedAvatarHash = obj.TryGetString("uploadedAvatarHash");
			_url = obj.TryGetString("url");
			_username = obj.TryGetString("username");
			UpdateStatus();
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
			           		{"id", Id},
			           		{"avatarHash", _avatarHash},
			           		{"avatarSource", _avatarSource},
			           		{"bio", _bio},
			           		{"confirmed", _confirmed.HasValue ? _confirmed.Value : JsonValue.Null},
			           		{"email", _email},
			           		{"fullName", _fullName},
			           		{"gravatarHash", _gravatarHash},
			           		{"initials", _initials},
			           		{"loginTypes", _loginTypes.ToJson()},
			           		{"memberType", _memberType},
			           		{"status", _apiStatus},
			           		{"trophies", _trophies.ToJson()},
			           		{"uploadedAvatarHash", _uploadedAvatarHash},
			           		{"url", _url},
			           		{"username", _username}
			           	};
			return json;
		}
		/// <summary>
		/// Indicates whether the current object is equal to another object of the same type.
		/// </summary>
		/// <returns>
		/// true if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.
		/// </returns>
		/// <param name="other">An object to compare with this object.</param>
		public bool Equals(Member other)
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
			if (!(obj is Member)) return false;
			return Equals((Member) obj);
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
			return FullName;
		}

		internal override void Refresh(ExpiringObject entity)
		{
			var member = entity as Member;
			if (member == null) return;
			_avatarHash = member._avatarHash;
			_avatarSource = member._avatarSource;
			_bio = member._bio;
			_confirmed = member._confirmed;
			_email = member._email;
			_fullName = member._fullName;
			_gravatarHash = member._gravatarHash;
			_initials = member._initials;
			_loginTypes = member._loginTypes;
			_memberType = member._memberType;
			_apiStatus = member._apiStatus;
			_trophies = member._trophies;
			_uploadedAvatarHash = member._uploadedAvatarHash;
			_url = member._url;
			_username = member._username;
			UpdateStatus();
			_isInitialized = true;
		}

		/// <summary>
		/// Retrieves updated data from the service instance and refreshes the object.
		/// </summary>
		protected override void Get()
		{
			var entity = Svc.Get(Svc.RequestProvider.Create<Member>(Id));
			Refresh(entity);
		}
		/// <summary>
		/// Propigates the service instance to the object's owned objects.
		/// </summary>
		protected override void PropigateService()
		{
			_actions.Svc = Svc;
			_boards.Svc = Svc;
			_invitedBoards.Svc = Svc;
			_invitedOrganizations.Svc = Svc;
			_notifications.Svc = Svc;
			_organizations.Svc = Svc;
			_pinnedBoards.Svc = Svc;
			_preferences.Svc = Svc;
			_premiumOrganizations.Svc = Svc;
		}

		private void Put()
		{
			if (Svc == null)
			{
				Parameters.Clear();
				return;
			}
			var request = Svc.RequestProvider.Create<Member>(this);
			Svc.Put(request);
		}
		private void UpdateStatus()
		{
			_status = _statusMap.Any(kvp => kvp.Value == _apiStatus) ? _statusMap[_apiStatus] : MemberStatusType.Unknown;
		}
		private void UpdateApiStatus()
		{
			if (_statusMap.Any(kvp => kvp.Key == _status))
				_apiStatus = _statusMap[_status];
		}
	}
}

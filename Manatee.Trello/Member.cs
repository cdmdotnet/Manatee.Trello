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
using Manatee.Trello.Contracts;
using Manatee.Trello.Internal;
using Manatee.Trello.Internal.Json;
using Manatee.Trello.Json;

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
	public class Member : ExpiringObject, IEquatable<Member>
	{
		private static readonly OneToOneMap<MemberStatusType, string> _statusMap;
		private static readonly OneToOneMap<AvatarSourceType, string> _avatarSourceMap;

		private IJsonMember _jsonMember;
		private readonly ExpiringList<Action, IJsonAction> _actions;
		private AvatarSourceType _avatarSource;
		private readonly ExpiringList<Board, IJsonBoard> _boards;
		private readonly ExpiringList<BoardInvitation, IJsonBoard> _invitedBoards;
		private readonly ExpiringList<OrganizationInvitation, IJsonOrganization> _invitedOrganizations;
		private readonly ExpiringList<Notification, IJsonNotification> _notifications;
		private readonly ExpiringList<Organization, IJsonOrganization> _organizations;
		private readonly ExpiringList<PinnedBoard, IJsonBoard> _pinnedBoards;
		private readonly MemberPreferences _preferences;
		private readonly ExpiringList<PremiumOrganization, IJsonOrganization> _premiumOrganizations;
		private MemberStatusType _status;

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
				return (_jsonMember == null) ? null : _jsonMember.AvatarHash;
			}
		}
		/// <summary>
		/// Gets or sets the source URL for the member's avatar.
		/// </summary>
		public AvatarSourceType AvatarSource
		{
			get
			{
				VerifyNotExpired();
				return _avatarSource;
			}
			set
			{
				Validate.Writable(Svc);
				if (_jsonMember == null) return;
				if (_avatarSource == value) return;
				_avatarSource = value;
				UpdateApiAvatarSource();
				Parameters.Add("avatarSource", _jsonMember.AvatarSource);
				Put();
			}
		}
		/// <summary>
		/// Gets or sets the bio of the member.
		/// </summary>
		public string Bio
		{
			get
			{
				VerifyNotExpired();
				return (_jsonMember == null) ? null : _jsonMember.Bio;
			}
			set
			{
				Validate.Writable(Svc);
				if (_jsonMember == null) return;
				if (_jsonMember.Bio == value) return;
				_jsonMember.Bio = value ?? string.Empty;
				Parameters.Add("bio", _jsonMember.Bio);
				Put();
			}
		}
		/// <summary>
		/// Enumerates the boards owned by the member.
		/// </summary>
		public IEnumerable<Board> Boards { get { return _boards; } }
		internal ExpiringList<Board, IJsonBoard> BoardsList { get { return _boards; } }
		/// <summary>
		/// Gets whether the member is confirmed.
		/// </summary>
		public bool? Confirmed
		{
			get
			{
				VerifyNotExpired();
				return (_jsonMember == null) ? null : _jsonMember.Confirmed;
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
				return (_jsonMember == null) ? null : _jsonMember.Email;
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
				return (_jsonMember == null) ? null : _jsonMember.FullName;
			}
			set
			{
				Validate.Writable(Svc);
				if (_jsonMember == null) return;
				if (_jsonMember.FullName == value) return;
				_jsonMember.FullName = Validate.MinStringLength(value, 4, "FullName");
				Parameters.Add("fullName", _jsonMember.FullName);
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
				return (_jsonMember == null) ? null : _jsonMember.GravatarHash;
			}
		}
		/// <summary>
		/// Gets a unique identifier (not necessarily a GUID).
		/// </summary>
		public override string Id
		{
			get { return _jsonMember != null ? _jsonMember.Id : base.Id; }
			internal set
			{
				if (_jsonMember != null)
					_jsonMember.Id = value;
				base.Id = value;
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
				return (_jsonMember == null) ? null : _jsonMember.Initials;
			}
			set
			{
				Validate.Writable(Svc);
				if (_jsonMember == null) return;
				if (_jsonMember.Initials == value) return;
				_jsonMember.Initials = Validate.StringLengthRange(value, 1, 3, "Initials");
				Parameters.Add("initials", _jsonMember.Initials);
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
				return (_jsonMember == null) ? null : _jsonMember.LoginTypes;
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
				return (_jsonMember == null) ? null : _jsonMember.MemberType;
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
		internal ExpiringList<Organization, IJsonOrganization> OrganizationsList { get { return _organizations; } }
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
				return (_jsonMember == null) ? null : _jsonMember.Trophies;
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
				return (_jsonMember == null) ? null : _jsonMember.UploadedAvatarHash;
			}
		}
		/// <summary>
		/// Gets the URL to the member's profile.
		/// </summary>
		public string Url { get { return (_jsonMember == null) ? null : _jsonMember.Url; } }
		/// <summary>
		/// Gets or sets the member's username.
		/// </summary>
		public string Username
		{
			get
			{
				VerifyNotExpired();
				return (_jsonMember == null) ? null : _jsonMember.Username;
			}
			set
			{
				Validate.Writable(Svc);
				if (_jsonMember == null) return;
				if (_jsonMember.Username == value) return;
				_jsonMember.Username = Validate.UserName(Api, value);
				Parameters.Add("username", _jsonMember.Username);
				Put();
			}
		}

		internal static string TypeKey { get { return "members"; } }
		internal static string TypeKey2 { get { return "members"; } }
		internal override string Key { get { return TypeKey; } }
		internal override string Key2 { get { return TypeKey2; } }

		static Member()
		{
			_statusMap = new OneToOneMap<MemberStatusType, string>
			           	{
			           		{MemberStatusType.Disconnected, "disconnected"},
			           		{MemberStatusType.Idle, "idle"},
			           		{MemberStatusType.Active, "active"},
			           	};
			_avatarSourceMap = new OneToOneMap<AvatarSourceType, string>
			                   	{
			                   		{AvatarSourceType.None, "none"},
			                   		{AvatarSourceType.Upload, "upload"},
			                   		{AvatarSourceType.Gravatar, "gravatar"},
			                   	};
		}
		/// <summary>
		/// Creates a new instance of the Member class.
		/// </summary>
		public Member()
		{
			_jsonMember = new InnerJsonMember();
			_actions = new ExpiringList<Action, IJsonAction>(this, Action.TypeKey) {Fields = "id"};
			_avatarSource = AvatarSourceType.Unknown;
			_boards = new ExpiringList<Board, IJsonBoard>(this, Board.TypeKey) {Fields = "id"};
			_invitedBoards = new ExpiringList<BoardInvitation, IJsonBoard>(this, BoardInvitation.TypeKey) {Fields = "id"};
			_invitedOrganizations = new ExpiringList<OrganizationInvitation, IJsonOrganization>(this, OrganizationInvitation.TypeKey) {Fields = "id"};
			_notifications = new ExpiringList<Notification, IJsonNotification>(this, Notification.TypeKey) {Fields = "id"};
			_organizations = new ExpiringList<Organization, IJsonOrganization>(this, Organization.TypeKey) {Fields = "id"};
			_pinnedBoards = new ExpiringList<PinnedBoard, IJsonBoard>(this, PinnedBoard.TypeKey) {Fields = "id"};
			_preferences = new MemberPreferences(this);
			_premiumOrganizations = new ExpiringList<PremiumOrganization, IJsonOrganization>(this, PremiumOrganization.TypeKey) {Fields = "id"};
		}

		/// <summary>
		/// Marks all unread notifications for the member as read.
		/// </summary>
		public void ClearNotifications()
		{
			if (Svc == null) return;
			Validate.Writable(Svc);
			var endpoint = new Endpoint(new[] {Notification.TypeKey, "all", "read"});
			var request = Api.RequestProvider.Create(endpoint.ToString());
			Api.Post<IJsonMember>(request);
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
			var board = new Board();
			var endpoint = EndpointGenerator.Default.Generate(board);
			var request = Api.RequestProvider.Create(endpoint.ToString());
			request.AddParameter("name", name);
			board.ApplyJson(Api.Post<IJsonBoard>(request));
			board.Svc = Svc;
			_boards.MarkForUpdate();
			return board;
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
			var org = new Organization();
			var endpoint = EndpointGenerator.Default.Generate(org);
			var request = Api.RequestProvider.Create(endpoint.ToString());
			request.AddParameter("displayName", displayName);
			org.ApplyJson(Api.Post<IJsonOrganization>(request));
			org.Svc = Svc;
			_organizations.MarkForUpdate();
			return org;
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
			var endpoint = EndpointGenerator.Default.Generate(this, new PinnedBoard());
			var request = Api.RequestProvider.Create(endpoint.ToString());
			request.AddParameter("value", board.Id);
			Api.Post<IJsonMember>(request);
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
			var endpoint = EndpointGenerator.Default.Generate(card, new VotingMember {Id = Id});
			var request = Api.RequestProvider.Create(endpoint.ToString());
			Api.Delete<IJsonMember>(request);
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
			var endpoint = EndpointGenerator.Default.Generate(this, new PinnedBoard {Id = board.Id});
			var request = Api.RequestProvider.Create(endpoint.ToString());
			Api.Delete<IJsonMember>(request);
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
			var endpoint = EndpointGenerator.Default.Generate(card, new VotingMember());
			var request = Api.RequestProvider.Create(endpoint.ToString());
			request.AddParameter("value", Id);
			Api.Post<IJsonMember>(request);
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

		/// <summary>
		/// Retrieves updated data from the service instance and refreshes the object.
		/// </summary>
		protected override void Refresh()
		{
			var endpoint = EndpointGenerator.Default.Generate(this);
			var request = Api.RequestProvider.Create(endpoint.ToString());
			request.AddParameter("fields", "avatarHash,bio,fullName,initials,memberType,status,url,username,avatarSource,confirmed,email,gravatarHash,loginTypes,newEmail,oneTimeMessagesDismissed,status,trophies,uploadedAvatarHash");
			request.AddParameter("actions", "none");
			request.AddParameter("cards", "none");
			request.AddParameter("boards", "none");
			request.AddParameter("boardsInvited", "none");
			request.AddParameter("organizations", "none");
			request.AddParameter("organizationsInvited", "none");
			request.AddParameter("notifications", "none");
			request.AddParameter("tokens", "none");
			ApplyJson(Api.Get<IJsonMember>(request));
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

		internal override void ApplyJson(object obj)
		{
			_jsonMember = (IJsonMember) obj;
			UpdateStatus();
			UpdateAvatarSource();
		}
		internal override bool Matches(string id)
		{
			return (Id == id) || (Username == id);
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
			Api.Put<IJsonMember>(request);
			Parameters.Clear();
			_actions.MarkForUpdate();
		}
		private void UpdateStatus()
		{
			_status = _statusMap.Any(kvp => kvp.Value == _jsonMember.Status)
			          	? _statusMap[_jsonMember.Status]
			          	: MemberStatusType.Unknown;
		}
		private void UpdateAvatarSource()
		{
			_avatarSource = _avatarSourceMap.Any(kvp => kvp.Value == _jsonMember.AvatarSource)
			                	? _avatarSourceMap[_jsonMember.AvatarSource]
			                	: AvatarSourceType.Unknown;
		}
		private void UpdateApiAvatarSource()
		{
			if (_avatarSourceMap.Any(kvp => kvp.Key == _avatarSource))
				_jsonMember.AvatarSource = _avatarSourceMap[_avatarSource];
		}
	}
}

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
using Manatee.Trello.Rest;

namespace Manatee.Trello
{
	/// <summary>
	/// Represents a member (user).
	/// </summary>
	public class Member : ExpiringObject, IEquatable<Member>, IComparable<Member>
	{
		private static readonly OneToOneMap<MemberStatusType, string> _statusMap;
		private static readonly OneToOneMap<AvatarSourceType, string> _avatarSourceMap;

		private IJsonMember _jsonMember;
		private readonly ExpiringList<Action, IJsonAction> _actions;
		private AvatarSourceType _avatarSource;
		private readonly ExpiringList<Board, IJsonBoard> _boards;
		private readonly ExpiringList<Board, IJsonBoard> _closedBoards;
		private readonly ExpiringList<BoardInvitation, IJsonBoard> _invitedBoards;
		private readonly ExpiringList<OrganizationInvitation, IJsonOrganization> _invitedOrganizations;
		private readonly ExpiringList<Notification, IJsonNotification> _notifications;
		private readonly ExpiringList<Organization, IJsonOrganization> _organizations;
		private readonly ExpiringList<PinnedBoard, IJsonBoard> _pinnedBoards;
		private readonly MemberPreferences _preferences;
		private readonly ExpiringList<PremiumOrganization, IJsonOrganization> _premiumOrganizations;
		private readonly ExpiringList<MemberSession, IJsonMemberSession> _sessions;
		private MemberStatusType _status;
		private readonly ExpiringList<Token, IJsonToken> _tokens;

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
				Validator.Writable();
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
				Validator.Writable();
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
		/// Enumerates the closed boards owned by the member.
		/// </summary>
		public IEnumerable<Board> ClosedBoards { get { return _closedBoards; } }
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
				Validator.Writable();
				if (_jsonMember == null) return;
				if (_jsonMember.FullName == value) return;
				_jsonMember.FullName = Validator.MinStringLength(value, 4, "FullName");
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
				Validator.Writable();
				if (_jsonMember == null) return;
				if (_jsonMember.Initials == value) return;
				_jsonMember.Initials = Validator.StringLengthRange(value, 1, 3, "Initials");
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
		internal IEnumerable<MemberSession> Sessions { get { return _sessions; } }
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
		/// Enumerates the tokens provided by the member.
		/// </summary>
		public IEnumerable<Token> Tokens { get { return _tokens; } }
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
				Validator.Writable();
				if (_jsonMember == null) return;
				if (_jsonMember.Username == value) return;
				_jsonMember.Username = Validator.UserName(value);
				Parameters.Add("username", _jsonMember.Username);
				Put();
			}
		}

		internal static string TypeKey { get { return "members"; } }
		internal static string TypeKey2 { get { return "members"; } }
		internal override string PrimaryKey { get { return TypeKey; } }
		internal override string SecondaryKey { get { return TypeKey2; } }

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
			_boards = new ExpiringList<Board, IJsonBoard>(this, Board.TypeKey) {Fields = "id", Filter = "open"};
			_closedBoards = new ExpiringList<Board, IJsonBoard>(this, Board.TypeKey) {Fields = "id", Filter = "closed"};
			_invitedBoards = new ExpiringList<BoardInvitation, IJsonBoard>(this, BoardInvitation.TypeKey) {Fields = "id"};
			_invitedOrganizations = new ExpiringList<OrganizationInvitation, IJsonOrganization>(this, OrganizationInvitation.TypeKey) {Fields = "id"};
			_notifications = new ExpiringList<Notification, IJsonNotification>(this, Notification.TypeKey) {Fields = "id"};
			_organizations = new ExpiringList<Organization, IJsonOrganization>(this, Organization.TypeKey) {Fields = "id"};
			_pinnedBoards = new ExpiringList<PinnedBoard, IJsonBoard>(this, PinnedBoard.TypeKey) {Fields = "id"};
			_preferences = new MemberPreferences(this);
			_premiumOrganizations = new ExpiringList<PremiumOrganization, IJsonOrganization>(this, PremiumOrganization.TypeKey) {Fields = "id"};
			_sessions = new ExpiringList<MemberSession, IJsonMemberSession>(this, MemberSession.TypeKey);
			_tokens = new ExpiringList<Token, IJsonToken>(this, Token.TypeKey) {Fields = "id"};
		}

		/// <summary>
		/// Marks all unread notifications for the member as read.
		/// </summary>
		public void ClearNotifications()
		{
			if (Svc == null) return;
			Validator.Writable();
			var endpoint = new Endpoint(new[] {Notification.TypeKey, "all", "read"});
			JsonRepository.Post<IJsonMember>(endpoint.ToString());
		}
		/// <summary>
		/// Creates a personal board for the current member.
		/// </summary>
		/// <param name="name">The name of the board.</param>
		/// <returns>The newly-created Board object.</returns>
		public Board CreateBoard(string name)
		{
			if (Svc == null) return null;
			Validator.Writable();
			Validator.NonEmptyString(name);
			var board = new Board();
			var endpoint = EndpointGenerator.Default.Generate(board);
			Parameters.Add("name", name);
			board.ApplyJson(JsonRepository.Post<IJsonBoard>(endpoint.ToString(), Parameters));
			Parameters.Clear();
			UpdateService(board);
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
			Validator.Writable();
			Validator.NonEmptyString(displayName);
			var org = new Organization();
			var endpoint = EndpointGenerator.Default.Generate(org);
			org.ApplyJson(JsonRepository.Post<IJsonOrganization>(endpoint.ToString(), Parameters));
			Parameters.Clear();
			UpdateService(org);
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
			Validator.Writable();
			Validator.Entity(board);
			var endpoint = EndpointGenerator.Default.Generate(this, new PinnedBoard());
			Parameters.Add("value", board.Id);
			JsonRepository.Post<IJsonMember>(endpoint.ToString(), Parameters);
			Parameters.Clear();
		}
		/// <summary>
		/// Removes the member's vote from a card.
		/// </summary>
		/// <param name="card"></param>
		public void RescindVoteForCard(Card card)
		{
			if (Svc == null) return;
			Validator.Writable();
			Validator.Entity(card);
			var endpoint = EndpointGenerator.Default.Generate(card, new VotingMember {Id = Id});
			JsonRepository.Delete<IJsonMember>(endpoint.ToString());
		}
		/// <summary>
		/// Removes a board from the member's boards menu.
		/// </summary>
		/// <param name="board"></param>
		public void UnpinBoard(Board board)
		{
			if (Svc == null) return;
			Validator.Writable();
			Validator.Entity(board);
			var endpoint = EndpointGenerator.Default.Generate(this, new PinnedBoard {Id = board.Id});
			JsonRepository.Delete<IJsonMember>(endpoint.ToString());
		}
		/// <summary>
		/// Applies the member's vote to a card.
		/// </summary>
		/// <param name="card"></param>
		public void VoteForCard(Card card)
		{
			if (Svc == null) return;
			Validator.Writable();
			Validator.Entity(card);
			var endpoint = EndpointGenerator.Default.GenerateForList<VotingMember>(card);
			Parameters.Add("value", Id);
			JsonRepository.Post<IJsonMember>(endpoint.ToString(), Parameters);
			Parameters.Clear();
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
		/// Compares the current object with another object of the same type.
		/// </summary>
		/// <returns>
		/// A value that indicates the relative order of the objects being compared. The return value has the following meanings: Value Meaning Less than zero This object is less than the <paramref name="other"/> parameter.Zero This object is equal to <paramref name="other"/>. Greater than zero This object is greater than <paramref name="other"/>. 
		/// </returns>
		/// <param name="other">An object to compare with this object.</param>
		public int CompareTo(Member other)
		{
			var order = string.Compare(FullName, other.FullName);
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
			return FullName;
		}
		/// <summary>
		/// Retrieves updated data from the service instance and refreshes the object.
		/// </summary>
		public override bool Refresh()
		{
			var endpoint = EndpointGenerator.Default.Generate(this);
			Parameters.Add("fields", "avatarHash,bio,fullName,initials,memberType,status,url,username,avatarSource,confirmed,email,gravatarHash,loginTypes,newEmail,oneTimeMessagesDismissed,status,trophies,uploadedAvatarHash");
			Parameters.Add("actions", "none");
			Parameters.Add("cards", "none");
			Parameters.Add("boards", "none");
			Parameters.Add("boardsInvited", "none");
			Parameters.Add("organizations", "none");
			Parameters.Add("organizationsInvited", "none");
			Parameters.Add("notifications", "none");
			Parameters.Add("tokens", "none");
			var obj = JsonRepository.Get<IJsonMember>(endpoint.ToString(), Parameters);
			Parameters.Clear();
			if (obj == null) return false;
			ApplyJson(obj);
			return true;
		}

		/// <summary>
		/// Propagates the service instance to the object's owned objects.
		/// </summary>
		protected override void PropagateService()
		{
			UpdateService(_actions);
			UpdateService(_boards);
			UpdateService(_closedBoards);
			UpdateService(_invitedBoards);
			UpdateService(_invitedOrganizations);
			UpdateService(_notifications);
			UpdateService(_organizations);
			UpdateService(_pinnedBoards);
			UpdateService(_preferences);
			UpdateService(_premiumOrganizations);
			UpdateService(_sessions);
			UpdateService(_tokens);
		}

		internal override void ApplyJson(object obj)
		{
			if (obj is IRestResponse)
				_jsonMember = ((IRestResponse<IJsonMember>)obj).Data;
			else
				_jsonMember = (IJsonMember)obj;
			UpdateStatus();
			UpdateAvatarSource();
		}
		internal override bool Matches(string id)
		{
			return (Id == id) || (Username == id);
		}

		private void Put()
		{
			var endpoint = EndpointGenerator.Default.Generate(this);
			JsonRepository.Put<IJsonMember>(endpoint.ToString(), Parameters);
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

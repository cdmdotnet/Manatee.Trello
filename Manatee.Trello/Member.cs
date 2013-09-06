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
	/// <summary>
	/// Represents a member (user).
	/// </summary>
	public class Member : ExpiringObject, IEquatable<Member>, IComparable<Member>
	{
		private static readonly OneToOneMap<MemberStatusType, string> _statusMap;
		private static readonly OneToOneMap<AvatarSourceType, string> _avatarSourceMap;

		private IJsonMember _jsonMember;
		private readonly ExpiringList<Action> _actions;
		private AvatarSourceType _avatarSource;
		private readonly ExpiringList<Board> _boards;
		private readonly ExpiringList<Board> _closedBoards;
		private readonly ExpiringList<Board> _invitedBoards;
		private readonly ExpiringList<Organization> _invitedOrganizations;
		private readonly ExpiringList<Notification> _notifications;
		private readonly ExpiringList<Organization> _organizations;
		//private readonly ExpiringList<Board> _pinnedBoards;
		private readonly MemberPreferences _preferences;
		private readonly ExpiringList<MemberSession> _sessions;
		private MemberStatusType _status;
		private readonly ExpiringList<Token> _tokens;

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
				Put(EntityRequestType.Member_Write_AvatarSource);
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
				Put(EntityRequestType.Member_Write_Bio);
			}
		}
		/// <summary>
		/// Enumerates the boards owned by the member.
		/// </summary>
		public IEnumerable<Board> Boards { get { return _boards; } }
		internal ExpiringList<Board> BoardsList { get { return _boards; } }
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
				Put(EntityRequestType.Member_Write_FullName);
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
				Put(EntityRequestType.Member_Write_Initials);
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
		internal ExpiringList<Organization> OrganizationsList { get { return _organizations; } }
		// / <summary>
		// / Enumerates the boards the member has pinnned to their boards menu.
		// / </summary>
		//internal IEnumerable<Board> PinnedBoards { get { return _pinnedBoards; } }
		///<summary>
		/// Gets the set of preferences for the member.
		///</summary>
		public MemberPreferences Preferences { get { return _preferences; } }
		/// <summary>
		/// Enumerates the active sessions with trello.com.
		/// </summary>
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
				Put(EntityRequestType.Member_Write_Username);
			}
		}
		/// <summary>
		/// Gets whether this entity represents an actual entity on Trello.
		/// </summary>
		public override bool IsStubbed { get { return _jsonMember is InnerJsonMember; } }

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
			_actions = new ExpiringList<Action>(this, EntityRequestType.Member_Read_Actions);
			_avatarSource = AvatarSourceType.Unknown;
			_boards = new ExpiringList<Board>(this, EntityRequestType.Member_Read_Boards) {Filter = "open"};
			_closedBoards = new ExpiringList<Board>(this, EntityRequestType.Member_Read_Boards) {Filter = "closed"};
			_invitedBoards = new ExpiringList<Board>(this, EntityRequestType.Member_Read_InvitedBoards);
			_invitedOrganizations = new ExpiringList<Organization>(this, EntityRequestType.Member_Read_InvitedOrganizations);
			_notifications = new ExpiringList<Notification>(this, EntityRequestType.Member_Read_Notifications);
			_organizations = new ExpiringList<Organization>(this, EntityRequestType.Member_Read_Organization);
			//_pinnedBoards = new ExpiringList<Board>(this, EntityRequestType.Member_Read_PinnedBoards);
			_preferences = new MemberPreferences(this);
			_sessions = new ExpiringList<MemberSession>(this, EntityRequestType.Member_Read_Sessions);
			_tokens = new ExpiringList<Token>(this, EntityRequestType.Member_Read_Tokens);
		}

		/// <summary>
		/// Marks all unread notifications for the member as read.
		/// </summary>
		public void ClearNotifications()
		{
			Validator.Writable();
			EntityRepository.Upload(EntityRequestType.Member_Write_ClearNotifications, Parameters);
		}
		/// <summary>
		/// Creates a personal board for the current member.
		/// </summary>
		/// <param name="name">The name of the board.</param>
		/// <returns>The newly-created Board object.</returns>
		public Board CreateBoard(string name)
		{
			Validator.Writable();
			Validator.NonEmptyString(name);
			Parameters.Add("name", name);
			var board = EntityRepository.Download<Board>(EntityRequestType.Member_Write_CreateBoard, Parameters);
			UpdateDependencies(board);
			_boards.Add(board);
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
			Validator.Writable();
			Validator.NonEmptyString(displayName);
			Parameters.Add("displayName", displayName);
			var org = EntityRepository.Download<Organization>(EntityRequestType.Member_Write_CreateOrganizations, Parameters);
			UpdateDependencies(org);
			_organizations.Add(org);
			_organizations.MarkForUpdate();
			return org;
		}
		/// <summary>
		/// Adds a board to the member's boards menu.
		/// </summary>
		/// <param name="board">The board to pin.</param>
		public void PinBoard(Board board)
		{
			Validator.Writable();
			Validator.Entity(board);
			Parameters["_id"] = Id;
			Parameters.Add("value", board.Id);
			EntityRepository.Upload(EntityRequestType.Member_Write_PinBoard, Parameters);
		}
		/// <summary>
		/// Removes the member's vote from a card.
		/// </summary>
		/// <param name="card"></param>
		public void RescindVoteForCard(Card card)
		{
			Validator.Writable();
			Validator.Entity(card);
			Parameters.Add("_cardId", card.Id);
			Parameters["_id"] = Id;
			EntityRepository.Upload(EntityRequestType.Member_Write_RescindVoteForCard, Parameters);
		}
		/// <summary>
		/// Removes a board from the member's boards menu.
		/// </summary>
		/// <param name="board"></param>
		public void UnpinBoard(Board board)
		{
			Validator.Writable();
			Validator.Entity(board);
			Parameters["_id"] = Id;
			Parameters.Add("_boardId", board.Id);
			EntityRepository.Upload(EntityRequestType.Member_Write_UnpinBoard, Parameters);
		}
		/// <summary>
		/// Applies the member's vote to a card.
		/// </summary>
		/// <param name="card"></param>
		public void VoteForCard(Card card)
		{
			Validator.Writable();
			Validator.Entity(card);
			Parameters.Add("_cardId", card.Id);
			Parameters["_id"] = Id;
			EntityRepository.Upload(EntityRequestType.Member_Write_VoteForCard, Parameters);
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
			Parameters["_id"] = Id;
			AddDefaultParameters();
			return EntityRepository.Refresh(this, EntityRequestType.Member_Read_Refresh);
		}

		internal override void ApplyJson(object obj)
		{
			_jsonMember = (IJsonMember)obj;
			UpdateStatus();
			UpdateAvatarSource();
			Expires = DateTime.Now + EntityRepository.EntityDuration;
		}
		internal override bool Matches(string id)
		{
			return (Id == id) || (Username == id);
		}
		internal override void PropagateDependencies()
		{
			UpdateDependencies(_actions);
			UpdateDependencies(_boards);
			UpdateDependencies(_closedBoards);
			UpdateDependencies(_invitedBoards);
			UpdateDependencies(_invitedOrganizations);
			UpdateDependencies(_notifications);
			UpdateDependencies(_organizations);
			UpdateDependencies(_preferences);
			UpdateDependencies(_sessions);
			UpdateDependencies(_tokens);
		}

		private void Put(EntityRequestType requestType)
		{
			Parameters["_id"] = Id;
			EntityRepository.Upload(requestType, Parameters);
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

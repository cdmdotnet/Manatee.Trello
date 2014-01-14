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
	public class Member : ExpiringObject, IEquatable<Member>, IComparable<Member>, ICanWebhook
	{
		private static readonly OneToOneMap<MemberStatusType, string> _statusMap;
		private static readonly OneToOneMap<AvatarSourceType, string> _avatarSourceMap;

		private IJsonMember _jsonMember;
		private AvatarSourceType _avatarSource;
		private MemberStatusType _status;

		///<summary>
		/// Enumerates all actions associated with this member.
		///</summary>
		public IEnumerable<Action> Actions { get { return BuildList<Action>(EntityRequestType.Member_Read_Actions); } }
		/// <summary>
		/// Gets the member's avatar hash.
		/// </summary>
		public string AvatarHash
		{
			get
			{
				VerifyNotExpired();
				return _jsonMember.AvatarHash;
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
				Validator.Enumeration(value);
				if (_avatarSource == value) return;
				_avatarSource = value;
				UpdateApiAvatarSource();
				Parameters.Add("avatarSource", _jsonMember.AvatarSource);
				Upload(EntityRequestType.Member_Write_AvatarSource);
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
				return _jsonMember.Bio;
			}
			protected set
			{
				Validator.Writable();
				if (_jsonMember.Bio == value) return;
				_jsonMember.Bio = value;
				Parameters.Add("bio", _jsonMember.Bio ?? string.Empty);
				Upload(EntityRequestType.Member_Write_Bio);
			}
		}
		/// <summary>
		/// Enumerates the boards owned by the member.
		/// </summary>
		public IEnumerable<Board> Boards { get { return BuildList<Board>(EntityRequestType.Member_Read_Boards); } }
		/// <summary>
		/// Gets whether the member is confirmed.
		/// </summary>
		public bool? Confirmed
		{
			get
			{
				VerifyNotExpired();
				return _jsonMember.Confirmed;
			}
		}
		/// <summary>
		/// Gets the member's registered email address.
		/// </summary>
		protected string Email
		{
			get
			{
				VerifyNotExpired();
				return _jsonMember.Email;
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
				return _jsonMember.FullName;
			}
			protected set
			{
				Validator.Writable();
				if (_jsonMember.FullName == value) return;
				_jsonMember.FullName = Validator.MinStringLength(value, 4, "FullName");
				Parameters.Add("fullName", _jsonMember.FullName);
				Upload(EntityRequestType.Member_Write_FullName);
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
				return _jsonMember.GravatarHash;
			}
		}
		/// <summary>
		/// Gets a unique identifier (not necessarily a GUID).
		/// </summary>
		public override string Id
		{
			get { return _jsonMember.Id; }
			internal set { _jsonMember.Id = value; }
		}
		/// <summary>
		/// Gets or sets the member's initials.
		/// </summary>
		public string Initials
		{
			get
			{
				VerifyNotExpired();
				return _jsonMember.Initials;
			}
			protected set
			{
				Validator.Writable();
				if (_jsonMember.Initials == value) return;
				_jsonMember.Initials = Validator.StringLengthRange(value, 1, 3, "Initials");
				Parameters.Add("initials", _jsonMember.Initials);
				Upload(EntityRequestType.Member_Write_Initials);
			}
		}
		/// <summary>
		/// Enumerates the boards to which the member has been invited to join.
		/// </summary>
		public IEnumerable<Board> InvitedBoards { get { return BuildList<Board>(EntityRequestType.Member_Read_InvitedBoards); } }
		/// <summary>
		/// Enumerates the organizations to which the member has been invited to join.
		/// </summary>
		public IEnumerable<Organization> InvitedOrganizations { get { return BuildList<Organization>(EntityRequestType.Member_Read_InvitedOrganizations); } }
		/// <summary>
		/// Enumerates the login types for the member.
		/// </summary>
		public IEnumerable<string> LoginTypes
		{
			get
			{
				VerifyNotExpired();
				return _jsonMember.LoginTypes;
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
				return _jsonMember.MemberType;
			}
		}
		/// <summary>
		/// Enumerates the organizations to which the member belongs.
		/// </summary>
		public IEnumerable<Organization> Organizations { get { return BuildList<Organization>(EntityRequestType.Member_Read_Organizations); } }
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
				return _jsonMember.Trophies;
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
				return _jsonMember.UploadedAvatarHash;
			}
		}
		/// <summary>
		/// Gets the URL to the member's profile.
		/// </summary>
		public string Url { get { return _jsonMember.Url; } }
		/// <summary>
		/// Gets or sets the member's username.
		/// </summary>
		public string Username
		{
			get
			{
				VerifyNotExpired();
				return _jsonMember.Username;
			}
			protected set
			{
				Validator.Writable();
				if (_jsonMember.Username == value) return;
				_jsonMember.Username = Validator.UserName(value);
				Parameters.Add("username", _jsonMember.Username);
				Upload(EntityRequestType.Member_Write_Username);
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
			_avatarSource = AvatarSourceType.Unknown;
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
			return Id.GetHashCode();
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
			var order = string.Compare(FullName, other.FullName, StringComparison.InvariantCulture);
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
		/// <summary>
		/// Applies the changes an action represents.
		/// </summary>
		/// <param name="action">The action.</param>
		void ICanWebhook.ApplyAction(Action action)
		{
			if (action.Type != ActionType.UpdateMember) return;
			MergeJson(action.Data);
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

		private void Upload(EntityRequestType requestType)
		{
			Parameters["_id"] = Id;
			EntityRepository.Upload(requestType, Parameters);
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
		private void MergeJson(IJsonActionData data)
		{
			_jsonMember.AvatarHash = data.TryGetString("member", "avatarHash") ?? _jsonMember.AvatarHash;
			_jsonMember.Bio = data.TryGetString("member", "bio") ?? _jsonMember.Bio;
			_jsonMember.FullName = data.TryGetString("member", "fullName") ?? _jsonMember.FullName;
			_jsonMember.Initials = data.TryGetString("member", "initials") ?? _jsonMember.Initials;
			_jsonMember.MemberType = data.TryGetString("member", "memberType") ?? _jsonMember.MemberType;
			_jsonMember.Status = data.TryGetString("member", "status") ?? _jsonMember.Status;
			_jsonMember.Url = data.TryGetString("member", "url") ?? _jsonMember.Url;
			_jsonMember.Username = data.TryGetString("member", "username") ?? _jsonMember.Username;
			_jsonMember.AvatarSource = data.TryGetString("member", "avatarSource") ?? _jsonMember.AvatarSource;
			_jsonMember.Confirmed = data.TryGetBoolean("member", "confirmed") ?? _jsonMember.Confirmed;
			_jsonMember.Email = data.TryGetString("member", "email") ?? _jsonMember.Email;
			_jsonMember.GravatarHash = data.TryGetString("member", "gravatarHash") ?? _jsonMember.GravatarHash;
			_jsonMember.UploadedAvatarHash = data.TryGetString("member", "uploadedAvatarHash") ?? _jsonMember.UploadedAvatarHash;
		}
	}
}

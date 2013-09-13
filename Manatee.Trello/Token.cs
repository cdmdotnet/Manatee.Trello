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
 
	File Name:		Token.cs
	Namespace:		Manatee.Trello
	Class Name:		Token
	Purpose:		Exposes details for a user token on Trello.com.

***************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using Manatee.Trello.Contracts;
using Manatee.Trello.Internal.Json;
using Manatee.Trello.Json;

namespace Manatee.Trello
{
	/// <summary>
	/// Exposes details for a user token on Trello.com.
	/// </summary>
	public class Token : ExpiringObject, IEquatable<Token>, IComparable<Token>
	{
		private IJsonToken _jsonToken;
		private IJsonTokenPermission _jsonBoardPermissions;
		private IJsonTokenPermission _jsonMemberPermissions;
		private IJsonTokenPermission _jsonOrganizationPermissions;
		private TokenPermission<Board> _boardPermissions;
		private Member _member;
		private TokenPermission<Member> _memberPermissions;
		private TokenPermission<Organization> _organizationPermissions;
		private string _value;
		private bool _isDeleted;

		/// <summary>
		/// Gets the scope of permissions granted to boards.
		/// </summary>
		public TokenPermission<Board> BoardPermissions
		{
			get
			{
				if (_isDeleted) return null;
				VerifyNotExpired();
				if (_jsonToken == null) return null;
				if (_jsonBoardPermissions == null) return null;
				if (_jsonBoardPermissions.IdModel == null) return null;
				return (_boardPermissions == null) || (_boardPermissions.Scope.Model.Id != _jsonToken.IdMember)
						   ? (_boardPermissions = new TokenPermission<Board>(_jsonBoardPermissions, Download<Board>(_jsonBoardPermissions.IdModel)))
						   : _boardPermissions;
			}
		}
		/// <summary>
		/// Gets the date a token was created.
		/// </summary>
		public DateTime? DateCreated
		{
			get
			{
				if (_isDeleted) return null;
				VerifyNotExpired();
				if (_jsonToken == null) return null;
				return _jsonToken.DateCreated;
			}
		}
		/// <summary>
		/// Gets the date, if any, a token expires.
		/// </summary>
		public DateTime? DateExpires
		{
			get
			{
				if (_isDeleted) return null;
				VerifyNotExpired();
				if (_jsonToken == null) return null;
				return _jsonToken.DateExpires;
			}
		}
		/// <summary>
		/// Gets a unique identifier (not necessarily a GUID).
		/// </summary>
		public override string Id
		{
			get { return _jsonToken != null ? _jsonToken.Id : base.Id; }
			internal set
			{
				if (_jsonToken != null)
					_jsonToken.Id = value;
				base.Id = value;
			}
		}
		/// <summary>
		/// Gets the application which requested a token.
		/// </summary>
		public string Identifier
		{
			get
			{
				if (_isDeleted) return null;
				VerifyNotExpired();
				if (_jsonToken == null) return null;
				return _jsonToken.Identifier;
			}
		}
		/// <summary>
		/// Gets the member who issued the token.
		/// </summary>
		public Member Member
		{
			get
			{
				if (_isDeleted) return null;
				VerifyNotExpired();
				if (_jsonToken == null) return null;
				if (_jsonToken.IdMember == null) return null; 
				return UpdateById(ref _member, EntityRequestType.Member_Read_Refresh, _jsonToken.IdMember);
			}
		}
		/// <summary>
		/// Gets the scope of permissions granted to members.
		/// </summary>
		public TokenPermission<Member> MemberPermissions
		{
			get
			{
				if (_isDeleted) return null;
				VerifyNotExpired();
				if (_jsonToken == null) return null;
				if (_jsonMemberPermissions == null) return null;
				if (_jsonMemberPermissions.IdModel == null) return null;
				return (_memberPermissions == null) || (_memberPermissions.Scope.Model.Id != _jsonToken.IdMember)
					   ? (_memberPermissions = new TokenPermission<Member>(_jsonMemberPermissions, Download<Member>(_jsonMemberPermissions.IdModel)))
					   : _memberPermissions;
			}
		}
		/// <summary>
		/// Gets the scope of permissions granted to organizations.
		/// </summary>
		public TokenPermission<Organization> OrganizationPermissions
		{
			get
			{
				if (_isDeleted) return null;
				VerifyNotExpired();
				if (_jsonToken == null) return null;
				if (_jsonOrganizationPermissions == null) return null;
				if (_jsonOrganizationPermissions.IdModel == null) return null;
				return (_organizationPermissions == null) || (_organizationPermissions.Scope.Model.Id != _jsonToken.IdMember)
				       	? (_organizationPermissions = new TokenPermission<Organization>(_jsonOrganizationPermissions, Download<Organization>(_jsonOrganizationPermissions.IdModel)))
				       	: _organizationPermissions;
			}
		}
		/// <summary>
		/// Gets the token value.
		/// </summary>
		public string Value { get { return _value; } internal set { _value = value; } }
		/// <summary>
		/// Gets whether this entity represents an actual entity on Trello.
		/// </summary>
		public override bool IsStubbed { get { return _jsonToken is InnerJsonToken; } }

		/// <summary>
		/// Creates a new instance of the Token class.
		/// </summary>
		public Token()
		{
			_jsonToken = new InnerJsonToken();
		}
		internal Token(string value)
			: this()
		{
			_value = value;
		}

		/// <summary>
		/// Delete the token.  This cannot be undone.
		/// </summary>
		internal void Delete()
		{
			if (_isDeleted) return;
			Validator.Writable();
			Parameters.Add("_token", Value);
			EntityRepository.Upload(EntityRequestType.Token_Write_Delete, Parameters);
			_isDeleted = true;
		}
		/// <summary>
		/// Compares the current object with another object of the same type.
		/// </summary>
		/// <returns>
		/// A value that indicates the relative order of the objects being compared. The return value has the following meanings: Value Meaning Less than zero This object is less than the <paramref name="other"/> parameter.Zero This object is equal to <paramref name="other"/>. Greater than zero This object is greater than <paramref name="other"/>. 
		/// </returns>
		/// <param name="other">An object to compare with this object.</param>
		public int CompareTo(Token other)
		{
			var diff = DateCreated - other.DateCreated;
			return diff.HasValue ? (int)diff.Value.TotalMilliseconds : 0;
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
			return string.Format("Token issued by {0} for use by {1}. {2}", Member == null ? "someone" : Member.FullName, Identifier, GetExpirationDateString());
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
			if (!(obj is Token)) return false;
			return Equals((Token) obj);
		}
		/// <summary>
		/// Indicates whether the current object is equal to another object of the same type.
		/// </summary>
		/// <returns>
		/// true if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.
		/// </returns>
		/// <param name="other">An object to compare with this object.</param>
		public bool Equals(Token other)
		{
			return (Id == other.Id) || (Value != other.Value);
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
		/// Retrieves updated data from the service instance and refreshes the object.
		/// </summary>
		public override bool Refresh()
		{
			if (_value == null) return false;
			Parameters.Add("_token", Value);
			AddDefaultParameters();
			return EntityRepository.Refresh(this, EntityRequestType.Token_Read_Refresh);
		}

		internal override void ApplyJson(object obj)
		{
			_jsonToken = (IJsonToken)obj;
			_jsonBoardPermissions = _jsonToken.Permissions.SingleOrDefault(p => p.ModelType == "Board");
			_jsonMemberPermissions = _jsonToken.Permissions.SingleOrDefault(p => p.ModelType == "Member");
			_jsonOrganizationPermissions = _jsonToken.Permissions.SingleOrDefault(p => p.ModelType == "Organization");
			Expires = DateTime.Now + EntityRepository.EntityDuration;
		}

		private string GetExpirationDateString()
		{
			return DateExpires.HasValue ? string.Format("Expires {0}.", DateExpires.Value) : "Never expires.";
		}
		private T Download<T>(string id)
			where T : ExpiringObject
		{
			return EntityRepository.Download<T>(EntityRequestType.Action_Read_Refresh,
												new Dictionary<string, object> { { "_id", id } });
		}
	}
}
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
using System.Linq;
using Manatee.Trello.Contracts;
using Manatee.Trello.Internal;
using Manatee.Trello.Internal.Json;
using Manatee.Trello.Json;

namespace Manatee.Trello
{
	/// <summary>
	/// Exposes details for a user token on Trello.com.
	/// </summary>
	public class Token : ExpiringObject
	{
		private IJsonToken _jsonToken;
		private TokenPermission<Board> _boardPermissions;
		private Member _member;
		private TokenPermission<Member> _memberPermissions;
		private TokenPermission<Organization> _organizationPermissions;
		private readonly string _value;

		/// <summary>
		/// Gets the scope of permissions granted to boards.
		/// </summary>
		public TokenPermission<Board> BoardPermissions
		{
			get
			{
				VerifyNotExpired();
				return _boardPermissions;
			}
		}
		/// <summary>
		/// Gets the date a token was created.
		/// </summary>
		public DateTime? DateCreated
		{
			get
			{
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
				VerifyNotExpired();
				if (_jsonToken == null) return null; 
				return ((_member == null) || (_member.Id != _jsonToken.IdMember)) && (Svc != null)
				       	? (_member = Svc.Retrieve<Member>(_jsonToken.IdMember))
				       	: _member;
			}
		}
		/// <summary>
		/// Gets the scope of permissions granted to members.
		/// </summary>
		public TokenPermission<Member> MemberPermissions
		{
			get
			{
				VerifyNotExpired();
				return _memberPermissions;
			}
		}
		/// <summary>
		/// Gets the scope of permissions granted to organizations.
		/// </summary>
		public TokenPermission<Organization> OrganizationPermissions
		{
			get
			{
				VerifyNotExpired();
				return _organizationPermissions;
			}
		}
		/// <summary>
		/// Gets the token value.
		/// </summary>
		public string Value { get { return _value; } }

		internal static string TypeKey { get { return "tokens"; } }
		internal override string Key { get { return TypeKey; } }
		internal override string KeyId { get { return Value; } }
		/// <summary>
		/// Gets whether the entity is a cacheable item.
		/// </summary>
		protected override bool Cacheable { get { return true; } }

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
		public void Delete()
		{
			if (Svc == null) return;
			Validate.Writable(Svc);
			var endpoint = EndpointGenerator.Default.Generate(this);
			var request = Api.RequestProvider.Create(endpoint.ToString());
			Api.Delete<IJsonToken>(request);
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
			return string.Format("Token issued by {0} for use by {1}. {2}", Member.FullName, Identifier, GetExpirationDateString());
		}

		/// <summary>
		/// Retrieves updated data from the service instance and refreshes the object.
		/// </summary>
		protected override void Refresh()
		{
			var endpoint = EndpointGenerator.Default.Generate(this);
			var request = Api.RequestProvider.Create(endpoint.ToString());
			ApplyJson(Api.Get<IJsonToken>(request));
		}
		/// <summary>
		/// Propigates the service instance to the object's owned objects.
		/// </summary>
		protected override void PropigateService()
		{
			if (_member != null) _member.Svc = Svc;
			if (_boardPermissions != null) _boardPermissions.Scope.Model.Svc = Svc;
			if (_memberPermissions != null) _memberPermissions.Scope.Model.Svc = Svc;
			if (_organizationPermissions != null) _organizationPermissions.Scope.Model.Svc = Svc;
		}

		internal override void ApplyJson(object obj)
		{
			if (obj == null) return;
			_jsonToken = (IJsonToken) obj;
			var boardPerms = _jsonToken.Permissions.SingleOrDefault(p => p.ModelType == "Board");
			if ((boardPerms != null) && (boardPerms.IdModel != ModelScope<Board>.All.ToString()))
			{
				var board = Svc.Retrieve<Board>(boardPerms.IdModel);
				_boardPermissions = new TokenPermission<Board>(boardPerms, board);
			}
			var memberPerms = _jsonToken.Permissions.SingleOrDefault(p => p.ModelType == "Member");
			if ((memberPerms != null) && (memberPerms.IdModel != ModelScope<Board>.All.ToString()))
			{
				if (_member == null)
					_member = Svc.Retrieve<Member>(memberPerms.IdModel);
				_memberPermissions = new TokenPermission<Member>(memberPerms, _member);
			}
			var orgPerms = _jsonToken.Permissions.SingleOrDefault(p => p.ModelType == "Organization");
			if ((orgPerms != null) && (orgPerms.IdModel != ModelScope<Board>.All.ToString()))
			{
				var organization = Svc.Retrieve<Organization>(orgPerms.IdModel);
				_organizationPermissions = new TokenPermission<Organization>(orgPerms, organization);
			}
		}

		private string GetExpirationDateString()
		{
			return DateExpires.HasValue ? string.Format("Expires {0}.", DateExpires.Value) : "Never expires.";
		}
	}
}
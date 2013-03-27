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
 
	File Name:		BoardMembership.cs
	Namespace:		Manatee.Trello
	Class Name:		BoardMemberhip
	Purpose:		Represents a member of a board on Trello.com.

***************************************************************************************/
using System;
using System.Linq;
using Manatee.Json;
using Manatee.Json.Enumerations;
using Manatee.Json.Extensions;
using Manatee.Trello.Contracts;
using Manatee.Trello.Implementation;

namespace Manatee.Trello
{
	//{
	//   "id":"5144051cbd0da66812002022",
	//   "idMember":"50b693ad6f122b4310000a3c",
	//   "memberType":"admin",
	//   "deactivated":false
	//},
	///<summary>
	/// Represents a member of a board, including their membership type.
	///</summary>
	public class BoardMembership : JsonCompatibleExpiringObject, IEquatable<BoardMembership>
	{
		private static readonly OneToOneMap<BoardMembershipType, string> _typeMap;

		private string _apiMembershipType;
		private bool? _isDeactivated;
		private string _memberId;
		private Member _member;
		private BoardMembershipType _membershipType;

		///<summary>
		/// Gets whether the membership is deactivated.
		///</summary>
		public bool? IsDeactivated
		{
			get
			{
				VerifyNotExpired();
				return _isDeactivated;
			}
		}
		///<summary>
		/// Get the member.
		///</summary>
		public Member Member
		{
			get
			{
				VerifyNotExpired();
				return ((_member == null) || (_member.Id != _memberId)) && (Svc != null) ? (_member = Svc.Retrieve<Member>(_memberId)) : _member;
			}
		}
		///<summary>
		/// Gets the membership type.
		///</summary>
		public BoardMembershipType MembershipType { get { return _membershipType; } }

		static BoardMembership()
		{
			_typeMap = new OneToOneMap<BoardMembershipType, string>
			           	{
			           		{BoardMembershipType.Admin, "admin"},
			           		{BoardMembershipType.Normal, "normal"},
			           		{BoardMembershipType.Observer, "observer"},
			           	};
		}
		///<summary>
		/// Creates a new instance of the BoardMembership class.
		///</summary>
		public BoardMembership() {}
		internal BoardMembership(TrelloService svc, Board owner)
			: base(svc, owner) {}

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
			_apiMembershipType = obj.TryGetString("memberType");
			_isDeactivated = obj.TryGetBoolean("deactivated");
			_memberId = obj.TryGetString("idMember");
			UpdateType();
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
							{"deactivated", _isDeactivated.HasValue ? _isDeactivated.Value : JsonValue.Null},
			           		{"idMember", _memberId},
			           		{"memberType", _apiMembershipType}
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
		public bool Equals(BoardMembership other)
		{
			return Id == other.Id;
		}
		
		internal override bool Match(string id)
		{
			return Id == id;
		}
		internal override void Refresh(ExpiringObject entity)
		{
			var membership = entity as BoardMembership;
			if (membership == null) return;
			_apiMembershipType = membership._apiMembershipType;
			_isDeactivated = membership._isDeactivated;
			_memberId = membership._memberId;
			_isInitialized = true;
		}

		/// <summary>
		/// Retrieves updated data from the service instance and refreshes the object.
		/// </summary>
		protected override void Get()
		{
			var entity = Svc.Api.Get(Svc.RequestProvider.Create<BoardMembership>(new[] {Owner, this}));
			Refresh(entity);
		}
		/// <summary>
		/// Propigates the service instance to the object's owned objects.
		/// </summary>
		protected override void PropigateSerivce()
		{
			if (_member != null) _member.Svc = Svc;
		}

		private void UpdateType()
		{
			_membershipType = _typeMap.Any(kvp => kvp.Value == _apiMembershipType)
			                  	? _typeMap[_apiMembershipType]
			                  	: _membershipType = BoardMembershipType.Normal;
		}
		private void UpdateApiType()
		{
			if (_typeMap.Any(kvp => kvp.Key == _membershipType))
				_apiMembershipType = _typeMap[_membershipType];
		}
	}
}

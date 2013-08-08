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
using Manatee.Trello.Contracts;
using Manatee.Trello.Internal;
using Manatee.Trello.Internal.Json;
using Manatee.Trello.Json;
using Manatee.Trello.Rest;

namespace Manatee.Trello
{
	///<summary>
	/// Represents a member of a board, including their membership type.
	///</summary>
	public class BoardMembership : ExpiringObject, IEquatable<BoardMembership>, IComparable<BoardMembership>
	{
		private static readonly OneToOneMap<BoardMembershipType, string> _typeMap;

		private IJsonBoardMembership _jsonBoardMembership;
		private Member _member;
		private BoardMembershipType _membershipType;

		/// <summary>
		/// Gets a unique identifier (not necessarily a GUID).
		/// </summary>
		public override string Id
		{
			get
			{
				return _jsonBoardMembership != null ? _jsonBoardMembership.Id : base.Id;
			}
			internal set
			{
				if (_jsonBoardMembership != null)
					_jsonBoardMembership.Id = value;
				base.Id = value;
			}
		}
		///<summary>
		/// Gets whether the membership is deactivated.
		///</summary>
		public bool? IsDeactivated
		{
			get
			{
				VerifyNotExpired();
				if (_jsonBoardMembership == null) return null;
				return _jsonBoardMembership.Deactivated;
			}
		}
		///<summary>
		/// Gets the member.
		///</summary>
		public Member Member
		{
			get
			{
				VerifyNotExpired();
				if (_jsonBoardMembership == null) return null;
				return ((_member == null) || (_member.Id != _jsonBoardMembership.IdMember)) && (Svc != null)
				       	? (_member = Svc.Retrieve<Member>(_jsonBoardMembership.IdMember))
				       	: _member;
			}
		}
		///<summary>
		/// Gets the membership type.
		///</summary>
		public BoardMembershipType MembershipType { get { return _membershipType; } }

		internal static string TypeKey { get { return "memberships"; } }
		internal static string TypeKey2 { get { return "memberships"; } }
		internal override string Key { get { return TypeKey; } }
		internal override string Key2 { get { return TypeKey2; } }

		static BoardMembership()
		{
			_typeMap = new OneToOneMap<BoardMembershipType, string>
			           	{
			           		{BoardMembershipType.Admin, "admin"},
			           		{BoardMembershipType.Normal, "normal"},
			           		{BoardMembershipType.Observer, "observer"},
			           	};
		}
		/// <summary>
		/// Creates a new instance of the BoardMembership class.
		/// </summary>
		public BoardMembership()
		{
			_jsonBoardMembership = new InnerJsonBoardMembership();
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
		/// <summary>
		/// Determines whether the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>.
		/// </summary>
		/// <returns>
		/// true if the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>; otherwise, false.
		/// </returns>
		/// <param name="obj">The object to compare with the current object. </param><filterpriority>2</filterpriority>
		public override bool Equals(object obj)
		{
			if (!(obj is BoardMembership)) return false;
			return Equals((BoardMembership) obj);
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
		public int CompareTo(BoardMembership other)
		{
			return MembershipType.CompareTo(other.MembershipType);
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
			return string.Format("{0} as {1}", Member, MembershipType);
		}
		/// <summary>
		/// Retrieves updated data from the service instance and refreshes the object.
		/// </summary>
		public override bool Refresh()
		{
			//var endpoint = EndpointGenerator.Default.Generate(Owner, this);
			//var request = Api.RequestProvider.Create(endpoint.ToString());
			//ApplyJson(Api.Get<IJsonBoardMembership>(request));
			return false;
		}

		/// <summary>
		/// Propigates the service instance to the object's owned objects.
		/// </summary>
		protected override void PropigateService()
		{
			if (_member != null) _member.Svc = Svc;
		}

		internal override void ApplyJson(object obj)
		{
			if (obj is IRestResponse)
				_jsonBoardMembership = ((IRestResponse<IJsonBoardMembership>)obj).Data;
			else
				_jsonBoardMembership = (IJsonBoardMembership)obj;
			UpdateType();
		}

		private void UpdateType()
		{
			_membershipType = _typeMap.Any(kvp => kvp.Value == _jsonBoardMembership.MemberType)
								? _typeMap[_jsonBoardMembership.MemberType]
			                  	: _membershipType = BoardMembershipType.Normal;
		}
	}
}

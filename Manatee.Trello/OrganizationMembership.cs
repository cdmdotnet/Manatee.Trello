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
 
	File Name:		OrganizationMembership.cs
	Namespace:		Manatee.Trello
	Class Name:		OrganizationMembership
	Purpose:		Represents a membership of an Organization.

***************************************************************************************/
using System;
using System.Linq;
using Manatee.Trello.Contracts;
using Manatee.Trello.Internal;
using Manatee.Trello.Internal.Json;
using Manatee.Trello.Json;

namespace Manatee.Trello
{
	//{"id":"50d4eb07a1b090215200332a","idMember":"50b693ad6f122b4310000a3c","memberType":"admin","unconfirmed":false}
	/// <summary>
	/// Represents a membership of an Organization.
	/// </summary>
	public class OrganizationMembership : ExpiringObject, IEquatable<OrganizationMembership>, IComparable<OrganizationMembership>
	{
		private static readonly OneToOneMap<OrganizationMembershipType, string> _typeMap;

		private IJsonOrganizationMembership _jsonOrganizationMembership;
		private Member _member;
		private OrganizationMembershipType _membershipType;

		/// <summary>
		/// Gets a unique identifier (not necessarily a GUID).
		/// </summary>
		public override string Id
		{
			get
			{
				return _jsonOrganizationMembership != null ? _jsonOrganizationMembership.Id : base.Id;
			}
			internal set
			{
				if (_jsonOrganizationMembership != null)
					_jsonOrganizationMembership.Id = value;
				base.Id = value;
			}
		}
		///<summary>
		/// Gets whether the membership is unconfirmed.
		///</summary>
		public bool? IsUnconfirmed
		{
			get
			{
				VerifyNotExpired();
				if (_jsonOrganizationMembership == null) return null;
				return _jsonOrganizationMembership.Unconfirmed;
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
				if (_jsonOrganizationMembership == null) return null;
				return ((_member == null) || (_member.Id != _jsonOrganizationMembership.IdMember)) && (Svc != null)
				       	? (_member = Svc.Retrieve<Member>(_jsonOrganizationMembership.IdMember))
				       	: _member;
			}
		}
		///<summary>
		/// Gets the membership type.
		///</summary>
		public OrganizationMembershipType MembershipType { get { return _membershipType; } }

		internal static string TypeKey { get { return "memberships"; } }
		internal static string TypeKey2 { get { return "memberships"; } }
		internal override string Key { get { return TypeKey; } }
		internal override string Key2 { get { return TypeKey2; } }

		static OrganizationMembership()
		{
			_typeMap = new OneToOneMap<OrganizationMembershipType, string>
			           	{
			           		{OrganizationMembershipType.Admin, "admin"},
			           		{OrganizationMembershipType.Normal, "normal"},
			           		//{OrganizationMembershipType.Observer, "observer"},
			           	};
		}
		/// <summary>
		/// Creates a new instance of the OrganizationMembership class.
		/// </summary>
		public OrganizationMembership()
		{
			_jsonOrganizationMembership = new InnerJsonOrganizationMembership();
		}

		/// <summary>
		/// Indicates whether the current object is equal to another object of the same type.
		/// </summary>
		/// <returns>
		/// true if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.
		/// </returns>
		/// <param name="other">An object to compare with this object.</param>
		public bool Equals(OrganizationMembership other)
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
			if (!(obj is OrganizationMembership)) return false;
			return Equals((OrganizationMembership)obj);
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
		public int CompareTo(OrganizationMembership other)
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
		protected override bool Refresh()
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
			_jsonOrganizationMembership = (IJsonOrganizationMembership)obj;
			UpdateType();
		}

		private void UpdateType()
		{
			_membershipType = _typeMap.Any(kvp => kvp.Value == _jsonOrganizationMembership.MemberType)
								? _typeMap[_jsonOrganizationMembership.MemberType]
								: _membershipType = OrganizationMembershipType.Normal;
		}
	}
}
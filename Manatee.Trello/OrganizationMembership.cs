/***************************************************************************************

	Copyright 2015 Greg Dennis

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
	Purpose:		Represents the permission level a member has on an organization.

***************************************************************************************/

using System;
using System.Collections.Generic;
using Manatee.Trello.Contracts;
using Manatee.Trello.Internal;
using Manatee.Trello.Internal.Synchronization;
using Manatee.Trello.Internal.Validation;
using Manatee.Trello.Json;

namespace Manatee.Trello
{
	/// <summary>
	/// Represents the permission level a member has on an organization.
	/// </summary>
	public class OrganizationMembership : ICacheable
	{
		private readonly Field<Member> _member;
		private readonly Field<OrganizationMembershipType?> _memberType;
		private readonly Field<bool?> _isDeactivated;
		private readonly OrganizationMembershipContext _context;
		private DateTime? _creation;

		/// <summary>
		/// Gets the creation date of the membership.
		/// </summary>
		public DateTime CreationDate
		{
			get
			{
				if (_creation == null)
					_creation = Id.ExtractCreationDate();
				return _creation.Value;
			}
		}
		/// <summary>
		/// Gets the membership definition's ID.
		/// </summary>
		public string Id { get; private set; }
		/// <summary>
		/// Gets whether the member has accepted the invitation to join Trello.
		/// </summary>
		public bool? IsDeactivated => _isDeactivated.Value;
		/// <summary>
		/// Gets the member.
		/// </summary>
		public Member Member => _member.Value;
		/// <summary>
		/// Gets the membership's permission level.
		/// </summary>
		public OrganizationMembershipType? MemberType
		{
			get { return _memberType.Value; }
			set { _memberType.Value = value; }
		}

		internal IJsonOrganizationMembership Json
		{
			get { return _context.Data; }
			set { _context.Merge(value); }
		}

#if IOS
		private Action<OrganizationMembership, IEnumerable<string>> _updatedInvoker;

		/// <summary>
		/// Raised when data on the membership is updated.
		/// </summary>
		public event Action<OrganizationMembership, IEnumerable<string>> Updated
		{
			add { _updatedInvoker += value; }
			remove { _updatedInvoker -= value; }
		}
#else
		/// <summary>
		/// Raised when data on the membership is updated.
		/// </summary>
		public event Action<OrganizationMembership, IEnumerable<string>> Updated;
#endif

		internal OrganizationMembership(IJsonOrganizationMembership json, string ownerId, TrelloAuthorization auth)
		{
			Id = json.Id;
			_context = new OrganizationMembershipContext(Id, ownerId, auth);
			_context.Synchronized += Synchronized;

			_member = new Field<Member>(_context, nameof(Member));
			_memberType = new Field<OrganizationMembershipType?>(_context, nameof(MemberType));
			_memberType.AddRule(NullableHasValueRule<OrganizationMembershipType>.Instance);
			_memberType.AddRule(EnumerationRule<OrganizationMembershipType?>.Instance);
			_isDeactivated = new Field<bool?>(_context, nameof(IsDeactivated));

			TrelloConfiguration.Cache.Add(this);

			_context.Merge(json);
		}

		/// <summary>
		/// Marks the organization membership to be refreshed the next time data is accessed.
		/// </summary>
		public void Refresh()
		{
			_context.Expire();
		}

		private void Synchronized(IEnumerable<string> properties)
		{
			Id = _context.Data.Id;
#if IOS
			var handler = _updatedInvoker;
#else
			var handler = Updated;
#endif
			handler?.Invoke(this, properties);
		}
	}
}
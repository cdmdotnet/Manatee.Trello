﻿using System;
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
		public virtual DateTime CreationDate
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
		public virtual string Id { get; private set; }
		/// <summary>
		/// Gets whether the member has accepted the invitation to join Trello.
		/// </summary>
		public virtual bool? IsDeactivated => _isDeactivated.Value;
		/// <summary>
		/// Gets the member.
		/// </summary>
		public virtual Member Member => _member.Value;
		/// <summary>
		/// Gets the membership's permission level.
		/// </summary>
		public virtual OrganizationMembershipType? MemberType
		{
			get { return _memberType.Value; }
			set { _memberType.Value = value; }
		}

		internal IJsonOrganizationMembership Json
		{
			get { return _context.Data; }
			set { _context.Merge(value); }
		}

		/// <summary>
		/// Raised when data on the membership is updated.
		/// </summary>
		public virtual event Action<OrganizationMembership, IEnumerable<string>> Updated;

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
		public virtual void Refresh()
		{
			_context.Expire();
		}

		private void Synchronized(IEnumerable<string> properties)
		{
			Id = _context.Data.Id;
			var handler = Updated;
			handler?.Invoke(this, properties);
		}
	}
}
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Manatee.Trello.Internal;
using Manatee.Trello.Internal.DataAccess;
using Manatee.Trello.Internal.Synchronization;
using Manatee.Trello.Internal.Validation;
using Manatee.Trello.Json;

namespace Manatee.Trello
{
	/// <summary>
	/// Represents the permission level a member has on an organization.
	/// </summary>
	public class OrganizationMembership : IOrganizationMembership, IMergeJson<IJsonOrganizationMembership>, IBatchRefresh, IHandleSynchronization
	{
		private readonly Field<Member> _member;
		private readonly Field<OrganizationMembershipType?> _memberType;
		private readonly Field<bool?> _isUnconfirmed;
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
		public bool? IsUnconfirmed => _isUnconfirmed.Value;
		/// <summary>
		/// Gets the member.
		/// </summary>
		public IMember Member => _member.Value;
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
		TrelloAuthorization IBatchRefresh.Auth => _context.Auth;

		/// <summary>
		/// Raised when data on the membership is updated.
		/// </summary>
		public event Action<IOrganizationMembership, IEnumerable<string>> Updated;

		internal OrganizationMembership(IJsonOrganizationMembership json, string ownerId, TrelloAuthorization auth)
		{
			Id = json.Id;
			_context = new OrganizationMembershipContext(Id, ownerId, auth);
			_context.Synchronized.Add(this);

			_member = new Field<Member>(_context, nameof(Member));
			_memberType = new Field<OrganizationMembershipType?>(_context, nameof(MemberType));
			_memberType.AddRule(NullableHasValueRule<OrganizationMembershipType>.Instance);
			_memberType.AddRule(EnumerationRule<OrganizationMembershipType?>.Instance);
			_isUnconfirmed = new Field<bool?>(_context, nameof(IsUnconfirmed));

			if (auth != TrelloAuthorization.Null)
				TrelloConfiguration.Cache.Add(this);

			_context.Merge(json);
		}

		/// <summary>
		/// Refreshes the organization membership data.
		/// </summary>
		/// <param name="force">Indicates that the refresh should ignore the value in <see cref="TrelloConfiguration.RefreshThrottle"/> and make the call to the API.</param>
		/// <param name="ct">(Optional) A cancellation token for async processing.</param>
		public Task Refresh(bool force = false, CancellationToken ct = default(CancellationToken))
		{
			return _context.Synchronize(force, ct);
		}

		/// <summary>Returns a string that represents the current object.</summary>
		/// <returns>A string that represents the current object.</returns>
		public override string ToString()
		{
			return $"{Member} ({MemberType})";
		}

		void IHandleSynchronization.HandleSynchronized(IEnumerable<string> properties)
		{
			Id = _context.Data.Id;
			var handler = Updated;
			handler?.Invoke(this, properties);
		}

		void IMergeJson<IJsonOrganizationMembership>.Merge(IJsonOrganizationMembership json, bool overwrite)
		{
			_context.Merge(json, overwrite);
		}

		Endpoint IBatchRefresh.GetRefreshEndpoint()
		{
			return _context.GetRefreshEndpoint();
		}

		void IBatchRefresh.Apply(string content)
		{
			var json = TrelloConfiguration.Deserializer.Deserialize<IJsonOrganizationMembership>(content);
			_context.Merge(json);
		}
	}
}
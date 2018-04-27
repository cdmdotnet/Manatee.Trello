using System.Collections.Generic;

namespace Manatee.Trello
{
	/// <summary>
	/// A read-only collection of organization memberships.
	/// </summary>
	public interface IReadOnlyOrganizationMembershipCollection : IReadOnlyCollection<IOrganizationMembership>
	{
		/// <summary>
		/// Retrieves a membership which matches the supplied key.
		/// </summary>
		/// <param name="key">The key to match.</param>
		/// <returns>The matching list, or null if none found.</returns>
		/// <remarks>
		/// Matches on OrganizationMembership.Id, OrganizationMembership.Member.Id, OrganizationMembership.Member.FullName, and OrganizationMembership.Member.Username. Comparison is case-sensitive.
		/// </remarks>
		IOrganizationMembership this[string key] { get; }

		/// <summary>
		/// Adds a filter to the collection.
		/// </summary>
		/// <param name="filter">The filter value.</param>
		void Filter(MembershipFilter filter);
		/// <summary>
		/// Adds a set of filters to the collection.
		/// </summary>
		/// <param name="filters">The filter values.</param>
		void Filter(IEnumerable<MembershipFilter> filters);
	}
}
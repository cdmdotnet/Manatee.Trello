using System.Collections.Generic;

namespace Manatee.Trello
{
	/// <summary>
	/// A read-only collection of board memberships.
	/// </summary>
	public interface IReadOnlyBoardMembershipCollection : IReadOnlyCollection<IBoardMembership>
	{
		/// <summary>
		/// Retrieves a membership which matches the supplied key.
		/// </summary>
		/// <param name="key">The key to match.</param>
		/// <returns>The matching membership, or null if none found.</returns>
		/// <remarks>
		/// Matches on BoardMembership.Id, BoardMembership.Member.Id, BoardMembership.Member.Name, and BoardMembership.Usernamee. Comparison is case-sensitive.
		/// </remarks>
		IBoardMembership this[string key] { get; }

		/// <summary>
		/// Adds a filter to the collection.
		/// </summary>
		/// <param name="membership">The filter value.</param>
		void Filter(MembershipFilter membership);
		/// <summary>
		/// Adds a filter to the collection.
		/// </summary>
		/// <param name="memberships">The filter values.</param>
		void Filter(IEnumerable<MembershipFilter> memberships);
	}
}
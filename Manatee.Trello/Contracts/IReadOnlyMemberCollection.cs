using System.Collections.Generic;

namespace Manatee.Trello
{
	/// <summary>
	/// A read-only collection of members.
	/// </summary>
	public interface IReadOnlyMemberCollection : IReadOnlyCollection<IMember>
	{
		/// <summary>
		/// Retrieves a member which matches the supplied key.
		/// </summary>
		/// <param name="key">The key to match.</param>
		/// <returns>The matching member, or null if none found.</returns>
		/// <remarks>
		/// Matches on Member.Id, Member.FullName, and Member.Username.  Comparison is case-sensitive.
		/// </remarks>
		IMember this[string key] { get; }

		/// <summary>
		/// Adds a filter to the collection.
		/// </summary>
		/// <param name="filter">The filter value.</param>
		void Filter(MemberFilter filter);
		/// <summary>
		/// Adds a set of filters to the collection.
		/// </summary>
		/// <param name="filters">The filter values.</param>
		void Filter(IEnumerable<MemberFilter> filters);
	}
}
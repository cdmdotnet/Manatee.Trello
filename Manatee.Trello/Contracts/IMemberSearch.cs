using System.Collections.Generic;

namespace Manatee.Trello
{
	/// <summary>
	/// Performs a search for members.
	/// </summary>
	public interface IMemberSearch : IRefreshable
	{
		/// <summary>
		/// Gets the collection of results returned by the search.
		/// </summary>
		IEnumerable<MemberSearchResult> Results { get; }
	}
}
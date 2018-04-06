using System.Collections.Generic;

namespace Manatee.Trello
{
	/// <summary>
	/// Performs a search for members.
	/// </summary>
	public interface IMemberSearch
	{
		/// <summary>
		/// Gets the collection of results returned by the search.
		/// </summary>
		IEnumerable<MemberSearchResult> Results { get; }

		/// <summary>
		/// Marks the member search to be refreshed the next time data is accessed.
		/// </summary>
		void Refresh();
	}
}
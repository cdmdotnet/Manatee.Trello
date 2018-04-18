using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

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
		/// <param name="ct">(Optional) A cancellation token for async processing.</param>
		Task Refresh(CancellationToken ct = default(CancellationToken));
	}
}
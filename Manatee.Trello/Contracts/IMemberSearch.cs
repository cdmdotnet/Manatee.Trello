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
		/// Refreshes the search results.
		/// </summary>
		/// <param name="force">Indicates that the refresh should ignore the value in <see cref="TrelloConfiguration.RefreshThrottle"/> and make the call to the API.</param>
		/// <param name="ct">(Optional) A cancellation token for async processing.</param>
		Task Refresh(bool force = false, CancellationToken ct = default(CancellationToken));
	}
}
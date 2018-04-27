using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Manatee.Trello
{
	/// <summary>
	/// Performs a search.
	/// </summary>
	public interface ISearch
	{
		/// <summary>
		/// Gets the collection of actions returned by the search.
		/// </summary>
		IEnumerable<IAction> Actions { get; }

		/// <summary>
		/// Gets the collection of boards returned by the search.
		/// </summary>
		IEnumerable<IBoard> Boards { get; }

		/// <summary>
		/// Gets the collection of cards returned by the search.
		/// </summary>
		IEnumerable<ICard> Cards { get; }

		/// <summary>
		/// Gets the collection of members returned by the search.
		/// </summary>
		IEnumerable<IMember> Members { get; }

		/// <summary>
		/// Gets the collection of organizations returned by the search.
		/// </summary>
		IEnumerable<IOrganization> Organizations { get; }

		/// <summary>
		/// Gets the query.
		/// </summary>
		string Query { get; }

		/// <summary>
		/// Refreshes the search results.
		/// </summary>
		/// <param name="ct">(Optional) A cancellation token for async processing.</param>
		Task Refresh(CancellationToken ct = default(CancellationToken));
	}
}
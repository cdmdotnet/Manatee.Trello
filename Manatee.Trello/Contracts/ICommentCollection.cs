using System.Threading;
using System.Threading.Tasks;

namespace Manatee.Trello
{
	/// <summary>
	/// A collection of comment actions.
	/// </summary>
	public interface ICommentCollection : IReadOnlyActionCollection
	{
		/// <summary>
		/// Posts a new comment to a card.
		/// </summary>
		/// <param name="text">The content of the comment.</param>
		/// <param name="ct">(Optional) A cancellation token for async processing.</param>
		/// <returns>The <see cref="IAction"/> associated with the comment.</returns>
		Task<IAction> Add(string text, CancellationToken ct = default(CancellationToken));
	}
}
using System.Threading;
using System.Threading.Tasks;

namespace Manatee.Trello
{
	/// <summary>
	/// A collection of labels for cards.
	/// </summary>
	public interface ICardLabelCollection : IReadOnlyCollection<ILabel>
	{
		/// <summary>
		/// Adds a label to the collection.
		/// </summary>
		/// <param name="label">The label to add.</param>
		/// <param name="ct">(Optional) A cancellation token for async processing.</param>
		Task Add(ILabel label, CancellationToken ct = default(CancellationToken));

		/// <summary>
		/// Removes a label from the collection.
		/// </summary>
		/// <param name="label">The label to add.</param>
		/// <param name="ct">(Optional) A cancellation token for async processing.</param>
		Task Remove(ILabel label, CancellationToken ct = default(CancellationToken));
	}
}
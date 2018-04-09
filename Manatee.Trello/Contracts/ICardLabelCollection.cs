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
		Task Add(ILabel label);

		/// <summary>
		/// Removes a label from the collection.
		/// </summary>
		/// <param name="label">The label to add.</param>
		Task Remove(ILabel label);
	}
}
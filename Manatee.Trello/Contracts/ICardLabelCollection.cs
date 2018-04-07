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
		void Add(ILabel label);

		/// <summary>
		/// Removes a label from the collection.
		/// </summary>
		/// <param name="label">The label to add.</param>
		void Remove(ILabel label);
	}
}
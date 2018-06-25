namespace Manatee.Trello
{
	/// <summary>
	/// A read-only collection of boards.
	/// </summary>
	public interface IReadOnlyBoardCollection : IReadOnlyCollection<IBoard>
	{
		/// <summary>
		/// Retrieves a board which matches the supplied key.
		/// </summary>
		/// <param name="key">The key to match.</param>
		/// <returns>The matching board, or null if none found.</returns>
		/// <remarks>
		/// Matches on Board.Id and Board.Name.  Comparison is case-sensitive.
		/// </remarks>
		IBoard this[string key] { get; }
		/// <summary>
		/// Adds a filter to the collection.
		/// </summary>
		/// <param name="filter">The filter value.</param>
		void Filter(BoardFilter filter);
	}
}
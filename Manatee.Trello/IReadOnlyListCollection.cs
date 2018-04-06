namespace Manatee.Trello
{
	/// <summary>
	/// A read-only collection of lists.
	/// </summary>
	public interface IReadOnlyListCollection : IReadOnlyCollection<IList>
	{
		/// <summary>
		/// Retrieves a list which matches the supplied key.
		/// </summary>
		/// <param name="key">The key to match.</param>
		/// <returns>The matching list, or null if none found.</returns>
		/// <remarks>
		/// Matches on List.Id and List.Name.  Comparison is case-sensitive.
		/// </remarks>
		IList this[string key] { get; }

		/// <summary>
		/// Adds a filter to the collection.
		/// </summary>
		/// <param name="filter">The filter value.</param>
		void Filter(ListFilter filter);
	}
}
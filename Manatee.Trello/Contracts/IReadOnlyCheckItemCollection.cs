namespace Manatee.Trello
{
	/// <summary>
	/// A read-only collection of checklist items.
	/// </summary>
	public interface IReadOnlyCheckItemCollection : IReadOnlyCollection<ICheckItem>
	{
		/// <summary>
		/// Retrieves a check list item which matches the supplied key.
		/// </summary>
		/// <param name="key">The key to match.</param>
		/// <returns>The matching check list item, or null if none found.</returns>
		/// <remarks>
		/// Matches on CheckItem.Id and CheckItem.Name.  Comparison is case-sensitive.
		/// </remarks>
		ICheckItem this[string key] { get; }
	}
}
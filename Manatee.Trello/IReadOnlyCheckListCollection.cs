namespace Manatee.Trello
{
	/// <summary>
	/// A read-only collection of checklists.
	/// </summary>
	public interface IReadOnlyCheckListCollection : IReadOnlyCollection<ICheckList>
	{
		/// <summary>
		/// Retrieves a check list which matches the supplied key.
		/// </summary>
		/// <param name="key">The key to match.</param>
		/// <returns>The matching check list, or null if none found.</returns>
		/// <remarks>
		/// Matches on CheckList.Id and CheckList.Name.  Comparison is case-sensitive.
		/// </remarks>
		ICheckList this[string key] { get; }
	}
}
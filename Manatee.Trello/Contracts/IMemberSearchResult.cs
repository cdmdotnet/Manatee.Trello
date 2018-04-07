namespace Manatee.Trello
{
	/// <summary>
	/// Represents a result from a member search.
	/// </summary>
	public interface IMemberSearchResult
	{
		/// <summary>
		/// Gets the returned member.
		/// </summary>
		IMember Member { get; }

		/// <summary>
		/// Gets a value indicating the similarity of the member to the search query.
		/// </summary>
		int? Similarity { get; }
	}
}
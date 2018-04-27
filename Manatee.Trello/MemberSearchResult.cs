namespace Manatee.Trello
{
	/// <summary>
	/// Represents a result from a member search.
	/// </summary>
	public class MemberSearchResult : IMemberSearchResult
	{
		/// <summary>
		/// Gets the returned member.
		/// </summary>
		public IMember Member { get; }
		/// <summary>
		/// Gets a value indicating the similarity of the member to the search query.
		/// </summary>
		public int? Similarity { get; }

		internal MemberSearchResult(IMember member, int? similarity)
		{
			Member = member;
			Similarity = similarity;
		}
	}
}

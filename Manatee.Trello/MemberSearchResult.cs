namespace Manatee.Trello
{
	/// <summary>
	/// Represents a result from a member search.
	/// </summary>
	public class MemberSearchResult
	{
		/// <summary>
		/// Gets the returned member.
		/// </summary>
		public virtual Member Member { get; }
		/// <summary>
		/// Gets a value indicating the similarity of the member to the search query.
		/// </summary>
		public virtual int? Similarity { get; }

		internal MemberSearchResult(Member member, int? similarity)
		{
			Member = member;
			Similarity = similarity;
		}
	}
}

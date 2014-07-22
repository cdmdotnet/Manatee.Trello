namespace Manatee.Trello
{
	public class MemberSearchResult
	{
		public Member Member { get; private set; }
		public int? Similarity { get; private set; }

		public MemberSearchResult(Member member, int? similarity)
		{
			Member = member;
			Similarity = similarity;
		}
	}
}

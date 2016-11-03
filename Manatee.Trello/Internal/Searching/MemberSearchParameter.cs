namespace Manatee.Trello.Internal.Searching
{
	internal class MemberSearchParameter : ISearchParameter
	{
		public string Query { get; }

		public MemberSearchParameter(Member member)
		{
			Query = member is Me ? "@me" : member.Mention;
		}
	}
}
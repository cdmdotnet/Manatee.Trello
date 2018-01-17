using Moq;

namespace Manatee.Trello.Moq
{
	public class MemberSearchResultMock : Mock<MemberSearchResult>
	{
		public MemberSearchResultMock()
			: base(string.Empty, null, null, null, null, null)
		{
		}
	}
}
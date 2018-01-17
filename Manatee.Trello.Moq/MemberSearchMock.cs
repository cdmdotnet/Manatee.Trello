using Moq;

namespace Manatee.Trello.Moq
{
	public class MemberSearchMock : Mock<MemberSearch>
	{
		public MemberSearchMock()
			: base(string.Empty, null)
		{
		}
	}
}
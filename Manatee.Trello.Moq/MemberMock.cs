using Moq;

namespace Manatee.Trello.Moq
{
	public class MemberMock : Mock<Member>
	{
		public MemberMock()
			: base(string.Empty, null)
		{
		}
	}
}
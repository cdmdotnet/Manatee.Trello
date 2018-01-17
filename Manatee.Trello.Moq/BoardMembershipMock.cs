using Moq;

namespace Manatee.Trello.Moq
{
	public class BoardMembershipMock : Mock<BoardMembership>
	{
		public BoardMembershipMock()
			: base(null)
		{
		}
	}
}
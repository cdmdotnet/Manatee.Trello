using Moq;

namespace Manatee.Trello.Moq
{
	public class BoardMembershipCollectionMock : Mock<BoardMembershipCollection>
	{
		public BoardMembershipCollectionMock()
			: base(null)
		{
		}
	}
}
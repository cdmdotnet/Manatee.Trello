using Moq;

namespace Manatee.Trello.Moq
{
	public class ReadOnlyBoardMembershipCollectionMock : Mock<ReadOnlyBoardMembershipCollection>
	{
		public ReadOnlyBoardMembershipCollectionMock()
			: base(null)
		{
		}
	}
}
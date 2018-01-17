using Moq;

namespace Manatee.Trello.Moq
{
	public class ReadOnlyBoardCollectionMock : Mock<ReadOnlyBoardCollection>
	{
		public ReadOnlyBoardCollectionMock()
			: base(null)
		{
		}
	}
}
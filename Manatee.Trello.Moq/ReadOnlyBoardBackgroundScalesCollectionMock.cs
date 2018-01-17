using Moq;

namespace Manatee.Trello.Moq
{
	public class ReadOnlyBoardBackgroundScalesCollectionMock : Mock<ReadOnlyBoardBackgroundScalesCollection>
	{
		public ReadOnlyBoardBackgroundScalesCollectionMock()
			: base(null)
		{
		}
	}
}
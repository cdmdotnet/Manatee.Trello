using Moq;

namespace Manatee.Trello.Moq
{
	public class BoardCollectionMock : Mock<BoardCollection>
	{
		public BoardCollectionMock()
			: base(null)
		{
		}
	}
}
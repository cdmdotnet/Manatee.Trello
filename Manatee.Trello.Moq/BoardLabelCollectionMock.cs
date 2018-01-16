using Moq;

namespace Manatee.Trello.Moq
{
	public class BoardLabelCollectionMock : Mock<BoardLabelCollection>
	{
		public BoardLabelCollectionMock()
			: base(null)
		{
		}
	}
}
using Moq;

namespace Manatee.Trello.Moq
{
	public class BoardBackgroundMock : Mock<BoardBackground>
	{
		public BoardBackgroundMock()
			: base(null)
		{
		}
	}
}
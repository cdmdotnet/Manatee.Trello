using Moq;

namespace Manatee.Trello.Moq
{
	public class BoardMock : Mock<Board>
	{
		public BoardMock()
			: base(string.Empty, null)
		{
		}
	}
}
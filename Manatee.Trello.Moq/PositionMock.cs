using Moq;

namespace Manatee.Trello.Moq
{
	public class PositionMock : Mock<Position>
	{
		public PositionMock()
			: base(0.0)
		{
		}
	}
}
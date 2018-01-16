using Moq;

namespace Manatee.Trello.Moq
{
	public class ListMock : Mock<List>
	{
		public ListMock()
			: base(string.Empty, null)
		{
		}
	}
}
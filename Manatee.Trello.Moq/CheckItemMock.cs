using Moq;

namespace Manatee.Trello.Moq
{
	public class CheckItemMock : Mock<CheckItem>
	{
		public CheckItemMock()
			: base(null)
		{
		}
	}
}
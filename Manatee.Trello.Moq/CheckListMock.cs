using Moq;

namespace Manatee.Trello.Moq
{
	public class CheckListMock : Mock<CheckList>
	{
		public CheckListMock()
			: base(string.Empty, null)
		{
		}
	}
}
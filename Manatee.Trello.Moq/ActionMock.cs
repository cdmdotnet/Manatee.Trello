using Moq;

namespace Manatee.Trello.Moq
{
	public class ActionMock : Mock<Action>
	{
		public ActionMock()
			: base(string.Empty, null)
		{

		}
	}
}
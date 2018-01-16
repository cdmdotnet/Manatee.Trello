using Moq;

namespace Manatee.Trello.Moq
{
	public class WebColorMock : Mock<WebColor>
	{
		public WebColorMock()
			: base(string.Empty)
		{
		}
	}
}
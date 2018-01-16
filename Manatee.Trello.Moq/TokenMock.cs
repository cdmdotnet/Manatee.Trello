using Moq;

namespace Manatee.Trello.Moq
{
	public class TokenMock : Mock<Token>
	{
		public TokenMock()
			: base(string.Empty, null)
		{
		}
	}
}
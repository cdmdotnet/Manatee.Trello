using Moq;

namespace Manatee.Trello.Moq
{
	public class TokenPermissionMock : Mock<TokenPermission>
	{
		public TokenPermissionMock()
			: base(null)
		{
		}
	}
}
using Moq;

namespace Manatee.Trello.Moq
{
	public class PowerUpMock : Mock<UnknownPowerUp>
	{
		public PowerUpMock()
			: base(null)
		{
		}
	}
}
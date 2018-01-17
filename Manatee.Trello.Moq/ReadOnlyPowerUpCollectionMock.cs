using Moq;

namespace Manatee.Trello.Moq
{
	public class ReadOnlyPowerUpCollectionMock : Mock<ReadOnlyPowerUpCollection>
	{
		public ReadOnlyPowerUpCollectionMock()
			: base(null)
		{
		}
	}
}
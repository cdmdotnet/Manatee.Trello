using Moq;

namespace Manatee.Trello.Moq
{
	public class ReadOnlyPowerUpDataCollectionMock : Mock<ReadOnlyPowerUpDataCollection>
	{
		public ReadOnlyPowerUpDataCollectionMock()
			: base(null)
		{
		}
	}
}
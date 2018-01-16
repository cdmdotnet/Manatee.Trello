using Moq;

namespace Manatee.Trello.Moq
{
	public class ReadOnlyNotificationCollectionMock : Mock<ReadOnlyNotificationCollection>
	{
		public ReadOnlyNotificationCollectionMock()
			: base(null)
		{
		}
	}
}
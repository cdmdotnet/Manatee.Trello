using Moq;

namespace Manatee.Trello.Moq
{
	public class NotificationMock : Mock<Notification>
	{
		public NotificationMock()
			: base(string.Empty, null)
		{
		}
	}
}
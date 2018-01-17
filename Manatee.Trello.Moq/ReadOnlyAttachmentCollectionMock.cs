using Moq;

namespace Manatee.Trello.Moq
{
	public class ReadOnlyAttachmentCollectionMock : Mock<ReadOnlyAttachmentCollection>
	{
		public ReadOnlyAttachmentCollectionMock()
			: base(null)
		{
		}
	}
}
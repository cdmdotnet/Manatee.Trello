using Moq;

namespace Manatee.Trello.Moq
{
	public class ReadOnlyAttachmentPreviewCollectionMock : Mock<ReadOnlyAttachmentPreviewCollection>
	{
		public ReadOnlyAttachmentPreviewCollectionMock()
			: base(null)
		{
		}
	}
}
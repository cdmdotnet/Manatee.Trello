using Moq;

namespace Manatee.Trello.Moq
{
	public class ReadOnlyStickerPreviewCollectionMock : Mock<ReadOnlyStickerPreviewCollection>
	{
		public ReadOnlyStickerPreviewCollectionMock()
			: base(null)
		{
		}
	}
}
using Moq;

namespace Manatee.Trello.Moq
{
	public class ReadOnlyStickerCollectionMock : Mock<ReadOnlyStickerCollection>
	{
		public ReadOnlyStickerCollectionMock()
			: base(null)
		{
		}
	}
}
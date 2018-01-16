using Moq;

namespace Manatee.Trello.Moq
{
	public class CardStickerCollectionMock : Mock<CardStickerCollection>
	{
		public CardStickerCollectionMock()
			: base(null)
		{
		}
	}
}
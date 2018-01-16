using Moq;

namespace Manatee.Trello.Moq
{
	public class StickerMock : Mock<Sticker>
	{
		public StickerMock()
			: base(null)
		{
		}
	}
}
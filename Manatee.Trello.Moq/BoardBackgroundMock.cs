using Moq;

namespace Manatee.Trello.Moq
{
	public class BoardBackgroundMock : Mock<BoardBackground>
	{
		private ReadOnlyBoardBackgroundScalesCollectionMock _scaledImages;

		public ReadOnlyBoardBackgroundScalesCollectionMock ScaledImages => _scaledImages ?? (_scaledImages = new ReadOnlyBoardBackgroundScalesCollectionMock());

		public BoardBackgroundMock()
			: base(null)
		{
			SetupGet(b => b.ScaledImages).Returns(() => ScaledImages.Object);
		}
	}
}
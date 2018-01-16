using Moq;

namespace Manatee.Trello.Moq
{
	public class CardMock : Mock<Card>
	{
		private ReadOnlyActionCollectionMock _actions;
		private BoardMock _board;
		private CommentCollectionMock _comments;

		public ReadOnlyActionCollectionMock Actions => _actions ?? (_actions = new ReadOnlyActionCollectionMock());
		public BoardMock Board => _board ?? (_board = new BoardMock());
		public CommentCollectionMock Comments => _comments ?? (_comments = new CommentCollectionMock());

		public CardMock()
			: base(string.Empty, null)
		{
			SetupGet(c => c.Actions).Returns(() => Actions.Object);
			SetupGet(c => c.Board).Returns(() => Board.Object);
		}
	}
}
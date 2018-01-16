using Moq;

namespace Manatee.Trello.Moq
{
	public class CardMock : Mock<Card>
	{
		private ReadOnlyActionCollectionMock _actions;
		private AttachmentCollectionMock _attachments;
		private BadgesMock _badges;
		private BoardMock _board;
		private CheckListCollectionMock _checkLists;
		private CommentCollectionMock _comments;
		private CardLabelCollectionMock _labels;
		private ListMock _list;
		private MemberCollectionMock _members;
		private PositionMock _position;
		private ReadOnlyPowerUpDataCollectionMock _powerUpData;
		private CardStickerCollectionMock _stickers;
		private ReadOnlyMemberCollectionMock _votingMembers;

		public ReadOnlyActionCollectionMock Actions => _actions ?? (_actions = new ReadOnlyActionCollectionMock());
		public AttachmentCollectionMock Attachments => _attachments ?? (_attachments = new AttachmentCollectionMock());
		public BadgesMock Badges => _badges ?? (_badges = new BadgesMock());
		public BoardMock Board => _board ?? (_board = new BoardMock());
		public CheckListCollectionMock CheckLists => _checkLists ?? (_checkLists = new CheckListCollectionMock());
		public CommentCollectionMock Comments => _comments ?? (_comments = new CommentCollectionMock());
		public CardLabelCollectionMock Labels => _labels ?? (_labels = new CardLabelCollectionMock());
		public ListMock List => _list ?? (_list = new ListMock());
		public MemberCollectionMock Members => _members ?? (_members = new MemberCollectionMock());
		public PositionMock Position => _position ?? (_position = new PositionMock());
		public ReadOnlyPowerUpDataCollectionMock PowerUpData => _powerUpData ?? (_powerUpData = new ReadOnlyPowerUpDataCollectionMock());
		public CardStickerCollectionMock Stickers => _stickers ?? (_stickers = new CardStickerCollectionMock());
		public ReadOnlyMemberCollectionMock VotingMembers => _votingMembers ?? (_votingMembers = new ReadOnlyMemberCollectionMock());

		public CardMock()
			: base(string.Empty, null)
		{
			SetupGet(c => c.Actions).Returns(() => Actions.Object);
			SetupGet(c => c.Attachments).Returns(() => Attachments.Object);
			SetupGet(c => c.Badges).Returns(() => Badges.Object);
			SetupGet(c => c.Board).Returns(() => Board.Object);
			SetupGet(c => c.CheckLists).Returns(() => CheckLists.Object);
			SetupGet(c => c.Comments).Returns(() => Comments.Object);
			SetupGet(c => c.Labels).Returns(() => Labels.Object);
			SetupGet(c => c.List).Returns(() => List.Object);
			SetupGet(c => c.Members).Returns(() => Members.Object);
			SetupGet(c => c.Position).Returns(() => Position.Object);
			SetupGet(c => c.PowerUpData).Returns(() => PowerUpData.Object);
			SetupGet(c => c.Stickers).Returns(() => Stickers.Object);
			SetupGet(c => c.VotingMembers).Returns(() => VotingMembers.Object);
		}
	}
}
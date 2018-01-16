using Moq;

namespace Manatee.Trello.Moq
{
	public class ActionDataMock : Mock<ActionData>
	{
		private AttachmentMock _attachment;
		private BoardMock _board;
		private BoardMock _boardSource;
		private BoardMock _boardTarget;
		private CardMock _card;
		private CardMock _cardSource;
		private CheckItemMock _checkItem;
		private CheckListMock _checkList;
		private ListMock _list;
		private ListMock _listAfter;
		private ListMock _listBefore;
		private MemberMock _member;
		private ListMock _oldList;
		private PositionMock _oldPosition;
		private OrganizationMock _organization;
		private PowerUpMock _powerUp;

		public AttachmentMock Attachment => _attachment ?? (_attachment = new AttachmentMock());
		public BoardMock Board => _board ?? (_board = new BoardMock());
		public BoardMock BoardSource => _boardSource ?? (_boardSource = new BoardMock());
		public BoardMock BoardTarget => _boardTarget ?? (_boardTarget = new BoardMock());
		public CardMock Card => _card ?? (_card = new CardMock());
		public CardMock CardSource => _cardSource ?? (_cardSource = new CardMock());
		public CheckItemMock CheckItem => _checkItem ?? (_checkItem = new CheckItemMock());
		public CheckListMock CheckList => _checkList ?? (_checkList = new CheckListMock());
		public ListMock List => _list ?? (_list = new ListMock());
		public ListMock ListAfter => _listAfter ?? (_listAfter = new ListMock());
		public ListMock ListBefore => _listBefore ?? (_listBefore = new ListMock());
		public MemberMock Member => _member ?? (_member = new MemberMock());
		public ListMock OldList => _oldList ?? (_oldList = new ListMock());
		public PositionMock OldPosition => _oldPosition ?? (_oldPosition = new PositionMock());
		public OrganizationMock Organization => _organization ?? (_organization = new OrganizationMock());
		public PowerUpMock PowerUp => _powerUp ?? (_powerUp = new PowerUpMock());

		public ActionDataMock()
			: base(null)
		{
			SetupGet(a => a.Attachment).Returns(() => Attachment.Object);
			SetupGet(a => a.Board).Returns(() => Board.Object);
			SetupGet(a => a.BoardSource).Returns(() => BoardSource.Object);
			SetupGet(a => a.BoardTarget).Returns(() => BoardTarget.Object);
			SetupGet(a => a.Card).Returns(() => Card.Object);
			SetupGet(a => a.CardSource).Returns(() => CardSource.Object);
			SetupGet(a => a.CheckItem).Returns(() => CheckItem.Object);
			SetupGet(a => a.CheckList).Returns(() => CheckList.Object);
			SetupGet(a => a.List).Returns(() => List.Object);
			SetupGet(a => a.ListAfter).Returns(() => ListAfter.Object);
			SetupGet(a => a.ListBefore).Returns(() => ListBefore.Object);
			SetupGet(a => a.Member).Returns(() => Member.Object);
			SetupGet(a => a.OldList).Returns(() => OldList.Object);
			SetupGet(a => a.OldPosition).Returns(() => OldPosition.Object);
			SetupGet(a => a.Organization).Returns(() => Organization.Object);
			SetupGet(a => a.PowerUp).Returns(() => PowerUp.Object);
		}
	}
}
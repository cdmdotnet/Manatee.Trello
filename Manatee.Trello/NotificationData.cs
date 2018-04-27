using Manatee.Trello.Internal;
using Manatee.Trello.Internal.Synchronization;
using Manatee.Trello.Internal.Validation;

namespace Manatee.Trello
{
	/// <summary>
	/// Exposes any data associated with a notification.
	/// </summary>
	public class NotificationData : INotificationData
	{
		private readonly Field<Attachment> _attachment;
		private readonly Field<Board> _board;
		private readonly Field<Board> _boardSource;
		private readonly Field<Board> _boardTarget;
		private readonly Field<Card> _card;
		private readonly Field<Card> _cardSource;
		private readonly Field<CheckItem> _checkItem;
		private readonly Field<CheckList> _checkList;
		private readonly Field<List> _list;
		private readonly Field<List> _listAfter;
		private readonly Field<List> _listBefore;
		private readonly Field<Member> _member;
		private readonly Field<bool?> _wasArchived;
		private readonly Field<string> _oldDescription;
		private readonly Field<List> _oldList;
		private readonly Field<Position> _oldPosition;
		private readonly Field<string> _oldText;
		private readonly Field<Organization> _organization;
		private readonly Field<string> _text;
		private readonly NotificationDataContext _context;

		/// <summary>
		/// Gets an assocated attachment.
		/// </summary>
		/// <associated-notification-types>
		/// - AddedAttachmentToCard
		/// </associated-notification-types>
		public IAttachment Attachment => _attachment.Value;
		/// <summary>
		/// Gets an assocated board.
		/// </summary>
		/// <associated-notification-types>
		/// - AddedToBoard
		/// - AddAdminToBoard
		/// - CloseBoard
		/// - RemovedFromBoard
		/// - MakeAdminOfBoard
		/// </associated-notification-types>
		public IBoard Board => _board.Value;
		/// <summary>
		/// Gets an assocated board.
		/// </summary>
		public IBoard BoardSource => _boardSource.Value;
		/// <summary>
		/// Gets an assocated board.
		/// </summary>
		public IBoard BoardTarget => _boardTarget.Value;
		/// <summary>
		/// Gets an assocated card.
		/// </summary>
		/// <associated-notification-types>
		/// - AddedAttachmentToCard
		/// - AddedToCard
		/// - AddedMemberToCard
		/// - ChangeCard
		/// - CommentCard
		/// - CreatedCard
		/// - RemovedFromCard
		/// - RemovedMemberFromCard
		/// - MentionedOnCard
		/// - UpdateCheckItemStateOnCard
		/// - CardDueSoon
		/// </associated-notification-types>
		public ICard Card => _card.Value;
		/// <summary>
		/// Gets an assocated card.
		/// </summary>
		public ICard CardSource => _cardSource.Value;
		/// <summary>
		/// Gets an assocated checklist item.
		/// </summary>
		/// <associated-notification-types>
		/// - UpdateCheckItemStateOnCard
		/// </associated-notification-types>
		public ICheckItem CheckItem => _checkItem.Value;
		/// <summary>
		/// Gets an assocated checklist.
		/// </summary>
		public ICheckList CheckList => _checkList.Value;
		/// <summary>
		/// Gets an assocated list.
		/// </summary>
		public IList List => _list.Value;
		/// <summary>
		/// Gets the current list.
		/// </summary>
		/// <remarks>
		/// For some action types, this information may be in the <see cref="List"/> or <see cref="OldList"/> properties.
		/// </remarks>
		public IList ListAfter => _listAfter.Value;
		/// <summary>
		/// Gets the previous list.
		/// </summary>
		/// <remarks>
		/// For some action types, this information may be in the <see cref="List"/> or <see cref="OldList"/> properties.
		/// </remarks>
		public IList ListBefore => _listBefore.Value;
		/// <summary>
		/// Gets an assocated member.
		/// </summary>
		/// <associated-notification-types>
		/// - AddedMemberToCard
		/// - RemovedMemberFromCard
		/// - MentionedOnCard
		/// </associated-notification-types>
		public IMember Member => _member.Value;
		/// <summary>
		/// Gets the previous description.
		/// </summary>
		/// <associated-notification-types>
		/// - ChangeCard
		/// </associated-notification-types>
		public string OldDescription => _oldDescription.Value;
		/// <summary>
		/// Gets the previous list.
		/// </summary>
		/// <remarks>
		/// For some action types, this information may be in the <see cref="ListAfter"/> or <see cref="ListBefore"/> properties.
		/// </remarks>
		public IList OldList => _oldList.Value;
		/// <summary>
		/// Gets the previous position.
		/// </summary>
		/// <associated-notification-types>
		/// - ChangeCard
		/// </associated-notification-types>
		public Position OldPosition => _oldPosition.Value;
		/// <summary>
		/// Gets the previous text value. 
		/// </summary>
		/// <associated-notification-types>
		/// - CommentCard
		/// </associated-notification-types>
		public string OldText => _oldText.Value;
		/// <summary>
		/// Gets an assocated organization.
		/// </summary>
		/// <associated-notification-types>
		/// - AddedToOrganization
		/// - AddAdminToOrganization
		/// - RemovedFromOrganization
		/// - MakeAdminOfOrganization
		/// </associated-notification-types>
		public IOrganization Organization => _organization.Value;
		/// <summary>
		/// Gets assocated text.
		/// </summary>
		/// <associated-notification-types>
		/// - CommentCard
		/// </associated-notification-types>
		public string Text
		{
			get { return _text.Value; }
			set { _text.Value = value; }
		}
		/// <summary>
		/// Gets whether the object was previously archived.
		/// </summary>
		/// <associated-notification-types>
		/// - ChangeCard
		/// - CloseBoard
		/// </associated-notification-types>
		public bool? WasArchived => _wasArchived.Value;

		internal NotificationData(NotificationDataContext context)
		{
			_context = context;

			_attachment = new Field<Attachment>(_context, nameof(Attachment));
			_board = new Field<Board>(_context, nameof(Board));
			_boardSource = new Field<Board>(_context, nameof(BoardSource));
			_boardTarget = new Field<Board>(_context, nameof(BoardTarget));
			_card = new Field<Card>(_context, nameof(Card));
			_cardSource = new Field<Card>(_context, nameof(CardSource));
			_checkItem = new Field<CheckItem>(_context, nameof(CheckItem));
			_checkList = new Field<CheckList>(_context, nameof(CheckList));
			_list = new Field<List>(_context, nameof(List));
			_listAfter = new Field<List>(_context, nameof(ListAfter));
			_listBefore = new Field<List>(_context, nameof(ListBefore));
			_member = new Field<Member>(_context, nameof(Member));
			_wasArchived = new Field<bool?>(_context, nameof(WasArchived));
			_oldDescription = new Field<string>(_context, nameof(OldDescription));
			_oldList = new Field<List>(_context, nameof(OldList));
			_oldPosition = new Field<Position>(_context, nameof(OldPosition));
			_oldText = new Field<string>(_context, nameof(OldText));
			_organization = new Field<Organization>(_context, nameof(Organization));
			_text = new Field<string>(_context, nameof(Text));
			_text.AddRule(OldValueNotNullOrWhiteSpaceRule.Instance);
		}
	}
}
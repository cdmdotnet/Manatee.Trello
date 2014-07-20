// TODO: add file headers
using Manatee.Trello.Internal;
using Manatee.Trello.Internal.Synchronization;
using Manatee.Trello.Internal.Validation;

namespace Manatee.Trello
{
	public class ActionData
	{
		private readonly Field<Attachment> _attachment;
		private readonly Field<Board> _board;
		private readonly Field<Card> _card;
		private readonly Field<CheckItem> _checkItem;
		private readonly Field<CheckList> _checkList;
		private readonly Field<List> _list;
		private readonly Field<List> _listAfter;
		private readonly Field<List> _listBefore;
		private readonly Field<Member> _member;
		private readonly Field<bool?> _oldClosed;
		private readonly Field<string> _oldDescription;
		private readonly Field<List> _oldList;
		private readonly Field<Position> _oldPosition;
		private readonly Field<string> _oldText;
		private readonly Field<Organization> _organization;
		private readonly Field<string> _text;
		private readonly ActionDataContext _context;

		public Attachment Attachment { get { return _attachment.Value; } }
		public Board Board { get { return _board.Value; } }
		public Card Card { get { return _card.Value; } }
		public CheckItem CheckItem { get { return _checkItem.Value; } }
		public CheckList CheckList { get { return _checkList.Value; } }
		public List List { get { return _list.Value; } }
		public List ListAfter { get { return _listAfter.Value; } }
		public List ListBefore { get { return _listBefore.Value; } }
		public Member Member { get { return _member.Value; } }
		public bool? OldClosed { get { return _oldClosed.Value; } }
		public string OldDescription { get { return _oldDescription.Value; } }
		public List OldList { get { return _oldList.Value; } }
		public Position OldPosition { get { return _oldPosition.Value; } }
		public string OldText { get { return _oldText.Value; } }
		public Organization Organization { get { return _organization.Value; } }
		public string Text
		{
			get { return _text.Value; }
			set { _text.Value = value; }
		}

		internal ActionData(ActionDataContext context)
		{
			_context = context;

			_attachment = new Field<Attachment>(_context, () => Attachment);
			_board = new Field<Board>(_context, () => Board);
			_card = new Field<Card>(_context, () => Card);
			_checkItem = new Field<CheckItem>(_context, () => CheckItem);
			_checkList = new Field<CheckList>(_context, () => CheckList);
			_list = new Field<List>(_context, () => List);
			_listAfter = new Field<List>(_context, () => ListAfter);
			_listBefore = new Field<List>(_context, () => ListBefore);
			_member = new Field<Member>(_context, () => Member);
			_oldClosed = new Field<bool?>(_context, () => OldClosed);
			_oldDescription = new Field<string>(_context, () => OldDescription);
			_oldList = new Field<List>(_context, () => OldList);
			_oldPosition = new Field<Position>(_context, () => OldPosition);
			_oldText = new Field<string>(_context, () => OldText);
			_organization = new Field<Organization>(_context, () => Organization);
			_text = new Field<string>(_context, () => Text);
			_text.AddRule(OldValueNotNullOrWhiteSpaceRule.Instance);
		}
	}
}
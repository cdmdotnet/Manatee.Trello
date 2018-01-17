using System;
using Manatee.Trello.Internal;
using Manatee.Trello.Internal.Synchronization;
using Manatee.Trello.Internal.Validation;

namespace Manatee.Trello
{
	public interface IActionData
	{
		/// <summary>
		/// Gets an assocated attachment.
		/// </summary>
		IAttachment Attachment { get; }

		/// <summary>
		/// Gets an assocated board.
		/// </summary>
		IBoard Board { get; }

		/// <summary>
		/// Gets an assocated board.
		/// </summary>
		IBoard BoardSource { get; }

		/// <summary>
		/// Gets an assocated board.
		/// </summary>
		IBoard BoardTarget { get; }

		/// <summary>
		/// Gets an assocated card.
		/// </summary>
		ICard Card { get; }

		/// <summary>
		/// Gets an assocated card.
		/// </summary>
		ICard CardSource { get; }

		/// <summary>
		/// Gets an assocated checklist item.
		/// </summary>
		ICheckItem CheckItem { get; }

		/// <summary>
		/// Gets an assocated checklist.
		/// </summary>
		ICheckList CheckList { get; }

		/// <summary>
		/// Gets the date/time a comment was last edited.
		/// </summary>
		DateTime? LastEdited { get; }

		/// <summary>
		/// Gets an assocated list.
		/// </summary>
		IList List { get; }

		/// <summary>
		/// Gets the current list.
		/// </summary>
		/// <remarks>
		/// For some action types, this information may be in the <see cref="List"/>
		/// or <see cref="OldList"/> properties.
		/// </remarks>
		IList ListAfter { get; }

		/// <summary>
		/// Gets the previous list.
		/// </summary>
		/// <remarks>
		/// For some action types, this information may be in the <see cref="List"/>
		/// or <see cref="OldList"/> properties.
		/// </remarks>
		IList ListBefore { get; }

		/// <summary>
		/// Gets an assocated member.
		/// </summary>
		IMember Member { get; }

		/// <summary>
		/// Gets the previous description.
		/// </summary>
		string OldDescription { get; }

		/// <summary>
		/// Gets the previous list.
		/// </summary>
		/// <remarks>
		/// For some action types, this information may be in the <see cref="ListAfter"/>
		/// or <see cref="ListBefore"/> properties.
		/// </remarks>
		IList OldList { get; }

		/// <summary>
		/// Gets the previous position.
		/// </summary>
		Position OldPosition { get; }

		/// <summary>
		/// Gets the previous text value. 
		/// </summary>
		string OldText { get; }

		/// <summary>
		/// Gets an associated organization.
		/// </summary>
		IOrganization Organization { get; }

		/// <summary>
		/// Gets an associated power-up.
		/// </summary>
		IPowerUp PowerUp { get; }

		/// <summary>
		/// Gets assocated text.
		/// </summary>
		string Text { get; set; }

		/// <summary>
		/// Gets whether the object was previously archived.
		/// </summary>
		bool? WasArchived { get; }

		/// <summary>
		/// Gets a custom value associate with the action if any.
		/// </summary>
		string Value { get; }
	}

	/// <summary>
	/// Exposes any data associated with an action.
	/// </summary>
	public class ActionData : IActionData
	{
		private readonly Field<Attachment> _attachment;
		private readonly Field<Board> _board;
		private readonly Field<Board> _boardSource;
		private readonly Field<Board> _boardTarget;
		private readonly Field<Card> _card;
		private readonly Field<Card> _cardSource;
		private readonly Field<CheckItem> _checkItem;
		private readonly Field<CheckList> _checkList;
		private readonly Field<DateTime?> _lastEdited;
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
		private readonly Field<PowerUpBase> _powerUp;
		private readonly Field<string> _text;
		private readonly Field<string> _value;
		private readonly ActionDataContext _context;

		/// <summary>
		/// Gets an assocated attachment.
		/// </summary>
		public IAttachment Attachment => _attachment.Value;
		/// <summary>
		/// Gets an assocated board.
		/// </summary>
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
		public ICard Card => _card.Value;
		/// <summary>
		/// Gets an assocated card.
		/// </summary>
		public ICard CardSource => _cardSource.Value;
		/// <summary>
		/// Gets an assocated checklist item.
		/// </summary>
		public ICheckItem CheckItem => _checkItem.Value;
		/// <summary>
		/// Gets an assocated checklist.
		/// </summary>
		public ICheckList CheckList => _checkList.Value;
		/// <summary>
		/// Gets the date/time a comment was last edited.
		/// </summary>
		public DateTime? LastEdited => _lastEdited.Value;
		/// <summary>
		/// Gets an assocated list.
		/// </summary>
		public IList List => _list.Value;
		/// <summary>
		/// Gets the current list.
		/// </summary>
		/// <remarks>
		/// For some action types, this information may be in the <see cref="List"/>
		/// or <see cref="OldList"/> properties.
		/// </remarks>
		public IList ListAfter => _listAfter.Value;
		/// <summary>
		/// Gets the previous list.
		/// </summary>
		/// <remarks>
		/// For some action types, this information may be in the <see cref="List"/>
		/// or <see cref="OldList"/> properties.
		/// </remarks>
		public IList ListBefore => _listBefore.Value;
		/// <summary>
		/// Gets an assocated member.
		/// </summary>
		public IMember Member => _member.Value;
		/// <summary>
		/// Gets the previous description.
		/// </summary>
		public string OldDescription => _oldDescription.Value;
		/// <summary>
		/// Gets the previous list.
		/// </summary>
		/// <remarks>
		/// For some action types, this information may be in the <see cref="ListAfter"/>
		/// or <see cref="ListBefore"/> properties.
		/// </remarks>
		public IList OldList => _oldList.Value;
		/// <summary>
		/// Gets the previous position.
		/// </summary>
		public Position OldPosition => _oldPosition.Value;
		/// <summary>
		/// Gets the previous text value. 
		/// </summary>
		public string OldText => _oldText.Value;
		/// <summary>
		/// Gets an associated organization.
		/// </summary>
		public IOrganization Organization => _organization.Value;
		/// <summary>
		/// Gets an associated power-up.
		/// </summary>
		public IPowerUp PowerUp => _powerUp.Value;
		/// <summary>
		/// Gets assocated text.
		/// </summary>
		public string Text
		{
			get { return _text.Value; }
			set { _text.Value = value; }
		}
		/// <summary>
		/// Gets whether the object was previously archived.
		/// </summary>
		public bool? WasArchived => _wasArchived.Value;
		/// <summary>
		/// Gets a custom value associate with the action if any.
		/// </summary>
		public string Value => _value.Value;

		internal ActionData(ActionDataContext context)
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
			_lastEdited = new Field<DateTime?>(_context, nameof(LastEdited));
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
			_powerUp = new Field<PowerUpBase>(_context, nameof(PowerUp));
			_text = new Field<string>(_context, nameof(Text));
			_text.AddRule(OldValueNotNullOrWhiteSpaceRule.Instance);
			_value = new Field<string>(_context, nameof(Value));
		}
	}
}
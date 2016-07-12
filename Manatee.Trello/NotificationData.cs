/***************************************************************************************

	Copyright 2015 Greg Dennis

	   Licensed under the Apache License, Version 2.0 (the "License");
	   you may not use this file except in compliance with the License.
	   You may obtain a copy of the License at

		 http://www.apache.org/licenses/LICENSE-2.0

	   Unless required by applicable law or agreed to in writing, software
	   distributed under the License is distributed on an "AS IS" BASIS,
	   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
	   See the License for the specific language governing permissions and
	   limitations under the License.
 
	File Name:		NotificationData.cs
	Namespace:		Manatee.Trello
	Class Name:		NotificationData
	Purpose:		Exposes any data associated with a notification.

***************************************************************************************/

using Manatee.Trello.Internal;
using Manatee.Trello.Internal.Synchronization;
using Manatee.Trello.Internal.Validation;

namespace Manatee.Trello
{
	/// <summary>
	/// Exposes any data associated with a notification.
	/// </summary>
	public class NotificationData
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
		public Attachment Attachment => _attachment.Value;
		/// <summary>
		/// Gets an assocated board.
		/// </summary>
		public Board Board => _board.Value;
		/// <summary>
		/// Gets an assocated board.
		/// </summary>
		public Board BoardSource => _boardSource.Value;
		/// <summary>
		/// Gets an assocated board.
		/// </summary>
		public Board BoardTarget => _boardTarget.Value;
		/// <summary>
		/// Gets an assocated card.
		/// </summary>
		public Card Card => _card.Value;
		/// <summary>
		/// Gets an assocated card.
		/// </summary>
		public Card CardSource => _cardSource.Value;
		/// <summary>
		/// Gets an assocated checklist item.
		/// </summary>
		public CheckItem CheckItem => _checkItem.Value;
		/// <summary>
		/// Gets an assocated checklist.
		/// </summary>
		public CheckList CheckList => _checkList.Value;
		/// <summary>
		/// Gets an assocated list.
		/// </summary>
		public List List => _list.Value;
		/// <summary>
		/// Gets the current list.
		/// </summary>
		/// <remarks>
		/// For some action types, this information may be in the <see cref="List"/>
		/// or <see cref="OldList"/> properties.
		/// </remarks>
		public List ListAfter => _listAfter.Value;
		/// <summary>
		/// Gets the previous list.
		/// </summary>
		/// <remarks>
		/// For some action types, this information may be in the <see cref="List"/>
		/// or <see cref="OldList"/> properties.
		/// </remarks>
		public List ListBefore => _listBefore.Value;
		/// <summary>
		/// Gets an assocated member.
		/// </summary>
		public Member Member => _member.Value;
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
		public List OldList => _oldList.Value;
		/// <summary>
		/// Gets the previous position.
		/// </summary>
		public Position OldPosition => _oldPosition.Value;
		/// <summary>
		/// Gets the previous text value. 
		/// </summary>
		public string OldText => _oldText.Value;
		/// <summary>
		/// Gets an assocated organization.
		/// </summary>
		public Organization Organization => _organization.Value;
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
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
 
	File Name:		ActionData.cs
	Namespace:		Manatee.Trello
	Class Name:		ActionData
	Purpose:		Exposes any data associated with an action.

***************************************************************************************/

using System;
using Manatee.Trello.Internal;
using Manatee.Trello.Internal.Synchronization;
using Manatee.Trello.Internal.Validation;

namespace Manatee.Trello
{
	/// <summary>
	/// Exposes any data associated with an action.
	/// </summary>
	public class ActionData
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
		private readonly Field<string> _text;
		private readonly Field<string> _value;
		private readonly ActionDataContext _context;

		/// <summary>
		/// Gets an assocated attachment.
		/// </summary>
		public Attachment Attachment { get { return _attachment.Value; } }
		/// <summary>
		/// Gets an assocated board.
		/// </summary>
		public Board Board { get { return _board.Value; } }
		/// <summary>
		/// Gets an assocated board.
		/// </summary>
		public Board BoardSource { get { return _boardSource.Value; } }
		/// <summary>
		/// Gets an assocated board.
		/// </summary>
		public Board BoardTarget { get { return _boardTarget.Value; } }
		/// <summary>
		/// Gets an assocated card.
		/// </summary>
		public Card Card { get { return _card.Value; } }
		/// <summary>
		/// Gets an assocated card.
		/// </summary>
		public Card CardSource { get { return _cardSource.Value; } }
		/// <summary>
		/// Gets an assocated checklist item.
		/// </summary>
		public CheckItem CheckItem { get { return _checkItem.Value; } }
		/// <summary>
		/// Gets an assocated checklist.
		/// </summary>
		public CheckList CheckList { get { return _checkList.Value; } }
		/// <summary>
		/// Gets the date/time a comment was last edited.
		/// </summary>
		public DateTime? LastEdited { get { return _lastEdited.Value; } }
		/// <summary>
		/// Gets an assocated list.
		/// </summary>
		public List List { get { return _list.Value; } }
		/// <summary>
		/// Gets the current list.
		/// </summary>
		/// <remarks>
		/// For some action types, this information may be in the <see cref="List"/>
		/// or <see cref="OldList"/> properties.
		/// </remarks>
		public List ListAfter { get { return _listAfter.Value; } }
		/// <summary>
		/// Gets the previous list.
		/// </summary>
		/// <remarks>
		/// For some action types, this information may be in the <see cref="List"/>
		/// or <see cref="OldList"/> properties.
		/// </remarks>
		public List ListBefore { get { return _listBefore.Value; } }
		/// <summary>
		/// Gets an assocated member.
		/// </summary>
		public Member Member { get { return _member.Value; } }
		/// <summary>
		/// Gets the previous description.
		/// </summary>
		public string OldDescription { get { return _oldDescription.Value; } }
		/// <summary>
		/// Gets the previous list.
		/// </summary>
		/// <remarks>
		/// For some action types, this information may be in the <see cref="ListAfter"/>
		/// or <see cref="ListBefore"/> properties.
		/// </remarks>
		public List OldList { get { return _oldList.Value; } }
		/// <summary>
		/// Gets the previous position.
		/// </summary>
		public Position OldPosition { get { return _oldPosition.Value; } }
		/// <summary>
		/// Gets the previous text value. 
		/// </summary>
		public string OldText { get { return _oldText.Value; } }
		/// <summary>
		/// Gets an assocated organization.
		/// </summary>
		public Organization Organization { get { return _organization.Value; } }
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
		public bool? WasArchived { get { return _wasArchived.Value; } }
		/// <summary>
		/// Gets a custom value associate with the action if any.
		/// </summary>
		public string Value { get { return _value.Value; } }

		internal ActionData(ActionDataContext context)
		{
			_context = context;

			_attachment = new Field<Attachment>(_context, () => Attachment);
			_board = new Field<Board>(_context, () => Board);
			_boardSource = new Field<Board>(_context, () => BoardSource);
			_boardTarget = new Field<Board>(_context, () => BoardTarget);
			_card = new Field<Card>(_context, () => Card);
			_cardSource = new Field<Card>(_context, () => CardSource);
			_checkItem = new Field<CheckItem>(_context, () => CheckItem);
			_checkList = new Field<CheckList>(_context, () => CheckList);
			_lastEdited = new Field<DateTime?>(_context, () => LastEdited);
			_list = new Field<List>(_context, () => List);
			_listAfter = new Field<List>(_context, () => ListAfter);
			_listBefore = new Field<List>(_context, () => ListBefore);
			_member = new Field<Member>(_context, () => Member);
			_wasArchived = new Field<bool?>(_context, () => WasArchived);
			_oldDescription = new Field<string>(_context, () => OldDescription);
			_oldList = new Field<List>(_context, () => OldList);
			_oldPosition = new Field<Position>(_context, () => OldPosition);
			_oldText = new Field<string>(_context, () => OldText);
			_organization = new Field<Organization>(_context, () => Organization);
			_text = new Field<string>(_context, () => Text);
			_text.AddRule(OldValueNotNullOrWhiteSpaceRule.Instance);
			_value = new Field<string>(_context, () => Value);
		}
	}
}
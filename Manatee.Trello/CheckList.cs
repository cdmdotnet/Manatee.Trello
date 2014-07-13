/***************************************************************************************

	Copyright 2014 Greg Dennis

	   Licensed under the Apache License, Version 2.0 (the "License");
	   you may not use this file except in compliance with the License.
	   You may obtain a copy of the License at

		 http://www.apache.org/licenses/LICENSE-2.0

	   Unless required by applicable law or agreed to in writing, software
	   distributed under the License is distributed on an "AS IS" BASIS,
	   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
	   See the License for the specific language governing permissions and
	   limitations under the License.
 
	File Name:		CheckList.cs
	Namespace:		Manatee.Trello
	Class Name:		CheckList
	Purpose:		Represents a checklist.

***************************************************************************************/

using Manatee.Trello.Internal;
using Manatee.Trello.Internal.Synchronization;
using Manatee.Trello.Internal.Validation;
using Manatee.Trello.Json;

namespace Manatee.Trello
{
	public class CheckList
	{
		private readonly Field<Board> _board;
		private readonly Field<Card> _card;
		private readonly CheckItemCollection _checkItems;
		private readonly string _id;
		private readonly Field<string> _name;
		private readonly Field<Position> _position;
		private readonly CheckListContext _context;
		private bool _deleted;

		public Board Board { get { return _board.Value; } }
		public Card Card
		{
			get { return _card.Value; }
			set { _card.Value = value; }
		}
		public CheckItemCollection CheckItems { get { return _checkItems; } }
		public string Id { get { return _id; } }
		public string Name
		{
			get { return _name.Value; }
			set { _name.Value = value; }
		}
		public Position Position
		{
			get { return _position.Value; }
			set { _position.Value = value; }
		}

		public CheckList(string id)
		{
			_context = new CheckListContext(id);

			_board = new Field<Board>(_context, () => Board);
			_card = new Field<Card>(_context, () => Card);
			_card.AddRule(NotNullRule<Card>.Instance);
			_checkItems = new CheckItemCollection(id);
			_id = id;
			_name = new Field<string>(_context, () => Name);
			_name.AddRule(NotNullOrWhiteSpaceRule.Instance);
			_position = new Field<Position>(_context, () => Position);

			TrelloConfiguration.Cache.Add(this);
		}
		internal CheckList(IJsonCheckList json)
			: this(json.Id)
		{
			_context.Merge(json);
		}

		public void Delete()
		{
			if (_deleted) return;

			_context.Delete();
			_deleted = true;
			TrelloConfiguration.Cache.Remove(this);
		}
	}
}
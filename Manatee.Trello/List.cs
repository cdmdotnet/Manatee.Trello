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
 
	File Name:		List2.cs
	Namespace:		Manatee.Trello
	Class Name:		List2
	Purpose:		Represents a list.

***************************************************************************************/

using Manatee.Trello.Internal;
using Manatee.Trello.Internal.Synchronization;
using Manatee.Trello.Internal.Validation;
using Manatee.Trello.Json;

namespace Manatee.Trello
{
	public class List
	{
		private readonly Field<Board> _board;
		private readonly CardCollection _cards;
		private readonly string _id;
		private readonly Field<bool?> _isArchived;
		private readonly Field<bool?> _isSubscribed;
		private readonly Field<string> _name;
		private readonly Field<Position> _position;
		private readonly ListContext _context;

		public Board Board
		{
			get { return _board.Value; }
			set { _board.Value = value; }
		}
		public CardCollection Cards
		{
			get { return _cards; }
		}
		public string Id {get { return _id; } }
		public bool? IsArchived
		{
			get { return _isArchived.Value; }
			set { _isArchived.Value = value; }
		}
		public bool? IsSubscribed
		{
			get { return _isSubscribed.Value; }
			set { _isSubscribed.Value = value; }
		}
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

		internal IJsonList Json { get { return _context.Data; } }

		public List(string id)
		{
			_context = new ListContext(id);

			_board = new Field<Board>(_context, () => Board);
			_cards = new CardCollection(id);
			_id = id;
			_isArchived = new Field<bool?>(_context, () => IsArchived);
			_isArchived.AddRule(NullableHasValueRule<bool>.Instance);
			_isSubscribed = new Field<bool?>(_context, () => IsSubscribed);
			_isSubscribed.AddRule(NullableHasValueRule<bool>.Instance);
			_name = new Field<string>(_context, () => Name);
			_name.AddRule(NotNullOrWhiteSpaceRule.Instance);
			_position = new Field<Position>(_context, () => Position);
			_position.AddRule(NotNullRule<Position>.Instance);
			_position.AddRule(PositionRule.Instance);

			TrelloConfiguration.Cache.Add(this);
		}
		internal List(IJsonList json)
			: this(json.Id)
		{
			_context.Merge(json);
		}
	}
}
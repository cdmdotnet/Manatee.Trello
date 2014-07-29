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

using System;
using System.Collections.Generic;
using Manatee.Trello.Contracts;
using Manatee.Trello.Internal;
using Manatee.Trello.Internal.Synchronization;
using Manatee.Trello.Internal.Validation;
using Manatee.Trello.Json;

namespace Manatee.Trello
{
	/// <summary>
	/// Represents a checklist.
	/// </summary>
	public class CheckList : ICacheable
	{
		private readonly Field<Board> _board;
		private readonly Field<Card> _card;
		private readonly Field<string> _name;
		private readonly Field<Position> _position;
		private readonly CheckListContext _context;

		/// <summary>
		/// Gets the board on which the checklist belongs.
		/// </summary>
		public Board Board { get { return _board.Value; } }
		/// <summary>
		/// Gets or sets the card on which the checklist belongs.
		/// </summary>
		public Card Card
		{
			get { return _card.Value; }
			set { _card.Value = value; }
		}
		/// <summary>
		/// Gets the collection of items in the checklist.
		/// </summary>
		public CheckItemCollection CheckItems { get; private set; }
		/// <summary>
		/// Gets the checklist's ID.
		/// </summary>
		public string Id { get; private set; }
		/// <summary>
		/// Gets the checklist's name.
		/// </summary>
		public string Name
		{
			get { return _name.Value; }
			set { _name.Value = value; }
		}
		/// <summary>
		/// Gets the checklist's position.
		/// </summary>
		public Position Position
		{
			get { return _position.Value; }
			set { _position.Value = value; }
		}

		internal IJsonCheckList Json
		{
			get { return _context.Data; }
			set { _context.Merge(value); }
		}

		/// <summary>
		/// Raised when data on the checklist is updated.
		/// </summary>
		public event Action<CheckList, IEnumerable<string>> Updated;

		/// <summary>
		/// Creates a new instance of the <see cref="CheckList"/> object.
		/// </summary>
		/// <param name="id">The checklist's ID.</param>
		public CheckList(string id)
			: this(id, true) {}
		internal CheckList(IJsonCheckList json, bool cache)
			: this(json.Id, cache)
		{
			_context.Merge(json);
		}
		private CheckList(string id, bool cache)
		{
			Id = id;
			_context = new CheckListContext(id);
			_context.Synchronized += Synchronized;

			_board = new Field<Board>(_context, () => Board);
			_card = new Field<Card>(_context, () => Card);
			_card.AddRule(NotNullRule<Card>.Instance);
			CheckItems = new CheckItemCollection(_context);
			_name = new Field<string>(_context, () => Name);
			_name.AddRule(NotNullOrWhiteSpaceRule.Instance);
			_position = new Field<Position>(_context, () => Position);
			_position.AddRule(NotNullRule<Position>.Instance);
			_position.AddRule(PositionRule.Instance);

			if (cache)
				TrelloConfiguration.Cache.Add(this);
		}

		/// <summary>
		/// Deletes the checklist.
		/// </summary>
		/// <remarks>
		/// This permanently deletes the checklist from Trello's server, however, this object
		/// will remain in memory and all properties will remain accessible.
		/// </remarks>
		public void Delete()
		{
			_context.Delete();
			TrelloConfiguration.Cache.Remove(this);
		}
		/// <summary>
		/// Marks the checklist to be refreshed the next time data is accessed.
		/// </summary>
		public void Refresh()
		{
			_context.Expire();
		}
		/// <summary>
		/// Returns a string that represents the current object.
		/// </summary>
		/// <returns>
		/// A string that represents the current object.
		/// </returns>
		/// <filterpriority>2</filterpriority>
		public override string ToString()
		{
			return Name;
		}

		private void Synchronized(IEnumerable<string> properties)
		{
			Id = _context.Data.Id;
			var handler = Updated;
			if (handler != null)
				handler(this, properties);
		}
	}
}
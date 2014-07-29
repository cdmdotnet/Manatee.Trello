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
	/// Represents a list.
	/// </summary>
	public class List : ICanWebhook, ICacheable
	{
		private readonly Field<Board> _board;
		private readonly Field<bool?> _isArchived;
		private readonly Field<bool?> _isSubscribed;
		private readonly Field<string> _name;
		private readonly Field<Position> _position;
		private readonly ListContext _context;

		private string _id;

		/// <summary>
		/// Gets the collection of actions performed on the list.
		/// </summary>
		public ReadOnlyActionCollection Actions { get; private set; }
		/// <summary>
		/// Gets or sets the board on which the list belongs.
		/// </summary>
		public Board Board
		{
			get { return _board.Value; }
			set { _board.Value = value; }
		}
		/// <summary>
		/// Gets the collection of cards contained in the list.
		/// </summary>
		public CardCollection Cards { get; private set; }
		/// <summary>
		/// Gets the list's ID.
		/// </summary>
		public string Id
		{
			get
			{
				if (!_context.IsDataComplete)
					_context.Synchronize();
				return _id;
			}
			private set { _id = value; }
		}
		/// <summary>
		/// Gets or sets whether the list is archived.
		/// </summary>
		public bool? IsArchived
		{
			get { return _isArchived.Value; }
			set { _isArchived.Value = value; }
		}
		/// <summary>
		/// Gets or sets whether the current member is subscribed to the list.
		/// </summary>
		public bool? IsSubscribed
		{
			get { return _isSubscribed.Value; }
			set { _isSubscribed.Value = value; }
		}
		/// <summary>
		/// Gets the list's name.
		/// </summary>
		public string Name
		{
			get { return _name.Value; }
			set { _name.Value = value; }
		}
		/// <summary>
		/// Gets the list's position.
		/// </summary>
		public Position Position
		{
			get { return _position.Value; }
			set { _position.Value = value; }
		}

		internal IJsonList Json
		{
			get { return _context.Data; }
			set { _context.Merge(value); }
		}

		/// <summary>
		/// Raised when data on the list is updated.
		/// </summary>
		public event Action<List, IEnumerable<string>> Updated;

		/// <summary>
		/// Creates a new instance of the <see cref="List"/> object.
		/// </summary>
		/// <param name="id">The list's ID.</param>
		public List(string id)
			: this(id, true) {}
		internal List(IJsonList json, bool cache)
			: this(json.Id, cache)
		{
			_context.Merge(json);
		}
		private List(string id, bool cache)
		{
			Id = id;
			_context = new ListContext(id);
			_context.Synchronized += Synchronized;

			Actions = new ReadOnlyActionCollection(typeof(List), id);
			_board = new Field<Board>(_context, () => Board);
			_board.AddRule(NotNullRule<Board>.Instance);
			Cards = new CardCollection(id);
			_isArchived = new Field<bool?>(_context, () => IsArchived);
			_isArchived.AddRule(NullableHasValueRule<bool>.Instance);
			_isSubscribed = new Field<bool?>(_context, () => IsSubscribed);
			_isSubscribed.AddRule(NullableHasValueRule<bool>.Instance);
			_name = new Field<string>(_context, () => Name);
			_name.AddRule(NotNullOrWhiteSpaceRule.Instance);
			_position = new Field<Position>(_context, () => Position);
			_position.AddRule(NotNullRule<Position>.Instance);
			_position.AddRule(PositionRule.Instance);

			if (cache)
				TrelloConfiguration.Cache.Add(this);
		}

		/// <summary>
		/// Applies the changes an action represents.
		/// </summary>
		/// <param name="action">The action.</param>
		public void ApplyAction(Action action)
		{
			if (action.Type != ActionType.UpdateList || action.Data.List == null || action.Data.List.Id != Id) return;
			_context.Merge(action.Data.List.Json);
		}
		/// <summary>
		/// Marks the list to be refreshed the next time data is accessed.
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
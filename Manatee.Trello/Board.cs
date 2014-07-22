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
 
	File Name:		Board2.cs
	Namespace:		Manatee.Trello
	Class Name:		Board2
	Purpose:		Represents a board.

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
	public class Board : ICanWebhook, IQueryable
	{
		private readonly Field<string> _description;
		private readonly Field<bool?> _isClosed;
		private readonly Field<bool?> _isPinned;
		private readonly Field<bool?> _isSubscribed;
		private readonly Field<string> _name;
		private readonly Field<Organization> _organization;
		private readonly Field<string> _url;
		private readonly BoardContext _context;

		public ReadOnlyActionCollection Actions { get; private set; }
		public ReadOnlyCardCollection Cards { get; private set; }
		public string Description
		{
			get { return _description.Value; }
			set { _description.Value = value; }
		}
		public string Id { get; private set; }
		public bool? IsClosed
		{
			get { return _isClosed.Value; }
			set { _isClosed.Value = value; }
		}
		public bool? IsPinned { get { return _isPinned.Value; } }
		public bool? IsSubscribed
		{
			get { return _isSubscribed.Value; }
			set { _isSubscribed.Value = value; }
		}
		public LabelNames LabelNames { get; private set; }
		public ListCollection Lists { get; private set; }
		public ReadOnlyMemberCollection Members { get; private set; }
		public BoardMembershipCollection Memberships { get; private set; }
		public string Name
		{
			get { return _name.Value; }
			set { _name.Value = value; }
		}
		public Organization Organization
		{
			get { return _organization.Value; }
			set { _organization.Value = value; }
		}
		public BoardPreferences Preferences { get; private set; }
		public string Url { get { return _url.Value; } }

		internal IJsonBoard Json { get { return _context.Data; } }

		public event Action<Board, IEnumerable<string>> Updated;

		public Board(string id)
			: this(id, true) {}
		internal Board(IJsonBoard json, bool cache)
			: this(json.Id, cache)
		{
			_context.Merge(json);
		}
		private Board(string id, bool cache)
		{
			_context = new BoardContext(id);
			_context.Synchronized += Synchronized;

			Actions = new ReadOnlyActionCollection(typeof(Board), id);
			Cards = new ReadOnlyCardCollection(typeof(Board), id);
			_description = new Field<string>(_context, () => Description);
			Id = id;
			_isClosed = new Field<bool?>(_context, () => IsClosed);
			_isClosed.AddRule(NullableHasValueRule<bool>.Instance);
			_isPinned = new Field<bool?>(_context, () => IsPinned);
			_isPinned.AddRule(NullableHasValueRule<bool>.Instance);
			_isSubscribed = new Field<bool?>(_context, () => IsSubscribed);
			_isSubscribed.AddRule(NullableHasValueRule<bool>.Instance);
			LabelNames = new LabelNames(_context.LabelNamesContext);
			Lists = new ListCollection(id);
			Members = new ReadOnlyMemberCollection(typeof(Board), id);
			Memberships = new BoardMembershipCollection(id);
			_name = new Field<string>(_context, () => Name);
			_name.AddRule(NotNullOrWhiteSpaceRule.Instance);
			_organization = new Field<Organization>(_context, () => Organization);
			Preferences = new BoardPreferences(_context.BoardPreferencesContext);
			_url = new Field<string>(_context, () => Url);

			if (cache)
				TrelloConfiguration.Cache.Add(this);
		}

		void ICanWebhook.ApplyAction(Action action)
		{
			if (action.Type != ActionType.UpdateBoard || action.Data.Board == null || action.Data.Board.Id != Id) return;
			_context.Merge(action.Data.Board.Json);
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
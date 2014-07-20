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
 
	File Name:		Card2.cs
	Namespace:		Manatee.Trello
	Class Name:		Card2
	Purpose:		Represents a card.

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
	public class Card : ICanWebhook
	{
		private readonly Field<Board> _board;
		private readonly Field<string> _description;
		private readonly Field<DateTime?> _dueDate;
		private readonly Field<bool?> _isArchived;
		private readonly Field<bool?> _isSubscribed;
		private readonly Field<DateTime?> _lastActivity;
		private readonly Field<List> _list;
		private readonly Field<string> _name;
		private readonly Field<Position> _position;
		private readonly Field<int?> _shortId;
		private readonly Field<string> _shortUrl;
		private readonly Field<string> _url;
		private readonly CardContext _context;

		private bool _deleted;

		public ReadOnlyActionCollection Actions { get; private set; }
		public AttachmentCollection Attachments { get; private set; }
		public Badges Badges { get; private set; }
		public Board Board { get { return _board.Value; } }
		public CheckListCollection CheckLists { get; private set; }
		public CommentCollection Comments { get; private set; }
		public string Description
		{
			get { return _description.Value; }
			set { _description.Value = value; }
		}
		public DateTime? DueDate
		{
			get { return _dueDate.Value; }
			set { _dueDate.Value = value; }
		}
		public string Id { get; private set; }
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
		public LabelCollection Labels { get; private set; }
		public DateTime? LastActivity { get { return _lastActivity.Value; } }
		public List List
		{
			get { return _list.Value; }
			set { _list.Value = value; }
		}
		public MemberCollection Members { get; private set; }
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
		public int? ShortId { get { return _shortId.Value; } }
		public string ShortUrl { get { return _shortUrl.Value; } }
		public string Url { get { return _url.Value; } }

		internal IJsonCard Json { get { return _context.Data; } }

		public event Action<Card, IEnumerable<string>> Updated; 

		public Card(string id)
			: this(id, true) {}
		internal Card(IJsonCard json, bool cache)
			: this(json.Id, cache)
		{
			_context.Merge(json);
		}
		private Card(string id, bool cache)
		{
			Id = id;
			_context = new CardContext(id);
			_context.Synchronized += Synchronized;

			Actions = new ReadOnlyActionCollection(typeof(Card), id);
			Attachments = new AttachmentCollection(id);
			Badges = new Badges(_context.BadgesContext);
			_board = new Field<Board>(_context, () => Board);
			_board.AddRule(NotNullRule<Board>.Instance);
			CheckLists = new CheckListCollection(id);
			Comments = new CommentCollection(id);
			_description = new Field<string>(_context, () => Description);
			_dueDate = new Field<DateTime?>(_context, () => DueDate);
			_isArchived = new Field<bool?>(_context, () => IsArchived);
			_isArchived.AddRule(NullableHasValueRule<bool>.Instance);
			_isSubscribed = new Field<bool?>(_context, () => IsSubscribed);
			_isSubscribed.AddRule(NullableHasValueRule<bool>.Instance);
			Labels = new LabelCollection(_context);
			_lastActivity = new Field<DateTime?>(_context, () => LastActivity);
			_list = new Field<List>(_context, () => List);
			_list.AddRule(NotNullRule<List>.Instance);
			Members = new MemberCollection(id);
			_name = new Field<string>(_context, () => Name);
			_name.AddRule(NotNullOrWhiteSpaceRule.Instance);
			_position = new Field<Position>(_context, () => Position);
			_position.AddRule(NotNullRule<Position>.Instance);
			_position.AddRule(PositionRule.Instance);
			_shortId = new Field<int?>(_context, () => ShortId);
			_shortUrl = new Field<string>(_context, () => ShortUrl);
			_url = new Field<string>(_context, () => Url);

			if (cache)
				TrelloConfiguration.Cache.Add(this);
		}

		void ICanWebhook.ApplyAction(Action action)
		{
			if (action.Type != ActionType.UpdateCard || action.Data.Card == null || action.Data.Card.Id != Id) return;
			_context.Merge(action.Data.Card.Json);
		}
		public void Delete()
		{
			if (_deleted) return;

			_context.Delete();
			_deleted = true;
			TrelloConfiguration.Cache.Remove(this);
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
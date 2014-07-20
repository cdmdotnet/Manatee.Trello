using System;
using System.Collections.Generic;
using Manatee.Trello.Internal;
using Manatee.Trello.Internal.Synchronization;
using Manatee.Trello.Internal.Validation;
using Manatee.Trello.Json;

namespace Manatee.Trello
{
	public class CheckItem
	{
		private readonly Field<CheckList> _checkList;
		private readonly Field<string> _name;
		private readonly Field<Position> _position;
		private readonly Field<CheckItemState> _state;
		private readonly CheckItemContext _context;

		private bool _deleted;

		public CheckList CheckList
		{
			get { return _checkList.Value; }
			set { _checkList.Value = value; }
		}
		public string Id { get; private set; }
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
		public CheckItemState State
		{
			get { return _state.Value; }
			set { _state.Value = value; }
		}

		internal IJsonCheckItem Json { get { return _context.Data; } }

		public event Action<CheckItem, IEnumerable<string>> Updated;

		internal CheckItem(IJsonCheckItem json, string checkListId, bool cache)
		{
			Id = json.Id;
			_context = new CheckItemContext(Id, checkListId);
			_context.Synchronized += Synchronized;

			_checkList = new Field<CheckList>(_context, () => CheckList);
			_checkList.AddRule(NotNullRule<CheckList>.Instance);
			_name = new Field<string>(_context, () => Name);
			_name.AddRule(NotNullOrWhiteSpaceRule.Instance);
			_position = new Field<Position>(_context, () => Position);
			_position.AddRule(NotNullRule<Position>.Instance);
			_position.AddRule(PositionRule.Instance);
			_state = new Field<CheckItemState>(_context, () => State);
			_state.AddRule(EnumerationRule<CheckItemState>.Instance);

			if (cache)
				TrelloConfiguration.Cache.Add(this);

			_context.Merge(json);
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
using Manatee.Trello.Internal;
using Manatee.Trello.Internal.Synchronization;
using Manatee.Trello.Internal.Validation;
using Manatee.Trello.Json;

namespace Manatee.Trello
{
	public class CheckItem
	{
		private readonly string _id;
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
		public CheckItemState State
		{
			get { return _state.Value; }
			set { _state.Value = value; }
		}

		internal CheckItem(IJsonCheckItem json, string checkListId)
		{
			_id = json.Id;
			_context = new CheckItemContext(_id, checkListId);

			_checkList = new Field<CheckList>(_context, () => CheckList);
			_checkList.AddRule(NotNullRule<CheckList>.Instance);
			_name = new Field<string>(_context, () => Name);
			_name.AddRule(NotNullOrWhiteSpaceRule.Instance);
			_position = new Field<Position>(_context, () => Position);
			_state = new Field<CheckItemState>(_context, () => State);

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
	}
}
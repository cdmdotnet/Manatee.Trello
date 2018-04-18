using System.Threading;
using System.Threading.Tasks;
using Manatee.Trello.Internal;
using Manatee.Trello.Internal.Synchronization;
using Manatee.Trello.Json;

namespace Manatee.Trello
{
	public class DropDownOption : IDropDownOption, IMergeJson<IJsonCustomDropDownOption>
	{
		private readonly Field<CustomFieldDefinition> _field;
		private readonly Field<string> _text;
		private readonly Field<LabelColor?> _labelColor;
		private readonly Field<Position> _position;
		private readonly DropDownOptionContext _context;

		public string Id => Json.Id;
		public ICustomFieldDefinition Field => _field.Value;
		public string Text => _text.Value;
		public LabelColor? Color => _labelColor.Value;
		public Position Position => _position.Value;

		internal IJsonCustomDropDownOption Json
		{
			get { return _context.Data; }
			set { _context.Merge(value); }
		}

		internal DropDownOption(IJsonCustomDropDownOption json, TrelloAuthorization auth)
		{
			_context = new DropDownOptionContext(auth);
			_context.Merge(json);

			_field = new Field<CustomFieldDefinition>(_context, nameof(Field));
			_text = new Field<string>(_context, nameof(Text));
			_labelColor = new Field<LabelColor?>(_context, nameof(LabelColor));
			_position = new Field<Position>(_context, nameof(Position));

			TrelloConfiguration.Cache.Add(this);
		}

		public async Task Delete(CancellationToken ct = default(CancellationToken))
		{
			await _context.Delete(ct);
			if (TrelloConfiguration.RemoveDeletedItemsFromCache)
				TrelloConfiguration.Cache.Remove(this);
		}

		public async Task Refresh(CancellationToken ct = default(CancellationToken))
		{
			await _context.Synchronize(ct);
		}

		public override string ToString()
		{
			return Text;
		}

		void IMergeJson<IJsonCustomDropDownOption>.Merge(IJsonCustomDropDownOption json)
		{
			_context.Merge(json);
		}
	}
}
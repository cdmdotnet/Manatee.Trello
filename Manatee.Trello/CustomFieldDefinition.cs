using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Manatee.Trello.Internal;
using Manatee.Trello.Internal.Synchronization;
using Manatee.Trello.Json;

namespace Manatee.Trello
{
	/// <summary>
	/// Represents a custom field definition.
	/// </summary>
	public class CustomFieldDefinition : ICustomFieldDefinition, IMergeJson<IJsonCustomFieldDefinition>
	{
		private readonly Field<IBoard> _board;
		private readonly Field<string> _fieldGroup;
		private readonly Field<string> _name;
		private readonly Field<Position> _position;
		private readonly Field<CustomFieldType?> _type;
		private readonly CustomFieldDefinitionContext _context;

		/// <summary>
		/// Gets the board on which the field is defined.
		/// </summary>
		public IBoard Board => _board.Value;

		/// <summary>
		/// Gets an identifier that groups fields across boards.
		/// </summary>
		public string FieldGroup => _fieldGroup.Value;

		/// <summary>
		/// Gets an ID on which matching can be performed.
		/// </summary>
		public string Id { get; private set; }

		/// <summary>
		/// Gets or sets the name of the field.
		/// </summary>
		public string Name
		{
			get { return _name.Value; }
			set { _name.Value = value; }
		}

		/// <summary>
		/// Gets drop down options, if applicable.
		/// </summary>
		public IDropDownOptionCollection Options => _context.DropDownOptions;

		/// <summary>
		/// Gets or sets the position of the field.
		/// </summary>
		public Position Position
		{
			get { return _position.Value; }
			set { _position.Value = value; }
		}

		/// <summary>
		/// Gets the data type of the field.
		/// </summary>
		public CustomFieldType? Type => _type.Value;

		internal IJsonCustomFieldDefinition Json
		{
			get { return _context.Data; }
			set { _context.Merge(value); }
		}

		/// <summary>
		/// Raised when data on the custom field definition is updated.
		/// </summary>
		public event Action<ICustomFieldDefinition, IEnumerable<string>> Updated;

		internal CustomFieldDefinition(IJsonCustomFieldDefinition json, TrelloAuthorization auth)
		{
			Id = json.Id;
			_context = new CustomFieldDefinitionContext(Id, auth);

			_board = new Field<IBoard>(_context, nameof(Board));
			_fieldGroup = new Field<string>(_context, nameof(FieldGroup));
			_name = new Field<string>(_context, nameof(Name));
			_position = new Field<Position>(_context, nameof(Position));
			_type = new Field<CustomFieldType?>(_context, nameof(Type));

			TrelloConfiguration.Cache.Add(this);

			// we need to enumerate the collection to cache all of the values
			// ReSharper disable once ReturnValueOfPureMethodIsNotUsed
			//Options?.ToList();

			_context.Merge(json);
			_context.Synchronized += Synchronized;
		}

		/// <summary>
		/// Deletes the field definition.
		/// </summary>
		/// <param name="ct">(Optional) A cancellation token for async processing.</param>
		public async Task Delete(CancellationToken ct = default(CancellationToken))
		{
			await _context.Delete(ct);
			if (TrelloConfiguration.RemoveDeletedItemsFromCache)
				TrelloConfiguration.Cache.Remove(this);
		}

		/// <summary>
		/// Refreshes the custom field definition data.
		/// </summary>
		/// <param name="ct">(Optional) A cancellation token for async processing.</param>
		public async Task Refresh(CancellationToken ct = default(CancellationToken))
		{
			await _context.Synchronize(ct);
		}

		/// <summary>Returns a string that represents the current object.</summary>
		/// <returns>A string that represents the current object.</returns>
		public override string ToString()
		{
			return $"{Name} ({Type})";
		}

		private void Synchronized(IEnumerable<string> properties)
		{
			Id = _context.Data.Id;
			var handler = Updated;
			handler?.Invoke(this, properties);
		}

		void IMergeJson<IJsonCustomFieldDefinition>.Merge(IJsonCustomFieldDefinition json)
		{
			_context.Merge(json);
		}
	}
}

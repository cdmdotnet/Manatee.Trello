using System;
using System.Collections.Generic;
using Manatee.Trello.Internal;
using Manatee.Trello.Internal.Synchronization;
using Manatee.Trello.Json;

namespace Manatee.Trello
{
	public abstract class CustomField : ICustomField, IMergeJson<IJsonCustomField>
	{
		private readonly Field<ICustomFieldDefinition> _definition;
		private readonly CustomFieldContext _context;

		internal CustomFieldContext Context => _context;

		public string Id { get; private set; }
		public ICustomFieldDefinition Definition => _definition.Value;

		internal IJsonCustomField Json
		{
			get { return _context.Data; }
			set { _context.Merge(value); }
		}

		/// <summary>
		/// Raised when data on the custom field is updated.
		/// </summary>
		public event Action<ICustomField, IEnumerable<string>> Updated;

		internal CustomField(IJsonCustomField json, string cardId, TrelloAuthorization auth)
		{
			Id = json.Id;
			_context = new CustomFieldContext(Id, cardId, auth);

			_definition = new Field<ICustomFieldDefinition>(_context, nameof(Definition));

			TrelloConfiguration.Cache.Add(this);

			_context.Merge(json);
			_context.Synchronized += Synchronized;
		}

		void IMergeJson<IJsonCustomField>.Merge(IJsonCustomField json)
		{
			_context.Merge(json);
		}

		private void Synchronized(IEnumerable<string> properties)
		{
			Id = _context.Data.Id;
			var handler = Updated;
			handler?.Invoke(this, properties);
		}
	}

	public abstract class CustomField<T> : CustomField, ICustomField<T>
	{
		public abstract T Value { get; set; }

		internal CustomField(IJsonCustomField json, string cardId, TrelloAuthorization auth)
			: base(json, cardId, auth)
		{
		}

		public override string ToString()
		{
			if (Value == null) return Definition.ToString();
			return $"{Definition.Name} - {Value}";
		}
	}
}
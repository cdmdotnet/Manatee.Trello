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

		public string Id => Json.Id;
		public ICustomFieldDefinition Definition => _definition.Value;

		internal IJsonCustomField Json
		{
			get { return _context.Data; }
			set { _context.Merge(value); }
		}

		internal CustomField(IJsonCustomField json, TrelloAuthorization auth)
		{
			_context = new CustomFieldContext(auth);
			_context.Merge(json);

			_definition = new Field<ICustomFieldDefinition>(_context, nameof(Definition));

			TrelloConfiguration.Cache.Add(this);
		}

		void IMergeJson<IJsonCustomField>.Merge(IJsonCustomField json)
		{
			_context.Merge(json);
		}
	}

	public abstract class CustomField<T> : CustomField, ICustomField<T>
	{
		public abstract T Value { get; }

		internal CustomField(IJsonCustomField json, TrelloAuthorization auth)
			: base(json, auth)
		{
		}

		public override string ToString()
		{
			if (Value == null) return Definition.ToString();
			return $"{Definition.Name} - {Value}";
		}
	}
}
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
	/// Represents a custom field instance.
	/// </summary>
	public abstract class CustomField : ICustomField, IMergeJson<IJsonCustomField>
	{
		private readonly Field<ICustomFieldDefinition> _definition;
		private readonly CustomFieldContext _context;

		internal CustomFieldContext Context => _context;

		/// <summary>
		/// Gets an ID on which matching can be performed.
		/// </summary>
		public string Id { get; private set; }

		/// <summary>
		/// Gets the custom field definition.
		/// </summary>
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

			if (auth != TrelloAuthorization.Null)
				TrelloConfiguration.Cache.Add(this);

			_context.Merge(json);
			_context.Synchronized += Synchronized;
		}

		/// <summary>
		/// Refreshes the custom field instance data.
		/// </summary>
		/// <param name="ct">(Optional) A cancellation token for async processing.</param>
		public async Task Refresh(CancellationToken ct = default(CancellationToken))
		{
			await _context.Synchronize(ct);
		}

		void IMergeJson<IJsonCustomField>.Merge(IJsonCustomField json, bool overwrite)
		{
			_context.Merge(json, overwrite);
		}

		private void Synchronized(IEnumerable<string> properties)
		{
			Id = _context.Data.Id;
			var handler = Updated;
			handler?.Invoke(this, properties);
		}
	}

	/// <summary>
	/// Represents a custom field instance.
	/// </summary>
	/// <typeparam name="T">The data type of the custom field.</typeparam>
	public abstract class CustomField<T> : CustomField, ICustomField<T>
	{
		/// <summary>
		/// Gets or sets the value.
		/// </summary>
		public abstract T Value { get; set; }

		internal CustomField(IJsonCustomField json, string cardId, TrelloAuthorization auth)
			: base(json, cardId, auth)
		{
		}

		/// <summary>Returns a string that represents the current object.</summary>
		/// <returns>A string that represents the current object.</returns>
		public override string ToString()
		{
			if (Value == null) return Definition.ToString();
			return $"{Definition.Name} - {Value}";
		}
	}
}
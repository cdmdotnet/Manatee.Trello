using System.Threading;
using System.Threading.Tasks;
using Manatee.Trello.Internal;
using Manatee.Trello.Internal.Synchronization;
using Manatee.Trello.Json;

namespace Manatee.Trello
{
	/// <summary>
	/// Represents an option for a custom selection field.
	/// </summary>
	public class DropDownOption : IDropDownOption, IMergeJson<IJsonCustomDropDownOption>
	{
		private readonly Field<CustomFieldDefinition> _field;
		private readonly Field<string> _text;
		private readonly Field<LabelColor?> _labelColor;
		private readonly Field<Position> _position;
		private readonly DropDownOptionContext _context;

		/// <summary>
		/// Gets an ID on which matching can be performed.
		/// </summary>
		public string Id => Json.Id;

		/// <summary>
		/// Gets the custom field definition that defines this option.
		/// </summary>
		public ICustomFieldDefinition Field => _field.Value;

		/// <summary>
		/// Gets the option text.
		/// </summary>
		public string Text => _text.Value;

		/// <summary>
		/// Gets the option color.
		/// </summary>
		public LabelColor? Color => _labelColor.Value;

		/// <summary>
		/// Gets the option position.
		/// </summary>
		public Position Position => _position.Value;

		internal IJsonCustomDropDownOption Json
		{
			get { return _context.Data; }
			set { _context.Merge(value); }
		}

		internal DropDownOption(IJsonCustomDropDownOption json, TrelloAuthorization auth, bool created = false)
		{
			_context = new DropDownOptionContext(auth, created);
			_context.Merge(json);

			_field = new Field<CustomFieldDefinition>(_context, nameof(Field));
			_text = new Field<string>(_context, nameof(Text));
			_labelColor = new Field<LabelColor?>(_context, nameof(LabelColor));
			_position = new Field<Position>(_context, nameof(Position));

			if (!created)
				TrelloConfiguration.Cache.Add(this);
		}

		/// <summary>
		/// Creates a new drop down option.
		/// </summary>
		/// <param name="text">The text.</param>
		/// <param name="color">(Optional) The label color.</param>
		/// <returns>A new drop down option.</returns>
		/// <remarks>This object will not update.  It is intended for adding new options to custom drop down fields.</remarks>
		public static IDropDownOption Create(string text, LabelColor color = LabelColor.None)
		{
			var json = TrelloConfiguration.JsonFactory.Create<IJsonCustomDropDownOption>();
			json.Text = text;
			json.Color = color;

			return new DropDownOption(json, null, true);
		}

		/// <summary>
		/// Deletes the drop down option.
		/// </summary>
		/// <param name="ct">(Optional) A cancellation token for async processing.</param>
		/// <remarks>
		/// This permanently deletes the drop down option from Trello's server, however, this object will remain in memory and all properties will remain accessible.
		/// </remarks>
		public async Task Delete(CancellationToken ct = default(CancellationToken))
		{
			await _context.Delete(ct);
			if (TrelloConfiguration.RemoveDeletedItemsFromCache)
				TrelloConfiguration.Cache.Remove(this);
		}

		/// <summary>
		/// Refreshes the drop down option data.
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
			return Text;
		}

		void IMergeJson<IJsonCustomDropDownOption>.Merge(IJsonCustomDropDownOption json, bool overwrite)
		{
			_context.Merge(json, overwrite);
		}
	}
}
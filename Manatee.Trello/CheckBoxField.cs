using Manatee.Trello.Internal;
using Manatee.Trello.Json;

namespace Manatee.Trello
{
	/// <summary>
	/// Represents a custom checkbox field instance.
	/// </summary>
	public class CheckBoxField : CustomField<bool?>
	{
		private readonly Field<bool?> _value;

		/// <summary>
		/// Gets or sets the value.
		/// </summary>
		public override bool? Value
		{
			get { return _value.Value; }
			set { _value.Value = value; }
		}

		internal CheckBoxField(IJsonCustomField json, string cardId, TrelloAuthorization auth)
			: base(json, cardId, auth)
		{
			_value = new Field<bool?>(Context, nameof(IJsonCustomField.Checked));
		}
	}
}
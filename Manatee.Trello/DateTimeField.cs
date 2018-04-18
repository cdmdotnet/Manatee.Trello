using System;
using Manatee.Trello.Internal;
using Manatee.Trello.Json;

namespace Manatee.Trello
{
	/// <summary>
	/// Represents a custom date/time field instance.
	/// </summary>
	public class DateTimeField : CustomField<DateTime?>
	{
		private readonly Field<DateTime?> _value;

		/// <summary>
		/// Gets or sets the value.
		/// </summary>
		public override DateTime? Value
		{
			get { return _value.Value; }
			set { _value.Value = value; }
		}

		internal DateTimeField(IJsonCustomField json, string cardId, TrelloAuthorization auth)
			: base(json, cardId, auth)
		{
			_value = new Field<DateTime?>(Context, nameof(IJsonCustomField.Date));
		}
	}
}
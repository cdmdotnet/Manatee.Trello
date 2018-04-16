using System;
using Manatee.Trello.Internal;
using Manatee.Trello.Json;

namespace Manatee.Trello
{
	public class DateTimeField : CustomField<DateTime?>
	{
		private readonly Field<DateTime?> _value;

		public override DateTime? Value => _value.Value;

		internal DateTimeField(IJsonCustomField json, TrelloAuthorization auth)
			: base(json, auth)
		{
			_value = new Field<DateTime?>(Context, nameof(IJsonCustomField.Date));
		}
	}
}
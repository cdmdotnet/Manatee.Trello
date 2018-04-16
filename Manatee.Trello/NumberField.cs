using Manatee.Trello.Internal;
using Manatee.Trello.Json;

namespace Manatee.Trello
{
	public class NumberField : CustomField<double?>
	{
		private readonly Field<double?> _value;

		public override double? Value => _value.Value;

		internal NumberField(IJsonCustomField json, TrelloAuthorization auth)
			: base(json, auth)
		{
			_value = new Field<double?>(Context, nameof(IJsonCustomField.Number));
		}
	}
}
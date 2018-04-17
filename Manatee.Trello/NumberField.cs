using Manatee.Trello.Internal;
using Manatee.Trello.Json;

namespace Manatee.Trello
{
	public class NumberField : CustomField<double?>
	{
		private readonly Field<double?> _value;

		public override double? Value
		{
			get { return _value.Value; }
			set { _value.Value = value; }
		}

		internal NumberField(IJsonCustomField json, string cardId, TrelloAuthorization auth)
			: base(json, cardId, auth)
		{
			_value = new Field<double?>(Context, nameof(IJsonCustomField.Number));
		}
	}
}
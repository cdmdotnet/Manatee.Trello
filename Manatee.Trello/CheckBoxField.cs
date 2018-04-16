using Manatee.Trello.Internal;
using Manatee.Trello.Json;

namespace Manatee.Trello
{
	public class CheckBoxField : CustomField<bool?>
	{
		private readonly Field<bool?> _value;

		public override bool? Value => _value.Value;

		internal CheckBoxField(IJsonCustomField json, TrelloAuthorization auth)
			: base(json, auth)
		{
			_value = new Field<bool?>(Context, nameof(IJsonCustomField.Checked));
		}
	}
}
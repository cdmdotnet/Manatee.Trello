using Manatee.Trello.Internal;
using Manatee.Trello.Json;

namespace Manatee.Trello
{
	public class DropDownField : CustomField<IDropDownOption>
	{
		private readonly Field<DropDownOption> _value;

		public override IDropDownOption Value => _value.Value;

		internal DropDownField(IJsonCustomField json, TrelloAuthorization auth)
			: base(json, auth)
		{
			_value = new Field<DropDownOption>(Context, nameof(IJsonCustomField.Selected));
		}
	}
}
using Manatee.Trello.Internal;
using Manatee.Trello.Json;

namespace Manatee.Trello
{
	public class DropDownField : CustomField<IDropDownOption>
	{
		private readonly Field<DropDownOption> _value;

		public override IDropDownOption Value
		{
			get { return _value.Value; }
			set { _value.Value = (DropDownOption) value; }
		}

		internal DropDownField(IJsonCustomField json, string cardId, TrelloAuthorization auth)
			: base(json, cardId, auth)
		{
			_value = new Field<DropDownOption>(Context, nameof(IJsonCustomField.Selected));
		}
	}
}
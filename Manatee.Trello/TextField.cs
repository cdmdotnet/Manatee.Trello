using Manatee.Trello.Internal;
using Manatee.Trello.Json;

namespace Manatee.Trello
{
	public class TextField : CustomField<string>
	{
		private readonly Field<string> _value;

		public override string Value => _value.Value;

		internal TextField(IJsonCustomField json, TrelloAuthorization auth)
			: base(json, auth)
		{
			_value = new Field<string>(Context, nameof(IJsonCustomField.Text));
		}
	}
}
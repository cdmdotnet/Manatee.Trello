using Manatee.Trello.Json;

namespace Manatee.Trello
{
	public class NumberField : CustomField<double?>
	{
		public override double? Value => Json.Number;

		internal NumberField(IJsonCustomField json, TrelloAuthorization auth)
			: base(json, auth)
		{
		}
	}
}
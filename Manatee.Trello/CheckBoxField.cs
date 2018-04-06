using Manatee.Trello.Json;

namespace Manatee.Trello
{
	public class CheckBoxField : CustomField<bool?>
	{
		public override bool? Value => Json.Checked;

		internal CheckBoxField(IJsonCustomField json, TrelloAuthorization auth)
			: base(json, auth)
		{
		}
	}
}
using Manatee.Trello.Json;

namespace Manatee.Trello
{
	public class TextField : CustomField<string>
	{
		public override string Value => Json.Text;

		internal TextField(IJsonCustomField json, TrelloAuthorization auth)
			: base(json, auth)
		{
		}
	}
}
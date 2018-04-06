using Manatee.Trello.Internal.Caching;
using Manatee.Trello.Json;

namespace Manatee.Trello
{
	public class DropDownField : CustomField<DropDownOption>
	{
		public override DropDownOption Value => Json.Selected.GetFromCache<DropDownOption>(_auth);

		internal DropDownField(IJsonCustomField json, TrelloAuthorization auth)
			: base(json, auth)
		{
		}
	}
}
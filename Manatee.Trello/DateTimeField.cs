using System;
using Manatee.Trello.Json;

namespace Manatee.Trello
{
	public class DateTimeField : CustomField<DateTime?>
	{
		public override DateTime? Value => Json.Date;

		internal DateTimeField(IJsonCustomField json, TrelloAuthorization auth)
			: base(json, auth)
		{
		}
	}
}
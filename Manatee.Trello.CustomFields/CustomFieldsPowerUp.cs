using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Manatee.Trello.Json;

namespace Manatee.Trello.CustomFields
{
	public class CustomFieldsPowerUp : PowerUpBase
	{
		public CustomFieldsPowerUp(IJsonPowerUp json, TrelloAuthorization auth)
			: base(json, auth) {}
	}
}

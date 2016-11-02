using Manatee.Trello.Json;

namespace Manatee.Trello.CustomFields
{
	public class CustomFieldsPowerUp : PowerUpBase
	{
		public const string PluginId = "56d5e249a98895a9797bebb9";

		public CustomFieldsPowerUp(IJsonPowerUp json, TrelloAuthorization auth)
			: base(json, auth) {}
	}
}

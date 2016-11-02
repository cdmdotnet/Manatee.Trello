using Manatee.Trello.Json;

namespace Manatee.Trello
{
	public class UnknownPowerUp : PowerUpBase
	{
		internal UnknownPowerUp(IJsonPowerUp json, TrelloAuthorization auth)
			: base(json, auth) {}
	}
}
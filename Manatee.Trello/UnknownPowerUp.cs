using Manatee.Trello.Json;

namespace Manatee.Trello
{
	/// <summary>
	/// Provides an implementation for an unknown power-up.
	/// </summary>
	public class UnknownPowerUp : PowerUpBase
	{
		internal UnknownPowerUp(IJsonPowerUp json, TrelloAuthorization auth)
			: base(json, auth) {}
	}
}
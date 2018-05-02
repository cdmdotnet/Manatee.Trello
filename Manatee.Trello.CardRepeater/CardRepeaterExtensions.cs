using System.Linq;

namespace Manatee.Trello.CardRepeater
{
	public static class CardRepeaterExtensions
	{
		public static CardRepitition Repitition(this ICard card, TrelloAuthorization auth = null)
		{
			var powerUpData = card.PowerUpData.FirstOrDefault(p => p.PluginId == CardRepeaterPowerUp.PluginId);
			if (powerUpData == null) return null;

			return new CardRepitition(powerUpData.Value, auth);
		}
	}
}
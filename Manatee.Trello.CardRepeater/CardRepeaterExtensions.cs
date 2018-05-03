using System.Linq;
using Manatee.Json;

namespace Manatee.Trello.CardRepeater
{
	public static class CardRepeaterExtensions
	{
		public static CardRepetition Repetition(this ICard card, TrelloAuthorization auth = null)
		{
			var powerUpData = card.PowerUpData.FirstOrDefault(p => p.PluginId == CardRepeaterPowerUp.PluginId);
			if (powerUpData == null) return null;

			var content = JsonValue.Parse(powerUpData.Value);
			if (content.Type != JsonValueType.Object || !content.Object.Any()) return null;

			var data = content.Object.TryGetObject("recurrence");
			var cached = TrelloConfiguration.Cache.Find<CardRepetition>(powerUpData.Id);

			if (cached != null)
			{
				cached.Update(data);
				return cached;
			}

			return new CardRepetition(data, auth);
		}
	}
}
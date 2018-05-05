using Manatee.Trello.Json;

namespace Manatee.Trello.CardRepeater
{
	public class CardRepeaterPowerUp : PowerUpBase
	{
		internal const string PluginId = "57b47fb862d25a30298459b1";

		private static bool _isRegistered;

		internal CardRepeaterPowerUp(IJsonPowerUp json, TrelloAuthorization auth)
			: base(json, auth)
		{
		}

		public static void Register()
		{
			if (_isRegistered) return;

			_isRegistered = true;
			TrelloConfiguration.RegisterPowerUp(PluginId, (j, a) => new CardRepeaterPowerUp(j, a));

			TrelloConfiguration.Serializer = CardRepetitionSerializerDecorator.Instance;
			TrelloConfiguration.Deserializer = CardRepetitionSerializerDecorator.Instance;

		}
	}
}

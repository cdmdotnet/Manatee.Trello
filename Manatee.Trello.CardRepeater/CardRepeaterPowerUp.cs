using Manatee.Trello.Json;
using Manatee.Trello.Rest;

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

	internal class CardRepetitionSerializerDecorator : ISerializer, IDeserializer
	{
		private readonly ISerializer _serializer;
		private readonly IDeserializer _deserializer;

		public static CardRepetitionSerializerDecorator Instance { get; }

		static CardRepetitionSerializerDecorator()
		{
			Instance = new CardRepetitionSerializerDecorator();
		}
		private CardRepetitionSerializerDecorator()
		{
			_serializer = TrelloConfiguration.Serializer;
			_deserializer = TrelloConfiguration.Deserializer;
		}

		public string Serialize(object obj)
		{
			if (obj is CardRepetition repetition) return repetition.ToJson();

			return _serializer.Serialize(obj);
		}

		public T Deserialize<T>(IRestResponse<T> response)
		{
			return _deserializer.Deserialize(response);
		}

		public T Deserialize<T>(string content)
		{
			return _deserializer.Deserialize<T>(content);
		}
	}
}

using Manatee.Trello.Json;
using Manatee.Trello.Rest;

namespace Manatee.Trello.CardRepeater
{
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
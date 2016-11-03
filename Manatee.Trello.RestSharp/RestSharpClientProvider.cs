using Manatee.Trello.Rest;

namespace Manatee.Trello.RestSharp
{
	/// <summary>
	/// Implements IRestClientProvider to provide instances of RestSharp.RestClient
	/// wrapped in an IRestClient implementation.
	/// </summary>
	public class RestSharpClientProvider : IRestClientProvider
	{
		private RestSharpRequestProvider _requestProvider;
		private RestSharpSerializer _serializer;
		private RestSharpDeserializer _deserializer;
		
		/// <summary>
		/// Creates requests for the client.
		/// </summary>
		public IRestRequestProvider RequestProvider => _requestProvider ?? (_requestProvider = new RestSharpRequestProvider(VerifySerializer()));

		/// <summary>
		/// Creates an instance of IRestClient.
		/// </summary>
		/// <param name="apiBaseUrl">The base URL to be used by the client</param>
		/// <returns>An instance of IRestClient.</returns>
		public IRestClient CreateRestClient(string apiBaseUrl)
		{
			var client = new RestSharpClient(TrelloConfiguration.Log, VerifyDeserializer(), apiBaseUrl);
			return client;
		}

		private void GetDefaultSerializer()
		{
			_serializer = new RestSharpSerializer(TrelloConfiguration.Serializer);
			_deserializer = new RestSharpDeserializer(TrelloConfiguration.Deserializer);
		}
		private RestSharpSerializer VerifySerializer()
		{
			if (_serializer == null)
				GetDefaultSerializer();
			return _serializer;
		}
		private RestSharpDeserializer VerifyDeserializer()
		{
			if (_serializer == null)
				GetDefaultSerializer();
			return _deserializer;
		}
	}
}
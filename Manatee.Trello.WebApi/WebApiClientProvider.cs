using Manatee.Trello.Rest;

namespace Manatee.Trello.WebApi
{
	/// <summary>
	/// Implements IRestClientProvider for WebApi.
	/// </summary>
	public class WebApiClientProvider : IRestClientProvider
	{
		/// <summary>
		/// Creates requests for the client.
		/// </summary>
		public IRestRequestProvider RequestProvider { get; }

		/// <summary>
		/// Creates a new instance of the <see cref="WebApiClientProvider"/> class.
		/// </summary>
		public WebApiClientProvider()
		{
			RequestProvider = new WebApiRequestProvider();
		}

		/// <summary>
		/// Creates an instance of IRestClient.
		/// </summary>
		/// <param name="apiBaseUrl">The base URL to be used by the client</param>
		/// <returns>An instance of IRestClient.</returns>
		public IRestClient CreateRestClient(string apiBaseUrl)
		{
			return new WebApiClient();
		}
	}
}
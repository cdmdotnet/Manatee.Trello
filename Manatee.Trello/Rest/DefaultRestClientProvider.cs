namespace Manatee.Trello.Rest
{
	/// <summary>
	/// Implements IRestClientProvider for WebApi.
	/// </summary>
	public class DefaultRestClientProvider : IRestClientProvider
	{
		/// <summary>
		/// Singleton instance of <see cref="DefaultRestClientProvider"/>.
		/// </summary>
		public static DefaultRestClientProvider Instance { get; } = new DefaultRestClientProvider();

		/// <summary>
		/// Creates requests for the client.
		/// </summary>
		public IRestRequestProvider RequestProvider { get; }

		/// <summary>
		/// Creates a new instance of the <see cref="DefaultRestClientProvider"/> class.
		/// </summary>
		private DefaultRestClientProvider()
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
			return new WebApiClient(apiBaseUrl);
		}
	}
}
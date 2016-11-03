namespace Manatee.Trello.Rest
{
	/// <summary>
	/// Defines methods required to create an instance of IRestClient.
	/// </summary>
	public interface IRestClientProvider
	{
		/// <summary>
		/// Creates requests for the client.
		/// </summary>
		IRestRequestProvider RequestProvider { get; }

		/// <summary>
		/// Creates an instance of IRestClient.
		/// </summary>
		/// <param name="apiBaseUrl">The base URL to be used by the client</param>
		/// <returns>An instance of IRestClient.</returns>
		IRestClient CreateRestClient(string apiBaseUrl);
	}
}
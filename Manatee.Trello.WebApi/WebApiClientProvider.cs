using Manatee.Trello.Rest;

namespace Manatee.Trello.WebApi
{
	public class WebApiClientProvider : IRestClientProvider
	{
		public IRestRequestProvider RequestProvider { get; }

		public WebApiClientProvider()
		{
			RequestProvider = new WebApiRequestProvider();
		}

		public IRestClient CreateRestClient(string apiBaseUrl)
		{
			return new WebApiClient();
		}
	}
}
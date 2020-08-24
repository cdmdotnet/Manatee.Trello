using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Manatee.Trello.Rest;

namespace Manatee.Trello.Internal.RequestProcessing
{
	internal static class RestRequestProcessor
	{
		private const string BaseUrl = @"https://trello.com/1";

		private static IRestClient Client => TrelloConfiguration.RestClientProvider.CreateRestClient(BaseUrl);

		public static event Func<Task> LastCall;

		public static Task<IRestResponse> AddRequest(IRestRequest request, CancellationToken ct)
		{
			return Process(() => Client.Execute(request, ct), request, ct);
		}
		public static Task<IRestResponse> AddRequest<T>(IRestRequest request, CancellationToken ct)
			where T : class
		{
			return Process(async () => await Client.Execute<T>(request, ct), request, ct);
		}
		public static async Task Flush()
		{
			if (LastCall == null) return;

			var handlers = LastCall.GetInvocationList().Cast<Func<Task>>();

			await Task.WhenAll(handlers.Select(h => h()));
			
			handlers = null;
		}

		private static async Task<IRestResponse> Process(Func<Task<IRestResponse>> ask, IRestRequest request, CancellationToken ct)
		{
			IRestResponse response;
			try
			{
				response = await Execute(ask, request, ct);
			}
			catch (Exception e)
			{
				response = new NullRestResponse {Exception = e};
				TrelloConfiguration.Log.Error(e);
			}

			return response;
		}
		private static async Task<IRestResponse> Execute(Func<Task<IRestResponse>> ask, IRestRequest request, CancellationToken ct)
		{
			IRestResponse response;
			if (!ct.IsCancellationRequested)
			{
				try
				{
					response = await ask();
				}
				catch (Exception e)
				{
					var tie = new TrelloInteractionException(e);
					response = new NullRestResponse {Exception = e};
					TrelloConfiguration.Log.Error(tie);
				}
			}
			else
			{
				response = new NullRestResponse();
			}

			return response;
		}
	}
}
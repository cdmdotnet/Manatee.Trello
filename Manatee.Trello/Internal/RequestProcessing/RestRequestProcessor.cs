using System;
using System.Threading;
using System.Threading.Tasks;
using Manatee.Trello.Rest;

namespace Manatee.Trello.Internal.RequestProcessing
{
	internal static class RestRequestProcessor
	{
		private const string BaseUrl = @"https://trello.com/1";

		private static int _pendingRequestCount;
		private static bool _cancelPendingRequests;

		public static event System.Action LastCall;

		public static Task AddRequest(IRestRequest request, CancellationToken ct)
		{
			return Process(async c => request.Response = await c.Execute(request, ct), request);
		}
		public static Task AddRequest<T>(IRestRequest request, CancellationToken ct)
			where T : class
		{
			return Process(async c => request.Response = await c.Execute<T>(request, ct), request);
		}
		public static void Flush()
		{
			LastCall?.Invoke();
		}
		public static void CancelPendingRequests()
		{
			_cancelPendingRequests = true;
			Flush();
		}

		private static async Task Process(Func<IRestClient, Task> ask, IRestRequest request)
		{
			try
			{
				_pendingRequestCount++;
				await Execute(ask, request);
			}
			catch (Exception e)
			{
				request.Response = new NullRestResponse {Exception = e};
				TrelloConfiguration.Log.Error(e);
			}
			finally
			{
				_pendingRequestCount--;
				if (_pendingRequestCount == 0)
					_cancelPendingRequests = false;
			}
		}
		private static async Task Execute(Func<IRestClient, Task> ask, IRestRequest request)
		{
			if (!_cancelPendingRequests)
			{
				var client = TrelloConfiguration.RestClientProvider.CreateRestClient(BaseUrl);
				LogRequest(request, "Sending");
				try
				{
					await ask(client);
					LogResponse(request.Response, "Received");
				}
				catch (Exception e)
				{
					var tie = new TrelloInteractionException(e);
					request.Response = new NullRestResponse {Exception = e};
					TrelloConfiguration.Log.Error(tie);
				}
			}
			else
			{
				LogRequest(request, "Stubbing");
				request.Response = new NullRestResponse();
			}
		}
		private static void LogRequest(IRestRequest request, string action)
		{
			TrelloConfiguration.Log.Info("{2}: {0} {1}", request.Method, request.Resource, action);
		}
		private static void LogResponse(IRestResponse response, string action)
		{
			TrelloConfiguration.Log.Info("{0}: {1}", action, response.Content);
		}
	}
}
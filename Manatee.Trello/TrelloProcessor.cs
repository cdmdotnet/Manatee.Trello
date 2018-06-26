using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Manatee.Trello.Internal;
using Manatee.Trello.Internal.DataAccess;
using Manatee.Trello.Internal.Licensing;
using Manatee.Trello.Internal.RequestProcessing;
using Manatee.Trello.Json;

namespace Manatee.Trello
{
	/// <summary>
	/// Provides options and control for the internal request queue processor.
	/// </summary>
	public static class TrelloProcessor
	{
		/// <summary>
		/// Signals the processor that the application is shutting down.  The processor will perform a "last call" for pending requests.
		/// </summary>
		public static async Task Flush()
		{
			await LicenseHelpers.Batch(RestRequestProcessor.Flush());
		}

		/// <summary>
		/// Processes webhook notification content.
		/// </summary>
		/// <param name="content">The string content of the notification.</param>
		/// <param name="auth">The <see cref="TrelloAuthorization"/> under which the notification should be processed</param>
		public static void ProcessNotification(string content, TrelloAuthorization auth = null)
		{
			LicenseHelpers.IncrementAndCheckRetrieveCount();
			var notification = TrelloConfiguration.Deserializer.Deserialize<IJsonWebhookNotification>(content);
			var action = new Action(notification.Action, auth);

			foreach (var obj in TrelloConfiguration.Cache.OfType<ICanWebhook>())
			{
				obj.ApplyAction(action);
			}
		}

		/// <summary>
		/// Performs a batch refresh of all entities.
		/// </summary>
		/// <param name="ct"></param>
		/// <param name="entities"></param>
		/// <returns></returns>
		/// <remarks>
		/// Trello imposes a limit of 10 entities per call.  Therefore this method will break all entities into batches
		/// of 10 and make a single call for each, returning when all have completed.
		/// </remarks>
		public static Task Refresh(CancellationToken ct = default(CancellationToken), params IRefreshable[] entities)
		{
			var groupedByAuth = entities.OfType<IRefreshEndpointSupplier>()
			                            .GroupBy(e => e.Auth);
			var batches = groupedByAuth.SelectMany(ga => ga.Select((entity, index) => new {entity, index})
			                                               .GroupBy(gc => gc.index / 10, gc => gc.entity))
			                           .ToList();

			return Task.WhenAll(batches.Select(g => _RefreshBatch(g, ct)));
		}

		private static Task _RefreshBatch(List<IRefreshEndpointSupplier> batch, CancellationToken ct)
		{
			var auth = batch.First().Auth;
			var entityEndpoints = batch.Select(e => e.GetRefreshEndpoint());

			var batchEndpoint = EndpointFactory.Build(EntityRequestType.Batch_Read_Refresh);
			var parameters = new Dictionary<string, object>
				{
					["urls"] = string.Join(",", entityEndpoints.Select(e => e.ToString()))
				};
			var newData = await JsonRepository.Execute<IJsonCard>(Auth, batchEndpoint, ct, parameters);

			MarkInitialized();
			return newData;
		}
	}
}
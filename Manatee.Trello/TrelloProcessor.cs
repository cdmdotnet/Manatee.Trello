using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Manatee.Trello.Internal;
using Manatee.Trello.Internal.DataAccess;
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
		public static Task Flush()
		{
			return RestRequestProcessor.Flush();
		}

		/// <summary>
		/// Processes webhook notification content.
		/// </summary>
		/// <param name="content">The string content of the notification.</param>
		/// <param name="auth">The <see cref="TrelloAuthorization"/> under which the notification should be processed</param>
		public static void ProcessNotification(string content, TrelloAuthorization auth = null)
		{
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
		/// <param name="entities">The entities to refresh.</param>
		/// <param name="ct">(Optional) A cancellation token for async processing.</param>
		/// <remarks>
		/// Trello imposes a limit of 10 entities per call.  Therefore this method will break all entities into batches
		/// of 10 and make a single call for each, returning when all have completed.
		/// </remarks>
		public static Task Refresh(IEnumerable<IBatchRefreshable> entities, CancellationToken ct = default)
		{
			var groupedByAuth = entities.OfType<IBatchRefresh>()
			                            .GroupBy(e => e.Auth);
			var batches = groupedByAuth.SelectMany(ga => ga.Select((entity, index) => new {entity, index})
			                                               .GroupBy(gc => gc.index / 10, gc => gc.entity)
			                                               .Select(g => g.ToList()));

			return Task.WhenAll(batches.Select(g => _RefreshBatch(g, ct)));
		}

		private static async Task _RefreshBatch(List<IBatchRefresh> batch, CancellationToken ct)
		{
			var auth = batch.First().Auth;
			var entityEndpoints = batch.Select(e => e.GetRefreshEndpoint());

			var batchEndpoint = EndpointFactory.Build(EntityRequestType.Batch_Read_Refresh);
			var parameters = new Dictionary<string, object>
				{
					["urls"] = string.Join(",", entityEndpoints.Select(e => $"/{e}"))
				};
			var newData = await JsonRepository.Execute<IJsonBatch>(auth, batchEndpoint, ct, parameters);

			foreach (var item in newData.Items.Where(r => r.Error == null))
			{
				var entity = batch.FirstOrDefault(e => e.Id == item.EntityId);
				entity?.Apply(item.Content);
			}
		}
	}
}
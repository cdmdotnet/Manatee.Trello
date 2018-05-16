using System.Linq;
using System.Threading.Tasks;
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

	}
}
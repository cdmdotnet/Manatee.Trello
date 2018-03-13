﻿using System.Linq;
using Manatee.Trello.Contracts;
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
		/// Specifies the number of concurrent calls to the Trello API that the processor can make.  Default is 1.
		/// </summary>
		public static int ConcurrentCallCount { get; set; }

		static TrelloProcessor()
		{
			ConcurrentCallCount = 1;
		}

		/// <summary>
		/// Signals the processor that the application is shutting down.  The processor will perform a "last call" for pending requests.
		/// </summary>
		public static void Flush()
		{
			RestRequestProcessor.Flush();
		}
		/// <summary>
		/// Cancels any requests that are still pending.  This applies to both downloads and uploads.
		/// </summary>
		public static void CancelPendingRequests()
		{
			RestRequestProcessor.CancelPendingRequests();
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

	}
}
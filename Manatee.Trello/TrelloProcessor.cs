using System;
using Manatee.Trello.Internal.RequestProcessing;

namespace Manatee.Trello
{
	/// <summary>
	/// Provides options and control for the internal request queue processor.
	/// </summary>
	public static class TrelloProcessor
	{
		/// <summary>
		/// Specifies whether the request processor can keep the application process alive after the main thread exits.  Default is false.
		/// </summary>
		/// <remarks>
		/// When this property is set to true, the application must call <see cref="Shutdown"/> at the end of execution.  This can be used
		/// to ensure that all requests are sent to Trello before an application ends.  This property appears to have no effect in testing
		/// environments.
		/// </remarks>
		[Obsolete("This property is no longer used as of v1.9.0 and will be removed in the next major version.")]
		public static bool WaitForPendingRequests { get; set; }
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
		[Obsolete("This method is no longer used as of v1.9.0 and will be removed in the next major version.  Functionality has been " +
		          "redirected to Flush().  Please use that method instead.")]
		public static void Shutdown()
		{
			Flush();
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
	}
}
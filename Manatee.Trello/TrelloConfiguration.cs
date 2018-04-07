using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using Manatee.Trello.Internal.Caching;
using Manatee.Trello.Internal.ExceptionHandling;
using Manatee.Trello.Json;
using Manatee.Trello.Rest;

namespace Manatee.Trello
{
	/// <summary>
	/// Exposes a set of run-time options for Manatee.Trello.
	/// </summary>
	public static class TrelloConfiguration
	{
		private static ILog _log;
		private static ISerializer _serializer;
		private static IDeserializer _deserializer;
		private static IRestClientProvider _restClientProvider;
		private static ICache _cache;
		private static IJsonFactory _jsonFactory;
		private static Func<IRestResponse, int, bool> _retryPredicate;

		/// <summary>
		/// Specifies the serializer for the REST client.
		/// </summary>
		public static ISerializer Serializer
		{
			get { return _serializer ?? (_serializer = DefaultJsonSerializer.Instance); }
			set { _serializer = value; }
		}

		/// <summary>
		/// Specifies the deserializer for the REST client.
		/// </summary>
		public static IDeserializer Deserializer
		{
			get { return _deserializer ?? (_deserializer = DefaultJsonSerializer.Instance); }
			set { _deserializer = value; }
		}

		/// <summary>
		/// Specifies the REST client provider.
		/// </summary>
		public static IRestClientProvider RestClientProvider
		{
			get { return _restClientProvider ?? (_restClientProvider = DefaultRestClientProvider.Instance); }
			set { _restClientProvider = value; }
		}

		/// <summary>
		/// Provides a cache to manage all Trello objects.
		/// </summary>
		public static ICache Cache
		{
			get { return _cache ?? (_cache = new ThreadSafeCacheDecorator(new SimpleCache())); }
			set { _cache = value; }
		}
		/// <summary>
		/// Provides logging for Manatee.Trello.  The default log writes to the Console window.
		/// </summary>
		public static ILog Log
		{
			get { return _log ?? (_log = new DebugLog()); }
			set { _log = value ?? new DebugLog(); }
		}
		/// <summary>
		/// Provides a factory which is used to create instances of JSON objects.
		/// </summary>
		public static IJsonFactory JsonFactory
		{
			get { return _jsonFactory ?? (_jsonFactory = DefaultJsonFactory.Instance); }
			set { _jsonFactory = value; }
		}
		/// <summary>
		/// Specifies whether the service should throw an exception when an error is received from Trello.  Default is true.
		/// </summary>
		public static bool ThrowOnTrelloError { get; set; }
		/// <summary>
		/// Specifies a length of time after which each Trello object will be marked as expired. Default is 30 seconds.
		/// </summary>
		public static TimeSpan ExpiryTime { get; set; }
		/// <summary>
		/// Specifies a length of time an object holds changes before it submits them.  The timer is reset with every change.  Default is 100 ms.
		/// </summary>
		/// <remarks>
		/// Setting a value of 0 ms will result in instant upload of changes, dramatically increasing call volume and slowing performance.
		/// </remarks>
		public static TimeSpan ChangeSubmissionTime { get; set; }
		/// <summary>
		/// Specifies which HTTP response status codes should trigger an automatic retry.
		/// </summary>
		public static IList<HttpStatusCode> RetryStatusCodes { get; }
		/// <summary>
		/// Specifies a maximum number of retries allowed before an error is thrown.  
		/// </summary>
		public static int MaxRetryCount { get; set; }
		/// <summary>
		/// Specifies a delay between retry attempts.
		/// </summary>
		public static TimeSpan DelayBetweenRetries { get; set; }
		/// <summary>
		/// Specifies a predicate to execute to determine if a retry should be attempted.  The default simply uses <see cref="MaxRetryCount"/> and <see cref="DelayBetweenRetries"/>.
		/// </summary>
		/// <remarks>
		/// Parameters:
		/// 
		/// - <see cref="IRestResponse"/> - The response object from the REST provider.  Will need to be cast to the appropriate type.
		/// - <see cref="int"/> - The number of retries attempted.
		///
		/// Return value:
		///
		/// - <see cref="bool"/> - True if the call should be retried; false otherwise.
		/// </remarks>
		public static Func<IRestResponse, int, bool> RetryPredicate
		{
			get { return _retryPredicate ?? DefaultRetry; }
			set { _retryPredicate = value; }
		}

		internal static Dictionary<string, Func<IJsonPowerUp, TrelloAuthorization, IPowerUp>> RegisteredPowerUps { get; }

		static TrelloConfiguration()
		{
			ThrowOnTrelloError = true;
			ExpiryTime = TimeSpan.FromSeconds(30);
			ChangeSubmissionTime = TimeSpan.FromMilliseconds(100);
			RegisteredPowerUps = new Dictionary<string, Func<IJsonPowerUp, TrelloAuthorization, IPowerUp>>();
			RetryStatusCodes = new List<HttpStatusCode>();
		}

		/// <summary>
		/// Registers a new power-up implementation.
		/// </summary>
		/// <param name="id">The Trello ID of the power-up.</param>
		/// <param name="factory">A factory function that creates instances of the power-up implementation.</param>
		public static void RegisterPowerUp(string id, Func<IJsonPowerUp, TrelloAuthorization, IPowerUp> factory)
		{
			RegisteredPowerUps[id] = factory;
		}

		private static bool DefaultRetry(IRestResponse response, int callCount)
		{
			var retry = RetryStatusCodes.Contains(response.StatusCode) &&
			            callCount <= MaxRetryCount;
			if (retry)
				Thread.Sleep(DelayBetweenRetries);
			return retry;
		}
	}
}

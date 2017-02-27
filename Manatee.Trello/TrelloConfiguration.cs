using System;
using System.Collections.Generic;
using System.Net;
using Manatee.Trello.Contracts;
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

		/// <summary>
		/// Specifies the serializer for the REST client.
		/// </summary>
		public static ISerializer Serializer
		{
			get
			{
				if (_serializer == null)
					throw new InvalidOperationException("TrelloConfiguration.Serializer must be set before creating Trello objects.");
				return _serializer;
			}
			set
			{
				if (value == null)
					Log.Error(new ArgumentNullException(nameof(value)));
				_serializer = value;
			}
		}
		/// <summary>
		/// Specifies the deserializer for the REST client.
		/// </summary>
		public static IDeserializer Deserializer
		{
			get
			{
				if (_deserializer == null)
					throw new InvalidOperationException("TrelloConfiguration.Deserializer must be set before creating Trello objects.");
				return _deserializer;
			}
			set
			{
				if (value == null)
					Log.Error(new ArgumentNullException(nameof(value)));
				_deserializer = value;
			}
		}
		/// <summary>
		/// Specifies the REST client provider.
		/// </summary>
		public static IRestClientProvider RestClientProvider
		{
			get
			{
				if (_restClientProvider == null)
					throw new InvalidOperationException("TrelloConfiguration.RestClientProvider must be set before creating Trello objects.");
				return _restClientProvider;
			}
			set
			{
				if (value == null)
					Log.Error(new ArgumentNullException(nameof(value)));
				_restClientProvider = value;
			}
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
			get
			{
				if (_jsonFactory == null)
					throw new InvalidOperationException("TrelloConfiguration.JsonFactory must be set before creating Trello objects.");
				return _jsonFactory;
			}
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
		/// 
		/// </summary>
		public static int MaxRetryCount { get; set; }

		internal static Dictionary<string, Func<IJsonPowerUp, TrelloAuthorization, IPowerUp>> RegisteredPowerUps { get; }

		static TrelloConfiguration()
		{
			ThrowOnTrelloError = true;
			ExpiryTime = TimeSpan.FromSeconds(30);
			ChangeSubmissionTime = TimeSpan.FromMilliseconds(100);
			RegisteredPowerUps = new Dictionary<string, Func<IJsonPowerUp, TrelloAuthorization, IPowerUp>>();
			RetryStatusCodes = new List<HttpStatusCode>();
		}

		public static void RegisterPowerUp(string id, Func<IJsonPowerUp, TrelloAuthorization, IPowerUp> factory)
		{
			RegisteredPowerUps[id] = factory;
		}
	}
}

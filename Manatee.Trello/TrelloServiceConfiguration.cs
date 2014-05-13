/***************************************************************************************

	Copyright 2013 Little Crab Solutions

	   Licensed under the Apache License, Version 2.0 (the "License");
	   you may not use this file except in compliance with the License.
	   You may obtain a copy of the License at

		 http://www.apache.org/licenses/LICENSE-2.0

	   Unless required by applicable law or agreed to in writing, software
	   distributed under the License is distributed on an "AS IS" BASIS,
	   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
	   See the License for the specific language governing permissions and
	   limitations under the License.
 
	File Name:		Options.cs
	Namespace:		Manatee.Trello
	Class Name:		Options
	Purpose:		Exposes a set of run-time options for Manatee.Trello.

***************************************************************************************/
using System;
using Manatee.Trello.Contracts;
using Manatee.Trello.Internal;
using Manatee.Trello.Internal.ExceptionHandling;
using Manatee.Trello.Json;
using Manatee.Trello.Rest;

namespace Manatee.Trello
{
	/// <summary>
	/// Exposes a set of run-time options for all automatically-refreshing objects.
	/// </summary>
	public static class TrelloServiceConfiguration
	{
		private static ILog _log;
		private static ISerializer _serializer;
		private static IDeserializer _deserializer;
		private static IRestClientProvider _restClientProvider;
		private static ICache _cache;

		/// <summary>
		/// Specifies the serializer which is used the first time a request is made from
		/// a given instance of the TrelloService class.
		/// </summary>
		public static ISerializer Serializer
		{
			get { return _serializer; }
			set
			{
				if (value == null)
					Log.Error(new ArgumentNullException("value"));
				_serializer = value;
			}
		}
		/// <summary>
		/// Specifies the deserializer which is used the first time a request is made from
		/// a given instance of the TrelloService class.
		/// </summary>
		public static IDeserializer Deserializer
		{
			get { return _deserializer; }
			set
			{
				if (value == null)
					Log.Error(new ArgumentNullException("value"));
				_deserializer = value;
			}
		}
		/// <summary>
		/// Specifies the REST client provider which is used the first time a request is made from
		/// a given instance of the TrelloService class.
		/// </summary>
		public static IRestClientProvider RestClientProvider
		{
			get { return _restClientProvider; }
			set
			{
				if (value == null)
					Log.Error(new ArgumentNullException("value"));
				_restClientProvider = value;
			}
		}
		/// <summary>
		/// Provides a cache for TrelloService.  Defaults to TrelloServiceConfiguration.GlobalCache.
		/// </summary>
		public static ICache Cache
		{
			get { return _cache ?? (_cache = new ThreadSafeCacheDecorator(new SimpleCache())); }
			set { _cache = value ?? new ThreadSafeCacheDecorator(new SimpleCache()); }
		}
		/// <summary>
		/// Provides logging for all of Manatee.Trello.  The default log only writes to the Debug window.
		/// </summary>
		public static ILog Log
		{
			get { return _log ?? (_log = new DebugLog()); }
			set { _log = value ?? new DebugLog(); }
		}
		/// <summary>
		/// Specifies whether the service should throw an exception when an error is received from Trello.
		/// </summary>
		public static bool ThrowOnTrelloError { get; set; }

		static TrelloServiceConfiguration()
		{
			ThrowOnTrelloError = true;
		}
	}
}

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
	public class TrelloServiceConfiguration : ITrelloServiceConfiguration
	{
		private ILog _log;
		private ISerializer _serializer;
		private IDeserializer _deserializer;
		private IRestClientProvider _restClientProvider;

		/// <summary>
		/// Provides a default logging solution.  New ITrelloService instances will use
		/// this unless overridden in an ITrelloServiceConfiguration instance.
		/// </summary>
		public static ILog GlobalLog { get; set; }
		/// <summary>
		/// Provides a default caching solution.  New ITrelloService instances will use
		/// this unless overridden in an ITrelloServiceConfiguration instance.
		/// </summary>
		public static ICache GlobalCache { get; set; }
		/// <summary>
		/// Gets and sets the global duration setting for all auto-refreshing objects.
		/// </summary>
		public TimeSpan ItemDuration { get; set; }
		/// <summary>
		/// Specifies the serializer which is used the first time a request is made from
		/// a given instance of the TrelloService class.
		/// </summary>
		public ISerializer Serializer
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
		public IDeserializer Deserializer
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
		public IRestClientProvider RestClientProvider
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
		public ICache Cache { get; set; }
		/// <summary>
		/// Provides logging for all of Manatee.Trello.  The default log only writes to the Debug window.
		/// </summary>
		public ILog Log { get { return _log ?? (_log = new DebugLog()); } set { _log = value ?? new DebugLog(); } }

		static TrelloServiceConfiguration()
		{
			GlobalCache = new ThreadSafeCache(new SimpleCache());
			GlobalLog = new DebugLog();
		}
		/// <summary>
		/// Creates a new instance of the TrelloServiceConfiguration class.
		/// </summary>
		public TrelloServiceConfiguration()
		{
			ItemDuration = TimeSpan.FromSeconds(60);
			Cache = GlobalCache;
		}
	}
}

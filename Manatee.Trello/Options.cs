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
using Manatee.Trello.Json;
using Manatee.Trello.Json.Manatee;
using Manatee.Trello.Json.Newtonsoft;
using Manatee.Trello.Rest;

namespace Manatee.Trello
{
	/// <summary>
	/// Exposes a set of run-time options for all automatically-refreshing objects.
	/// </summary>
	public static class Options
	{
		/// <summary>
		/// The default persistence time for object data.  Value is one minute.
		/// </summary>
		public static readonly TimeSpan DefaultItemDuration;
		private static ISerializer _serializer;
		private static IDeserializer _deserializer;
		private static ManateeSerializer _manateeSerializer;
		private static NewtonsoftSerializer _newtonsoftSerializer;
		private static IRestClientProvider _restClientProvider;
		private static RestSharpClientProvider _restSharpProvider;

		private static ISerializer ManateeSerializer { get { return _manateeSerializer ?? (_manateeSerializer = new ManateeSerializer()); } }
		private static IDeserializer ManateeDeserializer { get { return _manateeSerializer ?? (_manateeSerializer = new ManateeSerializer()); } }
		private static ISerializer NewtonsoftSerializer { get { return _newtonsoftSerializer ?? (_newtonsoftSerializer = new NewtonsoftSerializer()); } }
		private static IDeserializer NewtonsoftDeserializer { get { return _newtonsoftSerializer ?? (_newtonsoftSerializer = new NewtonsoftSerializer()); } }
		private static IRestClientProvider RestSharpClientProvider { get { return _restSharpProvider ?? (_restSharpProvider = new RestSharpClientProvider()); } }

		/// <summary>
		/// Gets and sets the global duration setting for all auto-refreshing objects.
		/// </summary>
		public static TimeSpan ItemDuration { get; set; }
		/// <summary>
		/// Enables/disables auto-refreshing for all auto-refreshing objects.
		/// </summary>
		public static bool AutoRefresh { get; set; }
		/// <summary>
		/// Specifies the serializer which is used the first time a request is made from
		/// a given instance of the TrelloService class.
		/// </summary>
		public static ISerializer Serializer
		{
			get
			{
				if (_serializer == null)
					CreateDefaultSerializer();
				return _serializer;
			}
			set
			{
				if (value == null)
					throw new ArgumentNullException("value");
				_serializer = value;
			}
		}
		/// <summary>
		/// Specifies the deserializer which is used the first time a request is made from
		/// a given instance of the TrelloService class.
		/// </summary>
		public static IDeserializer Deserializer
		{
			get
			{
				if (_deserializer == null)
					CreateDefaultSerializer();
				return _deserializer;
			}
			set
			{
				if (value == null)
					throw new ArgumentNullException("value");
				_deserializer = value;
			}
		}
		/// <summary>
		/// Specifies the REST client provider which is used the first time a request is made from
		/// a given instance of the TrelloService class.
		/// </summary>
		public static IRestClientProvider RestClientProvider
		{
			get
			{
				if (_restClientProvider == null)
					CreateDefaultRestClientProvider();
				return _restClientProvider;
			}
			set
			{
				if (value == null)
					throw new ArgumentNullException("value");
				_restClientProvider = value;
			}
		}

		static Options()
		{
			DefaultItemDuration = TimeSpan.FromSeconds(60);
			ItemDuration = DefaultItemDuration;
			AutoRefresh = true;
		}

		/// <summary>
		/// Sets Manatee.Json as the serializer/deserializer (default).
		/// </summary>
		public static void UseManateeJson()
		{
			_serializer = ManateeSerializer;
			_deserializer = ManateeDeserializer;
		}
		/// <summary>
		/// Sets Newtonsoft's Json.Net as the serializer/deserializer.
		/// </summary>
		public static void UseNewtonsoftJson()
		{
			_serializer = NewtonsoftSerializer;
			_deserializer = NewtonsoftDeserializer;
		}

		private static void CreateDefaultSerializer()
		{
			UseManateeJson();
		}
		private static void CreateDefaultRestClientProvider()
		{
			_restClientProvider = new RestSharpClientProvider();
		}
	}
}

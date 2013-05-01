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
 
	File Name:		RestSharpClientProvider.cs
	Namespace:		Manatee.Trello.Rest
	Class Name:		RestSharpClientProvider<T>
	Purpose:		Implements IRestClientProvider to provide instances of
					RestSharp.RestClient wrapped in an IRestClient implementation.

***************************************************************************************/
using System;
using Manatee.Trello.Json.Manatee;

namespace Manatee.Trello.Rest
{
	internal class RestSharpClientProvider : IRestClientProvider
	{
		private class RestClient : RestSharp.RestClient, IRestClient
		{
			private readonly RestSharp.Deserializers.IDeserializer _deserializer;

			public RestClient(RestSharp.Deserializers.IDeserializer deserializer, string apiBaseUrl)
				: base(apiBaseUrl)
			{
				_deserializer = deserializer;
				AddHandler("application/json", _deserializer);
				AddHandler("text/json", _deserializer);
			}

			public IRestResponse<T> Execute<T>(IRestRequest<T> request)
				where T : class
			{
				var restSharpRequest = (RestSharpRequest<T>) request;
				var restSharpResponse = base.Execute(restSharpRequest);
				var data = _deserializer.Deserialize<T>(restSharpResponse);
				return new RestSharpResponse<T>(restSharpResponse, data);
			}
		}
		private class RestSharpRequestProvider : IRestRequestProvider
		{
			private readonly RestSharp.Serializers.ISerializer _serializer;

			public RestSharpRequestProvider(RestSharp.Serializers.ISerializer serializer)
			{
				if (serializer == null)
					throw new ArgumentNullException("serializer");
				_serializer = serializer;
			}

			public IRestRequest<T> Create<T>(string endpoint)
			{
				return new RestSharpRequest<T>(_serializer, endpoint);
			}
		}

		private RestSharpRequestProvider _requestProvider;

		private RestSharp.Serializers.ISerializer _serializer;
		private RestSharp.Deserializers.IDeserializer _deserializer;

		/// <summary>
		/// Creates requests for the client.
		/// </summary>
		public IRestRequestProvider RequestProvider
		{
			get { return _requestProvider ?? (_requestProvider = new RestSharpRequestProvider(Serializer)); }
		}

		/// <summary>
		/// Gets and sets the serializer instance to be used by the client.
		/// </summary>
		/// <remarks>
		/// This property can be set a maximum of one time.  If a client is generated
		/// before both Serializer and Deserializer are set, the defaults will be used.
		/// The defaults are provided by ManateeJsonSerializer.
		/// </remarks>
		public RestSharp.Serializers.ISerializer Serializer
		{
			get { return _serializer ?? GetDefaultSerializer(); }
			set
			{
				if (value == null)
					throw new ArgumentNullException("value");
				if (_serializer != null)
					throw new InvalidOperationException("Serializer already set.");
				_serializer = value;
			}
		}
		/// <summary>
		/// Gets and sets the deserializer instance to be used by the client.
		/// </summary>
		/// <remarks>
		/// This property can be set a maximum of one time.  If a client is generated
		/// before both Serializer and Deserializer are set, the defaults will be used.
		/// The defaults are provided by ManateeJsonSerializer.
		/// </remarks>
		public RestSharp.Deserializers.IDeserializer Deserializer
		{
			get { return _deserializer ?? GetDefaultSerializer(); }
			set
			{
				if (value == null)
					throw new ArgumentNullException("value");
				if (_deserializer != null)
					throw new InvalidOperationException("Deserializer already set.");
				_deserializer = value;
			}
		}

		/// <summary>
		/// Creates an instance of IRestClient.
		/// </summary>
		/// <param name="apiBaseUrl">The base URL to be used by the client</param>
		/// <returns>An instance of IRestClient.</returns>
		public IRestClient CreateRestClient(string apiBaseUrl)
		{
			var client = new RestClient(Deserializer, apiBaseUrl);
			return client;
		}

		private ManateeSerializer GetDefaultSerializer()
		{
			var serializer = new ManateeSerializer();
			_serializer = serializer;
			_deserializer = serializer;
			return serializer;
		}
	}
}
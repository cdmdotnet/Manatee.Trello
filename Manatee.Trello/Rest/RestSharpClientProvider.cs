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
	Class Name:		RestSharpClientProvider
	Purpose:		Implements IRestClientProvider to provide instances of
					RestSharp.RestClient wrapped in an IRestClient implementation.

***************************************************************************************/
using System;
using Manatee.Trello.Json;

namespace Manatee.Trello.Rest
{
	internal class RestSharpClientProvider : IRestClientProvider
	{
		private RestSharpRequestProvider _requestProvider;

		private RestSharpSerializer _serializer;
		private RestSharpDeserializer _deserializer;

		public IRestRequestProvider RequestProvider
		{
			get { return _requestProvider ?? (_requestProvider = new RestSharpRequestProvider(VerifySerializer())); }
		}

		public ISerializer Serializer
		{
			get
			{
				return VerifySerializer().Inner;
			}
			set
			{
				if (value == null)
					throw new ArgumentNullException("value");
				_serializer = new RestSharpSerializer(value);
			}
		}
		public IDeserializer Deserializer
		{
			get
			{
				return VerifyDeserializer().Inner;
			}
			set
			{
				if (value == null)
					throw new ArgumentNullException("value");
				_deserializer = new RestSharpDeserializer(value);
			}
		}

		public IRestClient CreateRestClient(string apiBaseUrl)
		{
			var client = new RestSharpClient(VerifyDeserializer(), apiBaseUrl);
			return client;
		}

		private void GetDefaultSerializer()
		{
			_serializer = new RestSharpSerializer(Options.Serializer);
			_deserializer = new RestSharpDeserializer(Options.Deserializer);
		}
		private RestSharpSerializer VerifySerializer()
		{
			if (_serializer == null)
				GetDefaultSerializer();
			return _serializer;
		}
		private RestSharpDeserializer VerifyDeserializer()
		{
			if (_serializer == null)
				GetDefaultSerializer();
			return _deserializer;
		}
	}
}
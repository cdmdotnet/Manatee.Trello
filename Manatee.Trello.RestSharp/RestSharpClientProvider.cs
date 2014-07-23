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

using Manatee.Trello.Rest;

namespace Manatee.Trello.RestSharp
{
	/// <summary>
	/// Implements IRestClientProvider to provide instances of RestSharp.RestClient
	/// wrapped in an IRestClient implementation.
	/// </summary>
	public class RestSharpClientProvider : IRestClientProvider
	{
		private RestSharpRequestProvider _requestProvider;
		private RestSharpSerializer _serializer;
		private RestSharpDeserializer _deserializer;
		
		/// <summary>
		/// Creates requests for the client.
		/// </summary>
		public IRestRequestProvider RequestProvider
		{
			get { return _requestProvider ?? (_requestProvider = new RestSharpRequestProvider(VerifySerializer())); }
		}

		/// <summary>
		/// Creates an instance of IRestClient.
		/// </summary>
		/// <param name="apiBaseUrl">The base URL to be used by the client</param>
		/// <returns>An instance of IRestClient.</returns>
		public IRestClient CreateRestClient(string apiBaseUrl)
		{
			var client = new RestSharpClient(TrelloConfiguration.Log, VerifyDeserializer(), apiBaseUrl);
			return client;
		}

		private void GetDefaultSerializer()
		{
			_serializer = new RestSharpSerializer(TrelloConfiguration.Serializer);
			_deserializer = new RestSharpDeserializer(TrelloConfiguration.Deserializer);
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
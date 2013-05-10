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
 
	File Name:		RestSharpClient.cs
	Namespace:		Manatee.Trello.Rest
	Class Name:		RestSharpClient<T>
	Purpose:		A RestSharp client which implemements IRestClient.

***************************************************************************************/
using System;
using System.Net;

namespace Manatee.Trello.Rest
{
	internal class RestSharpClient : RestSharp.RestClient, IRestClient
	{
		private readonly RestSharp.Deserializers.IDeserializer _deserializer;

		public RestSharpClient(RestSharp.Deserializers.IDeserializer deserializer, string apiBaseUrl)
			: base(apiBaseUrl)
		{
			_deserializer = deserializer;
			AddHandler("application/json", _deserializer);
			AddHandler("text/json", _deserializer);
		}

		public IRestResponse<T> Execute<T>(IRestRequest request)
			where T : class
		{
			var restSharpRequest = (RestSharpRequest)request;
			var restSharpResponse = base.Execute(restSharpRequest);
			ValidateResponse(restSharpResponse);
			var data = _deserializer.Deserialize<T>(restSharpResponse);
			return new RestSharpResponse<T>(restSharpResponse, data);
		}

		private static void ValidateResponse(RestSharp.IRestResponse response)
		{
			if (response == null)
				throw new WebException("Received null response from Trello.");
			if (response.StatusCode >= HttpStatusCode.BadRequest)
				throw new WebException(string.Format("Trello returned with an error.\nError Message: {0}\nContent: {1}", response.ErrorMessage, response.Content), response.ErrorException);
		}
	}
}
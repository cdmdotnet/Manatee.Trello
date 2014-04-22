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
 
	File Name:		RestSharpRequestProvider.cs
	Namespace:		Manatee.Trello.Rest
	Class Name:		RestSharpRequestProvider
	Purpose:		A request object for use with all REST calls.

***************************************************************************************/

using System.Collections.Generic;
using Manatee.Trello.Rest;
using RestSharp.Serializers;

namespace Manatee.Trello.RestSharp
{
	internal class RestSharpRequestProvider : IRestRequestProvider
	{
		private readonly ISerializer _serializer;

		public RestSharpRequestProvider(ISerializer serializer)
		{
			_serializer = serializer;
		}

		public IRestRequest Create(string endpoint, IDictionary<string, object> parameters = null)
		{
			var request = new RestSharpRequest(_serializer, endpoint);
			if (parameters != null)
				foreach (var parameter in parameters)
				{
                    if (parameter.Key == "file")
                    {
                        var rf = (RestFile)parameter.Value;
                        request.AddFile(parameter.Key, rf.ContentBytes, rf.FileName);
                    }
                    else
                    {
                        request.AddParameter(parameter.Key, parameter.Value);
                    }
				}
			return request;
		}
		public IRestRequest Create(IRestRequest request)
		{
			var retVal = new RestSharpRequest(_serializer, request.Resource) {Method = request.Method};
			foreach (var parameter in request.Parameters)
			{
				retVal.AddParameter(parameter.Key, parameter.Value);
			}
			return retVal;
		}
	}
}
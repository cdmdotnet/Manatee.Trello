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
 
	File Name:		RestSharpRequest.cs
	Namespace:		Manatee.Trello.Rest
	Class Name:		RestSharpRequest
	Purpose:		A request object for use with all REST calls.

***************************************************************************************/

using System;
using System.Linq;
using Manatee.Trello.Rest;
using RestSharp;
using RestSharp.Serializers;
using IRestRequest = Manatee.Trello.Rest.IRestRequest;
using IRestResponse = Manatee.Trello.Rest.IRestResponse;

namespace Manatee.Trello.RestSharp
{
	internal class RestSharpRequest : RestRequest, IRestRequest
	{
		private bool _hasBody;

		public new RestMethod Method
		{
			get { return GetMethod(); }
			set { SetMethod(value); }
		}
		public IRestResponse Response { get; set; }

		public RestSharpRequest(ISerializer serializer, string endpoint)
			: base(endpoint)
		{
			RequestFormat = DataFormat.Json;
			DateFormat = "yyyy-MM-ddTHH:mm:ss.fffZ";
			JsonSerializer = serializer;
		}

		public new void AddParameter(string name, object value)
		{
			if (_hasBody)
			{
				if (!Resource.Contains("?"))
					Resource += "?";
				else
					Resource += "&";
				Resource += string.Format("{0}={1}", name, value);
				return;
			}
			AddParameter(name, value, ParameterType.GetOrPost);
		}
		public new void AddBody(object body)
		{
			if (Parameters.Any())
			{
				var parameterList = Parameters.Where(p => p.Type == ParameterType.GetOrPost).ToList();
#if NET35 || NET35C
				var parameterCallout = string.Join("&", parameterList.Select(p => string.Format("{0}={1}", p.Name, p.Value)).ToArray());
#elif NET4 || NET4C || NET45
				var parameterCallout = string.Join("&", parameterList.Select(p => string.Format("{0}={1}", p.Name, p.Value)));
#endif
				Resource += string.Format("?{0}", parameterCallout);
				foreach (var parameter in parameterList)
				{
					Parameters.Remove(parameter);
				}
			}
			// See http://boredwookie.net/index.php/blog/how-get-restsharp-send-content-type-header-applicationjson/.
			// Due to the way that R# tries to determine the Content-Type header automatically, we can't use AddBody() here.
			// Instead we serialze the content manually and add the parameter explicitly as the body.
			var content = JsonSerializer.Serialize(body);
			AddParameter("application/json", content, ParameterType.RequestBody);
			_hasBody = true;
		}

		private RestMethod GetMethod()
		{
			switch (base.Method)
			{
				case global::RestSharp.Method.GET:
					return RestMethod.Get;
				case global::RestSharp.Method.POST:
					return RestMethod.Post;
				case global::RestSharp.Method.PUT:
					return RestMethod.Put;
				case global::RestSharp.Method.DELETE:
					return RestMethod.Delete;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}
		private void SetMethod(RestMethod value)
		{
			switch (value)
			{
				case RestMethod.Get:
					base.Method = global::RestSharp.Method.GET;
					break;
				case RestMethod.Put:
					base.Method = global::RestSharp.Method.PUT;
					break;
				case RestMethod.Post:
					base.Method = global::RestSharp.Method.POST;
					break;
				case RestMethod.Delete:
					base.Method = global::RestSharp.Method.DELETE;
					break;
				default:
					throw new ArgumentOutOfRangeException("value");
			}
		}
	}
}
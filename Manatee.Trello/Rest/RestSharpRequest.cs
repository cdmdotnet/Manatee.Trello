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
using RestSharp;

namespace Manatee.Trello.Rest
{
	internal class RestSharpRequest : RestRequest, IRestRequest
	{
		public new RestMethod Method
		{
			get { return GetMethod(); }
			set { SetMethod(value); }
		}

		public RestSharpRequest(RestSharp.Serializers.ISerializer serializer, string endpoint)
			: base(endpoint)
		{
			RequestFormat = DataFormat.Json;
			DateFormat = "yyyy-MM-ddTHH:mm:ss.fffZ";
			JsonSerializer = serializer;
		}

		public new void AddParameter(string name, object value)
		{
			base.AddParameter(name, value);
		}
		public new void AddBody(object body)
		{
			base.AddBody(body);
		}

		private RestMethod GetMethod()
		{
			switch (base.Method)
			{
				case RestSharp.Method.GET:
					return RestMethod.Get;
				case RestSharp.Method.POST:
					return RestMethod.Post;
				case RestSharp.Method.PUT:
					return RestMethod.Put;
				case RestSharp.Method.DELETE:
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
					base.Method = RestSharp.Method.GET;
					break;
				case RestMethod.Put:
					base.Method = RestSharp.Method.PUT;
					break;
				case RestMethod.Post:
					base.Method = RestSharp.Method.POST;
					break;
				case RestMethod.Delete:
					base.Method = RestSharp.Method.DELETE;
					break;
				default:
					throw new ArgumentOutOfRangeException("value");
			}
		}
	}
}
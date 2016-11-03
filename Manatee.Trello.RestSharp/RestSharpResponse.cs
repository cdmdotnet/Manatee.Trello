using System;
using IRestResponse = RestSharp.IRestResponse;

namespace Manatee.Trello.RestSharp
{
	internal class RestSharpResponse : Rest.IRestResponse
	{
		private readonly IRestResponse _response;

		public string Content => _response.Content;
		public Exception Exception { get; set; }

		public RestSharpResponse(IRestResponse response)
		{
			_response = response;
		}
	}

	internal class RestSharpResponse<T> : RestSharpResponse, Rest.IRestResponse<T>
	{
		public T Data { get; private set; }

		public RestSharpResponse(IRestResponse response, T data)
			: base(response)
		{
			Data = data;
		}
	}
}
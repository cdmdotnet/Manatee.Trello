using System;
using System.Net;

namespace Manatee.Trello.RestSharp
{
	public class RestSharpResponse : Rest.IRestResponse
	{
		public string Content { get; set; }
		public Exception Exception { get; set; }
		public HttpStatusCode StatusCode { get; set; }
	}

	public class RestSharpResponse<T> : RestSharpResponse, Rest.IRestResponse<T>
	{
		public T Data { get; set; }
	}
}
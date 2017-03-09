using System;
using System.Net;
using Manatee.Trello.Rest;

namespace Manatee.Trello.WebApi
{
	public class WebApiRestResponse : IRestResponse
	{
		public string Content { get; set; }
		public Exception Exception { get; set; }
		public HttpStatusCode StatusCode { get; set; }
	}

	public class WebApiRestResponse<T> : WebApiRestResponse, IRestResponse<T>
	{
		public T Data { get; set; }
	}
}
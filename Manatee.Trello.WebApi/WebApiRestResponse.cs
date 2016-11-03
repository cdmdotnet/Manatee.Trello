using System;
using Manatee.Trello.Rest;

namespace Manatee.Trello.WebApi
{
	internal class WebApiRestResponse : IRestResponse
	{
		public string Content { get; set; }
		public Exception Exception { get; set; }
	}

	internal class WebApiRestResponse<T> : WebApiRestResponse, IRestResponse<T>
	{
		public T Data { get; set; }
	}
}
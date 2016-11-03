using System;
using Manatee.Trello.Rest;

namespace Manatee.Trello.Internal.RequestProcessing
{
	internal class NullRestResponse : IRestResponse
	{
		public string Content { get; set; }
		public Exception Exception { get; set; }
	}
	internal class NullRestResponse<T> : NullRestResponse, IRestResponse<T>
	{
		public T Data { get; set; }
	}
}
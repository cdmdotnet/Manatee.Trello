using System;
using System.Net;
using Manatee.Trello.Rest;

namespace Manatee.Trello.UnitTests.Framework
{
	internal class MockRestResponse : IRestResponse
	{
		public string Content { get; set; }
		public Exception Exception { get; set; }
		public HttpStatusCode StatusCode { get; set; }
	}
}
using Manatee.Trello.Rest;

namespace Manatee.Trello.UnitTests.Framework
{
	internal class MockedRestResponse<T> : MockRestResponse, IRestResponse<T>
	{
		public T Data { get; set; }
	}
}
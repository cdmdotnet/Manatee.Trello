using System;
using Manatee.Trello.Internal.RequestProcessing;
using Manatee.Trello.Rest;
using Moq;

namespace Manatee.Trello.Test.Unit.Mocks
{
	public class MockRequestProcessor : IRestRequestProcessor
	{
		public bool IsActive { get; set; }
		public string AppKey { get; private set; }
		public string UserToken { get; set; }
		public void AddRequest<T>(IRestRequest request) where T : class
		{
			if (!IsActive) return;
			var response = new Mock<IRestResponse<T>>();
			response.SetupGet(r => r.Data).Returns(new Mock<T>().Object);
			request.Response = response.Object;
		}
		public void ShutDown() { }
		public void NetworkStatusChanged(object sender, EventArgs e) { }
	}
}
using Manatee.Trello.Rest;
using Moq;

namespace Manatee.Trello.UnitTests.Framework
{
	internal static class ClientMockExtensions
	{
		public static void SetupFor<T>(this Mock<IRestClient> client, T data = null)
			where T : class
		{
			client.Setup(c => c.Execute<T>(It.IsAny<IRestRequest>()))
			      .Returns(new MockedRestResponse<T> {Data = data ?? MockedJsonFactory.Instance.Create<T>()});
		}
	}
}
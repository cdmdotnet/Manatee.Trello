using Manatee.Trello.Json;
using Moq;

namespace Manatee.Trello.UnitTests.Framework
{
	internal class MockedJsonFactory : IJsonFactory
	{
		public static MockedJsonFactory Instance { get; }

		static MockedJsonFactory()
		{
			Instance = new MockedJsonFactory();
		}
		private MockedJsonFactory() { }

		public T Create<T>()
			where T : class
		{
			var mock = new Mock<T>();
			mock.SetupAllProperties();
			return mock.Object;
		}
	}
}

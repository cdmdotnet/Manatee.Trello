using System;
using System.Threading;
using Manatee.Trello.Json;
using Manatee.Trello.Rest;
using Manatee.Trello.Tests.Common;
using Manatee.Trello.UnitTests.Framework;
using Moq;
using NUnit.Framework;

namespace Manatee.Trello.UnitTests
{
	[TestFixture]
	public class BoardTest
	{
		private static MockedRestClientProvider Initialize()
		{
			var clientProvider = new MockedRestClientProvider();
			TrelloConfiguration.JsonFactory = MockedJsonFactory.Instance;
			TrelloConfiguration.RestClientProvider = clientProvider;
			return clientProvider;
		}

		[Test]
		public void CreationDoesNotDownloadDetails()
		{
			var clientProvider =  Initialize();

			var board = new Board(TrelloIds.ShortFakeId);

			clientProvider.ClientMock.Verify(c => c.Execute<IJsonBoard>(It.IsAny<IRestRequest>()), Times.Never);
		}

		[Test]
		public void IdAccessTriggersCall()
		{
			var clientProvider = Initialize();
			clientProvider.ClientMock.SetupFor<IJsonBoard>();

			var board = new Board(TrelloIds.ShortFakeId);
			var id = board.Id;

			clientProvider.ClientMock.Verify(c => c.Execute<IJsonBoard>(It.IsAny<IRestRequest>()), Times.Once);
		}

		[Test]
		public void RapidIdAccessTriggersSingleCall()
		{
			var clientProvider = Initialize();
			var data = new Mock<IJsonBoard>();
			data.SetupGet(d => d.Id).Returns(TrelloIds.FakeId);
			clientProvider.ClientMock.SetupFor(data.Object);

			var board = new Board(TrelloIds.ShortFakeId);
			var id = board.Id;
			var id2 = board.Id;

			clientProvider.ClientMock.Verify(c => c.Execute<IJsonBoard>(It.IsAny<IRestRequest>()), Times.Once);
		}

		[Test]
		public void DelayedNameAccessTriggersMultipleCalls()
		{
			var clientProvider = Initialize();
			var data = new Mock<IJsonBoard>();
			data.SetupGet(d => d.Id).Returns(TrelloIds.FakeId);
			data.SetupGet(d => d.Name).Returns("Name");
			clientProvider.ClientMock.SetupFor(data.Object);
			TrelloConfiguration.ExpiryTime = TimeSpan.FromSeconds(1);

			var board = new Board(TrelloIds.FakeId);
			var name = board.Name;
			Thread.Sleep(1500);
			var name2 = board.Name;

			clientProvider.ClientMock.Verify(c => c.Execute<IJsonBoard>(It.IsAny<IRestRequest>()), Times.Exactly(2));
			TrelloConfiguration.ExpiryTime = TimeSpan.FromMilliseconds(100);
		}
	}
}

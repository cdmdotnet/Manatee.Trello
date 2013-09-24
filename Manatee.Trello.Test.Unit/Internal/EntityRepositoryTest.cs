using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Manatee.Trello.Contracts;
using Manatee.Trello.Internal;
using Manatee.Trello.Internal.DataAccess;
using Manatee.Trello.Internal.Genesis;
using Manatee.Trello.Internal.RequestProcessing;
using Manatee.Trello.Rest;
using Manatee.Trello.Test.Unit.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Manatee.Trello.Test.Unit.Internal
{
	[TestClass]
	public class EntityRepositoryTest : TrelloTestBase<IEntityRepository>
	{
		#region Dependencies

		private class DependencyCollection
		{
			public Mock<IJsonRepository> JsonRepository { get; private set; }
			public Mock<IEndpointFactory> EndpointFactory { get; private set; }
			public Mock<IEntityFactory> EntityFactory { get; private set; }
			public Mock<IOfflineChangeQueue> OfflineChangeQueue { get; private set; }
			public ExpiringObject Entity { get; set; }
			public Endpoint Endpoint { get; set; }

			public DependencyCollection()
			{
				JsonRepository = new Mock<IJsonRepository>();
				EndpointFactory = new Mock<IEndpointFactory>();
				EntityFactory = new Mock<IEntityFactory>();
				OfflineChangeQueue = new Mock<IOfflineChangeQueue>();

				EndpointFactory.Setup(f => f.Build(It.IsAny<EntityRequestType>(), It.IsAny<IDictionary<string, object>>()))
							   .Callback((EntityRequestType r, IDictionary<string, object> p) => Endpoint = new Endpoint(RestMethod.Get))
							   .Returns(Endpoint);
			}
		}

		private class SystemUnderTest : SystemUnderTest<DependencyCollection>
		{
			public SystemUnderTest()
			{
				Sut = new EntityRepository(Dependencies.JsonRepository.Object,
										   Dependencies.EndpointFactory.Object,
										   Dependencies.EntityFactory.Object,
										   Dependencies.OfflineChangeQueue.Object,
										   TimeSpan.FromMinutes(30));
			}
		}

		#endregion

		#region Data

		private SystemUnderTest _test;

		#endregion

		#region Scenarios

		[TestMethod]
		public void Refresh()
		{
			throw new NotImplementedException();
		}
		[TestMethod]
		public void RefreshCollection()
		{
			throw new NotImplementedException();
		}
		[TestMethod]
		public void Download()
		{
			throw new NotImplementedException();
		}
		[TestMethod]
		public void Upload()
		{
			var feature = CreateFeature();
			var parameters = new Dictionary<string, object>();

			feature.WithScenario("Upload a request")
				   .Given(ARepository)
				   .When(UploadIsCalled, EntityRequestType.Board_Read_Lists, parameters)
				   .Then(EndpointFactoryBuildIsCalled, EntityRequestType.Board_Read_Lists, parameters)
				   .And(JsonRepositoryExecuteIsCalled<object>, parameters)
				   .And(ExceptionIsNotThrown)

				   .Execute();
		}
		[TestMethod]
		public void NetworkStatusChanged()
		{
			throw new NotImplementedException();
		}

		#endregion

		#region Given

		private void ARepository()
		{
			_test = new SystemUnderTest();
		}
		private void AnEntity()
		{
			_test.Dependencies.Entity = new MockEntity();
		}

		#endregion

		#region When

		private void RefreshIsCalled(EntityRequestType request)
		{
			Execute(() => _test.Sut.Refresh(_test.Dependencies.Entity, request));
		}
		private void RefreshCollectionIsCalled(EntityRequestType request)
		{
			
		}
		private void DownloadIsCalled(EntityRequestType request, IDictionary<string, object> parameters)
		{
			Execute(() => _test.Sut.Download<MockEntity>(request, parameters));
		}
		private void UploadIsCalled(EntityRequestType request, IDictionary<string, object> parameters)
		{
			Execute(() => _test.Sut.Upload(request, parameters));
		}
		private void NetworkStatusChangedIsTriggered()
		{
			Execute(() => _test.Sut.NetworkStatusChanged(null, null));
		}

		#endregion

		#region Then

		[ParameterizedMethodFormat("EndpointFactory.Build({0}, {1}) is called")]
		private void EndpointFactoryBuildIsCalled(EntityRequestType request, IDictionary<string, object> parameters)
		{
			_test.Dependencies.EndpointFactory.Verify(f => f.Build(It.Is<EntityRequestType>(r => r == request),
																   It.Is<IDictionary<string, object>>(d => Equals(d, parameters))));
		}
		[ParameterizedMethodFormat("EndpointFactory.Build() is not called")]
		private void EndpointFactoryBuildIsNotCalled()
		{
			_test.Dependencies.EndpointFactory.Verify(f => f.Build(It.IsAny<EntityRequestType>(),
																   It.IsAny<IDictionary<string, object>>()), Times.Never());
		}
		[GenericMethodFormat("JsonRepository.Execute<{0}>({1}) is called")]
		private void JsonRepositoryExecuteIsCalled<T>(IDictionary<string, object> parameters)
			where T : class
		{
			_test.Dependencies.JsonRepository.Verify(r => r.Execute<T>(It.Is<Endpoint>(e => Equals(e, _test.Dependencies.Endpoint)),
																   It.Is<IDictionary<string, object>>(d => Equals(d, parameters))));
		}

		#endregion
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Manatee.Trello.Contracts;
using Manatee.Trello.Internal;
using Manatee.Trello.Internal.DataAccess;
using Manatee.Trello.Internal.Genesis;
using Manatee.Trello.Internal.Json;
using Manatee.Trello.Internal.RequestProcessing;
using Manatee.Trello.Json;
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
							   .Returns(() => Endpoint);
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
			var feature = CreateFeature();

			feature.WithScenario("Refresh an entity")
				   .Given(ARepository)
				   .And(AnExpiredEntity<Board, InnerJsonBoard>)
				   .When(RefreshIsCalled<Board>, EntityRequestType.Board_Read_Refresh)
				   .Then(EndpointFactoryBuildIsCalled, EntityRequestType.Board_Read_Refresh, (IDictionary<string, object>) null)
				   .And(JsonRepositoryExecuteIsCalled<IJsonBoard>, (IDictionary<string, object>) null)
				   .And(ExceptionIsNotThrown)

				   .Execute();

		}
		[TestMethod]
		public void RefreshCollection()
		{
			var feature = CreateFeature();
			var parameters = new Dictionary<string, object>();

			feature.WithScenario("Refresh a collection")
				   .Given(ARepository)
				   .And(AnExpiredList<Board, InnerJsonBoard>)
				   .When(RefreshCollectionIsCalled<Board>, EntityRequestType.Member_Read_Boards)
				   .Then(EndpointFactoryBuildIsCalled, EntityRequestType.Board_Read_Refresh, parameters)
				   .And(JsonRepositoryExecuteIsCalled<IJsonBoard>, parameters)
				   .And(EntityFactoryCreateEntityIsCalled<Board>)
				   .And(ExceptionIsNotThrown)

				   .Execute();
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
		private void AnExpiredEntity<T, TJson>()
			where T : ExpiringObject, new()
			where TJson : class, new()
		{
			_test.Dependencies.Entity = new T {EntityRepository = new Mock<IEntityRepository>().Object};
			_test.Dependencies.Entity.MarkForUpdate();
			_test.Dependencies.JsonRepository.Setup(r => r.Execute<TJson>(It.IsAny<Endpoint>(), It.IsAny<IDictionary<string, object>>()))
				 .Returns(new TJson());
		}
		private void AnExpiredList<T, TJson>()
			where T : ExpiringObject, IEquatable<T>, IComparable<T>
			where TJson : class, new()
		{
			_test.Dependencies.Entity = new ExpiringList<T>(new Board(), EntityRequestType.Member_Read_Boards);
			_test.Dependencies.JsonRepository.Setup(r => r.Execute<IEnumerable<TJson>>(It.IsAny<Endpoint>(), It.IsAny<IDictionary<string, object>>()))
				 .Returns(new List<TJson> {new TJson()});
		}

		#endregion

		#region When

		private void RefreshIsCalled<T>(EntityRequestType request)
			where T : ExpiringObject
		{
			Execute(() => _test.Sut.Refresh<T>((T) _test.Dependencies.Entity, request));
		}
		private void RefreshCollectionIsCalled<T>(EntityRequestType request)
			where T : ExpiringObject, IEquatable<T>, IComparable<T>
		{
			Execute(() => _test.Sut.RefreshCollection<T>(_test.Dependencies.Entity, request));
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
			if (parameters == null)
				parameters = _test.Dependencies.Entity.Parameters;
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
			if (parameters == null)
				parameters = _test.Dependencies.Entity.Parameters;
			_test.Dependencies.JsonRepository.Verify(r => r.Execute<T>(It.Is<Endpoint>(e => Equals(e, _test.Dependencies.Endpoint)),
																   It.Is<IDictionary<string, object>>(d => Equals(d, parameters))));
		}
		[GenericMethodFormat("EntityFactory.CreateEntity<{0}>() is called")]
		private void EntityFactoryCreateEntityIsCalled<T>()
			where T : ExpiringObject
		{
			_test.Dependencies.EntityFactory.Verify(f => f.CreateEntity<T>());
		}

		#endregion
	}
}

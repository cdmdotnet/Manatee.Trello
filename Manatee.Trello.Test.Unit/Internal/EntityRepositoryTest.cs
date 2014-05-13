using System;
using System.Collections.Generic;
using Manatee.Trello.Contracts;
using Manatee.Trello.Internal;
using Manatee.Trello.Internal.DataAccess;
using Manatee.Trello.Internal.Json;
using Manatee.Trello.Json;
using Manatee.Trello.Test.Unit.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Manatee.Trello.Test.Unit.Internal
{
	[TestClass]
	public class EntityRepositoryTest : TrelloTestBase
	{
		#region Dependencies

		private class DependencyCollection
		{
			public Mock<ICache> Cache { get; private set; }
			public Mock<IValidator> Validator { get; private set; }
			public Mock<ILog> Log { get; private set; }
			public ExpiringObject Entity { get; set; }
			public Endpoint Endpoint { get; set; }
			public TrelloAuthorization Auth { get; set; }

			public DependencyCollection()
			{
				Cache = new Mock<ICache>();
				Validator = new Mock<IValidator>();
				Log = new Mock<ILog>();
			}
		}

		private class SystemUnderTest : SystemUnderTest<EntityRepository, DependencyCollection>
		{
			public SystemUnderTest()
			{
				Sut = new EntityRepository(TimeSpan.FromMinutes(30),
										   Dependencies.Validator.Object,
										   Dependencies.Auth);
			}
		}

		#endregion

		#region Data

		private SystemUnderTest _test;

		#endregion

		#region Scenarios

		// TODO: Add checks for caching

		[TestMethod]
		public void Refresh()
		{
			var feature = CreateFeature();

			feature.WithScenario("Refresh an entity")
				   .Given(ARepository)
				   .And(AnExpiredEntity<Board, InnerJsonBoard>)
				   .When(RefreshIsCalled<Board>, EntityRequestType.Board_Read_Refresh)
				   .Then(ExceptionIsNotThrown)

				   .Execute();

		}
		[TestMethod]
		public void RefreshCollection()
		{
			var feature = CreateFeature();

			feature.WithScenario("Refresh a collection")
				   .Given(ARepository)
				   .And(AnExpiredList<Board, InnerJsonBoard>)
				   .When(RefreshCollectionIsCalled<Board>, EntityRequestType.Member_Read_Boards)
				   .Then(ExceptionIsNotThrown)

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
				   .Then(ExceptionIsNotThrown)

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
		}
		private void AnExpiredList<T, TJson>()
			where T : ExpiringObject, IEquatable<T>, IComparable<T>
			where TJson : class, new()
		{
			_test.Dependencies.Entity = new ExpiringCollection<T>(new Board(), EntityRequestType.Member_Read_Boards);
		}

		#endregion

		#region When

		private void RefreshIsCalled<T>(EntityRequestType request)
			where T : ExpiringObject
		{
			Execute(() => _test.Sut.Refresh((T) _test.Dependencies.Entity, request));
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

		#endregion

		#region Then

		#endregion
	}
}

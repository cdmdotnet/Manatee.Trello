using System;
using System.Collections.Generic;
using Manatee.Trello.Contracts;
using Manatee.Trello.Internal.DataAccess;
using Manatee.Trello.Test.Unit.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Manatee.Trello.Test.Unit.Internal
{
	[TestClass]
	public class CachingEntityRepositoryTest : TrelloTestBase<IEntityRepository>
	{
		#region Dependencies

		private class DependencyCollection
		{
			public Mock<IEntityRepository> EntityRepository { get; private set; }
			public Mock<ICache> Cache { get; private set; }

			public DependencyCollection()
			{
				EntityRepository = new Mock<IEntityRepository>();
				Cache = new Mock<ICache>();

				EntityRepository.SetupGet(r => r.EntityDuration)
								.Returns(TimeSpan.FromDays(1));
			}
		}

		private class RepositoryUnderTest : SystemUnderTest<DependencyCollection>
		{
			public RepositoryUnderTest()
			{
				Sut = new CachingEntityRepository(Dependencies.EntityRepository.Object, Dependencies.Cache.Object);
			}
		}

		#endregion

		#region Data

		private RepositoryUnderTest _test;

		#endregion

		#region Scenarios

		[TestMethod]
		public void Refresh()
		{
			var feature = CreateFeature();
			var entity = new MockEntity();
			const EntityRequestType request = EntityRequestType.Board_Read_Lists;

			feature.WithScenario("Cache is not used on Refresh")
				.Given(ACachingRepository)
				.When(RefreshIsCalled, entity, request)
				.Then(CacheFindIsNotCalled<MockEntity>)
				.And(InnerRepositoryRefreshIsCalled, entity, request)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void RefreshCollecion()
		{
			var feature = CreateFeature();
			var entity = new MockEntity();
			const EntityRequestType request = EntityRequestType.Board_Read_Lists;

			feature.WithScenario("Cache is not used on RefreshCollection")
				.Given(ACachingRepository)
				.When(RefreshCollectionIsCalled, entity, request)
				.Then(CacheFindIsNotCalled<MockEntity>)
				.And(InnerRepositoryRefreshCollectionIsCalled, entity, request)
				.And(ExceptionIsNotThrown)

				.Execute();
		}

		[TestMethod]
		public void Download()
		{
			var feature = CreateFeature();
			var parameters = new Dictionary<string, object> {{"_id", TrelloIds.Test}};
			const EntityRequestType request = EntityRequestType.Board_Read_Lists;

			feature.WithScenario("Cache does not contain entity, with ID")
				   .Given(ACachingRepository)
				   .And(CacheDoesNotContainEntity<MockEntity>)
				   .When(DownloadIsCalled<MockEntity>, request, parameters)
				   .Then(CacheFindIsCalled<MockEntity>)
				   .And(InnerRepositoryDownloadIsCalled<MockEntity>, request)
				   .And(ExceptionIsNotThrown)

				   .WithScenario("Cache does not contain entity, without ID")
				   .Given(ACachingRepository)
				   .And(CacheDoesNotContainEntity<MockEntity>)
				   .When(DownloadIsCalled<MockEntity>, request, (Dictionary<string, object>) null)
				   .Then(CacheFindIsNotCalled<MockEntity>)
				   .And(InnerRepositoryDownloadIsCalled<MockEntity>, request)
				   .And(ExceptionIsNotThrown)

				   .WithScenario("Cache contains entity")
				   .Given(ACachingRepository)
				   .And(CacheContainsEntity, new MockEntity())
				   .When(DownloadIsCalled<MockEntity>, request, parameters)
				   .Then(CacheFindIsCalled<MockEntity>)
				   .And(InnerRepositoryDownloadIsNotCalled<MockEntity>)
				   .And(ExceptionIsNotThrown)

				   .Execute();
		}

		[TestMethod]
		public void Upload()
		{
			var feature = CreateFeature();
			const EntityRequestType request = EntityRequestType.Board_Read_Lists;

			feature.WithScenario("Cache is not used on Upload")
				.Given(ACachingRepository)
				.When(UploadIsCalled, request)
				.Then(CacheFindIsNotCalled<MockEntity>)
				.And(InnerRepositoryUploadIsCalled, request)
				.And(ExceptionIsNotThrown)

				.Execute();
		}

		#endregion

		#region Given

		private void ACachingRepository()
		{
			_test = new RepositoryUnderTest();
		}
		private void CacheDoesNotContainEntity<T>()
		{
			_test.Dependencies.Cache.Setup(c => c.Find(It.IsAny<Func<T, bool>>(), It.IsAny<Func<T>>()))
				 .Returns((Func<T, bool> match, Func<T> search) => search());
		}
		private void CacheContainsEntity<T>(T entity)
		{
			_test.Dependencies.Cache.Setup(c => c.Find(It.IsAny<Func<T, bool>>(), It.IsAny<Func<T>>()))
				 .Returns(entity);
		}

		#endregion

		#region When

		[GenericMethodFormat("CachingRepository.Refresh<{0}>({1}, {2}) is called")]
		private void RefreshIsCalled<T>(T entity, EntityRequestType request)
			where T : ExpiringObject
		{
			Execute(() => _test.Sut.Refresh(entity, request));
		}
		[GenericMethodFormat("CachingRepository.RefreshCollection<{0}>({1}, {2}) is called")]
		private void RefreshCollectionIsCalled<T>(T entity, EntityRequestType request)
			where T : ExpiringObject, IEquatable<T>, IComparable<T>
		{
			Execute(() => _test.Sut.RefreshCollection<T>(entity, request));
		}
		[GenericMethodFormat("CachingRepository.Download<{0}>({1}) is called")]
		private void DownloadIsCalled<T>(EntityRequestType request, Dictionary<string, object> parameters = null)
			where T : ExpiringObject
		{
			Execute(() => _test.Sut.Download<T>(request, parameters ?? new Dictionary<string, object>()));
		}
		private void UploadIsCalled(EntityRequestType request)
		{
			Execute(() => _test.Sut.Upload(request, new Dictionary<string, object>()));
		}

		#endregion

		#region Then

		[GenericMethodFormat("Cache.Find<{0}>() is called")]
		private void CacheFindIsCalled<T>()
		{
			_test.Dependencies.Cache.Verify(c => c.Find(It.IsAny<Func<T, bool>>(), It.IsAny<Func<T>>()));
		}
		[GenericMethodFormat("Cache.Find<{0}>() is not called")]
		private void CacheFindIsNotCalled<T>()
		{
			_test.Dependencies.Cache.Verify(c => c.Find(It.IsAny<Func<T, bool>>(), It.IsAny<Func<T>>()), Times.Never());
		}
		[GenericMethodFormat("EntityRepository.Refresh<{0}>({1}, {2}) is not called")]
		private void InnerRepositoryRefreshIsCalled<T>(T entity, EntityRequestType request)
			where T : ExpiringObject
		{
			_test.Dependencies.EntityRepository.Verify(r => r.Refresh(It.Is<T>(e => Equals(e, entity)),
																	  It.Is<EntityRequestType>(t => t == request)));
		}
		[GenericMethodFormat("EntityRepository.RefreshCollection<{0}>({1}, {2}) is not called")]
		private void InnerRepositoryRefreshCollectionIsCalled<T>(T entity, EntityRequestType request)
			where T : ExpiringObject, IEquatable<T>, IComparable<T>
		{
			_test.Dependencies.EntityRepository.Verify(r => r.RefreshCollection<T>(It.Is<T>(e => Equals(e, entity)),
																	  It.Is<EntityRequestType>(t => t == request)));
		}
		[GenericMethodFormat("EntityRepository.Download<{0}>({1}) is not called")]
		private void InnerRepositoryDownloadIsCalled<T>(EntityRequestType request)
			where T : ExpiringObject
		{
			_test.Dependencies.EntityRepository.Verify(r => r.Download<T>(It.Is<EntityRequestType>(t => t == request),
																		  It.IsAny<IDictionary<string, object>>()));
		}
		[GenericMethodFormat("EntityRepository.Download<{0}>() is not called")]
		private void InnerRepositoryDownloadIsNotCalled<T>()
			where T : ExpiringObject
		{
			_test.Dependencies.EntityRepository.Verify(r => r.Download<T>(It.IsAny<EntityRequestType>(),
																		  It.IsAny<IDictionary<string, object>>()), Times.Never());
		}
		[ParameterizedMethodFormat("EntityRepository.Upload({0}) is not called")]
		private void InnerRepositoryUploadIsCalled(EntityRequestType request)
		{
			_test.Dependencies.EntityRepository.Verify(r => r.Upload(It.Is<EntityRequestType>(t => t == request),
																	 It.IsAny<IDictionary<string, object>>()));
		}

		#endregion
	}
}

using System;
using System.Linq;
using Manatee.Trello.Contracts;
using Manatee.Trello.Internal;
using Manatee.Trello.Test.Unit.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Manatee.Trello.Test.Unit.Internal
{
	[TestClass]
	public class ExpiringListTest : UnitTestBase
	{
		#region Dependencies

		private class EntityUnderTest : SystemUnderTest<ExpiringCollection<MockEntity>, DependencyCollection>
		{
			internal ExpiringCollection<MockEntity> List
			{
				get { return Sut; }
			}

			public EntityUnderTest()
			{
				Sut = new ExpiringCollection<MockEntity>(new MockEntity(), EntityRequestType.Board_Read_Lists)
					{
						EntityRepository = Dependencies.EntityRepository.Object,
						Validator = Dependencies.Validator.Object
					};
				List.PropagateDependencies();
				List.Update(Enumerable.Empty<MockEntity>());

				Dependencies.EntityRepository.Setup(r => r.RefreshCollection<MockEntity>(It.Is<ExpiringObject>(e => e.Id == List.Id),
				                                                                         It.IsAny<EntityRequestType>()))
				            .Callback((ExpiringObject e, EntityRequestType t) => e.Parameters.Clear())
				            .Returns(true);
			}
		}

		#endregion

		#region Data

		private EntityUnderTest _test;

		#endregion

		#region Scenarios

		[TestMethod]
		public void GetEnumerator()
		{
			var feature = CreateFeature();

			feature.WithScenario("Call GetEnumerator when not expired")
			       .Given(AnExpiringList)
			       .When(GetEnumeratorIsCalled)
			       .Then(RepositoryRefreshCollectionIsNotCalled<MockEntity>)
			       .And(ExceptionIsNotThrown)

			       .WithScenario("Call GetEnumerator when expired")
			       .Given(AnExpiringList)
			       .And(EntityIsExpired)
			       .When(GetEnumeratorIsCalled)
			       .Then(RepositoryRefreshCollectionIsCalled<MockEntity>, EntityRequestType.Board_Read_Lists)
			       .And(ExceptionIsNotThrown)

			       .Execute();
		}

		#endregion

		#region Given

		private void AnExpiringList()
		{
			_test = new EntityUnderTest();
		}
		private void EntityIsExpired()
		{
			_test.List.MarkForUpdate();
		}

		#endregion

		#region When

		private void GetEnumeratorIsCalled()
		{
			Execute(() => _test.List.ToList());
		}

		#endregion

		#region Then

		[GenericMethodFormat("Repository.RefreshCollection<{0}>({1}) is called")]
		private void RepositoryRefreshCollectionIsCalled<TEntity>(EntityRequestType requestType)
			where TEntity : ExpiringObject, IEquatable<TEntity>, IComparable<TEntity>
		{
			_test.Dependencies.EntityRepository.Verify(r => r.RefreshCollection<TEntity>(It.IsAny<ExpiringCollection<TEntity>>(),
			                                                                             It.Is<EntityRequestType>(rt => rt == requestType)), Times.Once());
		}
		[GenericMethodFormat("Repository.RefreshCollection<{0}>() is not called")]
		private void RepositoryRefreshCollectionIsNotCalled<TEntity>()
			where TEntity : ExpiringObject, IEquatable<TEntity>, IComparable<TEntity>
		{
			_test.Dependencies.EntityRepository.Verify(r => r.RefreshCollection<TEntity>(It.IsAny<ExpiringCollection<TEntity>>(),
			                                                                             It.IsAny<EntityRequestType>()), Times.Never());
		}

		#endregion
	}
}

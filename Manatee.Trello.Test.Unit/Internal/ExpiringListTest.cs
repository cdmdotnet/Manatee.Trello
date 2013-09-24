using System;
using System.Collections.Generic;
using System.Linq;
using Manatee.Trello.Contracts;
using Manatee.Trello.Internal;
using Manatee.Trello.Test.Unit.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Manatee.Trello.Test.Unit.Internal
{
	[TestClass]
	public class ExpiringListTest : UnitTestBase<IEnumerable<MockEntity>>
	{
		#region Dependencies

		protected class EntityUnderTest : SystemUnderTest<DependencyCollection>
		{
			internal ExpiringList<MockEntity> List { get { return (ExpiringList<MockEntity>) Sut; } } 

			public EntityUnderTest()
			{
				Sut = new ExpiringList<MockEntity>(new MockEntity(), EntityRequestType.Board_Read_Lists)
					{
						EntityRepository = Dependencies.EntityRepository.Object,
						Log = Dependencies.Log.Object,
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

		protected EntityUnderTest _test;

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
			Execute(() => _test.List.GetEnumerator());
		}

		#endregion

		#region Then

		[GenericMethodFormat("Repository.RefreshCollection<{0}>({1}) is called")]
		protected void RepositoryRefreshCollectionIsCalled<TEntity>(EntityRequestType requestType)
			where TEntity : ExpiringObject, IEquatable<TEntity>, IComparable<TEntity>
		{
			_test.Dependencies.EntityRepository.Verify(r => r.RefreshCollection<TEntity>(It.IsAny<ExpiringList<TEntity>>(),
																						It.Is<EntityRequestType>(rt => rt == requestType)), Times.Once());
		}
		[GenericMethodFormat("Repository.RefreshCollection<{0}>() is not called")]
		protected void RepositoryRefreshCollectionIsNotCalled<TEntity>()
			where TEntity : ExpiringObject, IEquatable<TEntity>, IComparable<TEntity>
		{
			_test.Dependencies.EntityRepository.Verify(r => r.RefreshCollection<TEntity>(It.IsAny<ExpiringList<TEntity>>(),
																						It.IsAny<EntityRequestType>()), Times.Never());
		}

		#endregion
	}
}

using System;
using System.Collections.Generic;
using Manatee.Trello.Contracts;
using Manatee.Trello.Internal;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Manatee.Trello.Test.Unit.Entities
{
	public abstract class EntityTestBase<T, TJson> : UnitTestBase where T : ExpiringObject, new()
	                                                              where TJson : class
	{
		#region Dependencies

		protected class EntityUnderTest : SystemUnderTest<T, DependencyCollection>
		{
			public Mock<TJson> Json { get; private set; }

			public EntityUnderTest()
			{
				Json = new Mock<TJson>();
				Json.SetupAllProperties();

				Sut = new T
					{
						EntityRepository = Dependencies.EntityRepository.Object,
						Validator = Dependencies.Validator.Object
					};
				Sut.PropagateDependencies();
				Sut.ApplyJson(Json.Object);

				Dependencies.EntityRepository.Setup(r => r.Refresh(It.Is<T>(e => e.Id == Sut.Id), It.IsAny<EntityRequestType>()))
				            .Callback((T e, EntityRequestType t) => e.Parameters.Clear())
				            .Returns(true);
				Dependencies.EntityRepository.SetupGet(r => r.AllowSelfUpdate)
				            .Returns(true);
			}
		}

		#endregion

		#region Data

		protected EntityUnderTest _test;

		#endregion

		#region Given

		protected void EntityIsExpired()
		{
			_test.Sut.MarkForUpdate();
		}
		protected void OwnedBy<TEntity>()
			where TEntity : ExpiringObject, new()
		{
			var entity = new Mock<TEntity>();
			_test.Sut.Owner = entity.Object;
		}

		#endregion

		#region Then

		[GenericMethodFormat("Repository.Download<{0}>({1}) is called")]
		protected void RepositoryDownloadIsCalled<TEntity>(EntityRequestType requestType)
			where TEntity : ExpiringObject
		{
			_test.Dependencies.EntityRepository.Verify(r => r.Download<TEntity>(It.Is<EntityRequestType>(rt => rt == requestType),
			                                                                    It.IsAny<Dictionary<string, object>>()), Times.Once());
		}
		[GenericMethodFormat("Repository.Download<{0}>() is not called")]
		protected void RepositoryDownloadIsNotCalled<TEntity>()
			where TEntity : ExpiringObject
		{
			_test.Dependencies.EntityRepository.Verify(r => r.Download<TEntity>(It.IsAny<EntityRequestType>(),
			                                                                    It.IsAny<Dictionary<string, object>>()), Times.Never());
		}
		[GenericMethodFormat("Repository.Refresh<{0}>({1}) is called")]
		protected void RepositoryRefreshIsCalled<TEntity>(EntityRequestType requestType)
			where TEntity : ExpiringObject
		{
			_test.Dependencies.EntityRepository.Verify(r => r.Refresh(It.IsAny<TEntity>(),
			                                                          It.Is<EntityRequestType>(rt => rt == requestType)), Times.Once());
		}
		[GenericMethodFormat("Repository.Refresh<{0}>() is not called")]
		protected void RepositoryRefreshIsNotCalled<TEntity>()
			where TEntity : ExpiringObject
		{
			_test.Dependencies.EntityRepository.Verify(r => r.Refresh(It.IsAny<TEntity>(), It.IsAny<EntityRequestType>()), Times.Never);
		}
		[GenericMethodFormat("Repository.RefreshCollection<{0}>({1}) is called")]
		protected void RepositoryRefreshCollectionIsCalled<TEntity>(EntityRequestType requestType)
			where TEntity : ExpiringObject, IEquatable<TEntity>, IComparable<TEntity>
		{
			_test.Dependencies.EntityRepository.Verify(r => r.RefreshCollection<TEntity>(It.IsAny<ExpiringCollection<TEntity>>(),
			                                                                             It.Is<EntityRequestType>(rt => rt == requestType)));
		}
		[GenericMethodFormat("Repository.RefreshCollection<{0}>() is not called")]
		protected void RepositoryRefreshCollectionIsNotCalled<TEntity>()
			where TEntity : ExpiringObject, IEquatable<TEntity>, IComparable<TEntity>
		{
			_test.Dependencies.EntityRepository.Verify(r => r.RefreshCollection<TEntity>(It.IsAny<ExpiringCollection<TEntity>>(),
			                                                                             It.IsAny<EntityRequestType>()), Times.Never());
		}
		[ParameterizedMethodFormat("Repository.Upload({0}) is called")]
		protected void RepositoryUploadIsCalled(EntityRequestType requestType)
		{
			_test.Dependencies.EntityRepository.Verify(r => r.Upload(It.Is<EntityRequestType>(rt => rt == requestType),
			                                                         It.IsAny<Dictionary<string, object>>()), Times.Once());
		}
		[ParameterizedMethodFormat("Repository.Upload() is not called")]
		protected void RepositoryUploadIsNotCalled()
		{
			_test.Dependencies.EntityRepository.Verify(r => r.Upload(It.IsAny<EntityRequestType>(),
			                                                         It.IsAny<Dictionary<string, object>>()), Times.Never());
		}
		[ParameterizedMethodFormat("Validator.NonEmptyString() is called")]
		protected void ValidatorNonEmptyStringIsCalled()
		{
			_test.Dependencies.Validator.Verify(v => v.NonEmptyString(It.IsAny<string>()));
		}
		[ParameterizedMethodFormat("Validator.NonEmptyString() is not called")]
		protected void ValidatorNonEmptyStringIsNotCalled()
		{
			_test.Dependencies.Validator.Verify(v => v.NonEmptyString(It.IsAny<string>()), Times.Never());
		}
		[ParameterizedMethodFormat("Validator.Position() is called")]
		protected void ValidatorPositionIsCalled()
		{
			_test.Dependencies.Validator.Verify(v => v.Position(It.IsAny<Position>()));
		}
		[ParameterizedMethodFormat("Validator.Position() is not called")]
		protected void ValidatorPositionIsNotCalled()
		{
			_test.Dependencies.Validator.Verify(v => v.Position(It.IsAny<Position>()), Times.Never());
		}
		[ParameterizedMethodFormat("Validator.Url() is called")]
		protected void ValidatorUrlIsCalled()
		{
			_test.Dependencies.Validator.Verify(v => v.Url(It.IsAny<string>()));
		}
		[ParameterizedMethodFormat("Validator.Url() is not called")]
		protected void ValidatorUrlIsNotCalled()
		{
			_test.Dependencies.Validator.Verify(v => v.Url(It.IsAny<string>()), Times.Never());
		}
		[ParameterizedMethodFormat("Validator.Writable() is called")]
		protected void ValidatorWritableIsCalled()
		{
			_test.Dependencies.Validator.Verify(v => v.Writable());
		}
		[ParameterizedMethodFormat("Validator.Writable() is not called")]
		protected void ValidatorWritableIsNotCalled()
		{
			_test.Dependencies.Validator.Verify(v => v.Writable(), Times.Never());
		}
		[GenericMethodFormat("Validator.Enumeration<{0}>() is called")]
		protected void ValidatorEnumerationIsCalled<TEnum>()
		{
			_test.Dependencies.Validator.Verify(v => v.Enumeration(It.IsAny<TEnum>()));
		}
		[ParameterizedMethodFormat("Validator.Writable() is not called")]
		protected void ValidatorEnumerationIsNotCalled<TEnum>()
		{
			_test.Dependencies.Validator.Verify(v => v.Enumeration(It.IsAny<TEnum>()), Times.Never());
		}
		[ParameterizedMethodFormat("Validator.MinStringLength({0}) is called")]
		protected void ValidatorMinStringLengthIsCalled(int length)
		{
			_test.Dependencies.Validator.Verify(v => v.MinStringLength(It.IsAny<string>(), It.Is<int>(i => i == length),
			                                                           It.IsAny<string>()));
		}
		[ParameterizedMethodFormat("Validator.MinStringLength() is not called")]
		protected void ValidatorMinStringLengthIsNotCalled()
		{
			_test.Dependencies.Validator.Verify(v => v.MinStringLength(It.IsAny<string>(), It.IsAny<int>(),
			                                                           It.IsAny<string>()), Times.Never());
		}
		[ParameterizedMethodFormat("Validator.StringLengthRange({0}, {1}) is called")]
		protected void ValidatorStringLengthRangeIsCalled(int min, int max)
		{
			_test.Dependencies.Validator.Verify(v => v.StringLengthRange(It.IsAny<string>(), It.Is<int>(i => i == min),
			                                                             It.Is<int>(i => i == max), It.IsAny<string>()));
		}
		[ParameterizedMethodFormat("Validator.StringLengthRange() is not called")]
		protected void ValidatorStringLengthRangeIsNotCalled()
		{
			_test.Dependencies.Validator.Verify(v => v.StringLengthRange(It.IsAny<string>(), It.IsAny<int>(),
			                                                             It.IsAny<int>(), It.IsAny<string>()), Times.Never());
		}
		[ParameterizedMethodFormat("Validator.UserName() is called")]
		protected void ValidatorUserNameIsCalled()
		{
			_test.Dependencies.Validator.Verify(v => v.UserName(It.IsAny<string>()));
		}
		[ParameterizedMethodFormat("Validator.UserName() is not called")]
		protected void ValidatorUserNameIsNotCalled()
		{
			_test.Dependencies.Validator.Verify(v => v.UserName(It.IsAny<string>()), Times.Never());
		}
		[ParameterizedMethodFormat("Validator.OrgName() is called")]
		protected void ValidatorOrgNameIsCalled()
		{
			_test.Dependencies.Validator.Verify(v => v.OrgName(It.IsAny<string>()));
		}
		[ParameterizedMethodFormat("Validator.OrgName() is not called")]
		protected void ValidatorOrgNameIsNotCalled()
		{
			_test.Dependencies.Validator.Verify(v => v.OrgName(It.IsAny<string>()), Times.Never());
		}
		[ParameterizedMethodFormat("Validator.Entity() is called")]
		protected void ValidatorEntityIsCalled<TEntity>()
			where TEntity : ExpiringObject
		{
			_test.Dependencies.Validator.Verify(v => v.Entity(It.IsAny<TEntity>(), It.IsAny<bool>()));
		}
		[ParameterizedMethodFormat("Validator.Entity() is not called")]
		protected void ValidatorEntityIsNotCalled<TEntity>()
			where TEntity : ExpiringObject
		{
			_test.Dependencies.Validator.Verify(v => v.Entity(It.IsAny<TEntity>(), It.IsAny<bool>()), Times.Never());
		}
		[GenericMethodFormat("Validator.Nullable<{0}>() is called")]
		protected void ValidatorNullableIsCalled<TValue>()
			where TValue : struct
		{
			_test.Dependencies.Validator.Verify(v => v.Nullable<TValue>(It.IsAny<TValue>()));
		}
		[GenericMethodFormat("Validator.Nullable<{0}>() is not called")]
		protected void ValidatorNullableIsNotCalled<TValue>()
			where TValue : struct
		{
			_test.Dependencies.Validator.Verify(v => v.Nullable<TValue>(It.IsAny<TValue>()), Times.Never());
		}
		[GenericMethodFormat("'{0}' is returned")]
		protected void ValueIsReturned<TResult>(TResult expectedValue)
		{
			Assert.IsInstanceOfType(ActualResult, typeof (TResult));
			Assert.AreEqual(expectedValue, (TResult) ActualResult);
		}
		[GenericMethodFormat("Non-null value of type '{0}' is returned")]
		protected void NonNullValueOfTypeIsReturned<TResult>()
		{
			Assert.IsNotNull(ActualResult);
			Assert.IsInstanceOfType(ActualResult, typeof (TResult));
		}
		protected void PropertyIsSet<TProp>(TProp expectedValue, TProp propValue)
		{
			Assert.AreEqual(expectedValue, propValue);
		}

		#endregion

		#region Other Methods

		protected void ReapplyJson()
		{
			_test.Sut.ApplyJson(_test.Json.Object);
		}
		protected void SetupRepositoryDownload<TEntity>()
			where TEntity : ExpiringObject, new()
		{
			_test.Dependencies.EntityRepository.Setup(r => r.Download<TEntity>(It.IsAny<EntityRequestType>(),
			                                                                   It.IsAny<IDictionary<string, object>>()))
			     .Returns(new TEntity());
		}
		protected TObj Create<TObj>()
			where TObj : ExpiringObject, new()
		{
			return new TObj
				{
					EntityRepository = _test.Dependencies.EntityRepository.Object,
					Validator = _test.Dependencies.Validator.Object,
					Id = TrelloIds.Test
				};
		}

		#endregion
	}
}

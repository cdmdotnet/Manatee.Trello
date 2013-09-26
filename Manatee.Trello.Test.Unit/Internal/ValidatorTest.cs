using System;
using System.Collections.Generic;
using System.Linq;
using Manatee.Trello.Contracts;
using Manatee.Trello.Exceptions;
using Manatee.Trello.Internal;
using Manatee.Trello.Internal.DataAccess;
using Manatee.Trello.Json;
using Manatee.Trello.Test.Unit.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Manatee.Trello.Test.Unit.Internal
{
	[TestClass]
	public class ValidatorTest : TrelloTestBase<IValidator>
	{
		#region Dependencies

		private class DependencyCollection
		{
			public Mock<ILog> Log { get; private set; }
			public Mock<ITrelloService> TrelloService { get; private set; }

			public DependencyCollection()
			{
				Log = new Mock<ILog>();
				TrelloService = new Mock<ITrelloService>();

				Log.Setup(l => l.Error(It.IsAny<Exception>(), It.Is<bool>(b => b)))
						.Callback((Exception e, bool b) => { throw e; });
			}
		}

		private class ValidatorUnderTest : SystemUnderTest<DependencyCollection>
		{
			public ValidatorUnderTest()
			{
				Sut = new Validator(Dependencies.Log.Object,
									Dependencies.TrelloService.Object);
			}
		}

		private ValidatorUnderTest _test;

		#endregion

		#region Scenarios

		[TestMethod]
		public void Writable()
		{
			var feature = CreateFeature();

			feature.WithScenario("Writable is called with null UserToken")
				   .Given(AValidator)
				   .And(UserTokenIs, (string) null)
				   .When(WritableIsCalled)
				   .Then(TrelloServiceUserTokenIsAccessed)
				   .And(ExceptionIsThrown<ReadOnlyAccessException>)

				   .WithScenario("Writable is called with empty UserToken")
				   .Given(AValidator)
				   .And(UserTokenIs, string.Empty)
				   .When(WritableIsCalled)
				   .Then(TrelloServiceUserTokenIsAccessed)
				   .And(ExceptionIsThrown<ReadOnlyAccessException>)

				   .WithScenario("Writable is called with whitespace UserToken")
				   .Given(AValidator)
				   .And(UserTokenIs, "      ")
				   .When(WritableIsCalled)
				   .Then(TrelloServiceUserTokenIsAccessed)
				   .And(ExceptionIsThrown<ReadOnlyAccessException>)

				   .WithScenario("Writable is called with whitespace UserToken")
				   .Given(AValidator)
				   .And(UserTokenIs, "some non-null string")
				   .When(WritableIsCalled)
				   .Then(TrelloServiceUserTokenIsAccessed)
				   .And(ExceptionIsNotThrown)

				   .Execute();
		}
		[TestMethod]
		public void Entity()
		{
			var feature = CreateFeature();

			feature.WithScenario("Entity is null and not allowed")
				   .Given(AValidator)
				   .When(EntityIsCalled, (MockEntity) null, false)
				   .Then(LogErrorIsCalled<ArgumentNullException>)
				   .And(ExceptionIsThrown<ArgumentNullException>)

				   .WithScenario("Entity is null and allowed")
				   .Given(AValidator)
				   .When(EntityIsCalled, (MockEntity) null, true)
				   .Then(LogErrorIsNotCalled)
				   .And(ExceptionIsNotThrown)

				   .WithScenario("Entity has null ID")
				   .Given(AValidator)
				   .When(EntityIsCalled, new MockEntity {Id = null}, false)
				   .Then(LogErrorIsCalled<EntityNotOnTrelloException<MockEntity>>)
				   .And(ExceptionIsThrown<EntityNotOnTrelloException<MockEntity>>)

				   .WithScenario("Entity has empty ID")
				   .Given(AValidator)
				   .When(EntityIsCalled, new MockEntity {Id = string.Empty}, false)
				   .Then(LogErrorIsCalled<EntityNotOnTrelloException<MockEntity>>)
				   .And(ExceptionIsThrown<EntityNotOnTrelloException<MockEntity>>)

				   .WithScenario("Entity has whitespace ID")
				   .Given(AValidator)
				   .When(EntityIsCalled, new MockEntity {Id = "     "}, false)
				   .Then(LogErrorIsCalled<EntityNotOnTrelloException<MockEntity>>)
				   .And(ExceptionIsThrown<EntityNotOnTrelloException<MockEntity>>)

				   .WithScenario("Entity has valid ID")
				   .Given(AValidator)
				   .When(EntityIsCalled, new MockEntity {Id = TrelloIds.Test}, false)
				   .Then(LogErrorIsNotCalled)
				   .And(ExceptionIsNotThrown)

				   .Execute();
		}
		[TestMethod]
		public void Nullable()
		{
			var feature = CreateFeature();

			feature.WithScenario("Value is not null")
				   .Given(AValidator)
				   .When(NullableIsCalled, (bool?) false)
				   .Then(LogErrorIsNotCalled)
				   .And(ExceptionIsNotThrown)

				   .WithScenario("Value is null")
				   .Given(AValidator)
				   .When(NullableIsCalled, (bool?) null)
				   .Then(LogErrorIsCalled<ArgumentNullException>)
				   .And(ExceptionIsThrown<ArgumentNullException>)

				   .Execute();
		}
		[TestMethod]
		public void NonEmptyString()
		{
			var feature = CreateFeature();

			feature.WithScenario("Value is null")
				   .Given(AValidator)
				   .When(NonEmptyStringIsCalled, (string) null)
				   .Then(LogErrorIsCalled<ArgumentNullException>)
				   .And(ExceptionIsThrown<ArgumentNullException>)

				   .WithScenario("Value is empty")
				   .Given(AValidator)
				   .When(NonEmptyStringIsCalled, string.Empty)
				   .Then(LogErrorIsCalled<ArgumentNullException>)
				   .And(ExceptionIsThrown<ArgumentNullException>)

				   .WithScenario("Value is whitespace")
				   .Given(AValidator)
				   .When(NonEmptyStringIsCalled, "     ")
				   .Then(LogErrorIsCalled<ArgumentNullException>)
				   .And(ExceptionIsThrown<ArgumentNullException>)

				   .WithScenario("Value is valid")
				   .Given(AValidator)
				   .When(NonEmptyStringIsCalled, TrelloIds.Test)
				   .Then(LogErrorIsNotCalled)
				   .And(ExceptionIsNotThrown)

				   .Execute();
		}
		[TestMethod]
		public void Position()
		{
			var feature = CreateFeature();

			feature.WithScenario("Value is null")
				   .Given(AValidator)
				   .When(PositionIsCalled, (Position) null)
				   .Then(LogErrorIsCalled<ArgumentNullException>)
				   .And(ExceptionIsThrown<ArgumentNullException>)

				   .WithScenario("Value is valid")
				   .Given(AValidator)
				   .When(PositionIsCalled, Trello.Position.Bottom)
				   .Then(LogErrorIsNotCalled)
				   .And(ExceptionIsNotThrown)

				   .Execute();
		}
		[TestMethod]
		public void MinStringLength()
		{
			var feature = CreateFeature();

			feature.WithScenario("Value is null")
				   .Given(AValidator)
				   .When(MinStringLengthIsCalled, (string)null, 5)
				   .Then(LogErrorIsCalled<ArgumentNullException>)
				   .And(ExceptionIsThrown<ArgumentNullException>)

				   .WithScenario("Value is empty")
				   .Given(AValidator)
				   .When(MinStringLengthIsCalled, string.Empty, 5)
				   .Then(LogErrorIsCalled<ArgumentNullException>)
				   .And(ExceptionIsThrown<ArgumentNullException>)

				   .WithScenario("Value is long enough but is whitespace")
				   .Given(AValidator)
				   .When(MinStringLengthIsCalled, "         ", 5)
				   .Then(LogErrorIsCalled<ArgumentNullException>)
				   .And(ExceptionIsThrown<ArgumentNullException>)

				   .WithScenario("Value is too short")
				   .Given(AValidator)
				   .When(MinStringLengthIsCalled, "123", 5)
				   .Then(LogErrorIsCalled<ArgumentException>)
				   .And(ExceptionIsThrown<ArgumentException>)

				   .WithScenario("Value is valid")
				   .Given(AValidator)
				   .When(MinStringLengthIsCalled, "123456", 5)
				   .Then(LogErrorIsNotCalled)
				   .And(ExceptionIsNotThrown)

				   .Execute();
		}
		[TestMethod]
		public void StringLengthRange()
		{
			var feature = CreateFeature();

			feature.WithScenario("Value is null")
				   .Given(AValidator)
				   .When(StringLengthRangeIsCalled, (string)null, 5, 10)
				   .Then(LogErrorIsCalled<ArgumentNullException>)
				   .And(ExceptionIsThrown<ArgumentNullException>)

				   .WithScenario("Value is empty")
				   .Given(AValidator)
				   .When(StringLengthRangeIsCalled, string.Empty, 5, 10)
				   .Then(LogErrorIsCalled<ArgumentNullException>)
				   .And(ExceptionIsThrown<ArgumentNullException>)

				   .WithScenario("Value is within range but is whitespace")
				   .Given(AValidator)
				   .When(StringLengthRangeIsCalled, "         ", 5, 10)
				   .Then(LogErrorIsCalled<ArgumentNullException>)
				   .And(ExceptionIsThrown<ArgumentNullException>)

				   .WithScenario("Value is too short")
				   .Given(AValidator)
				   .When(StringLengthRangeIsCalled, "123", 5, 10)
				   .Then(LogErrorIsCalled<ArgumentException>)
				   .And(ExceptionIsThrown<ArgumentException>)

				   .WithScenario("Value is too long")
				   .Given(AValidator)
				   .When(StringLengthRangeIsCalled, "123456789012", 5, 10)
				   .Then(LogErrorIsCalled<ArgumentException>)
				   .And(ExceptionIsThrown<ArgumentException>)

				   .WithScenario("Value is valid")
				   .Given(AValidator)
				   .When(StringLengthRangeIsCalled, "123456", 5, 10)
				   .Then(LogErrorIsNotCalled)
				   .And(ExceptionIsNotThrown)

				   .Execute();
		}
		[TestMethod]
		public void UserName()
		{
			var feature = CreateFeature();

			feature.WithScenario("UserName is null")
				   .Given(AValidator)
				   .When(UserNameIsCalled, (string)null)
				   .Then(TrelloServiceSearchMembersIsNotCalled)
				   .And(LogErrorIsCalled<ArgumentNullException>)
				   .And(ExceptionIsThrown<ArgumentNullException>)

				   .WithScenario("UserName is empty")
				   .Given(AValidator)
				   .When(UserNameIsCalled, string.Empty)
				   .Then(TrelloServiceSearchMembersIsNotCalled)
				   .And(LogErrorIsCalled<ArgumentNullException>)
				   .And(ExceptionIsThrown<ArgumentNullException>)

				   .WithScenario("UserName is long enough but is whitespace")
				   .Given(AValidator)
				   .When(UserNameIsCalled, "       ")
				   .Then(TrelloServiceSearchMembersIsNotCalled)
				   .And(LogErrorIsCalled<ArgumentNullException>)
				   .And(ExceptionIsThrown<ArgumentNullException>)

				   .WithScenario("UserName is too short")
				   .Given(AValidator)
				   .When(UserNameIsCalled, "12")
				   .Then(TrelloServiceSearchMembersIsNotCalled)
				   .And(LogErrorIsCalled<ArgumentException>)
				   .And(ExceptionIsThrown<ArgumentException>)

				   .WithScenario("UserName contains invalid character")
				   .Given(AValidator)
				   .When(UserNameIsCalled, "wvoin_9229wv.cuioR")
				   .Then(TrelloServiceSearchMembersIsNotCalled)
				   .And(LogErrorIsCalled<ArgumentException>)
				   .And(ExceptionIsThrown<ArgumentException>)

				   .WithScenario("UserName is valid and exists")
				   .Given(AValidator)
				   .And(UserNameExists)
				   .When(UserNameIsCalled, "123456")
				   .Then(TrelloServiceSearchMembersIsCalled)
				   .And(LogErrorIsCalled<ArgumentException>)
				   .And(ExceptionIsThrown<ArgumentException>)

				   .WithScenario("UserName is valid and does not exist")
				   .Given(AValidator)
				   .And(UserNameDoesNotExist)
				   .When(UserNameIsCalled, "123456")
				   .Then(TrelloServiceSearchMembersIsCalled)
				   .And(LogErrorIsNotCalled)
				   .And(ExceptionIsNotThrown)

				   .Execute();
		}
		[TestMethod]
		public void OrgName()
		{
			var feature = CreateFeature();

			feature.WithScenario("OrgName is null")
				   .Given(AValidator)
				   .When(OrgNameIsCalled, (string)null)
				   .Then(TrelloServiceSearchIsNotCalled)
				   .And(LogErrorIsCalled<ArgumentNullException>)
				   .And(ExceptionIsThrown<ArgumentNullException>)

				   .WithScenario("OrgName is empty")
				   .Given(AValidator)
				   .When(OrgNameIsCalled, string.Empty)
				   .Then(TrelloServiceSearchIsNotCalled)
				   .And(LogErrorIsCalled<ArgumentNullException>)
				   .And(ExceptionIsThrown<ArgumentNullException>)

				   .WithScenario("OrgName is long enough but is whitespace")
				   .Given(AValidator)
				   .When(OrgNameIsCalled, "       ")
				   .Then(TrelloServiceSearchIsNotCalled)
				   .And(LogErrorIsCalled<ArgumentNullException>)
				   .And(ExceptionIsThrown<ArgumentNullException>)

				   .WithScenario("OrgName is too short")
				   .Given(AValidator)
				   .When(OrgNameIsCalled, "12")
				   .Then(TrelloServiceSearchIsNotCalled)
				   .And(LogErrorIsCalled<ArgumentException>)
				   .And(ExceptionIsThrown<ArgumentException>)

				   .WithScenario("OrgName contains invalid character")
				   .Given(AValidator)
				   .When(OrgNameIsCalled, "wvoin_9229wv.cuioR")
				   .Then(TrelloServiceSearchIsNotCalled)
				   .And(LogErrorIsCalled<ArgumentException>)
				   .And(ExceptionIsThrown<ArgumentException>)

				   .WithScenario("OrgName is valid and exists")
				   .Given(AValidator)
				   .And(OrgNameExists)
				   .When(OrgNameIsCalled, "123456")
				   .Then(TrelloServiceSearchIsCalled)
				   .And(LogErrorIsCalled<ArgumentException>)
				   .And(ExceptionIsThrown<ArgumentException>)

				   .WithScenario("OrgName is valid and does not exist")
				   .Given(AValidator)
				   .And(OrgNameDoesNotExist)
				   .When(OrgNameIsCalled, "123456")
				   .Then(TrelloServiceSearchIsCalled)
				   .And(LogErrorIsNotCalled)
				   .And(ExceptionIsNotThrown)

				   .Execute();
		}
		[TestMethod]
		public void Enumeration()
		{
			var feature = CreateFeature();

			feature.WithScenario("Value is defined")
				.Given(AValidator)
				.When(EnumerationIsCalled, ActionType.DeleteCard)
				.Then(LogErrorIsNotCalled)
				.And(ExceptionIsNotThrown)

				.WithScenario("Value is not defined")
				.Given(AValidator)
				.When(EnumerationIsCalled, (ActionType) (-1))
				.Then(LogErrorIsCalled<ArgumentException>)
				.And(ExceptionIsThrown<ArgumentException>)

				.Execute();
		}
		[TestMethod]
		public void Url()
		{
			var feature = CreateFeature();

			feature.WithScenario("Value is null")
				   .Given(AValidator)
				   .When(UrlIsCalled, (string) null)
				   .Then(LogErrorIsNotCalled)
				   .And(ExceptionIsNotThrown)

				   .WithScenario("Value is empty")
				   .Given(AValidator)
				   .When(UrlIsCalled, string.Empty)
				   .Then(LogErrorIsNotCalled)
				   .And(ExceptionIsNotThrown)

				   .WithScenario("Value is whitespace")
				   .Given(AValidator)
				   .When(UrlIsCalled, "     ")
				   .Then(LogErrorIsNotCalled)
				   .And(ExceptionIsNotThrown)

				   .WithScenario("Value is not http(s):// URL")
				   .Given(AValidator)
				   .When(UrlIsCalled, "ftp://trello.com/")
				   .Then(LogErrorIsCalled<ArgumentException>)
				   .And(ExceptionIsThrown<ArgumentException>)

				   .WithScenario("Value is http:// URL")
				   .Given(AValidator)
				   .When(UrlIsCalled, "http://trello.com/")
				   .Then(LogErrorIsNotCalled)
				   .And(ExceptionIsNotThrown)

				   .WithScenario("Value is https:// URL")
				   .Given(AValidator)
				   .When(UrlIsCalled, "https://trello.com/")
				   .Then(LogErrorIsNotCalled)
				   .And(ExceptionIsNotThrown)

				   .Execute();
		}
		[TestMethod]
		public void ArgumentNotNull()
		{
			var feature = CreateFeature();

			feature.WithScenario("Value is null")
				   .Given(AValidator)
				   .When(ArgumentNotNullIsCalled, (object) null)
				   .Then(LogErrorIsCalled<ArgumentNullException>)
				   .And(ExceptionIsThrown<ArgumentNullException>)

				   .WithScenario("Value is not null")
				   .Given(AValidator)
				   .When(ArgumentNotNullIsCalled, new object())
				   .Then(LogErrorIsNotCalled)
				   .And(ExceptionIsNotThrown)

				   .Execute();
		}

		#endregion

		#region Given

		private void AValidator()
		{
			_test = new ValidatorUnderTest();
		}
		private void UserTokenIs(string value)
		{
			_test.Dependencies.TrelloService.SetupGet(s => s.UserToken)
				 .Returns(value);
		}
		private void UserNameExists()
		{
			_test.Dependencies.TrelloService.Setup(s => s.SearchMembers(It.IsAny<string>(), It.IsAny<int>()))
				 .Returns(new List<Member> {new Member {Id = TrelloIds.Test}});
		}
		private void UserNameDoesNotExist()
		{
			_test.Dependencies.TrelloService.Setup(s => s.SearchMembers(It.IsAny<string>(), It.IsAny<int>()))
				 .Returns(Enumerable.Empty<Member>());
		}
		private void OrgNameExists()
		{
			var results = new SearchResults {EntityRepository = new Mock<IEntityRepository>().Object};
			var repository = new Mock<IEntityRepository>();
			var json = new Mock<IJsonSearchResults>();
			repository.Setup(r => r.Download<Organization>(It.IsAny<EntityRequestType>(),
														   It.IsAny<IDictionary<string, object>>()))
					  .Returns(new Organization {Id = TrelloIds.Test});
			json.SetupGet(j => j.OrganizationIds)
				.Returns(new List<string> {TrelloIds.Test});
			results.ApplyJson(json.Object);
			_test.Dependencies.TrelloService.Setup(s => s.Search(It.IsAny<string>(), It.IsAny<List<ExpiringObject>>(),
																 It.IsAny<SearchModelType>()))
				 .Returns(results);
		}
		private void OrgNameDoesNotExist()
		{
			_test.Dependencies.TrelloService.Setup(s => s.Search(It.IsAny<string>(), It.IsAny<List<ExpiringObject>>(),
																 It.IsAny<SearchModelType>()))
				 .Returns(new SearchResults());
		}

		#endregion

		#region When

		private void WritableIsCalled()
		{
			Execute(() => _test.Sut.Writable());
		}
		private void EntityIsCalled<T>(T entity, bool allowNulls)
			where T : ExpiringObject
		{
			Execute(() => _test.Sut.Entity(entity, allowNulls));
		}
		private void NullableIsCalled<T>(T? value)
			where T : struct
		{
			Execute(() => _test.Sut.Nullable(value));
		}
		private void NonEmptyStringIsCalled(string value)
		{
			Execute(() => _test.Sut.NonEmptyString(value));
		}
		private void PositionIsCalled(Position value)
		{
			Execute(() => _test.Sut.Position(value));
		}
		private void MinStringLengthIsCalled(string str, int min)
		{
			Execute(() => _test.Sut.MinStringLength(str, min, "str"));
		}
		private void StringLengthRangeIsCalled(string str, int min, int max)
		{
			Execute(() => _test.Sut.StringLengthRange(str, min, max, "str"));
		}
		private void UserNameIsCalled(string value)
		{
			Execute(() => _test.Sut.UserName(value));
		}
		private void OrgNameIsCalled(string value)
		{
			Execute(() => _test.Sut.OrgName(value));
		}
		private void EnumerationIsCalled<T>(T value)
		{
			Execute(() => _test.Sut.Enumeration(value));
		}
		private void UrlIsCalled(string value)
		{
			Execute(() => _test.Sut.Url(value));
		}
		private void ArgumentNotNullIsCalled(object value)
		{
			Execute(() => _test.Sut.ArgumentNotNull(value));
		}

		#endregion

		#region Then

		private void LogErrorIsCalled<TEx>()
			where TEx : Exception
		{
			_test.Dependencies.Log.Verify(l => l.Error(It.IsAny<TEx>(), It.IsAny<bool>()));
		}
		private void LogErrorIsNotCalled()
		{
			_test.Dependencies.Log.Verify(l => l.Error(It.IsAny<Exception>(), It.IsAny<bool>()), Times.Never());
		}
		private void TrelloServiceSearchIsCalled()
		{
			_test.Dependencies.TrelloService.Verify(s => s.Search(It.IsAny<string>(), It.IsAny<List<ExpiringObject>>(),
																  It.IsAny<SearchModelType>()));
		}
		private void TrelloServiceSearchIsNotCalled()
		{
			_test.Dependencies.TrelloService.Verify(s => s.Search(It.IsAny<string>(), It.IsAny<List<ExpiringObject>>(),
																  It.IsAny<SearchModelType>()), Times.Never());
		}
		private void TrelloServiceSearchMembersIsCalled()
		{
			_test.Dependencies.TrelloService.Verify(s => s.SearchMembers(It.IsAny<string>(), It.IsAny<int>()));
		}
		private void TrelloServiceSearchMembersIsNotCalled()
		{
			_test.Dependencies.TrelloService.Verify(s => s.SearchMembers(It.IsAny<string>(), It.IsAny<int>()), Times.Never());
		}
		private void TrelloServiceUserTokenIsAccessed()
		{
			_test.Dependencies.TrelloService.Verify(s => s.UserToken);
		}

		#endregion
	}
}

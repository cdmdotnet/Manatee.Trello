using System.Collections.Generic;
using Manatee.Trello.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Manatee.Trello.Test.Unit.Entities
{
	[TestClass]
	public class TokenTest : EntityTestBase<Token, IJsonToken>
	{
		[TestMethod]
		public void BoardPermissions()
		{
			var feature = CreateFeature();

			feature.WithScenario("Access BoardPermissions property when not expired")
				.Given(AToken)
				.When(BoardPermissionsIsAccessed)
				.Then(RepositoryRefreshIsNotCalled<Token>)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access BoardPermissions property when expired")
				.Given(AToken)
				.And(EntityIsExpired)
				.When(BoardPermissionsIsAccessed)
				.Then(RepositoryRefreshIsCalled<Token>, EntityRequestType.Token_Read_Refresh)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void DateCreated()
		{
			var feature = CreateFeature();

			feature.WithScenario("Access DateCreated property when not expired")
				.Given(AToken)
				.When(DateCreatedIsAccessed)
				.Then(RepositoryRefreshIsNotCalled<Token>)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access DateCreated property when expired")
				.Given(AToken)
				.And(EntityIsExpired)
				.When(DateCreatedIsAccessed)
				.Then(RepositoryRefreshIsCalled<Token>, EntityRequestType.Token_Read_Refresh)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void DateExpires()
		{
			var feature = CreateFeature();

			feature.WithScenario("Access DateExpires property when not expired")
				.Given(AToken)
				.When(DateExpiresIsAccessed)
				.Then(RepositoryRefreshIsNotCalled<Token>)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access DateExpires property when expired")
				.Given(AToken)
				.And(EntityIsExpired)
				.When(DateExpiresIsAccessed)
				.Then(RepositoryRefreshIsCalled<Token>, EntityRequestType.Token_Read_Refresh)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void Identifier()
		{
			var feature = CreateFeature();

			feature.WithScenario("Access Identifier property when not expired")
				.Given(AToken)
				.When(IdentifierIsAccessed)
				.Then(RepositoryRefreshIsNotCalled<Token>)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access Identifier property when expired")
				.Given(AToken)
				.And(EntityIsExpired)
				.When(IdentifierIsAccessed)
				.Then(RepositoryRefreshIsNotCalled<Token>)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void Member()
		{
			var feature = CreateFeature();

			feature.WithScenario("Access Member property when not expired")
				.Given(AToken)
				.When(MemberIsAccessed)
				.Then(RepositoryRefreshIsNotCalled<Token>)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access Member property when expired")
				.Given(AToken)
				.And(EntityIsExpired)
				.When(MemberIsAccessed)
				.Then(RepositoryRefreshIsCalled<Token>, EntityRequestType.Token_Read_Refresh)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void MemberPermissions()
		{
			var feature = CreateFeature();

			feature.WithScenario("Access MemberPermissions property when not expired")
				.Given(AToken)
				.When(MemberPermissionsIsAccessed)
				.Then(RepositoryRefreshIsNotCalled<Token>)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access MemberPermissions property when expired")
				.Given(AToken)
				.And(EntityIsExpired)
				.When(MemberPermissionsIsAccessed)
				.Then(RepositoryRefreshIsCalled<Token>, EntityRequestType.Token_Read_Refresh)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void OrganizationPermissions()
		{
			var feature = CreateFeature();

			feature.WithScenario("Access OrganizationPermissions property when not expired")
				.Given(AToken)
				.When(OrganizationPermissionsIsAccessed)
				.Then(RepositoryRefreshIsNotCalled<Token>)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access OrganizationPermissions property when expired")
				.Given(AToken)
				.And(EntityIsExpired)
				.When(OrganizationPermissionsIsAccessed)
				.Then(RepositoryRefreshIsCalled<Token>, EntityRequestType.Token_Read_Refresh)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void Delete()
		{
			var feature = CreateFeature();

			feature.WithScenario("Delete is called")
				.Given(AToken)
				.When(DeleteIsCalled)
				.Then(ValidatorWritableIsCalled)
				.And(RepositoryUploadIsCalled, EntityRequestType.Token_Write_Delete)
				.And(ExceptionIsNotThrown)

				.WithScenario("Delete is called when already deleted")
				.Given(AToken)
				.And(AlreadyDeleted)
				.When(DeleteIsCalled)
				.Then(ValidatorWritableIsNotCalled)
				.And(RepositoryUploadIsNotCalled)
				.And(ExceptionIsNotThrown)

				.Execute();
		}

		#region Given

		private void AToken()
		{
			var boardPerm = new Mock<IJsonTokenPermission>();
			boardPerm.SetupGet(p => p.ModelType)
				.Returns("Board");
			var memberPerm = new Mock<IJsonTokenPermission>();
			memberPerm.SetupGet(p => p.ModelType)
				.Returns("Member");
			var orgPerm = new Mock<IJsonTokenPermission>();
			orgPerm.SetupGet(p => p.ModelType)
				.Returns("Organization");
			_test = new EntityUnderTest();
			_test.Json.SetupGet(j => j.Permissions)
				 .Returns(new List<IJsonTokenPermission> {boardPerm.Object, memberPerm.Object, orgPerm.Object});
			_test.Json.SetupGet(j => j.IdMember)
				 .Returns(TrelloIds.MemberId);
			_test.Json.SetupGet(j => j.Identifier)
				 .Returns(TrelloIds.Test);
			ReapplyJson();
		}
		private void AlreadyDeleted()
		{
			_test.Sut.ForceDeleted(true);
		}

		#endregion

		#region When

		private void BoardPermissionsIsAccessed()
		{
			Execute(() => _test.Sut.BoardPermissions);
		}
		private void DateCreatedIsAccessed()
		{
			Execute(() => _test.Sut.DateCreated);
		}
		private void DateExpiresIsAccessed()
		{
			Execute(() => _test.Sut.DateExpires);
		}
		private void IdentifierIsAccessed()
		{
			Execute(() => _test.Sut.Identifier);
		}
		private void MemberIsAccessed()
		{
			Execute(() => _test.Sut.Member);
		}
		private void MemberPermissionsIsAccessed()
		{
			Execute(() => _test.Sut.MemberPermissions);
		}
		private void OrganizationPermissionsIsAccessed()
		{
			Execute(() => _test.Sut.OrganizationPermissions);
		}
		private void DeleteIsCalled()
		{
			Execute(() => _test.Sut.Delete());
		}

		#endregion
	}
}

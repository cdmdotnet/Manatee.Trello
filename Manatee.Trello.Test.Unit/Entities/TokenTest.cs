using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Manatee.Trello.Exceptions;
using Manatee.Trello.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using StoryQ;

namespace Manatee.Trello.Test.Unit
{
	[TestClass]
	public class TokenTest : EntityTestBase<Token>
	{
		[TestMethod]
		public void BoardPermissions()
		{
			var story = new Story("BoardPermissions");

			var feature = story.InOrderTo("get the permissions over boards allowed by a token")
				.AsA("developer")
				.IWant("to get the BoardPermissions");

			feature.WithScenario("Access BoardPermissions property")
				.Given(AToken)
				.And(EntityIsRefreshed)
				.When(BoardPermissionsIsAccessed)
				.Then(MockApiGetIsCalled<IJsonToken>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access BoardPermissions property when expired")
				.Given(AToken)
				.And(EntityIsExpired)
				.When(BoardPermissionsIsAccessed)
				.Then(MockApiGetIsCalled<IJsonToken>, 1)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void DateCreated()
		{
			var story = new Story("DateCreated");

			var feature = story.InOrderTo("get the date when a token was created")
				.AsA("developer")
				.IWant("to get the DateCreated");

			feature.WithScenario("Access DateCreated property")
				.Given(AToken)
				.And(EntityIsRefreshed)
				.When(DateCreatedIsAccessed)
				.Then(MockApiGetIsCalled<IJsonToken>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access DateCreated property when expired")
				.Given(AToken)
				.And(EntityIsExpired)
				.When(DateCreatedIsAccessed)
				.Then(MockApiGetIsCalled<IJsonToken>, 1)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void DateExpires()
		{
			var story = new Story("DateExpires");

			var feature = story.InOrderTo("get the date when a token expires")
				.AsA("developer")
				.IWant("to get the DateExpires");

			feature.WithScenario("Access DateExpires property")
				.Given(AToken)
				.And(EntityIsRefreshed)
				.When(DateExpiresIsAccessed)
				.Then(MockApiGetIsCalled<IJsonToken>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access DateExpires property when expired")
				.Given(AToken)
				.And(EntityIsExpired)
				.When(DateExpiresIsAccessed)
				.Then(MockApiGetIsCalled<IJsonToken>, 1)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void Identifier()
		{
			var story = new Story("Identifier");

			var feature = story.InOrderTo("get the application which requested a token")
				.AsA("developer")
				.IWant("to get the Identifier");

			feature.WithScenario("Access Identifier property")
				.Given(AToken)
				.And(EntityIsRefreshed)
				.When(IdentifierIsAccessed)
				.Then(MockApiGetIsCalled<IJsonToken>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access Identifier property when expired")
				.Given(AToken)
				.And(EntityIsExpired)
				.When(IdentifierIsAccessed)
				.Then(MockApiGetIsCalled<IJsonToken>, 1)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void Member()
		{
			var story = new Story("Member");

			var feature = story.InOrderTo("get the member who issued a token")
				.AsA("developer")
				.IWant("to get the Member");

			feature.WithScenario("Access Member property")
				.Given(AToken)
				.And(EntityIsRefreshed)
				.When(MemberIsAccessed)
				.Then(MockApiGetIsCalled<IJsonToken>, 1)
				.And(MockSvcRetrieveIsCalled<Member>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access Member property when expired")
				.Given(AToken)
				.And(EntityIsExpired)
				.When(MemberIsAccessed)
				.Then(MockApiGetIsCalled<IJsonToken>, 1)
				.And(MockSvcRetrieveIsCalled<Member>, 1)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void MemberPermissions()
		{
			var story = new Story("MemberPermissions");

			var feature = story.InOrderTo("get the permissions over boards allowed by a token")
				.AsA("developer")
				.IWant("to get the MemberPermissions");

			feature.WithScenario("Access MemberPermissions property")
				.Given(AToken)
				.And(EntityIsRefreshed)
				.When(MemberPermissionsIsAccessed)
				.Then(MockApiGetIsCalled<IJsonToken>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access MemberPermissions property when expired")
				.Given(AToken)
				.And(EntityIsExpired)
				.When(MemberPermissionsIsAccessed)
				.Then(MockApiGetIsCalled<IJsonToken>, 1)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void OrganizationPermissions()
		{
			var story = new Story("OrganizationPermissions");

			var feature = story.InOrderTo("get the permissions over boards allowed by a token")
				.AsA("developer")
				.IWant("to get the OrganizationPermissions");

			feature.WithScenario("Access OrganizationPermissions property")
				.Given(AToken)
				.And(EntityIsRefreshed)
				.When(OrganizationPermissionsIsAccessed)
				.Then(MockApiGetIsCalled<IJsonToken>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access OrganizationPermissions property when expired")
				.Given(AToken)
				.And(EntityIsExpired)
				.When(OrganizationPermissionsIsAccessed)
				.Then(MockApiGetIsCalled<IJsonToken>, 1)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void Delete()
		{
			var story = new Story("Delete");

			var feature = story.InOrderTo("delete a token")
				.AsA("developer")
				.IWant("to call Delete");

			feature.WithScenario("Delete is called")
				.Given(AToken)
				.When(DeleteIsCalled)
				.Then(MockApiDeleteIsCalled<IJsonToken>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Delete is called without UserToken")
				.Given(AToken)
				.And(TokenNotSupplied)
				.When(DeleteIsCalled)
				.Then(MockApiPutIsCalled<IJsonToken>, 0)
				.And(ExceptionIsThrown<ReadOnlyAccessException>)

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
			_systemUnderTest = new EntityUnderTest();
			_systemUnderTest.Sut.Value = TrelloIds.Invalid;
			var mock = SetupMockGet<IJsonToken>();
			mock.Object.Permissions = new List<IJsonTokenPermission> {boardPerm.Object, memberPerm.Object, orgPerm.Object};
			mock.Object.IdMember = TrelloIds.MemberId;
			SetupMockRetrieve<Member>();
		}

		#endregion

		#region When

		private void BoardPermissionsIsAccessed()
		{
			Execute(() => _systemUnderTest.Sut.BoardPermissions);
		}
		private void DateCreatedIsAccessed()
		{
			Execute(() => _systemUnderTest.Sut.DateCreated);
		}
		private void DateExpiresIsAccessed()
		{
			Execute(() => _systemUnderTest.Sut.DateExpires);
		}
		private void IdentifierIsAccessed()
		{
			Execute(() => _systemUnderTest.Sut.Identifier);
		}
		private void MemberIsAccessed()
		{
			Execute(() => _systemUnderTest.Sut.Member);
		}
		private void MemberPermissionsIsAccessed()
		{
			Execute(() => _systemUnderTest.Sut.MemberPermissions);
		}
		private void OrganizationPermissionsIsAccessed()
		{
			Execute(() => _systemUnderTest.Sut.OrganizationPermissions);
		}
		private void DeleteIsCalled()
		{
			Execute(() => _systemUnderTest.Sut.Delete());
		}

		#endregion
	}
}

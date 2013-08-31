using Manatee.Trello.Contracts;
using Manatee.Trello.Exceptions;
using Manatee.Trello.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using StoryQ;

namespace Manatee.Trello.Test.Unit
{
	[TestClass]
	public class AttachmentTest : EntityTestBase<Attachment>
	{
		[TestMethod]
		public void Member()
		{
			var story = new Story("Member");

			var feature = story.InOrderTo("access the details of the member who added an attachment")
				.AsA("developer")
				.IWant("to get the member object.");

			feature.WithScenario("Access Member property")
				.Given(AnAttachment)
				.And(EntityIsRefreshed)
				.When(MemberIsAccessed)
				.Then(MockApiGetIsCalled<IJsonAttachment>, 0)
				.And(MockSvcRetrieveIsCalled<Member>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access Member property when expired")
				.Given(AnAttachment)
				.And(EntityIsRefreshed)
				.And(EntityIsExpired)
				.When(MemberIsAccessed)
				.Then(MockApiGetIsCalled<IJsonAttachment>, 0)
				.And(MockSvcRetrieveIsCalled<Member>, 1)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void Delete()
		{
			var story = new Story("Delete");

			var feature = story.InOrderTo("delete an attachment")
				.AsA("developer")
				.IWant("Delete to call the service.");

			feature.WithScenario("Delete is called")
				.Given(AnAttachment)
				.When(DeleteIsCalled)
				.Then(MockApiDeleteIsCalled<IJsonAttachment>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Delete is called without UserToken")
				.Given(AnAttachment)
				.And(TokenNotSupplied)
				.When(DeleteIsCalled)
				.Then(MockApiDeleteIsCalled<IJsonAttachment>, 0)
				.And(ExceptionIsThrown<ReadOnlyAccessException>)

				.Execute();
		}

		private void AnAttachment()
		{
			_systemUnderTest = new EntityUnderTest();
			OwnedBy<Card>();
			SetupMockGet<IJsonAttachment>();
			SetupMockRetrieve<Member>();
		}

		private void MemberIsAccessed()
		{
			Execute(() => _systemUnderTest.Sut.Member);
		}
		private void DeleteIsCalled()
		{
			Execute(() => _systemUnderTest.Sut.Delete());
		}
	}
}

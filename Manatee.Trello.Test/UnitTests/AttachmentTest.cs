using Manatee.Trello.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using StoryQ;

namespace Manatee.Trello.Test.UnitTests
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
				.When(MemberIsAccessed)
				.Then(MockApiGetIsCalled<Attachment>, 0)
				.And(MemberIsReturned)
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

			feature.WithScenario("Call Delete")
				.Given(AnAttachment)
				.When(DeleteIsCalled)
				.Then(MockApiDeleteIsCalled<Attachment>, 1)
				.And(ExceptionIsNotThrown)

				.Execute();
		}

		private void AnAttachment()
		{
			_systemUnderTest = new SystemUnderTest();
			_systemUnderTest.Sut.Svc = _systemUnderTest.Dependencies.Api.Object;
			SetupMockGet<Member>();
		}

		private void MemberIsAccessed()
		{
			Execute(() => _systemUnderTest.Sut.Member);
		}
		private void DeleteIsCalled()
		{
			Execute(() => _systemUnderTest.Sut.Delete());
		}

		private void MemberIsReturned()
		{
			Assert.IsNotNull(_actualResult);
			Assert.IsInstanceOfType(_actualResult, typeof(Member));
		}
	}
}

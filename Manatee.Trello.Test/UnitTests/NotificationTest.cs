using System;
using Manatee.Trello.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StoryQ;

namespace Manatee.Trello.Test.UnitTests
{
	[TestClass]
	public class NotificationTest : EntityTestBase<Notification>
	{
		[TestMethod]
		public void IsUnread()
		{
			var story = new Story("IsUnread");

			var feature = story.InOrderTo("control whether the board allows members to join without an invitation")
				.AsA("developer")
				.IWant("to get the IsUnread property value.");

			feature.WithScenario("Access IsUnread property")
				.Given(ANotification)
				.And(EntityIsNotExpired)
				.When(IsUnreadIsAccessed)
				.Then(MockApiGetIsCalled<Notification>, 0)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access IsUnread property when expired")
				.Given(ANotification)
				.And(EntityIsExpired)
				.When(IsUnreadIsAccessed)
				.Then(MockApiGetIsCalled<Notification>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set IsUnread property")
				.Given(ANotification)
				.When(IsUnreadIsSet, (bool?) true)
				.Then(MockApiPutIsCalled<Notification>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set IsUnread property to null")
				.Given(ANotification)
				.And(IsUnreadIs, (bool?) true)
				.When(IsUnreadIsSet, (bool?) null)
				.Then(MockApiPutIsCalled<Notification>, 0)
				.And(ExceptionIsThrown<ArgumentNullException>)

				.WithScenario("Set IsUnread property to same")
				.Given(ANotification)
				.And(IsUnreadIs, (bool?) true)
				.When(IsUnreadIsSet, (bool?) true)
				.Then(MockApiPutIsCalled<Notification>, 0)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set IsUnread property without AuthToken")
				.Given(ANotification)
				.And(TokenNotSupplied)
				.When(IsUnreadIsSet, (bool?) true)
				.Then(MockApiPutIsCalled<Notification>, 0)
				.And(ExceptionIsThrown<ReadOnlyAccessException>)

				.Execute();
		}
		[TestMethod]
		public void MemberCreator()
		{
			var story = new Story("MemberCreator");

			var feature = story.InOrderTo("access the details of the member who created a notification")
				.AsA("developer")
				.IWant("to get the member object.");

			feature.WithScenario("Access MemberCreator property")
				.Given(ANotification)
				.When(MemberIsAccessed)
				.Then(MockApiGetIsCalled<Notification>, 0)
				.And(NonNullValueOfTypeIsReturned<Member>)
				.And(ExceptionIsNotThrown)

				.Execute();
		}

		#region Given

		private void ANotification()
		{
			_systemUnderTest = new SystemUnderTest();
			_systemUnderTest.Sut.Svc = _systemUnderTest.Dependencies.Api.Object;
			SetupMockGet<Member>();
		}
		private void IsUnreadIs(bool? value)
		{
			SetupProperty(() => _systemUnderTest.Sut.IsUnread = value);
		}

		#endregion

		#region When

		private void IsUnreadIsAccessed()
		{
			Execute(() => _systemUnderTest.Sut.IsUnread);
		}
		private void IsUnreadIsSet(bool? value)
		{
			Execute(() => _systemUnderTest.Sut.IsUnread = value);
		}
		private void MemberIsAccessed()
		{
			Execute(() => _systemUnderTest.Sut.MemberCreator);
		}

		#endregion
	}
}
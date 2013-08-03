using System;
using Manatee.Trello.Exceptions;
using Manatee.Trello.Json;
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
				.And(EntityIsRefreshed)
				.When(IsUnreadIsAccessed)
				.Then(MockApiGetIsCalled<IJsonNotification>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access IsUnread property when expired")
				.Given(ANotification)
				.And(EntityIsExpired)
				.When(IsUnreadIsAccessed)
				.Then(MockApiGetIsCalled<IJsonNotification>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set IsUnread property")
				.Given(ANotification)
				.And(EntityIsRefreshed)
				.When(IsUnreadIsSet, (bool?)true)
				.Then(MockApiPutIsCalled<IJsonNotification>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set IsUnread property to null")
				.Given(ANotification)
				.And(EntityIsRefreshed)
				.And(IsUnreadIs, (bool?)true)
				.When(IsUnreadIsSet, (bool?) null)
				.Then(MockApiPutIsCalled<IJsonNotification>, 0)
				.And(ExceptionIsThrown<ArgumentNullException>)

				.WithScenario("Set IsUnread property to same")
				.Given(ANotification)
				.And(EntityIsRefreshed)
				.And(IsUnreadIs, (bool?)true)
				.When(IsUnreadIsSet, (bool?) true)
				.Then(MockApiPutIsCalled<IJsonNotification>, 0)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set IsUnread property without UserToken")
				.Given(ANotification)
				.And(TokenNotSupplied)
				.When(IsUnreadIsSet, (bool?) true)
				.Then(MockApiPutIsCalled<IJsonNotification>, 0)
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
				.And(EntityIsRefreshed)
				.When(MemberCreatorIsAccessed)
				.Then(MockSvcRetrieveIsCalled<Member>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access MemberCreator property when expired")
				.Given(ANotification)
				.And(EntityIsRefreshed)
				.And(EntityIsExpired)
				.When(MemberCreatorIsAccessed)
				.Then(MockSvcRetrieveIsCalled<Member>, 1)
				.And(ExceptionIsNotThrown)

				.Execute();
		}

		#region Given

		private void ANotification()
		{
			_systemUnderTest = new EntityUnderTest();
			_systemUnderTest.Sut.Svc = _systemUnderTest.Dependencies.Svc.Object;
			var notification = SetupMockGet<IJsonNotification>();
			notification.SetupGet(n => n.IdMemberCreator).Returns(TrelloIds.MemberId);
			SetupMockRetrieve<Member>();
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
		private void MemberCreatorIsAccessed()
		{
			Execute(() => _systemUnderTest.Sut.MemberCreator);
		}

		#endregion
	}
}
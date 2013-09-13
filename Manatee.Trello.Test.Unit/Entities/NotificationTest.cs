using Manatee.Trello.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Manatee.Trello.Test.Unit.Entities
{
	[TestClass]
	public class NotificationTest : EntityTestBase<Notification, IJsonNotification>
	{
		[TestMethod]
		public void IsUnread()
		{
			var feature = CreateFeature();

			feature.WithScenario("Access IsUnread property when not expired")
				.Given(ANotification)
				.When(IsUnreadIsAccessed)
				.Then(RepositoryRefreshIsNotCalled<Notification>)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access IsUnread property when expired")
				.Given(ANotification)
				.And(EntityIsExpired)
				.When(IsUnreadIsAccessed)
				.Then(RepositoryRefreshIsCalled<Notification>, EntityRequestType.Notification_Read_Refresh)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set IsUnread property")
				.Given(ANotification)
				.When(IsUnreadIsSet, (bool?)true)
				.Then(ValidatorWritableIsCalled)
				.And(ValidatorNullableIsCalled<bool>)
				.And(RepositoryUploadIsCalled, EntityRequestType.Notification_Write_IsUnread)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set IsUnread property to same")
				.Given(ANotification)
				.And(IsUnreadIs, (bool?)true)
				.When(IsUnreadIsSet, (bool?) true)
				.Then(ValidatorWritableIsCalled)
				.And(ValidatorNullableIsCalled<bool>)
				.And(RepositoryUploadIsNotCalled)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void MemberCreator()
		{
			var feature = CreateFeature();

			feature.WithScenario("Access MemberCreator property when not expired")
				.Given(ANotification)
				.And(EntityIsExpired)
				.When(MemberCreatorIsAccessed)
				.Then(RepositoryRefreshIsNotCalled<Notification>)
				.And(ExceptionIsNotThrown)

				.Execute();
		}

		#region Given

		private void ANotification()
		{
			_test = new EntityUnderTest();
		}
		private void IsUnreadIs(bool? value)
		{
			_test.Json.SetupGet(j => j.Unread)
				 .Returns(value);
		}

		#endregion

		#region When

		private void IsUnreadIsAccessed()
		{
			Execute(() => _test.Sut.IsUnread);
		}
		private void IsUnreadIsSet(bool? value)
		{
			Execute(() => _test.Sut.IsUnread = value);
		}
		private void MemberCreatorIsAccessed()
		{
			Execute(() => _test.Sut.MemberCreator);
		}

		#endregion
	}
}
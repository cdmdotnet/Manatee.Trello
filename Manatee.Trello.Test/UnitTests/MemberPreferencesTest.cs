using System;
using Manatee.Trello.Exceptions;
using Manatee.Trello.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StoryQ;

namespace Manatee.Trello.Test.UnitTests
{
	[TestClass]
	public class MemberPreferencesTest : EntityTestBase<MemberPreferences>
	{
		[TestMethod]
		public void ColorBlind()
		{
			var story = new Story("ColorBlind");

			var feature = story.InOrderTo("control whether a member prefers color blind mode")
				.AsA("developer")
				.IWant("to get and set ColorBlind");

			feature.WithScenario("Access ColorBlind property")
				.Given(AMemberPreferencesObject)
				.And(EntityIsRefreshed)
				.When(ColorBlindIsAccessed)
				.Then(MockApiGetIsCalled<IJsonMemberPreferences>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access ColorBlind property when expired")
				.Given(AMemberPreferencesObject)
				.And(EntityIsExpired)
				.When(ColorBlindIsAccessed)
				.Then(MockApiGetIsCalled<IJsonMemberPreferences>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set ColorBlind property")
				.Given(AMemberPreferencesObject)
				.And(EntityIsRefreshed)
				.When(ColorBlindIsSet, (bool?) true)
				.Then(MockApiPutIsCalled<IJsonMemberPreferences>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set ColorBlind property to null")
				.Given(AMemberPreferencesObject)
				.And(EntityIsRefreshed)
				.And(ColorBlindIs, (bool?) true)
				.When(ColorBlindIsSet, (bool?) null)
				.Then(MockApiPutIsCalled<IJsonMemberPreferences>, 0)
				.And(ExceptionIsThrown<ArgumentNullException>)

				.WithScenario("Set ColorBlind property to same")
				.Given(AMemberPreferencesObject)
				.And(EntityIsRefreshed)
				.And(ColorBlindIs, (bool?) true)
				.When(ColorBlindIsSet, (bool?) true)
				.Then(MockApiPutIsCalled<IJsonMemberPreferences>, 0)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set ColorBlind property without AuthToken")
				.Given(AMemberPreferencesObject)
				.And(TokenNotSupplied)
				.When(ColorBlindIsSet, (bool?) true)
				.Then(MockApiPutIsCalled<IJsonMemberPreferences>, 0)
				.And(ExceptionIsThrown<ReadOnlyAccessException>)

				.Execute();
		}
		[TestMethod]
		public void MinutesBetweenSummaries()
		{
			var story = new Story("MinutesBetweenSummaries");

			var feature = story.InOrderTo("control how often a member receives notification")
				.AsA("developer")
				.IWant("to get and set MinutesBetweenSummaries");

			feature.WithScenario("Access MinutesBetweenSummaries property")
				.Given(AMemberPreferencesObject)
				.And(EntityIsRefreshed)
				.When(MinutesBetweenSummariesIsAccessed)
				.Then(MockApiGetIsCalled<IJsonMemberPreferences>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access MinutesBetweenSummaries property when expired")
				.Given(AMemberPreferencesObject)
				.And(EntityIsExpired)
				.When(MinutesBetweenSummariesIsAccessed)
				.Then(MockApiGetIsCalled<IJsonMemberPreferences>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set MinutesBetweenSummaries property")
				.Given(AMemberPreferencesObject)
				.And(EntityIsRefreshed)
				.When(MinutesBetweenSummariesIsSet, (int?) 10)
				.Then(MockApiPutIsCalled<IJsonMemberPreferences>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set MinutesBetweenSummaries property to null")
				.Given(AMemberPreferencesObject)
				.And(EntityIsRefreshed)
				.And(MinutesBetweenSummariesIs, (int?) 10)
				.When(MinutesBetweenSummariesIsSet, (int?) null)
				.Then(MockApiPutIsCalled<IJsonMemberPreferences>, 0)
				.And(ExceptionIsThrown<ArgumentNullException>)

				.WithScenario("Set MinutesBetweenSummaries property to same")
				.Given(AMemberPreferencesObject)
				.And(EntityIsRefreshed)
				.And(MinutesBetweenSummariesIs, (int?) 10)
				.When(MinutesBetweenSummariesIsSet, (int?) 10)
				.Then(MockApiPutIsCalled<IJsonMemberPreferences>, 0)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set MinutesBetweenSummaries property without AuthToken")
				.Given(AMemberPreferencesObject)
				.And(TokenNotSupplied)
				.When(MinutesBetweenSummariesIsSet, (int?) 10)
				.Then(MockApiPutIsCalled<IJsonMemberPreferences>, 0)
				.And(ExceptionIsThrown<ReadOnlyAccessException>)

				.Execute();
		}
		[TestMethod]
		public void SendSummaries()
		{
			var story = new Story("SendSummaries");

			var feature = story.InOrderTo("control whether a member receives notification summary emails")
				.AsA("developer")
				.IWant("to get and set SendSummaries");

			feature.WithScenario("Access SendSummaries property")
				.Given(AMemberPreferencesObject)
				.And(EntityIsRefreshed)
				.When(SendSummariesIsAccessed)
				.Then(MockApiGetIsCalled<IJsonMemberPreferences>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access SendSummaries property when expired")
				.Given(AMemberPreferencesObject)
				.And(EntityIsExpired)
				.When(SendSummariesIsAccessed)
				.Then(MockApiGetIsCalled<IJsonMemberPreferences>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set SendSummaries property")
				.Given(AMemberPreferencesObject)
				.And(EntityIsRefreshed)
				.When(SendSummariesIsSet, (bool?) true)
				.Then(MockApiPutIsCalled<IJsonMemberPreferences>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set SendSummaries property to null")
				.Given(AMemberPreferencesObject)
				.And(EntityIsRefreshed)
				.And(SendSummariesIs, (bool?) true)
				.When(SendSummariesIsSet, (bool?) null)
				.Then(MockApiPutIsCalled<IJsonMemberPreferences>, 0)
				.And(ExceptionIsThrown<ArgumentNullException>)

				.WithScenario("Set SendSummaries property to same")
				.Given(AMemberPreferencesObject)
				.And(EntityIsRefreshed)
				.And(SendSummariesIs, (bool?) true)
				.When(SendSummariesIsSet, (bool?) true)
				.Then(MockApiPutIsCalled<IJsonMemberPreferences>, 0)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set SendSummaries property without AuthToken")
				.Given(AMemberPreferencesObject)
				.And(TokenNotSupplied)
				.When(SendSummariesIsSet, (bool?) true)
				.Then(MockApiPutIsCalled<IJsonMemberPreferences>, 0)
				.And(ExceptionIsThrown<ReadOnlyAccessException>)

				.Execute();
		}
		[TestMethod]
		public void MinutesBeforeDeadlineToNotify()
		{
			var story = new Story("MinutesBeforeDeadlineToNotify");

			var feature = story.InOrderTo("control how soon before a notification a member receives email notification")
				.AsA("developer")
				.IWant("to get and set MinutesBeforeDeadlineToNotify");

			feature.WithScenario("Access MinutesBeforeDeadlineToNotify property")
				.Given(AMemberPreferencesObject)
				.And(EntityIsRefreshed)
				.When(MinutesBeforeDeadlineToNotifyIsAccessed)
				.Then(MockApiGetIsCalled<IJsonMemberPreferences>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access MinutesBeforeDeadlineToNotify property when expired")
				.Given(AMemberPreferencesObject)
				.And(EntityIsExpired)
				.When(MinutesBeforeDeadlineToNotifyIsAccessed)
				.Then(MockApiGetIsCalled<IJsonMemberPreferences>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set MinutesBeforeDeadlineToNotify property")
				.Given(AMemberPreferencesObject)
				.And(EntityIsRefreshed)
				.When(MinutesBeforeDeadlineToNotifyIsSet, (int?) 10)
				.Then(MockApiPutIsCalled<IJsonMemberPreferences>, 1)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set MinutesBeforeDeadlineToNotify property to null")
				.Given(AMemberPreferencesObject)
				.And(EntityIsRefreshed)
				.And(MinutesBeforeDeadlineToNotifyIs, (int?) 10)
				.When(MinutesBeforeDeadlineToNotifyIsSet, (int?) null)
				.Then(MockApiPutIsCalled<IJsonMemberPreferences>, 0)
				.And(ExceptionIsThrown<ArgumentNullException>)

				.WithScenario("Set MinutesBeforeDeadlineToNotify property to same")
				.Given(AMemberPreferencesObject)
				.And(EntityIsRefreshed)
				.And(MinutesBeforeDeadlineToNotifyIs, (int?) 10)
				.When(MinutesBeforeDeadlineToNotifyIsSet, (int?) 10)
				.Then(MockApiPutIsCalled<IJsonMemberPreferences>, 0)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set MinutesBeforeDeadlineToNotify property without AuthToken")
				.Given(AMemberPreferencesObject)
				.And(TokenNotSupplied)
				.When(MinutesBeforeDeadlineToNotifyIsSet, (int?) 10)
				.Then(MockApiPutIsCalled<IJsonMemberPreferences>, 0)
				.And(ExceptionIsThrown<ReadOnlyAccessException>)

				.Execute();
		}

		#region Given

		private void AMemberPreferencesObject()
		{
			_systemUnderTest = new EntityUnderTest();
			_systemUnderTest.Sut.Svc = _systemUnderTest.Dependencies.Svc.Object;
			OwnedBy<Member>();
			SetupMockGet<IJsonMemberPreferences>();
		}
		private void ColorBlindIs(bool? value)
		{
			SetupProperty(() => _systemUnderTest.Sut.ColorBlind = value);
		}
		private void MinutesBetweenSummariesIs(int? value)
		{
			SetupProperty(() => _systemUnderTest.Sut.MinutesBetweenSummaries = value);
		}
		private void SendSummariesIs(bool? value)
		{
			SetupProperty(() => _systemUnderTest.Sut.SendSummaries = value);
		}
		private void MinutesBeforeDeadlineToNotifyIs(int? value)
		{
			SetupProperty(() => _systemUnderTest.Sut.MinutesBeforeDeadlineToNotify = value);
		}

		#endregion

		#region When

		private void ColorBlindIsAccessed()
		{
			Execute(() => _systemUnderTest.Sut.ColorBlind);
		}
		private void ColorBlindIsSet(bool? value)
		{
			Execute(() => _systemUnderTest.Sut.ColorBlind = value);
		}
		private void MinutesBetweenSummariesIsAccessed()
		{
			Execute(() => _systemUnderTest.Sut.MinutesBetweenSummaries);
		}
		private void MinutesBetweenSummariesIsSet(int? value)
		{
			Execute(() => _systemUnderTest.Sut.MinutesBetweenSummaries = value);
		}
		private void SendSummariesIsAccessed()
		{
			Execute(() => _systemUnderTest.Sut.SendSummaries);
		}
		private void SendSummariesIsSet(bool? value)
		{
			Execute(() => _systemUnderTest.Sut.SendSummaries = value);
		}
		private void MinutesBeforeDeadlineToNotifyIsAccessed()
		{
			Execute(() => _systemUnderTest.Sut.MinutesBeforeDeadlineToNotify);
		}
		private void MinutesBeforeDeadlineToNotifyIsSet(int? value)
		{
			Execute(() => _systemUnderTest.Sut.MinutesBeforeDeadlineToNotify = value);
		}

		#endregion
	}
}
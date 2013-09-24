using Manatee.Trello.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Manatee.Trello.Test.Unit.Entities
{
	[TestClass]
	public class MemberPreferencesTest : EntityTestBase<MemberPreferences, IJsonMemberPreferences>
	{
		[TestMethod]
		public void ColorBlind()
		{
			var feature = CreateFeature();

			feature.WithScenario("Access ColorBlind property when not expired")
				.Given(AMemberPreferencesObject)
				.When(ColorBlindIsAccessed)
				.Then(RepositoryRefreshIsNotCalled<MemberPreferences>)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access ColorBlind property when expired")
				.Given(AMemberPreferencesObject)
				.And(EntityIsExpired)
				.When(ColorBlindIsAccessed)
				.Then(RepositoryRefreshIsCalled<MemberPreferences>, EntityRequestType.MemberPreferences_Read_Refresh)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set ColorBlind property")
				.Given(AMemberPreferencesObject)
				.When(ColorBlindIsSet, (bool?) true)
				.Then(ValidatorWritableIsCalled)
				.And(ValidatorNullableIsCalled<bool>)
				.And(RepositoryUploadIsCalled, EntityRequestType.MemberPreferences_Write_ColorBlind)
				.And(ExceptionIsNotThrown)


				.WithScenario("Set ColorBlind property to same")
				.Given(AMemberPreferencesObject)
				.And(ColorBlindIs, (bool?) true)
				.When(ColorBlindIsSet, (bool?) true)
				.Then(ValidatorWritableIsCalled)
				.And(ValidatorNullableIsCalled<bool>)
				.And(RepositoryUploadIsNotCalled)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void MinutesBetweenSummaries()
		{
			var feature = CreateFeature();

			feature.WithScenario("Access MinutesBetweenSummaries property when not expired")
				.Given(AMemberPreferencesObject)
				.When(MinutesBetweenSummariesIsAccessed)
				.Then(RepositoryRefreshIsNotCalled<MemberPreferences>)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access MinutesBetweenSummaries property when expired")
				.Given(AMemberPreferencesObject)
				.And(EntityIsExpired)
				.When(MinutesBetweenSummariesIsAccessed)
				.Then(RepositoryRefreshIsCalled<MemberPreferences>, EntityRequestType.MemberPreferences_Read_Refresh)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set MinutesBetweenSummaries property")
				.Given(AMemberPreferencesObject)
				.When(MinutesBetweenSummariesIsSet, (MemberPreferenceSummaryPeriodType?) MemberPreferenceSummaryPeriodType.OneMinute)
				.Then(ValidatorWritableIsCalled)
				.And(ValidatorNullableIsCalled<MemberPreferenceSummaryPeriodType>)
				.And(ValidatorEnumerationIsCalled<MemberPreferenceSummaryPeriodType>)
				.And(RepositoryUploadIsCalled, EntityRequestType.MemberPreferences_Write_MinutesBetweenSummaries)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set MinutesBetweenSummaries property to same")
				.Given(AMemberPreferencesObject)
				.And(MinutesBetweenSummariesIs, (MemberPreferenceSummaryPeriodType?) MemberPreferenceSummaryPeriodType.OneHour)
				.When(MinutesBetweenSummariesIsSet, (MemberPreferenceSummaryPeriodType?)MemberPreferenceSummaryPeriodType.OneHour)
				.Then(ValidatorWritableIsCalled)
				.And(ValidatorNullableIsCalled<MemberPreferenceSummaryPeriodType>)
				.And(RepositoryUploadIsNotCalled)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void SendSummaries()
		{
			var feature = CreateFeature();

			feature.WithScenario("Access SendSummaries property when not expired")
				.Given(AMemberPreferencesObject)
				.When(SendSummariesIsAccessed)
				.Then(RepositoryRefreshIsNotCalled<MemberPreferences>)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access SendSummaries property when expired")
				.Given(AMemberPreferencesObject)
				.And(EntityIsExpired)
				.When(SendSummariesIsAccessed)
				.Then(RepositoryRefreshIsCalled<MemberPreferences>, EntityRequestType.MemberPreferences_Read_Refresh)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set SendSummaries property")
				.Given(AMemberPreferencesObject)
				.When(SendSummariesIsSet, (bool?) true)
				.Then(ValidatorWritableIsCalled)
				.And(ValidatorNullableIsCalled<bool>)
				.And(RepositoryUploadIsCalled, EntityRequestType.MemberPreferences_Write_SendSummaries)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set SendSummaries property to same")
				.Given(AMemberPreferencesObject)
				.And(SendSummariesIs, (bool?) true)
				.When(SendSummariesIsSet, (bool?) true)
				.Then(ValidatorWritableIsCalled)
				.And(ValidatorNullableIsCalled<bool>)
				.And(RepositoryUploadIsNotCalled)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void MinutesBeforeDeadlineToNotify()
		{
			var feature = CreateFeature();

			feature.WithScenario("Access MinutesBeforeDeadlineToNotify property when not expired")
				.Given(AMemberPreferencesObject)
				.When(MinutesBeforeDeadlineToNotifyIsAccessed)
				.Then(RepositoryRefreshIsNotCalled<MemberPreferences>)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access MinutesBeforeDeadlineToNotify property when expired")
				.Given(AMemberPreferencesObject)
				.And(EntityIsExpired)
				.When(MinutesBeforeDeadlineToNotifyIsAccessed)
				.Then(RepositoryRefreshIsCalled<MemberPreferences>, EntityRequestType.MemberPreferences_Read_Refresh)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set MinutesBeforeDeadlineToNotify property")
				.Given(AMemberPreferencesObject)
				.When(MinutesBeforeDeadlineToNotifyIsSet, (int?) 10)
				.Then(ValidatorWritableIsCalled)
				.And(ValidatorNullableIsCalled<int>)
				.And(RepositoryUploadIsCalled, EntityRequestType.MemberPreferences_Write_MinutesBeforeDeadlineToNotify)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set MinutesBeforeDeadlineToNotify property to same")
				.Given(AMemberPreferencesObject)
				.And(MinutesBeforeDeadlineToNotifyIs, (int?) 10)
				.When(MinutesBeforeDeadlineToNotifyIsSet, (int?) 10)
				.Then(ValidatorWritableIsCalled)
				.And(ValidatorNullableIsCalled<int>)
				.And(RepositoryUploadIsNotCalled)
				.And(ExceptionIsNotThrown)

				.Execute();
		}

		#region Given

		private void AMemberPreferencesObject()
		{
			_test = new EntityUnderTest();
			OwnedBy<Member>();
		}
		private void ColorBlindIs(bool? value)
		{
			_test.Json.SetupGet(j => j.ColorBlind)
				 .Returns(value);
		}
		private void MinutesBetweenSummariesIs(MemberPreferenceSummaryPeriodType? value)
		{
			_test.Json.SetupGet(j => j.MinutesBetweenSummaries)
				 .Returns((int?) value);
			ReapplyJson();
		}
		private void SendSummariesIs(bool? value)
		{
			_test.Json.SetupGet(j => j.SendSummaries)
				 .Returns(value);
		}
		private void MinutesBeforeDeadlineToNotifyIs(int? value)
		{
			_test.Json.SetupGet(j => j.MinutesBeforeDeadlineToNotify)
				 .Returns(value);
		}

		#endregion

		#region When

		private void ColorBlindIsAccessed()
		{
			Execute(() => _test.Sut.ColorBlind);
		}
		private void ColorBlindIsSet(bool? value)
		{
			Execute(() => _test.Sut.ColorBlind = value);
		}
		private void MinutesBetweenSummariesIsAccessed()
		{
			Execute(() => _test.Sut.MinutesBetweenSummaries);
		}
		private void MinutesBetweenSummariesIsSet(MemberPreferenceSummaryPeriodType? value)
		{
			Execute(() => _test.Sut.MinutesBetweenSummaries = value);
		}
		private void SendSummariesIsAccessed()
		{
			Execute(() => _test.Sut.SendSummaries);
		}
		private void SendSummariesIsSet(bool? value)
		{
			Execute(() => _test.Sut.SendSummaries = value);
		}
		private void MinutesBeforeDeadlineToNotifyIsAccessed()
		{
			Execute(() => _test.Sut.MinutesBeforeDeadlineToNotify);
		}
		private void MinutesBeforeDeadlineToNotifyIsSet(int? value)
		{
			Execute(() => _test.Sut.MinutesBeforeDeadlineToNotify = value);
		}

		#endregion
	}
}
using Manatee.Trello.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Manatee.Trello.Test.Unit
{
	[TestClass]
	public class BoardTest : EntityTestBase<Board>
	{
		#region Setup

		private class BoardUnderTest : SystemUnderTest
		{
			protected override Board BuildSut()
			{
				return new Board(TrelloIds.Test);
			}
		}

		private BoardUnderTest Board { get { return (BoardUnderTest) _sut; } }

		#endregion

		#region Test methods

		[TestMethod]
		public void Constructor()
		{
			CreateFeature().WithScenario("Constructor makes no calls")
						   .Given(ABoard)
						   .When(NothingHappens)
						   .Then(RestClientProviderCreateClientIsNotInvoked)
						   .And(CacheAddIsInvoked)
						   .And(ExceptionIsNotThrown)
						   .Execute();
		}
		[TestMethod]
		public void Actions()
		{
			CreateFeature().WithScenario("Get Actions")
						   .Given(ABoard)
						   .When(ActionsIsAccessed)
						   .Then(ResponseIsNotNull)
						   .And(RestClientProviderCreateClientIsNotInvoked)
						   .And(ExceptionIsNotThrown)
						   .Execute();
		}
		[TestMethod]
		public void Cards()
		{
			CreateFeature().WithScenario("Get Cards")
						   .Given(ABoard)
						   .When(CardsIsAccessed)
						   .Then(ResponseIsNotNull)
						   .And(RestClientProviderCreateClientIsNotInvoked)
						   .And(ExceptionIsNotThrown)
						   .Execute();
		}
		[TestMethod]
		public void Description()
		{
			CreateFeature().WithScenario("Get Description")
						   .Given(ABoard)
						   .When(DescriptionIsAccessed)
						   .Then(ResponseIsNotNull)
						   .And(RestClientProviderCreateClientIsInvoked)
						   .And(ExceptionIsNotThrown)
						   .WithScenario("Set Description with non-empty")
						   .Given(ABoard)
						   .When(DescriptionIsSet, "A description")
						   .Then(RestClientProviderCreateClientIsInvoked)
						   .And(ExceptionIsNotThrown)
						   .WithScenario("Set Description with null")
						   .Given(ABoard)
						   .When(DescriptionIsSet, (string)null)
						   .Then(RestClientProviderCreateClientIsInvoked)
						   .And(ExceptionIsNotThrown)
						   .WithScenario("Set Description with empty")
						   .Given(ABoard)
						   .When(DescriptionIsSet, string.Empty)
						   .Then(RestClientProviderCreateClientIsInvoked)
						   .And(ExceptionIsNotThrown)
						   .Execute();
		}
		[TestMethod]
		public void IsArchived()
		{
			CreateFeature().WithScenario("Get IsClosed")
						   .Given(ABoard)
						   .When(IsClosedIsAccessed)
						   .Then(ResponseIsNotNull)
						   .And(RestClientProviderCreateClientIsInvoked)
						   .And(ExceptionIsNotThrown)
						   .WithScenario("Set IsClosed with non-null")
						   .Given(ABoard)
						   .When(IsClosedIsSet, (bool?)false)
						   .Then(RestClientProviderCreateClientIsInvoked)
						   .And(ExceptionIsNotThrown)
						   .WithScenario("Set IsClosed with null")
						   .Given(ABoard)
						   .When(IsClosedIsSet, (bool?)null)
						   .Then(RestClientProviderCreateClientIsNotInvoked)
						   .And(ExceptionIsThrown<ValidationException<bool?>>)
						   .Execute();
		}
		[TestMethod]
		public void IsSubscribed()
		{
			CreateFeature().WithScenario("Get IsSubscribed")
						   .Given(ABoard)
						   .When(IsSubscribedIsAccessed)
						   .Then(ResponseIsNotNull)
						   .And(RestClientProviderCreateClientIsInvoked)
						   .And(ExceptionIsNotThrown)
						   .WithScenario("Set IsSubscribed with non-null")
						   .Given(ABoard)
						   .When(IsSubscribedIsSet, (bool?)false)
						   .Then(RestClientProviderCreateClientIsInvoked)
						   .And(ExceptionIsNotThrown)
						   .WithScenario("Set IsSubscribed with null")
						   .Given(ABoard)
						   .When(IsSubscribedIsSet, (bool?)null)
						   .Then(RestClientProviderCreateClientIsNotInvoked)
						   .And(ExceptionIsThrown<ValidationException<bool?>>)
						   .Execute();
		}
		[TestMethod]
		public void LabelNames()
		{
			CreateFeature().WithScenario("Get LabelNames")
						   .Given(ABoard)
						   .When(LabelNamesIsAccessed)
						   .Then(ResponseIsNotNull)
						   .And(RestClientProviderCreateClientIsNotInvoked)
						   .And(ExceptionIsNotThrown)
						   .Execute();
		}
		[TestMethod]
		public void Lists()
		{
			CreateFeature().WithScenario("Get Lists")
						   .Given(ABoard)
						   .When(ListsIsAccessed)
						   .Then(ResponseIsNotNull)
						   .And(RestClientProviderCreateClientIsNotInvoked)
						   .And(ExceptionIsNotThrown)
						   .Execute();
		}
		[TestMethod]
		public void Members()
		{
			CreateFeature().WithScenario("Get Members")
						   .Given(ABoard)
						   .When(MembersIsAccessed)
						   .Then(ResponseIsNotNull)
						   .And(RestClientProviderCreateClientIsNotInvoked)
						   .And(ExceptionIsNotThrown)
						   .Execute();
		}
		[TestMethod]
		public void Memberships()
		{
			CreateFeature().WithScenario("Get Memberships")
						   .Given(ABoard)
						   .When(MembershipsIsAccessed)
						   .Then(ResponseIsNotNull)
						   .And(RestClientProviderCreateClientIsNotInvoked)
						   .And(ExceptionIsNotThrown)
						   .Execute();
		}
		[TestMethod]
		public void Name()
		{
			CreateFeature().WithScenario("Get Name")
						   .Given(ABoard)
						   .When(NameIsAccessed)
						   .Then(ResponseIsNotNull)
						   .And(RestClientProviderCreateClientIsInvoked)
						   .And(ExceptionIsNotThrown)
						   .WithScenario("Set Name with non-empty")
						   .Given(ABoard)
						   .When(NameIsSet, "A description")
						   .Then(RestClientProviderCreateClientIsInvoked)
						   .And(ExceptionIsNotThrown)
						   .WithScenario("Set Name with null")
						   .Given(ABoard)
						   .When(NameIsSet, (string)null)
						   .Then(RestClientProviderCreateClientIsNotInvoked)
						   .And(ExceptionIsThrown<ValidationException<string>>)
						   .WithScenario("Set Name with empty")
						   .Given(ABoard)
						   .When(NameIsSet, string.Empty)
						   .Then(RestClientProviderCreateClientIsNotInvoked)
						   .And(ExceptionIsThrown<ValidationException<string>>)
						   .Execute();
		}
		[TestMethod]
		public void Organization()
		{
			CreateFeature().WithScenario("Get Organization")
						   .Given(ABoard)
						   .When(OrganizationIsAccessed)
						   .Then(ResponseIsNotNull)
						   .And(CacheFindIsInvoked<Organization>)
						   .And(RestClientProviderCreateClientIsInvoked)
						   .And(ExceptionIsNotThrown)
						   .WithScenario("Set Organization with non-null")
						   .Given(ABoard)
						   .When(OrganizationIsSet, TrelloIds.Test)
						   .Then(RestClientProviderCreateClientIsInvoked)
						   .And(ExceptionIsNotThrown)
						   .WithScenario("Set Organization with null")
						   .Given(ABoard)
						   .When(OrganizationIsSet, (string)null)
						   .Then(RestClientProviderCreateClientIsInvoked)
						   .And(ExceptionIsNotThrown)
						   .Execute();
		}
		[TestMethod]
		public void Preferences()
		{
			CreateFeature().WithScenario("Get Preferences")
						   .Given(ABoard)
						   .When(PreferencesIsAccessed)
						   .Then(ResponseIsNotNull)
						   .And(RestClientProviderCreateClientIsNotInvoked)
						   .And(ExceptionIsNotThrown)
						   .Execute();
		}
		[TestMethod]
		public void Url()
		{
			CreateFeature().WithScenario("Get Url")
						   .Given(ABoard)
						   .When(UrlIsAccessed)
						   .Then(ResponseIsNotNull)
						   .And(RestClientProviderCreateClientIsInvoked)
						   .And(ExceptionIsNotThrown)
						   .Execute();
		}

		#endregion

		#region Given

		private void ABoard()
		{
			_sut = new BoardUnderTest();
		}

		#endregion

		#region When

		private void ActionsIsAccessed()
		{
			Execute(() => Board.Sut.Actions);
		}
		private void CardsIsAccessed()
		{
			Execute(() => Board.Sut.Cards);
		}
		private void DescriptionIsAccessed()
		{
			Execute(() => Board.Sut.Description);
		}
		private void DescriptionIsSet(string value)
		{
			Execute(() => Board.Sut.Description = value);
			WaitForProcessor();
		}
		private void IsClosedIsAccessed()
		{
			Execute(() => Board.Sut.IsClosed);
		}
		private void IsClosedIsSet(bool? value)
		{
			Execute(() => Board.Sut.IsClosed = value);
			WaitForProcessor();
		}
		private void IsSubscribedIsAccessed()
		{
			Execute(() => Board.Sut.IsSubscribed);
		}
		private void IsSubscribedIsSet(bool? value)
		{
			Execute(() => Board.Sut.IsSubscribed = value);
			WaitForProcessor();
		}
		private void LabelNamesIsAccessed()
		{
			Execute(() => Board.Sut.LabelNames);
		}
		private void ListsIsAccessed()
		{
			Execute(() => Board.Sut.Lists);
		}
		private void MembersIsAccessed()
		{
			Execute(() => Board.Sut.Members);
		}
		private void MembershipsIsAccessed()
		{
			Execute(() => Board.Sut.Memberships);
		}
		private void NameIsAccessed()
		{
			Execute(() => Board.Sut.Name);
		}
		private void NameIsSet(string value)
		{
			Execute(() => Board.Sut.Name = value);
			WaitForProcessor();
		}
		private void OrganizationIsAccessed()
		{
			Execute(() => Board.Sut.Organization);
		}
		private void OrganizationIsSet(string value)
		{
			var list = value == null ? null : new Organization(value);
			Execute(() => Board.Sut.Organization = list);
			WaitForProcessor();
		}
		private void PreferencesIsAccessed()
		{
			Execute(() => Board.Sut.Preferences);
		}
		private void UrlIsAccessed()
		{
			Execute(() => Board.Sut.Url);
		}

		#endregion

		#region Then


		#endregion
	}
}

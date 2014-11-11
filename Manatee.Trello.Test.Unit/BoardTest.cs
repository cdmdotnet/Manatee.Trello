using Manatee.Trello.Exceptions;
using Manatee.Trello.Json;
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
			CreateScenario().Given(ABoard)
							.When(NothingHappens)
							.Then(RestClientExecuteIsNotInvoked<IJsonBoard>)
							.And(CacheAddIsInvoked)
							.And(ExceptionIsNotThrown)
							.Execute();
		}
		[TestMethod]
		public void GetActions()
		{
			CreateScenario().Given(ABoard)
							.When(ActionsIsAccessed)
							.Then(ResponseIsNotNull)
							.And(RestClientExecuteIsNotInvoked<IJsonBoard>)
							.And(ExceptionIsNotThrown)
							.Execute();
		}
		[TestMethod]
		public void GetCards()
		{
			CreateScenario().Given(ABoard)
							.When(CardsIsAccessed)
							.Then(ResponseIsNotNull)
							.And(RestClientExecuteIsNotInvoked<IJsonBoard>)
							.And(ExceptionIsNotThrown)
							.Execute();
		}
		[TestMethod]
		public void GetDescription()
		{
			CreateScenario().Given(ABoard)
							.When(DescriptionIsAccessed)
							.Then(ResponseIsNotNull)
							.And(RestClientExecuteIsInvoked<IJsonBoard>)
							.And(ExceptionIsNotThrown)
							.Execute();
		}
		[TestMethod]
		public void SetDescription()
		{
			CreateScenario().Given(ABoard)
							.When(DescriptionIsSet, "A description")
							.Then(RestClientExecuteIsInvoked<IJsonBoard>)
							.And(ExceptionIsNotThrown)
							.Execute();
		}
		[TestMethod]
		public void SetDescriptionWithNull()
		{
			CreateScenario().Given(ABoard)
							.When(DescriptionIsSet, (string) null)
							.Then(RestClientExecuteIsInvoked<IJsonBoard>)
							.And(ExceptionIsNotThrown)
							.Execute();
		}
		[TestMethod]
		public void SetDescriptionWithEmpty()
		{
			CreateScenario().Given(ABoard)
							.When(DescriptionIsSet, string.Empty)
							.Then(RestClientExecuteIsInvoked<IJsonBoard>)
							.And(ExceptionIsNotThrown)
							.Execute();
		}
		[TestMethod]
		public void GetIsArchived()
		{
			CreateScenario().Given(ABoard)
							.When(IsClosedIsAccessed)
							.Then(ResponseIsNotNull)
							.And(RestClientExecuteIsInvoked<IJsonBoard>)
							.And(ExceptionIsNotThrown)
							.Execute();
		}
		[TestMethod]
		public void SetIsArchived()
		{
			CreateScenario().Given(ABoard)
							.When(IsClosedIsSet, (bool?) true)
							.Then(RestClientExecuteIsInvoked<IJsonBoard>)
							.And(ExceptionIsNotThrown)
							.Execute();
		}
		[TestMethod]
		public void SetIsArchivedWithNull()
		{
			CreateScenario().Given(ABoard)
							.When(IsClosedIsSet, (bool?) null)
							.Then(RestClientExecuteIsNotInvoked<IJsonBoard>)
							.And(ExceptionIsThrown<ValidationException<bool?>>)
							.Execute();
		}
		[TestMethod]
		public void GetIsSubscribed()
		{
			CreateScenario().Given(ABoard)
							.When(IsSubscribedIsAccessed)
							.Then(ResponseIsNotNull)
							.And(RestClientExecuteIsInvoked<IJsonBoard>)
							.And(ExceptionIsNotThrown)
							.Execute();
		}
		[TestMethod]
		public void SetIsSubscribed()
		{
			CreateScenario().Given(ABoard)
							.When(IsSubscribedIsSet, (bool?) false)
							.Then(RestClientExecuteIsInvoked<IJsonBoard>)
							.And(ExceptionIsNotThrown)
							.Execute();
		}
		[TestMethod]
		public void SetIsSubscribedWithNull()
		{
			CreateScenario().Given(ABoard)
							.When(IsSubscribedIsSet, (bool?) null)
							.Then(RestClientExecuteIsNotInvoked<IJsonBoard>)
							.And(ExceptionIsThrown<ValidationException<bool?>>)
							.Execute();
		}
		[TestMethod]
		public void GetLabelNames()
		{
			CreateScenario().Given(ABoard)
							.When(LabelNamesIsAccessed)
							.Then(ResponseIsNotNull)
							.And(RestClientExecuteIsNotInvoked<IJsonBoard>)
							.And(ExceptionIsNotThrown)
							.Execute();
		}
		[TestMethod]
		public void GetLists()
		{
			CreateScenario().Given(ABoard)
							.When(ListsIsAccessed)
							.Then(ResponseIsNotNull)
							.And(RestClientExecuteIsNotInvoked<IJsonBoard>)
							.And(ExceptionIsNotThrown)
							.Execute();
		}
		[TestMethod]
		public void GetMembers()
		{
			CreateScenario().Given(ABoard)
							.When(MembersIsAccessed)
							.Then(ResponseIsNotNull)
							.And(RestClientExecuteIsNotInvoked<IJsonBoard>)
							.And(ExceptionIsNotThrown)
							.Execute();
		}
		[TestMethod]
		public void GetMemberships()
		{
			CreateScenario().Given(ABoard)
							.When(MembershipsIsAccessed)
							.Then(ResponseIsNotNull)
							.And(RestClientExecuteIsNotInvoked<IJsonBoard>)
							.And(ExceptionIsNotThrown)
							.Execute();
		}
		[TestMethod]
		public void GetName()
		{
			CreateScenario().Given(ABoard)
							.When(NameIsAccessed)
							.Then(ResponseIsNotNull)
							.And(RestClientExecuteIsInvoked<IJsonBoard>)
							.And(ExceptionIsNotThrown)
							.Execute();
		}
		[TestMethod]
		public void SetName()
		{
			CreateScenario().Given(ABoard)
							.When(NameIsSet, "A description")
							.Then(RestClientExecuteIsInvoked<IJsonBoard>)
							.And(ExceptionIsNotThrown)
							.Execute();
		}
		[TestMethod]
		public void SetNameWithNull()
		{
			CreateScenario().Given(ABoard)
							.When(NameIsSet, (string) null)
							.Then(RestClientExecuteIsNotInvoked<IJsonBoard>)
							.And(ExceptionIsThrown<ValidationException<string>>)
							.Execute();
		}
		[TestMethod]
		public void SetNameWithEmpty()
		{
			CreateScenario().Given(ABoard)
							.When(NameIsSet, string.Empty)
							.Then(RestClientExecuteIsNotInvoked<IJsonBoard>)
							.And(ExceptionIsThrown<ValidationException<string>>)
							.Execute();
		}
		[TestMethod]
		public void GetOrganization()
		{
			CreateScenario().Given(ABoard)
							.When(OrganizationIsAccessed)
							.Then(ResponseIsNotNull)
							.And(CacheFindIsInvoked<Organization>)
							.And(RestClientExecuteIsInvoked<IJsonBoard>)
							.And(ExceptionIsNotThrown)
							.Execute();
		}
		[TestMethod]
		public void SetOrganization()
		{
			CreateScenario().Given(ABoard)
							.When(OrganizationIsSet, TrelloIds.Test)
							.Then(RestClientExecuteIsInvoked<IJsonBoard>)
							.And(ExceptionIsNotThrown)
							.Execute();
		}
		[TestMethod]
		public void SetOrganizationWithNull()
		{
			CreateScenario().Given(ABoard)
							.When(OrganizationIsSet, (string) null)
							.Then(RestClientExecuteIsInvoked<IJsonBoard>)
							.And(ExceptionIsNotThrown)
							.Execute();
		}
		[TestMethod]
		public void GetPreferences()
		{
			CreateScenario().Given(ABoard)
							.When(PreferencesIsAccessed)
							.Then(ResponseIsNotNull)
							.And(RestClientExecuteIsNotInvoked<IJsonBoard>)
							.And(ExceptionIsNotThrown)
							.Execute();
		}
		[TestMethod]
		public void GetUrl()
		{
			CreateScenario().Given(ABoard)
							.When(UrlIsAccessed)
							.Then(ResponseIsNotNull)
							.And(RestClientExecuteIsInvoked<IJsonBoard>)
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

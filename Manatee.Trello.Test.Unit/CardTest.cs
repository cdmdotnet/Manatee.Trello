using System;
using Manatee.Trello.Exceptions;
using Manatee.Trello.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Manatee.Trello.Test.Unit
{
	[TestClass]
	public class CardTest : EntityTestBase<Card>
	{
		#region Setup

		private class CardUnderTest : SystemUnderTest
		{
			protected override Card BuildSut()
			{
				return new Card(TrelloIds.Test);
			}
		}

		private CardUnderTest Card { get { return (CardUnderTest) _sut; } }

		#endregion

		#region Test methods

		[TestMethod]
		public void Constructor()
		{
			CreateScenario().Given(ACard)
			               .When(NothingHappens)
						   .Then(RestClientExecuteIsNotInvoked<IJsonCard>)
						   .And(CacheAddIsInvoked)
			               .And(ExceptionIsNotThrown)
						   .Execute();
		}
		[TestMethod]
		public void GetActions()
		{
			CreateScenario().Given(ACard)
						   .When(ActionsIsAccessed)
						   .Then(ResponseIsNotNull)
						   .And(RestClientExecuteIsNotInvoked<IJsonCard>)
						   .And(ExceptionIsNotThrown)
						   .Execute();
		}
		[TestMethod]
		public void GetAttachments()
		{
			CreateScenario().Given(ACard)
						   .When(AttachmentsIsAccessed)
						   .Then(ResponseIsNotNull)
						   .And(RestClientExecuteIsNotInvoked<IJsonCard>)
						   .And(ExceptionIsNotThrown)
						   .Execute();
		}
		[TestMethod]
		public void GetBadges()
		{
			CreateScenario().Given(ACard)
						   .When(BadgesIsAccessed)
						   .Then(ResponseIsNotNull)
						   .And(RestClientExecuteIsNotInvoked<IJsonCard>)
						   .And(ExceptionIsNotThrown)
						   .Execute();
		}
		[TestMethod]
		public void GetBoard()
		{
			CreateScenario().Given(ACard)
						   .When(BoardIsAccessed)
						   .Then(ResponseIsNotNull)
						   .And(CacheFindIsInvoked<Board>)
						   .And(RestClientExecuteIsInvoked<IJsonCard>)
						   .And(ExceptionIsNotThrown)
						   .Execute();
		}
		[TestMethod]
		public void GetCheckLists()
		{
			CreateScenario().Given(ACard)
						   .When(CheckListsIsAccessed)
						   .Then(ResponseIsNotNull)
						   .And(RestClientExecuteIsNotInvoked<IJsonCard>)
						   .And(ExceptionIsNotThrown)
						   .Execute();
		}
		[TestMethod]
		public void GetComments()
		{
			CreateScenario().Given(ACard)
						   .When(CommentsIsAccessed)
						   .Then(ResponseIsNotNull)
						   .And(RestClientExecuteIsNotInvoked<IJsonCard>)
						   .And(ExceptionIsNotThrown)
						   .Execute();
		}
		[TestMethod]
		public void GetDescription()
		{
			CreateScenario().Given(ACard)
						   .When(DescriptionIsAccessed)
						   .Then(ResponseIsNotNull)
						   .And(RestClientExecuteIsInvoked<IJsonCard>)
						   .And(ExceptionIsNotThrown)
						   .Execute();
		}
		[TestMethod]
		public void SetDescription()
		{
			CreateScenario().Given(ACard)
						   .When(DescriptionIsSet, "A description")
						   .Then(RestClientExecuteIsInvoked<IJsonCard>)
						   .And(ExceptionIsNotThrown)
						   .Execute();
		}
		[TestMethod]
		public void SetDescriptionWithNull()
		{
			CreateScenario().Given(ACard)
						   .When(DescriptionIsSet, (string) null)
						   .Then(RestClientExecuteIsInvoked<IJsonCard>)
						   .And(ExceptionIsNotThrown)
						   .Execute();
		}
		[TestMethod]
		public void SetDescriptionWithEmpty()
		{
			CreateScenario().Given(ACard)
						   .When(DescriptionIsSet, string.Empty)
						   .Then(RestClientExecuteIsInvoked<IJsonCard>)
						   .And(ExceptionIsNotThrown)
						   .Execute();
		}
		[TestMethod]
		public void GetDueDate()
		{
			CreateScenario().Given(ACard)
						   .When(DueDateIsAccessed)
						   .Then(ResponseIsNotNull)
						   .And(RestClientExecuteIsInvoked<IJsonCard>)
						   .And(ExceptionIsNotThrown)
						   .Execute();
		}
		[TestMethod]
		public void SetDueDate()
		{
			CreateScenario().Given(ACard)
						   .When(DueDateIsSet, (DateTime?)DateTime.Today.AddDays(1))
						   .Then(RestClientExecuteIsInvoked<IJsonCard>)
						   .And(ExceptionIsNotThrown)
						   .Execute();
		}
		[TestMethod]
		public void SetDueDateWithNull()
		{
			CreateScenario().Given(ACard)
						   .When(DueDateIsSet, (DateTime?)null)
						   .Then(RestClientExecuteIsNotInvoked<IJsonCard>)
						   .And(ExceptionIsThrown<ValidationException<DateTime?>>)
						   .Execute();
		}
		[TestMethod]
		public void GetIsArchived()
		{
			CreateScenario().Given(ACard)
						   .When(IsArchivedIsAccessed)
						   .Then(ResponseIsNotNull)
						   .And(RestClientExecuteIsInvoked<IJsonCard>)
						   .And(ExceptionIsNotThrown)
						   .Execute();
		}
		[TestMethod]
		public void SetIsArchived()
		{
			CreateScenario().Given(ACard)
						   .When(IsArchivedIsSet, (bool?)false)
						   .Then(RestClientExecuteIsInvoked<IJsonCard>)
						   .And(ExceptionIsNotThrown)
						   .Execute();
		}
		[TestMethod]
		public void SetIsArchivedWithNull()
		{
			CreateScenario().Given(ACard)
						   .When(IsArchivedIsSet, (bool?)null)
						   .Then(RestClientExecuteIsNotInvoked<IJsonCard>)
						   .And(ExceptionIsThrown<ValidationException<bool?>>)
						   .Execute();
		}
		[TestMethod]
		public void GetIsSubscribed()
		{
			CreateScenario().Given(ACard)
						   .When(IsSubscribedIsAccessed)
						   .Then(ResponseIsNotNull)
						   .And(RestClientExecuteIsInvoked<IJsonCard>)
						   .And(ExceptionIsNotThrown)
						   .Execute();
		}
		[TestMethod]
		public void SetIsSubscribed()
		{
			CreateScenario().Given(ACard)
						   .When(IsSubscribedIsSet, (bool?)false)
						   .Then(RestClientExecuteIsInvoked<IJsonCard>)
						   .And(ExceptionIsNotThrown)
						   .Execute();
		}
		[TestMethod]
		public void SetIsSubscribedWithNull()
		{
			CreateScenario().Given(ACard)
						   .When(IsSubscribedIsSet, (bool?)null)
						   .Then(RestClientExecuteIsNotInvoked<IJsonCard>)
						   .And(ExceptionIsThrown<ValidationException<bool?>>)
						   .Execute();
		}
		[TestMethod]
		public void GetLabels()
		{
			CreateScenario().Given(ACard)
						   .When(LabelsIsAccessed)
						   .Then(ResponseIsNotNull)
						   .And(RestClientExecuteIsNotInvoked<IJsonCard>)
						   .And(ExceptionIsNotThrown)
						   .Execute();
		}
		[TestMethod]
		public void GetLastActivity()
		{
			CreateScenario().Given(ACard)
						   .When(LastActivityIsAccessed)
						   .Then(ResponseIsNotNull)
						   .And(RestClientExecuteIsInvoked<IJsonCard>)
						   .And(ExceptionIsNotThrown)
						   .Execute();
		}
		[TestMethod]
		public void GetList()
		{
			CreateScenario().Given(ACard)
			               .When(ListIsAccessed)
			               .Then(ResponseIsNotNull)
						   .And(RestClientExecuteIsInvoked<IJsonCard>)
						   .And(CacheFindIsInvoked<List>)
						   .And(ExceptionIsNotThrown)
						   .Execute();
		}
		[TestMethod]
		public void SetList()
		{
			CreateScenario().Given(ACard)
			               .When(ListIsSet, TrelloIds.Test)
						   .Then(RestClientExecuteIsInvoked<IJsonCard>)
						   .And(ExceptionIsNotThrown)
						   .Execute();
		}
		[TestMethod]
		public void SetListWithNull()
		{
			CreateScenario().Given(ACard)
			               .When(ListIsSet, (string) null)
						   .Then(RestClientExecuteIsNotInvoked<IJsonCard>)
						   .And(ExceptionIsThrown<ValidationException<List>>)
			               .Execute();
		}
		[TestMethod]
		public void GetMembers()
		{
			CreateScenario().Given(ACard)
						   .When(MembersIsAccessed)
						   .Then(ResponseIsNotNull)
						   .And(RestClientExecuteIsNotInvoked<IJsonCard>)
						   .And(ExceptionIsNotThrown)
						   .Execute();
		}
		[TestMethod]
		public void GetName()
		{
			CreateScenario().Given(ACard)
						   .When(NameIsAccessed)
						   .Then(ResponseIsNotNull)
						   .And(RestClientExecuteIsInvoked<IJsonCard>)
						   .And(ExceptionIsNotThrown)
						   .Execute();
		}
		[TestMethod]
		public void SetName()
		{
			CreateScenario().Given(ACard)
						   .When(NameIsSet, "A description")
						   .Then(RestClientExecuteIsInvoked<IJsonCard>)
						   .And(ExceptionIsNotThrown)
						   .Execute();
		}
		[TestMethod]
		public void SetNameWithNull()
		{
			CreateScenario().Given(ACard)
						   .When(NameIsSet, (string)null)
						   .Then(RestClientExecuteIsNotInvoked<IJsonCard>)
						   .And(ExceptionIsThrown<ValidationException<string>>)
						   .Execute();
		}
		[TestMethod]
		public void SetNameWithEmpty()
		{
			CreateScenario().Given(ACard)
						   .When(NameIsSet, string.Empty)
						   .Then(RestClientExecuteIsNotInvoked<IJsonCard>)
						   .And(ExceptionIsThrown<ValidationException<string>>)
						   .Execute();
		}
		[TestMethod]
		public void GetPosition()
		{
			CreateScenario().Given(ACard)
			               .When(PositionIsAccessed)
			               .Then(ResponseIsNotNull)
						   .And(RestClientExecuteIsInvoked<IJsonCard>)
						   .And(ExceptionIsNotThrown)
						   .Execute();
		}
		[TestMethod]
		public void SetPosition()
		{
			CreateScenario().Given(ACard)
			               .When(PositionIsSet, new Position(30))
						   .Then(RestClientExecuteIsInvoked<IJsonCard>)
						   .And(ExceptionIsNotThrown)
						   .Execute();
		}
		[TestMethod]
		public void SetPositionWithNull()
		{
			CreateScenario().Given(ACard)
			               .When(PositionIsSet, (Position) null)
						   .Then(RestClientExecuteIsNotInvoked<IJsonCard>)
						   .And(ExceptionIsThrown<ValidationException<Position>>)
						   .Execute();
		}
		[TestMethod]
		public void SetPositionWithInvalid()
		{
			CreateScenario().Given(ACard)
						   .When(PositionIsSet, new Position(-30))
						   .Then(RestClientExecuteIsNotInvoked<IJsonCard>)
						   .And(ExceptionIsThrown<ValidationException<Position>>)
						   .Execute();
		}
		[TestMethod]
		public void GetShortId()
		{
			CreateScenario().Given(ACard)
						   .When(ShortIdIsAccessed)
						   .Then(ResponseIsNotNull)
						   .And(RestClientExecuteIsInvoked<IJsonCard>)
						   .And(ExceptionIsNotThrown)
						   .Execute();
		}
		[TestMethod]
		public void GetShortUrl()
		{
			CreateScenario().Given(ACard)
						   .When(ShortUrlIsAccessed)
						   .Then(ResponseIsNotNull)
						   .And(RestClientExecuteIsInvoked<IJsonCard>)
						   .And(ExceptionIsNotThrown)
						   .Execute();
		}
		[TestMethod]
		public void GetUrl()
		{
			CreateScenario().Given(ACard)
						   .When(UrlIsAccessed)
						   .Then(ResponseIsNotNull)
						   .And(RestClientExecuteIsInvoked<IJsonCard>)
						   .And(ExceptionIsNotThrown)
						   .Execute();
		}
		[TestMethod]
		public void Delete()
		{
			CreateScenario().Given(ACard)
						   .When(DeleteIsCalled)
						   .Then(RestClientExecuteIsInvoked<IJsonCard>)
						   .And(CacheRemoveIsInvoked)
						   .And(ExceptionIsNotThrown)
						   // TODO: Test when already deleted
						   //.WithScenario("Delete a deleted card")
						   //.Given(ACard)
						   //.And(CardIsDeleted)
						   //.When(DeleteIsCalled)
						   //.Then(RestClientProviderCreateClientIsNotInvoked)
						   //.And(ExceptionIsNotThrown)
						   .Execute();
		}

		#endregion

		#region Given

		private void ACard()
		{
			_sut = new CardUnderTest();
		}
		private void CardIsDeleted()
		{
			
		}

		#endregion

		#region When

		private void ActionsIsAccessed()
		{
			Execute(() => Card.Sut.Actions);
		}
		private void AttachmentsIsAccessed()
		{
			Execute(() => Card.Sut.Attachments);
		}
		private void BadgesIsAccessed()
		{
			Execute(() => Card.Sut.Badges);
		}
		private void BoardIsAccessed()
		{
			Execute(() => Card.Sut.Board);
		}
		private void CheckListsIsAccessed()
		{
			Execute(() => Card.Sut.CheckLists);
		}
		private void CommentsIsAccessed()
		{
			Execute(() => Card.Sut.Comments);
		}
		private void DescriptionIsAccessed()
		{
			Execute(() => Card.Sut.Description);
		}
		private void DescriptionIsSet(string value)
		{
			Execute(() => Card.Sut.Description = value);
			WaitForProcessor();
		}
		private void DueDateIsAccessed()
		{
			Execute(() => Card.Sut.DueDate);
		}
		private void DueDateIsSet(DateTime? value)
		{
			Execute(() => Card.Sut.DueDate = value);
			WaitForProcessor();
		}
		private void IsArchivedIsAccessed()
		{
			Execute(() => Card.Sut.IsArchived);
		}
		private void IsArchivedIsSet(bool? value)
		{
			Execute(() => Card.Sut.IsArchived = value);
			WaitForProcessor();
		}
		private void IsSubscribedIsAccessed()
		{
			Execute(() => Card.Sut.IsSubscribed);
		}
		private void IsSubscribedIsSet(bool? value)
		{
			Execute(() => Card.Sut.IsSubscribed = value);
			WaitForProcessor();
		}
		private void LabelsIsAccessed()
		{
			Execute(() => Card.Sut.Labels);
		}
		private void LastActivityIsAccessed()
		{
			Execute(() => Card.Sut.LastActivity);
		}
		private void ListIsAccessed()
		{
			Execute(() => Card.Sut.List);
		}
		private void ListIsSet(string value)
		{
			var list = value == null ? null : new List(value);
			Execute(() => Card.Sut.List = list);
			WaitForProcessor();
		}
		private void MembersIsAccessed()
		{
			Execute(() => Card.Sut.Members);
		}
		private void NameIsAccessed()
		{
			Execute(() => Card.Sut.Name);
		}
		private void NameIsSet(string value)
		{
			Execute(() => Card.Sut.Name = value);
			WaitForProcessor();
		}
		private void PositionIsAccessed()
		{
			Execute(() => Card.Sut.Position);
		}
		private void PositionIsSet(Position value)
		{
			Execute(() => Card.Sut.Position = value);
			WaitForProcessor();
		}
		private void ShortIdIsAccessed()
		{
			Execute(() => Card.Sut.ShortId);
		}
		private void ShortUrlIsAccessed()
		{
			Execute(() => Card.Sut.ShortUrl);
		}
		private void UrlIsAccessed()
		{
			Execute(() => Card.Sut.Url);
		}
		private void DeleteIsCalled()
		{
			Execute(() => Card.Sut.Delete());
		}

		#endregion

		#region Then


		#endregion

	}
}
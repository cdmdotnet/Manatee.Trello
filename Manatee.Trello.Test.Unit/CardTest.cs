using System;
using Manatee.Trello.Exceptions;
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
			CreateFeature().WithScenario("Constructor makes no calls")
			               .Given(ACard)
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
						   .Given(ACard)
						   .When(ActionsIsAccessed)
						   .Then(ResponseIsNotNull)
						   .And(RestClientProviderCreateClientIsNotInvoked)
						   .And(ExceptionIsNotThrown)
						   .Execute();
		}
		[TestMethod]
		public void Attachments()
		{
			CreateFeature().WithScenario("Get Attachments")
						   .Given(ACard)
						   .When(AttachmentsIsAccessed)
						   .Then(ResponseIsNotNull)
						   .And(RestClientProviderCreateClientIsNotInvoked)
						   .And(ExceptionIsNotThrown)
						   .Execute();
		}
		[TestMethod]
		public void Badges()
		{
			CreateFeature().WithScenario("Get Badges")
						   .Given(ACard)
						   .When(BadgesIsAccessed)
						   .Then(ResponseIsNotNull)
						   .And(RestClientProviderCreateClientIsNotInvoked)
						   .And(ExceptionIsNotThrown)
						   .Execute();
		}
		[TestMethod]
		public void Board()
		{
			CreateFeature().WithScenario("Get Board")
						   .Given(ACard)
						   .When(BoardIsAccessed)
						   .Then(ResponseIsNotNull)
						   .And(CacheFindIsInvoked<Board>)
						   .And(RestClientProviderCreateClientIsInvoked)
						   .And(ExceptionIsNotThrown)
						   .Execute();
		}
		[TestMethod]
		public void CheckLists()
		{
			CreateFeature().WithScenario("Get CheckLists")
						   .Given(ACard)
						   .When(CheckListsIsAccessed)
						   .Then(ResponseIsNotNull)
						   .And(RestClientProviderCreateClientIsNotInvoked)
						   .And(ExceptionIsNotThrown)
						   .Execute();
		}
		[TestMethod]
		public void Comments()
		{
			CreateFeature().WithScenario("Get Comments")
						   .Given(ACard)
						   .When(CommentsIsAccessed)
						   .Then(ResponseIsNotNull)
						   .And(RestClientProviderCreateClientIsNotInvoked)
						   .And(ExceptionIsNotThrown)
						   .Execute();
		}
		[TestMethod]
		public void Description()
		{
			CreateFeature().WithScenario("Get Description")
						   .Given(ACard)
						   .When(DescriptionIsAccessed)
						   .Then(ResponseIsNotNull)
						   .And(RestClientProviderCreateClientIsInvoked)
						   .And(ExceptionIsNotThrown)
						   .WithScenario("Set Description with non-empty")
						   .Given(ACard)
						   .When(DescriptionIsSet, "A description")
						   .Then(RestClientProviderCreateClientIsInvoked)
						   .And(ExceptionIsNotThrown)
						   .WithScenario("Set Description with null")
						   .Given(ACard)
						   .When(DescriptionIsSet, (string) null)
						   .Then(RestClientProviderCreateClientIsInvoked)
						   .And(ExceptionIsNotThrown)
						   .WithScenario("Set Description with empty")
						   .Given(ACard)
						   .When(DescriptionIsSet, string.Empty)
						   .Then(RestClientProviderCreateClientIsInvoked)
						   .And(ExceptionIsNotThrown)
						   .Execute();
		}
		[TestMethod]
		public void DueDate()
		{
			CreateFeature().WithScenario("Get DueDate")
						   .Given(ACard)
						   .When(DueDateIsAccessed)
						   .Then(ResponseIsNotNull)
						   .And(RestClientProviderCreateClientIsInvoked)
						   .And(ExceptionIsNotThrown)
						   .WithScenario("Set DueDate with non-null")
						   .Given(ACard)
						   .When(DueDateIsSet, (DateTime?)DateTime.Today.AddDays(1))
						   .Then(RestClientProviderCreateClientIsInvoked)
						   .And(ExceptionIsNotThrown)
						   .WithScenario("Set DueDate with null")
						   .Given(ACard)
						   .When(DueDateIsSet, (DateTime?)null)
						   .Then(RestClientProviderCreateClientIsNotInvoked)
						   .And(ExceptionIsThrown<ValidationException<DateTime?>>)
						   .Execute();
		}
		[TestMethod]
		public void IsArchived()
		{
			CreateFeature().WithScenario("Get IsArchived")
						   .Given(ACard)
						   .When(IsArchivedIsAccessed)
						   .Then(ResponseIsNotNull)
						   .And(RestClientProviderCreateClientIsInvoked)
						   .And(ExceptionIsNotThrown)
						   .WithScenario("Set IsArchived with non-null")
						   .Given(ACard)
						   .When(IsArchivedIsSet, (bool?)false)
						   .Then(RestClientProviderCreateClientIsInvoked)
						   .And(ExceptionIsNotThrown)
						   .WithScenario("Set IsArchived with null")
						   .Given(ACard)
						   .When(IsArchivedIsSet, (bool?)null)
						   .Then(RestClientProviderCreateClientIsNotInvoked)
						   .And(ExceptionIsThrown<ValidationException<bool?>>)
						   .Execute();
		}
		[TestMethod]
		public void IsSubscribed()
		{
			CreateFeature().WithScenario("Get IsSubscribed")
						   .Given(ACard)
						   .When(IsSubscribedIsAccessed)
						   .Then(ResponseIsNotNull)
						   .And(RestClientProviderCreateClientIsInvoked)
						   .And(ExceptionIsNotThrown)
						   .WithScenario("Set IsSubscribed with non-null")
						   .Given(ACard)
						   .When(IsSubscribedIsSet, (bool?)false)
						   .Then(RestClientProviderCreateClientIsInvoked)
						   .And(ExceptionIsNotThrown)
						   .WithScenario("Set IsSubscribed with null")
						   .Given(ACard)
						   .When(IsSubscribedIsSet, (bool?)null)
						   .Then(RestClientProviderCreateClientIsNotInvoked)
						   .And(ExceptionIsThrown<ValidationException<bool?>>)
						   .Execute();
		}
		[TestMethod]
		public void Labels()
		{
			CreateFeature().WithScenario("Get Labels")
						   .Given(ACard)
						   .When(LabelsIsAccessed)
						   .Then(ResponseIsNotNull)
						   .And(RestClientProviderCreateClientIsNotInvoked)
						   .And(ExceptionIsNotThrown)
						   .Execute();
		}
		[TestMethod]
		public void LastActivity()
		{
			CreateFeature().WithScenario("Get LastActivity")
						   .Given(ACard)
						   .When(LastActivityIsAccessed)
						   .Then(ResponseIsNotNull)
						   .And(RestClientProviderCreateClientIsInvoked)
						   .And(ExceptionIsNotThrown)
						   .Execute();
		}
		[TestMethod]
		public void List()
		{
			CreateFeature().WithScenario("Get List")
			               .Given(ACard)
			               .When(ListIsAccessed)
			               .Then(ResponseIsNotNull)
			               .And(RestClientProviderCreateClientIsInvoked)
						   .And(CacheFindIsInvoked<List>)
						   .And(ExceptionIsNotThrown)
			               .WithScenario("Set List with non-null")
			               .Given(ACard)
			               .When(ListIsSet, TrelloIds.Test)
			               .Then(RestClientProviderCreateClientIsInvoked)
						   .And(ExceptionIsNotThrown)
			               .WithScenario("Set List with null")
			               .Given(ACard)
			               .When(ListIsSet, (string) null)
			               .Then(RestClientProviderCreateClientIsNotInvoked)
						   .And(ExceptionIsThrown<ValidationException<List>>)
			               .Execute();
		}
		[TestMethod]
		public void Members()
		{
			CreateFeature().WithScenario("Get Members")
						   .Given(ACard)
						   .When(MembersIsAccessed)
						   .Then(ResponseIsNotNull)
						   .And(RestClientProviderCreateClientIsNotInvoked)
						   .And(ExceptionIsNotThrown)
						   .Execute();
		}
		[TestMethod]
		public void Name()
		{
			CreateFeature().WithScenario("Get Name")
						   .Given(ACard)
						   .When(NameIsAccessed)
						   .Then(ResponseIsNotNull)
						   .And(RestClientProviderCreateClientIsInvoked)
						   .And(ExceptionIsNotThrown)
						   .WithScenario("Set Name with non-empty")
						   .Given(ACard)
						   .When(NameIsSet, "A description")
						   .Then(RestClientProviderCreateClientIsInvoked)
						   .And(ExceptionIsNotThrown)
						   .WithScenario("Set Name with null")
						   .Given(ACard)
						   .When(NameIsSet, (string)null)
						   .Then(RestClientProviderCreateClientIsNotInvoked)
						   .And(ExceptionIsThrown<ValidationException<string>>)
						   .WithScenario("Set Name with empty")
						   .Given(ACard)
						   .When(NameIsSet, string.Empty)
						   .Then(RestClientProviderCreateClientIsNotInvoked)
						   .And(ExceptionIsThrown<ValidationException<string>>)
						   .Execute();
		}
		[TestMethod]
		public void Position()
		{
			CreateFeature().WithScenario("Get Position")
			               .Given(ACard)
			               .When(PositionIsAccessed)
			               .Then(ResponseIsNotNull)
			               .And(RestClientProviderCreateClientIsInvoked)
			               .And(ExceptionIsNotThrown)
			               .WithScenario("Set Position with non-null")
			               .Given(ACard)
			               .When(PositionIsSet, new Position(30))
			               .Then(RestClientProviderCreateClientIsInvoked)
			               .And(ExceptionIsNotThrown)
			               .WithScenario("Set Position with null")
			               .Given(ACard)
			               .When(PositionIsSet, (Position) null)
			               .Then(RestClientProviderCreateClientIsNotInvoked)
			               .And(ExceptionIsThrown<ValidationException<Position>>)
						   .WithScenario("Set Position with invalid")
						   .Given(ACard)
						   .When(PositionIsSet, new Position(-30))
						   .Then(RestClientProviderCreateClientIsNotInvoked)
						   .And(ExceptionIsThrown<ValidationException<Position>>)
						   .Execute();
		}
		[TestMethod]
		public void ShortId()
		{
			CreateFeature().WithScenario("Get ShortId")
						   .Given(ACard)
						   .When(ShortIdIsAccessed)
						   .Then(ResponseIsNotNull)
						   .And(RestClientProviderCreateClientIsInvoked)
						   .And(ExceptionIsNotThrown)
						   .Execute();
		}
		[TestMethod]
		public void ShortUrl()
		{
			CreateFeature().WithScenario("Get ShortUrl")
						   .Given(ACard)
						   .When(ShortUrlIsAccessed)
						   .Then(ResponseIsNotNull)
						   .And(RestClientProviderCreateClientIsInvoked)
						   .And(ExceptionIsNotThrown)
						   .Execute();
		}
		[TestMethod]
		public void Url()
		{
			CreateFeature().WithScenario("Get Url")
						   .Given(ACard)
						   .When(UrlIsAccessed)
						   .Then(ResponseIsNotNull)
						   .And(RestClientProviderCreateClientIsInvoked)
						   .And(ExceptionIsNotThrown)
						   .Execute();
		}
		[TestMethod]
		public void Delete()
		{
			CreateFeature().WithScenario("Delete an active card")
						   .Given(ACard)
						   .When(DeleteIsCalled)
						   .Then(RestClientProviderCreateClientIsInvoked)
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
using System.Linq;
using Manatee.Trello.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Manatee.Trello.Test.Unit.Entities
{
	[TestClass]
	public class CheckListTest : EntityTestBase<CheckList, IJsonCheckList>
	{
		[TestMethod]
		public void Board()
		{
			var feature = CreateFeature();

			feature.WithScenario("Access Board property when not expired")
				.Given(ACheckList)
				.When(BoardIsAccessed)
				.Then(RepositoryRefreshIsNotCalled<CheckList>)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access Board property when expired")
				.Given(ACheckList)
				.And(EntityIsExpired)
				.When(BoardIsAccessed)
				.Then(RepositoryRefreshIsCalled<CheckList>, EntityRequestType.CheckList_Read_Refresh)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void Card()
		{
			var feature = CreateFeature();

			feature.WithScenario("Access Card property when not expired")
				.Given(ACheckList)
				.When(CardIsAccessed)
				.Then(RepositoryRefreshIsNotCalled<CheckList>)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access Card property when expired")
				.Given(ACheckList)
				.And(EntityIsExpired)
				.When(CardIsAccessed)
				.Then(RepositoryRefreshIsCalled<CheckList>, EntityRequestType.CheckList_Read_Refresh)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set Card property")
				.Given(ACheckList)
				.When(CardIsSet, new Card { Id = TrelloIds.Test })
				.Then(ValidatorWritableIsCalled)
				.And(ValidatorEntityIsCalled<Card>)
				.And(RepositoryUploadIsCalled, EntityRequestType.CheckList_Write_Card)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set Card property to same")
				.Given(ACheckList)
				.And(CardIs, TrelloIds.Test)
				.When(CardIsSet, new Card {Id = TrelloIds.Test})
				.Then(ValidatorWritableIsCalled)
				.And(ValidatorEntityIsCalled<Card>)
				.And(RepositoryUploadIsNotCalled)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void CheckItems()
		{
			var feature = CreateFeature();

			feature.WithScenario("Access Lists property")
				.Given(ACheckList)
				.And(EntityIsExpired)
				.When(CheckItemsIsAccessed)
				.Then(RepositoryRefreshIsNotCalled<CheckList>)
				.And(RepositoryRefreshCollectionIsNotCalled<CheckItem>)
				.And(ExceptionIsNotThrown)

				.WithScenario("Actions collection enumerates")
				.Given(ACheckList)
				.And(EntityIsExpired)
				.When(CheckItemsIsEnumerated)
				.Then(RepositoryRefreshCollectionIsCalled<CheckItem>, EntityRequestType.CheckList_Read_CheckItems)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void Name()
		{
			var feature = CreateFeature();

			feature.WithScenario("Access Name property when not expired")
				.Given(ACheckList)
				.When(NameIsAccessed)
				.Then(RepositoryRefreshIsNotCalled<CheckList>)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access Name property when expired")
				.Given(ACheckList)
				.And(EntityIsExpired)
				.When(NameIsAccessed)
				.Then(RepositoryRefreshIsCalled<CheckList>, EntityRequestType.CheckList_Read_Refresh)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set Name property")
				.Given(ACheckList)
				.When(NameIsSet, "name")
				.Then(ValidatorWritableIsCalled)
				.And(ValidatorNonEmptyStringIsCalled)
				.And(RepositoryUploadIsCalled, EntityRequestType.CheckList_Write_Name)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set Name property to same")
				.Given(ACheckList)
				.And(NameIs, "description")
				.When(NameIsSet, "description")
				.Then(ValidatorWritableIsCalled)
				.And(ValidatorNonEmptyStringIsCalled)
				.And(RepositoryUploadIsNotCalled)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void Position()
		{
			var feature = CreateFeature();

			feature.WithScenario("Access Position property when not expired")
				.Given(ACheckList)
				.When(PositionIsAccessed)
				.Then(RepositoryRefreshIsNotCalled<CheckList>)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access Position property when expired")
				.Given(ACheckList)
				.And(EntityIsExpired)
				.When(PositionIsAccessed)
				.Then(RepositoryRefreshIsCalled<CheckList>, EntityRequestType.CheckList_Read_Refresh)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set Position property")
				.Given(ACheckList)
				.When(PositionIsSet, Trello.Position.Bottom)
				.Then(ValidatorWritableIsCalled)
				.And(ValidatorPositionIsCalled)
				.And(RepositoryUploadIsCalled, EntityRequestType.CheckList_Write_Position)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set Position property to same")
				.Given(ACheckList)
				.And(PositionIs, Trello.Position.Bottom)
				.When(PositionIsSet, Trello.Position.Bottom)
				.Then(ValidatorWritableIsCalled)
				.And(ValidatorPositionIsCalled)
				.And(RepositoryUploadIsNotCalled)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void AddCheckItem()
		{
			var feature = CreateFeature();

			feature.WithScenario("AddCheckItem is called")
				.Given(ACheckList)
				.When(AddCheckItemIsCalled, "list")
				.Then(ValidatorWritableIsCalled)
				.And(ValidatorNonEmptyStringIsCalled)
				.And(RepositoryDownloadIsCalled<CheckItem>, EntityRequestType.CheckList_Write_AddCheckItem)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void Delete()
		{
			var feature = CreateFeature();

			feature.WithScenario("Delete is called when not deleted")
				.Given(ACheckList)
				.When(DeleteIsCalled)
				.Then(ValidatorWritableIsCalled)
				.And(RepositoryUploadIsCalled, EntityRequestType.CheckList_Write_Delete)
				.And(ExceptionIsNotThrown)

				.WithScenario("Delete is called when already deleted")
				.Given(ACheckList)
				.And(AlreadyDeleted)
				.When(DeleteIsCalled)
				.Then(ValidatorWritableIsNotCalled)
				.And(RepositoryUploadIsNotCalled)
				.And(ExceptionIsNotThrown)

				.Execute();
		}

		#region Given

		private void ACheckList()
		{
			_test = new EntityUnderTest();
			_test.Dependencies.SetupListGeneration<CheckItem>();
			OwnedBy<Card>();
		}
		private void CardIs(string value)
		{
			_test.Json.SetupGet(j => j.IdCard)
				 .Returns(value);
		}
		private void NameIs(string value)
		{
			_test.Json.SetupGet(j => j.Name)
				 .Returns(value);
		}
		private void PositionIs(Position value)
		{
			_test.Json.SetupGet(j => j.Pos)
				 .Returns(value.Value);
			ReapplyJson();
		}
		private void AlreadyDeleted()
		{
			_test.Sut.ForceDeleted(true);
		}

		#endregion

		#region When

		private void BoardIsAccessed()
		{
			Execute(() => _test.Sut.Board);
		}
		private void CardIsAccessed()
		{
			Execute(() => _test.Sut.Card);
		}
		private void CardIsSet(Card value)
		{
			Execute(() => _test.Sut.Card = value);
		}
		private void CheckItemsIsAccessed()
		{
			Execute(() => _test.Sut.CheckItems);
		}
		private void CheckItemsIsEnumerated()
		{
			Execute(() => _test.Sut.CheckItems.ToList());
		}
		private void NameIsAccessed()
		{
			Execute(() => _test.Sut.Name);
		}
		private void NameIsSet(string value)
		{
			Execute(() => _test.Sut.Name = value);
		}
		private void PositionIsAccessed()
		{
			Execute(() => _test.Sut.Position);
		}
		private void PositionIsSet(Position value)
		{
			Execute(() => _test.Sut.Position = value);
		}
		private void AddCheckItemIsCalled(string name)
		{
			SetupRepositoryDownload<CheckItem>();
			Execute(() => _test.Sut.AddCheckItem(name));
		}
		private void DeleteIsCalled()
		{
			Execute(() => _test.Sut.Delete());
		}

		#endregion
	}
}
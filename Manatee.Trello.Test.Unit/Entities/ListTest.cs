using Manatee.Trello.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Manatee.Trello.Test.Unit.Entities
{
	[TestClass]
	public class ListTest : EntityTestBase<List, IJsonList>
	{
		[TestMethod]
		public void Actions()
		{
			var feature = CreateFeature();

			feature.WithScenario("Access Actions property")
				.Given(AList)
				.And(EntityIsExpired)
				.When(ActionsIsAccessed)
				.Then(RepositoryRefreshIsNotCalled<List>)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void Board()
		{
			var feature = CreateFeature();

			feature.WithScenario("Access Board property when not expired")
				.Given(AList)
				.When(BoardIsAccessed)
				.Then(RepositoryRefreshIsNotCalled<List>)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access Board property when expired")
				.Given(AList)
				.And(EntityIsExpired)
				.When(BoardIsAccessed)
				.Then(RepositoryRefreshIsCalled<List>, EntityRequestType.List_Read_Refresh)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set Board property")
				.Given(AList)
				.When(BoardIsSet, new Board { Id = TrelloIds.Invalid })
				.Then(ValidatorWritableIsCalled)
				.And(ValidatorEntityIsCalled<Board>)
				.And(RepositoryUploadIsCalled, EntityRequestType.List_Write_Board)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set Board property to same")
				.Given(AList)
				.And(BoardIs, new Board { Id = TrelloIds.Invalid })
				.When(BoardIsSet, new Board { Id = TrelloIds.Invalid })
				.Then(ValidatorWritableIsCalled)
				.And(ValidatorEntityIsCalled<Board>)
				.And(RepositoryUploadIsNotCalled)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void Cards()
		{
			var feature = CreateFeature();

			feature.WithScenario("Access Cards property")
				.Given(AList)
				.And(EntityIsExpired)
				.When(CardsIsAccessed)
				.Then(RepositoryRefreshIsNotCalled<List>)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void IsClosed()
		{
			var feature = CreateFeature();

			feature.WithScenario("Access IsClosed property")
				.Given(AList)
				.When(IsClosedIsAccessed)
				.Then(RepositoryRefreshIsNotCalled<List>)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access IsClosed property when expired")
				.Given(AList)
				.And(EntityIsExpired)
				.When(IsClosedIsAccessed)
				.Then(RepositoryRefreshIsCalled<List>, EntityRequestType.List_Read_Refresh)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set IsClosed property")
				.Given(AList)
				.When(IsClosedIsSet, (bool?) true)
				.Then(ValidatorWritableIsCalled)
				.And(ValidatorNullableIsCalled<bool>)
				.And(RepositoryUploadIsCalled, EntityRequestType.List_Write_IsClosed)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set IsClosed property to same")
				.Given(AList)
				.And(IsClosedIs, (bool?) true)
				.When(IsClosedIsSet, (bool?) true)
				.Then(ValidatorWritableIsCalled)
				.And(ValidatorNullableIsCalled<bool>)
				.And(RepositoryUploadIsNotCalled)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void IsSubscribed()
		{
			var feature = CreateFeature();

			feature.WithScenario("Access IsSubscribed property")
				.Given(AList)
				.When(IsSubscribedIsAccessed)
				.Then(RepositoryRefreshIsNotCalled<List>)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access IsSubscribed property when expired")
				.Given(AList)
				.And(EntityIsExpired)
				.When(IsSubscribedIsAccessed)
				.Then(RepositoryRefreshIsCalled<List>, EntityRequestType.List_Read_Refresh)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set IsSubscribed property")
				.Given(AList)
				.When(IsSubscribedIsSet, (bool?)true)
				.Then(ValidatorWritableIsCalled)
				.And(ValidatorNullableIsCalled<bool>)
				.And(RepositoryUploadIsCalled, EntityRequestType.List_Write_IsSubscribed)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set IsSubscribed property to same")
				.Given(AList)
				.And(IsSubscribedIs, (bool?)true)
				.When(IsSubscribedIsSet, (bool?) true)
				.Then(ValidatorWritableIsCalled)
				.And(ValidatorNullableIsCalled<bool>)
				.And(RepositoryUploadIsNotCalled)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void Name()
		{
			var feature = CreateFeature();

			feature.WithScenario("Access Name property")
				.Given(AList)
				.When(NameIsAccessed)
				.Then(RepositoryRefreshIsNotCalled<List>)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access Name property when expired")
				.Given(AList)
				.And(EntityIsExpired)
				.When(NameIsAccessed)
				.Then(RepositoryRefreshIsCalled<List>, EntityRequestType.List_Read_Refresh)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set Name property")
				.Given(AList)
				.When(NameIsSet, "name")
				.Then(ValidatorWritableIsCalled)
				.And(ValidatorNonEmptyStringIsCalled)
				.And(RepositoryUploadIsCalled, EntityRequestType.List_Write_Name)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set Name property to same")
				.Given(AList)
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

			feature.WithScenario("Access Position property")
				.Given(AList)
				.When(PositionIsAccessed)
				.Then(RepositoryRefreshIsNotCalled<List>)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access Position property when expired")
				.Given(AList)
				.And(EntityIsExpired)
				.When(PositionIsAccessed)
				.Then(RepositoryRefreshIsCalled<List>, EntityRequestType.List_Read_Refresh)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set Position property")
				.Given(AList)
				.When(PositionIsSet, Trello.Position.Bottom)
				.Then(ValidatorWritableIsCalled)
				.And(ValidatorPositionIsCalled)
				.And(RepositoryUploadIsCalled, EntityRequestType.List_Write_Position)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set Position property to same")
				.Given(AList)
				.And(PositionIs, Trello.Position.Bottom)
				.When(PositionIsSet, Trello.Position.Bottom)
				.Then(ValidatorWritableIsCalled)
				.And(ValidatorPositionIsCalled)
				.And(RepositoryUploadIsNotCalled)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void AddCard()
		{
			var feature = CreateFeature();

			feature.WithScenario("AddCard is called")
				.Given(AList)
				.When(AddCardIsCalled, "checklist")
				.Then(ValidatorWritableIsCalled)
				.And(ValidatorNonEmptyStringIsCalled)
				.And(RepositoryDownloadIsCalled<Card>, EntityRequestType.List_Write_AddCard)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		//[TestMethod]
		//[Ignore]
		//public void Delete()
		//{
		//	throw new NotImplementedException();
		//}

		#region Given

		private void AList()
		{
			_test = new EntityUnderTest();
		}
		private void BoardIs(Board value)
		{
			_test.Json.SetupGet(j => j.IdBoard)
				 .Returns(value.Id);
		}
		private void IsClosedIs(bool? value)
		{
			_test.Json.SetupGet(j => j.Closed)
				 .Returns(value);
		}
		private void IsSubscribedIs(bool? value)
		{
			_test.Json.SetupGet(j => j.Subscribed)
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

		#endregion

		#region When

		private void ActionsIsAccessed()
		{
			Execute(() => _test.Sut.Actions);
		}
		private void BoardIsAccessed()
		{
			Execute(() => _test.Sut.Board);
		}
		private void BoardIsSet(Board value)
		{
			Execute(() => _test.Sut.Board = value);
		}
		private void CardsIsAccessed()
		{
			Execute(() => _test.Sut.Cards);
		}
		private void IsClosedIsAccessed()
		{
			Execute(() => _test.Sut.IsClosed);
		}
		private void IsClosedIsSet(bool? value)
		{
			Execute(() => _test.Sut.IsClosed = value);
		}
		private void IsSubscribedIsAccessed()
		{
			Execute(() => _test.Sut.IsSubscribed);
		}
		private void IsSubscribedIsSet(bool? value)
		{
			Execute(() => _test.Sut.IsSubscribed = value);
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
		private void AddCardIsCalled(string name)
		{
			SetupRepositoryDownload<Card>();
			Execute(() => _test.Sut.AddCard(name));
		}
		//private void DeleteIsCalled()
		//{
		//	Execute(() => _test.Sut.Delete());
		//}

		#endregion
	}
}
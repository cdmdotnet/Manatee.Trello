using Manatee.Trello.Internal;
using Manatee.Trello.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Manatee.Trello.Test.Unit.Entities
{
	[TestClass]
	public class CheckItemTest : EntityTestBase<CheckItem, IJsonCheckItem>
	{
		[TestMethod]
		public void Name()
		{
			var feature = CreateFeature();

			feature.WithScenario("Access Name property")
				.Given(ACheckItem)
				.When(NameIsAccessed)
				.Then(RepositoryRefreshIsNotCalled<CheckItem>)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access Name property when expired")
				.Given(ACheckItem)
				.And(EntityIsExpired)
				.When(NameIsAccessed)
				.Then(RepositoryRefreshIsCalled<CheckItem>, EntityRequestType.CheckItem_Read_Refresh)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set Name property")
				.Given(ACheckItem)
				.When(NameIsSet, "name")
				.Then(ValidatorWritableIsCalled)
				.And(ValidatorNonEmptyStringIsCalled)
				.And(RepositoryUploadIsCalled, EntityRequestType.CheckItem_Write_Name)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set Name property to same")
				.Given(ACheckItem)
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
				.Given(ACheckItem)
				.When(PositionIsAccessed)
				.Then(RepositoryRefreshIsNotCalled<CheckItem>)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access Position property when expired")
				.Given(ACheckItem)
				.And(EntityIsExpired)
				.When(PositionIsAccessed)
				.Then(RepositoryRefreshIsCalled<CheckItem>, EntityRequestType.CheckItem_Read_Refresh)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set Position property")
				.Given(ACheckItem)
				.When(PositionIsSet, Trello.Position.Bottom)
				.Then(ValidatorWritableIsCalled)
				.And(ValidatorPositionIsCalled)
				.And(RepositoryUploadIsCalled, EntityRequestType.CheckItem_Write_Position)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set Position property to same")
				.Given(ACheckItem)
				.And(PositionIs, Trello.Position.Bottom)
				.When(PositionIsSet, Trello.Position.Bottom)
				.Then(ValidatorWritableIsCalled)
				.And(ValidatorPositionIsCalled)
				.And(RepositoryUploadIsNotCalled)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void State()
		{
			var feature = CreateFeature();

			feature.WithScenario("Access State property")
				.Given(ACheckItem)
				.When(StateIsAccessed)
				.Then(RepositoryRefreshIsNotCalled<CheckItem>)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access State property when expired")
				.Given(ACheckItem)
				.And(EntityIsExpired)
				.When(StateIsAccessed)
				.Then(RepositoryRefreshIsCalled<CheckItem>, EntityRequestType.CheckItem_Read_Refresh)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set State property")
				.Given(ACheckItem)
				.When(StateIsSet, CheckItemStateType.Complete)
				.Then(ValidatorWritableIsCalled)
				.And(RepositoryUploadIsCalled, EntityRequestType.CheckItem_Write_State)
				.And(ExceptionIsNotThrown)

				.WithScenario("Set State property to same")
				.Given(ACheckItem)
				.And(StateIs, CheckItemStateType.Complete)
				.When(StateIsSet, CheckItemStateType.Complete)
				.Then(ValidatorWritableIsCalled)
				.And(RepositoryUploadIsNotCalled)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void Delete()
		{
			var feature = CreateFeature();

			feature.WithScenario("Delete is called")
				.Given(ACheckItem)
				.When(DeleteIsCalled)
				.Then(ValidatorWritableIsCalled)
				.And(RepositoryUploadIsCalled, EntityRequestType.CheckItem_Write_Delete)
				.And(ExceptionIsNotThrown)

				.WithScenario("Delete is called when already deleted")
				.Given(ACheckItem)
				.And(AlreadyDeleted)
				.When(DeleteIsCalled)
				.Then(ValidatorWritableIsNotCalled)
				.And(RepositoryUploadIsNotCalled)
				.And(ExceptionIsNotThrown)

				.Execute();
		}

		#region Given

		private void ACheckItem()
		{
			_test = new EntityUnderTest();
			var card = new Card {Id = TrelloIds.Invalid};
			var checklist = new CheckList {Id = TrelloIds.Invalid, Owner = card};
			_test.Sut.Owner = checklist;
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
		private void StateIs(CheckItemStateType value)
		{
			_test.Json.SetupGet(j => j.State)
				 .Returns(value.ToLowerString());
			ReapplyJson();
		}
		private void AlreadyDeleted()
		{
			_test.Sut.ForceDeleted(true);
		}

		#endregion

		#region When

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
		private void StateIsAccessed()
		{
			Execute(() => _test.Sut.State);
		}
		private void StateIsSet(CheckItemStateType value)
		{
			Execute(() => _test.Sut.State = value);
		}
		private void DeleteIsCalled()
		{
			Execute(() => _test.Sut.Delete());
		}

		#endregion
	}
}
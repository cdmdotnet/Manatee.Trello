using Manatee.Trello.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Manatee.Trello.Test.Unit.Entities
{
	[TestClass]
	public class AttachmentTest : EntityTestBase<Attachment, IJsonAttachment>
	{
		[TestMethod]
		public void Member()
		{
			var feature = CreateFeature();

			feature.WithScenario("Access Member property when not expired")
				.Given(AnAttachment)
				.When(MemberIsAccessed)
				.Then(RepositoryDownloadIsNotCalled<Attachment>)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access Member property when expired")
				.Given(AnAttachment)
				.And(EntityIsExpired)
				.When(MemberIsAccessed)
				.Then(RepositoryDownloadIsCalled<Member>, EntityRequestType.Member_Read_Refresh)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void Delete()
		{
			var feature = CreateFeature();

			feature.WithScenario("Delete is called when not deleted")
				.Given(AnAttachment)
				.When(DeleteIsCalled)
				.Then(ValidatorWritableIsCalled)
				.And(RepositoryUploadIsCalled, EntityRequestType.Attachment_Write_Delete)
				.And(ExceptionIsNotThrown)

				.WithScenario("Delete is called when already deleted")
				.Given(AnAttachment)
				.And(AlreadyDeleted)
				.When(DeleteIsCalled)
				.Then(ValidatorWritableIsNotCalled)
				.And(RepositoryUploadIsNotCalled)
				.And(ExceptionIsNotThrown)

				.Execute();
		}

		private void AnAttachment()
		{
			_test = new EntityUnderTest();
			OwnedBy<Card>();
		}
		private void AlreadyDeleted()
		{
			_test.Sut.ForceDeleted(true);
		}

		private void MemberIsAccessed()
		{
			Execute(() => _test.Sut.Member);
		}
		private void DeleteIsCalled()
		{
			Execute(() => _test.Sut.Delete());
		}
	}
}

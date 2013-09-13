using Manatee.Trello.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Manatee.Trello.Test.Unit.Entities
{
	[TestClass]
	public class BoardMembershipTest : EntityTestBase<BoardMembership, IJsonBoardMembership>
	{
		[TestMethod]
		public void IsDeactivated()
		{
			var feature = CreateFeature();

			feature.WithScenario("Access IsDeactivated property when not expired")
				.Given(ABoardMembership)
				.When(IsDeactivatedIsAccessed)
				.Then(RepositoryRefreshIsNotCalled<BoardMembership>)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access IsDeactivated property when expired")
				.Given(ABoardMembership)
				.And(EntityIsExpired)
				.When(IsDeactivatedIsAccessed)
				.Then(RepositoryRefreshIsCalled<BoardMembership>, EntityRequestType.BoardMembership_Read_Refresh)
				.And(ExceptionIsNotThrown)

				.Execute();
		}
		[TestMethod]
		public void Member()
		{
			var feature = CreateFeature();

			feature.WithScenario("Access Member property when not expired")
				.Given(ABoardMembership)
				.When(MemberIsAccessed)
				.Then(RepositoryRefreshIsNotCalled<BoardMembership>)
				.And(ExceptionIsNotThrown)

				.WithScenario("Access Member property when expired")
				.Given(ABoardMembership)
				.And(EntityIsExpired)
				.When(MemberIsAccessed)
				.Then(RepositoryRefreshIsCalled<BoardMembership>, EntityRequestType.BoardMembership_Read_Refresh)
				.And(ExceptionIsNotThrown)

				.Execute();
		}

		#region Given

		private void ABoardMembership()
		{
			_test = new EntityUnderTest();
			OwnedBy<Board>();
		}

		#endregion

		#region When

		private void IsDeactivatedIsAccessed()
		{
			Execute(() => _test.Sut.IsDeactivated);
		}
		private void MemberIsAccessed()
		{
			Execute(() => _test.Sut.Member);
		}

		#endregion
	}
}
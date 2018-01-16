using Moq;

namespace Manatee.Trello.Moq
{
	public class OrganizationMembershipMock : Mock<OrganizationMembership>
	{
		public OrganizationMembershipMock()
			: base(null)
		{
		}
	}
}
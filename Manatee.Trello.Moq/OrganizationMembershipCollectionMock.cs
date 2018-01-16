using Moq;

namespace Manatee.Trello.Moq
{
	public class OrganizationMembershipCollectionMock : Mock<OrganizationMembershipCollection>
	{
		public OrganizationMembershipCollectionMock()
			: base(null)
		{
		}
	}
}
using Moq;

namespace Manatee.Trello.Moq
{
	public class ReadOnlyOrganizationMembershipCollectionMock : Mock<ReadOnlyOrganizationMembershipCollection>
	{
		public ReadOnlyOrganizationMembershipCollectionMock()
			: base(null)
		{
		}
	}
}
using Moq;

namespace Manatee.Trello.Moq
{
	public class OrganizationCollectionMock : Mock<OrganizationCollection>
	{
		public OrganizationCollectionMock()
			: base(null)
		{
		}
	}
}
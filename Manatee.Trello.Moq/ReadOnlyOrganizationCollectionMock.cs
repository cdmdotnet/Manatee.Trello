using Moq;

namespace Manatee.Trello.Moq
{
	public class ReadOnlyOrganizationCollectionMock : Mock<ReadOnlyOrganizationCollection>
	{
		public ReadOnlyOrganizationCollectionMock()
			: base(null)
		{
		}
	}
}
using Moq;

namespace Manatee.Trello.Moq
{
	public class ReadOnlyMemberCollectionMock : Mock<ReadOnlyMemberCollection>
	{
		public ReadOnlyMemberCollectionMock()
			: base(null)
		{
		}
	}
}
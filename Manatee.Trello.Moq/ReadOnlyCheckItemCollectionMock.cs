using Moq;

namespace Manatee.Trello.Moq
{
	public class ReadOnlyCheckItemCollectionMock : Mock<ReadOnlyCheckItemCollection>
	{
		public ReadOnlyCheckItemCollectionMock()
			: base(null)
		{
		}
	}
}
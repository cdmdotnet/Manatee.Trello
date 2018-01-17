using Moq;

namespace Manatee.Trello.Moq
{
	public class ReadOnlyListCollectionMock : Mock<ReadOnlyListCollection>
	{
		public ReadOnlyListCollectionMock()
			: base(null)
		{
		}
	}
}
using Moq;

namespace Manatee.Trello.Moq
{
	public class ReadOnlyCardCollectionMock : Mock<ReadOnlyCardCollection>
	{
		public ReadOnlyCardCollectionMock()
			: base(null)
		{
		}
	}
}
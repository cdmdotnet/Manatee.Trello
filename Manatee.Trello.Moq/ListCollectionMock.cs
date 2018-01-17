using Moq;

namespace Manatee.Trello.Moq
{
	public class ListCollectionMock : Mock<ListCollection>
	{
		public ListCollectionMock()
			: base(null)
		{
		}
	}
}
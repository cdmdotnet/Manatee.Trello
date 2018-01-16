using Moq;

namespace Manatee.Trello.Moq
{
	public class ReadOnlyActionCollectionMock : Mock<ReadOnlyActionCollection>
	{
		public ReadOnlyActionCollectionMock()
			: base(null)
		{
		}
	}
}
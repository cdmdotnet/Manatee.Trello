using Moq;

namespace Manatee.Trello.Moq
{
	public class ReadOnlyCheckListCollectionMock : Mock<ReadOnlyCheckListCollection>
	{
		public ReadOnlyCheckListCollectionMock()
			: base(null)
		{
		}
	}
}
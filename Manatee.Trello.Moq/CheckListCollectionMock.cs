using Moq;

namespace Manatee.Trello.Moq
{
	public class CheckListCollectionMock : Mock<CheckListCollection>
	{
		public CheckListCollectionMock()
			: base(null)
		{
		}
	}
}
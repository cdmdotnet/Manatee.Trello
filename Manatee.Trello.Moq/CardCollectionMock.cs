using Moq;

namespace Manatee.Trello.Moq
{
	public class CardCollectionMock : Mock<CardCollection>
	{
		public CardCollectionMock()
			: base(null)
		{
		}
	}
}
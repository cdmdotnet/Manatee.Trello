using Moq;

namespace Manatee.Trello.Moq
{
	public class CardLabelCollectionMock : Mock<CardLabelCollection>
	{
		public CardLabelCollectionMock()
			: base(null)
		{
		}
	}
}
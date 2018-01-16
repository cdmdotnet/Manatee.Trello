using Moq;

namespace Manatee.Trello.Moq
{
	public class MemberCollectionMock : Mock<MemberCollection>
	{
		public MemberCollectionMock()
			: base(null)
		{
		}
	}
}
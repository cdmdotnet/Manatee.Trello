using Moq;

namespace Manatee.Trello.Moq
{
	public class CommentCollectionMock : Mock<CommentCollection>
	{
		public CommentCollectionMock()
			: base(null)
		{
		}
	}
}
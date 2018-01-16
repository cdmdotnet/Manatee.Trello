using System;
using Moq;

namespace Manatee.Trello.Moq
{
	public class CardMock : Mock<Card>
	{
		private ActionCollectionMock _actions;
		private BoardMock _board;
		private CommentCollectionMock _comments;

		public ActionCollectionMock Actions => _actions ?? (_actions = new ActionCollectionMock());
		public BoardMock Board => _board ?? (_board = new BoardMock());
		public CommentCollectionMock Comments => _comments ?? (_comments = new CommentCollectionMock());

		public CardMock()
			: base(string.Empty, null)
		{
			SetupGet(c => c.Actions).Returns(() => Actions.Object);
			SetupGet(c => c.Board).Returns(() => Board.Object);
		}
	}

	public class BoardMock : Mock<Board>
	{
		public BoardMock()
			: base(string.Empty, null)
		{
		}
	}

	public class ActionMock : Mock<Action>
	{
		public ActionMock()
			: base(string.Empty, null)
		{

		}
	}

	public class ActionCollectionMock : Mock<ReadOnlyActionCollection>
	{
		public ActionCollectionMock()
		{

		}
	}

	public class CommentCollectionMock : Mock<CommentCollection>
	{
		public CommentCollectionMock()
			: base(string.Empty, null)
		{

		}
	}

	//public class ActionDataMock : Mock<ActionData>
	//{
	//	public ActionDataMock()
	//		: base(string.Empty, null)
	//	{

	//	}
	//}

	//public class AttachmentMock : Mock<Attachment>
	//{
	//	public AttachmentMock()
	//		: base(string.Empty, null)
	//	{
	//	}
	//}

	//public class AttachmentCollectionMock : Mock<AttachmentCollection>
	//{
	//	public AttachmentCollectionMock()
	//		: base(string.Empty, null)
	//	{

	//	}
	//}

	//public class AttachmentPreviewCollectionMock : Mock<ReadOnlyAttachmentPreviewCollection>
	//{
	//	public AttachmentPreviewCollectionMock()
	//		: base(string.Empty, null)
	//	{

	//	}
	//}

	//public class ActionDataMock : Mock<Action>
	//{
	//	public ActionDataMock()
	//		: base(string.Empty, null)
	//	{

	//	}
	//}

	//public class BoardMock : Mock<Board>
	//{
	//	public BoardMock()
	//		: base(string.Empty, null)
	//	{
	//	}
	//}

	//public class ActionMock : Mock<Action>
	//{
	//	public ActionMock()
	//		: base(string.Empty, null)
	//	{

	//	}
	//}

	//public class ActionCollectionMock : Mock<Action>
	//{
	//	public ActionCollectionMock()
	//		: base(string.Empty, null)
	//	{

	//	}
	//}

	//public class ActionDataMock : Mock<Action>
	//{
	//	public ActionDataMock()
	//		: base(string.Empty, null)
	//	{

	//	}
	//}

	//public class BoardMock : Mock<Board>
	//{
	//	public BoardMock()
	//		: base(string.Empty, null)
	//	{
	//	}
	//}

	//public class ActionMock : Mock<Action>
	//{
	//	public ActionMock()
	//		: base(string.Empty, null)
	//	{

	//	}
	//}

	//public class ActionCollectionMock : Mock<Action>
	//{
	//	public ActionCollectionMock()
	//		: base(string.Empty, null)
	//	{

	//	}
	//}

	//public class ActionDataMock : Mock<Action>
	//{
	//	public ActionDataMock()
	//		: base(string.Empty, null)
	//	{

	//	}
	//}

	//public class BoardMock : Mock<Board>
	//{
	//	public BoardMock()
	//		: base(string.Empty, null)
	//	{
	//	}
	//}

	//public class ActionMock : Mock<Action>
	//{
	//	public ActionMock()
	//		: base(string.Empty, null)
	//	{

	//	}
	//}

	//public class ActionCollectionMock : Mock<Action>
	//{
	//	public ActionCollectionMock()
	//		: base(string.Empty, null)
	//	{

	//	}
	//}

	//public class ActionDataMock : Mock<Action>
	//{
	//	public ActionDataMock()
	//		: base(string.Empty, null)
	//	{

	//	}
	//}

	//public class BoardMock : Mock<Board>
	//{
	//	public BoardMock()
	//		: base(string.Empty, null)
	//	{
	//	}
	//}

	//public class ActionMock : Mock<Action>
	//{
	//	public ActionMock()
	//		: base(string.Empty, null)
	//	{

	//	}
	//}

	//public class ActionCollectionMock : Mock<Action>
	//{
	//	public ActionCollectionMock()
	//		: base(string.Empty, null)
	//	{

	//	}
	//}

	//public class ActionDataMock : Mock<Action>
	//{
	//	public ActionDataMock()
	//		: base(string.Empty, null)
	//	{

	//	}
	//}
}

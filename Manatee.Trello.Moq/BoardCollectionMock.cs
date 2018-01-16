using System.Collections.Generic;
using Moq;

namespace Manatee.Trello.Moq
{
	public class BoardCollectionMock : Mock<BoardCollection>
	{
		public List<Board> Items { get; }

		public BoardCollectionMock()
			: base(null)
		{
			Items = new List<Board>();

			Setup(c => c.GetEnumerator()).Returns(() => Items.GetEnumerator());
			Setup(c => c.Add(It.IsAny<string>(), It.IsAny<Board>()))
				.Returns((string s, Board b) =>
					{
						var mock = new BoardMock();
						mock.SetupGet(q => q.Name).Returns(s);
						Items.Add(mock.Object);
						return mock.Object;
					});
		}
	}
}
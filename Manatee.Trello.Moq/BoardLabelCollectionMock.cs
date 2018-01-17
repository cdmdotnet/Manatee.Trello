using System.Collections.Generic;
using Moq;

namespace Manatee.Trello.Moq
{
	public class BoardLabelCollectionMock : Mock<BoardLabelCollection>
	{
		public List<Label> Items { get; }

		public BoardLabelCollectionMock()
			: base(null)
		{
			Items = new List<Label>();

			Setup(c => c.GetEnumerator()).Returns(() => Items.GetEnumerator());
			Setup(c => c.Add(It.IsAny<string>(), It.IsAny<LabelColor?>()))
				.Returns((string name, LabelColor? color) =>
					{
						var mock = new LabelMock();
						mock.SetupGet(q => q.Color).Returns(color);
						mock.SetupGet(q => q.Name).Returns(name);
						Items.Add(mock.Object);
						return mock.Object;
					});
		}
	}
}
using System.Collections.Generic;
using Moq;

namespace Manatee.Trello.Moq
{
	public class AttachmentCollectionMock : Mock<AttachmentCollection>
	{
		public List<Attachment> Items { get; }

		public AttachmentCollectionMock()
			: base(null)
		{
			Items = new List<Attachment>();

			Setup(c => c.GetEnumerator()).Returns(() => Items.GetEnumerator());
			Setup(c => c.Add(It.IsAny<string>(), It.IsAny<string>()))
				.Returns((string url, string name) =>
					{
						var mock = new AttachmentMock();
						mock.SetupGet(q => q.Name).Returns(name);
						mock.SetupGet(q => q.Url).Returns(url);
						Items.Add(mock.Object);
						return mock.Object;
					});
			Setup(c => c.Add(It.IsAny<byte[]>(), It.IsAny<string>()))
				.Returns((byte[] data, string name) =>
					{
						var mock = new AttachmentMock();
						mock.SetupGet(q => q.Name).Returns(name);
						Items.Add(mock.Object);
						return mock.Object;
					});
		}
	}
}
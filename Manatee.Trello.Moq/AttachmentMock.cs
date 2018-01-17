using Moq;

namespace Manatee.Trello.Moq
{
	public class AttachmentMock : Mock<Attachment>
	{
		private MemberMock _member;
		public MemberMock Member => _member ?? (_member = new MemberMock());
		private ReadOnlyAttachmentPreviewCollectionMock _previews;
		public ReadOnlyAttachmentPreviewCollectionMock Previews => _previews ?? (_previews = new ReadOnlyAttachmentPreviewCollectionMock());

		public AttachmentMock()
			: base(null)
		{
			SetupGet(a => a.Member).Returns(() => Member.Object);
			SetupGet(a => a.Previews).Returns(() => Previews.Object);
		}
	}
}
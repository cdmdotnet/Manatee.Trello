using Moq;

namespace Manatee.Trello.Moq
{
	public class ActionMock : Mock<Action>
	{
		private MemberMock _creator;
		private ActionDataMock _data;

		public MemberMock Creator => _creator ?? (_creator = new MemberMock());
		public ActionDataMock Data => _data ?? (_data = new ActionDataMock());

		public ActionMock()
			: base(string.Empty, null)
		{
			SetupGet(a => a.Creator).Returns(() => Creator.Object);
			SetupGet(a => a.Data).Returns(() => Data.Object);
		}
	}
}
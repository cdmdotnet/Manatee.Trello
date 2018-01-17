using Moq;

namespace Manatee.Trello.Moq
{
	public class LabelMock : Mock<Label>
	{
		public LabelMock()
			: base(null)
		{
		}
	}
}
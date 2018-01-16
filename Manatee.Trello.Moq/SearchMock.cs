using Moq;

namespace Manatee.Trello.Moq
{
	public class SearchMock : Mock<Search>
	{
		public SearchMock()
			: base((string)null, null,SearchModelType.All, null, null, false)
		{
		}
	}
}
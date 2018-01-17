using Moq;

namespace Manatee.Trello.Moq
{
	public class OrganizationMock : Mock<Organization>
	{
		public OrganizationMock()
			: base(string.Empty, null)
		{
		}
	}
}
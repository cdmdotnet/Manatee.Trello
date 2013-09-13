using Manatee.Trello.Contracts;

namespace Manatee.Trello.Test.Unit.Mocks
{
	public class MockEntity : ExpiringObject
	{
		public int RefreshCallCount { get; private set; }
		public override bool IsStubbed { get { return false; } }
		public override bool Refresh()
		{
			RefreshCallCount++;
			return true;
		}
		internal override void ApplyJson(object obj) { }
	}
}
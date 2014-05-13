using System;
using Manatee.Trello.Contracts;

namespace Manatee.Trello.Test.Unit.Mocks
{
	public class MockEntity : ExpiringObject, IEquatable<MockEntity>, IComparable<MockEntity>
	{
		private bool _isStubbed;

		public int RefreshCallCount { get; private set; }
		public override bool IsStubbed { get { return _isStubbed; } }
		public override bool Refresh()
		{
			RefreshCallCount++;
			return true;
		}
		public bool Equals(MockEntity other)
		{
			return true;
		}
		public int CompareTo(MockEntity other)
		{
			return 0;
		}
		public void SetIsStubbed(bool isStubbed)
		{
			_isStubbed = isStubbed;
		}

		internal override void ApplyJson(object obj) { }
		internal override bool EqualsJson(object obj)
		{
			return true;
		}
	}
}
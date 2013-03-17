using System;

namespace Manatee.Trello.Implementation
{
	public abstract class EquatableExpiringObject : ExpiringObject, IEquatable<EquatableExpiringObject>
	{
		public EquatableExpiringObject() {}
		internal EquatableExpiringObject(TrelloService svc)
			: base(svc) {}

		public abstract bool Equals(EquatableExpiringObject other);
		internal abstract void Refresh(EquatableExpiringObject entity);
		internal abstract bool Match(string id);
	}
}
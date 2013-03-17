using System.Collections.Generic;

namespace Manatee.Trello.Implementation
{
	internal class EntityComparer<T> : IEqualityComparer<T> where T : EntityBase
	{
		public bool Equals(T x, T y)
		{
			return x.Id == y.Id;
		}
		public int GetHashCode(T obj)
		{
			return obj.GetHashCode();
		}
	}
}
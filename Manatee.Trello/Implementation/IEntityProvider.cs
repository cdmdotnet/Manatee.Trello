using Manatee.Trello.Contracts;

namespace Manatee.Trello.Implementation
{
	internal interface IEntityProvider<T> where T : ExpiringObject
	{
		T Parse(T obj);
	}
}
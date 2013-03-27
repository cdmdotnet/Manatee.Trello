using System;

namespace Manatee.Trello.Exceptions
{
	public class EntityNotOnTrelloException<T> : Exception
	{
		public T Entity { get; private set; }

		public EntityNotOnTrelloException(T entity)
			: base("Attempted to perform an operation with an entity which does not exist on Trello.")
		{
			Entity = entity;
		}
	}
}

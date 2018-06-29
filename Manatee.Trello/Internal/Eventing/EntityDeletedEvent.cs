using System;
using System.Linq;
using Manatee.Trello.Json;

namespace Manatee.Trello.Internal.Eventing
{
	internal class EntityDeletedEvent
	{
		public static EntityDeletedEvent Create(Type dataType, IJsonCacheable data)
		{
			var ctor = typeof(EntityDeletedEvent<>).MakeGenericType(dataType)
			                                       .GetConstructors()
			                                       .First();
			return (EntityDeletedEvent) ctor.Invoke(new object[] {data});
		}
	}

	internal class EntityDeletedEvent<T> : EntityDeletedEvent
		where T : IJsonCacheable
	{
		public T Data { get; }

		public EntityDeletedEvent(T data)
		{
			Data = data;
		}
	}
}
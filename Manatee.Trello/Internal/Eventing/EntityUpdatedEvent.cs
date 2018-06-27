using System;
using System.Linq;
using Manatee.Trello.Json;

namespace Manatee.Trello.Internal.Eventing
{
	internal class EntityUpdatedEvent
	{
		public static EntityUpdatedEvent Create(Type dataType, IJsonCacheable data, string property)
		{
			var ctor = typeof(EntityUpdatedEvent<>).MakeGenericType(dataType)
			                                       .GetConstructors()
			                                       .First();
			return (EntityUpdatedEvent) ctor.Invoke(new object[] {data, property});
		}
	}

	internal class EntityUpdatedEvent<T> : EntityUpdatedEvent
		where T : IJsonCacheable
	{
		public T Data { get; }
		public string Property { get; }

		public EntityUpdatedEvent(T data, string property)
		{
			Data = data;
			Property = property;
		}
	}
}

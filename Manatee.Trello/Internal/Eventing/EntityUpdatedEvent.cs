using System;
using System.Collections.Generic;
using System.Linq;
using Manatee.Trello.Json;

namespace Manatee.Trello.Internal.Eventing
{
	internal class EntityUpdatedEvent
	{
		public static EntityUpdatedEvent Create(Type dataType, IJsonCacheable data, IEnumerable<string> properties)
		{
			var ctor = typeof(EntityUpdatedEvent<>).MakeGenericType(dataType)
			                                       .GetConstructors()
			                                       .First();
			return (EntityUpdatedEvent) ctor.Invoke(new object[] {data, properties});
		}
	}

	internal class EntityUpdatedEvent<T> : EntityUpdatedEvent
		where T : IJsonCacheable
	{
		public T Data { get; }
		public List<string> Properties { get; }

		public EntityUpdatedEvent(T data, IEnumerable<string> properties)
		{
			Data = data;
			Properties = properties.ToList();
		}
	}
}

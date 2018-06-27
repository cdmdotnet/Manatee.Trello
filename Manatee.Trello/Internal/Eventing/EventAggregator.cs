using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Manatee.Trello.Internal.Eventing
{
	internal static class EventAggregator
	{
		private class Handler
		{
			private readonly WeakReference _reference;
			private readonly Dictionary<Type, MethodInfo> _supportedHandlers = new Dictionary<Type, MethodInfo>();

			public Handler(object handler)
			{
				_reference = new WeakReference(handler);

				var interfaces = handler.GetType().GetInterfaces()
										.Where(x => typeof(IHandle).IsAssignableFrom(x) && x.IsGenericType());

				foreach (var @interface in interfaces)
				{
					var type = @interface.GetGenericArguments().First();
					var method = @interface.GetMethod("Handle", type);

					if (method != null)
						_supportedHandlers[type] = method;
				}
			}

			public bool Matches(object instance)
			{
				return _reference.Target == instance;
			}

			public bool Handle(Type messageType, object message)
			{
				var target = _reference.Target;
				if (target == null) return false;

				foreach (var pair in _supportedHandlers)
				{
					if (pair.Key.IsAssignableFrom(messageType))
						pair.Value.Invoke(target, new[] { message });
				}

				return true;
			}
		}

		private static readonly List<Handler> Handlers = new List<Handler>();

		public static void Subscribe(IHandle subscriber)
		{
			if (subscriber == null) throw new ArgumentNullException(nameof(subscriber));

			lock (Handlers)
			{
				if (Handlers.Any(x => x.Matches(subscriber))) return;

				Handlers.Add(new Handler(subscriber));
			}
		}

		public static void Unsubscribe(IHandle subscriber)
		{
			if (subscriber == null) throw new ArgumentNullException(nameof(subscriber));

			lock (Handlers)
			{
				var found = Handlers.FirstOrDefault(x => x.Matches(subscriber));

				if (found != null)
					Handlers.Remove(found);
			}
		}

		public static void Publish(object message)
		{
			if (message == null) throw new ArgumentNullException(nameof(message));

			Handler[] toNotify;
			lock (Handlers)
			{
				toNotify = Handlers.ToArray();
			}

	        var messageType = message.GetType();

	        var dead = toNotify.Where(handler => !handler.Handle(messageType, message)).ToList();

	        if (!dead.Any()) return;

	        lock (Handlers)
	        {
		        dead.Apply(x => Handlers.Remove(x));
	        }
		}
	}
}

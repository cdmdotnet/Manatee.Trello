using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Manatee.Trello.Internal
{
	internal static class EventAggregator
	{
		private class Handler
		{
			private readonly WeakReference _reference;
			private readonly Dictionary<Type, MethodInfo> _supportedHandlers = new Dictionary<Type, MethodInfo>();

			public bool IsDead => _reference.Target == null;

			public Handler(object handler)
			{
				_reference = new WeakReference(handler);

				var interfaces = handler.GetType()._GetInterfaces()
										.Where(x => typeof(IHandle)._IsAssignableFrom(x) && x._IsGenericType());

				foreach (var @interface in interfaces)
				{
					var type = @interface._GetGenericArguments().First();
					var method = @interface._GetMethod("Handle", type);

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
					if (pair.Key._IsAssignableFrom(messageType))
						pair.Value.Invoke(target, new[] { message });
				}

				return true;
			}

			public bool Handles(Type messageType)
			{
				return _supportedHandlers.Any(pair => pair.Key._IsAssignableFrom(messageType));
			}
		}

		private static readonly List<Handler> Handlers = new List<Handler>();

		public static bool HandlerExistsFor(Type messageType)
		{
			return Handlers.Any(handler => handler.Handles(messageType) & !handler.IsDead);
		}

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

		public static void Publish(object message, Action<System.Action> marshal = null)
		{
			if (message == null) throw new ArgumentNullException(nameof(message));

			marshal = marshal ?? (action => action());

			Handler[] toNotify;
			lock (Handlers)
			{
				toNotify = Handlers.ToArray();
			}

			marshal(() =>
				        {
					        var messageType = message.GetType();

					        var dead = toNotify.Where(handler => !handler.Handle(messageType, message)).ToList();

					        if (!dead.Any()) return;

					        lock (Handlers)
					        {
						        dead.Apply(x => Handlers.Remove(x));
					        }
				        });
		}

		private static bool _IsAssignableFrom(this Type type, Type otherType)
		{
#if NET45
			return type.IsAssignableFrom(otherType);
#else
			return type.GetTypeInfo().IsAssignableFrom(otherType.GetTypeInfo());
#endif
		}

		private static IEnumerable<Type> _GetInterfaces(this Type type)
		{
#if NET45
			return type.GetInterfaces();
#else
			return type.GetTypeInfo().ImplementedInterfaces;
#endif
		}

		private static bool _IsGenericType(this Type type)
		{
#if NET45
			return type.IsGenericType;
#else
			return type.GetTypeInfo().IsGenericType;
#endif
		}

		private static IEnumerable<Type> _GetGenericArguments(this Type type)
		{
#if NET45
			return type.GetGenericArguments();
#else
			return type.GetTypeInfo().GenericTypeArguments;
#endif
		}

		private static MethodInfo _GetMethod(this Type type, string methodName, params Type[] typeParams)
		{
#if NET45
			return type.GetMethod(methodName, typeParams);
#else
			return type.GetTypeInfo().DeclaredMethods
			           .FirstOrDefault(m => m.Name == methodName &&
			                                m.GetParameters()
			                                 .Select(p => p.ParameterType)
			                                 .SequenceEqual(typeParams));
#endif
		}
	}
}

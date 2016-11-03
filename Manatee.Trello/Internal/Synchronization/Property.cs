using System;

namespace Manatee.Trello.Internal.Synchronization
{
	internal class Property<T>
	{
		public Func<T, TrelloAuthorization, object> Get { get; private set; }
		public Action<T, object> Set { get; private set; }

		protected Property(Func<T, TrelloAuthorization, object> get, Action<T, object> set)
		{
			Get = get;
			Set = set;
		}
	}

	internal class Property<TJson, T> : Property<TJson>
	{
		public Property(Func<TJson, TrelloAuthorization, T> get, Action<TJson, T> set)
			: base((j, a) => get(j, a), (j, o) => set(j, (T) o)) {}
	}
}
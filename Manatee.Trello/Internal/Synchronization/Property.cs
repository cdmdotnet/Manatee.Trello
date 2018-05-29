using System;

namespace Manatee.Trello.Internal.Synchronization
{
	internal class Property<T>
	{
		public Func<T, TrelloAuthorization, object> Get { get; }
		public Action<T, object> Set { get; }
		public bool IsHidden { get; }

		protected Property(Func<T, TrelloAuthorization, object> get, Action<T, object> set, bool isHidden = false)
		{
			Get = get;
			Set = set;
			IsHidden = isHidden;
		}
	}

	internal class Property<TJson, T> : Property<TJson>
	{
		public Property(Func<TJson, TrelloAuthorization, T> get, Action<TJson, T> set, bool isHidden = false)
			: base((j, a) => get(j, a), (j, o) => set(j, (T) o), isHidden) {}
	}
}
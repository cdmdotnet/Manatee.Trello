using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Manatee.Trello.Implementation;

namespace Manatee.Trello.Contracts
{
	internal class CachingTrelloRest : ITrelloRest
	{
		private ITrelloRest _inner;
		private readonly List<ExpiringObject> _cache;
		private readonly ActionProvider _actionProvider;
		private readonly NotificationProvider _notificationProvider;

		public IRestClientProvider RestClientProvider
		{
			get { return _inner.RestClientProvider; }
			set { _inner.RestClientProvider = value; }
		}
		public IRestRequestProvider RequestProvider { get { return RestClientProvider.RequestProvider; } }

		public CachingTrelloRest(ITrelloRest inner, ActionProvider actionProvider, NotificationProvider notificationProvider)
		{
			if (inner == null)
				throw new ArgumentNullException("inner");
			if (actionProvider == null)
				throw new ArgumentNullException("actionProvider");
			if (notificationProvider == null)
				throw new ArgumentNullException("notificationProvider");
			_inner = inner;
			_actionProvider = actionProvider;
			_notificationProvider = notificationProvider;
			_cache = new List<ExpiringObject>();
		}
		~CachingTrelloRest()
		{
			_cache.ForEach(i => i.Svc = null);
		}

		public T Get<T>(IRestRequest<T> request) where T : ExpiringObject, new()
		{
			T item = _cache.OfType<T>().FirstOrDefault(i => i.Equals(request.Template));
			if (item != null) return item;
			if (typeof(T).IsAssignableFrom(typeof(Action)))
			{
				return Execute(() => GetAction<T>(_inner.Get(request)));
			}
			if (typeof(T).IsAssignableFrom(typeof(Notification)))
			{
				return Execute(() => GetNotifcation<T>(_inner.Get(request)));
			}
			return Execute(() => _inner.Get(request));
		}
		public IEnumerable<T> Get<T>(IRestCollectionRequest<T> request) where T : ExpiringObject, new()
		{
			var items = _inner.Get(request);
			if (items != null)
			{
				foreach (var item in items)
				{
					item.Svc = this;
					T newItem;
					if (typeof(T).IsAssignableFrom(typeof(Action)))
					{
						newItem = Execute(() => GetAction<T>(item));
					}
					else if (typeof(T).IsAssignableFrom(typeof(Notification)))
					{
						newItem = Execute(() => GetNotifcation<T>(item));
					}
					else
					{
						newItem = item;
					}
					if (!_cache.OfType<T>().Contains(newItem))
						_cache.Add(newItem);
					yield return newItem;
				}
			}
		}
		public T Put<T>(IRestRequest<T> request) where T : ExpiringObject, new()
		{
			return Execute(() => _inner.Put(request));
		}
		public T Post<T>(IRestRequest<T> request) where T : ExpiringObject, new()
		{
			return Execute(() => _inner.Post(request));
		}
		public T Delete<T>(IRestRequest<T> request) where T : ExpiringObject, new()
		{
			var retVal = _inner.Delete(request);
			_cache.Remove(retVal);
			return retVal;
		}

		private T Execute<T>(Func<T> func)
			where T : ExpiringObject
		{
			var retVal = func();
			if (retVal != null)
			{
				retVal.Svc = this;
				if (!_cache.Contains(retVal))
					_cache.Add(retVal);
			}
			return retVal;
		}
		private T GetAction<T>(object action)
		{
			if (action == null) return default(T);
			object obj = _actionProvider.Parse((Action)action);
			return (T)obj;
		}
		private T GetNotifcation<T>(object notification)
		{
			if (notification == null) return default(T);
			object obj = _notificationProvider.Parse((Notification)notification);
			return (T)obj;
		}

	}
}

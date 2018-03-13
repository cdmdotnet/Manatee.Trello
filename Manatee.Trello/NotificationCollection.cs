﻿using System;
using System.Collections.Generic;
using System.Linq;
using Manatee.Trello.Internal;
using Manatee.Trello.Internal.Caching;
using Manatee.Trello.Internal.DataAccess;
using Manatee.Trello.Json;

namespace Manatee.Trello
{
	/// <summary>
	/// A read-only collection of actions.
	/// </summary>
	public interface IReadOnlyNotificationCollection : IReadOnlyCollection<INotification>
	{
		/// <summary>
		/// Adds a filter to the collection.
		/// </summary>
		/// <param name="filter">The filter type.</param>
		void Filter(NotificationType filter);
		/// <summary>
		/// Adds a set of filters to the collection.
		/// </summary>
		/// <param name="filters">A collection of filters.</param>
		void Filter(IEnumerable<NotificationType> filters);
	}

	/// <summary>
	/// A read-only collection of actions.
	/// </summary>
	public class ReadOnlyNotificationCollection : ReadOnlyCollection<INotification>, IReadOnlyNotificationCollection
	{
		private Dictionary<string, object> _additionalParameters;

		internal ReadOnlyNotificationCollection(Func<string> getOwnerId, TrelloAuthorization auth)
			: base(getOwnerId, auth) {}
		internal ReadOnlyNotificationCollection(ReadOnlyNotificationCollection source, TrelloAuthorization auth)
			: this(() => source.OwnerId, auth)
		{
			if (source._additionalParameters != null)
				_additionalParameters = new Dictionary<string, object>(source._additionalParameters);
		}

		/// <summary>
		/// Adds a filter to the collection.
		/// </summary>
		/// <param name="filter">The filter type.</param>
		public void Filter(NotificationType filter)
		{
			var filters = filter.GetFlags().Cast<NotificationType>();
			Filter(filters);
		}

		/// <summary>
		/// Adds a set of filters to the collection.
		/// </summary>
		/// <param name="filters">A collection of filters.</param>
		public void Filter(IEnumerable<NotificationType> filters)
		{
			if (_additionalParameters == null)
				_additionalParameters = new Dictionary<string, object> {{"filter", string.Empty}};
			var filter = (string)_additionalParameters["filter"];
			if (!filter.IsNullOrWhiteSpace())
				filter += ",";
			filter += filters.Select(a => a.GetDescription()).Join(",");
			_additionalParameters["filter"] = filter;
		}

		/// <summary>
		/// Implement to provide data to the collection.
		/// </summary>
		protected override void Update()
		{
			IncorporateLimit(_additionalParameters);

			var endpoint = EndpointFactory.Build(EntityRequestType.Member_Read_Notifications, new Dictionary<string, object> {{"_id", OwnerId}});
			var newData = JsonRepository.Execute<List<IJsonNotification>>(Auth, endpoint, _additionalParameters);

			Items.Clear();
			Items.AddRange(newData.Select(jn =>
				{
					var notification = jn.GetFromCache<Notification>(Auth);
					notification.Json = jn;
					return notification;
				}));
		}
	}
}
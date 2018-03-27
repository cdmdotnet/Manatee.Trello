using System;
using System.Collections.Generic;
using System.Linq;
using Manatee.Trello.Internal;
using Manatee.Trello.Internal.Caching;
using Manatee.Trello.Internal.DataAccess;
using Manatee.Trello.Json;

namespace Manatee.Trello
{
	/// <summary>
	/// A read-only collection of notifications.
	/// </summary>
	public class ReadOnlyNotificationCollection : ReadOnlyCollection<Notification>
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
		/// Implement to provide data to the collection.
		/// </summary>
		protected sealed override void Update()
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

		internal void AddFilter(IEnumerable<NotificationType> actionTypes)
		{
			if (_additionalParameters == null)
				_additionalParameters = new Dictionary<string, object> {{"filter", string.Empty}};
			var filter = (string)_additionalParameters["filter"];
			if (!filter.IsNullOrWhiteSpace())
				filter += ",";
			filter += actionTypes.Select(a => a.GetDescription()).Join(",");
			_additionalParameters["filter"] = filter;
		}
	}
}
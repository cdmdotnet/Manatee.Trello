using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Manatee.Trello.Internal;
using Manatee.Trello.Internal.Caching;
using Manatee.Trello.Internal.DataAccess;
using Manatee.Trello.Internal.Synchronization;
using Manatee.Trello.Json;

namespace Manatee.Trello
{
	/// <summary>
	/// A read-only collection of actions.
	/// </summary>
	public class ReadOnlyNotificationCollection : ReadOnlyCollection<INotification>, IReadOnlyNotificationCollection
	{
		internal ReadOnlyNotificationCollection(Func<string> getOwnerId, TrelloAuthorization auth)
			: base(getOwnerId, auth)
		{
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
			var filter = (string) AdditionalParameters["filter"];
			if (!filter.IsNullOrWhiteSpace())
				filter += ",";
			filter += filters.Select(a => a.GetDescription()).Join(",");
			AdditionalParameters["filter"] = filter;
		}

		internal sealed override async Task PerformRefresh(bool force, CancellationToken ct)
		{
			IncorporateLimit();

			var allParameters = AdditionalParameters.Concat(NotificationContext.CurrentParameters)
			                                        .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
			var endpoint = EndpointFactory.Build(EntityRequestType.Member_Read_Notifications, new Dictionary<string, object> {{"_id", OwnerId}});
			var newData = await JsonRepository.Execute<List<IJsonNotification>>(Auth, endpoint, ct, allParameters);

			Items.Clear();
			Items.AddRange(newData.Select(jn =>
				{
					var notification = jn.GetFromCache<Notification, IJsonNotification>(Auth);
					notification.Json = jn;
					return notification;
				}));
		}
	}
}
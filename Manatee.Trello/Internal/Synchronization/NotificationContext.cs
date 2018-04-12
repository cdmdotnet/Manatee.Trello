using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Manatee.Trello.Internal.Caching;
using Manatee.Trello.Internal.DataAccess;
using Manatee.Trello.Json;

namespace Manatee.Trello.Internal.Synchronization
{
	internal class NotificationContext : SynchronizationContext<IJsonNotification>
	{
		private static readonly Dictionary<string, object> Parameters;
		private static readonly Notification.Fields MemberFields;

		public NotificationDataContext NotificationDataContext { get; }

		static NotificationContext()
		{
			Parameters = new Dictionary<string, object>();
			MemberFields = Notification.Fields.Creator |
			               Notification.Fields.Data |
			               Notification.Fields.IsUnread |
			               Notification.Fields.Type |
						   Notification.Fields.Date;
			Properties = new Dictionary<string, Property<IJsonNotification>>
				{
					{
						nameof(Notification.Creator),
						new Property<IJsonNotification, Member>((d, a) => d.MemberCreator.GetFromCache<Member>(a),
						                                        (d, o) =>
							                                        {
								                                        if (o != null) d.MemberCreator = o.Json;
							                                        })
					},
					{
						nameof(Notification.Date),
						new Property<IJsonNotification, DateTime?>((d, a) => d.Date, (d, o) => d.Date = o)
					},
					{
						nameof(Notification.Id),
						new Property<IJsonNotification, string>((d, a) => d.Id, (d, o) => d.Id = o)
					},
					{
						nameof(Notification.IsUnread),
						new Property<IJsonNotification, bool?>((d, a) => d.Unread, (d, o) => d.Unread = o)
					},
					{
						nameof(Notification.Type),
						new Property<IJsonNotification, NotificationType?>((d, a) => d.Type, (d, o) => d.Type = o)
					},
				};
		}
		public NotificationContext(string id, TrelloAuthorization auth)
			: base(auth)
		{
			Data.Id = id;
			NotificationDataContext = new NotificationDataContext(Auth);
			NotificationDataContext.SynchronizeRequested += ct => Synchronize(ct);
			Data.Data = NotificationDataContext.Data;
		}

		public static void UpdateParameters()
		{
			lock (Parameters)
			{
				Parameters.Clear();
				var flags = Enum.GetValues(typeof(Notification.Fields)).Cast<Notification.Fields>().ToList();
				var availableFields = (Notification.Fields) flags.Cast<int>().Sum();

				var memberFields = availableFields & MemberFields & Notification.DownloadedFields;
				Parameters["fields"] = memberFields.GetDescription();
			}
		}

		public override async Task Expire(CancellationToken ct)
		{
			await base.Expire(ct);
		}

		protected override async Task<IJsonNotification> GetData(CancellationToken ct)
		{
			Dictionary<string, object> parameters;
			lock (Parameters)
			{
				parameters = new Dictionary<string, object>(Parameters);
			}
			var endpoint = EndpointFactory.Build(EntityRequestType.Notification_Read_Refresh,
			                                     new Dictionary<string, object> {{"_id", Data.Id}});
			var newData = await JsonRepository.Execute<IJsonNotification>(Auth, endpoint, ct, parameters);

			return newData;
		}
		protected override async Task SubmitData(IJsonNotification json, CancellationToken ct)
		{
			var endpoint = EndpointFactory.Build(EntityRequestType.Notification_Write_Update,
			                                     new Dictionary<string, object> {{"_id", Data.Id}});
			var newData = await JsonRepository.Execute(Auth, endpoint, json, ct);
			Merge(newData);
		}
		protected override IEnumerable<string> MergeDependencies(IJsonNotification json)
		{
			return NotificationDataContext.Merge(json.Data);
		}
	}
}
using System.Collections.Generic;
using Manatee.Trello.Json;

namespace Manatee.Trello.Internal.Synchronization
{
	internal class NotificationDataContext : LinkedSynchronizationContext<IJsonNotificationData>
	{
		static NotificationDataContext()
		{
			Properties = new Dictionary<string, Property<IJsonNotificationData>>
				{
					{
						nameof(NotificationData.Attachment),
						new Property<IJsonNotificationData, Attachment>((d, a) => d.Attachment != null
							                                                          ? new Attachment(d.Attachment, d.Card.Id, TrelloAuthorization.Null)
							                                                          : null,
						                                                (d, o) =>
							                                                {
								                                                if (o != null) d.Attachment = o.Json;
							                                                })
					},
					{
						nameof(NotificationData.Board),
						new Property<IJsonNotificationData, Board>((d, a) => d.Board != null
							                                                     ? new Board(d.Board, TrelloAuthorization.Null)
							                                                     : null,
						                                           (d, o) =>
							                                           {
								                                           if (o != null) d.Board = o.Json;
							                                           })
					},
					{
						nameof(NotificationData.BoardSource),
						new Property<IJsonNotificationData, Board>((d, a) => d.BoardSource != null
							                                                     ? new Board(d.BoardSource, TrelloAuthorization.Null)
							                                                     : null,
						                                           (d, o) =>
							                                           {
								                                           if (o != null) d.BoardSource = o.Json;
							                                           })
					},
					{
						nameof(NotificationData.BoardTarget),
						new Property<IJsonNotificationData, Board>((d, a) => d.BoardTarget != null
							                                                     ? new Board(d.BoardTarget, TrelloAuthorization.Null)
							                                                     : null,
						                                           (d, o) =>
							                                           {
								                                           if (o != null) d.BoardTarget = o.Json;
							                                           })
					},
					{
						nameof(NotificationData.Card),
						new Property<IJsonNotificationData, Card>((d, a) => d.Card != null
							                                                    ? new Card(d.Card, TrelloAuthorization.Null)
							                                                    : null,
						                                          (d, o) =>
							                                          {
								                                          if (o != null) d.Card = o.Json;
							                                          })
					},
					{
						nameof(NotificationData.CardSource),
						new Property<IJsonNotificationData, Card>((d, a) => d.CardSource != null
							                                                    ? new Card(d.CardSource, TrelloAuthorization.Null)
							                                                    : null,
						                                          (d, o) =>
							                                          {
								                                          if (o != null) d.CardSource = o.Json;
							                                          })
					},
					{
						nameof(NotificationData.CheckItem),
						new Property<IJsonNotificationData, CheckItem>((d, a) => d.CheckItem != null
							                                                         ? new CheckItem(d.CheckItem, d.CheckList.Id, TrelloAuthorization.Null)
							                                                         : null,
						                                               (d, o) =>
							                                               {
								                                               if (o != null) d.CheckItem = o.Json;
							                                               })
					},
					{
						nameof(NotificationData.CheckList),
						new Property<IJsonNotificationData, CheckList>((d, a) => d.CheckList != null
							                                                         ? new CheckList(d.CheckList, TrelloAuthorization.Null)
							                                                         : null,
						                                               (d, o) =>
							                                               {
								                                               if (o != null) d.CheckList = o.Json;
							                                               })
					},
					{
						nameof(NotificationData.List),
						new Property<IJsonNotificationData, List>((d, a) => d.List != null
							                                                    ? new List(d.List, TrelloAuthorization.Null)
							                                                    : null,
						                                          (d, o) =>
							                                          {
								                                          if (o != null) d.List = o.Json;
							                                          })
					},
					{
						nameof(NotificationData.ListAfter),
						new Property<IJsonNotificationData, List>((d, a) => d.ListAfter != null
							                                                    ? new List(d.ListAfter, TrelloAuthorization.Null)
							                                                    : null,
						                                          (d, o) =>
							                                          {
								                                          if (o != null) d.ListAfter = o.Json;
							                                          })
					},
					{
						nameof(NotificationData.ListBefore),
						new Property<IJsonNotificationData, List>((d, a) => d.ListBefore != null
							                                                    ? new List(d.ListBefore, TrelloAuthorization.Null)
							                                                    : null,
						                                          (d, o) =>
							                                          {
								                                          if (o != null) d.ListBefore = o.Json;
							                                          })
					},
					{
						nameof(NotificationData.Member),
						new Property<IJsonNotificationData, Member>((d, a) => d.Member != null
							                                                      ? new Member(d.Member, TrelloAuthorization.Null)
							                                                      : null,
						                                            (d, o) =>
							                                            {
								                                            if (o != null) d.Member = o.Json;
							                                            })
					},
					{
						nameof(NotificationData.WasArchived),
						new Property<IJsonNotificationData, bool?>((d, a) => d.Old?.Closed,
						                                           (d, o) =>
							                                           {
								                                           if (d.Old != null && o != null) d.Old.Closed = o;
							                                           })
					},
					{
						nameof(NotificationData.OldDescription),
						new Property<IJsonNotificationData, string>((d, a) => d.Old?.Desc,
						                                            (d, o) =>
							                                            {
								                                            if (d.Old != null && o != null) d.Old.Desc = o;
							                                            })
					},
					{
						nameof(NotificationData.OldList),
						new Property<IJsonNotificationData, List>((d, a) => d.Old?.List != null
							                                                    ? new List(d.Old.List, TrelloAuthorization.Null)
							                                                    : null,
						                                          (d, o) =>
							                                          {
								                                          if (d.Old != null) d.Old.List = o.Json;
							                                          })
					},
					{
						nameof(NotificationData.OldPosition),
						new Property<IJsonNotificationData, Position>((d, a) => d.Old?.Pos,
						                                              (d, o) =>
							                                              {
								                                              if (d.Old != null) d.Old.Pos = o.Value;
							                                              })
					},
					{
						nameof(NotificationData.OldText),
						new Property<IJsonNotificationData, string>((d, a) => d.Old?.Text,
						                                            (d, o) =>
							                                            {
								                                            if (d.Old != null) d.Old.Text = o;
							                                            })
					},
					{
						nameof(NotificationData.Organization),
						new Property<IJsonNotificationData, Organization>((d, a) => d.Org != null
							                                                            ? new Organization(d.Org, TrelloAuthorization.Null)
							                                                            : null,
						                                                  (d, o) =>
							                                                  {
								                                                  if (o != null) d.Org = o.Json;
							                                                  })
					},
					{
						nameof(NotificationData.Text),
						new Property<IJsonNotificationData, string>((d, a) => d.Text, (d, o) => d.Text = o)
					},
				};
		}
		public NotificationDataContext(TrelloAuthorization auth)
			: base(auth) {}
	}
}
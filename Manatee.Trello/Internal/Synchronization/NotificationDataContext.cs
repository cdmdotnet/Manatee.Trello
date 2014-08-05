/***************************************************************************************

	Copyright 2014 Greg Dennis

	   Licensed under the Apache License, Version 2.0 (the "License");
	   you may not use this file except in compliance with the License.
	   You may obtain a copy of the License at

		 http://www.apache.org/licenses/LICENSE-2.0

	   Unless required by applicable law or agreed to in writing, software
	   distributed under the License is distributed on an "AS IS" BASIS,
	   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
	   See the License for the specific language governing permissions and
	   limitations under the License.
 
	File Name:		NotificationDataContext.cs
	Namespace:		Manatee.Trello.Internal.Synchronization
	Class Name:		NotificationDataContext
	Purpose:		Provides a data context for action data.

***************************************************************************************/
using System.Collections.Generic;
using Manatee.Trello.Internal.Caching;
using Manatee.Trello.Json;

namespace Manatee.Trello.Internal.Synchronization
{
	internal class NotificationDataContext : LinkedSynchronizationContext<IJsonNotificationData>
	{
		static NotificationDataContext()
		{
			_properties = new Dictionary<string, Property<IJsonNotificationData>>
				{
					{
						"Attachment", new Property<IJsonNotificationData, Attachment>(d => d.Attachment == null
							                                                             ? null
							                                                             : new Attachment(d.Attachment, d.Card.Id),
						                                                        (d, o) => { if (o != null) d.Attachment = o.Json; })
					},
					{
						"Board", new Property<IJsonNotificationData, Board>(d => d.Board == null
							                                                   ? null
							                                                   : new Board(d.Board),
						                                              (d, o) => { if (o != null) d.Board = o.Json; })
					},
					{
						"BoardSource", new Property<IJsonNotificationData, Board>(d => d.BoardSource == null
							                                                         ? null
							                                                         : new Board(d.BoardSource),
						                                                    (d, o) => { if (o != null) d.BoardSource = o.Json; })
					},
					{
						"BoardTarget", new Property<IJsonNotificationData, Board>(d => d.BoardTarget == null
							                                                         ? null
							                                                         : new Board(d.BoardTarget),
						                                                    (d, o) => { if (o != null) d.BoardTarget = o.Json; })
					},
					{
						"Card", new Property<IJsonNotificationData, Card>(d => d.Card == null
							                                                 ? null
							                                                 : new Card(d.Card),
						                                            (d, o) => { if (o != null) d.Card = o.Json; })
					},
					{
						"CardSource", new Property<IJsonNotificationData, Card>(d => d.CardSource == null
							                                                       ? null
							                                                       : new Card(d.CardSource),
						                                                  (d, o) => { if (o != null) d.CardSource = o.Json; })
					},
					{
						"CheckItem", new Property<IJsonNotificationData, CheckItem>(d => d.CheckItem == null || d.CheckList == null
							                                                           ? null
							                                                           : new CheckItem(d.CheckItem, d.CheckList.Id),
						                                                      (d, o) => { if (o != null) d.CheckItem = o.Json; })
					},
					{
						"CheckList", new Property<IJsonNotificationData, CheckList>(d => d.CheckList == null
							                                                           ? null
							                                                           : new CheckList(d.CheckList),
						                                                      (d, o) => { if (o != null) d.CheckList = o.Json; })
					},
					{
						"List", new Property<IJsonNotificationData, List>(d => d.List == null
							                                                 ? null
							                                                 : new List(d.List),
						                                            (d, o) => { if (o != null) d.List = o.Json; })
					},
					{
						"ListAfter", new Property<IJsonNotificationData, List>(d => d.ListAfter == null
							                                                      ? null
							                                                      : new List(d.ListAfter),
						                                                 (d, o) => { if (o != null) d.ListAfter = o.Json; })
					},
					{
						"ListBefore", new Property<IJsonNotificationData, List>(d => d.ListBefore == null
							                                                       ? null
							                                                       : new List(d.ListBefore),
						                                                  (d, o) => { if (o != null) d.ListBefore = o.Json; })
					},
					{
						"Member", new Property<IJsonNotificationData, Member>(d => d.Member == null
							                                                     ? null
							                                                     : d.Member.GetFromCache<Member>(),
						                                                (d, o) => { if (o != null) d.Member = o.Json; })
					},
					{"WasArchived", new Property<IJsonNotificationData, bool?>(d => d.Old == null ? null : d.Old.Closed, (d, o) => { if (d.Old != null && o != null) d.Old.Closed = o; })},
					{"OldDesc", new Property<IJsonNotificationData, string>(d => d.Old == null ? null : d.Old.Desc, (d, o) => { if (d.Old != null && o != null) d.Old.Desc = o; })},
					{
						"OldList", new Property<IJsonNotificationData, List>(d => d.Old == null || d.Old.List == null
							                                                    ? null
							                                                    : new List(d.Old.List),
						                                               (d, o) => { if (d.Old != null) d.Old.List = o.Json; })
					},
					{"OldPos", new Property<IJsonNotificationData, Position>(d => d.Old == null ? null : d.Old.Pos, (d, o) => { if (d.Old != null) d.Old.Pos = o.Value; })},
					{"OldText", new Property<IJsonNotificationData, string>(d => d.Old == null ? null : d.Old.Text, (d, o) => { if (d.Old != null) d.Old.Text = o; })},
					{
						"Organization", new Property<IJsonNotificationData, Organization>(d => d.Org == null
							                                                                 ? null
							                                                                 : new Organization(d.Org),
						                                                            (d, o) => { if (o != null) d.Org = o.Json; })
					},
					{"Text", new Property<IJsonNotificationData, string>(d => d.Text, (d, o) => d.Text = o)},
				};
		}
	}
}
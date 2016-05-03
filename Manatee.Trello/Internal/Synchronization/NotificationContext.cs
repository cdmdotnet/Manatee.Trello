/***************************************************************************************

	Copyright 2015 Greg Dennis

	   Licensed under the Apache License, Version 2.0 (the "License");
	   you may not use this file except in compliance with the License.
	   You may obtain a copy of the License at

		 http://www.apache.org/licenses/LICENSE-2.0

	   Unless required by applicable law or agreed to in writing, software
	   distributed under the License is distributed on an "AS IS" BASIS,
	   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
	   See the License for the specific language governing permissions and
	   limitations under the License.
 
	File Name:		NotificationContext.cs
	Namespace:		Manatee.Trello.Internal.Synchronization
	Class Name:		NotificationContext
	Purpose:		Provides a data context for notifications.

***************************************************************************************/

using System;
using System.Collections.Generic;
using Manatee.Trello.Internal.Caching;
using Manatee.Trello.Internal.DataAccess;
using Manatee.Trello.Json;

namespace Manatee.Trello.Internal.Synchronization
{
	internal class NotificationContext : SynchronizationContext<IJsonNotification>
	{
		public NotificationDataContext NotificationDataContext { get; }

		static NotificationContext()
		{
			_properties = new Dictionary<string, Property<IJsonNotification>>
				{
					{
						"Creator", new Property<IJsonNotification, Member>((d, a) => d.MemberCreator.GetFromCache<Member>(a),
						                                                   (d, o) => { if (o != null) d.MemberCreator = o.Json; })
					},
					{"Date", new Property<IJsonNotification, DateTime?>((d, a) => d.Date, (d, o) => d.Date = o)},
					{"Id", new Property<IJsonNotification, string>((d, a) => d.Id, (d, o) => d.Id = o)},
					{"IsUnread", new Property<IJsonNotification, bool?>((d, a) => d.Unread, (d, o) => d.Unread = o)},
					{"Type", new Property<IJsonNotification, NotificationType?>((d, a) => d.Type, (d, o) => d.Type = o)},
				};
		}
		public NotificationContext(string id, TrelloAuthorization auth)
			: base(auth)
		{
			Data.Id = id;
			NotificationDataContext = new NotificationDataContext(Auth);
			NotificationDataContext.SynchronizeRequested += () => Synchronize();
			Data.Data = NotificationDataContext.Data;
		}

		protected override IJsonNotification GetData()
		{
			var endpoint = EndpointFactory.Build(EntityRequestType.Notification_Read_Refresh, new Dictionary<string, object> {{"_id", Data.Id}});
			var newData = JsonRepository.Execute<IJsonNotification>(Auth, endpoint);

			return newData;
		}
		protected override void SubmitData(IJsonNotification json)
		{
			var endpoint = EndpointFactory.Build(EntityRequestType.Notification_Write_Update, new Dictionary<string, object> {{"_id", Data.Id}});
			var newData = JsonRepository.Execute(Auth, endpoint, json);
			Merge(newData);
		}
		protected override IEnumerable<string> MergeDependencies(IJsonNotification json)
		{
			return NotificationDataContext.Merge(json.Data);
		}
	}
}
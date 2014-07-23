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
 
	File Name:		ListContext.cs
	Namespace:		Manatee.Trello.Internal.Synchronization.Contexts
	Class Name:		ListContext
	Purpose:		Provides a data context for a list.

***************************************************************************************/

using System.Collections.Generic;
using Manatee.Trello.Internal.Caching;
using Manatee.Trello.Internal.DataAccess;
using Manatee.Trello.Json;

namespace Manatee.Trello.Internal.Synchronization
{
	internal class ListContext : SynchronizationContext<IJsonList>
	{
		static ListContext()
		{
			_properties = new Dictionary<string, Property<IJsonList>>
				{
					{
						"Board", new Property<IJsonList, Board>(d => d.Board == null ? null : d.Board.GetFromCache<Board>(),
						                                        (d, o) => { if (o != null) d.Board = o.Json; })
					},
					{"Id", new Property<IJsonList, string>(d => d.Id, (d, o) => d.Id = o)},
					{"IsArchived", new Property<IJsonList, bool?>(d => d.Closed, (d, o) => d.Closed = o)},
					{"IsSubscribed", new Property<IJsonList, bool?>(d => d.Subscribed, (d, o) => d.Subscribed = o)},
					{"Name", new Property<IJsonList, string>(d => d.Name, (d, o) => d.Name = o)},
					{
						"Position", new Property<IJsonList, Position>(d => d.Pos.HasValue ? new Position(d.Pos.Value) : null,
						                                              (d, o) => { if (o != null) d.Pos = o.Value; })
					},
				};
		}
		public ListContext(string id)
		{
			Data.Id = id;
		}

		protected override IJsonList GetData()
		{
			var endpoint = EndpointFactory.Build(EntityRequestType.List_Read_Refresh, new Dictionary<string, object> {{"_id", Data.Id}});
			var newData = JsonRepository.Execute<IJsonList>(TrelloAuthorization.Default, endpoint);
			return newData;
		}
		protected override void SubmitData()
		{
			var endpoint = EndpointFactory.Build(EntityRequestType.List_Write_Update, new Dictionary<string, object> {{"_id", Data.Id}});
			JsonRepository.Execute(TrelloAuthorization.Default, endpoint, Data);
		}
	}
}
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
 
	File Name:		CardContext.cs
	Namespace:		Manatee.Trello.Internal.Synchronization.Contexts
	Class Name:		CardContext
	Purpose:		Provides a data context for a card.

***************************************************************************************/

using System;
using System.Collections.Generic;
using Manatee.Trello.Enumerations;
using Manatee.Trello.Internal.DataAccess;
using Manatee.Trello.Internal.Genesis;
using Manatee.Trello.Json;

namespace Manatee.Trello.Internal.Synchronization
{
	internal class CardContext : SynchronizationContext<IJsonCard>
	{
		public BadgesContext BadgesContext { get; private set; }

		static CardContext()
		{
			_properties = new Dictionary<string, Property<IJsonCard>>
				{
					{
						"Board", new Property<IJsonCard>(d => TrelloConfiguration.Cache.Find<Board>(b => b.Id == d.Board.Id) ?? new Board(d.Board),
						                                 (d, o) => d.Board = o != null ? ((Board) o).Json : null)
					},
					{"Description", new Property<IJsonCard>(d => d.Desc, (d, o) => d.Desc = (string) o)},
					{"DueDate", new Property<IJsonCard>(d => d.Due, (d, o) => d.Due = (DateTime?) o)},
					{"IsArchived", new Property<IJsonCard>(d => d.Closed, (d, o) => d.Closed = (bool?) o)},
					{"IsSubscribed", new Property<IJsonCard>(d => d.Subscribed, (d, o) => d.Subscribed = (bool?) o)},
					{"Labels", new Property<IJsonCard>(d => d.Labels, (d, o) => d.Labels = (List<IJsonLabel>) o)},
					{"LastActivity", new Property<IJsonCard>(d => d.DateLastActivity, (d, o) => d.DateLastActivity = (DateTime?) o)},
					{
						"List", new Property<IJsonCard>(d => TrelloConfiguration.Cache.Find<List>(l => l.Id == d.List.Id) ?? new List(d.List),
						                                (d, o) => d.List = ((List) o).Json)
					},
					{"Name", new Property<IJsonCard>(d => d.Name, (d, o) => d.Name = (string) o)},
					{"Position", new Property<IJsonCard>(d => d.Pos.HasValue ? new Position(d.Pos.Value) : null, (d, o) => d.Pos = ((Position) o).Value)},
					{"ShortId", new Property<IJsonCard>(d => d.IdShort, (d, o) => d.IdShort = (int?) o)},
					{"ShortUrl", new Property<IJsonCard>(d => d.ShortUrl, (d, o) => d.ShortUrl = (string) o)},
					{"Url", new Property<IJsonCard>(d => d.Url, (d, o) => d.Url = (string) o)},
				};
		}
		public CardContext(string id)
		{
			Data.Id = id;
			BadgesContext = new BadgesContext();
			BadgesContext.SynchronizeRequested += () => Synchronize();
			Data.Badges = BadgesContext.Data;
		}

		public void Delete()
		{
			var endpoint = EndpointFactory.Build(EntityRequestType.Card_Write_Delete, new Dictionary<string, object> {{"_id", Data.Id}});
			JsonRepository.Execute(TrelloAuthorization.Default, endpoint);
		}

		protected override IJsonCard GetData()
		{
			var endpoint = EndpointFactory.Build(EntityRequestType.Card_Read_Refresh, new Dictionary<string, object> {{"_id", Data.Id}});
			var newData = JsonRepository.Execute<IJsonCard>(TrelloAuthorization.Default, endpoint);

			BadgesContext.Merge(newData.Badges);

			return newData;
		}
		protected override void SubmitData()
		{
			var endpoint = EndpointFactory.Build(EntityRequestType.Card_Write_Update, new Dictionary<string, object> {{"_id", Data.Id}});
			JsonRepository.Execute(TrelloAuthorization.Default, endpoint, Data);
		}
	}
}
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
using Manatee.Trello.Exceptions;
using Manatee.Trello.Internal.Caching;
using Manatee.Trello.Internal.DataAccess;
using Manatee.Trello.Json;

namespace Manatee.Trello.Internal.Synchronization
{
	internal class CardContext : SynchronizationContext<IJsonCard>
	{
		private bool _deleted;
		private bool _successfulDownload;

		public BadgesContext BadgesContext { get; private set; }
		public override bool IsDataComplete { get { return !Data.Name.IsNullOrWhiteSpace(); } }

		static CardContext()
		{
			_properties = new Dictionary<string, Property<IJsonCard>>
				{
					{
						"Board", new Property<IJsonCard, Board>(d => d.Board == null ? null : d.Board.GetFromCache<Board>(),
						                                        (d, o) => d.Board = o == null ? null : o.Json)
					},
					{"Description", new Property<IJsonCard, string>(d => d.Desc, (d, o) => d.Desc = o)},
					{"DueDate", new Property<IJsonCard, DateTime?>(d => d.Due, (d, o) => d.Due = o)},
					{"Id", new Property<IJsonCard, string>(d => d.Id, (d, o) => d.Id = o)},
					{"IsArchived", new Property<IJsonCard, bool?>(d => d.Closed, (d, o) => d.Closed = o)},
					{"IsSubscribed", new Property<IJsonCard, bool?>(d => d.Subscribed, (d, o) => d.Subscribed = o)},
					{"Labels", new Property<IJsonCard, List<IJsonLabel>>(d => d.Labels, (d, o) => d.Labels = o)},
					{"LastActivity", new Property<IJsonCard, DateTime?>(d => d.DateLastActivity, (d, o) => d.DateLastActivity = o)},
					{
						"List", new Property<IJsonCard, List>(d => d.List == null ? null : d.List.GetFromCache<List>(),
						                                      (d, o) => d.List = o == null ? null : o.Json)
					},
					{"Name", new Property<IJsonCard, string>(d => d.Name, (d, o) => d.Name = o)},
					{
						"Position", new Property<IJsonCard, Position>(d => Position.GetPosition(d.Pos),
						                                              (d, o) => d.Pos = Position.GetJson(o))
					},
					{"ShortId", new Property<IJsonCard, int?>(d => d.IdShort, (d, o) => d.IdShort = o)},
					{"ShortUrl", new Property<IJsonCard, string>(d => d.ShortUrl, (d, o) => d.ShortUrl = o)},
					{"Url", new Property<IJsonCard, string>(d => d.Url, (d, o) => d.Url = o)},
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
			if (_deleted) return;

			var endpoint = EndpointFactory.Build(EntityRequestType.Card_Write_Delete, new Dictionary<string, object> {{"_id", Data.Id}});
			JsonRepository.Execute(TrelloAuthorization.Default, endpoint);

			_deleted = true;
		}

		protected override IJsonCard GetData()
		{
			try
			{
				var endpoint = EndpointFactory.Build(EntityRequestType.Card_Read_Refresh, new Dictionary<string, object> {{"_id", Data.Id}});
				var newData = JsonRepository.Execute<IJsonCard>(TrelloAuthorization.Default, endpoint);
				_successfulDownload = true;
				
				return newData;
			}
			catch (TrelloInteractionException e)
			{
				if (!_successfulDownload || e.IsNotFoundError())
					throw;
				_deleted = true;
				return Data;
			}
		}
		protected override void SubmitData()
		{
			var endpoint = EndpointFactory.Build(EntityRequestType.Card_Write_Update, new Dictionary<string, object> {{"_id", Data.Id}});
			JsonRepository.Execute(TrelloAuthorization.Default, endpoint, Data);
		}
		protected override IEnumerable<string> MergeDependencies(IJsonCard json)
		{
			return BadgesContext.Merge(json.Badges);
		}
		protected override bool CanUpdate()
		{
			return !_deleted;
		}
	}
}
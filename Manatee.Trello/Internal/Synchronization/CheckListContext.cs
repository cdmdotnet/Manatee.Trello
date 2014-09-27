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
 
	File Name:		CheckListContext.cs
	Namespace:		Manatee.Trello.Internal.Synchronization
	Class Name:		CheckListContext
	Purpose:		Provides a data context for a check list.

***************************************************************************************/

using System.Collections.Generic;
using Manatee.Trello.Exceptions;
using Manatee.Trello.Internal.Caching;
using Manatee.Trello.Internal.DataAccess;
using Manatee.Trello.Internal.Validation;
using Manatee.Trello.Json;

namespace Manatee.Trello.Internal.Synchronization
{
	internal class CheckListContext : SynchronizationContext<IJsonCheckList>
	{
		private bool _deleted;

		public virtual bool HasValidId { get { return IdRule.Instance.Validate(Data.Id, null) == null; } }

		static CheckListContext()
		{
			_properties = new Dictionary<string, Property<IJsonCheckList>>
				{
					{
						"Board", new Property<IJsonCheckList, Board>(d => d.Board.GetFromCache<Board>(),
						                                             (d, o) => d.Board = o != null ? (o).Json : null)
					},
					{
						"Card", new Property<IJsonCheckList, Card>(d => d.Card.GetFromCache<Card>(),
						                                           (d, o) => d.Card = o != null ? (o).Json : null)
					},
					{"CheckItems", new Property<IJsonCheckList, List<IJsonCheckItem>>(d => d.CheckItems, (d, o) => d.CheckItems = o)},
					{"Id", new Property<IJsonCheckList, string>(d => d.Id, (d, o) => d.Id = o)},
					{"Name", new Property<IJsonCheckList, string>(d => d.Name, (d, o) => d.Name = o)},
					{"Position", new Property<IJsonCheckList, Position>(d => Position.GetPosition(d.Pos), (d, o) => d.Pos = Position.GetJson(o))},
				};
		}
		public CheckListContext(string id)
		{
			Data.Id = id;
		}

		public void Delete()
		{
			if (_deleted) return;

			var endpoint = EndpointFactory.Build(EntityRequestType.CheckList_Write_Delete, new Dictionary<string, object> {{"_id", Data.Id}});
			JsonRepository.Execute(TrelloAuthorization.Default, endpoint);

			_deleted = true;
		}

		protected override IJsonCheckList GetData()
		{
			try
			{
				var endpoint = EndpointFactory.Build(EntityRequestType.CheckList_Read_Refresh, new Dictionary<string, object> {{"_id", Data.Id}});
				var newData = JsonRepository.Execute<IJsonCheckList>(TrelloAuthorization.Default, endpoint);

				return newData;
			}
			catch (TrelloInteractionException e)
			{
				if (!e.IsNotFoundError()) throw;
				_deleted = true;
				return Data;
			}
		}
		protected override void SubmitData(IJsonCheckList json)
		{
			var endpoint = EndpointFactory.Build(EntityRequestType.CheckList_Write_Update, new Dictionary<string, object> {{"_id", Data.Id}});
			var newData = JsonRepository.Execute(TrelloAuthorization.Default, endpoint, json);
			Merge(newData);
		}
		protected override bool CanUpdate()
		{
			return !_deleted;
		}
	}
}
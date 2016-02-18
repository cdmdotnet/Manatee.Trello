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
 
	File Name:		CheckItemContext.cs
	Namespace:		Manatee.Trello.Internal.Synchronization
	Class Name:		CheckItemContext
	Purpose:		Provides a data context for a check item.

***************************************************************************************/

using System.Collections.Generic;
using Manatee.Trello.Exceptions;
using Manatee.Trello.Internal.DataAccess;
using Manatee.Trello.Json;

namespace Manatee.Trello.Internal.Synchronization
{
	internal class CheckItemContext : SynchronizationContext<IJsonCheckItem>
	{
		private readonly string _ownerId;
		private bool _deleted;

		static CheckItemContext()
		{
			_properties = new Dictionary<string, Property<IJsonCheckItem>>
				{
					{"Id", new Property<IJsonCheckItem, string>((d, a) => d.Id, (d, o) => d.Id = o)},
					{"Name", new Property<IJsonCheckItem, string>((d, a) => d.Name, (d, o) => d.Name = o)},
					{"Position", new Property<IJsonCheckItem, Position>((d, a) => Position.GetPosition(d.Pos), (d, o) => d.Pos = Position.GetJson(o))},
					{"State", new Property<IJsonCheckItem, CheckItemState?>((d, a) => d.State, (d, o) => d.State = o)},
				};
		}
		public CheckItemContext(string id, string ownerId, TrelloAuthorization auth)
			: base(auth)
		{
			_ownerId = ownerId;
			Data.Id = id;
		}

		public void Delete()
		{
			if (_deleted) return;
			CancelUpdate();

			var endpoint = EndpointFactory.Build(EntityRequestType.CheckItem_Write_Delete, new Dictionary<string, object> {{"_checklistId", _ownerId}, {"_id", Data.Id}});
			JsonRepository.Execute(Auth, endpoint);

			_deleted = true;
		}

		protected override IJsonCheckItem GetData()
		{
			try
			{
				var endpoint = EndpointFactory.Build(EntityRequestType.CheckItem_Read_Refresh, new Dictionary<string, object> {{"_checklistId", _ownerId}, {"_id", Data.Id}});
				var newData = JsonRepository.Execute<IJsonCheckItem>(Auth, endpoint);
				MarkInitialized();

				return newData;
			}
			catch (TrelloInteractionException e)
			{
				if (!e.IsNotFoundError() || !IsInitialized) throw;
				_deleted = true;
				return Data;
			}
		}
		protected override void SubmitData(IJsonCheckItem json)
		{
			// Checklist should be downloaded already since CheckItem ctor is internal,
			// but allow for the case where it has not been anyway.
			var checkList = TrelloConfiguration.Cache.Find<CheckList>(cl => cl.Id == _ownerId) ?? new CheckList(_ownerId);
			// This may make a call to get the card, but it can't be avoided.  We need its ID.
			var endpoint = EndpointFactory.Build(EntityRequestType.CheckItem_Write_Update, new Dictionary<string, object>
				{
					{"_cardId", checkList.Card.Id},
					{"_checklistId", _ownerId},
					{"_id", Data.Id},
				});
			var newData = JsonRepository.Execute(Auth, endpoint, json);
			Merge(newData);
		}
		protected override bool CanUpdate()
		{
			return !_deleted;
		}
	}
}
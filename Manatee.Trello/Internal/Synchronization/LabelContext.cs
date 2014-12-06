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
 
	File Name:		LabelContext.cs
	Namespace:		Manatee.Trello.Internal.Synchronization
	Class Name:		LabelContext
	Purpose:		Provides a data context for a label.

***************************************************************************************/

using System.Collections.Generic;
using Manatee.Trello.Internal.Caching;
using Manatee.Trello.Internal.DataAccess;
using Manatee.Trello.Json;

namespace Manatee.Trello.Internal.Synchronization
{
	internal class LabelContext : SynchronizationContext<IJsonLabel>
	{
		private bool _deleted;

		static LabelContext()
		{
			_properties = new Dictionary<string, Property<IJsonLabel>>
				{
					{
						"Board", new Property<IJsonLabel, Board>(d => d.Board == null
																		  ? null
																		  : d.Board.GetFromCache<Board>(),
																 (d, o) => d.Board = o != null ? o.Json : null)
					},
					{"Color", new Property<IJsonLabel, LabelColor?>(d => d.Color, (d, o) => d.Color = o)},
					{"Id", new Property<IJsonLabel, string>(d => d.Id, (d, o) => d.Id = o)},
					{"Name", new Property<IJsonLabel, string>(d => d.Name, (d, o) => d.Name = o)},
					{"Uses", new Property<IJsonLabel, int?>(d => d.Uses, (d, o) => d.Uses = o)},
				};
		}
		public LabelContext(string id)
		{
			Data.Id = id;
		}

		public void Delete()
		{
			if (_deleted) return;
			CancelUpdate();

			var endpoint = EndpointFactory.Build(EntityRequestType.Label_Write_Delete, new Dictionary<string, object> {{"_id", Data.Id}});
			JsonRepository.Execute(TrelloAuthorization.Default, endpoint);

			_deleted = true;
		}

		protected override IJsonLabel GetData()
		{
			var endpoint = EndpointFactory.Build(EntityRequestType.Label_Read_Refresh, new Dictionary<string, object> {{"_boardId", Data.Board.Id}, {"_id", Data.Id}});
			var newData = JsonRepository.Execute<IJsonLabel>(TrelloAuthorization.Default, endpoint);

			return newData;
		}
		protected override void SubmitData(IJsonLabel json)
		{
			var endpoint = EndpointFactory.Build(EntityRequestType.Label_Write_Update, new Dictionary<string, object> {{"_boardId", Data.Board.Id}, {"_id", Data.Id}});
			var newData = JsonRepository.Execute(TrelloAuthorization.Default, endpoint, json);
			Merge(newData);
		}
	}
}
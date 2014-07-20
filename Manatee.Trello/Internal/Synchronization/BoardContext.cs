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
 
	File Name:		BoardContext.cs
	Namespace:		Manatee.Trello.Internal.Synchronization
	Class Name:		BoardContext
	Purpose:		Provides a data context for a board.

***************************************************************************************/

using System.Collections.Generic;
using System.Linq;
using Manatee.Trello.Internal.DataAccess;
using Manatee.Trello.Json;

namespace Manatee.Trello.Internal.Synchronization
{
	internal class BoardContext : SynchronizationContext<IJsonBoard>
	{
		public LabelNamesContext LabelNamesContext { get; private set; }
		public BoardPreferencesContext BoardPreferencesContext { get; private set; }

		static BoardContext()
		{
			_properties = new Dictionary<string, Property<IJsonBoard>>
				{
					{"Description", new Property<IJsonBoard, string>(d => d.Desc, (d, o) => d.Desc = o)},
					{"Id", new Property<IJsonBoard, string>(d => d.Id, (d, o) => d.Id = o)},
					{"IsClosed", new Property<IJsonBoard, bool?>(d => d.Closed, (d, o) => d.Closed = o)},
					{"IsSubscribed", new Property<IJsonBoard, bool?>(d => d.Subscribed, (d, o) => d.Subscribed = o)},
					{"Name", new Property<IJsonBoard, string>(d => d.Name, (d, o) => d.Name = o)},
					{
						"Organization", new Property<IJsonBoard, Organization>(d => d.Organization == null
							                                                            ? null
							                                                            : TrelloConfiguration.Cache.Find<Organization>(b => b.Id == d.Organization.Id) ??
							                                                              new Organization(d.Organization, true),
						                                                       (d, o) => d.Organization = o != null ? o.Json : null)
					},
					{"Url", new Property<IJsonBoard, string>(d => d.Url, (d, o) => d.Url = o)},
				};
		}
		public BoardContext(string id)
		{
			Data.Id = id;
			LabelNamesContext = new LabelNamesContext();
			LabelNamesContext.SynchronizeRequested += () => Synchronize();
			LabelNamesContext.SubmitRequested += ResetTimer;
			Data.LabelNames = LabelNamesContext.Data;
			BoardPreferencesContext = new BoardPreferencesContext();
			BoardPreferencesContext.SynchronizeRequested += () => Synchronize();
			BoardPreferencesContext.SubmitRequested += ResetTimer;
			Data.Prefs = BoardPreferencesContext.Data;
		}

		protected override IJsonBoard GetData()
		{
			var endpoint = EndpointFactory.Build(EntityRequestType.Board_Read_Refresh, new Dictionary<string, object> {{"_id", Data.Id}});
			var newData = JsonRepository.Execute<IJsonBoard>(TrelloAuthorization.Default, endpoint);

			return newData;
		}
		protected override void SubmitData()
		{
			var endpoint = EndpointFactory.Build(EntityRequestType.Board_Write_Update, new Dictionary<string, object> {{"_id", Data.Id}});
			JsonRepository.Execute(TrelloAuthorization.Default, endpoint, Data);
		}
		protected override IEnumerable<string> MergeDependencies(IJsonBoard json)
		{
			return LabelNamesContext.Merge(json.LabelNames)
			                        .Concat(BoardPreferencesContext.Merge(json.Prefs));
		}
	}
}
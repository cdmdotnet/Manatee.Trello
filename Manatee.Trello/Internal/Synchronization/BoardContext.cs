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
 
	File Name:		BoardContext.cs
	Namespace:		Manatee.Trello.Internal.Synchronization
	Class Name:		BoardContext
	Purpose:		Provides a data context for a board.

***************************************************************************************/

using System.Collections.Generic;
using Manatee.Trello.Internal.Caching;
using Manatee.Trello.Internal.DataAccess;
using Manatee.Trello.Internal.Validation;
using Manatee.Trello.Json;

namespace Manatee.Trello.Internal.Synchronization
{
	internal class BoardContext : SynchronizationContext<IJsonBoard>
	{
		public BoardPreferencesContext BoardPreferencesContext { get; private set; }
		public virtual bool HasValidId => IdRule.Instance.Validate(Data.Id, null) == null;

		static BoardContext()
		{
			_properties = new Dictionary<string, Property<IJsonBoard>>
				{
					{"Description", new Property<IJsonBoard, string>((d, a) => d.Desc, (d, o) => d.Desc = o)},
					{"Id", new Property<IJsonBoard, string>((d, a) => d.Id, (d, o) => d.Id = o)},
					{"IsClosed", new Property<IJsonBoard, bool?>((d, a) => d.Closed, (d, o) => d.Closed = o)},
					{"IsSubscribed", new Property<IJsonBoard, bool?>((d, a) => d.Subscribed, (d, o) => d.Subscribed = o)},
					{"Name", new Property<IJsonBoard, string>((d, a) => d.Name, (d, o) => d.Name = o)},
					{
						"Organization", new Property<IJsonBoard, Organization>((d, a) => d.Organization?.GetFromCache<Organization>(a),
																			   (d, o) => d.Organization = o?.Json)
					},
					{"Preferences", new Property<IJsonBoard, IJsonBoardPreferences>((d, a) => d.Prefs, (d, o) => d.Prefs = o)},
					{"Url", new Property<IJsonBoard, string>((d, a) => d.Url, (d, o) => d.Url = o)},
				};
		}
		public BoardContext(string id, TrelloAuthorization auth)
			: base(auth)
		{
			Data.Id = id;
			BoardPreferencesContext = new BoardPreferencesContext(Auth);
			BoardPreferencesContext.SynchronizeRequested += () => Synchronize();
			BoardPreferencesContext.SubmitRequested += () => HandleSubmitRequested("Preferences");
			Data.Prefs = BoardPreferencesContext.Data;
		}

		protected override IJsonBoard GetData()
		{
			var endpoint = EndpointFactory.Build(EntityRequestType.Board_Read_Refresh, new Dictionary<string, object> {{"_id", Data.Id}});
			var newData = JsonRepository.Execute<IJsonBoard>(Auth, endpoint);

			return newData;
		}
		protected override void SubmitData(IJsonBoard json)
		{
			var endpoint = EndpointFactory.Build(EntityRequestType.Board_Write_Update, new Dictionary<string, object> {{"_id", Data.Id}});
			var newData = JsonRepository.Execute(Auth, endpoint, json);
			Merge(newData);
		}
		protected override void ApplyDependentChanges(IJsonBoard json)
		{
			if (json.Prefs != null)
			{
				json.Prefs = BoardPreferencesContext.GetChanges();
				BoardPreferencesContext.ClearChanges();
			}
		}
		protected override IEnumerable<string> MergeDependencies(IJsonBoard json)
		{
			return BoardPreferencesContext.Merge(json.Prefs);
		}
		protected override bool IsDataComplete => !Data.Name.IsNullOrWhiteSpace();
	}
}
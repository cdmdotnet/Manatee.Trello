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
 
	File Name:		BoardPersonalPreferencesContext.cs
	Namespace:		Manatee.Trello.Internal.Synchronization
	Class Name:		BoardPersonalPreferencesContext
	Purpose:		Provides a data context for a boards personal preferences.

***************************************************************************************/
using System.Collections.Generic;
using Manatee.Trello.Internal.Caching;
using Manatee.Trello.Internal.DataAccess;
using Manatee.Trello.Json;

namespace Manatee.Trello.Internal.Synchronization
{
	internal class BoardPersonalPreferencesContext : SynchronizationContext<IJsonBoardPersonalPreferences>
	{
		private readonly string _ownerId;

		static BoardPersonalPreferencesContext()
		{
			_properties = new Dictionary<string, Property<IJsonBoardPersonalPreferences>>
				{
					{"ShowSidebar", new Property<IJsonBoardPersonalPreferences, bool?>((d, a) => d.ShowSidebar, (d, o) => d.ShowSidebar = o)},
					{"ShowSidebarMembers", new Property<IJsonBoardPersonalPreferences, bool?>((d, a) => d.ShowSidebarMembers, (d, o) => d.ShowSidebarMembers = o)},
					{"ShowSidebarBoardActions", new Property<IJsonBoardPersonalPreferences, bool?>((d, a) => d.ShowSidebarBoardActions, (d, o) => d.ShowSidebarBoardActions = o)},
					{"ShowSidebarActivity", new Property<IJsonBoardPersonalPreferences, bool?>((d, a) => d.ShowSidebarActivity, (d, o) => d.ShowSidebarActivity = o)},
					{"ShowListGuide", new Property<IJsonBoardPersonalPreferences, bool?>((d, a) => d.ShowListGuide, (d, o) => d.ShowListGuide = o)},
					{
						"EmailPosition", new Property<IJsonBoardPersonalPreferences, Position>((d, a) => Position.GetPosition(d.EmailPosition),
																							   (d, o) => d.EmailPosition = Position.GetJson(o))
					},
					{
						"EmailListId", new Property<IJsonBoardPersonalPreferences, List>((d, a) => d.EmailList?.GetFromCache<List>(a),
																						 (d, o) => d.EmailList = o?.Json)
					},
				};
		}
		public BoardPersonalPreferencesContext(string ownerId, TrelloAuthorization auth)
			: base(auth)
		{
			_ownerId = ownerId;
		}

		protected override IJsonBoardPersonalPreferences GetData()
		{
			var endpoint = EndpointFactory.Build(EntityRequestType.Board_Read_PersonalPrefs, new Dictionary<string, object> {{"_id", _ownerId}});
			var newData = JsonRepository.Execute<IJsonBoardPersonalPreferences>(TrelloAuthorization.Default, endpoint);

			return newData;
		}
		protected override void SubmitData(IJsonBoardPersonalPreferences json)
		{
			if (json.EmailList != null)
				SubmitDataPoint(json.EmailList.Id, "emailList");
			SubmitDataPoint(json.EmailPosition, "emailPosition");
			SubmitDataPoint(json.ShowListGuide, "showListGuide");
			SubmitDataPoint(json.ShowSidebar, "showSidebar");
			SubmitDataPoint(json.ShowSidebarActivity, "showSidebarActivity");
			SubmitDataPoint(json.ShowSidebarBoardActions, "showSidebarBoardActions");
			SubmitDataPoint(json.ShowSidebarMembers, "showSidebarMembers");
		}

		private void SubmitDataPoint<T>(T value, string segment)
		{
			if (Equals(value, default(T))) return;

			var json = TrelloConfiguration.JsonFactory.Create<IJsonParameter>();
			if (typeof (T) == typeof (bool?))
				json.Boolean = (bool?) (object) value;
			else
				json.String = value.ToString();

			var endpoint = EndpointFactory.Build(EntityRequestType.Board_Write_PersonalPrefs, new Dictionary<string, object> {{"_id", _ownerId}});
			endpoint.AddSegment(segment);
			JsonRepository.Execute(Auth, endpoint, json);
		}
	}
}
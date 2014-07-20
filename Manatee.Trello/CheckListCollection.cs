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
 
	File Name:		CheckListCollection.cs
	Namespace:		Manatee.Trello
	Class Name:		CheckListCollection
	Purpose:		Represents a collection of CheckLists.

***************************************************************************************/

using System.Collections.Generic;
using System.Linq;
using Manatee.Trello.Internal;
using Manatee.Trello.Internal.DataAccess;
using Manatee.Trello.Json;

namespace Manatee.Trello
{
	public class ReadOnlyCheckListCollection : ReadOnlyCollection<CheckList>
	{
		internal ReadOnlyCheckListCollection(string ownerId)
			: base(ownerId) {}

		protected override sealed void Update()
		{
			var endpoint = EndpointFactory.Build(EntityRequestType.CheckList_Read_Refresh, new Dictionary<string, object> {{"_id", OwnerId}});
			var newData = JsonRepository.Execute<List<IJsonCheckList>>(TrelloAuthorization.Default, endpoint);

			Items.Clear();
			Items.AddRange(newData.Select(jc => TrelloConfiguration.Cache.Find<CheckList>(b => b.Id == jc.Id) ?? new CheckList(jc, true)));
		}
	}

	public class CheckListCollection : ReadOnlyCheckListCollection
	{
		internal CheckListCollection(string ownerId)
			: base(ownerId) {}

		public CheckList Add(string name)
		{
			var json = TrelloConfiguration.JsonFactory.Create<IJsonCheckList>();
			json.Name = name;

			var endpoint = EndpointFactory.Build(EntityRequestType.Card_Write_AddChecklist);
			var newData = JsonRepository.Execute(TrelloAuthorization.Default, endpoint, json);

			return new CheckList(newData, true);
		}
	}
}
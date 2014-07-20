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
 
	File Name:		CheckItemCollection.cs
	Namespace:		Manatee.Trello
	Class Name:		CheckItemCollection
	Purpose:		Represents a collection of CheckItems.

***************************************************************************************/

using System.Collections.Generic;
using System.Linq;
using Manatee.Trello.Internal;
using Manatee.Trello.Internal.DataAccess;
using Manatee.Trello.Json;

namespace Manatee.Trello
{
	public class ReadOnlyCheckItemCollection : ReadOnlyCollection<CheckItem>
	{
		internal ReadOnlyCheckItemCollection(string ownerId)
			: base(ownerId) {}

		protected override sealed void Update()
		{
			var endpoint = EndpointFactory.Build(EntityRequestType.CheckItem_Read_Refresh, new Dictionary<string, object> {{"_id", OwnerId}});
			var newData = JsonRepository.Execute<List<IJsonCheckItem>>(TrelloAuthorization.Default, endpoint);

			Items.Clear();
			Items.AddRange(newData.Select(jc => TrelloConfiguration.Cache.Find<CheckItem>(b => b.Id == jc.Id) ?? new CheckItem(jc, OwnerId, true)));
		}
	}

	public class CheckItemCollection : ReadOnlyCheckItemCollection
	{
		internal CheckItemCollection(string ownerId)
			: base(ownerId) {}

		public CheckItem Add(string name)
		{
			var json = TrelloConfiguration.JsonFactory.Create<IJsonCheckItem>();
			json.Name = name;

			var endpoint = EndpointFactory.Build(EntityRequestType.CheckList_Write_AddCheckItem, new Dictionary<string, object> {{"_id", OwnerId}});
			var newData = JsonRepository.Execute(TrelloAuthorization.Default, endpoint, json);

			return new CheckItem(newData, OwnerId, true);
		}
	}
}
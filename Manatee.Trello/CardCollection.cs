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
 
	File Name:		CardCollection.cs
	Namespace:		Manatee.Trello
	Class Name:		CardCollection
	Purpose:		Represents a collection of cards.

***************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using Manatee.Trello.Enumerations;
using Manatee.Trello.Internal;
using Manatee.Trello.Internal.DataAccess;
using Manatee.Trello.Json;

namespace Manatee.Trello
{
	public class ReadOnlyCardCollection : ReadOnlyCollection<Card>
	{
		private readonly EntityRequestType _updateRequestType;

		internal ReadOnlyCardCollection(Type type, string ownerId)
			: base(ownerId)
		{
			_updateRequestType = type == typeof (List)
				                     ? EntityRequestType.List_Read_Cards
				                     : EntityRequestType.Board_Read_Cards;
		}

		protected override sealed void Update()
		{
			var endpoint = EndpointFactory.Build(_updateRequestType, new Dictionary<string, object> {{"_id", OwnerId}});
			var newData = JsonRepository.Execute<List<IJsonCard>>(TrelloAuthorization.Default, endpoint);

			Items.Clear();
			Items.AddRange(newData.Select(jc => TrelloConfiguration.Cache.Find<Card>(c => c.Id == jc.Id) ?? new Card(jc, true)));
		}
	}

	public class CardCollection : ReadOnlyCardCollection
	{
		internal CardCollection(string ownerId)
			: base(typeof (List), ownerId) {}

		public Card Add(string name)
		{
			var json = TrelloConfiguration.JsonFactory.Create<IJsonCard>();
			json.Name = name;
			json.List = TrelloConfiguration.JsonFactory.Create<IJsonList>();
			json.List.Id = OwnerId;

			var endpoint = EndpointFactory.Build(EntityRequestType.List_Write_AddCard);
			var newData = JsonRepository.Execute(TrelloAuthorization.Default, endpoint, json);

			return new Card(newData, true);
		}
	}
}